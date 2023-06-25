from pathlib import Path
from lxml import etree
from git import Repo
import argparse
import json
import sys

parser = argparse.ArgumentParser(description="Exports device and experiment metadata for the specified workflow file.")
parser.add_argument('workflow', type=str, help="The path to the workflow file used for data acquisition.")
parser.add_argument('--indent', type=int, help="The optional indent level for JSON pretty printing.")
parser.add_argument('--strict', action="store_true", help="Optionally enforce exporting metadata only for clean repositories.")
parser.add_argument('--output', type=str, help="The optional path to the exported JSON file.")
args = parser.parse_args()
dname = Path(__file__).parent

repo = Repo(dname.parent)
if args.strict and (repo.is_dirty() or len(repo.untracked_files) > 0):
    parser.error("all modifications to the acquisition repository must be committed before exporting metadata")

ns = {
    'x' : 'https://bonsai-rx.org/2018/workflow',
    'xsi' : 'http://www.w3.org/2001/XMLSchema-instance'
}

def recursive_dict(element):
    ename = etree.QName(element)
    if "ArrayOf" in ename.localname:
        values = [value for name,value in map(recursive_dict, element)]
        return ename.localname, values
    return ename.localname, \
        dict(map(recursive_dict, element)) or element.text

def list_metadata(elements, **kwargs):
    metadata = {}
    for x in elements:
        grouptype = x.get(f'{{{ns["xsi"]}}}type')
        if grouptype == "GroupWorkflow":
            name = x.xpath('./x:Name', namespaces=ns)[0].text
            nodes = x.xpath('./x:Workflow/x:Nodes', namespaces=ns)[0]
            metadata[name] = list_metadata(nodes)
        elif grouptype != "ExternalizedMapping":
            elem_metadata = recursive_dict(x)[1]
            if elem_metadata is None:
                continue
            elem_metadata.update(kwargs)
            if grouptype == "IncludeWorkflow":
                path = Path(x.get('Path')).stem
                elem_metadata['Type'] = path.rsplit(':', 1)[-1]
            metadata |= elem_metadata
    return metadata

root = etree.parse(args.workflow)
workflow = root.xpath('/x:WorkflowBuilder/x:Workflow/x:Nodes', namespaces=ns)[0]
metadata_group = workflow.xpath('./x:Expression[@xsi:type="GroupWorkflow" and ./x:Name[text()="Metadata"]]/x:Workflow/x:Nodes', namespaces=ns)[0]
metadata = {
    'Workflow' : args.workflow,
    'Commit' : repo.head.commit.hexsha
}
metadata |= list_metadata(metadata_group)

if args.output:
    outpath = Path(args.output)
    outpath.parent.mkdir(parents=True, exist_ok=True)
    with open(outpath, "w") as outfile:
        json.dump(metadata, outfile, indent=args.indent)
else:
    json.dump(metadata, sys.stdout, indent=args.indent)