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

public partial class EditApplication : BasePage
{
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["applid"] != null)
        {
                String Temp_applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
               
                if (!IsPostBack)
                {
                    UniqueRandomNumber = randObj.Next(1, 10000);
                    Session["token"] = UniqueRandomNumber.ToString();
                    this.csrftoken.Value = Session["token"].ToString();

                    txtapplid.Text = Temp_applid;

                }
        }
    }
    protected void btnconform_Click(object sender, EventArgs e)
    {

    }
    protected void edit_print_Load(object sender, EventArgs e)
    {

    }
}
