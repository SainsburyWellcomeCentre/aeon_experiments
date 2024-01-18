using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Bonsai.Design;
using ZedGraph;
using Bonsai.Design.Visualizers;
using System.Windows.Forms;
using Bonsai.Expressions;
using System.Reactive.Linq;
using System.Linq;

[Combinator]
[Description("Display individual labeled measurements in a line plot.")]
[WorkflowElementCategory(ElementCategory.Combinator)]
[WorkflowElementIcon("Bonsai:ElementIcon.Visualizer")]
[TypeVisualizer(typeof(LabeledSeriesVisualizer))]
public class LabeledSeriesVisualizerBuilder
{
    public int Capacity { get; set; }

    public IObservable<LabeledMeasurement[]> Process(IObservable<Tuple<double, string>[]> source)
    {
        return source.Select(xs => Array.ConvertAll(xs, x => new LabeledMeasurement(x.Item1, x.Item2)));
    }

    public IObservable<LabeledMeasurement[]> Process(IObservable<LabeledMeasurement[]> source)
    {
        return source;
    }
}

public class LabeledSeriesVisualizer : DialogTypeVisualizer
{
    int capacity;
    GraphControl graph;
    Dictionary<string, IPointListEdit> seriesMap;

    public override void Load(IServiceProvider provider)
    {
        var context = (ITypeVisualizerContext)provider.GetService(typeof(ITypeVisualizerContext));
        var visualizerElement = ExpressionBuilder.GetVisualizerElement(context.Source);
        var builder = (LabeledSeriesVisualizerBuilder)ExpressionBuilder.GetWorkflowElement(visualizerElement);
        capacity = builder.Capacity;

        graph = new GraphControl();
        graph.Dock = DockStyle.Fill;
        var timeAxis = graph.GraphPane.XAxis;
        timeAxis.Type = AxisType.Date;
        timeAxis.Scale.Format = "HH:mm:ss";
        timeAxis.Scale.MajorUnit = DateUnit.Second;
        timeAxis.Scale.MinorUnit = DateUnit.Millisecond;
        timeAxis.MinorTic.IsAllTics = false;

        seriesMap = new Dictionary<string, IPointListEdit>();
        var visualizerService = (IDialogTypeVisualizerService)provider.GetService(typeof(IDialogTypeVisualizerService));
        if (visualizerService != null)
        {
            visualizerService.AddControl(graph);
        }
    }

    private void ShowMeasurement(DateTime timestamp, LabeledMeasurement measurement)
    {
        IPointListEdit series;
        if (!seriesMap.TryGetValue(measurement.Name, out series))
        {
            series = capacity > 0 ? (IPointListEdit)new RollingPointPairList(capacity) : new PointPairList();
            var curve = graph.GraphPane.AddCurve(measurement.Name, series, graph.GetNextColor(), SymbolType.Circle);
            curve.Line.IsOptimizedDraw = true;
            curve.Symbol.Fill.Type = FillType.Solid;
            seriesMap.Add(measurement.Name, series);
        }

        series.Add((XDate)timestamp, measurement.Value);
    }

    public override void Show(object value)
    {
        var dateTime = DateTime.UtcNow;
        var array = value as LabeledMeasurement[];
        if (array != null)
        {
            for (int i = 0; i < array.Length; i++)
            {
                ShowMeasurement(dateTime, array[i]);
            }
        }
        else ShowMeasurement(dateTime, (LabeledMeasurement)value);
        graph.Invalidate();
    }

    public override void Unload()
    {
        graph.Dispose();
    }
}

public struct LabeledMeasurement
{
    public double Value;
    public string Name;

    public LabeledMeasurement(double value, string name)
    {
        Value = value;
        Name = name;
    }
}
