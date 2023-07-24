using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class UnwrapRotation
{
    public IObservable<double> Process(IObservable<double> source)
    {
        double prev = double.NaN;
        double curr = double.NaN;

        return source.Select(value => {
            if (double.IsNaN(value)){
                prev = value;
                return double.NaN;
            }
            else{
                curr = value; //update the current value
                double[] pos = {curr, curr + 2 * Math.PI, curr - 2 * Math.PI};
                double delta;
                double min_delta = Math.Abs(pos[0] - prev);
                int arg_min_delta = 0;
                for (int i = 1; i < pos.Length; i++) {
                    delta = Math.Abs(pos[i] - prev);

                    if (delta < min_delta){
                        min_delta = delta;
                        arg_min_delta = i;
                    }
                }
                var p = (pos[arg_min_delta]- prev);
                prev = value; //update history
                return -p;
            }
        });
    }

    public IObservable<double> Process(IObservable<Tuple<double,double>> source)
    {
        return source.Select(value => {
            double prev = value.Item1;
            double curr = value.Item2;

            if ((double.IsNaN(prev)) | (double.IsNaN(curr))){
                return double.NaN;
            }
            else{
                double[] pos = {curr, curr + 2 * Math.PI, curr - 2 * Math.PI};
                double delta;
                double min_delta = Math.Abs(pos[0] - prev);
                int arg_min_delta = 0;
                for (int i = 1; i < pos.Length; i++) {
                    delta = Math.Abs(pos[i] - prev);

                    if (delta < min_delta){
                        min_delta = delta;
                        arg_min_delta = i;
                    }
                }
                var p = (pos[arg_min_delta]- prev);
                return -p;
            }
        });
    }

}