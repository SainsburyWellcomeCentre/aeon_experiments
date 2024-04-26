using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("Normalizes the absolute angle value to the range -1 to 1.")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class NormalizeAngle
{
    public IObservable<double> Process(IObservable<float> source)
    {
        return source.Select(value =>
        {
            var rescale = (value + 3 * Math.PI) % (2 * Math.PI) - Math.PI;
            return rescale / (2 * Math.PI);
        });
    }
}
