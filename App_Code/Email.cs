using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using System.IO;
using System.Text;
using emailnew;
using System.Net.Mail;
using System.Net;


public class Email
{
    
	public Email()
	{
       
	}

    //public bool sendMail(string To, string Cc, string Bcc, string From, string Subject, string Body, string Attachment)
    //{
    //    try
    //    {
    //        // SmtpMail.SmtpServer = "relay.nic.in";
    //        SmtpMail.SmtpServer = "164.100.14.95";
    //        MailMessage mail = new MailMessage();
    //        mail.To = To;
    //        mail.Cc = Cc;
    //        mail.Bcc = Bcc;
    //        mail.From = From;
    //        mail.Subject = Subject;
    //        mail.Body = Body;
    //        if (Attachment != "")
    //        {
    //            MailAttachment attach = new MailAttachment(Attachment);
    //            mail.Attachments.Add(attach);
    //        }
    //        SmtpMail.Send(mail);

    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        //HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + ex.Message + "')</script>");
    //        return false;
    //    }
    //}
    //public bool sendMailAC(string To, string Cc, string Bcc, string From, string Subject, string Body, string Attachment)
    //{
    //    try
    //    {
    //        // SmtpMail.SmtpServer = "relay.nic.in";
    //        SmtpMail.SmtpServer = "164.100.14.95";
    //        MailMessage mail = new MailMessage();
    //        mail.To = To;
    //        mail.Cc = Cc;
    //        mail.Bcc = Bcc;
    //        mail.From = From;
    //        mail.Subject = Subject;
    //        mail.Body = Body;
    //        MailAttachment attach = new MailAttachment(Attachment);
    //        mail.Attachments.Add(attach);
    //        SmtpMail.Send(mail);

    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        //HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + ex.Message + "')</script>");
    //        return false;
    //    }
    //}

    //private void SendEmail(string To, string Cc, string Bcc, string From, string Subject, string Body, string Attachment)
    //{
    //    try
    //    {
    //        MailMessage message = new MailMessage();
    //        ServicePointManager.ServerCertificateValidationCallback += (s, ce, ca, p) => true;
    //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;// | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
    //        //SmtpClient smtp = new SmtpClient("164.100.2.239", 465);
    //        SmtpClient smtp = new SmtpClient("otprelay.nic.in", 465);
    //        message.From = new MailAddress(From);
    //        message.Subject = Subject;
    //        message.To.Add(new MailAddress(To));
    //        message.IsBodyHtml = true;
    //        message.Body = Body;
    //        smtp.UseDefaultCredentials = false;
    //        smtp.EnableSsl = true;
    //        smtp.Credentials = new NetworkCredential(From, "Dss@2024");
    //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
    //        smtp.Send(message);
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}


    public bool sendMail(string To, string Cc, string Bcc, string From, string Subject, string Body, string Attachment)
    {
        try
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
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
