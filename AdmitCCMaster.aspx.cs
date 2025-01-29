using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdmitCCMaster : BasePage
{
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    Random randObjCode = new Random();

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
            //TRgetCodeMob.Visible = true;
            if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
    }
}