using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueSet: IValueSet
    {
        public class ValueCell
        {
            public bool IsValid;
            public object Value;
        }
        private readonly ArtifactType type;
        private readonly List<ValueCell> values = new List<ValueCell>();
        private readonly List<IArtifact> index2Artifact = new List<IArtifact>();
        private readonly Dictionary<IArtifact, int> artifact2Index = new Dictionary<IArtifact, int>();

        public ValueSet(ArtifactType type)
        {
            this.type = type;
        }

        public object GetValue(IArtifact artifact)
        {
            return GetCell(artifact)?.Value;
        }

        private ValueCell GetCell(IArtifact artifact)
        {
            if (artifact.ArtifactType != type)
                throw new InvalidOperationException(); //TODO
            if (!artifact2Index.ContainsKey(artifact))
                return null;
            var index = artifact2Index[artifact];
            return values[index];
        }

        public void SetValue(IArtifact artifact, object value)
        {
            if (artifact.ArtifactType != type)
                throw new InvalidOperationException(); //TODO
            if (!artifact2Index.ContainsKey(artifact))
            {
                artifact2Index[artifact] = index2Artifact.Count;
                index2Artifact.Add(artifact);
                values.Add(new ValueCell());
            }
            var index = artifact2Index[artifact];
            values[index].Value = value;
            values[index].IsValid = true;
        }

        public bool IsValid(IArtifact artifact)
        {
            return GetCell(artifact)?.IsValid ?? false;
        }

        public void Invalidate(IArtifact artifact)
        {
            var cell = GetCell(artifact);
            if (cell != null)
                cell.IsValid = false;
        }
    }
}
