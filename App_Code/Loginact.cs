using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using System.Web.SessionState;
/// <summary>
/// Summary description for Loginact
/// </summary>
public class Loginact
{
    //DataAccessLayer da = new DataAccessLayer();
    //DataAccess da = new DataAccess();
    DataTable dt = new DataTable();
   
    public Loginact()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public DataTable UserValidate(UserLogin Log)
    //{
    //    string str = "Select user_id,InStrength from user_master where user_id=@user_id ";
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@user_id", SqlDbType.NVarChar, 30);
    //    param[0].Value = Log.User;
    //    try
    //    {
    //        dt = da.GetDataTableQry(str, param);           
    //        return dt;            
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }              
        
    //}

    //public UserLogin UserAuthentication(UserLogin Log)
    //{
    //    UserLogin logValidate = new UserLogin();

    //    string str = "select user_id,user_type,user_name,password,flg,convert(varchar,chngpwd_date,103) as chngpwd_date,deptcode,isnull(days,0) as days from user_master left outer join [dept_master] on [dept_master].deptcode=user_master.department_code left outer join MonitoringPeriodMaster mpm on user_type=mpm.usertype where user_id=@user_id and instrength='1' ";
       
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@user_id", SqlDbType.NVarChar, 30);
    //    param[0].Value = Log.User;
    //    try
    //    {
    //        dt = da.GetDataTableQry(str, param);

    //        /*DataTable dt = new DataTable();
    //        dt = da.GetDataTable(str);*/
    //        if (dt.Rows.Count > 0)
    //        {
    //            HttpSessionState session = HttpContext.Current.Session;
    //            string salt = Convert.ToString(session["randomno"]);   // Log.UserName;
    //            string Hash = dt.Rows[0]["password"].ToString();
    //            string DataVal = MD5Util.md5(Hash + salt);
    //            string SaltVal = Log.Password;
    //            if (SaltVal == DataVal)
    //            {
    //                logValidate.User = dt.Rows[0]["user_id"].ToString();
    //                logValidate.UserType = dt.Rows[0]["user_type"].ToString();
    //                logValidate.UserName = dt.Rows[0]["user_name"].ToString();
    //                logValidate.Password = dt.Rows[0]["password"].ToString();
    //                logValidate.DeptCode = dt.Rows[0]["deptcode"].ToString();
    //                logValidate.Flag = Convert.ToChar(dt.Rows[0]["flg"]);
    //                logValidate.ChngPwdDate = dt.Rows[0]["chngpwd_date"].ToString();
    //                logValidate.days = Convert.ToInt32(dt.Rows[0]["days"]);
    //            }
    //            else
    //            {
    //                logValidate = null;
    //            }

    //        }
    //        else
    //        {
    //            logValidate = null;
    //        }
    //        return logValidate;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    

    //public int ChangePassword(UserLogin Log,string ip)
    //{
    //    string edate = Utlity.formatDate(DateTime.Now);
    //    //string sStr = "update user_master set password='" + Log.Password   + "' where user_id='" + Log.User  + "'";
    //    string sStr = "update user_master set password=Password,um_logid=@logid,um_ipaddress=@ip,um_edate=@edate where user_id=user_id ";
    //    SqlParameter[] param = new SqlParameter[5];
    //    param[0] = new SqlParameter("@Password", SqlDbType.VarChar, 200);
    //    param[0].Value = Log.Password;
    //    param[1] = new SqlParameter("@user_id", SqlDbType.NVarChar, 30);
    //    param[1].Value = Log.User;
    //    param[2] = new SqlParameter("@logid", SqlDbType.NVarChar, 30);
    //    param[2].Value = Log.User;
    //    param[3] = new SqlParameter("@ip", SqlDbType.VarChar, 50);
    //    param[3].Value = ip;
    //    param[4] = new SqlParameter("@edate", SqlDbType.DateTime, 8);
    //    param[4].Value = edate;
    //    try
    //    {
    //        int id1 = da.ExecuteParameterizedQuery(sStr, param);
    //        return id1;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    /*int i = 0;
    //    i = da.ExecuteSql(sStr);
    //    return i;*/
    //}
    //public UserLogin UserAuthenticationencrypt(UserLogin Log)
    //{
    //    UserLogin logValidate = new UserLogin();
    //    //string str = "select user_id,user_type,Password from user_master where user_id='" + Log.User + "' and Password='" + Log.Password + "' ";
    //    string str = "select user_id,user_type,Password from user_master where user_id=@user_id and Password=@Password ";
    //    /*DataTable dt = new DataTable();
    //    dt = da.GetDataTable(str);*/
    //    SqlParameter[] param = new SqlParameter[2];
    //    param[0] = new SqlParameter("@user_id", SqlDbType.NVarChar, 30);
    //    param[0].Value = Log.User;
    //    param[1] = new SqlParameter("@Password", SqlDbType.VarChar, 200);
    //    param[1].Value = Log.Password;
    //    try
    //    {
    //        dt = da.GetDataTableQry(str, param);

    //        if (dt.Rows.Count > 0)
    //        {
    //            logValidate.User = dt.Rows[0]["user_id"].ToString();
    //            logValidate.UserType = dt.Rows[0]["user_type"].ToString();
    //            logValidate.Password = dt.Rows[0]["Password"].ToString();
    //            //logValidate.DeptCode = dt.Rows[0]["department_code"].ToString();
    //            //logValidate.sdept_code = dt.Rows[0]["sdept_code"].ToString();
    //        }
    //        else
    //        {
    //            logValidate = null;
    //        }
    //        return logValidate;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    
    //public int ChangePasswordencrypt(UserLogin Log,string ip)
    //{
    //    string edate = Utlity.formatDate(DateTime.Now);
    //    //string sStr = "update user_master set Password='" + Log.Password + "' where user_id='" + Log.User + "'";
    //    string sStr = "update user_master set Password=@Password,um_logid=@logid,um_ipaddress=@ip,um_edate=@edate where user_id=@user_id ";
    //    SqlParameter[] param = new SqlParameter[5];
    //    param[0] = new SqlParameter("@Password", SqlDbType.VarChar, 200);
    //    param[0].Value = Log.Password;
    //    param[1] = new SqlParameter("@user_id", SqlDbType.NVarChar, 30);
    //    param[1].Value = Log.User;
    //    param[2] = new SqlParameter("@logid", SqlDbType.NVarChar, 30);
    //    param[2].Value = Log.User;
    //    param[3] = new SqlParameter("@ip", SqlDbType.VarChar, 50);
    //    param[3].Value = ip;
    //    param[4] = new SqlParameter("@edate", SqlDbType.DateTime, 8);
    //    param[4].Value = edate;
    //    try
    //    {
    //        int id1 = da.ExecuteParameterizedQuery(sStr, param);
    //        return id1;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
        
    //}
    //public int ResetPassword(UserLogin Log,string logid,string ip)
    //{
    //    string um_edate = Utlity.formatDate(DateTime.Now);
    //    //string sStr = "update user_master set Password='" + Log.Password + "' where user_id='" + Log.User + "'";
    //    string sStr = "update user_master set Password=@Password,um_ipaddress=@um_ipaddress,um_edate=@um_edate,um_logid=@um_logid where user_id=@user_id ";
    //    SqlParameter[] param = new SqlParameter[5];
    //    param[0] = new SqlParameter("@Password", SqlDbType.VarChar, 200);
    //    param[0].Value = Log.Password;
    //    param[1] = new SqlParameter("@user_id", SqlDbType.NVarChar, 30);
    //    param[1].Value = Log.User;
    //    param[2] = new SqlParameter("@um_ipaddress", SqlDbType.VarChar, 50);
    //    param[2].Value = ip;
    //    param[3] = new SqlParameter("@um_edate", SqlDbType.DateTime, 8);
    //    param[3].Value = um_edate;
    //    param[4] = new SqlParameter("@um_logid", SqlDbType.NVarChar, 30);
    //    param[4].Value = logid;
    //    try
    //    {
    //        int id1 = da.ExecuteParameterizedQuery(sStr, param);
           
    //        return id1;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    /*int i = 0;
    //    i = da.ExecuteSql(sStr);
    //    return i;*/
    //}

    //public DataTable userDetail(string userid)
    //{
    //    string str = "Select id,userid,convert(varchar,ud_edate,103) as ud_edate from userdetails where userid=@userid";
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@userid", SqlDbType.VarChar, 30);
    //    param[0].Value = userid;
    //    try
    //    {
    //        dt = da.GetDataTableQry(str, param);
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public int Upadte(string active, string userId, string ip, string um_userid, string date)
    //{

    //    string strupdt = "Update registration set active=@active,um_ipaddress=@um_ipaddress,um_edate=@um_edate,um_logid=@um_logid where rid=@userId ";
    //    SqlParameter[] param = new SqlParameter[5];
    //    param[0] = new SqlParameter("@active", SqlDbType.Char, 1);
    //    param[0].Value = active;
    //    param[1] = new SqlParameter("@userId", SqlDbType.VarChar);
    //    param[1].Value = userId;
    //    param[2] = new SqlParameter("@um_ipaddress", SqlDbType.VarChar, 50);
    //    param[2].Value = ip;
    //    param[3] = new SqlParameter("@um_logid", SqlDbType.VarChar);
    //    param[3].Value = um_userid;
    //    param[4] = new SqlParameter("@um_edate", SqlDbType.DateTime, 8);
    //    param[4].Value = date;

    //    try
    //    {
    //        ApplicantData da = new ApplicantData();
    //        int id = da.ExecuteParameterizedQuery(strupdt, param);
    //        return id;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public DataTable Changepassword(string userid)
    //{
    //   string str = "select password from user_master  where user_id=@userid";
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@userid", SqlDbType.NVarChar,30);
    //    param[0].Value = userid;
    //    try
    //    {
    //        dt = da.GetDataTableQry(str, param);
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public int comparepwds(string inputpwd, string dbpwd)
    //{
    //    HttpSessionState session = HttpContext.Current.Session;
    //    string randomno = Convert.ToString(session["randomno"]);

    //    string encptdpwd = MD5Util.getMd5Hash(dbpwd + randomno);
    //    if (inputpwd == encptdpwd)
    //        return 1;
    //    else
    //        return 0;
    //}

    //public int ResetPassword(string pwd, string userId, string ip, string um_userid, string date, char flg)
    //{
    //    string strupdt = "Update user_master set password = @pwd,um_ipaddress=@um_ipaddress,um_edate=@um_edate,um_logid=@um_logid,flg=@flg,chngpwd_date=@um_edate where user_id = @userId ";
    //    SqlParameter[] param = new SqlParameter[6];
    //    param[0] = new SqlParameter("@pwd", SqlDbType.VarChar,200);
    //    param[0].Value = pwd;
    //    param[1] = new SqlParameter("@userId", SqlDbType.NVarChar,30);
    //    param[1].Value = userId;
    //    param[2] = new SqlParameter("@um_ipaddress", SqlDbType.VarChar,50);
    //    param[2].Value = ip;
    //    param[3] = new SqlParameter("@um_logid", SqlDbType.NVarChar, 30);
    //    param[3].Value = um_userid;
    //    param[4] = new SqlParameter("@um_edate", SqlDbType.DateTime, 8);
    //    param[4].Value = date;
    //    param[5] = new SqlParameter("@flg", SqlDbType.Char, 1);
    //    param[5].Value = flg;

    //    try
    //    {
    //        int id = da.ExecuteParameterizedQuery(strupdt, param);
    //        if (id > 0)
    //        {
    //            ClsAudit objClsAudit = new ClsAudit();
    //            string i = objClsAudit.InsertAudit(um_userid, ip, date, 'Y', 'C');
    //        }
    //        return id;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public DataTable getuserdivisions(string userid)
    //{
    //    string str = "select divid,subdivid from user_master where user_id=@userid";
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@userid", SqlDbType.NVarChar, 30);
    //    param[0].Value = userid;
    //    try
    //    {
    //        dt = da.GetDataTableQry(str, param);
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public DataTable CheckSecuQuestion(string userid)
    //{
    //    //string str = "select count(UserID) UserID from UserSecurityQues where UserID=@UserID";
    //    string str = "select count(answer) answer from userdetails where UserID=@UserID ";
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@UserID", SqlDbType.NVarChar, 30);
    //    param[0].Value = userid;
    //    try
    //    {
    //        dt = da.GetDataTableQry(str, param);            
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
}
