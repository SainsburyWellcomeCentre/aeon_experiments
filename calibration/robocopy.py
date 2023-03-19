import os
import sys
import subprocess
import argparse
from pathlib import Path

parser = argparse.ArgumentParser(description="Moves data files from the specified source folder to a remote storage folder.")
parser.add_argument('source', type=str, help="The path where the local data is stored.")
parser.add_argument('destination', type=str, help="The remote path where data is to be stored.")
args = parser.parse_args()
source = Path(args.source)
destination = Path(args.destination)
dataset = Path(os.getcwd()).name
robocopy_parameters = ["/E", "/MOVE", "/J", "/R:2", "/W:30"]
process = subprocess.run(
    ["robocopy", source.joinpath(dataset), destination.joinpath(dataset)] + robocopy_parameters,
    shell=True)
sys.exit(process.returncode)