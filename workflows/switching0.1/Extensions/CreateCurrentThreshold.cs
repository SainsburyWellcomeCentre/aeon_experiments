using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateCurrentThreshold
{
    public IObservable<CurrentThresholdState> Process(IObservable<Tuple<PatchState, int>> source)
    {
    return source.Select(value => new CurrentThresholdState(){ Threshold = value.Item1.Threshold, D0 = value.Item1.D1, Rate = value.Item1.Delta, DeliveryCount = value.Item2});
    }
}

public struct CurrentThresholdState 
{
    public double Threshold;
    public double D0;
    public double Rate;
    public int DeliveryCount;
}