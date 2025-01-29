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

public partial class ReplaceRecallDoc : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    DataTable dt;
    MD5Util md5Util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            string jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
            FillGrid(applid, jid);
        }
    }

    private void FillGrid(string applid, string jid)
    {
        try
        {
            DataTable dtdoc = new DataTable();
            dtdoc = objcd.GetRecalledDoc(jid, applid);
            if (dtdoc.Rows.Count > 0)
            {
                grdother.DataSource = dtdoc;
                grdother.DataBind();
            }
            else
            {
                msg.Show("No any Recalled Documents");
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    
   

    protected void grdother_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string edid = grdother.DataKeys[e.Row.RowIndex].Values["edid"].ToString();

            HyperLink hyviewdoc = (HyperLink)e.Row.FindControl("hyviewdoc");
            FileUpload fileupload = (FileUpload)e.Row.FindControl("fileupload");
            // LinkButton lbsave = (LinkButton)e.Row.FindControl("lbsave");
            //LinkButton lbchange = (LinkButton)e.Row.FindControl("lbchange");
            Image img = (Image)e.Row.FindControl("img");
            Image img2 = (Image)e.Row.FindControl("img2");
            Label lbladharno = (Label)e.Row.FindControl("lbladharno");
            TextBox txtadharno = (TextBox)e.Row.FindControl("txtadharno");
            Label lbladhar = (Label)e.Row.FindControl("lbladhar");
            string certificateReq = grdother.DataKeys[e.Row.RowIndex].Values["certificateReq"].ToString();
            string url = md5Util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
            hyviewdoc.NavigateUrl = url;

            if (edid != "")
            {
                if (certificateReq == "Photo")
                {
                    hyviewdoc.Visible = false;
                    img.Visible = true;
                    img2.Visible = false;
                    string urlp = md5Util.CreateTamperProofURL("ImgHandlerEdossier.ashx", null, "edid=" + MD5Util.Encrypt(edid.ToString(), true));

                    img.ImageUrl = urlp;
                }
                else if (certificateReq == "Signature")
                {
                    hyviewdoc.Visible = false;
                    img2.Visible = true;
                    img.Visible = false;
                    string urls = md5Util.CreateTamperProofURL("ImgHandlerEdossier.ashx", null, "edid=" + MD5Util.Encrypt(edid.ToString(), true));

                    img2.ImageUrl = urls;
                }
                else if (certificateReq == "Thumb Impression")
                {
                    hyviewdoc.Visible = false;
                    img2.Visible = true;
                    img.Visible = false;
                    string urls = md5Util.CreateTamperProofURL("ImgHandlerEdossier.ashx", null, "edid=" + MD5Util.Encrypt(edid.ToString(), true));
                    img2.ImageUrl = urls;
                }
                else
                {
                    hyviewdoc.Visible = true;
                    img.Visible = false;
                    img2.Visible = false;
                    if (certificateReq == "AdharCard")
                    {
                        lbladharno.Visible = true;
                        txtadharno.Visible = false;
                        lbladhar.Visible = true;
                    }
                    else
                    {
                        lbladharno.Visible = false;
                        txtadharno.Visible = false;
                        lbladhar.Visible = false;
                    }
                }
                fileupload.Visible = true;
                // lbchange.Visible = true;
                // lbsave.Visible = false;

            }
            else
            {
                hyviewdoc.Visible = true;
                fileupload.Visible = true;
                //lbchange.Visible = false;
                //lbsave.Visible = true;
                if (certificateReq == "AdharCard")
                {
                    lbladharno.Visible = false;
                    txtadharno.Visible = true;
                    lbladhar.Visible = true;
                }
                else
                {
                    lbladharno.Visible = false;
                    txtadharno.Visible = false;
                    lbladhar.Visible = false;
                }
            }
        }
    }
    protected void grdother_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string edid = grdother.DataKeys[index].Values["edid"].ToString();
        string certificateReq = grdother.DataKeys[index].Values["certificateReq"].ToString();
        FileUpload fileupload = (FileUpload)grdother.Rows[index].FindControl("fileupload");
        TextBox txtadharno = (TextBox)grdother.Rows[index].FindControl("txtadharno");
        string adharno = "";
        if (certificateReq == "AdharCard")
        {
            if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
            {
                if (txtadharno.Text == "")
                {
                    msg.Show("Please Enter Adhar No.");
                    return;
                }
                else
                {
                    adharno = txtadharno.Text;
                }
            }
            // adharno = ((TextBox)grdother.Rows[index].FindControl("txtadharno")).Text;
        }
        try
        {
            if (certificateReq == "Photo")
            {
                uploadphoto("", fileupload, "U", edid);
            }
            else if (certificateReq == "Signature")
            {
                uploadsign("", fileupload, "U", edid);
            }
            else if (certificateReq == "Thumb Impression")
            {
                uploadthumbimp("", fileupload, "U", edid);
            }
            else
            {
                uploadfile("", fileupload, "U", edid, adharno, "", "");
            }
            grdother.EditIndex = -1;
            string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            string jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
            FillGrid(applid, jid);
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    private void uploadphoto(string edmid, FileUpload photoupload, string function, string edid)
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
                                    int i = objcd.inserteDossier(edmid, imageSize, hfdummy_no.Value, Session["rid"].ToString(), ipaddress, "", "", "","");
                                    if (i > 0)
                                    {

                                        msg.Show("Photo Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objcd.UpdateCandidateEdossier(edid, imageSize, "", ipaddress, Session["rid"].ToString(),"");
                                    if (i > 0)
                                    {
                                        int temp = objcd.updateCandEdossierStatus(edid);
                                        if (temp > 0)
                                        {
                                            msg.Show("Photo Updated Successfully");
                                        }

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

    private void uploadsign(string edmid, FileUpload signatureupload, string function, string edid)
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
                                    int i = objcd.inserteDossier(edmid, imageSize, hfdummy_no.Value, Session["rid"].ToString(), ipaddress, "", "", "","");
                                    if (i > 0)
                                    {

                                        msg.Show("Signature Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objcd.UpdateCandidateEdossier(edid, imageSize, "", ipaddress, Session["rid"].ToString(),"");
                                    if (i > 0)
                                    {
                                        int temp = objcd.updateCandEdossierStatus(edid);
                                        if (temp > 0)
                                        {
                                            msg.Show("Signature Updated Successfully");
                                        }

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

    private void uploadfile(string edmid, FileUpload fileupload, string function, string edid, string adharno, string subcat, string othermiscdoc)
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
                else
                {
                    HttpPostedFile uploadedImage = fileupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)fileupload.PostedFile.ContentLength);
                    //bool checkfiletype = chkfiletype(imageSize, ext);
                    //if (checkfiletype)
                    //{
                    if (function == "I")
                    {
                        int i = objcd.inserteDossier(edmid, imageSize, hfdummy_no.Value, Session["rid"].ToString(), ipaddress, adharno, subcat, othermiscdoc,"");
                        if (i > 0)
                        {

                            msg.Show("Document Inserted Successfully");

                        }
                    }
                    else
                    {
                        int i = objcd.UpdateCandidateEdossier(edid, imageSize, adharno, ipaddress, Session["rid"].ToString(),"");
                        if (i > 0)
                        {
                            int temp = objcd.updateCandEdossierStatus(edid);
                            if (temp > 0)
                            {
                                msg.Show("Document Updated Successfully");
                            }

                        }
                    }
                    // }
                    //else
                    //{
                    //    msg.Show("Only pdf files can be uploaded");
                    //}

                }
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
        if (ext.Equals("pdf", StringComparison.OrdinalIgnoreCase))
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
    private void uploadthumbimp(string edmid, FileUpload signatureupload, string function, string edid)
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
                                    int i = objcd.inserteDossier(edmid, imageSize, hfdummy_no.Value, Session["rid"].ToString(), ipaddress, "", "", "", "");
                                    if (i > 0)
                                    {

                                        msg.Show("Thumb Impression Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objcd.UpdateCandidateEdossier(edid, imageSize, "", ipaddress, Session["rid"].ToString(), "");
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
}