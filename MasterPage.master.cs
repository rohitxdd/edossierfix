using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;


public partial class MasterPage : System.Web.UI.MasterPage
{
    int sid;
    MD5Util md5util = new MD5Util();
    Marks objmarks = new Marks();
    CandidateData objCandD = new CandidateData();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //masterbody.Attributes.Add("onload", "GoBack();");  
        lbtnApplyOnline.Attributes.Add("style", "text-decoration:blink");
       if (Session["rid"] != null && Session["rid"].ToString() == "111111111111112012")
        {
            btnhome.Visible = false;
            ButtonFeeVerification.Visible = false;
            btnprintapplication.Visible = false;
            btnupdatemob.Visible = false;
        }
        else
        {
            btn_audit.Visible = false;
        }
        if (!IsPostBack)
        {
            if (Session["rid"] != null)
            {
                DataTable dt = objmarks.GetexamidforAdmitCC(Session["rid"].ToString(), "0");
                if (dt.Rows.Count == 0)
                {
                    lbtVeriAdmitCardConsent.Visible = false;
                }
                else
                {
                    lbtVeriAdmitCardConsent.Visible = true;
                }
            }
            else
            {
                lbtVeriAdmitCardConsent.Visible = true;
            }
        }

    }
    
    protected void btnhome_Click(object sender, EventArgs e)
    {
        Server.Transfer("Home.aspx");

    }
    protected void btneditapp_Click(object sender, EventArgs e)
    {

    }
    protected void btnlogout_Click(object sender, EventArgs e)
    {
	string foldPathPCP = "~/SelectedPostCardPhoto/" + Session["rid"].ToString();
        string foldPathIDP = "~/SelectedIDProofImage/" + Session["rid"].ToString();

        if (Directory.Exists(Server.MapPath(foldPathPCP)))
        {
            Directory.Delete(Server.MapPath(foldPathPCP), true);
        }
        if (Directory.Exists(Server.MapPath(foldPathIDP)))
        {
            Directory.Delete(Server.MapPath(foldPathIDP), true);
        }
        Server.Transfer("Logout.aspx");

    }
    protected void ButtonFeeVerification_Click(object sender, EventArgs e)
    {
        Server.Transfer("FeeVerification.aspx");
    }
    protected void btnprintapplication_Click(object sender, EventArgs e)
    {
        //Request.QueryString["applid"] = null;
        Server.Transfer("printapplform.aspx");
    }
    protected void btnpass_Click(object sender, EventArgs e)
    {
        ///////candidate activity log////////
        string regno = Session["regno"].ToString();
        string ip = GetIPAddress();
        //objCandD.InsertIntoCandidateAcivityLog(regno, "True", "Proceeded to change password", ip);
        Server.Transfer("Changepwd.aspx");
    }
    protected void btnupdatemob_Click(object sender, EventArgs e)
    {
        ///////candidate activity log////////
        string regno = Session["regno"].ToString();
        string ip = GetIPAddress();
       // objCandD.InsertIntoCandidateAcivityLog(regno, "True", "Update Registration Details", ip);
        Server.Transfer("updatemobile.aspx");
    }
    protected void btn_audit_Click(object sender, EventArgs e)
    {
        Server.Transfer("AuditDetails.aspx");
    }


    private void lbtnApplyOnline_Click(object sender, System.EventArgs e)
    {
        //Dim url As String
        //If (DateTime.Now.Second Mod 2) = 0 Then
        //    url = ConfigurationManager.AppSettings("SiteUrl1").ToString
        //Else
        //    url = ConfigurationManager.AppSettings("SiteUrl2").ToString
        //End If
        Response.Redirect("AdvtList.aspx?type=curr");
    }

    private void lbtnConfAppl_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("EditApplication.aspx?opt=confirm");
    }

    private void lbtnEditAppl_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("EditApplication.aspx");
    }

    //Private Sub lbtnPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnPayment.Click

    //    Response.Redirect("Payment.aspx")
    //End Sub

    private void lbtnPrintAppl_Click(object sender, System.EventArgs e)
    {
        //Dim script As String = " <script> "
        //script += " alert('Due to technical Difficulties Printing is stopped and it will be available in short time.');"
        //script += " </script> "
        //Page.ClientScript.RegisterStartupScript(GetType(String), "", script)
        //Exit Sub
        Response.Redirect("PrintApplFormStatic.aspx");
    }

    private void lbtnPrintCallLetter_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("PrintApplForm.aspx?opt=call");
    }

    private void lbtnUploadPhoto_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("jobupload.aspx");
    }

    protected void lbtnMainexamcallletter_Click(object sender, EventArgs e)
    {
        Response.Redirect("Preference.aspx");
    }

    protected void lbtnApplyOnline_Click1(object sender, EventArgs e)
    {
        Response.Redirect("AdvtList.aspx");
    }
    protected void lbtnEditAppl_Click1(object sender, EventArgs e)
    {
        string url = md5util.CreateTamperProofURL("Update_app.aspx", null, "update=" + MD5Util.Encrypt("1", true));
        Response.Redirect(url);
    }

    protected void btnuploadedossier_Click(object sender, EventArgs e)
    {
        //Request.QueryString["applid"] = null;
       // Response.Redirect("uploadedossier.aspx");
        Response.Redirect("Edossier_PerInfo.aspx");
        
    }
    protected void btnchallenge_Click(object sender, EventArgs e)
    {
        Response.Redirect("challengeanswersheet.aspx");
    }
    protected void btnviewupload_Click(object sender, EventArgs e)
    {
        Response.Redirect("viewmarks.aspx");
    }

    protected void lbtVeriAdmitCardConsent_Click(object sender, EventArgs e)
    {
      
    }
    //protected void btnedossier_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Edossier_PerInfo.aspx");
    //}
    protected void btfanskey_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewFinalAnsKey.aspx");
    }

    public string GetIPAddress()
    {
        string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            ipAddress = ipAddress.Split(',')[0];
        }
        return ipAddress;
    }
}
