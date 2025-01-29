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



public partial class UserControl_Menu_latestannounce : System.Web.UI.UserControl
{
    MD5Util md5util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {

         try
        {
            //Announcement
            string strSql = null;
            try
            {
                strSql = "<table width='100%'>";
                strSql = strSql + "</table>";
                string strScrolling = "";
                HtmlTableCell cellScrolling = new HtmlTableCell();
                fill_announcement();
                if (Session["serial_no"]!=null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
                {
                    grdsplpost.Visible = true;
                    fill_announcementsplpost();
                }
                else
                {
                    grdsplpost.Visible = false;
                }
                if (grdannouncement.Rows.Count == 0 && grdsplpost.Rows.Count == 0)
                {
                    lblmsg.Visible = true;
                }
                else
                {
                    lblmsg.Visible = false;
                }


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

        //fill_message()
    }


    private void form1_Load(object sender, System.EventArgs e)
    {
        //fill_message()
    }

    private void fill_announcement()
    {
        DataTable dt = new DataTable();
        CandidateData ObjCandD = new CandidateData();
        //dt = ObjCandD.getannouncement();
        dt = ObjCandD.GetJobAdvt("");
        //if (dt.Rows.Count == 0)
        //{
        //    lblmsg.Visible = true;
        //}
        //else
        //{
        //    lblmsg.Visible = false;
        //}
        grdannouncement.DataSource = dt;
        grdannouncement.DataBind();

    }
    private void fill_announcementsplpost()
    {
        DataTable dt = new DataTable();
        CandidateData ObjCandD = new CandidateData();
        //dt = ObjCandD.getannouncement();
        dt = ObjCandD.GetJobAdvt("spl");
        //if (dt.Rows.Count == 0)
        //{
        //    lblmsg.Visible = true;
        //}
        //else
        //{
        //    lblmsg.Visible = false;
        //}
        grdsplpost.DataSource = dt;
        grdsplpost.DataBind();

    }
    //GridView_message_RowDataBound

    protected void grdannouncement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //CandidateData objcd = new CandidateData();
            //string advtno = grdannouncement.DataKeys[e.Row.RowIndex]["ADVT_NO"].ToString();
            //HyperLink hannounce = ((HyperLink)e.Row.FindControl("hyplannounce"));
            //hannounce.NavigateUrl = md5util.CreateTamperProofURL("AdvtList.aspx", null, "AdvtNo=" + MD5Util.Encrypt(advtno, true));

        }
    }

}
