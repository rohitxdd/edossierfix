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

public partial class uploadedossier : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    DataTable dt;
    MD5Util md5Util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fill_ddlPost();
        }
    }

    private void fill_ddlPost()
    {
        string regno = Session["rid"].ToString();
        dt = objcd.get_post_eDossier(regno);
        if (dt.Rows.Count > 0)
        {
            DropDownList_post.Items.Clear();
            DropDownList_post.DataTextField = "post";
            DropDownList_post.DataValueField = "jid";
            DropDownList_post.DataSource = dt;
            DropDownList_post.DataBind();
            ListItem l1 = new ListItem();
            l1.Text = "--Select--";
            l1.Value = "";
            DropDownList_post.Items.Insert(0, l1);
            lbl_msg.Visible = false;
        }
        else
        {
            DropDownList_post.Visible = false;
            lblappno.Visible = false;
            btn_submit.Visible = false;
            lbl_msg.Visible = true;
            Session["post"] = "0";
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList_post.SelectedIndex == 0)
            {
                msg.Show("Please select Post Applied");
                return;
            }
            string regno = Session["rid"].ToString();
            string applid = objcd.getapplid(DropDownList_post.SelectedValue, regno);
            hfdummy_no.Value = applid;
            DataTable dtget = objcd.Getedossiersfinal(hfdummy_no.Value);
            if (dtget.Rows.Count > 0)
            {
                if (dtget.Rows[0]["final"].ToString() == "Y")
                {
                    FillGrid();
                    FillGrid_other();
                    FillGrid_Category();
                    DropDownList_post.Enabled = false;
                    btn_submit.Visible = false;
                    trdocgrd.Visible = true;
                    trdocgrd1.Visible = true;
                    trdocgrd2.Visible = true;
                    trothdocgrd.Visible = true;
                    trothdocgrd1.Visible = true;
                    //trothdocgrd2.Visible = true;
                    trcatdoc.Visible = true;
                    trcatdoc1.Visible = true;
                    trcatdoc2.Visible = true;
                    //trnote.Visible = true;
                    FillGrid_SubCategory();
                    //trsubcatdoc.Visible = true;
                    //trsubcatdoc1.Visible = true;
                    //trsubcatdoc2.Visible = true;

                    btnupload.Visible = false;
                    DataTable dtchkmisc = objcd.GetEdossierMaster_miscdoc(DropDownList_post.SelectedValue);
                    if (dtchkmisc.Rows.Count > 0)
                    {
                        trmisc.Visible = true;
                        trmisc1.Visible = true;
                        trmisc2.Visible = true;
                        FillGrid_misc(dtchkmisc.Rows[0]["edmid"].ToString());
                    }
                    grddoc.Columns[5].Visible = false;
                    grdother.Columns[5].Visible = false;
                    grdcat.Columns[5].Visible = false;
                    grdsubcat.Columns[5].Visible = false;
                    grdmisc.Columns[4].Visible = false;
                    btnfinal.Visible = false;
                    trfinalnote.Visible = false;
                    btnreplace.Visible = false;
                }
                else
                {
                    msg.Show("You have not finally submitted the eDossier, you can't view before final submission");
                    return;
                }

            }
            else
            {
                msg.Show("You have not finally submitted the eDossier, you can't view before final submission");
                return;
            }
        }
        catch (Exception ex)
        {
            msg.Show("Something went wrong");
        }

    }

    private void FillGrid()
    {
        try
        {
            DataTable dtdoc = new DataTable();
            string regno = Session["rid"].ToString();
            string applid = objcd.getapplid(DropDownList_post.SelectedValue, regno);
            hfdummy_no.Value = applid;
            dtdoc = objcd.GetEdossierMaster(DropDownList_post.SelectedValue, applid, "E");
            if (dtdoc.Rows.Count > 0)
            {
                grddoc.DataSource = dtdoc;
                grddoc.DataBind();
                trdocgrd.Visible = true;
                trdocgrd1.Visible = true;
                trdocgrd2.Visible = true;
            }
            else
            {
                trdocgrd.Visible = false;
                trdocgrd1.Visible = false;
                trdocgrd2.Visible = false;
            }

        }
        catch (Exception ex)
        {
            //Response.Redirect("ErrorPage.aspx");
        }

    }
    protected void grddoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Change")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);
            FileUpload fileupload = (FileUpload)grddoc.Rows[index].FindControl("fileupload");
            LinkButton lbupdate = (LinkButton)grddoc.Rows[index].FindControl("lbupdate");
            LinkButton lnkcancel = (LinkButton)grddoc.Rows[index].FindControl("lnkbtncancel");
            HyperLink hyviewdoc = (HyperLink)grddoc.Rows[index].FindControl("hyviewdoc");
            LinkButton lbsave = (LinkButton)grddoc.Rows[index].FindControl("lbsave");
            LinkButton lbchange = (LinkButton)grddoc.Rows[index].FindControl("lbchange");
            TextBox remarks = (TextBox)grddoc.Rows[index].FindControl("txtboxremarksvalue");
            Label lblremarks = (Label)grddoc.Rows[index].FindControl("lblremarks");

            remarks.Focus();

            string edid = grddoc.DataKeys[index].Values["edid"].ToString();
            if (edid != "")
            {
                fileupload.Visible = true;
                lbupdate.Visible = true;
                hyviewdoc.Visible = false;
                lbsave.Visible = false;
                lbchange.Visible = false;
                lblremarks.Visible = false;
                lnkcancel.Visible = true;
               // remarks1.Visible = true;
               remarks.Visible = true;
               if (lblremarks.Text == "--")
               {
                   lblremarks.Text = "";
               }
               remarks.Text = lblremarks.Text;

            }
            else
            {
               //remarks.Visible = false;
            }


        }
        if (e.CommandName == "Edit")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);

            string edmid = grddoc.DataKeys[index].Values["edmid"].ToString();
            FileUpload fileupload = (FileUpload)grddoc.Rows[index].FindControl("fileupload");
            TextBox txtboxremarksvalue = (TextBox)grddoc.Rows[index].FindControl("txtboxremarksvalue");
            if (fileupload.PostedFile.ContentLength == 0)
            {
                msg.Show("Please browse the document");
            }

            uploadfile(edmid, fileupload, "I", "", "", "", "", txtboxremarksvalue.Text);
            FillGrid();
        }
      
    }
    protected void grddoc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grddoc.EditIndex = e.NewEditIndex;
        FillGrid();
        //FileUpload fileupload = (FileUpload)grddoc.Rows[e.NewEditIndex].FindControl("fileupload");
        ////TextBox remarks = (TextBox)grddoc.Rows[e.NewEditIndex].FindControl("txtboxremarks");
        //TextBox remarks = (TextBox)grddoc.Rows[e.NewEditIndex].FindControl("txtboxremarksvalue");
        //remarks.Visible = true;
        //string edmid = grddoc.DataKeys[e.NewEditIndex].Values["edmid"].ToString();
        //uploadfile(edmid, fileupload, "I", "", "", "", "", remarks.Text);
        grddoc.EditIndex = -1;
        FillGrid();
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
                            int i = objcd.inserteDossier(edmid, imageSize, hfdummy_no.Value, Session["rid"].ToString(), ipaddress, adharno, subcat, othermiscdoc,remarks);
                            if (i > 0)
                            {

                                msg.Show("Document Inserted Successfully");

                            }
                        }
                        else
                        {
                            int i = objcd.UpdateCandidateEdossier(edid, imageSize, adharno, ipaddress, Session["rid"].ToString(),remarks);
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

    //private bool checkFileSizeDocupload()
    //{
    //    bool flag = true;
    //    if (fileupload.HasFile)
    //    {
    //        if (fileupload.PostedFile.ContentLength > 1048576)
    //        {

    //            flag = false;

    //        }
    //    }
    //    return flag;
    //}


    public bool chkfiletype(byte[] file, string ext)
    {
        byte[] chkByte = null;
        //if (ext.Equals("pdf", StringComparison.OrdinalIgnoreCase))
        //{
        //    chkByte = new byte[] { 37, 80, 68, 70 };
        //}
        //int j = 0;
        //for (int i = 0; i <= 3; i++)
        //{
        //    if (file[i] == chkByte[i])
        //    {
        //        j = j + 1;

        //    }
        //}
        //if (j == 4)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}


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
    protected void grddoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string edid = grddoc.DataKeys[e.Row.RowIndex].Values["edid"].ToString();

                HyperLink hyviewdoc = (HyperLink)e.Row.FindControl("hyviewdoc");
                FileUpload fileupload = (FileUpload)e.Row.FindControl("fileupload");
                LinkButton lbsave = (LinkButton)e.Row.FindControl("lbsave");
                LinkButton lbchange = (LinkButton)e.Row.FindControl("lbchange");
              
                TextBox txtboxremarksvalue = (TextBox)e.Row.FindControl("txtboxremarksvalue");
                Label lblremarks = (Label)e.Row.FindControl("lblremarks");


                string url = md5Util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
                hyviewdoc.NavigateUrl = url;

                if (edid != "")
                {
                    hyviewdoc.Visible = true;
                    fileupload.Visible = false;
                    lbchange.Visible = true;
                    lbsave.Visible = false;
                    // remarks1.Visible = false;
                    //remarks.Visible = false;

                }
                else
                {
                    hyviewdoc.Visible = false;
                    fileupload.Visible = true;
                    lbchange.Visible = false;
                    lbsave.Visible = true;
                    lblremarks.Visible = false;
                    txtboxremarksvalue.Visible = true;
                    //remarks1.Text = "";
                }
            }
            catch (Exception ex)
            {

            }

            }
     
    }


    protected void grddoc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string edid = grddoc.DataKeys[index].Values["edid"].ToString();
        string remarksdatakey = grddoc.DataKeys[index].Values["remarks"].ToString();
        FileUpload fileupload = (FileUpload)grddoc.Rows[index].FindControl("fileupload");
      
        TextBox remarks = (TextBox)grddoc.Rows[index].FindControl("txtboxremarksvalue");
        if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
        {
            try
            {
                uploadfile("", fileupload, "U", edid, "", "", "", remarks.Text);
                grddoc.EditIndex = -1;
                FillGrid();
            }
            catch (Exception ex)
            {
                //Response.Redirect("ErrorPage.aspx");
            }
        }
        else
        {
            remarks.Focus();
            msg.Show("Please browse the file");
        }


    }

    private void FillGrid_other()
    {
        try
        {
            DataTable dtdoc1 = new DataTable();
            string regno = Session["rid"].ToString();
            string applid = objcd.getapplid(DropDownList_post.SelectedValue, regno);
            hfdummy_no.Value = applid;
            dtdoc1 = objcd.GetEdossierMaster(DropDownList_post.SelectedValue, applid, "O");
            if (dtdoc1.Rows.Count > 0)
            {

                grdother.DataSource = dtdoc1;
                grdother.DataBind();
                trothdocgrd.Visible=true;
                trothdocgrd1.Visible=true;
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
            Label lbladharno = (Label)grdother.Rows[index].FindControl("lbladharno");
            TextBox txtadharno = (TextBox)grdother.Rows[index].FindControl("txtadharno");
            string edid = grdother.DataKeys[index].Values["edid"].ToString();
            TextBox remarks = (TextBox)grdother.Rows[index].FindControl("txtboxremarksothervalue");
            remarks.Focus();
            LinkButton lnkcancel = (LinkButton)grdother.Rows[index].FindControl("lnkbtncancel");
            Label lblremarks = (Label)grdother.Rows[index].FindControl("lblremarks");


            if (edid != "")
            {
                fileupload.Visible = true;
                lbupdate.Visible = true;
                hyviewdoc.Visible = false;
                lbsave.Visible = false;
                lbchange.Visible = false;
                txtadharno.Visible = true;
                lbladharno.Visible = false;
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

            }


        }
        if (e.CommandName == "Edit")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);

            //string edmid = grddoc.DataKeys[index].Values["edmid"].ToString();
            //FileUpload fileupload = (FileUpload)grddoc.Rows[index].FindControl("fileupload");
            //TextBox txtboxremarksvalue = (TextBox)grddoc.Rows[index].FindControl("txtboxremarksvalue");
            //if (fileupload.PostedFile.ContentLength == 0)
            //{
            //    msg.Show("Please browse the document");
            //}

            //uploadfile(edmid, fileupload, "I", "", "", "", "", txtboxremarksvalue.Text);
            //FillGrid();
            FileUpload fileupload = (FileUpload)grdother.Rows[index].FindControl("fileupload");
            string edmid = grdother.DataKeys[index].Values["edmid"].ToString();
            string certificateReq = grdother.DataKeys[index].Values["certificateReq"].ToString();
            TextBox txtadharno = (TextBox)grdother.Rows[index].FindControl("txtadharno");
            TextBox txtboxremarksothervalue = (TextBox)grdother.Rows[index].FindControl("txtboxremarksothervalue");
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
            }
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
                uploadfile(edmid, fileupload, "I", "", adharno, "", "", txtboxremarksothervalue.Text);
            }
            FillGrid_other();
        }
    }
    protected void grdother_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdother.EditIndex = e.NewEditIndex;
        FillGrid_other();
        //FileUpload fileupload = (FileUpload)grdother.Rows[e.NewEditIndex].FindControl("fileupload");
        //string edmid = grdother.DataKeys[e.NewEditIndex].Values["edmid"].ToString();
        //string certificateReq = grdother.DataKeys[e.NewEditIndex].Values["certificateReq"].ToString();
        //TextBox txtadharno = (TextBox)grdother.Rows[e.NewEditIndex].FindControl("txtadharno");
        //string adharno = "";
        //if (certificateReq == "AdharCard")
        //{
        //    if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
        //    {
        //        if (txtadharno.Text == "")
        //        {
        //            msg.Show("Please Enter Adhar No.");
        //            return;
        //        }
        //        else
        //        {
        //            adharno = txtadharno.Text;
        //        }
        //    }
        //}
        //if (certificateReq == "Photo")
        //{
        //    if (fileupload.PostedFile.ContentLength == 0)
        //    {
        //        msg.Show("Please browse the Photo");
        //        return;
        //    } 
        //    uploadphoto(edmid, fileupload, "I", "");
        //}
        //else if (certificateReq == "Signature")
        //{
        //    if (fileupload.PostedFile.ContentLength == 0)
        //    {
        //        msg.Show("Please browse the Signature");
        //        return;
        //    } 
        //    uploadsign(edmid, fileupload, "I", "");
        //}
        //else if (certificateReq == "Thumb Impression")
        //{
        //    if (fileupload.PostedFile.ContentLength == 0)
        //    {
        //        msg.Show("Please browse the Thumb Impression");
        //        return;
        //    } 
        //    uploadthumbimp(edmid, fileupload, "I", "");
        //}
        //else
        //{
        //    if (fileupload.PostedFile.ContentLength == 0)
        //    {
        //        msg.Show("Please browse the Document");
        //        return;
        //    } 
        //    uploadfile(edmid, fileupload, "I", "", adharno, "", "","");
        //}

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
            Label lbladharno = (Label)e.Row.FindControl("lbladharno");
            TextBox txtadharno = (TextBox)e.Row.FindControl("txtadharno");
            Label lbladhar = (Label)e.Row.FindControl("lbladhar");
            Label lblmaxsize = (Label)e.Row.FindControl("lb2");
            string certificateReq = grdother.DataKeys[e.Row.RowIndex].Values["certificateReq"].ToString();

            TextBox txtboxremarksothervalue = (TextBox)e.Row.FindControl("txtboxremarksothervalue");
            Label lblremarks = (Label)e.Row.FindControl("lblremarks");

            string url = md5Util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
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
            else if(certificateReq != "Photo" && certificateReq != "Signature" && certificateReq !="Thumb Impression")
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
                    string urlp = md5Util.CreateTamperProofURL("ImgHandlerEdossier.ashx", null, "edid=" + MD5Util.Encrypt(edid.ToString(), true));

                    img.ImageUrl = urlp;
                }
                else if (certificateReq == "Signature")
                {
                    lblmaxsize.Text = "(Signature should be in jpg/jpeg format only and Max size is 20KB,Max width 140px and Max Height 110px)";
                    hyviewdoc.Visible = false;
                    img2.Visible = true;
                    img.Visible = false;
                    string urls = md5Util.CreateTamperProofURL("ImgHandlerEdossier.ashx", null, "edid=" + MD5Util.Encrypt(edid.ToString(), true));

                    img2.ImageUrl = urls;
                }
                else if (certificateReq == "Thumb Impression")
                {
                    lblmaxsize.Text = "(Thumb Impression should be in jpg/jpeg format only and Max size is 40KB,Max width 110px and Max Height 140px)";
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
                    //lblmaxsize.Text = "(Documents should be in PDF format only and Maximum size is 2MB for each document.)";
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
        TextBox remarks = (TextBox)grdother.Rows[index].FindControl("txtboxremarksothervalue");
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
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    remarks.Focus();
                    msg.Show("Please browse the Photo");
                    return;
                } 
                uploadphoto("", fileupload, "U", edid,remarks.Text);
            }
            else if (certificateReq == "Signature")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    remarks.Focus();
                    msg.Show("Please browse the Signature");
                    return;
                } 
                uploadsign("", fileupload, "U", edid,remarks.Text);
            }
            else if (certificateReq == "Thumb Impression")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    remarks.Focus();
                    msg.Show("Please browse the Thumb Impression");
                    return;
                } 
                uploadthumbimp("", fileupload, "U", edid,remarks.Text);
            }

            else
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    remarks.Focus();
                    msg.Show("Please browse the Document");
                    return;
                } 
                uploadfile("", fileupload, "U", edid, adharno, "", "",remarks.Text);
            }
            grdother.EditIndex = -1;
            FillGrid_other();
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    private void uploadphoto(string edmid, FileUpload photoupload, string function, string edid,string remarks)
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
                                    int i = objcd.inserteDossier(edmid, imageSize, hfdummy_no.Value, Session["rid"].ToString(), ipaddress, "", "", "",remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Photo Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objcd.UpdateCandidateEdossier(edid, imageSize, "",ipaddress, Session["rid"].ToString(),remarks);
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

    private void uploadsign(string edmid, FileUpload signatureupload, string function, string edid,string remarks)
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
                                    int i = objcd.inserteDossier(edmid, imageSize, hfdummy_no.Value, Session["rid"].ToString(), ipaddress, "", "", "",remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Signature Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objcd.UpdateCandidateEdossier(edid, imageSize, "", ipaddress, Session["rid"].ToString(),remarks);
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


    private void uploadthumbimp(string edmid, FileUpload signatureupload, string function, string edid,string remarks)
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
                                    int i = objcd.inserteDossier(edmid, imageSize, hfdummy_no.Value, Session["rid"].ToString(), ipaddress, "", "", "",remarks);
                                    if (i > 0)
                                    {

                                        msg.Show("Thumb Impression Inserted Successfully");

                                    }
                                }
                                else
                                {
                                    int i = objcd.UpdateCandidateEdossier(edid, imageSize, "", ipaddress, Session["rid"].ToString(),remarks);
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


    private void FillGrid_Category()
    {
        try
        {
            DataTable dtcat = new DataTable();
            string regno = Session["rid"].ToString();
            string applid = objcd.getapplid(DropDownList_post.SelectedValue, regno);
            hfdummy_no.Value = applid;
            dtcat = objcd.GetEdossierMaster_cat(DropDownList_post.SelectedValue, applid, "C");
            if (dtcat.Rows.Count > 0)
            {
                if (dtcat.Rows[0]["category"].ToString() == "UR Certificate")
                {
                    trcatdoc.Visible = false;
                    trcatdoc1.Visible = false;
                    trcatdoc2.Visible = false;
                }
                else
                {
                    trcatdoc.Visible = true;
                    trcatdoc1.Visible = true;
                    trcatdoc2.Visible = true;
                    grdcat.DataSource = dtcat;
                    grdcat.DataBind();
                }
            }
            else
            {
                trcatdoc.Visible = false;
                trcatdoc1.Visible = false;
                trcatdoc2.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    protected void grdcat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Change")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);
            FileUpload fileupload = (FileUpload)grdcat.Rows[index].FindControl("fileupload");
            LinkButton lbupdate = (LinkButton)grdcat.Rows[index].FindControl("lbupdate");
            HyperLink hyviewdoc = (HyperLink)grdcat.Rows[index].FindControl("hyviewdoc");
            LinkButton lbsave = (LinkButton)grdcat.Rows[index].FindControl("lbsave");
            LinkButton lbchange = (LinkButton)grdcat.Rows[index].FindControl("lbchange");

            string edid = grdcat.DataKeys[index].Values["edid"].ToString();
            TextBox remarks = (TextBox)grdcat.Rows[index].FindControl("txtboxremarksvalue");
            Label lblremarks = (Label)grdcat.Rows[index].FindControl("lblremarks");
            LinkButton lnkcancel = (LinkButton)grdcat.Rows[index].FindControl("lnkbtncancel");
            remarks.Focus();


            if (edid != "")
            {
                fileupload.Visible = true;
                lbupdate.Visible = true;
                hyviewdoc.Visible = false;
                lbsave.Visible = false;
                lbchange.Visible = false;
                lblremarks.Visible = false;
                lnkcancel.Visible = true;
                // remarks1.Visible = true;
                remarks.Visible = true;
                if (lblremarks.Text == "--")
                {
                    lblremarks.Text = "";
                }
                remarks.Text = lblremarks.Text;

            }


        }
        if (e.CommandName == "Edit")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);

            string edmid = grdcat.DataKeys[index].Values["edmid"].ToString();
            FileUpload fileupload = (FileUpload)grdcat.Rows[index].FindControl("fileupload");
            TextBox txtboxremarksvalue = (TextBox)grdcat.Rows[index].FindControl("txtboxremarksvalue");
            if (fileupload.PostedFile.ContentLength == 0)
            {
                msg.Show("Please browse the document");
            }

            uploadfile(edmid, fileupload, "I", "", "", "", "", txtboxremarksvalue.Text);
            FillGrid_Category();
        }
    }
    protected void grdcat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdcat.EditIndex = e.NewEditIndex;
        FillGrid_Category();
        //FileUpload fileupload = (FileUpload)grdcat.Rows[e.NewEditIndex].FindControl("fileupload");
        //string edmid = grdcat.DataKeys[e.NewEditIndex].Values["edmid"].ToString();
        //uploadfile(edmid, fileupload, "I", "", "", "", "","");
        grdcat.EditIndex = -1;
        FillGrid_Category();
    }

    protected void grdcat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string edid = grdcat.DataKeys[e.Row.RowIndex].Values["edid"].ToString();

            HyperLink hyviewdoc = (HyperLink)e.Row.FindControl("hyviewdoc");
            FileUpload fileupload = (FileUpload)e.Row.FindControl("fileupload");
            LinkButton lbsave = (LinkButton)e.Row.FindControl("lbsave");
            LinkButton lbchange = (LinkButton)e.Row.FindControl("lbchange");
            TextBox txtboxremarksvalue = (TextBox)e.Row.FindControl("txtboxremarksvalue");
            Label lblremarks = (Label)e.Row.FindControl("lblremarks");
            string url = md5Util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
            hyviewdoc.NavigateUrl = url;

            if (edid != "")
            {
                hyviewdoc.Visible = true;
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
                txtboxremarksvalue.Visible = true;
            }
        }
    }
    protected void grdcat_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string edid = grdcat.DataKeys[index].Values["edid"].ToString();
        FileUpload fileupload = (FileUpload)grdcat.Rows[index].FindControl("fileupload");
        TextBox remarks = (TextBox)grdcat.Rows[index].FindControl("txtboxremarksvalue");
        if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
        {
            try
            {
                uploadfile("", fileupload, "U", edid, "", "", "", remarks.Text.Trim());
                grdcat.EditIndex = -1;
                FillGrid_Category();
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
        else
        {
            remarks.Focus();
            msg.Show("Please browse the document");
        }


    }

    private void FillGrid_SubCategory()
    {
        try
        {
            DataTable dtsubcat = new DataTable();
            string regno = Session["rid"].ToString();
            string applid = objcd.getapplid(DropDownList_post.SelectedValue, regno);
            hfdummy_no.Value = applid;
            dtsubcat = objcd.GetEdossierMaster_subcat(DropDownList_post.SelectedValue, applid, "S");
            if (dtsubcat.Rows.Count > 0)
            {
                string[] subcat = { "," };
                string[] scVal = dtsubcat.Rows[0]["SubCategory"].ToString().Split(subcat, StringSplitOptions.RemoveEmptyEntries);
                DataTable dtresult = new DataTable();
                dtresult.Columns.Add("edmid", typeof(String));
                dtresult.Columns.Add("jid", typeof(String));
                dtresult.Columns.Add("certificateReq", typeof(String));
                dtresult.Columns.Add("ctype", typeof(String));
                dtresult.Columns.Add("priority", typeof(String));
                dtresult.Columns.Add("final", typeof(String));
                dtresult.Columns.Add("edid", typeof(String));
                dtresult.Columns.Add("ctypename", typeof(String));

                dtresult.Columns.Add("subcategory", typeof(String));
                dtresult.Columns.Add("subcat", typeof(String));
                dtresult.Columns.Add("subcatname", typeof(String));

                dtresult.Columns.Add("remarks", typeof(String));
                DataRow dr;
                //  int j = 0;
                for (int i = 0; i < scVal.Length; i++)
                {
                    string subcategory = scVal[i];
                    string subcatname = objcd.GetSubcategory(scVal[i]);
                    dr = dtresult.NewRow();

                    dr["edmid"] = dtsubcat.Rows[0][0].ToString();
                    dr["jid"] = dtsubcat.Rows[0][1].ToString();
                    dr["certificateReq"] = dtsubcat.Rows[0][2].ToString();
                    dr["ctype"] = dtsubcat.Rows[0][3].ToString();
                    dr["priority"] = dtsubcat.Rows[0][4].ToString();
                    dr["final"] = dtsubcat.Rows[0][5].ToString();

                    dr["ctypename"] = dtsubcat.Rows[0][7].ToString();

                    dr["subcategory"] = subcategory;
                    dr["subcatname"] = subcatname;
                    dr["remarks"] = dtsubcat.Rows[0][10].ToString();
                    for (int j = i; j < dtsubcat.Rows.Count; j++)
                    {

                        if (subcategory == dtsubcat.Rows[j][9].ToString())
                        {
                            dr["edid"] = dtsubcat.Rows[j][6].ToString();
                            dr["subcat"] = dtsubcat.Rows[j][9].ToString();
                            
                            // j++;

                        }
                    }
                    if (dtsubcat.Rows[0][9].ToString() == "")
                    {
                        //dr["edid"] = "";
                    }

                    dtresult.Rows.Add(dr);
                }

                if (dtresult.Rows.Count > 0)
                {
                    trsubcatdoc.Visible = true;
                    trsubcatdoc1.Visible = true;
                    trsubcatdoc2.Visible = true;
                    grdsubcat.DataSource = dtresult;
                    grdsubcat.DataBind();
                }
                else
                {
                    trsubcatdoc.Visible = false;
                    trsubcatdoc1.Visible = false;
                    trsubcatdoc2.Visible = false;
                }
            }
            else
            {
                trsubcatdoc.Visible = false;
                trsubcatdoc1.Visible = false;
                trsubcatdoc2.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    protected void grdsubcat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Change")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);
            FileUpload fileupload = (FileUpload)grdsubcat.Rows[index].FindControl("fileupload");
            LinkButton lbupdate = (LinkButton)grdsubcat.Rows[index].FindControl("lbupdate");
            HyperLink hyviewdoc = (HyperLink)grdsubcat.Rows[index].FindControl("hyviewdoc");
            LinkButton lbsave = (LinkButton)grdsubcat.Rows[index].FindControl("lbsave");
            LinkButton lbchange = (LinkButton)grdsubcat.Rows[index].FindControl("lbchange");

            string edid = grdsubcat.DataKeys[index].Values["edid"].ToString();
            TextBox remarks = (TextBox)grdsubcat.Rows[index].FindControl("txtboxremarksvalue");
            Label lblremarks = (Label)grdsubcat.Rows[index].FindControl("lblremarks");
            LinkButton lnkcancel = (LinkButton)grdsubcat.Rows[index].FindControl("lnkbtncancel");
            remarks.Focus();


            if (edid != "")
            {
                fileupload.Visible = true;
                lbupdate.Visible = true;
                hyviewdoc.Visible = false;
                lbsave.Visible = false;
                lbchange.Visible = false;
                lblremarks.Visible = false;
                lnkcancel.Visible = true;
                // remarks1.Visible = true;
                remarks.Visible = true;
                if (lblremarks.Text == "--")
                {
                    lblremarks.Text = "";
                }
                remarks.Text = lblremarks.Text;

            }


        }
        if (e.CommandName == "Edit")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);

            string edmid = grdsubcat.DataKeys[index].Values["edmid"].ToString();
            string subcategory = grdsubcat.DataKeys[index].Values["subcategory"].ToString();
            FileUpload fileupload = (FileUpload)grdsubcat.Rows[index].FindControl("fileupload");
            TextBox txtboxremarksvalue = (TextBox)grdsubcat.Rows[index].FindControl("txtboxremarksvalue");
            if (fileupload.PostedFile.ContentLength == 0)
            {
                msg.Show("Please browse the document");
            }
            uploadfile(edmid, fileupload, "I", "", "", subcategory, "", txtboxremarksvalue.Text);
            FillGrid_SubCategory();
        }
    }
    protected void grdsubcat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdsubcat.EditIndex = e.NewEditIndex;
        FillGrid_SubCategory();
        //FileUpload fileupload = (FileUpload)grdsubcat.Rows[e.NewEditIndex].FindControl("fileupload");
        //string edmid = grdsubcat.DataKeys[e.NewEditIndex].Values["edmid"].ToString();
        //string subcategory = grdsubcat.DataKeys[e.NewEditIndex].Values["subcategory"].ToString();
        //uploadfile(edmid, fileupload, "I", "", "", subcategory, "","");
        grdsubcat.EditIndex = -1;
        FillGrid_SubCategory();
    }

    protected void grdsubcat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string edid = grdsubcat.DataKeys[e.Row.RowIndex].Values["edid"].ToString();

            HyperLink hyviewdoc = (HyperLink)e.Row.FindControl("hyviewdoc");
            FileUpload fileupload = (FileUpload)e.Row.FindControl("fileupload");
            LinkButton lbsave = (LinkButton)e.Row.FindControl("lbsave");
            LinkButton lbchange = (LinkButton)e.Row.FindControl("lbchange");
            TextBox txtboxremarksvalue = (TextBox)e.Row.FindControl("txtboxremarksvalue");
            Label lblremarks = (Label)e.Row.FindControl("lblremarks");
            string url = md5Util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
            hyviewdoc.NavigateUrl = url;

            if (edid != "")
            {
                hyviewdoc.Visible = true;
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
                txtboxremarksvalue.Visible = true;
            }
        }
    }
    protected void grdsubcat_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string edid = grdsubcat.DataKeys[index].Values["edid"].ToString();
        FileUpload fileupload = (FileUpload)grdsubcat.Rows[index].FindControl("fileupload");
        string subcategory = grdsubcat.DataKeys[index].Values["subcategory"].ToString();
        TextBox remarks = (TextBox)grdsubcat.Rows[index].FindControl("txtboxremarksvalue");
        if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
        {
            try
            {
                uploadfile("", fileupload, "U", edid, "", subcategory, "", remarks.Text.Trim());
                grdsubcat.EditIndex = -1;
                FillGrid_SubCategory();
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
        else
        {
            remarks.Focus();
            msg.Show("Please browse the document");
        }

    }

    private void FillGrid_misc(string edmid)
    {
        try
        {
            DataTable dtmisc = new DataTable();
            string regno = Session["rid"].ToString();
            string applid = objcd.getapplid(DropDownList_post.SelectedValue, regno);
            hfdummy_no.Value = applid;
            dtmisc = objcd.GetEdossierMaster_misc(edmid, applid);
            if (dtmisc.Rows.Count > 0)
            {
                grdmisc.DataSource = dtmisc;
                grdmisc.DataBind();
                trmisc.Visible = true;
                trmisc1.Visible = true;
                trmisc2.Visible = true;
            }
            else
            {
                grdmisc.DataSource = dtmisc;
                grdmisc.DataBind();
                trmisc.Visible = true;
                trmisc1.Visible = true;
                trmisc2.Visible = true;
            }


        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    protected void grdmisc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtchkmisc = objcd.GetEdossierMaster_miscdoc(DropDownList_post.SelectedValue);
        string edmid = "";
        if (dtchkmisc.Rows.Count > 0)
        {
            edmid = dtchkmisc.Rows[0]["edmid"].ToString();

        }
        if (e.CommandName == "Add")
        {
            TextBox txtcertificateReq = (TextBox)(grdmisc.FooterRow.FindControl("txtcertificateReq"));
            txtcertificateReq.Visible = true;
            txtcertificateReq.Focus();
            FileUpload fileupload = (FileUpload)(grdmisc.FooterRow.FindControl("fileupload"));
            fileupload.Visible = true;
            LinkButton lnkAdd = (LinkButton)(grdmisc.FooterRow.FindControl("lnkadd"));
            lnkAdd.Visible = false;
            LinkButton lnkIn = (LinkButton)(grdmisc.FooterRow.FindControl("lnkIn"));
            lnkIn.Visible = true;
            LinkButton lnkC = (LinkButton)(grdmisc.FooterRow.FindControl("lnkC"));
            lnkC.Visible = true;
            TextBox remarks = (TextBox)(grdmisc.FooterRow.FindControl("txtboxremarksvalue"));
            remarks.Visible = true;
        }
        else if (e.CommandName == "EInsert")
        {
            TextBox txtcertificateReq = (TextBox)(grdmisc.Controls[0].Controls[0].FindControl("txtcertificateReq"));
            TextBox txtremarks = (TextBox)(grdmisc.Controls[0].Controls[0].FindControl("txtboxremarksvalue"));
            FileUpload fileupload = (FileUpload)grdmisc.Controls[0].Controls[0].FindControl("fileupload");
            TextBox remarks = (TextBox)(grdmisc.Controls[0].Controls[0].FindControl("txtboxremarksvalue"));

            if (txtcertificateReq.Text == "")
            {
                msg.Show("Field can't be left blank");
            }

            else if ((Validation.chkLevel7(txtcertificateReq.Text)))
            {
                msg.Show("Invalid Character in Certificate Req");
            }


            else
            {
                uploadfile(edmid, fileupload, "I", "", "", "", txtcertificateReq.Text,txtremarks.Text.Trim());

                FillGrid_misc(edmid);
                msg.Show("Record saved successfully");

            }

        }
        else if (e.CommandName == "Insert")
        {
            TextBox txtcertificateReq = (TextBox)(grdmisc.FooterRow.FindControl("txtcertificateReq"));
            FileUpload fileupload = (FileUpload)grdmisc.FooterRow.FindControl("fileupload");
            TextBox txtremarks = (TextBox)(grdmisc.FooterRow.FindControl("txtboxremarksvalue"));
           

            if (txtcertificateReq.Text == "")
            {
                msg.Show("Field can't be left blank");
            }

            else if ((Validation.chkLevel7(txtcertificateReq.Text)))
            {
                msg.Show("Invalid Character in Certificate Req");
            }


            else
            {
                uploadfile(edmid, fileupload, "I", "", "", "", txtcertificateReq.Text,txtremarks.Text.Trim());

                FillGrid_misc(edmid);
                msg.Show("Record saved successfully");

            }

        }
        else if (e.CommandName == "Change")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);
            FileUpload fileupload = (FileUpload)grdmisc.Rows[index].FindControl("fileupload");
            LinkButton lbupdate = (LinkButton)grdmisc.Rows[index].FindControl("lbupdate");
            HyperLink hyviewdoc = (HyperLink)grdmisc.Rows[index].FindControl("hyviewdoc");
            //LinkButton lbsave = (LinkButton)grdmisc.Rows[index].FindControl("lbsave");
            LinkButton lbchange = (LinkButton)grdmisc.Rows[index].FindControl("lbchange");
            LinkButton lbremove = (LinkButton)grdmisc.Rows[index].FindControl("lbremove");
            string edid = grdmisc.DataKeys[index].Values["edid"].ToString();
            TextBox txtremarks = (TextBox)grdmisc.Rows[index].FindControl("txtboxremarksvalue");
            Label lblremarks = (Label)grdmisc.Rows[index].FindControl("lblremarks");
            LinkButton lnkcancel = (LinkButton)grdmisc.Rows[index].FindControl("lnkbtncancel");
            txtremarks.Focus();

            if (edid != "")
            {
                fileupload.Visible = true;
                lbupdate.Visible = true;
                hyviewdoc.Visible = false;
                //  lbsave.Visible = false;
                lbchange.Visible = false;
                lbremove.Visible = false;
                lblremarks.Visible = false;

                // remarks1.Visible = true;
                txtremarks.Visible = true;
                lnkcancel.Visible = true;
                if (lblremarks.Text == "--")
                {
                    lblremarks.Text = "";
                }
                txtremarks.Text = lblremarks.Text;

            }


        }
    }

    protected void grdmisc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string edid = grdmisc.DataKeys[e.Row.RowIndex].Values["edid"].ToString();
            string editflag = grdmisc.DataKeys[e.Row.RowIndex].Values["editflag"].ToString();

            HyperLink hyviewdoc = (HyperLink)e.Row.FindControl("hyviewdoc");
            FileUpload fileupload = (FileUpload)e.Row.FindControl("fileupload");
            //   LinkButton lbsave = (LinkButton)e.Row.FindControl("lbsave");
            LinkButton lbchange = (LinkButton)e.Row.FindControl("lbchange");
            LinkButton lbremove = (LinkButton)e.Row.FindControl("lbremove");
            string url = md5Util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
            hyviewdoc.NavigateUrl = url;

            if (edid != "")
            {
                hyviewdoc.Visible = true;
                fileupload.Visible = false;
                lbchange.Visible = true;
                lbremove.Visible = true;
                // lbsave.Visible = false;
            }
            else
            {
                hyviewdoc.Visible = false;
                fileupload.Visible = true;
                lbchange.Visible = false;
                // lbsave.Visible = true;
                lbremove.Visible = false;
            }
            if (editflag == "Y")
            {
                lbchange.Visible = false;
                lbremove.Visible = false;
            }
            else
            {
                lbchange.Visible = true;
                lbremove.Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            LinkButton lnkadd = (LinkButton)e.Row.FindControl("lnkadd");
            DataTable dtchkmisc = objcd.GetEdossierMaster_miscdoc(DropDownList_post.SelectedValue);
            string edmid = "";
            if (dtchkmisc.Rows.Count > 0)
            {
                edmid = dtchkmisc.Rows[0]["edmid"].ToString();

            }
            DataTable dtmisc = objcd.GetEdossierMaster_misc(edmid, hfdummy_no.Value);
            if (dtmisc.Rows.Count < 7)
            {
                lnkadd.Visible = true;
            }
            else
            {
                lnkadd.Visible = false;
            }


        }
    }

    protected void grdmisc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataTable dtchkmisc = objcd.GetEdossierMaster_miscdoc(DropDownList_post.SelectedValue);
        string edmid = "";
        if (dtchkmisc.Rows.Count > 0)
        {
            edmid = dtchkmisc.Rows[0]["edmid"].ToString();

        }
        grdmisc.EditIndex = -1;
        FillGrid_misc(edmid);
    }

    protected void grdmisc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string edid = grdmisc.DataKeys[index].Values["edid"].ToString();
        string edmid = grdmisc.DataKeys[index].Values["edmid"].ToString();
        FileUpload fileupload = (FileUpload)grdmisc.Rows[index].FindControl("fileupload");
        TextBox txtremarks = (TextBox)grdmisc.Rows[index].FindControl("txtboxremarksvalue");
        if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
        {
            try
            {
                uploadfile("", fileupload, "U", edid, "", "", "", txtremarks.Text.Trim());
                grdmisc.EditIndex = -1;
                FillGrid_misc(edmid);
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
        else
        {
            txtremarks.Focus();
            msg.Show("Please browse the file");
        }

    }
    protected void grdmisc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        string edid = grdmisc.DataKeys[index].Values["edid"].ToString();
        string edmid = grdmisc.DataKeys[index].Values["edmid"].ToString();
        try
        {
            int temp = objcd.delete_candidateedossier(edid);
            if (temp > 0)
            {
                grdmisc.EditIndex = -1;
                FillGrid_misc(edmid);
                msg.Show("Record Deleted successfully");


            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btnfinal_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtcheck = objcd.CheckEdossierforfinal(DropDownList_post.SelectedValue, hfdummy_no.Value);
           
            if (grdcat.Rows.Count > 0)
            {
                if (grdcat.DataKeys[0].Values["edid"].ToString() == "")
                {
                    msg.Show("Please upload applicable document related to category");
                    return;
                }

            }
            if (grdsubcat.Rows.Count > 0)
            {
                for (int a = 0; a < grdsubcat.Rows.Count; a++)
                {
                    if (grdsubcat.DataKeys[a].Values["edid"].ToString() == "")
                    {
                        msg.Show("Please upload applicable document related to subcategory");
                        return;
                    }
                }
            }

            if (dtcheck.Rows.Count > 0)
            {
                msg.Show("Please upload all the mandatory documents before Final Submit");
                return;

            }
            else
            {
                string ipaddress = GetIPAddress();
                int temp = objcd.inserteDossierFinal(hfdummy_no.Value, "Y", Session["rid"].ToString(), ipaddress);
                if (temp > 0)
                {
                    msg.Show("eDossiers Finally Submitted");
                }

            }
            checkforediting();
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }

    private void checkforediting()
    {
        DataTable dtget = objcd.Getedossiersfinal(hfdummy_no.Value);
        if (dtget.Rows.Count > 0)
        {
            if (dtget.Rows[0]["final"].ToString() == "Y")
            {
                grddoc.Columns[4].Visible = false;
                grdother.Columns[4].Visible = false;
                grdcat.Columns[4].Visible = false;
                grdsubcat.Columns[4].Visible = false;
                grdmisc.Columns[3].Visible = false;
                btnfinal.Visible = false;
                trfinalnote.Visible = false;
            }
            else
            {
                grddoc.Columns[4].Visible = true;
                grdother.Columns[4].Visible = true;
                grdcat.Columns[4].Visible = true;
                grdsubcat.Columns[4].Visible = true;
                grdmisc.Columns[3].Visible = true;
                btnfinal.Visible = true;
                trfinalnote.Visible = true;
            }
        }
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList_post.SelectedIndex == 0)
            {
                msg.Show("Please select Post Applied");
                return;
            }
            if (DropDownList_post.SelectedValue != "")
            {
                string regno = Session["rid"].ToString();
                DataTable dt1 = objcd.check_post_foruploadeDossier(regno, DropDownList_post.SelectedValue);

                string applid = objcd.getapplid(DropDownList_post.SelectedValue, regno);
                hfdummy_no.Value = applid;
                DataTable dtget = objcd.Getedossiersfinal(hfdummy_no.Value);
                DataTable dtchkmisc = objcd.GetEdossierMaster_miscdoc(DropDownList_post.SelectedValue);
                //if (dt1.Rows.Count > 0 && dtget.Rows.Count == 0)
                if (dt1.Rows.Count > 0)
                {
                    if (dtget.Rows.Count == 0)
                    {
                        FillGrid();
                        FillGrid_other();
                        FillGrid_Category();
                        DropDownList_post.Enabled = false;
                        btn_submit.Visible = false;
                        trdocgrd.Visible = true;
                        //trdocgrd1.Visible = true;
                        //trdocgrd2.Visible = true;
                        trothdocgrd.Visible = true;
                        //trothdocgrd1.Visible = true;
                        //trothdocgrd2.Visible = true;
                        trcatdoc.Visible = true;
                        //trcatdoc1.Visible = true;
                        //trcatdoc2.Visible = true;
                        //trnote.Visible = true;
                        FillGrid_SubCategory();
                        trsubcatdoc.Visible = true;
                        //trsubcatdoc1.Visible = true;
                        //trsubcatdoc2.Visible = true;
                        btnfinal.Visible = true;
                        trfinalnote.Visible = true;
                        // lbl_msg.Visible = false;
                        btnupload.Visible = false;
                        btnreplace.Visible = false;
                       // DataTable dtchkmisc = objcd.GetEdossierMaster_miscdoc(DropDownList_post.SelectedValue);
                        if (dtchkmisc.Rows.Count > 0)
                        {
                            trmisc.Visible = true;
                            trmisc1.Visible = true;
                            trmisc2.Visible = true;
                            FillGrid_misc(dtchkmisc.Rows[0]["edmid"].ToString());
                        }
                        else
                        {
                            //trmisc.Visible = false;
                            trmisc.Visible = true;
                            //trmisc1.Visible = false;
                            trmisc1.Visible = true;
                            //trmisc2.Visible = false;
                            trmisc2.Visible = true;
                        }
                        checkforediting();
                    }
                    else
                    {                        
                        if (dtchkmisc.Rows.Count > 0)
                        {
                            trmisc.Visible = true;
                            trmisc1.Visible = true;
                            trmisc2.Visible = true;
                            FillGrid_misc(dtchkmisc.Rows[0]["edmid"].ToString());
                        }
                        else
                        {
                            //trmisc.Visible = false;
                            trmisc.Visible = true;
                            //trmisc1.Visible = false;
                            trmisc1.Visible = true;
                            //trmisc2.Visible = false;
                            trmisc2.Visible = true;
                        }
                    }
                }
                else
                {
                    trmisc.Visible = false;
                    trmisc1.Visible = false;
                    trmisc2.Visible = false;
                    msg.Show("Either the Last Date for eDossier Submission is already over or you have done final submission. Please click on View to see the finally submitted eDossier");
                    return;
                    // lbl_msg.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            msg.Show("Something went wrong");
        }
    }
    protected void btnreplace_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList_post.SelectedIndex == 0)
            {
                msg.Show("Please select Post Applied");
                return;
            }

            if (DropDownList_post.SelectedValue != "")
            {
                string regno = Session["rid"].ToString();
                string applid = objcd.getapplid(DropDownList_post.SelectedValue, regno);
                DataTable dtget = objcd.Getedossiersfinal(hfdummy_no.Value);
                hfdummy_no.Value = applid;
                if (dtget.Rows.Count > 0)
                {
                    if (dtget.Rows[0]["final"].ToString() == "Y")
                    {
                        string url = md5Util.CreateTamperProofURL("ReplaceRecallDoc.aspx", null, "applid=" + MD5Util.Encrypt(applid, true) + "&jid=" + MD5Util.Encrypt(DropDownList_post.SelectedValue, true));
                        Response.Redirect(url);
                    }
                    else
                    {
                        msg.Show("You have not finally submitted the eDossier");
                        return;
                    }
                }
                else
                {
                    msg.Show("No Replace/Recalled Documents");
                    return;
                }
            }
        }
        catch (System.Threading.ThreadAbortException ext)
        {
        }
        catch (Exception ex)
        {
            msg.Show("Something went wrong");
        }
    }

    protected void grddoc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddoc.EditIndex = -1;
        FillGrid();
    }

    protected void grdother_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddoc.EditIndex = -1;
        FillGrid_other();
    }
    protected void grdcat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddoc.EditIndex = -1;
        FillGrid_Category();
    }
    protected void grdsubcat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddoc.EditIndex = -1;
        FillGrid_SubCategory();
    }
}