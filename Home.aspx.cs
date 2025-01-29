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

public partial class Home : BasePage
{
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    MD5Util md5util = new MD5Util();
    CandidateData objcd = new CandidateData();
    message msg = new message();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["rid"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            //fill_message();

            getStatusIfCandidateIn116();
        }
        //else
        //{
        //    if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
        //    {
        //        //valid Page
        //    }
        //    else
        //    {
        //        //Response.Redirect("Default.aspx");
        //       Response.Redirect("ErrorPage.aspx");
        //    }
        //}
    }
    private void getStatusIfCandidateIn116()
    {
        try
        {
            DataTable dt = objcd.getStatusIfCandidateIn116(Session["rid"].ToString());
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = objcd.getLinkEnableDisableStatus("116postCode");
                if (dt1.Rows.Count > 0)
                {
                    trUploadDocPCSP.Visible = true;
                }
                else
                {
                    trUploadDocPCSP.Visible = false;
                }
            }
            else
            {
                trUploadDocPCSP.Visible = false;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void txtblnk_Click(object sender, EventArgs e)
    {
        DataTable dt = objcd.getLinkEnableDisableStatus("116postCode");
        if (dt.Rows.Count > 0)
        {
            Response.Redirect("insertAdharByCandidate.aspx?linkClicked=" + MD5Util.Encrypt("RD", true));
        }
        else
        {
            msg.Show("Last date to upload ID proof and postcard size photograph is over.");
        }
    }
}
