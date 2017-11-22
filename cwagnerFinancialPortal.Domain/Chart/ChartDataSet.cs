using System;
using System.Collections.Generic;

namespace cwagnerFinancialPortal.Domain
{
    public class ChartDataSet<T> where T : struct, IComparable
    {
        public string Label { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}