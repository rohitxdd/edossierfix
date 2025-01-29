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

public partial class EdossierExpDetails : BasePage
{
    DataTable dt = new DataTable();
    CandidateData objCandD = new CandidateData();
    message msg = new message();
    MD5Util md5util = new MD5Util();
    string jid = "", edid = "", applid = "", rollno = "", post = "", subcatcode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
        edid = MD5Util.Decrypt(Request.QueryString["edid"].ToString(), true);
        applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        rollno = MD5Util.Decrypt(Request.QueryString["rollno"].ToString(), true);
        post = MD5Util.Decrypt(Request.QueryString["post"].ToString(), true);
        subcatcode = MD5Util.Decrypt(Request.QueryString["subcatcode"].ToString(), true);
        if (!IsPostBack)
        {
            lblpostcode.Text = post;
            lblrollno.Text = rollno;
            checkforediting();
        }
    }
    private void checkforediting()
    {
        string regno = Session["rid"].ToString();

        DataTable dt1 = objCandD.check_post_foruploadeDossier(regno, jid);

        DataTable dtget = objCandD.Getedossiersfinal(applid);
        DataTable dtchkexp = objCandD.GetEdossierMaster_expdoc(jid);
      
        if (dtchkexp.Rows.Count > 0)
        {
            hfedmid.Value = dtchkexp.Rows[0]["edmid"].ToString();
            FillGrid_exp(dtchkexp.Rows[0]["edmid"].ToString());
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
                    grdexp.Columns[9].Visible = false;
                }
                else
                {
                    grdexp.Columns[9].Visible = true;
                }

            }
            else
            {
                grdexp.Columns[9].Visible = true;
            }
        }
        else
        {
            grdexp.Columns[9].Visible = false;

        }
    }
    public int uploadfile(string edmid, FileUpload fileupload, string function, string edid, string adharno, string subcat, string othermiscdoc, string remarks)
    {
        byte[] imageSize = new byte[fileupload.PostedFile.ContentLength];
        string ipaddress = GetIPAddress();
        int i = 0;
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
                            i = objCandD.inserteDossier(edmid, imageSize, applid, Session["rid"].ToString(), ipaddress, adharno, subcat, othermiscdoc, remarks);
                            if (i > 0)
                            {

                                msg.Show("Document Inserted Successfully");

                            }
                        }
                        else
                        {
                            i = objCandD.UpdateCandidateEdossier(edid, imageSize, adharno, ipaddress, Session["rid"].ToString(), remarks);
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
        return i;
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



    private void FillGrid_exp(string edmid)
    {
        try
        {
            DataTable dtexp = new DataTable();

            dtexp = objCandD.GetEdossierMaster_exp(edmid, edid);
            grdexp.DataSource = dtexp;
            grdexp.DataBind();
            trexp.Visible = true;
            trexp1.Visible = true;
            trexp2.Visible = true;
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    protected void grdexp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       // DataTable dtchkexp = objCandD.GetEdossierMaster_expdoc(jid);
        string IP = GetIPAddress();
        //string edmid = "";
        //if (dtchkexp.Rows.Count > 0)
        //{
        //    edmid = dtchkexp.Rows[0]["edmid"].ToString();

        //}
        if (e.CommandName == "Add")
        {
            TextBox txtcurrentoffname = (TextBox)(grdexp.FooterRow.FindControl("txtcurrentoffname"));
            txtcurrentoffname.Visible = true;
            txtcurrentoffname.Focus();
            TextBox txtcurrentoffadd = (TextBox)(grdexp.FooterRow.FindControl("txtcurrentoffadd"));
            txtcurrentoffadd.Visible = true;
            RadioButtonList rbtcentralorstate = (RadioButtonList)(grdexp.FooterRow.FindControl("rbtcentralorstate"));
            rbtcentralorstate.Visible = true;
            //TextBox txtstategovtname = (TextBox)(grdexp.FooterRow.FindControl("txtstategovtname"));
            // TextBox txtministryname = (TextBox)(grdexp.FooterRow.FindControl("txtministryname"));
            RadioButtonList rbtWheAutonomous = (RadioButtonList)(grdexp.FooterRow.FindControl("rbtWheAutonomous"));
            rbtWheAutonomous.Visible = true;
            TextBox txtappointmentdate = (TextBox)(grdexp.FooterRow.FindControl("txtappointmentdate"));
            txtappointmentdate.Visible = true;
            TextBox txtcurrentdesig = (TextBox)(grdexp.FooterRow.FindControl("txtcurrentdesig"));
            txtcurrentdesig.Visible = true;
            FileUpload fileupload = (FileUpload)(grdexp.FooterRow.FindControl("fileupload"));
            fileupload.Visible = true;
            LinkButton lnkAdd = (LinkButton)(grdexp.FooterRow.FindControl("lnkadd"));
            lnkAdd.Visible = false;
            LinkButton lnkIn = (LinkButton)(grdexp.FooterRow.FindControl("lnkIn"));
            lnkIn.Visible = true;
            LinkButton lnkC = (LinkButton)(grdexp.FooterRow.FindControl("lnkC"));
            lnkC.Visible = true;
            TextBox remarks = (TextBox)(grdexp.FooterRow.FindControl("txtboxremarksvalue"));
            remarks.Visible = true;
        }
        else if (e.CommandName == "EInsert")
        {
            TextBox txtcurrentoffname = (TextBox)(grdexp.Controls[0].Controls[0].FindControl("txtcurrentoffname"));
            TextBox txtcurrentoffadd = (TextBox)(grdexp.Controls[0].Controls[0].FindControl("txtcurrentoffadd"));
            RadioButtonList rbtcentralorstate = (RadioButtonList)(grdexp.Controls[0].Controls[0].FindControl("rbtcentralorstate"));
            TextBox txtstategovtname = (TextBox)(grdexp.Controls[0].Controls[0].FindControl("txtstategovtname"));
            TextBox txtministryname = (TextBox)(grdexp.Controls[0].Controls[0].FindControl("txtministryname"));
            RadioButtonList rbtWheAutonomous = (RadioButtonList)(grdexp.Controls[0].Controls[0].FindControl("rbtWheAutonomous"));
            TextBox txtappointmentdate = (TextBox)(grdexp.Controls[0].Controls[0].FindControl("txtappointmentdate"));
            TextBox txtcurrentdesig = (TextBox)(grdexp.Controls[0].Controls[0].FindControl("txtcurrentdesig"));
            TextBox txtremarks = (TextBox)(grdexp.Controls[0].Controls[0].FindControl("txtboxremarksvalue"));
            FileUpload fileupload = (FileUpload)grdexp.Controls[0].Controls[0].FindControl("fileupload");


            if (txtcurrentoffname.Text == "" || txtcurrentoffadd.Text == "" || rbtcentralorstate.SelectedValue == "" || rbtWheAutonomous.SelectedValue == "" || txtappointmentdate.Text == "" || txtcurrentdesig.Text == "")
            {
                msg.Show("Field can't be left blank");
            }

            else if ((Validation.chkLevel7(txtcurrentoffname.Text)))
            {
                msg.Show("Invalid Character in Organization Name");
            }
            else if ((Validation.chkLevel7(txtcurrentoffadd.Text)))
            {
                msg.Show("Invalid Character in Organization Address");
            }
            else if ((Validation.chkLevel7(txtcurrentdesig.Text)))
            {
                msg.Show("Invalid Character in Current Designation");
            }

            else
            {
                if (rbtcentralorstate.SelectedValue == "central")
                {
                    txtstategovtname.Text = "";
                }
                else
                {
                    txtministryname.Text = "";
                }
                DataTable dtchkexpDetail = objCandD.CheckExpDetails(edid, txtcurrentoffname.Text,Utility.formatDate( txtappointmentdate.Text), subcatcode);
                if (dtchkexpDetail.Rows.Count > 0)
                {
                    msg.Show("You already entered this record ");
                }
                else
                {
                    int temp3 = objCandD.insertEDDeptcandsubcatDetails(Convert.ToInt32(edid), subcatcode, txtcurrentoffname.Text, txtcurrentoffadd.Text, rbtcentralorstate.SelectedValue, txtstategovtname.Text, txtministryname.Text, rbtWheAutonomous.SelectedValue, Utility.formatDate(txtappointmentdate.Text), txtcurrentdesig.Text, IP);
                    int i = uploadfile(hfedmid.Value, fileupload, "I", "", "", "", "", txtremarks.Text.Trim());
                    int tmp = objCandD.Updateexpdocid(i, temp3);
                    FillGrid_exp(hfedmid.Value);
                    msg.Show("Record saved successfully");
                }

            }

        }
        else if (e.CommandName == "Insert")
        {
            TextBox txtcurrentoffname = (TextBox)(grdexp.FooterRow.FindControl("txtcurrentoffname"));
            TextBox txtcurrentoffadd = (TextBox)(grdexp.FooterRow.FindControl("txtcurrentoffadd"));
            RadioButtonList rbtcentralorstate = (RadioButtonList)(grdexp.FooterRow.FindControl("rbtcentralorstate"));
            TextBox txtstategovtname = (TextBox)(grdexp.FooterRow.FindControl("txtstategovtname"));
            TextBox txtministryname = (TextBox)(grdexp.FooterRow.FindControl("txtministryname"));
            RadioButtonList rbtWheAutonomous = (RadioButtonList)(grdexp.FooterRow.FindControl("rbtWheAutonomous"));
            TextBox txtappointmentdate = (TextBox)(grdexp.FooterRow.FindControl("txtappointmentdate"));
            TextBox txtcurrentdesig = (TextBox)(grdexp.FooterRow.FindControl("txtcurrentdesig"));
            TextBox txtremarks = (TextBox)(grdexp.FooterRow.FindControl("txtboxremarksvalue"));
            FileUpload fileupload = (FileUpload)grdexp.FooterRow.FindControl("fileupload");


            if (txtcurrentoffname.Text == "" || txtcurrentoffadd.Text == "" || rbtcentralorstate.SelectedValue == "" || rbtWheAutonomous.SelectedValue == "" || txtappointmentdate.Text == "" || txtcurrentdesig.Text == "")
            {
                msg.Show("Field can't be left blank");
            }

            else if ((Validation.chkLevel7(txtcurrentoffname.Text)))
            {
                msg.Show("Invalid Character in Organization Name");
            }
            else if ((Validation.chkLevel7(txtcurrentoffadd.Text)))
            {
                msg.Show("Invalid Character in Organization Address");
            }
            else if ((Validation.chkLevel7(txtcurrentdesig.Text)))
            {
                msg.Show("Invalid Character in Current Designation");
            }


            else
            {
                if (rbtcentralorstate.SelectedValue == "central")
                {
                    txtstategovtname.Text = "";
                }
                else
                {
                    txtministryname.Text = "";
                }
                   DataTable dtchkexpDetail = objCandD.CheckExpDetails(edid, txtcurrentoffname.Text,Utility.formatDate( txtappointmentdate.Text), subcatcode);
                   if (dtchkexpDetail.Rows.Count > 0)
                   {
                       msg.Show("You already entered this record ");
                   }
                   else
                   {
                       int temp3 = objCandD.insertEDDeptcandsubcatDetails(Convert.ToInt32(edid), subcatcode, txtcurrentoffname.Text, txtcurrentoffadd.Text, rbtcentralorstate.SelectedValue, txtstategovtname.Text, txtministryname.Text, rbtWheAutonomous.SelectedValue, Utility.formatDate(txtappointmentdate.Text), txtcurrentdesig.Text, IP);
                       int i = uploadfile(hfedmid.Value, fileupload, "I", "", "", "", "", txtremarks.Text.Trim());
                       int tmp = objCandD.Updateexpdocid(i, temp3);
                       FillGrid_exp(hfedmid.Value);
                       msg.Show("Record saved successfully");
                   }

            }

        }
        else if (e.CommandName == "Change")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = Convert.ToInt32(e.CommandArgument);
            FileUpload fileupload = (FileUpload)grdexp.Rows[index].FindControl("fileupload");
            LinkButton lbupdate = (LinkButton)grdexp.Rows[index].FindControl("lbupdate");
            HyperLink hyviewdoc = (HyperLink)grdexp.Rows[index].FindControl("hyviewdoc");
            //LinkButton lbsave = (LinkButton)grdmisc.Rows[index].FindControl("lbsave");
            LinkButton lbchange = (LinkButton)grdexp.Rows[index].FindControl("lbchange");
            // LinkButton lbremove = (LinkButton)grdexp.Rows[index].FindControl("lbremove");
            string docid = grdexp.DataKeys[index].Values["docid"].ToString();
            TextBox txtremarks = (TextBox)grdexp.Rows[index].FindControl("txtboxremarksvalue");
            Label lblremarks = (Label)grdexp.Rows[index].FindControl("lblremarks");
            LinkButton lnkcancel = (LinkButton)grdexp.Rows[index].FindControl("lnkbtncancel");

            TextBox txtcurrentoffname = (TextBox)(grdexp.Rows[index].FindControl("txtcurrentoffname"));
            TextBox txtcurrentoffadd = (TextBox)(grdexp.Rows[index].FindControl("txtcurrentoffadd"));
            RadioButtonList rbtcentralorstate = (RadioButtonList)(grdexp.Rows[index].FindControl("rbtcentralorstate"));
            TextBox txtstategovtname = (TextBox)(grdexp.Rows[index].FindControl("txtstategovtname"));
            TextBox txtministryname = (TextBox)(grdexp.Rows[index].FindControl("txtministryname"));
            RadioButtonList rbtWheAutonomous = (RadioButtonList)(grdexp.Rows[index].FindControl("rbtWheAutonomous"));
            TextBox txtappointmentdate = (TextBox)(grdexp.Rows[index].FindControl("txtappointmentdate"));
            TextBox txtcurrentdesig = (TextBox)(grdexp.Rows[index].FindControl("txtcurrentdesig"));
            // txtcurrentoffname.Focus();
            string Wcentralorstate = grdexp.DataKeys[index].Values["Wcentralorstate"].ToString();
            string isAutonomous = grdexp.DataKeys[index].Values["isAutonomous"].ToString();
            Label lblministry = (Label)(grdexp.Rows[index].FindControl("lblministry"));
            Label lblstate = (Label)(grdexp.Rows[index].FindControl("lblstate"));

            Label lblcurrentoffname = (Label)(grdexp.Rows[index].FindControl("lblcurrentoffname"));
            Label lblcurrentoffadd = (Label)(grdexp.Rows[index].FindControl("lblcurrentoffadd"));
            Label lblcentralorstate = (Label)(grdexp.Rows[index].FindControl("lblcentralorstate"));
            Label lblorgstatename = (Label)(grdexp.Rows[index].FindControl("lblorgstatename"));
            Label lblorgministryname = (Label)(grdexp.Rows[index].FindControl("lblorgministryname"));
            Label lblWheAutonomous = (Label)(grdexp.Rows[index].FindControl("lblWheAutonomous"));
            Label lblappointmentdate = (Label)(grdexp.Rows[index].FindControl("lblappointmentdate"));
            Label lblcurrentdesig = (Label)(grdexp.Rows[index].FindControl("lblcurrentdesig"));
            Label lblstat = (Label)(grdexp.Rows[index].FindControl("lblstat"));
            Label lblmin = (Label)(grdexp.Rows[index].FindControl("lblmin"));
            rbtcentralorstate.SelectedValue = Wcentralorstate;
            rbtWheAutonomous.SelectedValue = isAutonomous;
            if (docid != "")
            {
                fileupload.Visible = true;
                lbupdate.Visible = true;
                hyviewdoc.Visible = false;
                //  lbsave.Visible = false;
                lbchange.Visible = false;
                // lbremove.Visible = false;
                lblremarks.Visible = false;

                // remarks1.Visible = true;
                txtremarks.Visible = true;
                lnkcancel.Visible = true;
                if (lblremarks.Text == "--")
                {
                    lblremarks.Text = "";
                }
                txtremarks.Text = lblremarks.Text;
                txtcurrentoffname.Visible = true;
                txtappointmentdate.Visible = true;
                txtcurrentdesig.Visible = true;
                txtcurrentoffadd.Visible = true;
                rbtcentralorstate.Visible = true;
                rbtWheAutonomous.Visible = true;

                lblcurrentoffname.Visible = false;
                lblcurrentoffadd.Visible = false;
                lblcentralorstate.Visible = false;
                lblorgstatename.Visible = false;
                lblorgministryname.Visible = false;
                lblWheAutonomous.Visible = false;
                lblappointmentdate.Visible = false;
                lblcurrentdesig.Visible = false;
                lblstat.Visible = false;
                lblmin.Visible = false;
                if (Wcentralorstate == "central")
                {
                    txtministryname.Visible = true;
                    txtstategovtname.Visible = false;
                    lblministry.Visible = true;
                    lblstate.Visible = false;
                }
                else
                {
                    txtministryname.Visible = false;
                    txtstategovtname.Visible = true;
                    lblministry.Visible = false;
                    lblstate.Visible = true;
                }

            }


        }
    }

    protected void grdexp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string docid = grdexp.DataKeys[e.Row.RowIndex].Values["docid"].ToString();
            string editflag = grdexp.DataKeys[e.Row.RowIndex].Values["editflag"].ToString();
            string Wcentralorstate = grdexp.DataKeys[e.Row.RowIndex].Values["Wcentralorstate"].ToString();
            HyperLink hyviewdoc = (HyperLink)e.Row.FindControl("hyviewdoc");
            FileUpload fileupload = (FileUpload)e.Row.FindControl("fileupload");
            //   LinkButton lbsave = (LinkButton)e.Row.FindControl("lbsave");
            LinkButton lbchange = (LinkButton)e.Row.FindControl("lbchange");
            // LinkButton lbremove = (LinkButton)e.Row.FindControl("lbremove");
            string url = md5util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(docid, true));
            hyviewdoc.NavigateUrl = url;
            Label lblorgstatename = (Label)e.Row.FindControl("lblorgstatename");
            Label lblorgministryname = (Label)e.Row.FindControl("lblorgministryname");
            Label lblstat = (Label)e.Row.FindControl("lblstat");
            Label lblmin = (Label)e.Row.FindControl("lblmin");
            if (docid != "")
            {
                hyviewdoc.Visible = true;
                fileupload.Visible = false;
                lbchange.Visible = true;
                //lbremove.Visible = true;
                // lbsave.Visible = false;
            }
            else
            {
                hyviewdoc.Visible = false;
                fileupload.Visible = true;
                lbchange.Visible = false;
                // lbsave.Visible = true;
                // lbremove.Visible = false;
            }
            if (editflag == "Y")
            {
                lbchange.Visible = false;
                // lbremove.Visible = false;
            }
            else
            {
                lbchange.Visible = true;
                // lbremove.Visible = true;
            }
            if (Wcentralorstate == "central")
            {
                lblorgministryname.Visible = true;
                lblmin.Visible = true;
                lblstat.Visible = false;
                lblorgstatename.Visible = false;
            }
            else
            {
                lblorgministryname.Visible = false;
                lblmin.Visible = false;
                lblstat.Visible = true;
                lblorgstatename.Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //LinkButton lnkadd = (LinkButton)e.Row.FindControl("lnkadd");
            //DataTable dtchkexp = objCandD.GetEdossierMaster_expdoc(jid);
            //string edmid = "";
            //if (dtchkexp.Rows.Count > 0)
            //{
            //    edmid = dtchkexp.Rows[0]["edmid"].ToString();

            //}
           
            // DataTable dtexp = objCandD.GetEdossierMaster_misc(edmid, applid);
            //if (dtexp.Rows.Count < 7)
            //{
            //    lnkadd.Visible = true;
            //}
            //else
            //{
            //    lnkadd.Visible = false;
            //}


        }
    }

    protected void grdexp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //DataTable dtchkexp = objCandD.GetEdossierMaster_expdoc(jid);
        //string edmid = "";
        //if (dtchkexp.Rows.Count > 0)
        //{
        //    edmid = dtchkexp.Rows[0]["edmid"].ToString();

        //}
        grdexp.EditIndex = -1;
        FillGrid_exp(hfedmid.Value);
    }

    protected void grdexp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string docid = grdexp.DataKeys[index].Values["docid"].ToString();
        string id = grdexp.DataKeys[index].Values["id"].ToString();
        string edmid = grdexp.DataKeys[index].Values["edmid"].ToString();
        string IP = GetIPAddress();
        TextBox txtcurrentoffname = (TextBox)(grdexp.Rows[index].FindControl("txtcurrentoffname"));
        TextBox txtcurrentoffadd = (TextBox)(grdexp.Rows[index].FindControl("txtcurrentoffadd"));
        RadioButtonList rbtcentralorstate = (RadioButtonList)(grdexp.Rows[index].FindControl("rbtcentralorstate"));
        TextBox txtstategovtname = (TextBox)(grdexp.Rows[index].FindControl("txtstategovtname"));
        TextBox txtministryname = (TextBox)(grdexp.Rows[index].FindControl("txtministryname"));
        RadioButtonList rbtWheAutonomous = (RadioButtonList)(grdexp.Rows[index].FindControl("rbtWheAutonomous"));
        TextBox txtappointmentdate = (TextBox)(grdexp.Rows[index].FindControl("txtappointmentdate"));
        TextBox txtcurrentdesig = (TextBox)(grdexp.Rows[index].FindControl("txtcurrentdesig"));


        FileUpload fileupload = (FileUpload)grdexp.Rows[index].FindControl("fileupload");
        TextBox txtremarks = (TextBox)grdexp.Rows[index].FindControl("txtboxremarksvalue");
        if (fileupload.PostedFile != null && fileupload.PostedFile.FileName != "")
        {
            try
            {
                if (rbtcentralorstate.SelectedValue == "central")
                {
                    txtstategovtname.Text = "";
                }
                else
                {
                    txtministryname.Text = "";
                }
                   DataTable dtchkexpDetail = objCandD.CheckExpDetails(edid, txtcurrentoffname.Text,Utility.formatDate( txtappointmentdate.Text), subcatcode);
                   if (dtchkexpDetail.Rows.Count > 0)
                   {
                       msg.Show("You already entered this record ");
                   }
                   else
                   {
                       int temp3 = objCandD.updateEDDeptcandsubcatDetails(Convert.ToInt32(id), subcatcode, txtcurrentoffname.Text, txtcurrentoffadd.Text, rbtcentralorstate.SelectedValue, txtstategovtname.Text, txtministryname.Text, rbtWheAutonomous.SelectedValue, Utility.formatDate(txtappointmentdate.Text), txtcurrentdesig.Text, IP);
                       int i = uploadfile("", fileupload, "U", docid, "", "", "", txtremarks.Text.Trim());
                       grdexp.EditIndex = -1;
                       FillGrid_exp(edmid);
                   }
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
    //protected void grdmisc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int index = e.RowIndex;
    //    string edid = grdexp.DataKeys[index].Values["docid"].ToString();
    //    string edmid = grdexp.DataKeys[index].Values["edmid"].ToString();
    //    try
    //    {
    //        int temp = objCandD.delete_candidateedossier(edid);
    //        if (temp > 0)
    //        {
    //            grdexp.EditIndex = -1;
    //            FillGrid_exp(edmid);
    //            msg.Show("Record Deleted successfully");


    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("ErrorPage.aspx");
    //    }
    //}

    protected void btnnext_Click(object sender, EventArgs e)
    {
        if (grdexp.Rows.Count > 0)
        {
            string url = md5util.CreateTamperProofURL("Edossier_qualiinfo.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
            Server.Transfer(url);
        }
        else
        {
            msg.Show("Please enter experience details");
            
        }
    }
    protected void rbtcentralorstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rbtcentralorstate = sender as RadioButtonList;
        GridViewRow Row = rbtcentralorstate.NamingContainer as GridViewRow;
        TextBox txtstategovtname = Row.FindControl("txtstategovtname") as TextBox;
        TextBox txtministryname = Row.FindControl("txtministryname") as TextBox;
        Label lblministry = Row.FindControl("lblministry") as Label;
        Label lblstate = Row.FindControl("lblstate") as Label;
        if (rbtcentralorstate.SelectedValue == "central")
        {
            txtministryname.Visible = true;
            txtstategovtname.Visible = false;
            lblministry.Visible = true;
            lblstate.Visible = false;
        }
        else
        {
            txtministryname.Visible = false;
            txtstategovtname.Visible = true;
            lblministry.Visible = false;
            lblstate.Visible = true;
        }
    }
    protected void rbtcentralorstate_SelectedIndexChanged1(object sender, EventArgs e)
    {
        RadioButtonList rbtcentralorstate = sender as RadioButtonList;
        GridViewRow Row = rbtcentralorstate.NamingContainer as GridViewRow;
        TextBox txtstategovtname = Row.FindControl("txtstategovtname") as TextBox;
        TextBox txtministryname = Row.FindControl("txtministryname") as TextBox;
        Label lblministry = Row.FindControl("lblministry") as Label;
        Label lblstate = Row.FindControl("lblstate") as Label;
        if (rbtcentralorstate.SelectedValue == "central")
        {
            txtministryname.Visible = true;
            txtstategovtname.Visible = false;
            lblministry.Visible = true;
            lblstate.Visible = false;
        }
        else
        {
            txtministryname.Visible = false;
            txtstategovtname.Visible = true;
            lblministry.Visible = false;
            lblstate.Visible = true;
        }
    }
}