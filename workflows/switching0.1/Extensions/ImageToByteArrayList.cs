using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using NetMQ;
using System.Runtime.InteropServices;
using System.Text;

[Combinator]
[Description("Converts a IplImage into a list of byte[] each element in nthe following order: Width; Height; Depth; Channels and imaageData ")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ImageToByteArrayList
{
    public IObservable<List<byte[]>> Process(IObservable<Tuple<IplImage,string>> source)
    {
        return source.Select(value =>
        {
            var result = new List<byte[]>();
            result.Add(Encoding.ASCII.GetBytes(value.Item2));
            //NetMQMessage message = new NetMQMessage(5);
            result.Add(BitConverter.GetBytes(value.Item1.Width));
            result.Add(BitConverter.GetBytes(value.Item1.Height));
            result.Add(BitConverter.GetBytes((int)value.Item1.Depth));
            result.Add(BitConverter.GetBytes(value.Item1.Channels));
            result.Add(ToArray((Mat)value.Item1));
            return result;  
        });
    }
    public IObservable<List<byte[]>> Process(IObservable<IplImage> source)
    {
        return source.Select(value =>
        {
            var result = new List<byte[]>();
            //NetMQMessage message = new NetMQMessage(5);
            result.Add(BitConverter.GetBytes(value.Width));
            result.Add(BitConverter.GetBytes(value.Height));
            result.Add(BitConverter.GetBytes((int)value.Depth));
            result.Add(BitConverter.GetBytes(value.Channels));
            result.Add(ToArray((Mat)value));
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
