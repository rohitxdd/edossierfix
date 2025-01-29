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
using System.Net;
using System.IO;


public partial class Demo : BasePage
{
    Sms objSms = new Sms();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
      // " https://smsgw72.nic.in/sendsms_nic/sendmsg.php?uname=dsssb.auth&pass=H*57Pe$f&send=-DSSSB&dest=ten digit mobile number&msg=hello"

        //string usernm = "dsssb.auth";  // ConfigurationManager.AppSettings["username"].ToString();
        //string pwd = "H*57Pe$f"; //ConfigurationManager.AppSettings["pwd"].ToString();
        //string senderid = "-DSSSB"; // ConfigurationManager.AppSettings["senderid"].ToString();
        //string messgae = "Manindra";
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://smsgw72.nic.in/sendsms_nic/sendmsg.php?uname=" + usernm + "&pass=" + pwd + "&send=" + senderid + "&dest=" + 9871664225 + "&msg=" + "One Time Security Code is" + messgae);
        ///* Assign the response object of ‘WebRequest’ to the ‘WebResponse’
        //variable, response.*/
        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //// Assign the response version to the String variable.
        //String ver = response.ProtocolVersion.ToString();
        ///* Create and assign the response stream to the StreamReader
        //variable.*/
        //StreamReader reader = new StreamReader(response.GetResponseStream());


        string messgae = "2222";
        string mobile = "9871664225";
        objSms.sendmsg(mobile, messgae);

    }
}
