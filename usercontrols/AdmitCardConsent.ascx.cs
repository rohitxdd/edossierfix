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
using System.Threading;
using System.IO;


public partial class usercontrols_AdmitCardConsent : System.Web.UI.UserControl
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    DataTable dt;
    Utility Utlity = new Utility(); 
    Marks objmarks = new Marks();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {        
           
            fillddlexam();
            //GetverifiedId();

        }
        
    }

    private void Filldetail()
    {
        DataTable dt = objcd.getdetail(Session["rid"].ToString());
        lblname.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["name"].ToString()));
        txt_mob.Text = dt.Rows[0]["mobileno"].ToString();
        txt_email.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["email"].ToString()));
        //get data on basis of registration id
    }


    protected void btncancel_Click(object sender, EventArgs e)
    {
        msg.Show("You have not Verified your Mobile No. and Emailid. Your Admit Card for Exam ::"+ ddlexam.SelectedItem.Text +" will be issued only after you will Verify your above Particulars");
        Server.Transfer("Home.aspx");
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string ipaddress = GetIPAddress();
        string edate = Utility.formatDatewithtime(DateTime.Now);
        string flagphase = "";
        int i;

        if (CheckBoxdisclaimer.Checked)
        {
            if (txt_mob.Text == "")
            {
                msg.Show("Please Enter Mobile No.");
            }
            else if (txt_email.Text == "")
            {
                msg.Show("Please Enter Email ID");
            }

            else if (Validation.chkLevel(txt_mob.Text))
            {

                msg.Show("Invalid Character in Mobile No");

            }
            else
            {

                int tmp = objcd.updatemobile(Session["rid"].ToString(), txt_mob.Text.Trim(), txt_email.Text.Trim());
                if (tmp > 0)
                {
                    Int64 VerificationID = Utlity.GetRequestID(Convert.ToInt64(ddlexam.SelectedValue));

                    //checking  for Tier 2 if radmitcard ='Y' in exammast take consent for Tier 2
                    if (hfradmitcard.Value != "Y")
                    {
                        flagphase = "1";
                        i = objcd.Update_AdmitCConsentTransaction(ddlexam.SelectedValue, Session["rid"].ToString(), VerificationID, edate, ipaddress, flagphase);
                    }
                    else
                    {
                        flagphase = "2";
                        i = objcd.Update_AdmitCConsentTransaction(ddlexam.SelectedValue, Session["rid"].ToString(), VerificationID, edate, ipaddress, flagphase);
                    }

                    if (i > 0)
                    {
                        //msg.Show("Your verification process is complete. Your Admit Card will be available for printing through your OARS account between " + userName + " and " + date + " "); 
                        msg.Show("Your Verification Process is Complete. Verification Id is " + VerificationID +". Your e Admit Card will be available tentatively after " + Session["releasedate"].ToString() +" for the Post Code: " +  Session["Posts"] + " ");
                        Server.Transfer("Home.aspx");

                    }
                    else
                    {
                       
                        trhead.Visible = false;
                        TRUC.Visible = false;
                        trhead.Visible = false;

                    }

                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");

                }
            }
        }
        else
        {
            msg.Show("Select the Checkbox");
        }
    }

    private void fillddlexam()
    {
        try
        {
            string regno = Session["rid"].ToString();
            dt = objmarks.GetexamidforAdmitCC(regno,"0");
            if (dt.Rows.Count > 0)
            {
                hfradmitcard.Value = dt.Rows[0]["radmitcard"].ToString();
                hfradmitcardphase2.Value = dt.Rows[0]["radmitcardphase2"].ToString();
                hfacconsent.Value = dt.Rows[0]["acconsent"].ToString();
                hfacconsent_phase2.Value = dt.Rows[0]["acconsent_phase2"].ToString();

                trddlexam.Visible = true;
                ddlexam.DataTextField = "examdtl";
                ddlexam.DataValueField = "examid";
                ddlexam.DataSource = dt;
                ddlexam.DataBind();
                ddlexam.Items.Insert(0, Utility.ddl_Select_Value());
            }
            else
            {
               // msg.Show("You have already verified");
                trddlexam.Visible = false;
                //GetverifiedId();
            }


        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }


    protected void ddlexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        string regno = Session["rid"].ToString();
        if (ddlexam.SelectedItem.Text != "--Select--")
        {
            // dt = objmarks.GetexamidforAdmitCC(regno,"0");
            dt = GetverifiedId(ddlexam.SelectedValue);
            GEtPostCode();
        }
        else
        {
            trhead.Visible = false;
            TRUC.Visible = false;
        }

    }


    public DataTable GetverifiedId(string examid)
    {
       string flagphase = "";
        string regno = Session["rid"].ToString();
        dt = objmarks.GetexamidforAdmitCC(regno, "0",ddlexam.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            hfradmitcard.Value = dt.Rows[0]["radmitcard"].ToString();
            hfradmitcardphase2.Value = dt.Rows[0]["radmitcardphase2"].ToString();
            hfacconsent.Value = dt.Rows[0]["acconsent"].ToString();
            hfacconsent_phase2.Value = dt.Rows[0]["acconsent_phase2"].ToString();

           
        }
      
        if (hfradmitcard.Value != "Y")
        {
            flagphase = "1";
        }
        else
        {
            flagphase = "2";
        }
            DataTable dtgetverified = objmarks.GetVerifiedID(ddlexam.SelectedValue, regno, flagphase);
            if (dtgetverified.Rows.Count > 0)
            {
                GEtPostCode();
                Session["releasedate"] = dtgetverified.Rows[0]["releasedate"].ToString();
                lblheldon.Text = ddlexam.SelectedItem.Text;
                lblreleasedate.Text = Session["releasedate"].ToString();
                if (dtgetverified.Rows[0]["verificationid"].ToString() != "" && dtgetverified.Rows[0]["verificationid"].ToString() != null)
                {
                    Int64 verificationid = Convert.ToInt64(dtgetverified.Rows[0]["verificationid"].ToString());
                    string releasedate = dtgetverified.Rows[0]["releasedate"].ToString();
                    if (flagphase == "1")
                    {
                        msg.Show("You have already Verified your particulars.Your Verification ID is " + verificationid + ". Your e Admit Card will be available tentatively after  " + releasedate + " for the Post Code: " + Session["Posts"].ToString() + " ");
                    }
                    else if (flagphase == "2")
                    {
                        if ((hfacconsent_phase2.Value == "" && hfacconsent.Value == "Y" && hfradmitcard.Value == "Y") || (hfacconsent.Value == "Y" && hfacconsent_phase2.Value == "Y" && hfradmitcardphase2.Value == "Y"))
                        {
                            msg.Show("Your Admit Card is Available for Download");
                        }
                        else if (hfacconsent.Value == "Y" && hfacconsent_phase2.Value == "Y" && hfradmitcardphase2.Value != "Y")
                        {
                            msg.Show("You have already Verified your particulars.Your Verification ID is " + verificationid + ". Your e Admit Card will be available tentatively after  " + releasedate + " for the Post Code: " + Session["Posts"].ToString() + " ");

                        }
                    }
                     trddlexam.Visible = false;
                    TRUC.Visible = false;
                    Server.Transfer("Home.aspx");
                }


                else
                {

                    string crrntdate = DateTime.Now.ToString("yyyy-MM-dd");
                    //code for time check
                    string crtime="";
                    string strDataExpression = "";
                    if (dtgetverified.Rows[0]["totimephase2"].ToString() != "")
                    {
                         crtime = DateTime.Now.ToString("HH:mm");
                         strDataExpression = "cafromdate <= '" + crrntdate.ToString() + "' and catodate >= '" + crrntdate.ToString() + "' and totimephase2 >= '" + crtime.ToString() + "'";
                    }
                    //accs.cafromdate < = convert(varchar(10),GETDATE(),120) and accs.catodate>=convert(varchar(10),GETDATE(),120) 
                    else
                    {
                        strDataExpression = "cafromdate <= '" + crrntdate.ToString() + "' and catodate >= '" + crrntdate.ToString() + "'"; 
                    }
                    DataRow[] drAdmiCConsent = dtgetverified.Select(strDataExpression);
                    if (drAdmiCConsent.Length == 0)
                    {
                        msg.Show("The Verification Schedule for generating the Admit Card is already over.");
                    }
                    else
                    {
                        //trddlexam.Visible = true;

                        trhead.Visible = true;
                        TRUC.Visible = true;
                        Filldetail();

                    }
                }
            }     
                        
        return dt;

    }


    public void GEtPostCode()
    {
        string regno = Session["rid"].ToString();
        DataTable dt = objmarks.GetPostCode(ddlexam.SelectedValue, regno, "1");
        string posts = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i != 0)
            {
                posts += ",";
            }
            posts += Utility.getstring(dt.Rows[i]["post"].ToString());
        }

        Session["Posts"] = posts;
        lblpostcode.Text = Session["Posts"].ToString();
        
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