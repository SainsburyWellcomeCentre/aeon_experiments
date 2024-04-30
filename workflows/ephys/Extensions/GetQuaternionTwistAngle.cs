using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Numerics;

[Combinator]
[Description("Calculates the twist angle about the specified axis for each quaternion in the sequence.")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class GetQuaternionTwistAngle
{
    public GetQuaternionTwistAngle()
    {
        Direction = Vector3.UnitZ;
    }

    [TypeConverter(typeof(NumericRecordConverter))]
    [Description("The direction vector around which to calculate the twist component.")]
    public Vector3 Direction { get; set; }

    public IObservable<double> Process(IObservable<Quaternion> source)
    {
        // project rotation axis onto the direction axis
        return source.Select(rotation =>
        {
            var direction = Direction;
            var rotationAxis = new Vector3(rotation.X, rotation.Y, rotation.Z);
            var dotProduct = Vector3.Dot(rotationAxis, direction);
            var projection = dotProduct / Vector3.Dot(direction, direction) * direction;
            var twist = new Quaternion(projection, rotation.W);
            twist = Quaternion.Normalize(twist);
            if (dotProduct < 0) // account for angle-axis flipping
            {
                twist = -twist;
            }

            return 2 * Math.Acos(twist.W);
        });
    }
}
