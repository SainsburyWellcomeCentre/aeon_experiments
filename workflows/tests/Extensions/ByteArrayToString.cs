using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ByteArrayToString
{
    public IObservable<string> Process(IObservable<byte[]> source)
    {
        return source.Select(value => {
            Console.WriteLine(value.Length);
            return Encoding.ASCII.GetString(value, 0, 16);
            // return Encoding.Unicode.GetString(value, 0, 4);
        });
    }
}
