using ClassLibrary1.E01_Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Facade.Impl;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Instance.Impl
{
    public class RawNode : IRawNode
    {
        private static readonly IReadOnlyDictionary<string, IDependency> Empty = new Dictionary<string, IDependency>();
        private IMetaGraph metaGraph;
        private LinkedList<IValueSubscription> subscriptions;

        public RawNode(IMetaNode meta, IMetaGraph graph, IArtifact artifact)
        {
            MetaNode = meta;
            Artifact = artifact;
            this.metaGraph = graph;
        }

        public IReadOnlyDictionary<string, IDependency> Inputs { get { return Empty; } }

        public object Value
        {
            get
            {
                var key = this.MetaNode.Key;
                return metaGraph.Storage.GetValue(key, Artifact);
            }
            set
            {
                var key = this.MetaNode.Key;
                metaGraph.Storage.SetValue(key, Artifact, value);
                Invalidate();
            }
        }

        public bool Invalid { get { return false; } }

        public IValueSubscription Subscribe()
        {
            if (subscriptions == null)
                subscriptions = new LinkedList<IValueSubscription>();
            IValueSubscription subscription = null;
            subscription = new ValueSubscription(this, () => subscriptions.Remove(subscription));
            subscriptions.AddLast(subscription);
            return subscription;
        }

        public IEnumerable<IDependency> Outputs
        {
            get
            {
                return metaGraph.Instances.Values.SelectMany(g => g.GetOutputsOf(this));
            }
        }

        public void Validate() { /* nothing to do */ }

        public void Invalidate()
        {
            foreach (var output in Outputs)
                output.Invalidate();
            if (subscriptions != null)
                foreach (var subscription in subscriptions)
                    subscription.NotifyChanged();
        }
        public IArtifact Artifact { get; private set; }
        public IMetaNode MetaNode { get; private set; }
    }
}
