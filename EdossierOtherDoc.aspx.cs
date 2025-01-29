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

public partial class EdossierOtherDoc : BasePage
{
    DataTable dt = new DataTable();
    CandidateData objCandD = new CandidateData();
    CandCombdData objCombd = new CandCombdData();
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
        //___________________Added for preference on 22/02/2023______________

        DataTable dtt = objCombd.GetEdossierMaster_prefer(jid);
        DataTable check = objCombd.checkcombd(jid);
        if (check.Rows[0]["deptcode"].ToString() == "COMBD")
        {
            if (dtt.Rows.Count > 0)
            {
                string edmid = dtt.Rows[0]["edmid"].ToString();
                DataTable dt1 = objCombd.select_preferCertificate(applid, edmid);
                if (dt1.Rows.Count > 0)
                {
                    grdviewdoc.Visible = true;
                    filldoc();
                    Label4.Visible = false;
                    lbl_note1.Visible = false;
                    lbl_note2.Visible = false;
                    lbl_note3.Visible = false;
                    lbl_note4.Visible = false;
                    lbl_note5.Visible = false;
                    lbl_note6.Visible = false;
                    // lbl_note7.Visible = false;
                    Lbl_prefer.Visible = false;
                    td_msg.Visible = false;
                    Label3.Visible = false;
                    btn_save.Visible = false;
                }
            }
        }
        dt = objCombd.getdepartment(jid);
        if (dt.Rows.Count == 0)
        {
            Label4.Visible = false;
            lbl_note1.Visible = false;
            lbl_note2.Visible = false;
            lbl_note3.Visible = false;
            lbl_note4.Visible = false;
            lbl_note5.Visible = false;
            lbl_note6.Visible = false;
            //  lbl_note7.Visible = false;
            Lbl_prefer.Visible = false;
            td_msg.Visible = false;
            Label3.Visible = false;
            btn_save.Visible = false;
        }
        else
        {
            Label4.Visible = true;
            lbl_note1.Visible = true;
            lbl_note2.Visible = true;
            lbl_note3.Visible = true;
            lbl_note4.Visible = true;
            lbl_note5.Visible = true;
            lbl_note6.Visible = true;
            // lbl_note7.Visible = false;
            Lbl_prefer.Visible = true;
            td_msg.Visible = true;
            dt = objCombd.showdepartment(jid, rollno);
            if (dt.Rows.Count > 0)
            {
                btn_save.Visible = false;
            }
            else
            {
                btn_save.Visible = true;
            }
        }
        //___________________Added for preference on 22/02/2023______________
        if (!IsPostBack)
        {
            lblpostcode.Text = post;
            lblrollno.Text = rollno;
            checkforediting();
            //___________________Added for preference______________
            if (check.Rows[0]["deptcode"].ToString() == "COMBD")
            {
                msg.Show("PLEASE GO THROUGH THE POINTS IN NOTE BEFORE INDICATING YOUR PREFERENCE");
                string edmid = dtt.Rows[0]["edmid"].ToString();
                DataTable dt2 = objCombd.select_preferCertificate(applid, edmid);
                if (dt2.Rows.Count > 0)
                {
                    grdviewdoc.Visible = true;
                    filldoc();
                    td_msg.Visible = false;
                    Label3.Visible = false;
                    btn_save.Visible = false;
                    Lbl_prefer.Visible = false;
                }
                else
                {
                    dt = objCombd.showdepartment(jid, rollno);
                    if (dt.Rows.Count > 0)
                    {
                        grdselecteddept.Visible = true;
                        filldept();
                        grdpreferdept.Visible = false;

                    }
                    else
                    {
                        fill_dept();
                        grdpreferdept.Visible = true;
                    }
                }
            }
            //_______________________________________________________
        }
    }
    private void checkforediting()
    {
        string regno = Session["rid"].ToString();

        DataTable dt1 = objCandD.check_post_foruploadeDossier(regno, jid);

        DataTable dtget = objCandD.Getedossiersfinal(applid);
        DataTable dtchkmisc = objCandD.GetEdossierMaster_miscdoc(jid);

        if (dtchkmisc.Rows.Count > 0)
        {
            FillGrid_misc(dtchkmisc.Rows[0]["edmid"].ToString());
            //trmisc.Visible = true;
            //  trmisc1.Visible = true;
            // trmisc2.Visible = true;

        }
        if (dt1.Rows.Count > 0)
        {

            if (dtget.Rows.Count > 0)
            {
                if (dtget.Rows[0]["final"].ToString() == "Y")
                {

                    grdmisc.Columns[4].Visible = false;
                    btnfinal.Visible = false;
                    //grdmisc.Columns[3].Visible = false;
                    lbledno.Text = "Your Edossier No is : " + dtget.Rows[0]["edossierNo"].ToString();
                    tredno.Visible = true;
                    chkdis.Checked = true;
                }
                else
                {
                    // grdmisc.Columns[3].Visible = true;
                    btnfinal.Visible = true;
                    tredno.Visible = false;
                    chkdis.Checked = false;
                }

            }
            else
            {
                btnfinal.Visible = true;
                tredno.Visible = false;
                chkdis.Checked = false;
            }
        }
        else
        {
            btnfinal.Visible = false;
            chkdis.Checked = true;
            if (dtget.Rows.Count > 0)
            {
                tredno.Visible = true;
                lbledno.Text = lbledno.Text = "Your Edossier No is : " + dtget.Rows[0]["edossierNo"].ToString();
            }
            grdmisc.Columns[4].Visible = false;

            //trmisc.Visible = false;
            // trmisc1.Visible = false;
            // trmisc2.Visible = false;
            //   msg.Show("Either the Last Date for eDossier Submission is already over or you have done final submission.");
            // return;
            // lbl_msg.Visible = true;
        }
      if (dtget.Rows.Count > 0)
        {
            if (dtget.Rows[0]["edossierNo"].ToString()==null)
            {
            // grdmisc.Columns[4].Visible = false;
            btnfinal.Visible = true;
            //grdmisc.Columns[3].Visible = false;
             lbledno.Visible=false;
             tredno.Visible = false;
             chkdis.Checked = false;
            }
        }
        else
        {
            btnfinal.Visible =true;
            //grdmisc.Columns[3].Visible = false;
            lbledno.Visible = false;
            tredno.Visible = false;
            chkdis.Checked = false;
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



    private void FillGrid_misc(string edmid)
    {
        try
        {
            DataTable dtmisc = new DataTable();

            dtmisc = objCandD.GetEdossierMaster_misc(edmid, applid);
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
        DataTable dtchkmisc = objCandD.GetEdossierMaster_miscdoc(jid);
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
                uploadfile(edmid, fileupload, "I", "", "", "", txtcertificateReq.Text, txtremarks.Text.Trim());

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
                uploadfile(edmid, fileupload, "I", "", "", "", txtcertificateReq.Text, txtremarks.Text.Trim());

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
            string url = md5util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
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
            DataTable dtchkmisc = objCandD.GetEdossierMaster_miscdoc(jid);
            string edmid = "";
            if (dtchkmisc.Rows.Count > 0)
            {
                edmid = dtchkmisc.Rows[0]["edmid"].ToString();

            }
            DataTable dtmisc = objCandD.GetEdossierMaster_misc(edmid, applid);
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
        DataTable dtchkmisc = objCandD.GetEdossierMaster_miscdoc(jid);
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
            int temp = objCandD.delete_candidateedossier(edid);
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
            if (chkdis.Checked)
            {
                DataTable dtcheck = objCandD.CheckEdossierforfinal(jid, applid);
                DataTable dtqcheck = objCandD.checkedqualidetails(edid);
                DataTable dtexsubcat = objCandD.getcandsubcategory(applid);

                if (dtcheck.Rows.Count > 0)
                {
                    msg.Show("Please upload all the mandatory documents before Final Submit");
                    return;

                }
		else if (dtqcheck.Rows.Count <= 0)
                {
                  msg.Show("Please enter all Qualification Details before Final Submit");
                  return;
                }
                else if (dtqcheck.Rows[0]["finalresultdate"].ToString() == "" || dtqcheck.Rows[0]["govtorpvt"].ToString() == "" || dtqcheck.Rows[0]["instname"].ToString() == "" || dtqcheck.Rows[0]["edqid"].ToString() == "")
                {
                    msg.Show("Please enter all Qualification Details before Final Submit");
                    return;
                }
                else if (dtqcheck.Rows[0]["govtorpvt"].ToString() == "P" && dtqcheck.Rows[0]["docproofpvtinst"].ToString() == "")
                {
                    msg.Show("Please upload Documentary Proof of Private Insitute  before Final Submit");
                    return;
                }

                else
                {
                    if (dtexsubcat.Rows.Count>0)
                    {
                      //  string[] a = { "," };
                      //  string[] subcat = dtexsubcat.Rows[0]["SubCategory"].ToString().Split(a, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < dtexsubcat.Rows.Count; i++)
                        {
                            if (dtexsubcat.Rows[i]["SubCat_code"].ToString() == "DC" || dtexsubcat.Rows[i]["SubCat_code"].ToString() == "DGS")
                            {
                                DataTable dtedcat = objCandD.getedcatsubcatdetails(edid);
                                for (int j = 0; j < dtedcat.Rows.Count; j++)
                                {
                                    if (dtedcat.Rows[j]["subcatcode"].ToString() == "DGS" || dtedcat.Rows[j]["subcatcode"].ToString() == "DC")
                                    {
                                        if (dtedcat.Rows[j]["docid"].ToString() == "" || dtedcat.Rows[j]["docid"].ToString() == "0")
                                        {
                                            msg.Show("Please Enter Experience Details");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    string ipaddress = GetIPAddress();

                    int temp = objCandD.inserteDossierFinal(applid, "Y", Session["rid"].ToString(), ipaddress);
                    if (temp > 0)
                    {
                        long a = objCandD.insert_edossierNO(applid);
                        if (a > 0)
                        {
                            msg.Show("eDossiers Finally Submitted");
                        }
                    }

                }
                checkforediting();
            }
            else
            {
                msg.Show("Please check Declaration");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    //created by heena 16/12/2022
    private void fill_ddldept(DropDownList ddl_dept)
    {
        try
        {
            dt = objCombd.GetCandidateFilledDepartment(applid);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl_dept.DataSource = dt;
                ddl_dept.DataValueField = "DeptReqId";
                ddl_dept.DataTextField = "preferdept";
                ddl_dept.DataBind();
                ddl_dept.Items.Insert(0, new ListItem("--Select--"));
            }
            else
            {
                dt = objCombd.getdepartment(jid);
                ddl_dept.DataSource = dt;
                ddl_dept.DataTextField = "preferdept";
                ddl_dept.DataValueField = "preferdept";
                ddl_dept.DataBind();
                ddl_dept.Items.Insert(0, new ListItem("--Select--"));
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void fill_dept()
    {
        try
        {
            dt = objCombd.GetCandidateFilledDepartment(applid);
            if (dt.Rows.Count == 0)
            {
                dt = objCombd.getdepartment(jid);
            }
            grdpreferdept.DataSource = dt;
            grdpreferdept.DataBind();
            td_msg.Visible = true;
            btn_up.Visible = false;
            //Lbl_prefer.Visible = true;
            //btn_save.Visible = true;
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void filldept()
    {
        try
        {
            dt = objCombd.showdepartment(MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true), MD5Util.Decrypt(Request.QueryString["rollno"].ToString(), true));
            grdselecteddept.DataSource = dt;
            grdselecteddept.DataBind();
            btn_edit.Visible = true;
            btn_save.Visible = false;
            Btn_finalsubmit.Visible = true;
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void filldoc()
    {
        DataTable dtd = objCombd.GetEdossierMaster_prefer(jid);
        string edmid = "";
        edmid = dtd.Rows[0]["edmid"].ToString();
        DataTable dt1 = objCombd.select_preferCertificate(applid, edmid);
        grdviewdoc.DataSource = dt1;
        grdviewdoc.DataBind();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            string ipaddress = GetIPAddress();
            jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            rollno = MD5Util.Decrypt(Request.QueryString["rollno"].ToString(), true);
            bool flag = true;
            for (int i = 0; i < grdpreferdept.Rows.Count; i++)
            {
                int pref = i + 1;
                DropDownList ddl_dept = (DropDownList)grdpreferdept.Rows[i].FindControl("ddl_dept");
                DropDownList ddl_dept2 = (DropDownList)grdpreferdept.Rows[0].FindControl("ddl_dept");
                if (ddl_dept2.SelectedValue == "--Select--")
                {
                    flag = false;
                    break;
                }
                for (int j = i + 1; j < grdpreferdept.Rows.Count; j++)
                {
                    DropDownList ddl_dept1 = (DropDownList)grdpreferdept.Rows[j].FindControl("ddl_dept");

                    if (ddl_dept.SelectedValue == ddl_dept1.SelectedValue && ddl_dept.SelectedValue != "--Select--" && ddl_dept1.SelectedValue != "--Select--")
                    {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
            {
                int pref = 1;
                bool isCandPrefAvail = objCombd.GetCandidateFilledDepartment(applid).Rows.Count > 0;
                for (int i = 0; i < grdpreferdept.Rows.Count; i++)
                {

                    DropDownList ddl_dept = (DropDownList)grdpreferdept.Rows[i].FindControl("ddl_dept");
                    if (ddl_dept.SelectedValue != "--Select--")
                    {
                        string prefDeptName = ddl_dept.SelectedValue;
                        string deptreqid = string.Empty;
                        if (isCandPrefAvail)
                        {
                            prefDeptName = ddl_dept.SelectedItem.Text;
                            deptreqid = ddl_dept.SelectedValue;
                        }
                        int temp = objCombd.insertcandidatepreferdept(jid, applid, rollno, pref.ToString(), prefDeptName, System.DateTime.Now, ipaddress, deptreqid);
                        pref++;
                    }
                }
                msg.Show("Departments saved successfully");
                btn_save.Visible = false;
                grdpreferdept.Visible = false;
                grdselecteddept.Visible = true;
                btn_edit.Visible = true;
                Btn_finalsubmit.Visible = true;
                btn_viewdownload.Visible = false;
                btn_up.Visible = false;
                filldept();
            }
            else
            {
                msg.Show("select preference correctly");
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        try
        {
            jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
            rollno = MD5Util.Decrypt(Request.QueryString["rollno"].ToString(), true);
            for (int i = 0; i < grdselecteddept.Rows.Count; i++)
            {
                int temp = objCombd.delete_department(jid, rollno);
                btn_save.Visible = true;
                grdpreferdept.Visible = true;
                fill_dept();
                grdselecteddept.Visible = false;
                btn_edit.Visible = false;
                btn_viewdownload.Visible = false;
                btn_up.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void Btn_finalsubmit_Click(object sender, EventArgs e)
    {
        jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
        applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        rollno = MD5Util.Decrypt(Request.QueryString["rollno"].ToString(), true);
        msg.Show("Are you sure to Submit the final prefernces");
        msg.Show("Are you sure to Submit the final prefernces");
        for (int i = 0; i < grdpreferdept.Rows.Count; i++)
        {
            int temp = objCombd.update_prefer(jid, rollno);
        }
        filldept();
        btn_edit.Visible = false;
        btn_viewdownload.Visible = true;
        btn_up.Visible = true;
        Btn_finalsubmit.Visible = false;
    }
    protected void grdpreferdept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl_dept = (DropDownList)e.Row.FindControl("ddl_dept");
            fill_ddldept(ddl_dept);
        }
    }
    protected void btn_viewdownload_Click(object sender, EventArgs e)
    {
        string url = md5util.CreateTamperProofURL("preferCandPDF.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(rollno, true));
        Server.Transfer(url);
    }
    protected void btn_up_Click(object sender, EventArgs e)
    {
        fu_upload.Visible = true;
        btn_upload.Visible = true;
        btn_up.Visible = false;
    }
    protected void grdviewdoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtd = objCombd.GetEdossierMaster_prefer(jid);
            string edmid = "";
            edmid = dtd.Rows[0]["edmid"].ToString();
            HyperLink hyviewdoc1 = (HyperLink)e.Row.FindControl("hyviewdoc1");
            string url = md5util.CreateTamperProofURL("viewuploadedPreferDoc.aspx", null, "applid=" + MD5Util.Encrypt(applid, true) + "&edmid=" + MD5Util.Encrypt(edmid, true));
            hyviewdoc1.NavigateUrl = url;
        }
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        DataTable dtd = objCombd.GetEdossierMaster_prefer(jid);
        string edmid = "";
        edmid = dtd.Rows[0]["edmid"].ToString();
        uploadfile(edmid, fu_upload, "I", "", "", "", "", "");
        grdselecteddept.Visible = false;
        fu_upload.Visible = false;
        btn_upload.Visible = false;
        btn_up.Visible = false;
        btn_edit.Visible = false;
        btn_viewdownload.Visible = false;
        grdpreferdept.Visible = false;
        Lbl_prefer.Visible = false;
        td_msg.Visible = false;
        btn_save.Visible = false;
        Label3.Visible = false;
        Btn_finalsubmit.Visible = false;
        grdviewdoc.Visible = true;
        filldoc();     
    }
    //created by heena 16/12/2022
}