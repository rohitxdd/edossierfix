using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

using System.Net;
using System.IO;
public partial class TestSms : BasePage
{
    //Service s = new Service();
    Sms objsms = new Sms();
    message msg = new message();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("http://10.128.65.106/dsssbonline");
    }
    protected void btnsms_Click(object sender, EventArgs e)
    {
        string mobile = "9873973194";
        string code = "Welcome DsssbOnline";
        //objsms.sendmsg(mobile, code);
        sendmsg(mobile, code);
        //try
        //{
        //    s.invokesms(mobile, code);
        //}
        //catch (Exception ex)
        //{
        //    msg.Show(ex.Message);
        //}

        //mobile = "9971788155";
        //code = "Welcome DsssbOnline";
        //objsms.sendmsg(mobile, code);
    }

    public void sendmsg(string mobilenos, string code)
    {
        try
        {
            //Service sms = new Service();
            //sms.invokesms(mobilenos, message);

            string usernm = "dsssb.auth";  // ConfigurationManager.AppSettings["username"].ToString();
            string pwd = "H*57Pe$f"; //ConfigurationManager.AppSettings["pwd"].ToString();
            string senderid = "-DSSSB"; // ConfigurationManager.AppSettings["senderid"].ToString();


            //http://smsgw.sms.gov.in/failsafe/HttpLink?username=dsssb.auth&pin=H*57Pe$f&message=hello&mnumber=9873973194&signature=-DSSSB
            //http://smsgw.sms.gov.in/failsafe/HttpLink?uname=dsssb.auth&pin=H*57Pe$f&message=hello&mnumber=919873973194&signature=KISAAN

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://smsgw.sms.gov.in/failsafe/HttpLink?username=" + usernm + "&pin=" + pwd + "&message=" + code + "&mnumber=" + Int64.Parse(mobilenos) +"&signature=" + senderid);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://smsgw72.nic.in/sendsms_nic/sendmsg.php?uname=dsssb.auth&pass=H*57Pe$f&send=-DSSSB&dest=9560338840&msg=hello");
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
}
