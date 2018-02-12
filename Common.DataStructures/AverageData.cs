using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataStructures
{
    public class AverageData
    {
        public double Sum;
        public int Weight;

        public AverageData(double sum, int weight)
        {
            Sum = sum;
            Weight = weight;
        }
    }
}
