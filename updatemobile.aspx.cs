using System;
using System.Data;
using System.Configuration;
//using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class updatemobile : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    Random randObjCode = new Random();
    Int32 UniqueRandomNumberCode = 0;
    string SecurityCode;
    LoginMast ObjMast = new LoginMast();
    Sms objsms = new Sms();
    string ipaddress = string.Empty;
    int c;//counter for update count

    protected void Page_Load(object sender, EventArgs e)
    {
         ipaddress = GetIPAddress();
        //c = objcd.getcountervalue(txt_reg.Text);//only two details can be updated by the registerred candidate

        //if (c == 2)
        //{
           
        //    btnUpdateNewDetails.Visible = false;
        //    CheckBox1.Enabled = false;
        //    CheckBox2.Enabled = false;
        //    CheckBox3.Enabled = false;
        //    CheckBox4.Enabled = false;
        //    Ddl_gender.Enabled = false;
        //    CheckBox5.Enabled = false;
        //    TxtName.Enabled = false;
        //    TxtFname.Enabled = false;
        //    TxtMname.Enabled = false;
        //    TxtIdentity.Enabled = false;
        //    ChkAgreed.Enabled = false;
        //}

        //txt_mob.Attributes.Add("onblur", "ValidatorValidate(" + valsum.ClientID + ")");

        //TRgetCodeMob.Visible = false;
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            Filldetail();
            getStatusIfCandidateIn116();
           
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
    private void getStatusIfCandidateIn116()
    {
        try
        {
            DataTable dt = objcd.getStatusIfCandidateIn116(Session["rid"].ToString());
            if (dt.Rows.Count > 0)
            {
                //trUploadDocPCSP.Visible = true;
                DataTable dt1 = objcd.getLinkEnableDisableStatus("116postCode");
                if (dt1.Rows.Count > 0)
                {
                    trUploadDocPCSP.Visible = true;
                }
                else
                {
                    trUploadDocPCSP.Visible = false;
                }
            }
            else
            {
                trUploadDocPCSP.Visible = false;
            }
        }
        catch (Exception ex)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), ex.ToString(), "default.aspx", "loginbutton", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    private void Filldetail()
    {
        try
        {
            string proofOfIDDoc = string.Empty;
            DataTable dt = objcd.getdetail(Session["rid"].ToString());
            if (dt.Rows.Count > 0)
            {
                txt_name1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["name"].ToString()));
                //txtuid.Text = dt.Rows[0]["um_logid"].ToString();
                string Adhar = dt.Rows[0]["aadharNo"].ToString(); //21122020 bind adharno #start
                //string proofIDDoc = dt.Rows[0]["proofOfIDNo"].ToString(); //04012021
                if (Adhar != "")      //22122020//04012021
                {
                    proofOfIDDoc = Adhar;
                }
                //else if (proofOfIDDoc != "")
                //{
                //    proofOfIDDoc = proofIDDoc;
                //    Label2.Text = dt.Rows[0]["docName"].ToString() + " number";
                //}
                else
                {
                    //
                }
                if (proofOfIDDoc != "")
                {
                    string pfIDDocNo = MD5Util.Decrypt(proofOfIDDoc, true);
                    var lastDigits = pfIDDocNo.Substring(pfIDDocNo.Length - 4, 4);
                    var requiredMask = new String('X', pfIDDocNo.Length - lastDigits.Length);
                    var maskedString = string.Concat(requiredMask, lastDigits);
                    var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                    txtuid.Text = maskedCardNumberWithSpaces;
                }//21112020 #end
                txt_fh_name1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["fname"].ToString()));
                txt_mothername.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["mothername"].ToString()));
                ddlgender.SelectedValue = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["sex"].ToString()));
                txt_reg.Text = dt.Rows[0]["rid"].ToString();
                lblnation.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["nationality"].ToString()));
                txt_mob.Text = dt.Rows[0]["mobileno"].ToString();
                Session["mobNo"] = dt.Rows[0]["mobileno"].ToString();
                Hidden_txtmob.Value = txt_mob.Text;
                txt_email.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["email"].ToString()));
                Session["email"] = txt_email.Text;
                txtSpouse.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["spousename"].ToString()));
                DataTable dtcheck = objcd.getcandidatepostforupdate(Session["rid"].ToString());
                if (dtcheck.Rows.Count > 0)
                {
                    btnname.Visible = false;
                    btnfname.Visible = false;
                    btngender.Visible = false;
                    btnmname.Visible = false;
                    btnsname.Visible = false;
                }
                else
                {
                    btnname.Visible = true;
                    btnfname.Visible = true;
                    btngender.Visible = true;
                    btnmname.Visible = true;
                    btnsname.Visible = true;
                }
            }
            else
            {
                //
            }
        }
        catch (Exception ex)
        {
           int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), ex.ToString(), "updatemobile.aspx", "Filldetail", ipaddress, Request.Headers["User-Agent"].ToString());
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btnname_Click(object sender, EventArgs e)
    {
        txt_name1.Enabled = true;
        btnupdatename.Visible = true;
        btnname.Visible = false;
    }
    protected void btnfname_Click(object sender, EventArgs e)
    {
        txt_fh_name1.Enabled = true;
        btnupdatefname.Visible = true;
        btnfname.Visible = false;
    }
    protected void btngender_Click(object sender, EventArgs e)
    {
        ddlgender.Enabled = true;
        btnupdategender.Visible = true;
        btngender.Visible = false;
    }
    protected void btnmobile_Click(object sender, EventArgs e)
    {
        txt_mob.Enabled = true;
        btnupdatemobile.Visible = true;
        btnmobile.Visible = false;
    }
    protected void btnemail_Click(object sender, EventArgs e)
    {
        txt_email.Enabled = true;
        btnupdateemail.Visible = true;
        btnemail.Visible = false;
    }
    protected void btnupdatename_Click(object sender, EventArgs e)
    {
        try
        {
            Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");
            if (txt_name1.Text == "")
            {
                msg.Show("Please Enter Name");
            }
            else if (Validation.chkescape(txt_name1.Text) || txt_name1.Text.Length > 50 || !reg_num_only.IsMatch(txt_name1.Text))
            {
                msg.Show("Invalid Character in Name or Name length is more than 50 Characters.");

            }
            else
            {
                int tmp = objcd.update_name(Session["rid"].ToString(), txt_name1.Text);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    Server.Transfer("Home.aspx");
                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");
                }
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnupdatename", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    protected void btnupdatefname_Click(object sender, EventArgs e)
    {
        try
        {
            Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");
            if (txt_fh_name1.Text == "")
            {
                msg.Show("Please Enter Father Name");
            }
            else if (Validation.chkescape(txt_fh_name1.Text) || txt_fh_name1.Text.Length > 50 || !reg_num_only.IsMatch(txt_fh_name1.Text))
            {
                msg.Show("Invalid Character in Father Name or Father Name length is more than 50 Characters.");

            }
            else
            {
                int tmp = objcd.update_fname(Session["rid"].ToString(), txt_fh_name1.Text);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    Server.Transfer("Home.aspx");
                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");
                }
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnupdatefname", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    protected void btnupdategender_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlgender.SelectedValue == "")
            {
                msg.Show("Please Select Gender");
            }

            else
            {
                int tmp = objcd.update_gender(Session["rid"].ToString(), ddlgender.SelectedValue);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    Server.Transfer("Home.aspx");
                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");
                }
            }
        }
        catch (Exception exp)
        {

            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnupdategender", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    protected void btnupdatemobile_Click(object sender, EventArgs e)
    {

        try
        {


            if (txt_mob.Text == "")
            {
                msg.Show("Please Enter Mobile No");
            }
            else if (Validation.chkLevel(txt_mob.Text))
            {
                msg.Show("Invalid Character in Mobile No");
            }
            else
            {
                DataTable dt = ObjMast.IsExist_Applicant("", txt_mob.Text, "", "", "");
                if (dt.Rows.Count > 0)
                {
                    if (txt_mob.Text != "")
                    {
                        txt_mob.Text = "";
                        msg.Show("Mobile number entered is already registered in OARS.");
                    }
                }
                else
                {
                    int tmp = objcd.update_mobileno(Session["rid"].ToString(), txt_mob.Text);
                    if (tmp > 0)
                    {
                        ///////candidate activity log////////
                        string regno = Session["regno"].ToString();
                        string ip = GetIPAddress();

                        msg.Show("Updated successfully");
                        Server.Transfer("Home.aspx");
                    }
                    else
                    {
                        //txtcode.Text = "";
                        msg.Show("Some Error Occured");
                    }
                }

            }
        }
        catch (Exception ex)
        {

            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), ex.ToString(), "updatemobile.aspx", "btnupdatemobile", ipaddress, Request.Headers["User-Agent"].ToString());
        }


    }
    protected void btnupdateemail_Click(object sender, EventArgs e)
    {
        try
        {

            if (txt_email.Text == "")
            {
                msg.Show("Please Enter email");
            }
            else
            {
                DataTable dt = ObjMast.IsExist_Applicant("", "", Utility.putstring(txt_email.Text.Trim()), "", "");
                if (dt.Rows.Count > 0)
                {
                    txt_email.Text = "";
                    msg.Show("Email address entered is already registered in OARS.");
                }
                else
                {
                    int tmp = objcd.update_email(Session["rid"].ToString(), Utility.putstring(txt_email.Text));
                    if (tmp > 0)
                    {
                        ///////candidate activity log////////
                        string regno = Session["regno"].ToString();
                        string ip = GetIPAddress();

                        msg.Show("Updated successfully");
                        Server.Transfer("Home.aspx");
                    }
                    else
                    {
                        msg.Show("Some Error Occured");
                    }
                }
            }
        }
        catch (Exception ex)
        {

            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), ex.ToString(), "updatemobile.aspx", "btnupdateemail", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    protected void btnmname_Click(object sender, EventArgs e)
    {
        txt_mothername.Enabled = true;
        btnupdatemname.Visible = true;
        btnmname.Visible = false;
    }
    protected void btnupdatemname_Click(object sender, EventArgs e)
    {
        try
        {
            Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");
            if (txt_mothername.Text == "")
            {
                msg.Show("Please Enter Mother Name");
            }
            else if (Validation.chkescape(txt_mothername.Text) || txt_mothername.Text.Length > 50 || !reg_num_only.IsMatch(txt_mothername.Text))
            {
                msg.Show("Invalid Character in Mother Name or Mother Name length is more than 50 Characters.");

            }
            else
            {
                int tmp = objcd.update_mname(Session["rid"].ToString(), txt_mothername.Text);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    Server.Transfer("Home.aspx");
                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");
                }
            }
        }
        catch (Exception ex)
        {

            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), ex.ToString(), "updatemobile.aspx", "btnupdatemname", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    protected void btnsname_Click(object sender, EventArgs e)
    {
        txtSpouse.Enabled = true;
        btnupdatesname.Visible = true;
        btnsname.Visible = false;
    }
    protected void btnupdatesname_Click(object sender, EventArgs e)
    {
        try
        {
            Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");
            if (txtSpouse.Text == "")
            {
                msg.Show("Please Enter Spouse Name");
            }
            else if (Validation.chkescape(txtSpouse.Text) || txtSpouse.Text.Length > 50 || !reg_num_only.IsMatch(txtSpouse.Text))
            {
                msg.Show("Invalid Character in Spouse Name or Spouse Name length is more than 50 Characters.");

            }
            else
            {
                int tmp = objcd.update_sname(Session["rid"].ToString(), txtSpouse.Text);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    Server.Transfer("Home.aspx");
                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");
                }
            }
        }
        catch (Exception ex)
        {
           int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), ex.ToString(), "updatemobile.aspx", "btnupdatesname", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    protected void txtblnk_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = objcd.getLinkEnableDisableStatus("116postCode");
            if (dt.Rows.Count > 0)
            {
                Response.Redirect("insertAdharByCandidate.aspx?linkClicked=" + MD5Util.Encrypt("RD", true));
            }
            {
                msg.Show("Last date to upload ID proof and postcard size photograph is over.");
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "txtblnk", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("insertAdharByCandidate.aspx?linkClicked=" + MD5Util.Encrypt("RD", true));
    //    //Response.Redirect("insertAdharByCandidate.aspx?linkClicked=" + MD5Util.Encrypt("PC", true));
    //}
    protected void txt_mob_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string mobNo = txt_mob.Text.Trim();
            DataTable dt = ObjMast.IsExist_Applicant("", mobNo, "", "", "");
            if (dt.Rows.Count > 0)
            {
                if (mobNo != "")
                {
                    txt_mob.Text = "";
                    msg.Show("Mobile number entered is already registered in OARS.");
                }
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "txt_mob", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    protected void txt_email_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string email = txt_email.Text.Trim();
            DataTable dt = ObjMast.IsExist_Applicant("", "", Utility.putstring(email), "", "");
            if (dt.Rows.Count > 0)
            {
                if (email != "")
                {
                    txt_email.Text = "";
                    msg.Show("Email address entered is already registered in OARS.");
                }
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "txt_email_TextChan", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    /*Inserting existing data of registered candidate in Reg_Update_PD before Modification  */
    protected void btnUpdateNewDetails_Click(object sender, EventArgs e)
    {
        newUpdate.Visible = true;
        BtnFinalSubmit.Enabled = false;
       

        string rno = txt_reg.Text;
        string fn = txt_fh_name1.Text;
        string n = txt_name1.Text;
        string g = ddlgender.SelectedIndex.ToString();
        string id = txtuid.Text;
        string mn = txt_mothername.Text;


        //int i = objcd.insert_PD(rno, n, fn, mn, g, id);
        //if (i > 0)
        //{
        //    msg.Show("Old Data saved in Log table.");
        //}

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            btnApplicantName.Visible = true;
            TxtName.Enabled = true;
            if (CheckBox2.Checked == true)
            {
                CheckBox3.Enabled = false;
                CheckBox4.Enabled = false;
                CheckBox5.Enabled = false;
                btnFatherName.Visible = true;
                TxtFname.Enabled = true;
            }
            if (CheckBox3.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox4.Enabled = false;
                CheckBox5.Enabled = false;
                btnMotherName.Visible = true;
                TxtMname.Enabled = true;
            }
            if (CheckBox4.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox5.Enabled = false;
                btnUpdatenewGender.Visible = true;
                Ddl_gender.Enabled = true;
            }
            if (CheckBox5.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox4.Enabled = false;
                btnIdentity.Visible = true;
                TxtIdentity.Enabled = true;
            }
        }
        else
        {
            CheckBox2.Enabled = true;
            CheckBox3.Enabled = true;
            CheckBox4.Enabled = true;
            CheckBox5.Enabled = true;
            btnApplicantName.Visible = false;
            TxtName.Enabled = false;
        }
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            btnFatherName.Visible = true;
            TxtFname.Enabled = true;
            if (CheckBox1.Checked == true)
            {
                CheckBox3.Enabled = false;
                CheckBox4.Enabled = false;
                CheckBox5.Enabled = false;
                btnApplicantName.Visible = true;
                TxtName.Enabled = true;
            }
            if (CheckBox3.Checked == true)
            {
                CheckBox1.Enabled = false;
                CheckBox4.Enabled = false;
                CheckBox5.Enabled = false;
                btnMotherName.Visible = true;
                TxtMname.Enabled = true;
            }
            if (CheckBox4.Checked == true)
            {
                CheckBox1.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox5.Enabled = false;
                btnUpdatenewGender.Visible = true;
                Ddl_gender.Enabled = true;
            }
            if (CheckBox5.Checked == true)
            {
                CheckBox1.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox4.Enabled = false;
                btnIdentity.Visible = true;
                TxtIdentity.Enabled = true;
            }
        }
        else
        {
            CheckBox1.Enabled = true;
            CheckBox3.Enabled = true;
            CheckBox4.Enabled = true;
            CheckBox5.Enabled = true;
            btnFatherName.Visible = false;
            TxtFname.Enabled = false;

        }
    }

    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            btnMotherName.Visible = true;
            TxtMname.Enabled = true;
            if (CheckBox2.Checked == true)
            {
                CheckBox1.Enabled = false;
                CheckBox4.Enabled = false;
                CheckBox5.Enabled = false;
                btnFatherName.Visible = true;
                TxtFname.Enabled = true;
            }
            if (CheckBox1.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox4.Enabled = false;
                CheckBox5.Enabled = false;
                btnApplicantName.Visible = true;
                TxtName.Enabled = true;
            }
            if (CheckBox4.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox1.Enabled = false;
                CheckBox5.Enabled = false;
                btnUpdatenewGender.Visible = true;
                Ddl_gender.Enabled = true;
            }
            if (CheckBox5.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox1.Enabled = false;
                CheckBox4.Enabled = false;
                btnIdentity.Visible = true;
                TxtIdentity.Enabled = true;
            }
        }
        else
        {
            CheckBox1.Enabled = true;
            CheckBox2.Enabled = true;
            CheckBox4.Enabled = true;
            CheckBox5.Enabled = true;
            btnMotherName.Visible = false;
            TxtMname.Enabled = false;

        }
    }

    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox4.Checked == true)
        {
            btnUpdatenewGender.Visible = true;
            Ddl_gender.Enabled = true;
            if (CheckBox2.Checked == true)
            {
                CheckBox3.Enabled = false;
                CheckBox1.Enabled = false;
                CheckBox5.Enabled = false;
                btnFatherName.Visible = true;
                TxtFname.Enabled = true;
            }
            if (CheckBox3.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox1.Enabled = false;
                CheckBox5.Enabled = false;
                btnMotherName.Visible = true;
                TxtMname.Enabled = true;
            }
            if (CheckBox1.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox5.Enabled = false;
                btnApplicantName.Visible = true;
                TxtName.Enabled = true;
            }
            if (CheckBox5.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox1.Enabled = false;
                btnIdentity.Visible = true;
                TxtIdentity.Enabled = true;
            }
        }
        else
        {
            CheckBox1.Enabled = true;
            CheckBox3.Enabled = true;
            CheckBox2.Enabled = true;
            CheckBox5.Enabled = true;
            btnUpdatenewGender.Visible = false;
            Ddl_gender.Enabled = false;

        }
    }

    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox5.Checked == true)
        {
            btnIdentity.Visible = true;
            TxtIdentity.Enabled = true;
            if (CheckBox2.Checked == true)
            {
                CheckBox3.Enabled = false;
                CheckBox4.Enabled = false;
                CheckBox1.Enabled = false;
                btnFatherName.Visible = true;
                TxtFname.Enabled = true;
            }

            if (CheckBox3.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox4.Enabled = false;
                CheckBox1.Enabled = false;
                btnMotherName.Visible = true;
                TxtMname.Enabled = true;
            }
            if (CheckBox4.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox1.Enabled = false;
                btnUpdatenewGender.Visible = true;
                Ddl_gender.Enabled = true;
            }
            if (CheckBox1.Checked == true)
            {
                CheckBox2.Enabled = false;
                CheckBox3.Enabled = false;
                CheckBox4.Enabled = false;
                btnApplicantName.Visible = true;
                TxtName.Enabled = true;
            }
        }
        else
        {
            CheckBox1.Enabled = true;
            CheckBox3.Enabled = true;
            CheckBox4.Enabled = true;
            CheckBox2.Enabled = true;
            btnIdentity.Visible = false;
            TxtIdentity.Enabled = false;

        }
    }
    /*Update Registered Candidate's Name*/
    protected void btnApplicantName_Click(object sender, EventArgs e)
    {
        try
        {
            if (c < 2)
            {
                c++;
            }

            Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");
            if (TxtName.Text == "")
            {
                msg.Show("Please Enter Name");
            }
            else if (Validation.chkescape(TxtName.Text) || TxtName.Text.Length > 50 || !reg_num_only.IsMatch(TxtName.Text))
            {
                msg.Show("Invalid Character in Name or Name length is more than 50 Characters.");

            }
            else
            {
                int tmp = objcd.update_Newname(Session["rid"].ToString(), TxtName.Text, c, DateTime.Now);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    btnApplicantName.Enabled = false;
                }
                else
                {

                    msg.Show("Some Error Occured");
                }
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnApplicantName", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    /*Update registered candidate's Father's name*/
    protected void btnFatherName_Click(object sender, EventArgs e)
    {
        try
        {
            if (c < 2)
            {
                c++;
            }
            Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");
            if (TxtFname.Text == "")
            {
                msg.Show("Please Enter Father Name");
            }
            else if (Validation.chkescape(TxtFname.Text) || TxtFname.Text.Length > 50 || !reg_num_only.IsMatch(TxtFname.Text))
            {
                msg.Show("Invalid Character in Father Name or Father Name length is more than 50 Characters.");

            }
            else
            {
                int tmp = objcd.update_NewFname(Session["rid"].ToString(), TxtFname.Text, c, DateTime.Now);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    btnFatherName.Enabled = false;
                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");
                }
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnFatherName", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    /*update registered candidate's Mother's name*/
    protected void btnMotherName_Click(object sender, EventArgs e)
    {
        try
        {
            if (c < 2)
            {
                c++;
            }
            Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");
            if (TxtMname.Text == "")
            {
                msg.Show("Please Enter Mother Name");
            }
            else if (Validation.chkescape(TxtMname.Text) || TxtMname.Text.Length > 50 || !reg_num_only.IsMatch(TxtMname.Text))
            {
                msg.Show("Invalid Character in Mother Name or Mother Name length is more than 50 Characters.");

            }
            else
            {
                int tmp = objcd.update_NewMname(Session["rid"].ToString(), TxtMname.Text, c, DateTime.Now);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    btnMotherName.Enabled = false;
                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");
                }
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnMotherName", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    /*Update registered candidate's gender*/
    protected void btnUpdatenewGender_Click(object sender, EventArgs e)
    {
        try
        {
            if (c < 2)
            {
                c++;
            }
            if (Ddl_gender.SelectedValue == "")
            {
                msg.Show("Please Select Gender");
            }

            else
            {
                int tmp = objcd.update_Newgender(Session["rid"].ToString(), Ddl_gender.SelectedValue, c, DateTime.Now);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    Ddl_gender.Enabled = false;
                }
                else
                {
                    //txtcode.Text = "";
                    msg.Show("Some Error Occured");
                }
            }
        }
        catch(Exception exp)
        {
           int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnUpdatenewGender", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    /*update registered candidate's id card*/
    protected void btnIdentity_Click(object sender, EventArgs e)
    {
        try
        {
            if (c < 2)
            {
                c++;
            }
            if (txtuid.Text == "")
            {
                int tmp = objcd.update_NewIdentity(Session["rid"].ToString(), TxtIdentity.Text, c, DateTime.Now);
                if (tmp > 0)
                {
                    msg.Show("Updated successfully");
                    btnIdentity.Enabled = false;
                }
                else
                {
                    msg.Show("Some Error Occured");
                }
            }
            else
            {
                msg.Show("Identity Card details already mentioned.");
            }
        }
        catch(Exception exp)
        {
           int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnIdentity", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }

    /*Undertaking checked function*/
    protected void ChkAgreed_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (ChkAgreed.Checked == true)
            {
                BtnFinalSubmit.Enabled = true;
                ChkAgreed.Enabled = true;
            }
            else
            {
                BtnFinalSubmit.Enabled = false;
                ChkAgreed.Enabled = true;
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "ChkAgreed", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    /*Send OTP to registered mobile number*/
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {

        try
        {
            if (btnSendOTP.Text == "Get OTP")
            {
                if (txt_mob.Text != "")
                {
                    int num = new Random().Next(1000, 9999);
                    hdnfMOTP.Value = Convert.ToString(num);
                    string code = "OTP for new registration on DSSSB OARS portal is: " + hdnfMOTP.Value;
                    objsms.sendmsgNew(txt_mob.Text.Trim(), code, "1007161770334127098");

                    btnSendOTP.Text = "Resend OTP";

                    btnVerifyOTP.Visible = true;
                    txt_mob.Enabled = false;
                    BtnFinalSubmit.Enabled = false;
                    Label56.Text = "OTP has been sent on your registered Mobile No.";

                }
            }
            else
            {
                if (btnSendOTP.Text == "Resend OTP")
                {
                    string code = "OTP for updating personal detail on DSSSB OARS portal is: " + hdnfMOTP.Value;
                    objsms.sendmsgNew(txt_mob.Text.Trim(), code, "1007161770334127098");

                    btnSendOTP.Text = "Resend OTP";
                    btnVerifyOTP.Visible = true;
                    BtnFinalSubmit.Enabled = false;
                    Label56.Text = "Your OTP has been re-sent successfully to your provided mobile number.";

                    btnSendOTP.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), ex.ToString(), "updatemobile.aspx", "sendotp", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }

    /*Verify OTP*/
    protected void btnVerifyOTP_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtMOTP.Text.Trim() == hdnfMOTP.Value)
            {
                OTPVerified.Value = "True";
                hdnfOTPVerifiedMEB.Value = "M";
                btnSendOTP.Visible = false;
                btnVerifyOTP.Visible = false;
                txtMOTP.Visible = false;
                BtnFinalSubmit.Enabled = true;
                Label56.Text = "OTP verified. Please proceed to Final submit.";
                Label56.Focus();
            }

            else
            {
                OTPVerified.Value = "False";
                btnSendOTP.Visible = true;
                btnVerifyOTP.Visible = true;
                BtnFinalSubmit.Enabled = false;
                Label56.Text = "OTP not verified. Please resend OTP to verify.";
                Label56.Focus();
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "btnVerifyOTP", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
    /*Update registered candidate's modifications in registration table*/
    protected void BtnFinalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ChkAgreed.Checked == true && OTPVerified.Value == "True")
            {

                if (TxtName.Text != "" && CheckBox1.Checked == true)
                {
                    int tmp = objcd.update_name(Session["rid"].ToString(), TxtName.Text);
                    if (tmp > 0)
                    {
                        msg.Show("Updated successfully");
                        BtnFinalSubmit.Visible = false;
                    }
                    else
                    {
                        msg.Show("Error in Updating name");
                    }
                }
                if (TxtFname.Text != "" && CheckBox2.Checked == true)
                {
                    int tmp = objcd.update_fname(Session["rid"].ToString(), TxtFname.Text);
                    if (tmp > 0)
                    {
                        msg.Show("Updated successfully");
                        BtnFinalSubmit.Visible = false;
                    }
                    else
                    {
                        msg.Show("Error in Updating Father's name");
                    }
                }
                if (TxtMname.Text != "" && CheckBox3.Checked == true)
                {
                    int tmp = objcd.update_mname(Session["rid"].ToString(), TxtMname.Text);
                    if (tmp > 0)
                    {
                        msg.Show("Updated successfully");
                        BtnFinalSubmit.Visible = false;
                    }
                    else
                    {
                        msg.Show("Error in Updating Mother's name");
                    }
                }
                if (Ddl_gender.SelectedValue != "" && CheckBox4.Checked == true)
                {
                    int tmp = objcd.update_gender(Session["rid"].ToString(), Ddl_gender.SelectedValue);
                    if (tmp > 0)
                    {
                        msg.Show("Updated successfully");
                        BtnFinalSubmit.Visible = false;
                    }
                    else
                    {
                        msg.Show("Error in Updating Gender");
                    }
                }
                if (TxtIdentity.Text != "" && CheckBox5.Checked == true)
                {
                    int tmp = objcd.update_Identity(Session["rid"].ToString(), TxtIdentity.Text);
                    if (tmp > 0)
                    {
                        msg.Show("Updated successfully");
                        BtnFinalSubmit.Visible = false;
                    }
                    else
                    {
                        msg.Show("Error in Updating Identity detail");
                    }
                }
            }
            else
            {

                msg.Show("Please ensure the undertaking is being ticked and OTP verified before proceding. ");
            }
        }
        catch(Exception exp)
        {
            int val = objcd.Inserterrorlog_pag(Session["name"].ToString(), Session["rid"].ToString(), exp.ToString(), "updatemobile.aspx", "BtnFinalSubmit", ipaddress, Request.Headers["User-Agent"].ToString());
        }
    }
}
       


