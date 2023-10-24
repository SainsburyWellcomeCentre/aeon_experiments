using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ConvertToBytes
{
    public IObservable<byte[]> Process(IObservable<ulong> source)
    {
        return source.Select(value => BitConverter.GetBytes(value));
    }

    public IObservable<byte[]> Process(IObservable<int> source)
    {
        return source.Select(value => BitConverter.GetBytes(value));
    }
}
