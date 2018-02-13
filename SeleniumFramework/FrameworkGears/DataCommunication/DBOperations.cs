using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace SeleniumFramework.FrameworkGears.DataCommunication
{
    public class DBOperations
    {
        private static SqlConnection ConnectionHandle;
        static DBOperations()
        {

            ConnectionHandle = new SqlConnection("user id=CIQINDIA\\PRATEEKVATS;" +
                                       "password=;server=II02-CMBFG32;" +
                                       "Trusted_Connection=yes;" +
                                       "database=SeleniumTest; " +
                                       "connection timeout=30");
            OpenConnection();
        }

        private static void OpenConnection()
        {
            try
            {
                ConnectionHandle.Open();
            }
            catch (Exception e)
            {
                Trace.WriteLine("DB Error:"+e.Message);
            }
        }

        public static void CloseConnection()
        {
            try
            {
                ConnectionHandle.Close();
            }
            catch (Exception e)
            {
                Trace.WriteLine("DB Error:" + e.Message);
            }
        }

        public string RunInsertQuery(string sqlQuery )
        {
            SqlCommand command=new SqlCommand(sqlQuery+"SELECT SCOPE_IDENTITY();",ConnectionHandle);
            return command.ExecuteScalar().ToString();
        }
        public bool RunUpdateQuery(string sqlQuery)
        {
            SqlCommand command = new SqlCommand(sqlQuery + "SELECT SCOPE_IDENTITY();", ConnectionHandle);
            bool updateFlag = command.ExecuteNonQuery() > 0 ? true : false;
            return updateFlag;
        }
    }
}
