using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ViewStatus : BasePage
{
    CandidateData objcd = new CandidateData();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();

            filljob();
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
    }

    private void filljob()
    {
        try
        {
            string regno = Session["rid"].ToString();
            DataTable dt = new DataTable();
            dt = objcd.getpostforstatus(regno);
            ddjob.DataTextField = "post";
            ddjob.DataValueField = "applid";
            ddjob.DataSource = dt;
            ddjob.DataBind();
            ListItem l1 = new ListItem();
            l1.Text = "--Select--";
            l1.Value = "";
            ddjob.Items.Insert(0, l1);
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        filldetail();
    }

    private void filldetail()
    {
        if (ddjob.SelectedValue != "")
        {
            tbl1.Visible = true;
            DataTable dt = objcd.getdataforapplid(ddjob.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                lblano.Text = dt.Rows[0]["dummy_no"].ToString();
                lbldt.Text = dt.Rows[0]["appldt"].ToString();
                if (dt.Rows[0]["feereq"].ToString() == "Y")
                {
                    trfee.Visible = true;
                    lblfeedt.Text = dt.Rows[0]["feedt"].ToString();
                }
                else
                {
                    trfee.Visible = false;
                }
                DataTable dt1 = objcd.getIDProofPostCardPhotoStatus(ddjob.SelectedValue, lblano.Text);
                if (dt1.Rows.Count > 0)
                {
                     if (dt1.Rows[0]["PostCardPhoto"].ToString() != "")
                    {
                        tr2.Visible = true;
                        lbltr2.Text = "Uploaded for this Application Number.";
                    }
                    else
                    {
                        tr2.Visible = true;
                        lbltr2.Text = "Not Uploaded.";
                    }
                    if ((dt1.Rows[0]["aadharno"].ToString() != "" && dt1.Rows[0]["PostCardPhoto"].ToString() != ""))
                    {
                        tr1.Visible = true;
                        lbltr1.Text = "Provided Aadhaar detail.";
                    }
                    else if ((dt1.Rows[0]["nameOnIDProof"].ToString() != "" && dt1.Rows[0]["PostCardPhoto"].ToString() != ""))
                    {
                        tr1.Visible = true;
                        lbltr1.Text = "Provided ID Proof Detail";
                    }
                    else
                    {
                        tr1.Visible = true;
                        lbltr1.Text = "Not updated.";
                    }
                }
                else
                {
                    tr1.Visible = false;
                    tr2.Visible = false;
                }
            }
        }
        else
        {
            tbl1.Visible = false;
        }
        ///////candidate activity log////////
        string regno = Session["regno"].ToString();
        string ip = GetIPAddress();
        //objcd.InsertIntoCandidateAcivityLog(regno, "True", "Check application status", ip);
    }
}