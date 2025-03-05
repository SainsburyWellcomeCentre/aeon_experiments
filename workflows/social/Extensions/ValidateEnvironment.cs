using Bonsai;
using System;
using System.ComponentModel;
using System.Reactive.Linq;

[Combinator]
[Description("Ensures all patch distributions are initialized.")]
[WorkflowElementCategory(ElementCategory.Sink)]
public class ValidateEnvironment
{
    public string Path { get; set; }

    public IObservable<Social.Environment> Process(IObservable<Social.Environment> source)
    {
        return source.Do(environment =>
        {
            if (string.IsNullOrEmpty(environment.Name))
            {
                environment.Name = System.IO.Path.GetFileNameWithoutExtension(Path);
            }

            foreach (var block in environment.Blocks)
            {
                foreach (var patch in block.Patches.Values)
                {
                    if (patch.Distribution == null)
                    {
                        patch.Distribution = environment.Distributions[patch.DistributionRef];
                    }
                }
            }
        });
    }
}
