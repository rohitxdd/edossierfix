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
using AjaxControlToolkit;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class Registration : BasePage
{
    LoginMast ObjMast = new LoginMast();
    message msg = new message();
    string flag = "";
    DataTable dt = new DataTable();
    Random randObj = new Random();
    Random randObjCode = new Random();
    Int32 UniqueRandomNumber = 0;
    Sms objsms = new Sms();
    Int32 UniqueRandomNumberCode = 0;
    string SecurityCode;
    string regno = "";
    CandidateData objcand = new CandidateData();
    MD5Util md5Util = new MD5Util();//AnkitaSingh
    static string rno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "checkJavaScriptValidity();", true);
        txt_name.Focus();
        //txt_re_password.Attributes.Add("onblur", "javascript:SignValidate()");        
        txtpassword.Attributes.Add("onblur", "javascript:PassValidate()");

        if (!IsPostBack)
        {

            Session["rno"] = null;

if(Request.QueryString["dob"] == null)
{
    Response.Redirect("~/default.aspx");
}
            UniqueRandomNumber = randObj.Next(1, 10000);

            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            fillyear("");
            if (Request.QueryString["flag"] != null)
            {
                txt_name.Text = MD5Util.Decrypt(Request.QueryString["name"].ToString(), true);
                txt_name.Enabled = false;
                txt_fh_name.Text = MD5Util.Decrypt(Request.QueryString["fname"].ToString(), true);
                txt_fh_name.Enabled = true;
                txt_DOB.Text = MD5Util.Decrypt(Request.QueryString["dob"].ToString(), true);
                txt_DOB.Enabled = false;
            }

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
    public void fillyear(string dob)
    {
        string can_dob = dob;
        int y = 60;
        int year = DateTime.Now.Year;
        if (can_dob != "")
        {
            string b_year = can_dob.Substring(6);
            y = Convert.ToInt16(b_year);
            y = y + 12;
            y = year - y;
        }
        ddl_pass_year.Items.Clear();

        ListItem li;

        //string current_date = DateTime.Now.AddYears(-12).ToString("dd/MM/yyyy");
        for (int i = year; i >= year - y; i--)
        {
            li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            ddl_pass_year.Items.Add(li);
        }
        li = new ListItem();
        li.Text = "--Select Year--";
        li.Value = "";
        ddl_pass_year.Items.Insert(0, li);
    }
    protected void btnrsubmit_Click(object sender, EventArgs e)
    {
        //if (chk_decl.Checked && CheckBoxdisclaimer.Checked)
        string current_date = DateTime.Now.AddYears(-12).ToString("dd/MM/yyyy");

        Regex reg = new Regex(@"^[a-zA-Z0-9!$%^*@#&]{6,}$");

        Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");

        if (CheckBoxdisclaimer.Checked)
        {
            string[] listofvalues = new string[] { "Indian", "Other" };
            if (Validation.chkescape(txtuid.Text))
            {
                msg.Show("Invalid Character in Title");
            }
            else if (Validation.chkescape(txt_name.Text) || txt_name.Text.Length > 50 || !reg_num_only.IsMatch(txt_name.Text))
            {
                msg.Show("Invalid Character in Name or Name length is more than 50 Characters.");

            }
            else if (txt_fh_name.Text!="" && ( Validation.chkLevel20(txt_fh_name.Text) || txt_fh_name.Text.Length > 50 || !reg_num_only.IsMatch(txt_fh_name.Text)))
            {
                msg.Show("Invalid Character in Father Name or Father Name length is more than 50 Characters.");

            }
            else if (txt_mothername.Text!="" && (Validation.chkLevel20(txt_mothername.Text) || txt_mothername.Text.Length > 50 || !reg_num_only.IsMatch(txt_mothername.Text)))
            {
                msg.Show("Invalid Character in Mother Name or Mother Name length is more than 50 Characters.");

            }
            else if (txtspouse.Text!="" && ( Validation.chkLevel20(txtspouse.Text) || txtspouse.Text.Length > 50 || !reg_num_only.IsMatch(txtspouse.Text)))
            {
                msg.Show("Invalid Character in Spouse Name or Spouse Name length is more than 50 Characters.");

            }
            else if (Validation.chkescape(RadioButtonList_mf.SelectedValue))
            {
                msg.Show("Invalid Character in Gender");
            }
            else if (Validation.chkLevel13(txt_DOB.Text))
            {
                msg.Show("Invalid Character in Date of Birth");
            }

            else if (Utility.comparedatesDMY(txt_DOB.Text, current_date) > 0)
            {
                msg.Show("Age Can Not be less than 12 Year Form Current Date");
            }
            //else if (!ValidateDropdown.validate(DDL_Nationality.SelectedValue, listofvalues))
            //{
            //}
            else if (Validation.chkLevel(txt_mob.Text))
            {
                msg.Show("Invalid Character in Mobile No");
            }
            else if (Validation.chkLevel(ddl_pass_year.SelectedValue))
            {
                msg.Show("Invalid Character in Passing year");
            }

            else if (Validation.chkLevel(txt_roll_no.Text) || txt_roll_no.Text.Length < 2 || txt_roll_no.Text.Length > 15)
            {
                msg.Show("Invalid Character in Roll No or Roll No length is less than 2 digit or Roll No length is more than 15 digit.");
            }
            //else if (!reg.IsMatch(txtpassword.Text))
            //{
            //    msg.Show("Enter Valid Password");
            //}
            //else if (!reg.IsMatch(txt_re_password.Text))
            //{
            //    msg.Show("Enter Valid Password");
            //}
            ////else if (Validation.check_essential(txt_name.Text))
            ////{
            ////    msg.Show("Please enter name");
            ////}
            //else if (Validation.check_essential(txt_fh_name.Text))
            //{
            //    msg.Show("Please enter father name");
            //}
            //else if (Validation.check_essential(txt_mothername.Text))
            //{
            //    msg.Show("Please enter mother name");
            //}
            //else if (Validation.check_essential(txt_mob.Text))
            //{
            //    msg.Show("Please enter moblie No.");
            //}
            //else if (Validation.check_essential(txt_email.Text))
            //{
            //    msg.Show("Please enter E-mail Id");
            //}
            //else if (Validation.check_essential(txt_DOB.Text))
            //{
            //    msg.Show("Please enter Date of Birth");
            //}
            //else if (Validation.check_essential(txt_roll_no.Text))
            //{
            //    msg.Show("Please enter 10th Roll No.");
            //}
            else if (txt_fh_name.Text == "" && txt_mothername.Text == "" && txtspouse.Text == "")
            {
                msg.Show("Please enter at least one of Father Name/Mother Name/Spouse Name");
            }

            else
            {
                try
                {
                    //if (MD5Util.Decrypt(Hidden_SecCode.Value, true) == txtcode.Text.Trim())
                    //{
                    //dt = ObjMast.IsExist_Applicant(txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue);
                    //int count = dt.Rows.Count;
                    //if (count == 0)
                    //{

                    string ip = GetIPAddress();
                    string rdate = Utility.formatDatewithtime(DateTime.Now);
                    string rids = "";
                    //dt = ObjMast.GetMaxReg_coun();
                    //string rids = dt.Rows[0]["rid"].ToString();
                    rids = txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue;

                    // int i = ObjMast.insert_registration(rids, txtpassword.Text, txtuid.Text, Utility.putstring(txt_name.Text), Utility.putstring(txt_fh_name.Text), Utility.putstring(txt_mothername.Text), RadioButtonList_mf.SelectedValue, txt_DOB.Text, DDL_Nationality.SelectedValue, Utility.putstring(txt_mob.Text), Utility.putstring(txt_email.Text), ip, "Y", rdate, txt_roll_no.Text, ddl_pass_year.SelectedValue);
                    int i = ObjMast.insert_registration(rids, txtpassword.Text, "0", Utility.putstring(txt_name.Text), Utility.putstring(txt_fh_name.Text), Utility.putstring(txt_mothername.Text), RadioButtonList_mf.SelectedValue, txt_DOB.Text, DDL_Nationality.SelectedValue, Utility.putstring(txt_mob.Text), Utility.putstring(txt_email.Text), ip, "Y", rdate, txt_roll_no.Text, ddl_pass_year.SelectedValue, Utility.putstring(txtspouse.Text));
                    if (i > 0)
                    {


                        if (Request.QueryString["flag"] != null)
                        {
                            //string serialno = MD5Util.Decrypt(Request.QueryString["serial"].ToString(), true);
                            string serialno = Session["serial"].ToString();
                            string postcode = MD5Util.Decrypt(Request.QueryString["postcode"].ToString(), true);
                            int temp = objcand.Insertspecial_reg_mapping(rids, serialno, postcode);
                        }



                        Type cstype = this.GetType();
                        lbl_r1.Text = rids.Substring(0, 8);
                        lbl_r2.Text = rids.Substring(8, rids.Length - 12);
                        lbl_r3.Text = rids.Substring((rids.Length - 4), 4);
                        regno = "You are successfully registered in OARS at DSSSB , Regn. No. " + rids + ".It is a combination of Date of Birth, Roll No and Passing year of 10th.";//"Please Note Your Regn. No:" + rids + ". It is a combination of Date of Birth,Roll No and Passing Year of 10th.";
                        Session["regmsg"] = regno;
                        trnewreg.Visible = false;
                        tblfilldtl.Visible = false;
                        trreg.Visible = true;
                        lbl.Text = "You are successfully registered in OARS at DSSSB , Regn. No.";//"Please Note Your Regn. No:";
                        lblmsg.Text = rids;
                        Label5.Text = "It is a combination of ";
                        Label10.Text = "Date of Birth, Roll No and Passing Year of 10th.";
                        //msg.Show(regno);

                        //MD5Util md5util = new MD5Util();
                        //string url = md5util.CreateTamperProofURL("message_reg.aspx", null, "regno=" + MD5Util.Encrypt(rids, true));

                        //ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>javascript:window.open('" + url + "','_blank','width=500,height=100,left=650,top=400,resizable=no,scrollbars=no,location=no').focus();</script>");
                        //ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>centeredPopup('" + url + "','myWindow','600','200','no')</script>");
                        //ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>centered_Popup('" + url + "','myWindow','600','200')</script>");
                        //Server.Transfer("Default.aspx");

                        //                             var PopupWindow=null;
                        //settings='width='+ w + ',height='+ h + ',location=no,directories=no,menubar=no,toolbar=no,status=no,scrollbars=no,resizable=no,dependent=no';        
                        //PopupWindow=window.open('DatePicker.aspx?Ctl=' + ctl,'DatePicker',settings);
                        //PopupWindow.focus();
                    }
                    else
                    {
                        msg.Show("Data Not Saved");
                    }
                    //}
                    //else
                    //{
                    //    if (Request.QueryString["flag"] == null)
                    //    {
                    //        tblshow.Visible = false;
                    //        msg.Show("Applicant is already Registered.Your Registration No. is :" + txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue);
                    //        Server.Transfer("default.aspx");
                    //    }
                    //    else
                    //    {
                    //       // trnewreg.Visible = false;
                    //        tblshow.Visible = true;
                    //        lblcandregno.Text = dt.Rows[0]["rid"].ToString();
                    //        lblcandname.Text = dt.Rows[0]["name"].ToString();
                    //        lblcandfname.Text = dt.Rows[0]["fname"].ToString();
                    //        lblcanddob.Text = dt.Rows[0]["birthdt"].ToString();
                    //    }
                    //}
                    //}
                    //else
                    //{
                    //    txtcode.Text = "";
                    //    msg.Show("Security code does not Match");
                    //}
                }
                catch (Exception ex)
                {

                }

            }
        }
        else
        {
            msg.Show("Please agree with the Undertaking.");
        }
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        //Response.Redirect("default.aspx");
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    UniqueRandomNumberCode = randObjCode.Next(1000, 9999);
    //    Hidden_SecCode.Value = UniqueRandomNumberCode.ToString();
    //    Hidden_SecCode.Value = MD5Util.Encrypt(Hidden_SecCode.Value, true);
    //    //jagat un_comment the line here
    //    string SecurityCode1 = "Please Note Your Security Code:" + UniqueRandomNumberCode;
    //    msg.Show(SecurityCode1);
    //    //code for sms
    //    //SecurityCode = Convert.ToString(UniqueRandomNumberCode);
    //    //objsms.sendmsg(txt_mob.Text, SecurityCode);
    //    //msg.Show(SecurityCode);
    //}




    protected void btnprceed_Click(object sender, EventArgs e)
    {
        Sms objsms = new Sms();
        objsms.sendmsg(txt_mob.Text, Session["regmsg"].ToString());
        msg.Show("Please use your registration no. and password to apply for the post.");
        Server.Transfer("Default.aspx");
    }
    protected void txt_DOB_TextChanged(object sender, EventArgs e)
    {
        string current_date = DateTime.Now.AddYears(-12).ToString("dd/MM/yyyy");
        if (!txt_DOB.Text.Contains("/"))
        {
            msg.Show("Date can only be separated by '/'");
            txt_DOB.Text = "";
            txt_DOB.Focus();
        }
        else if (txt_DOB.Text.Length == 10)
        {
            if (Utility.comparedatesDMY(txt_DOB.Text, current_date) > 0)
            {
                msg.Show("Age Can Not be less than 12 Year Form Current Date");
                txt_DOB.Text = "";
                txt_DOB.Focus();
            }
        }
        else
        {
            string dob = txt_DOB.Text.ToString();
            fillyear(dob);
        }

    }
    protected void btnagree_Click(object sender, EventArgs e)
    {
        if (chkagree.Checked)
        {
            string serialno = Session["serial"].ToString();
            string postcode = MD5Util.Decrypt(Request.QueryString["postcode"].ToString(), true);
            int temp = objcand.Insertspecial_reg_mapping(lblcandregno.Text, serialno,postcode);
            if (temp > 0)
            {
                Server.Transfer("Default.aspx");
            }
        }
        else
        {
            msg.Show("Please check disclaimer first");
            return;
        }
    }
    protected void btndisagree_Click(object sender, EventArgs e)
    {
        if (chkagree.Checked)
        {
            msg.Show("You will not be able to apply for any vacant post you have already applied.");
            Server.Transfer("Default.aspx");
        }
        else
        {
            msg.Show("Please check disclaimer first");
            return;
        }
    }
    protected void btnchkavail_Click(object sender, EventArgs e)
    {
        if (Validation.chkLevel13(txt_DOB.Text))
        {
            msg.Show("Invalid Character in Date of Birth");
        }
        else if (Validation.chkLevel(ddl_pass_year.SelectedValue))
        {
            msg.Show("Invalid Character in Passing year");
        }

        else if (Validation.chkLevel(txt_roll_no.Text) || txt_roll_no.Text.Length < 2 || txt_roll_no.Text.Length > 15)
        {
            msg.Show("Invalid Character in Roll No or Roll No length is less than 2 digit or Roll No length is more than 15 digit.");
        }
        else
        {
            rno = txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue;//Added by AnkitaSingh for 90/09 Dated:28-11-2022
            ObjMast.updateOldpostEntry(txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue, Session["serial"].ToString());//Added by AnkitaSingh for 90/09 Dated:04-10-2022
            dt = ObjMast.IsExist_Applicant(txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue,"","","","");
            int count = dt.Rows.Count;
            if (count == 0)
            {
               // tblfilldtl.Visible = true;
                btnchkavail.Visible = false;
                LblMessage.Visible = true;//Added by AnkitaSingh Dated: 23-12-2022
                Panel1.Visible = false;//Added by AnkitaSingh Dated: 23-12-2022
                UploadDoc.Visible = false;//Added by AnkitaSingh Dated: 23-12-2022
                
                // msg.Show("You are requested to register as a new user to proceed");
                //Server.Transfer("Default.aspx");
            }
            else
            {

                //Session["rno"] = rno;
                //tblfilldtl.Visible = false;
                //if (Request.QueryString["flag"] == null)
                //{
                //    tblshow.Visible = false;
                //    msg.Show("Applicant is already Registered.Your Registration No. is :" + txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue);
                //    Server.Transfer("default.aspx");
                //}
                //else
                //{
                //    // trnewreg.Visible = false;
                //    tblshow.Visible = true;
                //    btnchkavail.Visible = false;
                //    lblcandregno.Text = dt.Rows[0]["rid"].ToString();
                //    lblcandname.Text = dt.Rows[0]["name"].ToString();
                //    lblcandfname.Text = dt.Rows[0]["fname"].ToString();
                //    lblcanddob.Text = dt.Rows[0]["birthdt"].ToString();
                //}
                //modified by AnkitaSingh for 90/09

                LblMessage.Visible = false;//Added by AnkitaSingh Dated: 23-12-2022

                tblDetails.Visible = true;//Added by AnkitaSingh dated:03-11-2022
                string dob = (txt_DOB.Text).Replace("/", "");
                string dob1 = dob.Substring(0, 4);
                string dd = dob.Substring(0, 1);
                if (dd == "0")
                {
                    dob1 = dob.Substring(1, 3);
                }
                string yr = dob.Substring(6, 2);
                string birthdt = dob1 + yr;
                DataTable dtcheck = objcand.checkserialnoforcandidate(Session["serial"].ToString(), birthdt);
                int match = 0;
                if (dtcheck.Rows.Count != 0)
                {
                    if (dt.Rows[0]["name"].ToString().Trim().Equals(dtcheck.Rows[0]["name"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        match++;
                    }
                    if (dt.Rows[0]["fname"].ToString().Trim().Equals(dtcheck.Rows[0]["f_name"].ToString().Trim(),StringComparison.OrdinalIgnoreCase))
                    {
                        match++;
                    }
                }
                LblRegno.Text = dt.Rows[0]["rid"].ToString();
                LblName.Text = dt.Rows[0]["name"].ToString();
                LblFname.Text= dt.Rows[0]["fname"].ToString();
                LblDob.Text = dt.Rows[0]["birthdt"].ToString();

                LblName1.Text = dtcheck.Rows[0]["name"].ToString();
                LblFname1.Text = dtcheck.Rows[0]["f_name"].ToString();
                LblDob1.Text = dtcheck.Rows[0]["dob"].ToString();

                if (match==1)
                {
                    LblMatch.Text = "DATA PARTIALLY MATCHED!!!";
                }
                else if(match==2)
                {
                    LblMatch.Text = "DATA MATCHED!!!";
                }
                else
                {
                    LblMatch.Text = "DATA NOT MATCHED!!!";
                    BtnOK.Visible = false;//13-03-2023  
                }
               
            }
        }
    }


    //Added by AnkitaSingh
    protected void BtnOK_Click(object sender, EventArgs e)//Added by AnkitaSingh
    {
        //if (LblMatch.Text == "DATA MATCHED!!!")
        //{
        //    msg.Show(" Applicant is already Registered.\n\n Your Registration No. is : " + txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue + "\n\n Login to apply for postcode 90/09 ");
        //}
        //else if(LblMatch.Text == "DATA NOT MATCHED!!!" || LblMatch.Text == "DATA PARTIALLY MATCHED!!!")
        //{
        //    msg.Show("Contact DSSSB Helpdesk to proceed");
        //}
        //Server.Transfer("Default.aspx");

        Panel1.Visible = true;
        UploadDoc.Visible = false;
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        message objmsg = new message();
        int len = fuRR.PostedFile.ContentLength;
        string ctype = fuRR.PostedFile.ContentType;

        if (fuRR.PostedFile == null || len == 0)
        {
            objmsg.Show("Please upload the document");
            return;
        }
        if (len > 0 && ctype != "application/pdf")
        {
            objmsg.Show("Only pdf files can be uploaded");
            return;
        }
        byte[] file1 = new byte[len];
        fuRR.PostedFile.InputStream.Read(file1, 0, len);
        string StrFileName = fuRR.PostedFile.FileName.Substring(fuRR.PostedFile.FileName.LastIndexOf("\\") + 1);
        string fileext = "";
        if (StrFileName != "")
        {
            string[] filename = new string[2];
            filename = StrFileName.Split('.');
            fileext = filename[1].ToString();
        }

        bool checkfiletype = chkfiletype(file1, fileext);
        if (!checkfiletype)
        {
            objmsg.Show("Only pdf files can be uploaded");
            return;
        }

        string serialno = Session["serial"].ToString();
        string postcode = "90/09";
        char consent = 'Y';

        //if (Session["rno"] != null)
        //{
        //    rno = Session["rno"].ToString();
        //}else
        //{
        //    Response.Redirect("~/splcandidate.aspx");
        //}

        int tmp = objcand.uploadDoc(txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue, serialno, postcode, consent, file1, RblSelect.SelectedItem.Text);
        if (tmp > 0)
        {
            msg.Show("Document uploaded successfully!");
            msg.Show("Kindly Sign In to apply for the vacancy");
            Server.Transfer("Default.aspx");
        }
        else
        {
            msg.Show("Document upload error, TRY AGAIN!!!");
        }
    }
       

    public bool chkfiletype(byte[] file, string ext)
    {
        byte[] chkByte = null;
        if (ext == "pdf")
        {
            chkByte = new byte[] { 37, 80, 68, 70 };
        }
        int j = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (file[i] == chkByte[i])
            {
                j = j + 1;

            }
        }
        if (j == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void RblSelect_OnSelectIndexChanged(object sender, EventArgs e)
    {
        UploadDoc.Visible = true;
    }

   
}
