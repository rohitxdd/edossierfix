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
using AjaxControlToolkit;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using CaptchaDLL;

public partial class knowYourRegistration : System.Web.UI.Page
{

    message msg = new message();
    string flag = "";
    DataTable dt = new DataTable();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    Sms objsms = new Sms();
    Email sendmail = new Email();
    string SecurityCode;
    string regno = "";
    byte[] imgfile;
    LoginMast ObjMast = new LoginMast();
    verifyAadhar vAadhar = new verifyAadhar();
    CandidateData objcand = new CandidateData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            populate_year(DropDownList_year);
            Table1.Visible = true;
            tblshow.Visible = false;
            Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.Numeric, 6);
        }           
       
    }
    protected void ibtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    protected void clear()
    {
        Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.Numeric, 6);
        txtCode.Text = "";
        txt_dd.Text = "";
        txt_mm.Text = "";
        txt_yyyy.Text = "";
        txt_rollNo.Text = "";
        DropDownList_year.ClearSelection();
        enterName.Text = "";
    }
   


    //protected void searchrgstrdetail_click(object sender, eventargs e)
    //{
    //    string regid = rgstridinput.text;
    //    dt = objmast.getregistrationdetail(regid);
    //    if (dt.rows.count > 0)
    //    {
    //        registrationno.text = dt.rows[0]["rid"].tostring();
    //        name.text = dt.rows[0]["name"].tostring();
    //        fname.text = dt.rows[0]["fname"].tostring();
    //        dob.text = dt.rows[0]["birthdt"].tostring();
    //        mobno.text = dt.rows[0]["maskedphonenumber"].tostring();
    //        email.text = dt.rows[0]["maskedemail"].tostring();
    //        tblshow.visible = true;
    //    }
    //    else
    //    {
    //        msg.show("no record found");
    //        tblshow.visible = false;
    //        rgstridinput.text = string.empty;
    //    }

    //}

    protected void btn_back_Click(object sender, EventArgs e)
    {
        //Response.Redirect("default.aspx");
    }

    private void populate_year(DropDownList passyear)
    {
        ListItem li;

        int year = DateTime.Now.Year;

        for (int i = year; i >= year - 40; i--)
        {
            li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            passyear.Items.Add(li);
        }
        li = new ListItem();
        li.Text = "-Select-";
        li.Value = "-1";
        passyear.Items.Insert(0, li);
    }

    protected void searchRgstrDetail_Click(object sender, EventArgs e)
    {
       
        int dd;
        if (!int.TryParse(txt_dd.Text, out dd) || dd < 1 || dd > 31)
        {
            msg.Show("Invalid Day Format.");
            return;
        }
        int mm;
        if (!int.TryParse(txt_mm.Text, out mm) || mm < 1 || mm > 12)
        {
            msg.Show("Invalid Month Format.");
            return;
        }
        int yyyy;
        if (!int.TryParse(txt_yyyy.Text, out yyyy))
        {
            msg.Show("Invalid Year Format.");
            return;
        }
        string name = enterName.Text.Trim();
        if (string.IsNullOrEmpty(name) || !Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
        {
            msg.Show("Invalid Name Format.");
            return;
        }

        string rid = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_rollNo.Text + DropDownList_year.SelectedItem.ToString();
        string userName = enterName.Text;
        var captcha = Session["CaptchaImageText"].ToString();

        if (txtCode.Text != "")
        {
            if (Session["CaptchaImageText"] != null && txtCode.Text == Session["CaptchaImageText"].ToString())
            {
                dt = ObjMast.getRegistrationDetail(rid, userName);
                if (dt.Rows.Count > 0)
                {
                    registrationNo.Text = dt.Rows[0]["rid"].ToString();
                    Cname.Text = dt.Rows[0]["name"].ToString();
                    fname.Text = dt.Rows[0]["fname"].ToString();
                    dob.Text = dt.Rows[0]["birthdt"].ToString();
                    mobNo.Text = dt.Rows[0]["maskedphonenumber"].ToString();
                    email.Text = dt.Rows[0]["maskedemail"].ToString();
                    tblshow.Visible = true;
                }
                else
                {
                    clear();
                    msg.Show("no record found");
                    tblshow.Visible = false;

                }
            }
            else
            {
                clear();
                msg.Show("The security code you entered is incorrect. Enter the security code as shown in the image.");
                Server.Transfer("knowYourRegistration.aspx");
            }
        }
        else
        {
            clear();
            msg.Show("Please enter visual code");
            Server.Transfer("knowYourRegistration.aspx");
        }


                
    }




}