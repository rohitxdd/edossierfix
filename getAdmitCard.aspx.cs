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


public partial class getAdmitCard : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fill_ddl();
        }
    }

    private void fill_ddl()
    {
        string regno = Session["rid"].ToString();
        dt = objcd.get_post_admit(regno);
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
            lbl_msg.Visible = false;
        }
        else
        {
            DropDownList_post.Visible = false;
            lblappno.Visible = false;
            btn_submit.Visible = false;
            lbl_msg.Visible = true;
            Session["post"] = "0";
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        DataTable dt = objcd.CheckValidAdmitCard(Int32.Parse(DropDownList_post.SelectedValue));
        MD5Util md5util = new MD5Util();
        string url = md5util.CreateTamperProofURL("admitcard.aspx", null, "applid=" + MD5Util.Encrypt(DropDownList_post.SelectedValue, true) + "&examid=" + MD5Util.Encrypt(dt.Rows[0]["examid"].ToString(), true));
        Response.Redirect(url);
    }
}
