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
/// Summary description for baseUI
/// </summary>
public class baseUI : BasePage
{
    DataAccess da = new DataAccess();
    public  message msg = new message();
    
	public baseUI()
	{
        
	}
    public void SessionStoreage(userlogin log)
    {
        Session["userid"] = log.User;
        Session["username"] = log.UserName;
        Session["usertype"] = log.UserType;
        Session["deptcode"] = log.DeptCode;
    }
   
}
