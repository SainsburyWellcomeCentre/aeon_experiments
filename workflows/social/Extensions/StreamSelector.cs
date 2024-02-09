using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using Bonsai.Expressions;

[Combinator]
[Description("Selects a specific stream by name from a set of input streams.")]
[WorkflowElementCategory(ElementCategory.Combinator)]
[DefaultProperty("StreamNames")]
public class StreamSelector
{
    readonly Collection<string> streamNames = new Collection<string>();

    [TypeConverter(typeof(SelectedStreamConverter))]
    public string SelectedStream { get; set; }

    public Collection<string> StreamNames
    {
        get { return streamNames; }
    }

    public IObservable<TSource> Process<TSource>(params IObservable<TSource>[] source)
    {
        var filteredStreams = new IObservable<TSource>[source.Length];
        for (int i = 0; i < source.Length; i++)
        {
            var name = i < streamNames.Count ? streamNames[i] : string.Empty;
            filteredStreams[i] = source[i].Where(_ => name == SelectedStream);
        }

        return Observable.Merge(filteredStreams);
    }

    class SelectedStreamConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            StreamSelector selector = null;
            var workflowBuilder = (WorkflowBuilder)context.GetService(typeof(WorkflowBuilder));
            var descendants = workflowBuilder.Workflow.Descendants();
            foreach (var node in descendants)
            {
                selector = ExpressionBuilder.GetWorkflowElement(node) as StreamSelector;
                if (selector != null)
                {
                    break;
                }
            }

            return new StandardValuesCollection(selector.StreamNames);
        }
    }
}
