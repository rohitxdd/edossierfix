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
using System.Collections.Generic;

public partial class usercontrols_print : System.Web.UI.UserControl
{
    DataTable dt = new DataTable();
    CandidateData objCandD = new CandidateData();
    message msg = new message();
    MD5Util md5util = new MD5Util();
    int flag = 0;
    public int Yr = 0;
    public int Yu = 0;
    public int Yscat = 0;
    DataTable dt_age_relax;
    string Combdreqid;
    static int years = 0;//28-02-2023
    static int months = 0;
    static int days = 0;
    string candapplid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["flag"] == null)
            {
                TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
                Session["cand_applid"] = txt_applid.Text;
                string applids = txt_applid.Text;
                int applid = Int32.Parse(applids);
                fill_application_data(applid);
            }
            else
            {
                if (Session["Print_applid"] != null)
                {
                    string print_aaplid = Session["Print_applid"].ToString();
                    int applid = Int32.Parse(print_aaplid);
                    fill_application_data(applid);
                }
            }          
        }
        if (Session["cand_applid"] != null)
        {
            candapplid = Session["cand_applid"].ToString();
        }
        else if (Session["Print_applid"] != null)
        {
            candapplid = Session["Print_applid"].ToString();
        }
        else
        {
            candapplid = "";
            return;
        }
        DataTable dtreqid = objCandD.Get_fill_combdreqid(candapplid);
        if (dtreqid.Rows.Count > 0)
        {
            Combdreqid = dtreqid.Rows[0]["reqid"].ToString();
        }
        dt = objCandD.Getdeptcode(Combdreqid);
        string deptcode = dt.Rows[0]["deptcode"].ToString();
        if (deptcode == "COMBD")
        {
            DataTable dtf = objCandD.GetCombdDepart(Combdreqid, candapplid);
            for (int i = 0; i < dtf.Rows.Count; i++)
            {
                string depreqid = dtf.Rows[i]["DeptReqId"].ToString();
                DataTable dtedu = objCandD.getedudetail1(depreqid, candapplid);
                DataTable dtl = objCandD.getedudetail(depreqid, candapplid);
                if (dtl.Rows.Count > 0)
                {
                    if (dtedu.Rows.Count == 0)
                    {
                        msg.Show("Please fill qualification for all Department Selected");
                        btnconform.Visible = false;
                        return;
                    }
                }
                if (dtl.Rows.Count == 0)
                {
                    msg.Show("Please fill qualification for all Department Selected");
                    btnconform.Visible = false;
                    return;
                }
            }
        }
    }
    public void fill_application_data(int applid)
    {
        try
           // GetfillAppicantDetails
        {
            string datatransfer = "";
            //dt = objCandD.fill_application_data(applid);
            dt = objCandD.fill_personal_data(applid);
            DataTable dt_applid = objCandD.checkapplidexists(applid);
            if (dt_applid.Rows.Count == 0)
            {
                datatransfer = "Y";
            }
            string url = "";
            if (dt.Rows.Count > 0)
            {
                string dummy_no = dt.Rows[0]["dummy_no"].ToString();
                string final = dt.Rows[0]["final"].ToString();
                txtjid.Text = dt.Rows[0]["jid"].ToString();
                lbladvt.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["post"].ToString()));
                lblappid1.Text = dt.Rows[0]["dummy_no"].ToString();


                lblname1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["name"].ToString()));
                lblgen1.Text = dt.Rows[0]["can_gender"].ToString();
                lblfat1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["fname"].ToString()));
                lblmth1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["mothername"].ToString()));
                lbladd1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["address"].ToString())) + " - "+dt.Rows[0]["PIN"].ToString();
                lblemail1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["email"].ToString()));
                lblnat1.Text = dt.Rows[0]["nationality"].ToString();
                //  lblphy1.Text = dt.Rows[0]["ph"].ToString();
                lblser1.Text = dt.Rows[0]["SubCategory"].ToString();
                lblcat1.Text = dt.Rows[0]["category"].ToString();
                lblbrth1.Text = dt.Rows[0]["birthdt"].ToString();
                lblmobile.Text = dt.Rows[0]["mobileno"].ToString();
                lblMobilno.Text = dt.Rows[0]["mobileno"].ToString();
                lblfeeldate.Text = dt.Rows[0]["FeeLastDate"].ToString();
                lblspouse1.Text = dt.Rows[0]["spousename"].ToString();
                lblcatcertno.Text = dt.Rows[0]["category"].ToString() + " Certificate No.";
                lblcatcertno1.Text = dt.Rows[0]["CLCNo"].ToString();
                lblcatcertdt.Text = dt.Rows[0]["category"].ToString() + " Certificate Issuing Date";
                lblcatcertdt1.Text = dt.Rows[0]["CLCDate"].ToString();
                lblcatcertauth.Text = dt.Rows[0]["category"].ToString() + " Certificate Issuing Authority";
                lblcatcertauth1.Text = dt.Rows[0]["CastCertIssueAuth"].ToString();

                Lbl_name.Text = dt.Rows[0]["name"].ToString();//16-09-2022
                Lbl_guardian.Text = dt.Rows[0]["fname"].ToString();//16-09-2022

                if (dt.Rows[0]["maritalstatus"].ToString() == "U")
                {
                    lblmstatus.Text = "Unmarried";

                }
                else
                {
                    lblmstatus.Text = "Married";
                }
                string photo = dt.Rows[0]["OLEModule"].ToString();
                string exp_noofyear = dt.Rows[0]["exp_noofyears"].ToString();
                string dob = Session["birthdt"].ToString();
                DateTime dtime = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime enddate = DateTime.ParseExact(dt.Rows[0]["endson"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime today = DateTime.Now;
                TimeSpan ts = enddate - dtime;
                DateTime age = DateTime.MinValue + ts;
                years = age.Year - 1;
                months = age.Month - 1;
                days = age.Day - 1;
                string age_diff = years.ToString() + " Year/s " + months.ToString() + " Month/s " + days.ToString() + " Day/s";
                lblCandidateAge.Text = Convert.ToString(years) + " Years";

                hfreqid.Value = dt.Rows[0]["reqid"].ToString();

                if (exp_noofyear != "")
                {
                    hf_expnoofyear.Value = exp_noofyear;
                }
                else
                {
                    hf_expnoofyear.Value = "0";
                }

                if (photo != "")
                {
                    if (datatransfer == "Y")
                    {
                        url = md5util.CreateTamperProofURL("~/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true));
                    }
                    else
                    {
                        url = md5util.CreateTamperProofURL("~/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true));
                        //img.ImageUrl = "~/ImgHandler.ashx?id=" + applid + "&type=p ";//i
                    }

                    img.ImageUrl = url;

                }
                else
                {
                    flag = 1;
                    lbphoto.Visible = true;

                }
                string sign = dt.Rows[0]["signature"].ToString();
                if (sign != "")
                {
                    if (datatransfer == "Y")
                    {
                        url = md5util.CreateTamperProofURL("~/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true));

                    }
                    else
                    {
                        url = md5util.CreateTamperProofURL("~/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true));
                        //img.ImageUrl = "~/ImgHandler.ashx?id=" + applid + "&type=s ";//i
                    }

                    img2.ImageUrl = url;
                }
                else
                {
                    flag = 1;
                    lbsign.Visible = true;
                }
                string LTI = dt.Rows[0]["LTI"].ToString();
                if (LTI != "")
                {
                    if (datatransfer == "Y")
                    {
                        url = md5util.CreateTamperProofURL("~/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("l", true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true));

                    }
                    else
                    {
                        url = md5util.CreateTamperProofURL("~/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("l", true));
                        //img.ImageUrl = "~/ImgHandler.ashx?id=" + applid + "&type=s ";//i
                    }

                    imgLTI.ImageUrl = url;
                }
                else
                {
                    flag = 1;
                    lbLTI.Visible = true;
                }
                string RTI = dt.Rows[0]["RTI"].ToString();
                if (RTI != "")
                {
                    if (datatransfer == "Y")
                    {
                        url = md5util.CreateTamperProofURL("~/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("r", true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true));

                    }
                    else
                    {
                        url = md5util.CreateTamperProofURL("~/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("r", true));
                        //img.ImageUrl = "~/ImgHandler.ashx?id=" + applid + "&type=s ";//i
                    }

                    imgRTI.ImageUrl = url;
                }
                else
                {
                    flag = 1;
                    lbRTI.Visible = true;
                }
                if (lblcat1.Text == "OBC" && dt.Rows[0]["OBCRegion"].ToString() == "")
                {
                    flag = 1;
                }
                if (flag == 1)
                {
                    btnconform.Visible = false;
                    lblmsg.Visible = true;
                }

                fillgrid_quli(applid);
                desirable(applid);
                desirable1(applid);
               // Desirable.Visible = false;
                fillgrid_exp(applid);
                if (lblcat1.Text != "UR")
                {
                    trcert1.Visible = true;
                    trcert2.Visible = true;
                    trcert3.Visible = true;
                }
                else
                {
                    trcert1.Visible = false;
                    trcert2.Visible = false;
                    trcert3.Visible = false;
                }
                if (final == "")
                {
                    DataTable dt_no = objCandD.checkphoto(applid.ToString());
                    if (dt_no.Rows.Count > 0)
                    {
                        btn_editphoto.Visible = true;
                        btn_addphoto.Visible = false;
                    }
                    else
                    {
                        btn_addphoto.Visible = true;
                        btn_editphoto.Visible = false;
                    }
                    btnedit_personal.Visible = true;
                    if (gvquli.Rows.Count == 0 && gvexp.Rows.Count == 0)
                    {
                        btn_addexp.Visible = true;
                        btn_editexp.Visible = false;
                    }
                    else
                    {
                        btn_editexp.Visible = true;
                        btn_addexp.Visible = false;
                    }

                    lbl_fee.Visible = false;
                    Label_fee.Visible = false;

                }
                else
                {
                    btnconform.Visible = false;
                    tr_regid.Visible = true;
                    //chk_decl.Checked = true;
                    
                    lbl_warning.Visible = true;

                    lbl_fee.Visible = true;
                    Label_fee.Visible = true;
                }
                if (dummy_no != "")
                    {
                    chk_decl.Checked = true;
                    chkPreiview.Checked = true;
                    }


                dt = objCandD.CheckFee(applid);
                string feereq = dt.Rows[0]["feereq"].ToString();
                //string serial_no = "";
                //if (Session["serial_no"] != null)
                //{
                //     serial_no = Session["serial_no"].ToString();
                //}
                DataTable dtchkqualiexmpt = objCandD.CheckWhetherqualiexmpted(txtjid.Text);
                string feeexmpt = "";
                if (dtchkqualiexmpt.Rows.Count > 0)
                {
                    feeexmpt = dtchkqualiexmpt.Rows[0]["feeexmpt"].ToString();
                }
                if (feeexmpt == "" && feereq == "Y")
                {
                    btnconform.Text = "Pay Online and Submit Final Application";
                    if (final == "Y" && dummy_no == "")
                    {
                        btnpay.Visible = true;
                    }
                    else
                    {
                        btnpay.Visible = false;
                    }
                }
                else if (feeexmpt == "Y" || feereq == "N")
                {
                    btnconform.Text = "Submit Final Application";
                    btnpay.Visible = false;
                }

                if (dt.Rows[0]["feereq"].ToString() != "N")
                {
                    if (dt.Rows[0]["feerecd"].ToString() == "Y")
                    {
                        lbl_fee.Text = "Confirmed at DSSSB";
                    }
                    else
                    {
                        lbl_fee.Text = "Pending";
                    }
                }
                else
                {
                    lbl_fee.Text = "Exempted";
                }
            }
            else
            {
                msg.Show("Data Not Found");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void fillgrid_quli(int applid)
    {
        //dated 19/sept/2023 by shahid
        string appli_id = Convert.ToString(applid);
        DataTable dtreqid = objCandD.Get_fill_combdreqid(appli_id);
        if (dtreqid.Rows.Count > 0)
        {
            Combdreqid = dtreqid.Rows[0]["reqid"].ToString();
        }
        dt = objCandD.Getdeptcode(Combdreqid);
        string deptcode = dt.Rows[0]["deptcode"].ToString();
        if (deptcode == "COMBD")
        {
            DataTable dtlbl = objCandD.GetCombdLabelJobApplication_Education(appli_id);

            List<string> departmentNamesList = new List<string>();
            int index = 1;

            foreach (DataRow row in dtlbl.Rows)
            {
                string departmentName = row.Field<string>("DepartmentName");
                departmentNamesList.Add(index + ". " + departmentName);
                index++;
            }
            string departmentNames = string.Join("<br />", departmentNamesList);
            depapplfor.Text = departmentNames;
           
            //string departmentNames = string.Join("<br />", dtlbl.AsEnumerable()
            //.Select(row => row.Field<string>("DepartmentName")));

            //depapplfor.Text = departmentNames;
        }
        else
        {
            depapplfor.Visible = false;
            depapplforlbl.Visible = false;
        }
        if (deptcode == "COMBD")
        {
            dt = objCandD.GetCombdJobApplication_Education(appli_id, "E", "");
        }
        else
        {
            dt = objCandD.GetJobApplication_Education(appli_id, "", "");

        }
        if (dt.Rows.Count > 0)
        {
            bool hasDepartmentNameColumn = dt.Columns.Contains("DepartmentName");
            if (!hasDepartmentNameColumn)
            {
                GridViewColumnRemoveByName(gvquli, "Department Name");
            }
            gvquli.DataSource = dt;
            gvquli.DataBind();

        }
        dt = objCandD.GetJobApplication_Education(appli_id, "E", "");
        if (dt.Rows.Count <= 0)
        {
            flag = 1;
            btnconform.Visible = false;
            lblmsg.Visible = true;
        }
        //string appli_id = Convert.ToString(applid);
        //dt = objCandD.GetJobApplication_Education(appli_id, "", "");
        //if (dt.Rows.Count > 0)
        //{
        //    gvquli.DataSource = dt;
        //    gvquli.DataBind();
        //}

        //dt = objCandD.GetJobApplication_Education(appli_id, "E", "");
        //if (dt.Rows.Count <= 0)
        //{
        //    flag = 1;
        //    btnconform.Visible = false;
        //    lblmsg.Visible = true;
        //}

    }
    private void GridViewColumnRemoveByName(GridView gridView, string columnName)
    {
        foreach (DataControlField field in gridView.Columns)
        {
            if (field.HeaderText == columnName)
            {
                gridView.Columns.Remove(field);
                break;
            }
        }
    }
    public void desirable(int applid)
    {
        string appli_id = Convert.ToString(applid);
        DataTable dtreqid = objCandD.Get_fill_combdreqid(appli_id);
        if (dtreqid.Rows.Count > 0)
        {
            Combdreqid = dtreqid.Rows[0]["reqid"].ToString();
        }
        dt = objCandD.Getdeptcode(Combdreqid);
        string deptcode = dt.Rows[0]["deptcode"].ToString();
        if (deptcode == "COMBD")
        {
            dt = objCandD.Getdesirable_combd(appli_id);
        }
        else
        {
            dt = objCandD.Getdesirable(appli_id);
        }
        if (dt.Rows.Count > 0)
        {
            grd_desirableEdu.DataSource = dt;
            grd_desirableEdu.DataBind();
            Desirable.Visible = true;
            if (deptcode != "COMBD")  //rohitxd => departmentname datafield not required in same of non comb post and this is raising exception.
            {
                grd_desirableEdu.Columns[1].Visible = false;
            }
        }
        else
        {
            grd_desirableEdu.Visible = false;//17/11/2023
            Desirable.Visible = false;
        }
        //if (dt.Rows.Count > 0)
        //{
        //    for (int i = 0; dt.Rows.Count > i;i++ )
        //    {
        //        lbldesirable.Text = dt.Rows[i]["DesirableQualification"].ToString();
        //        lblvalues.Text = dt.Rows[i]["desirable"].ToString();
        //    }
        //    Desirable.Visible = true;
        //}
    }

    public void desirable1(int applid)
    {
        string appli_id = Convert.ToString(applid);
        //dt = objCandD.Getdesirable1(appli_id);
        DataTable dtreqid = objCandD.Get_fill_combdreqid(appli_id);
        if (dtreqid.Rows.Count > 0)
        {
            Combdreqid = dtreqid.Rows[0]["reqid"].ToString();
        }
        dt = objCandD.Getdeptcode(Combdreqid);
        string deptcode = dt.Rows[0]["deptcode"].ToString();
        if (deptcode == "COMBD")
        {
            dt = objCandD.Getdesirable1_combd(appli_id);
        }
        else
        {
            dt = objCandD.Getdesirable1(appli_id);
        }
        if (dt.Rows.Count > 0)
        {
            grd_desirableExp.DataSource = dt;
            grd_desirableExp.DataBind();
            //for (int i = 0; dt.Rows.Count > i; i++)
            //{
            //    LbldesirableExp.Text = dt.Rows[i]["desirableExperience"].ToString();
            //    Lblvalues1.Text = dt.Rows[i]["desirableExp"].ToString();
            //}
            DesirableExp.Visible = true;
            if (deptcode != "COMBD")  //rohitxd => departmentname datafield not required in same of non comb post and this is raising exception.
            {
                grd_desirableExp.Columns[1].Visible = false;
            }
        }
        else
        {
            grd_desirableExp.Visible = false;//17/11/2023
            DesirableExp.Visible = false;
        }
    }
    public void fillgrid_exp(int applid)//changed on 19/12/2023
    {
        string appli_id = Convert.ToString(applid);
        DataTable dtreqid = objCandD.Get_fill_combdreqid(appli_id);
        if (dtreqid.Rows.Count > 0)
        {
            Combdreqid = dtreqid.Rows[0]["reqid"].ToString();
        }
        DataTable ddt = objCandD.Getdeptcode(Combdreqid);
        string deptcode = ddt.Rows[0]["deptcode"].ToString();
        if (deptcode == "COMBD")
        {
            dt = objCandD.GetJobApplication_Exp_Combd(appli_id);
        }
        else
        {
            dt = objCandD.GetJobApplication_Exp(appli_id);
        }
        if (dt.Rows.Count > 0)
        {
            trExperience.Visible = true;
        }
        string exp_year = hf_expnoofyear.Value;
        if (exp_year != "0")
        {
            if (dt.Rows.Count > 0)
            {
                //trExperience.Visible = true;
                double exp_noofyear = double.Parse(exp_year);
                double exp_noofdays = (exp_noofyear * 365);
                double total_exp_days = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string DayFrome = dt.Rows[j]["datefrom"].ToString();
                    string DayTo = dt.Rows[j]["dateto"].ToString();
                    DateTime date_from = Utility.converttodatetime(DayFrome);
                    DateTime date_to = Utility.converttodatetime(DayTo);
                    TimeSpan t = date_to - date_from;
                    double nooodays = t.TotalDays;
                    total_exp_days += nooodays;
                }
                if (exp_noofdays > total_exp_days)
                {
                    flag = 1;
                    msg.Show("Experience is less then Requirement");
                    btnconform.Visible = false;
                    lblmsg.Visible = true;
                }
            }
            else
            {
                flag = 1;
                msg.Show("Please Enter the Essential Experience");
                btnconform.Visible = false;
                lblmsg.Visible = true;
            }
        }
        if (deptcode != "COMBD")  //rohitxd => departmentname datafield not required in same of non comb post and this is raising exception.
        {
            gvexp.Columns[1].Visible = false;
        }
        gvexp.DataSource = dt;
        gvexp.DataBind();
       
    }
    protected void btnedit_personal_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("apply.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);

    }
    protected void btn_editphoto_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("jobupload.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);
    }
   
    protected void lbphoto_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("jobupload.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);

    }
    protected void lbsign_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("jobupload.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);

    }
    protected void lbLTI_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("jobupload.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);

    }
    protected void lbRTI_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("jobupload.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);

    }
    protected void btnconform_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string jid = txtjid.Text;

        string groupno = "";
        if (!chk_decl.Checked || !chkPreiview.Checked)
        {
            msg.Show("Please Agree with the Declaration.");
        }
        else
        {
            for (int a = 0; a < gvquli.Rows.Count; a++)
            {
                if (gvquli.DataKeys[a].Value.ToString() != "")
                {
                    groupno = gvquli.DataKeys[a].Value.ToString();
                    break;
                }
            }
            DataTable checkquali = new DataTable();

          
            checkquali = objCandD.CheckEssentialQuali(Int32.Parse(applid), Int32.Parse(jid), groupno);

            DataTable dtchkqualiexmpt = objCandD.CheckWhetherqualiexmpted(jid);

            string eqvalidationexmpt = "", feeexmpt = "", agevalidationexmpt = "";
            if (dtchkqualiexmpt.Rows.Count > 0)
            {
                agevalidationexmpt = dtchkqualiexmpt.Rows[0]["agevalidationexmpt"].ToString();
                eqvalidationexmpt = dtchkqualiexmpt.Rows[0]["eqvalidationexmpt"].ToString();
                feeexmpt = dtchkqualiexmpt.Rows[0]["feeexmpt"].ToString();
            }
            if (checkquali.Rows.Count > 0 && eqvalidationexmpt == "")
            {
                msg.Show("Please fill all Essential Qualification");
                return;
            }
            //if (dtchkgroupquali.Rows.Count > 0 && eqvalidationexmpt == "")
            //{
            //    msg.Show("Please fill all Essential Qualification");
            //    return;
            //}

            // rohitxd 22/11/202
            try
            {
                string deptcode = objCandD.Getdeptcode1(jid);
                bool isvalid = objCandD.CheckEssentialQualiNew(Int32.Parse(applid), Int32.Parse(jid), deptcode);
                if (!isvalid)
                {
                    msg.Show("Please fill all Essential Qualification");
                    return;
                }
            }
            catch
            {
                //
            }

            if (!check_Experience(groupno))
            {
                msg.Show("Please Fill Required Experience");
                return;
            }
            else if (check_suitabilit() || agevalidationexmpt == "Y")
            {
                DataTable dt = objCandD.CheckFee(Int32.Parse(applid));
                string fee = dt.Rows[0]["feereq"].ToString();
                string serial_no = "";
                if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
                {
                    serial_no = Session["serial_no"].ToString();
                }

                long temp = objCandD.UpdateFinalSumitwithdummy_noTransaction(applid, jid, fee);

                if (temp > 0)
                {
                    Sms objsms = new Sms();
                    if (feeexmpt == "Y")
                    {
                        if (serial_no != "")
                        {
                            try
                            {
                                long appno = objCandD.InsertFeedetailwithdummy_noTransaction(applid, "", "0", "", Utility.formatDate(DateTime.Now), "Y", jid, "OLD");
                                if (appno > 0)
                                {
                                    //string msg2 = "Your Application for the post of " + lbladvt.Text + " has been finally submitted.Your Application no. is " + appno.ToString();
                                    string msg2 = "Your Application for the post of " + lbladvt.Text + " has been finally submitted. Your DSSSB Application No. is " + appno.ToString();
                                    //objsms.sendmsg(lblmobile.Text, msg2);
                                    string templateID = "1007161562121019384";
                                    objsms.sendmsgNew(lblmobile.Text, msg2, templateID);
                                    msg.Show(msg2);
                                    string url = "Home.aspx";
                                    Server.Transfer(url);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    else if (feeexmpt == "")
                    {
                        if (fee == "Y")
                        {
                            string url = md5util.CreateTamperProofURL("PayOnline.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
                            Response.Redirect(url);
                        }
                        else
                        {
                            string msg2 = "Your Application for the post of " + lbladvt.Text + " has been finally submitted. Your DSSSB Application No. is " + temp.ToString();
                            string templateID = "1007161562121019384";
                            objsms.sendmsgNew(lblmobile.Text, msg2, templateID);
                            msg.Show(msg2);
                            string url = "Home.aspx";
                            Server.Transfer(url);
                        }
                    }
                    //if (fee == "Y")
                    //{
                    //    if (serial_no == "" || (serial_no != "" && feeexmpt == ""))
                    //    {
                    //        Server.Transfer("PayOnline.aspx");
                    //    }
                    //    else if (serial_no != "" && feeexmpt == "Y")
                    //    {
                    //        try
                    //            {
                    //                //  int k = objCandD.insert_old_fee_data(applid, jid, name, ipo_no, amount, feetype, last_date);
                    //                long appno = objCandD.InsertFeedetailwithdummy_noTransaction(applid, "", "0", "", Utility.formatDate(DateTime.Now), "Y", jid, "OLD");
                    //                if (appno > 0)
                    //                {
                    //                    string msg2 = "Your Application for the post of " + lbladvt.Text + " has been finally submitted.Your Application no. is " + appno.ToString();
                    //                    objsms.sendmsg(lblmobile.Text, msg2);
                    //                    msg.Show(msg2);
                    //                    string url = "Home.aspx";
                    //                    Server.Transfer(url);
                    //                }
                    //            }
                    //            catch (Exception ex)
                    //            {

                    //            }
                    //         }
                    //}
                    //else
                    //{
                    //    string msg2 = "Your Application for the post of " + lbladvt.Text + " has been finally submitted.Your Application no. is " + temp.ToString();
                    //    objsms.sendmsg(lblmobile.Text, msg2);
                    //    msg.Show(msg2);
                    //    string url = "Home.aspx";
                    //    Server.Transfer(url);
                    //}
                }

            }
            else
            {
                msg.Show("Due to age criteria you are not eligible to apply for this post.");
            }

        }
    }

    private bool check_age(DateTime DOB_date, DateTime DOB_from_date, DateTime DOB_to_date, string contract_duration)
    {
        int cont_days = 0;

        if (contract_duration == "")
        {
            cont_days = 0;
        }
        else
        {
            cont_days = Int32.Parse(contract_duration);
        }
        //DOB_date = DOB_date.AddDays(0-cont_days);
        if (Yu == 0)
        {
            if (DateTime.Compare(DOB_from_date, DOB_date) <= 0 && DateTime.Compare(DOB_to_date, DOB_date) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan t = DOB_to_date - DOB_date;
            int years = (zeroTime + t).Year - 1;

            if (years <= Yu)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void YR_Age(string cat_code, string CatIndS, int reqid, int applid)
    {
        int v_dYear = 0;
        if (CatIndS == "C")
        {
            dt = objCandD.Getdeptcode(reqid.ToString());
            string deptcode = dt.Rows[0]["deptcode"].ToString();
            if (deptcode == "COMBD")
            {
                dt_age_relax = objCandD.age_relax_combd(reqid);
            }
            else
            {
                dt_age_relax = objCandD.age_relax(reqid);
            }

            DataRow[] dt_row = dt_age_relax.Select("CatCode='" + cat_code + "' AND CatIndCS='" + CatIndS + "'");
            foreach (DataRow r in dt_row)
            {

                v_dYear = Int32.Parse(r["D_Year"].ToString());
                //  if (CatIndS == "S" && Yscat < v_dYear)
                //{
                //  Yscat = v_dYear;
                //}
                // else if (CatIndS == "C")
                //{
                Yr = v_dYear;
                //}
                //if (r["Fee_exmp"].ToString() == "Y")
                //{
                //    feereq = "N";
                //}            
                if (r["CM"].ToString() == "U")
                {
                    v_dYear = Int32.Parse(r["D_Year"].ToString());
                    Yu = v_dYear;
                }

            }
        }
        else
        {
            dt = objCandD.Getdeptcode(reqid.ToString());
            string deptcode = dt.Rows[0]["deptcode"].ToString();
            if (deptcode == "COMBD")
            {
                dt_age_relax = objCandD.age_relax_combdforfinal(reqid, applid);
            }
            else
            {
                dt_age_relax = objCandD.age_relax(reqid, applid);
            }
            if (dt_age_relax.Rows.Count > 0)
            {
                v_dYear = Int32.Parse(dt_age_relax.Rows[0]["D_Year"].ToString());
                Yscat = v_dYear;
                if (dt_age_relax.Rows[0]["CM"].ToString() == "U")
                {
                    v_dYear = Int32.Parse(dt_age_relax.Rows[0]["D_Year"].ToString());
                    Yu = v_dYear;
                }
            }
        }
    }


    private bool check_suitabilit()
    {
        string cat = "";
        string scat = "";
        string ex_duration = "";
        string cont_duration = "";
        string reqid = "";
        bool ex_grpC_flag = false;
        string dob_from = "";
        string dob_to = "";
        string dob_date = "";
        string obcregion = "";

        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applids = txt_applid.Text;
        int applid = Int32.Parse(applids);
        // fill_application_data(applid);


        DataTable dt = new DataTable();
        dt = objCandD.fill_personal_data(applid);
        DataTable getsubcat = objCandD.getcandsubcatdetail(applid.ToString());
        if (dt.Rows.Count > 0)
        {
            cat = dt.Rows[0]["category"].ToString();
            if (getsubcat.Rows.Count > 0)
            {
                scat = getsubcat.Rows[0]["SubCat_code"].ToString();
            }
            ex_duration = dt.Rows[0]["ExServiceDuration"].ToString();
            cont_duration = dt.Rows[0]["ContractDuration"].ToString();
            reqid = dt.Rows[0]["reqid"].ToString();
            dob_from = dt.Rows[0]["DOBFrom"].ToString();
            dob_to = dt.Rows[0]["DOBTO"].ToString();
            dob_date = dt.Rows[0]["DOB"].ToString();
            obcregion = dt.Rows[0]["OBCRegion"].ToString();
        }
        YR_Age(cat, "C", Int32.Parse(reqid), applid);
        YR_Age("", "S", Int32.Parse(reqid), applid);
        if (scat.Contains("EX"))
        {
            ex_grpC_flag = true;
        }


        DateTime DOB_from_date;

        if (ex_grpC_flag == true)
        {
            Yr = Yscat;
            int len_ex_duration = Int32.Parse(ex_duration);
            DOB_from_date = DateTime.ParseExact(dob_from, "dd/MM/yyyy", new CultureInfo("en-US")).AddYears(0 - Yr).AddDays(0 - len_ex_duration);
        }
        else
        {

                double age = 0;
                age = years;
                DateTime DOBTo_date = DateTime.ParseExact(dob_to, "dd/MM/yyyy", new CultureInfo("en-US"));
                DOB_from_date = DateTime.ParseExact(dob_from, "dd/MM/yyyy", new CultureInfo("en-US"));
                DateTime DOBdate = DateTime.ParseExact(dob_date, "dd/MM/yyyy", new CultureInfo("en-US"));
                if (cat == "OBC" && obcregion == "O")
                {
                     Yr = 0;
                    if(DOB_from_date >= DOBdate && DOBTo_date >= DOBdate)
                    {
                      if (Yu > 0)
                            {
                                Yr = Yu;
                            }    
                      else if(Yscat > 0)
                        {
                            Yr = Yscat;
                        }
                      else
                       {
                        msg.Show("you are not eligible for this post");
                        return false;
                       }
                    }               
                }
                else
                   {
                    Yr = Yr + Yscat;
                   }
                DOB_from_date = DateTime.ParseExact(dob_from, "dd/MM/yyyy", new CultureInfo("en-US")).AddYears(0 - Yr);
                }
        if (Yu > 0)
        {
            //27 jan 2023 age relaxation
            double age = 0;
            DateTime DOBdate = DateTime.ParseExact(dob_date, "dd/MM/yyyy", new CultureInfo("en-US"));
            //age = DateTime.Now.AddYears(-DOBdate.Year).Year;
            age = years;// Dated: 28-02-2023
            if (Yu <= age && (cat == "UR" ||cat == "EWS"))
            {
                if (age == Yu)
                {
                    if (months > 0 || days > 0)
                    {
                        msg.Show("You are not Eligible for this post");
                        return false;
                    }
                }
                else
                {
                    msg.Show("You are not Eligible for this post");
                    return false;
                }

            }
            else if (cat == "SC" || cat == "ST" || cat == "OBC")
            {
                Yu = Yr;
                if (Yu <= age)
                {
                    if (age == Yu)
                    {
                        if (months > 0 || days > 0)
                        {
                            msg.Show("You are not Eligible for this post");
                            return false;
                        }
                    }
                    else
                    {

                        msg.Show("You are not Eligible for this post");
                        return false;
                    }
                }
            }
        }
        DateTime DOB_to_date = DateTime.ParseExact(dob_to, "dd/MM/yyyy", new CultureInfo("en-US"));
        DateTime DOB_date = DateTime.ParseExact(dob_date, "dd/MM/yyyy", new CultureInfo("en-US"));

        bool chk_flag = check_age(DOB_date, DOB_from_date, DOB_to_date, cont_duration);

        return chk_flag;
    }
    protected void btn_addphoto_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("jobupload.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);
    }
    protected void btn_addexp_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("Experience.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);
    }
    protected void btn_editexp_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        string url = md5util.CreateTamperProofURL("Experience.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(applid, true));
        Response.Redirect(url);
    }

    protected void btnpay_Click(object sender, EventArgs e)
    {
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        if (chk_decl.Checked && chkPreiview.Checked)
        {

            string url = md5util.CreateTamperProofURL("PayOnline.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
            Response.Redirect(url);
        }
        else
        {

            msg.Show("Please Agree with the Declaration.");
        }
    }
    private bool check_Experience(string groupno)
    {
        bool expflag = false;
        TextBox txt_applid = (TextBox)this.Parent.FindControl("txtapplid");
        string applid = txt_applid.Text;
        DataTable dtqexp = objCandD.Getexpyears(hfreqid.Value, groupno);
        if (dtqexp.Rows.Count > 0)
        {
            int expyear = 0;
            if (dtqexp.Rows[0]["exp_noofyears"].ToString() != "" && dtqexp.Rows[0]["exp_noofyears"] != null)
            {
                expyear = Convert.ToInt32(dtqexp.Rows[0]["exp_noofyears"]);
            }
            if (expyear != 0)
            {
                double exp_noofyear = double.Parse(expyear.ToString());
                double exp_noofdays = (exp_noofyear * 365);
                double total_exp_days = 0;
                DataTable getviewSS = objCandD.Getexpdetails(applid);
                for (int j = 0; j < getviewSS.Rows.Count; j++)
                {
                    string DayFrome = getviewSS.Rows[j]["datefrom"].ToString();
                    string DayTo = getviewSS.Rows[j]["dateto"].ToString();
                    DateTime date_from = DateTime.ParseExact(DayFrome, "dd/MM/yyyy", new CultureInfo("en-US"));
                    DateTime date_to = DateTime.ParseExact(DayTo, "dd/MM/yyyy", new CultureInfo("en-US"));

                    TimeSpan t = date_to - date_from;
                    double nooodays = t.TotalDays;
                    total_exp_days += nooodays;
                }
                if (exp_noofdays > total_exp_days)
                {
                    expflag = false;
                }
                else
                {
                    expflag = true;
                }
            }
            else
            {
                expflag = true;
            }

        }
        else
        {
            expflag = true;
        }
        return expflag;
    }
}
