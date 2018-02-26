using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.E03_Tags
{
    public class Tag : ITag
    {
        public Tag(string name)
        {
            Name = name;
        }

        public string Name
        {
            get; private set;
        }
    }
}
