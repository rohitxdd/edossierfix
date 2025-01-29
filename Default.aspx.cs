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
public partial class Default : System.Web.UI.Page
{
    Random randObj = new Random();
    Random randObjCrf = new Random();
    Int32 UniqueRandomNumber = 0;
    Int32 UniqueRandomNumberCrf = 0;
    public string chngpwd_date;
    public string UDedate;
    public string flg;
    int result = 0;
    DateTime debardTillDate;
    MD5Util md5util = new MD5Util();
    public bool _showcaptcha;
    DataTable dt = new DataTable();
    message msg = new message();
    LoginMast objLogin = new LoginMast();
    string url = "";
    ClsAudit objClsAudit = new ClsAudit();
    Marks objmarks = new Marks();
    //Loginact ValidUser = new Loginact();
    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtusername.Focus();


        if (Request.QueryString["regno"] != null)
        {
            ByPass();
        }
        // txt_dd.Focus();
        String id = null;
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();

        //body.Attributes.Add("onload", "GoBack();");
        //form1.Attributes.Add("onload", "noBack();");
        if (!IsPostBack)
        {
            populate_year(DropDownList_year);
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
            Response.Redirect("Default.aspx");

        }
    }

    private void ByPass()
    {
        if (Request.QueryString["regno"] != null)
        {
            //string program = MD5Util.Decrypt(Request.QueryString["program"].ToString(), true).ToLower();
            //string year = MD5Util.Decrypt(Request.QueryString["year"].ToString(), true);
            //string userid = MD5Util.Decrypt(Request.QueryString["userid"].ToString(), true);

            string regno = MD5Util.Decrypt(Request.QueryString["regno"].ToString(), true);
            string intraflag = MD5Util.Decrypt(Request.QueryString["intraflag"].ToString(), true);
            Session["rid"] = regno;
            Session["intraflag"] = intraflag;

            DataTable dt = objLogin.GetUser_regno_details(regno);

            if (dt.Rows.Count > 0)
            {

                Session["initial"] = dt.Rows[0]["initial"].ToString();
                //Session["name"] = dt.Rows[0]["name"].ToString();
                //Session["fname"] = dt.Rows[0]["fname"].ToString();
                //Session["mothername"] = dt.Rows[0]["mothername"].ToString();
                //Session["gender"] = dt.Rows[0]["gender"].ToString();
                Session["birthdt"] = dt.Rows[0]["birthdt"].ToString();
                Session["nationality"] = dt.Rows[0]["nationality"].ToString();
                //Session["mobileno"] = dt.Rows[0]["mobileno"].ToString();
                //Session["email"] = dt.Rows[0]["email"].ToString();
                //Session["spousename"] = dt.Rows[0]["spousename"].ToString();
                Session["Random_No"] = "1000";
                Session["Salt_value"] = MD5Util.getMd5Hash(MD5Util.getMd5Hash(dt.Rows[0]["initial"].ToString()) + MD5Util.getMd5Hash("1000"));

                SessionIDManager Manager = new SessionIDManager();
                string NewID = Manager.CreateSessionID(Context);
                Response.Cookies.Add(new HttpCookie("newId", NewID));
                Session["newId"] = NewID;



                string advt_no = MD5Util.Decrypt(Request.QueryString["advt_no"].ToString(), true);
                string postCode = MD5Util.Decrypt(Request.QueryString["postCode"].ToString(), true);
                string jobtitle = MD5Util.Decrypt(Request.QueryString["jobtitle"].ToString(), true);
                string dobfrom = MD5Util.Decrypt(Request.QueryString["dobfrom"].ToString(), true);
                string dobto = MD5Util.Decrypt(Request.QueryString["dobto"].ToString(), true);
                string jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
                string reqid = MD5Util.Decrypt(Request.QueryString["reqid"].ToString(), true);
                string endson = MD5Util.Decrypt(Request.QueryString["endson"].ToString(), true);
                string jobDesc = MD5Util.Decrypt(Request.QueryString["JobDesc"].ToString(), true);
                string endson_org = MD5Util.Decrypt(Request.QueryString["endson_org"].ToString(), true);
                string agevalidationexmpt = MD5Util.Decrypt(Request.QueryString["agevalidationexmpt"].ToString(), true);
                string eqvalidationexmpt = MD5Util.Decrypt(Request.QueryString["eqvalidationexmpt"].ToString(), true);
                string url = md5util.CreateTamperProofURL("Apply.aspx", null, "advt_no=" + MD5Util.Encrypt(advt_no.ToString(), true) + "&postCode=" + MD5Util.Encrypt(postCode.ToString(), true) + "&jobtitle=" + MD5Util.Encrypt(jobtitle.ToString(), true) + "&dobfrom=" + MD5Util.Encrypt(dobfrom.ToString(), true) + "&dobto=" + MD5Util.Encrypt(dobto.ToString(), true) + "&jid=" + MD5Util.Encrypt(jid.ToString(), true) + "&endson=" + MD5Util.Encrypt(endson.ToString(), true) + "&JobDesc=" + MD5Util.Encrypt(jobDesc.ToString(), true) + "&reqid=" + MD5Util.Encrypt(reqid.ToString(), true) + "&endson_org=" + MD5Util.Encrypt(endson_org.ToString(), true) + "&agevalidationexmpt=" + MD5Util.Encrypt(agevalidationexmpt.ToString(), true) + "&eqvalidationexmpt=" + MD5Util.Encrypt(eqvalidationexmpt.ToString(), true));
                Response.Redirect(url);

                //Response.Redirect("home.aspx");

            }
        }
        else
        {
            msg.Show("Bad parameters.");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

         string username, pwd;
        //username = Utility.putstring(txtusername.Text);
        //*******added on 13/04/2023 for debarment of candidate************//
        username = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();
        pwd = txtpass.Text;
        Session["regno"] = username;
        DataTable getDebardStatus = objLogin.getDebardStatusForLogin(username);
        if (getDebardStatus.Rows.Count > 0)
        {
            debardTillDate = Convert.ToDateTime(getDebardStatus.Rows[0]["debardTillDate"].ToString());
            //var date1 = Utility.formatDateinDMY(debardTillDate);


            //if ((getDebardStatus.Rows.Count > 0) && (DateTime.Now <= debardTillDate))
            // if (DateTime.Now >= debardTillDate)
            DateTime oDate = DateTime.Now;
            result = DateTime.Compare(oDate, debardTillDate);
        }
        if (result < 0)
        {
            clear();
            msg.Show("Your candidature is DEBARRED till " + debardTillDate + " So, you cannot access this portal.");
        }
            //***************************************************//
        else
        {
            if (Validation.chklogin(username) == "Error")
            {
                clear();
                msg.Show("Login failed; Invalid user ID or password");

            }
            else if (Validation.chkpwd(pwd) == "Error")
            {
                msg.Show("Login failed; Invalid user ID or password");
                clear();
            }
            else if (Validation.chkLevel(username) || Validation.chkLevel1(txtpass.Text))
            {
                msg.Show("Login failed; Invalid user ID or password");
                clear();
            }
            else if (Validation.chkLevel(txtCode.Text))
            {
                msg.Show("Login failed; Invalid user ID or password");
                clear();
            }
            else
            {
                if (txtCode.Text != "")
                {

                    if (Session["CaptchaImageText"] != null && txtCode.Text == Session["CaptchaImageText"].ToString())
                    {
                        string myHost = System.Net.Dns.GetHostName();
                        string ipaddress = GetIPAddress();
                        string logindate;
                        string sessionId = "";
                        logindate = Utility.formatDatewithtime(DateTime.Now);
                        DataTable ds = new DataTable();
                        DataTable dtlog = objLogin.UserValidate(username);
                        if (dtlog.Rows.Count > 0)
                        {
                            string Active = (dtlog.Rows[0]["active"]).ToString();
                            if (Active == "Y")
                            {
                                #region

                                //string utyp = dtlog.Rows[0]["userType"].ToString();
                                #endregion

                                ds = objLogin.GetUserAuth(username, pwd, Session["randomno"].ToString());
                                int count = ds.Rows.Count;
                                if (count > 0)
                                {


                                    Session["rid"] = ds.Rows[0]["rid"].ToString();
                                    Session["initial"] = ds.Rows[0]["initial"].ToString();
                                    Session["name"] = ds.Rows[0]["name"].ToString();
                                    //Session["fname"] = ds.Rows[0]["fname"].ToString();
                                    //Session["mothername"] = ds.Rows[0]["mothername"].ToString();
                                    //Session["gender"] = ds.Rows[0]["gender"].ToString();
                                    Session["birthdt"] = ds.Rows[0]["birthdt"].ToString();
                                    Session["nationality"] = ds.Rows[0]["nationality"].ToString();
                                    //string adharno = ds.Rows[0]["aadharNo"].ToString();              //22122020
                                    //string nameOnIDProof = ds.Rows[0]["nameOnIDProof"].ToString();
                                    //string proofOfID = ds.Rows[0]["proofOfID"].ToString();
                                    //Session["mobileno"] = ds.Rows[0]["mobileno"].ToString();
                                    // Session["email"] = ds.Rows[0]["email"].ToString();
                                    //Session["spousename"] = ds.Rows[0]["spousename"].ToString();
                                    DataTable dtcheckserialno = objLogin.checkserialno(username);
                                    if (dtcheckserialno.Rows.Count > 0)
                                    {
                                        Session["serial_no"] = dtcheckserialno.Rows[0]["serial_no"].ToString();
                                    }
                                    try
                                    {
                                        sessionId = objClsAudit.InsertAudit(username, ipaddress, logindate, 'Y', 'S');
                                        Session["sessionId"] = sessionId;
                                    }
                                    catch (Exception ex)
                                    {
                                        Response.Redirect("ErrorPage.aspx");
                                    }

                                    SessionIDManager Manager = new SessionIDManager();
                                    string NewID = Manager.CreateSessionID(Context);
                                    Response.Cookies.Add(new HttpCookie("newId", NewID));
                                    Session["newId"] = NewID;

                                    if ((ds.Rows[0]["email"] != null && ds.Rows[0]["email"] == "") || (ds.Rows[0]["mobileno"] != null && ds.Rows[0]["mobileno"] == ""))
                                    {
                                        msg.Show("Update Mobile No./Email Id");
                                        Response.AddHeader("REFRESH", "0;URL=updateME.aspx");
                                    }
                                    else
                                    {
                                        dt = objmarks.GetexamidforAdmitCC(Session["rid"].ToString(), "1");
                                        if (dt.Rows.Count == 0)
                                        {
                                            if (!string.IsNullOrEmpty(ds.Rows[0]["chngpwd_date"].ToString()))
					    {
                                                chngpwd_date = ds.Rows[0]["chngpwd_date"].ToString();
                                            }
                                            else
                                            {
                                                chngpwd_date = ds.Rows[0]["rdate"].ToString();
                                            }                                             
                                            string dttocmp = Utility.formatDateinDMY(DateTime.Now.AddMonths(-3));                                           
                                            DateTime date = DateTime.Parse(chngpwd_date);
                                            string change_date = Utility.formatDateTimeinDMY(date);
                                            //string formattedDateString = date.ToString("dd/MM/yy");
                                            if ( Utility.comparedatesDMY(dttocmp, change_date) >= 0)
                                            {
                                                Response.Redirect("ChangePassword.aspx");
                                            }
                                            else if (date < DateTime.Parse("2024-08-14 00:00:00.000"))
{
    Response.Redirect("ChangePassword.aspx");
}
                                            else
                                            {
                                                Response.Redirect("updatemobile.aspx");
                                            }
                                            // Response.Redirect("Home.aspx");
                                            //if (adharno != "")
                                            //{
                                            //    Response.Redirect("updatemobile.aspx");
                                            //}
                                            //else
                                            //{
                                            //    if (nameOnIDProof != "" && proofOfID != "")
                                            //    {
                                            //        Response.Redirect("updatemobile.aspx");
                                            //    }
                                            //    else
                                            //    {
                                            //        Response.Redirect("insertAdharByCandidate.aspx");  //insertAdhar by Candidate 22122020
                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            Response.Redirect("AdmitCConsent.aspx");
                                        }
                                    }

                                    #region

                                    //    Session["sessionId"] = sessionId;
                                    //    Session["userid"] = ds.Rows[0]["userid"].ToString();
                                    //    Session["usertype"] = ds.Rows[0]["usertype"].ToString();
                                    //    Session["deptcode"] = ds.Rows[0]["dcode"].ToString();
                                    //    Session["bcode"] = ds.Rows[0]["bcode"].ToString();
                                    //    if (Session["bcode"].ToString() != "")
                                    //    {
                                    //        Session["depttype"] = "B";
                                    //        Session["depid"] = Session["bcode"].ToString();
                                    //    }
                                    //    else
                                    //    {
                                    //        Session["depttype"] = "D";
                                    //        Session["depid"] = Session["deptcode"].ToString();
                                    //    }

                                    //    Session["sbcode"] = ds.Rows[0]["sbcode"].ToString();
                                    //    if (utyp != "4")
                                    //    {
                                    //        Session["dname"] = ds.Rows[0]["lac_name"].ToString();
                                    //        Session["dtype"] = ds.Rows[0]["dtype"].ToString();
                                    //    }
                                    //    Session["uname"] = ds.Rows[0]["name"].ToString();
                                    //    if (ds.Rows[0]["chngpwd_date"].ToString() != "")
                                    //    {
                                    //        chngpwd_date = ds.Rows[0]["chngpwd_date"].ToString();
                                    //    }
                                    //    flg = ds.Rows[0]["flg"].ToString();

                                    //    //New Session after successfull login.



                                    //    string dttocmp = Utility.formatDateinDMY(DateTime.Now.AddMonths(-3));

                                    //    //if flag ='N' or umedate<=dttocmp then change pwd
                                    //    if ((flg == "N") || (Utility.comparedatesDMY(dttocmp, chngpwd_date) >= 0))
                                    //    {
                                    //        Response.Redirect("Changepasswrd.aspx");
                                    //    }
                                    //    else
                                    //    {
                                    //        DataTable dttemp = objGetQuery.userDetail(username);
                                    //        if (dttemp.Rows.Count != 0)
                                    //        {
                                    //            if (dttemp.Rows[0]["ud_edate"].ToString() != "")
                                    //            {
                                    //                UDedate = dttemp.Rows[0]["ud_edate"].ToString();
                                    //            }
                                    //            //if udedate<=dttocmp the modify userdetails
                                    //            if (Utility.comparedatesDMY(dttocmp, UDedate) >= 0)
                                    //            {
                                    //                Response.Redirect("EnterUserDetails.aspx");
                                    //            }
                                    //            else
                                    //            {
                                    //                if (Session["usertype"].ToString() == "7")
                                    //                {
                                    //                    Response.Redirect("Btf_Request.aspx");
                                    //                }
                                    //                else if ((Session["usertype"].ToString() == "8") || (Session["usertype"].ToString() == "9") || (Session["usertype"].ToString() == "5"))
                                    //                {
                                    //                    Master objmaster = new Master();
                                    //                    DataTable dtw = objmaster.getwcourt();
                                    //                    string wcourt = dtw.Rows[0]["wcourt"].ToString();
                                    //                    if (wcourt == "Y")
                                    //                    {
                                    //                        Response.Redirect("CaseStatistics.aspx");
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        Response.Redirect("CaseStatusReport.aspx");
                                    //                    }
                                    //                }
                                    //                else if (Session["usertype"].ToString() == "3")
                                    //                {
                                    //                    Response.Redirect("MonitorProceedings.aspx");
                                    //                }
                                    //                else if (Session["usertype"].ToString() == "2")
                                    //                {
                                    //                    Response.Redirect("ViewList.aspx");
                                    //                }
                                    //                else
                                    //                {
                                    //                    Response.Redirect("Home.aspx");
                                    //                }
                                    //            }

                                    //        }
                                    //        else
                                    //        {
                                    //            Response.Redirect("EnterUserDetails.aspx");
                                    //        }
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    try
                                    //    {
                                    //        sessionId = objClsAudit.InsertAudit(username, ipaddress, logindate, 'N', 'U');
                                    //        int j = objClsAudit.updateAudit(logindate, sessionId);

                                    //        DataTable dt = objClsAudit.GetAttempts(username, ipaddress, Utility.formatDate(DateTime.Now));
                                    //        int noofattempts = Convert.ToInt32(dt.Rows[0]["noofattempts"]);
                                    //        int loginattempts = noofattempts + 1;
                                    //        objClsAudit.updateauditlog(username, ipaddress, loginattempts, Utility.formatDate(DateTime.Now));
                                    //        if (loginattempts >= 5)
                                    //        {
                                    //            int n = objGetQuery.Upadte("N", username, ipaddress, username, logindate);
                                    //        }
                                    //        //msg.Show("Unauthorized Access. Please contact system administrator.");
                                    //    }
                                    //    catch (Exception ex)
                                    //    {
                                    //        Response.Redirect("ErrorPage.aspx");
                                    //    }
                                    //    Session["sessionId"] = sessionId;
                                    //    Session["userid"] = txtusername.Text;
                                    //    Session["flag"] = "r";
                                    //    SessionIDManager Manager = new SessionIDManager();
                                    //    string NewID = Manager.CreateSessionID(Context);
                                    //    Response.Cookies.Add(new HttpCookie("newId", NewID));
                                    //    Session["newId"] = NewID;
                                    //    //string url = md5util.CreateTamperProofURL("ValidateSecurityPin.aspx", null, "&flag=" + MD5Util.Encrypt("r", true));                           
                                    //    //Response.Redirect(url);
                                    //    Response.Redirect("ValidateSecurityPin.aspx");
                                    //}
                                    #endregion
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

                                        if (loginattempts >= 5)
                                        {
                                            int n = objLogin.Upadte("Y", username, ipaddress, username, logindate);
                                        }

                                        //hlreset.NavigateUrl = url;
                                        //hlunlock.Visible = false;
                                        //hlreset.Visible = true;
                                        //msg.Show("Your password is incorrect. Please click on Reset Password to reset your password.");
                                        // lblerror.Text = "Your password is incorrect. Please click on Reset Password to reset your password.";
                                    }
                                    catch (Exception ex)
                                    {
                                        //int val = objLogin.Inserterrorlog_pag(username, Session["rid"].ToString(), ex.ToString(), "default.aspx", "loginbutton", ipaddress, Request.Headers["User-Agent"].ToString());
                                        //int ie = objLogin.Inserterrorlog_pag(username, Session["rid"], ex.ToString(), "default.aspx", "loginbutton", ipaddress,"");
                                        Response.Redirect("ErrorPage.aspx");
                                        //int val=objLogin.inserterrorllog_p
                                    }

                                    Session["userid"] = username;
                                    Session["sessionId"] = sessionId;
                                    SessionIDManager Manager = new SessionIDManager();
                                    string NewID = Manager.CreateSessionID(Context);
                                    Response.Cookies.Add(new HttpCookie("newId", NewID));
                                    Session["newId"] = NewID;
                                    Session["flag"] = "u";
                                    //string url = md5util.CreateTamperProofURL("ValidateSecurityPin.aspx", null,"&flag=" + MD5Util.Encrypt("u", true));
                                    //Response.Redirect(url);
                                    //jagat
                                    //Response.Redirect("ValidateSecurityPin.aspx");
                                    //txtCode.Text = "";
                                    Cleartxtboxes();
                                    clear();
                                    msg.Show("Login failed; Invalid user ID or Password");
                                    Server.Transfer("Default.aspx");
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
                                Cleartxtboxes();
                                clear();
                                msg.Show("Login failed; Invalid user ID or Password");
                                Server.Transfer("Default.aspx");
                            }
                        }
                        else
                        {
                            txtCode.Text = "";
                            Cleartxtboxes();
                            clear();
                            //msg.Show("Please Enter Valid User ID");
                            msg.Show("Login failed; Invalid user ID or Password");
                            Server.Transfer("Default.aspx");

                        }

                    }
                    else
                    {
                        clear();
                        msg.Show("The security code you entered is incorrect. Enter the security code as shown in the image.");
                        Server.Transfer("Default.aspx");
                    }
                }
                else
                {
                    clear();
                    msg.Show("Please Enter Visual Code");
                    Server.Transfer("Default.aspx");
                }

                //txtusername.Text = "";
                txtpass.Text = "";
            }
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
        Cleartxtboxes();
    }
    protected void btnnewreg_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration_NewForm.aspx");
        //Response.Redirect("Registration.aspx");
    }
    private void populate_year(DropDownList passyear)
    {
        ListItem li;

        int year = DateTime.Now.Year;

        for (int i = year; i >= year - 60; i--)
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

    protected void ButtonForgetPass_Click(object sender, EventArgs e)
    {
        //Server.Transfer("ForgetPassword.aspx");
        Response.Redirect("ForgetPassword.aspx");
    }

    private void Cleartxtboxes()
    {
        txt_dd.Text = "";
        txt_mm.Text = "";
        txt_yyyy.Text = "";
        txt_regno.Text = "";
        DropDownList_year.ClearSelection();
        txtpass.Text = "";
        txtCode.Text = "";
    }
    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        if (divSignIn.Visible == false)
        {
            divSignIn.Visible = true;
            trSigInbtn.Visible = true;
            tr1.Visible = false;//true;
        }
        else
        {
            divSignIn.Visible = false;
            trSigInbtn.Visible = true;
            tr1.Visible = false; //true;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (DateTime.Now < Convert.ToDateTime("08-03-2021"))
        {
            if (divSignIn.Visible == false)
            {
                divSignIn.Visible = true;
                trSigInbtn.Visible = true;
                tr1.Visible =false; //true;
            }
            else
            {
                divSignIn.Visible = false;
                trSigInbtn.Visible = true;
                tr1.Visible = false; //true;
            }
        }
        else
        {
            msg.Show("Last date to upoload detail was 07-03-2021");
        }
    }

    protected void knowRgstrn_Click(object sender, EventArgs e)
    {


        Response.Redirect("knowYourRegistration.aspx");

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
}