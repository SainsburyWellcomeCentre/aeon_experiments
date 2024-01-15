using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Bonsai.Sleap;
using Bonsai.Sleap.Design;
using OpenCV.Net;

[Combinator]
[Description("Provides a type visualizer that draws a visual representation of active subjects.")]
[TypeVisualizer(typeof(ActiveSubjectVisualizer))]
[WorkflowElementIcon("Bonsai:ElementIcon.Visualizer")]
[WorkflowElementCategory(ElementCategory.Combinator)]
public class ActiveSubjectVisualizerBuilder
{
    public IObservable<Tuple<IList<PoseIdentity>, IplImage>> Process(IObservable<Tuple<IList<PoseIdentity>, IplImage>> source)
    {
        return source;
    }
}

public class ActiveSubjectVisualizer : PoseIdentityCollectionVisualizer
{
    public override void Show(object value)
    {
        var sourceValue = (Tuple<IList<PoseIdentity>, IplImage>)value;
        var image = sourceValue.Item2;
        var poseIdentities = new PoseIdentityCollection(image);
        foreach (var pose in sourceValue.Item1)
        {
            poseIdentities.Add(pose);
        }
        base.Show(poseIdentities);
    }
}
