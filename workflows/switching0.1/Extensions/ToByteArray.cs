using Bonsai;
using Bonsai.Dsp;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using OpenCV.Net;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ToByteArray
{
    public IObservable<byte[]> Process(IObservable<IplDepth> source)
    {
        //return Process((IObservable<int>)source);
        return source.Select(value => BitConverter.GetBytes((int)value));
    }
    public IObservable<byte[]> Process(IObservable<int> source)
    {
        return source.Select(value => BitConverter.GetBytes(value));
    }
 
}
