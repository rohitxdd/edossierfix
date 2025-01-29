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
/// Summary description for challengeansheet
/// </summary>
public class challengeansheet
{
    string str;
    DataTable dt = new DataTable();
    ApplicantData da = new ApplicantData();
    public challengeansheet()
    {
        //
        // TODO: Add constructor logic here
        //
    }

     public DataTable getexamtochallenge(string regno)
    {
//          str = @"select examid, convert(varchar,dateofexam,103) +' ('+ timeofexam+ ')' as examdtl 
//                from exammast where examid in (select examid from ExamBookLetMaster)
//                and examid in (select examid from applicantcenter ac 
//                left outer join jobapplication jap on ac.applid=jap.applid and jap.regno=@regno
//                where  ISNULL(ac.regno,jap.regno)=@regno
//                union
//                select examid from applicantcenter ac 
//                left outer join dsssbonline_recdapp.dbo.jobapplication jap on ac.applid=jap.applid 
//                and jap.regno=@regno
//                where ISNULL(ac.regno,jap.regno)=@regno)
//                order by examid desc ";
        str = @"select examid,examdtl from (
                select cast (examid as varchar) as examid , convert(varchar,dateofexam,103) +' ('+ timeofexam+ ')' as examdtl ,dateofexam
                from exammast where examid in (select examid from ExamBookLetMaster)
                and examid in 
				(select examid from attendancemaster where regno=@regno)		
				union
                select cast(examid as varchar)+':'+cast(batchid as varchar) as examid , 
				CONVERT(VARCHAR(10),examdate,103)+'('+ examtime +') :: '+ batchname as examdtl , 
				examdate as dateofexam
                from BatchMaster where batchid in (select batchid from ExamBookLetMaster)
                and 
				batchid in 
				(select batchid from attendancemaster where regno=@regno)	
						)a
                order by dateofexam desc ";



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


