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

using System.Globalization;
using System.Text.RegularExpressions;

public partial class UserControl_Menu_noticeboard : System.Web.UI.UserControl
{
    MD5Util md5util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        //hyall.NavigateUrl = md5util.CreateTamperProofURL("AdvtList.aspx", null, "AdvtNo=" + MD5Util.Encrypt("", true));
        //if (md5util.IsURLTampered(StringUtil.GetWithoutDigest(StringUtil.GetQueryString(Request.Url.ToString())),
        //            StringUtil.GetDigest(StringUtil.GetQueryString(Request.Url.ToString()))) == true)
        //{
        //    Response.Redirect("Login.aspx?id=0");
        //}
        try
        {
            //Notice Board
          //  string strSql = null;
            try
            {
                //strSql = "<table width='100%'>";
                //strSql = strSql + "</table>";
                //string strScrolling = "";
                //HtmlTableCell cellScrolling = new HtmlTableCell();
               
 		fill_message(); // on 23062021 rkp

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

 
    private void fill_message()
    {
        DataTable dt = new DataTable();
        CandidateData ObjCandD = new CandidateData();
        dt = ObjCandD.GetMessage("N");
        if (dt.Rows.Count > 0)
        {
            GridView_message.DataSource = dt;
            GridView_message.DataBind();
        }

    }

    protected void GridView_message_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CandidateData objcd = new CandidateData();
            string msgid = GridView_message.DataKeys[e.Row.RowIndex]["msgid"].ToString();
            string fileexist = GridView_message.DataKeys[e.Row.RowIndex]["fileexist"].ToString();
            string m_edate = GridView_message.DataKeys[e.Row.RowIndex]["m_edate"].ToString();

            

           // DateTime messate_date = DateTime.ParseExact(m_edate, "dd/MM/yyyy", new CultureInfo("en-US"));
            //DateTime messate_date = DateTime.Parse(m_edate);

            //TimeSpan t = DateTime.Now - messate_date;
           // int length_of_display = (int)t.TotalDays;
            string dt2 = Utility.formatDateinDMY(DateTime.Now.AddDays(-7));

            int t = Utility.comparedatesDMY(m_edate, dt2);
            Image img = (Image)e.Row.FindControl("img_grid");
            Image img_arrow = (Image)e.Row.FindControl("img_arrow");

            //if (t < 0)
            //{
            //    img.Visible = false;
            //    img_arrow.Visible = true;
            //}
            //else
            //{
            //    img.Visible = true;
            //    img_arrow.Visible = false;
            //}
            img.Visible = false;
            img_arrow.Visible = true;



            HyperLink hmsg = ((HyperLink)e.Row.FindControl("hypl"));

            //if (msgid == "27" || msgid == "28")
            //{
            //    hmsg.ForeColor = System.Drawing.Color.Brown;
            //}
            if (fileexist == "Y")
            {
                hmsg.NavigateUrl = md5util.CreateTamperProofURL("~/msgfile.aspx", null, "msgid=" + MD5Util.Encrypt(msgid, true) + "&type=" + MD5Util.Encrypt("N", true));
            }

        }
    }
}
