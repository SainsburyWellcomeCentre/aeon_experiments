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
public class UInt64ToString
{
    public IObservable<string> Process(IObservable<ulong> source)
    {
        return source.Select(value => {
            Console.WriteLine(Convert.ToString((long)value, 2));
            return Encoding.ASCII.GetString(BitConverter.GetBytes(value));
        });
    }
}
