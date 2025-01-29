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

public partial class ApplyInsert : BasePage
{
    MD5Util md5util = new MD5Util();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;

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
            if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }

        string postcode = MD5Util.Decrypt(Request.QueryString["postcode"].ToString(), true);
        
        string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);

        InsertMsg.Text = "Your Online Application for the Post Code :: " + postcode + " has been submitted Partly. For completing the application, candidate is required to follow the following instructions.";
        //The application will only be completed after submitting Educational Qualification / Experience and Uploading Photo/Signature and after confirmation of your details.
        //hlphoto.NavigateUrl = md5util.CreateTamperProofURL("jobupload.aspx", null, "applid=" + MD5Util.Encrypt(applid , true));
        hlquli.NavigateUrl = md5util.CreateTamperProofURL("Experience.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));

    }
}
