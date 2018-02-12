using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.E04_MetricSources
{
    public interface IComputationValueSet
    {
        void SetValue(IArtifact artifact, TypedKey computationId, object value);
        object GetValue(IArtifact artifact, TypedKey computationId);
        bool HasValue(IArtifact artifact, TypedKey computationId);
        IReadOnlyDictionary<ArtifactType, int> GetArtifactStatistics(TypedKey computationId);
        //Implementierung: Array je ComputationId und ArtifactType
    }
}
