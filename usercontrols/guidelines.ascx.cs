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

public partial class usercontrols_guidelines : System.Web.UI.UserControl
{
    MD5Util md5util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Notice Board
            string strSql = null;
            try
            {
                strSql = "<table width='100%'>";
                strSql = strSql + "</table>";
                string strScrolling = "";
                HtmlTableCell cellScrolling = new HtmlTableCell();
                fill_message();
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
        dt = ObjCandD.GetMessage("I");
        GridView_message.DataSource = dt;
        GridView_message.DataBind();

    }
    protected void GridView_message_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CandidateData objcd = new CandidateData();
            string msgid = GridView_message.DataKeys[e.Row.RowIndex]["msgid"].ToString();
            string fileexist = GridView_message.DataKeys[e.Row.RowIndex]["fileexist"].ToString();
            HyperLink hmsg = ((HyperLink)e.Row.FindControl("hypl"));

            if (fileexist == "N")
            {
                hmsg.Enabled = false;
            }
            else
            {
                hmsg.NavigateUrl = md5util.CreateTamperProofURL("~/msgfile.aspx", null, "msgid=" + MD5Util.Encrypt(msgid, true) + "&type=" + MD5Util.Encrypt("I", true));
            }
            //else
            //    msg.Show("File Not Found");

        }
    }
}
