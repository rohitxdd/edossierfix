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

public partial class Edossier_catinfo : BasePage
{
    DataTable dt = new DataTable();
    CandidateData objCandD = new CandidateData();
    message msg = new message();
    MD5Util md5util = new MD5Util();
    string jid = "", edid = "", rollno = "", post = "", applid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
        edid = MD5Util.Decrypt(Request.QueryString["edid"].ToString(), true);
        rollno = MD5Util.Decrypt(Request.QueryString["rollno"].ToString(), true);
        post = MD5Util.Decrypt(Request.QueryString["post"].ToString(), true);
        applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        if (!IsPostBack)
        {
            fill_application_data(jid, Session["rid"].ToString());
        }
    }
    public void fill_application_data(string jid, string regno)
    {
        try
        {
            DataTable dtedcat = objCandD.getedcatsubcatdetails(edid);
            if (dtedcat.Rows.Count == 0)
            {
                string IP = GetIPAddress();
                dt = objCandD.getcatsubcatdetailsforedossier(jid, regno);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["category"].ToString() != "UR")
                    {
                        lblcatcertno.Text = "i) " + dt.Rows[0]["category"].ToString() + " Certificate Number";
                        lblcatcertissuedate.Text = "ii) Date of issuance of " + dt.Rows[0]["category"].ToString() + " Certificate";
                        if (dt.Rows[0]["clcdate"].ToString() != null && dt.Rows[0]["clcdate"].ToString() != "")
                        { txtcatcertissuedate.Text = dt.Rows[0]["clcdate"].ToString(); }
                        else
                        {
                        }
                        txtcatcertno.Text = dt.Rows[0]["CLCNo"].ToString();
                        
                        txtcatcertissuedesig.Text = dt.Rows[0]["CastCertIssueAuth"].ToString();
                        if (txtcatcertissuestate.Text == "Delhi")
                        {
                            fillddldistrict(ddlcatcertissuetehsil);
                        }
                        else
                        {
                        }
                        txtcatcertissuestate.Text = dt.Rows[0]["state"].ToString();
                        int tempcat = objCandD.InsertEDCatSubcat(int.Parse(edid), dt.Rows[0]["category"].ToString(), "", IP, "", txtcatcertno.Text, Utility.formatDate(txtcatcertissuedate.Text), txtcatcertissuedesig.Text, ddlcatcertissuetehsil.SelectedValue, txtcatcertissuestate.Text, txtcatcertissuetehsil.Text);
                        FillGrid_Category(jid, applid);
                    }
                    if (dt.Rows[0]["SubCategory"].ToString() != "")
                    {
                        DataTable dtsubcat = objCandD.getcandsubcategory(applid);
                        for (int i = 0; i < dtsubcat.Rows.Count; i++)
                        {
                            if (dtsubcat.Rows[i]["SubCat_code"].ToString() != "DC" && dtsubcat.Rows[i]["SubCat_code"].ToString() != "DGS")
                            {
                                int tempsubcat = objCandD.InsertEDCatSubcat(int.Parse(edid), "", dtsubcat.Rows[i]["SubCat_code"].ToString(), IP, "", "", "", "", "", "", "");
                            }
                            else
                            {
                                hfsubcatcode.Value = dtsubcat.Rows[i]["SubCat_code"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    msg.Show("Data Not Found");
                }
            }
            FillGrid_SubCategory(jid, applid);

            lblpostcode.Text = post;
            lblrollno.Text = rollno;
            filleddata();
            checkforediting();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void checkforediting()
    {
        DataTable dtget = objCandD.Getedossiersfinal(applid);
        DataTable dt1 = objCandD.check_post_foruploadeDossier(Session["rid"].ToString(), jid);
        if (dtget.Rows.Count > 0)
        {
            hffinal.Value = "Y";
        }
        else
        {
            hffinal.Value = "N";
        }
        if (dt1.Rows.Count > 0)
        {
            hfschedule.Value = "Y";
        }
        else
        {
            hfschedule.Value = "N";
        }

        if (dt1.Rows.Count > 0 && dtget.Rows.Count == 0)
        {
            grdcat.Columns[5].Visible = true;
            grdcat.Columns[6].Visible = true;
            grdsubcat.Columns[5].Visible = true;
            btnnext.Visible = false;
            btnsave.Visible = true;
            txtcatcertno.Enabled = true;
            txtcatcertissuedate.Enabled = true;
            txtcatcertissuedesig.Enabled = true;
            ddlcatcertissuetehsil.Enabled = true;
            txtcatcertissuetehsil.Enabled = true;
            txtcatcertissuestate.Enabled = true;
            txtphcertno.Enabled = true;
            txtphcertissuedate.Enabled = true;
            txtphissuedesig.Enabled = true;
            txtphissuemedinst.Enabled = true;
            txtphcertissuetehsil.Enabled = true;
            txtphcertissuestate.Enabled = true;
            txtdefservjoindate.Enabled = true;
            txtdefservdischargedate.Enabled = true;
            txtlenofdefserv.Enabled = true;
            txtdischargereason.Enabled = true;
            txtdischargerank.Enabled = true;
            txtdischargeunitname.Enabled = true;
            txtdischargeunitadd.Enabled = true;
        }
        else
        {
            grdcat.Columns[5].Visible = false;
            grdcat.Columns[6].Visible = false;
            grdsubcat.Columns[5].Visible = false;
            btnnext.Visible = true;
            btnsave.Visible = false;
            txtcatcertno.Enabled = false;
            txtcatcertissuedate.Enabled = false;
            txtcatcertissuedesig.Enabled = false;
            ddlcatcertissuetehsil.Enabled = false;
            txtcatcertissuetehsil.Enabled = false;
            txtcatcertissuestate.Enabled = false;
            txtphcertno.Enabled = false;
            txtphcertissuedate.Enabled = false;
            txtphissuedesig.Enabled = false;
            txtphissuemedinst.Enabled = false;
            txtphcertissuetehsil.Enabled = false;
            txtphcertissuestate.Enabled = false;
            txtdefservjoindate.Enabled = false;
            txtdefservdischargedate.Enabled = false;
            txtlenofdefserv.Enabled = false;
            txtdischargereason.Enabled = false;
            txtdischargerank.Enabled = false;
            txtdischargeunitname.Enabled = false;
            txtdischargeunitadd.Enabled = false;
        }


    }
    private void filleddata()
    {
        DataTable dtedcat = objCandD.getedcatsubcatdetails(edid);

        string subcat = "";
        if (dtedcat.Rows.Count > 0)
        {
            for (int i = 0; i < dtedcat.Rows.Count; i++)
            {
                if (dtedcat.Rows[i]["catcode"].ToString() != "")
                {
                    lblcat1.Text = dtedcat.Rows[i]["catcode"].ToString();
                    if (dtedcat.Rows[i]["catcode"].ToString() == "UR")
                    {
                        trcat.Visible = false;
                        trcategory.Visible = false;
                        // trstate.Visible = false;
                    }
                    else
                    {

                        if (dtedcat.Rows[i]["catcode"].ToString() == "OBC")
                        {
                            //chkfobc.Checked = true;
                            if (!chkfobc.Checked)
                            {
                                trcatf.Visible = true;
                                lblcatcertno_f.Text = "i) " + dtedcat.Rows[i]["catcode"].ToString() + " Certificate Number";
                                lblcatcertissuedate_f.Text = "ii) Date of issuance of " + dtedcat.Rows[i]["catcode"].ToString() + " Certificate";
                                // txtcatcertissuestate_f.Text = "Delhi";
                                DataTable dtfather = objCandD.getfathercertdetails(edid);
                                if (dtfather.Rows.Count > 0)
                                {
                                    hffcdid.Value = dtfather.Rows[0]["fcdid"].ToString();
                                    ddlOBCForM.SelectedValue = dtfather.Rows[0]["certfatherormother"].ToString();
                                    txtcatcertno_f.Text = dtfather.Rows[0]["certificateno_father"].ToString();
                                    txtcatcertissuedate_f.Text = dtfather.Rows[0]["certissuedate_father"].ToString();
                                    txtcatcertissuedesig_f.Text = dtfather.Rows[0]["certissuedesig_father"].ToString();
                                    txtcatcertissuetehsil_f.Text = dtfather.Rows[0]["certissuedist_father"].ToString();
                                    string obcfatherdocid = dtfather.Rows[0]["docid"].ToString();
                                    hfobcfdocid.Value = obcfatherdocid;
                                    string url = md5util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(obcfatherdocid, true));
                                    hyviewdoc.NavigateUrl = url;
                                    hyviewdoc.Visible = true;
                                    lblviewcert.Visible = true;
                                    txtfremarks.Text = dtfather.Rows[0]["remarks"].ToString();
                                }
                            }
                            else
                            {
                                trcatf.Visible = false;
                            }
                            fillddldistrict(ddlcatcertissuetehsil);
                            fillddldistrict(ddlcatcertissuetehsil_f);
                            fillddlstate(ddlcatcertissuestate_f);
                            trchkfobc.Visible = true;
                        }
                        else
                        {
                            trchkfobc.Visible = false;
                        }
                        trcat.Visible = true;

                        fillddldistrict(ddlcatcertissuetehsil);
                        trcategory.Visible = true;
                        lblcatcertno.Text = "i) " + dtedcat.Rows[i]["catcode"].ToString() + " Certificate Number";
                        lblcatcertissuedate.Text = "ii) Date of issuance of " + dtedcat.Rows[i]["catcode"].ToString() + " Certificate";
                        txtcatcertno.Text = dtedcat.Rows[i]["certificateno"].ToString();
                        txtcatcertissuedate.Text = dtedcat.Rows[i]["certissuedate"].ToString();
                        txtcatcertissuedesig.Text = dtedcat.Rows[i]["certissuedesig"].ToString();
                        if (dtedcat.Rows[i]["certissuestate"].ToString() == "Delhi")
                        {
                            ddlcatcertissuetehsil.SelectedValue = dtedcat.Rows[i]["certissuedist"].ToString();
                            ddlcatcertissuetehsil.Visible = true;
                            txtcatcertissuetehsil.Visible = false;
                        }
                        else
                        {
                            txtcatcertissuetehsil.Text = dtedcat.Rows[i]["certissuedistoutsidedelhi"].ToString();
                            ddlcatcertissuetehsil.Visible = false;
                            txtcatcertissuetehsil.Visible = true;
                        }
                        txtcatcertissuestate.Text = dtedcat.Rows[i]["certissuestate"].ToString();
                        hfidcat.Value = dtedcat.Rows[i]["id"].ToString();

                        FillGrid_Category(jid, applid);
                    }
                }
                if (dtedcat.Rows[i]["subcatcode"].ToString() != "" && dtedcat.Rows[i]["subcatcode"].ToString() != "DC" && dtedcat.Rows[i]["subcatcode"].ToString() != "DGS")
                {
                    subcat = subcat + dtedcat.Rows[i]["subcatcode"].ToString() + ',';
                }
                if (dtedcat.Rows[i]["subcatcode"].ToString() == "PH")
                {
                    trph.Visible = true;
                    txtphcertno.Text = dtedcat.Rows[i]["certificateno"].ToString();
                    txtphcertissuedate.Text = dtedcat.Rows[i]["certissuedate"].ToString();
                    txtphissuedesig.Text = dtedcat.Rows[i]["certissuedesig"].ToString();
                    txtphissuemedinst.Text = dtedcat.Rows[i]["certissueMInst"].ToString();
                    txtphcertissuetehsil.Text = dtedcat.Rows[i]["certissuedistoutsidedelhi"].ToString();
                    txtphcertissuestate.Text = dtedcat.Rows[i]["certissuestate"].ToString();
                    hfidph.Value = dtedcat.Rows[i]["id"].ToString();
                }
                if (dtedcat.Rows[i]["subcatcode"].ToString() == "EX")
                {
                    trexsm.Visible = true;
                    txtdefservjoindate.Text = dtedcat.Rows[i]["Defservjoindate"].ToString();
                    txtdefservdischargedate.Text = dtedcat.Rows[i]["defservdiscdate"].ToString();
                    txtlenofdefserv.Text = dtedcat.Rows[i]["tlendefserv"].ToString();
                    txtdischargereason.Text = dtedcat.Rows[i]["discreason"].ToString();
                    txtdischargerank.Text = dtedcat.Rows[i]["discrank"].ToString();
                    txtdischargeunitname.Text = dtedcat.Rows[i]["discoffname"].ToString();
                    txtdischargeunitadd.Text = dtedcat.Rows[i]["discoffaddress"].ToString();
                    hfidex.Value = dtedcat.Rows[i]["id"].ToString();
                }
                if (dtedcat.Rows[i]["subcatcode"].ToString() == "DC" || dtedcat.Rows[i]["subcatcode"].ToString() == "DGS")
                {
                    hfsubcatcode.Value = dtedcat.Rows[i]["subcatcode"].ToString();
                }

            }


        }
        dt = objCandD.getcatsubcatdetailsforedossier(jid, Session["rid"].ToString());
        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["SubCategory"].ToString() != "")
            {
                DataTable dtsubcat = objCandD.getcandsubcategory(applid);
                for (int i = 0; i < dtsubcat.Rows.Count; i++)
                {
                    if (dtsubcat.Rows[i]["SubCat_code"].ToString() == "DC" || dtsubcat.Rows[i]["SubCat_code"].ToString() == "DGS")
                    {
                        hfsubcatcode.Value = dtsubcat.Rows[i]["SubCat_code"].ToString();
                    }

                }
            }
        }

        // }
        if (subcat == "")
        {
            subcat = "--";
            lblsubcat.Visible = false;
            lblsubcat1.Visible = false;
        }
        else
        {
            lblsubcat.Visible = true;
            lblsubcat1.Visible = true;
            subcat = subcat.Substring(0, (subcat.Length - 1));
        }
        lblsubcat1.Text = subcat;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string IP = GetIPAddress();
        if (lblcat1.Text != "UR" && lblcat1.Text != "")
        {
            //if (lblcat1.Text == "OBC" && rbtobcregion.SelectedValue == "")
            //{
            //    msg.Show("Please Select OBC State (Delhi/Outside)");
            //    return;
            //}
            int temp = 0;
            if (txtcatcertno.Text == "" || txtcatcertissuedate.Text == "" || txtcatcertissuedesig.Text == "" || txtcatcertissuestate.Text == "" || (txtcatcertissuetehsil.Text == "" && ddlcatcertissuetehsil.SelectedValue == ""))
            {
                msg.Show("Please enter all Details");
            }
            else
            {
                temp = objCandD.UpdateEDCatDetails(Convert.ToInt32(hfidcat.Value), lblcat1.Text, txtcatcertno.Text, Utility.formatDate(txtcatcertissuedate.Text), txtcatcertissuedesig.Text, ddlcatcertissuetehsil.SelectedValue, txtcatcertissuestate.Text, IP, txtcatcertissuetehsil.Text);

                if (temp > 0 && txtcatcertno_f.Text != "")
                {
                    if (hffcdid.Value == "")
                    {
                        temp = objCandD.insertfathercertdetails(Convert.ToInt32(edid), txtcatcertno_f.Text, txtcatcertissuedate_f.Text, txtcatcertissuedesig_f.Text, ddlcatcertissuetehsil_f.SelectedValue, ddlcatcertissuestate_f.Text, IP, ddlOBCForM.SelectedValue, txtcatcertissuetehsil_f.Text);
                    }
                    else
                    {
                        temp = objCandD.updatefathercertdetails(Convert.ToInt32(hffcdid.Value), txtcatcertno_f.Text, txtcatcertissuedate_f.Text, txtcatcertissuedesig_f.Text, ddlcatcertissuetehsil_f.SelectedValue, ddlcatcertissuestate_f.Text, ddlOBCForM.SelectedValue, txtcatcertissuetehsil_f.Text);
                    }
                    if (temp > 0 && !chkfobc.Checked)
                    {
                        string edmid = grdcat.DataKeys[0].Values["edmid"].ToString();

                        if (fufathercert.PostedFile.ContentLength == 0)
                        {
                            //msg.Show("Please browse the document");
                        }
                        else
                        {
                            if (hfobcfdocid.Value == "")
                            {
                                int documentid = uploadfile(edmid, fufathercert, "I", "", "", "", "", txtfremarks.Text);
                                objCandD.Updatewfatherdocid(documentid);
                            }
                            else
                            {
                                uploadfile(edmid, fufathercert, "U", hfobcfdocid.Value, "", "", "", txtfremarks.Text);
                            }

                        }
                    }
                }
            }
        }
        if (lblsubcat1.Text != "" && lblsubcat1.Text != "--")
        {
            string[] a = { "," };
            string[] subcat = lblsubcat1.Text.Split(a, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < subcat.Length; i++)
            {
                if (subcat[i] == "PH")
                {
                    if (txtphcertno.Text == "" || txtphcertissuedate.Text == "" || txtphissuedesig.Text == "" || txtphcertissuetehsil.Text == "" || txtphcertissuestate.Text == "" || txtphissuemedinst.Text == "")
                    {
                        msg.Show("Please enter all Details");
                    }
                    else
                    {
                        int temp1 = objCandD.updateEDphsubcatDetails(Convert.ToInt32(hfidph.Value), subcat[i], txtphcertno.Text, Utility.formatDate(txtphcertissuedate.Text), txtphissuedesig.Text, txtphcertissuetehsil.Text, txtphcertissuestate.Text, IP, txtphissuemedinst.Text);
                    }
                }
                if (subcat[i] == "EX")
                {
                    if (txtdefservjoindate.Text == "" || txtdefservdischargedate.Text == "" || txtlenofdefserv.Text == "" || txtdischargereason.Text == "" || txtdischargerank.Text == "" || txtdischargeunitname.Text == "" || txtdischargeunitadd.Text == "")
                    {
                        msg.Show("Please enter all Details");
                    }
                    else
                    {
                        int temp2 = objCandD.updateEDEXsubcatDetails(Convert.ToInt32(hfidex.Value), subcat[i], Utility.formatDate(txtdefservjoindate.Text), Utility.formatDate(txtdefservdischargedate.Text), txtlenofdefserv.Text, txtdischargereason.Text, txtdischargerank.Text, txtdischargeunitname.Text, txtdischargeunitadd.Text, IP);
                    }
                }
                //if (subcat[i] == "DC")
                //{
                //    int temp3 = objCandD.updateEDDeptcandsubcatDetails(Convert.ToInt32(hfiddc.Value), subcat[i], txtcurrentoffname.Text, txtcurrentoffadd.Text, rbtcentralorstate.SelectedValue, txtstategovtname.Text, txtministryname.Text, rbtWheAutonomous.SelectedValue, Utility.formatDate(txtappointmentdate.Text), txtcurrentdesig.Text, IP);
                //}
            }
        }

        string url = "";
        if (hfsubcatcode.Value == "DC" || hfsubcatcode.Value == "DGS")
        {
            url = md5util.CreateTamperProofURL("EdossierExpDetails.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true) + "&subcatcode=" + MD5Util.Encrypt(hfsubcatcode.Value, true));
        }
        else
        {
            url = md5util.CreateTamperProofURL("Edossier_qualiinfo.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
        }
        Server.Transfer(url);
        // Response.Redirect(url);
    }
    private void FillGrid_Category(string jid, string applid)
    {
        try
        {
            DataTable dtcat = new DataTable();

            dtcat = objCandD.GetEdossierMaster_cat(jid, applid, "C");
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
            FillGrid_Category(jid, applid);
        }
    }
    protected void grdcat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdcat.EditIndex = e.NewEditIndex;
        FillGrid_Category(jid, applid);

        grdcat.EditIndex = -1;
        FillGrid_Category(jid, applid);
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
            LinkButton lbupdate = (LinkButton)e.Row.FindControl("lbupdate");
            TextBox txtboxremarksvalue = (TextBox)e.Row.FindControl("txtboxremarksvalue");
            Label lblremarks = (Label)e.Row.FindControl("lblremarks");
            string url = md5util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
            hyviewdoc.NavigateUrl = url;
            if (edid != "")
            {
                hyviewdoc.Visible = true;
                fileupload.Visible = false;
                lbchange.Visible = true;
                lbsave.Visible = false;
                lblremarks.Visible = true;
                txtboxremarksvalue.Visible = false;
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
                FillGrid_Category(jid, applid);
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
    private int uploadfile(string edmid, FileUpload fileupload, string function, string edid, string adharno, string subcat, string othermiscdoc, string remarks)
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
                            int k = objCandD.UpdateCandidateEdossier(edid, imageSize, adharno, ipaddress, Session["rid"].ToString(), remarks);
                            if (k > 0)
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
    private void FillGrid_SubCategory(string jid, string applid)
    {
        try
        {
            DataTable dtsubcat = new DataTable();
            dtsubcat = objCandD.GetEdossierMaster_subcat(jid, applid, "S");
            if (dtsubcat.Rows.Count > 0)
            {
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
                DataTable dtscat = objCandD.getcandsubcategory(applid);
                for (int i = 0; i < dtscat.Rows.Count; i++)
                {
                    string subcategory = dtscat.Rows[i]["SubCat_code"].ToString();
                    if (subcategory != "DC" && subcategory != "DGS")
                    {
                        // string subcatname = objCandD.GetSubcategory(scVal[i]);
                        dr = dtresult.NewRow();

                        dr["edmid"] = dtsubcat.Rows[0][0].ToString();
                        dr["jid"] = dtsubcat.Rows[0][1].ToString();
                        dr["certificateReq"] = dtsubcat.Rows[0][2].ToString();
                        dr["ctype"] = dtsubcat.Rows[0][3].ToString();
                        dr["priority"] = dtsubcat.Rows[0][4].ToString();
                        dr["final"] = dtsubcat.Rows[0][5].ToString();
                        dr["ctypename"] = dtsubcat.Rows[0][7].ToString();
                        dr["subcategory"] = subcategory;
                        dr["subcatname"] = dtscat.Rows[i]["SubCat_name"].ToString();
                        dr["remarks"] = dtsubcat.Rows[0][10].ToString();
                        for (int j = i; j < dtsubcat.Rows.Count; j++)
                        {

                            if (subcategory == dtsubcat.Rows[j][9].ToString())
                            {
                                dr["edid"] = dtsubcat.Rows[j][6].ToString();
                                dr["subcat"] = dtsubcat.Rows[j][9].ToString();
                            }
                        }
                        if (dtsubcat.Rows[0][9].ToString() == "")
                        {
                            //dr["edid"] = "";
                        }

                        dtresult.Rows.Add(dr);
                    }
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
            FillGrid_SubCategory(jid, applid);
        }
    }
    protected void grdsubcat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdsubcat.EditIndex = e.NewEditIndex;
        FillGrid_SubCategory(jid, applid);

        grdsubcat.EditIndex = -1;
        FillGrid_SubCategory(jid, applid);
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
            string url = md5util.CreateTamperProofURL("viewcertificate.aspx", null, "edid=" + MD5Util.Encrypt(edid, true));
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
                FillGrid_SubCategory(jid, applid);
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
    protected void grdcat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdcat.EditIndex = -1;
        FillGrid_Category(jid, applid);
    }
    protected void grdsubcat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdsubcat.EditIndex = -1;
        FillGrid_SubCategory(jid, applid);
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        string url = "";
        if (hfsubcatcode.Value == "DC" || hfsubcatcode.Value == "DGS")
        {
            url = md5util.CreateTamperProofURL("EdossierExpDetails.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true) + "&subcatcode=" + MD5Util.Encrypt(hfsubcatcode.Value, true));
        }
        else
        {
            url = md5util.CreateTamperProofURL("Edossier_qualiinfo.aspx", null, "jid=" + MD5Util.Encrypt(jid, true) + "&edid=" + MD5Util.Encrypt(edid, true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
        }
        Server.Transfer(url);
    }
    private void fillddldistrict(DropDownList ddlcatcertissuetehsil)
    {
        try
        {
            DataTable dtdist = objCandD.GetEdossierdistict();
            ddlcatcertissuetehsil.DataSource = dtdist;
            ddlcatcertissuetehsil.DataTextField = "distname_e";
            ddlcatcertissuetehsil.DataValueField = "distid";
            ddlcatcertissuetehsil.DataBind();
            ddlcatcertissuetehsil.Items.Insert(0, Utility.ddl_Select_Value());
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void fillddlstate(DropDownList ddlstate)
    {
        try
        {
            DataTable dtdist = objCandD.GetEdossierstate();
            ddlstate.DataSource = dtdist;
            ddlstate.DataTextField = "state";
            ddlstate.DataValueField = "code";
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, Utility.ddl_Select_Value());
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void ddlcatcertissuestate_f_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcatcertissuestate_f.SelectedValue == "7")
        {
            fillddldistrict(ddlcatcertissuetehsil_f);
            ddlcatcertissuetehsil_f.Visible = true;
            txtcatcertissuetehsil_f.Visible = false;
        }
        else
        {
            ddlcatcertissuetehsil_f.Visible = false;
            txtcatcertissuetehsil_f.Visible = true;
        }
    }
    protected void chkfobc_CheckedChanged(object sender, EventArgs e)
    {
        if (chkfobc.Checked)
        {
            trcatf.Visible = false;
        }
        else
        {
            trcatf.Visible = true;
        }
    }
}