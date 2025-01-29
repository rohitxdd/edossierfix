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

public partial class advtpdf : Page
{
    CandidateData objCandD = new CandidateData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["adid"].ToString() != null)
        {
            string adid = MD5Util.Decrypt(Request.QueryString["adid"].ToString(), true);
            get_advt_pdf(adid);

        }
    }

    public void get_advt_pdf(string adid)
    {

        DataTable dt = new DataTable();
        dt = objCandD.Get_advt_pdf(adid);
        if (dt.Rows.Count > 0)
        {

            byte[] file = (byte[])dt.Rows[0]["adv_file"];

            //Response.AddHeader("Content-Length", file.Length.ToString());
            ////Response.AddHeader("content-disposition", "attachment;filename=" + dt.Rows[0]["Srno"].ToString() + ".pdf");
            //Response.ContentType = "application/pdf";
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.BinaryWrite(file);
            //Response.End();


            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=filename.pdf");     // to open file prompt Box open or Save file
            Response.BinaryWrite(file);
            //Server.Transfer("home.aspx");
            Response.End();

        }
    }
}
