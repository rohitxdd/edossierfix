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

/// <summary>
/// Summary description for ClsAudit
/// </summary>
public class ClsAudit
{
    //DataAccess da = new DataAccess();
    ApplicantData da = new ApplicantData();
    DataTable dt = new DataTable();
    string str = "";
    string connectionString = ConfigurationManager.AppSettings["CSreader"];
    public ClsAudit()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string InsertAudit(string UserId, string ipaddress, string logindt, char status, char action)
    {
        string InsetAudit = "Insert into AuditLog(UserId,IpAddress,LoginDatetime,Status,action) values(@UserId,@IpAddress,@LoginDatetime,@Status,@action) select id=scope_identity()";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 50);
        param[0].Value = UserId;
        param[1] = new SqlParameter("@IpAddress", SqlDbType.VarChar, 20);
        param[1].Value = ipaddress;
        param[2] = new SqlParameter("@LoginDatetime", SqlDbType.DateTime, 8);
        param[2].Value = logindt;
        param[3] = new SqlParameter("@Status", SqlDbType.Char, 1);
        param[3].Value = status;
        param[4] = new SqlParameter("@action", SqlDbType.Char, 1);
        param[4].Value = action;
        
        try
        {
            string id = Convert.ToString(da.ExecScaler(InsetAudit, param));
            return id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


  



    public int updateAudit(string LogoutDatetime, string userid)
    {
        str = "update AuditLog set LogoutDatetime=@LogoutDatetime where id=@userid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@LogoutDatetime", SqlDbType.DateTime, 8);
        param[0].Value = LogoutDatetime;
        param[1] = new SqlParameter("@userid", SqlDbType.Int, 4);
        param[1].Value = userid;

        try
        {
            int id = da.ExecuteParameterizedQuery(str, param);
            return id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetAttempts(string userid, string IpAddress, string LoginDatetime)
    {
        string NoofAttemp = "";
        NoofAttemp = "select isnull(cntloginattempts,0) as noofattempts from AuditLog where userid=@userid and IpAddress=@IpAddress and convert(varchar(10),LoginDatetime,111)=@LoginDatetime and status='N'";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
        param[0].Value = userid;
        param[1] = new SqlParameter("@IpAddress", SqlDbType.VarChar, 20);
        param[1].Value = IpAddress;
        param[2] = new SqlParameter("@LoginDatetime", SqlDbType.DateTime, 8);
        param[2].Value = LoginDatetime;
        try
        {
            dt = da.GetDataTableQry(NoofAttemp, param);
            return dt;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
   
    public int updateauditlog(string UserId, string ipaddress, int noofattempts, string LoginDatetime)
    {
        str = "update auditlog set cntloginattempts=@noofattempts where userId=@userId and IpAddress=@IpAddress and status='N' and convert(varchar(10),LoginDatetime,111)=@LoginDatetime";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@userId", SqlDbType.VarChar, 50);
        param[0].Value = UserId;
        param[1] = new SqlParameter("@IpAddress", SqlDbType.VarChar, 20);
        param[1].Value = ipaddress;
        param[2] = new SqlParameter("@noofattempts", SqlDbType.Int, 12);
        param[2].Value = noofattempts;
        param[3] = new SqlParameter("@LoginDatetime", SqlDbType.DateTime, 8);
        param[3].Value = LoginDatetime;
        try
        {
            int id = da.ExecuteParameterizedQuery(str, param);
            return id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int updateauditlog(string UserId)
    {
        str = "update auditlog set cntloginattempts=0 where userId=@userId and status='N'";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userId", SqlDbType.VarChar, 50);
        param[0].Value = UserId;
        try
        {
            int id = da.ExecuteParameterizedQuery(str, param);
            return id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable bindauditgrid(string ddlUserId, string ddlActive, string usertype)
    {
        string qry = "";

        qry = "Select Id, UserId, IpAddress,LoginDatetime, LogoutDatetime,case Status when 'Y' Then 'Yes' When 'N'then 'No'end as Status from auditlog where 1=1";

        if (ddlUserId != "")
        {
            qry = qry + " and UserId=@UserId";
        }
        if (ddlActive != "")
        {
            qry = qry + " and Status=@Status";
        }

        qry = qry + " ORDER BY LoginDatetime DESC";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 50);
        if (ddlUserId == "")
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = ddlUserId;
        }
        param[1] = new SqlParameter("@Status", SqlDbType.Char, 1);
        if (ddlActive == "")
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = ddlActive;
        }

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

}
