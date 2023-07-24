using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using NetMQ;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class MessageToImage
{
    public IObservable<IplImage> Process(IObservable<NetMQMessage> source)
    {
        return source.Select(value => Process(value));
    }
    private IplImage Process(NetMQMessage value)
    {
        var startindex = value.Count()-5;
        var imageData = value[startindex+4].Buffer;
        var width = BitConverter.ToInt32(value[startindex].Buffer,0);
        var height = BitConverter.ToInt32(value[startindex+1].Buffer,0);
        var depth = BitConverter.ToInt32(value[startindex+2].Buffer,0);
        var channels = BitConverter.ToInt32(value[startindex+3].Buffer,0);
        var image =Mat.FromArray( imageData, height, width, (Depth)depth, channels).GetImage();

        return image;
    }
}

