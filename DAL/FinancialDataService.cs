using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Model;

namespace DAL
{
    public class FinancialDataService
    {
        /// <summary>
        /// 从数据库中获取全部数据
        /// </summary>
        public static DataTable GetAllData()
        {
            string sql = "select * from FinancialData";
            DataSet dataSet = DBHelper.GetDataSet(sql);
            return dataSet.Tables[0];
        }

        public static int AddFinancialData(FinancialData data)
        {
            //【1】构建sql脚本
            string sql = $"insert into FinancialData(CompanyName," +
                $"ReportTime," +
                $"Revenue," +
                $"NetProfit," +
                $"GrossMargin," +
                $"MarketValue," +
                $"ROE," +
                $"PERatio," +
                $"RevenueGrowthRate," +
                $"ProfitGrowthRate," +
                $"FreeCashFlow," +
                $"FCFGrowthRate) values(" +
                $"'{data.CompanyName}'," +
                $"'{data.ReportTime}'," +
                $"{data.Revenue}," +
                $"{data.NetProfit}," +
                $"{data.GrossMargin}," +
                $"{data.MarketValue}," +
                $"{data.ROE}," +
                $"{data.PERatio}," +
                $"{data.RevenueGrowthRate}," +
                $"{data.ProfitGrowthRate}," +
                $"{data.FreeCashFlow}," +
                $"{data.FCFGrowthRate})";
            //【2】调用写入程序
            return DBHelper.Update(sql);
        }
    }
}
