using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Harp;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class FormatLogMessage
{
    public IObservable<Timestamped<bool>> Process(IObservable<Timestamped<bool>> source)
    {
        return source.Select(value => value);
    }
}
