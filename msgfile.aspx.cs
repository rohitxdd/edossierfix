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

public partial class msgfile : BasePage
{
    CandidateData objcd = new CandidateData();
    MD5Util md5util = new MD5Util();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
   
        }
        else
        {
            //if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            //{
            //    //valid Page
            //}
            //else
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}
        }

        if (StringUtil.GetQueryString(Request.Url.ToString()) != null)
        {  if (md5util.IsURLTampered(StringUtil.GetWithoutDigest(StringUtil.GetQueryString(Request.Url.ToString())),
                StringUtil.GetDigest(StringUtil.GetQueryString(Request.Url.ToString()))) == true)
            {
                //Response.Redirect("home.aspx?id=0");
            }
        }

        if (Request.QueryString["msgid"] != "")
        {
            string msgid = MD5Util.Decrypt(Request.QueryString["msgid"].ToString(), true);
            string type = MD5Util.Decrypt(Request.QueryString["type"].ToString(), true);
            DataTable dtmsg = objcd.getmsg(msgid, type);
            if (dtmsg.Rows.Count > 0)
            {
                if (dtmsg.Rows[0]["msg_file"].ToString() != "")
                {
                    Byte[] buffer = (byte[])dtmsg.Rows[0]["msg_file"];

                    if (buffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        if (type == "N")
                        {
                            Response.AddHeader("content-disposition", "attachment;filename=msgfile.pdf");     // to open file prompt Box open or Save file         
                        }
                        else
                        {
                            Response.AddHeader("content-disposition", "attachment;filename=Instruction.pdf");
                        }
                        Response.BinaryWrite(buffer);
                        //Server.Transfer("home.aspx");
                        Response.End();
                    }
                }
                else
                {
                    message msg = new message();
                    msg.Show("No File Exist");
                }

            }
            else
            {
                message msg = new message();
                msg.Show("No File Exist");
            }
        }
      
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
}
