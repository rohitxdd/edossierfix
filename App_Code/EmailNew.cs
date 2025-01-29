
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace emailnew
{
    public class EmailNew
    {
        public EmailNew() { }

        public bool sendMail(string To, string Cc, string Bcc, string From, string Subject, string Body, string Attachment)
        {
            try
            {
                MailMessage message = new MailMessage();
                ServicePointManager.ServerCertificateValidationCallback += (s, ce, ca, p) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;// | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //SmtpClient smtp = new SmtpClient("164.100.2.239", 465);
                SmtpClient smtp = new SmtpClient("otprelay.nic.in", 465);
                message.From = new MailAddress(From);
                message.Subject = Subject;
                message.To.Add(new MailAddress(To));
                message.IsBodyHtml = true;
                message.Body = Body;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(From, "Delhi@2024");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
