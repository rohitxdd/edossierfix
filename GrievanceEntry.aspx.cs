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

public partial class GrievanceEntry : BasePage
{
    CandidateData objcd = new CandidateData();
    Grievance objgc = new Grievance();
    message msg = new message();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    MD5Util md5util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            fillddlcat();
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
    protected void rblwise_SelectedIndexChanged(object sender, EventArgs e)
    {
        string regno=Session["rid"].ToString();
        if (rblwise.SelectedValue == "P")
        {
            trpost.Visible = true;
            trexam.Visible = false;
            fillappliedpost(regno);
        }
        else
        {
            trexam.Visible = true;
            trpost.Visible = false;
            fillreleasedexam(regno);
        }
    }
    public void fillappliedpost(string regno)
    {
        try
        {
           DataTable dt = objcd.get_app_status(regno);
            if (dt.Rows.Count > 0)
            {
                ddlpost.Items.Clear();
                ddlpost.DataTextField = "post";
                ddlpost.DataValueField = "jid";
                ddlpost.DataSource = dt;
                ddlpost.DataBind();
                ddlpost.Items.Insert(0, Utility.ddl_Select_Value());
            }
           
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }
    public void fillreleasedexam(string regno)
    {
        try
        {
            DataTable dt = objgc.getcandidatereleasedexams(regno);
            if (dt.Rows.Count > 0)
            {
                ddleaxm.Items.Clear();
                ddleaxm.DataTextField = "exam";
                ddleaxm.DataValueField = "examid";
                ddleaxm.DataSource = dt;
                ddleaxm.DataBind();
                ddleaxm.Items.Insert(0, Utility.ddl_Select_Value());
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdesc.Text == "")
            {
                msg.Show("Please enter Grievance Discription");
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    public void fillddlcat()
    {
        try
        {
            DataTable dt = objgc.getGcat();
            if (dt.Rows.Count > 0)
            {
                ddlcat.Items.Clear();
                ddlcat.DataTextField = "GCDesc";
                ddlcat.DataValueField = "GCID";
                ddlcat.DataSource = dt;
                ddlcat.DataBind();
                ddlcat.Items.Insert(0, Utility.ddl_Select_Value());
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }
    public void fillddlsubcat()
    {
        try
        {
            DataTable dt = objgc.getGSubcat(ddlcat.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlsubcat.Items.Clear();
                ddlsubcat.DataTextField = "GCDesc";
                ddlsubcat.DataValueField = "GSCID";
                ddlsubcat.DataSource = dt;
                ddlsubcat.DataBind();
                ddlsubcat.Items.Insert(0, Utility.ddl_Select_Value());
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }
    protected void ddlcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlsubcat();
    }
}