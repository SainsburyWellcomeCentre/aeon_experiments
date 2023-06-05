using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class CreatePatchState
{
    public IObservable<PatchState> Process()
    {
        return Observable.Return(new PatchState());
    }
}
public struct PatchState 
{
    public double Threshold;
    public double D1;
    public double Delta;
}