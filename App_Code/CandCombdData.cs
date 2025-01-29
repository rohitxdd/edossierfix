using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Ocsp;

public class CandCombdData
{
    #region LogException
    void LogException(Exception ex)
    {
        //HttpBrowserCapabilities browser = Request.Browser;
        //String browserInfo = "Browser Name:" + browser.Browser + " and Browser Version:" + browser.Version;
        ExceptionLog obj = new ExceptionLog();
        try
        {
            obj.SaveException(ex, "", "", "", "");
        }
        catch
        {
            //
        }
        //Server.Transfer("ErrorPage.aspx");
    }
    #endregion
    public CandCombdData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    string str;

    DataTable dt = new DataTable();
    ApplicantData da = new ApplicantData();
    public DataTable getdeptdata(string jid)
    {
        string str = @"select preferdept as deptname,deptcode from deptselectedforpost dsf inner join deptmaster dm on dm.deptname=dsf.preferdept where jid=@jid order by deptname";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[0].Value = jid;
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
    public DataTable getcandidatedata(string jid, string applid)
    {
        string str = @"select preference,dm.deptcode as deptcode from Candidatepreferddept cpd inner join deptmaster dm on dm.deptname=cpd.Preferdepartmentname where jid=@jid and applid=@applid order by preference";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[0].Value = jid;
        param[1] = new SqlParameter("@applid", SqlDbType.NVarChar, 200);
        param[1].Value = applid;
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
    public DataTable postdata(string jid)
    {
        string str = @"select postcode, jobtitle from job_Advt where jid=@jid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[0].Value = jid;
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
    public DataTable canddata(string applid)
    {
        string str = @"select name,ar.rollno as rollno from dsssbonline_recdapp.dbo.jobApplication ja inner join applicant_result ar on ja.applid=ar.applid  where ja.applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.VarChar);
        param[0].Value = applid;
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
    public DataTable getdepartment(string jid)
    {
        string str = @"select preferdept from deptselectedforpost where jid=@jid and final_submit='Y' order by preferdept ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[0].Value = jid;
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
    public DataTable showdepartment(string jid, string rollnumber)
    {
        string str = @"select preference,Preferdepartmentname from Candidatepreferddept where jid=@jid and rollnumber=@rollnumber";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[0].Value = jid;
        param[1] = new SqlParameter("@rollnumber", SqlDbType.VarChar, 100);
        param[1].Value = rollnumber;
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
    public int insertcandidatepreferdept(string jid, string applid, string rollnumber, string preference, string Preferdepartmentname, DateTime getdate, string ipaddress, string deptreqid)
    {
        string str = @"insert into Candidatepreferddept(jid,applid,rollnumber,preference,Preferdepartmentname,entry_date,ipaddress, deptreqid)values(@jid,@applid,@rollnumber,@preference,@Preferdepartmentname,@entry_date,@ipaddress, @deptreqid)";
        SqlParameter[] param = new SqlParameter[8];
        param[0] = new SqlParameter("@jid", SqlDbType.Int);
        param[0].Value = jid;
        param[1] = new SqlParameter("@applid", SqlDbType.VarChar, 10);
        param[1].Value = applid;
        param[2] = new SqlParameter("@rollnumber", SqlDbType.VarChar, 50);
        param[2].Value = rollnumber;
        param[3] = new SqlParameter("@preference", SqlDbType.Int);
        param[3].Value = preference;
        param[4] = new SqlParameter("@Preferdepartmentname", SqlDbType.VarChar, 200);
        param[4].Value = Preferdepartmentname;
        param[5] = new SqlParameter("@entry_date", SqlDbType.DateTime);
        param[5].Value = getdate;
        param[6] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 20);
        param[6].Value = ipaddress;
        param[7] = new SqlParameter("@deptreqid", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(deptreqid))
        {
            param[7].Value = DBNull.Value;
        }
        else
        {
            param[7].Value = deptreqid;
        }
        try
        {
            int temp = Convert.ToInt32(da.ExecuteParameterizedQuery(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int delete_department(string jid, string rollno)
    {
        string str = @"delete from Candidatepreferddept where jid=@jid and rollnumber=@rollno";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[0].Value = jid;
        param[1] = new SqlParameter("@rollno", SqlDbType.NVarChar, 200);
        param[1].Value = rollno;
        try
        {
            int dt = da.ExecuteParameterizedQuery(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int update_prefer(string jid, string rollno)
    {
        string str = @"update Candidatepreferddept set final_submit ='Y' where jid=@jid and rollnumber=@rollno";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[0].Value = jid;
        param[1] = new SqlParameter("@rollno", SqlDbType.NVarChar, 200);
        param[1].Value = rollno;
        try
        {
            int dt = da.ExecuteParameterizedQuery(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int uploadDoc(string applid, string userid, string ipaddress, byte[] doc)//AnkitaSingh  doc upload
    {
        string qry = @"insert into CandidateEdossier(applid,doc,userid,edate,ipaddress) values(@applid,@doc,@userid,@edate,@ipaddress)";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@applid", SqlDbType.VarChar, 50);
        param[0].Value = applid;
        param[1] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
        param[1].Value = userid;
        param[2] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
        param[2].Value = ipaddress;
        param[3] = new SqlParameter("@edate", SqlDbType.Char, 1);
        param[3].Value = System.DateTime.Now;
        param[4] = new SqlParameter("@doc", SqlDbType.Image, doc.Length);
        param[4].Value = doc;

        try
        {
            int id = da.ExecuteParameterizedQuery(qry, param);
            return id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetEdossierMaster_prefer(string jid)
    {
        string str = @"SELECT edmid, jid, certificateReq, ctype, priority,case ctype  when 'M' then 'Mandatory' when 'O' then 'Optional' 
                     else 'If-Applicable' end as ctypename
                     FROM EdossierMaster
                   where jid=@jid and CCategory='P' ";

        str += " order by priority  ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[0].Value = jid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
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
    public DataTable checkcombd(string jid)
    {
        string str = "select deptcode from Job_Advt where jid=@jid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.Int);
        param[0].Value = jid;

        try
        {
            DataTable dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataTable select_preferCertificate(string applid, string edmid)
    {
        string str = "select doc,applid,edmid from CandidateEdossier where applid=@applid and edmid=@edmid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = applid;
        param[1] = new SqlParameter("@edmid", SqlDbType.Int);
        param[1].Value = edmid;
        try
        {
            DataTable dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetCandidateFilledDepartment(string applid)
    {
        string str = @"select distinct DeptReqId, CONCAT(DepartmentName, '::', JobTitle) as preferdept from  CombdCandDeptDetails a 
                        inner join job_advt b on a.DeptReqId = b.reqid
                        where applid = @applid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = applid;
        try
        {
            DataTable dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
