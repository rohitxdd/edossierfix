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

public partial class viewcertificate : BasePage
{
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    MD5Util md5Util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (md5Util.IsURLTampered(StringUtil.GetWithoutDigest(StringUtil.GetQueryString(Request.Url.ToString())),
                                             StringUtil.GetDigest(StringUtil.GetQueryString(Request.Url.ToString()))) == true)
            {
                Response.Redirect("Default.aspx");
            }

            //UniqueRandomNumber = randObj.Next(1, 10000);
            //Session["token"] = UniqueRandomNumber.ToString();
            //this.csrftoken.Value = Session["token"].ToString();

            if (Request.QueryString["edid"] != null)
            {
                string edid = MD5Util.Decrypt(Request.QueryString["edid"].ToString(), true);
                try
                {
                    CandidateData objcd = new CandidateData();
                    DataTable dt = objcd.select_eDossierCertificate(edid);
                    if (dt.Rows.Count > 0)
                    {
                        byte[] file = (byte[])dt.Rows[0]["doc"];
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.AddHeader("content-disposition", "attachment;filename=" + dt.Rows[0]["edid"].ToString() + ".pdf");
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
}