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


public partial class AdvtList : BasePage
{
    CandidateData objCandD = new CandidateData();
    MD5Util md5util = new MD5Util();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    message msg = new message();
    string regno = "";
    LoginMast ObjMast = new LoginMast();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();

            if (StringUtil.GetQueryString(Request.Url.ToString()) != null)
            {
                if (md5util.IsURLTampered(StringUtil.GetWithoutDigest(StringUtil.GetQueryString(Request.Url.ToString())),
                     StringUtil.GetDigest(StringUtil.GetQueryString(Request.Url.ToString()))) == true)
                {
                    // Response.Redirect("home.aspx?id=0");
                }
            }

            FillGrid();
            if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
            {
                grdsplpost.Visible = true;
                FillGridsplpost();
            }
            else
            {
                grdsplpost.Visible = false;
            }
        }
        //Session["ad_flag"] = "0";
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

    private void FillGrid()
    {

        //LblJobTitle.Text = "List of Current Advertisements";
        string advtno = "";
        if (Request.QueryString["AdvtNo"] != null)
        {
            advtno = MD5Util.Decrypt(Request.QueryString["AdvtNo"].ToString(), true);

        }
        DataTable dt = new DataTable();
        if (advtno == "")
        {
            dt = objCandD.GetJobAdvt("");
        }
        else
        {
            dt = objCandD.GetJobAdvt(advtno, "");
        }


        dgJobList.DataSource = dt;
        dgJobList.DataBind();

    }
    private void FillGridsplpost()
    {

        //LblJobTitle.Text = "List of Current Advertisements";
        string advtno = "";
        if (Request.QueryString["AdvtNo"] != null)
        {
            advtno = MD5Util.Decrypt(Request.QueryString["AdvtNo"].ToString(), true);

        }
        DataTable dt = new DataTable();

        if (advtno == "")
        {
            dt = objCandD.GetJobAdvt("spl");
        }
        else
        {
            dt = objCandD.GetJobAdvt(advtno, "spl");
        }




        grdsplpost.DataSource = dt;
        grdsplpost.DataBind();

    }
    protected void dgJobList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Apply")
        //{          

        //    int rowIndex = int.Parse(e.CommandArgument.ToString());
        //    string url = md5util.CreateTamperProofURL("Apply.aspx", null, "JobSourceID=" + MD5Util.Encrypt(this.dgJobList.DataKeys[rowIndex]["JobSourceID"].ToString(), true) + "&AdvtYear=" + MD5Util.Encrypt(this.dgJobList.DataKeys[rowIndex]["AdvtYear"].ToString(), true)+ "&AdvtNo=" +MD5Util.Encrypt(this.dgJobList.DataKeys[rowIndex]["AdvtNo"].ToString(),true));
        //    Server.Transfer(url);
        //}
    }
    protected void dgJobList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Start Check for ID Proof uploaded or not
        regno = Session["rid"].ToString();
        dt = ObjMast.getDetailIfDocUploaded(regno);

        //End Check for ID Proof uploaded or not
        if (dt.Rows.Count > 0)
        {
            string apply = check_apply(dgJobList.DataKeys[e.NewEditIndex].Values[5].ToString());
            //string standcode = gvquali.DataKeys[e.Row.RowIndex].Values["standard"].ToString();
            DataTable dtdata = objCandD.getdetail(Session["rid"].ToString());
            string candt_gender = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["sex"].ToString()));//Session["gender"].ToString();
            string Post_gender = dgJobList.DataKeys[e.NewEditIndex].Values[11].ToString();
            if (candt_gender == Post_gender || Post_gender == "A" || Post_gender == "B")//Post_gender == "A" is updated on Dated:21-12-2022
            {
                if (apply == "0")
                {
                    if (dgJobList.DataKeys[e.NewEditIndex].Values[1].ToString() == "90/09")//Added on Dated: 16-02-2023
{
    DataTable tbl = new DataTable();
    tbl = objCandD.CheckVerification(Session["rid"].ToString());

    if (tbl.Rows.Count > 0)
    {
        if (tbl.Rows[0]["serial_no"].ToString() != null && tbl.Rows[0]["DocFile"].ToString() != null && tbl.Rows[0]["DocFile"].ToString() != "")

        {
            string url = md5util.CreateTamperProofURL("Apply.aspx", null, "advt_no=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[0].ToString(), true) + "&postCode=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[1].ToString(), true) + "&jobtitle=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[2].ToString(), true) + "&dobfrom=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[3].ToString(), true) + "&dobto=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[4].ToString(), true) + "&jid=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[5].ToString(), true) + "&endson=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values["endson_org"].ToString(), true) + "&JobDesc=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[12].ToString(), true) + "&reqid=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[13].ToString(), true) + "&endson_org=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[14].ToString(), true));     //Added by AnkitaSingh 08-02-2023               
            //string url = md5util.CreateTamperProofURL("Apply.aspx", null, "advt_no=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[0].ToString(), true) + "&postCode=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[1].ToString(), true) + "&jobtitle=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[2].ToString(), true) + "&dobfrom=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[3].ToString(), true) + "&dobto=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[4].ToString(), true) + "&jid=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[5].ToString(), true) + "&endson=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[6].ToString(), true) + "&JobDesc=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[12].ToString(), true) + "&reqid=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[13].ToString(), true) + "&endson_org=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[14].ToString(), true));

            Response.Redirect(url);
        }
        else
        {
            msg.Show("Complete 90/09 verification before application.");
        }
    }
    else
    {
        msg.Show("You are not eligible to apply for this post.");
    }
}
else
{
    //string url = md5util.CreateTamperProofURL("Apply.aspx", null, "advt_no=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[0].ToString(), true) + "&postCode=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[1].ToString(), true) + "&jobtitle=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[2].ToString(), true) + "&dobfrom=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[3].ToString(), true) + "&dobto=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[4].ToString(), true) + "&jid=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[5].ToString(), true) + "&endson=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[6].ToString(), true) + "&JobDesc=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[12].ToString(), true) + "&reqid=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[13].ToString(), true) + "&endson_org=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[14].ToString(), true));
    string url = md5util.CreateTamperProofURL("Apply.aspx", null, "advt_no=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[0].ToString(), true) + "&postCode=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[1].ToString(), true) + "&jobtitle=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[2].ToString(), true) + "&dobfrom=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[3].ToString(), true) + "&dobto=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[4].ToString(), true) + "&jid=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[5].ToString(), true) + "&endson=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values["endson_org"].ToString(), true) + "&JobDesc=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[12].ToString(), true) + "&reqid=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[13].ToString(), true) + "&endson_org=" + MD5Util.Encrypt(dgJobList.DataKeys[e.NewEditIndex].Values[14].ToString(), true));     //Added by AnkitaSingh 08-02-2023               
    Response.Redirect(url);
}
                }
                else
                {
                    string url_status = md5util.CreateTamperProofURL("FeeVerification.aspx", null, "applid=" + MD5Util.Encrypt(apply, true));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:Confirm('" + url_status + "');", true);
                    //msg.Show("You have already applied for this post.");
                }
            }
            else
            {
                string gen_value = "";
                if (Post_gender == "M")
                {
                    gen_value = "Male";
                }
                if (Post_gender == "F")
                {
                    gen_value = "Female";
                }
                if (Post_gender == "A")
                {
                    gen_value = "All";
                }
                if (Post_gender == "B")
                {
                    gen_value = "Both Male & Female";
                }
                msg.Show("This Post is Open For " + gen_value + " Candidates");//21-12-2022
            }
        }
        else
        {
            DataTable dt1 = objCandD.getLinkEnableDisableStatus("116postCode");
            if (dt1.Rows.Count > 0)
            {

                DataTable dtCheck = ObjMast.CheckIfWithin1by20to116by20PostCode(regno);
                if (dtCheck.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please update your ID-Proof and upload postcard size photograph in postcode of 2020 first before applying to new post code of 2021.');window.location ='updatemobile.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Kindly update mandatory id proof details before applying.');window.location ='UploadIDProof.aspx?linkClicked=" + MD5Util.Encrypt("IDProof", true) + "';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Kindly update mandatory id proof details before applying.');window.location ='UploadIDProof.aspx?linkClicked=" + MD5Util.Encrypt("IDProof", true) + "';", true);
            }
        }
    }
    bool ReturnValue()
    {
        return false;
    }
    protected void dgJobList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlsatename");
            string flag = dgJobList.DataKeys[e.Row.RowIndex].Values["flag"].ToString();
            string adid = dgJobList.DataKeys[e.Row.RowIndex].Values["adid"].ToString();
            string jid = dgJobList.DataKeys[e.Row.RowIndex].Values["jobid"].ToString();
            Button applybtn = (Button)e.Row.FindControl("BtnApply");
            
            if (flag == "Y")
            {
                HyperLink hypadv = (HyperLink)e.Row.FindControl("hypadv");
                HyperLink hyplimage = (HyperLink)e.Row.FindControl("hyplimage");
                string url = md5util.CreateTamperProofURL("advtpdf.aspx", null, "adid=" + MD5Util.Encrypt(adid, true));
                hyplimage.NavigateUrl = url;
                string urlpost = md5util.CreateTamperProofURL("postdetail.aspx", null, "jobid=" + MD5Util.Encrypt(jid, true));
                //hypadv.NavigateUrl = urlpost;
            }
            else
            {
                HyperLink hyplimage = (HyperLink)e.Row.FindControl("hyplimage");
                hyplimage.Visible = false;
            }
        }

    }
    private string check_apply(string jid)
    {
        DataTable dt = objCandD.Search_JobApplication_PD(jid, Session["rid"].ToString());
        if (dt.Rows.Count == 0)
        {
            return "0";
        }
        else
        {
            return dt.Rows[0]["applid"].ToString();
        }
    }
    protected void grdsplpost_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string apply = check_apply(grdsplpost.DataKeys[e.NewEditIndex].Values[5].ToString());
        //string standcode = gvquali.DataKeys[e.Row.RowIndex].Values["standard"].ToString();
        DataTable dtdata = objCandD.getdetail(Session["rid"].ToString());
        string candt_gender = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["sex"].ToString()));//Session["gender"].ToString();
        string Post_gender = grdsplpost.DataKeys[e.NewEditIndex].Values[11].ToString();

        if (candt_gender == Post_gender || Post_gender == "B")
        {
            if (apply == "0")
            {
                string url = md5util.CreateTamperProofURL("Apply.aspx", null, "advt_no=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[0].ToString(), true) + "&postCode=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[1].ToString(), true) + "&jobtitle=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[2].ToString(), true) + "&dobfrom=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[3].ToString(), true) + "&dobto=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[4].ToString(), true) + "&jid=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[5].ToString(), true) + "&endson=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[6].ToString(), true) + "&JobDesc=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[12].ToString(), true) + "&reqid=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[13].ToString(), true) + "&endson_org=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[14].ToString(), true) + "&agevalidationexmpt=" + MD5Util.Encrypt(grdsplpost.DataKeys[e.NewEditIndex].Values[15].ToString(), true));
                Response.Redirect(url);
            }
            else
            {
                string url_status = md5util.CreateTamperProofURL("FeeVerification.aspx", null, "applid=" + MD5Util.Encrypt(apply, true));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:Confirm('" + url_status + "');", true);
                //msg.Show("You have already applied for this post.");
            }
        }
        else
        {
            string gen_value = "";
            if (Post_gender == "M")
            {
                gen_value = "Male";
            }
            if (Post_gender == "F")
            {
                gen_value = "Female";
            }

            msg.Show("This Post Only For " + gen_value + " Candiate");
        }

    }
    protected void grdsplpost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlsatename");
            string flag = grdsplpost.DataKeys[e.Row.RowIndex].Values["flag"].ToString();
            string adid = grdsplpost.DataKeys[e.Row.RowIndex].Values["adid"].ToString();
            string jid = grdsplpost.DataKeys[e.Row.RowIndex].Values["jobid"].ToString();
            string postcode = grdsplpost.DataKeys[e.Row.RowIndex].Values["postcode"].ToString();
            #region hide
            //if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
            //{
            //    DataTable serial_no_dt = objCandD.get_serial_no(Session["rid"].ToString()); 

            //        ////Session["serial_no"].ToString();
            //    string p_code = "";
            //    string s_no = "";
            //    for (int j = 0; j < serial_no_dt.Rows.Count; j++)
            //    {
            //        p_code += serial_no_dt.Rows[j]["postcode"] + ",";
            //        s_no += serial_no_dt.Rows[j]["serial_no"] + ",";
            //    }
            //    if (p_code != "" && s_no != "")
            //    {
            //        p_code = p_code.Substring(0, p_code.Length - 1);
            //        s_no = s_no.Substring(0, s_no.Length - 1);
            //    }



            //    if (p_code.Contains(postcode))
            //    {
            //        e.Row.Visible = true;
            //    }
            //    else
            //    {
            //        e.Row.Visible = false;
            //    }

            //    //for (int i = 0; i < serial_no_dt.Rows.Count; i++)
            //    //{
            //    //    DataTable dt = objCandD.checkserial_postcode(s_no, p_code);

            //    //    if (dt.Rows.Count > 0)
            //    //    {
            //    //        e.Row.Visible = true;
            //    //    }
            //    //    else
            //    //    {
            //    //        e.Row.Visible = false;
            //    //    }
            //    //}

            //}
            #endregion

            if (flag == "Y")
            {
                HyperLink hypadv = (HyperLink)e.Row.FindControl("hypadv");
                HyperLink hyplimage = (HyperLink)e.Row.FindControl("hyplimage");
                string url = md5util.CreateTamperProofURL("advtpdf.aspx", null, "adid=" + MD5Util.Encrypt(adid, true));
                hyplimage.NavigateUrl = url;
                string urlpost = md5util.CreateTamperProofURL("postdetail.aspx", null, "jobid=" + MD5Util.Encrypt(jid, true));
                //hypadv.NavigateUrl = urlpost;
            }
            else
            {
                HyperLink hyplimage = (HyperLink)e.Row.FindControl("hyplimage");
                hyplimage.Visible = false;
            }
        }

    }
    protected void BtnApply_Click(object sender, EventArgs e)
    {
        //get row clicked data for Postcode from gridview 
        string postcode = "";
        string nameOfPost = "";
        string regno = Session["regno"].ToString();
        string ip = GetIPAddress();
        //objCandD.InsertIntoCandidateAcivityLog(regno, "True", "Applied for job", ip);
       
    }
}
