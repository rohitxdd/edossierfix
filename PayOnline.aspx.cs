using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class PayOnline : BasePage
{
    DataTable dt = new DataTable();
    CandidateData objcan = new CandidateData();
    message msg = new message();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    MD5Util md5 = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            string regno = Session["rid"].ToString();
            filljob(regno);
        }
        else
        {
            //if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            //{
            //    //valid Page
            //}
            //else
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        //DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("ddjob");
        lbl_step.Visible = true;
        string applid = ddjob.SelectedValue;

        dt = objcan.getjobno(applid);

        if (dt.Rows.Count == 0)
        {
            msg.Show("Application does not exist.");
        }
        else
        {

            string fee_req = dt.Rows[0]["feereq"].ToString();
            if (fee_req == "Y")
            {
                string url = md5.CreateTamperProofURL("PayOnline2.aspx", null, "applid=" + MD5Util.Encrypt(applid, true) + "&flag=" + MD5Util.Encrypt("P", true));
                Response.Redirect(url);
            }
            else
            {
                lblmsg.Visible = true;
                btn_print_c.Visible = false;
                trcha.Visible = false;
            }
        }


    }
    public void filljob(string regno)
    {
        try
        {
            dt = objcan.get_post_for_payment(regno);
            if (dt.Rows.Count > 0)
            {
                ddjob.Items.Clear();
                ddjob.DataTextField = "post";
                ddjob.DataValueField = "applid";
                ddjob.DataSource = dt;
                ddjob.DataBind();
                ListItem l1 = new ListItem();
                l1.Text = "--Select--";
                l1.Value = "";
                ddjob.Items.Insert(0, l1);
                tblcha.Visible = true;

                if (Request.QueryString["applid"] != null)
                {
                    string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);

                    ddjob.Enabled = false;
                    ddjob.SelectedValue = applid;
                }
            }
            else
            {
                //ddjob.Visible = false;
                truser.Visible = false;
                tblcha.Visible = false;
                LabelNote.Visible = true;
                LabelNote.Text = "Nothing Pending";
                if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
                {
                    LabelNote.Text = "You are not applicable to Pay Online.";

                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }


    protected void btnverifystatus_Click(object sender, EventArgs e)
    {
        lbl_step.Visible = true;
        string applid = ddjob.SelectedValue;

        dt = objcan.getjobno(applid);

        if (dt.Rows.Count == 0)
        {
            msg.Show("Application does not exist.");
        }
        else
        {

            string fee_req = dt.Rows[0]["feereq"].ToString();
            if (fee_req == "Y")
            {
                string url = md5.CreateTamperProofURL("PayOnline2.aspx", null, "applid=" + MD5Util.Encrypt(applid, true) + "&flag=" + MD5Util.Encrypt("V", true));
                Response.Redirect(url);
            }
            else
            {
                lblmsg.Visible = true;
                btn_print_c.Visible = false;
                trcha.Visible = false;
            }
        }
    }
}