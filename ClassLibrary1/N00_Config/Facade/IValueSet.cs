using System;
using ClassLibrary1.E01_Artifacts;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueSet
    {
        IValue GetValue(IArtifact artifact);
        IValue SetValue(IArtifact artifact, object value);
    }
}