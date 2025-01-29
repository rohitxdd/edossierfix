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
/// Summary description for forgetpass
/// </summary>
public class forgetpass
{
    DataTable dt = new DataTable();
    ApplicantData da = new ApplicantData();
	public forgetpass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int ForgetPassRandom(string rid,int randomno,string ip)
    {

        string str = "insert into forgetpass (rid,randomno,date,ip,expired) values (@rid,@randomno,getdate(),@ip,'N')";
        int j = 0;
        SqlParameter[] param = new SqlParameter[3];
        param[j] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[j].Value = rid;
        j = j + 1;
        param[j] = new SqlParameter("@randomno", SqlDbType.VarChar,50);
        param[j].Value = Convert.ToString(randomno);
        j = j + 1;
        param[j] = new SqlParameter("@ip", SqlDbType.VarChar, 50);
        param[j].Value = ip;

   

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

    public DataTable ValidateResetCode(string resetcode, string date,string rid)
    {

        string str = "select rid,date,randomno from forgetpass where randomno=@randomno and date=@date and rid=@rid ";
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@randomno", SqlDbType.VarChar, 50);
        param[0].Value = resetcode;

        param[1] = new SqlParameter("@date", SqlDbType.DateTime);
        param[1].Value =date;

        param[2] = new SqlParameter("@rid", SqlDbType.VarChar,50);
        param[2].Value = rid;

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



    public DataTable CheckRegnoDuplicate(string username)
    {

        string str = "select rid,date,randomno from forgetpass where rid=@rid ";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = username;

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


    public int Updateusername(string rid, string randomnoat)
    {
        
        string str = "update forgetpass set expired='Y' where randomno=@randomno and rid=@rid ";
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;

        param[1] = new SqlParameter("@randomno", SqlDbType.VarChar,50);
        param[1].Value = Convert.ToString(randomnoat);

        
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


    public int updatePassword(string rid, string password,string ip)
    {

        string str = "update registration set password=@password ,um_ipaddress=@um_ipaddress where rid=@rid ";
        SqlParameter[] param = new SqlParameter[3];
        int j = 0;
        param[j] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[j].Value = rid;
        j=j + 1;
        param[j] = new SqlParameter("@password", SqlDbType.NVarChar,200);
        param[j].Value = password;
        j = j + 1;
        param[j] = new SqlParameter("@um_ipaddress", SqlDbType.VarChar, 50);
        param[j].Value = ip;
        
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

    //public DataTable Getsecuritycode(string rid)
    //{

    //    string str = "select reg.rid,reg.email,fg.randomno as randomno,reg.mobileno as mobile,isnull(expired,'Y') expired from registration reg left outer join forgetpass fg on reg.rid =fg.rid and reg.rid=@rid and convert(varchar,fg.date,103)=convert(varchar,getdate(),103) ";
    //    SqlParameter[] param = new SqlParameter[1];

    //    param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
    //    param[0].Value = rid;

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

    public DataTable VerifyRegno(string rid)
    {

        string str = "select rid,mobileno,email from registration where rid=@rid ";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
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

    public DataTable GetOTP(string rid)
    {

        string str = "select randomno,isnull(expired,'Y') expired from forgetpass where rid=@rid and expired='N'  and convert(varchar,date,103)=convert(varchar,getdate(),103)";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
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

    public DataTable CheckRIDinRegistration(string rid)
    {

        string str = "select rid from registration where rid=@rid ";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
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

    public DataTable validate_credentails(string rid,string email,string mobile)
    {

        string str = "select * from registration where reg.rid=@rid and mobileno=@moblie and email=@email";
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@mobile", SqlDbType.VarChar, 50);
        param[1].Value = mobile;
        param[2] = new SqlParameter("@email", SqlDbType.VarChar, 50);
        param[2].Value = email;


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


    public int TranscationForgetPasswordDetails(Int64 requestid,string regno, string Mothersname, string mobileno, string email, string ip, string edate, byte[] Doc1, string filenameDoc1, string extDoc1, byte[] Doc2, string filenameDoc2, string extDoc2)
    {

        int flag = 0;
        SqlCommand command = new SqlCommand();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString"]);
        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
            connection.Open();
            //SqlCommand command = connection.CreateCommand();
            //SqlTransaction transaction = null;

            using (SqlTransaction transaction = connection.BeginTransaction())
            {

                try
                {
                    //connection.Open();
                    //transaction = connection.BeginTransaction();
                    //command.Transaction = transaction;
                    command.Connection = connection;
                    command.Transaction = transaction;

                    string insrtqry = @"Insert into RequestPool_UpdateMobile(RequestID,regno,mothername,mobileno,emailid,ipaddress,edate) values
                                        (@RequestID,@regno,@mothername,@mobileno,@emailid,@ipaddress,@edate)";

                    SqlParameter[] param = new SqlParameter[7];

                    param[0] = new SqlParameter("@RequestID", SqlDbType.BigInt);
                    if (requestid == null)
                    {
                        param[0].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[0].Value = requestid;
                    }

                    param[1] = new SqlParameter("@regno", SqlDbType.VarChar,50);
                    param[1].Value = regno;


                    param[2] = new SqlParameter("@mothername", SqlDbType.VarChar, 50);
                    param[2].Value = Mothersname;


                    param[3] = new SqlParameter("@mobileno", SqlDbType.NVarChar, 10);
                    param[3].Value = mobileno;


                    param[4] = new SqlParameter("@emailid", SqlDbType.VarChar, 100);
                    param[4].Value = email;

                    param[5] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 20);
                    param[5].Value = ip;

                    param[6] = new SqlParameter("@edate", SqlDbType.DateTime, 8);
                    param[6].Value = edate;

                    command.CommandType = CommandType.Text;
                    command.CommandText = insrtqry;
                    command.Parameters.Clear();

                    if (param != null)
                    {
                        foreach (SqlParameter param1 in param)
                        {
                            command.Parameters.Add(param1);
                        }
                        // int ChallengeID=da.GetDataTableQry
                    }
                    command.Transaction = transaction;
                   //RequestID = Convert.ToInt64(command.ExecuteScalar());
                    command.ExecuteNonQuery();

                    //Insert 
                    string str = @"insert into RequestPool_UpdateMobile_Doc (RequestID,photoidcard,photoicardfname,ext1,marksheet,marksheetfname,ext2,ipaddress,edate) 
                                    values (@RequestID,@photoidcard,@photoicardfname,@ext1,@marksheet,@marksheetfname,@ext2,@ipaddress,@edate)";

                    SqlParameter[] param2 = new SqlParameter[9];


                    param2[0] = new SqlParameter("@RequestID", SqlDbType.BigInt);
                    param2[0].Value = requestid;

                    param2[1] = new SqlParameter("@photoidcard", SqlDbType.Image);
                    param2[1].Value = Doc1;

                    param2[2] = new SqlParameter("@photoicardfname", SqlDbType.VarChar, 100);
                    param2[2].Value = filenameDoc1;

                    param2[3] = new SqlParameter("@ext1", SqlDbType.VarChar,10);
                    param2[3].Value = extDoc1;


                    param2[4] = new SqlParameter("@marksheet", SqlDbType.Image);
                    param2[4].Value = Doc2;

                    param2[5] = new SqlParameter("@marksheetfname", SqlDbType.VarChar, 100);
                    param2[5].Value = filenameDoc2;

                    param2[6] = new SqlParameter("@ext2", SqlDbType.VarChar, 10);
                    param2[6].Value = extDoc2;

                    param2[7] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 20);
                    param2[7].Value = ip;

                    param2[8] = new SqlParameter("@edate", SqlDbType.DateTime,8);
                    param2[8].Value = edate;

                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param2 != null)
                    {
                        foreach (SqlParameter param3 in param2)
                        {
                            command.Parameters.Add(param3);
                        }

                    }
                    command.ExecuteNonQuery();
                    //command.ExecuteNonQuery();
                    transaction.Commit();
                    flag  = 1;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        return flag;
    }

    //@@@@@@@@@@@@@@@@@@@@@ End Transcation @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


    public DataTable GetRequestNo(string regno)
    {

        string str = "select regno,RequestID from RequestPool_UpdateMobile where regno=@regno and detverified='P' ";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[0].Value = regno;
      

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

    public DataTable GetRequestStatus(string regno)
    {
        string str = @"select regno,RequestID,convert(varchar(10),edate,103) as requesteddate,
                      case when detverified='P' then 'Pending' When detverified='Y' then 'Accepted' When detverified='R' then 'Rejected'
                       end as Status from RequestPool_UpdateMobile  where regno=@regno ";

        str +=" order by CONVERT(INT, edate) desc";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        if (regno == "")
        {
            param[0].Value = 0;
        }
        else
        {
            param[0].Value = regno;
        }
        DataTable dt = da.GetDataTableQry(str, param);
        return dt;
    }


    public DataTable GetRequestNofromRequestUpdate(string regno)
    {

        string str = "select regno,RequestID from RequestPool_UpdateMobile where regno=@regno ";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[0].Value = regno;


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

  public DataTable GetSchedulemUpdate()
    {

        string str = "select fromdate,todate from ScheduledMupdationRequestMaster where convert(varchar(10),GETDATE(),120) between fromdate and todate and verified='Y' ";
       
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
}
