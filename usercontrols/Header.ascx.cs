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


public partial class usercontrols_Header : System.Web.UI.UserControl
{
    CandidateData objCandD = new CandidateData();
    protected void Page_Load(object sender, EventArgs e)
    {
        string currentdate = Utility.formatDateinDMYWithTime(DateTime.Now.ToString());

        lbldatetime.Text = currentdate;
        if (Session["rid"] != null)
        {
           DataTable dtdata = objCandD.getdetail(Session["rid"].ToString());
            //lblname.Text = Session["name"].ToString() + ": Registration No: " + Session["rid"].ToString() ;
           lblname.Text = dtdata.Rows[0]["name"].ToString() + ": Registration No: " + Session["rid"].ToString();
            lblname.Visible = true;
        }
        else
        {
            lblname.Visible = false;
            lblname.Text = "";
        }
    }
}
