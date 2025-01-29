using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
/// <summary>
/// Summary description for dict
/// </summary>
public class dict
{
    public string QString { get; set; }
    public SqlParameter[] param { get; set; }    
}
