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

public partial class Logout : BasePage
{
    ClsAudit objClsAudit = new ClsAudit();
    protected void Page_Load(object sender, EventArgs e)
    {        
        
        if (!IsPostBack)
        {
            //if (Request.QueryString["logoutvariable"] != null && Session["logoutvariable"] != null && Request.QueryString["logoutvariable"].ToString() == Session["logoutvariable"].ToString())
            //{
                string nextpage = ResolveClientUrl("Default.aspx");
                string WriteFunction = @"";
                WriteFunction = WriteFunction + " var Backlen=history.length;";
                WriteFunction = WriteFunction + " history.go(-Backlen);";

                WriteFunction = WriteFunction + " window.location.href='" + nextpage + "';";


                string myScript = WriteFunction;// +" ClearHistoryData();";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", myScript, true);


                string logouttime = "";
                logouttime = Utility.formatDatewithtime(DateTime.Now);
                if (Session["sessionId"] != null)
                {
                    int i = objClsAudit.updateAudit(logouttime, Session["sessionId"].ToString());
                }
                Session.Clear();
                Session.Abandon();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                Response.Cookies.Add(new HttpCookie("newId", ""));
                Response.Redirect("Default.aspx");
                //Server.Transfer();
           // }

        }
        
    }
}
