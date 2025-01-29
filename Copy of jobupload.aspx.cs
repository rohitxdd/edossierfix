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

string jobid ="";
string year ="";
string advtno ="";
string no = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tblupload.Visible = false;
        }
    }
        
    
    private void uploadphoto(string appno)
    {
        byte[] imageSize = new byte[photoupload.PostedFile.ContentLength];
        string ip = GetIPAddress();
        try
        {
        int len = photoupload.PostedFile.ContentLength;
        string ctype = photoupload.PostedFile.ContentType;
        string ext = System.IO.Path.GetExtension(photoupload.PostedFile.FileName).ToLower().Trim();
       
        if (len > 0)
        {
             if (ext != ".jpg" || ext != ".jpeg")
             {
            
                byte[] file1 = new byte[len];
                string StrFileName1 = null, StrFileName2 = null;
                photoupload.PostedFile.InputStream.Read(file1, 0, len);

                string StrFileName = photoupload.PostedFile.FileName.Substring(photoupload.PostedFile.FileName.LastIndexOf("\\") + 1);

                string fileext = "";
                if (StrFileName != "")
                {
                    string[] filename = new string[2];
                    filename = StrFileName.Split('.');
                    fileext = filename[1].ToString();
                }
                try
                {
                    bool checkfiletype = chkfiletype(file1, fileext);
                    if (checkfiletype)
                    {
           
                    HttpPostedFile uploadedImage = photoupload.PostedFile;
                    uploadedImage.InputStream.Read(imageSize, 0, (int)photoupload.PostedFile.ContentLength);

                    System.Drawing.Image UpImage = System.Drawing.Image.FromStream(photoupload.PostedFile.InputStream);

                    if (UpImage.PhysicalDimension.Width > 110 && UpImage.PhysicalDimension.Height > 140)
                    {
                        msg.Show("File dimensions are not allowed");
                    }
                    else
                    {
                        double fileSizeKB = imageSize.Length / 1024;
                        if (fileSizeKB > 40)
                        {
                            msg.Show("File Size is greater than 40 KB.");
                        }
                        else
                        {
                            DataTable dt = objcd.checkphoto(appno);
                            int i = 0;
                            if (dt.Rows.Count > 0)
                            {
                                 i = objcd.updatephoto(appno, imageSize, ip, Utility.formatDate(DateTime.Now));

                            }
                            else
                            {
                                 i = objcd.insertphoto(appno, imageSize, ip, Utility.formatDate(DateTime.Now));
                            }
                                if (i > 0)
                                {
                                    img.ImageUrl = "ImgHandler.ashx?id=" + appno + "&type=p ";//i
                                    

                                }
                            }
                        }
      
                }
                else
                {
                    msg.Show("Only pdf & image files can be uploaded");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
        else
        {
            msg.Show("Only JPEG files can be uploaded");
        }
    }
    else
    {
        msg.Show("File not Selected");
    }


        }
        catch(Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }



    public bool chkfiletype(byte[] file, string ext)
    {
        byte[] chkByte = null;
        if (ext == "jpeg" || ext == "jpg")
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
    protected void btnupphoto_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (photoupload.PostedFile.ContentLength > 0)
            {
                no = Session["no"].ToString();
                uploadphoto(no);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void uploadsign(string appno)
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

                    if (UpImage.PhysicalDimension.Width > 140 && UpImage.PhysicalDimension.Height > 110)
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
                            DataTable dt = objcd.checkphoto(appno);
                            int i = 0;
                            if (dt.Rows.Count > 0)
                            {
                                 i = objcd.updatejobappsignature(appno, imageSize, ip, Utility.formatDate(DateTime.Now));
                            }
                            else
                            {
                                 i = objcd.insertsignature(appno, imageSize, ip, Utility.formatDate(DateTime.Now));
                            }
                            if (i > 0)
                            {
                                img2.ImageUrl = "ImgHandler.ashx?appid=" + appno + "&type=s ";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btnuploadsig_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (signatureupload.PostedFile.ContentLength > 0)
            {
                no = Session["no"].ToString(); 
                uploadsign(no);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        CandidateData cd = new CandidateData();

        TextBox vappno = (TextBox)this.appno.FindControl("txtappno");
        string appno = vappno.Text;
        TextBox vdob = (TextBox)this.appno.FindControl("txtdob");
        string dob = vdob.Text;
        //string appno = Session["appno"].ToString();

        if (appno != "" && dob != "")
        {
            string[] str = appno.Split('/');
            jobid = str[0].ToString();
            year = str[1].ToString();
            advtno = str[2].ToString();
            no = str[3].ToString();
            Session["no"] = no;

            if (Validation.chkescape(appno))
            {
                msg.Show("Invalid Character in Temporary Reference No");
            }
            else if (Validation.chkescape(dob))
            {
                msg.Show("Invalid Character in DOB");

            }

            dt = cd.Getappno(jobid, year, Server.HtmlEncode(advtno), no, dob);
            if (dt.Rows.Count == 0)
            {
                msg.Show("Application does not exist.");
                //Session["flg"] = "0";
                tblupload.Visible = false;
            }
            else if (dt.Rows.Count > 0 && dt.Rows[0]["dummy_no"].ToString() != "")
            {
                msg.Show("Your Application is Confirmed.Therefore you can not change your credentials.");
                tblupload.Visible = false;
            }        
            else
            {
                tblupload.Visible = true;
                ddl.SelectedValue = dt.Rows[0]["initial"].ToString().Trim();
                txtsurname.Text = dt.Rows[0]["name"].ToString();
                txtfname.Text = dt.Rows[0]["surname"].ToString();
                txthname.Text = dt.Rows[0]["fname"].ToString();
                txtmname.Text = dt.Rows[0]["mothername"].ToString();

                DataTable dt_no = objcd.checkphoto(no);
                if (dt_no.Rows.Count > 0)
                {
                    img.ImageUrl = "ImgHandler.ashx?appid=" + no + "&type=p ";//i
                    img2.ImageUrl = "ImgHandler.ashx?appid=" + no + "&type=s";
                    //btnupdatephoto.Visible = true;
                    //photoupload.Visible = false;
                    //btnupphoto.Visible = false;

                }
            }
        }
        else
        {
            msg.Show("Temporary No. and DOB can not be blank.");
        }
    }
}
