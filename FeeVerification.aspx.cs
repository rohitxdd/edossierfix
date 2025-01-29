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

public partial class FeeVerification : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    string applid = "";
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    DataTable dt;
    MD5Util md5util = new MD5Util();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            string regno = Session["rid"].ToString();
            filljob(regno);
        }
        else
        {
            if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
        if (Request.QueryString["applid"] != null)
        {
            tblconf.Visible = false;
            string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            ddjob.SelectedValue = applid;
            Button1_Click(this, new EventArgs());
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //DropDownList ddlpost = (DropDownL
        tbl_confirm.Visible = true;
        lbl_status.Visible = true;

        applid = ddjob.SelectedValue;


        dt = objcd.CheckFee(Int32.Parse(applid));
        //if(dt.Rows[0]["feereq"].ToString() != "N")
        //{
        //    if (dt.Rows[0]["feerecd"].ToString() == "Y")
        //    {
        //        msg.Show("You Fee has been confirmed at DSSSB");

        //    }
        //    else
        //    {
        //        msg.Show("Your fee is pending");
        //    }
        //}
        //else
        //{
        //    msg.Show("You are exempted from fee");
        //}


        if (dt.Rows[0]["jeid"].ToString() != "0")
        {
            lbl_edu.Text = "Yes";
            lbl_edu.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lbl_edu.Text = "No";
            lbl_edu.ForeColor = System.Drawing.Color.Red;
        }
        if (dt.Rows[0]["jexid"].ToString() != "0")
        {
            lbl_exp.Text = "Yes";
            lbl_exp.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lbl_exp.Text = "No";
            lbl_exp.ForeColor = System.Drawing.Color.Red;
        }
        if (dt.Rows[0]["pic"].ToString() != "0")
        {
            lbl_photo.Text = "Yes";
            lbl_photo.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lbl_photo.Text = "No";
            lbl_photo.ForeColor = System.Drawing.Color.Red;
        }
        if (dt.Rows[0]["sign"].ToString() != "0")
        {
            lbl_sign.Text = "Yes";
            lbl_sign.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lbl_sign.Text = "No";
            lbl_sign.ForeColor = System.Drawing.Color.Red;
        }
       // if (dt.Rows[0]["dummyno"].ToString() != "0")
        if (dt.Rows[0]["dummyno"].ToString() !="" && ( dt.Rows[0]["feereq"].ToString() == "N" ||  dt.Rows[0]["feerecd"].ToString() == "Y"))
        {
            lbl_confirm.Text = "Application Recieved";
            lbl_confirm.ForeColor = System.Drawing.Color.Green;
            tbl_status.Visible = false;
            tbl_exam.Visible = true;
            btnverifystatus.Visible = false;
            lblverfy.Visible = false;
        }
        else if ((dt.Rows[0]["final"].ToString() == "Y" && dt.Rows[0]["feereq"].ToString() == "Y") && ( dt.Rows[0]["dummyno"].ToString() =="" || dt.Rows[0]["feerecd"].ToString() == "N" ))
        {
            lbl_confirm.Text = "Application Pending due to Fee Deposit";
            lbl_confirm.ForeColor = System.Drawing.Color.Green;
            tbl_status.Visible = false;
            tbl_exam.Visible = false;
           // tbl_fee.Visible = true;
            if (dt.Rows[0]["readyforexam"].ToString() == "Y")
            {
                btnverifystatus.Visible = false;
                lblverfy.Visible = false;
            }
            else
            {
                btnverifystatus.Visible = true;
                lblverfy.Visible = true;
            }
        }
        else
        {
            lbl_confirm.Text = "Application Not Recieved in Board due to Pending Final Submit by Candidate";
            lbl_confirm.ForeColor = System.Drawing.Color.Red;
            tbl_status.Visible = true;
            tbl_exam.Visible = false;
            btnverifystatus.Visible = false;
            lblverfy.Visible = false;
        }
        //if (dt.Rows[0]["feerecd"].ToString() == "Y")
        //{
        //    lbl_fee.Text = "Confirmed";
        //    lbl_fee.ForeColor = System.Drawing.Color.Green;
        //    tbl_exam.Visible = true;
        //    btnverifystatus.Visible = false;
        //    lblverfy.Visible = false;
        //}
        //else if (dt.Rows[0]["feereq"].ToString() == "N")
        //{
        //    lbl_fee.Text = "Fee Exempted";
        //    lbl_fee.ForeColor = System.Drawing.Color.Green;
        //    tbl_exam.Visible = true;
        //    btnverifystatus.Visible = false;
        //    lblverfy.Visible = false;
        //}
        //else
        //{
        //    lbl_fee.Text = "Pending";
        //    lbl_fee.ForeColor = System.Drawing.Color.Red;
        //    tbl_exam.Visible = false;
        //    if (dt.Rows[0]["readyforexam"].ToString() == "Y")
        //    {
        //        btnverifystatus.Visible = false;
        //        lblverfy.Visible = false;
        //    }
        //    else
        //    {
        //        btnverifystatus.Visible = true;
        //        lblverfy.Visible = true;
        //    }
        //}
        if (dt.Rows[0]["examid"].ToString() != "0")
        {
            lbl_exam.Text = "Yes";
            lbl_exam.ForeColor = System.Drawing.Color.Green;
            tr_exam.Visible = true;
        }
        else
        {
            lbl_exam.Text = "No";
            lbl_exam.ForeColor = System.Drawing.Color.Red;
            tr_exam.Visible = false;
            tbl_exam.Visible = false;
        }
        if (dt.Rows[0]["examdate"].ToString() != "0")
        {
            lbl_exam_date.Text = dt.Rows[0]["examdate"].ToString();
            lbl_exam_date.ForeColor = System.Drawing.Color.Green;
            tr_exam.Visible = true;
        }
        else
        {
            lbl_exam_date.Text = "No";
            lbl_exam_date.ForeColor = System.Drawing.Color.Red;
            tr_exam.Visible = false;
        }

        DataTable dtbatch = objcd.getbatchdetails(applid);
        string radmitcard = dt.Rows[0]["radmitcard"].ToString();
        if (dtbatch.Rows.Count > 0)
        {
            radmitcard = dtbatch.Rows[0]["radmitcard"].ToString();
        }
        if (radmitcard == "Y" && (dt.Rows[0]["acstatid"].ToString() != null && dt.Rows[0]["acstatid"].ToString() != "") && dt.Rows[0]["acconsent"].ToString() == "Y")
        {
            lbl_admit_card.Text = "Yes";
            lbl_admit_card.ForeColor = System.Drawing.Color.Green;
            tr_exam.Visible = true;
        }
        else if (radmitcard != "N" && (dt.Rows[0]["acstatid"].ToString() == null && dt.Rows[0]["acstatid"].ToString() == ""))
        {
            lbl_admit_card.Text = "Not Eligible";
            lbl_admit_card.ForeColor = System.Drawing.Color.Red;
            tr_exam.Visible = true;
        }
        else
        {
            lbl_admit_card.Text = "Under Process";
            lbl_admit_card.ForeColor = System.Drawing.Color.Red;
            tr_exam.Visible = false;
        }

        DataTable dtckbatch = objcd.Checkbatchstatus(applid);
        if (dtckbatch.Rows.Count > 0 && dtbatch.Rows.Count > 0)
        {
            trbatchid.Visible = true;            
            string batchname = dtbatch.Rows[0]["batchname"].ToString();
            string schedule = dtbatch.Rows[0]["examdate"].ToString() + " " + dtbatch.Rows[0]["examtime"].ToString();
            lblbatchid.Text = batchname + " on " + schedule;
        }
        else
        {
            trbatchid.Visible = false;
            lblbatchid.Text = "";
        }
        

    }

    public void filljob(string regno)
    {
        try
        {

            dt = objcd.get_app_status(regno);
            if (dt.Rows.Count > 0)
            {
                ddjob.Items.Clear();
                ddjob.DataTextField = "post";
                ddjob.DataValueField = "applid";
                ddjob.DataSource = dt;
                ddjob.DataBind();
                ListItem l1 = new ListItem();
                l1.Text = "--Select--";
                l1.Value = "";
                ddjob.Items.Insert(0, l1);
            }
            else
            {
                //ddjob.Visible = false;
                TRHead.Visible = false;
                TR1.Visible = false;
                //Button1.Visible = false;
                LabelNote.Visible = true;
                LabelNote.Text = "No Post has been applied till yet.";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }
    protected void btnverifystatus_Click(object sender, EventArgs e)
    {
        string applid = ddjob.SelectedValue;
        if (applid != "")
        {
            string url = md5util.CreateTamperProofURL("PayOnline2.aspx", null, "applid=" + MD5Util.Encrypt(applid, true) + "&flag=" + MD5Util.Encrypt("V", true));
            Response.Redirect(url);
        }
    }
}
