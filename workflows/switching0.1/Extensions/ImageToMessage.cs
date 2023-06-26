using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using NetMQ;
using System.Runtime.InteropServices;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ImageToMessage
{
    public IObservable<List<byte[]>> Process(IObservable<IplImage> source)
    {
        return source.Select(value =>
        {
            var result = new List<byte[]>();
            //NetMQMessage message = new NetMQMessage(5);
            result.Append(BitConverter.GetBytes(value.Width));
            result.Append(BitConverter.GetBytes(value.Height));
            result.Append(BitConverter.GetBytes((int)value.Depth));
            result.Append(BitConverter.GetBytes(value.Channels));
            result.Append(ToArray((Mat)value));
            return result;
        });
    }
    private  byte[] ToArray(Mat input)
    {
        var step = input.ElementSize * input.Cols;
        var data = new byte[step * input.Rows];
        var dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
        try
        {
            Mat dataHeader;
            dataHeader = new Mat(input.Rows, input.Cols, input.Depth, input.Channels, dataHandle.AddrOfPinnedObject(), step);
            CV.Copy(input, dataHeader);
        }
        finally { dataHandle.Free(); }
        return data;
    }
}
