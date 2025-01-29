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
using System.Threading;

public partial class PrintInstruction : BasePage
{
    eAdmitCard objeadmit = new eAdmitCard();
    DataTable dt = new DataTable();
    MD5Util md5util = new MD5Util();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
            if (Request.QueryString["examid"] != null)
            {
                string examid = MD5Util.Decrypt(Request.QueryString["examid"].ToString(), true);
                string gender = "";
                if (Request.QueryString["gender"] != null)
                {
                    gender = MD5Util.Decrypt(Request.QueryString["gender"].ToString(), true);
                }
                try
                {

                    dt = objeadmit.selectinstructiondoc(examid, gender);
                    if (dt.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(e.GetType(), "MyScript", "javascript:window.print();", true);
                        byte[] file = (byte[])dt.Rows[0]["indoc"];
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.AddHeader("content-disposition", "inline;filename=" + dt.Rows[0]["examid"].ToString() + ".pdf");
                        Response.ContentType = "application/" + ".pdf";
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(file);
                        Response.End();
                    }
                    else
                    {
                        //message obj = new message();
                        //obj.Show("There is no  file in DataBase");
                    }
                }
                catch (ThreadAbortException TAE)
                {
                }
                catch (Exception ex)
                {
                    Response.Redirect("ErrorPage.aspx");
                }
            }
       
    }
}