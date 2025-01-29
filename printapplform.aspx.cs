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

public partial class printapplform : BasePage
{
    message msg = new message();
    MD5Util md5util = new MD5Util();
    CandidateData cd = new CandidateData();
    DataTable dt = new DataTable();
    string applid = "";
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
   
            //if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            //{
            //    //valid Page
            //}
            //else
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}
        }
        string regno = Session["rid"].ToString();
        dt = cd.get_post_print_app(regno);
        if (dt.Rows.Count == 0)
        {
            truser.Visible = false;
            Button1.Visible = false;
            trfrm.Visible = false;
            lblmsg.Visible = true;
        }

        //if (Request.QueryString["applid"] != null)
        //{
        //    applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        //    string url = md5util.CreateTamperProofURL("Application.aspx", null, "flag=" + MD5Util.Encrypt("print", true));
        //    ClientScript.RegisterStartupScript(this.GetType(), "CalValue", "<script>javascript:window.open('" + url + "','_blank')</script>");           

        //}
        //else
        //{
        //    DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("ddjob");
        //    ddlpost.SelectedValue = applid;
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("ddjob");
        string applid = ddlpost.SelectedValue;

        dt = cd.getjobno(applid);
        if (dt.Rows.Count == 0)
        {
            msg.Show("Application does not exist.");
        }
        else
        {
            Session["Print_applid"] = applid;
            string url = md5util.CreateTamperProofURL("Application.aspx", null, "flag=" + MD5Util.Encrypt("print", true));
            //OnClientClick="javascript:window.open('Application.aspx','','left=50,top=50,width=800,height=800');"
            ClientScript.RegisterStartupScript(this.GetType(), "CalValue", "<script>javascript:window.open('" + url + "','_blank')</script>");           
        }
    }
    
}
