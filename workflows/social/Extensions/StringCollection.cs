using Bonsai;
using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Collections.ObjectModel;

[Combinator]
[Description("Initializes and returns a fixed collection of strings.")]
[WorkflowElementCategory(ElementCategory.Source)]
public class StringCollection
{
    readonly Collection<string> values = new Collection<string>();

    [Description("The list of strings to include in the collection of strings.")]
    public Collection<string> Values
    {
        get { return values; }
    }

    public IObservable<string[]> Process()
    {
        return Observable.Defer(() =>
        {
            var result = new string[Values.Count];
            Values.CopyTo(result, 0);
            return Observable.Return(result);
        });
    }
}
