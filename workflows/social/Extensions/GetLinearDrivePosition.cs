using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Vision;
using OpenCV.Net;

[Combinator]
[Description("Calculates the normalized horizontal position of the linear drive, where zero is the position where" +
             "tether is fully retracted and one is the position where tether is fully extended.")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class GetLinearDrivePosition
{
    public GetLinearDrivePosition()
    {
        ArenaRadiusPixels = 500;
        ArenaRadiusCentimeters = 100;
        TetherGuideAltitude = 129.5;
        TetherGuideHeight = 17;
        TetherMaxSlackLength = 58.523;
        LinearRailLength = 56;
    }

    [Description("The coordinates of the arena center, in pixels.")]
    public Point ArenaCenter { get; set; }

    [Description("The radius of the circular arena, in pixels.")]
    public double ArenaRadiusPixels { get; set; }

    [Description("The radius of the circular arena, in centimeters.")]
    public double ArenaRadiusCentimeters { get; set; }

    [Description("Distance from arena floor to the bottom of the tether guide chute, in centimeters.")]
    public double TetherGuideAltitude { get; set; }

    [Description("Vertical distance from the bottom of the tether guide to the tether origin, in centimeters.")]
    public double TetherGuideHeight { get; set; }

    [Description("Maximum length of slack in the tether that we can allow, in centimeters.")]
    public double TetherMaxSlackLength { get; set; }

    [Description("Length of the linear drive rail, in centimeters.")]
    public double LinearRailLength { get; set; }

    [Description("Horizontal distance between center of the arena and the plumb line from the bottom of the tether guide, in centimeters.")]
    public double RadialDistanceOffset { get; set; }

    public IObservable<double> Process(IObservable<ConnectedComponentCollection> source)
    {
        return source.Select(value =>
        {
            var imageSize = value.ImageSize;
            if (value.Count != 1) return double.NaN;

            var center = ArenaCenter;
            var tetherGuideAltitude = TetherGuideAltitude; // c2
            var tetherGuideHeight = TetherGuideHeight;
            var pixelsToCentimeters = ArenaRadiusCentimeters / ArenaRadiusPixels;
            var centroid = value[0].Centroid;
            centroid.X -= center.X;
            centroid.Y -= center.Y;
            var radialDistance = Math.Sqrt(centroid.X * centroid.X + centroid.Y * centroid.Y) * pixelsToCentimeters + RadialDistanceOffset; // c1
            var distanceToTetherGuide = Math.Sqrt(radialDistance * radialDistance + tetherGuideAltitude * tetherGuideAltitude);
            var tetherGuideSlack = distanceToTetherGuide - tetherGuideAltitude; // D
            var remainingSlack = TetherMaxSlackLength - tetherGuideSlack;
            var linearMotorPosition = Math.Sqrt(remainingSlack * remainingSlack - tetherGuideHeight * tetherGuideHeight);
            return linearMotorPosition / LinearRailLength;
        });
    }
}