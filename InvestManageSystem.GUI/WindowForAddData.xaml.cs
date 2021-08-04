using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using DAL;

namespace InvestManageSystem.GUI
{
    /// <summary>
    /// WindowForAddData.xaml 的交互逻辑
    /// </summary>
    public partial class WindowForAddData : Window
    {
        //事件：添加完成，用于触发主窗口更新数据列表
        public event EventHandler FinancialDataAdded;
        private void OnFinancialDataAdded()
        {
            FinancialDataAdded?.Invoke(this, new EventArgs());
        }

        public WindowForAddData()
        {
            InitializeComponent();
            comboBoxForCompany.ItemsSource = ListForControl.Companies;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.WindowForAddData = null;
        }

        private void butttonForAdd_Click(object sender, RoutedEventArgs e)
        {
            //【1】非空验证，公司和日期不能为空
            if (this.comboBoxForCompany.SelectedIndex == -1)
            {
                MessageBox.Show("请选择公司","提示信息");
                return;
            }
            if (this.datePickerForReportTime.SelectedDate == null)
            {
                MessageBox.Show("请选择日期", "提示信息");
                return;
            }
            //【2】封装数据
            FinancialData financialData = new FinancialData()
            {
                CompanyName = this.comboBoxForCompany.Text,
                ReportTime = (DateTime)this.datePickerForReportTime.SelectedDate,
                Revenue = Convert.ToDouble(this.textBoxForRevenue.Text.Trim()),
                NetProfit = Convert.ToDouble(this.textBoxForNetProfit.Text.Trim()),
                GrossMargin = Convert.ToDouble(this.textBoxForGrossMargin.Text.Trim()),
                ROE = Convert.ToDouble(this.textBoxForROE.Text.Trim()),
                PERatio = Convert.ToDouble(this.textBoxForPERatio.Text.Trim()),
                MarketValue = Convert.ToDouble(this.textBoxForMarketValue.Text.Trim()),
                RevenueGrowthRate = Convert.ToDouble(this.textBoxForRevenueGrowthRate.Text.Trim()),
                ProfitGrowthRate = Convert.ToDouble(this.textBoxForProfitGrowthRate.Text.Trim()),
                FreeCashFlow = Convert.ToDouble(this.textBoxForFreeCashFlow.Text.Trim()),
                FCFGrowthRate = Convert.ToDouble(this.textBoxForFCFGrowthRate.Text.Trim())
            };
            //【3】写入数据库
            int res = 0;
            try
            {
                res = FinancialDataService.AddFinancialData(financialData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"提示信息");
                return;
            }
            //【4】更新主窗口数据列表
            if (res == 1)
            {
                MessageBoxResult result = MessageBox.Show("添加成功，是否继续？", "提示信息",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    //继续添加
                    //清空控件
                    foreach (var item in this.gridLevel1.Children)
                    {
                        if (item is TextBox)
                        {
                            ((TextBox)item).Text = "0";
                        }
                        else if (item is ComboBox)
                        {
                            ((ComboBox)item).SelectedIndex = -1;
                        }
                        else if (item is DatePicker)
                        {
                            ((DatePicker)item).SelectedDate = null;
                        }
                    }
                }
                else
                {
                    //结束添加，关闭窗口
                    this.Close();
                }
                //更新主窗口数据
                OnFinancialDataAdded();
            }
            else
            {
                MessageBox.Show("添加失败！", "提示信息");
            }
        }
    }
}
