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

using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

using System.Linq;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto;

public partial class Apply : BasePage
{
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    string advt_no = "";
    string postCode = "";
    string jobtitle = "";
    string dobfrom = "";
    string dobto = "";
    int applid;
    CandidateData objCandD = new CandidateData();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    message msg = new message();
    //public int Yr = 0;
    //public int Yu = 0;
    //public int Yscat = 0;
    MD5Util md5util = new MD5Util();
    DataTable dt_age_relax;
    string name = "";
    string fname = "";
    string mname = "";
    string category = "";
    string dob = "";
    string gender = "";
    string nationality = "";
    string mob = "";
    string email = "";
    string regno = "";
    string jid = "";
    string reqid = "";
    string endson = "";
    bool check_debar_flag = true;
    bool check_issue_date_flag = true;
    string jobDesc = "";
    string startsfrom_org = "";
    string endson_org = "";
    string agevalidationexmpt = "";
    //string eqvalidationexmpt = "";
    string spname = "";
    byte[] PH_docSize = null;
    static int years = 0;//28-02-2023
    static int months = 0;
    static int days = 0;
    int maxage = 0;
    public int otherage = 0;
    List<int> relaxedage = new List<int>();
    DataTable dtdata = new DataTable();


    //Added by AnkitaSingh Dated:14-12-2022 for ExceptionLog
    #region LogException
    void LogException(Exception ex)
    {
        HttpBrowserCapabilities browser = Request.Browser;
        String browserInfo = "Browser Name:" + browser.Browser + " and Browser Version:" + browser.Version;
        ExceptionLog obj = new ExceptionLog();
        try
        {
            obj.SaveException(ex, "Apply", GetIPAddress(), "", browserInfo);
        }
        catch
        {
            //
        }
        //Server.Transfer("ErrorPage.aspx");
    }
    #endregion

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dtdata = objCandD.getdetail(Session["rid"].ToString());
        regno = Session["rid"].ToString();
        Lbl_name.Text = dtdata.Rows[0]["name"].ToString();
        Lbl_guardian.Text = dtdata.Rows[0]["fname"].ToString();
        CalendarExtender8.EndDate = DateTime.Now;

