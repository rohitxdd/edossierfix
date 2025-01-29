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
using System.Threading;
using System.IO;

public partial class ForgetPassword : System.Web.UI.Page
{
    LoginMast objLogin = new LoginMast();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    Int32 UniqueRandomNumber_code = 0;
    forgetpass objforgetpass = new forgetpass();
    string todaydate;
    string username;
    message msg = new message();
    DataTable dt = new DataTable();
    Sms objsms = new Sms();
    Int32 UniqueRandomNumberCode = 0;
    Random randObjCode = new Random();
    string SecurityCode;
    Utility Utlity = new Utility();
    CandidateData objcd = new CandidateData();


    protected void Page_Load(object sender, EventArgs e)
    {
        //TextBox1.Focus();
        TRButton.Visible = false;
        TRconfirmPass.Visible = false;
        TRPassword.Visible = false;
        

        //txt_dd.Focus();
        txtpassword.Attributes.Add("onblur", "javascript:PassValidate()");
        UniqueRandomNumber = randObj.Next(1000, 9999);

        //UniqueRandomNumber_code = randObj.Next(1, 10000);
        //Session["token"] = UniqueRandomNumber_code.ToString();
        //this.csrftoken.Value = Session["token"].ToString();

        dt= objforgetpass.GetSchedulemUpdate();
        if (dt.Rows.Count == 0)
        {
            btnchangereq.Visible = false;
            btnrequeststatus.Visible = false;
        }
        else
        {
            btnchangereq.Visible = true;
            btnrequeststatus.Visible = true;
        }




        if (!IsPostBack)
        {
            UniqueRandomNumber_code = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber_code.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            txt_dd.Focus();
            populate_year(DropDownList_year);

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

    private void populate_year(DropDownList passyear)
    {
        ListItem li;

        int year = DateTime.Now.Year;

        for (int i = year; i >= year - 40; i--)
        {
            li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            passyear.Items.Add(li);
        }
        li = new ListItem();
        li.Text = "-Select-";
        li.Value = "-1";
        passyear.Items.Insert(0, li);
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        trentercode.Visible = true;
       // trUpdateMobEm.Visible = true;
        trresetpwd.Visible = true;
        trpwdtypenotice.Visible = true;
        btnchangereq.Visible = false;
        btnrequeststatus.Visible = false;
        TextBoxSecurityCode.Focus();
        bool emailsent = false;
        //UniqueRandomNumberCode = randObjCode.Next(1000, 9999);
        string ip = GetIPAddress();
        username = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();
        //username = TextBox1.Text;

        if (Validation.chklogin(username) == "Error")
        {

            msg.Show("Invalid Input");

        }
        else if (Validation.chkLevel(username))
        {
            msg.Show("Invalid Inputs");

        }
        else
        {
            DataTable dtReg = objforgetpass.VerifyRegno(username);

            if (dtReg.Rows.Count > 0)
            {
                DataTable dtFP = objforgetpass.GetOTP(username);

                if (dtFP.Rows.Count > 0)
                {

                    String secno = dtFP.Rows[0]["randomno"].ToString();
                    UniqueRandomNumber = Convert.ToInt32(secno);
                    
                    //String expired = rows[0]["expired"].ToString();
                }

                //DataRow[] rows = dtFP.Select("expired='N'");
                ////rows[0]["randomno"].ToString();
                //String secno = "";
                //String expired ="";

                //if (rows.Length > 0)
                //{
                //    secno = rows[0]["randomno"].ToString();
                //    expired = rows[0]["expired"].ToString();
                //}
                //if (secno != "" && expired != "Y")
                //{
                //    UniqueRandomNumber = Convert.ToInt32(secno);
                //}
                else
                {
                    int i = objforgetpass.ForgetPassRandom(username, UniqueRandomNumber,ip);                  
                }
                SecurityCode = Convert.ToString(UniqueRandomNumber);
                //2 line add by jagat
                //string SecurityCode1 = "Please Note Your Security Code:" + UniqueRandomNumber;
                //msg.Show(SecurityCode1);
                string mobile = dtReg.Rows[0]["mobileno"].ToString();
                string code = "OTP for resetting password of your DSSSB online login account is " + SecurityCode; //"OTP for Reset Password is : " + SecurityCode;
                string mail_code = "Dear Candidate, \nThe OTP for Reset Password is : " + SecurityCode + "\n\n Instructions:\n1. This OTP is valid for today only. \n2. This is auto generated mail. Please do not reply to it.\n\n--DSSSB";
                string email = Utility.getstring(dtReg.Rows[0]["email"].ToString());

                //Response.Redirect("http://10.128.65.106/sms/default.aspx?mobile=" + mobile + "&msg=" + code);
                //Server.Transfer("http://10.128.65.106/sms/default.aspx?mobile="+mobile+"&msg="+code);

                //objsms.sendmsg(mobile, code); commented on 06042021 by RKP top implement new TRAI rule for sms
                string templateID = "1007161562148943825";
                //string templateID = "1007163359256238823";12-01-2023
                objsms.sendmsgNew(mobile, code, templateID);
                Email obj_email = new Email();
                int cnt1 = 0;
                //string a = obj_email.sendMail(email, "", "", "DSSSB-DONOTREPLY@", "Security Code", mail_code, mail_code);

                if (email != "")//10-01-2023 required to open port 25 for email for dest public ip
                {
                    emailsent = obj_email.sendMail(email, "", "", "sadsssb.delhi@nic.in", "Security Code", mail_code, "");
                    if (emailsent)
                    {
                        cnt1++;
                    }
                }

                //HttpContext.Current.Response.Write("<script language=\"javascript\" type=\"text/javascript\">alert('" + a + "')</script>");
                //msg.Show(a.ToString());
                ////string SecurityCode = "Please Note Your Security Code:" + UniqueRandomNumber;
                //msg.Show(SecurityCode);

            }
            else
            {
                msg.Show("Please Enter Valid Registration Number");
            }


        }
    }
    protected void ButtonConfirmCode_Click(object sender, EventArgs e)
    {
 
        username = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();
        ViewState["username"] = username;
        //username = TextBox1.Text;

        if (Validation.chklogin(username) == "Error")
        {
            msg.Show("Invalid Input");

        }
        else if (Validation.chkLevel(username))
        {
            msg.Show("Invalid Inputs");
        }
        else if (Validation.chkLevel(TextBoxSecurityCode.Text))
        {
            msg.Show("Invalid Inputs");
        }
        else if (Validation.chklogin(TextBoxSecurityCode.Text) == "Error")
        {

            msg.Show("Invalid Inputs");

        }
        else
        {
            if (TextBoxSecurityCode.Text != "")
            {
                DataTable dtFP = objforgetpass.GetOTP(username);
                if (dtFP.Rows.Count > 0)
                {
                    String secno = dtFP.Rows[0]["randomno"].ToString();
                    if (secno != TextBoxSecurityCode.Text)
                    {
                        msg.Show("Enter Valid OTP. If you have not received the OTP on your Registered Mobile, Click above to get it again.");
                    }

                    else
                    {

                        TRButton.Visible = true;
                        TRconfirmPass.Visible = true;
                        TRPassword.Visible = true;
                        TextBoxSecurityCode.ReadOnly = true;
                        txtpassword.Focus();
                        //TextBox1.Enabled = false;
                        txt_dd.Enabled = false;
                        txt_mm.Enabled = false;
                        txt_yyyy.Enabled = false;
                        txt_regno.Enabled = false;
                        DropDownList_year.Enabled = false;
                        //TDTextbox.Visible = false;
                        ButtonConfirmCode.Visible = false;
                        Button1.Visible = false;
                    }
                }

                else
                {
                    msg.Show("First Get OTP on your Registered Mobile No to Reset Password");

                }
            }

            //else
            //{ 
            //    msg.Show("Incorrect OTP"); }

            //}
            //else
            //{
            //    msg.Show("Please Enter Valid Registration Number");
            //}
        }

        
    }

    protected void ButtonResetPass_Click(object sender, EventArgs e)
    {
        string ip = GetIPAddress();
          // username = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();

        username = ViewState["username"].ToString();

           string password = txtpassword.Text;
          if (Validation.chkLevel(username))
           {
               msg.Show("Invalid Inputs");
           }
           else if (Validation.chkLevel1(password))
           {
               msg.Show("Invalid Inputs");
           }
           else
           {
               int i = objforgetpass.updatePassword(username, password, ip);
               if (i > 0)
               {
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "javascript:alert('Password Updated Sucessfully')", true);
                   objforgetpass.Updateusername(username, TextBoxSecurityCode.Text);
                   Response.AddHeader("REFRESH", "0;URL=Default.aspx");
                   
               }
           }
                

            }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        //Response.Redirect("default.aspx");
    }
    protected void btn_ResetMobEma_Click(object sender, EventArgs e)
    {
        //Response.Redirect("ResetPasswordForm.aspx");
    }
    protected void Btnupdatemobemail_Click(object sender, EventArgs e)
    {
        try
        {
        

            //=================== Code for Document 1 ======================================
            byte[] imageSize1 = new byte[FileUpload1.PostedFile.ContentLength];
            HttpPostedFile uploadedImage = FileUpload1.PostedFile;
            uploadedImage.InputStream.Read(imageSize1, 0, FileUpload1.PostedFile.ContentLength);
            string ext1 = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            bool checkfiletypeDocupload_1 = chkfiletypeDocupload(imageSize1, ext1);
            //=================== End for Documnet 1 ======================================


            //================== Code for Document 2 ======================================
            byte[] imageSize2 = new byte[FileUpload2.PostedFile.ContentLength];
            HttpPostedFile uploadedImage1 = FileUpload2.PostedFile;
            uploadedImage1.InputStream.Read(imageSize2, 0, FileUpload2.PostedFile.ContentLength);
            string ext2 = System.IO.Path.GetExtension(FileUpload2.FileName).ToLower();
            bool checkfiletypeDocupload_2 = chkfiletypeDocupload(imageSize2, ext2);
            //================= End for Documnet 2 =========================================

            //=========Check for mobile No.=================================================
            
            if (txt_dd.Text == "" || txt_mm.Text == "" || txt_yyyy.Text == "" || txt_regno.Text == "" || DropDownList_year.SelectedItem.Text == "-Select--")
            {
                msg.Show("Please Enter Registration Number");
            }
            else  if (Validation.chkLevel0(txtboxUPMobile.Text))
            {
                msg.Show("Invalid value in Mobile Number");
            }
            else if (!this.FileUpload1.HasFile)
            {
                msg.Show("Please upload the Document 1.");
            }
            else if (!this.FileUpload2.HasFile)
            {
                msg.Show("Please upload the Document 2.");
            }
            else if (!checkImageDocupload_1())
            {
                msg.Show("Upload only  PDF File Only for Document 1");
            }

            else if (!checkImageDocupload_2())
            {
                msg.Show("Upload only  PDF File Only for Document 2");
            }

            else if (!checkfiletypeDocupload_1)
            {
                msg.Show("Uploaded File is not a Valid File for Document 1");
            }

            else if (!checkfiletypeDocupload_2)
            {
                msg.Show("Uploaded File is not a Valid File for Document 2");
            }

            else if (!checkFileSizeDocupload_1())
            {
                msg.Show("Upload only Maximum 1MB size for Document 1");
            }

            else if (!checkFileSizeDocupload_2())
            {
                msg.Show("Upload only Maximum 1MB size for Document 2");
            }
               
            else
            {
                //string docflgupload_1 = "N";
                //if (FileUpload1.HasFile && FileUpload1.PostedFile.ContentLength >= 0)
                //{
                //    docflgupload_1 = "Y";
                //}

                //string docflgupload_2 = "N";
                //if (FileUpload2.HasFile && FileUpload2.PostedFile.ContentLength >= 0)
                //{
                //    docflgupload_2 = "Y";
                //}

                // Getting RequestID
                Int64 RequestID = Utlity.GetRequestID();

                //===========Getting Ext and file Name for Doc 1 (Photo Identity Card) ============
                string filenameDoc1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string[] FileExtensionDoc1 = filenameDoc1.Split('.');
                string extDoc1 = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                //==========End of Getting Ext and file Name for Doc 1 (Photo Identity Card) =============


                //===========Getting Ext and file Name for Doc 2 (12th Marksheet) ============
                string filenameDoc2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string[] FileExtensionDoc2 = filenameDoc2.Split('.');
                string extDoc2 = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName).ToLower();
                //==========End of Getting Ext and file Name for Doc 2 (Photo Identity Card) =============

                string regno = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();
               
                if (regno == "" || regno == null)
                {
                    msg.Show("Please Enter Registration Number");
                    return;
                }
                DataTable dtcheckmobile = objcd.getdetail(regno);
                string mobileno = dtcheckmobile.Rows[0]["mobileno"].ToString();
                if (mobileno == txtboxUPMobile.Text)
                {
                    msg.Show("The entered Mobile No. is already registered");
                    return;
                }
                DataTable dtFP = objforgetpass.CheckRIDinRegistration(regno);

                if (dtFP.Rows.Count > 0)
                {

                    dt = objforgetpass.GetRequestNo(regno);
                    if (dt.Rows.Count > 0)
                    {
                        cleartextbox();
                        Int64 Request_ID = Convert.ToInt64(dt.Rows[0]["RequestID"].ToString());
                        msg.Show("Yours Request is Already Submitted,yours Request ID is : " + Request_ID + " ");
                        return;
                    }

                    string ip = GetIPAddress();
                    string edate = Utility.formatDatewithtime(DateTime.Now);

                    int i = objforgetpass.TranscationForgetPasswordDetails(RequestID, regno.ToString(), txtboxUPmothername.Text, txtboxUPMobile.Text, txtboxUPEmail.Text, ip, edate, imageSize1, filenameDoc1, extDoc1, imageSize2, filenameDoc2, extDoc2);
                    if (i > 0)
                    {
                       string sms_mobileno = dtcheckmobile.Rows[0]["mobileno"].ToString();
                       string sms_message = "A Request has been recieved in OARS for change of your registered Mobile No./EmailID for OARS RegNo. " + regno + ". If the same is not submitted by you ,Please contact DSSSB office Immediately."; 
                       

                        //Response.Redirect("http://10.128.65.106/sms/default.aspx?mobile=" + mobile + "&msg=" + code);
                        //Server.Transfer("http://10.128.65.106/sms/default.aspx?mobile="+mobile+"&msg="+code);

                       objsms.sendmsg(sms_mobileno, sms_message);
                       
                        cleartextbox();
                        string msgStr = "Request Sent Successfully, yours Request ID is : " + RequestID + " ";
                        if (msgStr != "")
                        {
                            Uri baseUri = new Uri("https://dsssbonline.nic.in");
                            string script = "alert('" + msgStr + "');\n";
                            script += "location.href='" + baseUri.AbsolutePath + "';\n";
                            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", script, true);
                        }
                    }

                    else
                    {
                        cleartextbox();
                        msg.Show("Some thing went wrong");
                    }
                }
                else
                {
                    cleartextbox();
                    msg.Show("Enter Valid Registration Number");
                }

            }
        }
        catch (ThreadAbortException ex)
        {

        }

        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }


    private void cleartextbox()
    {
             txt_dd.Text="";
             txt_mm.Text ="";
             txt_yyyy.Text="";
             txt_regno.Text = "";
             txtboxUPEmail.Text = "";
             txtboxUPMobile.Text="";
             txtboxUPmothername.Text="";
             DropDownList_year.ClearSelection();

    }

    private bool checkFileSizeDocupload_1()
    {
        bool flag = true;
        if (FileUpload1.HasFile)
        {
            if (FileUpload1.PostedFile.ContentLength > 1048576)
            {

                flag = false;

            }
        }
        return flag;
    }

    private bool checkFileSizeDocupload_2()
    {
        bool flag = true;
        if (FileUpload2.HasFile)
        {
            if (FileUpload2.PostedFile.ContentLength > 1048576)
            {

                flag = false;

            }
        }
        return flag;
    }


    public bool chkfiletypeDocupload(byte[] file, string ext)
    {
        byte[] chkByte = null;
        if (ext == ".jpeg" || ext == ".jpg")
        {
            chkByte = new byte[] { 255, 216, 255, 224 };
        }
        else if (ext == ".pdf")
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

    private bool checkImageDocupload_1()
    {
        bool flag = true;
        byte[] imageSize;
        string ext = "";
        if (FileUpload1.HasFile)
        {
            imageSize = new byte[FileUpload1.PostedFile.ContentLength];
            if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "" && FileUpload1.PostedFile.ContentLength > 0)
            {
                string filename = FileUpload1.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');
                ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();


                if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg")
                {
                    HttpPostedFile uploadedImage = FileUpload1.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)FileUpload1.PostedFile.ContentLength);
                    flag = true;

                }
                else
                {
                    flag = false;
                }

            }
        }
        return flag;
    }


    private bool checkImageDocupload_2()
    {
        bool flag = true;
        byte[] imageSize;
        string ext = "";
        if (FileUpload2.HasFile)
        {
            imageSize = new byte[FileUpload2.PostedFile.ContentLength];
            if (FileUpload2.PostedFile != null && FileUpload2.PostedFile.FileName != "" && FileUpload2.PostedFile.ContentLength > 0)
            {
                string filename = FileUpload2.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');
                ext = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName).ToLower();


                if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg")
                {
                    HttpPostedFile uploadedImage = FileUpload2.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)FileUpload2.PostedFile.ContentLength);
                    flag = true;

                }
                else
                {
                    flag = false;
                }

            }
        }
        return flag;
    }



    //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox1.Checked)
    //    {
    //        TRNewRequest.Visible = true;
    //    }
    //    else
    //    {
    //        TRNewRequest.Visible = false;
    //    }

    //}
    protected void btnchangereq_Click(object sender, EventArgs e)
    {
        //TRNewRequest.Visible = true;
        //trUpdateMobEm.Visible = false;
        //trentercode.Visible = false;
        //trresetpwd.Visible = false;
        //trpwdtypenotice.Visible = false;
        //Button1.Visible = false;
        //btnchangereq.Visible = false;
        //btnrequeststatus.Visible = false;
    }
    protected void btnrequeststatus_Click(object sender, EventArgs e)
    {
        
        string regno = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();

        if (regno == "" || regno == null)
        {
            msg.Show("Please Enter Registration Number");
            return;
        }

            //Checking regno from RequestPool_UpdateMobile Table
            DataTable dt1 = objforgetpass.GetRequestNofromRequestUpdate(regno);
            if (dt1.Rows.Count > 0)
            {
                dt = objforgetpass.GetRequestStatus(regno);
                if (dt.Rows.Count > 0)
                {
                    btnrequeststatus.Visible = false;
                    btnchangereq.Visible = false;
                    Button1.Visible = false;
                    TRRequestStatus.Visible = true;
                    DIVrequeststatus.Visible = true;
                    grdrequeststatus.DataSource = dt;
                    grdrequeststatus.DataBind();
                }
                else
                {
                    TRRequestStatus.Visible = false;
                    DIVrequeststatus.Visible = false;
                }
            }
            else
            {
                TRRequestStatus.Visible = false;
                DIVrequeststatus.Visible = false;
                msg.Show("No Request Submitted");
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

