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
using System.IO;
using System.Drawing;

public partial class Registration_NewForm : Page
{
    message msg = new message();
    string flag = "";
    DataTable dt = new DataTable();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    Sms objsms = new Sms();
    Email sendmail = new Email();
    string SecurityCode;
    string regno = "";
    byte[] imgfile;
    LoginMast ObjMast = new LoginMast();
    verifyAadhar vAadhar = new verifyAadhar();
    CandidateData objcand = new CandidateData();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "checkJavaScriptValidity();", true);
        //txt_name.Focus();
        //txt_re_password.Attributes.Add("onblur", "javascript:SignValidate()");        
        txtpassword.Attributes.Add("onblur", "javascript:PassValidate()");

        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            fillyear("");
            fillddlIdDoc();
            getAadharSelection();
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
        //if (fupIDProofDoc.PostedFile != null && fupIDProofDoc.PostedFile.ContentLength > 0)
        //    UpLoadAndDisplay();
    }
    //private void UpLoadAndDisplay()
    //{
    //    imgPicture.Visible = true;
    //    string imgName = fupIDProofDoc.FileName;
    //    string imgPath = "images/" + imgName;
    //    int imgSize = fupIDProofDoc.PostedFile.ContentLength;
    //    if (fupIDProofDoc.PostedFile != null && fupIDProofDoc.PostedFile.FileName != "")
    //    {
    //        fupIDProofDoc.SaveAs(Server.MapPath(imgPath));
    //        imgPicture.ImageUrl = "~/" + imgPath;
    //        //fupIDProofDoc.PostedFile.InputStream.Read = "~/" + imgPath;
    //    }
    //}
    public void fillddlIdDoc()
    {
        try
        {
            dt = objcand.getProofOfIdentity();
            ddlIdDoc.Items.Clear();
            ddlIdDoc.DataTextField = "docName";
            ddlIdDoc.DataValueField = "docID";
            ddlIdDoc.DataSource = dt;
            ddlIdDoc.DataBind();
            ListItem l1 = new ListItem();
            l1.Text = "--Select--";
            l1.Value = "0";
            ddlIdDoc.Items.Insert(0, l1);
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
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
        ddl_reenter_year.Items.Clear();
        ListItem li;

        //string current_date = DateTime.Now.AddYears(-12).ToString("dd/MM/yyyy");
        for (int i = year; i >= year - y; i--)
        {
            li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            ddl_pass_year.Items.Add(li);
            ddl_reenter_year.Items.Add(li);
        }
        li = new ListItem();
        li.Text = "--Select Year--";
        li.Value = "";
        ddl_pass_year.Items.Insert(0, li);
        ddl_reenter_year.Items.Insert(0, li);
    }
    protected void btnrsubmit_Click(object sender, EventArgs e)
    {
        string current_date = DateTime.Now.AddYears(-12).ToString("dd/MM/yyyy");
        string contentType = string.Empty;
        HttpPostedFile file;
        byte[] document = null;
        bool checkFileTypeIDProof = false;

        Regex reg = new Regex(@"^[a-zA-Z0-9!$%^*@#&]{6,}$");
        Regex regEmail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        Regex reg_num_only = new Regex(@"(?i)^[a-z ]+");
        if (chkPreview.Checked)
        {
            if (CheckBoxdisclaimer.Checked)
            {
                string[] listofvalues = new string[] { "Indian", "Other" };
                if (Validation.chkescape(txt_name.Text) || txt_name.Text.Length > 50 || !reg_num_only.IsMatch(txt_name.Text))
                {
                    msg.Show("Invalid Character in Name or Name length is more than 50 Characters.");

                }
                else if (txtReName.Text.Trim() != txt_name.Text.Trim())
                {
                    msg.Show("Re-enter name of applicant is not same as applicant name.");
                    txtReName.Focus();
                }
                else if (txtReDOB.Text.Trim() != txt_DOB.Text.Trim())
                {
                    msg.Show("Re-enter DOB is not same as DOB.");
                    txtReDOB.Focus();
                }
                else if (txt_fh_name.Text != "" && (Validation.chkLevel20(txt_fh_name.Text) || txt_fh_name.Text.Length > 50 || !reg_num_only.IsMatch(txt_fh_name.Text)))
                {
                    msg.Show("Invalid Character in Father Name or Father Name length is more than 50 Characters.");

                }
                else if (txt_mothername.Text != "" && (Validation.chkLevel20(txt_mothername.Text) || txt_mothername.Text.Length > 50 || !reg_num_only.IsMatch(txt_mothername.Text)))
                {
                    msg.Show("Invalid Character in Mother Name or Mother Name length is more than 50 Characters.");

                }
                else if (txtspouse.Text != "" && (Validation.chkLevel20(txtspouse.Text) || txtspouse.Text.Length > 50 || !reg_num_only.IsMatch(txtspouse.Text)))
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
                else if (RadioButtonList_mf.SelectedValue != rbtReSelectGend.SelectedValue)
                {
                    msg.Show("Re-select gender is not same as selected gender.");
                }
                else if (Validation.chkLevel(txt_mob.Text))
                {
                    msg.Show("Invalid Character in Mobile No");
                }
                else if (Validation.chkLevel(ddl_pass_year.SelectedValue))
                {
                    msg.Show("Invalid Character in Passing year");
                }

                else if (Validation.chkLevel(txt_roll_no.Text) || txt_roll_no.Text.Length < 1 || txt_roll_no.Text.Length > 15)
                {
                    msg.Show("Invalid Character in Roll No or Roll No length is less than 1 digit or Roll No length is more than 15 digit.");
                }

                else if (txt_fh_name.Text == "" && txt_mothername.Text == "")
                {
                    msg.Show("Please enter at least one of Father Name/Mother Name");
                }
                else if (!regEmail.IsMatch(txt_email.Text.Trim()))
                {
                    msg.Show("Please enter valid email.");
                }
                else if (txt_email.Text.Trim() != txtReEntEmail.Text.Trim())
                {
                    msg.Show("Re-enter email is not same as email adress.");
                }
                else
                {
                    try
                    {
                        int proofOfID = 0;
                        int i = 0;
                        string gender = "NULL";
                        string aadharno = string.Empty;
                        string ip = GetIPAddress();
                        string rdate = Utility.formatDatewithtime(DateTime.Now);
                        string rids = "";
                        string AdharNum = txtAdharNo.Text.Trim().Replace(" ", "");
                        if (AdharNum != "")
                        {
                            //validate adhar here using c# then encrypt
                            if (vAadhar.validateVerhoeff(AdharNum))
                            {
                                aadharno = MD5Util.Encrypt(AdharNum, true);
                            }
                            else
                            {
                                msg.Show("Please enter valid Aadhaar number.");
                                return;
                            }
                        }
                        if (rbtHaveAdhar.SelectedValue == "No" && ddlIdDoc.SelectedValue != "0")
                        {
                            proofOfID = Convert.ToInt32(ddlIdDoc.SelectedValue);
                        }
                        if (RadioButtonList_mf.SelectedValue != "S")
                        {
                            gender = RadioButtonList_mf.SelectedValue;
                        }
                        string pofIDNum = MD5Util.Encrypt(txtIdNumber.Text.Trim().ToUpper(), true);
                        string namePOID = txtNameIDProof.Text.Trim();
                        if (fupIDProofDoc.HasFile)
                        {
                            contentType = fupIDProofDoc.PostedFile.ContentType;
                            file = fupIDProofDoc.PostedFile;
                            document = new byte[file.ContentLength];
                            file.InputStream.Read(document, 0, (int)file.ContentLength);
                            checkFileTypeIDProof = uploadPhotoValidation(fupIDProofDoc, document);
                        }
                        rids = txt_DOB.Text.Replace("/", "") + txt_roll_no.Text.TrimStart(new Char[] { '0' }) + ddl_pass_year.SelectedValue;
                        if (rbtHaveAdhar.SelectedValue == "No")
                        {
                            if (fupIDProofDoc.HasFile)
                            {
                                if (pofIDNum != "" && txtReIdNumber.Text.Trim() != "")
                                {
                                    if (namePOID != "" && txtReNameIDProof.Text.Trim() != "")
                                    {
                                        if (checkFileTypeIDProof && OTPVerified.Value == "True")
                                        {
                                            i = ObjMast.insert_registration_new(rids, txtpassword.Text, "0", txt_name.Text, txt_fh_name.Text, txt_mothername.Text, gender, txt_DOB.Text, DDL_Nationality.SelectedValue, txt_mob.Text, Utility.putstring(txt_email.Text), ip, "Y", rdate, txt_roll_no.Text, ddl_pass_year.SelectedValue, txtspouse.Text, aadharno, txtNameAdhar.Text.Trim(), txtNameIDProof.Text.Trim(), proofOfID, pofIDNum, hdnfOTPVerifiedMEB.Value, document);
                                        }
                                        else
                                        {
                                            if (OTPVerified.Value != "True")
                                            {
                                                msg.Show("Please verify OTP sent to your provided number and email address.");
                                            }
                                            else
                                            {
                                                msg.Show("Please check, only image file can be uploaded.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg.Show("Enter name as on ID Proof.");
                                    }
                                }
                                else
                                {
                                    msg.Show("Please enter ID Proof number.");
                                }
                            }
                            else
                            {
                                msg.Show("Please select jpg/image of document to be uploaded.");
                            }
                        }
                        else
                        {
                            if (txtAdharNo.Text.Trim() != "" && txtReAdharNo.Text.Trim() != "")
                            {
                                if (txtNameAdhar.Text.Trim() != "" && txtReNameAdhar.Text.Trim() != "")
                                {
                                    if (OTPVerified.Value == "True")
                                    {
                                        i = ObjMast.insert_registration_new(rids, txtpassword.Text, "0", txt_name.Text, txt_fh_name.Text, txt_mothername.Text, gender, txt_DOB.Text, DDL_Nationality.SelectedValue, txt_mob.Text, Utility.putstring(txt_email.Text), ip, "Y", rdate, txt_roll_no.Text, ddl_pass_year.SelectedValue, txtspouse.Text, aadharno, txtNameAdhar.Text.Trim(), txtNameIDProof.Text.Trim(), proofOfID, pofIDNum, hdnfOTPVerifiedMEB.Value, document);
                                    }
                                    else
                                    {
                                        msg.Show("Please verify OTP sent to your provided number and email address.");
                                    }
                                }
                                else
                                {
                                    msg.Show("Enter name as on Aadhaar ");
                                }
                            }
                            else
                            {
                                msg.Show("Enter Aadhaar number or opt option to upload any other ID proof.");
                            }
                        }
                        if (i > 0)
                        {
                            if (Request.QueryString["flag"] != null)
                            {
                                string serialno = MD5Util.Decrypt(Request.QueryString["serial"].ToString(), true);
                                string postcode = MD5Util.Decrypt(Request.QueryString["postcode"].ToString(), true);
                                int temp = objcand.Insertspecial_reg_mapping(rids, serialno, postcode);
                            }
                            Type cstype = this.GetType();
                            //lbl_r1.Text = rids.Substring(0, 8);
                            //lbl_r2.Text = rids.Substring(8, rids.Length - 12);
                            //lbl_r3.Text = rids.Substring((rids.Length - 4), 4);
                            regno = "You are successfully registered on OARS Portal of DSSSB. Your Regn. No. is " + rids + " which is your login ID.It is a combination of Date of Birth, Class 10th Roll No and Passing year of 10th.";
                            //Session["regmsg"] = regno;

                            objsms.sendmsgNew(txt_mob.Text.Trim(), regno,"1007161770367022993");
                            sendmail.sendMail(txt_email.Text.Trim(), "", "", "sadsssb.delhi@nic.in", "DSSSB online registration", regno, "");
                            //msg.Show("Please use your registration no. and password to apply for the post.");
                            //Server.Transfer("Default.aspx");

                            trnewreg.Visible = false;
                            //tblfilldtl.Visible = false;
                            trreg.Visible = true;
                            lbl.Text = "You are successfully registered on OARS Portal of DSSSB";//"Please Note Your Regn. No:";
                            lblmsg.Text = "Your Registration Number is : " + rids;
                            Label5.Text = "It is a combination of D.O.B., Roll No of Class X and Year of passing of Class X";
                            Label10.Text = "Your registration number has been sent to your registered email-id and mobile number.";
                            Label11.Text = "Use your registration number as login ID and password to access your OARS account.";
                        }
                        else
                        {
                            msg.Show("Data Not Saved");
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                msg.Show("Please agree with the UNDERTAKING.");
            }
        }
        else
        {
            msg.Show("Please agree with preview check box.");
        }
    }

    // check for pdf start
    private bool uploadfile(FileUpload fileupload, byte[] imagesize)
    {
        bool checkfiletype = false;
        byte[] imageSize = imagesize;
        string ipaddress = GetIPAddress();
        try
        {
            if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
            {
                string filename = fileupload.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');
                string ext = System.IO.Path.GetExtension(fileupload.PostedFile.FileName).ToLower();
                if (ext != ".pdf" && ext != ".PDF")
                {
                    msg.Show("only pdf Files are allowed");
                }
                else if (fileupload.PostedFile.ContentLength < 40000)
                {
                    msg.Show("File size cannot be less than 40 KB.");
                }
                else if (fileupload.PostedFile.ContentLength > 60000)
                {
                    msg.Show("File size cannot be greater than 60 KB.");
                }
                else
                {
                    HttpPostedFile uploadedImage = fileupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)fileupload.PostedFile.ContentLength);
                    checkfiletype = chkfiletype(imageSize, ext);
                    if (!checkfiletype)
                    {
                        msg.Show("Only pdf files with size more than 40KB and less than 60KB can be uploaded");

                    }
                    else
                    {
                        return checkfiletype;
                    }

                }
            }

        }
        catch (Exception ex)
        {
            //Response.Redirect("ErrorPage.aspx");
        }
        return checkfiletype;
    }
    public bool chkfiletype(byte[] file, string ext)
    {
        byte[] chkByte = null;

        if (ext == ".pdf")
        {
            chkByte = new byte[] { 37, 80, 68, 70 };
        }

        int j = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (chkByte == null)
            {
                break;
            }
            else if (file[i] == chkByte[i])
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
    // check for pdf ends
    //check for jpg start
    public bool uploadPhotoValidation(FileUpload fu, byte[] imagesize)
    {
        Boolean Flag;
        int ttsize;
        int result1;
        string fileName1 = fu.PostedFile.FileName;
        string type1 = fileName1.Substring(fileName1.LastIndexOf("."));
        type1 = type1.ToLower();
        if (type1 == ".jpg")
        {
            result1 = 1;
            Flag = true;
        }
        else if (type1 == ".JPEG")
        {
            result1 = 1;
            Flag = true;
        }
        else if (type1 == ".jpeg")
        {
            result1 = 1;
            Flag = true;
        }
        else if (type1 == ".JPG")
        {
            result1 = 1;
            Flag = true;
        }
        //else if (type1 == ".PNG")
        //{
        //    result1 = 1;
        //    Flag = true;
        //}
        //else if (type1 == ".png")
        //{
        //    result1 = 1;
        //    Flag = true;
        //}
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "alert", "alert('Choose only jpeg image.')", true);
            result1 = 0;
            return false;
        }
        Session["file_type"] = fu.PostedFile.ContentType.ToString();
        System.Drawing.Image UpImage = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
        if (UpImage.PhysicalDimension.Width < 200 || UpImage.PhysicalDimension.Height < 200)
        {
            msg.Show("Minimum File dimensions should be (15kb to 60kb, min resolution for PANCARD/DRIVING LICENSE - 300*200(width*height), for voter-ID card 200*300(width*height), for passport 450*300(width*height) pixel");
            return false;
        }
        else
        {
            if (fu.PostedFile.ContentLength < 15000)
            {
                msg.Show("File size cannot be less than 15 KB.");
                return false;
            }
            else if (fu.PostedFile.ContentLength > 60000)
            {
                msg.Show("File size cannot be greater than 60 KB.");
                return false;
            }
            else
            {
                ttsize = 1;
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "alert", "alert('File of size more than 40KB and less than 60KB can be uploaded')", true);
                //return true;
            }
        }
        if (result1 == 1)
        {
            if (checkRealJPGFile(imagesize, fu) == true)
            //  if (true == true)
            {
                string type2 = fileName1;
                if (type2.Contains(":"))
                {
                    type2 = fileName1.Substring(fileName1.LastIndexOf("\\"));
                    type2 = type2.Substring(1);

                    Int32 i = type2.Length;
                    if (i < 4)
                    {
                        i = 0;
                    }
                    else
                    {
                        i = i - 4;
                        type2 = type2.Substring(0, i);
                    }
                    Flag = true;
                }
                else
                {
                    Int32 j = type2.Length;
                    if (j < 4)
                    {
                        j = 0;
                    }
                    else
                    {
                        j = j - 4;
                        type2 = type2.Substring(0, j);
                    }
                    Flag = true;
                }
                //if (!Regex.IsMatch(type2.Trim(), @"^[-0-9a-zA-Z_()]{1,100}?$"))
                //{
                //    lberror0.Visible = true;
                //    lberror0.Text = "Spaces / Special Character are not allowed in title of photo!";
                //    return false;
                //}

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "alert", "alert('Invalid image.')", true);
                return false;
            }
        }
        return Flag;
    }
    Boolean checkRealJPGFile(byte[] file, FileUpload passfile)
    {
        Stream fs = default(Stream);
        fs = passfile.PostedFile.InputStream;
        BinaryReader br1 = new BinaryReader(fs);
        imgfile = br1.ReadBytes(passfile.PostedFile.ContentLength);
        //Image file Starting Bytes       
        byte[] chkByte = { 255, 216, 255, 224 };
        int j = 0;

        byte[] buffer = new byte[512];
        // fu.Read(buffer, 0, 512);
        string content = System.Text.Encoding.UTF8.GetString(imgfile);
        if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "alert", "alert('Invalid image.')", true);
            return false;
        }

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
    //check for jpg ends
    protected void btnprceed_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    objsms.sendmsg(txt_mob.Text.Trim(), Session["regmsg"].ToString());
        //    sendmail.sendMail(txt_email.Text.Trim(), "", "", "", "DSSSB online registration", Session["regmsg"].ToString(), "");
        //    msg.Show("Please use your registration no. and password to apply for the post.");
        Server.Transfer("Default.aspx");
        //}
        //catch (Exception ex)
        //{

        //}
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
            string serialno = MD5Util.Decrypt(Request.QueryString["serial"].ToString(), true);
            string postcode = MD5Util.Decrypt(Request.QueryString["postcode"].ToString(), true);
            int temp = objcand.Insertspecial_reg_mapping(lblcandregno.Text, serialno, postcode);
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
    protected void rbtHaveAdhar_SelectedIndexChanged(object sender, EventArgs e)
    {
        getAadharSelection();
    }
    public void getAadharSelection()
    {
        if (rbtHaveAdhar.SelectedValue == "Yes")
        {
            divIDProofDoc.Visible = false;
            lblAadhaarMsg.Visible = true;
            divAadhaar.Visible = true;
            txtIdNumber.Text = string.Empty;
            txtReIdNumber.Text = string.Empty;
            txtNameIDProof.Text = string.Empty;
            txtReNameIDProof.Text = string.Empty;
            lblAadharVal.Text = string.Empty;
            ddlIdDoc.SelectedIndex = 0;
        }
        else
        {
            lblAadhaarMsg.Visible = false;
            divAadhaar.Visible = false;
            divIDProofDoc.Visible = true;
            txtAdharNo.Text = string.Empty;
            txtReAdharNo.Text = string.Empty;
            txtNameAdhar.Text = string.Empty;
            lblAadharVal.Text = string.Empty;
            txtReNameAdhar.Text = string.Empty;
        }
    }
    protected void ddlIdDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIdDoc.SelectedValue != "0")
        {
            divtxtIdNum.Visible = true;
            txtIdNumber.Focus();
            txtIdNumber.Text = string.Empty;
            txtReIdNumber.Text = string.Empty;
            txtNameIDProof.Text = string.Empty;
            txtReNameIDProof.Text = string.Empty;
        }
        else
        {
            //divtxtIdNum.Visible = false;
            msg.Show("Either enter your Aadhaar number or provide any type of identity proof.");
        }
    }
    protected void txt_mob_TextChanged(object sender, EventArgs e)
    {
        string mobNo = txt_mob.Text.Trim();
        checkAvailabilityForRegistration("", mobNo, "", "", "");
    }
    public void checkAvailabilityForRegistration(string regno, string mobNo, string email, string aadhar, string proofOfIDNo)
    {
        //string regno = string.Empty;
        //string mobNo = Utility.putstring(txt_mob.Text.Trim());
        //string email = Utility.putstring(txt_email.Text.Trim());
        //if (txt_DOB.Text != "" && txt_roll_no.Text != "")
        //{
        //    regno = txt_DOB.Text.Replace("/", "") + txt_roll_no.Text.TrimStart(new Char[] { '0' }) + ddl_pass_year.SelectedValue;
        //}
        dt = ObjMast.IsExist_Applicant(regno, mobNo, Utility.putstring(email), aadhar, proofOfIDNo);
        if (dt.Rows.Count > 0)
        {
            //txtOTP.Enabled = false;
            //btnOTP.Enabled = false;
            if (mobNo != "")
            {
                txt_mob.Text = string.Empty;
                txtReMobNo.Text = string.Empty;
                msg.Show("This mobile number is already registered.");
            }
            else if (email != "")
            {
                txt_email.Text = string.Empty;
                txtReEntEmail.Text = string.Empty;
                msg.Show("This email-id is already registered.");
            }
            else if (regno != "")
            {
                txt_roll_no.Text = string.Empty;
                txtReEntRollNo.Text = string.Empty;
                ddl_pass_year.SelectedIndex = 0;
                ddl_reenter_year.SelectedIndex = 0;
                txt_DOB.Text = string.Empty;
                txtReDOB.Text = string.Empty;
                msg.Show("You are already registred with us and your registration number : DOB(ddmmyyyy) + 10th Roll number + 10th Passing Year.");
            }
            else if (aadhar != "")
            {
                txtAdharNo.Text = string.Empty;
                txtReAdharNo.Text = string.Empty;
                msg.Show("You are already registred with the entered Aadhaar number.");
            }
            else if (proofOfIDNo != "")
            {
                txtIdNumber.Text = string.Empty;
                txtReIdNumber.Text = string.Empty;
                msg.Show("You are already registred with the entered ID Proof.");
            }
            else if (Request.QueryString["flag"] == null)
            {
                tblshow.Visible = false;
                msg.Show("Applicant is already Registered.Your Registration No. is :" + txt_DOB.Text.Replace("/", "") + txt_roll_no.Text + ddl_pass_year.SelectedValue);
                Server.Transfer("default.aspx");
            }
            else
            {
                trnewreg.Visible = false;
                tblshow.Visible = true;
                // btnchkavail.Visible = false;
                lblcandregno.Text = dt.Rows[0]["rid"].ToString();
                lblcandname.Text = dt.Rows[0]["name"].ToString();
                lblcandfname.Text = dt.Rows[0]["fname"].ToString();
                lblcanddob.Text = dt.Rows[0]["birthdt"].ToString();
            }
        }
        else
        {
            //txtOTP.Focus();
            //txtOTP.Enabled = true;
            //btnOTP.Enabled = true;
        }
    }
    protected void txt_email_TextChanged(object sender, EventArgs e)
    {
        string email = txt_email.Text.Trim();
        Regex reg = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        if (reg.IsMatch(email))
        {
            checkAvailabilityForRegistration("", "", email, "", "");
        }
        else
        {
            //Label44.Text = "Invalid email-id.";
            //msg.Show("Invalid email-id ");
        }
    }
    protected void ddl_pass_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Validation.chkLevel13(txt_DOB.Text))
        {
            msg.Show("Invalid Character in Date of Birth");
            return;
        }
        else if (Validation.chkLevel(ddl_pass_year.SelectedValue))
        {
            msg.Show("Invalid Character in Passing year");
            return;
        }

        else if ((Validation.chkLevel(txt_roll_no.Text) || txt_roll_no.Text.Length < 1 || txt_roll_no.Text.Length > 15) && (txt_DOB.Text == "" || txtReDOB.Text == ""))
        {
            msg.Show("Invalid Character in 10th Roll No or Roll No length is less than 1 digit or Roll No length is more than 15 digit.");
            ddl_pass_year.SelectedIndex = 0;
            ddl_reenter_year.SelectedIndex = 0;
            txt_DOB.Text = string.Empty;
            txtReDOB.Text = string.Empty;
            return;
        }
        else if (txt_DOB.Text == "" && txtReDOB.Text == "")
        {
            msg.Show("DOB and Re-enter DOB fields are blank.");
            ddl_pass_year.SelectedIndex = 0;
            ddl_reenter_year.SelectedIndex = 0;
            txt_DOB.Text = string.Empty;
            txtReDOB.Text = string.Empty;
            return;
        }
        else
        {
            if (txt_DOB.Text != "" && txt_roll_no.Text != "")
            {
                regno = txt_DOB.Text.Replace("/", "") + txt_roll_no.Text.TrimStart(new Char[] { '0' }) + ddl_pass_year.SelectedValue;

                checkAvailabilityForRegistration(regno, "", "", "", "");
            }
        }
    }
    protected void btnOTP_Click(object sender, EventArgs e)
    {
        Regex regEmail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        try
        {
            if (btnOTP.Text == "Get OTP")
            {
                if (txt_mob.Text != "" && txtReMobNo.Text != "")// txt_email.Text != "")
                {
                    if (regEmail.IsMatch(txt_email.Text))
                    {
                        if (regEmail.IsMatch(txtReEntEmail.Text))
                        {
                            int num = new Random().Next(1000, 9999);
                            hdnfMOTP.Value = Convert.ToString(num);
                            string code = "OTP for new registration on DSSSB OARS portal is: " + hdnfMOTP.Value;
                            objsms.sendmsgNew(txt_mob.Text.Trim(), code, "1007161770334127098");

                            int num1 = new Random().Next(1111, 9999);
                            hdnfEOTP.Value = Convert.ToString(num1);
                            string code1 = "OTP for new registration on DSSSB OARS portal is: " + hdnfEOTP.Value;
                            sendmail.sendMail(txt_email.Text.Trim(), "", "", "sadsssb.delhi@nic.in", "OTP : DSSSB online registration", code1, "");

                            btnOTP.Text = "Resend OTP";
                            lblReemail.Visible = false;
                            btnVerifyOTP.Visible = true;
                            txtReMobNo.Enabled = false; txt_mob.Enabled = false;
                            txt_email.Enabled = false; txtReEntEmail.Enabled = false;
                            Label56.Text = "Two separate OTP’s have been sent on your Email ID and Mobile No."; 
                            
                            //msg.Show("Two separate OTP’s have been sent on your Email ID and Mobile No." + "Mob " + code + "Email " + code1);
                        }
                        else
                        {
                            lblemail.Visible = false;
                            lblReemail.Visible = true;
                            lblMobEnter.Visible = false;
                            lblReMob.Visible = false;
                            lblReemail.Text = "Re-enter valid email address. ";
                            txtReEntEmail.Text = "";
                            txtReEntEmail.Focus();
                        }
                    }
                    else
                    {
                        lblemail.Visible = true;
                        lblReemail.Visible = false;
                        lblMobEnter.Visible = false;
                        lblReMob.Visible = false;
                        lblemail.Text = "Enter valid email address. ";
                        txt_email.Text = "";
                        txt_email.Focus();
                    }
                }
                else
                {
                    if (txt_mob.Text == "")
                    {
                        lblMobEnter.Visible = true;
                        lblReMob.Visible = false;
                        lblemail.Visible = false;
                        lblReemail.Visible = false;
                        lblMobEnter.Text = "Enter your mobile number. ";
                        txt_mob.Focus();
                    }
                    else
                    {
                        lblMobEnter.Visible = false;
                        lblReMob.Visible = true;
                        lblemail.Visible = false;
                        lblReemail.Visible = false;
                        lblReMob.Text = "Re-enter your mobile number. ";
                        txtReMobNo.Focus();
                    }
                }

            }
            else
            {
                if (btnOTP.Text == "Resend OTP")
                {
                    string code = "OTP for new registration on DSSSB OARS portal is: " + hdnfMOTP.Value;
                    objsms.sendmsgNew(txt_mob.Text.Trim(), code, "1007161770334127098");

                    string code1 = "OTP for new registration on DSSSB OARS portal is: " + hdnfEOTP.Value; 
                    sendmail.sendMail(txt_email.Text.Trim(), "", "", "sadsssb.delhi@nic.in", "OTP : DSSSB online registration", code1, "");

                    btnOTP.Text = "Resend OTP";
                    lblReemail.Visible = false;
                    btnVerifyOTP.Visible = true;
                    Label56.Text = "Your OTP has been re-sent successfully to your provided email and mobile number.";
              
                    //msg.Show("Your OTP has been re-sent successfully to your provided email and mobile number." + "Mob " + code + "Email " + code1);
                    btnOTP.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnVerifyOTP_Click(object sender, EventArgs e)
    {
        if (txtMOTP.Text.Trim() == hdnfMOTP.Value && txtEOTP.Text.Trim() == hdnfEOTP.Value)
        {
            OTPVerified.Value = "True";
            hdnfOTPVerifiedMEB.Value = "B";
            btnOTP.Visible = false;
            btnVerifyOTP.Visible = false;
            txtMOTP.Visible = false;
            txtEOTP.Visible = false;
            Label56.Text = "Email and Mobile both are verified. Please proceed to register.";
            Label56.Focus();
        }
        else if (txtMOTP.Text.Trim() == hdnfMOTP.Value)
        {
            OTPVerified.Value = "True";
            hdnfOTPVerifiedMEB.Value = "M";
            btnOTP.Visible = false;
            btnVerifyOTP.Visible = false;
            txtMOTP.Visible = false;
            txtEOTP.Visible = false;
            Label56.Text = "Mobile verified. Please proceed to register.";
            Label56.Focus();
        }
        else if (txtEOTP.Text.Trim() == hdnfEOTP.Value) 
        {
            OTPVerified.Value = "True";
            hdnfOTPVerifiedMEB.Value = "E";
            btnOTP.Visible = false;
            btnVerifyOTP.Visible = false;
            txtMOTP.Visible = false;
            txtEOTP.Visible = false;
            Label56.Text = "Email verified. Please proceed to register.";
            Label56.Focus();
        }
        else
        {
            OTPVerified.Value = "False";
            btnOTP.Visible = true;
            btnVerifyOTP.Visible = true;
            Label56.Text = "OTP not verified. Please resend OTP to verify.";
            Label56.Focus();
        }
    }
    protected void txtAdharNo_TextChanged(object sender, EventArgs e)
    {
        string aadhar = txtAdharNo.Text.Trim().Replace(" ", "");
        checkAadhaarValidity(aadhar);
    }
    protected void txtReAdharNo_TextChanged(object sender, EventArgs e)
    {
        string aadhar = txtReAdharNo.Text.Trim().Replace(" ", "");
        checkAadhaarValidity(aadhar);
    }
    protected void checkAadhaarValidity(string aadhar)
    {
        if (vAadhar.validateVerhoeff(aadhar))
        {
            lblAadharVal.Text = string.Empty;
            checkAvailabilityForRegistration("", "", "", MD5Util.Encrypt(aadhar, true), "");
        }
        else
        {
            txtAdharNo.Text = string.Empty;
            txtReAdharNo.Text = string.Empty;
            msg.Show("Aadhaar number is not valid.");
            lblAadharVal.Text = "Aadhaar number is not valid.";
        }
    }
    protected void txtIdNumber_TextChanged(object sender, EventArgs e)
    {
        Regex reg = new Regex(@"[a-zA-Z0-9]*$");
        if (reg.IsMatch(txtIdNumber.Text.Trim()))
        {
            string proofOfIDNo = MD5Util.Encrypt(txtIdNumber.Text.Trim().ToUpper(), true);
            checkAvailabilityForRegistration("", "", "", "", proofOfIDNo);
        }
        else
        {
            txtIdNumber.Text = string.Empty;
            msg.Show("Only alphanumeric characters allowed in ID Proof number.");
        }
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
