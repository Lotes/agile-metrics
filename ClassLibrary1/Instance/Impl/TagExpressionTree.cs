using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using Metrics.E01_Artifacts;

namespace Metrics.Instance.Impl
{
    public class TagExpressionTree : ITagExpressionTree, IDisposable
    {
        private Dictionary<IArtifact, int> artifact2Index = null;
        private List<IArtifact> index2Artifact = null;
        private List<TagRelevanceFlags> nodes = new List<TagRelevanceFlags>();
        private IArtifactCatalog catalog = null;

        public TagExpressionTree(ITagExpression expression)
        {
            Expression = expression;
        }
        public void Dispose()
        {
            Detach();
        }

        public ITagExpression Expression { get; private set; }
        public TagRelevanceFlags this[IArtifact artifact] { get { return nodes[artifact2Index[artifact]]; } }

        public event EventHandler<IEnumerable<TagRelevanceChangedArgs>> TagRelevanceChanged;

        public void Attach(IArtifactCatalog catalog)
        {
            Detach();

            var index = 0;
            this.catalog = catalog;
            index2Artifact = catalog.ToList();
            artifact2Index = index2Artifact.ToDictionary(e => e, e => index++);
            nodes = artifact2Index.Select(kv => TagRelevanceFlags.None).ToList();
            catalog.ForEach(TraversionType.Postfix, a => Notify(UpdateArtifact(a)));

            catalog.Added += Catalog_Added;
            catalog.Moved += Catalog_Moved;
            catalog.Tagged += Catalog_Tagged;
        }

        public void Detach()
        {
            if (catalog == null)
                return;
            catalog.Added -= Catalog_Added;
            catalog.Moved -= Catalog_Moved;
            catalog.Tagged -= Catalog_Tagged;
            catalog = null;
        }

        private TagRelevanceChangedArgs UpdateArtifact(IArtifact artifact)
        {
            var hasTaggedChildren = ComputeChildrenFlag(artifact);
            var tagged = ComputeSelfFlag(artifact);
            var index = artifact2Index[artifact];
            var old = nodes[index];
            nodes[index] = tagged | hasTaggedChildren;
            var @new = nodes[index];
            return new TagRelevanceChangedArgs(artifact, old, @new);
        }

        private TagRelevanceFlags ComputeChildrenFlag(IArtifact artifact)
        {
            return artifact.Children
                            .Select(a => nodes[artifact2Index[a]])
                            .All(a => a == TagRelevanceFlags.None)
                                ? TagRelevanceFlags.None
                                : TagRelevanceFlags.ChildrenTagged;
        }

        private TagRelevanceFlags ComputeSelfFlag(IArtifact artifact)
        {
            return Expression.Evaluate(artifact.Tags) ? TagRelevanceFlags.SelfTagged : TagRelevanceFlags.None;
        }

        private void Catalog_Tagged(object sender, TagArtifactArgs e)
        {
            var oldState = nodes[artifact2Index[e.Artifact]].HasFlag(TagRelevanceFlags.SelfTagged);
            var newState = Expression.Evaluate(e.Artifact.Tags);
            if (oldState == newState)
                return;
            UpdateSelf(e.Artifact);
            UpdateParents(e.Artifact.Parent);
        }

        private TagRelevanceChangedArgs UpdateSelf(IArtifact artifact)
        {
            var index = artifact2Index[artifact];
            var value = ComputeSelfFlag(artifact);
            var old = nodes[index];
            if (value == TagRelevanceFlags.None)
                nodes[index] &= ~TagRelevanceFlags.SelfTagged;
            else
                nodes[index] |= TagRelevanceFlags.SelfTagged;
            var @new = nodes[index];
            return new TagRelevanceChangedArgs(artifact, old, @new);
        }

        private IEnumerable<TagRelevanceChangedArgs> UpdateParents(IArtifact artifact)
        {
            var result = new LinkedList<TagRelevanceChangedArgs>();
            if (artifact == null)
                return result;
            var hadChildren = nodes[artifact2Index[artifact]] & TagRelevanceFlags.ChildrenTagged;
            var hasChildren = ComputeChildrenFlag(artifact);
            while (artifact != null && hadChildren != hasChildren)
            {
                var index = artifact2Index[artifact];
                var old = nodes[index];
                if (hasChildren == TagRelevanceFlags.None)
                    nodes[index] &= ~TagRelevanceFlags.ChildrenTagged;
                else
                    nodes[index] |= TagRelevanceFlags.ChildrenTagged;
                var @new = nodes[index];
                result.AddLast(new TagRelevanceChangedArgs(artifact, old, @new));
                artifact = artifact.Parent;
                if(artifact != null)
                {
                    hadChildren = nodes[artifact2Index[artifact]] & TagRelevanceFlags.ChildrenTagged;
                    hasChildren = ComputeChildrenFlag(artifact);
                }
            }
            return result;
        }

        private void Catalog_Moved(object sender, MoveArtifactArgs e)
        {
            Notify(UpdateParents(e.Parent.Old)
                .Concat(UpdateParents(e.MovedArtifact)).ToArray());
        }

        private void Catalog_Added(object sender, IArtifact e)
        {
            artifact2Index[e] = artifact2Index.Count;
            index2Artifact.Add(e);
            nodes.Add(TagRelevanceFlags.None);
            UpdateArtifact(e);
            UpdateParents(e.Parent);
        }

        private void Notify(params TagRelevanceChangedArgs[] events)
        {
            TagRelevanceChanged?.Invoke(this, events);
        }
    }
}
