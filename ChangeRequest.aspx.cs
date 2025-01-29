using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ChangeRequest : BasePage
{
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    CandidateData objcand = new CandidateData();
    message msg = new message();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtserialno.Focus();

        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            fillddlpostcode();
            if (Request.QueryString["flag"].ToString() == "N")
            {
                tbl1.Visible = true;
                trname.Visible = true;
                trfname.Visible = true;
                trpostcode.Visible = true;
                tridno.Visible = false;
                lblhead.Text = "New ID No. Request";
            }
            else
            {
                tbl1.Visible = false;
                trname.Visible = false;
                trfname.Visible = false;
                trpostcode.Visible = false;
                tridno.Visible = true;
                lblhead.Text = "Date of Birth Change Request";
            }
        }
        else
        {
            if (((Request.Form["csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
    }
    protected void btncheck_Click(object sender, EventArgs e)
    {
        if (objcand.checkidno(txtserialno.Text))
        {
            if (objcand.ischangerequestallowed(txtserialno.Text))
            {
                txtserialno.Enabled = false;
                btncheck.Visible = false;
                tbl1.Visible = true;
            }
            else
            {
                msg.Show("More than one DOB Change Request can not be submitted at a time.");
                tbl1.Visible = false;
                txtserialno.Enabled = true;
                btncheck.Visible = true;
            }
        }
        else
        {
            msg.Show("Id No. does not exists. Please submit New ID No. Request.");
            tbl1.Visible = false;
            txtserialno.Enabled = true;
            btncheck.Visible = true;
        }
    }
    private void fillddlpostcode()
    {
        try
        {
            DataTable dtpost = objcand.GetJobAdvt("spl");
            ddlpost.DataTextField = "announcement";
            ddlpost.DataValueField = "postcode";
            ddlpost.DataSource = dtpost;

            ddlpost.DataBind();
            ddlpost.Items.Insert(0, Utility.ddl_Select_Value());
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int len = fulcerti.PostedFile.ContentLength;
        int len1 = fulack.PostedFile.ContentLength;
        if (len == 0)
        {
            msg.Show("Please upload 10th Class Certificate.");
            return;
        }
        if (len1 == 0)
        {
            msg.Show("Please upload Acknowledgement Certificate.");
            return;
        }

        string ctype = fulcerti.PostedFile.ContentType;
        string ctype1 = fulack.PostedFile.ContentType;
        if (ctype != "application/pdf" || ctype1 != "application/pdf")
        {
            msg.Show("Only pdf files can be uploaded");
            return;
        }

        byte[] file1 = new byte[len];
        fulcerti.PostedFile.InputStream.Read(file1, 0, len);
        byte[] file2 = new byte[len1];
        fulack.PostedFile.InputStream.Read(file2, 0, len1);

        string StrFileName = fulcerti.PostedFile.FileName.Substring(fulcerti.PostedFile.FileName.LastIndexOf("\\") + 1);
        string fileext = "";
        if (StrFileName != "")
        {
            string[] filename = new string[2];
            filename = StrFileName.Split('.');
            fileext = filename[1].ToString();
        }

        string StrFileName2 = fulack.PostedFile.FileName.Substring(fulack.PostedFile.FileName.LastIndexOf("\\") + 1);
        string fileext2 = "";
        if (StrFileName2 != "")
        {
            string[] filename2 = new string[2];
            filename2 = StrFileName2.Split('.');
            fileext2 = filename2[1].ToString();
        }

        bool checkfiletype = chkfiletype(file1, fileext);
        bool checkfiletype2 = chkfiletype(file2, fileext2);
        if (!checkfiletype)
        {
            msg.Show("Only pdf files can be uploaded");
            return;
        }
        if (!checkfiletype2)
        {
            msg.Show("Only pdf files can be uploaded");
            return;
        }

        string mipaddress = GetIPAddress();
        int reqid = 0;
        if (Request.QueryString["flag"].ToString() == "N")
        {
            reqid = objcand.InsertOldPostChange_Req(txtname.Text, txtfname.Text, Utility.formatDate(txtdob.Text), mipaddress, ddlpost.SelectedValue, "", txt_mob.Text, "N");
        }
        else
        {
            reqid = objcand.InsertOldPostChange_Req("", "", Utility.formatDate(txtdob.Text), mipaddress, "", txtserialno.Text, txt_mob.Text, "C");
        }
        if (reqid > 0)
        {
            try
            {
                int temp = objcand.insertchangereqdocs(reqid, file1, file2);
                msg.Show("You have been requested successfully.Your Request No. is "+ reqid +" for further refrences");
                Server.Transfer("splcandidate.aspx");

            }
            catch (Exception ex)
            {
                //Response.Redirect("ErrorPage.aspx");
            }
        }

    }

    public bool chkfiletype(byte[] file, string ext)
    {
        byte[] chkByte = null;
        if (ext == "pdf")
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
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangeRequest.aspx?flag=" + Request.QueryString["flag"].ToString() + "");
    }
}