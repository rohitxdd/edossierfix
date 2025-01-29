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

/// <summary>
/// Summary description for Grievance
/// </summary>
public class Grievance
{
	public Grievance()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    string str;
    DataTable dt = new DataTable();
    ApplicantData da = new ApplicantData();
    public DataTable getGcat()
    {
        str = "select GCID,GCDesc from GCatMaster order by GCDesc";
        try
        {
            DataTable dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    public DataTable getGSubcat(string GCID)
    {
        str = "select GSCID,GCDesc from GSubCatMaster where GCID=@GCID order by GCDesc";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@GCID", SqlDbType.Int);
        if (GCID == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = GCID;
        }
        try
        {
            DataTable dt = da.GetDataTableQry(str,param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataTable getcandidatereleasedexams(string regno)
    {
        str = @"select examid, convert(varchar,dateofexam,103) +' ('+ timeofexam+ ')' as exam
                from exammast where radmitcard='Y' and convert(varchar,dateofexam,103) > convert(varchar,getdate(),103) and
                 examid in (select examid from applicantcenter ac left outer join JobApplication jap on ac.applid=jap.applid
                where ISNULL(ac.regno,jap.regno)=@regno) order by examid desc";


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

    public int InsertGrievanceTransaction(string GrNo, string regno, string GSCID, string GrDesc, string IP, string Examid, string jid, byte[] GrDocument)
    {
        int GrID = 0;
        SqlCommand command = new SqlCommand();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString_RO"]);
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

                    string insrtqry = @"Insert into GrievanceMaster(GrNo,regno,GSCID,GrDesc,Grdate,IP,Examid,jid) values (@GrNo,@regno,@GSCID,@GrDesc,getdate(),@IP,@Examid,@jid) select SCOPE_IDENTITY()";

                    SqlParameter[] param = new SqlParameter[7];

                    param[0] = new SqlParameter("@GrNo", SqlDbType.Int);
                    param[0].Value = GrNo;

                    param[1] = new SqlParameter("@regno", SqlDbType.VarChar,50);
                    if (regno == "" || regno == null)
                    {
                        param[1].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[1].Value = regno;
                    }

                    param[2] = new SqlParameter("@GSCID", SqlDbType.Int, 4);
                    if (GSCID == "" || GSCID == null)
                    {
                        param[2].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[2].Value = Convert.ToInt32(GSCID);
                    }

                    param[3] = new SqlParameter("@GrDesc", SqlDbType.VarChar, 500);
                    if (GrDesc == null || GrDesc == "")
                    {
                        param[3].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[3].Value = GrDesc;
                    }

                    param[4] = new SqlParameter("@Examid", SqlDbType.Int,4);
                    if (string.IsNullOrEmpty(Examid))
                    {
                        param[4].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[4].Value = Examid;
                    }

                    param[5] = new SqlParameter("@jid", SqlDbType.Int, 4);
                    if (string.IsNullOrEmpty(jid))
                    {
                        param[5].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[5].Value = jid;
                    }
                    param[6] = new SqlParameter("@IP", SqlDbType.VarChar, 50);
                    if (IP == null || IP == "")
                    {
                        param[6].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[6].Value = IP;
                    }
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
                    GrID = Convert.ToInt32(command.ExecuteScalar());
                    //command.ExecuteNonQuery();

                    //Insert Bank Doc 
                    string str = " insert into GrvDocMaster (GrID,GrDocument,IP,Edate,Userid) values (@GrID,@GrDocument,@IP,getdate(),@regno)";

                    // string str = "insert into BankDocs (AppID,image,filename,ext,ip,edate) values (@AppID,@image,@filename,@ext,@Ip,@edate)";

                    SqlParameter[] param2 = new SqlParameter[4];


                    param2[0] = new SqlParameter("@GrID", SqlDbType.Int, 4);
                    param2[0].Value = GrID;

                    param2[1] = new SqlParameter("@GrDocument", SqlDbType.Image);
                    param2[1].Value = GrDocument;

                    param2[2] = new SqlParameter("@IP", SqlDbType.VarChar, 50);
                    param2[2].Value = IP;

                    param2[3] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
                    param2[3].Value = regno;

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
                    // ChallengeID  = 1;

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
        return GrID;

    }
}