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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class Experience : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    DataTable dt = new DataTable();
    MD5Util md5util = new MD5Util();
    int count;
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string appli_id = string.Empty; 
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
           
            if (Request.QueryString["applid"] != null)
            {
                appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                insert_desire_check(Convert.ToInt32(appli_id));

            }
            
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
        string url = "";
        if (Request.QueryString["applid"] == null)
        {
           // url = "Confirm_app.aspx";
        }
        else
        {
            string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            //url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
            img_btn_next.Visible = true;
        }
     //   img_btn_next.PostBackUrl = url;

        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
        string regno = Session["rid"].ToString();
        dt = objcd.get_post(regno);
        if (dt.Rows.Count == 0)
        {
            Button_Vaidate.Visible = false;
            Label14.Visible = false;
            lblmsg.Visible = true;
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["update"] != null)
            {
                if (MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "P")
                {
                    String Temp_applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                    if (!IsPostBack)
                    {
                        insert_desire_check(Convert.ToInt32(Temp_applid));
                        //int dt = objcd.delete_Education(Convert.ToInt32(Temp_applid));
                        //btnexit.Visible = true;
                        img_btn_next.Visible = true;
                        tblconf.Visible = false;
                        fillrbtquali(Temp_applid);

                        if (hfqualitype.Value == "G")
                        {
                            PanExperience.Visible = false;
                            pnlquali.Visible = false;
                        }
                        else
                        {
                            PanExperience.Visible = true;
                            pnlquali.Visible = true;
                        }
                        fill_grid_data(Temp_applid);
                        fill_edu_essential_special(CheckBoxList_special);
                        fill_edu_desire_special(CheckBoxList_desire);
                    }
                }
            }

            if (Request.QueryString["applid"] != null)
            {
                tblconf.Visible = false;
                Button_Vaidate_Click(this, new EventArgs());
               // fillrbtquali(appli_id);
            }
            fill_quali_exp();
        }
    }

    public int slno()
    {
        count = count + 1;
        return count;
    }

    protected void Button_Vaidate_Click(object sender, EventArgs e)
    {
        lbl_step.Visible = true;
        string appli_id = "";
        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
        if (Request.QueryString["applid"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            int dt = objcd.delete_Edu(Convert.ToInt32(appli_id));
            if (rbtquali.Items.Count > 0)
            {
                for (int i = 0; i < rbtquali.Items.Count; i++)

                {

                    if (Session["checked"] != null)
                    {

                        int temp = objcd.delete_Education_full(Int32.Parse(appli_id), "E");
                        int temp2 = objcd.delete_Education_EX(Int32.Parse(appli_id), "E");
                        int temp3 = objcd.delete_JobApplication_Exp_D_full(Int32.Parse(appli_id));
                        gvquali.Visible = false;
                        gvexp.Visible = false;

                    }


                }
            }
        }
        if (Request.QueryString["update"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        if (appli_id == "")
        {
            appli_id = ddlpost.SelectedValue;
            int dt = objcd.delete_Edu(Convert.ToInt32(appli_id));
            insert_desire_check(Convert.ToInt32(appli_id));
        }
        else
        {
            if (Session["intraflag"] == null)
            {
                ddlpost.SelectedValue = appli_id;
            }
        }
        fillrbtquali(appli_id);
        if (hfqualitype.Value == "G")
        {
            PanExperience.Visible = false;
            pnlquali.Visible = false;
        }
        else
        {
            PanExperience.Visible = true;
            pnlquali.Visible = true;
        }
        fill_grid_data(appli_id);
        fill_edu_essential_special(CheckBoxList_special);
        fill_edu_desire_special(CheckBoxList_desire);
    }

    public void fill_grid_data(string applid)
    {
        if (applid != "")
        {
            if (Validation.chkescape(applid))
            {
                msg.Show("Invalid Character in Post Applied");
            }
            else
            {
                fill_quali_exp();
                ViewState["quli"] = "";
                ViewState["quli_desire"] = "";
                DatatableQuali();
                DatatableQuali_desire();
                grid_Qualification();
                ViewState["exp"] = "";
                DatatableExp();
                grid_experience();
            }
        }
        else
        {
            msg.Show("Select Value in Post Selected");
        }
    }
    public void fill_quali_exp()
    {
        string appli_id = "";
        if (Request.QueryString["update"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else if (Request.QueryString["applid"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            appli_id = ddlpost.SelectedValue;
        }
        if (appli_id!="")
        {
        dt = objcd.Get_fill_quali_exp(appli_id);
        
        if (dt.Rows.Count > 0)
        {
            string jid = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["jid"].ToString()));
            string reqid = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["reqid"].ToString()));
            txtjid.Text = reqid;
            string post = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["JobTitle"].ToString()));
            string post_code = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["postcode"].ToString()));
            string equli = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["essential_qual"].ToString()));
            string dquli = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["desire_qual"].ToString()));
            string desireexp = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["desire_exp"].ToString()));
                string eexp = "";
            eexp = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["essential_exp"].ToString()));
            string exp_noofyear = dt.Rows[0]["exp_noofyears"].ToString();
           
            DataTable dt_checkbox_check = objcd.GetJobApplication_Education(appli_id, "E", "G");
            if (dt_checkbox_check.Rows.Count > 0)
            {
                int groupno = 0;
                for (int k = 0; k < dt_checkbox_check.Rows.Count; k++)
                {
                    if (dt_checkbox_check.Rows[k]["groupno"].ToString() != "")
                    {
                        groupno = Convert.ToInt32(dt_checkbox_check.Rows[k]["groupno"]);
                        if (groupno != 0)
                        {
                            for (int i = 0; i < rbtquali.Items.Count; i++)

                            {
                                //string []rbt=rbtquali.Items[i].Text.ToString().Split(new char[] {'+' });

                                //for (int l = 0; l < rbt.Length; l++)
                                //{
                                //    if (dt_checkbox_check.Rows[k]["name"].ToString() == Session["checked"])
                                //    // if (dt_checkbox_check.Rows.Count== rbt.Length)
                                //    // if (rbtquali.Items[i].Selected)
                                //    {

                                //        rbtquali.Items[i].Selected = true;

                                //    }
                                //}

                                if (Session["checked"]!=null)
                                {
                                        //foreach (string s in Session["checked"].ToString().Split(new char['|'], StringSplitOptions.RemoveEmptyEntries))
                                        //{
                                        //    rbtquali.Items.FindByText(s).Selected = true;
                                        //} 

                                        //foreach (string s in Session["checked"].ToString().Split(new char['|'], StringSplitOptions.RemoveEmptyEntries))
                                        //{
                                        //    rbtquali.Items.FindByText(s).Selected = true;
                                        //}



                                        //if (rbtquali.Items[i].Text.ToString().Trim() == Session["checked"].ToString().Trim())
                                        //{
                                        //    rbtquali.Items[i].Selected = true;
                                        //}

                                        //fill_grid_data(appli_id);
                                        //fill_edu_essential_special(CheckBoxList_special);
                                        //fill_edu_desire_special(CheckBoxList_desire);
                                        int temp = objcd.delete_Education_full(Int32.Parse(appli_id), "E");
                                        int temp2 = objcd.delete_Education_EX(Int32.Parse(appli_id), "E");
                                        int temp3 = objcd.delete_JobApplication_Exp_D_full(Int32.Parse(appli_id));
                                        gvquali.Visible = false;
                                        gvexp.Visible = false;

                                        // tbl_desire_qual.Visible = false;
                                        // Table1.Visible = false;
                                    }


                                }
                        }
                        // rbtquali.c = groupno.ToString();
                        //  rbtquali.SelectedValue = groupno.ToString();
                        //rbtquali.Items[k].Selected = true;
                    }
                }
                if (groupno != 0)
                {
                    //rbtquali.SelectedValue = groupno.ToString();
                    // rbtquali.SelectedValue = "1";
                    PanExperience.Visible = true;
                    pnlquali.Visible = true;
                }
            }

            if (exp_noofyear == "" )
            {
                if (rbtquali.SelectedValue != "")
                {
                    if (hfqualitype.Value == "G")
                    {
                        List<int> grupno1 = new List<int>();
                        if (rbtquali.SelectedValue != "")
                        {
                            foreach (ListItem item1 in rbtquali.Items)
                            {
                                if (item1.Selected)
                                {
                                    grupno1.Add(Convert.ToInt32(item1.Value));
                                }
                            }
                            // grupno = grupno.Remove(grupno.Length - 1, 1);

                            DataTable dtqexp = objcd.GetGroupexp_ForCheckbox(reqid, grupno1);
                            //DataTable dtqexp = objcd.GetGroupexp(reqid, rbtquali.SelectedValue);
                            if (dtqexp.Rows.Count > 0)
                            {
                                eexp = Utility.getstring(Server.HtmlEncode(dtqexp.Rows[0]["essential_exp"].ToString()));
                                exp_noofyear = dtqexp.Rows[0]["exp_noofyears"].ToString();
                            }
                        }
                    }
                }
            }
            string dexp = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["desire_exp"].ToString()));

            if (exp_noofyear != "")
            {
                hf_expnoofyear.Value = exp_noofyear;
                gvexp.Visible = true;
            }
            else
            {
                hf_expnoofyear.Value = "0";
                gvexp.Visible = false;//Added by AnkitaSingh 26-07-2022
                tr_exp_h.Visible = false;
            }
            string closeing_date = dt.Rows[0]["EndsOn"].ToString();
            hfcldate.Value = closeing_date;
            if (post != "")
            {
                lbl_app.Text = post;
            }
            else
            {
                lbl_app.Text = "Nil";
            }
            if (post_code != "")
            {
                lbl_post_code.Text = post_code;
            }
            else
            {
                lbl_post_code.Text = "Nil";
            }
            if (equli != "")
            {
                lbl_equli.Text = equli;
                lbleq.Visible = true;
                lbl_equli.Visible = true;
                gvquali.Visible = true;
            }
            else
            {
                lbl_equli.Text = "Nil";
                lbleq.Visible = false;
                lbl_equli.Visible = false;
                gvquali.Visible = false;
            }
            if (lbl_equli.Text == "Nil")
            {
                lbleq.Visible = false;
                lbl_equli.Visible = false;
            }
            if (dquli != "")
            {
                lbl_dquli.Text = dquli;// for Desirable Educational Details for Qualification
                lbldq.Visible = true;
                lbl_dquli.Visible = true;
                gvquali_desire.Visible = false;
                yes.Visible = true;
                no.Visible = true;
                desirable.Visible = true;
            }
            if(desireexp!=null)
                {
                    tr_exp_h.Visible = true;
                    Exp_desirable.Visible = true;
                    trdesire_ep_1.Visible = true;
                }
            else
            {
                lbl_dquli.Text = "Nil";
               
            }
             if(lbl_dquli.Text == "Nil")
            {
                lbldq.Visible = false;
                tr_desire_ed_lbl.Visible = false;

                lbl_dquli.Visible = false;
                gvquali_desire.Visible = false;
                yes.Visible = false;
                no.Visible = false;
                desirable.Visible = false;
            }
            if (eexp != "")
            {
                lbl_eexp.Text = eexp;
                lblee.Visible = true;
                lbl_eexp.Visible = true;
                gvexp.Visible = true;//added by heena 27/07/2022
            }
            else
            {
                lbl_eexp.Text = "Nil";
                lblee.Visible = false;
                lbl_eexp.Visible = false;
                gvexp.Visible = false;//Added by AnkitaSingh 26-07-2022
                tr_exp_h.Visible = false;
            }
            if (dexp != "")
            {
                lbl_dexp.Text = dexp;
                lbl_dexp.Visible = true;//added by heena 27/07/2022
                gvexp.Visible = false;//added by heena 27/07/2022
                tbldesirable_exp.Visible = true;
            }
            else
            {
                lbl_dexp.Text = "Nil";
                lbldexp.Visible = false;
                lbl_dexp.Visible = false;
                trdesirable_exp.Visible = false;
                tbldesirable_exp.Visible = false;

            }
            if (lbl_eexp.Text == "Nil" && lbl_dexp.Text == "Nil")
            {
               
                tr_exp.Visible = false;
                tr_exp_h.Visible = false;
                tr_exp_l.Visible = true;
                trdesire_ep_1.Visible = false;
                gvexp.Visible = false;//Added by AnkitaSingh 26-07-2022
                trdesirable_exp.Visible = false;
                Exp_desirable.Visible = false;
            }
            if (lbl_dexp.Text == "Nil")
            {
                trdesire_ep_1.Visible = false;
                trdesirable_exp.Visible = false;
                tbldesirable_exp.Visible = false;
                    // Exp_desirable.Visible = false;
                }
            else
            {
                trdesire_ep_1.Visible = true;
                tbldesirable_exp.Visible = true;
            }
               if (lbl_eexp.Text != "Nil")
            {
                tr_exp.Visible = true;
                tr_exp_h.Visible = true;
                tr_exp_l.Visible = false;
                gvexp.Visible = true;////added by heena 27/07/2022

            }
        }
    }
    }
    public void fill_standard(DropDownList stand)
    {
        string reqid = txtjid.Text;
        List<int> grupno1 = new List<int>();
        if (rbtquali.SelectedValue != "")
        {
            foreach (ListItem item1 in rbtquali.Items)
            {
                if (item1.Selected)
                {
                    grupno1.Add(Convert.ToInt32(item1.Value));
                }
            }
            // grupno = grupno.Remove(grupno.Length - 1, 1);
            dt = objcd.fill_standard_check(reqid, "E", grupno1);
        }
        else
        {
            dt = objcd.fill_standard(reqid, "E", rbtquali.SelectedValue);
        }
        stand.DataTextField = "standard";
        stand.DataValueField = "id";
        stand.DataSource = dt;
        stand.DataBind();
        ListItem item = new ListItem();
        item.Text = "--Select Any--";
        item.Value = "";
        stand.Items.Insert(0, item);
    }
    public void fill_standard_desire(DropDownList stand)
    {
        string reqid = txtjid.Text;
        dt = objcd.fill_standard(reqid, "D", "");
        stand.DataTextField = "standard";
        stand.DataValueField = "id";
        stand.DataSource = dt;
        stand.DataBind();
        ListItem item = new ListItem();
        item.Text = "--Select Any--";
        item.Value = "";
        stand.Items.Insert(0, item);
    }
    public void fill_edu_essential_special(CheckBoxList chk_edu)
    {
        try
        {
            string reqid = txtjid.Text;
            DataTable dt = new DataTable();
            if (reqid != "")
              
                dt = objcd.GetEducationMinimumClass_special(reqid, "7", "E", rbtquali.SelectedValue);
            FillCheckboxList_special_essential(CheckBoxList_special, dt, "name", "uid");
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void fill_edu_desire_special(CheckBoxList chk_edu)
    {
        try
        {
            string reqid = txtjid.Text;
            DataTable dt = new DataTable();
            if (reqid != "")
                dt = objcd.GetEducationMinimumClass_special(reqid, "7", "D", rbtquali.SelectedValue);

            FillCheckboxList_special_desire(CheckBoxList_desire, dt, "name", "uid");
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void populateEdu(DropDownList quali, string depstand)
    {
        try
        {
            List<int> grupno1 = new List<int>();
            string appli_id = "";
            if (Request.QueryString["update"] != null)
            {
                appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            }
            else
            {
                DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                appli_id = ddlpost.SelectedValue;
            }
            string reqid = txtjid.Text;
            DataTable dt = new DataTable();
            if (reqid != "")

                if (rbtquali.SelectedValue != "")
                {
                    foreach (ListItem item1 in rbtquali.Items)
                    {
                        if (item1.Selected)
                        {
                            grupno1.Add(Convert.ToInt32(item1.Value));
                        }
                    }
                }
              dt = objcd.GetEducationMinimumClass(reqid, depstand, "E", rbtquali.SelectedValue);
           // dt = objcd.GetEducationMinimumClass_getbygroup(reqid, depstand, "E", grupno1);
           // FillDropDown(quali, dt, "name", "id");
            FillDropDown(quali, dt, "name", "uid");
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void populateEdu_desire(DropDownList quali, string depstand)
    {
        try
        {
            string appli_id = "";
            if (Request.QueryString["update"] != null)
            {
                appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            }
            else
            {
                DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                appli_id = ddlpost.SelectedValue;
            }
            string reqid = txtjid.Text;

            DataTable dt = new DataTable();
            if (reqid != "")
                dt = objcd.GetEducationMinimumClass(reqid, depstand, "D", "");
            FillDropDown(quali, dt, "name", "uid");
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void FillDropDown(DropDownList ddl, DataTable dt, string textfield, string valuefield)
    {
        ddl.Items.Clear();
        ddl.DataTextField = textfield;
        ddl.DataValueField = valuefield;
        ddl.DataSource = dt;
        ddl.DataBind();
        ListItem l1 = new ListItem();
        l1.Text = "--Select--";
        l1.Value = "";
        ddl.Items.Insert(0, l1);
    }
    private void populateState(DropDownList state)
    {
        try
        {
            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();
            dt = objCandD.SelectState();
            FillDropDown(state, dt, "state", "code");
            //state.SelectedValue = "7";
            FillDropDown(state, dt, "state", "code");
            //state.SelectedValue = "7";
            FillDropDown(state, dt, "state", "code");
            //state.SelectedValue = "7";
            FillDropDown(state, dt, "state", "code");
            //state.SelectedValue = "7";
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void populate_year(DropDownList passyear, string stnd)
    {
        ListItem li;
        string stndt = stnd;
        int can_dob = 0;
        string dob = Session["birthdt"].ToString();
        string dobc = dob.Substring(6);
        if (stndt == "")
        {
            can_dob = Convert.ToInt32(dobc) + 12;
        }
        else
        {
            int st_nd = Convert.ToInt32(stndt);
            if (st_nd == 1)
            {
                can_dob = Convert.ToInt32(dobc) + 12;
            }
            else if (st_nd == 2)
            {
                can_dob = Convert.ToInt32(dobc) + 14;
            }
            else if (st_nd == 3)
            {
                can_dob = Convert.ToInt32(dobc) + 17;
            }
            else if (st_nd == 4)
            {
                can_dob = Convert.ToInt32(dobc) + 19;
            }
            else if (st_nd == 5)
            {
                can_dob = Convert.ToInt32(dobc) + 20;
            }
        }

        int year = DateTime.Now.Year;
        //for (int i = year; i >= year - 36; i--)
        passyear.Items.Clear();
        for (int i = year; i >= can_dob; i--)
        {
            li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            passyear.Items.Add(li);
        }
        li = new ListItem();
        li.Text = "-Select-";
        li.Value = "";
        passyear.Items.Insert(0, li);
    }

    private void populate_month(DropDownList passmonth)
    {
        ListItem li;
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length - 1; i++)
        {
            passmonth.Items.Add(new ListItem(months[i], i.ToString()));
        }
        li = new ListItem();
        li.Text = "-Select-";
        li.Value = "";
        passmonth.Items.Insert(0, li);
    }

    public void grid_Qualification()
    {
        DataTable dt = new DataTable();
        DataTable dt_EX = new DataTable();
        string appli_id = "";
        if (Request.QueryString["update"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else if (Request.QueryString["applid"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            appli_id = ddlpost.SelectedValue;
        }
        dt = objcd.GetJobApplication_Education(appli_id, "E", "G");
        //if (dt.Rows.Count > 0)
        //{
        //    int groupno = 0;
        //    for (int k = 0; k < dt.Rows.Count; k++)
        //    {
        //        if (dt.Rows[k]["groupno"].ToString() != "")
        //        {
        //            groupno = Convert.ToInt32(dt.Rows[k]["groupno"]);
        //            if (groupno != 0)
        //            {
        //                for (int i=0; i<rbtquali.Items.Count; i++)
        //                 {
        //                    if(dt.Rows[k]["name"].ToString()==rbtquali.Items[i].Text.ToString())
        //                   // if (rbtquali.Items[i].Selected)
        //                    {
        //                        rbtquali.Items[i].Selected=true;
              
        //                    }

        //                 }

        //              }

        //               // rbtquali.c = groupno.ToString();
        //              //  rbtquali.SelectedValue = groupno.ToString();
        //                //rbtquali.Items[k].Selected = true;
        //            }
                
        //    }
        //    if (groupno != 0)
        //    {
        //        //rbtquali.SelectedValue = groupno.ToString();
        //       // rbtquali.SelectedValue = "1";
                
                
        //        PanExperience.Visible = true;
        //        pnlquali.Visible = true;
        //    }
        //}
        dt_EX = objcd.GetJobApplication_Education_EX(appli_id, "E", "G");
        if (dt.Rows.Count > 0)
        {
            ViewState["quli"] = dt;
        }
        else
        {
            btnquali.Visible = false;
            dt = (DataTable)ViewState["quli"];
        }
        if (dt_EX.Rows.Count > 0)
        {
            tr_ex.Visible = true;
            txt_ex_auth.Text = dt_EX.Rows[0]["iauth"].ToString();
            txt_ex_cer_no.Text = dt_EX.Rows[0]["certno"].ToString();
            txt_ex_issue_date.Text = Utility.formatDateinDMY(dt_EX.Rows[0]["doi"].ToString());
            disable_EX();
        }
        else
        {
            tr_ex.Visible = false;
        }
        gvquali.DataSource = dt;
        gvquali.DataBind();

        fill_edu_essential_special(CheckBoxList_special);
        dt = objcd.GetJobApplication_Education(appli_id, "E", "S");
        FillCheckboxList_saved_special_essential(dt);
        //***************************For desire qualification
        dt = objcd.GetJobApplication_Education(appli_id, "D", "G");
        if (dt.Rows.Count > 0)
        {
            ViewState["quli_desire"] = dt;
        }
        else
        {
            gvquali_desire.Visible = false;
            dt = (DataTable)ViewState["quli_desire"];
        }
        gvquali_desire.DataSource = dt;
        gvquali_desire.DataBind();

        fill_edu_desire_special(CheckBoxList_desire);
        dt = objcd.GetJobApplication_Education(appli_id, "D", "S");
        FillCheckboxList_saved_special_desire(dt);
    }

    public DataTable grid_Qualification_view()
    {
        btnqualialt.Visible = true;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["quli"];
        gvquali.DataSource = dt;
        gvquali.DataBind();
        return dt;
    }
    public DataTable grid_Qualification_desire_view()
    {
        btnqualialt_desire.Visible = true;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["quli_desire"];
        gvquali_desire.DataSource = dt;
        gvquali_desire.DataBind();
        return dt;
    }

    private void DatatableQuali()
    {
        DataTable tb = new DataTable("quli");
        tb.Columns.Add(new DataColumn("Id", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("applid", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("stnd", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("standard", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("qid", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("Extraquli", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("Percentage", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("board", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("Stateid", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("State", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("Month", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("year", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("myear", Type.GetType("System.String")));

        ViewState["quli"] = tb;

    }
    private void DatatableQuali_desire()
    {
        DataTable tb = new DataTable("quli");
        tb.Columns.Add(new DataColumn("Id", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("applid", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("stnd", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("standard", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("qid", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("Extraquli", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("Percentage", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("board", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("Stateid", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("State", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("Month", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("year", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("myear", Type.GetType("System.String")));

        ViewState["quli_desire"] = tb;

    }

    protected void gvquali_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {

                gvquali.Columns[3].Visible = false;
                DropDownList ddlstandard = (DropDownList)e.Row.FindControl("ddlstand");
                DropDownList ddlquali = (DropDownList)e.Row.FindControl("DropDownList_q");
                DropDownList ddlstate = (DropDownList)e.Row.FindControl("DropDownList_edu_state");
                DropDownList ddlboardUni = (DropDownList)e.Row.FindControl("ddlBoardUniv");//Praveen for university list
                DropDownList ddlyear = (DropDownList)e.Row.FindControl("DropDownList_year");
                DropDownList ddlmonth = (DropDownList)e.Row.FindControl("DropDownList_month");
                fill_standard(ddlstandard);

                //string standcode = gvquali.DataKeys[e.Row.RowIndex].Values["standard"].ToString();
                //ddlstandard.SelectedValue = standcode;
               
                // populateEdu(ddlquali, ""); --ambika
                string qids = gvquali.DataKeys[e.Row.RowIndex].Values["qid"].ToString();
                ddlquali.SelectedValue = qids;
                populateState(ddlstate);
                string stateid = gvquali.DataKeys[e.Row.RowIndex].Values["State"].ToString();
                ddlstate.SelectedValue = stateid;
                string stateEduid = gvquali.DataKeys[e.Row.RowIndex].Values["Stateid"].ToString();
                ddlstate.SelectedValue = stateEduid;
                populateBoardUniversity(ddlboardUni, int.Parse(stateEduid));//Bind Board University list

                populate_year(ddlyear, "");
                string yid = gvquali.DataKeys[e.Row.RowIndex].Values["YEAR"].ToString();
                ddlyear.SelectedValue = yid;
                populate_month(ddlmonth);
                string mid = gvquali.DataKeys[e.Row.RowIndex].Values["month"].ToString();
                int mnt = Int32.Parse(mid) - 1;
                string month = mnt.ToString();
                ddlmonth.SelectedValue = month;
            }
            Label lbl_stand = (Label)e.Row.FindControl("lblstand");
            Label lbl_percent = (Label)e.Row.FindControl("Label3");
            Label lbl_board = (Label)e.Row.FindControl("lblboard");
            Label lbl_state = (Label)e.Row.FindControl("Label6");
            Label lbl_year = (Label)e.Row.FindControl("lblyear");
            if (lbl_stand != null && lbl_stand.Text == "Ex-Serviceman Degree")
            {
                lbl_percent.Visible = false;
                lbl_board.Visible = false;
                lbl_state.Visible = false;
                lbl_year.Visible = false;
            }
            else if (lbl_percent != null)
            {
                lbl_percent.Visible = true;
                lbl_board.Visible = true;
                lbl_state.Visible = true;
                lbl_year.Visible = true;
            }
        }

        if (e.Row.RowState != DataControlRowState.Edit) // check for RowState
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //check for RowType
            {
                string id = e.Row.Cells[0].Text; // Get the id to be deleted
                //cast the ShowDeleteButton link to linkbutton
                LinkButton lb = (LinkButton)e.Row.Cells[0].Controls[2];
                //int lb1 = Convert.ToInt32(lb);
                if (lb != null && lb.Text != "Cancel")
                {

                    lb.Attributes.Add("onclick", "return ConfirmOnDelete('" + id + "');");
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            gvquali.Columns[3].Visible = false;
            DropDownList ddlstandard = (DropDownList)(e.Row.FindControl("ddlstande"));
            fill_standard(ddlstandard);
          
            DropDownList ddlquali = (DropDownList)(e.Row.FindControl("DropDownList_qe"));
           // populateEdu(ddlquali, "");
            DropDownList ddlstate = (DropDownList)(e.Row.FindControl("DropDownList_edu_statee"));
            populateState(ddlstate);
            DropDownList ddlmonth = (DropDownList)(e.Row.FindControl("DropDownList_monthe"));
            populate_month(ddlmonth);
            DropDownList ddlyear = (DropDownList)(e.Row.FindControl("DropDownList_yeare"));
            populate_year(ddlyear, "");
        }
    }
    protected void gvquali_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        gvquali.EditIndex = e.NewEditIndex;
        grid_Qualification_view();
        enable_EX();
        //DropDownList ddlstandard = (DropDownList)(FindControl("ddlstande"));
        //fill_standard(ddlstandard);
    }

    private void enable_EX()
    {
        txt_ex_auth.Enabled = true;
        txt_ex_cer_no.Enabled = true;
        txt_ex_issue_date.Enabled = true;
    }
    private void disable_EX()
    {
        txt_ex_auth.Enabled = false;
        txt_ex_cer_no.Enabled = false;
        txt_ex_issue_date.Enabled = false;
    }
    protected void gvquali_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string quliid = gvquali.DataKeys[index].Values["id"].ToString();
        string id = gvquali.DataKeys[index].Values["id"].ToString();
        int quli_id = Int32.Parse(quliid);
        DropDownList ddlstandard = ((DropDownList)gvquali.Rows[index].FindControl("ddlstand"));
        DropDownList ddlquali = ((DropDownList)gvquali.Rows[index].FindControl("DropDownList_q"));
        string txtquli = ((TextBox)gvquali.Rows[index].FindControl("txtquli")).Text;
        //dhiraj
        //string txtpercent = ((TextBox)gvquali.Rows[index].FindControl("txt_per")).Text;

        string txtpercent = string.Empty; ;
        //dhiraj
        DataTable dt1 = new DataTable();
        dt1 = objcd.Get_Education_Percentage(Int32.Parse(txtjid.Text), rbtquali.SelectedValue);
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; dt1.Rows.Count > i; i++)
            {
                if (dt1.Rows[i]["MinPercent"] != DBNull.Value)
                {

                    //int ttper = Convert.ToInt32(((TextBox)gvquali.Rows[index].FindControl("txt_per")).Text);
                    string txtpercent1 = ((TextBox)gvquali.Rows[index].FindControl("txt_per")).Text;

                    decimal pp = Convert.ToDecimal(txtpercent1);//30-12-2022
                    //string[] pp1 = pp.Split('.');
                    //string first1 = pp1[0];
                    //int ttper = Convert.ToInt32(first1);

                    // int per = Convert.ToInt32(dt1.Rows[0]["MinPercent"].ToString().Split('.'));
                    decimal val = Convert.ToDecimal(dt1.Rows[i]["MinPercent"]);
                    //string[] s1 = val.Split('.');
                    //string first = s1[0];
                    //// string second = s1[1];
                    //int per = Convert.ToInt32(first);//data table value
                    if (pp >= val)
                    {
                        txtpercent = Convert.ToString(pp);
                    }
                    else
                    {
                        msg.Show("Please enter minimum Percentage as per RR");
                    }

                }
                else
                {
                    txtpercent = ((TextBox)gvquali.Rows[index].FindControl("txt_per")).Text;
                }
            }
        }
        else
        {
            txtpercent = ((TextBox)gvquali.Rows[index].FindControl("txt_per")).Text;
        }

        string txtboard = ((TextBox)gvquali.Rows[index].FindControl("txt_ex_body")).Text;
        DropDownList ddlstate = ((DropDownList)gvquali.Rows[index].FindControl("DropDownList_edu_state"));

        DropDownList ddleduBorad = ((DropDownList)gvquali.Rows[index].FindControl("ddlBoardUniv"));

        DropDownList ddlmonth = ((DropDownList)gvquali.Rows[index].FindControl("DropDownList_month"));
        DropDownList ddlyear = ((DropDownList)gvquali.Rows[index].FindControl("DropDownList_year"));
        try
        {
            message msg = new message();
            string[] listofvalues = new string[] { "Y", "N" };
            string month = "";
            int mnt = Int32.Parse(ddlmonth.SelectedValue) + 1;
            if (mnt > 9)
            {
                month = mnt.ToString();
            }
            else
            {
                month = "0" + mnt.ToString();
            }
            string edudate = "01/" + month + "/" + ddlyear.SelectedValue;
            string cl_date = hfcldate.Value;
            //Regex regper = new Regex(@"^-?[0-9]{0,2}(\.[0-9]{1,2})?%?$|^-?(100)(\.[0]{1,2})?%?$");
            Regex regper = new Regex(@"^(?=.*[1-9].*$)\d{0,2}(?:\.\d{0,2})?$");
            Regex regboard = new Regex(@"[a-zA-Z]");
            Regex regboardlimit = new Regex(@".{2,50}.*");
            if (ddlstandard.SelectedValue == "")
            {
                msg.Show("Please Enter Standard");
            }
            else if (ddlquali.SelectedValue == "")
            {
                msg.Show("Please Enter Discipline");
            }
            else if (txtpercent == "")
            {
                msg.Show("Please Enter Percentage");
            }
            else if (!regper.IsMatch(txtpercent) || txtpercent.Length > 5)
            {
                msg.Show("Enter Valid Percentage");
            }
            else if (ddleduBorad.SelectedValue == "" && ddleduBorad.SelectedItem.Text == "--Select--")
            {
                msg.Show("Please select Board");
            }
            //else if (!regboardlimit.IsMatch(txtboard))
            //{
            //    msg.Show("Minimum 2 character and Maximum 50 character are allowed in Board/ University");
            //}
            //else if (!regboard.IsMatch(txtboard))
            //{
            //    msg.Show("Invalid characters in Board/ University");
            //}

            else if (ddlstate.SelectedValue == "")
            {
                msg.Show("Please Enter State");
            }
            else if (ddlmonth.SelectedValue == "")
            {
                msg.Show("Please Select Passing Month");
            }
            else if (ddlyear.SelectedValue == "")
            {
                msg.Show("Please Select Passing Year");
            }

            else if (!ValidateDropdown.validate(ddlstandard.SelectedValue, "standardMaster", "id"))
            {

                msg.Show("Invalid Inputs in Standard");
            }
            else if (Validation.chkescape(txtpercent))
            {
                msg.Show("Invalid Inputs in Percentage");
            }
            else if (Validation.chkescape(txtboard))
            {
                msg.Show("Invalid Inputs in Board");
            }

            else if (!ValidateDropdown.validate(ddlstate.SelectedValue, "m_state", "code"))
            {
                msg.Show("Invalid Inputs in Position(Columns)");
            }
            else if (Validation.chkescape(txtquli))
            {
                msg.Show("Invalid Inputs in Qualification");
            }
            else if (Utility.comparedatesDMY(edudate, cl_date) > 0)
            {
                msg.Show("Passing Date can not be greater then Closing Date");
            }

            else
            {
                string appli_id = "";
                if (Request.QueryString["update"] != null)
                {
                    appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                }
                else
                {
                    DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                    appli_id = ddlpost.SelectedValue;
                }
                int applid = Int32.Parse(appli_id);
                DataTable tb2 = (DataTable)ViewState["quli"];
                DataRow r = tb2.NewRow();
                tb2.Rows.RemoveAt(e.RowIndex);
                r[0] = id;
                r[1] = appli_id;
                r[2] = ddlstandard.SelectedItem.Text;
                r[3] = ddlstandard.SelectedValue;
                //r[4] = "";//ddlquali.SelectedValue;  Praveen 05012022
                r[4] = ddlquali.SelectedValue;
                r[5] = ddlquali.SelectedItem.Text;
                r[6] = txtquli;
                r[7] = txtpercent;
		    if (ddleduBorad.SelectedValue == "0")
                   {
                    r[8] = txtboard;
                   }
                else
                  {
                    r[8] = ddleduBorad.SelectedItem.Text;
                  }
                //r[8] = ddleduBorad.SelectedItem.Text;
                r[9] = ddlstate.SelectedValue;
                r[10] = ddlstate.SelectedItem.Text;
                r[11] = month;
                r[12] = ddlyear.SelectedItem.Text;
                r[13] = r[11].ToString() + "/" + r[12].ToString();
                //praveen for update qualification
                //r[14] = ddlquali.SelectedValue;
                //r[15] = "";
                if (txtjid.Text == "1518" && (ddlstandard.SelectedItem.Text == "XII" || ddlstandard.SelectedItem.Text == "XII 1"))
                {
                string pp = txtpercent;
                string[] pp1 = pp.Split('.');
                string first1 = pp1[0];

                dt1 = objcd.Get_Education_Percentage(Int32.Parse(txtjid.Text), rbtquali.SelectedValue);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["MinPercent"] != DBNull.Value)
                        {
                            if (Convert.ToInt32(first1) >= 45)
                            {


                                tb2.Rows.Add(r);


                            }
                            else
                            {
                                msg.Show("Please Enter Minimum Qualifying Percentage as per RR");
                            }

                        }
                        else
                        {
                            tb2.Rows.Add(r);
                        }
                    }
                }
                }
                else
                {
                      tb2.Rows.Add(r);
                }


                gvquali.EditIndex = -1;
                grid_Qualification_view();
                //msg.Show("The Record is Updated...");

            }

        }
        catch (System.Threading.ThreadAbortException TAE)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void gvquali_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        string quliid = gvquali.DataKeys[index].Values["id"].ToString();
        int quli_id = Int32.Parse(quliid);
        if (Validation.chkLevel(quliid))
        {
            msg.Show("Invalid Inputs in ID");
        }
        else
        {
            int temp = objcd.delete_Education(quli_id);
            DataTable tb2 = (DataTable)ViewState["quli"];
            tb2.Rows.RemoveAt(e.RowIndex);
            gvquali.EditIndex = -1;
            msg.Show("Qualification deleted...");
        }

        grid_Qualification_view();
    }
    protected void gvquali_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvquali.EditIndex = -1;
        grid_Qualification_view();
    }
    protected void gvquali_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Reset")
            {

            }
            else if (e.CommandName == "Add")
            {
                gvquali.Columns[3].Visible = false;
                LinkButton lnkAdd = (LinkButton)(gvquali.FooterRow.FindControl("lnkadd"));
                lnkAdd.Visible = false;
                LinkButton lnkIn = (LinkButton)(gvquali.FooterRow.FindControl("lnkIn"));
                lnkIn.Visible = true;
                LinkButton lnkC = (LinkButton)(gvquali.FooterRow.FindControl("lnkC"));
                lnkC.Visible = true;
                DropDownList ddlstandard = (DropDownList)(gvquali.FooterRow.FindControl("ddlstandf"));
                ddlstandard.Visible = true;
                fill_standard(ddlstandard);
                DropDownList ddlquali = (DropDownList)(gvquali.FooterRow.FindControl("DropDownList_qf"));
                ddlquali.Visible = true;
               // populateEdu(ddlquali, "");
                TextBox txtqulif = (TextBox)(gvquali.FooterRow.FindControl("txtqulif"));
                txtqulif.Visible = true;
                Label lblqulif = (Label)(gvquali.FooterRow.FindControl("tblqulife"));
                lblqulif.Visible = true;
                TextBox txtpercent = (TextBox)(gvquali.FooterRow.FindControl("txt_perf"));
                txtpercent.Visible = true;
                TextBox txtboard = (TextBox)(gvquali.FooterRow.FindControl("txt_ex_bodyf"));
                txtboard.Visible = true;
                DropDownList ddlstate = (DropDownList)(gvquali.FooterRow.FindControl("DropDownList_edu_statef"));
                ddlstate.Visible = true;
                populateState(ddlstate);

                DropDownList ddlbordUniv = (DropDownList)(gvquali.FooterRow.FindControl("ddlBoardUnivf"));//praveen for bordUniv list txt_ex_bodye
                ddlbordUniv.Visible = true;
                string sti = ddlstate.SelectedValue;
                int statid;
                if (sti == "")
                {
                    statid = 0;
                }
                else
                {
                    statid = Convert.ToInt32(ddlstate.SelectedValue);
                }
                populateBoardUniversity(ddlbordUniv, statid);
                if (ddlbordUniv.SelectedValue == "0")
                {
                    txtboard.Visible = true;
                }
                else
                {
                    txtboard.Visible = false;
                }
                DropDownList ddlmonth = (DropDownList)(gvquali.FooterRow.FindControl("DropDownList_monthf"));
                ddlmonth.Visible = true;
                populate_month(ddlmonth);
                DropDownList ddlyear = (DropDownList)(gvquali.FooterRow.FindControl("DropDownList_yearf"));
                ddlyear.Visible = true;
                populate_year(ddlyear, "");
            }
            else if (e.CommandName == "EAdd")
            {
                gvquali.Columns[3].Visible = false;
                LinkButton lnkAdd = (LinkButton)(gvquali.Controls[0].Controls[0].FindControl("lnkadd"));
                lnkAdd.Visible = false;
                LinkButton lnkIn = (LinkButton)(gvquali.Controls[0].Controls[0].FindControl("lnkIn"));
                lnkIn.Visible = true;
                LinkButton lnkC = (LinkButton)(gvquali.Controls[0].Controls[0].FindControl("lnkC"));
                lnkC.Visible = true;
                DropDownList ddlstandard = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("ddlstande"));
                ddlstandard.Visible = true;
                fill_standard(ddlstandard);
                DropDownList ddlquali = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("DropDownList_qe"));
                ddlquali.Visible = true;
              //  populateEdu(ddlquali, "");
                TextBox txtpercent = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_pere"));
                txtpercent.Visible = true;
                TextBox txtboard = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_ex_bodye"));
                txtboard.Visible = true;
                DropDownList ddlstate = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("DropDownList_edu_statee"));
                ddlstate.Visible = true;
                populateState(ddlstate);

                DropDownList ddlseduBoard = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("ddl_edu_boradee"));//Praveen For Education BoardUniv
                ddlseduBoard.Visible = true;
                int statbordid = Convert.ToInt32(ddlstate.SelectedValue);
                populateBoardUniversity(ddlseduBoard, statbordid);
                DropDownList ddlmonth = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("DropDownList_monthe"));
                ddlmonth.Visible = true;
                populate_month(ddlmonth);
                DropDownList ddlyear = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("DropDownList_yeare"));
                ddlyear.Visible = true;
                populate_year(ddlyear, "");
            }
            else if (e.CommandName == "EInsert")
            {
                DataTable tb2 = (DataTable)ViewState["quli"];
                DropDownList ddlstandard = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("ddlstande"));
                DropDownList ddlquali = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("DropDownList_qe"));
                TextBox txt_qulie = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_qulie"));

               // TextBox txtpercent = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_pere"));


                TextBox txtpercent = new TextBox();
                //dhiraj
                DataTable dt1 = new DataTable();
                dt1 = objcd.Get_Education_Percentage(Int32.Parse(txtjid.Text), rbtquali.SelectedValue);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["MinPercent"] != DBNull.Value)
                        {

                            //int ttper = Convert.ToInt32((TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_pere")));
                            TextBox txtpercent1 = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_pere"));

                            decimal pp = Convert.ToDecimal(txtpercent1.Text);//30-12-2022
                            //string[] pp1 = pp.Split('.');
                            //string first1 = pp1[0];
                            ////string second1 = pp1[1];

                            //int ttper = Convert.ToInt32(first1);

                            //int per = Convert.ToInt32(dt1.Rows[0]["MinPercent"].ToString().Split('.'));

                            decimal val = Convert.ToDecimal(dt1.Rows[i]["MinPercent"]);
                            //string[] s1 = val.Split('.');
                            //string first = s1[0];
                            ////string second = s1[1];
                            //int per = Convert.ToInt32(first);


                            if (pp >= val)
                            {
                                txtpercent.Text = Convert.ToString(pp);
                            }
                            else
                            {
                                msg.Show("Please Enter Minimum Qualifying Percentage as per RR");
                                return;
                            }

                        }
                        else
                        {
                            txtpercent = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_pere"));
                        }
                    }
                }
                else
                {
                    txtpercent = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_pere"));
                }
                TextBox txtboard = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_ex_bodye"));
                DropDownList ddlboradId = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("ddl_edu_boradee"));
                DropDownList ddlstate = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("DropDownList_edu_statee"));
                DropDownList ddlmonth = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("DropDownList_monthe"));
                DropDownList ddlyear = (DropDownList)(gvquali.Controls[0].Controls[0].FindControl("DropDownList_yeare"));
                string edudate = "";
                string cl_date = "";
                try
                {
                    message msg = new message();
                    string[] listofvalues = new string[] { "Y", "N" };
                    string month = "";
                    if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree")
                    {
                        int mnt = Int32.Parse(ddlmonth.SelectedValue) + 1;
                        if (mnt > 9)
                        {
                            month = mnt.ToString();
                        }
                        else
                        {
                            month = "0" + mnt.ToString();
                        }
                        edudate = "01/" + month + "/" + ddlyear.SelectedValue;
                        cl_date = hfcldate.Value;
                    }
                    if (!ValidateDropdown.validate(ddlstandard.SelectedValue, "standardMaster", "id"))
                    {

                        msg.Show("Invalid Inputs in Standard");
                    }
                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && Validation.chkescape(txtpercent.Text))
                    {
                        msg.Show("Invalid Inputs in Percentage");
                    }
                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && (Validation.chkescape(txtboard.Text)))
                    {
                        msg.Show("Invalid Inputs in Board or character length is greater than 50 or less than 2.");
                    }
                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && !ValidateDropdown.validate(ddlstate.SelectedValue, "m_state", "code"))
                    {
                        msg.Show("Invalid Inputs in Position(Columns)");
                    }
                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && Validation.chkescape(txt_qulie.Text))
                    {
                        msg.Show("Invalid Inputs in Qualification");
                    }
                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && Utility.comparedatesDMY(edudate, cl_date) > 0)
                    {
                        msg.Show("Passing Date can not be greater then Closing Date");
                    }

                    else
                    {
                        string appli_id = "";
                        if (Request.QueryString["update"] != null)
                        {
                            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                        }
                        else
                        {
                            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                            appli_id = ddlpost.SelectedValue;
                        }

                        DataRow r = tb2.NewRow();

                        r[0] = "1";
                        r[1] = appli_id;

                        r[2] = ddlstandard.SelectedItem.Text;
                        r[3] = ddlstandard.SelectedValue;
                        r[4] = ddlquali.SelectedValue;
                        r[5] = ddlquali.SelectedItem.Text;
                        if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree")
                        {
                            r[6] = txt_qulie.Text;
                            
                                r[7] = txtpercent.Text;
                           
                            

                            r[8] = txtboard.Text;
                            r[9] = ddlstate.SelectedValue;
                            r[10] = ddlstate.SelectedItem.Text;
                            r[11] = month;
                            r[12] = ddlyear.SelectedItem.Text;
                            r[13] = r[11].ToString() + "/" + r[12].ToString();
                        }
                        else
                        {
                            r[6] = "";
                            r[7] = "";
                            r[8] = "";
                            r[9] = "";
                            r[10] = "";
                            r[11] = "";
                            r[12] = "";
                            r[13] = "";
                        }
                        if (txtjid.Text == "1518" && (ddlstandard.SelectedItem.Text=="XII"||ddlstandard.SelectedItem.Text=="XII 1"))
                        {
                        string pp = txtpercent.Text;
                        string[] pp1 = pp.Split('.');
                        string first1 = pp1[0];

                        dt1 = objcd.Get_Education_Percentage(Int32.Parse(txtjid.Text), rbtquali.SelectedValue);
                        if (dt1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                if (dt1.Rows[i]["MinPercent"] != DBNull.Value)
                                {
                                    
                                    if (Convert.ToInt32(first1) >= 45)
                                    {


                                        tb2.Rows.Add(r);


                                    }
                                    else
                                    {
                                        msg.Show("Please Enter Minimum Qualifying Percentage as per RR");
                                    }
                                    break;
                                    
                                    
                                }

                                

                            }
                        }
                       

                        }
                        else
                        {
                            tb2.Rows.Add(r);
                        }

                        //else
                        //{
                        //    tb2.Rows.Add(r);
                        //}

                      
                        
                       

                        gvquali.EditIndex = -1;
                        grid_Qualification_view();
                    }
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else if (e.CommandName == "Insert")
            {
                DropDownList ddlstandard = (DropDownList)(gvquali.FooterRow.FindControl("ddlstandf"));
                DropDownList ddlquali = (DropDownList)(gvquali.FooterRow.FindControl("DropDownList_qf"));
                TextBox txtqulif = (TextBox)(gvquali.FooterRow.FindControl("txtqulif"));
                TextBox txtpercent=new TextBox();
                //dhiraj
                DataTable dt1=new DataTable();
                dt1 = objcd.Get_Education_Percentage(Int32.Parse(txtjid.Text), rbtquali.SelectedValue);
                 if (dt1.Rows.Count > 0)
                 {
                     for (int i = 0;  dt1.Rows.Count > i; i++)
                     {
                         if (dt1.Rows[i]["MinPercent"] != DBNull.Value)
                         {

                             //int ttper = Convert.ToInt32((TextBox)(gvquali.FooterRow.FindControl("txt_perf")));
                             TextBox txtpercent1 = (TextBox)(gvquali.FooterRow.FindControl("txt_perf"));

                             decimal ttper = Convert.ToDecimal(txtpercent1.Text);//30-12-2022

                             decimal val = Convert.ToDecimal(dt1.Rows[i]["MinPercent"]);
                            // string[] s1 = val.Split('.');
                            // string first = s1[i];
                            //// string second = s1[1];
                            // int per = Convert.ToInt32(first);

                             if (ttper >= val)
                             {
                                 txtpercent.Text = Convert.ToString(ttper);
                             }
                             else
                             {

                                 msg.Show("Please Enter Minimum Qualifying Percentage as per RR");
                                 return;
                             }
                         }


                         else
                         {
                             TextBox txtpercent2 = (TextBox)(gvquali.FooterRow.FindControl("txt_perf"));
                             txtpercent.Text = txtpercent2.Text;
                         }
                     }
                 }
                 else
                 {
                     txtpercent=(TextBox)(gvquali.FooterRow.FindControl("txt_perf"));

                 }

                TextBox txtboard = (TextBox)(gvquali.FooterRow.FindControl("txt_ex_bodyf"));
                DropDownList ddlstate = (DropDownList)(gvquali.FooterRow.FindControl("DropDownList_edu_statef"));
                DropDownList ddlbordUniv = (DropDownList)(gvquali.FooterRow.FindControl("ddlBoardUnivf"));
                DropDownList ddlmonth = (DropDownList)(gvquali.FooterRow.FindControl("DropDownList_monthf"));
                DropDownList ddlyear = (DropDownList)(gvquali.FooterRow.FindControl("DropDownList_yearf"));
                int Sno = 0;
                DataTable tb2 = (DataTable)ViewState["quli"];
                Sno = int.Parse(tb2.Rows[0]["Id"].ToString());
                Sno = Sno + 1;
                try
                {
                    message msg = new message();
                    string[] listofvalues = new string[] { "Y", "N" };
                    string month = "";
                    string edudate = "";
                    string cl_date = "";

                    if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree")
                    {
                        int mnt = Int32.Parse(ddlmonth.SelectedValue) + 1;
                        if (mnt > 9)
                        {
                            month = mnt.ToString();
                        }
                        else
                        {
                            month = "0" + mnt.ToString();
                        }
                        edudate = "01/" + month + "/" + ddlyear.SelectedValue;
                        cl_date = hfcldate.Value;
                    }
                    if (!ValidateDropdown.validate(ddlstandard.SelectedValue, "standardMaster", "id"))
                    {

                        msg.Show("Invalid Inputs in Standard");
                    }
                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && (Validation.chkescape(txtpercent.Text) || txtpercent.Text.Length > 5))
                    {
                        msg.Show("Invalid Inputs in Percentage");
                    }

                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && (Validation.chkescape(txtboard.Text)))
                    {
                        msg.Show("Invalid Inputs in Board or character length is greater than 100 or less than 2.");
                    }

                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && !ValidateDropdown.validate(ddlstate.SelectedValue, "m_state", "code"))
                    {
                        msg.Show("Invalid Inputs in Position(Columns)");
                    }

                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && Validation.chkescape(txtqulif.Text))
                    {
                        msg.Show("Invalid Inputs in Qualification");
                    }
                    else if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree" && Utility.comparedatesDMY(edudate, cl_date) > 0)
                    {
                        msg.Show("Passing Date can not be greater then Closing Date");
                    }

                    else
                    {
                        string appli_id = "";
                        if (Request.QueryString["update"] != null)
                        {
                            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                        }
                        else
                        {
                            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                            appli_id = ddlpost.SelectedValue;
                        }
                        int applid = Int32.Parse(appli_id);
                        DataRow r = tb2.NewRow();
                        r[0] = Sno;
                        r[1] = appli_id;
                        r[2] = ddlstandard.SelectedItem.Text;
                        r[3] = ddlstandard.SelectedValue;
                        r[4] = ddlquali.SelectedValue;
                        r[5] = ddlquali.SelectedItem.Text;

                        if (ddlstandard.SelectedItem.ToString() != "Ex-Serviceman Degree")
                        {
                            r[6] = txtqulif.Text;
                            r[7] = txtpercent.Text;
                            if (ddlbordUniv.SelectedValue == "0")
                            {
                                r[8] = txtboard.Text;
                            }
                            else
                            {
                                r[8] = ddlbordUniv.SelectedItem.Text;
                            }
                            r[9] = ddlstate.SelectedValue;
                            r[10] = ddlstate.SelectedItem.Text;
                            r[11] = month;
                            r[12] = ddlyear.SelectedItem.Text;
                            r[13] = r[11].ToString() + "/" + r[12].ToString();
                        }
                        else
                        {
                            r[6] = "";
                            r[7] = "0.00";
                            r[8] = "";
                            r[9] = "0";
                            r[10] = "";
                            r[11] = "";
                            r[12] = "";
                            r[13] = "";
                        }

                        if (txtjid.Text == "1518" && (ddlstandard.SelectedItem.Text == "XII" || ddlstandard.SelectedItem.Text == "XII 1"))
                        {
                            string pp = txtpercent.Text;
                            string[] pp1 = pp.Split('.');
                            string first1 = pp1[0];

                            dt1 = objcd.Get_Education_Percentage(Int32.Parse(txtjid.Text), rbtquali.SelectedValue);
                            if (dt1.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {
                                    if (dt1.Rows[i]["MinPercent"] != DBNull.Value)
                                    {

                                        if (Convert.ToInt32(first1) >= 45)
                                        {


                                            tb2.Rows.Add(r);


                                        }
                                        else
                                        {
                                            msg.Show("Please Enter Minimum Qualifying Percentage as per RR");
                                        }
                                        break;


                                    }



                                }
                            }
                        }
                        else
                        {
                            tb2.Rows.Add(r);
                        }


                        grid_Qualification_view();
                    }

                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            else if (e.CommandName == "Cancel")
            {
                //fillframe();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }


    protected void gvquali_desire_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                gvquali_desire.Columns[3].Visible = false;
                DropDownList ddlstandard = (DropDownList)e.Row.FindControl("ddlstand");
                DropDownList ddlquali = (DropDownList)e.Row.FindControl("DropDownList_q");
                DropDownList ddlstate = (DropDownList)e.Row.FindControl("DropDownList_edu_state");
                DropDownList ddlboardUni = (DropDownList)e.Row.FindControl("ddlBoardUniv");//Praveen for university list
                DropDownList ddlyear = (DropDownList)e.Row.FindControl("DropDownList_year");
                DropDownList ddlmonth = (DropDownList)e.Row.FindControl("DropDownList_month");
                fill_standard_desire(ddlstandard);
                string standcode = gvquali_desire.DataKeys[e.Row.RowIndex].Values["standard"].ToString();
                ddlstandard.SelectedValue = standcode;
                populateEdu_desire(ddlquali, "");
                string qids = gvquali_desire.DataKeys[e.Row.RowIndex].Values["qid"].ToString();
                ddlquali.SelectedValue = qids;
                populateState(ddlstate);
                string stateid = gvquali_desire.DataKeys[e.Row.RowIndex].Values["State"].ToString();
                ddlstate.SelectedValue = stateid;
                populate_year(ddlyear, "");
                string yid = gvquali_desire.DataKeys[e.Row.RowIndex].Values["YEAR"].ToString();
                ddlyear.SelectedValue = yid;
                populate_month(ddlmonth);
                string mid = gvquali_desire.DataKeys[e.Row.RowIndex].Values["month"].ToString();
                int mnt = Int32.Parse(mid) - 1;
                string month = mnt.ToString();
                ddlmonth.SelectedValue = month;
            }
        }

        if (e.Row.RowState != DataControlRowState.Edit) // check for RowState
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //check for RowType
            {
                string id = e.Row.Cells[0].Text; // Get the id to be deleted
                //cast the ShowDeleteButton link to linkbutton
                LinkButton lb = (LinkButton)e.Row.Cells[0].Controls[2];
                //int lb1 = Convert.ToInt32(lb);
                if (lb != null && lb.Text != "Cancel")
                {

                    lb.Attributes.Add("onclick", "return ConfirmOnDelete('" + id + "');");
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            gvquali_desire.Columns[3].Visible = false;
            LinkButton lnkAdd = (LinkButton)(e.Row.FindControl("lnkadd"));
            lnkAdd.Visible = false;
            LinkButton lnkIn = (LinkButton)(e.Row.FindControl("lnkIn"));
            lnkIn.Visible = true;
            LinkButton lnkC = (LinkButton)(e.Row.FindControl("lnkC"));
            lnkC.Visible = true;
            DropDownList ddlstandard = (DropDownList)(e.Row.FindControl("ddlstande"));
            ddlstandard.Visible = true;
            fill_standard_desire(ddlstandard);
            DropDownList ddlquali = (DropDownList)(e.Row.FindControl("DropDownList_qe"));
            ddlquali.Visible = true;
            populateEdu_desire(ddlquali, "");
            TextBox txtpercent = (TextBox)(e.Row.FindControl("txt_pere"));
            txtpercent.Visible = true;
            TextBox txtboard = (TextBox)(e.Row.FindControl("txt_ex_bodye"));
            txtboard.Visible = true;
            DropDownList ddlstate = (DropDownList)(e.Row.FindControl("DropDownList_edu_statee"));
            ddlstate.Visible = true;
            populateState(ddlstate);
            DropDownList ddlmonth = (DropDownList)(e.Row.FindControl("DropDownList_monthe"));
            ddlmonth.Visible = true;
            populate_month(ddlmonth);
            DropDownList ddlyear = (DropDownList)(e.Row.FindControl("DropDownList_yeare"));
            ddlyear.Visible = true;
            populate_year(ddlyear, "");
        }
    }
    protected void gvquali_desire_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        gvquali_desire.EditIndex = e.NewEditIndex;
        grid_Qualification_desire_view();
    }
    protected void gvquali_desire_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string quliid = gvquali_desire.DataKeys[index].Values["id"].ToString();
        string id = gvquali_desire.DataKeys[index].Values["id"].ToString();
        int quli_id = Int32.Parse(quliid);
        DropDownList ddlstandard = ((DropDownList)gvquali_desire.Rows[index].FindControl("ddlstand"));
        DropDownList ddlquali = ((DropDownList)gvquali_desire.Rows[index].FindControl("DropDownList_q"));
        string txtquli = ((TextBox)gvquali_desire.Rows[index].FindControl("txtquli")).Text;
        string txtpercent = ((TextBox)gvquali_desire.Rows[index].FindControl("txt_per")).Text;
        string txtboard = ((TextBox)gvquali_desire.Rows[index].FindControl("txt_ex_body")).Text;
        DropDownList ddlstate = ((DropDownList)gvquali_desire.Rows[index].FindControl("DropDownList_edu_state"));
        DropDownList ddlmonth = ((DropDownList)gvquali_desire.Rows[index].FindControl("DropDownList_month"));
        DropDownList ddlyear = ((DropDownList)gvquali_desire.Rows[index].FindControl("DropDownList_year"));
        try
        {
            message msg = new message();
            string[] listofvalues = new string[] { "Y", "N" };
            string month = "";
            int mnt = Int32.Parse(ddlmonth.SelectedValue) + 1;
            if (mnt > 9)
            {
                month = mnt.ToString();
            }
            else
            {
                month = "0" + mnt.ToString();
            }
            string edudate = "01/" + month + "/" + ddlyear.SelectedValue;
            string cl_date = hfcldate.Value;
            //Regex regper = new Regex(@"^-?[0-9]{0,2}(\.[0-9]{1,2})?%?$|^-?(100)(\.[0]{1,2})?%?$");
            Regex regper = new Regex(@"^(?=.*[1-9].*$)\d{0,2}(?:\.\d{0,2})?$");
            Regex regboard = new Regex(@"[a-zA-Z]");
            Regex regboardlimit = new Regex(@".{2,50}.*");
            if (ddlstandard.SelectedValue == "")
            {
                msg.Show("Please Enter Standard");
            }
            else if (ddlquali.SelectedValue == "")
            {
                msg.Show("Please Enter Discipline");
            }
            //else if (txtquli == "")
            //{
            //    msg.Show("Please Enter Qualification");
            //}
            else if (txtpercent == "")
            {
                msg.Show("Please Enter Percentage");
            }
            else if (!regper.IsMatch(txtpercent) || txtpercent.Length > 5)
            {
                msg.Show("Enter Valid Percentage");
            }
            else if (txtboard == "")
            {
                msg.Show("Please Enter Board/ University");
            }
            else if (!regboardlimit.IsMatch(txtboard))
            {
                msg.Show("Minimum 2 character and Maximum 50 character are allowed in Board/ University");
            }
            else if (!regboard.IsMatch(txtboard))
            {
                msg.Show("Invalid characters in Board/ University");
            }

            else if (ddlstate.SelectedValue == "")
            {
                msg.Show("Please Enter State");
            }
            else if (ddlmonth.SelectedValue == "")
            {
                msg.Show("Please Select Passing Month");
            }
            else if (ddlyear.SelectedValue == "")
            {
                msg.Show("Please Select Passing Year");
            }

            else if (!ValidateDropdown.validate(ddlstandard.SelectedValue, "standardMaster", "id"))
            {

                msg.Show("Invalid Inputs in Standard");
            }
            else if (Validation.chkescape(txtpercent))
            {
                msg.Show("Invalid Inputs in Percentage");
            }
            else if (Validation.chkescape(txtboard))
            {
                msg.Show("Invalid Inputs in Board");
            }

            else if (!ValidateDropdown.validate(ddlstate.SelectedValue, "m_state", "code"))
            {
                msg.Show("Invalid Inputs in Position(Columns)");
            }
            else if (Validation.chkescape(txtquli))
            {
                msg.Show("Invalid Inputs in Qualification");
            }
            else if (Utility.comparedatesDMY(edudate, cl_date) > 0)
            {
                msg.Show("Passing Date can not be greater then Closing Date");
            }

            else
            {
                string appli_id = "";
                if (Request.QueryString["update"] != null)
                {
                    appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                }
                else
                {
                    DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                    appli_id = ddlpost.SelectedValue;
                }
                int applid = Int32.Parse(appli_id);
                DataTable tb2 = (DataTable)ViewState["quli_desire"];
                DataRow r = tb2.NewRow();
                tb2.Rows.RemoveAt(e.RowIndex);
                r[0] = id;
                r[1] = appli_id;
                r[2] = ddlstandard.SelectedItem.Text;
                r[3] = ddlstandard.SelectedValue;
                r[4] = ddlquali.SelectedValue;
                r[5] = ddlquali.SelectedItem.Text;
                r[6] = txtquli;
                r[7] = txtpercent;
                r[8] = txtboard;
                r[9] = ddlstate.SelectedValue;
                r[10] = ddlstate.SelectedItem.Text;
                r[11] = month;
                r[12] = ddlyear.SelectedItem.Text;
                r[13] = r[11].ToString() + "/" + r[12].ToString();

                tb2.Rows.Add(r);
                gvquali_desire.EditIndex = -1;
                grid_Qualification_desire_view();
                //msg.Show("The Record is Updated...");

            }

        }
        catch (System.Threading.ThreadAbortException TAE)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void gvquali_desire_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        string quliid = gvquali_desire.DataKeys[index].Values["id"].ToString();
        int quli_id = Int32.Parse(quliid);
        if (Validation.chkLevel(quliid))
        {
            msg.Show("Invalid Inputs in ID");
        }
        else
        {
            //int temp = objcd.delete_Education(quli_id);
            DataTable tb2 = (DataTable)ViewState["quli_desire"];
            tb2.Rows.RemoveAt(e.RowIndex);
            gvquali.EditIndex = -1;
            msg.Show("Qualification deleted...");

        }

        grid_Qualification_desire_view();
    }
    protected void gvquali_desire_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvquali_desire.EditIndex = -1;
        grid_Qualification_desire_view();

    }
    protected void gvquali_desire_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Reset")
            {

            }
            else if (e.CommandName == "Add")
            {
                gvquali_desire.Columns[3].Visible = false;
                LinkButton lnkAdd = (LinkButton)(gvquali_desire.FooterRow.FindControl("lnkadd"));
                lnkAdd.Visible = false;
                LinkButton lnkIn = (LinkButton)(gvquali_desire.FooterRow.FindControl("lnkIn"));
                lnkIn.Visible = true;
                LinkButton lnkC = (LinkButton)(gvquali_desire.FooterRow.FindControl("lnkC"));
                lnkC.Visible = true;
                DropDownList ddlstandard = (DropDownList)(gvquali_desire.FooterRow.FindControl("ddlstandf"));
                ddlstandard.Visible = true;
                fill_standard_desire(ddlstandard);
                DropDownList ddlquali = (DropDownList)(gvquali_desire.FooterRow.FindControl("DropDownList_qf"));
                ddlquali.Visible = true;
                populateEdu_desire(ddlquali, "");
                TextBox txtqulif = (TextBox)(gvquali_desire.FooterRow.FindControl("txtqulif"));
                txtqulif.Visible = true;
                Label lblqulif = (Label)(gvquali_desire.FooterRow.FindControl("tblqulife"));
                lblqulif.Visible = true;
                TextBox txtpercent = (TextBox)(gvquali_desire.FooterRow.FindControl("txt_perf"));
                txtpercent.Visible = true;
                TextBox txtboard = (TextBox)(gvquali_desire.FooterRow.FindControl("txt_ex_bodyf"));
                txtboard.Visible = true;
                DropDownList ddlstate = (DropDownList)(gvquali_desire.FooterRow.FindControl("DropDownList_edu_statef"));
                ddlstate.Visible = true;
                populateState(ddlstate);
                DropDownList ddlmonth = (DropDownList)(gvquali_desire.FooterRow.FindControl("DropDownList_monthf"));
                ddlmonth.Visible = true;
                populate_month(ddlmonth);
                DropDownList ddlyear = (DropDownList)(gvquali_desire.FooterRow.FindControl("DropDownList_yearf"));
                ddlyear.Visible = true;
                populate_year(ddlyear, "");
            }
            else if (e.CommandName == "EAdd")
            {
                gvquali_desire.Columns[3].Visible = false;
                LinkButton lnkAdd = (LinkButton)(gvquali_desire.Controls[0].Controls[0].FindControl("lnkadd"));
                lnkAdd.Visible = false;
                LinkButton lnkIn = (LinkButton)(gvquali_desire.Controls[0].Controls[0].FindControl("lnkIn"));
                lnkIn.Visible = true;
                LinkButton lnkC = (LinkButton)(gvquali_desire.Controls[0].Controls[0].FindControl("lnkC"));
                lnkC.Visible = true;
                DropDownList ddlstandard = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("ddlstande"));
                ddlstandard.Visible = true;

                fill_standard_desire(ddlstandard);
                DropDownList ddlquali = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("DropDownList_qe"));
                ddlquali.Visible = true;
                populateEdu_desire(ddlquali, "");
                TextBox txtpercent = (TextBox)(gvquali_desire.Controls[0].Controls[0].FindControl("txt_pere"));
                txtpercent.Visible = true;
                TextBox txtboard = (TextBox)(gvquali_desire.Controls[0].Controls[0].FindControl("txt_ex_bodye"));
                txtboard.Visible = true;
                DropDownList ddlstate = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("DropDownList_edu_statee"));
                ddlstate.Visible = true;
                populateState(ddlstate);
                DropDownList ddlmonth = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("DropDownList_monthe"));
                ddlmonth.Visible = true;
                populate_month(ddlmonth);
                DropDownList ddlyear = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("DropDownList_yeare"));
                ddlyear.Visible = true;
                populate_year(ddlyear, "");
            }
            else if (e.CommandName == "EInsert")
            {
                DataTable tb2 = (DataTable)ViewState["quli_desire"];
                DropDownList ddlstandard = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("ddlstande"));
                DropDownList ddlquali = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("DropDownList_qe"));
                TextBox txt_qulie = (TextBox)(gvquali_desire.Controls[0].Controls[0].FindControl("txt_qulie"));
                TextBox txtpercent = (TextBox)(gvquali_desire.Controls[0].Controls[0].FindControl("txt_pere"));
                TextBox txtboard = (TextBox)(gvquali_desire.Controls[0].Controls[0].FindControl("txt_ex_bodye"));
                DropDownList ddlstate = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("DropDownList_edu_statee"));


                DropDownList ddlmonth = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("DropDownList_monthe"));
                DropDownList ddlyear = (DropDownList)(gvquali_desire.Controls[0].Controls[0].FindControl("DropDownList_yeare"));
                try
                {
                    message msg = new message();
                    string[] listofvalues = new string[] { "Y", "N" };
                    string month = "";
                    int mnt = Int32.Parse(ddlmonth.SelectedValue) + 1;
                    if (mnt > 9)
                    {
                        month = mnt.ToString();
                    }
                    else
                    {
                        month = "0" + mnt.ToString();
                    }
                    string edudate = "01/" + month + "/" + ddlyear.SelectedValue;
                    string cl_date = hfcldate.Value;
                    if (!ValidateDropdown.validate(ddlstandard.SelectedValue, "standardMaster", "id"))
                    {

                        msg.Show("Invalid Inputs in Standard");
                    }
                    else if (Validation.chkescape(txtpercent.Text))
                    {
                        msg.Show("Invalid Inputs in Percentage");
                    }
                    else if (Validation.chkescape(txtboard.Text) || txtboard.Text.Length < 2)
                    {
                        msg.Show("Invalid Inputs in Board or character length is greater than 50 or less than 2.");
                    }
                    else if (!ValidateDropdown.validate(ddlstate.SelectedValue, "m_state", "code"))
                    {
                        msg.Show("Invalid Inputs in Position(Columns)");
                    }
                    else if (Validation.chkescape(txt_qulie.Text))
                    {
                        msg.Show("Invalid Inputs in Qualification");
                    }
                    else if (Utility.comparedatesDMY(edudate, cl_date) > 0)
                    {
                        msg.Show("Passing Date can not be greater then Closing Date");
                    }

                    else
                    {
                        string appli_id = "";
                        if (Request.QueryString["update"] != null)
                        {
                            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                        }
                        else
                        {
                            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                            appli_id = ddlpost.SelectedValue;
                        }

                        DataRow r = tb2.NewRow();
                        r[0] = "1";
                        r[1] = appli_id;
                        r[2] = ddlstandard.SelectedItem.Text;
                        r[3] = ddlstandard.SelectedValue;
                        r[4] = ddlquali.SelectedValue;
                        r[5] = ddlquali.SelectedItem.Text;
                        r[6] = txt_qulie.Text;
                        r[7] = txtpercent.Text;
                        r[8] = txtboard.Text;
                        r[9] = ddlstate.SelectedValue;
                        r[10] = ddlstate.SelectedItem.Text;
                        r[11] = month;
                        r[12] = ddlyear.SelectedItem.Text;
                        r[13] = r[11].ToString() + "/" + r[12].ToString();
                        tb2.Rows.Add(r);
                        gvquali_desire.EditIndex = -1;
                        grid_Qualification_desire_view();
                    }
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else if (e.CommandName == "Insert")
            {
                DropDownList ddlstandard = (DropDownList)(gvquali_desire.FooterRow.FindControl("ddlstandf"));
                DropDownList ddlquali = (DropDownList)(gvquali_desire.FooterRow.FindControl("DropDownList_qf"));
                TextBox txtqulif = (TextBox)(gvquali_desire.FooterRow.FindControl("txtqulif"));
                TextBox txtpercent = (TextBox)(gvquali_desire.FooterRow.FindControl("txt_perf"));
                TextBox txtboard = (TextBox)(gvquali_desire.FooterRow.FindControl("txt_ex_bodyf"));
                DropDownList ddlstate = (DropDownList)(gvquali_desire.FooterRow.FindControl("DropDownList_edu_statef"));
                DropDownList ddlmonth = (DropDownList)(gvquali_desire.FooterRow.FindControl("DropDownList_monthf"));
                DropDownList ddlyear = (DropDownList)(gvquali_desire.FooterRow.FindControl("DropDownList_yearf"));
                int Sno = 0;
                DataTable tb2 = (DataTable)ViewState["quli_desire"];
                Sno = int.Parse(tb2.Rows[0]["Id"].ToString());
                Sno = Sno + 1;
                try
                {
                    message msg = new message();
                    string[] listofvalues = new string[] { "Y", "N" };
                    string month = "";
                    int mnt = Int32.Parse(ddlmonth.SelectedValue) + 1;
                    if (mnt > 9)
                    {
                        month = mnt.ToString();
                    }
                    else
                    {
                        month = "0" + mnt.ToString();
                    }
                    string edudate = "01/" + month + "/" + ddlyear.SelectedValue;
                    string cl_date = hfcldate.Value;
                    if (!ValidateDropdown.validate(ddlstandard.SelectedValue, "standardMaster", "id"))
                    {

                        msg.Show("Invalid Inputs in Standard");
                    }
                    else if (Validation.chkescape(txtpercent.Text) || txtpercent.Text.Length > 5)
                    {
                        msg.Show("Invalid Inputs in Percentage");
                    }

                    else if (Validation.chkescape(txtboard.Text) || txtboard.Text.Length < 2)
                    {
                        msg.Show("Invalid Inputs in Board or character length is greater than 50 or less than 2.");
                    }

                    else if (!ValidateDropdown.validate(ddlstate.SelectedValue, "m_state", "code"))
                    {
                        msg.Show("Invalid Inputs in Position(Columns)");
                    }

                    else if (Validation.chkescape(txtqulif.Text))
                    {
                        msg.Show("Invalid Inputs in Qualification");
                    }
                    else if (Utility.comparedatesDMY(edudate, cl_date) > 0)
                    {
                        msg.Show("Passing Date can not be greater then Closing Date");
                    }

                    else
                    {
                        string appli_id = "";
                        if (Request.QueryString["update"] != null)
                        {
                            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                        }
                        else
                        {
                            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                            appli_id = ddlpost.SelectedValue;
                        }
                        int applid = Int32.Parse(appli_id);
                        DataRow r = tb2.NewRow();
                        r[0] = Sno;
                        r[1] = appli_id;
                        r[2] = ddlstandard.SelectedItem.Text;
                        r[3] = ddlstandard.SelectedValue;
                        r[4] = ddlquali.SelectedValue;
                        r[5] = ddlquali.SelectedItem.Text;
                        r[6] = txtqulif.Text;
                        r[7] = txtpercent.Text;
                        r[8] = txtboard.Text;
                        r[9] = ddlstate.SelectedValue;
                        r[10] = ddlstate.SelectedItem.Text;
                        r[11] = month;
                        r[12] = ddlyear.SelectedItem.Text;
                        r[13] = r[11].ToString() + "/" + r[12].ToString();
                        tb2.Rows.Add(r);
                        grid_Qualification_desire_view();
                    }

                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            else if (e.CommandName == "Cancel")
            {
                //fillframe();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    protected void ddl_SelectedIndexFooterChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        DropDownList dlle = Row.FindControl("DropDownList_yearf") as DropDownList;
        DropDownList dlls = Row.FindControl("DropDownList_qf") as DropDownList;
        DropDownList ddl_stand = Row.FindControl("ddlstandf") as DropDownList;


        //TextBox txt_qulie = Row.FindControl("txt_qulif") as TextBox;
        TextBox txt_pere = Row.FindControl("txt_perf") as TextBox;
        TextBox txt_ex_bodye = Row.FindControl("txt_ex_bodyf") as TextBox;
        DropDownList ddl_state = Row.FindControl("DropDownList_edu_statef") as DropDownList;
        DropDownList ddl_month = Row.FindControl("DropDownList_monthf") as DropDownList;
        DropDownList ddl_year = Row.FindControl("DropDownList_yearf") as DropDownList;

        string stnd = dll.SelectedValue;
        populateEdu(dlls, stnd);
        populate_year(dlle, stnd);
        if (ddl_stand.SelectedItem.Text == "X")
        {
            string rids = Session["rid"].ToString();
            dlle.SelectedValue = rids.Substring((rids.Length - 4), 4);
            //dlle.Enabled = false;
        }
        else
        {
            dlle.Enabled = true;
        }
        if (ddl_stand.SelectedItem.ToString() == "Ex-Serviceman Degree")
        {

            tr_ex.Visible = true;

            dlle.Visible = false;
            dlls.Visible = true;

            //txt_qulie.Visible = false;
            txt_pere.Visible = false;
            txt_ex_bodye.Visible = false;
            ddl_state.Visible = false;
            ddl_month.Visible = false;
            ddl_year.Visible = false;
        }
        else
        {
            tr_ex.Visible = false;

            dlle.Visible = true;
            dlls.Visible = true;

            // txt_qulie.Visible = true;
            txt_pere.Visible = true;
            txt_ex_bodye.Visible = true;
            ddl_state.Visible = true;
            ddl_month.Visible = true;
            ddl_year.Visible = true;
        }

    }

    protected void ddl_SelectedIndexEditChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;

        DropDownList dlle = Row.FindControl("DropDownList_year") as DropDownList;
        DropDownList dlls = Row.FindControl("DropDownList_q") as DropDownList;
        DropDownList ddl_stand = Row.FindControl("ddlstand") as DropDownList;

        // TextBox txt_qulie = Row.FindControl("txt_quli") as TextBox;
        TextBox txt_pere = Row.FindControl("txt_per") as TextBox;
        TextBox txt_ex_bodye = Row.FindControl("txt_ex_body") as TextBox;
        DropDownList ddl_state = Row.FindControl("DropDownList_edu_state") as DropDownList;
        DropDownList ddl_month = Row.FindControl("DropDownList_month") as DropDownList;
        DropDownList ddl_year = Row.FindControl("DropDownList_year") as DropDownList;


        string stnd = dll.SelectedValue;

        populateEdu(dlls, stnd);
        populate_year(dlle, stnd);
        if (ddl_stand.SelectedItem.Text == "X")
        {
            string rids = Session["rid"].ToString();
            dlle.SelectedValue = rids.Substring((rids.Length - 4), 4);
            // dlle.Enabled = false;
        }
        else
        {
            dlle.Enabled = true;
        }
        if (ddl_stand.SelectedItem.ToString() == "Ex-Serviceman Degree")
        {

            tr_ex.Visible = true;


            dlle.Visible = false;
            dlls.Visible = true;

            //txt_qulie.Visible = false;
            txt_pere.Visible = false;
            txt_ex_bodye.Visible = false;
            ddl_state.Visible = false;
            ddl_month.Visible = false;
            ddl_year.Visible = false;
        }
        else
        {
            tr_ex.Visible = false;


            dlle.Visible = true;
            dlls.Visible = true;

            //txt_qulie.Visible = true;
            txt_pere.Visible = true;
            txt_ex_bodye.Visible = true;
            ddl_state.Visible = true;
            ddl_month.Visible = true;
            ddl_year.Visible = true;
        }

    }

    protected void ddl_SelectedIndexFooterChangedE(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        DropDownList dlle = Row.FindControl("DropDownList_yeare") as DropDownList;
        DropDownList dlls = Row.FindControl("DropDownList_qe") as DropDownList;
        DropDownList ddl_stand = Row.FindControl("ddlstande") as DropDownList;

        TextBox txt_qulie = Row.FindControl("txt_qulie") as TextBox;
        TextBox txt_pere = Row.FindControl("txt_pere") as TextBox;
        TextBox txt_ex_bodye = Row.FindControl("txt_ex_bodye") as TextBox;
        DropDownList ddl_state = Row.FindControl("DropDownList_edu_statee") as DropDownList;
        DropDownList ddl_month = Row.FindControl("DropDownList_monthe") as DropDownList;
        DropDownList ddl_year = Row.FindControl("DropDownList_yeare") as DropDownList;

        string stnd = dll.SelectedValue;

        populateEdu(dlls, stnd);
        populate_year(dlle, stnd);
        if (ddl_stand.SelectedItem.Text == "X")
        {
            string rids = Session["rid"].ToString();
            dlle.SelectedValue = rids.Substring((rids.Length - 4), 4);
            // dlle.Enabled = false;
        }
        else
        {
            dlle.Enabled = true;
        }
        if (ddl_stand.SelectedItem.ToString() == "Ex-Serviceman Degree")
        {
            tr_ex.Visible = true;
            dlle.Visible = false;
            dlls.Visible = true;
            txt_qulie.Visible = false;
            txt_pere.Visible = false;
            txt_ex_bodye.Visible = false;
            ddl_state.Visible = false;
            ddl_month.Visible = false;
            ddl_year.Visible = false;
        }
        else
        {
            tr_ex.Visible = false;
            dlle.Visible = true;
            dlls.Visible = true;
            txt_qulie.Visible = true;
            txt_pere.Visible = true;
            txt_ex_bodye.Visible = true;
            ddl_state.Visible = true;
            ddl_month.Visible = true;
            ddl_year.Visible = true;
        }
    }
    protected void ddl_desire_SelectedIndexFooterChangedE(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        DropDownList dlle = Row.FindControl("DropDownList_yeare") as DropDownList;
        DropDownList dlls = Row.FindControl("DropDownList_qe") as DropDownList;
        DropDownList ddl_stand = Row.FindControl("ddlstande") as DropDownList;
        string stnd = dll.SelectedValue;

        populateEdu_desire(dlls, stnd);
        populate_year(dlle, stnd);
        if (ddl_stand.SelectedItem.Text == "X")
        {
            string rids = Session["rid"].ToString();
            dlle.SelectedValue = rids.Substring((rids.Length - 4), 4);
            // dlle.Enabled = false;
        }
        else
        {
            dlle.Enabled = true;
        }
    }

    protected void ddlquli_SelectedIndexFooterChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        ListItem quli = dll.SelectedItem;
        string qulioth = quli.ToString();
        if (qulioth == "Others")
        {
            gvquali.Columns[3].Visible = true;

        }
    }
    protected void ddlquli_desire_SelectedIndexFooterChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        ListItem quli = dll.SelectedItem;
        string qulioth = quli.ToString();
        if (qulioth == "Others")
        {
            gvquali_desire.Columns[3].Visible = true;
        }
    }

    protected void ddlquli_SelectedIndexEditChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        ListItem quli = dll.SelectedItem;
        string qulioth = quli.ToString();
        if (qulioth == "Others")
        {
            gvquali.Columns[3].Visible = true;
        }
    }
    protected void ddlquli_desire_SelectedIndexEditChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        ListItem quli = dll.SelectedItem;
        string qulioth = quli.ToString();
        if (qulioth == "Others")
        {
            gvquali_desire.Columns[3].Visible = true;
        }
    }


    protected void ddlquli_SelectedIndexFooterChangedE(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        ListItem quli = dll.SelectedItem;
        string qulioth = quli.ToString();

        if (qulioth == "Others")
        {
            TextBox tdquli = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_qulie"));
            tdquli.Visible = true;
            Label tble_quli = (Label)(gvquali.Controls[0].Controls[0].FindControl("tble_quli"));
            tble_quli.Visible = true;
        }
        else
        {
            TextBox tdquli = (TextBox)(gvquali.Controls[0].Controls[0].FindControl("txt_qulie"));
            tdquli.Visible = false;
            Label tble_quli = (Label)(gvquali.Controls[0].Controls[0].FindControl("tble_quli"));
            tble_quli.Visible = false;
        }
    }
    protected void ddlquli_desire_SelectedIndexFooterChangedE(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        ListItem quli = dll.SelectedItem;
        string qulioth = quli.ToString();
        if (qulioth == "Others")
        {
            TextBox tdquli = (TextBox)(gvquali_desire.Controls[0].Controls[0].FindControl("txt_qulie"));
            tdquli.Visible = true;
            Label tble_quli = (Label)(gvquali_desire.Controls[0].Controls[0].FindControl("tble_quli"));
            tble_quli.Visible = true;
        }
        else
        {
            TextBox tdquli = (TextBox)(gvquali_desire.Controls[0].Controls[0].FindControl("txt_qulie"));
            tdquli.Visible = false;
            Label tble_quli = (Label)(gvquali_desire.Controls[0].Controls[0].FindControl("tble_quli"));
            tble_quli.Visible = false;
        }
    }
    protected void btnquali_Click(object sender, EventArgs e)
    {
        string appli_Id = "";
        if (Request.QueryString["update"] != null)
        {
            appli_Id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else if (Request.QueryString["applid"] != null)
        {
            appli_Id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            appli_Id = ddlpost.SelectedValue;
        }
        int applid = Int32.Parse(appli_Id);
        //int temp = objcd.delete_Education_full(applid, "E");

        if (insert_essential_check(applid))
        {
            gvquali.EditIndex = -1;
            DataTable getviewS = (DataTable)ViewState["quli"];
            int i = 0, k = 0;
            if (getviewS.Rows.Count > 0)
            {

                if (hfqualitype.Value == "G")
                {
                    string uidlist = "";
                    for (int b = 0; b < getviewS.Rows.Count; b++)
                    {
                        uidlist += getviewS.Rows[b]["qid"].ToString() + ",";
                    }
                    uidlist = uidlist.Remove(uidlist.Length - 1, 1);
                    List<int> grupno1 = new List<int>();
                    if (rbtquali.SelectedValue != "")
                    {
                        foreach (ListItem item1 in rbtquali.Items)
                        {
                            if (item1.Selected)
                            {
                                grupno1.Add(Convert.ToInt32(item1.Value));
                            }
                        }
                        DataTable dtgpquali = objcd.GetgroupqualiFor_CheckBox(txtjid.Text, grupno1, uidlist);
                        if (dtgpquali.Rows.Count > 0)
                        {
                            msg.Show("Please fill all the essential qualification for the group qualification you have selected");
                            return;
                        }
                    }
                    else if (hfqualitype.Value == "E")
                    {
                        string uidlist1 = "";
                        for (int b = 0; b < getviewS.Rows.Count; b++)
                        {
                            uidlist1 += getviewS.Rows[b]["qid"].ToString() + ",";
                        }
                        uidlist1 = uidlist1.Remove(uidlist1.Length - 1, 1);
                        DataTable dtgpquali1 = objcd.GetessentialqualiForCheck(txtjid.Text,  uidlist1);
                        if (dtgpquali1.Rows.Count > 0)
                        {
                            msg.Show("Please fill all the essential qualification for the qualification given above");
                            return;
                        }
                    }
                    
                    //DataTable dtgpquali = objcd.Getgroupquali(txtjid.Text, rbtquali.SelectedValue, uidlist);
                    //if (dtgpquali.Rows.Count > 0)
                    //{
                    //    msg.Show("Please fill all the essential qualification for the group qualification you have selected");
                    //    return;
                    //}

                }
                else if (hfqualitype.Value == "E")
                {
                    string uidlist1 = "";
                    for (int b = 0; b < getviewS.Rows.Count; b++)
                    {
                        uidlist1 += getviewS.Rows[b]["qid"].ToString() + ",";
                    }
                    uidlist1 = uidlist1.Remove(uidlist1.Length - 1, 1);
                    DataTable dtgpquali1 = objcd.GetessentialqualiForCheck(txtjid.Text, uidlist1);
                    if (dtgpquali1.Rows.Count > 0)
                    {
                        msg.Show("Please fill all the essential qualification for the qualification given above");
                        btnqualialt.Visible = true;
                        return;

                    }
                   

                }

                for (int j = 0; j < getviewS.Rows.Count; j++)
                {
                    try
                    {

                        string qid = "";
                        string appli_id = getviewS.Rows[j]["applid"].ToString();
                        qid = getviewS.Rows[j]["qid"].ToString();
                        if (qid == "")
                        {
                            //qid = "0";
                            msg.Show("Wrong Qualification is Entered. Please add that qualification again.");
                            continue;
                        }
                        string percent = "0";
                        string board = "";
                        string state = "";
                        string year = "0";
                        string standard = "";
                        string otherquali = "";
                        string month = "0";

                        standard = getviewS.Rows[j]["standard"].ToString();

                        if (getviewS.Rows[j]["stnd"].ToString() != "Ex-Serviceman Degree")
                        {
                            percent = getviewS.Rows[j]["Percentage"].ToString();
                            board = getviewS.Rows[j]["board"].ToString();
                            state = getviewS.Rows[j]["Stateid"].ToString();
                            year = getviewS.Rows[j]["year"].ToString();

                            otherquali = getviewS.Rows[j]["Extraquli"].ToString();
                            month = getviewS.Rows[j]["Month"].ToString();
                        }
                        if (getviewS.Rows[j]["stnd"].ToString() == "Ex-Serviceman Degree" && (txt_ex_auth.Text == "" || txt_ex_cer_no.Text == "" || txt_ex_issue_date.Text == ""))
                        {
                            msg.Show("Please fill required qualification for EX-Serviceman");
                            btnqualialt.Visible = true;
                            continue;
                        }
                        else
                        {
                            if (getviewS.Rows[j]["stnd"].ToString() != "Ex-Serviceman Degree")
                            {
                                i = objcd.InsertJobApplication_ED(appli_id, Int32.Parse(qid), float.Parse(percent), Server.HtmlEncode(board), state, year, Int32.Parse(standard), Server.HtmlEncode(otherquali), month);
                            }
                            else
                            {
                                i = objcd.InsertJobApplication_ED(appli_id, Int32.Parse(qid), float.Parse(percent), Server.HtmlEncode(board), state, year, Int32.Parse(standard), Server.HtmlEncode(otherquali), month);
                                k = objcd.InsertJobApplication_EX(appli_id, Int32.Parse(qid), txt_ex_auth.Text, txt_ex_cer_no.Text, Utility.formatDate(txt_ex_issue_date.Text));

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                if (i > 0)
                {
                    msg.Show("Data Saved...");
                    grid_Qualification();
                }
            }
            else
            {
                msg.Show("First Insert Data then Click Save");
            }
        }
    }

    protected void btnquali_desire_Click(object sender, EventArgs e)
    {
        string appli_Id = "";
        if (Request.QueryString["update"] != null)
        {
            appli_Id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else if (Request.QueryString["applid"] != null)
        {
            appli_Id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            appli_Id = ddlpost.SelectedValue;
        }
        int applid = Int32.Parse(appli_Id);
        //int temp = objcd.delete_Education_full(applid,"D");

        if (insert_desire_check(applid))
        {
            gvquali_desire.EditIndex = -1;
            DataTable getviewS = (DataTable)ViewState["quli_desire"];
            int i = 0;

            //if (gvquali_desire.Rows.Count > 0)
            //{

            for (int j = 0; j < gvquali_desire.Rows.Count; j++)
            {
                try
                {
                    string qid = "";
                    string appli_id = getviewS.Rows[j]["applid"].ToString();
                    qid = getviewS.Rows[j]["qid"].ToString();
                    if (qid == "")
                    {
                        qid = "0";
                    }
                    string percent = getviewS.Rows[j]["Percentage"].ToString();
                    string board = getviewS.Rows[j]["board"].ToString();
                    string state = getviewS.Rows[j]["Stateid"].ToString();
                    string year = getviewS.Rows[j]["year"].ToString();
                    string standard = getviewS.Rows[j]["standard"].ToString();
                    string otherquali = getviewS.Rows[j]["Extraquli"].ToString();
                    string month = getviewS.Rows[j]["Month"].ToString();
                    i = objcd.InsertJobApplication_ED(appli_id, Int32.Parse(qid), float.Parse(percent), Server.HtmlEncode(board), state, year, Int32.Parse(standard), Server.HtmlEncode(otherquali), month);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (i > 0)
            {
                msg.Show("Data Saved...");
                grid_Qualification();
            }

            btnquali_desire.Visible = false;
        }
    }

    public DataTable grid_experience()
    {
        string appli_id = "";
        if (Request.QueryString["update"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else if (Request.QueryString["applid"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            appli_id = ddlpost.SelectedValue;
        }
        dt = objcd.GetJobApplication_Exp(appli_id);
        if (dt.Rows.Count > 0)
        {
            ViewState["exp"] = dt;
            PanExperience.Visible = true;
            tr_exp_l.Visible = false;
            tr_exp.Visible = true; //Ambika 3012
        }
        else
        {
            btnexpalt.Visible = false;
            dt = (DataTable)ViewState["exp"];
        }
        gvexp.DataSource = dt;
        gvexp.DataBind();
        return dt;
    }

    public DataTable grid_experience_view()
    {
        btnexpalt.Visible = true;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["exp"];
        gvexp.DataSource = dt;
        gvexp.DataBind();
        return dt;
    }

    private void DatatableExp()
    {
        DataTable tb = new DataTable("exp");
        tb.Columns.Add(new DataColumn("Id", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("applid", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("post", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("datefrom", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("dateto", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("emp_name", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("emp_contactno", Type.GetType("System.String")));
        tb.Columns.Add(new DataColumn("emp_addr", Type.GetType("System.String")));
        ViewState["exp"] = tb;
    }


    protected void gvexp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowState != DataControlRowState.Edit) // check for RowState
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //check for RowType
            {
                string id = e.Row.Cells[0].Text; // Get the id to be deleted
                //cast the ShowDeleteButton link to linkbutton
                LinkButton lb = (LinkButton)e.Row.Cells[0].Controls[2];
                if (lb != null && lb.Text != "Cancel")
                {

                    lb.Attributes.Add("onclick", "return ConfirmOnDelete('" + id + "');");
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            TextBox txtEPoste = (TextBox)(e.Row.FindControl("txtEPoste"));
            txtEPoste.Visible = true;
            txtEPoste.Focus();
            TextBox txtDayFrome = (TextBox)(e.Row.FindControl("txtDayFrome"));
            txtDayFrome.Visible = true;
            Image imgtfrom = (Image)(e.Row.FindControl("cal_imgfrom"));
            imgtfrom.Visible = true;
            TextBox txtDayToe = (TextBox)(e.Row.FindControl("txtDayToe"));
            txtDayToe.Visible = true;
            Image imgtto = (Image)(e.Row.FindControl("cal_imgto"));
            imgtto.Visible = true;
            TextBox txtEmpNamee = (TextBox)(e.Row.FindControl("txtEmpNamee"));
            txtEmpNamee.Visible = true;
            TextBox txtEmpContacte = (TextBox)(e.Row.FindControl("txtEmpContacte"));
            txtEmpContacte.Visible = true;
            TextBox txtEmpAddre = (TextBox)(e.Row.FindControl("txtEmpAddre"));
            txtEmpAddre.Visible = true;
            LinkButton lnkAdd = (LinkButton)(e.Row.FindControl("lnkadd"));
            lnkAdd.Visible = false;
            LinkButton lnkIn = (LinkButton)(e.Row.FindControl("lnkIn"));
            lnkIn.Visible = true;
            LinkButton lnkC = (LinkButton)(e.Row.FindControl("lnkC"));
            lnkC.Visible = true;
        }

    }
    protected void gvexp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int index = e.NewEditIndex;
        gvexp.EditIndex = e.NewEditIndex;
        grid_experience_view();
    }
    protected void gvexp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string closing_date = hfcldate.Value;
        int index = e.RowIndex;
        string id = gvexp.DataKeys[index].Values["id"].ToString();
        string txtpost = ((TextBox)gvexp.Rows[index].FindControl("txtpost")).Text;
        string txtdatef = ((TextBox)gvexp.Rows[index].FindControl("txtdatef")).Text;
        string txtdateto = ((TextBox)gvexp.Rows[index].FindControl("txtdateto")).Text;
        string txtempname = ((TextBox)gvexp.Rows[index].FindControl("txtempname")).Text;
        string txtempcno = ((TextBox)gvexp.Rows[index].FindControl("txtempcno")).Text;
        string txtempadd = ((TextBox)gvexp.Rows[index].FindControl("txtempadd")).Text;
        string future_date = Utility.formatDateinDMY(System.DateTime.Now.ToString());
        try
        {
            //Regex regdate = new Regex(@"^([01]\d)/([0-3]\d)/(\d{4})$");
            Regex regdate = new Regex(@"(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d");
            Regex regcontact = new Regex(@"^[0-9]*$");
            Regex regcontactlen = new Regex(@".{7,11}.*");
            if (txtpost == "")
            {
                msg.Show("Please Enter Name of Post");
            }
            else if (txtpost.Length > 50)
            {
                msg.Show("Post lenth can not be more than 50 Character.");
            }
            else if (txtdatef == "")
            {
                msg.Show("Please Enter Date From");
            }
            else if (!regdate.IsMatch(txtdatef))
            {
                msg.Show("From Date should be in DD/MM/YYYY");
            }
            else if (txtdateto == "")
            {
                msg.Show("Please Enter Date To");
            }
            else if (!regdate.IsMatch(txtdateto))
            {
                msg.Show("To Date should be in DD/MM/YYYY");
            }
            else if (txtempname == "")
            {
                msg.Show("Please Enter Employer Name");
            }
            else if (txtempname.Length > 50)
            {
                msg.Show("Employer Name can not be more than 50 Character.");
            }
            else if (txtempcno == "")
            {
                msg.Show("Please Enter Employer Contact No");
            }
            else if (!regcontact.IsMatch(txtempcno))
            {
                msg.Show("Enter Only Numbers in EmpContact");
            }
            else if (!regcontactlen.IsMatch(txtempcno))
            {
                msg.Show("Minimum 7 digit and Maximum 11 digit numbers are allowed.");
            }
            else if (txtempadd == "")
            {
                msg.Show("Please Enter Employer Address");
            }
            else if (txtempadd.Length > 200)
            {
                msg.Show("Employer Address can not be more than 200 Character.");
            }
            else if (Validation.chkLevel(txtpost))
            {
                msg.Show("Invalid Inputs in  Name of Post");
            }
            else if (Validation.chkLevel13(txtdatef))
            {
                msg.Show("Invalid Inputs in Date From");
            }
            else if (Validation.chkLevel13(txtdateto))
            {
                msg.Show("Invalid Inputs in Date To");
            }
            else if (Utility.comparedatesDMY(txtdatef, txtdateto) > 0)
            {
                msg.Show("From Date Can not be greater then To Date");
            }
            //else if (Utility.comparedatesDMY(txtdateto, future_date) > 0)
            //{
            //    msg.Show("To Date Can not be greater then Today");
            //}
            else if (Utility.comparedatesDMY(txtdateto, closing_date) > 0)
            {
                msg.Show("To Date Can not be greater then Advt. last date");
            }
            else if (Validation.chkLevel(txtempname))
            {
                msg.Show("Invalid Inputs in Employer Name");
            }
            else if (Validation.chkLevel(txtempcno))
            {
                msg.Show("Invalid Inputs in Employer Contact No");
            }
            else if (Validation.chkescape(txtempadd))
            {
                msg.Show("Invalid Inputs in Employer Address");
            }

            else
            {
                string appli_id = "";
                if (Request.QueryString["update"] != null)
                {
                    appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                }
                else
                {
                    DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                    appli_id = ddlpost.SelectedValue;
                }

                DataTable tb2 = (DataTable)ViewState["exp"];
                DataRow r = tb2.NewRow();
                tb2.Rows.RemoveAt(e.RowIndex);
                r[0] = id;
                r[1] = appli_id;
                r[2] = txtpost;
                r[3] = txtdatef;
                r[4] = txtdateto;
                r[5] = txtempname;
                r[6] = txtempcno;
                r[7] = txtempadd;
                tb2.Rows.Add(r);
                gvexp.EditIndex = -1;
                grid_experience_view();
                //msg.Show("The Record is Updated...");
            }

        }
        catch (System.Threading.ThreadAbortException TAE)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void gvexp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvexp.EditIndex = -1;
        grid_experience_view();
    }
    protected void gvexp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string closing_date = hfcldate.Value;
        try
        {
            if (e.CommandName == "Reset")
            {
            }
            else if (e.CommandName == "Add")
            {
                LinkButton lnkAdd = (LinkButton)(gvexp.FooterRow.FindControl("lnkadd"));
                lnkAdd.Visible = false;
                LinkButton lnkIn = (LinkButton)(gvexp.FooterRow.FindControl("lnkIn"));
                lnkIn.Visible = true;
                LinkButton lnkC = (LinkButton)(gvexp.FooterRow.FindControl("lnkC"));
                lnkC.Visible = true;
                TextBox txtpostf = (TextBox)(gvexp.FooterRow.FindControl("txtpostf"));
                txtpostf.Visible = true;
                txtpostf.Focus();
                TextBox txtdatetff = (TextBox)(gvexp.FooterRow.FindControl("txtdatetff"));
                txtdatetff.Visible = true;
                Image imgfrm = (Image)(gvexp.FooterRow.FindControl("cal_imgfromf"));
                imgfrm.Visible = true;
                TextBox txtdatetof = (TextBox)(gvexp.FooterRow.FindControl("txtdatetof"));
                txtdatetof.Visible = true;
                Image imgto = (Image)(gvexp.FooterRow.FindControl("cal_imgtof"));
                imgto.Visible = true;
                TextBox txtempnamef = (TextBox)(gvexp.FooterRow.FindControl("txtempnamef"));
                txtempnamef.Visible = true;
                TextBox txtempcnof = (TextBox)(gvexp.FooterRow.FindControl("txtempcnof"));
                txtempcnof.Visible = true;
                TextBox txtempaddf = (TextBox)(gvexp.FooterRow.FindControl("txtempaddf"));
                txtempaddf.Visible = true;

            }
            else if (e.CommandName == "EAdd")
            {
                TextBox txtEPoste = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtEPoste"));
                txtEPoste.Visible = true;
                txtEPoste.Focus();
                TextBox txtDayFrome = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtDayFrome"));
                txtDayFrome.Visible = true;
                Image imgtfrom = (Image)(gvexp.Controls[0].Controls[0].FindControl("cal_imgfrom"));
                imgtfrom.Visible = true;
                TextBox txtDayToe = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtDayToe"));
                txtDayToe.Visible = true;
                Image imgtto = (Image)(gvexp.Controls[0].Controls[0].FindControl("cal_imgto"));
                imgtto.Visible = true;
                TextBox txtEmpNamee = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtEmpNamee"));
                txtEmpNamee.Visible = true;
                TextBox txtEmpContacte = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtEmpContacte"));
                txtEmpContacte.Visible = true;
                TextBox txtEmpAddre = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtEmpAddre"));
                txtEmpAddre.Visible = true;
                LinkButton lnkAdd = (LinkButton)(gvexp.Controls[0].Controls[0].FindControl("lnkadd"));
                lnkAdd.Visible = false;
                LinkButton lnkIn = (LinkButton)(gvexp.Controls[0].Controls[0].FindControl("lnkIn"));
                lnkIn.Visible = true;
                LinkButton lnkC = (LinkButton)(gvexp.Controls[0].Controls[0].FindControl("lnkC"));
                lnkC.Visible = true;
            }
            else if (e.CommandName == "EInsert")
            {
                DataTable tb2 = (DataTable)ViewState["exp"];
                TextBox txtEPoste = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtEPoste"));
                TextBox txtDayFrome = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtDayFrome"));
                TextBox txtDayToe = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtDayToe"));
                TextBox txtEmpNamee = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtEmpNamee"));
                TextBox txtEmpContacte = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtEmpContacte"));
                TextBox txtEmpAddre = (TextBox)(gvexp.Controls[0].Controls[0].FindControl("txtEmpAddre"));
                string future_date = Utility.formatDateinDMY(System.DateTime.Now.ToString());
                if (Validation.chkLevel(txtEPoste.Text) || txtEPoste.Text.Length > 50)
                {
                    msg.Show("Invalid Inputs in  Name of Post or character length is more than 50.");
                }
                else if (Validation.chkLevel13(txtDayFrome.Text))
                {
                    msg.Show("Invalid Inputs in Date From");
                }
                else if (Validation.chkLevel13(txtDayToe.Text))
                {
                    msg.Show("Invalid Inputs in Date To");
                }

                else if (Utility.comparedatesDMY(txtDayFrome.Text, txtDayToe.Text) > 0)
                {
                    msg.Show("From Date Can not be greater then To Date");
                }
                else if (Utility.comparedatesDMY(txtDayToe.Text, closing_date) > 0)
                {
                    msg.Show("To Date Can not be greater then Advt. last date");
                }
                else if (Validation.chkLevel(txtEmpNamee.Text) || txtEmpNamee.Text.Length > 50)
                {
                    msg.Show("Invalid Inputs in Employer Name or character length is greater than 50.");
                }
                else if (Validation.chkLevel(txtEmpContacte.Text) || txtEmpContacte.Text.Length > 11)
                {
                    msg.Show("Invalid Inputs in Employer Contact No or character length is greater than 11.");
                }
                else if (Validation.chkescape(txtEmpAddre.Text) || txtEmpAddre.Text.Length > 200)
                {
                    msg.Show("Invalid Inputs in Employer Address or character length is greater than 200.");
                }
                else
                {
                    string appli_id = "";
                    if (Request.QueryString["update"] != null)
                    {
                        appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                    }
                    else
                    {
                        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                        appli_id = ddlpost.SelectedValue;
                    }
                    DataRow r = tb2.NewRow();
                    r[0] = "1";
                    r[1] = appli_id;
                    r[2] = txtEPoste.Text;
                    r[3] = txtDayFrome.Text;
                    r[4] = txtDayToe.Text;
                    r[5] = txtEmpNamee.Text;
                    r[6] = txtEmpContacte.Text;
                    r[7] = txtEmpAddre.Text;
                    tb2.Rows.Add(r);
                    gvexp.EditIndex = -1;
                    grid_experience_view();
                }

            }
            else if (e.CommandName == "Insert")
            {
                TextBox txtpostf = (TextBox)(gvexp.FooterRow.FindControl("txtpostf"));
                TextBox txtdatetff = (TextBox)(gvexp.FooterRow.FindControl("txtdatetff"));
                TextBox txtdatetof = (TextBox)(gvexp.FooterRow.FindControl("txtdatetof"));
                TextBox txtempnamef = (TextBox)(gvexp.FooterRow.FindControl("txtempnamef"));
                TextBox txtempcnof = (TextBox)(gvexp.FooterRow.FindControl("txtempcnof"));
                TextBox txtempaddf = (TextBox)(gvexp.FooterRow.FindControl("txtempaddf"));
                string future_date = Utility.formatDateinDMY(System.DateTime.Now.ToString());

                if (Validation.chkLevel(txtpostf.Text) || txtpostf.Text.Length > 50)
                {
                    msg.Show("Invalid Inputs in  Name of Post or character length is greater than 50.");
                }
                else if (Validation.chkLevel13(txtdatetff.Text))
                {
                    msg.Show("Invalid Inputs in Date From");
                }
                else if (Validation.chkLevel13(txtdatetof.Text))
                {
                    msg.Show("Invalid Inputs in Date To");
                }
                else if (Utility.comparedatesDMY(txtdatetff.Text, txtdatetof.Text) > 0)
                {
                    msg.Show("From Date Can not be greater then To Date");
                }
                else if (Utility.comparedatesDMY(txtdatetof.Text, closing_date) > 0)
                {
                    msg.Show("To Date Can not be greater then Advt. last date");
                }
                else if (Validation.chkLevel(txtempnamef.Text) || txtempnamef.Text.Length > 50)
                {
                    msg.Show("Invalid Inputs in Employer Name or character length is greater than 50.");
                }
                else if (Validation.chkLevel(txtempcnof.Text) || txtempcnof.Text.Length > 50)
                {
                    msg.Show("Invalid Inputs in Employer Contact No or character length is greater than 50.");
                }
                else if (Validation.chkescape(txtempaddf.Text) || txtempaddf.Text.Length > 200)
                {
                    msg.Show("Invalid Inputs in Employer Address  or character length is greater than 200.");
                }

                else
                {
                    string appli_id = "";
                    if (Request.QueryString["update"] != null)
                    {
                        appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                    }
                    else
                    {
                        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                        appli_id = ddlpost.SelectedValue;
                    }
                    int Sno = 0;
                    DataTable tb2 = (DataTable)ViewState["exp"];
                    Sno = int.Parse(tb2.Rows[0]["Id"].ToString());
                    Sno = Sno + 1;
                    DataRow r = tb2.NewRow();
                    r[0] = Sno;
                    r[1] = appli_id;
                    r[2] = txtpostf.Text;
                    r[3] = txtdatetff.Text;
                    r[4] = txtdatetof.Text;
                    r[5] = txtempnamef.Text;
                    r[6] = txtempcnof.Text;
                    r[7] = txtempaddf.Text;
                    tb2.Rows.Add(r);
                    grid_experience_view();
                }
            }

            else if (e.CommandName == "Cancel")
            {

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }

    protected void gvexp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        string expid = gvexp.DataKeys[index].Values["id"].ToString();
        int exp_id = Int32.Parse(expid);
        if (Validation.chkLevel(expid))
        {
            msg.Show("Invalid Inputs in ID");
        }
        else
        {
            int temp = objcd.delete_JobApplication_Exp_D(exp_id);
            DataTable tb2 = (DataTable)ViewState["exp"];
            tb2.Rows.RemoveAt(e.RowIndex);
            gvexp.EditIndex = -1;
            msg.Show("Experience deleted...");
        }
        grid_experience_view();
    }

    protected void btnexp_Click(object sender, EventArgs e)
    {
        string exp_year = "0";
        if (rbtquali.SelectedValue != "")
        {
            if (hfqualitype.Value == "G")
            {
                string reqid = txtjid.Text;
                DataTable dtqexp = objcd.Getexpyears(reqid, rbtquali.SelectedValue);
                if (dtqexp.Rows.Count > 0)
                {
                    exp_year = dtqexp.Rows[0]["exp_noofyears"].ToString();
                }
            }
        }
        else
        {
            exp_year = hf_expnoofyear.Value;
        }
        int flag = 0;
        if (exp_year != "0")
        {
            double exp_noofyear = double.Parse(exp_year);
            double exp_noofdays = (exp_noofyear * 365);
            double total_exp_days = 0;
            DataTable getviewSS = (DataTable)ViewState["exp"];
            for (int j = 0; j < gvexp.Rows.Count; j++)
            {
                string DayFrome = getviewSS.Rows[j]["datefrom"].ToString();
                string DayTo = getviewSS.Rows[j]["dateto"].ToString();
                DateTime date_from = DateTime.ParseExact(DayFrome, "dd/MM/yyyy", new CultureInfo("en-US"));
                DateTime date_to = DateTime.ParseExact(DayTo, "dd/MM/yyyy", new CultureInfo("en-US"));

                TimeSpan t = (date_to - date_from);
                //double nooodays = t.TotalDays+2;
                double nooodays = t.TotalDays; //for experience dated 30jan2023

                double noOfDays = t.TotalDays;
                TimeSpan ts = date_to - date_from;
                double differnceinday = ts.TotalDays;
                //double differenceindays = differnceinday - 1;
                string days = differnceinday.ToString();

                total_exp_days += nooodays;
            }
            total_exp_days = total_exp_days + 1;//07/03/2023
            if (exp_noofdays > total_exp_days)
            {
                flag = 1;
            }
        }



        if (flag == 0)
        {
            string appli_Id = "";
            if (Request.QueryString["update"] != null)
            {
                appli_Id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            }
            else
            {
                DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
                appli_Id = ddlpost.SelectedValue;
            }
            int applid = Convert.ToInt32(appli_Id);
            int temp = objcd.delete_JobApplication_Exp_D_full(applid);
            gvexp.EditIndex = -1;
            DataTable getviewS = (DataTable)ViewState["exp"];
            int i = 0;
            if (gvexp.Rows.Count > 0)
            {

                for (int j = 0; j < gvexp.Rows.Count; j++)
                {
                    try
                    {
                        string appli_id = getviewS.Rows[j]["applid"].ToString();
                        string EPoste = getviewS.Rows[j]["post"].ToString();
                        string DayFrome = getviewS.Rows[j]["datefrom"].ToString();
                        string DayToe = getviewS.Rows[j]["dateto"].ToString();
                        string EmpNamee = getviewS.Rows[j]["emp_name"].ToString();
                        string EmpAddre = getviewS.Rows[j]["emp_addr"].ToString();
                        string EmpContacte = getviewS.Rows[j]["emp_contactno"].ToString();
                        i = objcd.InsertJobApplication_Exp_D(appli_id, EPoste, DayFrome, DayToe, EmpNamee, EmpAddre, EmpContacte);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                if (i > 0)
                {
                    msg.Show("Data Saved...");
                    grid_experience();
                }
            }
            else
            {
                msg.Show("First Insert Data then Click Save");
                return;//07/03/2023

            }
        }
        else
        {
            msg.Show("Experience is less then Requirement");
            return;//07/03/2023
        }

        //btnexpalt.Visible = true;
        btnexp.Visible = false;

    }

    protected void btnexit_Click(object sender, EventArgs e)
    {
        if (MD5Util.Decrypt(Request.QueryString["update"].ToString(), true) == "P")
        {
            String applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            string url = md5util.CreateTamperProofURL("EditApplication.aspx", null, "applid=" + MD5Util.Encrypt(applid, true));
            Response.Redirect(url);
        }
    }
    protected void btnqualialt_Click(object sender, EventArgs e)
    {
        //msg.Show("For Save Changes click on the Save Qualification Button");
        btnqualialt.Visible = false;
        //btnquali.Visible = true;
        btnquali_Click(this, new EventArgs());
    }
    protected void btnqualialt_desire_Click(object sender, EventArgs e)
    {
        //msg.Show("For Save Changes click on the Save Qualification Button");
        btnqualialt_desire.Visible = false;
        //btnquali_desire.Visible = true;
        btnquali_desire_Click(this, new EventArgs());
    }
    protected void btnexpalt_Click(object sender, EventArgs e)
    {
        //msg.Show("For Save Changes click on the Save Experience Button");
        btnexpalt.Visible = false;
        //btnexp.Visible = true;
        btnexp_Click(this, new EventArgs());
    }

    protected void img_btn_next_Click(object sender, ImageClickEventArgs e)
    {
         string url = "";
        string reqid = ""; string appli_Id = string.Empty;
        if (Request.QueryString["applid"] != null)
        {
            appli_Id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);

        }
        else
        {
           DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            appli_Id = ddlpost.SelectedValue;
        }

        if (appli_Id != "")
        {
            DataTable dtt = new DataTable();

            dtt = objcd.GetJobApplication_Education(appli_Id, "E", "G");// grid view fill Essential Educational Details
            DataTable dtreqid = objcd.Get_fill_quali_exp(appli_Id);//exp check
            if (dtreqid.Rows.Count > 0)
            {
                reqid = dtreqid.Rows[0]["reqid"].ToString();
            }

            List<int> grupno1 = new List<int>();
            if (rbtquali.SelectedValue != "")
            {
                foreach (ListItem item1 in rbtquali.Items)
                {
                    if (item1.Selected)
                    {
                        grupno1.Add(Convert.ToInt32(item1.Value));

                    }
                }
            }
            //string[] words = grupno1.sp(',',);

                    string[] the_array = new string[grupno1.Count];
                int j=0;
                foreach(var item in grupno1)
                {
                  the_array[j] = item.ToString();
                  j++;
                }
            bool radio = false;
            int k;   //dtt1 = objcd.getgroupquali(reqid);
            if (dtt.Rows.Count > 0)//dhiraj
            {
                if (dtt.Rows.Count == grupno1.Count)
                {
                    radio = true;
                }
                else if (grupno1.Count == 1 || dtt.Rows.Count==1)
                {
                    radio = true;
                }
                else
                {
                    radio = false;
                }
            }
            //else
            //{
            //    msg.Show("Plaase fill and save  essential qualification/Experience as per selected qualification category.");
            //}
            if (radio == false)
            {

                DataTable dtexpfilled = objcd.GetJobApplication_Exp(appli_Id);
                if (dtexpfilled.Rows.Count > 0 && yes.Checked == true && no.Checked == true)
                {
                    
                        url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(appli_Id, true));
                        Response.Redirect(url);
                    
                }
                else
                {
                    if (dtexpfilled.Rows.Count > 0 &&  btnqualialt.Visible==false && btnexpalt.Visible == false)
                    {
                        url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(appli_Id, true));
                        Response.Redirect(url);
                    }
                    else if (lbl_dexp.Text == "Nil" && btnqualialt.Visible == false && btnexpalt.Visible==false && dtexpfilled.Rows.Count > 0 )
                    {
                        url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(appli_Id, true));
                        Response.Redirect(url);


                    }
                    else
                    {
                        msg.Show("Plaase fill and save  essential qualification/Experience as per selected qualification category.");
                    }
                }
            }
            else
            {
                if(radio == true)
                {
                    DataTable dtexpfilled = objcd.GetJobApplication_Exp(appli_Id);
                             
                      List<int> grupno2 = new List<int>();
                      if (rbtquali.SelectedValue != "")
                      {
                          foreach (ListItem item1 in rbtquali.Items)
                          {
                              if (item1.Selected)
                              {
                                  grupno2.Add(Convert.ToInt32(item1.Value));
                              }
                          }
                          // grupno = grupno.Remove(grupno.Length - 1, 1);
                      }

                      DataTable dtqexp = objcd.GetGroupexp_ForCheckbox(reqid, grupno2);// exp check
                      int flag = 0;
                    //addition for total count of exp check
			    string noexpRR = string.Empty;
                      if (dtqexp.Rows.Count > 0)
                      {
                           noexpRR = dtqexp.Rows[0]["exp_noofyears"].ToString();
                      }
                      else
                      {
                           noexpRR = "0";
                      }
                     
                      if (noexpRR != "0")
                      {
                          double exp_noofyear = double.Parse(noexpRR);
                          double exp_noofdays = (exp_noofyear * 365);
                          double total_exp_days = 0;
                          // DataTable getviewSS = (DataTable)ViewState["exp"];
                          for (int rowj = 0; rowj < dtexpfilled.Rows.Count; rowj++)
                          {
                              string DayFrome = dtexpfilled.Rows[rowj]["datefrom"].ToString();
                              string DayTo = dtexpfilled.Rows[rowj]["dateto"].ToString();
                              DateTime date_from = DateTime.ParseExact(DayFrome, "dd/MM/yyyy", new CultureInfo("en-US"));
                              DateTime date_to = DateTime.ParseExact(DayTo, "dd/MM/yyyy", new CultureInfo("en-US"));

                              TimeSpan t = date_to - date_from;
                              double nooodays = t.TotalDays;
                              total_exp_days += nooodays;
                          }
                          total_exp_days = total_exp_days + 1;
                          if (exp_noofdays > total_exp_days)
                          {
                              flag = 1;
                          }
                      }

                   // if (dtexpfilled.Rows.Count >=dtqexp.Rows.Count)
                      if (flag == 0)
                    {
                        if (yes.Visible == true && no.Visible == true && yes.Checked != true && no.Checked != true || btnqualialt_desire.Visible==true)
                        {
                            msg.Show("Please Save desirable qualification details.");
                        }
                        else if (yes1.Visible == true && no1.Visible == true && yes1.Checked != true && no1.Checked != true || btnExpalt_desire.Visible == true) 
                        {
                            msg.Show("Please Save desirable Experience details.");
                        }
                        //else if (dtqexp.Rows.Count==0)
                        //{
                        //    msg.Show("Please Save desirable qualification details.");
                        //}
                        else
                        {
                            //DataTable dtexpfilled1 = objcd.GetJobApplication_Exp(appli_Id);
                            //if (dtexpfilled1.Rows.Count > 0)
                            //{
                            //    url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(appli_Id, true));
                            //    Response.Redirect(url);
                            //}

                            //else
                            //{
                            //    msg.Show("Plaase fill and save  essential qualification/Experience as per selected qualification category.");
                            //}
                            if (lbl_eexp.Text != "" )
                            {
                                if(lbl_eexp.Text == "Nil" && btnqualialt.Visible == false && btnexpalt.Visible == false)
                                {
                                url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(appli_Id, true));
                                Response.Redirect(url);
                                }
                                else
                                {
                                    DataTable dtexpfilled1 = objcd.GetJobApplication_Exp(appli_Id);
                                    if (dtexpfilled1.Rows.Count > 0 && btnqualialt.Visible == false && btnexpalt.Visible == false)
                                    {
                                        url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(appli_Id, true));
                                        Response.Redirect(url);
                                    }
                                    else
                                    {
                                        msg.Show("Please fill and save  essential qualification/Experience as per selected qualification category.");
                                        return;//07/03/2023
                                    }
                                }
                            }
                            
                               
                            
                            else
                            {
                                url = md5util.CreateTamperProofURL("Confirm_app.aspx", null, "applid=" + MD5Util.Encrypt(appli_Id, true));
                                Response.Redirect(url);
                                
                            }

                            
                        }
                      //  img_btn_next.PostBackUrl = url;
                    }
                    else
                    {
                        msg.Show("Please fill and save  essential qualification/Experience as per selected qualification category.");
                        return;//07/03/2023
                    }
                }  
            }          

            img_btn_next.Visible = true;            
        }
        else
        { 
           

            img_btn_next.Visible = true;
            url = "Confirm_app.aspx";
            Response.Redirect(url);

        }
           

    }
    public void FillCheckboxList_special_essential(CheckBoxList chbox_essential_special, DataTable dt, string textfield, string valuefield)
    {
        chbox_essential_special.Items.Clear();
        chbox_essential_special.DataTextField = textfield;
        chbox_essential_special.DataValueField = valuefield;
        chbox_essential_special.DataSource = dt;
        chbox_essential_special.DataBind();
    }
    public void FillCheckboxList_special_desire(CheckBoxList chbox_essential_special, DataTable dt, string textfield, string valuefield)
    {
        chbox_essential_special.Items.Clear();
        chbox_essential_special.DataTextField = textfield;
        chbox_essential_special.DataValueField = valuefield;
        chbox_essential_special.DataSource = dt;
        chbox_essential_special.DataBind();
    }
    public bool insert_essential_check(int appli_id)
    {
        int check_flag = 0;
        if (CheckBoxList_special.Items.Count == 0)
        {
            int temp = objcd.delete_Education_full(appli_id, "E");
            int temp2 = objcd.delete_Education_EX(appli_id, "E");
            return true;
        }
        if (CheckBoxList_special.Items.Count > 0)
        {
            for (int i = 0; i < CheckBoxList_special.Items.Count; i++)
            {
                if (CheckBoxList_special.Items[i].Selected)
                {
                    check_flag++;
                }
                else
                {
                    //check_flag = false;
                }
            }
        }
        if (check_flag == CheckBoxList_special.Items.Count)
        {
            int temp = objcd.delete_Education_full(appli_id, "E");
            try
            {
                for (int i = 0; i < CheckBoxList_special.Items.Count; i++)
                {
                    objcd.InsertJobApplication_ED_special(appli_id.ToString(), Int32.Parse(CheckBoxList_special.Items[i].Value), 7);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        else
        {
            msg.Show("Plaase select checklist of essential qualification.");
            return false;
        }
    }
    public bool insert_desire_check(int appli_id)
    {
        int check_flag = 0;
        int j = 0;
        if (yes.Checked == false && no.Checked == false)
        {
            int temp = objcd.delete_desire_check(appli_id);
            return true;
        }

        else if (yes.Checked == true || no.Checked == true)
        {
            int i = 0;
            if (yes.Checked == true)
            {
                i = 1;
            }

            else if (no.Checked == true)
            {
                i = 2;
            }
            j = objcd.updateJobApplication_ED_special(appli_id.ToString(), i, lbl_dquli.Text);
        }
        else if (CheckBoxList_desire.Items.Count == 0)
        {
            int temp = objcd.delete_Education_full(appli_id, "D");
            return true;
        }
        
       // objcd.delete_Education_full(appli_id, "D");
        if (CheckBoxList_desire.Items.Count > 0)
        {
            for (int i = 0; i < CheckBoxList_desire.Items.Count; i++)
            {
                if (CheckBoxList_desire.Items[i].Selected)
                {
                    j = objcd.InsertJobApplication_ED_special(appli_id.ToString(), Int32.Parse(CheckBoxList_desire.Items[i].Value), 7);
                }
                else
                {
                    //check_flag = false;
                }
            }
        }
        


        if (j > 0)
        {
            msg.Show("Your data have saved..");
        }




        return true;

    }

    public bool insert_desireexp_check(int appli_id)
    {
        int check_flag = 0;
        int j = 0;
        if (yes1.Checked == false && no1.Checked == false)
        {
            int temp = objcd.delete_desire_check(appli_id);
            return true;
        }

        else if (yes1.Checked == true || no1.Checked == true)
        {
            int i = 0;
            if (yes1.Checked == true)
            {
                i = 1;
            }

            else if (no1.Checked == true)
            {
                i = 2;
            }
            j = objcd.updateJobApplication_ExpD_special(appli_id.ToString(), i, lbl_dexp.Text);
        }
        //else if (CheckBoxList_desire.Items.Count == 0)
        //{
        //    int temp = objcd.delete_Education_full(appli_id, "D");
        //    return true;
        //}

        // objcd.delete_Education_full(appli_id, "D");
        //if (CheckBoxList_desire.Items.Count > 0)
        //{
        //    for (int i = 0; i < CheckBoxList_desire.Items.Count; i++)
        //    {
        //        if (CheckBoxList_desire.Items[i].Selected)
        //        {
        //            j = objcd.InsertJobApplication_ED_special(appli_id.ToString(), Int32.Parse(CheckBoxList_desire.Items[i].Value), 7);
        //        }
        //        else
        //        {
        //            //check_flag = false;
        //        }
        //    }
        //}



        if (j > 0)
        {
            msg.Show("Your data have saved..");
        }




        return true;

    }
    public void FillCheckboxList_saved_special_essential(DataTable dt)
    {

        DataRow[] r = dt.Select("standard = 7");

        for (int i = 0; i < r.Length; i++)
        {
            for (int j = 0; j < CheckBoxList_special.Items.Count; j++)
            {
                if (r[i]["qid"].ToString() == CheckBoxList_special.Items[j].Value)
                {
                    CheckBoxList_special.Items[j].Selected = true;
                }
            }
        }
    }
    public void FillCheckboxList_saved_special_desire(DataTable dt)
    {

        DataRow[] r = dt.Select("standard = 7");

        for (int i = 0; i < r.Length; i++)
        {
            for (int j = 0; j < CheckBoxList_desire.Items.Count; j++)
            {
                if (r[i]["qid"].ToString() == CheckBoxList_desire.Items[j].Value)
                {
                    CheckBoxList_desire.Items[j].Selected = true;
                }
            }
        }
    }
    protected void CheckBoxList_special_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnqualialt.Visible = true;
    }

    protected void CheckBoxList_desire_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnqualialt_desire.Visible = true;
    }

    //public int getuid(int reqid, string groupno, int qmid)
    //{
    //    int uid = 0;
    //    DataTable dtquali = objcd.GetEducationMinimumClass(reqid.ToString(), "", "");
    //    if (dtquali.Rows.Count > 0)
    //    {
    //        DataRow[] dr = dtquali.Select("reqid ='" + reqid + "' and groupno= '" + groupno + "' and id='" + qmid + "'");
    //        if (dr.Length > 0)
    //        {
    //            uid = Convert.ToInt32(dr[0]["uid"]);
    //        }
    //    }
    //    return uid;
    //}
    private void fillrbtquali(string appli_id)
    {
        try
        {
            string reqid = "";
            DataTable dtreqid = objcd.Get_fill_quali_exp(appli_id);
            if (dtreqid.Rows.Count > 0)
            {
                reqid = dtreqid.Rows[0]["reqid"].ToString();
            }
            DataTable dt = new DataTable();
            dt = objcd.getgroupquali(reqid);
            int groupno = 0;
            string qualiname = "";
            DataTable dtFinal = dt.Clone();

            for (int a = 0; a < dt.Rows.Count; a++)
            {
                //qualiname=dt.Rows[a]["name"].ToString();
                if (groupno == Convert.ToInt32(dt.Rows[a]["groupno"]))
                {
                    qualiname += " + " + dt.Rows[a]["name"].ToString();
                    dtFinal.Rows[dtFinal.Rows.Count - 1]["name"] = qualiname;
                }
                else
                {
                    qualiname = dt.Rows[a]["name"].ToString();
                    dtFinal.ImportRow(dt.Rows[a]);
                }
                groupno = Convert.ToInt32(dt.Rows[a]["groupno"]);
            }
            rbtquali.Items.Clear();
            rbtquali.DataTextField = "name";
            rbtquali.DataValueField = "groupno";
            rbtquali.DataSource = dtFinal;
            rbtquali.DataBind();
            if (dtFinal.Rows.Count > 0)
            {
                rbtquali.Visible = true;
                trselectqcat.Visible = true;
                hfqualitype.Value = "G";
            }
            else
            {
                rbtquali.Visible = false;
                trselectqcat.Visible = false;
                hfqualitype.Value = "E";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rbtquali_SelectedIndexChanged(object sender, EventArgs e)
    {
        string appli_id = "";
        if (Request.QueryString["update"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else if (Request.QueryString["applid"] != null)
        {
            appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            appli_id = ddlpost.SelectedValue;
        }
        if (rbtquali.SelectedValue != "")
        {
            int temp = objcd.delete_Education_full(Int32.Parse(appli_id), "E");
            int temp2 = objcd.delete_Education_EX(Int32.Parse(appli_id), "E");

            int temp3 = objcd.delete_JobApplication_Exp_D_full(Int32.Parse(appli_id));
            PanExperience.Visible = true; // ambika 30122021 --- should also delete experience
            pnlquali.Visible = true;

            //List<ListItem> selection = new List<ListItem>();
            // StringBuilder sb = new StringBuilder();
            //foreach (ListItem li in rbtquali.Items)
            //{
            //    if (li.Selected)
            //    {
            //       // selection.Add(li.Text);
            //        //string ch = li.Value;


            //    }
            //}
            //Session["checked"] = selection.ToString();

            int count = rbtquali.Items.Count;
            List<string> values = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (rbtquali.Items[i].Selected)
                {
                    values.Add(rbtquali.Items[i].Text);
                }
            }
            Session["checked"] = rbtquali.SelectedItem.Text;
          

            // Session["checked"] = rbtquali.SelectedItem;

            //StringBuilder sb = new StringBuilder();
            //foreach (ListItem i in rbtquali.Items)
            // {
            //    sb.Append(i.Text); 
            //    sb.Append("|");
            //}
            //Session["checked"] = sb.ToString();


        }
        else
        {
            PanExperience.Visible = false;
            pnlquali.Visible = false;
        }
        fill_grid_data(appli_id);
        fill_edu_essential_special(CheckBoxList_special);
        fill_edu_desire_special(CheckBoxList_desire);
    }

    private void populateBoardUniversity(DropDownList ddlboardUni, int stateID)
    {
        try
        {
            //DropDownList ddlstateid = (DropDownList)this.ddl_applid.FindControl("DropDownList_edu_state");
            //stateID = Convert.ToInt32(ddlstateid.SelectedValue);

            DataTable dtbord = new DataTable();
            if (stateID != null && stateID != 0)
            {
                dtbord = objcd.get_boardUniversityList(stateID);
                // dtbord.("Others",typeof(int)).SetOrdinal(0);
                DataRow dr = dtbord.NewRow();
                //dr[0].Value = "Others";
                //dtbord.Rows.Add(0,"Others");
                int rowcount = dtbord.Rows.Count + 1;

                dtbord.Rows.Add(0, "Others");
                //dtbord.Rows[rowcount]["boardUnivId"] = 0;
                //dtbord.Rows[rowcount]["boardUnivName"] = "Others";
                // dtbord.Rows.Add(dr);
                FillDropDown(ddlboardUni, dtbord, "boardUnivName", "boardUnivId");
                // dt = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DropDownList_edu_statef_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        DropDownList dlls = Row.FindControl("DropDownList_edu_statef") as DropDownList;
        DropDownList ddlboradedu = Row.FindControl("ddlBoardUnivf") as DropDownList;
        int stnd = Convert.ToInt32(dll.SelectedValue);
        populateBoardUniversity(ddlboradedu, stnd);
        TextBox txtboard = (TextBox)(gvquali.FooterRow.FindControl("txt_ex_bodyf"));
        txtboard.Visible = false;
    }
    protected void ddlBoardUnivf_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;

        DropDownList ddlboradedu = Row.FindControl("ddlBoardUnivf") as DropDownList;
        TextBox txtboard = (TextBox)(gvquali.FooterRow.FindControl("txt_ex_bodyf"));
        if (ddlboradedu.SelectedValue == "0")
        {
            txtboard.Visible = true;
            //ddlboradedu.Visible = false;
        }
        else
        {
            txtboard.Visible = false;
            //ddlboradedu.Visible = true;
        }
    }


    protected void DropDownList_edu_state_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        DropDownList dlls = Row.FindControl("DropDownList_edu_state") as DropDownList;
        DropDownList ddlboradedu = Row.FindControl("ddlBoardUniv") as DropDownList;
        TextBox txttxt_ex_body = Row.FindControl("txt_ex_body") as TextBox;
        int stnd = Convert.ToInt32(dll.SelectedValue);
        populateBoardUniversity(ddlboradedu, stnd);
        txttxt_ex_body.Text = "";
    }
    protected void ddlBoardUniv_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;

        DropDownList ddlboradedu = Row.FindControl("ddlBoardUniv") as DropDownList;
        TextBox txttxt_ex_body = Row.FindControl("txt_ex_body") as TextBox;
        //TextBox txtboard = (TextBox)(gvquali.FooterRow.FindControl("txt_ex_body"));
        if (ddlboradedu.SelectedValue == "0")
        {
            // txtboard.Visible = true;
            //ddlboradedu.Visible = false;
            txttxt_ex_body.Text = "";
        }
        else
        {
            //txtboard.Visible = false;
            txttxt_ex_body.Text = ddlboradedu.SelectedItem.Text;
        }
    }
    protected void DropDownList_edu_statee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        DropDownList dlls = Row.FindControl("DropDownList_edu_statee") as DropDownList;
        DropDownList ddlboradedu = Row.FindControl("ddl_edu_boradee") as DropDownList;
        TextBox txttxt_ex_body = Row.FindControl("txt_ex_bodye") as TextBox;
        int stnd = Convert.ToInt32(dll.SelectedValue);
        populateBoardUniversity(ddlboradedu, stnd);
        txttxt_ex_body.Text = "";
    }
    protected void ddl_edu_boradee_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;

        DropDownList ddlboradedu = Row.FindControl("ddl_edu_boradee") as DropDownList;
        TextBox txttxt_ex_bodye = Row.FindControl("txt_ex_bodye") as TextBox;
        //TextBox txtboard = (TextBox)(gvquali.FooterRow.FindControl("txt_ex_body"));
        if (ddlboradedu.SelectedValue == "0")
        {
            // txtboard.Visible = true;
            //ddlboradedu.Visible = false;
            txttxt_ex_bodye.Text = "";
        }
        else
        {
            //txtboard.Visible = false;
            txttxt_ex_bodye.Text = ddlboradedu.SelectedItem.Text;
        }
    }

    protected void img_btn_prev_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["applid"] != null)
        {
            string Applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            Response.Redirect(md5util.CreateTamperProofURL("jobupload.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(Applid, true)));
        }
        DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
        string appli_Id = ddlpost.SelectedValue;
        if (appli_Id != "")
        {
            Response.Redirect(md5util.CreateTamperProofURL("jobupload.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(appli_Id, true)));
        }
        if (appli_Id == "")
        {
            Response.Redirect("jobupload.aspx");
        }

    }
    protected void DropDownList_edu_statee_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList dll = sender as DropDownList;
        GridViewRow Row = dll.NamingContainer as GridViewRow;
        DropDownList dlls = Row.FindControl("DropDownList_edu_statee") as DropDownList;
        DropDownList ddlboradedu = Row.FindControl("ddledu_Uniboardee") as DropDownList;
        TextBox txttxt_ex_body = Row.FindControl("txt_ex_bodye") as TextBox;
        int stnd = Convert.ToInt32(dll.SelectedValue);
        populateBoardUniversity(ddlboradedu, stnd);
        txttxt_ex_body.Text = "";
    }


    protected void yes_CheckedChanged(object sender, EventArgs e)
    {
        btnqualialt_desire.Visible = true;
    }
    protected void no_CheckedChanged(object sender, EventArgs e)
    {
        btnqualialt_desire.Visible = true;
    }
    protected void yes_CheckedChanged1(object sender, EventArgs e)
    {
        btnExpalt_desire.Visible = true;
    }
    protected void no_CheckedChanged1(object sender, EventArgs e)
    {
        btnExpalt_desire.Visible = true;
    }
    protected void btnExpalt_desire_Click(object sender, EventArgs e)
    {
        //msg.Show("For Save Changes click on the Save Qualification Button");
        btnExpalt_desire.Visible = false;
        //btnquali_desire.Visible = true;
        btnExp_desire_Click(this, new EventArgs());

    }
    protected void btnExp_desire_Click(object sender, EventArgs e)
    {
        string appli_Id = "";
        if (Request.QueryString["update"] != null)
        {
            appli_Id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else if (Request.QueryString["applid"] != null)
        {
            appli_Id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        else
        {
            DropDownList ddlpost = (DropDownList)this.ddl_applid.FindControl("DropDownList_post");
            appli_Id = ddlpost.SelectedValue;
        }
        int applid = Int32.Parse(appli_Id);
        //int temp = objcd.delete_Education_full(applid,"D");

        if (insert_desireexp_check(applid))
        {
            gvquali_desire.EditIndex = -1;
            DataTable getviewS = (DataTable)ViewState["quli_desire"];
            int i = 0;

            //if (gvquali_desire.Rows.Count > 0)
            //{

            for (int j = 0; j < gvquali_desire.Rows.Count; j++)
            {
                try
                {
                    string qid = "";
                    string appli_id = getviewS.Rows[j]["applid"].ToString();
                    qid = getviewS.Rows[j]["qid"].ToString();
                    if (qid == "")
                    {
                        qid = "0";
                    }
                    string percent = getviewS.Rows[j]["Percentage"].ToString();
                    string board = getviewS.Rows[j]["board"].ToString();
                    string state = getviewS.Rows[j]["Stateid"].ToString();
                    string year = getviewS.Rows[j]["year"].ToString();
                    string standard = getviewS.Rows[j]["standard"].ToString();
                    string otherquali = getviewS.Rows[j]["Extraquli"].ToString();
                    string month = getviewS.Rows[j]["Month"].ToString();
                    i = objcd.InsertJobApplication_ED(appli_id, Int32.Parse(qid), float.Parse(percent), Server.HtmlEncode(board), state, year, Int32.Parse(standard), Server.HtmlEncode(otherquali), month);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (i > 0)
            {
                msg.Show("Data Saved...");
                grid_Qualification();
            }

            btnquali_desire.Visible = false;
        }
    }

   
}
