using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HCLUtility
{
    /// <summary>
    /// SQL数据库访问帮助类
    /// 链接字符串<add name="conStr" connectionString="server=.;database=Air;uid=sa;pwd=wanghao" providerName="System.Data.SqlClient" />
    /// </summary>
    public class SQLHelper
    {
         private static string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
    
      public static string Connectionstring
       {
            get { return SQLHelper.connectionstring; }
       }
	

	  public static int ExecuteQuery(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
	  {
		  SqlCommand cmd = GetCommand(cmdText, cmdType, parameters);
		  int result = cmd.ExecuteNonQuery();
		  cmd.Connection.Close();
		  return result;
	  }

	  public static Object ExecuteScalar(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
	  {
		  SqlCommand cmd = GetCommand(cmdText, cmdType, parameters);
		  object result = cmd.ExecuteScalar();
		  cmd.Connection.Close();
		  return result;
	  }
      public static DataTable ExecuteDatatable(string cmdtxt, CommandType cmdtype, params SqlParameter[] parameters)
      {
          SqlCommand cmd = GetCommand(cmdtxt, cmdtype, parameters);
          SqlDataAdapter adap = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          adap.Fill(dt);
          cmd.Parameters.Clear();
          cmd.Connection.Close();
          return dt;
      }
      public static DataSet ExecuteDataset(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
      {
          SqlCommand command = GetCommand(cmdText, cmdType, parameters);
          SqlDataAdapter adapter = new SqlDataAdapter(command);
          DataSet dataSet = new DataSet();
          adapter.Fill(dataSet);
          command.Parameters.Clear();
          command.Connection.Close();
          return dataSet;
      }
	  public static DataSet ExecuteReader(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
	  {
         
          SqlCommand command = GetCommand(cmdText, cmdType, parameters);
          SqlDataAdapter adapter = new SqlDataAdapter(command);
          DataSet dataSet = new DataSet();
          adapter.Fill(dataSet);
          command.Parameters.Clear();
          command.Connection.Close();
          return dataSet;
	  }
      public static SqlDataReader ExecuteDataReader(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
      {
          SqlCommand cmd = GetCommand(cmdText, cmdType, parameters);
          SqlDataReader result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          return result;
      }

      public static DataSet Query(string SQLString)
      {
          using (SqlConnection connection = new SqlConnection(connectionstring))
          {
              DataSet ds = new DataSet();
              try
              {
                  connection.Open();
                  SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                  command.Fill(ds, "ds");
              }
              catch (System.Data.SqlClient.SqlException ex)
              {
                  throw new Exception(ex.Message);
              }
              return ds;
          }
      }

	  private static SqlCommand GetCommand(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
	  {
		  SqlCommand cmd = new SqlCommand();
		  cmd.Connection = new SqlConnection(connectionstring);
		  cmd.CommandText = cmdText;
		  cmd.CommandType = cmdType;

		  if (parameters != null)
			  foreach (SqlParameter p in parameters)
				  cmd.Parameters.Add(p);

		  cmd.Connection.Open();
		  return cmd;
	  }
      public SQLHelper()
	{
//
		// TODO: 在此处添加构造函数逻辑
		//
    }
    }
}
