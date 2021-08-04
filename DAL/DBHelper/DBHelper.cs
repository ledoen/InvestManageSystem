using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DAL
{
    public class DBHelper
    {
        //用于连接数据库，从App.config中获取
        //private static string dataBaseInfo = "Server=.;DataBase=TargetFinancialDB;Uid=sa;Pwd=sa";
        private static string dataBaseInfo = ConfigurationManager.ConnectionStrings["connectToDB"].ToString();

        /// <summary>
        /// 调用ExecuteNonQuery方法，执行增、删、改
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <returns>返回受影响行数</returns>
        public static int Update(string sql)
        {
            //【1】建立连接
            SqlConnection sqlConnection = new SqlConnection(dataBaseInfo);
            //【2】建立操作指令
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            //【3】打开连接，执行指令
            try
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库访问出现错误，具体信息为：" + ex.Message);
            }
            finally
            {
                //【4】关闭连接
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 调用DataerAdapter，实现列表查询
        /// </summary>
        /// <param name="sql">SQL脚本</param>
        /// <returns>返回包含多行数据的DataSet</returns>
        public static DataSet GetDataSet(string sql)
        {
            //【1】新建连接
            SqlConnection sqlConnection = new SqlConnection(dataBaseInfo);
            //【2】新建操作指令
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            //【3】新建DataAdapter对象
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            //【4】新建DataSet
            DataSet dataSet = new DataSet();
            //【5】使用DataAdapter
            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception("数据库访问出现错误，具体信息为：" + ex.Message);
            }
            finally
            {
                //【6】关闭连接
                sqlConnection.Close();
            }
        }
    }
}
