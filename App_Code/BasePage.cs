using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
       
        ViewStateUserKey = Session.SessionID;
    }

    public void Page_PreLoad(object sender, EventArgs e)
    {
        if (Request.Cookies["newId"] != null)
        {
            String cookieValue = Request.Cookies["newId"].Value;
            if (Session["newId"] != null)
            {
                if (cookieValue.Equals(Session["newId"].ToString()))
                {
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
            }
            else
            {
                Response.Redirect("Logout.aspx");
            }
        }
        else
        {
            Response.Redirect("Logout.aspx");
        }
    }

    public void Page_PreRender(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        //Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;


        //Used for disabling page caching 
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore(); //Used for disabling page caching 
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
    }
    protected override void OnError(EventArgs e)
    {
        if (Server.GetLastError().GetBaseException() is System.Web.HttpRequestValidationException)
        {
            Response.Clear();
            Server.ClearError();
            //Response.Write("Invalid characters.");
            Response.StatusCode = 200;
            //Response.End();
            Response.Redirect(ResolveClientUrl("ErrorPage.aspx"));

        }
    }
    public void Page_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException();
        Response.Clear();

        //Response.Write("Invalid characters.");
        Response.StatusCode = 200;
        //Response.End();
        Server.ClearError();
        Response.Redirect(ResolveClientUrl("ErrorPage.aspx"));

    }
    public string GetIPAddress()
    {
        string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            ipAddress = ipAddress.Split(',')[0];
        }
        return ipAddress;
    }
}