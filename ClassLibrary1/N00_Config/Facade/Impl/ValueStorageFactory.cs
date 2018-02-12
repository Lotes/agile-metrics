using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.E01_Artifacts;

namespace ClassLibrary1.N00_Config.Facade.Impl
{
    public class ValueStorageFactory: IValueStorageFactory
    {
        public IValueStorage CreateStorage()
        {
            return new ValueStorage(this);
        }

        public IValueSet CreateValueSet(ArtifactType type)
        {
            return new ValueSet(type); 
        }
    }
}