    public DataTable question()
    {
        str = "select COptId,CoptDesc from ChallengeOptMast";
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

    public DataTable GetPostCode(string examid, string regno, string tierval,string batchid)
    {
        string condn = "";
        if (tierval == "1")
        {
            condn += " and examid=@examid";
            if (batchid != "")
            {
                condn += " and batchid=@batchid";
            }
        }
        else
        {
            condn += " and ja.jid in (select jid  from ResultVerification where examid =@examid)";


        }
         str = @"select postcode+' : '+jobtitle as post,ja.jid,jap.applid from job_advt ja inner join jobapplication jap on ja.jid = jap.jid 
               where regno=@regno and acstatid is not null "+condn+@"
              union 
              select postcode+' : '+jobtitle as post,ja.jid,jap.applid from job_advt ja inner join dsssbonline_recdapp.dbo.jobapplication jap on ja.jid = jap.jid 
               where regno=@regno and acstatid is not null " + condn ; 
       
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
        param[0].Value = examid;
        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[1].Value = regno;
        param[2] = new SqlParameter("@batchid", SqlDbType.Int, 4);
        if (batchid != "")
        {
            param[2].Value = batchid;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
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


    public DataTable ddlbookletno(string examid,string batchid)
    {
        str = "select ExamBookLetID,BookLetCode from ExamBookLetMaster where examid=@examid ";
        if (batchid != "")
        {
            str += " and batchid=@batchid ";
        }
        str += "order by BookLetCode ";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
        param[0].Value = examid;
        param[1] = new SqlParameter("@batchid", SqlDbType.Int, 4);
        if (batchid != "")
        {
            param[1].Value = batchid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }
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




    public DataTable rboptions_rb()
    {
        str = " select COptID,CoptDesc,cast (COptID as varchar) +'|' + AmongAns as OptId from ChallengeOptMast  order by COptID  ";
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

    public DataTable rboptions2(string lbldsssbans)
    {
        str = " select Ans from answermaster";

        if(lbldsssbans!= "")
        {
          str += " where Ans <> (@lbldsssbans)";
        }

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@lbldsssbans", SqlDbType.Char, 1);
        param[0].Value = lbldsssbans;
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

    public DataTable fillquestiono(string ExamBookLetID, string regno)
    {
        str = "  select QuestionNo,ExamBookLetQMasterID from ExamBookLetQMaster where ExamBookLetID=@ExamBookLetID and ExamBookLetQMasterID not in (select ExamBookLetQMasterID from ChallengeMaster where Cregno=@regno) ";

        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@ExamBookLetID", SqlDbType.Int);
        if (ExamBookLetID == null)
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = ExamBookLetID;
        }
        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[1].Value = regno;
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

    public DataTable getanswerkeydocs(string ExamBookLetID)
    {
        string str = "select BAnswerKey from ExamBookLetMaster where ExamBookLetID=@ExamBookLetID";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ExamBookLetID", SqlDbType.Int, 4);
        param[0].Value = ExamBookLetID;

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


    public DataTable GetChallengemast_Details(string examid, string regno,string batchid)
    {
        string str = @"select cm.ExamBookletQMasterID,ebm.exambookletid,cm.CRegno,cm.ChallengeID,cm.Remarks,
                       cm.CPdID,bookletcode,questionno,ans,
                       case coptid when 1 then 'Options Others than the Answer Key : '+ cm.copt2 when 2 then 'Multiple Answers : '+copt3 
                       when 3 then 'None of the Options given' end as sanswer, remarks,cm.CPdID,
                       case cpd.status when 'SUCCESS' then 'Paid' else 'Pending' end as feestatus,cpd.status,
                       case cm.cstatus when 'A' then 'Accepted' else 'Rejected' end as cstatus1,cstatus                     
                       from Challengemaster cm 
                       inner join exambookletqmaster ebqm on cm.ExamBookletQMasterID=ebqm.ExamBookletQMasterID
                       inner join exambookletmaster ebm on ebqm.exambookletid=ebm.exambookletid
                       left outer join ChallengePayDetails cpd on cm.CPdID=cpd.CPdID
                       where examid=@examid and cm.CRegno=@regno ";
        if (batchid != "")
        {
            str += " and batchid=@batchid ";
        }
               str += " order by cm.ChallengeID desc";

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
        param[0].Value = examid;
        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[1].Value = regno;
        param[2] = new SqlParameter("@batchid", SqlDbType.Int, 4);
        if (batchid != "")
        {
            param[2].Value = batchid;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
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

    public DataTable rbl_new(string ans)
    {
        string str = "select distinct(ans) from ExamBookLetQMaster where ans not in (@ans)";

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@ans", SqlDbType.Char, 1);
        if (ans == "")
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = ans;
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

    public DataTable getdsssbans(string questionid)
    {
        str = "  select ans from ExamBookLetQMaster where ExamBookLetQMasterID=@ExamBookLetQMasterID ";

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@ExamBookLetQMasterID", SqlDbType.Int);
        if (string.IsNullOrEmpty(questionid))
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = questionid;
        }

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

    //@@@@@@@@@@@@@@@@@@@@@ Transcation for Candidate Details   @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    public int Insert_CandDetails(string CRegno, string ExamBookletQMasterID, string COptID, string Copt2, string Remarks, string Cipaddress, string Cedate, byte[] Cdoc, string ext, string copt3, int CJPId)
    {

        int ChallengeID = 0;
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

                    string insrtqry = @"Insert into ChallengeMaster(CRegno,ExamBookletQMasterID,COptID,Copt2,Remarks,Cipaddress,Cedate,copt3,CJPId) values
                                        (@CRegno,@ExamBookletQMasterID,@COptID,@Copt2,@Remarks,@Cipaddress,@Cedate,@copt3,@CJPId) select SCOPE_IDENTITY()";

                    SqlParameter[] param = new SqlParameter[9];

                    param[0] = new SqlParameter("@CRegno", SqlDbType.VarChar, 50);
                    param[0].Value = CRegno;

                    param[1] = new SqlParameter("@ExamBookletQMasterID", SqlDbType.Int, 4);
                    if (ExamBookletQMasterID == "" || ExamBookletQMasterID == null)
                    {
                        param[1].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[1].Value = Convert.ToInt32(ExamBookletQMasterID);
                    }

                    param[2] = new SqlParameter("@COptID", SqlDbType.Int, 4);
                    if (COptID == "" || COptID == null)
                    {
                        param[2].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[2].Value = Convert.ToInt32(COptID);
                    }

                    param[3] = new SqlParameter("@Copt2", SqlDbType.Char, 1);
                    if (Copt2 == null || Copt2 == "")
                    {
                        param[3].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[3].Value = Convert.ToChar(Copt2);
                    }

                    param[4] = new SqlParameter("@Remarks", SqlDbType.VarChar, 300);
                    param[4].Value = Remarks;                  

                    param[5] = new SqlParameter("@Cipaddress", SqlDbType.VarChar, 50);
                    param[5].Value = Cipaddress;

                    param[6] = new SqlParameter("@Cedate", SqlDbType.DateTime, 8);
                    param[6].Value = Cedate;

                    param[7] = new SqlParameter("@copt3", SqlDbType.VarChar, 20);
                    if (string.IsNullOrEmpty(copt3))
                    {
                        param[7].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[7].Value = copt3;
                    }

                    param[8] = new SqlParameter("@CJPId", SqlDbType.Int, 4);
                    param[8].Value = CJPId; 

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
                    ChallengeID = Convert.ToInt32(command.ExecuteScalar());
                    //command.ExecuteNonQuery();

                    //Insert Bank Doc 
                    string str = " insert into ChallenegeDocMast (ChallengeID,Cdoc,ext) values (@ChallengeID,@Cdoc,@ext)";

                    // string str = "insert into BankDocs (AppID,image,filename,ext,ip,edate) values (@AppID,@image,@filename,@ext,@Ip,@edate)";

                    SqlParameter[] param2 = new SqlParameter[3];


                    param2[0] = new SqlParameter("@ChallengeID", SqlDbType.Int, 4);
                    param2[0].Value = ChallengeID;

                    param2[1] = new SqlParameter("@Cdoc", SqlDbType.Image);
                    param2[1].Value = Cdoc;

                    param2[2] = new SqlParameter("@ext", SqlDbType.VarChar, 10);
                    param2[2].Value = ext;

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
        return ChallengeID;
    }

    //@@@@@@@@@@@@@@@@@@@@@ End Transcation @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


    public int Delete_ChallgeDetails(string ChallengeID)
    {
        string str = @"delete from ChallengeMaster where ChallengeID=@ChallengeID";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@ChallengeID", SqlDbType.Int, 4);
        param[0].Value = ChallengeID;

        string strDoc = " delete from ChallenegeDocMast where ChallengeID=@ChallengeID";
        SqlParameter[] param2 = new SqlParameter[1];
        param2[0] = new SqlParameter("@ChallengeID", SqlDbType.Int, 4);
        param2[0].Value = ChallengeID;

        try
        {
            int id = da.ExecuteParameterizedQuery(str, param);
            int id2 = da.ExecuteParameterizedQuery(strDoc, param2);
            return id2;

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }


    public DataTable GetChallengePdf(string ChallengeID)
    {
        String str = "select ChallengeID,Cdoc,ext from ChallenegeDocMast where ChallengeID=@ChallengeID";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ChallengeID", SqlDbType.Int);
        param[0].Value = ChallengeID;
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

    public int GetCFee()
    {
        String str = "select CAmt from CFeeMaster";
        try
        {
            dt = da.GetDataTable(str);
            int amount = Convert.ToInt32(dt.Rows[0]["CAmt"]);
            return amount;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataTable GetRefDetls(string CRegNo)
    {
        string str = "select top 1 CRegNo,CRID,CBankIfsc,CBankName,CbankBrnch,CAccNo,CbankBrnchCode,Accholdername,TypeofAcc,CBankMICRNo from ChallengeRefdDtls where CRegNo=@CRegNo and (CBankIfsc <> '' or CBankIfsc is not null) and (CAccNo <> '' or CAccNo is not null) order by CRID desc";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@CRegNo", SqlDbType.VarChar, 50);
        param[0].Value = CRegNo;
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

    public int InsertRefDetls(string Cregno, string CBankIfsc, string CBankName, string CbankBrnch, string CAccNo, string CbankBrnchCode, string Accholdername, string TypeofAcc, string CBankMICRNo, string edate, string ip)
    {
        string str = "Insert into ChallengeRefdDtls(Cregno,CBankIfsc,CBankName,CbankBrnch,CAccNo,CbankBrnchCode,Accholdername,TypeofAcc,CBankMICRNo,edate,ip) values(@Cregno,@CBankIfsc,@CBankName,@CbankBrnch,@CAccNo,@CbankBrnchCode,@Accholdername,@TypeofAcc,@CBankMICRNo,@edate,@ip) select SCOPE_IDENTITY()";
        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@Cregno", SqlDbType.VarChar, 50);
        param[0].Value = Cregno;
        param[1] = new SqlParameter("@CBankIfsc", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(CBankIfsc))
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = CBankIfsc;
        }
        
        param[2] = new SqlParameter("@CBankName", SqlDbType.VarChar, 50);
        if (string.IsNullOrEmpty(CBankName))
        {
            param[2].Value = System.DBNull.Value;
        }
        else
        {
            param[2].Value = CBankName;
        }
        
        param[3] = new SqlParameter("@CbankBrnch", SqlDbType.VarChar, 50);
        if (string.IsNullOrEmpty(CbankBrnch))
        {
            param[3].Value = System.DBNull.Value;
        }
        else
        {
            param[3].Value = CbankBrnch;
        }
       
        param[4] = new SqlParameter("@CAccNo", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(CAccNo))
        {
            param[4].Value = System.DBNull.Value;
        }
        else
        {
            param[4].Value = CAccNo;
        }
        
        param[5] = new SqlParameter("@CbankBrnchCode", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(CbankBrnchCode))
        {
            param[5].Value = System.DBNull.Value;
        }
        else
        {
            param[5].Value = CbankBrnchCode;
        }
       
        param[6] = new SqlParameter("@Accholdername", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(Accholdername))
        {
            param[6].Value = System.DBNull.Value;
        }
        else
        {
            param[6].Value = Accholdername;
        }
       
        param[7] = new SqlParameter("@TypeofAcc", SqlDbType.Char, 1);
        if (string.IsNullOrEmpty(TypeofAcc))
        {
            param[7].Value = System.DBNull.Value;
        }
        else
        {
            param[7].Value = TypeofAcc;
        }
      
        param[8] = new SqlParameter("@CBankMICRNo", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(CBankMICRNo))
        {
            param[8].Value = System.DBNull.Value;
        }
        else
        {
            param[8].Value = CBankMICRNo;
        }
        
        param[9] = new SqlParameter("@edate", SqlDbType.DateTime, 8);
        param[9].Value = edate;
        param[10] = new SqlParameter("@ip", SqlDbType.VarChar, 50);
        param[10].Value = ip;
        try
        {
            //int i = da.ExecuteParameterizedQuery(str, param);
            int i = Convert.ToInt32(da.ExecScaler(str, param));
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public int UpdateRefDetls(string Cregno, string CBankIfsc, string CBankName, string CbankBrnch, string CAccNo, string CbankBrnchCode, string Accholdername, string TypeofAcc, string CBankMICRNo, string edate, string ip)
    {
        string str = "update ChallengeRefdDtls set CBankIfsc=@CBankIfsc,CBankName=@CBankName,CbankBrnch=@CbankBrnch,CAccNo=@CAccNo,CbankBrnchCode=@CbankBrnchCode,Accholdername=@Accholdername,TypeofAcc=@TypeofAcc,CBankMICRNo=@CBankMICRNo,edate=@edate,ip=@ip where Cregno=@Cregno";
        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@Cregno", SqlDbType.VarChar, 50);
        param[0].Value = Cregno;
        param[1] = new SqlParameter("@CBankIfsc", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(CBankIfsc))
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = CBankIfsc;
        }

        param[2] = new SqlParameter("@CBankName", SqlDbType.VarChar, 50);
        if (string.IsNullOrEmpty(CBankName))
        {
            param[2].Value = System.DBNull.Value;
        }
        else
        {
            param[2].Value = CBankName;
        }

        param[3] = new SqlParameter("@CbankBrnch", SqlDbType.VarChar, 50);
        if (string.IsNullOrEmpty(CbankBrnch))
        {
            param[3].Value = System.DBNull.Value;
        }
        else
        {
            param[3].Value = CbankBrnch;
        }

        param[4] = new SqlParameter("@CAccNo", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(CAccNo))
        {
            param[4].Value = System.DBNull.Value;
        }
        else
        {
            param[4].Value = CAccNo;
        }

        param[5] = new SqlParameter("@CbankBrnchCode", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(CbankBrnchCode))
        {
            param[5].Value = System.DBNull.Value;
        }
        else
        {
            param[5].Value = CbankBrnchCode;
        }

        param[6] = new SqlParameter("@Accholdername", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(Accholdername))
        {
            param[6].Value = System.DBNull.Value;
        }
        else
        {
            param[6].Value = Accholdername;
        }

        param[7] = new SqlParameter("@TypeofAcc", SqlDbType.Char, 1);
        if (string.IsNullOrEmpty(TypeofAcc))
        {
            param[7].Value = System.DBNull.Value;
        }
        else
        {
            param[7].Value = TypeofAcc;
        }

        param[8] = new SqlParameter("@CBankMICRNo", SqlDbType.VarChar, 20);
        if (string.IsNullOrEmpty(CBankMICRNo))
        {
            param[8].Value = System.DBNull.Value;
        }
        else
        {
            param[8].Value = CBankMICRNo;
        }
        param[9] = new SqlParameter("@edate", SqlDbType.DateTime, 8);
        param[9].Value = edate;
        param[10] = new SqlParameter("@ip", SqlDbType.VarChar, 50);
        param[10].Value = ip;
        try
        {
            int i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public int updateRfdt(string CRID, string ChallengeID)
    {
        string str = "update ChallengeMaster set CRID=@CRID where ChallengeID in (" + ChallengeID + ")";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@CRID", SqlDbType.Int, 4);
        param[0].Value = CRID;
        try
        {
            int i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int updatePayId(int CPdID, string ChallengeID)
    {
        string str = "update ChallengeMaster set CPdID=@CPdID where ChallengeID in (" + ChallengeID + ")";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@CPdID", SqlDbType.Int, 4);
        param[0].Value = CPdID;
        try
        {
            int i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int InsertPayDtls(string Cregno, string amount)
    {
        string str = "insert into ChallengePayDetails(CRegno,CAmt,transdate) values (@Cregno,@CAmt,@transDate) select SCOPE_IDENTITY() ";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Cregno", SqlDbType.VarChar, 50);
        param[0].Value = Cregno;
        param[1] = new SqlParameter("@CAmt", SqlDbType.Decimal);
        param[1].Value = amount;
        param[2] = new SqlParameter("@transDate", SqlDbType.DateTime);
        param[2].Value = Utility.formatDatewithtime(DateTime.Now);
        try
        {
            int temp = Convert.ToInt32(da.ExecScaler(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataTable getdataforpayment(int cpid)
    {

        string str = @"select cpd.cregno,cpd.camt,r.name,r.mobileno from ChallengePayDetails cpd 
                      inner join registration r on cpd.cregno=r.rid where cpd.cpdid=@cpid";


        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@cpid", SqlDbType.Int);
        param[0].Value = cpid;

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



    public int updatePaydtls(string CPdID, string orderno, string status, string paymode, string bankcode, string transdate)
    {
        string str = "update ChallengePayDetails set Orderno=@orderno,status=@status,transmode=@paymode,bankcode=@bankcode,banktrandate=@transdate where CPdID=@CPdID";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@orderno", SqlDbType.VarChar, 100);
        param[0].Value = orderno;

        param[1] = new SqlParameter("@status", SqlDbType.VarChar, 10);
        param[1].Value = status;

        param[2] = new SqlParameter("@paymode", SqlDbType.VarChar, 10);
        param[2].Value = paymode;

        param[3] = new SqlParameter("@bankcode", SqlDbType.VarChar, 20);
        param[3].Value = bankcode;

        param[4] = new SqlParameter("@transdate", SqlDbType.DateTime);
        if (transdate == "")
        {
            param[4].Value = System.DBNull.Value;
        }
        else
        {
            param[4].Value = transdate;
        }

        param[5] = new SqlParameter("@CPdID", SqlDbType.Int);
        param[5].Value = CPdID;


        try
        {
            int temp = da.ExecuteParameterizedQuery(str, param);
            return temp;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int getjpid(string examid, string bookletcode, string qno,string batchid)
    {
        string str = @"Select CJPID from ChallengeJPMaster where Examid=@examid and QNoSet" + bookletcode.Trim() + "=@qno";
        if (batchid != "")
        {
            str += " and batchid=@batchid ";
        }
        int CJPId = 0;

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@examid", SqlDbType.Int);
        param[0].Value = examid;

        param[1] = new SqlParameter("@qno", SqlDbType.Int);
        param[1].Value = qno;

        param[2] = new SqlParameter("@batchid", SqlDbType.Int, 4);
        if (batchid != "")
        {
            param[2].Value = batchid;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count > 0)
            {
                CJPId = Convert.ToInt32(dt.Rows[0]["CJPID"]);            
            }
            return CJPId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool checkexamattendance(string examid, string regno,string batchid)
    {
        bool wpresent = false;
        string str = "select rollno from AttendanceMaster where examid=@examid and regno=@regno and ispresent='Y'";
        if (batchid != "")
        {
            str += " and batchid=@batchid ";
        }
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@examid", SqlDbType.Int);
        param[0].Value = examid;

        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[1].Value = regno;

        param[2] = new SqlParameter("@batchid", SqlDbType.Int, 4);
        if (batchid != "")
        {
            param[2].Value = batchid;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }

        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count > 0)
            {
                wpresent = true;
            }
            return wpresent;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool CheckSchedule(string examid,string batchid)
    {
        string totime = DateTime.Now.ToString("HH:mm");
        bool IsReleased=false;

        string str = @"select csmid,timeto from ChallengeScheduleMaster where examid=@examid and convert(varchar,datefrom,111) <= CONVERT(varchar,getdate(),111) 
                    and (convert(varchar,dateto,111)> convert(varchar,getdate(),111) or 
                    ( convert(varchar,dateto,111)= convert(varchar,getdate(),111) and ((timeto>=@totime and timeto is not null) or timeto is null))) 
                    and Released ='Y'";
        if (batchid != "")
        {
            str += " and batchid=@batchid ";
        }
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@examid", SqlDbType.Int);
        param[0].Value = examid;
        param[1] = new SqlParameter("@totime", SqlDbType.VarChar,10);
        param[1].Value = totime;
        param[2] = new SqlParameter("@batchid", SqlDbType.Int, 4);
        if (batchid != "")
        {
            param[2].Value = batchid;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count > 0)
            {
                IsReleased = true;
            }
            return IsReleased;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable CheckScheduletime(string examid)
    {
       // bool IsReleased = false;
        string str = "select csmid,timeto from ChallengeScheduleMaster where examid=@examid and convert(varchar,datefrom,111) <= CONVERT(varchar,getdate(),111) and convert(varchar,dateto,111)>= convert(varchar,getdate(),111) and Released ='Y'";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@examid", SqlDbType.Int);
        param[0].Value = examid;

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
public DataTable GetSuccessChallengemast_Details(string examid, string regno, string cpdid,string batchid)
    {
        string str = @"select cm.ExamBookletQMasterID,ebm.exambookletid,cm.CRegno,cm.ChallengeID,cm.Remarks,
                       cm.CPdID,bookletcode,questionno,ans,
                       case coptid when 1 then  cm.copt2 when 2 then 'Multiple Answers : '+copt3 
                       when 3 then 'None of the Options given' end as sanswer, remarks,cm.CPdID,
                       case cpd.status when 'SUCCESS' then 'Paid' else 'Pending' end as feestatus,cpd.status,cpd.CAmt,cpd.Orderno,CONVERT(VARCHAR,cpd.banktrandate,113) as banktrandate
                       from Challengemaster cm 
                       inner join exambookletqmaster ebqm on cm.ExamBookletQMasterID=ebqm.ExamBookletQMasterID
                       inner join exambookletmaster ebm on ebqm.exambookletid=ebm.exambookletid
                       inner join ChallengePayDetails cpd on cm.CPdID=cpd.CPdID
                       where examid=@examid and cm.CRegno=@regno and cpd.CPdID = @cpdid and cpd.status ='SUCCESS' ";
        if (batchid != "")
        {
            str += " and batchid=@batchid ";
        }
    str += " order by cm.ChallengeID desc";

//        string str = @"select cm.ExamBookletQMasterID,ebm.exambookletid,cm.CRegno,cm.ChallengeID,cm.Remarks,
//                       cm.CPdID,bookletcode,questionno,ans,
//                       case coptid when 1 then 'Options Others than the Answer Key : '+ cm.copt2 when 2 then 'Multiple Answers : '+copt3 
//                       when 3 then 'None of the Options given' end as sanswer, remarks,cm.CPdID,
//                       case cpd.status when 'SUCCESS' then 'Paid' else 'Pending' end as feestatus,cpd.status,cpd.CAmt,cpd.Orderno,CONVERT(VARCHAR(11),cpd.banktrandate,106) as banktrandate
//                       from Challengemaster cm 
//                       inner join exambookletqmaster ebqm on cm.ExamBookletQMasterID=ebqm.ExamBookletQMasterID
//                       inner join exambookletmaster ebm on ebqm.exambookletid=ebm.exambookletid
//                       inner join ChallengePayDetails cpd on cm.CPdID=cpd.CPdID
//                       where examid=@examid and cm.CRegno=@regno and cpd.CPdID = @cpdid order by cm.ChallengeID desc";

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
        param[0].Value = examid;
        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[1].Value = regno;
        param[2] = new SqlParameter("@cpdid", SqlDbType.Int, 4);
        param[2].Value = cpdid;
        param[3] = new SqlParameter("@batchid", SqlDbType.Int, 4);
        if (batchid != "")
        {
            param[3].Value = batchid;
        }
        else
        {
            param[3].Value = System.DBNull.Value;
        }
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
public DataTable getExamTier(string examid)
    {

        string str = @" select is2tierexam,is3tierexam,isPETmarksreq,em.examtypeid ,tier from examMast em 
                       inner join examtypemaster etm on em.examtypeid=etm.examtypeid where examid=@examid";


        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@examid", SqlDbType.Int);
        param[0].Value = examid;

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

public DataTable getCSchedule(string examid,string batchid)
{
    str = "select RevReleased,sletterissued from ChallengeScheduleMaster where examid=@examid ";
    if (batchid != "")
    {
        str += " and batchid=@batchid ";
    }
    SqlParameter[] param = new SqlParameter[2];
    param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
    param[0].Value = examid;
    param[1] = new SqlParameter("@batchid", SqlDbType.Int, 4);
    if (batchid != "")
    {
        param[1].Value = batchid;
    }
    else
    {
        param[1].Value = System.DBNull.Value;
    }
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
public DataTable GetFinalAnsKey(string examid, string batchid,string QuestionNo)
    {
        str = @"select ebqm.QuestionNo,case a.RevAns when 'N' then 'Deleted' else isnull(a.RevAns+' *',ebqm.Ans)  end as answer from
               (select  QuestionNo, Ans from ExamBookLetQMaster where ExamBookLetID = (select min(ExamBookLetID) from ExamBookLetMaster where examid=@examid ";
        if (batchid != "")
        {
            str += " and batchid=@batchid ";
        }
        str += @" )) ebqm left outer join
               (select cjpm.qnoseta,crak.RevAns from ChallengeRevAnsKey  crak
               inner join ChallengeJPMaster cjpm on crak.CJPID=cjpm.CJPID
               where crak.examid=@examid ";
        if (batchid != "")
        {
            str += " and crak.batchid=@batchid ";
        }
        str+= " )a on ebqm.QuestionNo=a.qnoseta ";

        if (QuestionNo != "")
        {
            str += " where ebqm.QuestionNo=@QuestionNo ";
        }
       
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ExamID", SqlDbType.Int);
        if (examid == "")
        {
            param[0].Value = DBNull.Value;
        }
        else
        {
            param[0].Value = Convert.ToInt32(examid);
        }
        param[1] = new SqlParameter("@batchid", SqlDbType.Int);
        if (batchid == "")
        {
            param[1].Value = DBNull.Value;
        }
        else
        {
            param[1].Value = Convert.ToInt32(batchid);
        }
        param[2] = new SqlParameter("@QuestionNo", SqlDbType.Int);
        if (QuestionNo == "")
        {
            param[2].Value = DBNull.Value;
        }
        else
        {
            param[2].Value = Convert.ToInt32(QuestionNo);
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
    public DataTable getrevanskeyExam(string regno)
    {
        str = @"select examid,examdtl from (
                select cast (examid as varchar) as examid , convert(varchar,dateofexam,103) +' ('+ timeofexam+ ')' as examdtl ,dateofexam
                from exammast where examid in (select examid from ChallengeScheduleMaster where RevReleased='Y')  
                 and examid in 
				(select examid from attendancemaster where regno=@regno and ispresent='Y')
                union
                select cast(examid as varchar)+':'+cast(batchid as varchar) as examid , 
				CONVERT(VARCHAR(10),examdate,103)+'('+ examtime +') :: '+ batchname as examdtl , 
				examdate as dateofexam
                from BatchMaster where batchid in (select batchid from ChallengeScheduleMaster where RevReleased='Y')
                and 
				batchid in 
				(select batchid from attendancemaster where regno=@regno and ispresent='Y')
                )a
                order by dateofexam desc ";
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




