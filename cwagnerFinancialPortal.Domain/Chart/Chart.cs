using System;

namespace cwagnerFinancialPortal.Domain
{
    public class Chart<T> where T : struct, IComparable
    {
        public string Name { get; set; }
        public ChartDataSet<T>[] DataSets { get; set; }
        public string [] Labels { get; set; }
    }
}
