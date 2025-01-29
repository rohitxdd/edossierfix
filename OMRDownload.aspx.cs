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
using System.IO;

public partial class OMRDownload : BasePage
{
    MD5Util md5util = new MD5Util();
    message msg = new message();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string fileName = MD5Util.Decrypt(Request.QueryString["name"].ToString(),true);
            //string fileName = Request.QueryString["name"].ToString();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.TransmitFile(Server.MapPath("~/DownloadOMR/" + fileName));
            Response.End();
        }
        catch (Exception ex)
        {
            msg.Show("Enter Valid Roll Number");
        }
    }

}
