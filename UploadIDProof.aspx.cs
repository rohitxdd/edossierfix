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

public partial class UploadIDProof : BasePage
{
    LoginMast ObjMast = new LoginMast();
    message msg = new message();
    string flag = "";
    DataTable dt = new DataTable();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    string SecurityCode;
    string regno = "";
    byte[] imgfile;
    verifyAadhar vAadhar = new verifyAadhar();
    CandidateData objcand = new CandidateData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            string linkClick = MD5Util.Decrypt(Request.QueryString["linkClicked"].ToString(), true);
            if (linkClick == "IDProof")
            {
                //Start Check for ID Proof uploaded or not
                regno = Session["rid"].ToString();
                dt = ObjMast.getDetailIfDocUploaded(regno);
                if (dt.Rows.Count > 0)
                {
                    btnrsubmit.Text = "Proceed to Apply";
                    divSelect.Visible = false;
                    divAadhaar.Visible = false;
                    divIDProofDoc.Visible = false;
                    msg.Show("ID Proof already uploaded.");
                }
                else
                { }
                //End Check for ID Proof uploaded or not
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
            fillddlIdDoc();
            getAadharSelection();
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

    protected void btnrsubmit_Click(object sender, EventArgs e)
    {
        string contentType = string.Empty;
        HttpPostedFile file;
        byte[] document = null;
        bool checkFileTypeIDProof = false;

        try
        {
            int proofOfID = 0;
            int i = 0;
            string aadharno = string.Empty;
            string ip = GetIPAddress();
            string rdate = Utility.formatDatewithtime(DateTime.Now);
            string AdharNum = txtAdharNo.Text.Trim().Replace(" ", "");
            regno = Session["rid"].ToString();
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
            string pofIDNum = string.Empty;
            if (txtIdNumber.Text.Trim() != "")
            {
                pofIDNum = MD5Util.Encrypt(txtIdNumber.Text.Trim().ToUpper(), true);
            }
            else
            {
                pofIDNum = "";
            }
            string namePOID = txtNameIDProof.Text.Trim();
            if (fupIDProofDoc.HasFile)
            {
                contentType = fupIDProofDoc.PostedFile.ContentType;
                file = fupIDProofDoc.PostedFile;
                document = new byte[file.ContentLength];
                file.InputStream.Read(document, 0, (int)file.ContentLength);
                checkFileTypeIDProof = uploadPhotoValidation(fupIDProofDoc, document);
            }
            if (btnrsubmit.Text == "Submit")
            {
                if (rbtHaveAdhar.SelectedValue == "No")
                {
                    if (fupIDProofDoc.HasFile)
                    {
                        if (proofOfID > 0)
                        {
                            if (pofIDNum != "")
                            {
                                if (txtReIdNumber.Text.Trim() != "")
                                {
                                    if (namePOID != "")
                                    {
                                        if (txtReNameIDProof.Text.Trim() != "")
                                        {
                                            if (checkFileTypeIDProof)
                                            {
                                                i = objcand.insert_IDProof_Document(regno, aadharno, txtNameAdhar.Text.Trim(), txtNameIDProof.Text.Trim(), proofOfID, pofIDNum, document);
                                            }
                                            else
                                            {
                                                msg.Show("Failed to upload ID Proof.");
                                            }
                                        }
                                        else
                                        {
                                            msg.Show("Re-enter name as on ID Proof.");
                                            txtReNameIDProof.Focus();
                                        }
                                    }
                                    else
                                    {
                                        msg.Show("Enter name as on ID Proof.");
                                    }
                                }
                                else
                                {
                                    msg.Show("Re-enter ID Proof number.");
                                    txtReIdNumber.Focus();
                                }
                            }
                            else
                            {
                                msg.Show("Please enter ID Proof number.");
                            }
                        }
                        else
                        {
                            msg.Show("Select type of ID Proof.");
                        }
                    }
                    else
                    {
                        msg.Show("Please select jpg/jpeg image document to be uploaded.");
                    }
                }
                else
                {
                    if (txtAdharNo.Text.Trim().Replace(" ", "") != "")
                    {
                        if (txtReAdharNo.Text.Trim().Replace(" ", "") != "")
                        {
                            if (txtNameAdhar.Text.Trim() != "")
                            {
                                if (txtReNameAdhar.Text.Trim() != "")
                                {
                                    i = objcand.insert_IDProof_Document(regno, aadharno, txtNameAdhar.Text.Trim(), txtNameIDProof.Text.Trim(), proofOfID, pofIDNum, document);
                                }
                                else
                                {
                                    msg.Show("Re-enter name as on Aadhaar.");
                                    txtReNameAdhar.Focus();
                                }
                            }
                            else
                            {
                                msg.Show("Enter name as on Aadhaar.");
                            }
                        }
                        else
                        {
                            msg.Show("Re-enter Aadhaar number.");
                            txtReAdharNo.Focus();
                        }
                    }
                    else
                    {
                        msg.Show("Enter Aadhaar number or opt option to upload any other ID proof.");
                    }
                }
            }
            else
            {
                Response.Redirect("AdvtList.aspx");
            }
            if (i > 0)
            {
                txtAdharNo.Text = "";
                txtReAdharNo.Text = "";
                txtNameAdhar.Text = "";
                txtReNameAdhar.Text = "";
                btnrsubmit.Text = "Proceed to Apply";
                divSelect.Visible = false;
                divAadhaar.Visible = false;
                divIDProofDoc.Visible = false;
                msg.Show("Data Saved Successfully.");
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
        else if (UpImage.PhysicalDimension.Width > 480 || UpImage.PhysicalDimension.Height > 320)
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
    public void checkAvailabilityForRegistration(string regno, string mobNo, string email, string aadhar, string proofOfIDNo)
    {
        dt = ObjMast.IsExist_Applicant(regno, mobNo, email, aadhar, proofOfIDNo);
        if (dt.Rows.Count > 0)
        {
            if (aadhar != "")
            {
                txtAdharNo.Text = string.Empty;
                txtReAdharNo.Text = string.Empty;
                msg.Show("This Aadhaar number already exists.");
            }
            else if (proofOfIDNo != "")
            {
                txtIdNumber.Text = string.Empty;
                txtReIdNumber.Text = string.Empty;
                msg.Show("You are already registred with the entered ID Proof.");
            }
            else
            {
                //
            }
        }
        else
        {
            //
        }
    }
}
