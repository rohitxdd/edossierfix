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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;

public partial class CombdEduExp : BasePage
{
    CandidateData objCandD = new CandidateData();
    MD5Util md5util = new MD5Util();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    message msg = new message();
    DataTable dt = new DataTable();
    string applid;
    int maxage = 0, minage = 0;
    DataTable dt_age_relax;
    string Deptreqid;
    string Combdreqid;
    string category;
    string SubCategory;
    string Candgender;
    int CandAge = 0;
    int otherage = 0;
    DateTime dob;
    string OBCRegion;
    List<string> subcatlist = new List<string>();
    List<int> relaxedage = new List<int>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            string appli_id = string.Empty;

            if (Request.QueryString["applid"] != null)
            {
                try
                {
                    appli_id = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                    if (objCandD.isApplicationFinal(appli_id))
                    {
                        Response.Redirect("updatemobile.aspx", false); //Application is final
                        return;
                    }
                }
                catch
                {
                    Response.Redirect("updatemobile.aspx", false); //invalid encrypted string
                    return;
                }
            }
            //end
            DataTable dtreqid = objCandD.Get_fill_combdreqid(applid);
            if (dtreqid.Rows.Count > 0)
            {
                Combdreqid = dtreqid.Rows[0]["reqid"].ToString();
            }
            DataTable dt1 = objCandD.getcanddetail(applid);
            DataTable getsubcat = objCandD.getcandsubcatdetail(applid);

            //Devesh added Cut_OffDate
            //DateTime.ParseExact(chngpwd_date, "dd/MM/yyyy", null);
            string Cut_OffDateStr = dtreqid.Rows[0]["Cut_OffDate"].ToString();
            if (dt1.Rows.Count > 0)
            {
                string Dob = dt1.Rows[0]["birthdt"].ToString();
                category = dt1.Rows[0]["category"].ToString();
                // SubCategory = dt1.Rows[0]["SubCat_code"].ToString();
                if (getsubcat.Rows.Count > 0)
                {
                    for (int i = 0; i < getsubcat.Rows.Count; i++)
                    {
                        SubCategory = getsubcat.Rows[i]["SubCat_code"].ToString();
                        subcatlist.Add(SubCategory);
                    }
                }
                Candgender = dt1.Rows[0]["gender"].ToString();
                if (category == "OBC")
                {
                    OBCRegion = dt1.Rows[0]["OBCRegion"].ToString();
                }
                dob = DateTime.Parse(Dob);

                // Devesh changed DateTime.Now => Cut_OffDate
                if (!string.IsNullOrEmpty(Cut_OffDateStr))
                {
                    DateTime CutoffDate = DateTime.ParseExact(Cut_OffDateStr, "dd/MM/yyyy", null);
                    CandAge = CutoffDate.AddYears(-dob.Year).Year;

                }
                else
                {
                    CandAge = DateTime.Now.AddYears(-dob.Year).Year;
                }
            }
            //////////////////////age relax//////////// 
            // Loop through each row in the GridView
            if (!IsPostBack)
            {
                UniqueRandomNumber = randObj.Next(1, 10000);
                Session["token"] = UniqueRandomNumber.ToString();
                this.csrftoken.Value = Session["token"].ToString();
                //CAND DETAIL
                fillgrid();
                DataTable ddt = objCandD.check_detail(applid);
                if (ddt.Rows.Count > 0)
                {
                    for (int i = 0; i < grd_Dept.Rows.Count; i++)
                    {
                        CheckBox chk_select = (CheckBox)grd_Dept.Rows[i].FindControl("chk_select");
                        Label lblreqid = grd_Dept.Rows[i].FindControl("lblreqid") as Label;
                        Deptreqid = lblreqid.Text.ToString();
                        for (int j = 0; j < ddt.Rows.Count; j++)
                        {
                            string reqid1 = ddt.Rows[j]["DeptReqId"].ToString();
                            if (Deptreqid == reqid1)
                            {
                                chk_select.Checked = true;
                            }

                        }

                    }
                    btn_save.Visible = false;
                    Btn_next.Visible = true;

                }
                else
                {
                    btn_save.Visible = true;
                    Btn_next.Visible = false;
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
        }
        catch (Exception ex)
        {

            Response.Redirect("ErrorPage.aspx");
        }
    }
    private void fillgrid()
    {
        // Combdreqid = Session["reqid"].ToString();
        DataTable dt = objCandD.get_Deptname(Combdreqid);
        grd_Dept.DataSource = dt;
        grd_Dept.DataBind();
    }

