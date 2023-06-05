using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using MathNet.Numerics.Distributions;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ConvertToPatchState
{
    public IObservable<PatchState> Process(IObservable<Tuple<double,double,double>> source)
    {
        return source.Select(value => 
        {
            return new PatchState(){ 
                Threshold = value.Item1, 
                D1 = value.Item2,
                Delta = value.Item3
            };
        });
    }
}
