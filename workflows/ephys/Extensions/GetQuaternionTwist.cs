using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Numerics;

[Combinator]
[Description("Calculates the twist component around the specified axis for each quaternion in the sequence.")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class GetQuaternionTwist
{
    public GetQuaternionTwist()
    {
        Direction = Vector3.UnitZ;
    }

    [TypeConverter(typeof(NumericRecordConverter))]
    [Description("The direction vector around which to calculate the twist component.")]
    public Vector3 Direction { get; set; }

    static float Dot(Vector3 a, Vector3 b)
    {
        return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    }

    public IObservable<Quaternion> Process(IObservable<Quaternion> source)
    {
        // project rotation axis onto the direction axis
        return source.Select(rotation =>
        {
            var direction = Direction;
            var rotationAxis = new Vector3(rotation.X, rotation.Y, rotation.Z);
            var projection = Dot(rotationAxis, direction) / Dot(direction, direction) * direction;
            var twist = new Quaternion(projection, rotation.W);
            twist = Quaternion.Normalize(twist);
            return twist;
        });
    }
}
