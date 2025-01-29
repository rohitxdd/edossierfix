using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
/// <summary>
/// Summary description for Sms
/// </summary>
public class Sms
{
    string mobilenos;
    string message;
    string dlt_templateid;

    public Sms()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void sendmsg(string mobilenos, string message)
    {
        this.mobilenos = mobilenos;
        this.message = message;

        ThreadPool.QueueUserWorkItem(this.ThreadPoolCallback);

        //Thread MyThread = new Thread(new ThreadStart
        //(MyCallbackFunction));
        //MyThread.Start();

        //try
        //{
        //    string usernm = "dsssb.auth";  // ConfigurationManager.AppSettings["username"].ToString();
        //    string pwd = "H*57Pe$f"; //ConfigurationManager.AppSettings["pwd"].ToString();
        //    string senderid = "-DSSSB"; // ConfigurationManager.AppSettings["senderid"].ToString();
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://164.100.14.72/sendsms_nic/sendmsg.php?uname=" + usernm + "&pass=" + pwd + "&send=" + senderid + "&dest=" + Int64.Parse(mobilenos) + "&msg=" + message);
        //    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://smsgw72.nic.in/sendsms_nic/sendmsg.php?uname=dsssb.auth&pass=H*57Pe$f&send=-DSSSB&dest=9560338840&msg=hello");
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    // Assign the response version to the String variable.
        //    String ver = response.ProtocolVersion.ToString();

        //    /* Create and assign the response stream to the StreamReader
        //    variable.*/
        //    StreamReader reader = new StreamReader(response.GetResponseStream());
        //}
        //catch (Exception ex)
        //{

        //    message msg = new message();
        //    msg.Show("Inside function");
        //    msg.Show(ex.Message);
        //    //throw ex;
        //} 
    }

    public void ThreadPoolCallback(Object threadContext)
    {
        try
        {
            //string usernm = "lgpost.auth";  // ConfigurationManager.AppSettings["username"].ToString();
            //string pwd = "Y*re4$5Ed"; //ConfigurationManager.AppSettings["pwd"].ToString();
            //string senderid = "NICSMS"; // ConfigurationManager.AppSettings["senderid"].ToString();
            string usernm = "dsssb.auth";  // ConfigurationManager.AppSettings["username"].ToString();
            string pwd = "H*57Pe$f"; //ConfigurationManager.AppSettings["pwd"].ToString();
            string senderid = "DLOARS"; // ConfigurationManager.AppSettings["senderid"].ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + usernm + "&pin=" + pwd + "&message=" + message + "&mnumber=" + Int64.Parse(mobilenos) + "&signature=" + senderid + "&Dlrtype=1");
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            // https://smsgw.sms.gov.in/failsafe/HttpLink?username=dsssb.auth&pin=H*57Pe$f&message=hello&mnumber=9971788155&signature=DLOARS

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            request.Method = "POST";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Assign the response version to the String variable.
            String ver = response.ProtocolVersion.ToString();

            /* Create and assign the response stream to the StreamReader
            variable.*/
            StreamReader reader = new StreamReader(response.GetResponseStream());
        }
        catch (Exception ex)
        {

            message msg = new message();
            msg.Show("Inside function");
            msg.Show(ex.Message);
            //throw ex;
        }
    }

    public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        if (((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors))
        {
            return true;
        }
        else if (((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch))
        {
            Zone z = default(Zone);
            z = Zone.CreateFromUrl(((HttpWebRequest)sender).RequestUri.ToString());
            if ((z.SecurityZone == System.Security.SecurityZone.Intranet | z.SecurityZone == System.Security.SecurityZone.MyComputer))
            {
                return true;
            }
            return true;
        }
        else
        {
            return true;
        }
        //return true;
    }

    // this implementation is as per new TRAI rule for sending sms

    public void sendmsgNew(string mobilenos, string message, string templateID)
    {
        this.mobilenos = mobilenos;
        this.message = message;
        this.dlt_templateid = templateID;
        ThreadPool.QueueUserWorkItem(this.ThreadPoolCallbackNew);
    }
    public void ThreadPoolCallbackNew(Object threadContext)
    {
        try
        {
            //string usernm = "lgpost.auth";  // ConfigurationManager.AppSettings["username"].ToString();
            //string pwd = "Y*re4$5Ed"; //ConfigurationManager.AppSettings["pwd"].ToString();
            //string senderid = "NICSMS"; // ConfigurationManager.AppSettings["senderid"].ToString();
            string usernm = "dsssb.auth";  // ConfigurationManager.AppSettings["username"].ToString();
            string pwd = "H*57Pe$f"; //ConfigurationManager.AppSettings["pwd"].ToString();
            string senderid = "DLOARS"; // ConfigurationManager.AppSettings["senderid"].ToString();
            string dlt_entityid = "1001395840000022605";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/MLink?username=" + usernm + "&pin=" + pwd + "&message=" + message + "&mnumber=" + Int64.Parse(mobilenos) + "&signature=" + senderid +  "&dlt_entity_id=" + dlt_entityid+ "&dlt_template_id=" + dlt_templateid);
           
            
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/MLink?username=" + usernm + "&pin=" + pwd + "&message=" + message + "&mnumber=" + Int64.Parse(mobilenos) + "&signature=" + senderid + "&dlt_entity_id=" + dlt_entityid + "&dlt_template_id=" + dlt_templateid);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + usernm + "&pin=" + pwd + "&message=" + message + "&mnumber=" + Int64.Parse(mobilenos) + "&signature=" + senderid + "&dlt_entity_id=" + dlt_entityid + "&dlt_template_id=" + dlt_templateid);

           
            
            //HttpWebRequest request=(HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/MLink?username=dsssb.auth&pin=H*57Pe$f&message=OTP for resetting password of your DSSSB online login account is 3376&mnumber=9205505932&signature=DLOARS&dlt_entity_id=1001395840000022605&dlt_template_id=1007161562148943825")

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            request.Method = "POST";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Assign the response version to the String variable.
            String ver = response.ProtocolVersion.ToString();

            /* Create and assign the response stream to the StreamReader
            variable.*/
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string ResponseStatus = response.StatusCode.ToString();
            string ResponseString = reader.ReadLine();
        }
        catch (Exception ex)
        {

            message msg = new message();
            msg.Show("Inside function");
            msg.Show(ex.Message);
            //throw ex;
        }
    }
}
