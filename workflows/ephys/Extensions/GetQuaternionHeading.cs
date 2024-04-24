using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Numerics;

[Combinator]
[Description("Calculates the heading angle for each quaternion in the sequence.")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class GetQuaternionHeading
{
    public IObservable<double> Process(IObservable<Quaternion> source)
    {
        return source.Select(value =>
        {
            var q0 = value.W;
            var q1 = value.X;
            var q2 = value.Y;
            var q3 = value.Z;
            
            var norm = q0 * q0 + q1 * q1 + q2 * q2 + q3 * q3;
            var sing = q1 * q2 + q0 * q3;
            if (sing > 0.499 * norm)
            {
                return 2 * Math.Atan2(q1, q0);
            }
            else if (sing < -0.499*norm)
            {
                return -2 * Math.Atan2(q1, q0);
            }
            else return Math.Atan2(
                2 * (q0 * q3 + q1 * q2),
                1 - 2 * (q2*q2 + q3*q3));
        });
    }
}
