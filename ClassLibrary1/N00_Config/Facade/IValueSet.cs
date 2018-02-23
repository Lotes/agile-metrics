using System;
using ClassLibrary1.E01_Artifacts;

namespace ClassLibrary1.N00_Config.Facade
{
    public interface IValueSet
    {
        IValueCell GetValue(IArtifact artifact);
        IValueCell SetValue(IArtifact artifact, object value);
    }
}