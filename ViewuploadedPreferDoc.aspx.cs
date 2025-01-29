using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;


public partial class ViewuploadedPreferDoc : BasePage
{
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    MD5Util md5Util = new MD5Util();
    CandidateData objcd = new CandidateData();
    CandCombdData obj = new CandCombdData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (md5Util.IsURLTampered(StringUtil.GetWithoutDigest(StringUtil.GetQueryString(Request.Url.ToString())),
                                             StringUtil.GetDigest(StringUtil.GetQueryString(Request.Url.ToString()))) == true)
            {
                Response.Redirect("Default.aspx");
            }
            if (Request.QueryString["applid"] != null && Request.QueryString["edmid"]!=null)
            {
                string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                string edmid= MD5Util.Decrypt(Request.QueryString["edmid"].ToString(), true);
                try
                {                  
                    DataTable dt = obj.select_preferCertificate(applid,edmid);
                    if (dt.Rows.Count > 0)
                    {
                        byte[] file = (byte[])dt.Rows[0]["doc"];
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.AddHeader("content-disposition", "attachment;filename=" + dt.Rows[0]["applid"].ToString() + dt.Rows[0]["edmid"].ToString() + ".pdf");
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