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

public partial class FeeResponse : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    CandidateData objcan = new CandidateData();
    message msg = new message();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    MD5Util md5 = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
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
                string key = ConfigurationManager.AppSettings["KeytoPay"].ToString();
                string decryptedParam = PayOnline.DecryptforPay(encResponse, true, key);

		        InsertFeeResponse(decryptedParam); //rohitxd => 21/11/2023

                string[] data_string = decryptedParam.Split('|');
		        //System.Diagnostics.Debug.WriteLine(data_string);
                string applid = data_string[0].ToString();
                string orderno = data_string[1].ToString();
                string status = data_string[2].ToString();
                string amount = data_string[3].ToString();
                string paymode = data_string[4].ToString();
                string bankcode = data_string[5].ToString();
                string transactiondate = data_string[6].ToString();
                string paytype = data_string[7].ToString();
                DataTable dtfeecheck = objcan.checkfeedetails(applid);
                // int temp=0;
                long temp = 0;
                if (paytype == "N")
                {
                    LabelNote.Text = "You have not made the payment yet, please click on Pay Online";
                }
                else
                {
                    if (status == "SUCCESS")
                    {
                        string jid = objcan.getjid(applid);
                        if (dtfeecheck.Rows.Count == 0)
                        {
                            // temp = objcan.insert_online_feedata(applid, orderno, amount, paymode, transactiondate, "Y");
                            temp = objcan.InsertFeedetailwithdummy_noTransaction(applid, orderno, amount, paymode, transactiondate, "Y", jid,"");
                        }
                        else
                        {
                            //temp = objcan.update_online_feedata(applid, orderno, amount, paymode, transactiondate, "Y");
			    if (dtfeecheck.Rows[0]["feerecd"].ToString() != "Y")
                            {
                                temp = objcan.updateFeedetailwithdummy_noTransaction(applid, orderno, amount, paymode, transactiondate, "Y", jid);
                            }
                        }

                        if (temp > 0)
                        {

                            // long i = objcan.insert_dummyno(applid, jid);
                            //   Session["dum"] = i.ToString();
                            Session["dum"] = temp.ToString();
                            if (temp > 0)
                            {
                                LabelNote.Text = "Your Payment has been done successfully.Your Application no. is " + temp.ToString();
                            }

                        }
                    }
		    else if (status == "PENDING")
                    {
                        LabelNote.Text = "Your transaction is pending at bank's end.";
                    }
                    else if (status == "ABORT")
                    {
                        LabelNote.Text = "Your transaction is aborted, please re-try";
                    }
                    else if (status == "NO RECORDS FOUND")
                    {
                        LabelNote.Text = "No transaction record found for your application ID.";
                    }
                    else if (status == "EXPIRED")
                    {
                        LabelNote.Text = "Your transaction has expired, please re-try.";
                    }
                    else if (status == "IN PROGRESS")
                    {
                        LabelNote.Text = "Your transaction is in progress.";
                    }
                    else
                    {
                        if (dtfeecheck.Rows.Count == 0)
                        {
                            temp = objcan.insert_online_feedata(applid, orderno, amount, paymode, "", "");
                            LabelNote.Text = "Your Transaction has been failed, Please Try Again.";
                        }
                        else
                        {
                            if ((object)dtfeecheck.Rows[0]["feerecd"] != DBNull.Value && dtfeecheck.Rows[0]["feerecd"].ToString() == "Y")
                            {

                            }
                            else
                            {
                                temp = objcan.update_online_feedata(applid, orderno, amount, paymode, "", "");
                                LabelNote.Text = "Your Transaction has been failed, Please Try Again.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
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
	
	
     private void InsertFeeResponse(string decryptedParam)
    {
        try
        {
            objcan.InsertFeeResponseLog(decryptedParam);
        }
        catch
        {
            //
        }
    }
}