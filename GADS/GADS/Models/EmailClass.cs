using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace GADS.Models
{
    public class EmailClass
    {
        public void SendEmail(string strSubject, string strMessage, string receipient)
        {
          try
            {

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["fromEmail"]);

                message.To.Add(new MailAddress(receipient));
                message.Subject = strSubject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = strMessage;
                smtp.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SmptPort"]);
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["Host"]; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["UserCredentials"], System.Configuration.ConfigurationManager.AppSettings["PasswordCredentials"]);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
               System.Security.Cryptography.X509Certificates.X509Certificate certificate,
               System.Security.Cryptography.X509Certificates.X509Chain chain,
               System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smtp.Send(message);

            }
          
            catch (Exception ex)
            {
              
            }
        }
    }
 }