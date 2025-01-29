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
/// Summary description for eAdmitCard
/// </summary>
public class eAdmitCard
{
	public eAdmitCard()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    string str;
    
    DataTable dt = new DataTable();
    ApplicantData da = new ApplicantData();
    public DataTable get_exam_list(string flagfromboard,string chkprovisional)
    {
        try
        {
            string str = "";
            
            if (flagfromboard == "Y")
            {
                str = @"select distinct postcode,JobTitle, dateofexam,jid from admitcarddata
                        where examdate >=convert(varchar(10),GETDATE(),120) and is2tierexam is null and is3tierexam is null 
                        order by dateofexam,postcode,JobTitle,jid ";
            }
            else
            {

                str = @"select postcode,JobTitle,convert(varchar,dateofexam,103) dateofexam,jid  from Job_Advt ja
                        inner join examMast em on ja.examid=em.examid
                        where (exampostponed <>'Y' or  exampostponed is null) and
                         radmitcard='Y' and  dbo.wadmitcarddownload(em.examid,@chkprovisional)='Y'
						 union
						 select postcode,JobTitle,convert(varchar,dateofexam,103) dateofexam,jid  from Job_Advt ja
                         inner join PartiallyCancelExam pce on ja.examid=pce.examid
                          inner join examMast em on em.examid=pce.newexamid
                        where (exampostponed <>'Y' or  exampostponed is null) and
                         radmitcard='Y' and  dbo.wadmitcarddownload(pce.newexamid,@chkprovisional)='Y' 
						 union
						 select postcode,JobTitle,convert(varchar,bm.examdate,103) dateofexam,jid  from Job_Advt ja
                        inner join examMast em on ja.examid=em.examid
						inner join BatchMaster bm on em.examid=bm.examid
                        inner join BatchAllocationStatus bas on bm.batchid=bas.BatchId
                        where (exampostponed <>'Y' or  exampostponed is null) 
						and  dbo.wadmitcarddownload_batchwise(em.examid,bas.batchid,@chkprovisional)='Y' and bas.radmitcard='Y'
                        order by dateofexam,postcode,JobTitle ";

            }
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@chkprovisional", SqlDbType.Char,1);
            if (chkprovisional == "Y")
            {
                param[0].Value = chkprovisional;
            }
            else
            {
                param[0].Value = DBNull.Value;
            }
            dt = da.GetDataTableQry(str,param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_exam_list_2tier(string flagfromboard)
    {
        try
        {
            string str = "";
            if (flagfromboard == "Y")
            {
                str = @"select distinct postcode,JobTitle,dateofexam,jid from admitcarddata 
                       where examdate >=GETDATE() and is2tierexam='Y'
                       order by dateofexam,postcode,JobTitle,jid ";
            }
            else
            {
                str = @"select postcode,JobTitle,convert(varchar,dateofexam,103) dateofexam,rv.jid,JobTitle+' ('+postcode+')' as post from ResultVerification rv 
                        inner join examMast em on rv.examid =em.examid 
                        inner join job_advt ja on rv.jid=ja.jid
                        where (exampostponed <>'Y' or  exampostponed is null) and dbo.wadmitcarddownload(em.examid,'')='Y'
                        and radmitcard='Y' and tier=2 
                        order by dateofexam,cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int) ";
            }

            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_exam_list_2tier_test(string flagfromboard)
    {
        try
        {

             string str = "";
             if (flagfromboard == "Y")
             {
                 str = @"select distinct postcode,JobTitle, dateofexam,jid,examid from admitcarddata 
                       where examdate >=GETDATE() and is2tierexam='A'
                       order by dateofexam,postcode,JobTitle,jid,examid ";
             }
             else
             {
                  str = @"select distinct postcode,JobTitle,bm.examid,
                            rv.jid,cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int),JobTitle+' ('+postcode+')' as post from ResultVerification rv 
                            inner join examMast em on rv.examid =em.examid 
                            inner join job_advt ja on rv.jid=ja.jid
                            inner join BatchMaster bm on em.examid =bm.examid
                            where
                            dateadd(hh,10,convert(datetime,examdate))  >=GETDATE()
                            and radmitcard='Y' and (exampostponed <>'Y' or  exampostponed is null) and tier=2 
                            --and dateadd(day, -2, examdate) >= getdate()
                            order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int) ";
             }

            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_exam_list_1tier_test(string flagfromboard)
    {
        try
        {
            string str = "";
           if (flagfromboard == "Y")
            {
                str = @"select distinct postcode,JobTitle, dateofexam,jid,examid from admitcarddata
                        where examdate >=convert(varchar(10),GETDATE(),120) and is2tierexam is null and is3tierexam is null 
                        order by dateofexam,postcode,JobTitle,jid ";
            }
            else
            {


                str = @"select distinct postcode,JobTitle,bm.examid,
                            ja.jid,cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int)  from Job_Advt ja
                        inner join examMast em on ja.examid=em.examid
                          inner join BatchMaster bm on em.examid =bm.examid
                        where dateadd(hh,10,convert(datetime,examdate))  >=GETDATE()
                            and radmitcard='Y' and (exampostponed <>'Y' or  exampostponed is null) and examtypeid='6'
                            --and dateadd(day, -2, examdate) >= getdate()
                            order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int)";

            }
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_exam_list_3tier(string flagfromboard)
    {
        try
        {

                string str = "";
                if (flagfromboard == "Y")
                {
                    str = @"select distinct postcode,JobTitle,dateofexam,jid from admitcarddata 
                                    where examdate >=GETDATE() and is3tierexam='Y'
                                    order by dateofexam,postcode,JobTitle,jid ";
                }
                else
                {
                    str = @"select postcode,JobTitle,convert(varchar,dateofexam,103) dateofexam,rv.jid,JobTitle+' ('+postcode+')' as post from ResultVerification rv 
                                    inner join examMast em on rv.examid =em.examid 
                                        inner join job_advt ja on rv.jid=ja.jid
                                        where (exampostponed <>'Y' or  exampostponed is null) and dbo.wadmitcarddownload(em.examid,'')='Y'
                                         and radmitcard='Y'  and tier=3 
                                        order by dateofexam,cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int) ";
                }

            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_exam_list_3tier_test(string flagfromboard)
    {
        try
        {
              string str = "";
            string str1 = "";
              if (flagfromboard == "Y")
              {
                  str = @"select distinct postcode,JobTitle, dateofexam,jid,examid from admitcarddata 
                                    where examdate >=GETDATE() and is3tierexam='A' 
                                    order by dateofexam,postcode,JobTitle,jid,examid ";
              }
              else
              {

                   str = @"select distinct postcode,JobTitle,bm.examid,
                            rv.jid,cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int),JobTitle+' ('+postcode+')' as post from ResultVerification rv 
                            inner join examMast em on rv.examid =em.examid 
                            inner join job_advt ja on rv.jid=ja.jid
                            inner join BatchMaster bm on em.examid =bm.examid
                            where
                            dateadd(hh,24,convert(datetime,examdate))  >=GETDATE()
                            and radmitcard='Y' and (exampostponed <>'Y' or  exampostponed is null) and tier=3 
                         --and dateadd(day, -2, examdate) >= getdate()
                            order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int) ";

                    str1 = @"select distinct postc.postcode,ja.JobTitle,bm.examid,
                            rv.jid,cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int),postc.JobTitle+' ('+postcode+')' as post from ResultVerification rv 
                            inner join examMast em on rv.examid =em.examid 
                            inner join (select jid,JobTitle from job_advt where reqid in (select ce.DeptReqId from Job_Advt ja 
							inner join ResultVerification rv on rv.jid=ja.jid
							inner join CombinedEntry ce on ja.reqid = ce.DeptReqId
							where rv.tier = 3)) ja on rv.jid=ja.jid
                            inner join BatchMaster bm on em.examid =bm.examid
							inner join (select postcode, xy.jid, JobTitle from job_advt ja
								inner join (select ce.Combdreqid,rv.jid from Job_Advt ja 
								inner join ResultVerification rv on rv.jid=ja.jid
								inner join CombinedEntry ce on ja.reqid = ce.DeptReqId
								where rv.tier = 3) xy on ja.reqid = xy.CombdReqid
								where reqid in (select ce.Combdreqid from Job_Advt ja 
								inner join ResultVerification rv on rv.jid=ja.jid
								inner join CombinedEntry ce on ja.reqid = ce.DeptReqId
								where rv.tier = 3)) postc on postc. jid = rv.jid
                            where
                            dateadd(hh,24,convert(datetime,examdate))  >= GETDATE()
                            and radmitcard is null and (exampostponed <>'Y' or  exampostponed is null) and rv.tier=3 
                         --and dateadd(day, -2, examdate) >= getdate()
                            order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int)";
              }

            dt = da.GetDataTable(str);
            if (dt.Rows.Count == 0 )
            {
                dt = da.GetDataTable(str1);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getapplid(string dummyno, string birthdt, string jid, bool provchecked, string regno, string flagfromboard,string rbtvalue)
    {
        try
        {
            string str = "", examid="", isprov = "";
            if (flagfromboard == "Y")
            {
                str = @"select applid,acstatid,RegNo,examid,acconsent from AdmitCardData where birthdt=@birthdt
                        and examdate>=getdate() and dummy_no=@dummyno and acstatid=1";
            }
            else
            {
                string con = "";
               
                if (provchecked)
                {
                    isprov = "Y";
                }
                if (provchecked)
                {
                    con += " and jap.jid=@jid and (jap.acstatid=2 or jap.acstatid=4 or jap.acstatid=5) and jap.regno=@regno";
                }
                else
                {
                    con += " and dummy_no=@dummyno and jap.acstatid=1 ";
                }
                if (rbtvalue == "6")
                {
                   string str1 = @"select distinct em.examid
                from  jobapplication jap 
                 inner join job_advt ja on jap.jid=ja.jid
                inner join examMast em on ja.examid =em.examid 
                inner join BatchMaster bm on em.examid=bm.examid
                where  convert(varchar,bm.examdate,111)>=convert(varchar,getdate(),111) and radmitcard='Y' and birthdt=@birthdt and dummy_no=@dummyno ";
                   SqlParameter[] param1 = new SqlParameter[2];
                   param1[0] = new SqlParameter("@dummyno", SqlDbType.BigInt);
                   if (dummyno != "")
                   {
                       param1[0].Value = dummyno;
                   }
                   else
                   {
                       param1[0].Value = DBNull.Value;
                   }
                   param1[1] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
                   param1[1].Value = birthdt;
                 
                   DataTable dt1 = da.GetDataTableQry(str1, param1);
                    
                    if(dt1.Rows.Count>0)
                    {
                         examid = dt1.Rows[0]["examid"].ToString();
                    }

                    str = @"select jap.applid,jap.acstatid,jap.RegNo,wcommon,ja.examid,acconsent,em.radmitcard ,emp2.radmitcard as radmitcard2,acconsent_phase2 ,'0' as newexamid,'' as batchid
                          from JobApplication jap inner join Job_Advt ja on jap.jid=ja.jid
                          inner join examMast em on em.examid=ja.examid
                          left outer join examMast_Phase2 emp2 on em.examid=emp2.examid
						   INNER JOIN ApplicantCenter ac on 
                        ((jap.applid=ac.applid and jap.wcommon is null) 
                        or (jap.RegNo=ac.regno and jap.wcommon='Y'))                      
                        and ac.examid =@examid
						  inner join BatchMaster bm on ac.batchid = bm.batchid  
                        INNER JOIN
                        CentreMaster ON CentreMaster.Centrecode= bm.centercode 
                          where birthdt=@birthdt and
                          em.radmitcard='Y' 
						  and convert(varchar,bm.examdate,111)>=convert(varchar,getdate(),111) " +con;
                }
                else
                {
//                    str = @"select applid,jap.acstatid,RegNo,wcommon,ja.examid,acconsent,em.radmitcard ,emp2.radmitcard as radmitcard2,acconsent_phase2 
//                          from JobApplication jap inner join Job_Advt ja on jap.jid=ja.jid
//                          inner join examMast em on em.examid=ja.examid
//                          left outer join examMast_Phase2 emp2 on em.examid=emp2.examid
//                          where birthdt=@birthdt and
//                          em.radmitcard='Y' and  dbo.wadmitcarddownload(em.examid)='Y' ";
                    
                    str = @"select applid,jap.acstatid,RegNo,wcommon,ja.examid,acconsent,em.radmitcard ,emp2.radmitcard as radmitcard2,acconsent_phase2,'0' as newexamid ,'' as batchid
                          from JobApplication jap inner join Job_Advt ja on jap.jid=ja.jid
                          inner join examMast em on em.examid=ja.examid
                          left outer join examMast_Phase2 emp2 on em.examid=emp2.examid
                          where birthdt=@birthdt and
                          em.radmitcard='Y' and  dbo.wadmitcarddownload(em.examid,@isprov)='Y' 
						  " + con + @"
						  union 
						  select applid,jap.acstatid,RegNo,wcommon,ja.examid,acconsent,em.radmitcard ,emp2.radmitcard as radmitcard2,acconsent_phase2 ,pce.newexamid,'' as batchid
                          from JobApplication jap inner join Job_Advt ja on jap.jid=ja.jid
                           inner join PartiallyCancelExam pce on ja.examid=pce.examid
                          inner join examMast em on em.examid=pce.newexamid
                          left outer join examMast_Phase2 emp2 on em.examid=emp2.examid
                          where birthdt=@birthdt and
                          em.radmitcard='Y' and  dbo.wadmitcarddownload(pce.newexamid,@isprov)='Y' 
						 " + con;
                }
                
            }
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@dummyno", SqlDbType.BigInt);
            if (dummyno != "")
            {
                param[0].Value = dummyno;
            }
            else
            {
                param[0].Value = DBNull.Value;
            }
            param[1] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
            param[1].Value = birthdt;
            param[2] = new SqlParameter("@jid", SqlDbType.Int, 4);
            if (jid != "")
            {
                param[2].Value = jid;
            }
            else
            {
                param[2].Value = DBNull.Value;
            }
            param[3] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
            param[3].Value = regno;
            param[4] = new SqlParameter("@examid", SqlDbType.Int, 4);
            if (examid != "")
            {
                param[4].Value = examid;
            }
            else
            {
                param[4].Value = DBNull.Value;
            }
            param[5] = new SqlParameter("@isprov", SqlDbType.Char, 1);
            if (isprov == "Y")
            {
                param[5].Value = isprov;
            }
            else
            {
                param[5].Value = DBNull.Value;
            }
            dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getpostsforexam(string regno, string flagfromboard,string rbtvalue)
    {
        string strsql = "";
        if (flagfromboard == "Y")
        {
            strsql= @"select jid,jobtitle + ' ('+postcode+')' as postdesc from admitcarddata 
                      where regno=@regno and is2tierexam is null and is3tierexam is null and examdate>GETDATE()";
        }
        else
        {
            if (rbtvalue == "6")
            {
                strsql = @"select distinct jap.jid,jobtitle + ' ('+postcode+')' as postdesc from Job_Advt ja 
                        inner join exammast em on ja.examid=em.examid inner join JobApplication jap on ja.jid=jap.jid 
                         inner join BatchMaster bm on em.examid=bm.examid
                        where jap.regno=@regno and bm.examdate>GETDATE()";
            }
            else
            {
//                strsql = @"select jap.jid,jobtitle + ' ('+postcode+')' as postdesc from Job_Advt ja 
//                        inner join exammast em on ja.examid=em.examid inner join JobApplication jap on ja.jid=jap.jid 
//                        where jap.regno='" + regno + "' and em.dateofexam>GETDATE()";

                strsql = @"select jap.jid,jobtitle + ' ('+postcode+')' as postdesc from Job_Advt ja 
                        inner join exammast em on ja.examid=em.examid inner join JobApplication jap on ja.jid=jap.jid 
                        where jap.regno=@regno and em.dateofexam>GETDATE()
						union 
						select jap.jid,jobtitle + ' ('+postcode+')' as postdesc from Job_Advt ja 
                        inner join exammast em on ja.examid=em.examid inner join JobApplication jap on ja.jid=jap.jid 
						inner join BatchMaster bm on em.examid=bm.examid and jap.batchid=bm.batchid
                        where jap.regno=@regno and bm.examdate>GETDATE()";
            }
        }
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[0].Value = regno;
        DataTable dt = new DataTable();
        try
        {
            dt = da.GetDataTableQry(strsql, param);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
    public DataTable get_AdmitCard_2tier_test(string regno, string applid, string rollno, string tier, string flagfromboard,string jid)
    {
        string str = "";
        string str1 = "";
        if (flagfromboard == "Y")
        {
            str = @"select distinct jid,applid ,replace(JobTitle,'[dot]','.')+' Post Code :('+postcode+')' post,examid
                from AdmitCardData where  examdate >= GETDATE()";
            if (tier == "2")
            {
                str += " and is2tierexam='A'";
            }
            else
            {
                str += " and is3tierexam='A'";
            }
            if (rollno != "")
            {
                str += " and rollno=@rollno ";
            }
        }
        else
        {
            str = @"select distinct rv.jid,ar.applid as applid,replace(JobTitle,'[dot]','.')+' Post Code :('+ja.postcode+')' post,em.examid,jap.gender,ar.regno
                from Applicant_result ar
                inner join jobapplication jap on ar.applid=jap.applid
                inner join ResultVerification rv on ar.rvid=rv.rvid and rv.tier=@tier
                inner join examMast em on rv.examid =em.examid 
                inner join job_advt ja on rv.jid=ja.jid
                inner join BatchMaster bm on em.examid=bm.examid
                where  examdate >= GETDATE() and radmitcard='Y' and rv.jid=@jid  ";

            str1 = @"select distinct rv.jid,ar.applid as applid,replace(ja.JobTitle,'[dot]','.')+' Post Code :('+postc.postcode+')' post,em.examid,jap.gender,ar.regno
                from Combined_Applicant_result ar
                inner join jobapplication jap on ar.applid=jap.applid
                inner join ResultVerification rv on ar.rvid=rv.rvid and rv.tier=3
                inner join examMast em on rv.examid =em.examid 
                inner join (select jid,JobTitle from job_advt where reqid in (select ce.DeptReqId from Job_Advt ja 
							inner join ResultVerification rv on rv.jid=ja.jid
							inner join CombinedEntry ce on ja.reqid = ce.DeptReqId
							where rv.tier = 3)) ja on rv.jid=ja.jid
				inner join (select postcode, xy.jid, JobTitle from job_advt ja
					inner join (select ce.Combdreqid,rv.jid from Job_Advt ja 
					inner join ResultVerification rv on rv.jid=ja.jid
					inner join CombinedEntry ce on ja.reqid = ce.DeptReqId
					where rv.tier = 3) xy on ja.reqid = xy.CombdReqid
					where reqid in (select ce.Combdreqid from Job_Advt ja 
					inner join ResultVerification rv on rv.jid=ja.jid
					inner join CombinedEntry ce on ja.reqid = ce.DeptReqId
					where rv.tier = 3)) postc on postc. jid = rv.jid
                inner join BatchMaster bm on em.examid=bm.examid
                where  --examdate >= GETDATE() and radmitcard ='Y' and 
				rv.jid=@jid ";
            if (regno != "")
            {
                str += " and ar.RegNo=@regno ";
                str1 += " and ar.RegNo=@regno ";
            }
            if (rollno != "")
            {
                str += " and ar.rollno=@rollno ";
                str1 += " and ar.rollno=@rollno ";
            }
            if (applid != "")
            {
                str += " and ar.applid=@applid ";
                str1 += " and ar.applid=@applid ";
            }
        }
        SqlParameter[] param = new SqlParameter[5];
        int j = 0;
        param[j] = new SqlParameter("@regno", SqlDbType.VarChar);
        if (regno == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = regno;
        }
        j++;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(applid);
        }
        j++;
        param[j] = new SqlParameter("@rollno", SqlDbType.BigInt);
        if (rollno == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = rollno;
        }
        j++;
        param[j] = new SqlParameter("@tier", SqlDbType.Int);
        if (tier == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(tier);
        }
        j++;
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        if (jid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(jid);
        }

        //Devesh
        SqlParameter[] param1 = new SqlParameter[5];
        int k = 0;
        param1[k] = new SqlParameter("@regno", SqlDbType.VarChar);
        if (regno == "")
        {
            param1[k].Value = 0;
        }
        else
        {
            param1[k].Value = regno;
        }
        k++;
        param1[k] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param1[k].Value = 0;
        }
        else
        {
            param1[k].Value = Int32.Parse(applid);
        }
        k++;
        param1[k] = new SqlParameter("@rollno", SqlDbType.BigInt);
        if (rollno == "")
        {
            param1[k].Value = 0;
        }
        else
        {
            param1[k].Value = rollno;
        }
        k++;
        param1[k] = new SqlParameter("@tier", SqlDbType.Int);
        if (tier == "")
        {
            param1[k].Value = 0;
        }
        else
        {
            param1[k].Value = Int32.Parse(tier);
        }
        k++;
        param1[k] = new SqlParameter("@jid", SqlDbType.Int);
        if (jid == "")
        {
            param1[k].Value = 0;
        }
        else
        {
            param1[k].Value = Int32.Parse(jid);
        }
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        if (age_relax_flag.Rows.Count == 0)
        {
            age_relax_flag = da.GetDataTableQry(str1, param1);
        }
        return age_relax_flag;
    }
    public DataTable get_AdmitCard_2tier(string regno, string applid, string rollno, string tier, string flagfromboard,string jid)
    {
        string str = "";
        if (flagfromboard == "Y")
        {
            str = @"select distinct jid,applid ,replace(JobTitle,'[dot]','.')+' Post Code :('+postcode+')' post,examid
                from AdmitCardData where  examdate >= GETDATE()";
            if (tier == "2")
            {
                str += " and is2tierexam='Y'";
            }
            else
            {
                str += " and is3tierexam='Y'";
            }
            if (rollno != "")
            {
                str += " and rollno=@rollno ";
            }
        }
        else
        {
            str = @"select rv.jid,applid as applid,replace(JobTitle,'[dot]','.')+' Post Code :('+ja.postcode+')' post,em.examid,ar.regno
                from Applicant_result ar
                inner join ResultVerification rv on ar.rvid=rv.rvid and rv.tier=@tier
                inner join examMast em on rv.examid =em.examid 
                inner join job_advt ja on rv.jid=ja.jid
                where  dateofexam >= GETDATE() and radmitcard='Y' and rv.jid=@jid ";
            if (regno != "")
            {
                str += " and RegNo=@regno ";
            }
            if (rollno != "")
            {
                str += " and rollno=@rollno ";
            }
            if (applid != "")
            {
                str += " and applid=@applid ";
            }
        }
        SqlParameter[] param = new SqlParameter[5];
        int j = 0;
        param[j] = new SqlParameter("@regno", SqlDbType.VarChar);
        if (regno == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = regno;
        }
        j++;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(applid);
        }
        j++;
        param[j] = new SqlParameter("@rollno", SqlDbType.BigInt);
        if (rollno == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = rollno;
        }
        j++;
        param[j] = new SqlParameter("@tier", SqlDbType.Int);
        if (tier == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(tier);
        }
        j++;
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        if (jid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(jid);
        }
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;
    }
    public DataTable getAdmitcarddetails_test(string applid, string examid, string flagfromintra, string flagfromboard)
    {
        try
        {
            string str = "";
            string str1 = "";
            if (flagfromboard == "Y")
            {
                str = @"SELECT applid,examid, name, RegNo, fname, address, category, dummy_no, SubCategory,dateofexam,timeofexam, reportingtime, 
                        centername, rollno, centeraddress,center_code, acstatid, phsubcat
                        FROM AdmitCardData where applid='" + applid + @"' and examid='" + examid + @"' and examdate>=getdate()";
            }
            else
            {
                str = @"SELECT Applicant_result.applid,ResultVerification.examid, JobApplication.name, JobApplication.RegNo, JobApplication.fname, 
                        JobApplication.address, JobApplication.category, 
                        JobApplication.dummy_no, JobApplication.SubCategory,convert(varchar,BatchMaster.examdate,103)+' 
                        ('+DATENAME(dw,BatchMaster.examdate)+')' dateofexam, BatchMaster.examtime as timeofexam, BatchMaster.reportingtime, 
                        CentreMaster.centername, ApplicantCenter.rollno,
                        CentreMaster.address as centeraddress,CentreMaster.center_code,case isprovisional when 'Y' then '2' else '1' end as acstatid,dbo.getphsubcat(isnull(ph,'')) as phsubcat,JobApplication.gender
                        FROM  
                        JobApplication 
                        inner join Applicant_result on Applicant_result.applid=JobApplication.applid
                        inner join ResultVerification on ResultVerification.rvid=Applicant_result.rvid and ResultVerification.examid='" + examid + @"'
                        INNER JOIN examMast ON examMast.examid = ResultVerification.examid 
                        INNER JOIN ApplicantCenter on 
                        ((Applicant_result.applid=ApplicantCenter.applid and Applicant_result.wcommon is null) 
                        or (Applicant_result.RegNo=ApplicantCenter.regno and Applicant_result.wcommon='Y'))                      
                        and ApplicantCenter.examid ='" + examid + @"'
                        inner join BatchMaster on ApplicantCenter.batchid = BatchMaster.batchid  
                        INNER JOIN
                        CentreMaster ON CentreMaster.Centrecode= BatchMaster.centercode 
                                           
                        where Applicant_result.applid='" + applid + @"'";

                str1 = @"SELECT Combined_Applicant_result.applid,ResultVerification.examid, JobApplication.name, JobApplication.RegNo, JobApplication.fname, 
                        JobApplication.address, JobApplication.category, 
                        JobApplication.dummy_no, JobApplication.SubCategory,convert(varchar,BatchMaster.examdate,103)+' 
                        ('+DATENAME(dw,BatchMaster.examdate)+')' dateofexam, BatchMaster.examtime as timeofexam, BatchMaster.reportingtime, 
                        CentreMaster.centername, ApplicantCenter.rollno,
                        CentreMaster.address as centeraddress,CentreMaster.center_code,case isprovisional when 'Y' then '2' else '1' end as acstatid,dbo.getphsubcat(isnull(ph,'')) as phsubcat,JobApplication.gender
                        FROM  
                        JobApplication 
                        inner join Combined_Applicant_result on Combined_Applicant_result.applid=JobApplication.applid
                        inner join ResultVerification on ResultVerification.rvid=Combined_Applicant_result.rvid and ResultVerification.examid='" + examid + @"'
                        INNER JOIN examMast ON examMast.examid = ResultVerification.examid 
                        INNER JOIN ApplicantCenter on 
                        ((Combined_Applicant_result.applid=ApplicantCenter.applid and Combined_Applicant_result.wcommon is null) 
                        or (Combined_Applicant_result.RegNo=ApplicantCenter.regno and Combined_Applicant_result.wcommon = 'Y'))                      
                        and ApplicantCenter.examid ='" + examid + @"'
                        inner join BatchMaster on ApplicantCenter.batchid = BatchMaster.batchid  
                        INNER JOIN
                        CentreMaster ON CentreMaster.Centrecode= BatchMaster.centercode                                            
                        where Combined_Applicant_result.applid='" + applid + @"'";

                if (flagfromintra != "1")
                {
                    str = str + " and convert(varchar,examdate,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";
                    str1 = str1 + " and convert(varchar,examdate,111)>=convert(varchar,getdate(),111) and radmitcard ='Y'";

                }
            }
            // str += " and JobApplication.acstatid in('1','2')";


            dt = da.GetDataTable(str);
            if (dt.Rows.Count == 0)
            {
                dt = da.GetDataTable(str1);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_examdate_test(string examid, string flagfromboard,string rbtvalue)
    {
        try
        {
            string str ="";
            if (flagfromboard == "Y")
            {
                str = @"select dateofexam  as examdate from AdmitCardData where examid=@examid order by examdate";
            }
            else
            {
                if (rbtvalue == "6")
                {
                    str = @"select ' From ' + cast(convert(varchar,min(bm.examdate),103) as varchar)+' To '+CAST(convert(varchar,max(bm.examdate),103) as varchar)   as examdate
                            from job_advt ja
                            inner join examMast em on ja.examid =em.examid 
                           inner join BatchMaster bm on em.examid =bm.examid
                            where bm.examid=@examid and
                            dateadd(hh,10,convert(datetime,examdate))  >=GETDATE()
                            and radmitcard='Y' 
							and (exampostponed <>'Y' or  exampostponed is null)
                            order by examdate";
                }
                else
                {
                    str = @"select ' From ' + cast(convert(varchar,min(bm.examdate),103) as varchar)+' To '+CAST(convert(varchar,max(bm.examdate),103) as varchar)   as examdate
                            from ResultVerification rv 
                            inner join examMast em on rv.examid =em.examid 
                            inner join job_advt ja on rv.jid=ja.jid
                            inner join BatchMaster bm on em.examid =bm.examid
                            where bm.examid=@examid and
                            dateadd(hh,10,convert(datetime,examdate))  >=GETDATE()
                            and radmitcard='Y' and (exampostponed <>'Y' or  exampostponed is null)
                            order by examdate";
                }
            }

            int j = 0;
            SqlParameter[] param = new SqlParameter[1];
            param[j] = new SqlParameter("@examid", SqlDbType.Int, 4);
            param[j].Value = examid;


            dt = da.GetDataTableQry(str, param);
            return dt;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable selectinstructiondoc(string examid, string gender)
    {
        string str = "SELECT inid, indoc,examid FROM InstructionMaster where examid=@examid and (gendercategory=@gender or gendercategory='B') ";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
        if (examid == "")
        {
            param[0].Value = 0;
        }
        else
        {
            param[0].Value = Int32.Parse(examid);
        }
        param[1] = new SqlParameter("@gender", SqlDbType.Char, 1);
        if (gender == "")
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = gender;
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
    public DataTable getAdmitcarddetails(string applid, string examid, string flagfromintra, string rbtvalue,string flagfromboard,string isprov)
    {
        try
        {
            string str = "";
            string str1 = "";
            if (flagfromboard == "Y")
            {
               
                    str = @"SELECT applid,examid,name,RegNo,fname,address,category,dummy_no,SubCategory,
                        dateofexam,timeofexam,reportingtime,centername,rollno,centeraddress,center_code,acstatid,phsubcat,SigId,CDesig,JobTitle,postcode
                        FROM AdmitCardData where applid=@applid and examid=@examid ";
                   
            }
            else
            {
                string cond = "";
                if (flagfromintra == "")
                {
                   //changed on 26022020
                    if (examid == "483")
                    {
                        cond = " dsssbonline_recdapp.dbo.JobApplication";
                    }
                    else
                    {
                        cond = " JobApplication ";
                    }
                }
                else
                {
                    cond = "(select name,RegNo,fname,address,category,dummy_no,gender,applid,acstatid,wcommon,jid,email from JobApplication union select name,RegNo,fname,address,category,dummy_no,gender,applid,acstatid,wcommon,jid,email from dsssbonline_recdapp.dbo.JobApplication) ";
                }
                if (rbtvalue == "2" || rbtvalue == "4")
                {
                    str = @"SELECT ar.applid,rv.examid, jap.name, jap.RegNo, jap.fname, jap.address, jap.category,  jap.dummy_no, dbo.getsubcategory(@applid) as SubCategory,convert(varchar,em.dateofexam,103)+' 
                        ('+DATENAME(dw,em.dateofexam)+')' dateofexam, em.timeofexam, em.reportingtime, cm.centername, ac.rollno, cm.address as centeraddress,cm.center_code,case isprovisional 
						when 'Y' then '2' else '1' end as acstatid,
                        --dbo.getphsubcat(isnull(ph,'')) as phsubcat ,
                        jap.gender,em.SigId,sm.CDesig,em.isonline,jap.email,jap.jid
                        FROM " + cond + @" jap
                        inner join Applicant_result ar on ar.applid=jap.applid
                        inner join ResultVerification rv on rv.rvid=ar.rvid and rv.examid=@examid
                        INNER JOIN examMast em ON em.examid = rv.examid 
                        Left Outer JOIN SignatureMaster sm on em.sigid=sm.sigid
                        INNER JOIN ApplicantCenter ac on ((ar.applid=ac.applid and ar.wcommon is null) or (ar.RegNo=ac.regno and ar.wcommon='Y')) and ac.examid =@examid
                        INNER JOIN CentreMaster cm ON cm.Centrecode= ac.centercode                      
                        where ar.applid=@applid ";
                    if (flagfromintra == "")
                    {
                        //str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y' and convert(varchar(10),dateadd(day, 0, dateofexam),120) > convert(varchar(10),GETDATE(),120)  ";
                        //str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y' and convert(varchar(10),dateadd(day, 0, dateofexam),120) >= convert(varchar(10),GETDATE(),120)  ";
                        str = str + " and em.radmitcard='Y' and  dbo.wadmitcarddownload(em.examid,@isprov)='Y' ";
                    }
                    // str += " and JobApplication.acstatid in('1','2')";
                }
                else if (rbtvalue == "3" || rbtvalue == "5")
                {
                    str = @"SELECT ar.applid,rv.examid, jap.name, jap.RegNo, jap.fname,jap.address, jap.category, jap.dummy_no, dbo.getsubcategory(@applid) as SubCategory,convert(varchar,bm.examdate,103)+' 
                        ('+DATENAME(dw,bm.examdate)+')' dateofexam, bm.examtime as timeofexam, bm.reportingtime, cm.centername, ac.rollno, cm.address as centeraddress,cm.center_code,
                         case isprovisional when 'Y' then '2' else '1' end as acstatid,
                        --dbo.getphsubcat(isnull(ph,'')) as phsubcat,
                        jap.gender,em.SigId,sm.CDesig,em.isonline,jap.email,jap.jid
                         FROM " + cond + @" jap
                        inner join Applicant_result ar on ar.applid=jap.applid
                        inner join ResultVerification rv on rv.rvid=ar.rvid and rv.examid=@examid
                        INNER JOIN examMast em ON em.examid = rv.examid
			            Left Outer JOIN SignatureMaster sm on em.sigid=sm.sigid  
                        INNER JOIN ApplicantCenter ac on  ((ar.applid=ac.applid and ar.wcommon is null) or (ar.RegNo=ac.regno and ar.wcommon='Y')) and ac.examid =@examid
                        inner join BatchMaster bm on ac.batchid = bm.batchid  
                        INNER JOIN CentreMaster cm ON cm.Centrecode= bm.centercode 
                        where ar.applid=@applid ";

                    str1 = @"SELECT ar.applid,rv.examid, jap.name, jap.RegNo, jap.fname,jap.address, jap.category, jap.dummy_no, dbo.getsubcategory(@applid) as SubCategory,convert(varchar,bm.examdate,103)+' 
                        ('+DATENAME(dw,bm.examdate)+')' dateofexam, bm.examtime as timeofexam, bm.reportingtime, cm.centername, ac.rollno, cm.address as centeraddress,cm.center_code,
                         case isprovisional when 'Y' then '2' else '1' end as acstatid,
                        --dbo.getphsubcat(isnull(ph,'')) as phsubcat,
                        jap.gender,em.SigId,sm.CDesig,em.isonline,jap.email,jap.jid
                         FROM " + cond + @" jap
                        inner join Combined_Applicant_result ar on ar.applid=jap.applid
                        inner join ResultVerification rv on rv.rvid=ar.rvid and rv.examid=@examid
                        INNER JOIN examMast em ON em.examid = rv.examid
			            Left Outer JOIN SignatureMaster sm on em.sigid=sm.sigid  
                        INNER JOIN ApplicantCenter ac on  ((ar.applid=ac.applid and ar.wcommon is null) or (ar.RegNo=ac.regno and ar.wcommon='Y')) and ac.examid =@examid
                        inner join BatchMaster bm on ac.batchid = bm.batchid  
                        INNER JOIN CentreMaster cm ON cm.Centrecode= bm.centercode 
                        where ar.applid=@applid ";
                    if (flagfromintra == "")
                    {
                        str = str + " and convert(varchar,examdate,111)>=convert(varchar,getdate(),111) and radmitcard='Y' ";
                        str1 = str1 + " and convert(varchar,examdate,111)>=convert(varchar,getdate(),111) and radmitcard ='Y' ";
                        //and convert(varchar(10),dateadd(day, -1, examdate),120) > convert(varchar(10),GETDATE(),120) ";

                    }

                }
                    else if(rbtvalue=="6")
                {

                    str = @"SELECT jap.applid,ja.examid, jap.name, jap.RegNo, jap.fname, jap.address, jap.category, 
                            jap.dummy_no, dbo.getsubcategory(@applid) as SubCategory,convert(varchar,bm.examdate,103)+' 
                        ('+DATENAME(dw,bm.examdate)+')' dateofexam, bm.examtime as timeofexam, bm.reportingtime, ja.postcode, ja.JobTitle, cm.centername, ac.rollno,cm.address as centeraddress
                            ,cm.center_code,jap.acstatid,
                            --dbo.getphsubcat(isnull(ph,'')) as phsubcat ,
                            jap.gender,em.SigId,sm.CDesig,em.isonline,jap.email,jap.jid
                            FROM " + cond + @" jap 
                            INNER JOIN  Job_Advt ja ON ja.jid = jap.jid
                            INNER JOIN examMast em ON em.examid = ja.examid 
                            Left Outer JOIN SignatureMaster sm on em.sigid=sm.sigid
                            INNER JOIN ApplicantCenter ac on ((jap.applid=ac.applid and jap.wcommon is null) or (jap.RegNo=ac.regno and jap.wcommon='Y')) and ac.examid =@examid
                             inner join BatchMaster bm on ac.batchid = bm.batchid  
                        INNER JOIN CentreMaster cm ON cm.Centrecode= bm.centercode                   
                            where jap.applid=@applid ";

                   
                    if (flagfromintra == "")
                    {
                        //str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y' and convert(varchar(10),dateofexam,120) > convert(varchar(10),GETDATE(),120) ";
                       // str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y' and convert(varchar(10),dateofexam,120) >= convert(varchar(10),GETDATE(),120) ";
                       // str = str + " and em.radmitcard='Y' and  dbo.wadmitcarddownload(em.examid)='Y' ";
                        str = str + " and convert(varchar,examdate,111)>=convert(varchar,getdate(),111) and radmitcard='Y' ";
                    }
                    str += " and jap.acstatid in('1','2','4','5')";
                }
                else
                {

                    str = @"SELECT jap.applid,ja.examid, jap.name, jap.RegNo, jap.fname, jap.address, jap.category, 
                            jap.dummy_no, dbo.getsubcategory(@applid) as SubCategory,convert(varchar,em.dateofexam,103)+' ('+DATENAME(dw,em.dateofexam)+')' dateofexam, 
                            em.timeofexam, em.reportingtime, ja.postcode, ja.JobTitle, cm.centername, ac.rollno,cm.address as centeraddress
                            ,cm.center_code,jap.acstatid,
                            --dbo.getphsubcat(isnull(ph,'')) as phsubcat ,
                            jap.gender,em.SigId,sm.CDesig,em.isonline,jap.email,jap.jid
                            FROM " + cond + @" jap 
                            INNER JOIN  Job_Advt ja ON ja.jid = jap.jid
                            INNER JOIN examMast em ON em.examid = ja.examid 
                            Left Outer JOIN SignatureMaster sm on em.sigid=sm.sigid
                            INNER JOIN ApplicantCenter ac on ((jap.applid=ac.applid and jap.wcommon is null) or (jap.RegNo=ac.regno and jap.wcommon='Y')) and ac.examid =@examid
                            INNER JOIN CentreMaster cm ON cm.Centrecode= ac.centercode                      
                            where jap.applid=@applid ";

                    if (flagfromintra == "")
                    {
                        str = str + " and em.radmitcard='Y' and  dbo.wadmitcarddownload(em.examid,@isprov)='Y' ";
                    }
                    str += " and jap.acstatid in('1','2','4','5')";
                }
            }
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@applid", SqlDbType.Int);
            param[0].Value = applid;
            param[1] = new SqlParameter("@examid", SqlDbType.Int);
            param[1].Value = examid;
            param[2] = new SqlParameter("@isprov", SqlDbType.Char, 1);
            if (isprov == "Y")
            {
                param[2].Value = isprov;
            }
            else
            {
                param[2].Value = DBNull.Value;
            }

            SqlParameter[] param1 = new SqlParameter[3];

            param1[0] = new SqlParameter("@applid", SqlDbType.Int);
            param1[0].Value = applid;
            param1[1] = new SqlParameter("@examid", SqlDbType.Int);
            param1[1].Value = examid;
            param1[2] = new SqlParameter("@isprov", SqlDbType.Char, 1);
            if (isprov == "Y")
            {
                param1[2].Value = isprov;
            }
            else
            {
                param1[2].Value = DBNull.Value;
            }
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 0)
            {
                dt = da.GetDataTableQry(str1, param1);
            }
            return dt;
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_OAnumber(string applid)
    {
        str = @"select top 1 OAnumber from pacandidates where applid=@applid order by e_date desc";


        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);
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
    public DataTable getAdmitcarddetails_post(string applid, string examid, string flagfromintra, string rbtvalue)
    {
        try
        {
            string str = "";

            if (rbtvalue == "2" || rbtvalue == "4")
            {
                str = @"SELECT Job_Advt.postcode, Job_Advt.JobTitle,case isprovisional when 'Y' then '2' else '1' end as acstatid
                        FROM  Applicant_result 
                        inner join ResultVerification on ResultVerification.rvid=Applicant_result.rvid and ResultVerification.examid='" + examid + @"' 
                        INNER JOIN  Job_Advt ON Job_Advt.jid = ResultVerification.jid
                        INNER JOIN examMast ON examMast.examid = ResultVerification.examid                       
                        where 
                        Applicant_result.RegNo = (select regno from Applicant_result where applid='" + applid + "' and rvid in (select rvid from ResultVerification where examid='" + examid + @"' ))";

                if (flagfromintra != "1" && flagfromintra != "5" && flagfromintra != "2") 
                {
                    str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";

                }
                else if (flagfromintra == "5" || flagfromintra == "2")
                {
                    str = str + " and radmitcard='Y'";

                }
                str += " and examMast.examid ='" + examid + "'";
                //and JobApplication.acstatid in('1','2')";

                str = str + " order by cast(SUBSTRING(Job_Advt.postcode,1,CHARINDEX('/',Job_Advt.postcode)-1) as int)";
            }
            else if (rbtvalue == "3" || rbtvalue == "5")
            {
                str = @"SELECT distinct Job_Advt.postcode, Job_Advt.JobTitle,case isprovisional when 'Y' then '2' else '1' end as acstatid,cast(SUBSTRING(Job_Advt.postcode,1,CHARINDEX('/',Job_Advt.postcode)-1) as int)
                        FROM  Applicant_result 
                        inner join ResultVerification on ResultVerification.rvid=Applicant_result.rvid and ResultVerification.examid='" + examid + @"' 
                        INNER JOIN  Job_Advt ON Job_Advt.jid = ResultVerification.jid
                        INNER JOIN examMast ON examMast.examid = ResultVerification.examid 
                       inner join BatchMaster on examMast.examid = BatchMaster.examid                        
                        where 
                        Applicant_result.RegNo = (select regno from Applicant_result where applid='" + applid + "' and rvid in (select rvid from ResultVerification where examid='" + examid + @"' ))";

                if (flagfromintra != "1" && flagfromintra != "5" &&  flagfromintra != "2")
                {
                    str = str + " and convert(varchar,examdate,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";

                }
                else if (flagfromintra == "5" || flagfromintra == "2")
                {
                    str = str + " and radmitcard='Y'";

                }
                str += " and examMast.examid ='" + examid + "'";
                //and JobApplication.acstatid in('1','2')";

                str = str + " order by cast(SUBSTRING(Job_Advt.postcode,1,CHARINDEX('/',Job_Advt.postcode)-1) as int)";
            }
                 else if(rbtvalue=="6")
            {
                string cond = "";
                if (flagfromintra == "")
                {
                    cond = " JobApplication ";
                }
                else
                {
                    cond = " (select acstatid,jid,RegNo,applid,wcommon from JobApplication union select acstatid,jid,RegNo,applid,wcommon from dsssbonline_recdapp.dbo.JobApplication) ";
                }
                str = @"SELECT ja.postcode, ja.JobTitle,jap.acstatid FROM  " + cond + @" jap
                       INNER JOIN  Job_Advt ja ON ja.jid = jap.jid
                       INNER JOIN examMast em ON em.examid = ja.examid 
                       INNER JOIN ApplicantCenter ac on ((jap.applid=ac.applid and jap.wcommon is null) or (jap.RegNo=ac.regno and jap.wcommon='Y')) and ac.examid ='" + examid + @"'
                       inner join BatchMaster bm on ac.batchid=bm.batchid and ac.examid='" + examid + @"'                           
                       where 1=1 ";
                if (flagfromintra == "")
                {
                    str += " and jap.RegNo in(select regno from JobApplication where applid='" + applid + "' )";
                }
                else
                {
                    str += " and jap.RegNo in(select regno from JobApplication where applid='" + applid + "' union select regno from dsssbonline_recdapp.dbo.JobApplication where applid='" + applid + "')";
                }


                if (flagfromintra != "1" && flagfromintra != "5" && flagfromintra != "2")
                {
                    str = str + " and convert(varchar,examdate,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";

                }
                else if (flagfromintra == "5" || flagfromintra == "2")
                {
                    str = str + " and radmitcard='Y'";

                }
                str += " and em.examid ='" + examid + "' and jap.acstatid in('1','2','4','5')";

                str = str + " order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int)";
            }
            else
            {
                string cond = "";
                if (flagfromintra == "")
                {
                    cond = " JobApplication ";
                }
                else
                {
                    cond = " (select acstatid,jid,RegNo,applid,wcommon from JobApplication union select acstatid,jid,RegNo,applid,wcommon from dsssbonline_recdapp.dbo.JobApplication) ";
                }
                str = @"SELECT ja.postcode, ja.JobTitle,jap.acstatid FROM  " + cond + @" jap
                       INNER JOIN  Job_Advt ja ON ja.jid = jap.jid
                       INNER JOIN examMast em ON em.examid = ja.examid 
                       INNER JOIN ApplicantCenter ac on ((jap.applid=ac.applid and jap.wcommon is null) or (jap.RegNo=ac.regno and jap.wcommon='Y')) and ac.examid ='" + examid + @"'
                       INNER JOIN CentreMaster cm ON cm.Centrecode= ac.centercode                         
                       where 1=1 ";
                if (flagfromintra == "")
                {
                    str += " and jap.RegNo in(select regno from JobApplication where applid='" + applid + "' )";
                }
                else
                {
                    str += " and jap.RegNo in(select regno from JobApplication where applid='" + applid + "' union select regno from dsssbonline_recdapp.dbo.JobApplication where applid='" + applid + "')";
                }


                if (flagfromintra != "1" && flagfromintra != "5" && flagfromintra != "2")
                {
                    str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";

                }
                else if (flagfromintra == "5" || flagfromintra == "2")
                {
                    str = str + " and radmitcard='Y'";

                }
                str += " and em.examid ='" + examid + "' and jap.acstatid in('1','2','4','5')";

                str = str + " order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int)";
            }
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getcandidatephoto(string applid, string flagfromboard, string datatransfer)
    {
        string str = "";
      
            if (flagfromboard == "Y")
            {
                str = "select OLEModule,signature from AdmitCardPhoto where ApplId=@applid";
            }
            else
            {
                if (datatransfer == "Y")
                {
                    str = "select OLEModule,signature from dsssbonline_recdapp.dbo.JobApplicationPhoto where ApplId=@applid";
                }
                else
                {
                    str = "select OLEModule,signature from JobApplicationPhoto where ApplId=@applid";
                }
            }
        

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);
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
    public DataTable checkwdatatransfer(string applid)
    {
        string str = "select applid from JobApplication where ApplId=@applid";
       SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);
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
    public int insertAdmitcarddownload(string applid, string examtype,string examid, string ipaddress,string regno)
    {

        string str = "insert into admitcarddownload (applid, examtype,examid, edate, ipaddress,regno) values (@applid, @examtype,@examid, getdate(), @ipaddress,@regno)  ";
        SqlParameter[] param = new SqlParameter[5];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@examtype", SqlDbType.Int);
        param[j].Value = examtype;
        j++;
        param[j] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
        param[j].Value = ipaddress;
        j++;
        param[j] = new SqlParameter("@examid", SqlDbType.Int, 4);
        param[j].Value = examid;
        j++;
        param[j] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[j].Value = regno;
        try
        {
            int id1 = da.ExecuteParameterizedQuery(str, param);
            return id1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetExamtype()
    {
        string str;

        str = @"SELECT examtypeid,examtype,tier from examtypemaster order by tier";

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

    
    public DataTable checkrollnoforreexam(string applid,string examid)
    {
        string str = "select rollno from ApplicantCenter where applid=@applid and examid=@examid and rollno in(select rollno from ReExamApplicants)";
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);
        param[1] = new SqlParameter("@examid", SqlDbType.Int);
        param[1].Value = Int32.Parse(examid);
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

    public DataTable getAdmitcarddetailsforReExam(string applid, string examid, string flagfromintra,string isprov)
    {
        try
        {
            string str = "";
            string cond = "";
            if (flagfromintra == "")
            {
                cond = " JobApplication ";
            }
            else
            {
                cond = "(select name,RegNo,fname,address,category,dummy_no,gender,applid,acstatid,wcommon,jid,email from JobApplication union select name,RegNo,fname,address,category,dummy_no,gender,applid,acstatid,wcommon,jid,email from dsssbonline_recdapp.dbo.JobApplication) ";
            }

            str = @"SELECT jap.applid,ja.examid, jap.name, jap.RegNo, jap.fname, jap.address, jap.category,jap.jid, 
                            jap.dummy_no, dbo.getsubcategory(@applid) as SubCategory,convert(varchar,em.dateofexam,103)+' ('+DATENAME(dw,em.dateofexam)+')' dateofexam, 
                            em.timeofexam, em.reportingtime, ja.postcode, ja.JobTitle, cm.centername, ac.rollno,cm.address as centeraddress
                            ,cm.center_code,jap.acstatid,
                            --dbo.getphsubcat(isnull(ph,'')) as phsubcat ,
                            jap.gender,em.SigId,sm.CDesig,em.isonline,jap.email
                            from ApplicantCenter ac inner join PartiallyCancelExam pce on ac.examid=pce.examid and pce.newexamid=@examid
				            inner join ReExamApplicants rea on pce.pcid=rea.pcid and pce.newexamid=@examid and rea.rollno=ac.rollno
				            inner join CentreMaster cm on ac.centercode=cm.Centrecode
				            inner join " + cond + @" jap on jap.applid=ac.applid
				            inner join Job_Advt ja on jap.jid=ja.jid
				            INNER JOIN examMast em ON em.examid = @examid
				            Left Outer JOIN SignatureMaster sm on em.sigid=sm.sigid	               
                            where jap.applid=@applid ";

            if (flagfromintra == "")
            {
                str = str + " and em.radmitcard='Y' and  dbo.wadmitcarddownload(em.examid,@isprov)='Y' ";
            }

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@applid", SqlDbType.Int);
            param[0].Value = applid;
            param[1] = new SqlParameter("@examid", SqlDbType.Int);
            param[1].Value = examid;
            param[2] = new SqlParameter("@isprov", SqlDbType.Char, 1);
            if (isprov == "Y")
            {
                param[2].Value = isprov;
            }
            else
            {
                param[2].Value = DBNull.Value;
            }
            dt = da.GetDataTableQry(str, param);
            return dt;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable getAdmitcarddetails_post_ReExam(string applid, string examid, string flagfromintra)
    {
        try
        {
            string str = "";
            string cond = "";


            str = @"SELECT ja.postcode, ja.JobTitle,jap.acstatid 
                    from ApplicantCenter ac inner join PartiallyCancelExam pce on ac.examid=pce.examid and pce.newexamid=@examid
				    inner join ReExamApplicants rea on pce.pcid=rea.pcid and pce.newexamid=@examid and rea.rollno=ac.rollno				   
				    inner join JobApplication jap on jap.applid=ac.applid
				    inner join Job_Advt ja on jap.jid=ja.jid 
                    INNER JOIN examMast em ON em.examid = @examid where 1=1";

            if (flagfromintra == "")
            {
                str += " and jap.RegNo in(select regno from JobApplication where applid='" + applid + "' )";
            }
            else
            {
                str += " and jap.RegNo in(select regno from JobApplication where applid='" + applid + "' union select regno from dsssbonline_recdapp.dbo.JobApplication where applid='" + applid + "')";
            }


            if (flagfromintra != "1" && flagfromintra != "5" && flagfromintra != "2")
            {
                str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";

            }
            else if (flagfromintra == "5" || flagfromintra == "2")
            {
                str = str + " and radmitcard='Y'";

            }

            str = str + " order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int)";

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@applid", SqlDbType.Int);
            param[0].Value = applid;
            param[1] = new SqlParameter("@examid", SqlDbType.Int);
            param[1].Value = examid;
            dt = da.GetDataTableQry(str, param);
            return dt;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getAdmitcarddetails_batchwise(string applid, string examid, string flagfromintra,string batchid,string isprov)
    {
        try
        {
            string cond = "";
            if (flagfromintra == "")
            {
                cond = " JobApplication ";
            }
            else
            {
                cond = "(select name,RegNo,fname,address,category,dummy_no,gender,applid,acstatid,wcommon,jid,batchid,email from JobApplication union select name,RegNo,fname,address,category,dummy_no,gender,applid,acstatid,wcommon,jid,batchid,email from dsssbonline_recdapp.dbo.JobApplication) ";
            }
            string str = @"SELECT jap.applid,ja.examid, jap.name, jap.RegNo, jap.fname, jap.address, jap.category, jap.jid, 
                            jap.dummy_no, dbo.getsubcategory(@applid) as SubCategory,convert(varchar,bm.examdate,103)+' ('+DATENAME(dw,bm.examdate)+')' dateofexam, 
                            bm.examtime as timeofexam, bm.reportingtime, ja.postcode, ja.JobTitle, cm.centername, ac.rollno,cm.address as centeraddress
                            ,cm.center_code,jap.acstatid,jap.gender,bas.SigId,sm.CDesig,em.isonline,jap.email
                            FROM " + cond + @" jap 
                            INNER JOIN  Job_Advt ja ON ja.jid = jap.jid
                            INNER JOIN examMast em ON em.examid = ja.examid 
                            INNER JOIN ApplicantCenter ac on ((jap.applid=ac.applid and jap.wcommon is null) or (jap.RegNo=ac.regno and jap.wcommon='Y')) 
                            and ac.examid =@examid and ac.batchid=jap.batchid and ac.batchid=@batchid
                            INNER JOIN CentreMaster cm ON cm.Centrecode= ac.centercode
                            inner join BatchMaster bm on bm.batchid=@batchid
                            inner join BatchAllocationStatus bas on bm.batchid=bas.BatchId 
                            Left Outer JOIN SignatureMaster sm on bas.sigid=sm.sigid                      
                            where jap.applid=@applid and ac.batchid=@batchid and jap.acstatid in('1','2','4','5') ";

            if (flagfromintra == "")
            {
                str = str + " and  dbo.wadmitcarddownload_batchwise(@examid,@batchid,@isprov)='Y' and bas.radmitcard='Y' ";
            }
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@applid", SqlDbType.Int);
            param[0].Value = applid;
            param[1] = new SqlParameter("@examid", SqlDbType.Int);
            param[1].Value = examid;
            param[2] = new SqlParameter("@batchid", SqlDbType.Int);
            param[2].Value = batchid;
            param[3] = new SqlParameter("@isprov", SqlDbType.Char, 1);
            if (isprov == "Y")
            {
                param[3].Value = isprov;
            }
            else
            {
                param[3].Value = DBNull.Value;
            }
            dt = da.GetDataTableQry(str, param);
            return dt;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getAdmitcarddetails_post_batchwise(string applid, string examid, string flagfromintra,string batchid,string isprov)
    {
        try
        {
            string str = "";

                string cond = "";
                if (flagfromintra == "")
                {
                    cond = " JobApplication ";
                }
                else
                {
                    cond = " (select acstatid,jid,RegNo,applid,wcommon,batchid from JobApplication union select acstatid,jid,RegNo,applid,wcommon,batchid from dsssbonline_recdapp.dbo.JobApplication) ";
                }
                str = @"SELECT ja.postcode, ja.JobTitle,jap.acstatid FROM  " + cond + @" jap
                       INNER JOIN  Job_Advt ja ON ja.jid = jap.jid
                       INNER JOIN examMast em ON em.examid = ja.examid 
                       INNER JOIN ApplicantCenter ac on ((jap.applid=ac.applid and jap.wcommon is null) or (jap.RegNo=ac.regno and jap.wcommon='Y')) 
                       and ac.examid =@examid and ac.batchid=jap.batchid and ac.batchid=@batchid
                       INNER JOIN CentreMaster cm ON cm.Centrecode= ac.centercode 
                       inner join BatchMaster bm on bm.batchid=@batchid
                        inner join BatchAllocationStatus bas on bm.batchid=bas.BatchId                        
                       where 1=1 ";
                if (flagfromintra == "")
                {
                    str += " and jap.RegNo in(select regno from JobApplication where applid=@applid )";
                }
                else
                {
                    str += " and jap.RegNo in(select regno from JobApplication where applid=@applid union select regno from dsssbonline_recdapp.dbo.JobApplication where applid=@applid)";
                }


                if (flagfromintra != "1" && flagfromintra != "5" && flagfromintra != "2")
                {
                    str = str + " and dbo.wadmitcarddownload_batchwise(@examid,@batchid,@isprov)='Y' and bas.radmitcard='Y'";

                }
                else if (flagfromintra == "5" || flagfromintra == "2")
                {
                    str = str + " and bas.radmitcard='Y'";

                }
                str += " and em.examid =@examid and jap.acstatid in('1','2','4','5') and ac.batchid=@batchid";

                str = str + " order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int)";

                SqlParameter[] param = new SqlParameter[4];

                param[0] = new SqlParameter("@applid", SqlDbType.Int);
                param[0].Value = applid;
                param[1] = new SqlParameter("@examid", SqlDbType.Int);
                param[1].Value = examid;
                param[2] = new SqlParameter("@batchid", SqlDbType.Int);
                param[2].Value = batchid;
                param[3] = new SqlParameter("@isprov", SqlDbType.Char, 1);
                if (isprov == "Y")
                {
                    param[3].Value = isprov;
                }
                else
                {
                    param[3].Value = DBNull.Value;
                }
                dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getapplid_batchwise(string dummyno, string birthdt, string jid, bool provchecked, string regno)
    {
        try
        {
            string isprov = "";
            if (provchecked)
            {
                isprov = "Y";
            }
            string str = @"select applid,jap.acstatid,RegNo,wcommon,ja.examid,acconsent,bas.radmitcard ,null as radmitcard2,acconsent_phase2,'0' as newexamid,jap.batchid
                          from JobApplication jap inner join Job_Advt ja on jap.jid=ja.jid
                          inner join examMast em on em.examid=ja.examid
                          inner join BatchMaster bm on em.examid=bm.examid and bm.batchid=jap.batchid
                          inner join BatchAllocationStatus bas on bm.batchid=bas.BatchId
                          where birthdt=@birthdt
						  and bas.radmitcard='Y' and  dbo.wadmitcarddownload_batchwise(em.examid,bas.batchid,@isprov)='Y'  ";


                    if (provchecked)
                    {
                        str += " and jap.jid=@jid and (jap.acstatid=2 or jap.acstatid=4 or jap.acstatid=5) and jap.regno=@regno";
                    }
                    else
                    {
                        str += " and dummy_no=@dummyno and jap.acstatid=1 ";
                    }
            
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@dummyno", SqlDbType.BigInt);
            if (dummyno != "")
            {
                param[0].Value = dummyno;
            }
            else
            {
                param[0].Value = DBNull.Value;
            }
            param[1] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
            param[1].Value = birthdt;
            param[2] = new SqlParameter("@jid", SqlDbType.Int, 4);
            if (jid != "")
            {
                param[2].Value = jid;
            }
            else
            {
                param[2].Value = DBNull.Value;
            }
            param[3] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
            param[3].Value = regno;
            param[4] = new SqlParameter("@isprov", SqlDbType.Char, 1);
            if (isprov == "Y")
            {
                param[4].Value = isprov;
            }
            else
            {
                param[4].Value = DBNull.Value;
            }
            dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string checkwhetherexambatchwise(string dummyno, string birthdt, string jid, bool provchecked, string regno)
    {
        string str = @"select jap.batchid from  jobapplication jap 
                 inner join job_advt ja on jap.jid=ja.jid
                inner join examMast em on ja.examid =em.examid 
                inner join BatchMaster bm on em.examid=bm.examid and bm.batchid=jap.batchid
                inner join BatchAllocationStatus bas on bm.batchid=bas.BatchId
                where  convert(varchar,bm.examdate,111)>=convert(varchar,getdate(),111) and bas.radmitcard='Y'
				 and birthdt=@birthdt ";
        if (provchecked)
        {
            str += " and jap.jid=@jid and (jap.acstatid=2 or jap.acstatid=4 or jap.acstatid=5) and jap.regno=@regno";
        }
        else
        {
            str += " and dummy_no=@dummyno and jap.acstatid=1 ";
        }
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@dummyno", SqlDbType.BigInt);
        if (dummyno != "")
        {
            param[0].Value = dummyno;
        }
        else
        {
            param[0].Value = DBNull.Value;
        }
        param[1] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
        param[1].Value = birthdt;
        param[2] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[2].Value = jid;
        }
        else
        {
            param[2].Value = DBNull.Value;
        }
        param[3] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[3].Value = regno;
           
        try
        {
            dt = da.GetDataTableQry(str, param);
            string batchid = "";
            if (dt.Rows.Count > 0)
            {
                batchid = dt.Rows[0]["batchid"].ToString();
            }
            return batchid;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public string getexamtype(string examid)
    {
        try
        {
            string examtype = "";
            string str = @"select examtypeid from ExamMast where examid=@examid";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@examid", SqlDbType.Int);
            if (examid != "")
            {
                param[0].Value = examid;
            }
            else
            {
                param[0].Value = DBNull.Value;
            }
            
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count > 0)
            {
                examtype = Convert.ToString(dt.Rows[0]["examtypeid"]);
            }
            return examtype;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int UpdateAdmitcarddownload_emailsent(string applid)
    {

        string str = "update admitcarddownload set emailsent='Y' where applid=@applid ";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;
        
        try
        {
            int id1 = da.ExecuteParameterizedQuery(str, param);
            return id1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    public DataTable fill_personal_data(int applid, string examid)
    {
//        string str = @"select convert(varchar,EndsOn,103) endson,ja.regno,job_source.feeamount fee,ja.jid,ja.applid,postcode,
//                      replace(JobTitle,'[dot]','.') as jobtitle,(replace(Job_Advt.JobTitle,'[dot]','.') + '(Post Code:' + Job_Advt.postcode + ')') as post,ja.dummy_no,
//                      ja.name, ja.fname,  ja.mothername, ja.address, ja.address_per,
//                      ja.PIN, ja.PIN_per, japc.entrydate as pcspEntryDate, 
//                      ja.nationality, ja.mobileno,ja.email,replace(convert(varchar,ja.birthdt,105),'/','')  as birthdt, ja.gender as can_gender, ja.maritalstatus, 
//                      category,dbo.getsubcategory(69617) as SubCategory,
//                      ja.exserviceman,OLEModule,jap.signature,exp_noofyears,convert(varchar,FeeLastDate,103) as FeeLastDate,ja.ExServiceDuration,ja.ContractDuration,
//                      Job_Advt.reqid,CONVERT(varchar,DOBFrom,103) DOBFrom,
//                      CONVERT(varchar,DOBTO,103) DOBTO,CONVERT(varchar,ja.birthdt,103) DOB,LTI,RTI,ja.final,ja.OBCRegion,ja.spousename,ja.CLCNo,convert(varchar,ja.CLCDate,103) as CLCDate,
//                      CastCertIssueAuth,japc.PostCardPhoto,isnull(pdm.docName,'Aadhaar Card') as typeOfID,isnull(aadharno,proofOfIDNo) as idNumber,prg.IDUploaded, 
//					  ja.Entry_date,ja.receive_dt as finaldate,appC.rollno,cm.centername,cm.address,em.dateofexam,em.timeofexam,em.reportingtime
//                      from (select * from  jobapplication union select * from  dsssbonline_recdapp.dbo.jobapplication )ja 
//                      inner join Job_Advt on Job_Advt.jid=ja.jid
//                      inner join advmaster on advmaster.adid=Job_Advt.adid inner join job_source on advmaster.jobsourceid =Job_Source .Id 
//                      left outer join JobApplicationPhoto jap on jap.applid=ja.applid
//                      left join jobApplicationPostCardPhoto japc on japc.ApplId=ja.applid						
//                      left join registration reg on ja.regno=reg.rid 
//                      left join proofOfIDUploaded_Reg prg on reg.rid=prg.regNo and reg.proofOfId=prg.proofOfId 
//                      left join proofOfIdentityDocumentMaster pdm on pdm.docID= reg.proofofid
//                      left join ApplicantCenter appC on appC.applid=ja.applid
//					  left join CentreMaster cm on cm.Centrecode=appC.centercode
//					  left join examMast em on em.examid=appc.examid
//                      where ja.applid=@applid";


        string str = @"select convert(varchar,EndsOn,103) endson,ja.regno,job_source.feeamount fee,ja.jid,ja.applid,postcode,
                      replace(JobTitle,'[dot]','.') as jobtitle,(replace(Job_Advt.JobTitle,'[dot]','.') + '(Post Code:' + Job_Advt.postcode + ')') as post,ja.dummy_no,
                      ja.name, ja.fname,  ja.mothername, ja.address, ja.address_per,
                      ja.PIN, ja.PIN_per, japc.entrydate as pcspEntryDate, 
                      ja.nationality, ja.mobileno,ja.email,replace(convert(varchar,ja.birthdt,105),'/','')  as birthdt, ja.gender as can_gender, ja.maritalstatus, 
                      category,dbo.getsubcategory(ja.applid) as SubCategory,
                      ja.exserviceman,OLEModule,jap.signature,exp_noofyears,convert(varchar,FeeLastDate,103) as FeeLastDate,ja.ExServiceDuration,ja.ContractDuration,
                      Job_Advt.reqid,CONVERT(varchar,DOBFrom,103) DOBFrom,
                      CONVERT(varchar,DOBTO,103) DOBTO,CONVERT(varchar,ja.birthdt,103) DOB,LTI,RTI,ja.final,ja.OBCRegion,ja.spousename,ja.CLCNo,convert(varchar,ja.CLCDate,103) as CLCDate,
                      CastCertIssueAuth,japc.PostCardPhoto,isnull(pdm.docName,'Aadhaar Card') as typeOfID,isnull(aadharno,proofOfIDNo) as idNumber,prg.IDUploaded, 
					  ja.Entry_date,ja.receive_dt as finaldate,appC.rollno,cm.centername,cm.address,bm.examdate as dateofexam,bm.examtime as timeofexam,bm.reportingtime
                      from (select * from  jobapplication union select * from  dsssbonline_recdapp.dbo.jobapplication )ja 
                      left join Job_Advt on Job_Advt.jid=ja.jid
                      left join advmaster on advmaster.adid=Job_Advt.adid left join job_source on advmaster.jobsourceid =Job_Source .Id 
                      left join JobApplicationPhoto jap on jap.applid=ja.applid
                      left join jobApplicationPostCardPhoto japc on japc.ApplId=ja.applid						
                      left join registration reg on ja.regno=reg.rid 
                      left join proofOfIDUploaded_Reg prg on reg.rid=prg.regNo and reg.proofOfId=prg.proofOfId 
                      left join proofOfIdentityDocumentMaster pdm on pdm.docID= reg.proofofid
                      left join ApplicantCenter appC on appC.applid=ja.applid
					  left join BatchMaster bm on bm.examid=appC.examid and appc.batchid=bm.batchid
					  left join CentreMaster cm on cm.Centrecode=bm.centercode where ja.applid=@applid and bm.examid=@examid";

        string str2 = @"select convert(varchar,EndsOn,103) endson,ja.regno,job_source.feeamount fee,ja.jid,ja.applid,postcode,
                      replace(job.JobTitle,'[dot]','.') as jobtitle,(replace(Job_Advt.JobTitle,'[dot]','.') + '(Post Code:' + Job_Advt.postcode + ')') as post,ja.dummy_no,
                      ja.name, ja.fname,  ja.mothername, ja.address, ja.address_per,
                      ja.PIN, ja.PIN_per, japc.entrydate as pcspEntryDate, 
                      ja.nationality, ja.mobileno,ja.email,replace(convert(varchar,ja.birthdt,105),'/','')  as birthdt, ja.gender as can_gender, ja.maritalstatus, 
                      category,dbo.getsubcategory(ja.applid) as SubCategory,
                      ja.exserviceman,OLEModule,jap.signature,exp_noofyears,convert(varchar,FeeLastDate,103) as FeeLastDate,ja.ExServiceDuration,ja.ContractDuration,
                      Job_Advt.reqid,CONVERT(varchar,DOBFrom,103) DOBFrom,
                      CONVERT(varchar,DOBTO,103) DOBTO,CONVERT(varchar,ja.birthdt,103) DOB,LTI,RTI,ja.final,ja.OBCRegion,ja.spousename,ja.CLCNo,convert(varchar,ja.CLCDate,103) as CLCDate,
                      CastCertIssueAuth,japc.PostCardPhoto,isnull(pdm.docName,'Aadhaar Card') as typeOfID,isnull(aadharno,proofOfIDNo) as idNumber,prg.IDUploaded, 
					  ja.Entry_date,ja.receive_dt as finaldate,appC.rollno,cm.centername,cm.address,bm.examdate as dateofexam,bm.examtime as timeofexam,bm.reportingtime
                      from (select * from  jobapplication union select * from  dsssbonline_recdapp.dbo.jobapplication )ja 
                      left join Job_Advt on Job_Advt.jid=ja.jid
                      left join advmaster on advmaster.adid=Job_Advt.adid left join job_source on advmaster.jobsourceid =Job_Source .Id 
                      left join JobApplicationPhoto jap on jap.applid=ja.applid
                      left join jobApplicationPostCardPhoto japc on japc.ApplId=ja.applid						
                      left join registration reg on ja.regno=reg.rid 
                      left join proofOfIDUploaded_Reg prg on reg.rid=prg.regNo and reg.proofOfId=prg.proofOfId 
                      left join proofOfIdentityDocumentMaster pdm on pdm.docID= reg.proofofid
                      left join ApplicantCenter appC on appC.applid=ja.applid
					  left join BatchMaster bm on bm.examid=appC.examid and appc.batchid=bm.batchid
					  left join CentreMaster cm on cm.Centrecode=bm.centercode
					  inner join ResultVerification rv on bm.examid = rv.examid
					  inner join (select jid,JobTitle from job_advt where reqid in (select ce.DeptReqId from Job_Advt ja 
							inner join ResultVerification rv on rv.jid=ja.jid
							inner join CombinedEntry ce on ja.reqid = ce.DeptReqId
							where rv.tier = 3)) job on rv.jid = job.jid
					  where ja.applid=@applid and bm.examid=@examid";

        string str1 = @"select applid from combined_applicant_result where applid =  @applid";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = applid;
        param[1] = new SqlParameter("@examid", SqlDbType.VarChar);
        param[1].Value = examid;

        SqlParameter[] param2 = new SqlParameter[2];
        param2[0] = new SqlParameter("@applid", SqlDbType.Int);
        param2[0].Value = applid;
        param2[1] = new SqlParameter("@examid", SqlDbType.VarChar);
        param2[1].Value = examid;

        SqlParameter[] param1 = new SqlParameter[1];        
        param1[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param1[0].Value = applid;

        try
        {
            DataTable Combresult = da.GetDataTableQry(str1, param1);
            if (Combresult.Rows.Count != 0)
            {
                dt = da.GetDataTableQry(str2, param2);
            }
            else
            {

                dt = da.GetDataTableQry(str, param);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable get_ExamControllerSign()
    {
        string str = @"select top(1)CName,CSig from SignatureMaster order by SigId desc";
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