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
using System.Globalization;

public partial class Edossier_uploaddoc : BasePage
{
    DataTable dt = new DataTable();
    CandidateData objCandD = new CandidateData();
    message msg = new message();
    MD5Util md5util = new MD5Util();
    string jid = "", edid = "", applid = "", rollno = "", post = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
        edid = MD5Util.Decrypt(Request.QueryString["edid"].ToString(), true);
        applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        rollno = MD5Util.Decrypt(Request.QueryString["rollno"].ToString(), true);
        post = MD5Util.Decrypt(Request.QueryString["post"].ToString(), true);
        if (!IsPostBack)
        {
            lblpostcode.Text = post;
            lblrollno.Text = rollno;
            checkforediting();
            if (edid == "")
            {
                btnnext.Visible = false;
            }
            else
            {
                btnnext.Visible = true;
            }
        }
    }
    private void checkforediting()
    {
        string regno = Session["rid"].ToString();

        DataTable dt1 = objCandD.check_post_foruploadeDossier(regno, jid);

        DataTable dtget = objCandD.Getedossiersfinal(applid);
        FillGrid_other();
        if (dt1.Rows.Count > 0)
        {
            if (dtget.Rows.Count > 0)
            {
                if (dtget.Rows[0]["final"].ToString() == "Y")
                {

                    trothdocgrd.Visible = true;
                    trothdocgrd1.Visible = true;

                    grdother.Columns[5].Visible = false;
                  //  grdother.Columns[4].Visible = false;

                }
                else
                {
                    grdother.Columns[4].Visible = true;
                    grdother.Columns[5].Visible = true;
                }

            }

        }
        else
        {
            grdother.Columns[5].Visible = false;
        }



    }
    private void uploadfile(string edmid, FileUpload fileupload, string function, string edid, string adharno, string subcat, string othermiscdoc, string remarks)
    {
        byte[] imageSize = new byte[fileupload.PostedFile.ContentLength];
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
                //1024*1024*2= 2MB almost
                else if (fileupload.PostedFile.ContentLength > 2100000)
                {
                    msg.Show("File Size is greater than 2 MB.");
                }
                else
                {
                    HttpPostedFile uploadedImage = fileupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)fileupload.PostedFile.ContentLength);
                    bool checkfiletype = chkfiletype(imageSize, ext);
                    if (checkfiletype)
                    {
                        if (function == "I")
                        {
                            int i = objCandD.inserteDossier(edmid, imageSize, applid, Session["rid"].ToString(), ipaddress, adharno, subcat, othermiscdoc, remarks);
                            if (i > 0)
                            {

                                msg.Show("Document Inserted Successfully");

                            }
                        }
                        else
                        {
                            int i = objCandD.UpdateCandidateEdossier(edid, imageSize, adharno, ipaddress, Session["rid"].ToString(), remarks);
                            if (i > 0)
                            {

                                msg.Show("Document Updated Successfully");

                            }
                        }
                    }
                    else
                    {
                        msg.Show("Only pdf files can be uploaded");
                    }

                }
            }
        }
        catch (Exception ex)
        {
            //Response.Redirect("ErrorPage.aspx");
        }
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

    public bool chkfiletype_photo(byte[] file, string ext)
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
    private void FillGrid_other()
    {
        try
        {
            DataTable dtdoc1 = new DataTable();
            dtdoc1 = objCandD.GetEdossierMaster(jid, applid, "O");
            if (dtdoc1.Rows.Count > 0)
            {

                grdother.DataSource = dtdoc1;
                grdother.DataBind();
                trothdocgrd.Visible = true;
                trothdocgrd1.Visible = true;
                //trothdocgrd2.Visible = true;
            }
            else
            {
                trothdocgrd.Visible = false;
                trothdocgrd1.Visible = false;
                // trothdocgrd2.Visible = false;
            }

        }
        catch (Exception ex)
        {
            //Response.Redirect("ErrorPage.aspx");
        }

    }
    protected void grdother_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdother.EditIndex = -1;
        FillGrid_other();
    }
    protected void grdother_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Change")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);
            FileUpload fileupload = (FileUpload)grdother.Rows[index].FindControl("fileupload");
            LinkButton lbupdate = (LinkButton)grdother.Rows[index].FindControl("lbupdate");
            HyperLink hyviewdoc = (HyperLink)grdother.Rows[index].FindControl("hyviewdoc");
            LinkButton lbsave = (LinkButton)grdother.Rows[index].FindControl("lbsave");
            LinkButton lbchange = (LinkButton)grdother.Rows[index].FindControl("lbchange");
          //  Label lbladharno = (Label)grdother.Rows[index].FindControl("lbladharno");
          //  TextBox txtadharno = (TextBox)grdother.Rows[index].FindControl("txtadharno");
            string edid = grdother.DataKeys[index].Values["edid"].ToString();
            TextBox remarks = (TextBox)grdother.Rows[index].FindControl("txtboxremarksothervalue");
            remarks.Focus();
            LinkButton lnkcancel = (LinkButton)grdother.Rows[index].FindControl("lnkbtncancel");
            Label lblremarks = (Label)grdother.Rows[index].FindControl("lblremarks");
            string certificateReq = grdother.DataKeys[index].Values["certificateReq"].ToString();

            if (edid != "")
            {
                fileupload.Visible = true;
                lbupdate.Visible = true;
                hyviewdoc.Visible = false;
                lbsave.Visible = false;
                lbchange.Visible = false;
               // lbladharno.Visible = false;
                lblremarks.Visible = false;

                // remarks1.Visible = true;
                lblremarks.Visible = false;

                // remarks1.Visible = true;
                remarks.Visible = true;
                lnkcancel.Visible = true;
                if (lblremarks.Text == "--")
                {
                    lblremarks.Text = "";
                }
                remarks.Text = lblremarks.Text;
                //if (certificateReq == "Adhar Card")
                //{
                //    txtadharno.Visible = true;
                //}
                //else
                //{
                //    txtadharno.Visible = false;
                //}
            }


        }
        if (e.CommandName == "Edit")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);

           FileUpload fileupload = (FileUpload)grdother.Rows[index].FindControl("fileupload");
            string edmid = grdother.DataKeys[index].Values["edmid"].ToString();
            string certificateReq = grdother.DataKeys[index].Values["certificateReq"].ToString();
           // TextBox txtadharno = (TextBox)grdother.Rows[index].FindControl("txtadharno");
            TextBox txtboxremarksothervalue = (TextBox)grdother.Rows[index].FindControl("txtboxremarksothervalue");
         
            if (certificateReq == "Photo")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    msg.Show("Please browse the Photo");
                    return;
                }
                uploadphoto(edmid, fileupload, "I", "", txtboxremarksothervalue.Text);
            }
            else if (certificateReq == "Signature")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    msg.Show("Please browse the Signature");
                    return;
                }
                uploadsign(edmid, fileupload, "I", "", txtboxremarksothervalue.Text);
            }
            else if (certificateReq == "Thumb Impression")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    msg.Show("Please browse the Thumb Impression");
                    return;
                }
                uploadthumbimp(edmid, fileupload, "I", "", txtboxremarksothervalue.Text);
            }
            else
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    msg.Show("Please browse the Document");
                    return;
                }
                uploadfile(edmid, fileupload, "I", "", "", "", "", txtboxremarksothervalue.Text);
            }
            FillGrid_other();
        }
    }
    protected void grdother_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdother.EditIndex = e.NewEditIndex;
        FillGrid_other();
        grdother.EditIndex = -1;
        FillGrid_other();
    }

    protected void grdother_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string edid = grdother.DataKeys[e.Row.RowIndex].Values["edid"].ToString();

            HyperLink hyviewdoc = (HyperLink)e.Row.FindControl("hyviewdoc");
            FileUpload fileupload = (FileUpload)e.Row.FindControl("fileupload");
            LinkButton lbsave = (LinkButton)e.Row.FindControl("lbsave");
            LinkButton lbchange = (LinkButton)e.Row.FindControl("lbchange");
            Image img = (Image)e.Row.FindControl("img");
            Image img2 = (Image)e.Row.FindControl("img2");
           // Label lbladharno = (Label)e.Row.FindControl("lbladharno");
            //TextBox txtadharno = (TextBox)e.Row.FindControl("txtadharno");
            //Label lbladhar = (Label)e.Row.FindControl("lbladhar");
            Label lblmaxsize = (Label)e.Row.FindControl("lb2");
            string certificateReq = grdother.DataKeys[e.Row.RowIndex].Values["certificateReq"].ToString();

            TextBox txtboxremarksothervalue = (TextBox)e.Row.FindControl("txtboxremarksothervalue");
            Label lblremarks = (Label)e.Row.FindControl("lblremarks");

            string url = md5util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
            hyviewdoc.NavigateUrl = url;
            if (certificateReq == "Photo")
            {
                lblmaxsize.Text = "(Photo should be in jpg/jpeg format only and Max size is 40KB,Max width 110px and Max Height 140px)";
            }
            if (certificateReq == "Signature")
            {
                lblmaxsize.Text = "(Signature should be in jpg/jpeg format only and Max size is 20KB,Max width 140px and Max Height 110px)";
            }
            if (certificateReq == "Thumb Impression")
            {
                lblmaxsize.Text = "(Thumb Impression should be in jpg/jpeg format only and Max size is 40KB,Max width 110px and Max Height 140px)";
            }
            else if (certificateReq != "Photo" && certificateReq != "Signature" && certificateReq != "Thumb Impression")
            {
                lblmaxsize.Text = "(Documents should be in PDF format only and Maximum size is 2MB for each document.)";
            }
            if (edid != "")
            {
                if (certificateReq == "Photo")
                {
                    lblmaxsize.Text = "(Photo should be in jpg/jpeg format only and Max size is 40KB,Max width 110px and Max Height 140px)";
                    hyviewdoc.Visible = false;
                    img.Visible = true;
                    img2.Visible = false;
                    string urlp = md5util.CreateTamperProofURL("ImgHandlerEdossier.ashx", null, "edid=" + MD5Util.Encrypt(edid.ToString(), true));

                    img.ImageUrl = urlp;
                }
                else if (certificateReq == "Signature")
                {
                    lblmaxsize.Text = "(Signature should be in jpg/jpeg format only and Max size is 20KB,Max width 140px and Max Height 110px)";
                    hyviewdoc.Visible = false;
                    img2.Visible = true;
                    img.Visible = false;
                    string urls = md5util.CreateTamperProofURL("ImgHandlerEdossier.ashx", null, "edid=" + MD5Util.Encrypt(edid.ToString(), true));

                    img2.ImageUrl = urls;
                }
                else if (certificateReq == "Thumb Impression")
                {
                    lblmaxsize.Text = "(Thumb Impression should be in jpg/jpeg format only and Max size is 40KB,Max width 110px and Max Height 140px)";
                    hyviewdoc.Visible = false;
                    img2.Visible = true;
                    img.Visible = false;
                    string urls = md5util.CreateTamperProofURL("ImgHandlerEdossier.ashx", null, "edid=" + MD5Util.Encrypt(edid.ToString(), true));

                    img2.ImageUrl = urls;
                }

                else
                {
                    hyviewdoc.Visible = true;
                    img.Visible = false;
                    img2.Visible = false;
                    
                }

                fileupload.Visible = false;
                lbchange.Visible = true;
                lbsave.Visible = false;

            }
            else
            {
                hyviewdoc.Visible = false;
                fileupload.Visible = true;
                lbchange.Visible = false;
                lbsave.Visible = true;

                lblremarks.Visible = false;
                txtboxremarksothervalue.Visible = true;

               
            }
        }
    }
    protected void grdother_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string edid = grdother.DataKeys[index].Values["edid"].ToString();
        string certificateReq = grdother.DataKeys[index].Values["certificateReq"].ToString();
        FileUpload fileupload = (FileUpload)grdother.Rows[index].FindControl("fileupload");
      //  TextBox txtadharno = (TextBox)grdother.Rows[index].FindControl("txtadharno");
        TextBox remarks = (TextBox)grdother.Rows[index].FindControl("txtboxremarksothervalue");
      
        try
        {
            if (certificateReq == "Photo")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    remarks.Focus();
                    msg.Show("Please browse the Photo");
                    return;
                }
                uploadphoto("", fileupload, "U", edid, remarks.Text);
            }
            else if (certificateReq == "Signature")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    remarks.Focus();
                    msg.Show("Please browse the Signature");
                    return;
                }
                uploadsign("", fileupload, "U", edid, remarks.Text);
            }
            else if (certificateReq == "Thumb Impression")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    remarks.Focus();
                    msg.Show("Please browse the Thumb Impression");
                    return;
                }
                uploadthumbimp("", fileupload, "U", edid, remarks.Text);
            }

            else
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    remarks.Focus();
                    msg.Show("Please browse the Document");
                    return;
                }
                uploadfile("", fileupload, "U", edid, "", "", "", remarks.Text);
            }
            grdother.EditIndex = -1;
            FillGrid_other();
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    private void uploadphoto(string edmid, FileUpload photoupload, string function, string edid, string remarks)
    {
        byte[] imageSize = new byte[photoupload.PostedFile.ContentLength];
        string ipaddress = GetIPAddress();

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

                    if (UpImage.PhysicalDimension.Width > 110 || UpImage.PhysicalDimension.Height > 140)
                    {
                        msg.Show("File dimensions are not allowed");
                    }
                    else
                    {
                        bool checkfiletype = chkfiletype_photo(imageSize, ext);
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
                                if (function == "I")
                                {
                                    int i = objCandD.inserteDossier(edmid, imageSize, applid, Session["rid"].ToString(), ipaddress, "", "", "", remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Photo Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objCandD.UpdateCandidateEdossier(edid, imageSize, "", ipaddress, Session["rid"].ToString(), remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Photo Updated Successfully");

                                    }
                                }
                                //string  url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true));
                                // //img.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=p ";//i
                                // img.ImageUrl = url;


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

    private void uploadsign(string edmid, FileUpload signatureupload, string function, string edid, string remarks)
    {
        byte[] imageSize = new byte[signatureupload.PostedFile.ContentLength];
        string ipaddress = GetIPAddress();
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
                            bool checkfiletype = chkfiletype_photo(imageSize, ext);
                            if (checkfiletype)
                            {
                                if (function == "I")
                                {
                                    int i = objCandD.inserteDossier(edmid, imageSize, applid, Session["rid"].ToString(), ipaddress, "", "", "", remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Signature Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objCandD.UpdateCandidateEdossier(edid, imageSize, "", ipaddress, Session["rid"].ToString(), remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Signature Updated Successfully");

                                    }
                                }
                                //if (i > 0)
                                //{
                                //    url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true));
                                //    //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                                //    img2.ImageUrl = url;
                                //}
                            }
                            else
                            {
                                msg.Show("Uploaded File is not a Valid File. Upload a Valid Signature");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.Show("Uploaded File is not a Valid File. Upload a Valid Signature");
        }
    }


    private void uploadthumbimp(string edmid, FileUpload signatureupload, string function, string edid, string remarks)
    {
        byte[] imageSize = new byte[signatureupload.PostedFile.ContentLength];
        string ipaddress = GetIPAddress();
        try
        {
            if (signatureupload.PostedFile != null && signatureupload.PostedFile.FileName != "")
            {
                string filename = signatureupload.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');
                string ext = System.IO.Path.GetExtension(signatureupload.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg")
                {
                    msg.Show("only JPEG/JPG Files are allowed");
                }
                else
                {

                    HttpPostedFile uploadedImage = signatureupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)signatureupload.PostedFile.ContentLength);
                    System.Drawing.Image UpImage = System.Drawing.Image.FromStream(signatureupload.PostedFile.InputStream);

                    if (UpImage.PhysicalDimension.Width > 110 || UpImage.PhysicalDimension.Height > 140)
                    {
                        msg.Show("File dimensions are not allowed");
                    }
                    else
                    {
                        double signSizeKB = imageSize.Length / 1024;

                        if (signSizeKB > 40)
                        {
                            msg.Show("Thumb Impression Size is greater than 40 KB.");
                        }
                        else
                        {
                            bool checkfiletype = chkfiletype_photo(imageSize, ext);
                            if (checkfiletype)
                            {
                                if (function == "I")
                                {
                                    int i = objCandD.inserteDossier(edmid, imageSize, applid, Session["rid"].ToString(), ipaddress, "", "", "", remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Thumb Impression Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objCandD.UpdateCandidateEdossier(edid, imageSize, "", ipaddress, Session["rid"].ToString(), remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Thumb Impression Updated Successfully");

                                    }
                                }
                                //if (i > 0)
                                //{
                                //    url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true));
                                //    //img2.ImageUrl = "ImgHandler.ashx?id=" + applid + "&type=s";
                                //    img2.ImageUrl = url;
                                //}
                            }
                            else
                            {
                                msg.Show("Uploaded File is not a Valid File. Upload a Valid Thumb Impression");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.Show("Uploaded File is not a Valid File. Upload a Valid Thumb Impression");
        }
    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        geturl();
    }
    public void geturl()
    {
        DataTable dtcheck = objCandD.getcatsubcatdetailsforedossier(jid, Session["rid"].ToString());
        if (dtcheck.Rows.Count > 0)
        {
            string url = "";
            if (dtcheck.Rows[0]["category"].ToString() == "UR")
            {
                if (dtcheck.Rows[0]["SubCategory"].ToString() == "" )
                {
                    url = md5util.CreateTamperProofURL("Edossier_qualiinfo.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
                    Response.Redirect(url);
                }
                else
                {
                    string subcat = dtcheck.Rows[0]["SubCategory"].ToString();
                    if (subcat.Contains("PH") || subcat.Contains("EX"))
                    {
                        url = md5util.CreateTamperProofURL("Edossier_catinfo.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
                    }
                    else if (subcat == "DC" || subcat == "DGS")
                    {
                        url = md5util.CreateTamperProofURL("EdossierExpDetails.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true) + "&subcatcode=" + MD5Util.Encrypt(subcat, true));
                    }
                    else
                    {
                        url = md5util.CreateTamperProofURL("Edossier_qualiinfo.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
                        Response.Redirect(url);
                    }
                    Response.Redirect(url);
                }
            }
            else
            {
                url = md5util.CreateTamperProofURL("Edossier_catinfo.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
                Response.Redirect(url);
            }
        }
    }
}