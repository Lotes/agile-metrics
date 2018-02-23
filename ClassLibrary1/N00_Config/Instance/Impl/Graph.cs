﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E00_Addons;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using ClassLibrary1.N00_Config.Facade;
using ClassLibrary1.N00_Config.Meta;
using ClassLibrary1.N00_Config.Facade.Impl;

namespace ClassLibrary1.N00_Config.Instance.Impl
{
    public class Graph: IGraph
    {
        private readonly ITagExpression expression;
        private readonly IValueStorage storage;
        
        public Graph(IMetaGraph metaGraph, ITagExpression expression, IValueStorageFactory storageFactory)
        {
            this.storage = storageFactory.CreateStorage();
            this.MetaGraph = metaGraph;
            this.MetaGraph = metaGraph;
            this.expression = expression;
            foreach (var node in metaGraph.MetaNodes.OfType<IMetaSelfNode>())
                storage.Allocate(node, node.TargetArtifactTypes);
        }

        public IMetaGraph MetaGraph { get; }

        public ITagExpression TagExpression
        {
            get
            {
                return expression;
            }
        }

        public IValueStorage Storage
        {
            get
            {
                return storage;
            }
        }

        public IReadOnlyDictionary<IArtifact, IValueSubscription> SubscribeOn(TypedKey key, IEnumerable<IArtifact> artifacts)
        {
            var metaNode = MetaGraph.GetNode(key);
            return artifacts.ToDictionary(a => a, a => SubscribeOnCell(RequestCells(metaNode, a), metaNode, a));
        }

        private IValueCell RequestCells(IMetaNode metaNode, IArtifact artifact)
        {
            metaNode.ForEachInput((dependency, source) => 
            {
                if (dependency.Locality == DependencyLocality.Self)
                {
                    RequestCells(source, artifact);
                }
                else
                {
                    foreach (var child in artifact.Children)
                        RequestCells(source, child);
                }
            });
            var cell = storage.GetValue(metaNode, artifact);
            cell.ReferenceCount++;
            return cell;
        }

        private void ReleaseCell(IMetaNode metaNode, IArtifact artifact)
        {
            metaNode.ForEachInput((dependency, source) =>
            {
                if (dependency.Locality == DependencyLocality.Self)
                {
                    ReleaseCell(source, artifact);
                }
                else
                {
                    foreach (var child in artifact.Children)
                        ReleaseCell(source, child);
                }
            });
            var cell = storage.GetValue(metaNode, artifact);
            cell.ReferenceCount--;
            if (cell.ReferenceCount == 0)
                cell.IsValid = false;
        }

        private IValueSubscription SubscribeOnCell(IValueCell cell, IMetaNode metaNode, IArtifact artifact)
        {
            return new ValueSubscription(cell, () => ReleaseCell(metaNode, artifact));
        }
    }
}
