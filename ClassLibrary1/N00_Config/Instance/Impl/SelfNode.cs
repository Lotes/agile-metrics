using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Facade.Impl;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Instance.Impl
{
    public class SelfNode : ISelfNode
    {
        private readonly IGraph graph;
        private LinkedList<IValueSubscription> subscriptions = null;
        public SelfNode(IMetaNode meta, IArtifact artifact, IGraph graph)
        {
            this.graph = graph;
            MetaNode = meta;
            Artifact = artifact;
            Invalid = true;
            Artifact.Tags.CollectionChanged += Tags_CollectionChanged;
        }

        private void Tags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Invalidate();
        }

        public IArtifact Artifact { get; }
        public IMetaNode MetaNode { get; }
        public bool Invalid { get; protected set; }
        public void Invalidate()
        {
            Invalid = true;
            foreach (var output in Outputs)
                output.Invalidate();
            if (subscriptions != null)
                foreach (var subscription in subscriptions)
                    subscription.NotifyChanged();
        }
        public object Value
        {
            get
            {
                if(Invalid)
                    Validate();
                var key = MetaNode.Key;
                return graph.Storage.GetValue(key, Artifact);
            }
            private set
            {
                var key = MetaNode.Key;
                graph.Storage.SetValue(key, Artifact, value);
            }
        }
        public IReadOnlyDictionary<IMetaDependency, IEnumerable<IDependency>> Inputs { get { return graph.GetInputsOf(this).ToLookup(d => d.MetaDependency).ToDictionary(g => g.Key, g => (IEnumerable<IDependency>)g); } }
        public IEnumerable<IDependency> Outputs { get { return graph.GetOutputsOf(this); } }

        public IValueSubscription Subscribe()
        {
            if(subscriptions == null)
                subscriptions = new LinkedList<IValueSubscription>();
            IValueSubscription subscription = null;
            subscription = new ValueSubscription(this, () =>
            {
                subscriptions.Remove(subscription);
                //TODO clean up....
            });
            subscriptions.AddLast(subscription);
            return subscription;
        }

        public void Validate()
        {
            foreach (var input in Inputs.Values.SelectMany(a => a))
                if (input.Source.Invalid)
                    input.Source.Validate();
            Value = Compute();
            Invalid = false;
        }

        private object Compute()
        {
            if (!graph.TagExpression.Evaluate(Artifact))
                return null;
            var variables = Inputs.ToDictionary(kv => kv.Key.Name, kv =>
            {
                var values = kv.Value.Select(v =>
                {
                    if (v.Source.Invalid)
                        v.Source.Validate();
                    return v.Source.Value;
                }).ToArray();
                if(kv.Key.Locality == DependencyLocality.Self)
                    return values.First();
                var array = Array.CreateInstance(kv.Key.Source.Key.Type, values.Length);
                for(var index = 0; index < values.Length; index++)
                    array.SetValue(values[index], index);
                return array;
            });
            return ((IMetaSelfNode) MetaNode).Compute(this);
        }
    }
}
