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
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.Linq;
public partial class usercontrols_Change_Password : System.Web.UI.UserControl
{
    public string username;
    userlogin objuserLogin = new userlogin();
    
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    message msg = new message();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Change Password";
        NewPassword.Attributes.Add("onblur", "javascript:PassValidate()");

        NewPassword.Attributes.Add("onKeyPress", "searchKeyPress(event)");
        CurrentPassword.Attributes.Add("onKeyPress", "searchKeyPress(event)");
        ConfirmNewPassword.Attributes.Add("onKeyPress", "searchKeyPress(event)");
       

        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 1000);
            txtrandomno.Value = Convert.ToString(UniqueRandomNumber);
            Session["randomno"] = UniqueRandomNumber;
            if (Request.QueryString["flag"] != null)
            {
                trcurntpwd.Visible = false;
            }
            CurrentPassword.Focus();
        }

    }

    protected void ChangePasswordButton_Click(object sender, EventArgs e)
    {
        int i = 0;
        try
        {
            message ms = new message();
            if (Validation.chkLevel1(CurrentPassword.Text) || Validation.chkLevel1(NewPassword.Text) || Validation.chkLevel1(ConfirmNewPassword.Text))
            {
                ms.Show("Invalid Inputs");

            }
            else
            {
                string uid = Session["rid"].ToString();

                string ip = GetIPAddress();
                if (NewPassword.Text.Equals(uid, StringComparison.OrdinalIgnoreCase))
                {
                    ms.Show("Password can not be same as login.");                   
                }
                else
                {
                    DataTable dtchk = new DataTable();
                               string newpwdhash = MD5Util.md5(NewPassword.Text + Session["randomno"].ToString());                               
                               dtchk = objuserLogin.Changepassword(uid);
                                if (dtchk.Rows.Count > 0)
                                {
                                    int cmp = objuserLogin.comparepwds(CurrentPassword.Text, dtchk.Rows[0]["password"].ToString(), Session["randomno"].ToString());
                                    if (cmp == 1)
                                    {
                                        if (Validation.chkpwd(NewPassword.Text) == "Error")
                                        {
                                            ms.Show("Invalid New Password");
                                        }
                                        else
                                        {
                                            
                                                if (newpwdhash != CurrentPassword.Text)
                                                {
                                                    i = objuserLogin.ResetPassword(NewPassword.Text, uid, ip, uid, Utility.formatDatewithtime(DateTime.Now),'C');
                                                    if (i > 0)
                                                    {
                                                        Label2.Visible = true;
                                                        Int32 UniqueRandomNumber = randObj.Next(1, 10000);
                                                        Session["logoutvariable"] = UniqueRandomNumber;
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Password Changed Sucessfully')", true);
                                                        Response.AddHeader("REFRESH", "2;URL=Logout.aspx");
                                                    }
                                                }
                                                else
                                                {
                                                    ms.Show("New and Old Password can not be same.");

                                                }
                                            }
                                        
                                    }
                                    else
                                    {
                                        ms.Show("Old Password is not correct.");
                                    }

                                }
                     }
                }
            }
    
        catch (ThreadAbortException ex)
        {

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
        //if (i > 0)
        //{

        //    Int32 UniqueRandomNumber = randObj.Next(1, 10000);
        //    Session["logoutvariable"] = UniqueRandomNumber;
        //    //msg.Show("Password Changed...");
        //    //Response.Redirect("Logout.aspx?logoutvariable=" + UniqueRandomNumber.ToString());
        //    //Server.Transfer("Logout.aspx");
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Password Changed Sucessfully')", true);
        //    Response.AddHeader("REFRESH", "2;URL=Logout.aspx");

        //}

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

    //protected void CancelButton_Click(object sender, EventArgs e)
    //{
    //    CurrentPassword.Text = "";
    //    NewPassword.Text = "";
    //    ConfirmNewPassword.Text = "";
    //}
}
