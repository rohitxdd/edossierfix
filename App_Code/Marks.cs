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
/// Summary description for Marks
/// </summary>
public class Marks
{
    string str;
    DataTable dt = new DataTable();
    ApplicantData da = new ApplicantData();

	public Marks()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getexamtoViewMarks(string regno)
    {


//        str = @"select  em.examid,isnull(convert(varchar,dateofexam,103),'Batchwise Exam') +'(' + examtype +')' as examdtl from exammast em
//				inner join examtypemaster etm on em.examtypeid=etm.examtypeid
//				 where (MarksReleased='Y' or isPETmarksreq ='N') and
//                 em.examid in (select examid from (select jid,acstatid from JobApplication where regno=@regno
//				 union select jid,acstatid from dsssbonline_recdapp.dbo.JobApplication where regno=@regno) 
//				 jap inner join job_advt ja on jap.jid=ja.jid and jap.acstatid is not null ) 
//				 union
//				 select em.examid,convert(varchar,examdate,103) +'(' + examtype +')' as examdtl from exammast em
//				 inner join BatchMaster bm on em.examid=bm.examid
//				 inner join BatchAllocationStatus bas on bm.batchid=bas.batchid
//				 inner join examtypemaster etm on em.examtypeid=etm.examtypeid
//				 where bas.MarksReleased='Y'  and
//                 bas.batchid in (select batchid from (select jid,acstatid,batchid from JobApplication where regno=@regno
//				 union select jid,acstatid,batchid from dsssbonline_recdapp.dbo.JobApplication where regno=@regno) 
//				 jap inner join job_advt ja on jap.jid=ja.jid and jap.acstatid is not null) 
//				 order by examid desc ";
        str = @"select  em.examid,isnull(convert(varchar,dateofexam,103),'Batchwise Exam') +'(' + examtype +')' as examdtl from exammast em
				inner join examtypemaster etm on em.examtypeid=etm.examtypeid
				 where (MarksReleased='Y' or isPETmarksreq ='N') and
                 em.examid in (select examid from (select jid,acstatid from JobApplication where regno=@regno
				 union select jid,acstatid from dsssbonline_recdapp.dbo.JobApplication where regno=@regno) 
				 jap inner join job_advt ja on jap.jid=ja.jid and jap.acstatid is not null ) 
				 union
				 select em.examid,convert(varchar,examdate,103) +'(' + examtype +')' as examdtl from exammast em
				 inner join BatchMaster bm on em.examid=bm.examid
				 inner join BatchAllocationStatus bas on bm.batchid=bas.batchid
				 inner join examtypemaster etm on em.examtypeid=etm.examtypeid
				 where bas.MarksReleased='Y'  and
                 bas.batchid in (select batchid from (select jid,acstatid,batchid from JobApplication where regno=@regno
				 union select jid,acstatid,batchid from dsssbonline_recdapp.dbo.JobApplication where regno=@regno) 
				 jap inner join job_advt ja on jap.jid=ja.jid and jap.acstatid is not null) 
				 union
				 select  em.examid,isnull(convert(varchar,dateofexam,103),'Batchwise Exam') +'(' + examtype +')' as examdtl from exammast em
				inner join examtypemaster etm on em.examtypeid=etm.examtypeid
				 where (MarksReleased='Y' or isPETmarksreq ='N') and
                 em.examid in (select examid from ResultVerification rv inner join Applicant_result ar on rv.rvid=ar.rvid
				 where ar.regno=@regno and ar.flag='V' ) 

				 order by examid desc ";
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

    public DataTable GetCandDetails(string regno)
    {
        str = @"select name,convert(varchar,birthdt,103) dob from registration where rid=@rid";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
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


    public DataTable GetMarks(string FlagPostApplied, string examid, string regno, string tierval)
    {

       // if (FlagPostApplied == "S")
       // {
        if (tierval == "1")
        {

            //            str = @"select rollno,finalS1Marks,finalS2Marks,finaltotalmarks from ExamMarks where examid=@examid and (RollNo in (select RollNo from applicantcenter 
            //                    where applid in (select applid from jobapplication where regno=@regno union select applid 
            //                    from dsssbonline_recdapp.dbo.jobapplication where regno=@regno)) 
            //                    or RollNo in (select RollNo from applicantcenter where regno=@regno and examid=@examid))";


            str = @"select em.rollno,finalS1Marks,finalS2Marks,finaltotalmarks from ExamMarks em

inner join ApplicantCenter ac on em.rollno = ac.rollno

inner join (select applid, regno from jobapplication union select applid, regno from dsssbonline_recdapp.dbo.jobapplication) as ja on ac.applid = ja.applid or ja.regno = ac.regno

where  em.examid=@examid and ja.regno = @regno";

        }
        else
        {
           //str = @"select rollno,finalS1Marks,finalS2Marks,finaltotalmarks from ExamMarks where examid=@examid and (RollNo in (select RollNo from Applicant_result 
           //       ar inner join ResultVerification rv on ar.rvid=rv.rvid and examid=@examid                    where regno=@regno )) ";
           

//Modified on Dated: 18-05-2023       
//           str = @"select rollno,finalS1Marks,finalS2Marks,finaltotalmarks from ExamMarks where examid=@examid and (RollNo in (select RollNo from applicantcenter where examid=@examid and
//                   applid in (select applid from Applicant_result 
//                   ar inner join ResultVerification rv on ar.rvid=rv.rvid and examid=@examid
//                   where regno=@regno ) or regno=@regno ) or rollno in (select rollno from Applicant_result 
//                   ar inner join ResultVerification rv on ar.rvid=rv.rvid and examid=@examid
//                   where regno=@regno) ) ";
            str = @"select rollno,finalS1Marks,finalS2Marks,finaltotalmarks from ExamMarks where examid=@examid and (RollNo in (select RollNo from applicantcenter where examid=@examid and
                   applid in (select applid from Applicant_result 
                   ar inner join ResultVerification rv on ar.rvid=rv.rvid and examid=@examid
                   where regno=@regno ) or regno=@regno ) --or rollno in (select rollno from Applicant_result ar inner join ResultVerification rv on ar.rvid=rv.rvid and examid=@examid where regno=@regno) 
				   )";
/*
		str = @"select ExamMarks.rollno,finalS1Marks,finalS2Marks,finaltotalmarks from ExamMarks 
		inner join applicantcenter on ExamMarks.RollNo=ApplicantCenter.rollno
		inner join Applicant_result on ExamMarks.RollNo=Applicant_result.rollno
		inner join ResultVerification on ResultVerification.rvid=Applicant_result.rvid
		where ExamMarks.ExamID=@examid and Applicant_result.regno=@regno";


		str=@"select rollno,finalS1Marks,finalS2Marks,finaltotalmarks from ExamMarks 
		where  
		RollNo = 
		(
		select RollNo from applicantcenter where examid=@examid and
		applid = 
		(
		select applid from Applicant_result 
		ar inner join ResultVerification rv on ar.rvid=rv.rvid 
		where regno=@regno
		) 
		) and examid=@examid
		or rollno = 
		(
		select rollno from Applicant_result 
		ar inner join ResultVerification rv on ar.rvid=rv.rvid 
		where regno=@regno 
		) 
		and examid=@examid";
*/

        }

     //  }
       // else
        //{
          //  str = @"select rollno,marks from ExamMarks where examid=@examid and RollNo in (select RollNo from applicantcenter where regno=@regno) ";
        //}
        

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        if (regno == "")
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = regno;
        }

