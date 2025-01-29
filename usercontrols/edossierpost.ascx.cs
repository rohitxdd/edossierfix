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

public partial class usercontrols_edossierpost : System.Web.UI.UserControl
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    DataTable dt;
    string jid = "";
    # region Properties

    public string _jid
    {
        get { return jid; }
        set { jid = value; }
    }
   
    
   
    #endregion Properties
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            fill_ddlPost();
        }
    }
    private void fill_ddlPost()
    {
        string regno = Session["rid"].ToString();
        dt = objcd.get_post_eDossier(regno);
        if (dt.Rows.Count > 0)
        {
            DropDownList_post.Items.Clear();
            DropDownList_post.DataTextField = "post";
            DropDownList_post.DataValueField = "jid";
            DropDownList_post.DataSource = dt;
            DropDownList_post.DataBind();
            ListItem l1 = new ListItem();
            l1.Text = "--Select--";
            l1.Value = "";
            DropDownList_post.Items.Insert(0, l1);
            // lbl_msg.Visible = false;
            hfjid.Value = DropDownList_post.SelectedValue;
            _jid = hfjid.Value;
        }
        else
        {
            DropDownList_post.Visible = false;
            lblappno.Visible = false;
            //btn_submit.Visible = false;
            //lbl_msg.Visible = true;
            Session["post"] = "0";
        }
    }
  
}