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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using DAL;
using System.Data;

namespace InvestManageSystem.GUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 字段
        //数据表，存储从数据库返回的全部数据，包括所有股票的所有数据
        private DataTable dataTableForAll = new DataTable();
        //增加数据窗口
        public static WindowForAddData WindowForAddData = null;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.dataGridForDisplay.DataContext = dataTableForAll;
            comboBoxForCompany.ItemsSource = ListForControl.Companies;
            comboBoxForReportTime.ItemsSource = ListForControl.ReportTimes;
            //程序启动后将所有数据显示出来
            UpdateAllData();
        }

        #region 添加数据
        /// <summary>
        /// 添加数据响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonForAddData_Click(object sender, RoutedEventArgs e)
        {
            if (WindowForAddData == null)
            {
                //创建窗口，添加注册事件处理服务，并显示窗口
                WindowForAddData = new WindowForAddData();
                WindowForAddData.FinancialDataAdded += AddDataFinished;
                WindowForAddData.Show();
            }
            else
            {
                WindowForAddData.Activate();
                WindowForAddData.WindowState = WindowState.Normal;
            }
            //UpdateAllData();
        }
        /// <summary>
        /// 数据添加完成事件的服务函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDataFinished(object sender, EventArgs e)
        {
            UpdateAllData();
        }
        #endregion

        #region 数据显示
        /// <summary>
        /// 从数据库提取全部数据，并更新数据列表显示
        /// </summary>
        private void UpdateAllData()
        {
            dataTableForAll = FinancialDataService.GetAllData();
            this.dataGridForDisplay.DataContext = dataTableForAll;
        }

        /// <summary>
        /// 显示选定公司的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxForCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBoxForCompany.SelectedIndex == -1)
            {
                //构造一个肯定成立的条件，显示全部公司数据
                this.dataTableForAll.DefaultView.RowFilter = "CompanyName <> '0'";
            }
            else
            {
                this.dataTableForAll.DefaultView.RowFilter = $"CompanyName = '{comboBoxForCompany.SelectedItem.ToString()}'";
            }
            if (comboBoxForReportTime.SelectedIndex > -1)
            {
                string filter = "";
                switch (this.comboBoxForReportTime.SelectedItem.ToString())
                {
                    case "一季报":
                        filter = "Convert(ReportTime, 'System.String') like '%/3/%'";
                        break;
                    case "半年报":
                        filter = "Convert(ReportTime, 'System.String') like '%/6/%'";
                        break;
                    case "三季报":
                        filter = "Convert(ReportTime, 'System.String') like '%/9/%'";
                        break;
                    case "年报":
                        filter = "Convert(ReportTime, 'System.String') like '%/12/%'";
                        break;
                    default:
                        break;
                }
                this.dataTableForAll.DefaultView.RowFilter += $" and {filter}";
            }
        }

        private void comboBoxForReportTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxForReportTime.SelectedIndex == -1)
            {
                //构造一个肯定可以成立的条件，显示全部报告期数据
                this.dataTableForAll.DefaultView.RowFilter = "ReportTime <> '1900-1-1'";
            }
            else
            {      
                switch (this.comboBoxForReportTime.SelectedItem.ToString())
                {
                    case "一季报":
                        this.dataTableForAll.DefaultView.RowFilter = "Convert(ReportTime, 'System.String') like '%/3/%'";
                        break;
                    case "半年报":
                        this.dataTableForAll.DefaultView.RowFilter = "Convert(ReportTime, 'System.String') like '%/6/%'";
                        break;
                    case "三季报":
                        this.dataTableForAll.DefaultView.RowFilter = "Convert(ReportTime, 'System.String') like '%/9/%'";
                        break;
                    case "年报":
                        this.dataTableForAll.DefaultView.RowFilter = "Convert(ReportTime, 'System.String') like '%/12/%'";
                        break;
                    default:
                        break;
                }
            }

            if (comboBoxForCompany.SelectedIndex > -1)
            {
                this.dataTableForAll.DefaultView.RowFilter += $" and CompanyName = '{comboBoxForCompany.SelectedItem.ToString()}'";
            }
        }

        /// <summary>
        /// 重置公司选择，显示全部公司数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelForCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.comboBoxForCompany.SelectedIndex = -1;
        }
        /// <summary>
        /// 重置报告期选择，显示全部报告期数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelForReportTime_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.comboBoxForReportTime.SelectedIndex = -1;

        }
        #endregion


    }
}
