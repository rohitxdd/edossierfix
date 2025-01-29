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
using System.Configuration;
using System.Threading;
public partial class UserControls_ChangePassword : System.Web.UI.UserControl
{
    public string username;
    GetQuery objGetQuery = new GetQuery();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    SecuritPin objpin = new SecuritPin();
    message msg = new message();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Change Password";
        NewPassword.Attributes.Add("onblur", "javascript:PassValidate()");
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
                string uid = Session["userid"].ToString();

                string ip = GetIPAddress();
                if (NewPassword.Text.Equals(uid, StringComparison.OrdinalIgnoreCase))
                {
                    ms.Show("Password can not be same as login.");                   
                }
                else
                {
                    string pin = ""; 
                    string newpwdhash = MD5Util.md5(NewPassword.Text + Session["randomno"].ToString());
                    //DataTable dtchkS = objpin.getsecuritypin(uid);
                    //if (dtchkS.Rows.Count > 0)
                    //{
                    //    pin = dtchkS.Rows[0]["pin"].ToString();
                    //}
                    //int cmppin = objGetQuery.comparepwds(newpwdhash, pin, Session["randomno"].ToString());
                    //if (cmppin == 1)
                    //{
                    //    ms.Show("Password can not be same as Pin.");
                    //}
                    //else                                    
                    //{                         
                                                               
                            if (Request.QueryString["flag"] != null)
                            {
                                //i = objlogin.ResetPassword(NewPassword.Text, uid, ip, uid, Utlity.formatDate(DateTime.Now), 'R');
                                if (MD5Util.Decrypt(Request.QueryString["flag"].ToString(), true) == "c")
                                {
                                    uid = MD5Util.Decrypt(Request.QueryString["userid"].ToString(), true);
                                    i = objGetQuery.ResetPassword(NewPassword.Text, uid, ip, uid, Utility.formatDatewithtime(DateTime.Now), 'N', 'R');
                                    if (i > 0)
                                    {
                                        msg.Show("Password Has Changed Successfully!"); 
                                        //Response.Redirect("Home.aspx");
                                        Server.Transfer("Home.aspx");
                                        
                                        
                                    }
                                }
                                else
                                {
                                    i = objGetQuery.ResetPassword(NewPassword.Text, uid, ip, uid, Utility.formatDatewithtime(DateTime.Now), 'R', 'R');
						        if (i > 0)
                                {
                                    Label2.Visible = true;
                                    Int32 UniqueRandomNumber = randObj.Next(1, 10000);
                                    Session["logoutvariable"] = UniqueRandomNumber;
                                    Server.Transfer("Logout.aspx?logoutvariable=" + UniqueRandomNumber.ToString()); // Server.Transfer("Login.aspx");
                                }
                                }
                                
                            }
                            else
                            {
                               
                                DataTable dtchk = objGetQuery.Changepassword(uid);
                                if (dtchk.Rows.Count > 0)
                                {
                                    int cmp = objGetQuery.comparepwds(CurrentPassword.Text, dtchk.Rows[0]["pwd"].ToString(), Session["randomno"].ToString());
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
                                                    i = objGetQuery.ResetPassword(NewPassword.Text, uid, ip, uid, Utility.formatDatewithtime(DateTime.Now), 'R', 'C');
                                                    if (i > 0)
                                                    {
                                                        Label2.Visible = true;
                                                        Int32 UniqueRandomNumber = randObj.Next(1, 10000);
                                                        Session["logoutvariable"] = UniqueRandomNumber;

                                                        Response.Redirect("Logout.aspx?logoutvariable=" + UniqueRandomNumber.ToString());
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
            }
        //}
        catch (ThreadAbortException ex)
        {

        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
        if (i > 0)
        {

            Int32 UniqueRandomNumber = randObj.Next(1, 10000);
            Session["logoutvariable"] = UniqueRandomNumber;

            Response.Redirect("Logout.aspx?logoutvariable=" + UniqueRandomNumber.ToString());

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
    //protected void CancelButton_Click(object sender, EventArgs e)
    //{
    //    CurrentPassword.Text = "";
    //    NewPassword.Text = "";
    //    ConfirmNewPassword.Text = "";
    //}
}
