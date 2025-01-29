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
using System.Threading;
using System.IO;

public partial class ViewFinalAnsKey : BasePage
{
    challengeansheet objchallenge = new challengeansheet();
    message msg = new message();
    DataTable dt = new DataTable();
    DataTable dtques = new DataTable();
    MD5Util md5util = new MD5Util();
    Utility Utlity = new Utility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillddlexam();
        }
    }
    private void fillddlexam()
    {
        try
        {
            string regno = Session["rid"].ToString();
            dt = objchallenge.getrevanskeyExam(regno);

            ddlexam.DataTextField = "examdtl";
            ddlexam.DataValueField = "examid";
            ddlexam.DataSource = dt;
            ddlexam.DataBind();
            ddlexam.Items.Insert(0, Utility.ddl_Select_Value());
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ddlexam.SelectedValue != "")
        {
            trques.Visible = true;
            lblans.Text = "";
            fillddlques();
        }
        else
        {
            trques.Visible = false;
            msg.Show("Please select Exam");
        }
    }
    protected void ddlexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexam.SelectedValue != "")
            {
                string examid = ddlexam.SelectedValue;
                string batchid = "";
                int index = examid.IndexOf(":");
                if (index > 0)
                {
                    batchid = examid.Substring(index + 1);
                    examid = examid.Substring(0, index);
                }
                hfbatchid.Value = batchid;
                hfexamid.Value = examid;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void fillddlques()
    {
        try
        {
            dtques = objchallenge.GetFinalAnsKey(hfexamid.Value, hfbatchid.Value,"");
            ddlques.DataTextField = "QuestionNo";
            ddlques.DataValueField = "QuestionNo";
            ddlques.DataSource = dtques;
            ddlques.DataBind();
            ddlques.Items.Insert(0, Utility.ddl_Select_Value());
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void ddlques_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlques.SelectedValue != "")
        {
            DataTable dtq = objchallenge.GetFinalAnsKey(hfexamid.Value, hfbatchid.Value, ddlques.SelectedValue);
           if (dtq.Rows.Count > 0)
            {
                lblans.Text = dtq.Rows[0]["answer"].ToString();
            }
        }
    }
}