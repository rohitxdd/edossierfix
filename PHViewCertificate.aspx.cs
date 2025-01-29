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

using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
public partial class PHViewCertificate : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        // {
        // openpdf();
        // }
        if (!IsPostBack)
        {
            string flag = MD5Util.Decrypt(Request.QueryString["flag"].ToString(), true);
            if (flag == "fn")
            {
                string fn = MD5Util.Decrypt(Request.QueryString["fn"].ToString(), true);
                openpdf();
            }
            else
            {
                string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                UpdateReadPhDoc(sender, e);
            }
        }
    }

    private void UpdateReadPhDoc(object sender, EventArgs e)
    {
        CandidateData objCandD = new CandidateData();
        string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        int applid1 = Convert.ToInt32(applid);
        DataTable dt1 = objCandD.Get_PhFileDoc(applid1);
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            Page.ClientScript.RegisterStartupScript(e.GetType(), "MyScript", "javascript:window.print();", true);
            byte[] file = (byte[])dt1.Rows[0]["PhCertificateFile"];
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.AddHeader("content-disposition", "attachment;filename=Application.pdf");
            Response.ContentType = "application/pdf";
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(file);
            Response.End();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(e.GetType(), "MyScript", "javascript:window.close();", true);
        }
    }
    private void openpdf()
    {
        CandidateData objCandD = new CandidateData();
        string reqid = MD5Util.Decrypt(Request.QueryString["fn"].ToString(), true);
        if (reqid != "" || reqid != null)
        {
            System.Net.WebClient user = new WebClient();
            byte[] filebuffer = user.DownloadData(reqid);
            if (filebuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", filebuffer.Length.ToString());
                Response.BinaryWrite(filebuffer);
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
}