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
using System.Data ;

public partial class Challengepdf : Page
{
    challengeansheet objchallge= new challengeansheet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ChallengeID"].ToString() != null)
        {
            string ChallengeID = MD5Util.Decrypt(Request.QueryString["ChallengeID"].ToString(), true);
            GetChallengePDf(ChallengeID);
        }
    }

    public void GetChallengePDf(string ChallengeID)
    {
        DataTable dt = new DataTable();
        dt = objchallge.GetChallengePdf(ChallengeID);
        if (dt.Rows.Count > 0)
        {
            byte[] file = (byte[])dt.Rows[0]["Cdoc"];
            string ext = dt.Rows[0]["ext"].ToString();
            if (ext == ".pdf")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=file1.pdf");     // to open file prompt Box open or Save file
            }
            else
            {
                Response.ContentType = "application/jpeg";
                Response.AddHeader("content-disposition", "attachment;filename=file1.jpeg");     // to open file prompt Box open or Save file
            }
            Response.BinaryWrite(file);
            Response.End();
        }
    }

  
}
