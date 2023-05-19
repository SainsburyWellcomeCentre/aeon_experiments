using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Harp;
using Aeon.Acquisition;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ConvertToWeight
{
    public string Key { get; set; }
    public IObservable<WeightMeasurement> Process(IObservable<Timestamped<WeightMeasurement>> source)
    {
        return source.Select(value => new WeightMeasurement(value.Seconds, value.Value.Value, value.Value.Confidence));
    }
    public IObservable<WeightMeasurement> Process(IObservable<Timestamped<Tuple<string, string>>> source)
    {
        return source.Select(value => new WeightMeasurement(value.Seconds, float.Parse(value.Value.Item1), value.Value.Item2.Contains("?") ? 0f : 1f));
    }
    public IObservable<WeightMeasurement> Process(IObservable<Tuple<Tuple<string, string>,double>> source)
    {
        return source.Select(value => new WeightMeasurement(value.Item2, float.Parse(value.Item1.Item1), value.Item1.Item2.Contains("?") ? 0f : 1f));
    }
}   
