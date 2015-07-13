﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HCLUtility
{
    /// <summary>
    /// 正则表达式帮助类
    /// </summary>
    public class RegexHelper
    {
        /// <summary>
        /// 金额不四舍五入,并逗号隔开
        /// </summary>
        /// <param name="moeny"></param>
        /// <returns></returns>
        public static string MoneyComma(string moeny)
        {
            string str = Regex.Replace(moeny, @"(?<!^|\..*)(?=(\d\d\d)+(?:$|\.))", ",");
            return str;
        }
        public static bool IsNnInt(string input, string ilow)
        {
            int iReturn = int.Parse((Convert.ToDouble(input) % Convert.ToDouble(ilow)).ToString().Trim());
            Regex regex = new Regex("^\\+?[1-9][0-9]*$");
            return regex.IsMatch(iReturn.ToString().Trim());
        }
        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsHasCHZN(string inputData)
        {
             Regex regex = new Regex("[\u4e00-\u9fa5]");
             return regex.IsMatch(inputData);
        }
        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            Regex regex = new Regex("^1[3|4|5|8][0-9]\\d{4,8}$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断是否是Email
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEMail(string input)
        {
            Regex regex = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断是否是身份证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIDCard(string input)
        {
            Regex regex = new Regex("^(\\d{15}$|^\\d{18}$|^\\d{17}(\\d|X|x))$");
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 判断是否是银行卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsBandCard(string input)
        {
            Regex regex = new Regex(@"^\d{15,19}$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断是否是验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsVICode(string input)
        {
            Regex regex = new Regex(@"^\d{6}$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsInt(string input)
        {
            Regex regex = new Regex("/\\D/g");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断是否是邮政编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsZip(string input)
        {
            Regex regex = new Regex("^\\d{6}$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断是否是传真号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsFax(string input)
        {
            Regex regex = new Regex("^[0-9]{4}-[0-9]{8}$");
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 判断输入的字符是否为日期
        /// </summary>
        /// <param name="strValue">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsDate(string strValue)
        {
            return Regex.IsMatch(strValue,
                                 @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
        }
        /// <summary>
        /// 判断输入的字符是否为日期,如2004-07-12 14:25|||1900-01-01 00:00|||9999-12-31 23:59
        /// </summary>
        /// <param name="strValue">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsDateHourMinute(string strValue)
        {
            return Regex.IsMatch(strValue,
                                 @"^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
        }
        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>返回值</returns>			
        public static string CheckMathLength(string inputData, int maxLength)
        {
            if (!string.IsNullOrEmpty(inputData))
            {
                inputData = inputData.Trim();
                if (inputData.Length > maxLength) //按最大长度截取字符串
                {
                    inputData = inputData.Substring(0, maxLength);
                }
            }
            return inputData;
        }

        public static string file_get_contents(string url, Encoding encode)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            WebResponse response = request.GetResponse();
            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    int readc;
                    byte[] buffer = encode.GetBytes(url);// new byte[1024];
                    while ((readc = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, readc);
                    }
                }
                return encode.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// json时间戳转为Datetime
        /// </summary>
        /// <param name="jsonDate"></param>
        /// <returns></returns>
        public static DateTime JsonToDateTime(string jsonDate)
        {
            string value = jsonDate.Substring(6, jsonDate.Length - 8);
            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);
            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }
            return dateTime;
        }
    }
}
