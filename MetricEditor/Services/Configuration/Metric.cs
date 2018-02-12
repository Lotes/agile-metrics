using ClassLibrary1.E00_Addons;
using ClassLibrary1.E01_Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MetricEditor.Services.Configuration
{
    public interface IDeserializableString<TTarget>
    {
        string Text { get; set; }
        bool HasError { get; }
        TTarget Value { get; }
        event EventHandler Changed;
    }

    public static class StringDeserializers
    {
        private static Dictionary<Type, Func<string, object>> deserializers = new Dictionary<Type, Func<string, object>>();
        public static void Register<TTarget>(Func<string, TTarget> deserialize)
        {
            var type = typeof(TTarget);
            if (deserializers.ContainsKey(type))
                throw new InvalidOperationException();//TODO
            deserializers.Add(type, str => deserialize(str));
        }
        public static Func<string, TTarget> Get<TTarget>()
        {
            var type = typeof(TTarget);
            if (!deserializers.ContainsKey(type))
                throw new InvalidOperationException();//TODO
            return (str) => (TTarget)deserializers[type](str);
        }
    }

    public class DeserializableString<TTarget> : IDeserializableString<TTarget>
    {
        private Func<string, TTarget> deserialize;
        private string text;

        public DeserializableString(string text)
        {
            this.deserialize = StringDeserializers.Get<TTarget>();
            this.Text = text;
        }

        public bool HasError { get; private set; }
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                try
                {
                    Value = deserialize(Transform(text));
                    HasError = false;
                }
                catch
                {
                    Value = default(TTarget);
                    HasError = true;
                }
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        protected virtual string Transform(string text)
        {
            return text;
        }

        public TTarget Value { get; private set; }

        public event EventHandler Changed;

        public static implicit operator DeserializableString<TTarget>(string str)
        {
            return new DeserializableString<TTarget>(str);
        } 
    }

    public class DeserializableTemplateString<TTarget> : DeserializableString<TTarget>
    {
        private static readonly Regex regex = new Regex(@"{{[a-z_][a-z0-9_]+(\.[a-z_][a-z0-9_]+)+}}");

        public DeserializableTemplateString(string text) : base(text)
        {
        }

        protected override string Transform(string text)
        {
            return base.Transform(text);
        }
    }

    public class MetricId
    {
        public string Name { get; private set; }
    }
    public class Metric
    {
        public IEnumerable<ArtifactType> TargetArtifactTypes { get; }
        public MetricId Id { get; }
        public Type OutputType { get; }
    }

    public class RawMetric: Metric
    {
        public ImporterType Source { get; }
    }

    public class ComputedMetric: Metric
    {
        public IEnumerable<Metric> Inputs { get; }
    }
}
