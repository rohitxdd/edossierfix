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
using System.Data.SqlClient;

public partial class Msg_File : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int msgid = Convert.ToInt32(Request.QueryString["msgid"]);
    }
}
