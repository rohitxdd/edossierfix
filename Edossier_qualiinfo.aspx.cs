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

public partial class Edossier_qualiinfo : BasePage
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
            fill_application_data(applid);
        }
    }
    public void fill_application_data(string applid)
    {
        try
        {
            DataTable dtedquali = objCandD.getedqualidetails(edid);
            if (dtedquali.Rows.Count == 0)
            {

                dt = objCandD.GetJobApplication_Education(applid, "", "", "Y");
                string IP = GetIPAddress();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string qid = "", percentage = "", board = "", state = "", month = "", year = "", standard = "0";
                        qid = dt.Rows[i]["qid"].ToString();
                        percentage = dt.Rows[i]["percentage"].ToString();
                        board = dt.Rows[i]["board"].ToString();
                        // instname = dt.Rows[i][instname].ToString();
                        state = dt.Rows[i]["Stateid"].ToString();
                        month = dt.Rows[i]["month"].ToString();
                        year = dt.Rows[i]["year"].ToString();
                        standard = dt.Rows[i]["standard"].ToString();
                        //  finalresultdate = dt.Rows[i][finalresultdate].ToString();
                        // govtorpvt = dt.Rows[i][govtorpvt].ToString();
                        int temp = objCandD.InsertEDqualification(int.Parse(edid), qid, percentage, board, state, month, year, int.Parse(standard), IP,"","","","","","");
                    }
                }
            }
            DataTable dtget = objCandD.Getedossiersfinal(applid);
            if (dtget.Rows.Count > 0)
            {
                hffinal.Value = dtget.Rows[0]["final"].ToString();
            }
            int reqid = objCandD.get_reqid(jid);
            hfreqid.Value = reqid.ToString();
            fillgridquali();
            lblpostcode.Text = post;
            lblrollno.Text = rollno;
            FillGrid();
            checkforediting();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void checkforediting()
    {
        string regno = Session["rid"].ToString();

        DataTable dt1 = objCandD.check_post_foruploadeDossier(regno, jid);
        DataTable dtget = objCandD.Getedossiersfinal(applid);
        if (dt1.Rows.Count > 0)
        {
            if (dtget.Rows.Count > 0)
            {

                if (dtget.Rows[0]["final"].ToString() == "Y")
                {
                    // FillGrid();
                    //  trdocgrd.Visible = true;
                    // trdocgrd1.Visible = true;
                    // trdocgrd2.Visible = true;
                    grddoc.Columns[8].Visible = false;
                    gvquali.Columns[10].Visible = false;
                    btnnext.Visible = true;
                    btnsave.Visible = false;
                    //chkdis.Checked = true;
                    // chkdis.Enabled = false;
                    hfallowedit.Value = "N";
                }
                else
                {
                    grddoc.Columns[8].Visible = true;
                    gvquali.Columns[10].Visible = true;
                    btnnext.Visible = false;
                    btnsave.Visible = true;
                    //chkdis.Enabled = true;
                }
            }
        }
        else
        {
            hfallowedit.Value = "N";
            grddoc.Columns[8].Visible = false;
            gvquali.Columns[10].Visible = false;
            btnnext.Visible = true;
            btnsave.Visible = false;
        }

        //else
        //{
        //    msg.Show("Data Not Found");
        //}
    }
    private void fillgridquali()
    {
        DataTable dtedquali = objCandD.getedqualidetails(edid);
        //if (dtedquali.Rows.Count > 0)
       // {
            gvquali.DataSource = dtedquali;
            gvquali.DataBind();
       // }
    }


    protected void gvquali_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Change")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);
            string IP = GetIPAddress();
            string id = gvquali.DataKeys[index].Values["id"].ToString();
            TextBox txtfresultdt = (TextBox)gvquali.Rows[index].FindControl("txtfresultdt");
            TextBox txtinstname = (TextBox)gvquali.Rows[index].FindControl("txtinstname");
            TextBox txtotherdegree = (TextBox)gvquali.Rows[index].FindControl("txtotherdegree");
            DropDownList ddldegreename = (DropDownList)gvquali.Rows[index].FindControl("ddldegreename");
            RadioButtonList rbtgovorpvt = (RadioButtonList)gvquali.Rows[index].FindControl("rbtgovorpvt");
            FileUpload fileupload = (FileUpload)gvquali.Rows[index].FindControl("fileupload");
            Label lblpvtdocproof = (Label)gvquali.Rows[index].FindControl("lblpvtdocproof");
            LinkButton lbupdate = (LinkButton)gvquali.Rows[index].FindControl("lbupdate");
            LinkButton lnkcancel = (LinkButton)gvquali.Rows[index].FindControl("lnkbtncancel");
            HyperLink hyviewdoc = (HyperLink)gvquali.Rows[index].FindControl("hyviewdoc");
            LinkButton lbsave = (LinkButton)gvquali.Rows[index].FindControl("lbsave");
            LinkButton lbchange = (LinkButton)gvquali.Rows[index].FindControl("lbchange");
            Label lblqualiname = (Label)gvquali.Rows[index].FindControl("lblqualiname");
            Label lblfinalresultdate = (Label)gvquali.Rows[index].FindControl("lblfinalresultdate");
            Label lblinstname = (Label)gvquali.Rows[index].FindControl("lblinstname");
            Label lblinstgovorpvt = (Label)gvquali.Rows[index].FindControl("lblinstgovorpvt");
            string govtorpvt = gvquali.DataKeys[index].Values["govtorpvt"].ToString();
            string edmid = gvquali.DataKeys[index].Values["edmid"].ToString();
            string edqid = gvquali.DataKeys[index].Values["edqid"].ToString();
            if (govtorpvt != "")
            {
                rbtgovorpvt.SelectedValue = govtorpvt;
            }
            fillddldegreename(ddldegreename);
            ddldegreename.SelectedValue = edqid;
            if (ddldegreename.SelectedValue == "99")
            {
                txtotherdegree.Visible = true;
            }
            else
            {
                txtotherdegree.Visible = false;
            }
            if (id != "")
            {
                if (rbtgovorpvt.SelectedValue == "P")
                {
                    fileupload.Visible = true;
                    lblpvtdocproof.Visible = true;
                }
                else
                {
                    fileupload.Visible = false;
                    hyviewdoc.Visible = false;
                    lblpvtdocproof.Visible = false;
                }
               // fileupload.Visible = true;
               // lblpvtdocproof.Visible = true;
                lbupdate.Visible = true;
             //   hyviewdoc.Visible = false;
                lbsave.Visible = false;
                lbchange.Visible = false;
                lblqualiname.Visible = false;
                lblfinalresultdate.Visible = false;
                lblinstname.Visible = false;
                lblinstgovorpvt.Visible = false;
                lnkcancel.Visible = true;
                txtfresultdt.Visible = true;
                txtinstname.Visible = true;
                ddldegreename.Visible = true;
                rbtgovorpvt.Visible = true;

                
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
            string IP = GetIPAddress();
            string id = gvquali.DataKeys[index].Values["id"].ToString();
            TextBox txtfresultdt = (TextBox)gvquali.Rows[index].FindControl("txtfresultdt");
            TextBox txtinstname = (TextBox)gvquali.Rows[index].FindControl("txtinstname");
            TextBox txtotherdegree = (TextBox)gvquali.Rows[index].FindControl("txtotherdegree");
            DropDownList ddldegreename = (DropDownList)gvquali.Rows[index].FindControl("ddldegreename");
            RadioButtonList rbtgovorpvt = (RadioButtonList)gvquali.Rows[index].FindControl("rbtgovorpvt");
            FileUpload fileupload = (FileUpload)gvquali.Rows[index].FindControl("fileupload");
            string edmid = gvquali.DataKeys[index].Values["edmid"].ToString();
            if (ddldegreename.SelectedValue != "99")
            {
                txtotherdegree.Text = "";
            }
            if (rbtgovorpvt.SelectedValue == "P")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    msg.Show("Please browse the document");
                    return;
                }
                else
                {
                    uploadfile(edmid, fileupload, "I", "", "", "", "", "", "", "", "");
                }
            }
            int temp = objCandD.UpdateEDqualidetails(int.Parse(id), txtinstname.Text, Utility.formatDate(txtfresultdt.Text), rbtgovorpvt.SelectedValue, IP, ddldegreename.SelectedValue, txtotherdegree.Text,hfdocid.Value);
            if (temp > 0)
            {
                msg.Show("Record Saved");
            }

        }

        if (e.CommandName == "EInsert")
        {
          //  GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //int index = Convert.ToInt32(e.CommandArgument);
            string IP = GetIPAddress();
            //string id = gvquali.DataKeys[index].Values["id"].ToString();
            TextBox txtfresultdt = (TextBox)gvquali.Controls[0].Controls[0].FindControl("txtfresultdt");
            TextBox txtinstname = (TextBox)gvquali.Controls[0].Controls[0].FindControl("txtinstname");
            TextBox txtotherdegree = (TextBox)gvquali.Controls[0].Controls[0].FindControl("txtotherdegree");
            DropDownList ddldegreename = (DropDownList)gvquali.Controls[0].Controls[0].FindControl("ddldegreename");
            RadioButtonList rbtgovorpvt = (RadioButtonList)gvquali.Controls[0].Controls[0].FindControl("rbtgovorpvt");
            FileUpload fileupload = (FileUpload)gvquali.Controls[0].Controls[0].FindControl("fileupload");
            DropDownList DropDownList_qe = (DropDownList)gvquali.Controls[0].Controls[0].FindControl("DropDownList_qe");
            TextBox txt_pere = (TextBox)gvquali.Controls[0].Controls[0].FindControl("txt_pere");
            TextBox txt_ex_bodye = (TextBox)gvquali.Controls[0].Controls[0].FindControl("txt_ex_bodye");
            DropDownList DropDownList_edu_statee = (DropDownList)gvquali.Controls[0].Controls[0].FindControl("DropDownList_edu_statee");
            DropDownList DropDownList_monthe = (DropDownList)gvquali.Controls[0].Controls[0].FindControl("DropDownList_monthe");
            DropDownList DropDownList_yeare = (DropDownList)gvquali.Controls[0].Controls[0].FindControl("DropDownList_yeare");
            DropDownList ddlstande = (DropDownList)gvquali.Controls[0].Controls[0].FindControl("ddlstande");

            string edmid = objCandD.get_edmid_pvtinst(jid);
            if (ddldegreename.SelectedValue != "99")
            {
                txtotherdegree.Text = "";
            }
            if (rbtgovorpvt.SelectedValue == "P")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    msg.Show("Please browse the document");
                    return;
                }
                else
                {
                    uploadfile(edmid, fileupload, "I", "", "", "", "", "", "", "", "");
                }
            }
            int temp = objCandD.InsertEDqualification(int.Parse(edid), DropDownList_qe.SelectedValue, txt_pere.Text, txt_ex_bodye.Text, DropDownList_edu_statee.SelectedValue, DropDownList_monthe.SelectedValue, DropDownList_yeare.SelectedValue, int.Parse(ddlstande.SelectedValue), IP,Utility.formatDate( txtfresultdt.Text), rbtgovorpvt.SelectedValue, hfdocid.Value, ddldegreename.SelectedValue, txtotherdegree.Text, txtinstname.Text);
            if (temp > 0)
            {
                msg.Show("Record Saved");
                fillgridquali();
            }

        }
    }
    protected void gvquali_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string IP = GetIPAddress();
        string id = gvquali.DataKeys[index].Values["id"].ToString();
        TextBox txtfresultdt = (TextBox)gvquali.Rows[index].FindControl("txtfresultdt");
        TextBox txtinstname = (TextBox)gvquali.Rows[index].FindControl("txtinstname");
        TextBox txtotherdegree = (TextBox)gvquali.Rows[index].FindControl("txtotherdegree");
        DropDownList ddldegreename = (DropDownList)gvquali.Rows[index].FindControl("ddldegreename");
        RadioButtonList rbtgovorpvt = (RadioButtonList)gvquali.Rows[index].FindControl("rbtgovorpvt");
        FileUpload fileupload = (FileUpload)gvquali.Rows[index].FindControl("fileupload");
        string edmid = gvquali.DataKeys[index].Values["edmid"].ToString();
        string docproofpvtinst = gvquali.DataKeys[index].Values["docproofpvtinst"].ToString();
        if (ddldegreename.SelectedValue != "99")
        {
            txtotherdegree.Text = "";
        }
        if (rbtgovorpvt.SelectedValue != "")
        {
            string docid = "";
            if (rbtgovorpvt.SelectedValue == "P")
            {
                if (fileupload.PostedFile.ContentLength == 0)
                {
                    msg.Show("Please browse the document");
                    return;
                }
                else
                {
                    if (docproofpvtinst != "")
                    {
                        uploadfile(edmid, fileupload, "U", docproofpvtinst, "", "", "", "", "", "", "");
                    }
                    else
                    {
                        uploadfile(edmid, fileupload, "I", "", "", "", "", "", "", "", "");
                    }
                }
                
                if (docproofpvtinst == "")
                {
                    docid = hfdocid.Value;
                }
                else
                {
                    docid = docproofpvtinst;
                }
            }
           
            int temp = objCandD.UpdateEDqualidetails(int.Parse(id), txtinstname.Text, Utility.formatDate(txtfresultdt.Text), rbtgovorpvt.SelectedValue, IP, ddldegreename.SelectedValue, txtotherdegree.Text, docid);
            if (temp > 0)
            {
                msg.Show("Record Saved");
            }
            fillgridquali();
        }
        else
        {
            msg.Show("Please select Govt or Private Intitute");
        }
       
    }
    protected void gvquali_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvquali.EditIndex = -1;
        fillgridquali();
    }
    private void FillGrid()
    {
        try
        {
            DataTable dtdoc = new DataTable();
            string regno = Session["rid"].ToString();
            //string applid = objCandD.getapplid(jid, regno);
            //hfdummy_no.Value = applid;
            dtdoc = objCandD.GetEdossierMaster(jid, applid, "E");
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
            TextBox txtsubjects = (TextBox)grddoc.Rows[index].FindControl("txtsubjects");
            TextBox txtmaxmarks = (TextBox)grddoc.Rows[index].FindControl("txtmaxmarks");
            TextBox txtmarksobtained = (TextBox)grddoc.Rows[index].FindControl("txtmarksobtained");
            Label lblsubjects = (Label)grddoc.Rows[index].FindControl("lblsubjects");
            Label lblmaxmarks = (Label)grddoc.Rows[index].FindControl("lblmaxmarks");
            Label lblmarksobtained = (Label)grddoc.Rows[index].FindControl("lblmarksobtained");
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
                txtmarksobtained.Visible = true;
                txtmaxmarks.Visible = true;
                txtsubjects.Visible = true;
                lblsubjects.Visible = false;
                lblmaxmarks.Visible = false;
                lblmarksobtained.Visible = false;
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
            TextBox txtsubjects = (TextBox)grddoc.Rows[index].FindControl("txtsubjects");
            TextBox txtmaxmarks = (TextBox)grddoc.Rows[index].FindControl("txtmaxmarks");
            TextBox txtmarksobtained = (TextBox)grddoc.Rows[index].FindControl("txtmarksobtained");
            if (fileupload.PostedFile.ContentLength == 0)
            {
                msg.Show("Please browse the document");
            }

            uploadfile(edmid, fileupload, "I", "", "", "", "", txtboxremarksvalue.Text,txtsubjects.Text,txtmaxmarks.Text,txtmarksobtained.Text);
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

    private void uploadfile(string edmid, FileUpload fileupload, string function, string edid, string adharno, string subcat, string othermiscdoc, string remarks,string subjects,string maxmarks, string marksobtained)
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
                        if (edid == "")
                        {
                            hfdocid.Value = "";
                            int i = objCandD.inserteDossier(edmid, imageSize, applid, Session["rid"].ToString(), ipaddress, adharno, subcat, othermiscdoc, remarks,subjects,maxmarks,marksobtained);
                            if (i > 0)
                            {
                                hfdocid.Value = i.ToString();
                                msg.Show("Document Inserted Successfully");

                            }
                        }
                        else
                        {
                            int i = objCandD.UpdateCandidateEdossier(edid, imageSize, adharno, ipaddress, Session["rid"].ToString(), remarks, subjects, maxmarks, marksobtained);
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
                TextBox txtsubjects = (TextBox)e.Row.FindControl("txtsubjects");
                Label lblsubjects = (Label)e.Row.FindControl("lblsubjects");
                TextBox txtmaxmarks = (TextBox)e.Row.FindControl("txtmaxmarks");
                Label lblmaxmarks = (Label)e.Row.FindControl("lblmaxmarks");
                TextBox txtmarksobtained = (TextBox)e.Row.FindControl("txtmarksobtained");
                Label lblmarksobtained = (Label)e.Row.FindControl("lblmarksobtained");
                string url = md5util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
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
                    if (hfallowedit.Value == "N")
                    {
                        fileupload.Visible = false;
                    }
                    else
                    {
                        fileupload.Visible = true;
                    }
                    lbchange.Visible = false;
                    lbsave.Visible = true;
                    lblremarks.Visible = false;
                    txtboxremarksvalue.Visible = true;
                    lblsubjects.Visible = false;
                    txtsubjects.Visible = true;
                    lblmaxmarks.Visible = false;
                    txtmaxmarks.Visible = true;
                    lblmarksobtained.Visible = false;
                    txtmarksobtained.Visible = true;
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
        TextBox txtsubjects = (TextBox)grddoc.Rows[index].FindControl("txtsubjects");
        TextBox txtmaxmarks = (TextBox)grddoc.Rows[index].FindControl("txtmaxmarks");
        TextBox txtmarksobtained = (TextBox)grddoc.Rows[index].FindControl("txtmarksobtained");
        if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
        {
            try
            {
                uploadfile("", fileupload, "U", edid, "", "", "", remarks.Text,txtsubjects.Text,txtmaxmarks.Text,txtmarksobtained.Text);
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
    protected void grddoc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddoc.EditIndex = -1;
        FillGrid();
    }
    protected void gvquali_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvquali.EditIndex = e.NewEditIndex;

        gvquali.EditIndex = -1;
        fillgridquali();
    }
    protected void gvquali_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButtonList rbtgovorpvt = (RadioButtonList)e.Row.FindControl("rbtgovorpvt");
            DropDownList ddldegreename = (DropDownList)e.Row.FindControl("ddldegreename");
            TextBox txtfresultdt = (TextBox)e.Row.FindControl("txtfresultdt");
            TextBox txtinstname = (TextBox)e.Row.FindControl("txtinstname");
            TextBox txtotherdegree = (TextBox)e.Row.FindControl("txtotherdegree");
            string govtorpvt = gvquali.DataKeys[e.Row.RowIndex].Values["govtorpvt"].ToString();
            string edqid = gvquali.DataKeys[e.Row.RowIndex].Values["edqid"].ToString();
            string docproofpvtinst = gvquali.DataKeys[e.Row.RowIndex].Values["docproofpvtinst"].ToString();
            FileUpload fileupload = (FileUpload)e.Row.FindControl("fileupload");
            HyperLink hyviewdoc = (HyperLink)e.Row.FindControl("hyviewdoc");
            Label lblpvtdocproof = (Label)e.Row.FindControl("lblpvtdocproof");
            Label lblqualiname = (Label)e.Row.FindControl("lblqualiname");
            Label lblfinalresultdate = (Label)e.Row.FindControl("lblfinalresultdate");
            Label lblinstname = (Label)e.Row.FindControl("lblinstname");
            Label lblinstgovorpvt = (Label)e.Row.FindControl("lblinstgovorpvt");

            LinkButton lbchange = (LinkButton)e.Row.FindControl("lbchange");
            LinkButton lbsave = (LinkButton)e.Row.FindControl("lbsave");
          
           // rbtgovorpvt.SelectedValue = govtorpvt;
            fillddldegreename(ddldegreename);
            
           // ddldegreename.SelectedValue = edqid;
            
           // if (hffinal.Value == "Y")
           // {
                ddldegreename.Visible = false;
                txtfresultdt.Visible = false;
                txtinstname.Visible = false;
                rbtgovorpvt.Visible = false;
                txtotherdegree.Visible =false;
                fileupload.Visible = false;
                lblpvtdocproof.Visible = false;
                if (edqid != "")
                {
                    lbchange.Visible = true;
                    lbsave.Visible = false;
                    ddldegreename.Visible = false;
                    txtfresultdt.Visible = false;
                    txtinstname.Visible = false;
                    rbtgovorpvt.Visible = false;
                    txtotherdegree.Visible = false;
                    lblqualiname.Visible = true;
                    lblfinalresultdate.Visible = true;
                    lblinstname.Visible = true;
                    lblinstgovorpvt.Visible = true;
                }
                else
                {
                    lbchange.Visible = false;
                    lbsave.Visible = true;
                    ddldegreename.Visible = true;
                    txtfresultdt.Visible = true;
                    txtinstname.Visible = true;
                    rbtgovorpvt.Visible = true;
                  //  txtotherdegree.Visible = true;
                    lblqualiname.Visible = false;
                    lblfinalresultdate.Visible = false;
                    lblinstname.Visible = false;
                    lblinstgovorpvt.Visible = false;
                }
                if (ddldegreename.SelectedValue == "99")
                {
                    txtotherdegree.Visible = true;
                }
                else
                {
                    txtotherdegree.Visible = false;
                }
           // }
           // else
           // {
               
           // }
            if (docproofpvtinst != "")
            {
                hyviewdoc.Visible = true;
                string url = md5util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(docproofpvtinst, true));
                hyviewdoc.NavigateUrl = url;
            }
            else
            {
                hyviewdoc.Visible = false;
            }
          
            
        }
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            gvquali.Columns[3].Visible = false;
            DropDownList ddlstandard = (DropDownList)(e.Row.FindControl("ddlstande"));
            fill_standard(ddlstandard);
            DropDownList ddlquali = (DropDownList)(e.Row.FindControl("DropDownList_qe"));
            fill_edu(ddlquali, "");
            DropDownList ddlstate = (DropDownList)(e.Row.FindControl("DropDownList_edu_statee"));
            populateState(ddlstate);
            DropDownList ddlmonth = (DropDownList)(e.Row.FindControl("DropDownList_monthe"));
            populate_month(ddlmonth);
            DropDownList ddlyear = (DropDownList)(e.Row.FindControl("DropDownList_yeare"));
            populate_year(ddlyear, "");
            DropDownList ddldegreename = (DropDownList)(e.Row.FindControl("ddldegreename"));
            fillddldegreename(ddldegreename);
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        DataTable dtqcheck = objCandD.checkedqualidetails(edid);
        for (int i = 0; i < dtqcheck.Rows.Count; i++)
            {
                if (dtqcheck.Rows[0]["finalresultdate"].ToString() == "" || dtqcheck.Rows[0]["govtorpvt"].ToString() == "" || dtqcheck.Rows[0]["instname"].ToString() == "" || dtqcheck.Rows[0]["edqid"].ToString() == "")
                {
                    msg.Show("Please Enter and save Required Qualification details");
                    return;
                }
            }
            string url = md5util.CreateTamperProofURL("EdossierOtherDoc.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
            Server.Transfer(url);
       
    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        string url = md5util.CreateTamperProofURL("EdossierOtherDoc.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
        Server.Transfer(url);
    }
    private void fillddldegreename(DropDownList ddldegreename)
    {
        try
        {
            DataTable dtrr = objCandD.GetEdossierqualiMaster(jid);
            ddldegreename.DataSource = dtrr;
            ddldegreename.DataTextField = "edqname";
            ddldegreename.DataValueField = "edqid";

           // ddldegreename.DataBind();
          
            DataRow dr;
            dr = dtrr.NewRow();
            dr["edqname"] = "Not Available in the RR";
            dr["edqid"] = "99";
            dtrr.Rows.InsertAt(dr, 99);
            ddldegreename.DataBind();
            ddldegreename.Items.Insert(0, Utility.ddl_Select_Value());
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void ddldegreename_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddldegreename = sender as DropDownList;
        GridViewRow Row = ddldegreename.NamingContainer as GridViewRow;
        TextBox txtotherdegree = Row.FindControl("txtotherdegree") as TextBox;
        if (ddldegreename.SelectedValue == "99")
        {
            txtotherdegree.Visible = true;
        }
        else
        {
            txtotherdegree.Visible = false;
        }
    }
    protected void rbtgovorpvt_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rbtgovorpvt = sender as RadioButtonList;
        GridViewRow Row = rbtgovorpvt.NamingContainer as GridViewRow;
        FileUpload fileupload = Row.FindControl("fileupload") as FileUpload;
        Label lblpvtdocproof = Row.FindControl("lblpvtdocproof") as Label;
        HyperLink hyviewdoc = Row.FindControl("hyviewdoc") as HyperLink;
        if (rbtgovorpvt.SelectedValue == "P")
        {
            fileupload.Visible = true;
            lblpvtdocproof.Visible = true;
        }
        else
        {
            fileupload.Visible = false;
            hyviewdoc.Visible = false;
            lblpvtdocproof.Visible = false;
        }
    }
    public void fill_standard(DropDownList stand)
    {
        DataTable dt = objCandD.fill_standard(hfreqid.Value, "E","");
        stand.DataTextField = "standard";
        stand.DataValueField = "id";
        stand.DataSource = dt;
        stand.DataBind();
        ListItem item = new ListItem();
        item.Text = "--Select Any--";
        item.Value = "";
        stand.Items.Insert(0, item);
    }
    public void fill_edu(DropDownList edu, string depstand)
    {
        DataTable dt = objCandD.GetEducationMinimumClass(hfreqid.Value, depstand, "E","");
        edu.DataTextField = "name";
        edu.DataValueField = "uid";
        edu.DataSource = dt;
        edu.DataBind();
        ListItem item = new ListItem();
        item.Text = "--Select Any--";
        item.Value = "";
        edu.Items.Insert(0, item);
    }
    private void populateState(DropDownList state)
    {
        try
        {

            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();
            dt = objCandD.SelectState();
            FillDropDown(state, dt, "state", "code");
            state.SelectedValue = "7";
            FillDropDown(state, dt, "state", "code");
            state.SelectedValue = "7";
            FillDropDown(state, dt, "state", "code");
            state.SelectedValue = "7";
            FillDropDown(state, dt, "state", "code");
            state.SelectedValue = "7";
            dt = null;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void FillDropDown(DropDownList ddl, DataTable dt, string textfield, string valuefield)
    {
        ddl.Items.Clear();
        ddl.DataTextField = textfield;
        ddl.DataValueField = valuefield;
        ddl.DataSource = dt;
        ddl.DataBind();
        ListItem l1 = new ListItem();
        l1.Text = "--Select--";
        l1.Value = "";
        ddl.Items.Insert(0, l1);

    }
    private void populate_month(DropDownList passmonth)
    {
        ListItem li;
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length - 1; i++)
        {
            passmonth.Items.Add(new ListItem(months[i], (i+1).ToString()));
        }
        li = new ListItem();
        li.Text = "-Select-";
        li.Value = "";
        passmonth.Items.Insert(0, li);
    }
    private void populate_year(DropDownList passyear, string stnd)
    {
        ListItem li;
        string stndt = stnd;
        int can_dob = 0;
        string dob = Session["birthdt"].ToString();
        string dobc = dob.Substring(6);
        if (stndt == "")
        {
            can_dob = Convert.ToInt32(dobc) + 12;
        }
        else
        {
            int st_nd = Convert.ToInt32(stndt);
            if (st_nd == 1)
            {
                can_dob = Convert.ToInt32(dobc) + 12;
            }
            else if (st_nd == 2)
            {
                can_dob = Convert.ToInt32(dobc) + 14;
            }
            else if (st_nd == 3)
            {
                can_dob = Convert.ToInt32(dobc) + 17;
            }
            else if (st_nd == 4)
            {
                can_dob = Convert.ToInt32(dobc) + 19;
            }
            else if (st_nd == 5)
            {
                can_dob = Convert.ToInt32(dobc) + 20;
            }


        }

        int year = DateTime.Now.Year;
        //for (int i = year; i >= year - 36; i--)
        passyear.Items.Clear();
        for (int i = year; i >= can_dob; i--)
        {
            li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            passyear.Items.Add(li);
        }
        li = new ListItem();
        li.Text = "-Select-";
        li.Value = "";
        passyear.Items.Insert(0, li);
    }
    protected void ddl_SelectedIndexFooterChangedE(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;

        DropDownList dlle = Row.FindControl("DropDownList_yeare") as DropDownList;
        DropDownList dlls = Row.FindControl("DropDownList_qe") as DropDownList;
        DropDownList ddl_stand = Row.FindControl("ddlstande") as DropDownList;

        TextBox txt_qulie = Row.FindControl("txt_qulie") as TextBox;
        TextBox txt_pere = Row.FindControl("txt_pere") as TextBox;
        TextBox txt_ex_bodye = Row.FindControl("txt_ex_bodye") as TextBox;
        DropDownList ddl_state = Row.FindControl("DropDownList_edu_statee") as DropDownList;
        DropDownList ddl_month = Row.FindControl("DropDownList_monthe") as DropDownList;
        DropDownList ddl_year = Row.FindControl("DropDownList_yeare") as DropDownList;

        string stnd = dll.SelectedValue;

        fill_edu(dlls, stnd);
        populate_year(dlle, stnd);
        if (ddl_stand.SelectedItem.Text == "X")
        {
            string rids = Session["rid"].ToString();
            dlle.SelectedValue = rids.Substring((rids.Length - 4), 4);
            // dlle.Enabled = false;
        }
        else
        {
            dlle.Enabled = true;
        }
        if (ddl_stand.SelectedItem.ToString() == "Ex-Serviceman Degree")
        {
           dlle.Visible = false;
            dlls.Visible = true;

          //  txt_qulie.Visible = false;
            txt_pere.Visible = false;
            txt_ex_bodye.Visible = false;
            ddl_state.Visible = false;
            ddl_month.Visible = false;
            ddl_year.Visible = false;
        }
        else
        {
            dlle.Visible = true;
            dlls.Visible = true;

          //  txt_qulie.Visible = true;
            txt_pere.Visible = true;
            txt_ex_bodye.Visible = true;
            ddl_state.Visible = true;
            ddl_month.Visible = true;
            ddl_year.Visible = true;
        }
    }
}