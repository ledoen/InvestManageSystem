using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FinancialData
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime ReportTime { get; set; }
        public double Revenue { get; set; }
        public double NetProfit { get; set; }
        public double GrossMargin { get; set; }
        public double MarketValue { get; set; }
        public double ROE { get; set; }
        public double PERatio { get; set; }
        public double RevenueGrowthRate { get; set; }
        public double ProfitGrowthRate { get; set; }
        public double FreeCashFlow { get; set; }
        public double FCFGrowthRate { get; set; }
    }
}
