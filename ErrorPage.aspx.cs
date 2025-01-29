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

public partial class ErrorPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        Response.Cookies.Add(new HttpCookie("newId", ""));
        //Response.Redirect("Default.aspx");

    }
}
