using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCLUtility
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 以文件方式保存系统日志
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="dir">文件名称</param>
        public static void SaveFileLog(Exception ex, string dir)
        {
            using (TextWriter tw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "/log/" + dir + DateTime.Now.Ticks + ".txt"))
            {
                tw.WriteLine("log:" + ex.ToString());
                tw.Flush();
                tw.Close();
            }
        }

        /// <summary>
        /// 以文件方式保存系统日志
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="dir">文件名称</param>
        public static void SaveFileLogByStr(string str, string dir)
        {
            using (TextWriter tw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "/log/" + dir + DateTime.Now.Ticks + ".txt"))
            {
                tw.WriteLine("log:" + str);
                tw.Flush();
                tw.Close();
            }
        }
    }
}
