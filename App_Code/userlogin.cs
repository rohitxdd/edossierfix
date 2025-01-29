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
/// Summary description for userlogin
/// </summary>
public class userlogin
{
    DataAccess da = new DataAccess();
    DataTable dt = new DataTable();
    string str = "";

    string connectionString = ConfigurationManager.AppSettings["con"];

    string _userid;
    string _usertype;
    string _deptcode;
    string _password;
    string _UserName;
    
   

	public userlogin()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    public string User
    {
        get
        {
            return _userid;
        }
        set
        {
            _userid = value;
        }
    }
    public string UserType
    {
        get
        {
            return _usertype;
        }
        set
        {
            _usertype = value;
        }
    }
    public string DeptCode
    {
        get
        {
            return _deptcode;
        }
        set
        {
            _deptcode = value;
        }
    }
    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;
        }
    }
    public string UserName
    {
        get
        {
            return _UserName;
        }
        set
        {
            _UserName = value;
        }
    }

    
    public DataTable UserValidate(string userid)
    {
        string str = "Select userid,active,userType from usermaster where userid=@userid ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userid", SqlDbType.NVarChar, 30);
        param[0].Value = userid;
        try
        {
            dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public int comparepwds(string inputpwd, string dbpwd, string randomno)
    {
        string encptdpwd = MD5Util.md5(dbpwd + randomno);
        if (inputpwd == encptdpwd)
            return 1;
        else
            return 0;
    }
    public DataTable GetUserAuth(string username, string pwd, string userttype, string randomno)
    {
        string loginStr = "select pwd,upper(ltrim(rtrim(userid))) as userid,Name,usertype,deptcode from usermaster where active='Y' and userid=@userid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userId", SqlDbType.VarChar, 12);
        param[0].Value = username;
        try
        {
            dt = da.GetDataTableQry(loginStr, param);
            if (dt.Rows.Count > 0)
            {
                int flag = comparepwds(pwd, dt.Rows[0]["pwd"].ToString(), randomno);
                if (flag == 0)
                {
                    dt = new DataTable();
                }
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetMenuMaster(string usertype, string url)
    {
        str = "Select * from menumaster where MenuId in (Select MenuId from Menu_UserType where typeno = @usertype) and url like @url";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@usertype", SqlDbType.Int, 4);
        param[0].Value = usertype;
        param[1] = new SqlParameter("@url", SqlDbType.NVarChar, 502);
        param[1].Value = "%" + url + "%";
        try
        {
            dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetMenuDetail(string usertype)
    {

        str = "Select Menuid, subheadname,Description, ParentID,url from Menumaster where MenuId in (Select MenuId from Menu_UserType where typeno = @typeno) and prno is not null order by prno";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@typeno", SqlDbType.Int, 4);
        if (usertype == "")
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = usertype;
        }
        try
        {
            dt = da.GetDataTableQry(str, param);
            return dt;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int ResetPassword(string pwd, string userId, string ip, string um_userid, string date, char action)
    {

        string strupdt = @"Update registration set password = @pwd,um_ipaddress=@ip,um_edate=@um_edate,
                        um_logid=@um_logid,chngpwd_date=@um_edate
                        where rid = @userId ";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@pwd", SqlDbType.NVarChar, 200);
        param[0].Value = pwd;
        param[1] = new SqlParameter("@userId", SqlDbType.VarChar);
        param[1].Value = userId;
        param[2] = new SqlParameter("@ip", SqlDbType.VarChar, 15);
        param[2].Value = ip;
        param[3] = new SqlParameter("@um_logid", SqlDbType.VarChar, 12);
        param[3].Value = um_userid;
        param[4] = new SqlParameter("@um_edate", SqlDbType.DateTime, 8);
        param[4].Value = date;

        try
        {
            int id = da.ExecuteParameterizedQuery(strupdt, param);
            if (id > 0)
            {
                ClsAudit objClsAudit = new ClsAudit();
                string i = objClsAudit.InsertAudit(um_userid, ip, date, 'Y', action);
            }
            return id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //public int comparepwds(string inputpwd, string dbpwd, string randomno)
    //{
    //    string encptdpwd = MD5Util.md5(dbpwd + randomno);
    //    if (inputpwd == encptdpwd)
    //        return 1;
    //    else
    //        return 0;
    //}

    public DataTable Changepassword(string userId)
    {
        string str = "select password from registration where rid=@userId";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userId", SqlDbType.VarChar);
        param[0].Value = userId;
        try
        {
            dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
