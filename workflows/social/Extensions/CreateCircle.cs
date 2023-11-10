using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using Bonsai.Vision;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateCircle
{
    [Editor("Bonsai.Vision.Design.IplImageCircleEditor, Bonsai.Vision.Design", DesignTypes.UITypeEditor)]
    public Circle Circle { get; set; }

    public IObservable<Circle> Process(IObservable<IplImage> source)
    {
        return source.Select(value => Circle);
    }
}
