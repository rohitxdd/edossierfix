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
using System.Web.SessionState;
using System.Drawing.Text;
using CaptchaDLL;
using System.Threading;
 
public partial class login : baseUI
{
    
    //userlogin objuserlogin = new userlogin();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    ClsAudit objClsAudit = new ClsAudit();
    public string chngpwd_date;
    public string UDedate;
    public string flg;
    public bool _showcaptcha;
    userlogin objuserlogin = new userlogin();
    MD5Util md5util = new MD5Util();
    Int32 UniqueRandomNumberCrf = 0;
    Random randObjCrf = new Random(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("Default.aspx",true);
        txtusername.Focus();
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        txtusername.Focus();
        string id = null;
        if (!IsPostBack)
        {
            
            Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.Numeric, 6);           
            UniqueRandomNumber = randObj.Next(1, 1000);
            txtrandomno.Value = Convert.ToString(UniqueRandomNumber);
            Session["randomno"] = UniqueRandomNumber;

            UniqueRandomNumberCrf = randObjCrf.Next(1, 1000);
            Session["token"] = UniqueRandomNumberCrf.ToString();
            this.csrftoken.Value = Session["token"].ToString();
           
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
        if (Request.QueryString["id"] != null)
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.Cookies.Add(new HttpCookie("newId", ""));

            Page.ClientScript.RegisterStartupScript(this.GetType(), "cle", "window.history.clear;history.go(-(history.length+1));", true);
            Response.Write("<script language=\"javascript\" type=\"text/javascript\">window.history.clear;history.go(-(history.length+1));</script>");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ExpiresAbsolute = DateTime.Now.AddMonths(-1);
            Response.Redirect("login.aspx");

        }
        Session["userid"] = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        message msg = new message();
        string username, pwd;
        username = txtusername.Text;
        pwd = txtpass.Text;
        if (Validation.chklogin(username) == "Error")
        {
            msg.Show("Invalid Input");
        }
        else if (Validation.chkpwd(pwd) == "Error")
        {
            msg.Show("Unauthorized Access");
        }
        else if (Validation.chkLevel(txtCode.Text))
        {
            msg.Show("Invalid Visual Code");
            clear();
        }
        else
        {
             if (txtCode.Text != "")
            {

                if (Session["CaptchaImageText"] != null && txtCode.Text.ToLower() == Session["CaptchaImageText"].ToString().ToLower())
                {
            string myHost = System.Net.Dns.GetHostName();
            string ipaddress = GetIPAddress();
            string logindate;
            string sessionId = "";
            logindate = Utility.formatDatewithtime(DateTime.Now);
            DataTable ds = new DataTable();
            try
            {
                DataTable dtlog = objuserlogin.UserValidate(username);
                if (dtlog.Rows.Count > 0)
                {

                    string Active = (dtlog.Rows[0]["active"]).ToString();
                    string utyp = dtlog.Rows[0]["userType"].ToString();
                    if (Active == "Y")
                    {
                        message m1 = new message();
                       // string sessionId = "";
                        //ds = objuserlogin.GetUserAuth(username, pwd, utyp, txtrandomno.Value);
                        ds = objuserlogin.GetUserAuth(username, pwd, utyp, Session["randomno"].ToString());

                        int count = ds.Rows.Count;
                        if (count > 0)
                        {
                            //sessionId = objClsAudit.InsertAudit(username, ipaddress, logindate, 'Y', 'S');
                            Session["sessionId"] = sessionId;
                            Session["userid"] = ds.Rows[0]["userid"].ToString();
                            Session["username"] = ds.Rows[0]["Name"].ToString();
                            Session["usertype"] = ds.Rows[0]["usertype"].ToString();
                            Session["deptcode"] = ds.Rows[0]["deptcode"].ToString();
                            try
                            {
                                sessionId = objClsAudit.InsertAudit(username, ipaddress, logindate, 'Y', 'S');
                            }
                            catch (Exception ex)
                            {
                                Response.Redirect("ErrorPage.aspx");
                            }

                            SessionIDManager Manager = new SessionIDManager();
                            Session["sessionId"] = sessionId;
                            string NewID = Manager.CreateSessionID(Context);
                            Response.Cookies.Add(new HttpCookie("newId", NewID));
                            Session["newId"] = NewID;
                            Response.Redirect("home.aspx");
                        }
                        else
                        {
                            try
                            {
                                sessionId = objClsAudit.InsertAudit(username, ipaddress, logindate, 'N', 'U');
                                int j = objClsAudit.updateAudit(logindate, sessionId);

                                DataTable dt = objClsAudit.GetAttempts(username, ipaddress, Utility.formatDate(DateTime.Now));
                                int noofattempts = Convert.ToInt32(dt.Rows[0]["noofattempts"]);
                                int loginattempts = noofattempts + 1;
                                objClsAudit.updateauditlog(username, ipaddress, loginattempts, Utility.formatDate(DateTime.Now));

                                //if (loginattempts >= 5)
                                //{
                                //    int n = objuserlogin.Upadte("N", username, ipaddress, username, logindate);
                                //}

                                //hlreset.NavigateUrl = url;
                                //hlunlock.Visible = false;
                                //hlreset.Visible = true;
                                //msg.Show("Your password is incorrect. Please click on Reset Password to reset your password.");
                                // lblerror.Text = "Your password is incorrect. Please click on Reset Password to reset your password.";
                            }
                            catch (Exception ex)
                            {
                                Response.Redirect("ErrorPage.aspx");
                            }
                            //sessionId = objClsAudit.InsertAudit(username, ipaddress, logindate, 'Y', 'U');
                            //int j = objClsAudit.updateAudit(logindate, sessionId);
                            //Response.Redirect("login.aspx");

                            Session["userid"] = username;
                            Session["sessionId"] = sessionId;
                            SessionIDManager Manager = new SessionIDManager();
                            string NewID = Manager.CreateSessionID(Context);
                            Response.Cookies.Add(new HttpCookie("newId", NewID));
                            Session["newId"] = NewID;
                            Session["flag"] = "U";
                            //string url = md5util.CreateTamperProofURL("ValidateSecurityPin.aspx", null,"&flag=" + MD5Util.Encrypt("u", true));
                            //Response.Redirect(url);
                            //jagat
                            //Response.Redirect("ValidateSecurityPin.aspx");
                            //txtCode.Text = "";
                            //Cleartxtboxes();
                            clear();
                            msg.Show("UserID or Password is not Correct");
                            Server.Transfer("Login.aspx");
                        }
                    }
                    else
                    {
                        Session["userid"] = username;
                        Session["sessionId"] = sessionId;
                        Session["flag"] = "N";
                        SessionIDManager Manager = new SessionIDManager();
                        string NewID = Manager.CreateSessionID(Context);
                        Response.Cookies.Add(new HttpCookie("newId", NewID));
                        Session["newId"] = NewID;

                        // string url = md5util.CreateTamperProofURL("ValidateSecurityPin.aspx", null, "&flag=" + MD5Util.Encrypt("N", true));
                        //Response.Redirect(url);
                        //jagat
                        //Response.Redirect("ValidateSecurityPin.aspx");
                        //Cleartxtboxes();
                        clear();
                        msg.Show("User is not Active");
                    }

                }
                else
                {
                    clear();
                    msg.Show("Unauthorized Access. Please contact system administrator.");
                }
            }
            catch (ThreadAbortException the)
            {

            }
            //catch (Exception ex)
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}
                }

                else
                {
                    clear();
                    msg.Show("The security code you entered is incorrect. Enter the security code as shown in the image.");
                    Server.Transfer("Login.aspx");
                    //return;
                }
            }
             else
             {
                 clear();
                 msg.Show("Please Enter Visual Code");
                 Server.Transfer("Login.aspx");
             }
            //catch (Exception ex)
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}
            txtusername.Text = "";
            txtpass.Text = "";
        }
    }

    protected void ibtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    private void clear()
    {
        Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.Numeric, 6); //generate new string
        txtCode.Text = "";
        //txtusername.Text = "";
    }
    protected void lnknewaccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("Candidatedetailformwmp.aspx");
    }
}