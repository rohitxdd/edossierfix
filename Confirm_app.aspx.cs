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


public partial class Confirm_app : BasePage
{
    message msg = new message();
    DataTable dt = new DataTable();
    MD5Util md5util = new MD5Util();
    CandidateData cd = new CandidateData();
    string applid = "";
    string jid = "";
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            string regno = Session["rid"].ToString();
            fill_dll(regno);
            if (Request.QueryString["applid"] != null)
            {
                tblcon.Visible = false;
                DropDownList_post.SelectedValue = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                btn_confirm_Click(sender, e);
            }
        }
        else
        {
            if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
        
    }

    private void fill_dll(string regno)
    {
        dt = cd.get_postforfinalsubmit(regno);
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
            tblcon.Visible = false;            
            Session["post"] = "0";
            lblmsg.Visible = true;
        }
    }
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        Session.Remove("dum");
        lbl_step.Visible = true;
        img_btn_prev.Visible = true;
        string applid = DropDownList_post.SelectedValue;
                        
        if (applid != "")
        {
            if (Validation.chkescape(applid))
            {
                msg.Show("Invalid Character in Post Applied");
            }
            else
            {
                DataTable dt = cd.Getappno(applid);                
                if (dt.Rows.Count == 0)
                {
                    msg.Show("Application does not exist.");
                }
                else
                {
                    string url = md5util.CreateTamperProofURL("EditApplication.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                    Response.Redirect(url);
                }
            }
        }
        else
        {
            msg.Show("Invalid Value in Post Selected");
        }
    }
    protected void img_btn_prev_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["applid"] != null)
        {
            string Applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            Response.Redirect(md5util.CreateTamperProofURL("Experience.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(Applid, true)));
        }
        else
        {
            Response.Redirect("home.aspx");
        }

    } 
}
