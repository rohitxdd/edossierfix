using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class ChangePwd : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            settitle();
        }
    }
    private void settitle()
    {
        Master ms = new Master();
        DataTable dt = new DataTable();

        string url = Request.CurrentExecutionFilePath;
        string folder = "ecourtis/";
        url = url.Substring(url.LastIndexOf(folder) + folder.Length);
        dt = ms.setlbltitle1(url);
        if (dt.Rows.Count > 0)
        {
            lbltitle.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["title"].ToString()));
        }
    }
}
