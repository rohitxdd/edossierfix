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
using System.Web.SessionState;

public partial class usercontrols_appnumber : System.Web.UI.UserControl
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string regno = Session["rid"].ToString();
            fill_dll(regno);
        }
    }
    public void fill_dll(string regno)
    {
        
       dt= objcd.get_post(regno);
       if (dt.Rows.Count > 0)
       {
           DropDownList_post.Items.Clear();
           DropDownList_post.DataTextField = "post";
           DropDownList_post.DataValueField = "applid";
           DropDownList_post.DataSource = dt;
           DropDownList_post.DataBind();
           ListItem l1 = new ListItem();
           l1.Text = "--Select--";
           l1.Value = "";
           DropDownList_post.Items.Insert(0, l1);
       }
       else
       {
           DropDownList_post.Visible = false;
           lblappno.Visible = false;
           Session["post"] = "0";
       }

    }
    public string Temp_refNo
    {      
        
        get { return DropDownList_post.SelectedValue; }
        set { DropDownList_post.SelectedValue = value; }
    }
    
    
}

