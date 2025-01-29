using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Data.SqlClient;



public partial class insertAdharByCandidate : BasePage
{
    LoginMast ObjMast = new LoginMast();
    message msg = new message();
    Random randObjCode = new Random();
    Int32 UniqueRandomNumber = 0;
    Sms objsms = new Sms();
    Email sendmail = new Email();
    string regno = "";
    byte[] imgfile;
    string contentType = string.Empty;
    HttpPostedFile file;
    int i = 0;
    int k = 0;
    byte[] document = null;
    bool checkFileTypeIDProof = false;
    DataTable dt = new DataTable();
    verifyAadhar vAadhar = new verifyAadhar();
    CandidateData objcand = new CandidateData();
    Dictionary<int, dict> dictn = new Dictionary<int, dict>();
    dict d = null;
    int key = 0;
    DataAccess ds = new DataAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatePostCardPic();// this is called on selection of image thru fileupload control

        if (!IsPostBack)
        {
            try
            {
                DataTable dt = objcand.getLinkEnableDisableStatus("116postCode");
                if (dt.Rows.Count > 0)
                {
                    fillddlIdDoc();
                    getAadharSelection();
                    string linkClick = MD5Util.Decrypt(Request.QueryString["linkClicked"].ToString(), true);

                    if (linkClick == "RD")
                    {
                        getDetailIfDocumentUploaded();
                    }
                    else
                    {
                        getDetailIfPostCardPhotoUploaded();
                    }
                }
                else
                {
                    div2.Visible = false;
                    btnPostCardPic.Visible = false;
                    msg.Show("Last date to upload ID proof and postcard size photograph is over.");
                }


            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }
            //getDetailIfPostCardPhotoUploaded();
        }
    }
    public void getDetailIfDocumentUploaded()
    {
        if (Convert.ToString(Session["rid"]) != "")
        {
            regno = Session["rid"].ToString();
            dt = ObjMast.getDetailIfDocUploaded(regno);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["aadharNo"].ToString() != "")
                {
                    msg.Show("You have already entered your Aadhaar details.");
                }
                else if (dt.Rows[0]["nameOnIDProof"].ToString() != "")
                {
                    string ID = dt.Rows[0]["proofOfID"].ToString();
                    if (ID == "1")
                    {
                        msg.Show("You have already entered your Driving License details.");
                    }
                    else if (ID == "2")
                    {
                        msg.Show("You have already entered your PAN Card details.");
                    }
                    else if (ID == "3")
                    {
                        msg.Show("You have already entered your Passport details.");
                    }
                    else if (ID == "4")
                    {
                        msg.Show("You have already entered your Voter ID Card details.");
                    }
                    else
                    {
                        msg.Show("You have already entered your ID-Proof details.");
                    }
                }
                else
                {
                    msg.Show("Upload the postcard size photograph for selected post code.");
                }
                //divAdvtPCAppNo.Visible = false;
                //divVerify.Visible = false;
                divUploadDoc.Visible = false;
                getDetailIfPostCardPhotoUploaded();
            }
            else
            {
                divUploadDoc.Visible = true;
                //divAdvtPCAppNo.Visible = false;
                //divVerify.Visible = false;
                divuploadPCPhoto.Visible = false;
            }
        }
    }
    public void getDetailIfPostCardPhotoUploaded()
    {
        regno = Session["rid"].ToString();
        dt = ObjMast.getDetailIfPostCardPhotoUploaded(regno);
        if (dt.Rows.Count > 0)
        {
            //divAdvtPCAppNo.Visible = false;
            //divVerify.Visible = false;
            divUploadDoc.Visible = false;
            divuploadPCPhoto.Visible = false;
            //txtAppNo.Text = string.Empty;
            lblShowMsgAdharPcsp.Visible = true;
            lblShowMsgAdharPcsp.Text = "You have already provided your ID proof detail and postcard size photograph for the post applied below.";
            divuploadPCPhoto.Visible = true;
            divFinalSaveHide.Visible = false;
            div1.Visible = false;
            div2.Visible = false;
            DataTable dt1 = ObjMast.getApplidDummyNo(Session["rid"].ToString());
            if (dt1.Rows.Count > 0)
            {
                grdApplidDummyNo.DataSource = dt1;
                grdApplidDummyNo.DataBind();
                grdApplidDummyNo.Columns[3].Visible = false;
            }
        }

        else
        {
            DataTable dt1 = ObjMast.getApplidDummyNo(Session["rid"].ToString());
            if (dt1.Rows.Count > 0)
            {
                grdApplidDummyNo.DataSource = dt1;
                grdApplidDummyNo.DataBind();
                grdApplidDummyNo.Columns[3].Visible = false;
            }
        }
    }
    public void ValidatePostCardPic()
    {
        try
        {
            if (fuPostCard.HasFile)
            {
                contentType = fuPostCard.PostedFile.ContentType;
                file = fuPostCard.PostedFile;
                document = new byte[file.ContentLength];
                file.InputStream.Read(document, 0, (int)file.ContentLength);
                checkFileTypeIDProof = uploadPhotoValidation(fuPostCard, document);
                if (checkFileTypeIDProof)
                {
                    string foldPath = "~/SelectedPostCardPhoto/" + Session["rid"].ToString();
                    ViewState["foldPath"] = foldPath;

                    if (!Directory.Exists(Server.MapPath(foldPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(foldPath));
                        //Directory.Delete(Server.MapPath(foldPath), true);
                    }
                    imgPostCard.Visible = true;
                    string fileName = Session["rid"].ToString() + "_" + System.IO.Path.GetFileName(fuPostCard.PostedFile.FileName);
                    string a = Server.MapPath(foldPath) + "\\" + fileName;
                    fuPostCard.PostedFile.SaveAs(a);
                    ViewState["imagePCSP"] = document;
                    imgPostCard.ImageUrl = foldPath + "\\" + fileName;
                    imgPostCardPhoto.ImageUrl = foldPath + "\\" + fileName;


                    file.InputStream.Close();
                    file.InputStream.Dispose();
                }
                else
                {
                    imgPostCard.Visible = false;
                    //msg.Show("Upload proper image");
                }
            }
            else if (fupIDProofDoc.HasFile)
            {
                contentType = fupIDProofDoc.PostedFile.ContentType;
                file = fupIDProofDoc.PostedFile;
                document = new byte[file.ContentLength];
                Session["image"] = document;
                file.InputStream.Read(document, 0, (int)file.ContentLength);
                checkFileTypeIDProof = uploadPhotoValidation(fupIDProofDoc, document);
                if (checkFileTypeIDProof)
                {
                    string foldPath = "~/SelectedIDProofImage/" + Session["rid"].ToString();
                    ViewState["foldPath"] = foldPath;

                    if (!Directory.Exists(Server.MapPath(foldPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(foldPath));
                        //Directory.Delete(Server.MapPath(foldPath), true);
                    }
                    string fileName = Session["rid"].ToString() + "_" + System.IO.Path.GetFileName(fupIDProofDoc.PostedFile.FileName);
                    string a = Server.MapPath(foldPath) + "\\" + fileName;
                    fupIDProofDoc.PostedFile.SaveAs(a);
                    ViewState["document"] = document;
                    imgPicture.ImageUrl = foldPath + "\\" + fileName;

                    file.InputStream.Close();
                    file.InputStream.Dispose();
                    //document = (byte[])Session["image"];
                    //imgPicture.ImageUrl = "data:image/jpg; base64," + System.Convert.ToBase64String(document);
                }
                else
                {
                    //imgPostCard.Visible = false;
                    msg.Show("Upload proper jpeg image");
                }
            }
            else
            { }
        }
        catch (Exception ex)
        {
            // imgPostCard.Visible = false;
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
    protected void btnSaveAdharNo_Click(object sender, EventArgs e)
    {
        int proofOfID = 0;
        string aadharno = string.Empty;
        ViewState["aadhaarNo"] = string.Empty;
        ViewState["nameAadhaar"] = string.Empty;
        ViewState["nameIdProof"] = string.Empty;
        ViewState["proofofID"] = string.Empty;
        ViewState["pofIDNum"] = string.Empty;
        regno = Session["rid"].ToString();
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
                msg.Show("Please enter valid Aadhar number.");
                return;
            }
        }
        if (ddlIdDoc.SelectedValue != "0")
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
        //if (fupIDProofDoc.HasFile)
        //{
        //    contentType = fupIDProofDoc.PostedFile.ContentType;
        //    file = fupIDProofDoc.PostedFile;
        //    document = new byte[file.ContentLength];
        //    file.InputStream.Read(document, 0, (int)file.ContentLength);
        //    checkFileTypeIDProof = uploadPhotoValidation(fupIDProofDoc, document);
        //}
        if (rbtHaveAdhar.SelectedValue == "No")
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
                                if (ViewState["document"] != null)
                                {
                                    //if (checkFileTypeIDProof)
                                    //{
                                    //i = ObjMast.updateRegDoc_InsertIDProofDoc(regno, aadharno, txtNameAdhar.Text.Trim(), txtNameIDProof.Text.Trim(), proofOfID, pofIDNum, document);
                                    ViewState["aadhaarNo"] = aadharno;
                                    ViewState["nameAadhaar"] = txtNameAdhar.Text.Trim();
                                    ViewState["nameIdProof"] = txtNameIDProof.Text.Trim();
                                    ViewState["proofofID"] = proofOfID;
                                    ViewState["pofIDNum"] = pofIDNum;
                                    //ViewState["document"] = document;
                                    i = 1;
                                    //}
                                    //else
                                    //{
                                    //    msg.Show("Check only jpg/jpeg files can be uploaded.");
                                    //}
                                }
                                else
                                {
                                    msg.Show("Select jpg/image of document to be uploaded.");
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
                    msg.Show("Enter ID Proof number.");
                }
            }
            else
            {
                msg.Show("Select type of ID Proof.");
            }
        }
        else
        {
            if (txtAdharNo.Text.Trim().Replace(" ", "") != "")
            {
                if (txtReAdharNo.Text.Trim().Replace(" ", "") != "")
                {
                    //query to update aadhar number in registration table
                    if (txtNameAdhar.Text.Trim() != "")
                    {
                        if (txtReNameAdhar.Text.Trim() != "")
                        {
                            //i = ObjMast.updateRegDoc_InsertIDProofDoc(regno, aadharno, txtNameAdhar.Text.Trim(), txtNameIDProof.Text.Trim(), proofOfID, pofIDNum, document);
                            ViewState["aadhaarNo"] = aadharno;
                            ViewState["nameAadhaar"] = txtNameAdhar.Text.Trim();
                            ViewState["nameIdProof"] = txtNameIDProof.Text.Trim();
                            ViewState["proofofID"] = proofOfID;
                            ViewState["pofIDNum"] = pofIDNum;
                            //ViewState["document"] = document;
                            i = 1;
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
        if (i > 0)
        {
            txtAdharNo.Text = string.Empty;
            txtNameAdhar.Text = string.Empty;
            ddlIdDoc.SelectedIndex = 0;
            txtIdNumber.Text = string.Empty;
            txtNameAdhar.Text = string.Empty;
            //hide all 3 divs and show UPLOAD POST CARD PHOTO
            //divAdvtPCAppNo.Visible = false;
            //divVerify.Visible = false;
            divUploadDoc.Visible = false;
            divuploadPCPhoto.Visible = true;
            div2.Visible = false;
            getDetailIfPostCardPhotoUploaded();
            //bindLabelWithValues();
        }
        else
        {
            //msg.Show("Failed to Upload Identity Proof Details");
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
            msg.Show("Either enter your aadhar number or provide any type of identity proof.");
        }

    }
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
        if (fuPostCard.HasFile)
        {
            //check below for resolution W*H
            Bitmap bit = new Bitmap(fuPostCard.FileContent);
            var h = bit.Height;
            var w = bit.Width;
            System.Drawing.Image UpImage = System.Drawing.Image.FromStream(fuPostCard.PostedFile.InputStream);

            if (UpImage.PhysicalDimension.Width < 480.0 || UpImage.PhysicalDimension.Height < 672.0)
            {
                msg.Show("File Dimension of your photograph should be between 480*672 pixel (width*height) and 580*772 pixel (width*height).");
                return false;
            }
            else if (UpImage.PhysicalDimension.Width > 580.0 || UpImage.PhysicalDimension.Height > 772.0)
            {
                msg.Show("File Dimension of your photograph should be between 480*672 pixel (width*height) and 580*772 pixel (width*height).");
                return false;
            }
            else
            {
                if (fu.PostedFile.ContentLength < 50000)
                {
                    msg.Show("File size cannot be less than 50 KB.");
                    return false;
                }
                else if (fu.PostedFile.ContentLength > 300000)
                {
                    msg.Show("File size cannot be greater than 300 KB.");
                    return false;
                }
                else
                {
                    ttsize = 1;
                    //ScriptManager.RegisterStartupScript(this.Page, GetType(), "alert", "alert('File of size more than 40KB and less than 60KB can be uploaded')", true);
                    //return true;
                }
            }
        }
        else
        {
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
                    //ScriptManager.RegisterStartupScript(this.Page, GetType(), "alert", "alert('File of size more than 40KB and less than 60KB can be uploaded')", true);
                    //return true;
                }
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
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "alert", "alert('Uploaded File is not a Valid File. Upload a Valid Photo.')", true);
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
    protected void ddlAdvt_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string advtNo = ddlAdvt.SelectedValue;
        //dt = ObjMast.selectPostCodeFromAdvtNo(advtNo);
        //if (dt.Rows.Count > 0)
        //{
        //    ddlPostCode.Items.Clear();
        //    ddlPostCode.DataTextField = "postCodeTitle";
        //    ddlPostCode.DataValueField = "jid";
        //    ddlPostCode.DataSource = dt;
        //    ddlPostCode.DataBind();
        //    ListItem l1 = new ListItem();
        //    l1.Text = "--Select--";
        //    l1.Value = "";
        //    ddlPostCode.Items.Insert(0, l1);
        //    txtAppNo.Text = string.Empty;
        //}
        //else
        //{
        //    msg.Show("No post code for selected advertisement number.");
        //    txtAppNo.Text = string.Empty;
        //}
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        lblShowMsgAdharPcsp.Visible = false;
        //string advtNo = ddlAdvt.SelectedValue;
        //string jid = ddlPostCode.SelectedValue;
        //string applNo = txtAppNo.Text.Trim();
        //string regNO = Session["rid"].ToString();
        ////dt = ObjMast.verifyApplicantFromAdvtNo(jid, applNo, regNO);
        //if (applNo != "")
        //{
        //    dt = ObjMast.verifyApplicantFromAdvtNo(jid, applNo, regNO);
        //    if (dt.Rows.Count > 0)
        //    {
        //        lblAdvtNo.Text = ddlAdvt.SelectedItem.Text;
        //        lblPostCode.Text = ddlPostCode.SelectedItem.Text.Substring(0, 6);
        //        lblPostName.Text = ddlPostCode.SelectedItem.Text.Remove(0, 8);
        //        lblAppNo.Text = applNo;
        //        lblAppName.Text = dt.Rows[0]["name"].ToString().ToUpper();
        //        lblDob.Text = dt.Rows[0]["birthdt"].ToString();
        //        hdnApplid.Value = dt.Rows[0]["applid"].ToString();
        //        divAdvtPCAppNo.Visible = false;
        //        divVerify.Visible = true;
        //        ddlAdvt.SelectedIndex = 0;
        //        ddlPostCode.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        lblAdvtNo.Text = string.Empty;
        //        lblPostCode.Text = string.Empty;
        //        lblPostName.Text = string.Empty;
        //        lblAppNo.Text = string.Empty;
        //        lblAppName.Text = string.Empty;
        //        lblDob.Text = string.Empty;
        //        msg.Show("You have not applied in this post code.");
        //    }
        //}
        //else
        //{
        //    Label33.Text = "Enter Application Number.";
        //}

    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["rid"]) != "")
        {
            regno = Session["rid"].ToString();
            dt = ObjMast.getDetailIfDocUploaded(regno);
            //check if post card photo already uploaded or not then proceed
            string applid = hdnApplid.Value;

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["aadharNo"].ToString() != "")
                {
                    msg.Show("You have already entered your Aadhaar details.");
                }
                else if (dt.Rows[0]["nameOnIDProof"].ToString() != "")
                {
                    string ID = dt.Rows[0]["proofOfID"].ToString();
                    if (ID == "1")
                    {
                        msg.Show("You have already entered your Driving License details.");
                    }
                    else if (ID == "2")
                    {
                        msg.Show("You have already entered your PAN Card details.");
                    }
                    else if (ID == "3")
                    {
                        msg.Show("You have already entered your Passport details.");
                    }
                    else if (ID == "4")
                    {
                        msg.Show("You have already entered your Voter ID Card details.");
                    }
                    else
                    {
                        msg.Show("You have already entered your ID-Proof details.");
                    }
                }
                else
                {
                    msg.Show("Upload the postcard size photograph for selected post code.");
                }
                //hide all 3 divs and show UPLOAD POST CARD PHOTO
                //divAdvtPCAppNo.Visible = false;
                //divVerify.Visible = false;
                divUploadDoc.Visible = false;
                //divuploadPCPhoto.Visible = true;
                //bindLabelWithValues();
            }
            else
            {
                divUploadDoc.Visible = true;
                //divAdvtPCAppNo.Visible = false;
                //divVerify.Visible = false;
                divuploadPCPhoto.Visible = false;
            }

            if (applid != "")
            {
                dt = objcand.checkPostCardPhoto(applid);
                if (dt.Rows.Count > 0)
                {
                    //divAdvtPCAppNo.Visible = true;
                    //divVerify.Visible = false;
                    divUploadDoc.Visible = false;
                    divuploadPCPhoto.Visible = false;
                    //msg.Show("Post card size photograph already uploaded for this application number");
                    //txtAppNo.Text = string.Empty;
                    lblShowMsgAdharPcsp.Visible = true;
                    lblShowMsgAdharPcsp.Text = "You have already provided your ID proof details and postcard size photograph for entered application number.";
                    //Response.Redirect("insertAdharByCandidate.aspx");
                    //i = objcand.updatePostCardphoto(applid, imagePCSP, ipAddress, Utility.formatDate(DateTime.Now));
                }
                else
                {
                    if (divUploadDoc.Visible == true)
                    {
                        divuploadPCPhoto.Visible = false;
                    }
                    else
                    {
                        divuploadPCPhoto.Visible = true;
                        //bindLabelWithValues();
                    }
                }
            }
        }
    }
    public void bindLabelWithValues()
    {
        //Label14.Text = lblAdvtNo.Text;
        //Label16.Text = lblPostCode.Text;
        //Label19.Text = lblPostName.Text;
        //Label21.Text = lblAppNo.Text;
        //Label23.Text = lblAppName.Text;
        //Label26.Text = lblDob.Text;
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
            //txtReAdharNo.Text = string.Empty;
        }
        else
        {
            lblAadharVal.Text = "Invalid Aadhaar number.";
            lblAadharVal.ForeColor = System.Drawing.Color.Red;
            txtAdharNo.Text = string.Empty;
            txtReAdharNo.Text = string.Empty;
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
    protected void btnPostCardPic_Click(object sender, EventArgs e)
    {
        string aadharno = string.Empty;
        string txtNameAdhar = string.Empty;
        string txtNameIDProof = string.Empty;
        string proofOfID = string.Empty;
        string pofIDNum = string.Empty;

        try
        {
            DataTable dt = objcand.getLinkEnableDisableStatus("116postCode");
            if (dt.Rows.Count > 0)
            {
                if (chkUTPCSP.Checked && chkUPCSPA.Checked)
                //if (chkUTPCSP.Checked)
                {
                    if (ViewState["imagePCSP"] != null)
                    {
                        //string appNo = lblAppNo.Text;
                        string regNo = Session["rid"].ToString();
                        //string postCode = lblPostCode.Text;
                        string applid = hdnApplid.Value;
                        var imagePCSP = ViewState["imagePCSP"] as byte[];
                        string foldPath = ViewState["foldPath"].ToString();
                        string ipAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        //ViewState["imagePCSP"] = null;
                        if (ViewState["document"] != null)
                        {
                            document = ViewState["document"] as byte[];
                        }
                        else
                        { document = null; }

                        if (ViewState["aadhaarNo"] != "")
                        { aadharno = ViewState["aadhaarNo"].ToString(); }
                        else { aadharno = ""; }
                        if (ViewState["nameAadhaar"] != "")
                        {
                            txtNameAdhar = ViewState["nameAadhaar"].ToString();
                        }
                        else
                        { txtNameAdhar = ""; }
                        if (ViewState["nameIdProof"] != "")
                        {
                            txtNameIDProof = ViewState["nameIdProof"].ToString();
                        }
                        else
                        {
                            txtNameIDProof = "";
                        }
                        if (ViewState["proofofID"] != "")
                        {
                            proofOfID = ViewState["proofofID"].ToString();
                        }
                        else
                        { proofOfID = ""; }
                        if (ViewState["pofIDNum"] != "")
                        {
                            pofIDNum = ViewState["pofIDNum"].ToString();
                        }
                        else
                        { pofIDNum = ""; }
                        //k = ObjMast.updateRegDoc_InsertIDProofDoc(regNo, aadharno, txtNameAdhar, txtNameIDProof, Convert.ToInt32(proofOfID), pofIDNum, document);
                        //if (k > 0)
                        //{

                        string str = string.Empty;
                        dictn.Clear();
                        for (int j = 0; j < grdApplidDummyNo.Rows.Count; j++)
                        {
                            applid = grdApplidDummyNo.Rows[j].Cells[3].Text;
                            if (applid != "")
                            {
                                //i = objcand.insertPostCardphoto(applid, imagePCSP, ipAddress, Utility.formatDate(DateTime.Now));
                                //str = str + "insert into jobApplicationPostCardPhoto (ApplId, PostCardPhoto, IPAdress, EntryDate) values (" + applid + "," + imagePCSP + ", " + ipAddress + "," + Utility.formatDate(DateTime.Now) + ")";
                                Transaction(applid, imagePCSP, ipAddress, DateTime.Now);
                            }
                        }
                        updateRegDoc_InsertIDProofDoc(regNo, aadharno, txtNameAdhar, txtNameIDProof, Convert.ToInt32(proofOfID), pofIDNum, document);
                        //k = ObjMast.updateRegDoc_InsertIDProofDoc(regNo, aadharno, txtNameAdhar, txtNameIDProof, Convert.ToInt32(proofOfID), pofIDNum, document,str);
                        int i = ds.ExecuteSqlTransaction(dictn);
                        //}

                        //if (i > 0)
                        if (i == dictn.Count + 1)
                        {
                            //divAdvtPCAppNo.Visible = false;
                            //divVerify.Visible = false;
                            divUploadDoc.Visible = false;
                            divuploadPCPhoto.Visible = true;
                            msg.Show("you have successfully updated Id proof details and successfully uploaded postcard size photograph.");
                            lblShowMsgAdharPcsp.Visible = true;
                            lblShowMsgAdharPcsp.Text = "You have successfully updated Id proof details and successfully uploaded postcard size photograph for below mentioned posts.";
                            divFinalSaveHide.Visible = false;
                            div1.Visible = false;
                            objsms.sendmsg(Session["mobNo"].ToString(), "You have successfully updated Id proof details and successfully uploaded postcard size photograph.");
                            sendmail.sendMail(Session["email"].ToString(), "", "", "", "DSSSB registration update", "You have successfully updated Id proof details and successfully uploaded postcard size photograph.", "");

                        }
                        else
                        {
                            k = ObjMast.updateRegDoc_InsertIDProofDoc(regNo, Convert.ToInt32(proofOfID));
                            msg.Show("Failed to upload details, retry to upload.");
                        }
                    }
                    else
                    {
                        msg.Show("Please upload Post Card Size Photograph.");
                    }
                }
                else
                {
                    msg.Show("Please agree with both the above checkbox.");
                }
            }
            else
            {
                msg.Show("Last date to upload ID proof and postcard size photograph is over.");
            }

        }
        catch (ThreadAbortException th)
        {
            Response.Redirect("ErrorPage.aspx");
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "CallMyFunction", "showMCDModal();", true);
    }
    private void UpLoadAndDisplay()
    {
        imgPicture.Visible = true;
        string imgName = fupIDProofDoc.FileName;
        string imgPath = "images/" + imgName;
        int imgSize = fupIDProofDoc.PostedFile.ContentLength;
        if (fupIDProofDoc.PostedFile != null && fupIDProofDoc.PostedFile.FileName != "")
        {
            fupIDProofDoc.SaveAs(Server.MapPath(imgPath));
            imgPicture.ImageUrl = "~/" + imgPath;
            //fupIDProofDoc.PostedFile.InputStream.Read = "~/" + imgPath;
        }
    }

    public void Transaction(string applid, byte[] imagePCSP, string ipAddress, DateTime date)
    {
        //P+1

        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@PostCardPhoto", SqlDbType.Image, imagePCSP.Length);
        param[j].Value = imagePCSP;
        j++;
        param[j] = new SqlParameter("@IPAdress", SqlDbType.VarChar, 20);
        param[j].Value = ipAddress;
        j++;
        param[j] = new SqlParameter("@EntryDate", SqlDbType.DateTime);
        param[j].Value = date;

        d = new dict();
        string str = "insert into jobApplicationPostCardPhoto (ApplId, PostCardPhoto, IPAdress, EntryDate) values (@ApplId,@PostCardPhoto, @IPAdress, @EntryDate)";

        d.QString = str;
        d.param = param;
        dictn.Add(key++, d);


    }

    private void updateRegDoc_InsertIDProofDoc(string rid, string aadharNo, string nameOnAadhar, string nameOnIDProof, int proofOfID, string pofIDNum, byte[] pofIDDoc)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@aadharNo", SqlDbType.NVarChar);
        if (aadharNo != "")
        {
            param[0].Value = aadharNo;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }

        param[1] = new SqlParameter("@nameOnAadhar", SqlDbType.VarChar);
        if (nameOnAadhar != "")
        {
            param[1].Value = nameOnAadhar;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }

        param[2] = new SqlParameter("@nameOnIDProof", SqlDbType.VarChar);
        if (nameOnIDProof != "")
        {
            param[2].Value = nameOnIDProof;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }

        param[3] = new SqlParameter("@proofOfID", SqlDbType.Int);
        if (proofOfID != 0)
        {
            param[3].Value = Convert.ToInt32(proofOfID);
        }
        else
        {
            param[3].Value = System.DBNull.Value;
        }

        param[4] = new SqlParameter("@rid", SqlDbType.VarChar);
        param[4].Value = rid;

        d = new dict();

        string str_update = @"update registration set aadharNo=@aadharNo, nameOnAadhar=@nameOnAadhar,nameOnIDProof=@nameOnIDProof,proofOfID=@proofOfID where rid=@rid";
        //P=2
        d.QString = str_update;
        d.param = param;
        dictn.Add(key++, d);


        if (proofOfID > 0)
        {
            param = new SqlParameter[4];

            param[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
            param[0].Value = rid;
            param[1] = new SqlParameter("@proofOfId", SqlDbType.Int);
            param[1].Value = proofOfID;
            param[2] = new SqlParameter("@proofOfIDNo", SqlDbType.VarChar);
            param[2].Value = pofIDNum;
            param[3] = new SqlParameter("@IDUploaded", SqlDbType.VarBinary);
            param[3].Value = pofIDDoc;

            d = new dict();

            string str_insert = @"insert into proofOfIDUploaded_Reg (regNo,proofOfId,proofOfIDNo,IDUploaded,entryDate) 
                                   values(@regNo,@proofOfId,@proofOfIDNo,@IDUploaded,getdate())";

            d.QString = str_insert;
            d.param = param;
            dictn.Add(key++, d);
            //p+1
        }
    }
}