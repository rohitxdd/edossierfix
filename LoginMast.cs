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
/// Summary description for LoginMast
/// </summary>
public class LoginMast
{
	public LoginMast()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DataTable dt = new DataTable();
    ApplicantData da = new ApplicantData();

    public DataTable UserValidate(string userid)
    {
        string str = "Select rid,active from registration where rid=@rid ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
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

    public DataTable GetUserAuth(string username, string pwd, string randomno)
    {
        string str = @"select rid,password,initial,name,fname,mothername,gender,convert(varchar,birthdt,103) birthdt,nationality,mobileno,email 
        from registration where rid=@rid and active='Y'";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
        param[0].Value = username;
        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count > 0)
            {
                int flag = comparepwds(pwd, dt.Rows[0]["password"].ToString(), randomno);
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

    public int comparepwds(string inputpwd, string dbpwd, string randomno)
    {
        string encptdpwd = MD5Util.md5(dbpwd + randomno);
        if (inputpwd == encptdpwd)
            return 1;
        else
            return 0;
    }


    public DataTable IsExist_Applicant(string regno)
    {
        string qry = @" select rid from registration where 
                        rid=@regno";        
        SqlParameter[] param = new SqlParameter[1];
        int j=0;
        param[j] = new SqlParameter("@regno", SqlDbType.VarChar);
        param[j++].Value=regno;        
        try
        {

            dt = da.GetDataTableQry(qry, param);
            return dt;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public DataTable GetMaxReg_coun()
    {
        string str = "select max(rid) as rid from registration";
        try
        {
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   


    public int insert_registration(string rid, string password, string uid, string name, string fhname, string mothername, string gender, string dob, string nationality, string mobil, string mail, string ip, string active, string rdate,string rollno,string passing_year)
    {

        string str = @"insert into registration (rid,password,uid,name,fname,mothername,gender,birthdt,nationality,mobileno,email,um_ipaddress,active,rdate,rollno,passing_year) 
         values(@rid,@password,@um_logid,@name,@fname,@mothername,@gender,@birthdt,@nationality,@mobileno,@email,@IP,@active,@rdate,@rollno,@passing_year)";
        SqlParameter[] param = new SqlParameter[16];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
        param[0].Value = rid;
        param[1] = new SqlParameter("@password", SqlDbType.NVarChar, 200);
        param[1].Value = password;

        param[2] = new SqlParameter("@um_logid", SqlDbType.BigInt);
        if (uid == "" || uid == null)
        {
            param[2].Value = System.DBNull.Value;
        }
        else
        {
            param[2].Value = Int64.Parse(uid);
        }
       

        param[3] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
        param[3].Value = name;
        param[4] = new SqlParameter("@fname", SqlDbType.NVarChar, 50);
        param[4].Value = fhname;
        param[5] = new SqlParameter("@mothername", SqlDbType.NVarChar, 50);
        param[5].Value = mothername;
        param[6] = new SqlParameter("@gender", SqlDbType.Char, 1);
        param[6].Value = gender;
        param[7] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
        param[7].Value = Utility.formatDate(dob);
        param[8] = new SqlParameter("@nationality", SqlDbType.NVarChar, 50);
        param[8].Value = nationality;
        param[9] = new SqlParameter("@mobileno", SqlDbType.NVarChar, 12);
        param[9].Value = mobil;
        param[10] = new SqlParameter("@email", SqlDbType.VarChar, 50);
        param[10].Value = mail;
        param[11] = new SqlParameter("@IP", SqlDbType.VarChar, 50);
        param[11].Value = ip;
        param[12] = new SqlParameter("@rdate", SqlDbType.DateTime);
        param[12].Value = rdate;
        param[13] = new SqlParameter("@active", SqlDbType.Char, 1);
        param[13].Value = active;
        param[14] = new SqlParameter("@rollno", SqlDbType.BigInt);
        param[14].Value =Int64.Parse(rollno);
        param[15] = new SqlParameter("@passing_year", SqlDbType.Int);
        param[15].Value =Int32.Parse(passing_year);
        //param[16] = new SqlParameter("@percentage", SqlDbType.Decimal);
        try
        {
            int id = da.ExecuteParameterizedQuery(str, param);
            return id;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable Changepassword(string rid)
    {
        string str = "select password from registration where rid=@rid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
        param[0].Value = rid;
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

    public int ResetPassword(string pwd, string userId, string ip, string um_userid, string date, char flg, char action)
    {

        string strupdt = "Update registration set password = @pwd,um_ipaddress=@um_ipaddress,um_edate=@um_edate,um_logid=@um_logid,flg=@flg,chngpwd_date=@um_edate where rid = @userId ";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@pwd", SqlDbType.NVarChar, 200);
        param[0].Value = pwd;
        param[1] = new SqlParameter("@userId", SqlDbType.VarChar);
        param[1].Value = userId;
        param[2] = new SqlParameter("@um_ipaddress", SqlDbType.VarChar, 15);
        param[2].Value = ip;
        param[3] = new SqlParameter("@um_logid", SqlDbType.VarChar, 12);
        param[3].Value = um_userid;
        param[4] = new SqlParameter("@um_edate", SqlDbType.DateTime, 8);
        param[4].Value = date;
        param[5] = new SqlParameter("@flg", SqlDbType.Char, 1);
        param[5].Value = flg;

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

    public int Upadte(string active, string userId, string ip, string um_userid, string date)
    {

        string strupdt = "Update registration set active=@active,um_ipaddress=@um_ipaddress,um_edate=@um_edate,um_logid=@um_logid where rid=@userId ";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@active", SqlDbType.Char, 1);
        param[0].Value = active;
        param[1] = new SqlParameter("@userId", SqlDbType.VarChar);
        param[1].Value = userId;
        param[2] = new SqlParameter("@um_ipaddress", SqlDbType.VarChar, 50);
        param[2].Value = ip;
        param[3] = new SqlParameter("@um_logid", SqlDbType.VarChar);
        param[3].Value = um_userid;
        param[4] = new SqlParameter("@um_edate", SqlDbType.DateTime, 8);
        param[4].Value = date;

        try
        {
            ApplicantData da = new ApplicantData();
            int id = da.ExecuteParameterizedQuery(strupdt, param);
            return id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
