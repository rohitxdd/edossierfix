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


public partial class updateME : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         Filldetail();
        }
    }
    protected void btnrsubmit_Click(object sender, EventArgs e)
    {
        if (txt_mob.Text == "" || txt_email.Text == "")
        {
            msg.Show("Please Enter Mobile No/email");
        }
        else if (Validation.chkLevel(txt_mob.Text))
        {
            msg.Show("Invalid Character in Mobile No");
        }
        else
        {
            int tmp = objcd.updatemobile(Session["rid"].ToString(), txt_mob.Text, txt_email.Text);
            if (tmp > 0)
            {
                msg.Show("Updated successfully");
                Server.Transfer("Home.aspx");
            }
        }
    }
    private void Filldetail()
    {
        DataTable dt = objcd.getdetail(Session["rid"].ToString());
        lbl_regno.Text = Session["rid"].ToString();
        //txt_name.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["name"].ToString()));
        //txtuid.Text = dt.Rows[0]["um_logid"].ToString();
        //txt_fh_name.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["fname"].ToString()));
        //txt_mothername.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["mothername"].ToString()));
        //lblgender.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["gender"].ToString()));
        //txt_reg.Text = dt.Rows[0]["rid"].ToString();
        //lblnation.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["nationality"].ToString()));
        txt_mob.Text = dt.Rows[0]["mobileno"].ToString();
        //Hidden_txtmob.Value = txt_mob.Text;
        txt_email.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["email"].ToString()));
        //get data on basis of registration id
    }
}
