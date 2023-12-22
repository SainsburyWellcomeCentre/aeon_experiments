using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Harp;
using Bonsai.Sleap;

[Combinator]
[Description("Extracts the subject with the specified identity from the list of active poses.")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class GetActiveSubject
{
    [Description("The identity of the pose to retrieve.")]
    public string Identity { get; set; }

    public IObservable<Timestamped<PoseIdentity>> Process(IObservable<Timestamped<IList<PoseIdentity>>> source)
    {
        return source.Select(timestampedPoses => Timestamped.Create(
            timestampedPoses.Value.First(pose => pose.Identity == Identity),
            timestampedPoses.Seconds));
    }
}
