﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.N00_Config.Meta;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueSet: IValueSet
    {
        private readonly ArtifactType type;
        private readonly List<IValueCell> values = new List<IValueCell>();
        private readonly List<IArtifact> index2Artifact = new List<IArtifact>();
        private readonly Dictionary<IArtifact, int> artifact2Index = new Dictionary<IArtifact, int>();
        private readonly IMetaNode node;
        private readonly IValueStorageFactory factory;
        public ValueSet(IMetaNode node, ArtifactType type, IValueStorageFactory factory)
        {
            this.factory = factory;
            this.node = node;
            this.type = type;
        }

        public IValueCell GetValue(IArtifact artifact)
        {
            if (artifact.ArtifactType != type)
                throw new InvalidOperationException();
            if (!artifact2Index.ContainsKey(artifact))
            {
                artifact2Index[artifact] = index2Artifact.Count;
                index2Artifact.Add(artifact);
                values.Add(factory.CreateValueCell());
            }
            var index = artifact2Index[artifact];
            return values[index];
        }
        
        public IValueCell SetValue(IArtifact artifact, object value)
        {
            if (artifact.ArtifactType != type)
                throw new InvalidOperationException(); //TODO
            IValueCell cell;
            if (!artifact2Index.ContainsKey(artifact))
            {
                artifact2Index[artifact] = index2Artifact.Count;
                index2Artifact.Add(artifact);
                values.Add(cell = factory.CreateValueCell());
            }
            else
            {
                var index = artifact2Index[artifact];
                cell = values[index];
            }
            cell.Value = value;
            return cell;
        }

        public void ClearValue(IArtifact artifact)
        {
            if (artifact.ArtifactType == type && artifact2Index.ContainsKey(artifact))
            {
                var index = artifact2Index[artifact];
                values[index] = null;
            }
        }
    }
}
