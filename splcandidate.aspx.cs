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


public partial class splcandidate : BasePage
{
    //LoginMast ObjMast = new LoginMast();
    message msg = new message();
    // string flag = "";
    DataTable dt = new DataTable();
    Random randObj = new Random();
    Random randObjCode = new Random();
    Int32 UniqueRandomNumber = 0;
    Sms objsms = new Sms();
    Int32 UniqueRandomNumberCode = 0;
    string SecurityCode;
    string regno = "";
    CandidateData objcand = new CandidateData();
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "checkJavaScriptValidity();", true);
        txtserialno.Focus();
        //txt_re_password.Attributes.Add("onblur", "javascript:SignValidate()");

        Response.Redirect("Default.aspx", false);
        return;

        btnrsubmit.Visible = false;//Added by AnkitaSingh for 90/09 consent Dated:11-08-2022
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);

            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();


        }
        else
        {
            if (((Request.Form["csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
    }
    protected void btnrsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Validation.chkLevel13(txtdob.Text))
            {
                msg.Show("Invalid Character in Date of Birth");
            }
            else if (Validation.chkLevel(txtserialno.Text))
            {
                msg.Show("Invalid Character in ID No ");
            }

            else
            {
                string dob = (txtdob.Text).Replace("/", "");
                string dob1 = dob.Substring(0, 4);
                //************AnkitaSingh**********
                string dd = dob.Substring(0, 1);
                if (dd == "0")
                {
                    dob1 = dob.Substring(1, 3);
                }
                //*********************************
                string yr = dob.Substring(6, 2);
                string birthdt = dob1 + yr;

                DataTable tbl = objcand.VerifyRecord(txtserialno.Text.Trim());//Added for 90/09 dated: 02-06-2023
                if (tbl.Rows.Count > 0)
                {
                    if (tbl.Rows[0]["UploadedDoc"].ToString() != "" && tbl.Rows[0]["DocFile"].ToString() != "")
                    {
                        msg.Show(tbl.Rows[0]["UploadedDoc"].ToString() + " is already uploaded. Verification Successfully completed. Kindly login to proceed");
                        return;
                    }
                }

                DataTable dtcheck = objcand.checkserialnoforcandidate(txtserialno.Text, birthdt);
                if (dtcheck.Rows.Count == 0)
                {
                    tblshow.Visible = false;
                    tblalredymapped.Visible = false;
                    tbl1.Visible = true;
                    msg.Show("Your Perticulars are not matching.Please fill below performa for correction and contact to DSSSB Helpdesk.");
                    //hpr_correct.Visible = true;
                    return;
                }
                else
                {
                    if (dtcheck.Rows[0]["postcode"].ToString() == "")
                    {
                        msg.Show("Your Perticulars are not matching.Please fill below performa for correction and contact to DSSSB Helpdesk.");
                        //hpr_correct.Visible = true;
                        return;
                    }
                    else
                    {
                        //hpr_correct.Visible = false;
                        tblshow.Visible = true;
                        tbl1.Visible = false;
                        lblcanddob.Text = txtdob.Text;
                        objcand.oldpostEntry(txtserialno.Text, "90/09", "Y");//Added by AnkitaSingh for 90/09 Dated: 04-10-2022
                        lblcandfname.Text = dtcheck.Rows[0]["f_name"].ToString();
                        lblcat.Text = dtcheck.Rows[0]["cat"].ToString();
                        lblcandsrno.Text = dtcheck.Rows[0]["serial_no"].ToString();
                        lblname.Text = dtcheck.Rows[0]["name"].ToString();
                        lblpost.Text = dtcheck.Rows[0]["postcode"].ToString();
                        lblpostname.Text = dtcheck.Rows[0]["jobtitle"].ToString();
                        Session["serial"] = txtserialno.Text;//AnkitaSingh dated:03-11-2022
                        DataTable dtserial = objcand.checkserialnomapping(txtserialno.Text);
                        if (dtserial.Rows.Count > 0)
                        {
                            tblalredymapped.Visible = true;
                            lblregno.Text = dtserial.Rows[0]["regno"].ToString();
                            //=========== Manindra 27-04-2018
                            DataTable dtfname = objcand.getF_namefromRegistration(lblregno.Text);
                            if (lblcandfname.Text == "")
                            {
                                lblcandfname.Text = dtfname.Rows[0]["fname"].ToString();
                            }

                            //============================
                            trreg.Visible = false;
                        }
                        else
                        {
                            tblalredymapped.Visible = false;
                            lblregno.Text = "";
                            trreg.Visible = true;
                        }
                    }
                }


            }
        }
        //catch (ThreadAbortException the)
        //{

        //}
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }

    protected void lnknewreg_Click(object sender, EventArgs e)
    {
        MD5Util md5util = new MD5Util();
        string url = md5util.CreateTamperProofURL("registration.aspx", null, "flag=" + MD5Util.Encrypt("1", true) + "&name=" + MD5Util.Encrypt(lblname.Text, true) + "&fname=" + MD5Util.Encrypt(lblcandfname.Text, true) + "&dob=" + MD5Util.Encrypt(lblcanddob.Text, true) + "&serial=" + MD5Util.Encrypt(lblcandsrno.Text, true) + "&postcode=" + MD5Util.Encrypt(lblpost.Text, true));

        Response.Redirect(url);
    }

    protected void btnproceed_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }


    protected void ChkConsent_CheckedChanged(object sender, EventArgs e)//Added by AnkitaSingh 29-08-2022
    {
        if (ChkConsent.Checked == true)
        {
            btnrsubmit.Visible = true;
        }
        else
        {
            btnrsubmit.Visible = false;
        }
    }
}
