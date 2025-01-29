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


public partial class Update_app : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    MD5Util md5util = new MD5Util();
    DataTable dt = new DataTable();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();

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

        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");

        string regno = Session["rid"].ToString();
        dt = objcd.get_post(regno);
        if (dt.Rows.Count == 0)
        {
            Button_Vaidate.Visible = false;
            lblmsg.Visible = true;
        }
    }
    protected void Button_Vaidate_Click(object sender, EventArgs e)
    {
        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
        string applid = ddlpost.SelectedValue;
        if (applid != "")
        {
            Response.Redirect(md5util.CreateTamperProofURL("apply.aspx", null, "update=" + MD5Util.Encrypt("1", true) + "&applid=" + MD5Util.Encrypt(applid, true)));
        }
        else
        {
            msg.Show("Invalid Post select.");
        }
    }
}
