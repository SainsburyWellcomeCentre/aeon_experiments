using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]                   
public class MotorFeedback
{
    public IObservable<double> Process(IObservable<Tuple<double, double>> source)
    {
        return source.Select(value =>
        {
            var last = value.Item2;
            var curr = value.Item1;
            var a1 = curr + 2 * Math.PI;
            var a2 = curr - 2 * Math.PI;
            var pos = new double[] { curr-last, a1-last, a2-last };
            var index =0;
            var currMax = Math.PI;
            for(int count =0;  count< pos.Length; count ++)
            {
                if(Math.Abs(pos[count])< currMax)
                {
                    currMax = pos[count];
                    index =count;
                }

            }

          //  var p = pos.Min((x => Math.Abs(x - last));
            return (pos[index]) / (2 * Math.PI);
        });
    }
}
