using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HCLUtility
{
    /// <summary>
    /// 邮件发送帮助类
    /// </summary>
    public class EmailHelper
    {
        /// 指定用户邮箱发邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容</param>
        /// <param name="addresser">发件人</param>
        /// <param name="addresserEmail">发件人邮箱</param>
        /// <param name="addresserPSW">发件人密码</param>
        /// <param name="Recipient">收件人</param>
        /// <param name="RecipientName">收件人名称</param>
        /// <param name="CCFlag">是否抄送</param>
        /// <returns></returns>
        public static bool EnSystemMail(string subject, string content, string addresser, string addresserEmail, string addresserPSW, string Recipient, string RecipientName, bool CCFlag)
        {

            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Host = "smtp.163.com";
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(addresserEmail, addresserPSW);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.EnableSsl = false;
            client.Timeout = 100000;

            System.Net.Mail.MailMessage message = new MailMessage();

            message.From = new MailAddress(addresserEmail, addresser);//发送方

            message.To.Add(new MailAddress(Recipient, RecipientName));  //接收方

            if (CCFlag)
            {
                message.CC.Add(new MailAddress(addresserEmail, addresser));
            }


            message.Subject = subject;
            message.Body = content;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            try
            {
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }

        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="strSmtpServer">SMTP</param>
        /// <param name="port">SMTP端口号</param>
        /// <param name="ssl">是否采用SSL</param>
        /// <param name="strFrom">发信箱地址</param>
        /// <param name="strFromPass">发信箱密码</param>
        /// <param name="strto">收信箱</param>
        /// <param name="strSubject">主题</param>
        /// <param name="strBody">邮件正文</param>
        /// <returns>返回执行结果</returns>
        public static bool SendMail(string strSmtpServer, int port, bool ssl, string strFrom, string strFromName, string strFromPass, string strto, string strSubject, string strBody)
        {
            bool bTip = new bool();
            bTip = false;
            try
            {
                bTip = SendMail(strSmtpServer, port, ssl, MailPriority.Normal, strFrom, strFromName, strFromPass, new string[] { strto }, strSubject, strBody, null);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return bTip;
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="strSmtpServer">SMTP</param>
        /// <param name="port">SMTP端口号</param>
        /// <param name="ssl">是否采用SSL</param>
        /// <param name="strFrom">发信箱地址</param>
        /// <param name="strFromName">发信人昵称</param>
        /// <param name="strFromPass">发件人密码</param>
        /// <param name="strTos">收件人地址</param>
        /// <param name="strSubject">主题</param>
        /// <param name="strBody">邮件正文</param>
        /// <param name="attachments">邮件附件</param>
        /// <returns>返回执行结果</returns>
        public static bool SendMail(string strSmtpServer, int port, bool ssl, string strFrom, string strFromName, string strFromPass, string[] strTos, string strSubject, string strBody, params Attachment[] attachments)
        {
            bool bTip = new bool();
            bTip = false;
            try
            {
                bTip = SendMail(strSmtpServer, port, ssl, MailPriority.Normal, strFrom, strFromName, strFromPass, strTos, strSubject, strBody, attachments);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return bTip;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="strSmtpServer">SMTP</param>
        /// <param name="port">SMTP端口号</param>
        /// <param name="ssl">是否采用SSL</param>
        /// <param name="priority">设置邮件发送级别，枚举类型</param>
        /// <param name="strFrom">发信箱地址</param>
        /// <param name="strFromName">发信人昵称</param>
        /// <param name="strFromPass">发件人密码</param>
        /// <param name="strTos">收件人地址</param>
        /// <param name="strSubject">主题</param>
        /// <param name="strBody">邮件正文</param>
        /// <param name="attachments">邮件附件</param>
        /// <returns>返回执行结果</returns>
        public static bool SendMail(string strSmtpServer, int port, bool ssl, MailPriority priority, string strFrom, string strFromName, string strFromPass, string[] strTos, string strSubject, string strBody, params Attachment[] attachments)
        {
            bool bTip = new bool();
            bTip = false;
            try
            {
                SmtpClient client = new SmtpClient(strSmtpServer, port);
                client.EnableSsl = ssl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(strFrom, strFromPass);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage message = new MailMessage();
                MailAddress mailAddressFrom = new MailAddress(strFrom, strFromName);
                message.From = mailAddressFrom;
                //设置优先级
                message.Priority = priority;
                //添加发送人
                if (strTos != null)
                {
                    foreach (string item in strTos)
                    {
                        message.To.Add(item);
                    }
                }
                //添加主题
                message.SubjectEncoding = Encoding.GetEncoding("gb2312");
                message.Subject = strSubject;
                //添加内容
                message.BodyEncoding = Encoding.GetEncoding("gb2312");
                message.IsBodyHtml = true;
                message.Body = strBody;
                //添加附件
                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        message.Attachments.Add(item);
                    }
                }
                client.Send(message);
                bTip = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return bTip;
        }
    }
}
