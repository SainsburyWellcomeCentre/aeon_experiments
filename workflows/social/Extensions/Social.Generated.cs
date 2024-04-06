//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.9.0.0 (Newtonsoft.Json v9.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------


namespace Social
{
    #pragma warning disable // Disable all warnings

    /// <summary>
    /// Specifies the parameters of an exponential distribution.
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("Specifies the parameters of an exponential distribution.")]
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class ExponentialDistribution
    {
    
        private double _rate;
    
        private double _offset;
    
        /// <summary>
        /// Specifies the rate parameter of the distribution.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("rate", Required=Newtonsoft.Json.Required.Always)]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="rate")]
        [System.ComponentModel.DescriptionAttribute("Specifies the rate parameter of the distribution.")]
        public double Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                _rate = value;
            }
        }
    
        /// <summary>
        /// Specifies the minimum value sampled from the exponential distribution.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("offset", Required=Newtonsoft.Json.Required.Always)]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="offset")]
        [System.ComponentModel.DescriptionAttribute("Specifies the minimum value sampled from the exponential distribution.")]
        public double Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }
    
        public System.IObservable<ExponentialDistribution> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new ExponentialDistribution
                {
                    Rate = _rate,
                    Offset = _offset
                }));
        }
    }


    /// <summary>
    /// Specifies the parameters of a uniform distribution.
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("Specifies the parameters of a uniform distribution.")]
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class UniformDistribution
    {
    
        private double _mean;
    
        private double _offset;
    
        /// <summary>
        /// Specifies the mean value sampled from the uniform distribution.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("mean", Required=Newtonsoft.Json.Required.Always)]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="mean")]
        [System.ComponentModel.DescriptionAttribute("Specifies the mean value sampled from the uniform distribution.")]
        public double Mean
        {
            get
            {
                return _mean;
            }
            set
            {
                _mean = value;
            }
        }
    
        /// <summary>
        /// Specifies the minimum value sampled from the uniform distribution.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("offset", Required=Newtonsoft.Json.Required.Always)]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="offset")]
        [System.ComponentModel.DescriptionAttribute("Specifies the minimum value sampled from the uniform distribution.")]
        public double Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }
    
        public System.IObservable<UniformDistribution> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new UniformDistribution
                {
                    Mean = _mean,
                    Offset = _offset
                }));
        }
    }


    /// <summary>
    /// Specifies the configuration parameters for a single patch.
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("Specifies the configuration parameters for a single patch.")]
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class Patch
    {
    
        private ExponentialDistribution _distribution;
    
        private string _distributionRef;
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("distribution")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="distribution")]
        public ExponentialDistribution Distribution
        {
            get
            {
                return _distribution;
            }
            set
            {
                _distribution = value;
            }
        }
    
        /// <summary>
        /// Specifies the name of a predefined distribution.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("distributionRef")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="distributionRef")]
        [System.ComponentModel.DescriptionAttribute("Specifies the name of a predefined distribution.")]
        public string DistributionRef
        {
            get
            {
                return _distributionRef;
            }
            set
            {
                _distributionRef = value;
            }
        }
    
        public System.IObservable<Patch> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new Patch
                {
                    Distribution = _distribution,
                    DistributionRef = _distributionRef
                }));
        }
    }


    /// <summary>
    /// Specifies the configuration parameters for a single block.
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("Specifies the configuration parameters for a single block.")]
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class Block
    {
    
        private double _weight;
    
        private double _minDueTime;
    
        private double _maxDueTime;
    
        private int _minPelletCount;
    
        private int _maxPelletCount;
    
        private System.Collections.Generic.IDictionary<string, Patch> _patches = new System.Collections.Generic.Dictionary<string, Patch>();
    
        /// <summary>
        /// Specifies the probability mass for the block. Normalization is not required.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("weight", Required=Newtonsoft.Json.Required.Always)]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="weight")]
        [System.ComponentModel.DescriptionAttribute("Specifies the probability mass for the block. Normalization is not required.")]
        public double Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
            }
        }
    
        /// <summary>
        /// Specifies the minimum block duration, in minutes.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("minDueTime")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="minDueTime")]
        [System.ComponentModel.DescriptionAttribute("Specifies the minimum block duration, in minutes.")]
        public double MinDueTime
        {
            get
            {
                return _minDueTime;
            }
            set
            {
                _minDueTime = value;
            }
        }
    
        /// <summary>
        /// Specifies the maximum block duration, in minutes.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("maxDueTime")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="maxDueTime")]
        [System.ComponentModel.DescriptionAttribute("Specifies the maximum block duration, in minutes.")]
        public double MaxDueTime
        {
            get
            {
                return _maxDueTime;
            }
            set
            {
                _maxDueTime = value;
            }
        }
    
        /// <summary>
        /// Specifies the minimum number of pellets before block transition.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("minPelletCount")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="minPelletCount")]
        [System.ComponentModel.DescriptionAttribute("Specifies the minimum number of pellets before block transition.")]
        public int MinPelletCount
        {
            get
            {
                return _minPelletCount;
            }
            set
            {
                _minPelletCount = value;
            }
        }
    
        /// <summary>
        /// Specifies the maximum number of pellets before block transition.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("maxPelletCount")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="maxPelletCount")]
        [System.ComponentModel.DescriptionAttribute("Specifies the maximum number of pellets before block transition.")]
        public int MaxPelletCount
        {
            get
            {
                return _maxPelletCount;
            }
            set
            {
                _maxPelletCount = value;
            }
        }
    
        /// <summary>
        /// Specifies the configuration of each foraging patch in the block.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("patches", Required=Newtonsoft.Json.Required.Always)]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="patches")]
        [System.ComponentModel.DescriptionAttribute("Specifies the configuration of each foraging patch in the block.")]
        public System.Collections.Generic.IDictionary<string, Patch> Patches
        {
            get
            {
                return _patches;
            }
            set
            {
                _patches = value;
            }
        }
    
        public System.IObservable<Block> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new Block
                {
                    Weight = _weight,
                    MinDueTime = _minDueTime,
                    MaxDueTime = _maxDueTime,
                    MinPelletCount = _minPelletCount,
                    MaxPelletCount = _maxPelletCount,
                    Patches = _patches
                }));
        }
    }


    /// <summary>
    /// Stores the current state of a foraging block.
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("Stores the current state of a foraging block.")]
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class BlockState
    {
    
        private int _pelletCount;
    
        private int _maxPellets;
    
        private double _dueTime;
    
        /// <summary>
        /// Specifies the number of pellets delivered in the foraging block.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("pelletCount")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="pelletCount")]
        [System.ComponentModel.DescriptionAttribute("Specifies the number of pellets delivered in the foraging block.")]
        public int PelletCount
        {
            get
            {
                return _pelletCount;
            }
            set
            {
                _pelletCount = value;
            }
        }
    
        /// <summary>
        /// Specifies the optional maximum number of pellets in the foraging block.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("maxPellets")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="maxPellets")]
        [System.ComponentModel.DescriptionAttribute("Specifies the optional maximum number of pellets in the foraging block.")]
        public int MaxPellets
        {
            get
            {
                return _maxPellets;
            }
            set
            {
                _maxPellets = value;
            }
        }
    
        /// <summary>
        /// Specifies the optional block duration, in minutes.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("dueTime")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="dueTime")]
        [System.ComponentModel.DescriptionAttribute("Specifies the optional block duration, in minutes.")]
        public double DueTime
        {
            get
            {
                return _dueTime;
            }
            set
            {
                _dueTime = value;
            }
        }
    
        public System.IObservable<BlockState> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new BlockState
                {
                    PelletCount = _pelletCount,
                    MaxPellets = _maxPellets,
                    DueTime = _dueTime
                }));
        }
    }


    /// <summary>
    /// Represents patch configuration parameters used in foraging experiments.
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("Represents patch configuration parameters used in foraging experiments.")]
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class Environment
    {
    
        private System.Collections.Generic.IDictionary<string, ExponentialDistribution> _distributions;
    
        private System.Collections.Generic.List<Block> _blocks = new System.Collections.Generic.List<Block>();
    
        /// <summary>
        /// Specifies a pre-defined set of named probability distributions.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("distributions")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="distributions")]
        [System.ComponentModel.DescriptionAttribute("Specifies a pre-defined set of named probability distributions.")]
        public System.Collections.Generic.IDictionary<string, ExponentialDistribution> Distributions
        {
            get
            {
                return _distributions;
            }
            set
            {
                _distributions = value;
            }
        }
    
        /// <summary>
        /// Specifies the sequence of blocks in a foraging experiment.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("blocks")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="blocks")]
        [System.ComponentModel.DescriptionAttribute("Specifies the sequence of blocks in a foraging experiment.")]
        public System.Collections.Generic.List<Block> Blocks
        {
            get
            {
                return _blocks;
            }
            set
            {
                _blocks = value;
            }
        }
    
        public System.IObservable<Environment> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new Environment
                {
                    Distributions = _distributions,
                    Blocks = _blocks
                }));
        }
    }


    /// <summary>
    /// Serializes a sequence of data model objects into JSON strings.
    /// </summary>
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Transform)]
    [System.ComponentModel.DescriptionAttribute("Serializes a sequence of data model objects into JSON strings.")]
    public partial class SerializeToJson
    {
    
        private System.IObservable<string> Process<T>(System.IObservable<T> source)
        {
            return System.Reactive.Linq.Observable.Select(source, value => Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }

        public System.IObservable<string> Process(System.IObservable<ExponentialDistribution> source)
        {
            return Process<ExponentialDistribution>(source);
        }

        public System.IObservable<string> Process(System.IObservable<UniformDistribution> source)
        {
            return Process<UniformDistribution>(source);
        }

        public System.IObservable<string> Process(System.IObservable<Patch> source)
        {
            return Process<Patch>(source);
        }

        public System.IObservable<string> Process(System.IObservable<Block> source)
        {
            return Process<Block>(source);
        }

        public System.IObservable<string> Process(System.IObservable<BlockState> source)
        {
            return Process<BlockState>(source);
        }

        public System.IObservable<string> Process(System.IObservable<Environment> source)
        {
            return Process<Environment>(source);
        }
    }


    /// <summary>
    /// Deserializes a sequence of JSON strings into data model objects.
    /// </summary>
    [System.ComponentModel.DefaultPropertyAttribute("Type")]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Transform)]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<ExponentialDistribution>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<UniformDistribution>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<Patch>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<Block>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<BlockState>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<Environment>))]
    [System.ComponentModel.DescriptionAttribute("Deserializes a sequence of JSON strings into data model objects.")]
    public partial class DeserializeFromJson : Bonsai.Expressions.SingleArgumentExpressionBuilder
    {
    
        public DeserializeFromJson()
        {
            Type = new Bonsai.Expressions.TypeMapping<Environment>();
        }

        public Bonsai.Expressions.TypeMapping Type { get; set; }

        public override System.Linq.Expressions.Expression Build(System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
        {
            var typeMapping = (Bonsai.Expressions.TypeMapping)Type;
            var returnType = typeMapping.GetType().GetGenericArguments()[0];
            return System.Linq.Expressions.Expression.Call(
                typeof(DeserializeFromJson),
                "Process",
                new System.Type[] { returnType },
                System.Linq.Enumerable.Single(arguments));
        }

        private static System.IObservable<T> Process<T>(System.IObservable<string> source)
        {
            return System.Reactive.Linq.Observable.Select(source, value => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value));
        }
    }


    /// <summary>
    /// Serializes a sequence of data model objects into YAML strings.
    /// </summary>
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Transform)]
    [System.ComponentModel.DescriptionAttribute("Serializes a sequence of data model objects into YAML strings.")]
    public partial class SerializeToYaml
    {
    
        private System.IObservable<string> Process<T>(System.IObservable<T> source)
        {
            return System.Reactive.Linq.Observable.Defer(() =>
            {
                var serializer = new YamlDotNet.Serialization.SerializerBuilder().Build();
                return System.Reactive.Linq.Observable.Select(source, value => serializer.Serialize(value)); 
            });
        }

        public System.IObservable<string> Process(System.IObservable<ExponentialDistribution> source)
        {
            return Process<ExponentialDistribution>(source);
        }

        public System.IObservable<string> Process(System.IObservable<UniformDistribution> source)
        {
            return Process<UniformDistribution>(source);
        }

        public System.IObservable<string> Process(System.IObservable<Patch> source)
        {
            return Process<Patch>(source);
        }

        public System.IObservable<string> Process(System.IObservable<Block> source)
        {
            return Process<Block>(source);
        }

        public System.IObservable<string> Process(System.IObservable<BlockState> source)
        {
            return Process<BlockState>(source);
        }

        public System.IObservable<string> Process(System.IObservable<Environment> source)
        {
            return Process<Environment>(source);
        }
    }


    /// <summary>
    /// Deserializes a sequence of YAML strings into data model objects.
    /// </summary>
    [System.ComponentModel.DefaultPropertyAttribute("Type")]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Transform)]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<ExponentialDistribution>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<UniformDistribution>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<Patch>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<Block>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<BlockState>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<Environment>))]
    [System.ComponentModel.DescriptionAttribute("Deserializes a sequence of YAML strings into data model objects.")]
    public partial class DeserializeFromYaml : Bonsai.Expressions.SingleArgumentExpressionBuilder
    {
    
        public DeserializeFromYaml()
        {
            Type = new Bonsai.Expressions.TypeMapping<Environment>();
        }

        public Bonsai.Expressions.TypeMapping Type { get; set; }

        public override System.Linq.Expressions.Expression Build(System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
        {
            var typeMapping = (Bonsai.Expressions.TypeMapping)Type;
            var returnType = typeMapping.GetType().GetGenericArguments()[0];
            return System.Linq.Expressions.Expression.Call(
                typeof(DeserializeFromYaml),
                "Process",
                new System.Type[] { returnType },
                System.Linq.Enumerable.Single(arguments));
        }

        private static System.IObservable<T> Process<T>(System.IObservable<string> source)
        {
            return System.Reactive.Linq.Observable.Defer(() =>
            {
                var serializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
                return System.Reactive.Linq.Observable.Select(source, value =>
                {
                    var reader = new System.IO.StringReader(value);
                    var parser = new YamlDotNet.Core.MergingParser(new YamlDotNet.Core.Parser(reader));
                    return serializer.Deserialize<T>(parser);
                });
            });
        }
    }
}