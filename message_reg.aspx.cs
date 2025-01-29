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

public partial class message_reg : BasePage
{
    MD5Util md5util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StringUtil.GetQueryString(Request.Url.ToString()) != null)
        {
            if (md5util.IsURLTampered(StringUtil.GetWithoutDigest(StringUtil.GetQueryString(Request.Url.ToString())),
                 StringUtil.GetDigest(StringUtil.GetQueryString(Request.Url.ToString()))) == true)
            {
                Response.Redirect("home.aspx");
            }
            if (Request.QueryString["regno"] != null)
            {
                string msg = MD5Util.Decrypt(Request.QueryString["regno"].ToString(), true);
                lbl.Text = "Please Note Your Regn. No:";
                lblmsg.Text = msg;
                Label1.Text = "It is a combination of ";
                Label2.Text = "Date of Birth, Roll No and Passing Year of 10th class.";
                
            }
        }
    }
    protected void popupclose_Click(object sender, EventArgs e)
    {
        //ClientScript.RegisterStartupScript(JavaScript:closeWindow(););
        Response.Write("<script language='javascript'> { window.close() }</script>");
 
    }
}
