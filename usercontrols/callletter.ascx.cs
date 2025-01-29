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

public partial class usercontrols_callletter : System.Web.UI.UserControl
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    DataTable dt;  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string regno = Session["rid"].ToString();
            filljob(regno);
        }
    }
       
    public void filljob(string regno)
        {
        try
        {

            dt = objcd.get_post_after_c(regno);
            ddjob.Items.Clear();
            ddjob.DataTextField = "post";
            ddjob.DataValueField = "applid";
            ddjob.DataSource = dt;
            ddjob.DataBind();
            ListItem l1 = new ListItem();
            l1.Text = "--Select--";
            l1.Value = "";
            ddjob.Items.Insert(0, l1);
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }
    public string Temp_refNo
    {

        get { return ddjob.SelectedValue; }
        set { ddjob.SelectedValue = value; }
    }
}
