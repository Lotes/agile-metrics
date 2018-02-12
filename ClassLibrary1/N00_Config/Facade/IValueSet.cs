using System;
using ClassLibrary1.E01_Artifacts;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueSet //values for one(!) metric
    {
        object GetValue(IArtifact artifact);
        void SetValue(IArtifact artifact, object value);
    }
}