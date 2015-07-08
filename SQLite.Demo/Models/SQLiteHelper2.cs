using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace SQLite.Demo.Models
{
    public class SQLiteHelper2
    {
        private static string connectionstring = "Data Source=" + HttpRuntime.AppDomainAppPath +System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        public static string Connectionstring
        {
            get { return SQLiteHelper2.connectionstring; }
        }


        public static int ExecuteQuery(string cmdText, CommandType cmdType, params SQLiteParameter[] parameters)
        {
            SQLiteCommand cmd = GetCommand(cmdText, cmdType, parameters);
            int result = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Connection.Close();
            return result;
        }
        public static Object ExecuteScalar(string cmdText, CommandType cmdType, params SQLiteParameter[] parameters)
        {
            SQLiteCommand cmd = GetCommand(cmdText, cmdType, parameters);
            object result = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            cmd.Connection.Close();
            return result;
        }
        public static DataTable ExecuteDatatable(string cmdtxt, CommandType cmdtype, params SQLiteParameter[] parameters)
        {
            SQLiteCommand cmd = GetCommand(cmdtxt, cmdtype, parameters);
            SQLiteDataAdapter adap = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            cmd.Parameters.Clear();
            cmd.Connection.Close();
            return dt;
        }
        public static DataSet ExecuteDataset(string cmdText, CommandType cmdType, params SQLiteParameter[] parameters)
        {
            SQLiteCommand command = GetCommand(cmdText, cmdType, parameters);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            command.Parameters.Clear();
            command.Connection.Close();
            return dataSet;
        }
        public static SQLiteDataReader ExecuteDataReader(string cmdText, CommandType cmdType, params SQLiteParameter[] parameters)
        {
            SQLiteCommand cmd = GetCommand(cmdText, cmdType, parameters);
            SQLiteDataReader result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            cmd.Connection.Close();
            return result;
        }

        public static DataSet Query(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionstring))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        private static SQLiteCommand GetCommand(string cmdText, CommandType cmdType, params SQLiteParameter[] parameters)
        {

            SQLiteCommand cmd = new SQLiteCommand();
            
            //SQLiteConnectionStringBuilder scs = new SQLiteConnectionStringBuilder();
            //scs.DataSource = connectionstring;
            //scs.Password = "";
            
            cmd.Connection = new SQLiteConnection(connectionstring);
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (parameters != null)
                foreach (SQLiteParameter p in parameters)
                    cmd.Parameters.Add(p);

            cmd.Connection.Open();
            //cmd.Connection.ChangePassword("pwd");//给SQLite设置密码
            //cmd.Connection.SetPassword("pwd");//打开带密码的SQLite
            return cmd;
        }
       
    }
}