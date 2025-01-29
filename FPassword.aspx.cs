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
using System.Threading;

public partial class FPassword : BasePage
{
    LoginMast objLogin = new LoginMast();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    Int32 UniqueRandomNumber_code = 0;
    forgetpass objforgetpass = new forgetpass();
    string todaydate;
    string username;
    message msg = new message();
    DataTable dt = new DataTable();
    Sms objsms = new Sms();
    Int32 UniqueRandomNumberCode = 0;
    Random randObjCode = new Random();
    string SecurityCode;


    protected void Page_Load(object sender, EventArgs e)
    {
        //TextBox1.Focus();
        TRButton.Visible = false;
        TRconfirmPass.Visible = false;
        TRPassword.Visible = false;

        //txt_dd.Focus();
        txtpassword.Attributes.Add("onblur", "javascript:PassValidate()");
        UniqueRandomNumber = randObj.Next(1000, 9999);

        //UniqueRandomNumber_code = randObj.Next(1, 10000);
        //Session["token"] = UniqueRandomNumber_code.ToString();
        //this.csrftoken.Value = Session["token"].ToString();


        if (!IsPostBack)
        {
            UniqueRandomNumber_code = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber_code.ToString();
            this.csrftoken.Value = Session["token"].ToString();

            populate_year(DropDownList_year);

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


    protected void Button1_Click(object sender, EventArgs e)
    {
        //UniqueRandomNumberCode = randObjCode.Next(1000, 9999);
        string ip = GetIPAddress();
        username = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();
        //username = TextBox1.Text;

        if (Validation.chklogin(username) == "Error")
        {

            msg.Show("Invalid Input");

        }
        else if (Validation.chkLevel(username))
        {
            msg.Show("Invalid Inputs");

        }
        else
        {
            DataTable dtFP = objforgetpass.Getsecuritycode(username);
            
            if (dtFP.Rows.Count > 0)
            {
                DataRow[] rows = dtFP.Select("expired='N'");
                //rows[0]["randomno"].ToString();
                String secno = "";
                String expired ="";

                if (rows.Length > 0)
                {
                    secno = rows[0]["randomno"].ToString();
                    expired = rows[0]["expired"].ToString();
                }
                if (secno != "" && expired != "Y")
                {
                    UniqueRandomNumber = Convert.ToInt32(secno);
                }
                else
                {
                    int i = objforgetpass.ForgetPassRandom(username, UniqueRandomNumber,ip);                  
                }
                SecurityCode = Convert.ToString(UniqueRandomNumber);
                //2 line add by jagat
                //string SecurityCode1 = "Please Note Your Security Code:" + UniqueRandomNumber;
                //msg.Show(SecurityCode1);
                string mobile = dtFP.Rows[0]["mobile"].ToString();
                string code = "Security Code is : "+SecurityCode;

                //Response.Redirect("http://10.128.65.106/sms/default.aspx?mobile=" + mobile + "&msg=" + code);
                //Server.Transfer("http://10.128.65.106/sms/default.aspx?mobile="+mobile+"&msg="+code);

                objsms.sendmsg(mobile, code);
                ////string SecurityCode = "Please Note Your Security Code:" + UniqueRandomNumber;
                //msg.Show(SecurityCode);

            }
            else
            {
                msg.Show("Please Enter Valid Registration Number");
            }


        }
    }
    protected void ButtonConfirmCode_Click(object sender, EventArgs e)
    {
 
        
        
        username = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();
        //username = TextBox1.Text;

        if (Validation.chklogin(username) == "Error")
        {
            msg.Show("Invalid Input");

        }
        else if (Validation.chkLevel(username))
        {
            msg.Show("Invalid Inputs");
        }

        else
        {


            DataTable dtFP = objforgetpass.validate_credentails(username,txt_email.Text,txt_mobile.Text);
                      
            if (dtFP.Rows.Count > 0)
            {
                TRButton.Visible = true;
                TRconfirmPass.Visible = true;
                TRPassword.Visible = true;
                //TextBox1.Enabled = false;
                txt_dd.Enabled = false;
                txt_mm.Enabled = false;
                txt_yyyy.Enabled = false;
                txt_regno.Enabled = false;
                DropDownList_year.Enabled = false;
                //TDTextbox.Visible = false;
                //Button1.Visible = false;
            }

            else
            {
                msg.Show("Please Enter Valid Registration Number,Email,Mobile No");
            }

        }
    }

    protected void ButtonResetPass_Click(object sender, EventArgs e)
    {
        string ip = GetIPAddress();
           username = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();
                //username = TextBox1.Text;
           string password = txtpassword.Text;
          if (Validation.chkLevel(username))
           {
               msg.Show("Invalid Inputs");
           }
           else if (Validation.chkLevel1(password))
           {
               msg.Show("Invalid Inputs");
           }
           else
           {
               int i = objforgetpass.updatePassword(username, password, ip);
               if (i > 0)
               {
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "javascript:alert('Password Updated Sucessfully')", true);
                   //objforgetpass.Updateusername(username, TextBoxSecurityCode.Text);
                   Response.AddHeader("REFRESH", "0;URL=Default.aspx");
                   
               }
           }
                

            }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        //Response.Redirect("default.aspx");
    }
}