    public void YR_Age(string cat_code, string CatIndS)
    {
        try
        {
            int v_dYear = 0;
            DataRow[] dt_row = dt_age_relax.Select("CatCode='" + cat_code + "' AND CatIndCS='" + CatIndS + "'");
            foreach (DataRow r in dt_row)
            {
                v_dYear = Int32.Parse(r["D_Year"].ToString());
                if (CatIndS == "C" && r["CM"].ToString() == "M")
                {
                    if (cat_code == "SC" || cat_code == "ST" || (cat_code == "OBC" && OBCRegion == "D"))
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
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            string confirmValue = Request.Form["confirm_value"];
            for (int i = 0; i < grd_Dept.Rows.Count; i++)
            // foreach (GridViewRow row in grd_Dept.Rows)
            {
                CheckBox chk_select = (CheckBox)grd_Dept.Rows[i].FindControl("chk_select");
                Label lbldeptname = grd_Dept.Rows[i].FindControl("lbldeptname") as Label;
                string DeptName = lbldeptname.Text.ToString();
                Label lblreqid = grd_Dept.Rows[i].FindControl("lblreqid") as Label;
                Deptreqid = lblreqid.Text.ToString();
                Label lbldeptcode = grd_Dept.Rows[i].FindControl("lbldeptcode") as Label;
                string DeptCode = lbldeptcode.Text.ToString();
                if (chk_select.Checked == true)
                {
                    int appid = objCandD.Insert_deptdetails(applid, Combdreqid, Deptreqid, DeptName, DeptCode);
                    if (appid > 0)
                    {
                        flag = true;

                    }
                }
                else
                {
                    int del_edu = objCandD.delete_edu(applid, Deptreqid);
                    int del_desireexp = objCandD.delete_desireexp(applid, Deptreqid);
                }
            }
            if (flag == true)
            {
                string url = "";
                url = md5util.CreateTamperProofURL("CombdFillQualiExp.aspx", null, "applid=" + MD5Util.Encrypt(applid.ToString(), true));
                Response.Redirect(url, false);
                return;
            }
            else
            {
                msg.Show("Please select the department to proceed");
                return;
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("ErrorPage.aspx");
        }
    }


    protected void btn_back_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["applid"] != null)
            {
                string Applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                string url = "";
                url = md5util.CreateTamperProofURL("jobupload.aspx", null, "update=" + MD5Util.Encrypt("P", true) + "&applid=" + MD5Util.Encrypt(Applid, true));
                Response.Redirect(url);
                if (Applid == "")
                {
                    Response.Redirect("jobupload.aspx");
                }
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("ErrorPage.aspx");
        }
    }

    protected void Btn_next_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            string confirmValue = Request.Form["confirm_value"];
            DataTable ddt = objCandD.check_detail(applid);
            //if (ddt.Rows.Count > 0)
            //{
            //    objCandD.DeleteOnUpdateCombd(applid);
            //}
            for (int i = 0; i < grd_Dept.Rows.Count; i++)
            // foreach (GridViewRow row in grd_Dept.Rows)
            {
                CheckBox chk_select = (CheckBox)grd_Dept.Rows[i].FindControl("chk_select");
                Label lbldeptname1 = grd_Dept.Rows[i].FindControl("lbldeptname") as Label;
                string DeptName = lbldeptname1.Text.ToString();
                Label lblreqid = grd_Dept.Rows[i].FindControl("lblreqid") as Label;
                Deptreqid = lblreqid.Text.ToString();
                Label lbldeptcode = grd_Dept.Rows[i].FindControl("lbldeptcode") as Label;
                string DeptCode = lbldeptcode.Text.ToString();

                if (chk_select.Checked == true)
                {
                    var con = "DeptReqId = " + Deptreqid;
                    var drr = ddt.Select(con);
                    if (drr.Length > 0)
                    {
                        //do nothing
                        flag = true;
                    }
                    else
                    {
                        int appid = objCandD.Insert_deptdetails(applid, Combdreqid, Deptreqid, DeptName, DeptCode);
                        if (appid > 0)
                        {
                            flag = true;

                        }
                    }
                }
                else
                {
                    int deptdetail = objCandD.DeleteOnUpdateCombd(applid, Deptreqid);
                    int del_edu = objCandD.delete_edu(applid, Deptreqid);
                    int del_desireexp = objCandD.delete_desireexp(applid, Deptreqid);
                }
            }
            if (flag == true)
            {
                string url = "";
                url = md5util.CreateTamperProofURL("CombdFillQualiExp.aspx", null, "applid=" + MD5Util.Encrypt(applid.ToString(), true));
                Response.Redirect(url, false);
                return;
            }
            else
            {
                msg.Show("Please select the department to proceed");
                return;
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("ErrorPage.aspx");
        }
    }
    protected void chk_select_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grd_Dept.Rows.Count; i++)
            {
                CheckBox chk_select1 = (CheckBox)grd_Dept.Rows[i].FindControl("chk_select");
                chk_select1.Text = "";
                Label lblminage = grd_Dept.Rows[i].FindControl("lblminage") as Label;
                Label lblmaxage = grd_Dept.Rows[i].FindControl("lblmaxage") as Label;
                Label lbl_gender = grd_Dept.Rows[i].FindControl("lbl_gender") as Label;
                Label lblreqid = grd_Dept.Rows[i].FindControl("lblreqid") as Label;
                Deptreqid = lblreqid.Text.ToString();
                minage = Convert.ToInt32(lblminage.Text);
                maxage = Convert.ToInt32(lblmaxage.Text);
                string gender = lbl_gender.Text.ToString();
                // Apply your condition to show/hide checkboxes
                if (chk_select1.Checked)
                {
                    if (Candgender == gender || gender == "B")
                    {
                        if(Deptreqid == "1892" && !subcatlist.Contains("PH"))
                        {
                            msg.Show("This vacancy is only reserved for UR-PwD Candidates");
                            chk_select1.Checked = false;
                            return;
                        }

                        if (Deptreqid == "1853" && (category != "OBC" || (category == "OBC" && OBCRegion != "D")))
                        {
                            msg.Show("This vacancy is only reserved for OBC Delhi Candidates");
                            chk_select1.Checked = false;
                            return;
                        }

                        dt_age_relax = objCandD.agerelax(Deptreqid);
                        DataTable vacancy = objCandD.vacancy(Deptreqid);
                        DataRow[] dt_row = vacancy.Select("CatCode = 'UR'");
                        if (dt_row.Length > 0)
                        {
                            DataRow[] dr_category = vacancy.Select("CatCode ='" + category + "'");
                            if (dr_category.Length > 0)
                            {
                                var result = AgeRelaxation(category);
                                if (!result)
                                {
                                    msg.Show("You are not eligible for this Department");
                                    chk_select1.Checked = false;
                                    return;
                                }
                            }
                            else
                            {
                                var result = AgeRelaxation("UR");
                                if (!result)
                                {
                                    msg.Show("You are not eligible for this Department");
                                    chk_select1.Checked = false;
                                    return;
                                }

                            }

                        }
                        else 
                        {
                            DataRow[] dr_category = vacancy.Select("CatCode ='" + category + "'");
                            if (dr_category.Length > 0) 
                            {
                                var result = AgeRelaxation(category);
                                if (!result)
                                {
                                    msg.Show("You are not eligible for this Department");
                                    chk_select1.Checked = false;
                                    return;
                                }
                            }
                            else
                            {
                                msg.Show("You are not eligible for this Department");
                                chk_select1.Checked = false;
                                return;
                            }
                        }
                        chk_select1.Visible = true;
                        chk_select1.Checked = true;
                    }
                    else
                    {
                        string gen_value = "";
                        if (gender == "M")
                        {
                            gen_value = "Male";
                        }
                        if (gender == "F")
                        {
                            gen_value = "Female";
                        }

                        msg.Show("This Post Only For " + gen_value + " Candidate");
                        chk_select1.Checked = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("ErrorPage.aspx");
        }

    }
    protected bool AgeRelaxation(string candCat)
    {
        relaxedage.Clear();
        YR_Age(candCat, "C");
        if (!string.IsNullOrEmpty(SubCategory))
        {
            //YR_Age(SubCategory, "S");
            foreach (var subcat in subcatlist)
            {
                YR_Age(subcat, "S");
            }
            relaxedage.Sort();
            var maximumage = relaxedage.Last();
            maxage = maximumage;
            if (CandAge > maxage)
            {
                return false;

            }
        }
        if (CandAge > maxage)
        {
            return false;
        }
        else
        {
            if (CandAge == maxage)
            {
                //DateTime DOBTO = objCandD.GetDobTofromDB(Deptreqid,minage);
                DateTime endson = objCandD.GetDobTofromDB(Deptreqid);
                DateTime validdate = endson.AddYears(-maxage);
                //DateTime MaxDate = DOBTO.AddYears(-(maxage - minage)).AddYears((DateTime.Now.Year - DOBTO.Year));
                if (validdate > dob)
                {
                    return false;

                }
            }

        }
        return true;
    }
}