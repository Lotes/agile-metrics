using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.N00_Config
{
    public interface ITransaction
    {
        void Begin();
        void Rollback();
        void Commit();
    }
}
