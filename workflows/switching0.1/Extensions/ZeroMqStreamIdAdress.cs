using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NetMQ;
using System.Net;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ZeroMqStreamIdAdress
{
    public IObservable<ZeroMqAddress> Process(IObservable<Tuple<NetMQFrame, NetMQFrame>> source)
    {
        return source.Select(value => new ZeroMqAddress{ Address = value.Item1.ConvertToString(), StreamId = value.Item2.ConvertToString()});
    }
    public IObservable<ZeroMqAddress> Process(IObservable<Tuple<string, string>> source)
    {
        return source.Select(value => new ZeroMqAddress{ Address = value.Item1, StreamId = value.Item2});
    }
}
public struct ZeroMqAddress
{
    //IPAddress
    public string Address;
    public string StreamId;
}
