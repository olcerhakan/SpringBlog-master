using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace SpringBlog.Helpers
{
    public static class EmailUtilities
    {
        // mail gönderirken 3 şey. body,destination ,subject
        public static async Task SendEmailAsync(string destination, string subject, string body)
        {
            // http://csharp.net-informations.com/communications/csharp-smtp-mail.htm
            MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("hakanolcer.xyz");
            //SmtpClient smtpClient = new SmtpClient("hakanolcer.xyz");


            mail.From = new MailAddress("deneme@hakanolcer.xyz");
            mail.To.Add(destination);
            mail.Subject = subject;
            mail.Body =body;
            mail.IsBodyHtml = true;  //gmail
                                               //  "hakanolcer.xyz" bunu sildik.webconfig host ta zaten giriyoruz
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["mailAccount"], 
                    ConfigurationManager.AppSettings["mailPassword"]
                    
                 );
               await smtpClient.SendMailAsync(mail);
                //async oldugu icin awaitle cagırırız. bunu çağırınca mail sunucu geç cevap verdi . o 1 dk boyunca bu işlem sonucunu beklet. sunucu başka işlemler yapıyor. await olması için async metot olmalı ve await in kullandıgı metot Async kelimesi geçmeli
            }
            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("deneme@hakanolcer.xyz", "Ankara11!!");
            //SmtpServer.EnableSsl = true;


            //SmtpServer.Send(mail);
            //SmtpServer.Dispose();
            //smtpClient.Send(mail);
            //smtpClient.Dispose();
        }
    }
}