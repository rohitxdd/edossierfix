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
using System.Web.SessionState;
using System.Drawing.Imaging;


public partial class jobupload : BasePage
{
    message msg = new message();
    CandidateData objcd = new CandidateData();
    DataTable dt = new DataTable();
    DataTable dt_prev = new DataTable();
    MD5Util md5util = new MD5Util();
    string applid = "";
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    string regno = "";
    string url = "";
    string url_lti = "", url_rti = "";
    string reqid, deptcode;
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = "";
        if (Request.QueryString["applid"] == null)
        {
            //applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            //url = "Confirm_app.aspx";
            //url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
            //  url = "Experience.aspx";
        }
        else
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            //url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
            img_btn_next.Visible = true;
            // url = md5util.CreateTamperProofURL("Experience.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
        }
        // img_btn_next.PostBackUrl = url;
        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
        regno = Session["rid"].ToString();
        dt = objcd.get_post(regno);
        if (dt.Rows.Count == 0)
        {
            trvalidate.Visible = false;
            lblmsg.Visible = true;
        }
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();

            tblupload.Visible = false;
            if (Request.QueryString["applid"] != null)
            {
                tblconf.Visible = false;
                Button1_Click(Events, EventArgs.Empty);
            }
            if (Request.QueryString["update"] != null)
            {
                if (MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "P")
                {
                    String Temp_applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                    if (!IsPostBack)
                    {
                        tblconf.Visible = false;
                       // btnexit.Visible = true;
                        img_btn_next.Visible = true;
                        Button1_Click(Events, EventArgs.Empty);

                    }
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


    private void uploadphoto(string applid)
    {
        byte[] imageSize = new byte[photoupload.PostedFile.ContentLength];
        string ip = GetIPAddress();
        try
        {
            if (photoupload.PostedFile != null && photoupload.PostedFile.FileName != "")
            {
                string filename = photoupload.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');

                string ext = System.IO.Path.GetExtension(photoupload.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg")
                {
                    msg.Show("only JPEG and JPG Files are allowed");
                }
                else
                {
                    HttpPostedFile uploadedImage = photoupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)photoupload.PostedFile.ContentLength);

                    System.Drawing.Image UpImage = System.Drawing.Image.FromStream(photoupload.PostedFile.InputStream);

                    //if (UpImage.PhysicalDimension.Width > 110 || UpImage.PhysicalDimension.Height > 140)
                    //{
                    //    msg.Show("File dimensions are not allowed");
                    //}
                    if (UpImage.PhysicalDimension.Width < 480.0 || UpImage.PhysicalDimension.Height < 672.0)
                    {
                        msg.Show("File Dimension of your photograph should be between 480*672 pixel (width*height) and 500*700 pixel (width*height).");
                    }
                    else if (UpImage.PhysicalDimension.Width > 500.0 || UpImage.PhysicalDimension.Height > 700.0)
                    {
                        msg.Show("File Dimension of your photograph should be between 480*672 pixel (width*height) and 500*700 pixel (width*height).");
                    }
                    else
                    {
                        bool checkfiletype = chkfiletype(imageSize, ext);
                        //bool checkfiletype = true;
                        if (checkfiletype)
                        {
                            double fileSizeKB = imageSize.Length / 1024;
                            if (fileSizeKB < 50)
                            {
                                msg.Show("File size cannot be less than 50 KB.");
                            }
                            else if (fileSizeKB > 300)
                            {
                                msg.Show("File size cannot be greater than 300 KB.");
                            }
                            else
                            {
                                DataTable dt = objcd.checkphoto(applid);
                                if (dt.Rows.Count > 0)
                                {
                                    int i = objcd.updatephoto(applid, imageSize, ip, Utility.formatDate(DateTime.Now));
                                }
                                else
                                {
                                    int i = objcd.insertphoto(applid, imageSize, ip, Utility.formatDate(DateTime.Now));

                                }
                                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true));
                                //img.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p ";//i
                                img.ImageUrl = url;


                            }
                        }
                        else
                        {
                            msg.Show("Uploaded File is not a Valid File. Upload a Valid Photo");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.Show("Uploaded File is not a Valid File. Upload a Valid Photo");
        }


    }
    protected void btnupphoto_Click(object sender, EventArgs e)
    {
        string applid = "";
        if (Request.QueryString["update"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            applid = ddlpost.SelectedValue;
        }
        if (Session["intraflag"] != null)
        {
            if (Request.QueryString["applid"] != null)
            {
                applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            }
            else
            {
                DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                applid = ddlpost.SelectedValue;
            }
        }
        try
        {

            if (photoupload.PostedFile.ContentLength > 0)
            {
                uploadphoto(applid);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void uploadsign(string applid)
    {
        byte[] imageSize = new byte[signatureupload.PostedFile.ContentLength];
        string ip = GetIPAddress();
        try
        {
            if (signatureupload.PostedFile != null && signatureupload.PostedFile.FileName != "")
            {
                string filename = signatureupload.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');
                string ext = System.IO.Path.GetExtension(signatureupload.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg")
                {
                    msg.Show("only JPEG Files are allowed");
                }
                else
                {

                    HttpPostedFile uploadedImage = signatureupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)signatureupload.PostedFile.ContentLength);
                    System.Drawing.Image UpImage = System.Drawing.Image.FromStream(signatureupload.PostedFile.InputStream);

                    if (UpImage.PhysicalDimension.Width > 140 || UpImage.PhysicalDimension.Height > 110)
                    {
                        msg.Show("File dimensions are not allowed");
                    }
                    else
                    {
                        double signSizeKB = imageSize.Length / 1024;

                        if (signSizeKB > 20)
                        {
                            msg.Show("Signature Size is greater than 20 KB.");
                        }
                        else
                        {
                            bool checkfiletype = chkfiletype(imageSize, ext);
                            if (checkfiletype)
                            {
                                DataTable dt = objcd.checkphoto(applid);
                                int i = 0;
                                if (dt.Rows.Count > 0)
                                {
                                    i = objcd.updatejobappsignature(applid, imageSize, ip, Utility.formatDate(DateTime.Now));
                                }
                                else
                                {
                                    i = objcd.insertsignature(applid, imageSize, ip, Utility.formatDate(DateTime.Now));
                                }
                                if (i > 0)
                                {
                                    url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true));
                                    //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                                    img2.ImageUrl = url;
                                }
                            }
                            else
                            {
                                msg.Show("Uploaded File is not a Valid File. Upload a Valid Photo");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.Show("Uploaded File is not a Valid File. Upload a Valid Photo");
        }
    }
    protected void btnuploadsig_Click(object sender, EventArgs e)
    {
        string applid = "";
        if (Request.QueryString["update"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            applid = ddlpost.SelectedValue;
        }
        if (Session["intraflag"] != null)
        {
            if (Request.QueryString["applid"] != null)
            {
                applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            }
            else
            {
                DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                applid = ddlpost.SelectedValue;
            }
        }

        try
        {
            if (signatureupload.PostedFile.ContentLength > 0)
            {
                uploadsign(applid);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    public bool chkfiletype(byte[] file, string ext)
    {
        byte[] chkByte = null;
        if (ext == ".jpeg" || ext == ".jpg")
        {
            chkByte = new byte[] { 255, 216, 255, 224 };
        }
        //else if (ext == "pdf")
        //{
        //    chkByte = new byte[] { 37, 80, 68, 70 };
        //}
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        lbl_step.Visible = true;
        CandidateData cd = new CandidateData();
        string applid = "";
        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
        if (Request.QueryString["applid"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }

        if (Request.QueryString["update"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        if (applid == "")
        {
            applid = ddlpost.SelectedValue;
        }
        else
        {
            if (Session["intraflag"] == null)
            {
                ddlpost.SelectedValue = applid;
            }
        }
        //string appno = Session["appno"].ToString();

        if (applid != "")
        {

            dt = cd.Getappno(applid);
            if (dt.Rows.Count == 0)
            {
                msg.Show("Application does not exist.");
                //Session["flg"] = "0";
                tblupload.Visible = false;
            }
            else if (dt.Rows.Count > 0 && dt.Rows[0]["final"].ToString() != "")
            {
                msg.Show("Your Application is Confirmed.Therefore you can not change your credentials.");
                tblupload.Visible = false;
            }
            else
            {
                tblupload.Visible = true;
                lbl_app.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["jobtitle"].ToString()));
                lbl_post_code.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["postcode"].ToString()));
                lbl_advt.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["adno"].ToString()));
                lblname.Text = dt.Rows[0]["name"].ToString();
                lblfname.Text = dt.Rows[0]["fname"].ToString();
                lblmthrname.Text = dt.Rows[0]["mothername"].ToString();
                lbldobr.Text = dt.Rows[0]["dob"].ToString();
                DataTable dt_no = objcd.checkphoto(applid);
                if (dt_no.Rows.Count > 0)
                {
                    url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true));
                    //img.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p ";//i
                    img.ImageUrl = url;
                    url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true));
                    //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                    img2.ImageUrl = url;
                    url_lti = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("l", true));
                    //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                    imgLTI.ImageUrl = url_lti;
                    url_rti = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("r", true));
                    //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                    imgRTI.ImageUrl = url_rti;
                    string temp1 = dt_no.Rows[0]["OLEModule"].ToString();
                    if (temp1 != "0")
                    {
                        btn_upload_s_pic.Visible = false;
                        img_s_pic.Visible = false;
                    }
                    string temp2 = dt_no.Rows[0]["signature"].ToString();
                    if (temp2 != "0")
                    {
                        btn_upload_s_sign.Visible = false;
                        img_s_sign.Visible = false;
                    }
                    string temp3 = dt_no.Rows[0]["LTI"].ToString();
                    if (temp3 != "0")
                    {
                        btn_upload_s_lti.Visible = false;
                        img_s_lti.Visible = false;
                    }
                    string temp4 = dt_no.Rows[0]["RTI"].ToString();
                    if (temp4 != "0")
                    {
                        btn_upload_s_rti.Visible = false;
                        img_s_rti.Visible = false;
                    }
                    if (temp1 != "0" && temp2 != "0" && temp3 != "0" && temp4 != "0")
                    {
                        lbl_pre_msg.Visible = false;
                    }
                }


            }
        }
        else
        {
            msg.Show("Invalid Job selected");
        }
        dt_prev = objcd.get_prev_pic(regno);
        //fill_prev_photo(dt_prev);  //this code commented on 11032021 by RKP to hide option to opt previous photos uploaded
        hidePreviouslyUploadedDocSelectOption();//addeded to fullfill above scenerio
        ///////candidate activity log////////
        string regID = Session["regno"].ToString();
        string ip = GetIPAddress();
        //objcd.InsertIntoCandidateAcivityLog(regID, "True", "Upload Photograph and Signature", ip);
    }
    protected void hidePreviouslyUploadedDocSelectOption()
    {
        lbl_pre_msg.Visible = false;
        img_s_pic.Visible = false;
        img_s_sign.Visible = false;
        img_s_lti.Visible = false;
        img_s_rti.Visible = false;
        btn_upload_s_pic.Visible = false;
        btn_upload_s_sign.Visible = false;
        btn_upload_s_lti.Visible = false;
        btn_upload_s_rti.Visible = false;
    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        if (MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "P")
        {
            String applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            string url = md5util.CreateTamperProofURL("EditApplication.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
            Response.Redirect(url);
        }
    }
    private void fill_prev_photo(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string applid = dt.Rows[0]["applid"].ToString();
            url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true));
            //img_s_pic.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p";
            img_s_pic.ImageUrl = url;
            url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true));
            //img_s_sign.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
            img_s_sign.ImageUrl = url;
            url_lti = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("l", true));
            //img_s_pic.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p";
            img_s_lti.ImageUrl = url_lti;
            url_rti = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("r", true));
            //img_s_pic.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p";
            img_s_rti.ImageUrl = url_rti;
            Session["prev_applid"] = applid;
        }
        else
        {
            lbl_pre_msg.Visible = false;
            img_s_pic.Visible = false;
            img_s_sign.Visible = false;
            img_s_lti.Visible = false;
            img_s_rti.Visible = false;
            btn_upload_s_pic.Visible = false;
            btn_upload_s_sign.Visible = false;
            btn_upload_s_lti.Visible = false;
            btn_upload_s_rti.Visible = false;
        }
    }
    protected void btn_upload_s_pic_Click(object sender, EventArgs e)
    {
        upload_prev_pic_sign("p");
    }
    protected void btn_upload_s_sign_Click(object sender, EventArgs e)
    {
        upload_prev_pic_sign("s");
    }
    private void upload_prev_pic_sign(string type)
    {
        string applid = "";
        string prev_applid = Session["prev_applid"].ToString();
        if (Request.QueryString["update"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            applid = ddlpost.SelectedValue;
        }

        if (Session["intraflag"] != null && applid == "")
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }

        DataTable dt = objcd.checkphoto(applid);
        int i = 0;
        if (type == "p")
        {
            if (dt.Rows.Count > 0)
            {
                i = objcd.update_prev_picsign(prev_applid, applid, "p");
            }
            else
            {
                i = objcd.insert_prev_picsign(prev_applid, applid, "p");
            }
            if (i > 0)
            {
                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true));
                //img.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p";
                img.ImageUrl = url;
            }
        }
        else if (type == "s")
        {
            if (dt.Rows.Count > 0)
            {
                i = objcd.update_prev_picsign(prev_applid, applid, "s");
            }
            else
            {
                i = objcd.insert_prev_picsign(prev_applid, applid, "s");
            }
            if (i > 0)
            {
                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true));
                //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                img2.ImageUrl = url;
            }
        }
        else if (type == "l")
        {
            if (dt.Rows.Count > 0)
            {
                i = objcd.update_prev_picsign(prev_applid, applid, "l");
            }
            else
            {
                i = objcd.insert_prev_picsign(prev_applid, applid, "l");
            }
            if (i > 0)
            {
                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("l", true));
                //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                imgLTI.ImageUrl = url;
            }
        }
        else
        {
            if (dt.Rows.Count > 0)
            {
                i = objcd.update_prev_picsign(prev_applid, applid, "r");
            }
            else
            {
                i = objcd.insert_prev_picsign(prev_applid, applid, "r");
            }
            if (i > 0)
            {
                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("r", true));
                //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                imgRTI.ImageUrl = url;
            }
        }
    }
    protected void img_btn_next_Click(object sender, ImageClickEventArgs e)
    {
        string applid = "";
        if (Request.QueryString["update"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            applid = ddlpost.SelectedValue;
        }

        if (applid != "")
        {
            //****************13/09/2023****************************
            DataTable dtreqid = objcd.Get_fill_combdreqid(applid);
            if (dtreqid.Rows.Count > 0)
            {
                reqid = dtreqid.Rows[0]["reqid"].ToString();
            }
            DataTable dtt = objcd.Getdeptcode(reqid);
            deptcode = dtt.Rows[0]["deptcode"].ToString();
            //******************************************************//
            DataTable dt = objcd.checkphoto(applid);
            if (dt.Rows.Count <= 0)
            {

                msg.Show("Please upload the required documents");


            }
            else
            {

                if (dt.Rows[0]["OLEModule"].ToString() == "0" || dt.Rows[0]["Signature"].ToString() == "0" || dt.Rows[0]["LTI"].ToString() == "0" || dt.Rows[0]["RTI"].ToString() == "0")
                {
                    msg.Show("Please upload the required documents");
                }
                else
                {
                    if (Request.QueryString["applid"] == null)
                    {
                        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                        applid = ddlpost.SelectedValue;
                        if (applid != "")
                        {
                            if (deptcode == "COMBD")
                            {
                                url = md5util.CreateTamperProofURL("CombdEduExp.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                            }
                            else
                            {
                                url = md5util.CreateTamperProofURL("Experience.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                            }
                           // url = md5util.CreateTamperProofURL("Experience.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                        }
                        else
                        {
                            if (deptcode == "COMBD")
                            {
                                url = "CombdEduExp.aspx";
                            }
                            else
                            {
                                url = "Experience.aspx";
                            }
                           // url = "Experience.aspx";
                        }
                    }
                    else
                    {
                        applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                        //url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                        img_btn_next.Visible = true;
                        if (deptcode == "COMBD")
                        {
                            url = md5util.CreateTamperProofURL("CombdEduExp.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                        }
                        else
                        {
                            url = md5util.CreateTamperProofURL("Experience.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                        }
                       // url = md5util.CreateTamperProofURL("Experience.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                    }
                    // img_btn_next.PostBackUrl = url;
                    Response.Redirect(url);
                }
            }
        }
        else
        {
            if (deptcode == "COMBD")
            {
                Response.Redirect("CombdEduExp.aspx");
            }
            else
            {
                Response.Redirect("Experience.aspx");
            }
            //Response.Redirect("Experience.aspx");
        }
    }
    private void uploadLTI(string applid)
    {
        byte[] imageSize = new byte[LTIupload.PostedFile.ContentLength];
        string ip = GetIPAddress();
        try
        {
            if (LTIupload.PostedFile != null && LTIupload.PostedFile.FileName != "")
            {
                string filename = LTIupload.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');

                string ext = System.IO.Path.GetExtension(LTIupload.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg")
                {
                    msg.Show("only JPEG and JPG Files are allowed");
                }
                else
                {
                    HttpPostedFile uploadedImage = LTIupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)LTIupload.PostedFile.ContentLength);

                    System.Drawing.Image UpImage = System.Drawing.Image.FromStream(LTIupload.PostedFile.InputStream);

                    if (UpImage.PhysicalDimension.Width > 110 || UpImage.PhysicalDimension.Height > 140)
                    {
                        msg.Show("File dimensions are not allowed");
                    }
                    else
                    {
                        bool checkfiletype = chkfiletype(imageSize, ext);
                        //bool checkfiletype = true;
                        if (checkfiletype)
                        {
                            double fileSizeKB = imageSize.Length / 1024;
                            if (fileSizeKB > 40)
                            {
                                msg.Show("File Size is greater than 40 KB.");
                            }
                            else
                            {
                                DataTable dt = objcd.checkphoto(applid);
                                if (dt.Rows.Count > 0)
                                {
                                    int i = objcd.updateLTI(applid, imageSize, ip, Utility.formatDate(DateTime.Now));
                                }
                                else
                                {
                                    int i = objcd.insertLTI(applid, imageSize, ip, Utility.formatDate(DateTime.Now));

                                }
                                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("l", true));
                                //img.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p ";//i
                                imgLTI.ImageUrl = url;


                            }
                        }
                        else
                        {
                            msg.Show("Uploaded File is not a Valid File. Upload a Valid Left Thumb Impression");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.Show("Uploaded File is not a Valid File. Upload a Valid Left Thumb Impression");
        }


    }
    protected void btnupLTI_Click(object sender, EventArgs e)
    {
        string applid = "";
        if (Request.QueryString["update"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            applid = ddlpost.SelectedValue;
        }
        if (Session["intraflag"] != null)
        {
            if (Request.QueryString["applid"] != null)
            {
                applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            }
            else
            {
                DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                applid = ddlpost.SelectedValue;
            }
        }
        try
        {

            if (LTIupload.PostedFile.ContentLength > 0)
            {
                uploadLTI(applid);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btn_upload_s_lti_Click(object sender, EventArgs e)
    {
        upload_prev_pic_sign("l");
    }
    private void uploadRTI(string applid)
    {
        byte[] imageSize = new byte[RTIupload.PostedFile.ContentLength];
        string ip = GetIPAddress();
        try
        {
            if (RTIupload.PostedFile != null && RTIupload.PostedFile.FileName != "")
            {
                string filename = RTIupload.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');

                string ext = System.IO.Path.GetExtension(RTIupload.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg")
                {
                    msg.Show("only JPEG and JPG Files are allowed");
                }
                else
                {
                    HttpPostedFile uploadedImage = RTIupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)RTIupload.PostedFile.ContentLength);

                    System.Drawing.Image UpImage = System.Drawing.Image.FromStream(RTIupload.PostedFile.InputStream);

                    if (UpImage.PhysicalDimension.Width > 110 || UpImage.PhysicalDimension.Height > 140)
                    {
                        msg.Show("File dimensions are not allowed");
                    }
                    else
                    {
                        bool checkfiletype = chkfiletype(imageSize, ext);
                        //bool checkfiletype = true;
                        if (checkfiletype)
                        {
                            double fileSizeKB = imageSize.Length / 1024;
                            if (fileSizeKB > 40)
                            {
                                msg.Show("File Size is greater than 40 KB.");
                            }
                            else
                            {
                                DataTable dt = objcd.checkphoto(applid);
                                if (dt.Rows.Count > 0)
                                {
                                    int i = objcd.updateRTI(applid, imageSize, ip, Utility.formatDate(DateTime.Now));
                                }
                                else
                                {
                                    int i = objcd.insertRTI(applid, imageSize, ip, Utility.formatDate(DateTime.Now));

                                }
                                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("r", true));
                                //img.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p ";//i
                                imgRTI.ImageUrl = url;


                            }
                        }
                        else
                        {
                            msg.Show("Uploaded File is not a Valid File. Upload a Valid Left Thumb Impression");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.Show("Uploaded File is not a Valid File. Upload a Valid Left Thumb Impression");
        }


    }
    protected void btnupRTI_Click(object sender, EventArgs e)
    {
        string applid = "";
        if (Request.QueryString["update"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            applid = ddlpost.SelectedValue;
        }
        if (Session["intraflag"] != null)
        {
            if (Request.QueryString["applid"] != null)
            {
                applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            }
            else
            {
                DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                applid = ddlpost.SelectedValue;
            }
        }
        try
        {

            if (RTIupload.PostedFile.ContentLength > 0)
            {
                uploadRTI(applid);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btn_upload_s_rti_Click(object sender, EventArgs e)
    {
        upload_prev_pic_sign("r");
    }
    protected void btnUploadPostCardPhoto_Click(object sender, EventArgs e)
    {
        string applid = "";
        if (Request.QueryString["update"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            applid = ddlpost.SelectedValue;
        }
        if (Session["intraflag"] != null)
        {
            if (Request.QueryString["applid"] != null)
            {
                applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            }
            else
            {
                DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                applid = ddlpost.SelectedValue;
            }
        }
        //try
        //{
        // if (postCardPhotoUpload.HasFile)
        // {
        //   if (postCardPhotoUpload.PostedFile.ContentLength > 0)
        //   {
        //       uploadPostCardPhoto(applid);
        //  }
        //  }
        //  else
        //   {
        //       msg.Show("Please select an image to be uploaded");
        //   }
        // }
        // catch (Exception ex)
        // {
        //     Response.Redirect("ErrorPage.aspx");
        // }
    }
    /*private void uploadPostCardPhoto(string applid)
    {
        byte[] imageSize = new byte[postCardPhotoUpload.PostedFile.ContentLength];
        string ip = GetIPAddress();
        try
        {
            if (postCardPhotoUpload.PostedFile != null && postCardPhotoUpload.PostedFile.FileName != "")
            {
                string filename = postCardPhotoUpload.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');

                string ext = System.IO.Path.GetExtension(postCardPhotoUpload.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg")
                {
                    msg.Show("only JPEG and JPG Files are allowed");
                }
                else
                {
                    HttpPostedFile uploadedImage = postCardPhotoUpload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)postCardPhotoUpload.PostedFile.ContentLength);

                    System.Drawing.Image UpImage = System.Drawing.Image.FromStream(postCardPhotoUpload.PostedFile.InputStream);

                    if (UpImage.PhysicalDimension.Width > 1800 || UpImage.PhysicalDimension.Height > 1200)
                    {
                        msg.Show("File dimensions are not allowed");
                    }
                    else
                    {
                        bool checkfiletype = chkfiletype(imageSize, ext);
                        //bool checkfiletype = true;
                        if (checkfiletype)
                        {
                            double fileSizeKB = imageSize.Length / 1024;
                            if (fileSizeKB > 300)
                            {
                                msg.Show("File Size is greater than 300 KB.");
                            }
                            else
                            {
                                DataTable dt = objcd.checkPostCardPhoto(applid);
                                if (dt.Rows.Count > 0)
                                {
                                    int i = objcd.updatePostCardphoto(applid, imageSize, ip, Utility.formatDate(DateTime.Now));
                                }
                                else
                                {
                                    int i = objcd.insertPostCardphoto(applid, imageSize, ip, Utility.formatDate(DateTime.Now));

                                }
                                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("pc", true));
                                //img.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p ";//i
                                imgPostCard.ImageUrl = url;


                            }
                        }
                        else
                        {
                            msg.Show("Selected File is not a Valid File. Upload a Valid Photo");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //msg.Show("Selected File is not a Valid File. Upload a Valid Photo");
        }


    }*/
    protected void img_btn_prev_Click(object sender, ImageClickEventArgs e)
    {
        string Applid = string.Empty;
        if (Request.QueryString["applid"] != null)
        {
            Applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            Response.Redirect(md5util.CreateTamperProofURL("apply.aspx", null, "update=" + MD5Util.Encrypt("1", true) + "&applid=" + MD5Util.Encrypt(Applid, true)));
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            Applid = ddlpost.SelectedValue;
            if (Applid != "")
            {
                Response.Redirect(md5util.CreateTamperProofURL("apply.aspx", null, "update=" + MD5Util.Encrypt("1", true) + "&applid=" + MD5Util.Encrypt(Applid, true)));
            }
            else
            {
                Response.Redirect("home.aspx");
            }

        }
    }

}
