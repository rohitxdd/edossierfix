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
using System.Data.SqlClient;

using System.Collections.Specialized;

public partial class CFeeResponse : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    CandidateData objcan = new CandidateData();
    message msg = new message();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    MD5Util md5 = new MD5Util();
    challengeansheet objchal = new challengeansheet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            //string regno = Session["rid"].ToString();

            NameValueCollection reqparam = Request.Form;
            string encResponse = "";
            if (!string.IsNullOrEmpty(reqparam["encResponse"]))
            {
                encResponse = reqparam["encResponse"];
            }
            else if (!string.IsNullOrEmpty(reqparam["encryptQuery"]))
            {
                encResponse = reqparam["encryptQuery"];
            }

            string key = ConfigurationManager.AppSettings["CKeytoPay"].ToString();
            string decryptedParam = PayOnline.DecryptforPay(encResponse, true, key);
            string[] data_string = decryptedParam.Split('|');

            string cpid = data_string[0].ToString();
            string orderno = data_string[1].ToString();
            string status = data_string[2].ToString();
            string amount = data_string[3].ToString();
            string paymode = data_string[4].ToString();
            string bankcode = data_string[5].ToString();
            string transactiondate = data_string[6].ToString();
            string paytype = data_string[7].ToString();
            int temp = 0;
            if (paytype == "N")
            {
                LabelNote.Text = "You have not made the payment yet.";
            }
            else
            {
                temp = objchal.updatePaydtls(cpid, orderno, status, paymode, bankcode, transactiondate);
                if (status == "SUCCESS")
                {                    
                    LabelNote.Text = "Your Payment has been done successfully.";                    
                }
                else
                {
                    LabelNote.Text = "Your Transaction has been failed, Please Try Again.";
                }
            }
        }
        else
        {
            //if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            //{
            //    //valid Page
            //}
            //else
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}
        }
    }
}