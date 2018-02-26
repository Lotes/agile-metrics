namespace ClassLibrary1.N03_Average
{
    public class AverageData
    {
        public int Weight;
        public double Sum;

        public AverageData(int weight, double sum)
        {
            Weight = weight;
            Sum = sum;
        }

        public double Value { get { return Weight > 0 ? Sum / Weight : 0; } }
    }
}