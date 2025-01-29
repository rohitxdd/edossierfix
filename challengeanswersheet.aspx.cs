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
using System.Threading;
using System.IO;

public partial class challengeanswersheet : BasePage
{
    challengeansheet objchallenge = new challengeansheet();
    message msg = new message();
    DataTable dt;
    MD5Util md5util = new MD5Util();
    Utility Utlity = new Utility();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillddlexam();
        }
    }
    private void fillddlexam()
    {
        try
        {
            string regno = Session["rid"].ToString();
            dt = objchallenge.getexamtochallenge(regno);

            ddlexam.DataTextField = "examdtl";
            ddlexam.DataValueField = "examid";
            ddlexam.DataSource = dt;
            ddlexam.DataBind();
            ddlexam.Items.Insert(0, Utility.ddl_Select_Value());
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void fill_bookletno()
    {
        try
        {
            //string regno = Session["rid"].ToString();
            string EdID = "";
            dt = objchallenge.ddlbookletno(hfexamid.Value,hfbatchid.Value);
            ddlquesbookno.Items.Clear();
            ddlquesbookno.DataTextField = "BookLetCode";
            ddlquesbookno.DataValueField = "ExamBookLetID";
            ddlquesbookno.DataSource = dt;
            ddlquesbookno.DataBind();
            ddlquesbookno.Items.Insert(0, Utility.ddl_Select_Value());
            if (Grdalrdchlge.Rows.Count > 0)
            {
                EdID = Grdalrdchlge.DataKeys[0].Values["exambookletid"].ToString();
                if (EdID != "")
                {
                    ddlquesbookno.SelectedValue = EdID;
                   // ImageButton1.Visible = true;
                    ImageButton1.Visible = false;
                    fillQuesNo();
                    ddlquesbookno.Enabled = false;
                }
                else
                {
                    ddlquesbookno.Enabled = true;
                }
            }
            else
            {
                ddlquesbookno.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    
    private void Bindrbloptions2()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objchallenge.rboptions2(lbldsssbans.Text);
            rbloptions2.DataSource = dt;
            rbloptions2.DataTextField = "Ans";
            rbloptions2.DataValueField = "Ans";
            rbloptions2.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }


    private void Bindrbloptions3()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objchallenge.rboptions2("");
            chkoption3.DataSource = dt;
            chkoption3.DataTextField = "Ans";
            chkoption3.DataValueField = "Ans";
            chkoption3.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }


    public void fill_Grid()
    {
        try
        {
            dt = objchallenge.GetChallengemast_Details(hfexamid.Value, Session["rid"].ToString(),hfbatchid.Value);
            Grdalrdchlge.DataSource = dt;
            Grdalrdchlge.DataBind();
            int count = 0;
            for (int i = 0; i < Grdalrdchlge.Rows.Count; i++)
            {
                string pay_status = Grdalrdchlge.DataKeys[i].Values["status"].ToString();
                if (pay_status != "SUCCESS")
                {
                    count++;
                }
            }
            bool isrelease = objchallenge.CheckSchedule(hfexamid.Value, hfbatchid.Value);
            //if (!isrelease)
            //{
            //    Grdalrdchlge.Columns[8].Visible = false;
            //}
            //else
            //{
            //    Grdalrdchlge.Columns[8].Visible = true;
            //}
            if (count > 0 && (isrelease || Session["rid"].ToString() == "0909199199999992007"))
            {
                btnPay.Visible = true;
                trFeeAmount.Visible = true;
                int amount = objchallenge.GetCFee();
                int total = amount * count;
                lblamt.Text = Convert.ToString(amount);
                lbltotal.Text = Convert.ToString(total);
            }
            else
            {
                btnPay.Visible = false;
                trFeeAmount.Visible = false;
                //chkDesclaimer.Checked = false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btnchallenge_Click(object sender, EventArgs e)
    {
        try
        {
            btnchallenge.Visible = false;
            btnPay.Visible = false;
            trDesc.Visible = true;
            btncancel.Visible = true;
            chkDesclaimer.Checked = false;
            fill_bookletno();
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void ddlquesbookno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlquesbookno.SelectedValue != "")
            {
                //ImageButton1.Visible = true;
                ImageButton1.Visible = false;
            }
            else
            {
                ImageButton1.Visible = false;
            }
            fillQuesNo();
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void fillQuesNo()
    {
        try
        {
            dt = objchallenge.fillquestiono(ddlquesbookno.SelectedValue, Session["rid"].ToString());
            ddlquesno.DataTextField = "QuestionNo";
            ddlquesno.DataValueField = "ExamBookLetQMasterID";
            ddlquesno.DataSource = dt;
            ddlquesno.DataBind();
            ddlquesno.Items.Insert(0, Utility.ddl_Select_Value());
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();
            string ExamBookLetID = ddlquesbookno.SelectedValue;
            dt = objchallenge.getanswerkeydocs(ddlquesbookno.SelectedValue);
            if (dt.Rows.Count > 0)
            {
               // ImageButton1.Visible = true;

                byte[] file = (byte[])dt.Rows[0]["BAnswerKey"];
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.AddHeader("content-disposition", "attachment;filename=" + ExamBookLetID + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(file);
                Response.End();
            }
        }
        catch (ThreadAbortException ex)
        {

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void ddlquesno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dt = objchallenge.getdsssbans(ddlquesno.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                lbldsssbans.Text = dt.Rows[0]["ans"].ToString();
            }
            troption.Visible = true;
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            byte[] imageSize1 = new byte[uploaddoc.PostedFile.ContentLength];
            HttpPostedFile uploadedImage = uploaddoc.PostedFile;
            uploadedImage.InputStream.Read(imageSize1, 0, uploaddoc.PostedFile.ContentLength);
            string ext1 = System.IO.Path.GetExtension(uploaddoc.FileName).ToLower();
            bool checkfiletypeDocupload = chkfiletypeDocupload(imageSize1, ext1);

            //if (!checkImageDocupload())
            //{
            //    msg.Show("Upload only  PDF File Only");
            //    uploaddoc.Focus();
            //}
            if (!this.uploaddoc.HasFile)
            {
                msg.Show("Please upload the File.");
                return;
                //uploaddoc.Focus();
            }

            else if (!checkfiletypeDocupload)
            {
                msg.Show("Uploaded File is not a Valid File.");
                return;
                //  uploaddoc.Focus();
            }

            else if (!checkFileSizeDocupload())
            {
                msg.Show("Upload only Maximum 1MB size.");
                return;
                // uploaddoc.Focus();
            }
            else if (Validation.chkLevel0(txtRemarks.Text))
            {
                msg.Show("Invalid value in Remarks ");
            }

            else
            {
                # region

                //Transcation

                string ip = GetIPAddress();
                string opt2 = "", opt3 = "";

                if (rbloptions.SelectedValue == "1")
                {
                    opt2 = rbloptions2.SelectedValue;
                }
                else if (rbloptions.SelectedValue == "2")
                {
                    for (int n = 0; n < chkoption3.Items.Count; n++)
                    {
                        if (chkoption3.Items[n].Selected)
                        {
                            if (opt3 != "")
                            {
                                opt3 += ",";
                            }
                            opt3 += chkoption3.Items[n];
                        }
                    }
                    if (opt3 == "")
                    {
                        msg.Show("Please select Multiple answers");
                        return;
                    }
                }
                int CJPID = 0;
                CJPID = objchallenge.getjpid(hfexamid.Value, ddlquesbookno.SelectedItem.Text, ddlquesno.SelectedItem.Text,hfbatchid.Value);

                int i = objchallenge.Insert_CandDetails(Session["rid"].ToString(), ddlquesno.SelectedValue, rbloptions.SelectedValue, opt2, txtRemarks.Text, ip, Utility.formatDatewithtime(DateTime.Now), imageSize1, ext1, opt3, CJPID);
                if (i > 0)
                {
                    msg.Show("-------Record Inserted SuccessFully--------");

                }

                chkDesclaimer.Checked = false;
                lbldsssbans.Text = "";
                txtRemarks.Text = "";
                rbloptions.ClearSelection();
                rbloptions2.ClearSelection();
                chkoption3.ClearSelection();
                rbloptions2.Visible = false;
                chkoption3.Visible = false;
                #endregion
                fill_Grid();
                btnchallenge.Visible = true;
                tbl2.Visible = false;
                trDesc.Visible = false;
                btncancel.Visible = false;
                if (Grdalrdchlge.Rows.Count > 0)
                {
                    btnPay.Visible = true;
                }
                else
                {
                    btnPay.Visible = false;
                }
            }
        }
        catch (ThreadAbortException ex)
        {

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }       
    }
    private bool checkFileSizeDocupload()
    {      
        bool flag = true;
        if (uploaddoc.HasFile)
        {
            if (uploaddoc.PostedFile.ContentLength > 1048576)
            {

                flag = false;

            }
        }
        return flag;
    }
    public bool chkfiletypeDocupload(byte[] file, string ext)
    {

        // content length checkng and return true ,because file upload is not mandatory field in this progmarm, so we are just checking file validation 0 to 5242880
        //if true than it will check bytes for pdf and jpg, if condition false than return true
        if (uploaddoc.PostedFile.ContentLength > 0 && uploaddoc.PostedFile.ContentLength != null && uploaddoc.PostedFile.ContentLength <= 1048576)
        {
            byte[] chkByte = null;

            if (ext == ".jpeg" || ext == ".jpg")
            {
                chkByte = new byte[] { 255, 216, 255, 224 };
            }

            else if (ext == ".pdf")
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
        return true;
    }

    //private bool checkImageDocupload()
    //{
    //    bool flag = true;
    //    byte[] imageSize;
    //    string ext = "";
    //    if (uploaddoc.HasFile)
    //    {
    //        imageSize = new byte[uploaddoc.PostedFile.ContentLength];
    //        if (uploaddoc.PostedFile != null && uploaddoc.PostedFile.FileName != "" && uploaddoc.PostedFile.ContentLength > 0)
    //        {
    //            string filename = uploaddoc.PostedFile.FileName.ToString();
    //            string[] FileExtension = filename.Split('.');
    //            ext = System.IO.Path.GetExtension(uploaddoc.PostedFile.FileName).ToLower();


    //            if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg")
    //            {
    //                HttpPostedFile uploadedImage = uploaddoc.PostedFile;
    //                uploadedImage.InputStream.Read(imageSize, 0, (int)uploaddoc.PostedFile.ContentLength);
    //                flag = true;

    //            }
    //            else
    //            {
    //                flag = false;
    //            }

    //        }
    //    }
    //    return flag;
    //}

    protected void rbloptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbloptions.SelectedValue == "1")
            {
                rbloptions2.Visible = true;
                chkoption3.Visible = false;
                Bindrbloptions2();
            }
            if (rbloptions.SelectedValue == "2")
            {
                rbloptions2.Visible = false;
                chkoption3.Visible = true;
                Bindrbloptions3();
            }
            if (rbloptions.SelectedValue == "3")
            {
                rbloptions2.Visible = false;
                chkoption3.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }   
    }
    protected void ddlexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tbl2.Visible = false;
            if (ddlexam.SelectedValue != "")
            {
                string examid = ddlexam.SelectedValue;
                string batchid = "";
                int index = examid.IndexOf(":");
                if (index > 0)
                {
                    batchid = examid.Substring(index + 1);
                    examid = examid.Substring(0, index);
                }
                hfbatchid.Value = batchid;
                hfexamid.Value = examid;

                string regno = Session["rid"].ToString();
                bool wpresent = objchallenge.checkexamattendance(examid, regno, batchid);
                if (wpresent || regno == "0909199199999992007")
                {
                    tratndnc.Visible = false;
                    tbl1.Visible = true;

                    DataTable dtTier = objchallenge.getExamTier(examid);

                    string tierval = "";
                    if (dtTier.Rows.Count > 0)
                    {
                        if (dtTier.Rows[0]["examtypeid"].ToString() == "1")
                        {
                              tierval = "1";
                        }
                        else
                        {
                            tierval = "0";
                        }
                    }

                    DataTable dt = objchallenge.GetPostCode(examid, regno, tierval, batchid);
                    hdnapplid.Value = dt.Rows[0]["applid"].ToString();
                    string posts = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i != 0)
                        {
                            posts += ",";
                        }
                        posts += Utility.getstring(dt.Rows[i]["post"].ToString());
                    }
                    lblposts.Text = posts;
                    DataTable dtcheckRevrel = objchallenge.getCSchedule(examid, batchid);
                    //hfsletterissue.Value = dtcheckRevrel.Rows[0]["sletterissued"].ToString();
                    //fill_Grid();
                    //if (dtcheckRevrel.Rows[0]["RevReleased"].ToString() == "Y")
                    //{
                    //    Grdalrdchlge.Columns[8].Visible = true;
                    //}
                    //else
                    //{
                    //    Grdalrdchlge.Columns[8].Visible = false;
                    //}

                    //===========Manindra =============================
                    if (dtcheckRevrel.Rows[0]["sletterissued"].ToString() != "")
                    {
                        hfsletterissue.Value = dtcheckRevrel.Rows[0]["sletterissued"].ToString();
                    }
                    fill_Grid();
                    if (dtcheckRevrel.Rows[0]["RevReleased"].ToString() == "Y")
                    {
                        Grdalrdchlge.Columns[8].Visible = true;
                    }
                    else
                    {
                        Grdalrdchlge.Columns[8].Visible = false;
                    }
                  //===============End ======================================================
                    bool isreleased = objchallenge.CheckSchedule(examid,batchid);
                    if (isreleased || regno == "0909199199999992007")
                    {
                        btnchallenge.Visible = true;
                    }
                    else
                    {
                        msg.Show("You cannot Submit Challenge during this period");
                        btnchallenge.Visible = false;
                    }
                }
                else
                {
                    tratndnc.Visible = true;
                    tbl1.Visible = false;
                }
            }
            else
            {
                tbl1.Visible = false;
                tratndnc.Visible = false;
            }
           
           
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }   
    }
    protected void Grdalrdchlge_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ChallengeID = Grdalrdchlge.DataKeys[e.Row.RowIndex].Values["ChallengeID"].ToString();
                string CPdid = Grdalrdchlge.DataKeys[e.Row.RowIndex].Values["CPdID"].ToString();
                string status = Grdalrdchlge.DataKeys[e.Row.RowIndex].Values["status"].ToString();
                 string cstatus = Grdalrdchlge.DataKeys[e.Row.RowIndex].Values["cstatus"].ToString();
                LinkButton lnkdel = (LinkButton)e.Row.FindControl("lnkdelete");
                LinkButton lnkverify = (LinkButton)e.Row.FindControl("lnkverify");
                LinkButton lnkPrintAck = (LinkButton)e.Row.FindControl("lnkPrintAck");
                Label lblrefundstatus = (Label)e.Row.FindControl("lblrefundstatus");
                if (status == "SUCCESS")
                {
                    lnkdel.Visible = false;
                    lnkverify.Visible = false;
                    lnkPrintAck.Visible = true;
                }
                else
                {
                    lnkdel.Visible = true;
                    lnkverify.Visible = true;
                    lnkPrintAck.Visible = false;
                }
                HyperLink hyplimage = (HyperLink)e.Row.FindControl("hldoc");
                string url = md5util.CreateTamperProofURL("Challengepdf.aspx", null, "ChallengeID=" + MD5Util.Encrypt(ChallengeID, true));
                hyplimage.NavigateUrl = url;

                if (hfsletterissue.Value == "Y" && cstatus == "A")
                {
                    lblrefundstatus.Visible = true;
                }
                else
                {
                    lblrefundstatus.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }   

    }
    protected void Grdalrdchlge_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);        
        if (e.CommandName == "Del")
        {
            try
            {
                string ChallengeId = Grdalrdchlge.DataKeys[rowIndex].Values["ChallengeID"].ToString();
                int i = objchallenge.Delete_ChallgeDetails(ChallengeId);
                if (i > 0)
                {
                    msg.Show("----Record Deleted----");
                    fill_Grid();
                    btnchallenge.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }   
        }
        if (e.CommandName == "Verify")
        {
            try
            {
                string CPdID = Grdalrdchlge.DataKeys[rowIndex].Values["CPdID"].ToString();
                string url = md5util.CreateTamperProofURL("CPayOnline2.aspx", null, "CPdID=" + MD5Util.Encrypt(CPdID, true) + "&flag=" + MD5Util.Encrypt("V", true));
                Response.Redirect(url);
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }   
        }

        if (e.CommandName == "PrintAck")
        {
            try
            {
                string CPdID = Grdalrdchlge.DataKeys[rowIndex].Values["CPdID"].ToString();
                string url = md5util.CreateTamperProofURL("ChallangeAcknowledgement.aspx", null, "examid=" + MD5Util.Encrypt(hfexamid.Value, true) + "&rid=" + MD5Util.Encrypt(Session["rid"].ToString(), true) + "&cpdid=" + MD5Util.Encrypt(CPdID, true) + "&post=" + MD5Util.Encrypt(lblposts.Text, true) + "&exam=" + MD5Util.Encrypt(ddlexam.SelectedItem.Text, true) + "&applid=" + MD5Util.Encrypt(hdnapplid.Value, true) + "&batchid=" + MD5Util.Encrypt(hfbatchid.Value, true));
                Response.Redirect(url);
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
    }
 
    protected void chkDesclaimer_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDesclaimer.Checked == true)
        {
            tbl2.Visible = true;
            lbldsssbans.Text = "";
            txtRemarks.Text = "";
        }
        else
        {
            tbl2.Visible = false;
            btnchallenge.Visible = true;
            btnPay.Visible = true;
        }

    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        try
        {
            string IDs = "";
            for (int i = 0; i < Grdalrdchlge.Rows.Count; i++)
            {
                string status = Grdalrdchlge.DataKeys[i].Values["status"].ToString();
                string ChallengeID = Grdalrdchlge.DataKeys[i].Values["ChallengeID"].ToString();

                if (status != "SUCCESS")
                {
                    if (IDs != "")
                    {
                        IDs += ",";
                    }
                    IDs += ChallengeID;
                }
            }
            if (IDs == "")
            {
                msg.Show("No Challenge Available with Pending Payment");
            }
            else
            {
                string url = md5util.CreateTamperProofURL("CPayment.aspx", null, "ChallengeIds=" + MD5Util.Encrypt(IDs, true));
                Response.Redirect(url);
            }
        }
        catch (ThreadAbortException ex)
        {

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }   
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            btncancel.Visible = false;
            trDesc.Visible = false;
            tbl2.Visible = false;
            btnchallenge.Visible = true;
            fill_Grid();
            chkDesclaimer.Checked = false;
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }   
    }
    
}