        if (!IsPostBack)
        {
            if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
            {
                string p_code = "";
                if (Request.QueryString["postCode"] != null)
                {
                    p_code = MD5Util.Decrypt(Request.QueryString["postCode"].ToString(), true);
                }

                //string serial_no = Session["serial_no"].ToString();

                //DataTable dt_serial = objCandD.get_serial_data(regno, p_code);
                //if (dt_serial.Rows.Count > 0)
                //{
                //    name = dt_serial.Rows[0]["name"].ToString();
                //    fname = dt_serial.Rows[0]["f_name"].ToString();
                //    category = dt_serial.Rows[0]["cat"].ToString();
                //}
            }
            fname = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["fname"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["fname"].ToString()));
            mname = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["mothername"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["mothername"].ToString()));
            spname = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["spousename"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["spousename"].ToString()));
            name = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["name"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["name"].ToString()));

        }
        dob = Session["birthdt"].ToString();
        gender = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["sex"].ToString()));
        //Session["gender"].ToString();
        nationality = Session["nationality"].ToString();
        mob = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["mobileno"].ToString()));// Session["mobileno"].ToString();
        //Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["address_per"].ToString()));
        email = Utility.getstring(Server.HtmlEncode(dtdata.Rows[0]["email"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["email"].ToString()));

        btn_update.Visible = false;
        Session.Remove("physic");
        if (!IsPostBack)
        {
            if (StringUtil.GetQueryString(Request.Url.ToString()) != null)
            {
                if (md5util.IsURLTampered(StringUtil.GetWithoutDigest(StringUtil.GetQueryString(Request.Url.ToString())),
                     StringUtil.GetDigest(StringUtil.GetQueryString(Request.Url.ToString()))) == true)
                {
                    Response.Redirect("home.aspx");
                }

            }
        }
        if (Request.QueryString["update"] != null)
        {
            if (MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "1" || MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "P")
            {
                btn_insert.Visible = false;
                btn_update.Visible = true;
                String Temp_applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                applid = int.Parse(Temp_applid);
                reqid = hidden_jid.Value;
                if (!IsPostBack)
                {
                    fill_candidate_form(applid);
                    fill_physical(jid);
                }
            }
        }
        else
        {

            advt_no = MD5Util.Decrypt(Request.QueryString["advt_no"].ToString(), true);
            postCode = MD5Util.Decrypt(Request.QueryString["postCode"].ToString(), true);
            jobtitle = MD5Util.Decrypt(Request.QueryString["jobtitle"].ToString(), true);
            dobfrom = MD5Util.Decrypt(Request.QueryString["dobfrom"].ToString(), true);
            dobto = MD5Util.Decrypt(Request.QueryString["dobto"].ToString(), true);
            jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
            reqid = MD5Util.Decrypt(Request.QueryString["reqid"].ToString(), true);
            endson = MD5Util.Decrypt(Request.QueryString["endson"].ToString(), true);
            jobDesc = MD5Util.Decrypt(Request.QueryString["JobDesc"].ToString(), true);
            endson_org = MD5Util.Decrypt(Request.QueryString["endson_org"].ToString(), true);
            if (Request.QueryString["agevalidationexmpt"] != null)
            {
                agevalidationexmpt = MD5Util.Decrypt(Request.QueryString["agevalidationexmpt"].ToString(), true);
            }
            //if (Request.QueryString["eqvalidationexmpt"] != null)
            //{
            //    eqvalidationexmpt = MD5Util.Decrypt(Request.QueryString["eqvalidationexmpt"].ToString(), true);
            //}
            lbl_post_code.Text = postCode;
            lbl_advt.Text = advt_no;
            lbl_app.Text = Utility.getstring(jobtitle);
            lbl_dob_f.Text = dobfrom;
            lbl_dob_t.Text = dobto;
            //dob = Session["birthdt"].ToString();

            DateTime dt = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime enddate = DateTime.ParseExact(endson, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime today = DateTime.Now;
            TimeSpan ts = enddate - dt;
            DateTime age = DateTime.MinValue + ts;
            years = age.Year - 1;//28-02-2023
            months = age.Month - 1;
            days = age.Day - 1;
            string age_diff = years.ToString() + " Year/s " + months.ToString() + " Month/s " + days.ToString() + " Day/s";
            lblCandidateAge.Text = Convert.ToString(years) + " Years";

            lblEndDate.Text = endson + " (Last Date of Advertisement) :";

            fill_physical(jid);
            fill_weight(jid);
            if (!IsPostBack)
            {
                fill_data();
                UniqueRandomNumber = randObj.Next(1, 10000);
                Session["token"] = UniqueRandomNumber.ToString();
                this.csrftoken.Value = Session["token"].ToString();

                TDExservFromDate.Visible = true;
                TDExservToDate.Visible = true;
                TDDebbaredDateorder.Visible = false;
                TDDebbaredYear.Visible = false;
                populateCasteCategory();
                populate_subcatCheckbox();
                populateState();
                pupulate_radio_ph_subcat();
            }
            else
            {
                if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
                {
                    //valid Page
                }
                else
                {
                    //Response.Redirect("ErrorPage.aspx");
                }
            }
        }
    }

    public void fill_data()
    {

        txt_name.Text = name;
        txt_fh_name.Text = fname;
        txt_mothername.Text = mname;
        txt_mob.Text = mob;
        txt_email.Text = email;
        txt_DOB.Text = dob;
        DDL_Nationality.SelectedIndex = DDL_Nationality.Items.IndexOf(DDL_Nationality.Items.FindByText(nationality));
        RadioButtonList_mf.SelectedValue = gender;
        txtspouse.Text = spname;
        if (txt_fh_name.Text == "")
        {
            txt_fh_name.Enabled = true;
        }
        else
        {
            txt_fh_name.Enabled = false;
        }
        if (txt_mothername.Text == "")
        {
            txt_mothername.Enabled = true;
        }
        else
        {
            txt_mothername.Enabled = false;
        }
        if (txtspouse.Text == "")
        {
            txtspouse.Enabled = true;
        }
        else
        {
            txtspouse.Enabled = false;
        }
        //if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
        //{
        //    DropDownList_cat.SelectedValue = category;
        //    DropDownList_cat.Enabled = false;
        //}
    }
    public void fill_physical(String jid)
    {
        DataTable dt = objCandD.get_physical_standard(jid, RadioButtonList_mf.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Ess_Height_LowerFt"].ToString() == "")
            {
                lblHLLDFt.Visible = false;
            }
            else
            {
                lblHLLDFt.Visible = true;
                lblHLLDFt.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Ess_Height_LowerFt"].ToString()))) + "Feet ";
            }
            if (dt.Rows[0]["Desi_Height_UpperFt"].ToString() == "")
            {
                lblHULDFt.Visible = false;
            }
            else
            {
                lblHULDFt.Visible = true;
                lblHULDFt.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Desi_Height_UpperFt"].ToString()))) + "Feet ";
            }
            if (dt.Rows[0]["Desi_Chest_LowerFt"].ToString() == "")
            {
                lblCLLDFt.Visible = false;
            }
            else
            {
                lblCLLDFt.Visible = false;
                lblCLLDFt.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Desi_Chest_LowerFt"].ToString()))) + "Feet ";
            }
            if (dt.Rows[0]["Desi_Chest_UpperFt"].ToString() == "")
            {
                lblCULDFt.Visible = false;
            }
            else
            {
                lblCULDFt.Visible = false;
                lblCULDFt.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Desi_Chest_UpperFt"].ToString()))) + "Feet ";
            }



            if (dt.Rows[0]["Ess_Height_LowerIn"].ToString() == "")
            {
                lblHLLDIn.Visible = false;
            }
            else
            {
                lblHLLDIn.Visible = true;
                lblHLLDIn.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Ess_Height_LowerIn"].ToString()))) + "Inch ";
            }
            if (dt.Rows[0]["Height_Relax_In"].ToString() == "")
            {
                lblHULDIn.Visible = false;
            }
            else
            {
                lblHULDIn.Visible = true;
                lblHULDIn.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Height_Relax_In"].ToString()))) + "Inch ";
            }
            if (dt.Rows[0]["Ess_Chest_LowerIn"].ToString() == "")
            {
                lblCLLDIn.Visible = false;
            }
            else
            {
                lblCLLDIn.Visible = true;
                lblCLLDIn.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Ess_Chest_LowerIn"].ToString()))) + "Inch ";
            }
            if (dt.Rows[0]["Ess_Chest_UpperIn"].ToString() == "")
            {
                lblCULDIn.Visible = false;
            }
            else
            {
                lblCULDIn.Visible = true;
                lblCULDIn.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Ess_Chest_UpperIn"].ToString()))) + "Inch ";
            }


            if (dt.Rows[0]["Ess_Height_LowerCm"].ToString() == "")
            {
                lblHLLD.Visible = false;
            }
            else
            {
                lblHLLD.Visible = true;
                lblHLLD.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Ess_Height_LowerCm"].ToString()))) + "cm ";
            }
            if (dt.Rows[0]["Height_Relax_cm"].ToString() == "")
            {
                lblHULD.Visible = false;
            }
            else
            {
                lblHULD.Visible = true;
                lblHULD.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Height_Relax_cm"].ToString()))) + "cm ";
            }
            if (dt.Rows[0]["Ess_Chest_LowerCm"].ToString() == "")
            {
                lblCLLD.Visible = false;
            }
            else
            {
                lblCLLD.Visible = true;
                lblCLLD.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Ess_Chest_LowerCm"].ToString()))) + "cm ";
            }
            if (dt.Rows[0]["Ess_Chest_UpperCm"].ToString() == "")
            {
                lblCULD.Visible = false;
            }
            else
            {
                lblCULD.Visible = true;
                lblCULD.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Ess_Chest_UpperCm"].ToString()))) + "cm ";
            }

            if (dt.Rows[0]["Chest_Relax_In"].ToString() == "")
            {
                lbl_cst_rex_in.Visible = false;
            }
            else
            {
                lbl_cst_rex_in.Visible = true;
                lbl_cst_rex_in.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Chest_Relax_In"].ToString()))) + "Inch ";
            }
            if (dt.Rows[0]["Chest_Relax_cm"].ToString() == "")
            {
                lbl_cst_rex_cm.Visible = false;
            }
            else
            {
                lbl_cst_rex_cm.Visible = true;
                lbl_cst_rex_cm.Text = Utility.getstring(Server.HtmlEncode((dt.Rows[0]["Chest_Relax_cm"].ToString()))) + "cm ";
            }

            if (dt.Rows[0]["Desi_SoundHealthFreefromDefectDeformityDesease"].ToString() == "Y")
            {
                chkSoundD.Checked = true;
            }
            if (dt.Rows[0]["Desi_Vision6withoutGlassesbothEyes"].ToString() == "Y")
            {
                chkVisionD.Checked = true;
            }
            if (dt.Rows[0]["Desi_FreeFromColourBlindness"].ToString() == "Y")
            {
                chkcolorblindD.Checked = true;
            }
            Session["physic"] = "Y";
            if (dt.Rows[0]["SCST_Relax"].ToString() == "Y")
            {
                Session["SCST_flag"] = "Y";
            }
            else
            {
                Session["SCST_flag"] = "N";
            }
        }
        else
        {
            tr_physic.Visible = false;
            tr_physic_accept.Visible = false;
        }
    }
    public void fill_weight(String jid)
    {
        DataTable dt = objCandD.get_weight_standard(jid);
        if (dt.Rows.Count > 0)
        {
            if (RadioButtonList_mf.SelectedValue == "M")
            {
                td_1_female.Visible = false;
                td_2_female.Visible = false;
                if (dt.Rows[0]["weight_male"].ToString() != "")
                {
                    lbl_w_male.Text = dt.Rows[0]["weight_male"].ToString() + "Kg.";
                }
            }
            else if (RadioButtonList_mf.SelectedValue == "T")
            {
                td_1_transgender.Visible = false;
                td_2_transgender.Visible = false;
                if (dt.Rows[0]["weight_transgender"].ToString() != "")
                {
                    lbl_w_transgender.Text = dt.Rows[0]["weight_transgender"].ToString() + "Kg.";
                }
            }
            else if (RadioButtonList_mf.SelectedValue == "F")
            {
                td_1_male.Visible = false;
                td_2_male.Visible = false;
                if (dt.Rows[0]["weight_female"].ToString() != "")
                {
                    lbl_w_female.Text = dt.Rows[0]["weight_female"].ToString() + "Kg.";
                }
            }
            else
            {
            }

        }
        else
        {
            tr_weight.Visible = false;
            //tr_physic_accept.Visible = false;
        }
    }

    public void populateCasteCategory()
    {
        CandidateData objCandD = new CandidateData();
        DataTable dt = new DataTable();
        dt = objCandD.SelectCasteType();
        DropDownList_cat.Items.Clear();
        DropDownList_cat.DataTextField = "categcode";
        DropDownList_cat.DataValueField = "categcode";
        DropDownList_cat.DataSource = dt;
        DropDownList_cat.DataBind();
        ListItem l1 = new ListItem();
        l1.Text = "Select";
        l1.Value = "-1";
        DropDownList_cat.Items.Insert(0, l1);
        dt = null;
    }

    public void FillDropDown(DropDownList ddl, DataTable dt, string textfield, string valuefield)
    {
        ddl.Items.Clear();
        ddl.DataTextField = textfield;
        ddl.DataValueField = valuefield;
        ddl.DataSource = dt;
        ddl.DataBind();
        ListItem l1 = new ListItem();
        l1.Text = "Select";
        l1.Value = "-1";
        ddl.Items.Insert(0, l1);

    }

    public void FillCheckboxSubCat(CheckBoxList chboxSubCat, DataTable dt, string textfield, string valuefield)
    {

        chboxSubCat.Items.Clear();
        chboxSubCat.DataTextField = textfield;
        chboxSubCat.DataValueField = valuefield;
        chboxSubCat.DataSource = dt;
        chboxSubCat.DataBind();

    }

    private void populate_subcatCheckbox()
    {
        CandidateData objCandD = new CandidateData();
        //****************************25/08/2023*************************
        DataTable dt1 = objCandD.Getdeptcode(reqid);
        string deptcode = dt1.Rows[0]["deptcode"].ToString();
        // ******************************************************************
        DataTable dt = objCandD.get_sub_cat(reqid, deptcode);
        FillCheckboxSubCat(CheckBoxList_Subcategory, dt, "subcat_name", "SubCat_code");
    }


    protected void btn_insert_Click(object sender, EventArgs e)
    {
        if (lbl_post_code.Text == "28/23" && DropDownList_cat.SelectedItem.Text != "EWS")//03-03-2023
        {
            msg.Show("This vacancy is only for EWS category");
            return;
        }
        if (lbl_post_code.Text == "62/23" && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "EWS" || DropDownList_cat.SelectedItem.Text == "OBC"))//14-08-2023
        {
            msg.Show("This vacancy is only for SC/ST category");
            return;
        }
        if (lbl_post_code.Text == "104/23" && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "OBC"))//14-08-2023
        {
            msg.Show("This vacancy is not for UR and OBC Candidates");
            return;
        }
        if (lbl_post_code.Text == "03/24" && (DropDownList_cat.SelectedItem.Text == "ST"))//01-02-2024
        {
            msg.Show("This vacancy is not for ST Candidates");
            return;
        }
        if (lbl_post_code.Text == "07/24" && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "EWS" || DropDownList_cat.SelectedItem.Text == "ST"))//09-02-2024
        {
            msg.Show("This vacancy is only reserved for OBC and SC Candidates ");
            return;
        }
        if ((lbl_post_code.Text == "18/24" || lbl_post_code.Text == "51/24") && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "EWS" || DropDownList_cat.SelectedItem.Text == "ST" || DropDownList_cat.SelectedItem.Text == "SC" || (DropDownList_cat.SelectedItem.Text == "OBC" && rbtobcregion.SelectedValue == "O")))//18-03-2023
        {
            msg.Show("This vacancy is only reserved for OBC Candidates");
            return;
        }
        if (lbl_post_code.Text == "46/24" && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "EWS" || (DropDownList_cat.SelectedItem.Text == "OBC" && rbtobcregion.SelectedValue == "O")))//18-03-2023
        {
            msg.Show("This vacancy is not reserved for UR and EWS Candidates");
            return;
        }
        if (lbl_post_code.Text == "76/23" || lbl_post_code.Text == "77/23")//13th march2024
        {
            bool isPHSelected = false;
            for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
            {
                var x = CheckBoxList_Subcategory.Items[i];

                if (x.Selected && x.Value == "PH")
                {
                    isPHSelected = true;
                    break;
                }
            }
            if (!isPHSelected)
            {
                msg.Show("This vacancy is only reserved for PWD Candidates");
                return;
            }
        }
        //Regex reg_cerno = new Regex(@"^[a-zA-Z0-9/-][\s]");
        Regex reg_cerno = new Regex(@"^[\w\s-/\d]*$");
        bool ex_grpC_flag = false;

        string ph_visual = "";
        string ph_hearing = "";
        string ph_ortho = "";
        string physic = "";

        string cat_flag = "";
        string subcat_flag = "";
        string subsubcat_flag = "";
        string subsub_subcat_flag = "";

        // reqid = MD5Util.Decrypt(Request.QueryString["reqid"].ToString(), true);
        //DataTable reqReopenPost = objCandD.CheckReopenPostOnUpdateApplication(reqid, CheckBoxList_Subcategory.SelectedValue);
        // if (reqReopenPost.Rows.Count <= 0)
        //{
        //      msg.Show("This Post reopened for special criteria candidtes.Kindly check eligibility criteria.");
        //  }
        // else
        //  {
        if (Session["physic"] != null && Session["physic"].ToString() == "Y")
        {
            if (RadioButtonList_physcic_accept.SelectedValue == "Y")
            {
                //physic_accept = true;
                physic = "Y";
            }
            else if (RadioButtonList_physcic_accept.SelectedValue == "N")
            {
                //physic_accept = false;
                physic = "N";
            }
            else
            {
                //physic_accept = null;
                physic = "";
            }
        }



        if (chk_decl.Checked)
        {
            List<string> SelectedSubCats = new List<string>();

            for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
            {
                if (CheckBoxList_Subcategory.Items[i].Selected)
                {
                    string scat = CheckBoxList_Subcategory.Items[i].Value;
                    if (!string.IsNullOrEmpty(scat.Trim()))
                    {
                        SelectedSubCats.Add(scat.Trim());
                    }
                }
            }
            int CertIssueState = 0;
            string GovtDateJoin = txt_dob_dgs.Text;
            string NoncreamylayerDATE = txtbox_noncreamylayerDATE.Text;

            if (NoncreamylayerDATE != "")
            {
                //Added by AnkitaSingh Dated: 16-02-2023
                DataTable d = new DataTable();
                d = objCandD.GetAdvtEndDate(lbl_post_code.Text.Trim());
                DateTime a = Convert.ToDateTime(d.Rows[0]["EndsOn"].ToString());
                DateTime t_obc = DateTime.ParseExact(NoncreamylayerDATE, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                if (t_obc <= a)
                {
                    NoncreamylayerDATE = txtbox_noncreamylayerDATE.Text;
                }
                else
                {
                    NoncreamylayerDATE = "";
                    txtbox_noncreamylayerDATE.Text = "";
                    msg.Show("Issuing date must be prior to or equal to the cut-off date of advertisement");
                }
            }


            string ip = GetIPAddress();
            string DOB = txt_DOB.Text;
            string CertUssueAuth = txt_cert_issue_auth.Text;

            ph_visual = radio_vh.SelectedValue;
            ph_hearing = radio_hh.SelectedValue;
            ph_ortho = radio_oh.SelectedValue;

            if (DropDownList_c_state.SelectedValue != "")
            {
                CertIssueState = Convert.ToInt32(DropDownList_c_state.SelectedValue);
            }
            string fromdate = txt_ex_f_date.Text;
            string todate = txt_ex_t_date.Text;

            string DebarredDate = txt_d_date.Text;
            if (DropDownList_cat.SelectedItem.Text.Trim() == "Select")
            {
                msg.Show("Select category to proceed");
                return;
            }
            if (SelectedSubCats.Contains("PH"))//check box for Differentially Able
            {
                if (CheckboxList_PHSubCat.SelectedValue == "")
                {
                    msg.Show("Select Category of Disability");
                    return;
                }
                else if (CheckboxList_PHSubCat.SelectedValue == "1")
                {
                    dt = objCandD.get_sub_ph("Visual (VH)", Int32.Parse(reqid));
                    if (dt.Rows.Count > 0)
                    {
                        if (radio_vh.SelectedValue == "")
                        {
                            msg.Show("Select Sub Category of VH Disability");
                            return;
                        }
                    }
                    else { }
                }
                //bellow code is commented not required for Sub-SUb category
                //else if (CheckboxList_PHSubCat.SelectedValue == "2")
                //{
                //    if (radio_hh.SelectedValue == "")
                //    {
                //        msg.Show("Select Sub Category of HH Disability");
                //        return;
                //    }

                //}
                else if (CheckboxList_PHSubCat.SelectedValue == "3")
                {
                    if (radio_oh.SelectedValue == "")
                    {
                        msg.Show("Select Sub Category of OH Disability");
                        return;
                    }

                }
                else
                {

                }
            }



            // for PH certificate file Upload
            if (SelectedSubCats.Contains("PH"))
            {
                if (txtIssuingauthority.Text.Trim() == "" || txtIssuedate.Text.Trim() == "" || txtCertificateNo.Text.Trim() == "")
                {
                    msg.Show("Enter PH Certificate Details");
                    return;
                }
                //else if (PHCertUpload.PostedFile.ContentLength <= 0 || ViewState["PhDoc"] == "")
                else if (ViewState["PhDoc"] == "")
                {
                    msg.Show("Please upload disability Certificate");
                    return;
                }
                //if(phdocviewstate!=null)
                //{}
                else
                {
                    if (ViewState["PhDoc"] != null && ViewState["PhDoc"] != "")
                    {
                        //PH_docSize = new byte[PHCertUpload.PostedFile.ContentLength];
                        //try
                        //{
                        //    if (PHCertUpload.PostedFile != null && PHCertUpload.PostedFile.FileName != "")
                        //    {
                        //        string filename = PHCertUpload.PostedFile.FileName.ToString();
                        //        string[] FileExtension = filename.Split('.');
                        //        string ext = System.IO.Path.GetExtension(PHCertUpload.PostedFile.FileName).ToLower();
                        //        if (ext != ".pdf" && ext != ".PDF")
                        //        {
                        //            msg.Show("only pdf Files are allowed");
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            if (Convert.ToDouble(PH_docSize.Length) / (1024 * 1024) <= 2)
                        //            {
                        //                HttpPostedFile uploadedImage = PHCertUpload.PostedFile;
                        //                uploadedImage.InputStream.Read(PH_docSize, 0, (int)PHCertUpload.PostedFile.ContentLength);
                        //                bool checkfiletype = chkfiletype(PH_docSize, ext);
                        //                if (checkfiletype)
                        //                {
                        //                }
                        //                else
                        //                {
                        //                    msg.Show("only pdf Files are allowed");
                        //                    return;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                msg.Show("Select Maximum file size 1 MB");
                        //                return;
                        //            }
                        //        }
                        //    }
                        //}
                        //catch (Exception ex) { }
                        //string PhViewstate = ViewState["PhDoc"].ToString();

                        PH_docSize = ViewState["PhDoc"] as byte[];//System.Text.Encoding.ASCII.GetBytes(PhViewstate);
                    }
                    else
                    {
                        msg.Show("Please upload disability Certificate");
                        return;
                    }
                }
            }
            else { }

            if (SelectedSubCats.Contains("CESD"))
            {
                //if (ViewState["PhDoc"] == "") 03-03-2023
                //{
                //    msg.Show("Please upload the Certificate");
                //    return;
                //}
                //else
                //{
                //    if (ViewState["PhDoc"] != null && ViewState["PhDoc"] != "")
                //    {
                //        PH_docSize = ViewState["PhDoc"] as byte[];//System.Text.Encoding.ASCII.GetBytes(PhViewstate);
                //    }
                //    else
                //    {
                //        msg.Show("Please upload required Certificate");
                //        return;
                //    }


                //}
            }

            if (Validation.chkescape(txt_name.Text))
            {
                msg.Show("Invalid Character in Name");
            }
            else if (Validation.chkLevel20(txt_fh_name.Text))
            {
                msg.Show("Invalid Character in Father Name");

            }
            else if (Validation.chkLevel20(txt_mothername.Text))
            {
                msg.Show("Invalid Character in Mother Name");

            }
            else if (Validation.chkLevel17(txt_pre_add.Text) || txt_pre_add.Text.Length > 200)
            {
                msg.Show("Invalid Character in Present address or Address length is more than 200 Characters.");

            }
            else if (Validation.chkLevel17(txt_par_add.Text) || txt_par_add.Text.Length > 200)
            {
                msg.Show("Invalid Character in Permanent address or Address length is more than 200 Characters.");

            }
            else if ((DropDownList_cat.SelectedValue != "UR") && (txtbox_noncreamylayer.Text == "" || txtbox_noncreamylayerDATE.Text == "" || DropDownList_c_state.SelectedValue == "-1" || txt_cert_issue_auth.Text == ""))
            {
                msg.Show("Please fill the Certificate No,Certificate Issue Date and State");
            }
            else if (RadioButtonList_d.SelectedValue == "Y" && txt_d_date.Text == "")
            {
                msg.Show("Please fill Debar Date");
            }

            else if (Validation.chkescape(txtbox_noncreamylayer.Text) || !reg_cerno.IsMatch(txtbox_noncreamylayer.Text) || txtbox_noncreamylayer.Text.Length > 20)
            {
                msg.Show("Invalid Character in Creamylayer Certificate No.");
            }
            else if (Validation.chkLevel13(txtbox_noncreamylayerDATE.Text))
            {
                msg.Show("Invalid Character in Creamylayer Date");

            }
            else if (Validation.chkescape(txt_cert_issue_auth.Text))
            {
                msg.Show("Invalid Character in Certificate Issuing Authority");

            }
            else if (Session["physic"] != null && Session["physic"].ToString() == "Y" && (physic == null || physic == ""))
            {
                msg.Show("Please check the physical acceptance.");
            }
            else if (!check_reopen_eligibility(DropDownList_cat.SelectedValue, CheckBoxList_Subcategory.SelectedValue, CheckboxList_PHSubCat.SelectedValue))
            {
                msg.Show("This Post reopened for special criteria candidtes.Kindly check eligibility criteria.");
            }
            else if (!check_ex_service_date(txt_ex_f_date.Text, txt_ex_t_date.Text, endson_org))
            {

            }
            else if (txt_pre_pin.Text == "")
            {
                msg.Show("Please Enter Address Pincode");
            }
            else if (DropDownList_cat.SelectedValue == "OBC" && rbtobcregion.SelectedValue == "")
            {
                msg.Show("Please select OBC Delhi/Outside Delhi");
            }
            else if ((DropDownList_cat.SelectedValue == "OBC" && DropDownList_c_state.SelectedValue != "7" && rbtobcregion.SelectedValue == "D") && (ddlOBCForM.SelectedValue == "" || txtbox_noncreamylayer_f.Text == "" || txtbox_noncreamylayerDATE_f.Text == "" || txt_c_state_f.Text == "" || txt_cert_issue_auth_f.Text == ""))
            {
                msg.Show("Please fill Father OBC Certificate Details");
            }
            else if (RadioButtonList_m_status.SelectedValue == "M" && txtspouse.Text == "")
            {
                msg.Show("Please enter Spouse Name.");
            }
            else if (Validation.chkLevel20(txtspouse.Text))
            {
                msg.Show("Invalid Character in Spouse Name");

            }
            else
            {
                dt = objCandD.Getdeptcode(reqid);
                string deptcode = dt.Rows[0]["deptcode"].ToString();
                if (deptcode == "COMBD")
                {
                    dt_age_relax = objCandD.age_relax_combd(Int32.Parse(reqid));
                }
                else
                {
                    dt_age_relax = objCandD.age_relax(Int32.Parse(reqid));
                }
                YR_Age(DropDownList_cat.SelectedValue, "C");
                //string[] subcat = new string[CheckBoxList_Subcategory.Items.Count];
                //string sub_cat = "";
                //string cat_subcat = "";
                //for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
                //{
                //    if (CheckBoxList_Subcategory.Items[i].Selected)
                //    {
                //        subcat[i] = CheckBoxList_Subcategory.Items[i].Value;
                //        if (subcat[i] != null)
                //        {
                //            YR_Age(subcat[i].ToString(), "S");                         
                //            sub_cat = sub_cat + CheckBoxList_Subcategory.Items[i].Value + ",";
                //            if (subcat[i] == "EX" || subcat[i] == "ExSM")
                //            {
                //                ex_grpC_flag = true;
                //            }
                //        }
                //    }
                //}

                foreach (var subcat in SelectedSubCats)
                {
                    YR_Age(subcat, "S");
                    if (subcat == "EX" || subcat == "ExSM")
                    {
                        ex_grpC_flag = true;
                    }
                }
                if (relaxedage.Count > 0)
                {
                    relaxedage.Sort();
                    maxage = relaxedage.Last();
                }
                else
                {
                    maxage = objCandD.GetMaxAge(reqid);
                }


                string cat_subcat = DropDownList_cat.SelectedValue;
                if (SelectedSubCats.Count > 0)
                {
                    cat_subcat = DropDownList_cat.SelectedValue + "," + string.Join(",", SelectedSubCats);
                }

                string feereq = check_fee_relax(cat_subcat);

                if (feereq == "Y")
                {
                    feereq = check_fee_relax_female(RadioButtonList_mf.SelectedValue);
                }

                DateTime DOB_from_date;
                int length_ex_serv = 0;
                if (ex_grpC_flag == true)
                {
                    //Yr = Yscat;
                    //if(txt_len_serv.Text=="")
                    //{
                    //    length_ex_serv=0;
                    //}
                    //else
                    //{
                    if (SelectedSubCats.Contains("EX"))
                    {
                        DateTime ex_from_date = DateTime.ParseExact(txt_ex_f_date.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                        DateTime ex_to_date = DateTime.ParseExact(txt_ex_t_date.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                        TimeSpan t = ex_to_date - ex_from_date;
                        length_ex_serv = (int)t.TotalDays;
                        txt_len_serv.Text = length_ex_serv.ToString();
                    }
                    //}
                    DOB_from_date = DateTime.ParseExact(lbl_dob_f.Text, "dd/MM/yyyy", new CultureInfo("en-US")).AddYears(0 - maxage).AddDays(0 - length_ex_serv);

                }
                else
                {
                    //dated 14/08/2023
                    DateTime DOBTo_date = DateTime.ParseExact(lbl_dob_t.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                    DOB_from_date = DateTime.ParseExact(lbl_dob_f.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                    DateTime DOBdate = DateTime.ParseExact(txt_DOB.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                    DOB_from_date = DateTime.ParseExact(lbl_dob_f.Text, "dd/MM/yyyy", new CultureInfo("en-US")).AddYears(0 - maxage);

                }

                //27 jan 2023 age relaxation
                double age = 0;
                //age = DateTime.Now.AddYears(-DOBdate.Year).Year;
                age = years;// Dated: 28-02-2023
                if (maxage <= age)
                {
                    if (age == maxage)
                    {
                        if (months > 0 || days > 0)
                        {
                            msg.Show("You are not Eligible for this post");
                            return;
                        }
                    }
                    else
                    {
                        msg.Show("You are not Eligible for this post");
                        return;
                    }
                }
                DateTime DOB_to_date = DateTime.ParseExact(lbl_dob_t.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                DateTime DOB_date = DateTime.ParseExact(txt_DOB.Text, "dd/MM/yyyy", new CultureInfo("en-US"));

                if (RadioButtonList_d.SelectedValue == "Y")
                {
                    DateTime Debar_date = DateTime.ParseExact(txt_d_date.Text, "dd/MM/yyyy", new CultureInfo("en-US")).AddYears(Int32.Parse("0"));
                    //In all cases(fresh or reopen) of advertisement Debar date will check with original EndsOn date----
                    DateTime EndsOn_date = DateTime.ParseExact(endson_org, "dd/MM/yyyy", new CultureInfo("en-US"));

                    check_debar_flag = check_debar(Debar_date, EndsOn_date);
                }
                if (txtbox_noncreamylayerDATE.Text != "")
                {
                    DateTime issue_date = DateTime.ParseExact(txtbox_noncreamylayerDATE.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                    DateTime doday = DateTime.Now;
                    ////DateTime EndsOn_date = DateTime.ParseExact(endson, "dd/MM/yyyy", new CultureInfo("en-US"));

                    //In case of reopen for advt. No 02/2012 nonCreamyLayer Certificate date will check with original Endson Date
                    if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
                    {
                        doday = DateTime.ParseExact(endson_org, "dd/MM/yyyy", new CultureInfo("en-US"));
                    }

                    //check_issue_date_flag = check_debar(issue_date, doday);
                    check_issue_date_flag = issue_date <= DateTime.ParseExact(endson_org, "dd/MM/yyyy", new CultureInfo("en-US"));//rohitxd
                }
                string ph = "";
                bool ph_flag = true;
                bool ph_scrib = true;
                //for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
                //{
                //    if (CheckBoxList_Subcategory.Items[i].Selected)
                //    {
                //        if (CheckBoxList_Subcategory.Items[i].Value == "PH")
                //        {
                //            ph_flag = false;

                //            ph_scrib = false;
                //        }
                //    }
                //}

                if (SelectedSubCats.Contains("PH"))
                {
                    ph_flag = false;

                    ph_scrib = false;
                }

                for (int i = 0; i < CheckboxList_PHSubCat.Items.Count; i++)
                {
                    if (CheckboxList_PHSubCat.Items[i].Selected)
                    {
                        if (CheckboxList_PHSubCat.Items[i].Value != null)
                        {
                            ph = ph + CheckboxList_PHSubCat.Items[i].Value.ToString() + ",";
                            ph_flag = true;
                            if (CheckboxList_PHSubCat.Items[i].Value == "1" || CheckboxList_PHSubCat.Items[i].Value == "3")
                            {
                                ph_scrib = true;
                            }
                        }
                    }
                }
                if (ph != "")
                {
                    ph = ph.Substring(0, ph.Length - 1);
                }

                if (ph_flag == true)
                {
                    if (ph_scrib_chk(ph_flag, ph_scrib))
                    {
                        if (check_debar_PH(ph) && check_debar_sub_ph_all())
                        {
                            if (check_debar_flag)
                            {
                                if (check_issue_date_flag)
                                {
                                    if (check_age(DOB_date, DOB_from_date, DOB_to_date, txt_cont.Text) || agevalidationexmpt == "Y")
                                    {
                                        int max_app_no = objCandD.get_max_app_no() + 1;
                                        dt = objCandD.Search_JobApplication_PD(jid, regno);
                                        if (dt.Rows.Count == 0)
                                        {
                                            string ex = "";
                                            for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
                                            {
                                                if (CheckBoxList_Subcategory.Items[i].Value == "EX" && CheckBoxList_Subcategory.Items[i].Selected == true)
                                                {
                                                    ex = "Y";
                                                }
                                                else
                                                {
                                                    ex = "N";
                                                }
                                            }
                                            //
                                            if (SelectedSubCats.Contains("EX"))
                                            {
                                                ex = "Y";
                                            }
                                            //txtIssuingauthority.Text.Trim() == "" && txtIssuedate.Text.Trim() == "" && txtCertificateNo.Text.Trim()
                                            try
                                            {
                                                //txtIssuingauthority.Text.Trim() == "" && txtIssuedate.Text.Trim() == "" && txtCertificateNo.Text.Trim()
                                                applid = objCandD.Insert_JobapplicationTransaction(Int32.Parse(jid), max_app_no, Utility.putstring(txt_name.Text), Utility.putstring(txt_fh_name.Text), Utility.putstring(txt_mothername.Text), Utility.putstring(txt_pre_add.Text), Utility.putstring(txt_par_add.Text), Utility.putstring(txt_pre_pin.Text), Utility.putstring(txt_per_pin.Text), Utility.putstring(txt_mob.Text), Utility.putstring(txt_email.Text), DDL_Nationality.SelectedItem.Text, RadioButtonList_mf.SelectedValue, RadioButtonList_m_status.SelectedValue, DOB, DropDownList_cat.SelectedItem.Text, SelectedSubCats, ph, GovtDateJoin, Utility.putstring(txtbox_noncreamylayer.Text), NoncreamylayerDATE, CertIssueState, ex, fromdate, todate, RadioButtonList_d.SelectedValue.ToString(), DebarredDate, Utility.putstring(txt_d_year.Text), ip, regno, feereq, CertUssueAuth, txt_cont.Text, txt_len_serv.Text, ph_visual, ph_hearing, ph_ortho, physic, RadioButtonList_scrb_accept.SelectedValue, rbtobcregion.SelectedValue, Utility.putstring(txtspouse.Text), txtIssuingauthority.Text.Trim(), txtCertificateNo.Text.Trim(), txtIssuedate.Text.Trim(), PH_docSize, 0);
                                            }
                                            catch (Exception error)
                                            {
                                                LogException(error);
                                            }
                                            //applid = objCandD.Insert_JobapplicationTransaction(Int32.Parse(jid), max_app_no, Utility.putstring(txt_name.Text), Utility.putstring(txt_fh_name.Text), Utility.putstring(txt_mothername.Text), Utility.putstring(txt_pre_add.Text), Utility.putstring(txt_par_add.Text), Utility.putstring(txt_pre_pin.Text), Utility.putstring(txt_per_pin.Text), Utility.putstring(txt_mob.Text), Utility.putstring(txt_email.Text), DDL_Nationality.SelectedItem.Text, RadioButtonList_mf.SelectedValue, RadioButtonList_m_status.SelectedValue, DOB, DropDownList_cat.SelectedItem.Text, sub_cat, ph, GovtDateJoin, Utility.putstring(txtbox_noncreamylayer.Text), NoncreamylayerDATE, CertIssueState, ex, fromdate, todate, RadioButtonList_d.SelectedValue.ToString(), DebarredDate, Utility.putstring(txt_d_year.Text), ip, regno, feereq, CertUssueAuth, txt_cont.Text, txt_len_serv.Text, ph_visual, ph_hearing, ph_ortho, physic, RadioButtonList_scrb_accept.SelectedValue, rbtobcregion.SelectedValue, Utility.putstring(txtspouse.Text));
                                            if (applid > 0)
                                            {
                                                if (DropDownList_cat.SelectedValue == "OBC" && DropDownList_c_state.SelectedValue != "7" && rbtobcregion.SelectedValue == "D")
                                                {
                                                    string IP = GetIPAddress();
                                                    int temp = objCandD.insertOBCfathercertdetails(applid, txtbox_noncreamylayer_f.Text, txtbox_noncreamylayerDATE_f.Text, txt_cert_issue_auth_f.Text, "7", IP, ddlOBCForM.SelectedValue);
                                                }
                                                string url = "";
                                                //url = md5util.CreateTamperProofURL("ApplyInsert.aspx", null, "postcode=" + MD5Util.Encrypt(lbl_post_code.Text, true) + "&applid=" + MD5Util.Encrypt(applid.ToString(), true));
                                                //url = md5util.CreateTamperProofURL("Experience.aspx", null, "applid=" + MD5Util.Encrypt(applid.ToString(), true));

                                                url = md5util.CreateTamperProofURL("jobupload.aspx", null, "applid=" + MD5Util.Encrypt(applid.ToString(), true));
                                                Response.Redirect(url);

                                            }
                                            else
                                            {
                                                msg.Show("Invalid Submission");
                                            }
                                        }
                                        else
                                        {
                                            msg.Show("You have already applied for this post");
                                        }
                                    }
                                    else
                                    {
                                        msg.Show("According to Age Criteria of the Post, you are not eligible to apply");
                                    }
                                }
                                else
                                {
                                    msg.Show("Certificate Issue date should be earlier than or equal to Advt. End Date.");
                                }
                            }
                            else
                            {
                                msg.Show("Yor are debared by the Board.");
                            }
                        }
                    }
                    else
                    {
                        msg.Show("Please select choice for scribe facility.");
                    }
                }
                else
                {
                    msg.Show("Please select your PH subcategory.");
                }
            }


        }
        else
        {
            msg.Show("Please Agree with the Declaration.");
        }
        // }

    }

    private bool ph_scrib_chk(bool ph_flag, bool ph_scrib)
    {
        if (Session["intraflag"] != null)
        {
            return true;
        }
        if (RadioButtonList_scrb_accept.Visible == false)
        {
            return true;
        }
        else if (RadioButtonList_scrb_accept.Visible == true && RadioButtonList_scrb_accept.SelectedValue != "")
        {
            return true;
        }
        else
        {
            return false;
        }

        //if (ph_flag == true && ph_scrib == true && RadioButtonList_scrb_accept.Visible==false)
        //{
        //    return true;
        //}
        //else if (ph_flag == true && ph_scrib == true && RadioButtonList_scrb_accept.Visible==true && RadioButtonList_scrb_accept.SelectedValue == "")
        //{
        //    return false;
        //}
        //else if (ph_flag == true && ph_scrib == false)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

        //if (ph_flag == true && ph_scrib == true && (RadioButtonList_scrb_accept.SelectedValue != null && RadioButtonList_scrb_accept.SelectedValue != ""))
        //{
        //    return true;
        //}
        //else if (ph_flag == true && ph_scrib == true)
        //{
        //    return true;
        //}
        //else if (ph_flag == true && ph_scrib == false)
        //{
        //    return true;
        //}        
        //else
        //{
        //    return false;
        //}
    }

    private bool check_reopen_eligibility(string cat, string subcat, string subsubcat)
    {
        bool flag = true;
        string cat_flag = "";
        string subcat_flag = "";
        string subsubcat_flag = "";
        string subsub_subcat_flag = "";
        if (jid == "")
        {
            jid = Session["jid"].ToString();
            //Session.Remove("jid");
        }

        DataTable dt_reopen = objCandD.get_reopen_data(jid);
        if (dt_reopen.Rows.Count > 0)
        {
            cat_flag = dt_reopen.Rows[0]["category"].ToString();
            subcat_flag = dt_reopen.Rows[0]["subcat"].ToString();
            subsubcat_flag = dt_reopen.Rows[0]["subsubcat"].ToString();
            subsub_subcat_flag = dt_reopen.Rows[0]["subsub_subcat"].ToString();
        }


        if (dt_reopen.Rows.Count == 0)
        {
            return true;
        }
        else
        {
            if (cat == "-1" && cat_flag == "Y")
                flag = false;
            if (subcat == "" && subcat_flag == "Y")
                flag = false;
            if (subsubcat == "" && subsubcat_flag == "Y")
                flag = false;
            if (subsubcat == "" && subsubcat_flag == "Y" && subcat.ToUpper() == "EX")
                flag = true;
        }
        return flag;
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
        if (maxage == 0)
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

            if (years <= maxage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private bool check_debar(DateTime debar_date, DateTime endson)
    {
        if (DateTime.Compare(debar_date, endson) < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void populatePH()
    {
        try
        {
            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();
            DataTable ddt = objCandD.Getdeptcode(reqid);
            string deptcode = ddt.Rows[0]["deptcode"].ToString();
            if (deptcode == "COMBD")//21/09/2023 combd
            {
                dt = objCandD.SelectPH_combd(Int32.Parse(reqid));
            }
            else
            {
                dt = objCandD.SelectPH(Int32.Parse(reqid));
            }
            //dt = objCandD.SelectPH(Int32.Parse(reqid));
            CheckboxList_PHSubCat.Items.Clear();
            CheckboxList_PHSubCat.DataTextField = "PH_Cat_Desc";
            CheckboxList_PHSubCat.DataValueField = "PH_Code";
            CheckboxList_PHSubCat.DataSource = dt;
            CheckboxList_PHSubCat.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void pupulate_radio_ph_subcat()
    {
        try
        {
            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();
            DataTable ddt = objCandD.Getdeptcode(reqid);
            string deptcode = ddt.Rows[0]["deptcode"].ToString();
            if (deptcode == "COMBD")//21/09/2023
            {
                dt = objCandD.get_sub_ph_combd("Visual (VH)", Int32.Parse(reqid));
            }
            else
            {
                dt = objCandD.get_sub_ph("Visual (VH)", Int32.Parse(reqid));
            }
            if (dt.Rows.Count > 0)
            {
                radio_vh.DataTextField = "PH_SubCatCodeDesc";
                radio_vh.DataValueField = "phsubcatid";
                radio_vh.DataSource = dt;
                radio_vh.DataBind();
                //tr_vh.Visible = true;
            }
            else
            {
                //tr_vh.Visible = false;
            }

            if (deptcode == "COMBD")
            {
                dt = objCandD.get_sub_ph_combd("Hearing (HH)", Int32.Parse(reqid));
            }
            else
            {
                dt = objCandD.get_sub_ph("Hearing (HH)", Int32.Parse(reqid));
            }
            radio_hh.DataTextField = "PH_SubCatCodeDesc";
            radio_hh.DataValueField = "phsubcatid";
            radio_hh.DataSource = dt;
            radio_hh.DataBind();

            if (deptcode == "COMBD")
            {
                dt = objCandD.get_sub_ph_combd("Ortho (OH)", Int32.Parse(reqid));
            }
            else
            {
                dt = objCandD.get_sub_ph("Ortho (OH)", Int32.Parse(reqid));
            }
            radio_oh.DataTextField = "PH_SubCatCodeDesc";
            radio_oh.DataValueField = "phsubcatid";
            radio_oh.DataSource = dt;
            radio_oh.DataBind();
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    private void populateState()
    {
        try
        {
            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();
            dt = objCandD.SelectState();

            FillDropDown(DropDownList_c_state, dt, "state", "code");
            DropDownList_c_state.SelectedValue = "7";
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void DropDownList_cat_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtbox_noncreamylayer.Text = "";// Dated: 28-02-2023
        txtbox_noncreamylayerDATE.Text = "";
        txt_cert_issue_auth.Text = "";

        if (Request.QueryString["update"] != null)
        {
            if (MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "1" || MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "P")
            {
                String Temp_applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                applid = int.Parse(Temp_applid);

            }
        }

        if (lbl_post_code.Text == "28/23" && DropDownList_cat.SelectedItem.Text != "EWS")//03-03-2023
        {
            msg.Show("This vacancy is only for EWS category");
            return;
        }

        if (lbl_post_code.Text == "62/23" && (DropDownList_cat.SelectedItem.Text == "OBC" || DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "EWS"))//14-08-2023
        {
            msg.Show("This vacancy is only for SC/ST category");
            return;
        }
        if (applid > 0)//01-03-2023
        {
            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();
            dt = objCandD.CategoryDetails(applid);
            if (dt.Rows.Count > 0)
            {
                string JScatID = dt.Rows[0]["JScatID"].ToString();
                DataTable dt1 = new DataTable();
                dt1 = objCandD.subCategoryDetails(JScatID);
                if (dt1.Rows.Count > 0)
                {
                    string JSScatid = dt1.Rows[0]["JSScatid"].ToString();
                    DataTable dt2 = new DataTable();
                    dt2 = objCandD.subsubCategoryDetails(JSScatid);
                    if (dt2.Rows.Count > 0)
                    {
                        //objCandD.delete_subsubCategory(JSScatid);
                        //objCandD.delete_subCategory(JScatID);
                        //objCandD.delete_Category(applid);
                    }
                }
            }
            //objCandD.Updatecat(applid,DropDownList_cat.SelectedItem.Text);
        }

        if (DropDownList_cat.SelectedItem.Text == "OBC" || DropDownList_cat.SelectedItem.Text == "SC" || DropDownList_cat.SelectedItem.Text == "ST" || DropDownList_cat.SelectedItem.Text == "EWS")
        {
            tr_obc_cert.Visible = true;
            tr_obc_state.Visible = true;

            if (DropDownList_cat.SelectedItem.Text == "OBC")
            {
                lbl_issuing_no.Text = " Certificate No.";
                lbl_issuing_date.Text = "Issuing Date (On or Before Cut-off date)";
                lbl_issuing_state.Text = "Certificate Issuing State";
                rbtobcregion.Visible = true;
            }
            else if (DropDownList_cat.SelectedItem.Text == "SC")
            {
                lbl_issuing_no.Text = " Certificate No.";
                lbl_issuing_date.Text = "Issuing Date (On or Before Cut-off date)";
                lbl_issuing_state.Text = "Certificate Issuing State";
                rbtobcregion.Visible = false;
            }
            else if (DropDownList_cat.SelectedItem.Text == "ST")
            {
                lbl_issuing_no.Text = "Certificate No.";
                lbl_issuing_date.Text = "Issuing Date (On or Before Cut-off date)";
                lbl_issuing_state.Text = "Certificate Issuing State";
                rbtobcregion.Visible = false;
            }
            else if (DropDownList_cat.SelectedItem.Text == "EWS")
            {
                lbl_issuing_no.Text = " Certificate No.";
                lbl_issuing_date.Text = "Issuing Date (On or Before Cut-off date)";
                lbl_issuing_state.Text = "Certificate Issuing State";
                rbtobcregion.Visible = false;
            }

        }
        else
        {
            tr_obc_cert.Visible = false;
            tr_obc_state.Visible = false;
            rbtobcregion.Visible = false;
        }

        if (DropDownList_cat.SelectedItem.Text == "SC" || DropDownList_cat.SelectedItem.Text == "ST")
        {
            if (Session["SCST_flag"] == "Y")
            {
                chk_relax.Checked = true;
                chk_relax.Text = "I am eligible for relaxation in the Physical Standards.(SC / ST)";
            }
            else
            {
                chk_relax.Checked = false;
                chk_relax.Text = "I am eligible for relaxation in the Physical Standards.(For Hilly Area)";
            }
        }
        else
        {
            chk_relax.Checked = false;
            chk_relax.Text = "I am eligible for relaxation in the Physical Standards.(For Hilly Area)";
        }
        if (DropDownList_cat.SelectedValue == "OBC" && DropDownList_c_state.SelectedValue != "7" && rbtobcregion.SelectedValue == "D")
        {
            trobcf.Visible = true;
            tr_obc_cert_f.Visible = true;
            tr_obc_state_f.Visible = true;
            trobcform.Visible = true;
        }
        else
        {
            if (rbtobcregion.SelectedValue == "O")
            {
                msg.Show("OBC Outside Delhi is not eligible category for reservation");
            }
            trobcf.Visible = false;
            tr_obc_cert_f.Visible = false;
            tr_obc_state_f.Visible = false;
            trobcform.Visible = false;
        }
    }
    protected void RadioButtonList_d_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (RadioButtonList_d.SelectedValue == "Y")
        {
            TDDebbaredDateorder.Visible = true;
            TDDebbaredYear.Visible = false;
            txt_d_date.Text = "";

        }
        else
        {
            TDDebbaredDateorder.Visible = false;
            TDDebbaredYear.Visible = false;
            txt_d_date.Text = "";
        }
    }
    private void fill_candidate_form(int applid)
    {
        DataTable dtFill = objCandD.GetfillAppicantDetails(applid);
        jid = dtFill.Rows[0]["jid"].ToString();
        reqid = dtFill.Rows[0]["reqid"].ToString();

        Session.Remove("jid");
        Session.Remove("reqid");

        Session["jid"] = jid;
        Session["reqid"] = reqid;
        //jobDesc = Session["jobDesc"].ToString();
        hidden_jid.Value = reqid;
        hfjid.Value = jid;
        if (dtFill.Rows.Count > 0)
        {
            populateCasteCategory();
            populatePH();
            populate_subcatCheckbox();
            populateState();
            pupulate_radio_ph_subcat();

            Session.Remove("jobDesc");
            //Session["jobDesc"] = dtFill.Rows[0]["JobDescription"].ToString();
            //jobDesc = Session["jobDesc"].ToString();
            lbl_advt.Text = dtFill.Rows[0]["adno"].ToString();
            lbl_dob_f.Text = dtFill.Rows[0]["DOBFrom"].ToString();
            lbl_dob_t.Text = dtFill.Rows[0]["DOBTO"].ToString();
            lbl_app.Text = Utility.getstring(dtFill.Rows[0]["JobTitle"].ToString());
            lbl_post_code.Text = dtFill.Rows[0]["postcode"].ToString();
            txt_name.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["name"].ToString()));
            txt_fh_name.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["fname"].ToString()));
            txt_mothername.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["mothername"].ToString()));
            txt_pre_add.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["address_per"].ToString()));
            txt_par_add.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["address"].ToString()));
            txt_pre_pin.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["PIN_per"].ToString()));
            txt_per_pin.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["PIN"].ToString()));
            txt_mob.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["mobileno"].ToString()));
            txt_email.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["email"].ToString()));
            DDL_Nationality.SelectedIndex = DDL_Nationality.Items.IndexOf(DDL_Nationality.Items.FindByText(dtFill.Rows[0]["nationality"].ToString()));
            RadioButtonList_mf.SelectedValue = dtFill.Rows[0]["gender"].ToString();
            RadioButtonList_m_status.SelectedValue = dtFill.Rows[0]["maritalstatus"].ToString();
            txt_DOB.Text = Utility.formatDateinDMY(dtFill.Rows[0]["birthdt"].ToString());
            DropDownList_cat.SelectedIndex = DropDownList_cat.Items.IndexOf(DropDownList_cat.Items.FindByText(dtFill.Rows[0]["category"].ToString()));
            txt_endson.Text = dtFill.Rows[0]["endson"].ToString();
            txt_cert_issue_auth.Text = dtFill.Rows[0]["CastCertIssueAuth"].ToString();
            //    string[] s_cat = dtFill.Rows[0]["SubCategory"].ToString().Split(',');
            txt_cont.Text = dtFill.Rows[0]["ContractDuration"].ToString();
            txt_len_serv.Text = dtFill.Rows[0]["ExServiceDuration"].ToString();
            rbtobcregion.SelectedValue = dtFill.Rows[0]["OBCRegion"].ToString();
            txtspouse.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["spousename"].ToString()));

            txtCertificateNo.Text = dtFill.Rows[0]["PhCertiNo"].ToString();
            txtIssuingauthority.Text = dtFill.Rows[0]["PhCertIssueAuth"].ToString();
            txtIssuedate.Text = Utility.formatDateinDMY(dtFill.Rows[0]["PhCertIssueDate"].ToString());

            dob = Session["birthdt"].ToString();

            overrideTextValue(); // => experimental

            DateTime dt = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime enddate = DateTime.ParseExact(dtFill.Rows[0]["endson"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (lbl_post_code.Text == "90/09")//09-05-2023
            {
                enddate = DateTime.ParseExact("15/01/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                enddate = DateTime.ParseExact(dtFill.Rows[0]["endson"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            //DateTime today = DateTime.Now;
            TimeSpan ts = enddate - dt;
            DateTime age = DateTime.MinValue + ts;
            years = age.Year - 1;// Dated: 28-02-2023
            months = age.Month - 1;
            days = age.Day - 1;
            string age_diff = years.ToString() + " Year/s " + months.ToString() + " Month/s " + days.ToString() + " Day/s";

            lblCandidateAge.Text = Convert.ToString(years) + " Years";
            //lblEndDate.Text = dtFill.Rows[0]["endson"].ToString() + " (Last Date of Advertisement) :";

            if (lbl_post_code.Text == "90/09")//04-05-2023
            {
                lblEndDate.Text = "15/01/2010 (Last Date of Advertisement) :";
            }
            else
            {
                lblEndDate.Text = dtFill.Rows[0]["endson"].ToString() + " (Last Date of Advertisement) :";
            }

            btnViewPHDoc.Enabled = true;
            //byte[] phcertificae =Convert.ToBase64CharArray( dtFill.Rows[0]["PhCertificateFile"].ToString());
            if (RadioButtonList_m_status.SelectedValue == "M")
            {

                spn.Visible = true;
                lblspname.Visible = true;
                txtspouse.Visible = true;

            }
            else
            {
                spn.Visible = false;
                lblspname.Visible = false;
                txtspouse.Visible = false;

            }
            if (dtFill.Rows[0]["wscribe"].ToString() != "")
            {
                RadioButtonList_scrb_accept.SelectedValue = dtFill.Rows[0]["wscribe"].ToString();
            }
            DataRow[] s_cat = dtFill.Select("SubCat_code is not null");
            DataRow[] ss_cat = dtFill.Select("SubCat_code='PH'");
            DataRow[] sss_cat_VH = dtFill.Select("SScatid=1");
            DataRow[] sss_cat_HH = dtFill.Select("SScatid=2");
            DataRow[] sss_cat_OH = dtFill.Select("SScatid=3");
            for (int i = 0; i < s_cat.Length; i++)
            {
                for (int j = 0; j < CheckBoxList_Subcategory.Items.Count; j++)
                {
                    if (s_cat[i]["SubCat_code"].ToString() == CheckBoxList_Subcategory.Items[j].Value)
                    {
                        CheckBoxList_Subcategory.Items[j].Selected = true;
                    }
                }
            }

            CheckBoxList_Subcategory_SelectedIndexChanged(this, new EventArgs());
            DropDownList_cat_SelectedIndexChanged(this, new EventArgs());


            //  string[] ph = dtFill.Rows[0]["ph"].ToString().Split(',');
            for (int i = 0; i < ss_cat.Length; i++)
            {
                for (int j = 0; j < CheckboxList_PHSubCat.Items.Count; j++)
                {
                    if (ss_cat[i]["SScatid"].ToString() == CheckboxList_PHSubCat.Items[j].Value)
                    {
                        CheckboxList_PHSubCat.Items[j].Selected = true;
                    }
                }
            }

            CheckboxList_PHSubCat_SelectedIndexChanged(this, new EventArgs());

            txt_dob_dgs.Text = Utility.formatDateinDMY(dtFill.Rows[0]["GovtJoinDt"].ToString());
            txtbox_noncreamylayer.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["CLCNo"].ToString()));

            txtbox_noncreamylayerDATE.Text = Utility.formatDateinDMY(dtFill.Rows[0]["CLCDate"].ToString());
            DropDownList_c_state.SelectedValue = dtFill.Rows[0]["CastCerApplyState"].ToString();


            txt_ex_f_date.Text = Utility.formatDateinDMY(dtFill.Rows[0]["ExFromDt"].ToString());
            txt_ex_t_date.Text = Utility.formatDateinDMY(dtFill.Rows[0]["ExToDt"].ToString());
            RadioButtonList_d.SelectedValue = dtFill.Rows[0]["DebardDetails"].ToString();
            if (RadioButtonList_d.SelectedValue == "Y")
            {
                TDDebbaredDateorder.Visible = true;
                TDDebbaredYear.Visible = false;

            }
            else
            {
                TDDebbaredDateorder.Visible = false;
                TDDebbaredYear.Visible = false;

            }
            txt_d_date.Text = Utility.formatDateinDMY(dtFill.Rows[0]["DebardDt"].ToString());
            txt_d_year.Text = Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["DebardYr"].ToString()));

            if (sss_cat_VH.Length > 0)
            {
                // radio_vh.SelectedIndex = radio_vh.Items.IndexOf(radio_vh.Items.FindByValue(dtFill.Rows[0]["PH_Visual"].ToString().Trim()));
                radio_vh.SelectedIndex = radio_vh.Items.IndexOf(radio_vh.Items.FindByValue(sss_cat_VH[0]["SSScatid"].ToString().Trim()));
            }
            if (sss_cat_HH.Length > 0)
            {
                //radio_hh.SelectedIndex = radio_hh.Items.IndexOf(radio_hh.Items.FindByValue(dtFill.Rows[0]["PH_Hearing"].ToString().Trim()));
                radio_hh.SelectedIndex = radio_hh.Items.IndexOf(radio_hh.Items.FindByValue(sss_cat_HH[0]["SSScatid"].ToString().Trim()));
            }
            if (sss_cat_OH.Length > 0)
            {
                //radio_oh.SelectedIndex = radio_oh.Items.IndexOf(radio_oh.Items.FindByValue(dtFill.Rows[0]["PH_Ortho"].ToString().Trim()));
                radio_oh.SelectedIndex = radio_oh.Items.IndexOf(radio_oh.Items.FindByValue(sss_cat_OH[0]["SSScatid"].ToString().Trim()));
            }

            string relax = dtFill.Rows[0]["physical_relax"].ToString();
            if (relax != "")
            {
                if (relax == "Y")
                {
                    chk_relax.Checked = true;
                }
                else
                {
                    chk_relax.Checked = false;
                }
            }
            if (DropDownList_cat.SelectedValue == "OBC" && DropDownList_c_state.SelectedValue != "7" && rbtobcregion.SelectedValue == "D")
            {
                trobcf.Visible = true;
                tr_obc_cert_f.Visible = true;
                tr_obc_state_f.Visible = true;
                trobcform.Visible = true;
                DataTable dtobcf = objCandD.GetfatherOBCDetails(applid);
                if (dtobcf.Rows.Count > 0)
                {
                    ddlOBCForM.SelectedValue = dtobcf.Rows[0]["certfatherormother"].ToString();
                    // txt_c_state_f.Text = dtobcf.Rows[0]["certissuestate_father"].ToString();
                    txt_cert_issue_auth_f.Text = dtobcf.Rows[0]["certissuedesig_father"].ToString();
                    txtbox_noncreamylayer_f.Text = dtobcf.Rows[0]["certificateno_father"].ToString();
                    txtbox_noncreamylayerDATE_f.Text = dtobcf.Rows[0]["certissuedate_father"].ToString();
                }
            }
            else
            {
                //if (rbtobcregion.SelectedValue == "O")
                //{
                //    msg.Show("OBC Outside Delhi is not eligible category for reservation");
                //}
                trobcf.Visible = false;
                tr_obc_cert_f.Visible = false;
                tr_obc_state_f.Visible = false;
                trobcform.Visible = false;
            }
        }

    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (lbl_post_code.Text == "28/23" && DropDownList_cat.SelectedItem.Text != "EWS")//03-03-2023
        {
            msg.Show("This vacancy is only for EWS category");
            return;
        }
        if (lbl_post_code.Text == "62/23" && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "OBC" || DropDownList_cat.SelectedItem.Text == "EWS"))//14-08-2023
        {
            msg.Show("This vacancy is only for SC/ST category");
            return;
        }
        if (lbl_post_code.Text == "104/23" && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "OBC"))//14-08-2023
        {
            msg.Show("This vacancy is not for UR and OBC Candidates");
            return;
        }
        if (lbl_post_code.Text == "03/24" && (DropDownList_cat.SelectedItem.Text == "ST"))//01-02-2024
        {
            msg.Show("This vacancy is not for ST Candidates");
            return;
        }
        if (lbl_post_code.Text == "07/24" && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "EWS" || DropDownList_cat.SelectedItem.Text == "ST"))//09-02-2024
        {
            msg.Show("This vacancy is only reserved for OBC and SC Candidates ");
            return;
        }
        if ((lbl_post_code.Text == "18/24" || lbl_post_code.Text == "51/24") && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "EWS" || DropDownList_cat.SelectedItem.Text == "ST" || DropDownList_cat.SelectedItem.Text == "SC" || (DropDownList_cat.SelectedItem.Text == "OBC" && rbtobcregion.SelectedValue == "O")))//18-03-2023
        {
            msg.Show("This vacancy is only reserved for OBC Candidates");
            return;
        }
        if (lbl_post_code.Text == "46/24" && (DropDownList_cat.SelectedItem.Text == "UR" || DropDownList_cat.SelectedItem.Text == "EWS" || (DropDownList_cat.SelectedItem.Text == "OBC" && rbtobcregion.SelectedValue == "O")))//18-03-2023
        {
            msg.Show("This vacancy is not reserved for UR and EWS Candidates");
            return;
        }
        if (lbl_post_code.Text == "76/23" || lbl_post_code.Text == "77/23")//13th march2024
        {
            bool isPHSelected = false;
            for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
            {
                var x = CheckBoxList_Subcategory.Items[i];

                if (x.Selected && x.Value == "PH")
                {
                    isPHSelected = true;
                    break;
                }
            }
            if (!isPHSelected)
            {
                msg.Show("This vacancy is only reserved for PWD Candidates");
                return;
            }
        }
        bool ex_grpC_flag = false;
        string ip = GetIPAddress();
        string ph_visual = "";
        string ph_hearing = "";
        string ph_ortho = "";
        byte[] PH_docSize = null;
        string physic = "";
        //bool physic_accept;
        //jobDesc = Session["jobDesc"].ToString();
        //  reqid = Session["reqid"].ToString();
        // DataTable reqReopenPost = objCandD.CheckReopenPostOnUpdateApplication(reqid, CheckBoxList_Subcategory.SelectedValue);
        //if (reqReopenPost.Rows.Count <= 0)
        // {
        //   msg.Show("This Post reopened for special criteria candidtes.Kindly check eligibility criteria.");
        // }
        // else
        // {
        if (Session["physic"] != null && Session["physic"].ToString() == "Y")
        {
            if (RadioButtonList_physcic_accept.SelectedValue == "Y")
            {
                //physic_accept = true;
                physic = "Y";
            }
            else if (RadioButtonList_physcic_accept.SelectedValue == "N")
            {
                //physic_accept = false;
                physic = "N";
            }
            else
            {
                //physic_accept =;
                physic = "";
            }
        }
        if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
        {
            DataTable dtchkageexmpt = objCandD.CheckWhetherqualiexmpted(hfjid.Value);
            if (dtchkageexmpt.Rows.Count > 0)
            {
                agevalidationexmpt = dtchkageexmpt.Rows[0]["agevalidationexmpt"].ToString();
            }
        }



        if (DropDownList_cat.SelectedItem.Text.Trim() == "Select")
        {
            msg.Show("Select category to proceed");
            return;
        }


        List<string> SelectedSubCats = new List<string>();

        for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
        {
            if (CheckBoxList_Subcategory.Items[i].Selected)
            {
                string scat = CheckBoxList_Subcategory.Items[i].Value;
                if (!string.IsNullOrEmpty(scat.Trim()))
                {
                    SelectedSubCats.Add(scat.Trim());
                }
            }
        }

        if (SelectedSubCats.Contains("PH"))//check box for Differentially Able
        {
            if (CheckboxList_PHSubCat.SelectedValue == "")
            {
                msg.Show("Select Category of Disability");
                return;
            }
            else if (CheckboxList_PHSubCat.SelectedValue == "1")
            {
                if (radio_vh.SelectedValue == "")
                {
                    msg.Show("Select Sub Category of VH Disability");
                    return;
                }

            }
            //bellow code is commented not required for Sub-SUb category
            else if (CheckboxList_PHSubCat.SelectedValue == "2")
            {
                if (radio_hh.SelectedValue == "")
                {
                    msg.Show("Select Sub Category of HH Disability");
                    return;
                }

            }
            else if (CheckboxList_PHSubCat.SelectedValue == "3")
            {
                if (radio_oh.SelectedValue == "")
                {
                    msg.Show("Select Sub Category of OH Disability");
                    return;
                }

            }
            else
            {

            }
        }
        // for PH certificate file Upload
        if (SelectedSubCats.Contains("PH"))
        {
            if (txtIssuingauthority.Text.Trim() == "" || txtIssuedate.Text.Trim() == "" || txtCertificateNo.Text.Trim() == "")
            {
                msg.Show("Enter PH Certificate Details");
                return;
            }
            //else if (PHCertUpload.PostedFile.ContentLength <= 0)
            //{
            //    msg.Show("Please upload disability Certificate");
            //    return;
            //}
            else
            {
                if (ViewState["PhDoc"] != null && ViewState["PhDoc"] != "")
                {
                    //PH_docSize = new byte[PHCertUpload.PostedFile.ContentLength];
                    //try
                    //{
                    //    if (PHCertUpload.PostedFile != null && PHCertUpload.PostedFile.FileName != "")
                    //    {
                    //        string filename = PHCertUpload.PostedFile.FileName.ToString();
                    //        string[] FileExtension = filename.Split('.');
                    //        string ext = System.IO.Path.GetExtension(PHCertUpload.PostedFile.FileName).ToLower();
                    //        if (ext != ".pdf" && ext != ".PDF")
                    //        {
                    //            msg.Show("only pdf Files are allowed");
                    //            return;
                    //        }
                    //        else
                    //        {
                    //            if (Convert.ToDouble(PH_docSize.Length) / (1024 * 1024) <= 2)
                    //            {
                    //                HttpPostedFile uploadedImage = PHCertUpload.PostedFile;
                    //                uploadedImage.InputStream.Read(PH_docSize, 0, (int)PHCertUpload.PostedFile.ContentLength);
                    //                bool checkfiletype = chkfiletype(PH_docSize, ext);
                    //                if (checkfiletype)
                    //                {
                    //                }
                    //                else
                    //                {
                    //                    msg.Show("Only pdf files can be uploaded");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                msg.Show("Select Maximum file size 1 MB");
                    //                return;
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (Exception ex) { }
                    //string PhViewstate = ViewState["PhDoc"].ToString();

                    PH_docSize = ViewState["PhDoc"] as byte[]; //System.Text.Encoding.ASCII.GetBytes(PhViewstate);
                }
                else
                {
                    msg.Show("Please upload disability Certificate");
                    return;
                }
            }
        }
        else { }
        if (SelectedSubCats.Contains("CESD"))
        {
            //if (ViewState["PhDoc"] == "") 03-03-2023
            //{
            //    msg.Show("Please upload the Certificate");
            //    return;
            //}
            //else
            //{
            //    if (ViewState["PhDoc"] != null && ViewState["PhDoc"] != "")
            //    {
            //        PH_docSize = ViewState["PhDoc"] as byte[];//System.Text.Encoding.ASCII.GetBytes(PhViewstate);
            //    }
            //    else
            //    {
            //        msg.Show("Please upload required Certificate");
            //        return;
            //    }


            //}
        }

        if (Validation.chkLevel17(txt_pre_add.Text) || txt_pre_add.Text.Length > 200)
        {
            msg.Show("Invalid Character in address or Address length is more than 200 Characters.");
        }
        else if (Validation.chkLevel17(txt_par_add.Text) || txt_par_add.Text.Length > 200)
        {
            msg.Show("Invalid Character in Permanent address or Address length is more than 200 Characters.");
        }
        else if ((DropDownList_cat.SelectedValue != "UR") && (txtbox_noncreamylayer.Text == "" || txtbox_noncreamylayerDATE.Text == "" || DropDownList_c_state.SelectedValue == "-1" || txt_cert_issue_auth.Text == ""))
        {
            msg.Show("Please fill the Certificate No,Certificate Issue Date and State");
        }
        else if (RadioButtonList_d.SelectedValue == "Y" && txt_d_date.Text == "")
        {
            msg.Show("Please fill Debare Date");
        }
        else if (Session["physic"] != null && Session["physic"].ToString() == "Y" && (physic == null || physic == ""))
        {
            msg.Show("Please check the physical acceptance.");
        }
        else if (!check_reopen_eligibility(DropDownList_cat.SelectedValue, CheckBoxList_Subcategory.SelectedValue, CheckboxList_PHSubCat.SelectedValue))
        {
            msg.Show("This Post reopened for special criteria candidates.Kindly check the eligibility criteria.");
        }
        else if (!check_ex_service_date(txt_ex_f_date.Text, txt_ex_t_date.Text, txt_endson.Text))
        {

        }
        else if (DropDownList_cat.SelectedValue == "OBC" && rbtobcregion.SelectedValue == "")
        {
            msg.Show("Please select OBC Delhi/Outside Delhi");
        }
        else if ((DropDownList_cat.SelectedValue == "OBC" && DropDownList_c_state.SelectedValue != "7" && rbtobcregion.SelectedValue == "D") && (ddlOBCForM.SelectedValue == "" || txtbox_noncreamylayer_f.Text == "" || txtbox_noncreamylayerDATE_f.Text == "" || txt_c_state_f.Text == "" || txt_cert_issue_auth_f.Text == ""))
        {
            msg.Show("Please fill Father OBC Certificate Details");
        }
        else if (RadioButtonList_m_status.SelectedValue == "M" && txtspouse.Text == "")
        {
            msg.Show("Please enter Spouse Name.");
        }
        else if (Validation.chkLevel20(txtspouse.Text) || txtspouse.Text.Length > 50)
        {
            msg.Show("Invalid Character in Spouse Name or Spouse Name length is more than 50 Characters.");

        }
        else if ((DropDownList_cat.SelectedValue == "EWS") && (txtbox_noncreamylayer.Text == "" || txtbox_noncreamylayerDATE.Text == "" || DropDownList_c_state.SelectedValue == "" && txt_cert_issue_auth.Text == ""))
        {
            msg.Show("Please fill  EWS Certificate Details");
        }
        else if (chk_decl.Checked)
        {
            int CertIssueState = 0;
            string GovtDateJoin;
            string NoncreamylayerDATE = txtbox_noncreamylayerDATE.Text;//Initialized by AnkitaSingh Dated: 16-02-2023
            if (NoncreamylayerDATE != "")
            {
                //Added by AnkitaSingh Dated: 16-02-2023
                DataTable d = new DataTable();
                d = objCandD.GetAdvtEndDate(lbl_post_code.Text.Trim());
                DateTime a = Convert.ToDateTime(d.Rows[0]["EndsOn"].ToString());
                DateTime t_obc = DateTime.ParseExact(NoncreamylayerDATE, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                if (t_obc <= a)
                {
                    NoncreamylayerDATE = txtbox_noncreamylayerDATE.Text;
                }
                else
                {
                    NoncreamylayerDATE = "";
                    txtbox_noncreamylayerDATE.Text = "";
                    msg.Show("Issuing date must be prior to or equal to the cut-off date of advertisement.");
                }
            }
            string DOB = txt_DOB.Text;
            string CertUssueAuth = txt_cert_issue_auth.Text;

            ph_visual = radio_vh.SelectedValue;
            ph_hearing = radio_hh.SelectedValue;
            ph_ortho = radio_oh.SelectedValue;

            if (DropDownList_c_state.SelectedValue != "")
            {
                CertIssueState = Convert.ToInt32(DropDownList_c_state.SelectedValue);
            }
            string fromdate = txt_ex_f_date.Text;
            string todate = txt_ex_t_date.Text;
            string DebarredDate = txt_d_date.Text;

            if (txt_dob_dgs.Text != "")
            {
                GovtDateJoin = txt_dob_dgs.Text;
            }
            else
            {
                GovtDateJoin = txt_dob_dgs.Text;
            }

            if (txtbox_noncreamylayerDATE.Text != "")
            {
                NoncreamylayerDATE = txtbox_noncreamylayerDATE.Text;
            }
            else
            {
                NoncreamylayerDATE = txtbox_noncreamylayerDATE.Text;
            }

            string ph = "";
            bool ph_flag = true;
            bool ph_scrib = true;
            if (RadioButtonList_d.SelectedValue == "Y")
            {
                if (txt_d_date.Text != "")
                {
                    //DateTime Debar_date = DateTime.ParseExact(txt_d_date.Text, "dd/MM/yyyy", new CultureInfo("en-US")).AddYears(Int32.Parse(txt_d_year.Text));
                    DateTime Debar_date = DateTime.ParseExact(txt_d_date.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                    DateTime EndsOn_date = DateTime.ParseExact(txt_endson.Text, "dd/MM/yyyy", new CultureInfo("en-US"));

                    check_debar_flag = check_debar(Debar_date, EndsOn_date);
                }
                else
                {
                    msg.Show("Please fill Debare date");
                }
            }

            //if (CheckBoxList_Subcategory.SelectedValue == "PH")
            //{
            //    ph_flag = false;
            //}
            //for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
            //{
            //    if (CheckBoxList_Subcategory.Items[i].Selected)
            //    {
            //        if (CheckBoxList_Subcategory.Items[i].Value == "PH")
            //        {
            //           // ph_flag = false;
            //            ph_scrib = false;
            //        }
            //    }
            //}

            if (SelectedSubCats.Contains("PH"))
            {
                ph_scrib = false;
            }

            for (int i = 0; i < CheckboxList_PHSubCat.Items.Count; i++)
            {
                if (CheckboxList_PHSubCat.Items[i].Selected)
                {
                    if (CheckboxList_PHSubCat.Items[i].Value != null)
                    {
                        ph = ph + CheckboxList_PHSubCat.Items[i].Value.ToString() + ",";
                        ph_flag = true;
                        if (CheckboxList_PHSubCat.Items[i].Value == "1" || CheckboxList_PHSubCat.Items[i].Value == "3")
                        {
                            ph_scrib = true;
                        }
                    }
                }
            }
            if (ph != "")
            {
                ph = ph.Substring(0, ph.Length - 1);
            }

            dt = objCandD.Getdeptcode(reqid);
            string deptcode = dt.Rows[0]["deptcode"].ToString();
            if (deptcode == "COMBD")
            {
                dt_age_relax = objCandD.age_relax_combd(Int32.Parse(reqid));
            }
            else
            {
                dt_age_relax = objCandD.age_relax(Int32.Parse(hidden_jid.Value));
            }

            YR_Age(DropDownList_cat.SelectedValue, "C");
            foreach (var subcat in SelectedSubCats)
            {
                YR_Age(subcat, "S");
                if (subcat == "EX" || subcat == "ExSM")
                {
                    ex_grpC_flag = true;
                }
            }


            if (relaxedage.Count > 0)
            {
                relaxedage.Sort();
                maxage = relaxedage.Last();
            }
            else
            {
                maxage = objCandD.GetMaxAge(reqid);
            }

            string cat_subcat = DropDownList_cat.SelectedValue;
            if (SelectedSubCats.Count > 0)
            {
                cat_subcat = DropDownList_cat.SelectedValue + "," + string.Join(",", SelectedSubCats);
            }
            string feereq = check_fee_relax(cat_subcat);
            if (feereq == "Y")
            {
                feereq = check_fee_relax_female(RadioButtonList_mf.SelectedValue);
            }
            DateTime DOB_from_date;
            int length_ex_serv = 0;
            if (ex_grpC_flag == true)
            {

                //if (txt_len_serv.Text == "")
                //{
                //    length_ex_serv = 0;
                //}
                //else
                //{
                if (SelectedSubCats.Contains("EX"))
                {
                    DateTime ex_from_date = DateTime.ParseExact(txt_ex_f_date.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                    DateTime ex_to_date = DateTime.ParseExact(txt_ex_t_date.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                    TimeSpan t = ex_to_date - ex_from_date;
                    length_ex_serv = (int)t.TotalDays;
                    txt_len_serv.Text = length_ex_serv.ToString();
                }
                //}
                DOB_from_date = DateTime.ParseExact(lbl_dob_f.Text, "dd/MM/yyyy", new CultureInfo("en-US")).AddYears(0 - maxage).AddDays(0 - length_ex_serv);
            }
            else
            {
                //dated 14/08/2023
                DateTime DOBTo_date = DateTime.ParseExact(lbl_dob_t.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                DOB_from_date = DateTime.ParseExact(lbl_dob_f.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                DateTime DOBdate = DateTime.ParseExact(txt_DOB.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                DOB_from_date = DateTime.ParseExact(lbl_dob_f.Text, "dd/MM/yyyy", new CultureInfo("en-US")).AddYears(0 - maxage);

            }
            //27 jan 2023 age relaxation
            double age = 0;
            //age = DateTime.Now.AddYears(-DOBdate.Year).Year;
            age = years;// Dated: 28-02-2023
            if (maxage <= age)
            {
                if (age == maxage)
                {
                    if (months > 0 || days > 0)
                    {
                        msg.Show("You are not Eligible for this post");
                        return;
                    }
                }
                else
                {
                    msg.Show("You are not Eligible for this post");
                    return;
                }
            }
            DateTime DOB_to_date = DateTime.ParseExact(lbl_dob_t.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
            DateTime DOB_date = DateTime.ParseExact(txt_DOB.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
            string ex = "";
            for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
            {
                if (CheckBoxList_Subcategory.Items[i].Value == "EX" && CheckBoxList_Subcategory.Items[i].Selected == true)
                {
                    ex = "Y";
                }
                else
                {
                    ex = "N";
                }
            }
            if (SelectedSubCats.Contains("EX"))
            {
                ex = "Y";
            }
            if (DropDownList_cat.SelectedValue == "OBC")
            {
                if (txtbox_noncreamylayerDATE.Text == "")
                    msg.Show("Please fill the Date");
                else if (DropDownList_c_state.SelectedValue == "-1")
                    msg.Show("Please fill the State");
            }
            if (txtbox_noncreamylayerDATE.Text != "")
            {
                DateTime issue_date = DateTime.ParseExact(txtbox_noncreamylayerDATE.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                DateTime doday = DateTime.Now;
                ////DateTime EndsOn_date = DateTime.ParseExact(endson, "dd/MM/yyyy", new CultureInfo("en-US"));
                //In case of reopen for advt. No 02/2012 nonCreamyLayer Certificate date will check with original Endson Date
                if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
                {
                    doday = DateTime.ParseExact(txt_endson.Text, "dd/MM/yyyy", new CultureInfo("en-US"));
                }

                //check_issue_date_flag = check_debar(issue_date, doday);
                check_issue_date_flag = issue_date <= DateTime.ParseExact(txt_endson.Text, "dd/MM/yyyy", new CultureInfo("en-US")); //rohitxd
            }
            if (ph_flag == true)
            {
                if (ph_scrib_chk(ph_flag, ph_scrib))
                {
                    if (check_debar_PH(ph) && check_debar_sub_ph_all())
                    {
                        if (check_issue_date_flag)
                        {
                            if (check_age(DOB_date, DOB_from_date, DOB_to_date, txt_cont.Text) || agevalidationexmpt == "Y")
                            {
                                if (check_debar_flag)
                                {
                                    if (NoncreamylayerDATE != "" || DropDownList_cat.SelectedValue == "UR")//20-02-2023
                                    {
                                        try
                                        {
                                            //int l = objCandD.Update_JobapplicationTransaction(applid, Utility.putstring(txt_name.Text), Utility.putstring(txt_fh_name.Text), Utility.putstring(txt_mothername.Text), Utility.putstring(txt_pre_add.Text), Utility.putstring(txt_par_add.Text), Utility.putstring(txt_pre_pin.Text), Utility.putstring(txt_per_pin.Text), Utility.putstring(txt_mob.Text), Utility.putstring(txt_email.Text), DDL_Nationality.SelectedItem.Text, RadioButtonList_mf.SelectedValue, RadioButtonList_m_status.SelectedValue, DOB, DropDownList_cat.SelectedItem.Text, sub_cat, ph, GovtDateJoin, Utility.putstring(txtbox_noncreamylayer.Text),  NoncreamylayerDATE, CertIssueState, ex, fromdate, todate, RadioButtonList_d.SelectedValue.ToString(), DebarredDate, Utility.putstring(txt_d_year.Text), feereq, CertUssueAuth, txt_cont.Text, txt_len_serv.Text, ph_visual, ph_hearing, ph_ortho, physic, RadioButtonList_scrb_accept.SelectedValue, rbtobcregion.SelectedValue, regno, ip, Utility.putstring(txtspouse.Text));
                                            int l = objCandD.Update_JobapplicationTransaction(applid, Utility.putstring(txt_name.Text), Utility.putstring(txt_fh_name.Text), Utility.putstring(txt_mothername.Text), Utility.putstring(txt_pre_add.Text), Utility.putstring(txt_par_add.Text), Utility.putstring(txt_pre_pin.Text), Utility.putstring(txt_per_pin.Text), Utility.putstring(txt_mob.Text), Utility.putstring(txt_email.Text), DDL_Nationality.SelectedItem.Text, RadioButtonList_mf.SelectedValue, RadioButtonList_m_status.SelectedValue, DOB, DropDownList_cat.SelectedItem.Text, SelectedSubCats, ph, GovtDateJoin, Utility.putstring(txtbox_noncreamylayer.Text), NoncreamylayerDATE, CertIssueState, ex, fromdate, todate, RadioButtonList_d.SelectedValue.ToString(), DebarredDate, Utility.putstring(txt_d_year.Text), feereq, CertUssueAuth, txt_cont.Text, txt_len_serv.Text, ph_visual, ph_hearing, ph_ortho, physic, RadioButtonList_scrb_accept.SelectedValue, rbtobcregion.SelectedValue, regno, ip, Utility.putstring(txtspouse.Text), txtIssuingauthority.Text.Trim(), txtCertificateNo.Text.Trim(), txtIssuedate.Text.Trim(), PH_docSize, 0);

                                            if (l > 0)
                                            {
                                                int temp = 0;
                                                if (DropDownList_cat.SelectedValue == "OBC" && DropDownList_c_state.SelectedValue != "7" && rbtobcregion.SelectedValue == "D")
                                                {
                                                    DataTable dtobcf = objCandD.GetfatherOBCDetails(applid);
                                                    if (dtobcf.Rows.Count > 0)
                                                    {
                                                        temp = objCandD.updateOBCfathercertdetails(applid, txtbox_noncreamylayer_f.Text, txtbox_noncreamylayerDATE_f.Text, txt_cert_issue_auth_f.Text, "7", ddlOBCForM.SelectedValue);
                                                    }
                                                    else
                                                    {
                                                        string IP = GetIPAddress();
                                                        temp = objCandD.insertOBCfathercertdetails(applid, txtbox_noncreamylayer_f.Text, txtbox_noncreamylayerDATE_f.Text, txt_cert_issue_auth_f.Text, "7", IP, ddlOBCForM.SelectedValue);
                                                    }
                                                }
                                                else
                                                {
                                                    temp = objCandD.delete_obcfathercertdetails(applid);
                                                }
                                                if (MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "P")
                                                {
                                                    String applids = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                                                    string url = md5util.CreateTamperProofURL("EditApplication.aspx", null, "applid=" + MD5Util.Encrypt(applids, true));
                                                    Response.Redirect(url);
                                                }
                                                msg.Show("Your data has been Updated");
                                                string url_photo = md5util.CreateTamperProofURL("jobupload.aspx", null, "applid=" + MD5Util.Encrypt(applid.ToString(), true));
                                                // Response.Redirect(url_photo);
                                                Server.Transfer(url_photo);
                                            }
                                            else
                                            {
                                                msg.Show("Data can not be updated");
                                            }
                                        }
                                        catch (Exception error)
                                        {
                                            LogException(error);
                                        }
                                    }
                                    else if ((DropDownList_cat.SelectedValue == "OBC" || DropDownList_cat.SelectedValue == "SC" || DropDownList_cat.SelectedValue == "ST") && NoncreamylayerDATE == "")
                                    {
                                        msg.Show("Certificate issue date must be prior to or equal to actual advertisement date");//28-02-2023
                                    }
                                    else//01-03-2023
                                    {
                                        string url_photo = md5util.CreateTamperProofURL("jobupload.aspx", null, "applid=" + MD5Util.Encrypt(applid.ToString(), true));
                                        Server.Transfer(url_photo);
                                    }
                                }
                                else
                                {
                                    msg.Show("You are debarred by the Board");
                                }
                            }
                            else
                            {
                                msg.Show("According to Age Criteria of the Post, you are not eligible to apply");
                            }
                        }
                        else
                        {
                            msg.Show("Certificate Issue date should be or earlier than or equal to today Date.");
                        }
                    }
                }
                else
                {
                    msg.Show("Please select choice for scribe facility.");
                }
            }
            else
            {
                msg.Show("Please select your PH subcategory.");
            }
        }
        else
        {
            msg.Show("Please Agree with the Declaration.");
        }
        //}
    }
    protected void CheckBoxList_Subcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        dtdata = objCandD.getgender(Session["rid"].ToString()); //Date 27th Jan 2023
        int lastSelectedIndex = 0;
        string lastSelectedValue = string.Empty;
        tr_oh.Visible = false;
        if (IsPostBack)
        {
            txtCertificateNo.Text = "";
            txtIssuedate.Text = "";
            txtIssuingauthority.Text = "";
            ViewState["PhDoc"] = "";
            ViewState["filePath"] = "";
            ViewState["filename"] = "";
        }
        //if (Request.QueryString["reqid"] != null|| Request.QueryString["reqid"] != "")
        if (Request.QueryString["update"] == null)
        {
            reqid = MD5Util.Decrypt(Request.QueryString["reqid"].ToString(), true);// check its availability else get value from session
        }
        else if (Session["reqid"] != "")
        {
            reqid = Session["reqid"].ToString();
        }

        foreach (ListItem listitem in CheckBoxList_Subcategory.Items)
        {
            if (listitem.Selected)
            {
                int thisIndex = CheckBoxList_Subcategory.Items.IndexOf(listitem);

                //if (lastSelectedIndex < thisIndex)
                //{
                lastSelectedIndex = thisIndex;
                lastSelectedValue = listitem.Value;
                dt = objCandD.Getdeptcode(reqid);
                string deptcode = dt.Rows[0]["deptcode"].ToString();
                if (deptcode == "COMBD")
                {
                    DataTable reqReopencombdPost = objCandD.CheckReopenCombdPostOnUpdateApplication(reqid, lastSelectedValue);
                    if (reqReopencombdPost.Rows.Count > 0)
                    {
                        CheckBoxList_Subcategory.Items[lastSelectedIndex].Selected = true;
                    }
                }
                // }
                else
                {
                    DataTable reqReopenPost = objCandD.CheckReopenPostOnUpdateApplication(reqid, lastSelectedValue);
                    if (reqReopenPost.Rows.Count <= 0)
                    {
                        CheckBoxList_Subcategory.Items[lastSelectedIndex].Selected = false;
                        msg.Show("This Post reopened for special criteria candidates.Kindly check eligibility criteria.");
                        return;
                    }
                }

            }

            // DataTable reqReopenPost = objCandD.CheckReopenPostOnUpdateApplication(reqid, lastSelectedValue);
            // if (reqReopenPost.Rows.Count <= 0)
            //{
            // CheckBoxList_Subcategory.Items[lastSelectedIndex].Selected = false;
            //  msg.Show("This Post reopened for special criteria candidtes.Kindly check eligibility criteria.");
            // }
            //else
            // {

            for (int i = 0; i < CheckBoxList_Subcategory.Items.Count; i++)
            {
                //Date 27th Jan 2023
                string gender = dtdata.Rows[0]["gender"].ToString();
                string checkValue = CheckBoxList_Subcategory.Items[i].Value;
                bool isSelected = CheckBoxList_Subcategory.Items[i].Selected;

                if (gender == "male" && checkValue == "WO" && isSelected || gender == "male" && checkValue == "WD" && isSelected || gender == "male" && checkValue == "WOOB" && isSelected || gender == "male" && checkValue == "WOSC" && isSelected || gender == "male" && checkValue == "WC" && isSelected || gender == "male" && checkValue == "WCT" && isSelected)
                {
                    CheckBoxList_Subcategory.Items[i].Selected = false;
                    msg.Show("Please Select Correct Subcategory.");
                    checkValue = " ";
                }
                if (CheckBoxList_Subcategory.Items[i].Value == "CESD" && CheckBoxList_Subcategory.Items[i].Selected == true)
                {
                    //tr_CESD.Visible = true; 03-03-2023
                    //populatePH();

                }
                if (CheckBoxList_Subcategory.Items[i].Value == "PH" && CheckBoxList_Subcategory.Items[i].Selected == true)
                {
                    tr_ph.Visible = true;
                    tr_scrb_accept.Visible = true;
                    populatePH();
                }
                if (CheckBoxList_Subcategory.Items[i].Value == "DGS" && CheckBoxList_Subcategory.Items[i].Selected == true)
                {
                    //tr_dgs.Visible = true;
                }
                if (CheckBoxList_Subcategory.Items[i].Value == "EX" && CheckBoxList_Subcategory.Items[i].Selected == true)
                {
                    tr_ex_serv.Visible = true;
                    Session["EX"] = "EX";
                }

                if (CheckBoxList_Subcategory.Items[i].Value == "PH" && CheckBoxList_Subcategory.Items[i].Selected != true)
                {
                    tr_ph.Visible = false;
                    for (int m = 0; m < CheckboxList_PHSubCat.Items.Count; m++)
                    {
                        CheckboxList_PHSubCat.Items[m].Selected = false;

                        radio_vh.ClearSelection();
                        radio_hh.ClearSelection();

                        tr_hh.Visible = false;
                    }

                }
                if (CheckBoxList_Subcategory.Items[i].Value == "DGS" && CheckBoxList_Subcategory.Items[i].Selected != true)
                {
                    tr_dgs.Visible = false;
                    tr_vh.Visible = false;
                    radio_vh.ClearSelection();
                }
                if (CheckBoxList_Subcategory.Items[i].Value == "EX" && CheckBoxList_Subcategory.Items[i].Selected != true)
                {
                    tr_ex_serv.Visible = false;
                    txt_ex_f_date.Text = "";
                    txt_ex_t_date.Text = "";
                }
                if (CheckBoxList_Subcategory.Items[i].Value == "CON" && CheckBoxList_Subcategory.Items[i].Selected != true)
                {
                    tr_contract.Visible = false;
                    txt_cont.Text = "";
                }
                if (CheckBoxList_Subcategory.Items[i].Value == "CON" && CheckBoxList_Subcategory.Items[i].Selected == true)
                {
                    tr_contract.Visible = true;
                }
                if (CheckBoxList_Subcategory.Items[i].Value == "EX" && CheckBoxList_Subcategory.Items[i].Selected == true)
                {
                    //&& jobDesc == "Group C" 
                    tr_serv.Visible = false;
                }
                if (CheckBoxList_Subcategory.Items[i].Value == "EX" && CheckBoxList_Subcategory.Items[i].Selected != true)
                {
                    tr_serv.Visible = false;
                    txt_len_serv.Text = "";
                }
                if (CheckBoxList_Subcategory.Items[i].Value != "PH" && CheckBoxList_Subcategory.Items[i].Selected != true)
                {
                    tr_scrb_accept.Visible = false;
                }
            }
        }
    }
    public void YR_Age(string cat_code, string CatIndS)
    {
        try
        {
            maxage = objCandD.GetMaxAge(reqid);
            int v_dYear = 0;
            DataRow[] dt_row = dt_age_relax.Select("CatCode='" + cat_code + "' AND CatIndCS='" + CatIndS + "'");
            foreach (DataRow r in dt_row)
            {
                v_dYear = Int32.Parse(r["D_Year"].ToString());
                if (CatIndS == "C" && r["CM"].ToString() == "M")
                {
                    if (cat_code == "SC" || cat_code == "ST" || (cat_code == "OBC" && rbtobcregion.SelectedValue == "D"))
                    {
                        maxage = maxage + v_dYear;
                        otherage = v_dYear;
                    }
                    else
                    {
                        maxage = maxage;
                    }
                }
                else if (CatIndS == "S" && r["CM"].ToString() == "M")
                {
                    maxage = maxage + v_dYear;
                    maxage = maxage + otherage;
                }
                else
                {
                    v_dYear = Int32.Parse(r["D_Year"].ToString());
                    maxage = v_dYear;
                    if (cat_code == "EX" || cat_code == "ExSM")
                    {
                        maxage = v_dYear;
                    }
                    else
                    {
                        maxage = maxage + otherage;
                    }
                }
                relaxedage.Add(maxage);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }


    }
    protected void btn_click_Click(object sender, EventArgs e)
    {
        if (chkadd.Checked)
        {
            if (txt_pre_add.Text == "")
            {
                chkadd.Checked = false;
                msg.Show("Please Enter Permanent Address");
            }
            else if (txt_pre_pin.Text == "")
            {
                chkadd.Checked = false;
                msg.Show("Please Enter Permanent Address Pin");
            }
            else
            {

                txt_par_add.Text = txt_pre_add.Text;
                txt_per_pin.Text = txt_pre_pin.Text;
            }
        }
        else
        {
            txt_par_add.Text = "";
            txt_per_pin.Text = "";
        }
    }

    private bool check_debar_PH(string ph)
    {
        if (ph == "")
        {
            return true;
        }
        else
        {
            if (jid == "")
            {
                jid = Session["jid"].ToString();
                //Session.Remove("jid");
            }
            DataTable ddt = objCandD.Getdeptcode(reqid);
            string deptcode = ddt.Rows[0]["deptcode"].ToString();
            DataTable dt = new DataTable();
            if (deptcode == "COMBD")
            {
                dt = objCandD.check_debar_PH_combd(ph, jid);
            }
            else
            {
                dt = objCandD.check_debar_PH(ph, jid);
            }
            if (dt.Rows.Count == 0)
            {
                msg.Show("You are not eligible to apply for this Post due to the category of your Disability.");
                return false;
            }
            else
            {

                return true;
            }
        }
    }
    private bool check_debar_sub_ph_all()
    {
        return (check_debar_Sub_PH(radio_vh.SelectedValue) && check_debar_Sub_PH(radio_hh.SelectedValue) && check_debar_Sub_PH(radio_oh.SelectedValue));
    }
    private bool check_debar_Sub_PH(string sub_ph)
    {
        DataTable dt;
        if (sub_ph == "")
        {
            return true;
        }
        else
        {
            if (jid == "")
            {
                jid = Session["jid"].ToString();
                //Session.Remove("jid");
            }
            DataTable ddt = objCandD.Getdeptcode(reqid);//05/12/23 combd
            string deptcode = ddt.Rows[0]["deptcode"].ToString();
            if (deptcode == "COMBD")//05/12/23
            {
                dt = objCandD.check_debar_sub_PH_combd(sub_ph, jid);
            }
            else
            {
                dt = objCandD.check_debar_sub_PH(sub_ph, jid);
            }
            if (dt.Rows.Count == 0)
            {
                msg.Show("You are not eligible to apply for this Post due to the category of your Disability.");
                return false;
            }
            else
            {

                return true;
            }
        }
    }
    protected void CheckboxList_PHSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            txtCertificateNo.Text = "";
            txtIssuedate.Text = "";
            txtIssuingauthority.Text = "";
            ViewState["PhDoc"] = "";
            ViewState["filePath"] = "";
            ViewState["filename"] = "";

        }

        int lastSelectedIndex = 0;

        tr_oh.Visible = false;
        foreach (ListItem listitem in CheckboxList_PHSubCat.Items)
        {
            if (listitem.Selected)
            {
                lastSelectedIndex = lastSelectedIndex + 1;
            }
        }
        if (lastSelectedIndex > 1)
        {
            CheckboxList_PHSubCat.SelectedIndex = -1;
            msg.Show("You can select only one category of disability");
        }
        for (int i = 0; i < CheckboxList_PHSubCat.Items.Count; i++)
        {
            //if ((CheckboxList_PHSubCat.Items[i].Value == "1" && CheckboxList_PHSubCat.Items[i].Selected == true) || (CheckboxList_PHSubCat.Items[i].Value == "3" && CheckboxList_PHSubCat.Items[i].Selected == true))
            //{
            //    tr_scrb_accept.Visible = true;
            //}
            //if ((CheckboxList_PHSubCat.Items[i].Value == "1" && CheckboxList_PHSubCat.Items[i].Selected == false) && (CheckboxList_PHSubCat.Items[i].Value == "3" && CheckboxList_PHSubCat.Items[i].Selected == false) && (CheckboxList_PHSubCat.Items[i].Value == "2" && CheckboxList_PHSubCat.Items[i].Selected == true))
            //{
            //    tr_scrb_accept.Visible = false;
            //}
            //if ((CheckboxList_PHSubCat.Items[i].Value == "1" && CheckboxList_PHSubCat.Items[i].Selected == false) && (CheckboxList_PHSubCat.Items[i].Value == "3" && CheckboxList_PHSubCat.Items[i].Selected == false) && (CheckboxList_PHSubCat.Items[i].Value == "2" && CheckboxList_PHSubCat.Items[i].Selected == false))
            //{
            //    tr_scrb_accept.Visible = false;
            //}

            if (CheckboxList_PHSubCat.Items[i].Value == "1" && CheckboxList_PHSubCat.Items[i].Selected == true)
            {
                // pupulate_radio_ph_subcat();
                tr_vh.Visible = true;
                //scribe_flag = true;
            }
            if (CheckboxList_PHSubCat.Items[i].Value == "2" && CheckboxList_PHSubCat.Items[i].Selected == true)
            {
                tr_hh.Visible = true;
                //tr_hh.Visible = false;//for no Sub-SUb Category required by AMit Bhardwaj Praveen 04/10/2021
            }
            if (CheckboxList_PHSubCat.Items[i].Value == "3" && CheckboxList_PHSubCat.Items[i].Selected == true)
            {
                tr_oh.Visible = true;
                //scribe_flag = true;
            }

            if (CheckboxList_PHSubCat.Items[i].Value == "1" && CheckboxList_PHSubCat.Items[i].Selected == false)
            {
                tr_vh.Visible = false;
                radio_vh.ClearSelection();

            }
            if (CheckboxList_PHSubCat.Items[i].Value == "2" && CheckboxList_PHSubCat.Items[i].Selected == false)
            {
                tr_hh.Visible = false;
                radio_hh.ClearSelection();
            }
            if (CheckboxList_PHSubCat.Items[i].Value == "3" && CheckboxList_PHSubCat.Items[i].Selected == false)
            {
                tr_oh.Visible = false;
                radio_oh.ClearSelection();
            }
        }
    }
    private string check_fee_relax(string cat_subcat)
    {
        DataTable dt = new DataTable();
        dt = objCandD.check_fee_relax(cat_subcat);

        if (dt.Rows.Count > 0)
        {
            return "N";
        }
        else
        {
            return "Y";
        }
    }
    private string check_fee_relax_female(string gender)
    {
        DataTable dt = new DataTable();
        dt = objCandD.check_fee_relax_female(gender);

        if (dt.Rows.Count > 0)
        {
            return "N";
        }
        return "Y";
    }
    private bool check_ex_service_date(string from, string to, string endson)
    {
        int i = 0;
        if (from == "" && to == "")
        {
            return true;
        }
        i = Utility.comparedatesDMY(from, to);
        if (i < 0)
        {
            int j = Utility.comparedatesDMY(from, endson);
            DateTime dtendsonnew = (Utility.converttodatetime(endson)).AddYears(1);
            int k = Utility.comparedatesDMY(to, Utility.formatDateinDMY(dtendsonnew));
            if (j < 0 && k <= 0)
            {
                return true;
            }
            else
            {
                msg.Show("ExService Experience from date and to date should be before than Advt. End Date");
                return false;
            }

        }
        else
        {
            msg.Show("ExService experience from date should be less than To Date.");
            return false;
        }
    }
    protected void rbtobcregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_cat.SelectedValue == "OBC" && DropDownList_c_state.SelectedValue != "7" && rbtobcregion.SelectedValue == "D")
        {
            trobcf.Visible = true;
            tr_obc_cert_f.Visible = true;
            tr_obc_state_f.Visible = true;
            trobcform.Visible = true;
        }
        else
        {
            if (rbtobcregion.SelectedValue == "O")
            {
                msg.Show("OBC Outside Delhi is not eligible category for reservation");
                return;
            }
            trobcf.Visible = false;
            tr_obc_cert_f.Visible = false;
            tr_obc_state_f.Visible = false;
            trobcform.Visible = false;
        }

    }
    protected void DropDownList_c_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_cat.SelectedValue == "OBC" && DropDownList_c_state.SelectedValue != "7" && rbtobcregion.SelectedValue == "D")
        {
            trobcf.Visible = true;
            tr_obc_cert_f.Visible = true;
            tr_obc_state_f.Visible = true;
            trobcform.Visible = true;
        }
        else
        {
            if (rbtobcregion.SelectedValue == "O")
            {
                msg.Show("OBC Outside Delhi is not eligible category for reservation");
            }
            trobcf.Visible = false;
            tr_obc_cert_f.Visible = false;
            tr_obc_state_f.Visible = false;
            trobcform.Visible = false;
        }
    }
    protected void RadioButtonList_m_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList_m_status.SelectedValue == "M")
        {

            spn.Visible = true;
            lblspname.Visible = true;
            txtspouse.Visible = true;
            if (txtspouse.Text == "")
            {
                txtspouse.Enabled = true;
            }
            else
            {
                txtspouse.Enabled = false;
            }
        }
        else
        {
            spn.Visible = false;
            lblspname.Visible = false;
            txtspouse.Visible = false;
            txtspouse.Enabled = false;
        }
    }

    protected void btnViewPHDoc_Click(object sender, EventArgs e)
    {
        var x = ViewState["PhDoc"];

        if (ViewState["PhDoc"] != "" || Request.QueryString["update"] != null || PHCertUpload.PostedFile.ContentLength > 0)
        {
            if (Request.QueryString["update"] != null && (ViewState["PhDoc"] == null || ViewState["PhDoc"] == ""))
            {
                String Temp_applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "PHViewCertificate.aspx?applid=" + MD5Util.Encrypt(Temp_applid, true) + "&flag=" + MD5Util.Encrypt("Applid", true)));
            }
            else if (ViewState["filePath"] == null)
            //string strUploadFileName = ViewState["filePath"].ToString();
            {
                if (PHCertUpload.PostedFile != null)
                {
                    PH_docSize = new byte[PHCertUpload.PostedFile.ContentLength];
                    // try
                    // {
                    if (PHCertUpload.PostedFile != null && PHCertUpload.PostedFile.FileName != "")
                    {
                        string filename = PHCertUpload.PostedFile.FileName.ToString();
                        string[] FileExtension = filename.Split('.');
                        string ext = System.IO.Path.GetExtension(PHCertUpload.PostedFile.FileName).ToLower();

                        if (ext != ".pdf" && ext != ".PDF")
                        {
                            msg.Show("only pdf Files are allowed");
                            return;
                        }
                        else
                        {
                            if (Convert.ToDouble(PH_docSize.Length) / (1024 * 1024) <= 2)
                            {
                                HttpPostedFile uploadedImage = PHCertUpload.PostedFile;
                                uploadedImage.InputStream.Read(PH_docSize, 0, (int)PHCertUpload.PostedFile.ContentLength);
                                bool checkfiletype = chkfiletype(PH_docSize, ext);
                                if (checkfiletype)
                                {
                                    string appPath = HttpContext.Current.Request.ApplicationPath;
                                    string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                                    //string strUploadFileName = physicalPath + @"\PHDoc\" + filename;
                                    string strUploadFileName = physicalPath + @"\PHDoc\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                                    PHCertUpload.SaveAs(strUploadFileName);
                                    string pathOnly = Path.GetDirectoryName(strUploadFileName);

                                    string filepath = Server.MapPath(PHCertUpload.FileName);
                                    ViewState["filePath"] = filepath;
                                    string strBase64 = Convert.ToBase64String(PH_docSize);

                                    ViewState["PhDoc"] = strBase64;

                                    Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "PHViewCertificate.aspx?fn=" + MD5Util.Encrypt(strUploadFileName, true) + "&flag=" + MD5Util.Encrypt("fn", true)));

                                    PHCertUpload.Attributes.Add("FileName", filename);
                                    // WebClient User = new WebClient();
                                    //Byte[] FileBuffer = User.DownloadData(strUploadFileName);

                                    // if (FileBuffer != null)
                                    // {
                                    //   Response.ContentType = "application/pdf";
                                    //  Response.AddHeader("content-length", FileBuffer.Length.ToString());
                                    //  Response.BinaryWrite(FileBuffer);
                                    // }

                                }
                                else
                                {
                                    msg.Show("Only pdf files can be uploaded");
                                }
                            }
                            else
                            {
                                msg.Show("Select Maximum file size 1 MB");
                                return;
                            }
                        }
                    }
                    // }
                    // catch (Exception ex) { }
                }

            }
            else
            {
                string strUploadFileName = ViewState["filePath"].ToString();
                Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "PHViewCertificate.aspx?fn=" + MD5Util.Encrypt(strUploadFileName, true) + "&flag=" + MD5Util.Encrypt("fn", true)));
            }
        }
        else
        {
            msg.Show("No any file selected");
            return;
        }

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        UploadPHDoc();
    }


    public void UploadPHDoc()
    {
        if (PHCertUpload.PostedFile != null)
        {
            PH_docSize = new byte[PHCertUpload.PostedFile.ContentLength];
            // try
            // {
            if (PHCertUpload.PostedFile != null && PHCertUpload.PostedFile.FileName != "")
            {
                string filename = PHCertUpload.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');
                string ext = System.IO.Path.GetExtension(PHCertUpload.PostedFile.FileName).ToLower();

                if (ext != ".pdf" && ext != ".PDF")
                {
                    msg.Show("only pdf Files are allowed");
                    return;
                }
                else
                {
                    if (Convert.ToDouble(PH_docSize.Length) / (1024 * 1024) <= 2)
                    {
                        HttpPostedFile uploadedImage = PHCertUpload.PostedFile;
                        uploadedImage.InputStream.Read(PH_docSize, 0, (int)PHCertUpload.PostedFile.ContentLength);
                        bool checkfiletype = chkfiletype(PH_docSize, ext);
                        if (checkfiletype)
                        {
                            string appPath = HttpContext.Current.Request.ApplicationPath;
                            string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                            //string strUploadFileName = physicalPath + @"\PHDoc\" + filename;
                            string strUploadFileName = physicalPath + @"\PHDoc\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                            PHCertUpload.SaveAs(strUploadFileName);
                            string pathOnly = Path.GetDirectoryName(strUploadFileName);

                            string filepath = Server.MapPath(PHCertUpload.FileName);
                            //string strBase64 = Convert.ToBase64String(PH_docSize);
                            ViewState["filePath"] = strUploadFileName;
                            ViewState["filename"] = filename;
                            ViewState["PhDoc"] = PH_docSize;
                            msg.Show("Certificate uploaded successfully. You can preview the certificate by clicking on preview button.");

                            btnViewPHDoc.Enabled = true;
                            fileNameDisplay.InnerText = filename;

                        }
                        else
                        {
                            msg.Show("Only pdf files can be uploaded");
                        }
                    }
                    else
                    {
                        msg.Show("Select Maximum file size 1 MB");
                        return;
                    }

                }
            }
            // }
            // catch (Exception ex) { }
        }
        else { }
    }

    public void Get_PhDocfile(int applid)
    {
        DataTable dt = objCandD.Get_PhFileDoc(applid);
        if (dt.Rows.Count > 0)
        {
            string phDoc = Convert.ToBase64String((byte[])dt.Rows[0]["PhCertificateFile"]);
            ViewState["PhDocfile"] = phDoc;

        }
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

    protected void txtCertificateNo_TextChanged(object sender, EventArgs e)
    {

        ViewState["PhDoc"] = "";
        ViewState["filePath"] = "";
        ViewState["filename"] = "";
        btnViewPHDoc.Enabled = false;
    }
    protected void txtIssuedate_TextChanged(object sender, EventArgs e)
    {

        ViewState["PhDoc"] = "";
        ViewState["filePath"] = "";
        ViewState["filename"] = "";
        btnViewPHDoc.Enabled = false;
    }
    protected void txtIssuingauthority_TextChanged(object sender, EventArgs e)
    {

        ViewState["PhDoc"] = "";
        ViewState["filePath"] = "";
        ViewState["filename"] = "";
        btnViewPHDoc.Enabled = false;
    }

    protected void btnfile_Click(object sender, EventArgs e)
    {
        UploadDoc();
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        if (ViewState["PhDoc"] != "" || Request.QueryString["update"] != null || uploadcesdfile.PostedFile.ContentLength > 0)
        {

            if (Request.QueryString["update"] != null && (ViewState["PhDoc"] == null || ViewState["PhDoc"] == ""))
            {
                String Temp_applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "PHViewCertificate.aspx?applid=" + MD5Util.Encrypt(Temp_applid, true) + "&flag=" + MD5Util.Encrypt("Applid", true)));
            }
            else if (ViewState["filePath"] == null)
            //string strUploadFileName = ViewState["filePath"].ToString();
            {
                if (uploadcesdfile.PostedFile != null)
                {
                    PH_docSize = new byte[uploadcesdfile.PostedFile.ContentLength];
                    // try
                    // {
                    if (uploadcesdfile.PostedFile != null && uploadcesdfile.PostedFile.FileName != "")
                    {
                        string filename = uploadcesdfile.PostedFile.FileName.ToString();
                        string[] FileExtension = filename.Split('.');
                        string ext = System.IO.Path.GetExtension(uploadcesdfile.PostedFile.FileName).ToLower();

                        if (ext != ".pdf" && ext != ".PDF")
                        {
                            msg.Show("only pdf Files are allowed");
                            return;
                        }
                        else
                        {
                            if (Convert.ToDouble(PH_docSize.Length) / (1024 * 1024) <= 2)
                            {
                                HttpPostedFile uploadedImage = uploadcesdfile.PostedFile;
                                uploadedImage.InputStream.Read(PH_docSize, 0, (int)uploadcesdfile.PostedFile.ContentLength);
                                bool checkfiletype = chkfiletype(PH_docSize, ext);
                                if (checkfiletype)
                                {
                                    string appPath = HttpContext.Current.Request.ApplicationPath;
                                    string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                                    //string strUploadFileName = physicalPath + @"\PHDoc\" + filename;
                                    string strUploadFileName = physicalPath + @"\PHDoc\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                                    uploadcesdfile.SaveAs(strUploadFileName);
                                    string pathOnly = Path.GetDirectoryName(strUploadFileName);

                                    string filepath = Server.MapPath(uploadcesdfile.FileName);
                                    ViewState["filePath"] = filepath;
                                    string strBase64 = Convert.ToBase64String(PH_docSize);

                                    ViewState["PhDoc"] = strBase64;

                                    Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "PHViewCertificate.aspx?fn=" + MD5Util.Encrypt(strUploadFileName, true) + "&flag=" + MD5Util.Encrypt("fn", true)));

                                    uploadcesdfile.Attributes.Add("FileName", filename);
                                    // WebClient User = new WebClient();
                                    //Byte[] FileBuffer = User.DownloadData(strUploadFileName);

                                    // if (FileBuffer != null)
                                    // {
                                    //   Response.ContentType = "application/pdf";
                                    //  Response.AddHeader("content-length", FileBuffer.Length.ToString());
                                    //  Response.BinaryWrite(FileBuffer);
                                    // }

                                }
                                else
                                {
                                    msg.Show("Only pdf files can be uploaded");
                                }
                            }
                            else
                            {
                                msg.Show("Select Maximum file size 1 MB");
                                return;
                            }
                        }
                    }
                    // }
                    // catch (Exception ex) { }
                }

            }
            else
            {
                string strUploadFileName = ViewState["filePath"].ToString();
                Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "PHViewCertificate.aspx?fn=" + MD5Util.Encrypt(strUploadFileName, true) + "&flag=" + MD5Util.Encrypt("fn", true)));
            }
        }
        else
        {
            msg.Show("No any file selected");
            return;
        }
    }


    public void UploadDoc()
    {
        if (uploadcesdfile.PostedFile != null)
        {
            PH_docSize = new byte[uploadcesdfile.PostedFile.ContentLength];
            // try
            // {
            if (uploadcesdfile.PostedFile != null && uploadcesdfile.PostedFile.FileName != "")
            {
                string filename = uploadcesdfile.PostedFile.FileName.ToString();
                string[] FileExtension = filename.Split('.');
                string ext = System.IO.Path.GetExtension(uploadcesdfile.PostedFile.FileName).ToLower();

                if (ext != ".pdf" && ext != ".PDF")
                {
                    msg.Show("only pdf Files are allowed");
                    return;
                }
                else
                {
                    if (Convert.ToDouble(PH_docSize.Length) / (1024 * 1024) <= 2)
                    {
                        HttpPostedFile uploadedImage = uploadcesdfile.PostedFile;
                        uploadedImage.InputStream.Read(PH_docSize, 0, (int)uploadcesdfile.PostedFile.ContentLength);
                        bool checkfiletype = chkfiletype(PH_docSize, ext);
                        if (checkfiletype)
                        {
                            string appPath = HttpContext.Current.Request.ApplicationPath;
                            string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                            //string strUploadFileName = physicalPath + @"\PHDoc\" + filename;
                            string strUploadFileName = physicalPath + @"\PHDoc\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                            uploadcesdfile.SaveAs(strUploadFileName);
                            string pathOnly = Path.GetDirectoryName(strUploadFileName);

                            string filepath = Server.MapPath(uploadcesdfile.FileName);
                            //string strBase64 = Convert.ToBase64String(PH_docSize);
                            ViewState["filePath"] = strUploadFileName;
                            ViewState["filename"] = filename;
                            ViewState["PhDoc"] = PH_docSize;
                            msg.Show("Certificate uploaded successfully. You can preview the certificate by clicking on preview button.");

                            btnview.Enabled = true;

                        }
                        else
                        {
                            msg.Show("Only pdf files can be uploaded");
                        }
                    }
                    else
                    {
                        msg.Show("Select Maximum file size 2 MB");
                        return;
                    }

                }
            }
            // }
            // catch (Exception ex) { }
        }
        else { }
    }

    public void overrideTextValue()
    {
        var newdtdata = objCandD.getdetail(Session["rid"].ToString());
        fname = Utility.getstring(Server.HtmlEncode(newdtdata.Rows[0]["fname"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["fname"].ToString()));
        mname = Utility.getstring(Server.HtmlEncode(newdtdata.Rows[0]["mothername"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["mothername"].ToString()));
        spname = Utility.getstring(Server.HtmlEncode(newdtdata.Rows[0]["spousename"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["spousename"].ToString()));
        name = Utility.getstring(Server.HtmlEncode(newdtdata.Rows[0]["name"].ToString()));

        dob = Session["birthdt"].ToString();
        gender = Utility.getstring(Server.HtmlEncode(newdtdata.Rows[0]["sex"].ToString()));
        //Session["gender"].ToString();
        nationality = Session["nationality"].ToString();
        mob = Utility.getstring(Server.HtmlEncode(newdtdata.Rows[0]["mobileno"].ToString()));// Session["mobileno"].ToString();
        //Utility.getstring(Server.HtmlEncode(dtFill.Rows[0]["address_per"].ToString()));
        email = Utility.getstring(Server.HtmlEncode(newdtdata.Rows[0]["email"].ToString()));//Utility.getstring(Server.HtmlEncode(Session["email"].ToString()));

        txt_name.Text = name;
        txt_fh_name.Text = fname;
        txt_mothername.Text = mname;
        txt_mob.Text = mob;
        txt_email.Text = email;
        txt_DOB.Text = dob;
        DDL_Nationality.SelectedIndex = DDL_Nationality.Items.IndexOf(DDL_Nationality.Items.FindByText(nationality));
        RadioButtonList_mf.SelectedValue = gender;
        txtspouse.Text = spname;
        if (txt_fh_name.Text == "")
        {
            txt_fh_name.Enabled = true;
        }
        else
        {
            txt_fh_name.Enabled = false;
        }
        if (txt_mothername.Text == "")
        {
            txt_mothername.Enabled = true;
        }
        else
        {
            txt_mothername.Enabled = false;
        }
        if (txtspouse.Text == "")
        {
            txtspouse.Enabled = true;
        }
        else
        {
            txtspouse.Enabled = false;
        }
        //if (Session["serial_no"] != null && !string.IsNullOrEmpty(Session["serial_no"].ToString()))
        //{
        //    DropDownList_cat.SelectedValue = category;
        //    DropDownList_cat.Enabled = false;
        //}

    }
}