        param[1] = new SqlParameter("@examid", SqlDbType.Int,4);
        if (examid == "")
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = Convert.ToInt32(examid);
        }

       

        DataTable dt = da.GetDataTableQry(str, param);
        return dt;
    }





    public DataTable GetPostCode(string examid, string regno, string tierval)
    {
        
        //string condition = "";
        if (tierval == "1")
        {
            str = @" select postcode+' : '+jobtitle as post,ja.jid,jap.applid from job_advt ja inner join jobapplication jap on ja.jid = jap.jid 
               where regno=@regno and acstatid is not null and examid=@examid
                   UNION
                select postcode+' : '+jobtitle as post,ja.jid,jap.applid from job_advt ja inner join dsssbonline_recdapp.dbo.jobapplication jap on ja.jid = jap.jid 
               where regno=@regno and acstatid is not null and examid=@examid
                ";
        }
        else
        {
            str = @" select ja.postcode+' : '+jobtitle as post,ja.jid,ar.applid from ResultVerification rv inner join Applicant_result ar on rv.rvid=ar.rvid and rv.examid=@examid
			   inner join job_advt ja on rv.jid=ja.jid where ar.regno=@regno order by ja.jid";
        }
 
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
        param[0].Value = examid;
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


    public bool checkexamattendance(string examid, string regno)
    {
        bool wpresent = false;
        string str = "select rollno from AttendanceMaster where examid=@examid and regno=@regno and ispresent='Y'";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@examid", SqlDbType.Int);
        param[0].Value = examid;

        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[1].Value = regno;

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
    public string getattenrollno(string examid, string regno)
    {
        string rollno = "";
        string str = "select rollno from AttendanceMaster where examid=@examid and regno=@regno and ispresent='Y'";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@examid", SqlDbType.Int);
        param[0].Value = examid;

        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[1].Value = regno;

        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count > 0)
            {
                rollno = dt.Rows[0]["rollno"].ToString();
            }
            return rollno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataTable getrollno(string rollno,string tier)
    {
        str = @"select rollno,postcode from  applicant_result where rollno=@rollno and tier=@tier and flag='V' and rvid in (select rvid from ResultVerification where nextexam is null)";
      
       
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@rollno", SqlDbType.BigInt);
        if (rollno == null)
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = Convert.ToInt64(rollno);
        }

        param[1] = new SqlParameter("@tier", SqlDbType.Int);
        if (tier == null)
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = Convert.ToInt32(tier);
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
    public DataTable getrollno_nextexam(string rollno, string tier)
    {
        // str = @"select rollno,postcode from  applicant_result where rollno=@rollno and tier=@tier and flag='V' and rvid in (select rvid from ResultVerification where nextexam is null)";

        str = @"select rollno,postcode,rv.tier as nextexamtier from  applicant_result ar
                inner join ResultVerification rv on ar.rvid=rv.rvid where rollno=@rollno and ar.tier=@tier and flag='V' and nextexam in ('Y','A') ";
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@rollno", SqlDbType.BigInt);
        if (rollno == null)
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = Convert.ToInt64(rollno);
        }

        param[1] = new SqlParameter("@tier", SqlDbType.Int);
        if (tier == null)
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = Convert.ToInt32(tier);
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
    public DataTable getrollno_notshortlisted(string rollno, string tier)
    {
        str = @"select rollno,postcode from  applicant_result where rollno=@rollno and tier=@tier and flag='V' and rvid is not null";


        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@rollno", SqlDbType.BigInt);
        if (rollno == null)
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = Convert.ToInt64(rollno);
        }

        param[1] = new SqlParameter("@tier", SqlDbType.Int);
        if (tier == null)
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = Convert.ToInt32(tier);
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
    public DataTable isresultuploaded(string jid, string tier)
    {
        str = @"select rollno,postcode,flag from  applicant_result where  postcode in (select postcode from job_advt where jid=@jid) and rvid is not null and tier=@tier";


        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@jid", SqlDbType.Int);
        if (jid == null)
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = Convert.ToInt32(jid);
        }

        param[1] = new SqlParameter("@tier", SqlDbType.Int);
        if (tier == null)
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = Convert.ToInt32(tier);
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
    //public string getApplid(string Applid)
    //{

      
    //    //Get JID from JobApplication on basis of Applid

    //    string str1 = @" select jid from JobApplication where applid=@applid ";

    //    SqlParameter[] param1 = new SqlParameter[1];

    //    param1[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
    //    if (Applid == null)
    //    {
    //        param1[0].Value = System.DBNull.Value;
    //    }
    //    else
    //    {
    //        param1[0].Value = Applid;
    //    }
    //    int Jid = Convert.ToInt32(da.ExecScaler(str1, param1));

    //    return Convert.ToString(Jid);
    //}



    //public string getApplid(string regno)
    //{

    //    //Get applid from applicantCenter on basis of regno
    //    string str = @"select applid from applicantCenter where regno=@regno ";

    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
    //    if (regno == "")
    //    {
    //        param[0].Value = System.DBNull.Value;
    //    }
    //    else
    //    {
    //        param[0].Value = regno;
    //    }

    //    int Applid = Convert.ToInt32(da.ExecScaler(str, param));


    //    //Get JID from JobApplication on basis of Applid

    //    string str1 = @" select jid from JobApplication where applid=@applid ";

    //    SqlParameter[] param1 = new SqlParameter[1];

    //    param1[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
    //    if (Applid == null)
    //    {
    //        param1[0].Value = System.DBNull.Value;
    //    }
    //    else
    //    {
    //        param1[0].Value = Applid;
    //    }
    //    int Jid = Convert.ToInt32(da.ExecScaler(str1, param1));

    //    return Convert.ToString(Jid);
    //}

      public DataTable GetexamidforAdmitCC(string regno, string cflg)
    {

        #region 

//        //        str = @" select em.examid, convert(varchar,dateofexam,103) +' ('+ timeofexam+ ')' as examdtl ,convert(varchar(10),GETDATE(),120)
////                from exammast em inner join AdmitCardConsent_Schedule ACCS on em.examid=accs.examid where accs.casverified='Y' and
////                accs.cafromdate < = convert(varchar(10),GETDATE(),120) and accs.catodate>=convert(varchar(10),GETDATE(),120) 
////               and em.examid  in (select examid from job_advt where jid in (select jid from jobapplication where regno=@regno and acstatid ='1' 
////               and (acconsent <>'Y' or acconsent is null)))               
////               order by em.examid desc ";



        str = @" select em.examid, convert(varchar,dateofexam,103) +' ('+ timeofexam+ ')' as examdtl ,convert(varchar(10),GETDATE(),120),
                CONVERT(varchar(12),ACCS.cafromdate,120) as cafromdate,CONVERT(varchar(12),ACCS.catodate,120) as catodate,em.radmitcard,emph2.radmitcard as radmitcardphase2,
                acconsent,acconsent_phase2
                from exammast em 
                inner join job_advt ja on em.examid=ja.examid
                inner join jobapplication jap on ja.jid=jap.jid and regno=@regno and acstatid='1'
                inner join AdmitCardConsent_Schedule ACCS on em.examid=accs.examid
                left outer join examMast_Phase2 emph2 on em.examid=emph2.examid
                where accs.casverified='Y' and dateofexam >= convert(varchar(10),GETDATE(),120) ";

        if (cflg == "1")
        {
            str += @"and ((accs.casverified='Y' and (em.radmitcard <>'Y' or em.radmitcard is null)                
                and accs.cafromdate < = convert(varchar(10),GETDATE(),120) and accs.catodate>=convert(varchar(10),GETDATE(),120) )
                or (accs.casverifiedphase2='Y' and em.radmitcard ='Y'               
                and accs.cafromdatephase2 < = convert(varchar(10),GETDATE(),120) and accs.catodatephase2>=convert(varchar(10),GETDATE(),120) )
                and accs.totimephase2>=CONVERT (varchar(5),getdate(),108))         
                and (acconsent <>'Y' or acconsent is null)";
        }
        //else
        //{

        //    str += "))";
        //}

        str += "order by em.examid desc";

        #endregion
  

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
      public DataTable GetexamidforAdmitCC(string regno, string cflg,string examid)
      {

          #region

          //        //        str = @" select em.examid, convert(varchar,dateofexam,103) +' ('+ timeofexam+ ')' as examdtl ,convert(varchar(10),GETDATE(),120)
          ////                from exammast em inner join AdmitCardConsent_Schedule ACCS on em.examid=accs.examid where accs.casverified='Y' and
          ////                accs.cafromdate < = convert(varchar(10),GETDATE(),120) and accs.catodate>=convert(varchar(10),GETDATE(),120) 
          ////               and em.examid  in (select examid from job_advt where jid in (select jid from jobapplication where regno=@regno and acstatid ='1' 
          ////               and (acconsent <>'Y' or acconsent is null)))               
          ////               order by em.examid desc ";



          str = @" select em.examid, convert(varchar,dateofexam,103) +' ('+ timeofexam+ ')' as examdtl ,convert(varchar(10),GETDATE(),120),
                CONVERT(varchar(12),ACCS.cafromdate,120) as cafromdate,CONVERT(varchar(12),ACCS.catodate,120) as catodate,em.radmitcard,emph2.radmitcard as radmitcardphase2,
                acconsent,acconsent_phase2
                from exammast em 
                inner join job_advt ja on em.examid=ja.examid
                inner join jobapplication jap on ja.jid=jap.jid and regno=@regno and acstatid='1'
                inner join AdmitCardConsent_Schedule ACCS on em.examid=accs.examid
                left outer join examMast_Phase2 emph2 on em.examid=emph2.examid
                where accs.casverified='Y' and dateofexam >= convert(varchar(10),GETDATE(),120) and em.examid=@examid ";

          if (cflg == "1")
          {
              str += @"and ((accs.casverified='Y' and (em.radmitcard <>'Y' or em.radmitcard is null)                
                and accs.cafromdate < = convert(varchar(10),GETDATE(),120) and accs.catodate>=convert(varchar(10),GETDATE(),120) )
                or (accs.casverifiedphase2='Y' and em.radmitcard ='Y'               
                and accs.cafromdatephase2 < = convert(varchar(10),GETDATE(),120) and accs.catodatephase2>=convert(varchar(10),GETDATE(),120) )
                and accs.totimephase2>=CONVERT (varchar(5),getdate(),108))         
                and (acconsent <>'Y' or acconsent is null)";
          }
          //else
          //{

          //    str += "))";
          //}

          str += "order by em.examid desc";

          #endregion


          SqlParameter[] param = new SqlParameter[2];
          param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
          if (regno == "")
          {
              param[0].Value = 0;
          }
          else
          {
              param[0].Value = regno;
          }
          param[1] = new SqlParameter("@examid", SqlDbType.Int);
          if (examid == "")
          {
              param[1].Value = System.DBNull.Value;
          }
          else
          {
              param[1].Value = examid;
          }
          DataTable dt = da.GetDataTableQry(str, param);
          return dt;
      }


    public DataTable GetVerifiedID(string Examid, string regno, string flagphase)
    {

        if (flagphase == "1")
        {

            str = @" select  admicc.verificationid,convert(varchar(12),adccs.treleasedate,103) as releasedate,CONVERT(varchar(12),adccs.cafromdate,120) as cafromdate,
                 CONVERT(varchar(12),adccs.catodate,120) as catodate ,totimephase2 ";
                 
        }
        else
        {
            str = @"select  admicc.verificationid,convert(varchar(12),adccs.treleasedatephase2,103) as releasedate,CONVERT(varchar(12),adccs.cafromdatephase2,120) as cafromdate,
                     CONVERT(varchar(12),adccs.catodatephase2,120) as catodate,totimephase2 ";
           
        }
         str+=@" from AdmitCardConsent_Schedule adccS
                 left outer join AdmitCConsentRequest admicc  on admicc.examid=adccs.examid and regno=@regno 
                 where adccS.examid=@examid  ";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[0].Value = regno;
        

        param[1] = new SqlParameter("@examid", SqlDbType.Int, 4);
        if (Examid == "")
        {
            param[1].Value = 0;
        }
        else
        {
            param[1].Value = Convert.ToInt32(Examid);
        }

        DataTable dt = da.GetDataTableQry(str, param);
        return dt;
    }
}
