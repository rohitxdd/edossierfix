using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class SMSOTP : BasePage
{
    CandidateData can = new CandidateData();
    Sms objsms = new Sms();
    message msg = new message();
    protected void Page_Load(object sender, EventArgs e)
    {
        sendOTP();
    }
    public void sendOTP()
    {
        DataTable dt = can.getOTP();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string mobile = dt.Rows[i]["mobileno"].ToString();
            string security = dt.Rows[i]["randomno"].ToString();
            string code = "Your Security Code for Change Password is : " + security;

           

            //objsms.sendmsg(mobile, code);
        }
      
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            string id = dt.Rows[j]["id"].ToString();
            can.insert_OTP(id);           
        }
       // msg.Show("Messages have been sent.");
        Response.Redirect("default.aspx");
       
    }
}
