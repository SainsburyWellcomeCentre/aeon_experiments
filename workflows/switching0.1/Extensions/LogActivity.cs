using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Harp;

[Combinator]
[Description("Looks for the result of activity and outputs 0 if no activity or index of theat stream id  +1 in the Values array")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class LogActivity
{

    //public KeyValuePair<string,int> [] Values { get; set; }
    public string []Values { get; set; }

    public IObservable<int> Process(IObservable<Tuple<Timestamped<bool>, string>> source)
    {
        //return source.Select(value => value.Item1.Value ? Values.FirstOrDefault(a => a.Key.Equals(value.Item2)).Value:0);
        return source.Select(value => value.Item1.Value ? Array.IndexOf(Values,value.Item2)+1:0);
    }
}
