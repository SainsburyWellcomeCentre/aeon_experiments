using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Bonsai.Harp;
using Aeon.Environment;
using Bonsai.Design;
using ZedGraph;
using Bonsai.Design.Visualizers;
using System.Windows.Forms;
using Bonsai.Expressions;
using System.Reactive.Linq;
using OpenCV.Net;
using Aeon.Acquisition;

[Combinator]
[Description("Display individual RFID tag measurements in a raster plot.")]
[WorkflowElementCategory(ElementCategory.Combinator)]
[WorkflowElementIcon("Bonsai:ElementIcon.Visualizer")]
[TypeVisualizer(typeof(RfidMeasurementVisualizer))]
public class RfidVisualizerBuilder
{
    public int Capacity { get; set; }

    public IObservable<RfidTaggedMeasurement> Process(IObservable<Timestamped<RfidMeasurement>> source)
    {
        return source.Select(x => new RfidTaggedMeasurement(
            x.Seconds,
            x.Value.Location,
            x.Value.TagId,
            x.Value.Location.Y.ToString(),
            (int)x.Value.Location.Y));
    }

    public IObservable<RfidTaggedMeasurement> Process(IObservable<RfidTaggedMeasurement> source)
    {
        return source;
    }
}

public class RfidMeasurementVisualizer : DialogTypeVisualizer
{
    int capacity;
    GraphControl graph;
    Dictionary<ulong, IPointListEdit> seriesMap;
    Dictionary<int, string> locationMap;

    public override void Load(IServiceProvider provider)
    {
        var context = (ITypeVisualizerContext)provider.GetService(typeof(ITypeVisualizerContext));
        var visualizerElement = ExpressionBuilder.GetVisualizerElement(context.Source);
        var builder = (RfidVisualizerBuilder)ExpressionBuilder.GetWorkflowElement(visualizerElement);
        capacity = builder.Capacity;

        graph = new GraphControl();
        graph.Dock = DockStyle.Fill;
        var locationAxis = graph.GraphPane.YAxis;
        locationAxis.Type = AxisType.Linear;
        locationAxis.Scale.MinAuto = false;
        locationAxis.Scale.MaxAuto = false;
        locationAxis.MinorTic.IsAllTics = false;
        locationAxis.MajorTic.IsBetweenLabels = false;
        locationAxis.ScaleFormatEvent += (g, axis, value, index) =>
        {
            string name;
            if (g.CurveList.Count == 0) return string.Empty;
            if (value % 1 == 0 && locationMap.TryGetValue((int)value, out name))
            {
                return name;
            }
            return string.Empty;
        };
        var timeAxis = graph.GraphPane.XAxis;
        timeAxis.Type = AxisType.Date;
        timeAxis.Scale.Format = "HH:mm:ss";
        timeAxis.Scale.MajorUnit = DateUnit.Second;
        timeAxis.Scale.MinorUnit = DateUnit.Millisecond;
        timeAxis.MinorTic.IsAllTics = false;

        seriesMap = new Dictionary<ulong, IPointListEdit>();
        locationMap = new Dictionary<int, string>();
        var visualizerService = (IDialogTypeVisualizerService)provider.GetService(typeof(IDialogTypeVisualizerService));
        if (visualizerService != null)
        {
            visualizerService.AddControl(graph);
        }
    }

    public override void Show(object value)
    {
        IPointListEdit series;
        var measurement = (RfidTaggedMeasurement)value;
        var tagId = measurement.TagId;
        if (!seriesMap.TryGetValue(tagId, out series))
        {
            series = capacity > 0 ? (IPointListEdit)new RollingPointPairList(capacity) : new PointPairList();
            var curve = graph.GraphPane.AddCurve(tagId.ToString(), series, graph.GetNextColor(), SymbolType.Circle);
            curve.Line.IsVisible = false;
            curve.Symbol.Fill.Type = FillType.Solid;
            seriesMap.Add(tagId, series);
        }

        var dateTime = GetDateTime.FromSeconds(measurement.Seconds);
        var location = measurement.Index;
        locationMap[location] = measurement.Name;
        series.Add((XDate)dateTime, location);
        var axis = graph.GraphPane.YAxis;
        if (location <= axis.Scale.Min) axis.Scale.Min = location - 1;
        if (location >= axis.Scale.Max) axis.Scale.Max = location + 1;
        graph.Invalidate();
    }

    public override void Unload()
    {
        graph.Dispose();
    }
}

public struct RfidTaggedMeasurement
{
    public double Seconds;
    public Point2f Location;
    public ulong TagId;
    public string Name;
    public int Index;

    public RfidTaggedMeasurement(double seconds, Point2f location, ulong tagId, string name, int index)
    {
        Seconds = seconds;
        Location = location;
        TagId = tagId;
        Name = name;
        Index = index;
    }
}
