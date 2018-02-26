using ClassLibrary1.E01_Artifacts;
using ClassLibrary1.E02_TypedKeys;

namespace ClassLibrary1.N00_Config.Facade
{
    public static class MetricModelExtensions
    {
        public static void SetRawValue<T>(this IMetricModel mmodel, TypedKey<T> key, IArtifact artifact, T value)
        {
            mmodel.SetRawValue(key, artifact, value);
        }
    }
}