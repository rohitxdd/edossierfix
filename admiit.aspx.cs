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

public partial class admiit : BasePage
{
    message msg = new message();
    CandidateData cd = new CandidateData();
    MD5Util md5util = new MD5Util();
    string applid = "";
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    DataTable dt;
    CandidateData objcd = new CandidateData();

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

            //if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            //{
            //    //valid Page
            //}
            //else
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {        
        //DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("ddjob");
        applid = ddjob.SelectedValue;
        if (rbtexamtype.SelectedValue=="2")
        {
            string regno = Session["rid"].ToString();
            dt = objcd.get_AdmitCard_2tier(regno, applid,"","2");
            if (dt.Rows.Count > 0)
            {
                //Tr1.Visible = true;
                string url = md5util.CreateTamperProofURL("admitcard.aspx", null, "admiit=" + MD5Util.Encrypt("1", true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&examid=" + MD5Util.Encrypt(dt.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt("2", true));
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('" + url + "');</script>");

            }
        }
        else
        {
            dt = cd.CheckValidAdmitCard(Int32.Parse(applid));
            if (dt.Rows.Count == 0)
            {
                msg.Show("Application does not exist.");
            }
            else if (dt.Rows[0]["radmitcard"].ToString() == "N" || dt.Rows[0]["examid"].ToString() == "")
            {
                msg.Show("Preparation of Admit Card is in progess.");
            }
            else if (dt.Rows[0]["postponed"].ToString() == "Y")
            {
                msg.Show("Exam has postponed for this exam.");
            }
            else if (dt.Rows[0]["radmitcard"].ToString() == "Y" && ((dt.Rows[0]["feereq"].ToString() == "N") || (dt.Rows[0]["feereq"].ToString() == "Y" && dt.Rows[0]["feerecd"].ToString() == "Y")))
            {
              //  Tr1.Visible = true;
                string url = md5util.CreateTamperProofURL("admitcard.aspx", null, "admiit=" + MD5Util.Encrypt("1", true) + "&applid=" + MD5Util.Encrypt(applid, true) + "&examid=" + MD5Util.Encrypt(dt.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt("1", true));
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('" + url + "');</script>");

            }
            else
            {
                msg.Show("Due to Non-Payment of fee your Admit Card has not been Generated.");
            }
        }
    }

    public void filljob()
    {
        try
        {
            string regno = Session["rid"].ToString();
            if (rbtexamtype.SelectedValue=="2")
            {
                dt = objcd.get_AdmitCard_2tier(regno,"","","2");
            }
            else
            {
                dt = objcd.get_AdmitCard(regno);
            }
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
                tbladmit.Visible = false;
                //truser.Visible = false;
                //Tr1.Visible = false;
                //Button1.Visible = false;
                LabelNote.Visible = true;
                LabelNote.Text = "No Admit Card is Due";
            }
            
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

  
    protected void rbtexamtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        filljob();
    }
}
