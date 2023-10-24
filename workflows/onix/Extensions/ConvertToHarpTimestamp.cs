using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ConvertToHarpTimestamp
{
    public IObservable<double> Process(IObservable<uint> source)
    {
        return source.Select(value => (double)(value + 1));
    }
}
