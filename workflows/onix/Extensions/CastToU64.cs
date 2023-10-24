using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using System.Runtime.InteropServices;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CastToU64
{
    public IObservable<ulong[]> Process(IObservable<byte[]> source)
    {
        return source.Select(value =>
        {
            var result = new ulong[value.Length / sizeof(ulong)];
            Buffer.BlockCopy(value, 0, result, 0, value.Length);
            return result;
        });
    }

    public IObservable<ulong[]> Process(IObservable<Mat> source)
    {
        return source.Select(value =>
        {
            var result = new long[value.Cols];
            Marshal.Copy(value.Data, result, 0, result.Length);
            var result2 = new ulong[result.Length];
            Buffer.BlockCopy(result, 0, result2, 0, result.Length * sizeof(long));
            return result2;
        });
    }
}
