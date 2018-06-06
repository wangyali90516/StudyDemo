using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace DesignPatternDemo
{
    internal class Program
    {
        //private static void Main(string[] args)
        //{
        //    try
        //    {
        //        string emailAcount = ConfigurationManager.AppSettings["SendAcount"];
        //        string emailPassword = ConfigurationManager.AppSettings["SendPwd"];
        //        string reciver = ConfigurationManager.AppSettings["reciver"];
        //        string content = ConfigurationManager.AppSettings["content"];
        //        MailMessage message = new MailMessage();
        //        //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
        //        MailAddress fromAddr = new MailAddress(emailAcount);
        //        message.From = fromAddr;
        //        //设置收件人,可添加多个,添加方法与下面的一样
        //        message.To.Add(reciver);
        //        //设置邮件标题
        //        message.Subject = ConfigurationManager.AppSettings["Subject"];
        //        //设置邮件内容
        //        message.Body = content;
        //        //设置邮件发送服务器,服务器根据你使用的邮箱而不同 //设置发送人的邮箱账号和密码
        //        SmtpClient client = new SmtpClient("smtp.jinyinmao.com.cn", 25)
        //        {
        //            Credentials = new NetworkCredential(emailAcount, emailPassword)
        //        };
        //        //发送邮件
        //        client.Send(message);
        //        Console.Write("成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //        //throw ex;
        //    }
        //    Console.ReadKey();
        //}
    }
}