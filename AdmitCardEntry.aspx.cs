using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using CaptchaDLL;

public partial class AdmitCardEntry : Page
{
    message msg = new message();
    eAdmitCard objeadmit = new eAdmitCard();
    MD5Util md5util = new MD5Util();
    string flagfromboard = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["flagfromboard"] != null)
        {
            flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
        }
        if (!IsPostBack)
        {
            Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.Numeric, 6);
            //if (Request.QueryString["flagfromboard"] != null)
            //{
            //    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
            //}
            fillrbtvalue();
            fillgrid(flagfromboard);
            fillgrid_2tier(flagfromboard);
            fillgrid_2tier_test(flagfromboard);
            fillgrid_3tier(flagfromboard);
            fillgrid_3tier_test(flagfromboard);
            fillgrid_1tier_test(flagfromboard);
            if (grid_admit.Rows.Count <= 0 && grid2tier.Rows.Count <= 0 && grid2tiertest.Rows.Count <= 0 && gvtier3exam.Rows.Count <= 0 && gvtier3test.Rows.Count <= 0 && gv1tierpet.Rows.Count <= 0)
            {
                lbl_msg.Visible = true;
            }
            else
            {
                lbl_msg.Visible = false;
                // tbl.Visible = true;
            }
            if (grid_admit.Rows.Count > 0)
            {
                rbtexamtype.Items[0].Enabled = true;
            }

            else
            {
                rbtexamtype.Items[0].Enabled = false;
            }
            if (gv1tierpet.Rows.Count > 0)
            {
                rbtexamtype.Items[1].Enabled = true;
            }

            else
            {
                rbtexamtype.Items[1].Enabled = false;
            }
            if (grid2tier.Rows.Count > 0)
            {
                rbtexamtype.Items[2].Enabled = true;
            }

            else
            {
                rbtexamtype.Items[2].Enabled = false;
            }
            if (grid2tiertest.Rows.Count > 0)
            {
                rbtexamtype.Items[3].Enabled = true;
            }
            else
            {
                rbtexamtype.Items[3].Enabled = false;
            }
            if (gvtier3exam.Rows.Count > 0)
            {
                rbtexamtype.Items[4].Enabled = true;
            }

            else
            {
                rbtexamtype.Items[4].Enabled = false;
            }
            if (gvtier3test.Rows.Count > 0)
            {
                rbtexamtype.Items[5].Enabled = true;
            }

            else
            {
                rbtexamtype.Items[5].Enabled = false;
            }
            
            if (grid_admit.Rows.Count > 0 || grid2tier.Rows.Count > 0 || grid2tiertest.Rows.Count > 0 || gvtier3exam.Rows.Count > 0 || gvtier3test.Rows.Count > 0 || gv1tierpet.Rows.Count>0)
            {
                //tbl.Visible = true;
                trprint1.Visible = true;
                trprint2.Visible = true;
                if (flagfromboard == "Y")
                {
                    chk1.Visible = false;
                    lblprovmsg.Visible = true;
                }
                else
                {
                    if (grid_admit.Rows.Count > 0 || gv1tierpet.Rows.Count > 0)
                    {
                        chk1.Visible = true;
                        lblprovmsg.Visible = false;
                        lbl_msg.Visible = false;
                    }
                    else
                    {
                        chk1.Visible = false;
                        lblprovmsg.Visible = false;
                    }
                }
            }
            else
            {
               // tbl.Visible = false;
                trprint1.Visible = false;
                trprint2.Visible = false;
                chk1.Visible = false;
                lblprovmsg.Visible = false;
            }
            if (grid_admit.Rows.Count == 0)
            {
                chk1.Checked = true;
                fillgrid(flagfromboard);
                if (grid_admit.Rows.Count > 0)
                {
                    chk1.Enabled = false;
                    chk1.Visible = true;
                   // tbl.Visible = true;
                    trprint1.Visible = true;
                    trprint2.Visible = true;
                    lbl_msg.Visible = false;
                }
                else
                {
                    chk1.Checked = false;
                    chk1.Visible = false;
                }
                chk1_CheckedChanged(sender, e);
            }
            else
            {
                chk1.Checked = false;
            }

        }
    }

    private void fillgrid(string flagfromboard )
    {
        string isprov = "";
        if (chk1.Checked)
        {
            isprov = "Y";
        }
        DataTable dt = objeadmit.get_exam_list(flagfromboard, isprov);

        grid_admit.DataSource = dt;
        grid_admit.DataBind();
        if (isprov == "Y")
        {
            grid_admit.Caption = "e-Admit Card available for the Posts for Tier-1 Exam (Only for Provisional Candidates)";
        }
        else
        {
            grid_admit.Caption = "e-Admit Card available for the Posts for Tier-1 Exam";
        }

    }
    private void fillgrid_1tier_test(string flagfromboard)
    {
        DataTable dt = objeadmit.get_exam_list_1tier_test(flagfromboard);

        gv1tierpet.DataSource = dt;
        gv1tierpet.DataBind();


    }
    private void fillgrid_2tier(string flagfromboard)
    {
        DataTable dt_2tier = objeadmit.get_exam_list_2tier(flagfromboard);

        grid2tier.DataSource = dt_2tier;
        grid2tier.DataBind();
       

    }
    private void fillgrid_2tier_test(string flagfromboard)
    {
        DataTable dt_2tier_test = objeadmit.get_exam_list_2tier_test(flagfromboard);

        grid2tiertest.DataSource = dt_2tier_test;
        grid2tiertest.DataBind();


    }
    private void fillgrid_3tier(string flagfromboard)
    {
        DataTable dt_3tier = objeadmit.get_exam_list_3tier(flagfromboard);

        gvtier3exam.DataSource = dt_3tier;
        gvtier3exam.DataBind();


    }
    private void fillgrid_3tier_test(string flagfromboard)
    {
        DataTable dt_3tier_test = objeadmit.get_exam_list_3tier_test(flagfromboard);
        for (int i = 0; i < dt_3tier_test.Rows.Count; i++)
        {
            if (dt_3tier_test.Rows[i]["examid"].ToString() == "861" || dt_3tier_test.Rows[i]["examid"].ToString() == "862")
            {
                //dt_3tier_test.Rows[i].Delete();
                //Response.Redirect("Default.aspx");
            }
        }
        gvtier3test.DataSource = dt_3tier_test;
        gvtier3test.DataBind();
        



    }
    protected void ibtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    private void clear()
    {
        Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.Numeric, 6); //generate new string
        txtCode.Text = "";
        txt_appno.Text = "";
        txt_dob.Text = "";
        txtregno.Text = "";
        txtrollno.Text = "";
        fillpostcode();
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //if (Request.QueryString["flagfromboard"] != null)
        //{
        //    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
        //}
        if (txtCode.Text != "")
        {
            if (chk1.Checked && (txtregno.Text == "" || ddlpost.SelectedValue == ""))
            {
                msg.Show("Please Enter Data");
                return;
            }
            if (!chk1.Checked && txt_appno.Text == "")
            {
                msg.Show("Please Enter Application No.");
                return;
            }
            if (Session["CaptchaImageText"] != null && txtCode.Text == Session["CaptchaImageText"].ToString())
            {
                DataTable dt;
                string batchid = objeadmit.checkwhetherexambatchwise(txt_appno.Text, Utility.formatDate(txt_dob.Text), ddlpost.SelectedValue, chk1.Checked, txtregno.Text);
                if (batchid != "")
                {
                    dt = objeadmit.getapplid_batchwise(txt_appno.Text, Utility.formatDate(txt_dob.Text), ddlpost.SelectedValue, chk1.Checked, txtregno.Text);
                }
                else
                {
                    dt = objeadmit.getapplid(txt_appno.Text, Utility.formatDate(txt_dob.Text), ddlpost.SelectedValue, chk1.Checked, txtregno.Text, flagfromboard, rbtexamtype.SelectedValue);
                }
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["newexamid"].ToString() != "0")
                    {
                        DataTable dtckreexam = objeadmit.checkrollnoforreexam(dt.Rows[0]["applid"].ToString(), dt.Rows[0]["examid"].ToString());
                        if (dtckreexam.Rows.Count== 0)
                        {
                            msg.Show("Not eligible to print e-Admit Card.");
                            Server.Transfer("AdmitCardEntry.aspx");
                        }
                    }
                    // url = md5util.CreateTamperProofURL("AdmitCardPdf.aspx", null, "applid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["applid"].ToString(), true) + "&examid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt(rbtexamtype.SelectedValue, true));
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url + "');", true);

                    clear();
                    if ((dt.Rows[0]["acconsent"].ToString() == "Y" && dt.Rows[0]["radmitcard"].ToString() == "Y" && dt.Rows[0]["acconsent_phase2"].ToString() == "") || (dt.Rows[0]["acconsent"].ToString() == "Y" && dt.Rows[0]["radmitcard2"].ToString() == "Y" && dt.Rows[0]["acconsent_phase2"].ToString() == "Y") || ((dt.Rows[0]["acstatid"].ToString() == "2" || dt.Rows[0]["acstatid"].ToString() == "5") && dt.Rows[0]["radmitcard"].ToString() == "Y") || (dt.Rows[0]["acstatid"].ToString() == "4" && dt.Rows[0]["radmitcard2"].ToString() == "Y") || (dt.Rows[0]["acconsent"].ToString() == "Y" && dt.Rows[0]["radmitcard"].ToString() == "Y" && dt.Rows[0]["batchid"].ToString() != ""))
                    {
                          string ip = GetIPAddress();
                          string url = "";
                          int temp = 0;
                          string isprov = "";
                          if (chk1.Checked)
                          {
                              isprov = "Y";
                          }
                          if (dt.Rows[0]["newexamid"].ToString() != "0")
                          {
                              temp = objeadmit.insertAdmitcarddownload(dt.Rows[0]["applid"].ToString(), rbtexamtype.SelectedValue, dt.Rows[0]["newexamid"].ToString(), ip, dt.Rows[0]["regno"].ToString());
                              url = md5util.CreateTamperProofURL("AdmitCardPdf.aspx", null, "applid=" + MD5Util.Encrypt(dt.Rows[0]["applid"].ToString(), true) + "&examid=" + MD5Util.Encrypt(dt.Rows[0]["newexamid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt(rbtexamtype.SelectedValue, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&ReExam=" + MD5Util.Encrypt("Y", true) + "&isprov=" + MD5Util.Encrypt(isprov, true));
                          }
                          else
                          {
                               temp = objeadmit.insertAdmitcarddownload(dt.Rows[0]["applid"].ToString(), rbtexamtype.SelectedValue, dt.Rows[0]["examid"].ToString(), ip, dt.Rows[0]["regno"].ToString());
                               if (batchid != "")
                               {
                                   url = md5util.CreateTamperProofURL("AdmitCardPdf.aspx", null, "applid=" + MD5Util.Encrypt(dt.Rows[0]["applid"].ToString(), true) + "&examid=" + MD5Util.Encrypt(dt.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt(rbtexamtype.SelectedValue, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&batchid=" + MD5Util.Encrypt(dt.Rows[0]["batchid"].ToString(), true) + "&isprov=" + MD5Util.Encrypt(isprov, true));
                               }
                               else
                               {
                                   url = md5util.CreateTamperProofURL("AdmitCardPdf.aspx", null, "applid=" + MD5Util.Encrypt(dt.Rows[0]["applid"].ToString(), true) + "&examid=" + MD5Util.Encrypt(dt.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt(rbtexamtype.SelectedValue, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&isprov=" + MD5Util.Encrypt(isprov, true));
                               }
                          }
                        //Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                    }
                    else
                    {
                        msg.Show("Last Date for Generation of Admit Card is Over. Since you have not generated your Admit Card, it is not available for downloading.");
                        Server.Transfer("AdmitCardEntry.aspx");
                    }
                }
                else
                {
                    clear();
                    msg.Show("Not eligible to print e-Admit Card.");
                    Server.Transfer("AdmitCardEntry.aspx");
                }

            }
            else
            {
                clear();
                msg.Show("The security code you entered is incorrect. Enter the security code as shown in the image.");
                Server.Transfer("AdmitCardEntry.aspx");
            }
        }
        else
        {
            clear();
            msg.Show("Please Enter Visual Code");
            Server.Transfer("AdmitCardEntry.aspx");
        }
    }
    protected void chk1_CheckedChanged(object sender, EventArgs e)
    {
        fillgrid(flagfromboard);
        if (chk1.Checked)
        {
            trappno.Visible = false;
            trregno.Visible = true;
            trpostcode.Visible = true;
           
        }
        else
        {
            trappno.Visible = true;
            trregno.Visible = false;
            trpostcode.Visible = false;
        }
    }
    protected void txtregno_TextChanged(object sender, EventArgs e)
    {
        fillpostcode();
    }

    private void fillpostcode()
    {
        //if (Request.QueryString["flagfromboard"] != null)
        //{
        //    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
        //}
        DataTable dt = objeadmit.getpostsforexam(txtregno.Text.Trim(), flagfromboard,rbtexamtype.SelectedValue);
        ddlpost.DataTextField = "postdesc";
        ddlpost.DataValueField = "jid";
        ddlpost.DataSource = dt;
        ddlpost.DataBind();
        if (dt.Rows.Count > 1)
        {
            ddlpost.Items.Insert(0, Utility.ddl_Select_Value());
        }
    }
    //protected void grid_admit_RowBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //    }

    //}
    //protected void grid_admit_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Click")
    //    {

    //        // Convert the row index stored in the CommandArgument
    //        // property to an Integer.
    //        int index = Convert.ToInt32(e.CommandArgument);

    //        // Get the last name of the selected author from the appropriate
    //        // cell in the GridView control.




    //        string jid = (string)grid_admit.DataKeys[index][0].ToString();


    //        ddlpost.SelectedValue = jid;
    //        tbl.Visible = true;

    //    }
    //}
    protected void rbtexamtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Request.QueryString["flagfromboard"] != null)
        //{
        //    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
        //}
        if (rbtexamtype.SelectedValue == "2")
        {
            tbltier2.Visible = true;
            lbltierrollno.Text = "Enter Tier-1 Exam Roll No.";
            tbl.Visible = false;
            chk1.Visible = false;
            //btnprintinst.Visible = false;
        }
        else if (rbtexamtype.SelectedValue == "3")
        {
            tbltier2.Visible = true;
            lbltierrollno.Text = "Enter Tier-1 Exam Roll No.";
            tbl.Visible = false;
            chk1.Visible = false;
            //btnprintinst.Visible = true;
        }
        else if (rbtexamtype.SelectedValue == "4")
        {
            tbltier2.Visible = true;
            lbltierrollno.Text = "Enter Tier-2 Exam Roll No.";
            tbl.Visible = false;
            chk1.Visible = false;
            //btnprintinst.Visible = false;
        }
        else if (rbtexamtype.SelectedValue == "5")
        {
            tbltier2.Visible = true;
            lbltierrollno.Text = "Enter Tier-2 Exam Roll No.";
            tbl.Visible = false;
            chk1.Visible = false;
            //btnprintinst.Visible = true;
        }
        else
        {
            tbltier2.Visible = false;
            tbl.Visible = true;
            if (flagfromboard == "Y")
            {
                chk1.Visible = false;
                lblprovmsg.Visible = true;
            }
            else
            {
                chk1.Visible = true;
                lblprovmsg.Visible = false;
            }
        }
        if (rbtexamtype.SelectedValue == "2" || rbtexamtype.SelectedValue == "3" || rbtexamtype.SelectedValue == "4" || rbtexamtype.SelectedValue == "5")
        {
            fillpostcode_2tier();
        }
    }
    protected void btntier2submit_Click(object sender, EventArgs e)
    {
        //if (Request.QueryString["flagfromboard"] != null)
        //{
        //    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
        //}
        if (txtrollno.Text == "")
        {
            msg.Show("Please Enter Roll No.");
            return;
        }
        else if (ddl2tierpost.SelectedValue == "")
        {
            msg.Show("Please Select Post");
            return;
        }
        else
        {
            string tier = "";
            if (rbtexamtype.SelectedValue == "2" || rbtexamtype.SelectedValue == "3")
            {
                tier = "2";
            }
            else if (rbtexamtype.SelectedValue == "4" || rbtexamtype.SelectedValue == "5")
            {
                tier = "3";
            }
            DataTable dtgetdata = new DataTable();
            if (rbtexamtype.SelectedValue == "3" || rbtexamtype.SelectedValue == "5")
            {
                dtgetdata = objeadmit.get_AdmitCard_2tier_test("", "", txtrollno.Text, tier, flagfromboard, ddl2tierpost.SelectedValue);
            }
            else
            {

                dtgetdata = objeadmit.get_AdmitCard_2tier("", "", txtrollno.Text, tier, flagfromboard, ddl2tierpost.SelectedValue);
            }
            if (dtgetdata.Rows.Count > 0)
            {
                clear();
                string url = "";
                string ip = GetIPAddress();
                if (rbtexamtype.SelectedValue == "3" || rbtexamtype.SelectedValue == "5")
                {
                    DataTable dtcheckdata = objeadmit.getAdmitcarddetails_test(dtgetdata.Rows[0]["applid"].ToString(), dtgetdata.Rows[0]["examid"].ToString(), "", flagfromboard);
                    if (dtcheckdata.Rows.Count > 0)
                    {

                        int temp = objeadmit.insertAdmitcarddownload(dtgetdata.Rows[0]["applid"].ToString(), rbtexamtype.SelectedValue, dtgetdata.Rows[0]["examid"].ToString(), ip, dtgetdata.Rows[0]["regno"].ToString());
                        url = md5util.CreateTamperProofURL("AdmitCardPdf.aspx", null, "applid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["applid"].ToString(), true) + "&examid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt(rbtexamtype.SelectedValue, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true));
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                        // string url_instr = md5util.CreateTamperProofURL("PrintInstruction.aspx", null, "examid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["examid"].ToString(), true) + "&gender=" + MD5Util.Encrypt(dtgetdata.Rows[0]["gender"].ToString(), true));
                        // Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url_instr + "');", true);
                    }
                    else
                    {
                        msg.Show("Either Your Exam Date has been over or Your Exam has not scheduled Yet.");

                    }
                }
                else
                {
                    int temp = objeadmit.insertAdmitcarddownload(dtgetdata.Rows[0]["applid"].ToString(), rbtexamtype.SelectedValue, dtgetdata.Rows[0]["examid"].ToString(), ip, dtgetdata.Rows[0]["regno"].ToString());
                    url = md5util.CreateTamperProofURL("AdmitCardPdf.aspx", null, "applid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["applid"].ToString(), true) + "&examid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt(rbtexamtype.SelectedValue, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true));
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                    //Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
                }

            }
            else
            {
                clear();
                msg.Show("Not eligible to print e-Admit Card.");
                Server.Transfer("AdmitCardEntry.aspx");
            }
        }
    }
    protected void grid2tiertest_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (Request.QueryString["flagfromboard"] != null)
            //{
            //    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
            //}
            string examid = grid2tiertest.DataKeys[e.Row.RowIndex].Values["examid"].ToString();
            Label lblexamdate = (Label)e.Row.FindControl("lblexamdate");
            DataTable dt_examdate = objeadmit.get_examdate_test(examid, flagfromboard,"3");
            if (dt_examdate.Rows.Count > 0)
            {

                lblexamdate.Text = dt_examdate.Rows[0]["examdate"].ToString();

            }
        }
    }
    //protected void btnprintinst_Click(object sender, EventArgs e)
    //{
    //    if (Request.QueryString["flagfromboard"] != null)
    //    {
    //        flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
    //    }
    //    if (txtrollno.Text == "")
    //    {
    //        msg.Show("Please Enter Roll No.");
    //        return;
    //    }
    //    else
    //    {
    //        string tier = "";
    //        if (rbtexamtype.SelectedValue == "2" || rbtexamtype.SelectedValue == "3")
    //        {
    //            tier = "2";
    //        }
    //        else if (rbtexamtype.SelectedValue == "4" || rbtexamtype.SelectedValue == "5")
    //        {
    //            tier = "3";
    //        }
    //        DataTable dtgetdata = new DataTable();
    //        if (rbtexamtype.SelectedValue == "3" || rbtexamtype.SelectedValue == "5")
    //        {
    //            dtgetdata = objeadmit.get_AdmitCard_2tier_test("", "", txtrollno.Text, tier, flagfromboard);
    //        }
    //        else
    //        {

    //            dtgetdata = objeadmit.get_AdmitCard_2tier("", "", txtrollno.Text, tier, flagfromboard);
    //        }
    //        if (dtgetdata.Rows.Count > 0)
    //        {
    //            clear();
    //            string url = "";
    //            if (rbtexamtype.SelectedValue == "3" || rbtexamtype.SelectedValue == "5")
    //            {
    //                DataTable dtcheckdata = objeadmit.getAdmitcarddetails_test(dtgetdata.Rows[0]["applid"].ToString(), dtgetdata.Rows[0]["examid"].ToString(), "", flagfromboard);
    //                if (dtcheckdata.Rows.Count > 0)
    //                {
    //                    //url = md5util.CreateTamperProofURL("admitcard_test.aspx", null, "applid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["applid"].ToString(), true) + "&examid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt("3", true));
    //                    // Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
    //                    string url_instr = md5util.CreateTamperProofURL("PrintInstruction.aspx", null, "examid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["examid"].ToString(), true) + "&gender=" + MD5Util.Encrypt(dtgetdata.Rows[0]["gender"].ToString(), true));
    //                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url_instr + "');", true);
    //                }
    //                else
    //                {
    //                    msg.Show("Your Exam has not scheduled Yet");

    //                }
    //            }
    //            //else
    //            //{
    //            //    url = md5util.CreateTamperProofURL("admitcard.aspx", null, "applid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["applid"].ToString(), true) + "&examid=" + MD5Util.Encrypt(dtgetdata.Rows[0]["examid"].ToString(), true) + "&rbtvalue=" + MD5Util.Encrypt("2", true));
    //            //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
    //            //    //Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
    //            //}

    //        }
    //        else
    //        {
    //            clear();
    //            msg.Show("Not eligible to print e-Admit Card.");
    //            Server.Transfer("AdmitCardEntry.aspx");
    //        }
    //    }
    //}

    protected void gvtier3test_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (Request.QueryString["flagfromboard"] != null)
            //{
            //    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
            //}
            string examid = gvtier3test.DataKeys[e.Row.RowIndex].Values["examid"].ToString();
            Label lblexamdate = (Label)e.Row.FindControl("lblexamdate");
            DataTable dt_examdate = objeadmit.get_examdate_test(examid, flagfromboard,"5");
            if (dt_examdate.Rows.Count > 0)
            {

                lblexamdate.Text = dt_examdate.Rows[0]["examdate"].ToString();

            }
        }
    }
   
    protected void gv1tierpet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (Request.QueryString["flagfromboard"] != null)
            //{
            //    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
            //}
            string examid = gv1tierpet.DataKeys[e.Row.RowIndex].Values["examid"].ToString();
            Label lblexamdate = (Label)e.Row.FindControl("lblexamdate");
            DataTable dt_examdate = objeadmit.get_examdate_test(examid, flagfromboard,"6");
            if (dt_examdate.Rows.Count > 0)
            {

                lblexamdate.Text = dt_examdate.Rows[0]["examdate"].ToString();

            }
        }
    }
    private void fillrbtvalue()
    {
        try
        {
            DataTable dtrbt = new DataTable();
            dtrbt = objeadmit.GetExamtype();
            rbtexamtype.DataSource = dtrbt;
            rbtexamtype.DataTextField = "examtype";
            rbtexamtype.DataValueField = "examtypeid";
            rbtexamtype.DataBind();
           // rbtexamtype.Items[0].Selected = true;

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }

    }
    private void fillpostcode_2tier()
    {
        DataTable dtpost = new DataTable();
        if (rbtexamtype.SelectedValue == "2")
        {
             dtpost = objeadmit.get_exam_list_2tier(flagfromboard);
        }
        else if (rbtexamtype.SelectedValue == "3")
        {
            dtpost = objeadmit.get_exam_list_2tier_test(flagfromboard);
        }
        else if (rbtexamtype.SelectedValue == "4")
        {
            dtpost = objeadmit.get_exam_list_3tier(flagfromboard);
        }
        else if (rbtexamtype.SelectedValue == "5")
        {
            dtpost = objeadmit.get_exam_list_3tier_test(flagfromboard);
        }
        for (int i = 0; i < dtpost.Rows.Count; i++)
        {
            if (dtpost.Rows[i]["jid"].ToString() == "1008" || dtpost.Rows[i]["jid"].ToString() == "1012")
            {
                //ddl2tierpost.Text = "Personal Assistant (Delhi District Court)/Personal Assistant (Family Courts)";
                //dtpost.Rows[i].Delete();
                
            }
            

        }
        //dtpost.AcceptChanges();

        if (dtpost.Rows[0]["jid"].ToString() == "1009")
        {
            //ddl2tierpost.Text = "Personal Assistant (Delhi District Court)/Personal Assistant (Family Courts)";
            ddl2tierpost.DataTextField = "JobTitle";
            ddl2tierpost.DataValueField = "jid";
            ddl2tierpost.DataSource = dtpost;
            ddl2tierpost.DataBind();
            ddl2tierpost.Items.Insert(0, Comb_ddl_Select_Value());
            //tr2tierpost.Visible = true;
            
        }
        else
        {
            ddl2tierpost.DataTextField = "JobTitle";
            ddl2tierpost.DataValueField = "jid";
            ddl2tierpost.DataSource = dtpost;
            ddl2tierpost.DataBind();
            
        }
        


        if (dtpost.Rows.Count > 1)
        {
            ddl2tierpost.Items.Insert(0, Utility.ddl_Select_Value());
            tr2tierpost.Visible = true;
        }
        else
        {
            tr2tierpost.Visible = false;
        }
    }

    public string GetIPAddress()
    {
        string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            ipAddress = ipAddress.Split(',')[0];
        }
        return ipAddress;
    }
    public static ListItem Comb_ddl_Select_Value()
    {
        ListItem item = new ListItem();
        item.Text = "Personal Assistant (Delhi District Court)/Personal Assistant (Family Courts)";
        item.Value = "1012";
        return item;
    }

}
