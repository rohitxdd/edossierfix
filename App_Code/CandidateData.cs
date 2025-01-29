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
using System.Security.Cryptography;

/// <summary>
/// Summary description for CandidateData
/// </summary>fill_personal_data
public class CandidateData
{

    public CandidateData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    string str;

    DataTable dt = new DataTable();
    ApplicantData da = new ApplicantData();
    //public DataTable GetMessage()
    //{
    //    str = "Select msgid,pno,message,msg_file,fileexist from MessageMaster ";

    //    str += " order by pno ";
    //    try
    //    {
    //        dt = da.GetDataTable(str);
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //public DataTable getmsg(string msgid)
    //{
    //    str = @"SELECT msg_file,message,fileexist from MessageMaster where 1=1";
    //    if (msgid != "")
    //    {
    //        str += "  and msgid=@msgid";
    //    }
    //    int j = 0;
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[j] = new SqlParameter("@msgid", SqlDbType.Int, 4);
    //    if (msgid == "")
    //    {
    //        param[j].Value = System.DBNull.Value; ;
    //    }
    //    else
    //    {
    //        param[j].Value = msgid; 
    //    }
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
    public DataTable GetMessage(string flag)
    {
        str = "Select msgid,pno,message,convert(varchar,m_edate,103) m_edate,fileexist from MessageMaster where flag=@flag and status='Y' ";

        str += " order by pno ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@flag", SqlDbType.Char, 1);
        param[0].Value = flag;
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
    public DataTable getmsg(string msgid, string type)
    {
        //str = @"SELECT msg_file,message,fileexist from MessageMaster where flag=@flag";
        str = @"SELECT msg_file,message,fileexist from MessageMaster where flag=@flag and status='Y'";
        if (msgid != "")
        {
            str += "  and msgid=@msgid";
        }
        //int j = 0;
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@msgid", SqlDbType.Int, 4);
        if (msgid == "")
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = msgid;
        }
        param[1] = new SqlParameter("@flag", SqlDbType.Char, 1);
        param[1].Value = type;
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

    public DataTable GetJobAdvt(string flag)
    {
        //        str = @"SELECT advMaster.adid,jobdescription,flag,Job_Advt.gender,convert(varchar(20),convert(varchar,advMaster.AdNo)+'/'+convert(varchar(20), advMaster.AdYear)) 
        //                as ADVT_NO,Job_Advt.postcode,REPLACE((Job_Advt.JobTitle + ' :: Post Code:' + Job_Advt.postcode + ' '),'[dot]','.') as announcement,AdNo,AdYear ,REPLACE(Job_Advt.JobTitle,'[dot]','.') as JobTitle, CONVERT(VARCHAR,advMaster.StartsFrom,103) StartsFrom, 
        //                CONVERT(VARCHAR,advMaster.EndsOn,103) EndsOn,isnull(CONVERT(VARCHAR,advMaster.FeeLastDate,103),'') FeeLastDate,CONVERT(VARCHAR,Job_Advt.DOBFrom,103) DOBFrom, 
        //                CONVERT(VARCHAR,Job_Advt.DOBTO,103) DOBTO, convert(varchar,feeamount) +'/-' fee,REPLACE(essential_qual,'[dot]','.') as essential_qual,REPLACE(desire_qual,'[dot]','.') as desire_qual,REPLACE(essential_exp,'[dot]','.') as essential_exp,REPLACE(desire_exp,'[dot]','.') as desire_exp,
        //                jid jobid,reqid FROM Job_Advt 
        //                inner join advMaster on advMaster.adid=Job_Advt.adid
        //                inner join Job_Source on Job_Source.Id=advMaster.jobsourceid 
        //                where CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
        //                and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
        //                cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
        //                order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int) 
        //                --order by Startsfrom,postcode"; 
        str = @"SELECT advMaster.adid,advMaster.flag,jobdescription,Job_Advt.gender,convert(varchar(20),convert(varchar,advMaster.AdNo)+'/'+convert(varchar(20), advMaster.AdYear)) 
                   as ADVT_NO,Job_Advt.postcode,REPLACE((Job_Advt.JobTitle + ' :: Post Code:' + Job_Advt.postcode + ' '),'[dot]','.') as announcement,AdNo,AdYear ,
                    REPLACE(Job_Advt.JobTitle,'[dot]','.') as JobTitle,
                    CONVERT(VARCHAR,advMaster.StartsFrom,103) StartsFrom_org,
                    CONVERT(VARCHAR,advMaster.EndsOn,103) EndsOn_org,  
                    case Reopened 
                    when 'Y' then
                    CONVERT(VARCHAR,reopenadvt.StartsFrom,103) 
                    else
                    CONVERT(VARCHAR,advMaster.StartsFrom,103) 
                    end
                    StartsFrom,
                    case Reopened 
                    when 'Y' then
                    CONVERT(VARCHAR,reopenadvt.EndsOn,103) 
                    else
                    CONVERT(VARCHAR,advMaster.EndsOn,103) 
                    end 
                    EndsOn,
                    case Reopened 
                    when 'Y' then
                    isnull(CONVERT(VARCHAR,reopenadvt.FeeLastDate,103),'') 
                    else
                    isnull(CONVERT(VARCHAR,advMaster.FeeLastDate,103),'') 
                    end
                    FeeLastDate,
                    CONVERT(VARCHAR,Job_Advt.DOBFrom,103) DOBFrom, 
                    CONVERT(VARCHAR,Job_Advt.DOBTO,103) DOBTO, convert(varchar,feeamount) +'/-' fee,REPLACE(essential_qual,'[dot]','.') as essential_qual,REPLACE(desire_qual,'[dot]','.') as desire_qual,REPLACE(essential_exp,'[dot]','.') as essential_exp,REPLACE(desire_exp,'[dot]','.') as desire_exp,
                    Job_Advt.jid jobid,reqid,agevalidationexmpt FROM Job_Advt 
                    inner join advMaster on advMaster.adid=Job_Advt.adid
                    left outer join reopenadvt on advMaster.adid=reopenadvt.adid and AdvMaster.Reopened='Y' and reopenadvt.reopenstatus='C' and  reopenadvt.ReleaseStatus='Y'
                    inner join Job_Source on Job_Source.Id=advMaster.jobsourceid 
                    where (
                    (
                    CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
                    and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
                    cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) and advMaster.ReleaseStatus='Y'
                    )
                    or
                    (
                   
                    (CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
                    and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >=
                    cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME))  and reopenadvt.ReleaseStatus ='Y' ";
        if (flag == "")
        {

            str += " and Job_Advt.jid in(select jid from reopenpost where oldapplication is null  and  newadid in(select newadid from reopenadvt where (CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)  and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >= cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME))) ) ))";
        }
        else
        {
            str += " and Job_Advt.jid in(select jid from reopenpost where oldapplication='Y' and  newadid in(select newadid from reopenadvt where (CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)  and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >= cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME))) )))";
            if (HttpContext.Current.Session["rid"] != null)
            {
                str += " and Job_Advt.postcode in (select postcode from oldpostmatching where regno='" + HttpContext.Current.Session["rid"].ToString() + "')";
            }//oldpostmapping was replaced with oldpostmatching for 90/09 Dated:30-01-2023
        }



        str += "  order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as Varchar) ";

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
    public DataTable GetJobAdvt(string AdvtNo, string flag)
    {

        //        str = @"SELECT advMaster.adid,flag,jobdescription,Job_Advt.gender,convert(varchar(20),convert(varchar,advMaster.AdNo)+'/'+convert(varchar(20), advMaster.AdYear)) 
        //                as ADVT_NO,Job_Advt.postcode,REPLACE((Job_Advt.JobTitle + ' :: Post Code:' + Job_Advt.postcode + ' '),'[dot]','.') as announcement,AdNo,AdYear ,REPLACE(Job_Advt.JobTitle,'[dot]','.') as JobTitle, CONVERT(VARCHAR,advMaster.StartsFrom,103) StartsFrom, 
        //                CONVERT(VARCHAR,advMaster.EndsOn,103) EndsOn,isnull(CONVERT(VARCHAR,advMaster.FeeLastDate,103),'') FeeLastDate,CONVERT(VARCHAR,Job_Advt.DOBFrom,103) DOBFrom, 
        //                CONVERT(VARCHAR,Job_Advt.DOBTO,103) DOBTO, convert(varchar,feeamount) +'/-' fee,REPLACE(essential_qual,'[dot]','.') as essential_qual,REPLACE(desire_qual,'[dot]','.') as desire_qual,REPLACE(essential_exp,'[dot]','.') as essential_exp,REPLACE(desire_exp,'[dot]','.') as desire_exp,
        //                jid jobid,reqid FROM Job_Advt 
        //                inner join advMaster on advMaster.adid=Job_Advt.adid
        //                inner join Job_Source on Job_Source.Id=advMaster.jobsourceid 
        //                where CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
        //                and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
        //                cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)
        //                order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int)  
        //                --order by Startsfrom,postcode";


        //str = @"SELECT advMaster.adid,flag,jobdescription,Job_Advt.gender,convert(varchar(20),convert(varchar,advMaster.AdNo)+'/'+convert(varchar(20), advMaster.AdYear)) 
        //           as ADVT_NO,Job_Advt.postcode,REPLACE((Job_Advt.JobTitle + ' :: Post Code:' + Job_Advt.postcode + ' '),'[dot]','.') as announcement,AdNo,AdYear ,
        //            REPLACE(Job_Advt.JobTitle,'[dot]','.') as JobTitle, 
        //            CONVERT(VARCHAR,advMaster.StartsFrom,103) StartsFrom_org,
        //            CONVERT(VARCHAR,advMaster.EndsOn,103) EndsOn_org, 
        //            case Reopened 
        //            when 'Y' then
        //            CONVERT(VARCHAR,reopenadvt.StartsFrom,103) 
        //            else
        //            CONVERT(VARCHAR,advMaster.StartsFrom,103) 
        //            end
        //            StartsFrom,
        //            case Reopened 
        //            when 'Y' then
        //            CONVERT(VARCHAR,reopenadvt.EndsOn,103) 
        //            else
        //            CONVERT(VARCHAR,advMaster.EndsOn,103) 
        //            end 
        //            EndsOn,
        //            case Reopened 
        //            when 'Y' then
        //            isnull(CONVERT(VARCHAR,reopenadvt.FeeLastDate,103),'') 
        //            else
        //            isnull(CONVERT(VARCHAR,advMaster.FeeLastDate,103),'') 
        //            end
        //            FeeLastDate,
        //            CONVERT(VARCHAR,Job_Advt.DOBFrom,103) DOBFrom, 
        //            CONVERT(VARCHAR,Job_Advt.DOBTO,103) DOBTO, convert(varchar,feeamount) +'/-' fee,REPLACE(essential_qual,'[dot]','.') as essential_qual,REPLACE(desire_qual,'[dot]','.') as desire_qual,REPLACE(essential_exp,'[dot]','.') as essential_exp,REPLACE(desire_exp,'[dot]','.') as desire_exp,
        //            Job_Advt.jid jobid,reqid,agevalidationexmpt FROM Job_Advt 
        //            inner join advMaster on advMaster.adid=Job_Advt.adid
        //            left outer join reopenadvt on advMaster.adid=reopenadvt.adid and AdvMaster.Reopened='Y' 
        //            inner join Job_Source on Job_Source.Id=advMaster.jobsourceid 
        //            where (
        //            (
        //            CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
        //            and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
        //            cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) and advMaster.ReleaseStatus='Y'
        //            )
        //            or
        //            (
        //            --and reopenadvt.ReleaseStatus ='Y' 
        //            (CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
        //            and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >=
        //            cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)) and reopenadvt.ReleaseStatus ='Y'";


        //Devesh Added Cut_OffDate
        str = @"SELECT advMaster.adid,jobdescription,Job_Advt.gender,convert(varchar(20),convert(varchar,advMaster.AdNo)+'/'+convert(varchar(20), advMaster.AdYear)) 
                   as ADVT_NO,Job_Advt.postcode,REPLACE((Job_Advt.JobTitle + ' :: Post Code:' + Job_Advt.postcode + ' '),'[dot]','.') as announcement,AdNo,AdYear ,
                    REPLACE(Job_Advt.JobTitle,'[dot]','.') as JobTitle, 
                    CONVERT(VARCHAR,advMaster.StartsFrom,103) StartsFrom_org,
                    CONVERT(VARCHAR,advMaster.EndsOn,103) EndsOn_org, 
                    case Reopened 
                    when 'Y' then
                    CONVERT(VARCHAR,reopenadvt.StartsFrom,103) 
                    else
                    CONVERT(VARCHAR,advMaster.StartsFrom,103) 
                    end
                    StartsFrom,
                    case
					when Reopened = 'Y' and reopenadvt.Cut_OffDate is not null then CONVERT(VARCHAR,reopenadvt.Cut_OffDate,103)
					when Reopened = 'Y' and reopenadvt.Cut_OffDate is null then CONVERT(VARCHAR,reopenadvt.EndsOn,103)
					when Reopened is null and advmaster.Cut_OffDate is not null then CONVERT(VARCHAR,advMaster.Cut_OffDate,103) 
					when Reopened is null and advmaster.Cut_OffDate is null then CONVERT(VARCHAR,advMaster.EndsOn,103) 
                    end 
                    EndsOn,
                    case Reopened 
                    when 'Y' then
                    isnull(CONVERT(VARCHAR,reopenadvt.FeeLastDate,103),'') 
                    else
                    isnull(CONVERT(VARCHAR,advMaster.FeeLastDate,103),'') 
                    end
                    FeeLastDate,
                    CONVERT(VARCHAR,Job_Advt.DOBFrom,103) DOBFrom, 
                    CONVERT(VARCHAR,Job_Advt.DOBTO,103) DOBTO, convert(varchar,feeamount) +'/-' fee,REPLACE(essential_qual,'[dot]','.') as essential_qual,REPLACE(desire_qual,'[dot]','.') as desire_qual,REPLACE(essential_exp,'[dot]','.') as essential_exp,REPLACE(desire_exp,'[dot]','.') as desire_exp,
                    Job_Advt.jid jobid,reqid,agevalidationexmpt FROM Job_Advt 
                    inner join advMaster on advMaster.adid=Job_Advt.adid
                    left outer join reopenadvt on advMaster.adid=reopenadvt.adid and AdvMaster.Reopened='Y' 
                    inner join Job_Source on Job_Source.Id=advMaster.jobsourceid 
                    where (
                    (
                    CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
                    and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
                    cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) and advMaster.ReleaseStatus='Y'
                    )
                    or
                    (
                    --and reopenadvt.ReleaseStatus ='Y' 
                    (CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
                    and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >=
                    cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)) and reopenadvt.ReleaseStatus ='Y'";
        if (flag == "")
        {

            str += " and Job_Advt.jid in(select jid from reopenpost where oldapplication is null and  newadid in(select newadid from reopenadvt where (CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)  and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >= cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME))) )))";
        }
        else
        {
            str += " and Job_Advt.jid in(select jid from reopenpost where oldapplication='Y' and  newadid in(select newadid from reopenadvt where (CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)  and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >= cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME))) )))";
            if (HttpContext.Current.Session["rid"] != null)
            {
                str += " and Job_Advt.postcode in (select postcode from oldpostmatching where regno='" + HttpContext.Current.Session["rid"].ToString() + "')";
            }//oldpostmapping was replaced with oldpostmatching for 90/09 Dated:30-01-2023
        }



        str += "  order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as Varchar) ";

        if (AdvtNo != "")
        {
            str += " and AdvtNo=@AdvtNo";

        }
        //str += "  order by EndsOn";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@AdvtNo", SqlDbType.Int, 4);
        if (AdvtNo == "")
        {
            param[j].Value = System.DBNull.Value; ;
        }
        else
        {
            param[j].Value = AdvtNo;
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

    public DataTable SelectJobSource(string jid)
    {
        str = @"SELECT Name from advmaster 
                inner join Job_Advt on Job_Advt.jid=advmaster.adid
                inner join Job_Source  on advmaster.jobsourceid=Job_Source.Id
                where Job_Advt.jid=@jid";

        // str += " order by name";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        if (jid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = jid;
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

    public DataTable Find(string jid)
    {
        str = @"SELECT [JobTitle],[JobDescription],[PostCode],
                convert(varchar,[StartsFrom],103) StartsFrom,convert(varchar,EndsOn,103) EndsOn ,
                convert(varchar,DOBFrom,103) DOBFrom,convert(varchar,DOBTO,103) DOBTO,[Education],
                [Experience],[payment],[center],[centerlist],[Seat_Gen],[Seat_SC],[Seat_ST],[Seat_SEBC],[PDF],
                [status],[status_by],[status_dt],[PhyReceivedDt],[fee],[JobCategory],[JobGroup],[CallLetterDt],
                [ExamDt],[ph_apply],[AddInfo],[Data_edit_by],[Data_veri_1st],[Data_veri_final],[CallView_dt],
                [CallEnd_dt],[ExamTime],[Gen_ConfmNo_by],[App_delete_by],[App_delete_varify_by],[App_rej_by],
                [App_rej_fvarify_by],[App_rej_finalvarify_by],[Gen_RollNo_by],[AddEducation],[CompEducation],
                [AgeR_SC],[AgeR_ST],[AgeR_SEBC],[AgeR_Female],[AgeR_PH],[AgeR_ExSerMan],[showEnclosure],
                [ApplicationCount],[gender],[Multiple],[phlevel_from],[phlevel_to],[POCharge],[fees_rem],
                [POCharge_rem] from Job_Advt where jid=@jid";


        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        if (jid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = jid;
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

    public DataTable GetEducationMinimumClass(string reqid, string standard, string qtype, string groupno)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        str = @"SELECT uid,name,reqid,tbledu_trn.id,groupno from tbledu_TRN 
        inner join tbledu on tbledu_trn.id=tbledu.id 
        where reqid=@reqid ";
        if (qtype != "")
        {
            str += condn;
        }

        if (standard != "")
        {
            str += " and stid=@stid";
            groupno = "";
        }
        if (groupno != "")
        {
            //str += " and groupno=@groupno and stid <>'7' ";
            str += " and stid <>'7' ";
            //str += " and groupno=@groupno";
        }
        // str += " order by name";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@stid", SqlDbType.Int);
        if (standard == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (qtype == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = qtype;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
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



    public DataTable GetEducationMinimumClass_getbygroup(string reqid, string standard, string qtype, List<int> groupno)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        //str = @"SELECT uid,name,reqid,tbledu_trn.id,groupno from tbledu_TRN 
        // inner join tbledu on tbledu_trn.id=tbledu.id 
        // where reqid=@reqid ";

        str = @"select distinct tbledu_TRN.id,reqid,name from tbledu_TRN
		 inner join tbledu on tbledu_trn.id=tbledu.id where reqid=@reqid ";

        if (qtype != "")
        {
            str += condn;
        }

        if (standard != "")
        {
            str += " and stid=@stid";
            //groupno = "";
        }
        if (groupno != null)
        {
            // str += " and groupno=@groupno and stid <>'7' ";
            //str += " and stid <>'7' ";
            //str += "and groupno=@groupno";
            str += " and  groupno in(" + (groupno.Count > 0 ? string.Join(",", groupno.ToArray()) : System.DBNull.Value.ToString()) + ") ";
        }
        // str += " order by name";
        SqlParameter[] param = new SqlParameter[3];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@stid", SqlDbType.Int);
        if (standard == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (qtype == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = qtype;
        }
        j++;

        //param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        //if (groupno ==null )
        //{
        //    param[j].Value = System.DBNull.Value;
        //}
        //else
        //{
        //    param[j].Value = groupno;
        //}

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


    public DataTable GetEducationMinimumClass_special(string reqid, string standard, string qtype, string groupno)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        str = @"SELECT standard,uid,name from tbledu_TRN 
                inner join tbledu on tbledu_trn.id=tbledu.id 
                inner join standardMaster on standardMaster.id=tbledu.stid and standardMaster.TYPE='S' 
                where reqid=@reqid " + condn;


        //if (qtype=="D")
        //{
        //    str = @"SELECT  standard,uid,name from  tbledu_TRN  inner join tbledu on tbledu_TRN.id =tbledu.id  inner join standardMaster on standardMaster.id=tbledu.stid where tbledu_TRN.reqid= @reqid AND tbledu_TRN.qtype='G'";

        //}

        if (standard != "")
        {
            str += " and stid=@stid";
        }
        if (groupno != "")
        {
            str += " and groupno=@groupno ";
        }
        // str += " order by name";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@stid", SqlDbType.Int);
        if (standard == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (qtype == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = qtype;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
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


    public DataTable SelectCasteType()
    {
        str = "SELECT categcode FROM categorymaster order by id";
        //   str = "select CatCode from  tbl_CatWiseVacancy where CatOrSub='C'and reqid=@reqid";      
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

    //for edu_desirequlification by Essential

    public DataTable Getedu_desirequlification_by_Essential_special(string reqid, string qtype)
    {
        string condn = "";
        if (qtype == "")
        {
            condn = " and (qtype='E' or qtype='G' ) ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        str = @"Select stid from tbledu where  id= (select distinct id from tbledu_TRN where  reqid=@reqid) " + condn;


        //if (qtype=="D")
        //{
        //    str = @"SELECT  standard,uid,name from  tbledu_TRN  inner join tbledu on tbledu_TRN.id =tbledu.id  inner join standardMaster on standardMaster.id=tbledu.stid where tbledu_TRN.reqid= @reqid AND tbledu_TRN.qtype='G'";

        //}


        // str += " order by name";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;

        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (qtype == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = qtype;
        }
        j++;


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


    public DataTable FillDiv()
    {
        str = "SELECT Division FROM DivMaster order by prno";

        // str += " order by name";
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

    public DataTable SelectState()
    {
        str = "SELECT code,state FROM m_state order by state";

        // str += " order by name";
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
    public DataTable SelectPH(int reqid)
    {
        str = @"SELECT PH_Code,PH_Cat_Desc
            FROM SubCat_Master_PH scm inner join SubSubCatWiseVacancy vac on scm.PH_Cat_Desc=vac.PH_SubCatCode and reqid=@reqid ";

        // str += " order by name";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        param[j].Value = reqid;

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

    public DataTable get_post_after_c(string regno)
    {
        str = @"select Job_Advt.[jid] jid,applid as applid
            ,replace([JobTitle],'[dot]','.')+' Post Code :('+postcode+')' post
            from Job_Advt 
            inner join JobApplication on JobApplication.jid=Job_Advt.jid
            where adid in (select adid from advmaster where (CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
                and CAST(CONVERT(VARCHAR,advMaster.FeeLastDate,101) AS DATETIME) >=
                cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)))
            and RegNo=@regno and dummy_no is not null";

        SqlParameter[] param = new SqlParameter[1];
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
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;
    }
    public DataTable get_post_print_app(string regno)
    {
        str = @"select Job_Advt.[jid] jid,applid as applid
                ,replace([JobTitle],'[dot]','.')+' Post Code :('+postcode+')' post
                from Job_Advt 
                inner join JobApplication on JobApplication.jid=Job_Advt.jid
                where RegNo=@regno and dummy_no is not null

		union

		select Job_Advt.[jid] jid,applid as applid
                	,replace([JobTitle],'[dot]','.')+' Post Code :('+postcode+')' post
                	from Job_Advt 
                	inner join testdb_dsssbonline_recdapp.dbo.JobApplication on  testdb_dsssbonline_recdapp.dbo.JobApplication.jid=Job_Advt.jid
                	where RegNo=@regno and dummy_no is not null";

        SqlParameter[] param = new SqlParameter[1];
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
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;
    }
    public DataTable get_application(string regno)
    {
        str = @"select Job_Advt.[jid] jid,applid as applid
                ,replace([JobTitle],'[dot]','.')+' Post Code :('+postcode+')' post
                from Job_Advt 
                inner join JobApplication on JobApplication.jid=Job_Advt.jid
                where 
                 RegNo=@regno and dummy_no is not null
                 and (examid is null  or examid not in (select examid from examMast where dateofexam<GETDATE()))";

        SqlParameter[] param = new SqlParameter[1];
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
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;
    }


    //Checking Advertisement ends but Admit card will be downloaded after Advertisement Date is over.
    public DataTable get_AdmitCard(string regno)
    {
        str = @"select Job_Advt.[jid] jid,applid as applid
            ,replace([JobTitle],'[dot]','.')+' Post Code :('+postcode+')' post
            from Job_Advt 
            inner join JobApplication on JobApplication.jid=Job_Advt.jid
            where RegNo=@regno and dummy_no is not null ";

        SqlParameter[] param = new SqlParameter[1];
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
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;
    }
    public DataTable get_app_status(string regno)
    {
        str = @"select Job_Advt.[jid] jid,applid as applid
            ,replace([JobTitle],'[dot]','.')+' Post Code :('+postcode+')' post
            from Job_Advt 
            inner join JobApplication on JobApplication.jid=Job_Advt.jid
            where RegNo=@regno";

        SqlParameter[] param = new SqlParameter[1];
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
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;
    }


    public DataTable getjobno(string applid)
    {
        //        str = @"select ja.jid,feereq,ja.applid,ja.name,ja.gender,ja.fname,ja.mothername,ja.[address]+' '+ja.PIN +' MobNo'+ja.mobileno as address,
        //                ja.nationality,ja.ph,ja.exserviceman,ja.category,ja.subcategory,
        //                ja.mobileno,ja.email,convert(varchar,ja.birthdt,103) as birthdt ,tbledu.name subject,ja.dummy_no,
        //                APPNO,je.percentage,je.board,
        //                je.educlass,jp.PhotoID,jp.OLEModule,jp.Signature,ja.SubCategory,dummy_no 
        //                from 
        //                JobApplication ja 
        //                inner join JobEducation je on ja.applid=je.applid  
        //                left outer join JobApplicationPhoto jp on jp.applid=je.applid 
        //                inner join  tbledu_TRN on tbledu_TRN.jid = ja.jid
        //                inner join tbledu on tbledu.id = tbledu_TRN.id  
        //                where ja.applid=@applid";

        //        str = @"select ja.jid,feereq,ja.applid,ja.name,ja.gender,ja.fname,ja.mothername,ja.[address]+' '+ja.PIN +' MobNo'+ja.mobileno as address,
        //                ja.nationality,ja.ph,ja.exserviceman,ja.category,ja.subcategory,
        //                ja.mobileno,ja.email,convert(varchar,ja.birthdt,103) as birthdt ,tbledu.name subject,ja.dummy_no,
        //                APPNO,je.percentage,je.board,
        //                je.educlass,jp.PhotoID,jp.OLEModule,jp.Signature,ja.SubCategory,dummy_no 
        //                from 
        //                JobApplication ja 
        //                inner join JobEducation je on ja.applid=je.applid  
        //                left outer join JobApplicationPhoto jp on jp.applid=je.applid 
        //                inner join  tbledu_TRN on tbledu_TRN.uid = je.qid
        //                inner join tbledu on tbledu.id = tbledu_TRN.id  
        //                where ja.applid=@applid";


        //new query for empty jobeduction row modified on Dated: 20-09-2023
        //str = @"select ja.jid,feereq,ja.applid,ja.name,ja.gender,ja.fname,ja.mothername,ja.[address]+' '+ja.PIN +' MobNo'+ja.mobileno as address,
        //        ja.nationality,ja.ph,ja.exserviceman,ja.category,ja.subcategory,
        //        ja.mobileno,ja.email,convert(varchar,ja.birthdt,103) as birthdt ,tbledu.name subject,ja.dummy_no,
        //        APPNO,je.percentage,je.board,
        //        je.educlass,jp.PhotoID,jp.OLEModule,jp.Signature,ja.SubCategory,dummy_no 
        //        from 
        //        JobApplication ja 
        //        left join JobEducation je on ja.applid=je.applid
        //        left outer join JobApplicationPhoto jp on jp.applid=je.applid 
        //        left join  tbledu_TRN on tbledu_TRN.uid = je.qid
        //        left join tbledu on tbledu.id = tbledu_TRN.id  
        //        where ja.applid=@applid";

        // New Query 09/05/2024
        str = @"select ja.jid,feereq,ja.applid,ja.name,ja.gender,ja.fname,ja.mothername,ja.[address]+' '+ja.PIN +' MobNo'+ja.mobileno as address,
                    ja.nationality,ja.ph,ja.exserviceman,ja.category,ja.subcategory,
                    ja.mobileno,ja.email,convert(varchar,ja.birthdt,103) as birthdt ,tbledu.name subject,ja.dummy_no,
                    APPNO,je.percentage,je.board,
                    je.educlass,jp.PhotoID,jp.OLEModule,jp.Signature,ja.SubCategory,dummy_no 
                    from 
                    JobApplication ja --05/06/1979
                    left join JobEducation je on ja.applid=je.applid
                    left outer join JobApplicationPhoto jp on jp.applid=je.applid 
                    left join  tbledu_TRN on tbledu_TRN.uid = je.qid
                    left join tbledu on tbledu.id = tbledu_TRN.id  
                    where ja.applid=@applid";

        string str1 = @"select ja.jid,feereq,ja.applid,ja.name,ja.gender,ja.fname,ja.mothername,ja.[address]+' '+ja.PIN +' MobNo'+ja.mobileno as address,
                    ja.nationality,ja.ph,ja.exserviceman,ja.category,ja.subcategory,
                    ja.mobileno,ja.email,convert(varchar,ja.birthdt,103) as birthdt ,tbledu.name subject,ja.dummy_no,
                    APPNO,je.percentage,je.board,
                    je.educlass,jp.PhotoID,jp.OLEModule,jp.Signature,ja.SubCategory,dummy_no 
                    from 
                    testdb_dsssbonline_recdapp.dbo.JobApplication ja 
                    left join testdb_dsssbonline_recdapp.dbo.JobEducation je on ja.applid=je.applid
                    left outer join testdb_dsssbonline_recdapp.dbo.JobApplicationPhoto jp on jp.applid=je.applid 
                    left join  tbledu_TRN on tbledu_TRN.uid = je.qid
                    left join tbledu on tbledu.id = tbledu_TRN.id  
                    where ja.applid=@applid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        if (applid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(applid);
        }
        try
        {
            DataTable dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 0)
            {
                dt = da.GetDataTableQry(str1, param);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable checkphoto(string applid)
    {
        string instr = @"select ISNULL(DATALENGTH(OLEModule),0) OLEModule,ISNULL(DATALENGTH(Signature),0) Signature,ISNULL(DATALENGTH(LTI),0) LTI,ISNULL(DATALENGTH(RTI),0) RTI from jobapplicationphoto where ApplId=@ApplId  ";

        string str1 = @"select ISNULL(DATALENGTH(OLEModule),0) OLEModule,ISNULL(DATALENGTH(Signature),0) Signature,ISNULL(DATALENGTH(LTI),0) LTI,ISNULL(DATALENGTH(RTI),0) RTI from testdb_dsssbonline_recdapp.dbo.jobapplicationphoto where  ApplId=@ApplId";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = Int32.Parse(applid);
        try
        {
            DataTable dt = da.GetDataTableQry(instr, param);
            if (dt.Rows.Count == 0)
            {
                dt = da.GetDataTableQry(str1, param);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable checkphotondsign(string applid)
    {
        string instr = "select ApplId from jobapplicationphoto where ApplId=@ApplId and OLEModule is not null and Signature is not null";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = Int32.Parse(applid);
        try
        {
            DataTable dt = da.GetDataTableQry(instr, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public int insertphoto(string appno, byte[] image, string ip, string UPDATEDT)
    {

        string str = "insert into JobApplicationPhoto (ApplId, OLEModule, IP, UPDATEDT) values (@ApplId,@OLEModule, @IP, @UPDATEDT) select SCOPE_IDENTITY() ";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@OLEModule", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UPDATEDT", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
        try
        {
            int id1 = Convert.ToInt32(da.ExecScaler(str, param));
            return id1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int insertsignature(string appno, byte[] image, string ip, string UPDATEDT)
    {

        string str = "insert into JobApplicationPhoto (ApplId, Signature, IP, UPDATEDT) values (@ApplId,@Signature, @IP, @UPDATEDT) select SCOPE_IDENTITY() ";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@Signature", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UPDATEDT", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
        try
        {
            int id1 = Convert.ToInt32(da.ExecScaler(str, param));
            return id1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int insertLTI(string appno, byte[] image, string ip, string UPDATEDT)
    {

        string str = "insert into JobApplicationPhoto (ApplId, LTI, IP, UPDATEDT) values (@ApplId,@LTI, @IP, @UPDATEDT) select SCOPE_IDENTITY() ";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@LTI", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UPDATEDT", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
        try
        {
            int id1 = Convert.ToInt32(da.ExecScaler(str, param));
            return id1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int insertRTI(string appno, byte[] image, string ip, string UPDATEDT)
    {

        string str = "insert into JobApplicationPhoto (ApplId, RTI, IP, UPDATEDT) values (@ApplId,@RTI, @IP, @UPDATEDT) select SCOPE_IDENTITY() ";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@RTI", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UPDATEDT", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
        try
        {
            int id1 = Convert.ToInt32(da.ExecScaler(str, param));
            return id1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int updatephoto(string appno, byte[] image, string ip, string UPDATEDT)
    {
        string str = "update JobApplicationPhoto set OLEModule=@OLEModule,IP=@IP,UPDATEDT=@UPDATEDT where ApplId=@ApplId";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@OLEModule", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UPDATEDT", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
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

    public int updatejobappsignature(string appno, byte[] image, string ip, string UPDATEDT)
    {
        string str = "update JobApplicationPhoto set Signature=@Signature,IP=@IP,UPDATEDT=@UPDATEDT where ApplId=@ApplId";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@Signature", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UPDATEDT", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
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
    public int updateLTI(string appno, byte[] image, string ip, string UPDATEDT)
    {
        string str = "update JobApplicationPhoto set LTI=@LTI,IP=@IP,UPDATEDT=@UPDATEDT where ApplId=@ApplId";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@LTI", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UPDATEDT", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
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
    public int updateRTI(string appno, byte[] image, string ip, string UPDATEDT)
    {
        string str = "update JobApplicationPhoto set RTI=@RTI,IP=@IP,UPDATEDT=@UPDATEDT where ApplId=@ApplId";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@RTI", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UPDATEDT", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
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

    public DataTable Getappno(string applid)
    {
        str = @"select ja.jid,postcode,convert(varchar,adno)+'/'+convert(varchar,adyear) adno ,jobtitle,applid,dummy_no,advmaster.jobsourceid,APPNO,ja.name as name,fname,mothername,convert(varchar,birthdt,103) dob,ja.final from 
                JobApplication ja 
                inner join Job_Advt on ja.jid = Job_Advt.jid inner join advmaster on advmaster.adid=Job_Advt.adid
                where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.NVarChar, 50);
        param[j].Value = applid;

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


    public DataTable get_pic_pay(string jobid, string year, string advtno, string no)
    {
        str = @"select advtno from JobApplicationPayment
                inner join JobApplicationPhoto on JobApplicationPayment.appno=JobApplicationPhoto.ApplId where jobsourceid=@jobid
                and advtno=@advtno and AdvtYear=@AdvtYear and APPNO=@APPNO";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@jobid", SqlDbType.Int, 50);
        param[j].Value = jobid;
        param[j] = new SqlParameter("@advtno", SqlDbType.Int, 4);
        if (advtno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = advtno;
        }
        j++;
        param[j] = new SqlParameter("@AdvtYear", SqlDbType.Int, 4);
        if (year == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = year;
        }
        j++;
        param[j] = new SqlParameter("@APPNO", SqlDbType.Int, 4);
        if (no == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = no;
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
    public int get_max_app_no()
    {
        str = @"select isnull(max(APPNO),0) from jobapplication";

        //str += " order by pno ";
        try
        {
            dt = da.GetDataTable(str);
            return Int32.Parse(dt.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable get_sub_cat(string reqid, string deptcode)//combd 05/12/2023
    {
        //str = "SELECT id,SubCat_code,SubCat_name from subcat_master where show='Y'";
        if (deptcode == "COMBD")
        {
            str = @"SELECT  distinct subcat_master.id,SubCat_code,SubCat_name from subcat_master
                inner join RR_age_relax on RR_age_relax.CatCode=SubCat_Master.SubCat_code
				inner join dept_job_request djr on djr.reqid=RR_age_relax.reqid
				inner join CombinedEntry ce on ce.DeptReqId=djr.reqid where RR_age_relax.CatIndCS='S'and ce.CombdReqid=@reqid";
        }
        //***********************************************************************************
        else
        {
            str = @"SELECT subcat_master.id,SubCat_code,SubCat_name from subcat_master
                inner join RR_age_relax on RR_age_relax.CatCode=SubCat_Master.SubCat_code
                where RR_age_relax.CatIndCS='S' and reqid=@reqid";
        }
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[j].Value = reqid;
        try
        {
            dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //try
        //{
        //    dt = da.GetDataTable(str);
        //    return dt;
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    public DataTable get_sub_ph(string phcat_code, int reqid)
    {
        str = @"SELECT phsm.phsubcatid,PH_CatCode,PH_SubCatCode,PH_SubCatCodeDesc
                FROM PHSubCatMaster phsm inner join SubSubSplCatVacancies vac on phsm.phsubcatid=vac.phsubcatid where PH_CatCode=@phcat_code and reqid=@reqid";

        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@phcat_code", SqlDbType.VarChar, 50);
        param[j].Value = phcat_code;
        j++;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        param[j].Value = reqid;
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
    public int InsertJobApplication_PD(int jid, int applno, string Name, string FatherName, string MotherName, string PresentAdd, string ParmaAdd, string PresentPIN, string PermanenetPIN, string Mobile, string Email, string Nationality, string Gender, string MaritalStatus, string DOB, string Category, string Subcategory, string PHsubCate, string GovtDateJoin, string NonCreamyLayerCerNo, string NonCreamyLayerDate, int CastCertApplyState, string ExService, string exServiceFromDate, string ExServiceToDate, string Debard, string debarredDate, string debarredYear, string IP, string regno, string feereq, string CertUssueAuth, string contract_duration, string ex_serv_duration, string ph_visual, string ph_hearing, string ph_ortho, string physic, string wscribe, string OBCRegion)
    {
        int i = 0;
        int j = 0;

        string str = @"insert into JobApplication (jid,appno,name,fname,mothername,address_per,address,PIN_per,PIN,mobileno,email,nationality,gender,maritalstatus,birthdt,category,SubCategory,ph,GovtJoinDt,CLCNo,CLCDate,CastCerApplyState,exserviceman,ExFromDt,ExToDt,DebardDetails,DebardDt,DebardYr,IP,regno,feereq,CastCertIssueAuth,ContractDuration,ExServiceDuration,PH_Visual,PH_Hearing,PH_Ortho,physical_relax,wscribe,OBCRegion) 
         values(@jid,@applno,@name,@fname,@mothername,@address_per,@address,@PIN_per,@PIN,@mobileno,@email,@nationality,@gender,@maritalstatus,@birthdt,@category,@SubCategory,@ph,@GovtJoinDt,@CLCNo,@CLCDate,@CastCerApplyState,@exserviceman,@ExFromDt,@ExToDt,@DebardDetails,@DebardDt,@DebardYr,@IP,@regno,@feereq,@CertUssueAuth,@ContractDuration,@exServiceDuration,@ph_visual,@ph_hearing,@ph_ortho,@physic,@wscribe,@OBCRegion) Select SCOPE_IDENTITY() ";

        SqlParameter[] param = new SqlParameter[40];

        param[j] = new SqlParameter("@jid", SqlDbType.SmallInt);
        param[j].Value = jid;
        j++;
        param[j] = new SqlParameter("@applno", SqlDbType.Int);
        param[j].Value = applno;
        j++;


        param[j] = new SqlParameter("@name", SqlDbType.NVarChar);
        if (Name == "" || Name == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Name;
        }
        j++;

        param[j] = new SqlParameter("@fname", SqlDbType.NVarChar);
        if (FatherName == "" || FatherName == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = FatherName;
        }
        j++;
        param[j] = new SqlParameter("@mothername", SqlDbType.NVarChar);
        if (MotherName == "" || MotherName == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = MotherName;
        }
        j++;

        param[j] = new SqlParameter("@address_per", SqlDbType.NVarChar);
        if (PresentAdd == "" || PresentAdd == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PresentAdd;
        }
        j++;

        param[j] = new SqlParameter("@address", SqlDbType.NVarChar);
        if (ParmaAdd == "" || ParmaAdd == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = ParmaAdd;
        }
        j++;

        param[j] = new SqlParameter("@PIN_per", SqlDbType.VarChar);
        if (PresentPIN == "" || PresentPIN == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PresentPIN;
        }
        j++;

        param[j] = new SqlParameter("@PIN", SqlDbType.VarChar);
        if (PermanenetPIN == "" || PermanenetPIN == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PermanenetPIN;
        }
        j++;

        param[j] = new SqlParameter("@mobileno", SqlDbType.NVarChar);
        if (Mobile == "" || Mobile == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Mobile;
        }
        j++;
        param[j] = new SqlParameter("@email", SqlDbType.VarChar);
        if (Email == "" || Email == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Email;
        }
        j++;

        param[j] = new SqlParameter("@nationality", SqlDbType.NVarChar);
        if (Nationality == "" || Nationality == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Nationality;
        }
        j++;
        param[j] = new SqlParameter("@gender", SqlDbType.Char);
        if (Gender == "" || Gender == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Gender;
        }
        j++;

        param[j] = new SqlParameter("@maritalstatus", SqlDbType.Char);
        if (MaritalStatus == "" || MaritalStatus == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = MaritalStatus;
        }
        j++;

        param[j] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
        if (DOB == "" || DOB == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(DOB);
        }
        j++;

        param[j] = new SqlParameter("@category", SqlDbType.VarChar);
        if (Category == "" || Category == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Category;
        }
        j++;
        param[j] = new SqlParameter("@SubCategory", SqlDbType.VarChar);
        if (Subcategory == "" || Subcategory == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Subcategory;
        }
        j++;
        param[j] = new SqlParameter("@ph", SqlDbType.VarChar);
        if (PHsubCate == "" || PHsubCate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PHsubCate;
        }
        j++;

        param[j] = new SqlParameter("@GovtJoinDt", SqlDbType.DateTime, 8);
        if (GovtDateJoin == "" || GovtDateJoin == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(GovtDateJoin);
        }
        j++;

        param[j] = new SqlParameter("@CLCNo", SqlDbType.NVarChar);
        if (NonCreamyLayerCerNo == "" || NonCreamyLayerCerNo == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = NonCreamyLayerCerNo;
        }
        j++;

        param[j] = new SqlParameter("@CLCDate", SqlDbType.DateTime);
        if (NonCreamyLayerDate == "" || NonCreamyLayerDate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(NonCreamyLayerDate);
        }
        j++;

        param[j] = new SqlParameter("@CastCerApplyState", SqlDbType.Int);
        if (CastCertApplyState.ToString() == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = CastCertApplyState;
        }
        j++;

        param[j] = new SqlParameter("@exserviceman", SqlDbType.VarChar);
        if (ExService.ToString() == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = ExService;
        }
        j++;
        param[j] = new SqlParameter("@ExFromDt", SqlDbType.DateTime);
        if (exServiceFromDate == "" || exServiceFromDate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(exServiceFromDate);
        }
        j++;
        param[j] = new SqlParameter("@ExToDt", SqlDbType.DateTime);
        if (ExServiceToDate == "" || ExServiceToDate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(ExServiceToDate);
        }
        j++;
        param[j] = new SqlParameter("@DebardDetails", SqlDbType.VarChar);
        if (Debard == "" || Debard == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Debard;
        }
        j++;
        param[j] = new SqlParameter("@DebardDt", SqlDbType.DateTime);
        if (debarredDate == "" || debarredDate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(debarredDate);
        }
        j++;

        param[j] = new SqlParameter("@DebardYr", SqlDbType.Int);
        if (Convert.ToString(debarredYear) == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(debarredYear);
        }
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@regno", SqlDbType.VarChar);
        if (regno == "" || regno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = regno;
        }
        j++;
        param[j] = new SqlParameter("@feereq", SqlDbType.VarChar);
        if (feereq == "" || feereq == null)
        {
            param[j].Value = "Y";
        }
        else
        {
            param[j].Value = feereq;
        }
        j++;
        param[j] = new SqlParameter("@CertUssueAuth", SqlDbType.VarChar);
        if (CertUssueAuth == "" || CertUssueAuth == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = CertUssueAuth;
        }
        j++;
        param[j] = new SqlParameter("@ContractDuration", SqlDbType.VarChar);
        if (contract_duration == "" || contract_duration == null)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(contract_duration);
        }
        j++;
        param[j] = new SqlParameter("@exServiceDuration", SqlDbType.VarChar);
        if (ex_serv_duration == "" || ex_serv_duration == null)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(ex_serv_duration);
        }

        j++;
        param[j] = new SqlParameter("@ph_visual", SqlDbType.VarChar);
        if (ph_visual == "" || ph_visual == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = ph_visual;
        }
        j++;
        param[j] = new SqlParameter("@ph_hearing", SqlDbType.VarChar);
        if (ph_hearing == "" || ph_hearing == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = ph_hearing;
        }
        j++;
        param[j] = new SqlParameter("@ph_ortho", SqlDbType.VarChar);
        if (ph_ortho == "" || ph_ortho == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = ph_ortho;
        }
        j++;
        param[j] = new SqlParameter("@physic", SqlDbType.VarChar);
        if (physic == "" || physic == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = physic;
        }
        j++;
        param[j] = new SqlParameter("@wscribe", SqlDbType.VarChar);
        if (wscribe == "" || wscribe == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = wscribe;
        }
        j++;
        param[j] = new SqlParameter("@OBCRegion", SqlDbType.Char, 1);
        if (OBCRegion == "" || OBCRegion == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = OBCRegion;
        }

        try
        {
            i = Convert.ToInt32(da.ExecScaler(str, param));
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }


    public DataTable GetfillAppicantDetails(int applid)
    {
        str = @"SELECT postcode,convert(varchar,adno)+'/'+convert(varchar,adyear) adno,JobTitle,JobDescription,convert(varchar,DOBFrom,103) dobfrom,convert(varchar,dobto,103) dobto, 
                jap.applid,jap.jid jid,   APPNO, name, fname,  mothername, address,  address_per,CLCNo,CLCDate,CastCertIssueAuth,PIN, PIN_per,nationality, mobileno, birthdt, 
                jap.gender, maritalstatus, category, exserviceman, ExFromDt, ExToDt,   email, GovtJoinDt, IP, DebardDt, DebardYr, DebardDetails, CastCerApplyState ,
                CONVERT(VARCHAR,am.EndsOn,103) endson,ContractDuration,ExServiceDuration,isnull(physical_relax,'') physical_relax,reqid,isnull(wscribe,'') wscribe,OBCRegion
                ,js.SubCat_code,jss.SScatid,jsss.SSScatid,spousename,phcd.PhCertiNo ,phcd.PhCertIssueAuth ,phcd.PhCertIssueDate,phcd.PhIssueState,phcd.PhCertificateFile
                FROM JobApplication jap
                inner join Job_Advt ja on ja.jid=jap.jid
                inner join advmaster am on am.adid=ja.adid
                left outer join JapplicantScat JS on jap.applid=JS.applid
                left outer join JapplicantsScat JSs on JS.JScatID=jss.JScatID
                left outer join JApplicantSSScat JSss on JsS.JSScatID=jsss.JSScatid
				left join PhCertifiacteDetail phcd on phcd.applid=jap.applid WHERE jap.applid = @applid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 10);
        param[j].Value = applid;
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

    public DataTable GetfillAppicantDetails_Exp(int applid)
    {
        str = @"select applid,post,datefrom,dateto,emp_name,emp_addr,emp_contactno,emp_pin,salary
                from dbo.JobExperience
                where applid = @applid ";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];

        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
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

    public int UpdateJobApplicationTEST(int applid, string Name, string FatherName, string MotherName, string PresentAdd, string ParmaAdd, string PresentPIN, string PermanenetPIN, string Mobile, string Email, string Nationality, string Gender, string MaritalStatus, string DOB, string Category, string Subcategory, string PHsubCate, string GovtDateJoin, string NonCreamyLayerCerNo, string NonCreamyLayerDate, int CastCertApplyState, string ExService, string exServiceFromDate, string ExServiceToDate, string Debard, string debarredDate, string debarredYear, string feereq, string CertIssueAuth, string contract_duration, string ex_serv_duration, string ph_visual, string ph_hearing, string ph_ortho, string physic, string wscribe, string OBCRegion)
    {
        int i = 0;
        string str = @"update JobApplication set name=@name,fname=@fname,mothername=@mothername,
                       address_per=@address_per,address=@address,PIN_per=@PIN_per,PIN=@PIN,mobileno=@mobileno,
                       email=@email,nationality=@nationality,gender=@gender,maritalstatus=@maritalstatus,birthdt=@birthdt,category=@category,
                       SubCategory=@SubCategory,ph=@ph,GovtJoinDt=@GovtJoinDt,CLCNo=@CLCNo,CLCDate=@CLCDate,CastCerApplyState=@CastCerApplyState,
                       exserviceman=@exserviceman,ExFromDt=@ExFromDt,ExToDt=@ExToDt,DebardDetails=@DebardDetails,DebardDt=@DebardDt,DebardYr=@DebardYr,feereq=@feereq,
                       CastCertIssueAuth =@CertIssueAuth,ContractDuration=@ContractDuration,ExServiceDuration=@exServiceDuration,PH_Visual=@ph_visual,PH_Hearing=@ph_hearing,
                       PH_Ortho=@ph_ortho,physical_relax=@physic,wscribe=@wscribe,OBCRegion=@OBCRegion
                       where applid=@applid ";

        SqlParameter[] param = new SqlParameter[37];
        int j = 0;

        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;


        param[j] = new SqlParameter("@name", SqlDbType.NVarChar);
        if (Name == "" || Name == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Name;
        }
        j++;

        param[j] = new SqlParameter("@fname", SqlDbType.NVarChar);
        if (FatherName == "" || FatherName == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = FatherName;
        }
        j++;
        param[j] = new SqlParameter("@mothername", SqlDbType.NVarChar);
        if (MotherName == "" || MotherName == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = MotherName;
        }
        j++;
        param[j] = new SqlParameter("@address_per", SqlDbType.NVarChar);
        if (PresentAdd == "" || PresentAdd == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PresentAdd;
        }
        j++;
        param[j] = new SqlParameter("@address", SqlDbType.NVarChar);
        if (ParmaAdd == "" || ParmaAdd == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = ParmaAdd;
        }
        j++;


        param[j] = new SqlParameter("@PIN_per", SqlDbType.VarChar);
        if (PresentPIN == "" || PresentPIN == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PresentPIN;
        }
        j++;
        param[j] = new SqlParameter("@PIN", SqlDbType.VarChar);
        if (PermanenetPIN == "" || PermanenetPIN == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PermanenetPIN;
        }
        j++;
        param[j] = new SqlParameter("@mobileno", SqlDbType.NVarChar);
        if (Mobile == "" || Mobile == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Mobile;
        }
        j++;
        param[j] = new SqlParameter("@email", SqlDbType.VarChar);
        if (Email == "" || Email == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Email;
        }

        j++;
        param[j] = new SqlParameter("@nationality", SqlDbType.NVarChar);
        if (Nationality == "" || Nationality == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Nationality;
        }
        j++;
        param[j] = new SqlParameter("@gender", SqlDbType.Char);
        if (Gender == "" || Gender == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Gender;
        }
        j++;
        param[j] = new SqlParameter("@maritalstatus", SqlDbType.Char);
        if (MaritalStatus == "" || MaritalStatus == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = MaritalStatus;
        }
        j++;
        param[j] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
        if (DOB == "" || DOB == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(DOB);
        }
        j++;
        param[j] = new SqlParameter("@category", SqlDbType.VarChar);
        if (Category == "" || Category == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Category;
        }
        j++;
        param[j] = new SqlParameter("@SubCategory", SqlDbType.VarChar);
        if (Subcategory == "" || Subcategory == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Subcategory;
        }
        j++;
        param[j] = new SqlParameter("@ph", SqlDbType.VarChar);
        if (PHsubCate == "" || PHsubCate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PHsubCate;
        }
        j++;
        param[j] = new SqlParameter("@GovtJoinDt", SqlDbType.DateTime, 8);
        if (GovtDateJoin == "" || GovtDateJoin == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(GovtDateJoin);
        }
        j++;
        param[j] = new SqlParameter("@CLCNo", SqlDbType.NVarChar);
        if (NonCreamyLayerCerNo == "" || NonCreamyLayerCerNo == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = NonCreamyLayerCerNo;
        }
        j++;
        param[j] = new SqlParameter("@CLCDate", SqlDbType.DateTime);
        if (NonCreamyLayerDate == "" || NonCreamyLayerDate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(NonCreamyLayerDate);
        }
        j++;
        param[j] = new SqlParameter("@CastCerApplyState", SqlDbType.Int);
        if (CastCertApplyState.ToString() == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = CastCertApplyState;
        }
        j++;
        param[j] = new SqlParameter("@exserviceman", SqlDbType.VarChar);
        if (ExService.ToString() == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = ExService;
        }
        j++;
        param[j] = new SqlParameter("@ExFromDt", SqlDbType.DateTime);
        if (exServiceFromDate == "" || exServiceFromDate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(exServiceFromDate);
        }
        j++;
        param[j] = new SqlParameter("@ExToDt", SqlDbType.DateTime);
        if (ExServiceToDate == "" || ExServiceToDate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(ExServiceToDate);
        }
        j++;
        param[j] = new SqlParameter("@DebardDetails", SqlDbType.VarChar);
        if (Debard == "" || Debard == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Debard;
        }
        j++;
        param[j] = new SqlParameter("@DebardDt", SqlDbType.DateTime);
        if (debarredDate == "" || debarredDate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(debarredDate);
        }
        j++;

        param[j] = new SqlParameter("@DebardYr", SqlDbType.Int);
        if (Convert.ToString(debarredYear) == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = debarredYear;
        }
        j++;

        param[j] = new SqlParameter("@feereq", SqlDbType.VarChar);
        if (feereq == "" || feereq == null)
        {
            param[j].Value = "Y";
        }
        else
        {
            param[j].Value = feereq;
        }
        j++;

        param[j] = new SqlParameter("@CertIssueAuth", SqlDbType.VarChar);
        if (CertIssueAuth == "" || CertIssueAuth == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = CertIssueAuth;
        }
        j++;

        param[j] = new SqlParameter("@ContractDuration", SqlDbType.VarChar);
        if (contract_duration == "" || contract_duration == null)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(contract_duration);
        }
        j++;
        param[j] = new SqlParameter("@exServiceDuration", SqlDbType.VarChar);
        if (ex_serv_duration == "" || ex_serv_duration == null)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(ex_serv_duration);
        }
        j++;
        param[j] = new SqlParameter("@ph_visual", SqlDbType.VarChar);
        if (ph_visual == "" || ph_visual == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = ph_visual;
        }
        j++;
        param[j] = new SqlParameter("@ph_hearing", SqlDbType.VarChar);
        if (ph_hearing == "" || ph_hearing == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = ph_hearing;
        }
        j++;
        param[j] = new SqlParameter("@ph_ortho", SqlDbType.VarChar);
        if (ph_ortho == "" || ph_ortho == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = ph_ortho;
        }
        j++;
        param[j] = new SqlParameter("@physic", SqlDbType.VarChar);
        if (physic == "" || physic == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = physic;
        }
        j++;
        param[j] = new SqlParameter("@wscribe", SqlDbType.VarChar);
        if (wscribe == "" || wscribe == null)
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = wscribe;
        }
        j++;
        param[j] = new SqlParameter("@OBCRegion", SqlDbType.Char, 1);
        if (OBCRegion == "" || OBCRegion == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = OBCRegion;
        }
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
    public int InsertJobApplication_ED(string applid, int v_qid, float percentage, string board, string state, string year, int standard, string extraquli, string month)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into JobEducation (applid,qid,percentage,board,state,year,standard,Extraquli,month) 
         values(@applid,@qid,@percentage,@board,@state,@year,@stand,@extraquli,@month)";

        SqlParameter[] param = new SqlParameter[9];

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
        param[j] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@percentage", SqlDbType.Decimal);
        if (percentage == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = percentage;
        }
        j++;
        param[j] = new SqlParameter("@board", SqlDbType.VarChar);
        if (board == "" || board == null)
        {
            param[j].Value = "Delhi";
        }
        else
        {
            param[j].Value = board;
        }
        j++;
        param[j] = new SqlParameter("@state", SqlDbType.VarChar);
        if (state == "" || state == null)
        {
            param[j].Value = "7";
        }
        else
        {
            param[j].Value = state;
        }
        j++;
        param[j] = new SqlParameter("@year", SqlDbType.VarChar);
        if (year == "" || year == null)
        {
            param[j].Value = 2013;
        }
        else
        {
            param[j].Value = year;
        }
        j++;

        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@extraquli", SqlDbType.VarChar);
        if (extraquli == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = extraquli;
        }
        j++;
        param[j] = new SqlParameter("@month", SqlDbType.VarChar);
        if (month == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = month;
        }



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
    public int InsertJobApplication_EX(string applid, int v_qid, string iauth, string certno, string doi)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into JobEducation_EX (applid,qid,iauth,certno,doi) 
         values(@applid,@qid,@iauth,@certno,@doi)";

        SqlParameter[] param = new SqlParameter[5];

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
        param[j] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@iauth", SqlDbType.VarChar);
        if (iauth == "")
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = iauth;
        }
        j++;
        param[j] = new SqlParameter("certno", SqlDbType.VarChar);
        if (certno == "")
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = certno;
        }
        j++;
        param[j] = new SqlParameter("@doi", SqlDbType.VarChar);
        if (doi == "")
        {
            param[j].Value = "";
        }
        else
        {
            param[j].Value = doi;
        }

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
    public int updateJobApplication_ED_special(string applid, int v_qid, string DesirableQualification)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into jobdesirable_master (applid,desirable,DesirableQualification) values(@applid,@desirable,@DesirableQualification)";

        SqlParameter[] param = new SqlParameter[3];

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
        param[j] = new SqlParameter("@desirable", SqlDbType.Int);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@DesirableQualification", SqlDbType.NVarChar);
        if (DesirableQualification == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DesirableQualification;
        }

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

    public int updateJobApplication_ExpD_special(string applid, int v_eid, string DesirableExperience)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into jobdesirable_master (applid,desirableExp,DesirableExperience) values(@applid,@desirableExp,@DesirableExperience)";

        SqlParameter[] param = new SqlParameter[3];

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
        param[j] = new SqlParameter("@desirableExp", SqlDbType.Int);
        if (v_eid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_eid;
        }
        j++;
        param[j] = new SqlParameter("@DesirableExperience", SqlDbType.NVarChar);
        if (DesirableExperience == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DesirableExperience;
        }

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

    public int InsertJobApplication_ED_special(string applid, int v_qid, int standard)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into JobEducation (applid,qid,standard) values(@applid,@qid,@stand)";

        SqlParameter[] param = new SqlParameter[3];

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
        param[j] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }

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



    public int InsertJobApplication_apply(int applid, int s_id, string rid)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into JobEducation (applid,standard,percentage,board,year,state,month) 
         (select @applid,@stand,percentage,boardname,passing_year,state,month from registration where rid=@rid)";

        SqlParameter[] param = new SqlParameter[3];

        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = applid;
        }
        j++;
        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (s_id == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = s_id;
        }
        j++;
        param[j] = new SqlParameter("@rid", SqlDbType.VarChar);
        if (rid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = rid;
        }

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


    public int InsertJobApplication_Exp_D(string appid, string post, string datefrom, string dateto, string emp_name, string emp_addr, string emp_contactno)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into JobExperience (applid,post,datefrom,dateto,emp_name,emp_addr,emp_contactno) 
         values(@appid,@post,@datefrom,@dateto,@emp_name,@emp_addr,@emp_contactno)";

        SqlParameter[] param = new SqlParameter[7];

        param[j] = new SqlParameter("@appid", SqlDbType.Int);
        if (appid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Convert.ToInt32(appid);
        }
        j++;
        param[j] = new SqlParameter("@post", SqlDbType.VarChar);
        if (post == "" || post == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = post;
        }
        j++;
        param[j] = new SqlParameter("@datefrom", SqlDbType.DateTime);
        if (datefrom == "" || datefrom == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(datefrom);
        }
        j++;
        param[j] = new SqlParameter("@dateto", SqlDbType.DateTime);
        if (dateto == "" || dateto == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(dateto);
        }
        j++;
        param[j] = new SqlParameter("@emp_name", SqlDbType.VarChar);
        if (emp_name == "" || emp_name == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_name;
        }
        j++;
        param[j] = new SqlParameter("@emp_addr", SqlDbType.VarChar);
        if (emp_addr == "" || emp_addr == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_addr;
        }
        j++;
        param[j] = new SqlParameter("@emp_contactno", SqlDbType.VarChar);
        if (emp_contactno == "" || emp_contactno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_contactno;
        }

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

    public int update_JobApplication_Exp_D(int id, string post, string datefrom, string dateto, string emp_name, string emp_addr, string emp_contactno)
    {
        int i = 0;
        int j = 0;
        string str = @"Update JobExperience set post=@post,datefrom=@datefrom,dateto=@dateto,emp_name=@emp_name,emp_addr=@emp_addr,emp_contactno=@emp_contactno where id=@id";
        SqlParameter[] param = new SqlParameter[7];

        param[j] = new SqlParameter("@id", SqlDbType.Int);
        if (id == 0)
        {
            param[j].Value = 1;
        }
        else
        {
            param[j].Value = id;
        }
        j++;
        param[j] = new SqlParameter("@post", SqlDbType.VarChar);
        if (post == "" || post == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = post;
        }
        j++;
        param[j] = new SqlParameter("@datefrom", SqlDbType.DateTime);
        if (datefrom == "" || datefrom == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(datefrom);
        }
        j++;
        param[j] = new SqlParameter("@dateto", SqlDbType.DateTime);
        if (dateto == "" || dateto == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(dateto);
        }
        j++;
        param[j] = new SqlParameter("@emp_name", SqlDbType.VarChar);
        if (emp_name == "" || emp_name == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_name;
        }
        j++;
        param[j] = new SqlParameter("@emp_addr", SqlDbType.VarChar);
        if (emp_addr == "" || emp_addr == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_addr;
        }
        j++;
        param[j] = new SqlParameter("@emp_contactno", SqlDbType.VarChar);
        if (emp_contactno == "" || emp_contactno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_contactno;
        }
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

    public int delete_JobApplication_Exp_D(int id)
    {
        string str = "delete from JobExperience where id=@id";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@id", SqlDbType.Int);

        param[j].Value = id;
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

    public DataTable GetJobApplication_Exp(string applid)
    {
        string str = @"select id,applid,post,convert(varchar,datefrom,103) datefrom ,convert(varchar,dateto,103) dateto,emp_name,emp_contactno,emp_addr
                       from JobExperience
                       where applid =@applid";

        string str1 = @"select id,applid,post,convert(varchar,datefrom,103) datefrom ,convert(varchar,dateto,103) dateto,emp_name,emp_contactno,emp_addr
                       from testdb_dsssbonline_recdapp.dbo.JobExperience
                       where applid =@applid";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);


        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 0)
            {
                dt = da.GetDataTableQry(str1, param);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public DataTable fill_standard(string reqid, string qtype, string groupno)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        if (groupno != "")
        {
            condn += " and groupno=@groupno ";
        }
        str = "select ID,standard from standardMaster where type='G' and ID in (select stid from tbledu where id in (select id from tbledu_TRN where reqid=@reqid " + condn + @" ))";
        // str = "select ID,standard from standardMaster where ID in (select stid from tbledu where id in (select id from tbledu_TRN where reqid=@reqid and qtype=@qtype))";


        int j = 0;
        SqlParameter[] param = new SqlParameter[3];
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        param[j].Value = Int32.Parse(reqid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno != "")
        {
            param[j].Value = Int32.Parse(groupno);
        }
        else
        {
            param[j].Value = System.DBNull.Value;
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
    public DataTable fill_standard_check(string reqid, string qtype, List<int> groupno)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        //if (groupno.ToString() != "")
        if (groupno.Count > 0)
        {
            //condn += " and groupno=@groupno ";
            condn += " and  groupno in(" + (groupno.Count > 0 ? string.Join(",", groupno.ToArray()) : System.DBNull.Value.ToString()) + ") ";
        }
        else
        {


            //condn += " and groupno=@groupno
        }

        str = "select ID,standard from standardMaster where type='G' and ID in (select stid from tbledu where id in (select id from tbledu_TRN where reqid=@reqid " + condn + @" ))";
        // str = "select ID,standard from standardMaster where ID in (select stid from tbledu where id in (select id from tbledu_TRN where reqid=@reqid and qtype=@qtype))";


        int j = 0;
        SqlParameter[] param = new SqlParameter[2];
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        param[j].Value = Int32.Parse(reqid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        //param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        //if (groupno != "")
        //{
        //    param[j].Value = Int32.Parse(groupno);
        //}
        //else
        //{
        //    param[j].Value = System.DBNull.Value;
        //}
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
    public DataTable GetJobApplication_Education(string applid, string qtype, string stan_type)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (tr.qtype='E' or tr.qtype='G') ";
        }
        else
        {
            condn = " and tr.qtype=@qtype ";
        }
        string str = @"select je.id,je.applid,sm.standard as stnd,je.standard,qid,te.name,Extraquli,percentage,board,ms.code as Stateid,ms.State,month,YEAR,(month + '/' + YEAR) as myear,tr.groupno
                    from JobEducation je 
                    inner join standardMaster sm on je.standard=sm.id 
                    left outer join  tbledu_TRN tr on tr.uid = je.qid 
                    left outer join tbledu te on te.id=tr.id
                    left outer join m_state ms on ms.code=je.state   
                    inner join JobApplication ja on ja.applid =je.applid 
                    inner join Job_Advt jadvt on jadvt.jid=ja.jid
                    inner join AdvMaster adm on adm.adid=jadvt.adid
                    where  je.applid=@applid ";

        string str1 = @"select je.id,je.applid,sm.standard as stnd,je.standard,qid,te.name,Extraquli,percentage,board,ms.code as Stateid,ms.State,month,YEAR,(month + '/' + YEAR) as myear,tr.groupno
                    from testdb_dsssbonline_recdapp.dbo.JobEducation je 
                    inner join standardMaster sm on je.standard=sm.id 
                    left outer join  tbledu_TRN tr on tr.uid = je.qid 
                    left outer join tbledu te on te.id=tr.id
                    left outer join m_state ms on ms.code=je.state   
                    inner join testdb_dsssbonline_recdapp.dbo.JobApplication ja on ja.applid =je.applid 
                    inner join Job_Advt jadvt on jadvt.jid=ja.jid
                    inner join AdvMaster adm on adm.adid=jadvt.adid
                    where  je.applid=@applid  ";
        if (qtype != "")
        {
            str += condn;
        }
        if (stan_type != "")
        {
            str += " and sm.type=@stype";
        }
        str += " order by standard";
        int j = 0;
        SqlParameter[] param = new SqlParameter[3];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        param[j] = new SqlParameter("@stype", SqlDbType.VarChar);
        param[j].Value = stan_type;

        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 0)
            {
                dt = da.GetDataTableQry(str1, param);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public DataTable Getdesirable(string applid)
    {

        string str = @"select distinct DesirableQualification, null as DepartmentName, case
						when desirable=1 then'yes' 
					
						when desirable=2 Then 'No'
						end as desirable
						from jobdesirable_master
                    where  applid=@applid and DesirableQualification is not null";

        // str += " order by date";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);


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

    public DataTable Getdesirable1(string applid)
    {

        string str = @"select distinct desirableExperience , null as DepartmentName , case
						when desirableExp=1 then'yes' 				
						when desirableExp=2 Then 'No'
						end as desirableExp
						from jobdesirable_master 
                    where applid=@applid and desirableExperience is not null";

        // str += " order by date";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);


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

    public DataTable GetJobApplication_Education(string applid, string qtype, string stan_type, string edflag)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (tr.qtype='E' or tr.qtype='G') ";
        }
        else
        {
            condn = " and tr.qtype=@qtype ";
        }
        string str = @"select je.id,je.applid,sm.standard as stnd,je.standard,qid,te.name,Extraquli,percentage,board,ms.code as Stateid,ms.State,month,YEAR,(month + '/' + YEAR) as myear
                    from dsssbonline_recdapp.dbo.JobEducation je 
                    inner join standardMaster sm on je.standard=sm.id 
                    left outer join  tbledu_TRN tr on tr.uid = je.qid 
                    left outer join tbledu te on te.id=tr.id
                    left outer join m_state ms on ms.code=je.state   
                    inner join dsssbonline_recdapp.dbo.JobApplication ja on ja.applid =je.applid 
                    inner join Job_Advt jadvt on jadvt.jid=ja.jid
                    inner join AdvMaster adm on adm.adid=jadvt.adid
                    where  je.applid=@applid  ";
        if (qtype != "")
        {
            str += condn;
        }
        if (stan_type != "")
        {
            str += " and sm.type=@stype";
        }
        if (edflag == "Y")
        {
            str += " and edossierflg='Y' ";
        }
        str += " order by standard";
        int j = 0;
        SqlParameter[] param = new SqlParameter[3];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        param[j] = new SqlParameter("@stype", SqlDbType.VarChar);
        param[j].Value = stan_type;

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
    public DataTable GetJobApplication_Education_EX(string applid, string qtype, string stan_type)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (tr.qtype='E' or tr.qtype='G') ";
        }
        else
        {
            condn = " and tr.qtype=@qtype ";
        }
        string str = @"select je.id,je.applid,sm.standard as stnd,je.standard,je.qid,te.name,Extraquli,percentage,board,ms.code as Stateid,ms.State,month,YEAR,(month + '/' + YEAR) as myear,ex.iauth,ex.certno,ex.doi
                    from JobEducation je 
                    inner join standardMaster sm on je.standard=sm.id 
                    left outer join  tbledu_TRN tr on tr.uid = je.qid 
                    left outer join tbledu te on te.id=tr.id
                    left outer join m_state ms on ms.code=je.state   
                    inner join JobApplication ja on ja.applid =je.applid 
                    inner join Job_Advt jadvt on jadvt.jid=ja.jid
                    inner join AdvMaster adm on adm.adid=jadvt.adid
                    inner join jobeducation_ex ex on je.applid=ex.applid and je.qid=ex.qid
                    where  je.applid=@applid  ";
        if (qtype != "")
        {
            str += condn;
        }
        if (stan_type != "")
        {
            str += " and sm.type=@stype";
        }
        str += " order by standard";
        int j = 0;
        SqlParameter[] param = new SqlParameter[3];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        param[j] = new SqlParameter("@stype", SqlDbType.VarChar);
        param[j].Value = stan_type;

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
    public int UpdateJobApplication_Education(int id, int v_qid, float percentage, string board, string state, string year, int standard, string extraquli, string month)
    {
        string str = @"update JobEducation set qid=@qid,percentage=@percentage,
                    board=@board,state=@state,year=@year,standard=@stand,Extraquli=@extraquli,month=@month where id=@id ";

        SqlParameter[] param = new SqlParameter[9];
        int j = 0;
        param[j] = new SqlParameter("@id", SqlDbType.SmallInt);
        if (id == 0)
        {
            param[j].Value = 1;
        }
        else
        {
            param[j].Value = id;
        }
        j++;
        param[j] = new SqlParameter("@qid", SqlDbType.Int);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@percentage", SqlDbType.Int);
        if (percentage == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = percentage;
        }
        j++;
        param[j] = new SqlParameter("@board", SqlDbType.VarChar);
        if (board == "" || board == null)
        {
            param[j].Value = "Delhi";
        }
        else
        {
            param[j].Value = board;
        }
        j++;
        param[j] = new SqlParameter("@state", SqlDbType.VarChar);
        if (state == "" || state == null)
        {
            param[j].Value = "Delhi";
        }
        else
        {
            param[j].Value = state;
        }
        j++;
        param[j] = new SqlParameter("@year", SqlDbType.VarChar);
        if (year == "" || year == null)
        {
            param[j].Value = 2013;
        }
        else
        {
            param[j].Value = year;
        }
        j++;
        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@extraquli", SqlDbType.VarChar);
        if (extraquli == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = extraquli;
        }
        j++;
        param[j] = new SqlParameter("@month", SqlDbType.VarChar);
        if (month == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = month;
        }
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

    public int delete_Education(int id)
    {
        string str = "delete from JobEducation where id=@id";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@id", SqlDbType.Int);
        param[j].Value = id;
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

    //for percentage
    
    public DataTable Get_Education_Percentage(int reqid, string groupno)
    {
        string str = @"select  distinct tt.id,tt.MinPercent,tt.reqid from tbledu_TRN tt inner join tbledu te on tt.id=te.id where tt.reqid =@reqid  and  groupno in(@groupno)";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        param[j].Value = reqid;
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        param[j].Value = groupno;

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

    public DataTable age_relax(int reqid)
    {

        str = @"SELECT     isnull(ar.catcode,'0') catcode, isnull(ar.CatIndCS,'0') catindcs, CM, D_Year,isnull(fe.fee_exmp,'N') Fee_exmp
                FROM    RR_age_relax ar
                full outer join fee_exemption fe on fe.catcode=ar.catcode where (ar.reqid=@reqid)";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        if (reqid == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;

    }
    public DataTable age_relax(int reqid, int applid)
    {

        str = @"SELECT CM, max(isnull(D_Year,0)) as D_Year 
                FROM    RR_age_relax ar
                full outer join fee_exemption fe on fe.catcode=ar.catcode
				inner join JapplicantScat js on ar.CatCode=js.SubCat_code and ar.CatIndCS='S' where (ar.reqid=@reqid) and js.applid=@applid
				group by CM ";
        int j = 0;
        SqlParameter[] param = new SqlParameter[2];
        param[j] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        if (reqid == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        if (applid == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = applid;
        }
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;

    }
    public DataTable fillgrid(string applid)
    {
        str = @"SELECT post, convert(varchar,datefrom,103)as datefrom, convert(varchar,dateto,103)as dateto , emp_name, emp_addr + '   ' + emp_contactno AS [add], emp_sector, exptype, salary
                FROM  JobExperience where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;

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
    public DataTable Search_JobApplication_PD(string jid, string regno)
    {
        int i = 0;
        int j = 0;

        string str = @"select applid from JobApplication where jid=@jid 
                        and regno=@regno ";

        SqlParameter[] param = new SqlParameter[2];

        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        if (jid == "" || jid == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Int32.Parse(jid);
        }
        j++;

        param[j] = new SqlParameter("@regno", SqlDbType.VarChar);
        if (regno == "" || regno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = regno;
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

    public DataTable getcarddetails(string applid)
    {
        str = @"select Name,dummy_no,ja.examid,postcode,jap.applid, jap.[address]+' '+jap.PIN as address,fname,jap.category,isnull(jap.SubCategory,'--NA--') subcategory,replace(ja.JobTitle,'[dot]','.') JobTitle,cm.centername,
                convert(varchar,em.dateofexam,103) 
                as examdt,em.timeofexam,em.reportingtime,OLEModule,Signature 
                from JobApplication jap 
                inner join Job_Advt ja on ja.jid=jap.jid
                inner join JobApplicationPhoto on jap.applid=JobApplicationPhoto.ApplId
                left outer join centremaster cm on jap.centrecode=cm.centrecode
                inner join examMast em on ja.examid=em.examid 
                where ja.examid in(select examid from JobApplication 
                inner join job_advt on JobApplication.jid=Job_Advt.jid 
                where applid=@applid)
                and RegNo in(select RegNo from JobApplication where applid=@applid)  and dummy_no is not null";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = Int32.Parse(applid);


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
    public DataTable check_fee_excp(string jid, string cat)
    {
        str = @"select jobid from RR_age_relax
                where jid=@jid and fee_exmp='Y' and catcode in (" + cat + ")";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;

        param[j] = new SqlParameter("@advtno", SqlDbType.Int, 4);
        if (jid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(jid);
        }
        j++;
        param[j] = new SqlParameter("@cat", SqlDbType.VarChar);
        if (cat == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = cat;
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
    public DataTable check_fee_excemption(string cat)
    {
        str = @"select * from fee_exemption
                where fee_exmp='Y' and catcode in (" + cat + ")";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;


        param[j] = new SqlParameter("@cat", SqlDbType.VarChar);
        if (cat == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = cat;
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
    public DataTable get_post(string regno)
    {

        str = @"select Job_Advt.[jid] jid,Job_Advt.[reqid] reqid, applid as applid
                    ,replace([JobTitle]+' Post Code :('+postcode+')','[dot]','.') as post
                    from Job_Advt 
                    inner join JobApplication on JobApplication.jid=Job_Advt.jid
                    where 
                    (
	                    adid in 
	                    (select adid from advmaster

	                    where CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
	                    and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
	                    cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)
	                    )
                    )
                    and RegNo=@regno  
                    and 
                   -- dummy_no is null 
                     final is null
                    union

                    select reopenpost.[jid] jid,Job_Advt.[reqid] reqid, applid as applid
                    ,replace([JobTitle]+' Post Code :('+postcode+')','[dot]','.') as post
                    from reopenpost 
                    inner join Job_Advt on Job_Advt.jid=reopenpost.jid
                    inner join JobApplication on JobApplication.jid=reopenpost.jid
                    where
	                     newadid in
	                    (
	                    select reopenadvt.newadid from reopenadvt
	                    inner join advMaster on advMaster.adid=reopenadvt.adid and (reopenadvt.reopenstatus='C'";

        if (System.Web.HttpContext.Current.Session["intraflag"] != null)
        {
            str = str + " or reopenadvt.reopenstatus='D'";
        }
        str = str + @" )and  reopenadvt.ReleaseStatus='Y'
	                    and AdvMaster.Reopened='Y' 
                        inner join reopenpost on reopenpost.newadid=reopenadvt.newadid
                        inner join Job_Advt on Job_Advt.jid=reopenpost.jid
	                    where CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
	                    and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >=
	                    CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 	
	                    )
                    and RegNo=@regno  
                    and 
                   -- dummy_no is null
                      final is null";



        SqlParameter[] param = new SqlParameter[1];
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
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;

    }
    public DataTable get_post_admit(string regno)
    {
        //        str = @"select Job_Advt.[jid] jid,Job_Advt.[reqid] reqid, applid as applid
        //                ,replace([JobTitle]+' Post Code :('+postcode+')','[dot]','.') as post
        //                from Job_Advt 
        //                inner join JobApplication on JobApplication.jid=Job_Advt.jid
        //                inner join examMast on examMast.examid=Job_Advt.examid
        //                where
        //                RegNo=@regno
        //                and dateofexam>=GETDATE()
        //                and radmitcard='Y'
        //                and JobApplication.acstatid in ('1','2')";
        str = @"select Job_Advt.[jid] jid,Job_Advt.[reqid] reqid, applid as applid
                ,replace([JobTitle]+' Post Code :('+postcode+')','[dot]','.') as post
                from Job_Advt 
                inner join JobApplication on JobApplication.jid=Job_Advt.jid
                inner join examMast on examMast.examid=Job_Advt.examid
                where
                RegNo=@regno
                --and dateofexam>=GETDATE()
                and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111)
                and radmitcard='Y' and (exampostponed <>'Y' or  exampostponed is null)
                and JobApplication.acstatid in ('1','2')";

        SqlParameter[] param = new SqlParameter[1];
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
        DataTable dt = da.GetDataTableQry(str, param);
        return dt;

    }



    public DataTable Get_fill_quali_exp(string applid)
    {

        string str = @"select ja.jid,Job_Advt.reqid,JobTitle,postcode,essential_qual,desire_qual,essential_exp,exp_noofyears,desire_exp,CONVERT(varchar,EndsOn,103) EndsOn 
                        from JobApplication ja inner join Job_Advt on ja.jid = Job_Advt.jid
                        inner join AdvMaster on job_Advt.adid = AdvMaster.adid
                        where applid=@applid";

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

    //    public DataTable fill_application_data(int applid)
    //    {

    //        string str = @"select ja.jid,ja.applid,postcode,JobTitle,(Job_Advt.JobTitle + '(Post Code:' + Job_Advt.postcode + ')') as post,ja.name as cnd_name,fname,  mothername, address, distid, address_per,
    //                    distid_per, PIN, PIN_per,ja.state cnd_state, nationality, mobileno,email,birthdt, ja.gender as can_gender, maritalstatus, category, ph, 
    //                    exserviceman,je.id as edu_id,sm.standard as stnd,je.standard,qid,te.name as quli_name,percentage,educlass,board,ms.State as edu_state,year,
    //                    jexp.id as exp_id,jexp.applid as jexp_applid,post,convert(varchar,datefrom,103) datefrom ,convert(varchar,dateto,103) dateto,
    //                    emp_name,emp_addr,emp_contactno,emp_pin,salary
    //                    from  JobApplication ja 
    //                    inner join Job_Advt on Job_Advt.jid=ja.jid
    //                    inner join advmaster on advmaster.adid=Job_Advt.adid
    //                    inner join JobEducation je on  je.applid=ja.applid
    //                    inner join standardMaster sm on je.standard=sm.id
    //                    inner join tbledu_TRN tr on tr.uid = je.qid
    //                    inner join tbledu te on te.id=tr.id
    //                    inner join m_state ms on ms.code=je.state
    //                    inner join JobExperience jexp on jexp.applid = ja.applid
    //                    inner join JobApplicationPhoto jp on jp.ApplId=ja.applid
    //                    where ja.applid=@applid";

    //        SqlParameter[] param = new SqlParameter[1];
    //        param[0] = new SqlParameter("@applid", SqlDbType.Int);
    //        param[0].Value = applid;

    //        try
    //        {
    //            dt = da.GetDataTableQry(str, param);
    //            return dt;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }

    public DataTable fill_personal_data(int applid)
    {

        //        string str = @"select convert(varchar,EndsOn,103) endson,regno,job_source.feeamount fee,ja.jid,ja.applid,postcode,
        //replace(JobTitle,'[dot]','.'),(replace(Job_Advt.JobTitle,'[dot]','.') + '(Post Code:' + Job_Advt.postcode + ')') as post,dummy_no,
        //ja.name+' (Reg No.'+RegNo+')' name, ja.fname,  ja.mothername, address, address_per,
        //PIN, PIN_per,  
        //ja.nationality, reg.mobileno,ja.email,replace(convert(varchar,ja.birthdt,103),'/','') birthdt, ja.gender as can_gender, maritalstatus, 
        //category,dbo.getsubcategory(@applid) as SubCategory,
        //exserviceman,OLEModule,signature,exp_noofyears,convert(varchar,FeeLastDate,103) as FeeLastDate,ExServiceDuration,ContractDuration,
        //Job_Advt.reqid,CONVERT(varchar,DOBFrom,103) DOBFrom,
        //CONVERT(varchar,DOBTO,103) DOBTO,CONVERT(varchar,ja.birthdt,103) DOB,LTI,RTI,ja.final,OBCRegion,ja.spousename,CLCNo,convert(varchar,CLCDate,103) as CLCDate,CastCertIssueAuth
        //from  JobApplication ja 
        //inner join Job_Advt on Job_Advt.jid=ja.jid
        //inner join advmaster on advmaster.adid=Job_Advt.adid inner join job_source on advmaster.jobsourceid =Job_Source .Id 
        //left outer join JobApplicationPhoto jap on jap.applid=ja.applid
        //left outer join registration reg on reg.rid=ja.RegNo
        //where ja.applid=@applid";

        string str = @"select convert(varchar,EndsOn,103) endson,regno,job_source.feeamount fee,ja.jid,ja.applid,postcode,
                        replace(JobTitle,'[dot]','.'),(replace(Job_Advt.JobTitle,'[dot]','.') + '(Post Code:' + Job_Advt.postcode + ')') as post,dummy_no,
                        ja.name+' (Reg No.'+RegNo+')' name, fname,  mothername, address, address_per,
                        PIN, PIN_per,  
                        nationality, mobileno,ja.email,replace(convert(varchar,birthdt,103),'/','') birthdt, ja.gender as can_gender, maritalstatus, 
                        category,dbo.getsubcategory(@applid) as SubCategory,
                        exserviceman,OLEModule,signature,exp_noofyears,convert(varchar,FeeLastDate,103) as FeeLastDate,ExServiceDuration,ContractDuration,
                        Job_Advt.reqid,CONVERT(varchar,DOBFrom,103) DOBFrom,
                        CONVERT(varchar,DOBTO,103) DOBTO,CONVERT(varchar,birthdt,103) DOB,LTI,RTI,ja.final,OBCRegion,spousename,CLCNo,convert(varchar,CLCDate,103) as CLCDate,CastCertIssueAuth,OBCRegion
                        from  JobApplication ja 
                        inner join Job_Advt on Job_Advt.jid=ja.jid
                        inner join advmaster on advmaster.adid=Job_Advt.adid inner join job_source on advmaster.jobsourceid =Job_Source .Id 
                        left outer join JobApplicationPhoto jap on jap.applid=ja.applid
                        where ja.applid=@applid";

        string str1 = @"select convert(varchar,EndsOn,103) endson,regno,job_source.feeamount fee,ja.jid,ja.applid,postcode,
                        replace(JobTitle,'[dot]','.'),(replace(Job_Advt.JobTitle,'[dot]','.') + '(Post Code:' + Job_Advt.postcode + ')') as post,dummy_no,
                        ja.name+' (Reg No.'+RegNo+')' name, fname,  mothername, address, address_per,
                        PIN, PIN_per,  
                        nationality, mobileno,ja.email,replace(convert(varchar,birthdt,103),'/','') birthdt, ja.gender as can_gender, maritalstatus, 
                        category,dbo.getsubcategory(2267677) as SubCategory,
                        exserviceman,OLEModule,signature,exp_noofyears,convert(varchar,FeeLastDate,103) as FeeLastDate,ExServiceDuration,ContractDuration,
                        Job_Advt.reqid,CONVERT(varchar,DOBFrom,103) DOBFrom,
                        CONVERT(varchar,DOBTO,103) DOBTO,CONVERT(varchar,birthdt,103) DOB,LTI,RTI,ja.final,OBCRegion,spousename,CLCNo,convert(varchar,CLCDate,103) as CLCDate,CastCertIssueAuth,OBCRegion
                        from  testdb_dsssbonline_recdapp.dbo.JobApplication ja 
                        inner join Job_Advt on Job_Advt.jid=ja.jid
                        inner join advmaster on advmaster.adid=Job_Advt.adid
						inner join job_source on advmaster.jobsourceid =Job_Source .Id 
                        left outer join testdb_dsssbonline_recdapp.dbo.JobApplicationPhoto jap on jap.applid=ja.applid
                        where ja.applid=@applid";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = applid;

        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 0)
            {
                dt = da.GetDataTableQry(str1, param);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable checkapplidexists (int applid)
    {
        string str = @"select applid from jobapplication where applid=@applid";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
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

    public DataTable CheckValidAdmitCard(int applid)
    {
        str = @"SELECT job_advt.examid,isnull(radmitcard,'N') radmitcard,jrnlno,trandate,feereq,feerecd,isnull(examPostponed,'') postponed
               
                FROM JobApplication
                inner join Job_Advt on Job_Advt.jid=JobApplication.jid
                inner join advmaster on advmaster.adid=Job_Advt.adid
                left outer join examMast on examMast.examid=Job_Advt.examid 
                left outer join feedetails on JobApplication.applid=feedetails.applid WHERE JobApplication.applid = @applid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;



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
    public DataTable CheckFee(int applid)
    {
        //        str = @"select jrnlno,trandate,feereq,feerecd from JobApplication
        //                left outer join feedetails on JobApplication.applid=feedetails.applid WHERE JobApplication.applid = @applid";
        str = @"select top(1) isNULl(JobApplication.applid,'0') japid,isnull(JobEducation.applid,'0') jeid,isnull(JobExperience.applid,'0') jexid,
                isnull(datalength(OLEModule),0) pic,isnull(datalength(Signature),0) sign,
                dummy_no as dummyno,isnull(FeeDetails.jrnlno,'0') feedetails,
                ISNULL(Job_Advt.examid,'0') examid,CONVERT(varchar,examMast.dateofexam,103) examdate,
                isnull(radmitcard,'N') radmitcard,isnull(feereq,'N') feereq,isnull(feerecd,'N') feerecd,JobApplication.acstatid,acconsent,final,readyforexam
                from JobApplication
                left join JobEducation on JobApplication.applid=JobEducation.applid
                left  join JobExperience on JobApplication.applid=JobExperience.applid
                left join JobApplicationPhoto on JobApplication.applid=JobApplicationPhoto.ApplId
                left join  FeeDetails on JobApplication.applid=FeeDetails.applid
                left join Job_Advt on JobApplication.jid=Job_Advt.jid
                left join examMast on Job_Advt.examid=examMast.examid
                where JobApplication.applid=@applid";

        string str1 = @" select top(1) isNULl(testdb_dsssbonline_recdapp.dbo.JobApplication.applid,'0') japid,isnull(testdb_dsssbonline_recdapp.dbo.JobEducation.applid,'0') jeid,isnull						(testdb_dsssbonline_recdapp.dbo.JobExperience.applid,'0') jexid,
                isnull(datalength(OLEModule),0) pic,isnull(datalength(Signature),0) sign,
                dummy_no as dummyno,isnull(testdb_dsssbonline_recdapp.dbo.FeeDetails.jrnlno,'0') feedetails,
                ISNULL(Job_Advt.examid,'0') examid,CONVERT(varchar,examMast.dateofexam,103) examdate,
                isnull(radmitcard,'N') radmitcard,isnull(feereq,'N') feereq,isnull(feerecd,'N') feerecd,testdb_dsssbonline_recdapp.dbo.JobApplication.acstatid,acconsent,final,readyforexam
                from testdb_dsssbonline_recdapp.dbo.JobApplication
                left join testdb_dsssbonline_recdapp.dbo.JobEducation on testdb_dsssbonline_recdapp.dbo.JobApplication.applid=JobEducation.applid
                left  join testdb_dsssbonline_recdapp.dbo.JobExperience on testdb_dsssbonline_recdapp.dbo.JobApplication.applid=JobExperience.applid
                left join testdb_dsssbonline_recdapp.dbo.JobApplicationPhoto on testdb_dsssbonline_recdapp.dbo.JobApplication.applid=JobApplicationPhoto.ApplId
                left join  testdb_dsssbonline_recdapp.dbo.FeeDetails on testdb_dsssbonline_recdapp.dbo.JobApplication.applid=FeeDetails.applid
                left join Job_Advt on testdb_dsssbonline_recdapp.dbo.JobApplication.jid=Job_Advt.jid
                left join examMast on Job_Advt.examid=examMast.examid
                where testdb_dsssbonline_recdapp.dbo.JobApplication.applid=@applid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;



        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 0)
            {
                dt = da.GetDataTableQry(str1, param);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataTable check_debar_PH(string ph, string jid)
    {
        if (ph.Contains(","))
        {
            ph = ph.Replace(",", "','");
        }
        str = @" SELECT [PH_Code],PH_SubCatCode FROM SubSubCatWiseVacancy 
                inner join SubCat_Master_PH on SubSubCatWiseVacancy.PH_SubCatCode=SubCat_Master_PH.PH_Cat_Desc
                inner join Job_Advt on Job_Advt.reqid=SubSubCatWiseVacancy.reqid
                where ph_code in ('" + ph + "') and jid=@jid";



        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@ph", SqlDbType.VarChar);
        param[j].Value = ph;
        j++;
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        param[j].Value = Int32.Parse(jid);


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
    public DataTable check_debar_PH_combd(string ph, string jid)//22/09/2023 => 01/12/2023==>combd
    {
        if (ph.Contains(","))
        {
            ph = ph.Replace(",", "','");
        }
        //        str = @" SELECT [PH_Code],PH_SubCatCode FROM SubSubCatWiseVacancy 
        //                inner join SubCat_Master_PH on SubSubCatWiseVacancy.PH_SubCatCode=SubCat_Master_PH.PH_Cat_Desc
        //                inner join Job_Advt on Job_Advt.reqid=SubSubCatWiseVacancy.reqid
        //                where ph_code in ('" + ph + "') and Job_Advt.reqid in(select DeptReqId from CombinedEntry where CombdReqid in(select reqid from job_Advt where jid=@jid))";


        str = @" SELECT distinct [PH_Code],PH_SubCatCode FROM SubSubCatWiseVacancy 
                inner join SubCat_Master_PH on SubSubCatWiseVacancy.PH_SubCatCode=SubCat_Master_PH.PH_Cat_Desc
                where ph_code in ('" + ph + "') and SubSubCatWiseVacancy.reqid in(select DeptReqId from CombinedEntry where CombdReqid in(select reqid from job_Advt where jid=@jid))";

        string str1 = @" SELECT distinct [PH_Code],PH_SubCatCode FROM SubSubCatWiseVacancy 
                inner join SubCat_Master_PH on SubSubCatWiseVacancy.PH_SubCatCode=SubCat_Master_PH.PH_Cat_Desc
                where ph_code in ('" + ph + "') and SubSubCatWiseVacancy.reqid in(select reqid from job_Advt where jid=@jid)";

        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@ph", SqlDbType.VarChar);
        param[j].Value = ph;
        j++;
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        param[j].Value = Int32.Parse(jid);

        SqlParameter[] param1 = new SqlParameter[2];
        int k = 0;
        param1[k] = new SqlParameter("@ph", SqlDbType.VarChar);
        param1[k].Value = ph;
        k++;
        param1[k] = new SqlParameter("@jid", SqlDbType.Int);
        param1[k].Value = Int32.Parse(jid);


        try
        {
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
    public DataTable check_debar_sub_PH(string sub_ph, string jid)
    {
        if (sub_ph.Contains(","))
        {
            sub_ph = sub_ph.Replace(",", "','");
        }
        str = @" SELECT sssplid FROM SubSubSplCatVacancies 
                inner join Job_Advt on Job_Advt.reqid=SubSubSplCatVacancies.reqid
                where phsubcatid = @sub_ph and jid=@jid";



        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@sub_ph", SqlDbType.VarChar);
        param[j].Value = sub_ph;
        j++;
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        param[j].Value = Int32.Parse(jid);


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

    public DataTable check_debar_sub_PH_combd(string sub_ph, string jid) //=> 01/12/2023==combd
    {
        if (sub_ph.Contains(","))
        {
            sub_ph = sub_ph.Replace(",", "','");
        }
        //        str = @" SELECT sssplid FROM SubSubSplCatVacancies 
        //                inner join Job_Advt on Job_Advt.reqid=SubSubSplCatVacancies.reqid
        //                where phsubcatid = @sub_ph and jid=@jid";

        str = @"select sssplid from SubSubSplCatVacancies where phsubcatid = @sub_ph and reqid in (select deptreqid from CombinedEntry where CombdReqid =(select reqid from Job_Advt where jid=@jid))";

        string str1 = @"select sssplid from SubSubSplCatVacancies where phsubcatid = @sub_ph and reqid in (select reqid from Job_Advt where jid=@jid)";


        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@sub_ph", SqlDbType.VarChar);
        param[j].Value = sub_ph;
        j++;
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        param[j].Value = Int32.Parse(jid);

        SqlParameter[] param1 = new SqlParameter[2];
        int k = 0;
        param1[k] = new SqlParameter("@sub_ph", SqlDbType.VarChar);
        param1[k].Value = sub_ph;
        k++;
        param1[k] = new SqlParameter("@jid", SqlDbType.Int);
        param1[k].Value = Int32.Parse(jid);

        try
        {
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

    public DataTable IsExist_Qualification(int applid, string qid)
    {
        string qry = "select qid from JobEducation where applid=@applid and qid=@qid";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@qid", SqlDbType.Int);
        param[j].Value = Int32.Parse(qid);
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


    public DataTable Get_advt_pdf(string adid)
    {
        String str = "select id,adv_file from advfile where id=@adid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@adid", SqlDbType.Int);
        param[j].Value = Int32.Parse(adid);
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
    public DataTable getdetail(string rid)
    {
        //        String str = @"select  rid,name, fname, mothername, case gender when 'F' then 'female' when 'M' then 'male' end as gender,convert(varchar,birthdt,103) as birthdt, 
        //                       nationality, mobileno, email,um_logid,rollno,passing_year,gender as sex,spousename,aadharNo,nameOnIDProof,pidm.docName,pid.proofOfIDNo,
        //                       birthdt as dob from registration reg join proofOfIDUploaded_Reg pid on reg.rid=pid.regNo join 
        //                       proofOfIdentityDocumentMaster pidm on pidm.docID=reg.proofOfID where rid=@rid";    
        String str = @"select  rid,name, fname, mothername, case gender when 'F' then 'female' when 'M' then 'male' end as gender,convert(varchar,birthdt,103) as birthdt, 
                        nationality, mobileno, email,um_logid,rollno,passing_year,gender as sex,spousename,aadharNo,nameOnIDProof,
                        birthdt as dob from registration where rid=@rid";
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
    public int updatemobile(string rid, string mobile, string email)
    {
        string str = "update registration set mobileno=@mobile,email=@email where rid=@rid";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@mobile", SqlDbType.NVarChar, 12);
        param[1].Value = mobile;
        param[2] = new SqlParameter("@email", SqlDbType.VarChar, 50);
        param[2].Value = email;
        try
        {
            int i = da.ExecuteParameterizedQuery(str, param);
            int t = 0;
            if (i > 0)
            {
                t = updatemobileinaplctn(rid, mobile, email);
            }
            return t;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int updatemobileinaplctn(string rid, string mobile, string email)
    {
        string str = "update jobapplication set mobileno=@mobile,email=@email where regno=@rid";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@mobile", SqlDbType.NVarChar, 12);
        param[1].Value = mobile;
        param[2] = new SqlParameter("@email", SqlDbType.VarChar, 50);
        param[2].Value = email;
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

    public DataTable get_prev_pic(string regno)
    {
        //        String str = @"select top(1) regno,JobApplication.applid,OLEModule,Signature,LTI,RTI from JobApplicationPhoto 
        //                    inner join JobApplication on JobApplication.applid=JobApplicationPhoto.ApplId
        //                    where dummy_no is not null
        //                    and RegNo=@regno order by applid desc";
        string str = @"select applid,OLEModule,Signature,LTI,RTI from JobApplicationPhoto where applid =(select top 1 applid from JobApplication  where dummy_no is not null
                    and RegNo=@regno order by applid desc) ";

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
    public int insert_prev_picsign(string prev_applid, string applid, string type)
    {
        string str = "";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@prev_applid", SqlDbType.VarChar, 50);
        param[0].Value = prev_applid;
        param[1] = new SqlParameter("@applid", SqlDbType.VarChar, 50);
        param[1].Value = applid;
        if (type == "p")
        {
            str = @"insert into JobApplicationPhoto (ApplId, OLEModule) select @applid, OLEModule from JobApplicationPhoto where applid=@prev_applid";
        }
        else if (type == "l")
        {
            str = @"insert into JobApplicationPhoto (ApplId, LTI) select @applid, LTI from JobApplicationPhoto where applid=@prev_applid";
        }
        else if (type == "r")
        {
            str = @"insert into JobApplicationPhoto (ApplId, RTI) select @applid, RTI from JobApplicationPhoto where applid=@prev_applid";
        }
        else
        {
            str = @"insert into JobApplicationPhoto (ApplId, signature) select @applid, signature from JobApplicationPhoto where applid=@prev_applid";
        }
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
    public int delete_Education_full(int applid, string qtype)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " qtype=@qtype ";
        }
        string str = "delete from JobEducation where applid=@applid and qid in (select uid from tbledu_TRN where " + condn + @")";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
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

    public int delete_desire_check(int applid)
    {

        string str = "delete from jobdesirable_master where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;

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

    public int delete_Education_EX(int applid, string qtype)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " qtype=@qtype ";
        }
        string str = "delete from JobEducation_EX where applid=@applid and qid in (select uid from tbledu_TRN where " + condn + @")";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
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

    public int delete_JobApplication_Exp_D_full(int applid)
    {
        string str = "delete from JobExperience where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@applid", SqlDbType.Int);

        param[j].Value = applid;
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
    public int update_prev_picsign(string prev_applid, string applid, string type)
    {
        string str = "";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@prev_applid", SqlDbType.VarChar, 50);
        param[0].Value = prev_applid;
        param[1] = new SqlParameter("@applid", SqlDbType.VarChar, 50);
        param[1].Value = applid;
        if (type == "p")
        {
            str = @"update JobApplicationPhoto 
                    set  OLEModule=(select OLEModule from JobApplicationPhoto  where applid=@prev_applid)
                    where ApplId=@applid";
        }
        else if (type == "l")
        {
            str = @"update JobApplicationPhoto 
                    set  LTI=(select LTI from JobApplicationPhoto  where applid=@prev_applid)
                    where ApplId=@applid";
        }
        else if (type == "r")
        {
            str = @"update JobApplicationPhoto 
                    set  RTI=(select RTI from JobApplicationPhoto  where applid=@prev_applid)
                    where ApplId=@applid";
        }
        else
        {
            str = @"update JobApplicationPhoto 
                    set  signature=(select signature from JobApplicationPhoto  where applid=@prev_applid)
                    where ApplId=@applid";
        }
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

    public DataTable fillJobAdvtMaster(string jid)
    {
        string str = "SELECT jid,JobTitle,MinAge,MaxAge,JobDescription,payscale,probation_year,essential_qual,desire_qual,essential_exp,desire_exp,exp_noofyears from Job_Advt where jid=@jid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.Int);
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
    public DataTable GetJobName(string jid)
    {
        str = "Select JobTitle from Job_Advt where jid='" + jid + "' ";


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
    public DataTable getfeedetail(string jid)
    {
        str = @"select feeamount from Job_Advt ja inner join AdvMaster am on ja.adid=am.adid
                 inner join Job_Source js on am.jobsourceid=js.Id where ja.jid=@jid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
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
    public DataTable GetAge_Relax(string jid)
    {
        str = "SELECT dbo.SubCat_Master.SubCat_code, dbo.SubCat_Master.SubCat_name, dbo.RR_age_relax.id, dbo.RR_age_relax.jid, dbo.RR_age_relax.CatCode," +
                      " dbo.RR_age_relax.CatCode as CatCode1,dbo.RR_age_relax.CatIndCS, dbo.RR_age_relax.CM, dbo.RR_age_relax.Fee_exmp, dbo.RR_age_relax.D_Year, dbo.categorymaster.categcode," +
                      " dbo.categorymaster.category FROM dbo.categorymaster RIGHT OUTER JOIN" +
                      " dbo.RR_age_relax ON dbo.categorymaster.categcode = dbo.RR_age_relax.CatCode LEFT OUTER JOIN" +
                      " dbo.SubCat_Master ON dbo.RR_age_relax.CatCode = dbo.SubCat_Master.SubCat_code where jid=@jid order by CatIndCS";
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

    public DataTable OMRRollVerification(string Rollno, string DOB)
    {
        int j = 0;
        str = "select rollno,birthdt from registration where Rollno=@Rollno and birthdt=@birthdt ";
        SqlParameter[] param = new SqlParameter[2];

        param[j] = new SqlParameter("@RollNo", SqlDbType.VarChar, 50);
        param[j].Value = Rollno;

        j++;
        param[j] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
        if (DOB == "" || DOB == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(DOB);
        }
        j++;


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
    public DataTable get_physical_standard(string jid, string mf)
    {
        int j = 0;
        str = @"select jid,job_advt.[reqid],
                PhysicalStandards.Ess_Height_LowerCm, PhysicalStandards.Ess_Height_UpperCm, PhysicalStandards.Id, PhysicalStandards.Ess_Chest_LowerCm, 
                PhysicalStandards.Ess_Chest_UpperCm, PhysicalStandards.Ess_SoundHealthFreefromDefectDeformityDesease, 
                PhysicalStandards.Ess_Vision6withoutGlassesbothEyes, PhysicalStandards.Ess_FreeFromColourBlindness, PhysicalStandards.Desi_Height_LowerCm, 
                PhysicalStandards.Desi_Height_UpperCm, PhysicalStandards.Desi_Chest_LowerCm, PhysicalStandards.Desi_Chest_UpperCm, 
                PhysicalStandards.Desi_SoundHealthFreefromDefectDeformityDesease, PhysicalStandards.Desi_Vision6withoutGlassesbothEyes, 
                PhysicalStandards.Desi_FreeFromColourBlindness, PhysicalStandards.Ess_Height_LowerFt, PhysicalStandards.Ess_Height_UpperFt, 
                PhysicalStandards.Ess_Chest_LowerFt, PhysicalStandards.Ess_Chest_UpperFt, PhysicalStandards.Desi_Height_LowerFt, 
                PhysicalStandards.Desi_Height_UpperFt, PhysicalStandards.Desi_Chest_LowerFt, PhysicalStandards.Desi_Chest_UpperFt, 
                PhysicalStandards.Ess_Height_LowerIn, PhysicalStandards.Ess_Height_UpperIn, PhysicalStandards.Ess_Chest_LowerIn, 
                PhysicalStandards.Ess_Chest_UpperIn, PhysicalStandards.Desi_Height_LowerIn, PhysicalStandards.Desi_Height_UpperIn, 
                PhysicalStandards.Desi_Chest_LowerIn, PhysicalStandards.Desi_Chest_UpperIn,[Height_Relax_In]
                ,[Height_Relax_cm]
                ,[Chest_Relax_In]
                ,[Chest_Relax_cm],isnull(SCST_Relax,'') SCST_Relax
                FROM job_advt 
                inner JOIN PhysicalStandards ON job_advt.reqid = PhysicalStandards.reqid 
                where jid=@jid and (PhysicalStandards.gender=@mf or PhysicalStandards.gender='B') ";
        SqlParameter[] param = new SqlParameter[2];

        param[j] = new SqlParameter("@jid", SqlDbType.VarChar, 50);
        param[j].Value = jid;
        j++;
        param[j] = new SqlParameter("@mf", SqlDbType.VarChar);
        param[j].Value = mf;
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
    public DataTable get_weight_standard(string jid)
    {
        int j = 0;
        str = @"select jid,job_advt.[reqid],
                weight_male,weight_female
                FROM job_advt 
                inner JOIN RR_weight ON job_advt.reqid = RR_weight.reqid
                where jid=@jid";
        SqlParameter[] param = new SqlParameter[1];

        param[j] = new SqlParameter("@jid", SqlDbType.VarChar, 50);
        param[j].Value = jid;

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
    public DataTable check_fee_relax(string cat_subcat)
    {
        if (cat_subcat.Contains(","))
        {
            cat_subcat = cat_subcat.Replace(",", "','");
        }

        str = @"select [CatCode]
                ,[CatIndCS]
                ,[Fee_exmp]
                FROM [fee_exemption] where CatCode in('" + cat_subcat + "') ";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@cat_subcat", SqlDbType.VarChar);
        param[j].Value = cat_subcat;

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
    public DataTable getOTP()
    {
        str = @"SELECT [id]      
              ,[randomno],mobileno    
              FROM [forgetpass] fp
              inner join registration reg on reg.rid=fp.rid
              where expired='N' and CONVERT(varchar,date,103)=convert(varchar,GETDATE(),103)
              and id not in(select id from otpsend)";
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
    public DataTable insert_OTP(string id)
    {
        str = @"insert into otpsend(id) values('" + id + "')";
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
    public DataTable getAdmitcarddetails(string applid, string examid, string flagfromintra, string rbtvalue)
    {
        try
        {
            string str = "";

            if (rbtvalue == "2" || rbtvalue == "4")
            {
                str = @"SELECT Applicant_result.applid,ResultVerification.examid, JobApplication.name, JobApplication.RegNo, JobApplication.fname, 
                        JobApplication.address, JobApplication.category, 
                        JobApplication.dummy_no, JobApplication.SubCategory,convert(varchar,examMast.dateofexam,103)+' 
                        ('+DATENAME(dw,examMast.dateofexam)+')' dateofexam, examMast.timeofexam, examMast.reportingtime, 
                        CentreMaster.centername, Applicant_result.rollno,
                        CentreMaster.address as centeraddress,CentreMaster.center_code,case isprovisional when 'Y' then '2' else '1' end as acstatid,dbo.getphsubcat(isnull(ph,'')) as phsubcat
                        ,exammast.SigId,sm.CDesig
                        FROM  
                        JobApplication 
                        inner join Applicant_result on Applicant_result.applid=JobApplication.applid
                        inner join ResultVerification on ResultVerification.rvid=Applicant_result.rvid and ResultVerification.examid='" + examid + @"'
                        INNER JOIN examMast ON examMast.examid = ResultVerification.examid 
                        Left Outer JOIN SignatureMaster sm on exammast.sigid=sm.sigid
                        INNER JOIN ApplicantCenter on 
                        ((Applicant_result.applid=ApplicantCenter.applid and Applicant_result.wcommon is null) 
                        or (Applicant_result.RegNo=ApplicantCenter.regno and Applicant_result.wcommon='Y'))                      
                        and ApplicantCenter.examid ='" + examid + @"'
                        INNER JOIN
                        CentreMaster ON CentreMaster.Centrecode= ApplicantCenter.centercode                      
                        where Applicant_result.applid='" + applid + @"'";
                if (flagfromintra != "1")
                {
                    str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";

                }
                // str += " and JobApplication.acstatid in('1','2')";
            }
            else if (rbtvalue == "3" || rbtvalue == "5")
            {
                str = @"SELECT Applicant_result.applid,ResultVerification.examid, JobApplication.name, JobApplication.RegNo, JobApplication.fname, 
                        JobApplication.address, JobApplication.category, 
                        JobApplication.dummy_no, JobApplication.SubCategory,convert(varchar,BatchMaster.examdate,103)+' 
                        ('+DATENAME(dw,BatchMaster.examdate)+')' dateofexam, BatchMaster.examtime as timeofexam, BatchMaster.reportingtime, 
                        CentreMaster.centername, Applicant_result.rollno,
                        CentreMaster.address as centeraddress,CentreMaster.center_code,case isprovisional when 'Y' then '2' else '1' end as acstatid,dbo.getphsubcat(isnull(ph,'')) as phsubcat,JobApplication.gender,
                        exammast.SigId,sm.CDesig FROM  
                        JobApplication 
                        inner join Applicant_result on Applicant_result.applid=JobApplication.applid
                        inner join ResultVerification on ResultVerification.rvid=Applicant_result.rvid and ResultVerification.examid='" + examid + @"'
                        INNER JOIN examMast ON examMast.examid = ResultVerification.examid
			Left Outer JOIN SignatureMaster sm on exammast.sigid=sm.sigid  
                        INNER JOIN ApplicantCenter on 
                        ((Applicant_result.applid=ApplicantCenter.applid and Applicant_result.wcommon is null) 
                        or (Applicant_result.RegNo=ApplicantCenter.regno and Applicant_result.wcommon='Y'))                      
                        and ApplicantCenter.examid ='" + examid + @"'
                        inner join BatchMaster on ApplicantCenter.batchid = BatchMaster.batchid  
                        INNER JOIN
                        CentreMaster ON CentreMaster.Centrecode= BatchMaster.centercode 
                                           
                        where Applicant_result.applid='" + applid + @"'";
                if (flagfromintra != "1")
                {
                    str = str + " and convert(varchar,examdate,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";

                }

            }
            else
            {

                str = @"SELECT JobApplication.applid,Job_Advt.examid, JobApplication.name, JobApplication.RegNo, JobApplication.fname, JobApplication.address, JobApplication.category, 
                      JobApplication.dummy_no, JobApplication.SubCategory,convert(varchar,examMast.dateofexam,103)+' ('+DATENAME(dw,examMast.dateofexam)+')' dateofexam, examMast.timeofexam, examMast.reportingtime, 
                      Job_Advt.postcode, Job_Advt.JobTitle, CentreMaster.centername, ApplicantCenter.rollno,CentreMaster.address as centeraddress,CentreMaster.center_code,acstatid,dbo.getphsubcat(isnull(ph,'')) as phsubcat
                      ,exammast.SigId,sm.CDesig
                      FROM  
                      JobApplication 
                      INNER JOIN  Job_Advt ON Job_Advt.jid = JobApplication.jid
                      INNER JOIN examMast ON examMast.examid = Job_Advt.examid 
                      Left Outer JOIN SignatureMaster sm on exammast.sigid=sm.sigid
                      INNER JOIN ApplicantCenter on 
                      ((JobApplication.applid=ApplicantCenter.applid and JobApplication.wcommon is null) 
                      or (JobApplication.RegNo=ApplicantCenter.regno and JobApplication.wcommon='Y'))                      
                      and ApplicantCenter.examid ='" + examid + @"'
                      INNER JOIN
                      CentreMaster ON CentreMaster.Centrecode= ApplicantCenter.centercode                      
                      where JobApplication.applid='" + applid + @"'";

                //            if (flagfromintra != "2")
                //            {
                //                str = str + @"
                //                      --and dateofexam>=GETDATE()
                //                      and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111)
                //                      --and radmitcard='Y'
                //                      and JobApplication.acstatid in('1','2')";
                //            }
                if (flagfromintra != "1")
                {
                    str = str + " and convert(varchar,dateofexam,111)>=convert(varchar,getdate(),111) and radmitcard='Y'";

                }
                str += " and JobApplication.acstatid in('1','2','5')";
            }
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataTable getapplctnnos(string regno)
    {
        try
        {
            string str = @"select dummy_no,RegNo,dateofexam,ja.JobTitle + ' ('+ja.postcode+')' as jobdetails from JobApplication jap 
                        inner join Job_Advt ja on jap.jid=ja.jid and jap.dummy_no is not null and jap.RegNo=@regno
                        inner join examMast em on ja.examid=em.examid where em.dateofexam>GETDATE() order by RegNo";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
            param[0].Value = regno;

            dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    public DataTable get_reopen_data(string jid)
    {
        try
        {
            string str = @"select jid,isnull(category,'') category,isnull(subcat,'') subcat,isnull(quali,'') quali,isnull(agerelax,'') agerelax,isnull(subsubcat,'') subsubcat,
  isnull(subsub_subcat,'') subsub_subcat from reopenpost where jid=@jid";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@jid", SqlDbType.VarChar, 50);
            param[0].Value = jid;

            dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable checkserialno(string serial_no, string dob)
    {
        str = "select serial_no,dob from oldpostdata where serial_no=@serial_no and dob=@dob ";


        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        param[0].Value = serial_no;
        param[1] = new SqlParameter("@dob", SqlDbType.DateTime);
        param[1].Value = dob;
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
    public DataTable checkregno(string rid, string birthdt)
    {
        str = "select rid,birthdt from registration where rid=@rid and birthdt=@birthdt ";


        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@birthdt", SqlDbType.DateTime);
        param[1].Value = birthdt;
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
    public int Insertspecial_reg_mapping(string regno, string serial_no, string postcode)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into oldpostmapping (regno,serial_no,postcode) values(@regno,@serial_no,@postcode)";

        SqlParameter[] param = new SqlParameter[3];

        param[j] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[j].Value = regno;
        j++;
        param[j] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        param[j].Value = serial_no;
        j++;
        param[j] = new SqlParameter("@postcode", SqlDbType.VarChar, 50);
        param[j].Value = postcode;

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
    public DataTable checkserialnomapping(string serial_no)
    {
        str = "select serial_no,regno from oldpostmapping where serial_no=@serial_no ";


        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        param[0].Value = serial_no;

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
    //    public DataTable GetJobAdvt_splpost()
    //    {
    //        //        str = @"SELECT advMaster.adid,jobdescription,flag,Job_Advt.gender,convert(varchar(20),convert(varchar,advMaster.AdNo)+'/'+convert(varchar(20), advMaster.AdYear)) 
    //        //                as ADVT_NO,Job_Advt.postcode,REPLACE((Job_Advt.JobTitle + ' :: Post Code:' + Job_Advt.postcode + ' '),'[dot]','.') as announcement,AdNo,AdYear ,REPLACE(Job_Advt.JobTitle,'[dot]','.') as JobTitle, CONVERT(VARCHAR,advMaster.StartsFrom,103) StartsFrom, 
    //        //                CONVERT(VARCHAR,advMaster.EndsOn,103) EndsOn,isnull(CONVERT(VARCHAR,advMaster.FeeLastDate,103),'') FeeLastDate,CONVERT(VARCHAR,Job_Advt.DOBFrom,103) DOBFrom, 
    //        //                CONVERT(VARCHAR,Job_Advt.DOBTO,103) DOBTO, convert(varchar,feeamount) +'/-' fee,REPLACE(essential_qual,'[dot]','.') as essential_qual,REPLACE(desire_qual,'[dot]','.') as desire_qual,REPLACE(essential_exp,'[dot]','.') as essential_exp,REPLACE(desire_exp,'[dot]','.') as desire_exp,
    //        //                jid jobid,reqid FROM Job_Advt 
    //        //                inner join advMaster on advMaster.adid=Job_Advt.adid
    //        //                inner join Job_Source on Job_Source.Id=advMaster.jobsourceid 
    //        //                where CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
    //        //                and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
    //        //                cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
    //        //                order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int) 
    //        //                --order by Startsfrom,postcode"; 
    //        str = @"SELECT advMaster.adid,advMaster.flag,jobdescription,Job_Advt.gender,convert(varchar(20),convert(varchar,advMaster.AdNo)
    //                    +'/'+convert(varchar(20), advMaster.AdYear)) 
    //                    as ADVT_NO,Job_Advt.postcode,REPLACE((Job_Advt.JobTitle + ' :: Post Code:' + Job_Advt.postcode + ' '),'[dot]','.') 
    //                    as announcement,AdNo,AdYear ,
    //                    REPLACE(Job_Advt.JobTitle,'[dot]','.') as JobTitle, 
    //                    case Reopened 
    //                    when 'Y' then
    //                    CONVERT(VARCHAR,reopenadvt.StartsFrom,103) 
    //                    else
    //                    CONVERT(VARCHAR,advMaster.StartsFrom,103) 
    //                    end
    //                    StartsFrom,
    //                    case Reopened 
    //                    when 'Y' then
    //                    CONVERT(VARCHAR,reopenadvt.EndsOn,103) 
    //                    else
    //                    CONVERT(VARCHAR,advMaster.EndsOn,103) 
    //                    end 
    //                    EndsOn,
    //                    case Reopened 
    //                    when 'Y' then
    //                    isnull(CONVERT(VARCHAR,reopenadvt.FeeLastDate,103),'') 
    //                    else
    //                    isnull(CONVERT(VARCHAR,advMaster.FeeLastDate,103),'') 
    //                    end
    //                    FeeLastDate,
    //                    CONVERT(VARCHAR,Job_Advt.DOBFrom,103) DOBFrom, 
    //                    CONVERT(VARCHAR,Job_Advt.DOBTO,103) DOBTO, convert(varchar,feeamount) +'/-' 
    //                    fee,REPLACE(essential_qual,'[dot]','.') as essential_qual,REPLACE(desire_qual,'[dot]','.') 
    //                    as desire_qual,REPLACE(essential_exp,'[dot]','.') as essential_exp,REPLACE(desire_exp,'[dot]','.') as desire_exp,
    //                    Job_Advt.jid jobid,reqid FROM Job_Advt 
    //                    inner join advMaster on advMaster.adid=Job_Advt.adid
    //                    left outer join reopenadvt on advMaster.adid=reopenadvt.adid and AdvMaster.Reopened='Y' 
    //                    inner join Job_Source on Job_Source.Id=advMaster.jobsourceid 
    //                    where 
    //                    (
    //                    CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
    //                    and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
    //                    cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)
    //                    )
    //                    or
    //                    (
    //                    --and reopenadvt.ReleaseStatus ='Y' 
    //                    (CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
    //                    and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >=
    //                    cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)
    //                    )
    //                    and Job_Advt.jid in(select jid from reopenpost where  oldapplication='Y')
    //                    and reopenadvt.reopenstatus in ('C','D')
    //                    )
    //
    //                    order by cast(SUBSTRING(postcode,1,CHARINDEX('/',postcode)-1) as int) ";

    //        try
    //        {

    //            dt = da.GetDataTable(str);
    //            return dt;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }


    //    }
    public DataTable checkserialnoforcandidate(string serial_no, string dob)
    {
        str = "select serial_no,dob,name,f_name,cat,opd.postcode,':'+ja.jobtitle as jobtitle from oldpostdata opd left outer join job_advt ja on opd.postcode=ja.postcode where serial_no=@serial_no and dob=@dob ";


        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        param[0].Value = serial_no;
        param[1] = new SqlParameter("@dob", SqlDbType.VarChar, 10);
        param[1].Value = dob;
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
    public DataTable checkserial_postcode(string serial_no, string postcode)
    {
        str = "select serial_no,dob,name,f_name,cat,postcode from oldpostdata where serial_no=@serial_no and postcode=@postcode";


        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        param[0].Value = serial_no;
        param[1] = new SqlParameter("@postcode", SqlDbType.VarChar, 50);
        param[1].Value = postcode;
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
    public DataTable get_serial_data(string regno, string postcode)
    {
        string str = @"select oldpostmapping.serial_no,name,f_name,sex,cat,ipo_no,amount from oldpostmapping
                        inner join oldpostdata on oldpostdata.serial_no=oldpostmapping.serial_no
                         where regno=@regno and oldpostmapping.postcode=@postcode";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar);
        param[0].Value = regno;
        param[1] = new SqlParameter("@postcode", SqlDbType.VarChar);
        param[1].Value = postcode;


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
    public int insert_old_fee_data(string applid, string jid, string name, string ipo_no, string amount, string feetype, string lastdate)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into feedetails (jid,applid,jrnlno,feerecd,feetype,amount,can_name,trandate) 
         values(@jid,@applid,@ipo_no,'Y',@feetype,@amount,@name,@trandate)";

        SqlParameter[] param = new SqlParameter[7];

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
        param[j] = new SqlParameter("@jid", SqlDbType.Int);
        if (jid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Int32.Parse(jid);
        }
        j++;
        param[j] = new SqlParameter("@ipo_no", SqlDbType.VarChar);
        if (ipo_no == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = ipo_no;
        }
        j++;
        param[j] = new SqlParameter("@feetype", SqlDbType.VarChar);
        if (feetype == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = feetype;
        }
        j++;
        param[j] = new SqlParameter("@amount", SqlDbType.VarChar);
        if (amount == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = amount;
        }
        j++;
        param[j] = new SqlParameter("@name", SqlDbType.VarChar);
        if (name == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = name;
        }
        j++;
        param[j] = new SqlParameter("@trandate", SqlDbType.DateTime);
        if (lastdate == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = lastdate;
        }
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
    public DataTable get_fee_data(string applid)
    {
        //        string str = @"select applid,JobApplication.regno,oldpostmapping.serial_no,oldpostdata.name,f_name,sex,cat,ipo_no,amount 
        //                     from JobApplication  
        //                     inner join oldpostmapping on JobApplication.regno=oldpostmapping.regno
        //                     inner join oldpostdata on oldpostdata.serial_no=oldpostmapping.serial_no 
        //                     where applid=@applid
        //                     and oldpostmapping.postcode in(select postcode from JobApplication 
        //                     inner join Job_Advt on jobapplication.jid=Job_Advt.jid where applid=@applid)";

        string str = @"select applid,JobApplication.regno,oldpostmapping.serial_no,oldpostdata.name,f_name,sex,cat,ipo_no,amount,EndsOn
                     from JobApplication  
                     inner join oldpostmapping on JobApplication.regno=oldpostmapping.regno
                     inner join oldpostdata on oldpostdata.serial_no=oldpostmapping.serial_no 
                     inner join Job_Advt on Job_Advt.jid=jobapplication.jid
                     inner join AdvMaster on AdvMaster.adid=Job_Advt.adid
                     where applid=@applid
                     and oldpostmapping.postcode in(select postcode from JobApplication 
                     inner join Job_Advt on jobapplication.jid=Job_Advt.jid where applid=@applid)";

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
    public DataTable update_acstatid_for_intra_cand(string applid)
    {
        str = @"update jobapplication set acstatid=2 where applid=@applid";


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




    public DataTable check_fee_relax_female(string gender)
    {
        str = @"SELECT  CatCode, CatIndCS, Fee_exmp, gender, dateofimplimentation FROM  fee_exemption where gender=@gender and  Fee_exmp='Y'";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@gender", SqlDbType.Char, 1);
        param[j].Value = gender;

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
    public DataTable get_post_eDossier(string regno)
    {

        str = @"select distinct jobtitle + '( ' + ja.postcode + ' )' as post,
                 eds.jid 
                  -- ja.jid
                 from Job_Advt ja
                inner join EdossierSchedule eds on ja.jid=eds.jid
                inner join ResultVerification rv on rv.jid=ja.jid and rv.nextexam is null
                inner join Applicant_result ar on rv.rvid=ar.rvid and flag='V'
                where ar.RegNo=@regno 
               and released='Y' 
               ";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        if (regno == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = regno;
        }
        DataTable dt = da.GetDataTableQry(str, param);
        return dt;

    }

    public DataTable GetEdossierMaster(string jid, string applid, string CCategory)
    {
        string str = @"SELECT em.edmid, jid, certificateReq, ctype, priority,final,edid,case ctype  when 'M' then 'Mandatory' when 'O' then 'Optional' else 'If-Applicable' end as ctypename,adharno,Isnull(ced.remarks,'--') as remarks,subjects,maxmarks,marksobtained
                     FROM EdossierMaster em
                     left outer join candidateedossier ced on em.edmid=ced.edmid and applid=@applid
                     where jid=@jid and CCategory=@CCategory ";

        str += " order by priority  ";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[0].Value = jid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[1].Value = applid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }
        param[2] = new SqlParameter("@CCategory", SqlDbType.Char, 1);
        param[2].Value = CCategory;
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
    public string getapplid(string jid, string regno)
    {
        str = "SELECT  applid FROM  JobApplication where jid=@jid and regno=@regno";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[0].Value = jid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        if (regno == "")
        {
            param[1].Value = 0;
        }
        else
        {
            param[1].Value = regno;
        }
        try
        {
            dt = da.GetDataTableQry(str, param);
            string applid = dt.Rows[0]["applid"].ToString();
            return applid;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int inserteDossier(string edmid, Byte[] doc, string applid, string userid, string ipaddress, string adharno, string subcat, string othermiscdoc, string remarks)
    {
        string str = "insert into CandidateEdossier (applid, edmid, doc, userid, edate, ipaddress,adharno,subcat,othermiscdoc,remarks) values(@applid, @edmid, @doc, @userid, getdate(), @ipaddress,@adharno,@subcat,@othermiscdoc,@remarks) select scope_identity()";
        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@edmid", SqlDbType.Int, 4);
        if (edmid != "")
        {
            param[0].Value = edmid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@doc", SqlDbType.Image, doc.Length);
        param[1].Value = doc;
        param[2] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[2].Value = applid;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        param[3] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
        param[3].Value = userid;
        param[4] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
        param[4].Value = ipaddress;
        param[5] = new SqlParameter("@adharno", SqlDbType.VarChar, 50);
        if (adharno != "")
        {
            param[5].Value = adharno;
        }
        else
        {
            param[5].Value = System.DBNull.Value;
        }
        param[6] = new SqlParameter("@subcat", SqlDbType.VarChar, 4);
        if (subcat != "")
        {
            param[6].Value = subcat;
        }
        else
        {
            param[6].Value = System.DBNull.Value;
        }
        param[7] = new SqlParameter("@othermiscdoc", SqlDbType.VarChar, 200);
        if (othermiscdoc != "")
        {
            param[7].Value = othermiscdoc;
        }
        else
        {
            param[7].Value = System.DBNull.Value;
        }

        param[8] = new SqlParameter("@remarks", SqlDbType.VarChar, 200);
        if (remarks != "")
        {
            param[8].Value = remarks;
        }
        else
        {
            param[8].Value = System.DBNull.Value;
        }

        try
        {
            //int temp = da.ExecuteParameterizedQuery(str, param);
            int temp = Convert.ToInt32(da.ExecScaler(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int inserteDossier(string edmid, Byte[] doc, string applid, string userid, string ipaddress, string adharno, string subcat, string othermiscdoc, string remarks, string subjects, string maxmarks, string marksobtained)
    {
        string str = "insert into CandidateEdossier (applid, edmid, doc, userid, edate, ipaddress,adharno,subcat,othermiscdoc,remarks,subjects,maxmarks, marksobtained) values(@applid, @edmid, @doc, @userid, getdate(), @ipaddress,@adharno,@subcat,@othermiscdoc,@remarks,@subjects,@maxmarks, @marksobtained) select scope_identity()";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@edmid", SqlDbType.Int, 4);
        if (edmid != "")
        {
            param[0].Value = edmid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@doc", SqlDbType.Image, doc.Length);
        param[1].Value = doc;
        param[2] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[2].Value = applid;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        param[3] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
        param[3].Value = userid;
        param[4] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
        param[4].Value = ipaddress;
        param[5] = new SqlParameter("@adharno", SqlDbType.VarChar, 50);
        if (adharno != "")
        {
            param[5].Value = adharno;
        }
        else
        {
            param[5].Value = System.DBNull.Value;
        }
        param[6] = new SqlParameter("@subcat", SqlDbType.VarChar, 4);
        if (subcat != "")
        {
            param[6].Value = subcat;
        }
        else
        {
            param[6].Value = System.DBNull.Value;
        }
        param[7] = new SqlParameter("@othermiscdoc", SqlDbType.VarChar, 200);
        if (othermiscdoc != "")
        {
            param[7].Value = othermiscdoc;
        }
        else
        {
            param[7].Value = System.DBNull.Value;
        }

        param[8] = new SqlParameter("@remarks", SqlDbType.VarChar, 200);
        if (remarks != "")
        {
            param[8].Value = remarks;
        }
        else
        {
            param[8].Value = System.DBNull.Value;
        }
        param[9] = new SqlParameter("@subjects", SqlDbType.VarChar, 200);
        if (subjects != "")
        {
            param[9].Value = subjects;
        }
        else
        {
            param[9].Value = System.DBNull.Value;
        }
        param[10] = new SqlParameter("@maxmarks", SqlDbType.VarChar, 200);
        if (maxmarks != "")
        {
            param[10].Value = maxmarks;
        }
        else
        {
            param[10].Value = System.DBNull.Value;
        }
        param[11] = new SqlParameter("@marksobtained", SqlDbType.VarChar, 200);
        if (marksobtained != "")
        {
            param[11].Value = marksobtained;
        }
        else
        {
            param[11].Value = System.DBNull.Value;
        }
        try
        {
            //int temp = da.ExecuteParameterizedQuery(str, param);
            int temp = Convert.ToInt32(da.ExecScaler(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable select_eDossierCertificate(string edid)
    {
        string str = "select doc,edid from CandidateEdossier where edid=@edid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@edid", SqlDbType.Int);
        param[0].Value = edid;
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

    public int UpdateCandidateEdossier(string edid, Byte[] doc, string adharno, string ipaddress, string userid, string remarks)
    {
        string str = "update CandidateEdossier set doc=@doc,adharno=@adharno,userid=@userid, edate=getdate(), ipaddress=@ipaddress,remarks=@remarks where edid=@edid";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@edid", SqlDbType.Int, 4);
        param[0].Value = edid;
        param[1] = new SqlParameter("@doc", SqlDbType.Image, doc.Length);
        param[1].Value = doc;
        param[2] = new SqlParameter("@adharno", SqlDbType.VarChar, 50);
        if (adharno != "")
        {
            param[2].Value = adharno;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        param[3] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
        param[3].Value = userid;
        param[4] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
        param[4].Value = ipaddress;
        param[5] = new SqlParameter("@remarks", SqlDbType.VarChar, 200);
        if (remarks == "" || remarks == null)
        {
            param[5].Value = System.DBNull.Value;
        }
        else
        {
            param[5].Value = remarks.Trim();
        }

        try
        {
            int temp = da.ExecuteParameterizedQuery(str, param);
            //int temp =Convert.ToInt32( da.ExecScaler(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int UpdateCandidateEdossier(string edid, Byte[] doc, string adharno, string ipaddress, string userid, string remarks, string status)
    {
        string str = "update CandidateEdossier set doc=@doc,adharno=@adharno,userid=@userid, edate=getdate(), ipaddress=@ipaddress,remarks=@remarks,status=@status where edid=@edid";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@edid", SqlDbType.Int, 4);
        param[0].Value = edid;
        param[1] = new SqlParameter("@doc", SqlDbType.Image, doc.Length);
        param[1].Value = doc;
        param[2] = new SqlParameter("@adharno", SqlDbType.VarChar, 50);
        if (adharno != "")
        {
            param[2].Value = adharno;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        param[3] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
        param[3].Value = userid;
        param[4] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
        param[4].Value = ipaddress;
        param[5] = new SqlParameter("@remarks", SqlDbType.VarChar, 200);
        if (remarks == "" || remarks == null)
        {
            param[5].Value = System.DBNull.Value;
        }
        else
        {
            param[5].Value = remarks.Trim();
        }
        param[6] = new SqlParameter("@status", SqlDbType.Char, 1);
        if (status == "" || status == null)
        {
            param[6].Value = System.DBNull.Value;
        }
        else
        {
            param[6].Value = status;
        }
        try
        {
            int temp = da.ExecuteParameterizedQuery(str, param);
            //int temp =Convert.ToInt32( da.ExecScaler(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int UpdateCandidateEdossier(string edid, Byte[] doc, string adharno, string ipaddress, string userid, string remarks, string subjects, string maxmarks, string marksobtained)
    {
        string str = "update CandidateEdossier set doc=@doc,adharno=@adharno,userid=@userid, edate=getdate(), ipaddress=@ipaddress,remarks=@remarks,subjects=@subjects,maxmarks=@maxmarks,marksobtained=@marksobtained where edid=@edid";
        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@edid", SqlDbType.Int, 4);
        param[0].Value = edid;
        param[1] = new SqlParameter("@doc", SqlDbType.Image, doc.Length);
        param[1].Value = doc;
        param[2] = new SqlParameter("@adharno", SqlDbType.VarChar, 50);
        if (adharno != "")
        {
            param[2].Value = adharno;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        param[3] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
        param[3].Value = userid;
        param[4] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
        param[4].Value = ipaddress;
        param[5] = new SqlParameter("@remarks", SqlDbType.VarChar, 200);
        if (remarks == "" || remarks == null)
        {
            param[5].Value = System.DBNull.Value;
        }
        else
        {
            param[5].Value = remarks.Trim();
        }
        param[6] = new SqlParameter("@subjects", SqlDbType.VarChar, 200);
        if (subjects != "")
        {
            param[6].Value = subjects;
        }
        else
        {
            param[6].Value = System.DBNull.Value;
        }
        param[7] = new SqlParameter("@maxmarks", SqlDbType.VarChar, 200);
        if (maxmarks != "")
        {
            param[7].Value = maxmarks;
        }
        else
        {
            param[7].Value = System.DBNull.Value;
        }
        param[8] = new SqlParameter("@marksobtained", SqlDbType.VarChar, 200);
        if (marksobtained != "")
        {
            param[8].Value = marksobtained;
        }
        else
        {
            param[8].Value = System.DBNull.Value;
        }
        try
        {
            int temp = da.ExecuteParameterizedQuery(str, param);
            //int temp =Convert.ToInt32( da.ExecScaler(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetEdossierMaster_cat(string jid, string applid, string CCategory)
    {
        string str = @"SELECT em.edmid, jid, certificateReq, ctype, priority,final,edid,case ctype  when 'M' then 'Mandatory' when 'O' then 'Optional' 
                     else 'If-Applicable' end as ctypename,(select category from dsssbonline_recdapp.dbo.JobApplication where applid=@applid )+ ' Certificate' as category,Isnull(ced.remarks,'--') as remarks
                      FROM EdossierMaster em
                     left outer join candidateedossier ced on em.edmid=ced.edmid and applid=@applid

                     where jid=@jid and CCategory=@CCategory ";

        str += " order by priority  ";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[0].Value = jid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[1].Value = applid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }
        param[2] = new SqlParameter("@CCategory", SqlDbType.Char, 1);
        param[2].Value = CCategory;
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

    public DataTable GetEdossierMaster_subcat(string jid, string applid, string CCategory)
    {
        string str = @"SELECT em.edmid, jid, certificateReq, ctype, priority,final,edid,case ctype  when 'M' then 'Mandatory' when 'O' then 'Optional' 
                     else 'If-Applicable' end as ctypename,dbo.getsubcategory(@applid) as subcategory, subcat,Isnull(ced.remarks,'--') as remarks
                     FROM EdossierMaster em left outer join candidateedossier ced on em.edmid=ced.edmid and applid=@applid
                     where jid=@jid and CCategory=@CCategory order by priority  ";

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[0].Value = jid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[1].Value = applid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }
        param[2] = new SqlParameter("@CCategory", SqlDbType.Char, 1);
        param[2].Value = CCategory;
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

    public DataTable GetEdossierMaster_misc(string edmid, string applid)
    {
        //        string str = @"SELECT edmid, othermiscdoc,final,edid,remarks
        //                     from candidateedossier
        //                     where edmid in (select edmid from EdossierMaster where edmid=@edmid and CCategory='M') and applid=@applid ";
        string str = @"SELECT edmid, othermiscdoc,cd.final,edid,Isnull(remarks,'--') as remarks,cdf.final,cd.edate
                        ,
                        case
                        when 
                         cdf.final is null then 'N'
                        when
                        cd.edate<cdf.edate then 'Y'
                        when
                        cd.edate>=cdf.edate then 'N'
                        end as editflag
                        from candidateedossier cd
                        left outer join edossiersfinal  cdf on cd.applid=cdf.applid
                        where edmid in (select edmid from EdossierMaster where edmid=@edmid and CCategory='M') and cd.applid=@applid ";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@edmid", SqlDbType.Int, 4);
        if (edmid != "")
        {
            param[0].Value = edmid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[1].Value = applid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
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
    public DataTable GetEdossierMaster_exp(string edmid, string edid)
    {
        //        string str = @"SELECT edmid, othermiscdoc,final,edid,remarks
        //                     from candidateedossier
        //                     where edmid in (select edmid from EdossierMaster where edmid=@edmid and CCategory='M') and applid=@applid ";
        string str = @"SELECT edmid, cd.final,cd.edid as docid,Isnull(remarks,'--') as remarks,cdf.final,cd.edate
                        ,
                        case
                        when 
                         cdf.final is null then 'N'
                        when
                        cd.edate<cdf.edate then 'Y'
                        when
                        cd.edate>=cdf.edate then 'N'
                        end as editflag,
                        currentorgname, orgaddress, Wcentralorstate, orgstatename, orgministryname, isAutonomous, 
                         convert(varchar, dateofappoint,103) as dateofappoint, currentdesig,edc.edid,edc.id
                        from edcatdetails edc 
						inner join edpinfo edp on edp.edid=edc.edid
                        left outer join edossiersfinal  cdf on edp.applid=cdf.applid
                        left outer join candidateedossier cd  on edc.docid=cd.edid and edmid=@edmid
                        where  edc.edid=@edid and subcatcode='DGS' ";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@edmid", SqlDbType.Int, 4);
        if (edmid != "")
        {
            param[0].Value = edmid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@edid", SqlDbType.Int);
        if (edid != "")
        {
            param[1].Value = edid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
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
    public DataTable GetEdossierMaster_miscdoc(string jid)
    {
        string str = @"SELECT edmid, jid, certificateReq, ctype, priority,case ctype  when 'M' then 'Mandatory' when 'O' then 'Optional' 
                     else 'If-Applicable' end as ctypename
                     FROM EdossierMaster
                   where jid=@jid and CCategory='M' ";

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
    public DataTable GetEdossierMaster_expdoc(string jid)
    {
        string str = @"SELECT edmid, jid, certificateReq, ctype, priority,case ctype  when 'M' then 'Mandatory' when 'O' then 'Optional' 
                     else 'If-Applicable' end as ctypename
                     FROM EdossierMaster
                   where jid=@jid and CCategory='G' ";

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
    public int delete_candidateedossier(string edid)
    {

        string str = @"delete from candidateedossier where edid=@edid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@edid", SqlDbType.Int, 4);
        param[0].Value = edid;

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

    public DataTable CheckEdossierforfinal(string jid, string applid)
    {
        //        string str = @"SELECT edid, applid, ced.edmid, final, doc, adharno, subcat, othermiscdoc
        //                      FROM  EdossierMaster em left outer join CandidateEdossier ced on em.edmid =ced.edmid and applid=@applid 
        //                      where ctype='M'and jid=@jid and edid is null  ";
        string str = @"SELECT edid, applid, ced.edmid, final, doc, adharno, subcat, othermiscdoc
                      FROM  EdossierMaster em left outer join CandidateEdossier ced on em.edmid =ced.edmid and applid=@applid 
                      where ctype='M'and jid=@jid and edid is null  and certificateReq !='Admit Card IInd Page'";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[0].Value = jid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[1].Value = applid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
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

    public int inserteDossierFinal(string applid, string final, string userid, string ipaddress)
    {
        string str = "insert into edossiersfinal (applid, final, userid, edate, ipaddress) values(@applid, @final, @userid, getdate(), @ipaddress)";
        //select scope_identity();
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
        if (applid != "")
        {
            param[0].Value = applid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }

        param[1] = new SqlParameter("@final", SqlDbType.Char, 1);
        if (final != "")
        {
            param[1].Value = final;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }
        param[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
        param[2].Value = userid;
        param[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
        param[3].Value = ipaddress;

        try
        {
            int temp = da.ExecuteParameterizedQuery(str, param);
            //int temp =Convert.ToInt32( da.ExecScaler(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable Getedossiersfinal(string applid)
    {
        string str = @"SELECT eid, applid, final, userid, edate, ipaddress,edossierNo FROM  edossiersfinal where applid=@applid and final='Y' ";

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[0].Value = applid;
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

    public DataTable check_post_foruploadeDossier(string regno, string jid)
    {
        string con = "";
        string str1 = "select jid,rvid from EdossierSchedule where jid=@jid  and fromdt<=convert(varchar(10),GETDATE(),120) and todt>=convert(varchar(10),GETDATE(),120)";//30-11-2022
        SqlParameter[] param1 = new SqlParameter[1];
        int a = 0;
        param1[a] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param1[a].Value = jid;
        }
        else
        {
            param1[a].Value = System.DBNull.Value;
        }
        DataTable dt1 = da.GetDataTableQry(str1, param1);
        //if (dt1.Rows.Count > 1)
        //{
        //   // con = " and (eds.rvid=rv.rvid or eds.rvid is null) ";
        //  con = " and (eds.rvid is null or eds.rvid in(select rvid from  ResultVerification where jid=@jid)) ";
        //}

        if (dt1.Rows.Count >= 1)
        {
            if (dt1.Rows[0]["rvid"].ToString() == "")
            {
                // con = " and (eds.rvid=rv.rvid or eds.rvid is null) ";
                con = " and (eds.rvid is null or eds.rvid in(select rvid from  ResultVerification where jid=@jid)) ";
            }
            else
            {
                con = " and ar.rvid=" + dt1.Rows[0]["rvid"].ToString();
            }
        }
        str = @"select jobtitle + '( ' + ja.postcode + ' )' as post,
                eds.jid 
                --ja.jid
                from Job_Advt ja
                inner join EdossierSchedule eds on ja.jid=eds.jid
                inner join ResultVerification rv on rv.jid=ja.jid and rv.nextexam is null 
                inner join Applicant_result ar on rv.rvid=ar.rvid and flag='V'
                where ar.RegNo=@regno 
               and released='Y' and fromdt<=convert(varchar(10),GETDATE(),120) and todt>=convert(varchar(10),GETDATE(),120)
              and eds.jid=@jid " + con + @" 
--and ja.jid=@jid
";

        SqlParameter[] param = new SqlParameter[2];
        int j = 0;

        param[j] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        if (regno == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = regno;
        }
        j++;
        param[j] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[j].Value = jid;
        }
        else
        {
            param[j].Value = System.DBNull.Value;
        }
        DataTable dt = da.GetDataTableQry(str, param);
        return dt;

    }

    public string GetSubcategory(string SubCat_code)
    {
        string str = @"SELECT SubCat_name FROM  SubCat_Master where SubCat_code=@SubCat_code ";

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@SubCat_code", SqlDbType.VarChar, 4);
        if (SubCat_code != "")
        {
            param[0].Value = SubCat_code;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }

        try
        {
            dt = da.GetDataTableQry(str, param);
            string subcatname = dt.Rows[0]["SubCat_name"].ToString();
            return subcatname;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetRecalledDoc(string jid, string applid)
    {
        string str = @"SELECT em.edmid, jid, isnull(othermiscdoc,certificateReq) as certificateReq, ctype, priority,final,edid,case ctype  when 'M' then 'Mandatory' when 'O' then 'Optional' else 'If-Applicable' end as ctypename,adharno
                     FROM EdossierMaster em
                     inner join candidateedossier ced on em.edmid=ced.edmid and applid=@applid
                     where jid=@jid and applid=@applid and status='R' and replacedt+1>=getdate() ";

        str += " order by priority  ";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[0].Value = jid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid != "")
        {
            param[1].Value = applid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
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

    public int updateCandEdossierStatus(string edid)
    {
        string str = "update CandidateEdossier set status='S' where edid=@edid";
        //select scope_identity();
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@edid", SqlDbType.Int, 4);
        param[0].Value = edid;

        try
        {
            int temp = da.ExecuteParameterizedQuery(str, param);
            //int temp =Convert.ToInt32( da.ExecScaler(str, param));
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }













    public DataTable getcandidatepostforupdate(string regno)
    
    {
        try
        {
            string str = @"select applid from jobapplication where regno=@regno
                           union select applid from  testdb_dsssbonline_recdapp.dbo.jobapplication where regno=@regno";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
            if (regno != "")
            {
                param[0].Value = regno;
            }
            else
            {
                param[0].Value = DBNull.Value;
            }


            dt = da.GetDataTableQry(str, param);

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int update_mobileno(string rid, string mobile)
    {
        string str = "update registration set mobileno=@mobile where rid=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@mobile", SqlDbType.NVarChar, 12);
        param[1].Value = mobile;

        try
        {
            int i = da.ExecuteParameterizedQuery(str, param);
            int t = 0;
            if (i > 0)
            {
                t = updatemobile_jobapplication(rid, mobile);
            }
            return t;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public int update_email(string rid, string email)
    {
        string str = "update registration set email=@email where rid=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@email", SqlDbType.VarChar, 50);
        param[1].Value = email;
        try
        {
            int i = da.ExecuteParameterizedQuery(str, param);
            int t = 0;
            if (i > 0)
            {
                t = updateemail_jobapplication(rid, email);
            }
            return t;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public int update_name(string rid, string name)
    {
        string str = "update registration set name=@name where rid=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
        param[1].Value = name;

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

    public int Inserterrorlog_pag(string username, string regno1, string errorname, string pagename, string functionname, string ip, string browsername)
    {
        string InsetAudit = "Insert into mst_errorlog(username,regno,errorname,pagename,functionname,ip,browsername) values(@username,@regno,@errorname,@pagename,@functionname,@ip,@browsername)";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@username", SqlDbType.NVarChar);
        param[0].Value = username;
        param[1] = new SqlParameter("@regno", SqlDbType.NVarChar);
        param[1].Value = regno1;
        param[2] = new SqlParameter("@errorname", SqlDbType.NVarChar);
        param[2].Value = errorname;
        param[3] = new SqlParameter("@pagename", SqlDbType.NVarChar);
        param[3].Value = pagename;
        param[4] = new SqlParameter("@functionname", SqlDbType.NVarChar);
        param[4].Value = functionname;
        param[5] = new SqlParameter("@ip", SqlDbType.NVarChar);
        param[5].Value = ip;
        param[6] = new SqlParameter("@browsername", SqlDbType.NVarChar);
        param[6].Value = browsername;

        try
        {
            int id = da.ExecuteParameterizedQuery(InsetAudit, param);
            return id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int update_fname(string rid, string fname)
    {
        string str = "update registration set fname=@fname where rid=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@fname", SqlDbType.NVarChar, 50);
        param[1].Value = fname;

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
    public int update_mname(string rid, string mname)
    {
        string str = "update registration set mothername=@mname where rid=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@mname", SqlDbType.NVarChar, 50);
        param[1].Value = mname;

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
    public int update_sname(string rid, string spousename)
    {
        string str = "update registration set spousename=@spousename where rid=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@spousename", SqlDbType.VarChar, 50);
        param[1].Value = spousename;

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
    public int update_gender(string rid, string gender)
    {
        string str = "update registration set gender=@gender where rid=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@gender", SqlDbType.Char, 1);
        param[1].Value = gender;

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
    public int insert_online_feedata(string applid, string orderno, string amount, string transmode, string trandate, string feerecd)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into feedetails (jid,applid,jrnlno,feerecd,transmode,amount,trandate) 
         values((select jid from jobapplication where applid=@applid),@applid,@orderno,@feerecd,@transmode,@amount,@trandate)";

        SqlParameter[] param = new SqlParameter[6];

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

        param[j] = new SqlParameter("@orderno", SqlDbType.VarChar);
        if (orderno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = orderno;
        }
        j++;
        param[j] = new SqlParameter("@feerecd", SqlDbType.Char, 1);
        if (feerecd == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = feerecd;
        }
        j++;
        param[j] = new SqlParameter("@amount", SqlDbType.Decimal);
        if (amount == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = amount;
        }
        j++;

        param[j] = new SqlParameter("@trandate", SqlDbType.DateTime);
        if (trandate == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = trandate;
        }
        j++;

        param[j] = new SqlParameter("@transmode", SqlDbType.VarChar, 20);
        if (transmode == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = transmode;
        }
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
    public DataTable get_post_for_payment(string regno)
    {
        str = @"select Job_Advt.[jid] jid,JobApplication.applid as applid
            ,replace([JobTitle],'[dot]','.')+' Post Code :('+postcode+')' post
            from Job_Advt 
            inner join JobApplication on JobApplication.jid=Job_Advt.jid
            left outer join feedetails on JobApplication.applid=feedetails.applid
            where adid in (select adid from advmaster where CONVERT(VARCHAR,GETDATE(),111) between CONVERT(VARCHAR,StartsFrom,111) and CONVERT(VARCHAR,FeeLastDate,111)
				            union select adid from reopenadvt where CONVERT(VARCHAR,GETDATE(),111) between CONVERT(VARCHAR,StartsFrom,111) and CONVERT(VARCHAR,FeeLastDate,111))
			and RegNo=@regno and dummy_no is null and final='Y' and feerecd is null";

        SqlParameter[] param = new SqlParameter[1];
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
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;
    }
    public DataTable checkfeedetails(string applid)
    {
        try
        {
            string str = @"select applid,jid,feerecd from feedetails where applid=@applid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@applid", SqlDbType.Int);
            if (applid != "")
            {
                param[0].Value = applid;
            }
            else
            {
                param[0].Value = DBNull.Value;
            }


            dt = da.GetDataTableQry(str, param);

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int update_online_feedata(string applid, string orderno, string amount, string transmode, string trandate, string feerecd)
    {
        int i = 0;
        int j = 0;
        string str = @"update feedetails set jid=(select jid from jobapplication where applid=@applid),jrnlno=@orderno,feerecd=@feerecd,transmode=@transmode,
                       amount=@amount,trandate=@trandate where applid=@applid";

        SqlParameter[] param = new SqlParameter[6];

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

        param[j] = new SqlParameter("@orderno", SqlDbType.VarChar);
        if (orderno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = orderno;
        }
        j++;
        param[j] = new SqlParameter("@feerecd", SqlDbType.Char, 1);
        if (feerecd == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = feerecd;
        }
        j++;
        param[j] = new SqlParameter("@amount", SqlDbType.Decimal);
        if (amount == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = amount;
        }
        j++;

        param[j] = new SqlParameter("@trandate", SqlDbType.DateTime);
        if (trandate == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = trandate;
        }
        j++;

        param[j] = new SqlParameter("@transmode", SqlDbType.VarChar, 20);
        if (transmode == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = transmode;
        }
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


    public int Update_AdmitCConsent(string examid, string regno)
    {
        string str = @"update jobapplication set acconsent='Y' where jid in (select jid from job_advt where examid=@examid) 
                       and  acstatid ='1' and regno=@regno and (acconsent<>'Y' or acconsent is null) ";

        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
        param[0].Value = Convert.ToInt32(examid);

        param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[1].Value = examid;

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


    //@@@@@@@@@@@@@@@@@@@@@ Transcation for Update and Insert for Admit Card Consent   @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    public int Update_AdmitCConsentTransaction(string examid, string regno, Int64 verificationID, string edate, string ipaddress, string flagphase)
    {

        int Result = 0;
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
                    string str = "";

                    if (flagphase == "1")
                    {
                        str = @"update jobapplication set acconsent='Y' where jid in (select jid from job_advt where examid=@examid) 
                                   and  acstatid ='1' and regno=@regno and (acconsent<>'Y' or acconsent is null) ";
                    }
                    else
                    {
                        str = @"update jobapplication set acconsent='Y',acconsent_phase2='Y' where jid in (select jid from job_advt where examid=@examid) 
                                   and  acstatid ='1' and regno=@regno and (acconsent_phase2 <>'Y' or acconsent_phase2 is null) ";
                    }

                    SqlParameter[] param = new SqlParameter[2];

                    param[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
                    param[0].Value = Convert.ToInt32(examid);

                    param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
                    param[1].Value = regno;


                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param != null)
                    {
                        foreach (SqlParameter param1 in param)
                        {
                            command.Parameters.Add(param1);
                        }

                    }

                    command.ExecuteNonQuery();

                    //second query

                    string straccr = @"insert into AdmitCConsentRequest (examid, regno, verificationid, edate, cipaddress)values(@examid, @regno, @verificationid, @edate, @cipaddress) ";


                    SqlParameter[] param3 = new SqlParameter[5];

                    param3[0] = new SqlParameter("@examid", SqlDbType.Int, 4);
                    param3[0].Value = Convert.ToInt32(examid);

                    param3[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
                    param3[1].Value = regno;

                    param3[2] = new SqlParameter("@verificationid", SqlDbType.BigInt);
                    param3[2].Value = verificationID;

                    param3[3] = new SqlParameter("@edate", SqlDbType.DateTime, 8);
                    param3[3].Value = edate;

                    param3[4] = new SqlParameter("@cipaddress", SqlDbType.VarChar, 50);
                    param3[4].Value = ipaddress;

                    command.CommandType = CommandType.Text;
                    command.CommandText = straccr;
                    command.Parameters.Clear();


                    if (param3 != null)
                    {
                        foreach (SqlParameter param2 in param3)
                        {
                            command.Parameters.Add(param2);
                        }

                    }
                    command.ExecuteNonQuery();


                    //command.ExecuteNonQuery();
                    transaction.Commit();
                    Result = 1;

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
        return Result;
    }

    //@@@@@@@@@@@@@@@@@@@@@ End Transcation @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    public DataTable getperinfoedossier(string jid, string regno)
    {
        try
        {

            //CASE WHEN dateadd(year, datediff (year, birthdt, EndsOn), birthdt) > EndsOn THEN datediff(year, birthdt, EndsOn) - 1 ELSE datediff(year, birthdt, EndsOn) END as Age
            string str = @"select (replace(ja.JobTitle,'[dot]','.') + '(Post Code:' + ja.postcode + ')') as post,
                    name, fname,  mothername, address, address_per,mobileno,spousename,  CONVERT(varchar,birthdt,103) birthdt,applid,rollno,
                    email,pin_per,'' as maritalstatus,edid,age from  edpinfo edp inner join Job_Advt ja on ja.jid=edp.jid
                    where edp.jid=@jid and RegNo=@regno";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@jid", SqlDbType.Int);
            param[0].Value = jid;
            param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
            param[1].Value = regno;
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 0)
            {

                string str1 = @"select (replace(ja.JobTitle,'[dot]','.') + '(Post Code:' + ja.postcode + ')') as post,
                    name, fname,  mothername, address+'Pincode-'+pin as address, address_per,mobileno,  maritalstatus,  CONVERT(varchar,birthdt,103) birthdt,jap.applid,ar.rollno,
                    email,pin_per,spousename,'' as edid, 
                  --cast((DATEDIFF(m, birthdt, EndsOn)/12) as varchar) + ' Years, ' + cast((DATEDIFF(m, birthdt, EndsOn)%12) as varchar) + ' Months' as Age
                   convert(varchar, (DATEDIFF(year,birthdt ,EndsOn)- CASE WHEN MONTH(EndsOn)*100+DAY(EndsOn)<MONTH(birthdt)*100+
                    DAY(birthdt) then 1 else 0 end))+
    ' Years,'+convert(varchar,((DATEDIFF(month,birthdt,EndsOn)  - CASE WHEN DAY(EndsOn)<DAY(birthdt) THEN 1 ELSE 0 END ) % 12))+
   ' Months ,'+convert(varchar,( DATEDIFF (day,dateadd(month,(DATEDIFF(month,birthdt,EndsOn)  - CASE WHEN DAY(EndsOn)<DAY(birthdt) THEN 1 ELSE 0 END )  
    ,birthdt) ,EndsOn)))+' Days' as Age,ar.tier
from dsssbonline_recdapp.dbo.JobApplication jap inner join Job_Advt ja on ja.jid=jap.jid
                    inner join advmaster am on ja.adid=am.adid
                    inner join Applicant_result ar on jap.applid=ar.applid and ar.flag='V'
                    where jap.jid=@jid and jap.RegNo=@regno order by ar.tier DESC";
                //            string str1 = @"select (replace(ja.JobTitle,'[dot]','.') + '(Post Code:' + ja.postcode + ')') as post,
                //                    jap.name, jap.fname,  jap.mothername, jap.address+'Pincode-'+jap.PIN as address, jap.address_per,jap.mobileno,  maritalstatus,  CONVERT(varchar,jap.birthdt,103) birthdt,jap.applid,ar.rollno,
                //                    jap.email,jap.PIN_per,jap.spousename,edp.edid as edid,
                //                  --cast((DATEDIFF(m, birthdt, EndsOn)/12) as varchar) + ' Years, ' + cast((DATEDIFF(m, birthdt, EndsOn)%12) as varchar) + ' Months' as Age
                //                   convert(varchar, (DATEDIFF(year,jap.birthdt ,EndsOn)- CASE WHEN MONTH(EndsOn)*100+DAY(EndsOn)<MONTH(jap.birthdt)*100+
                //                    DAY(jap.birthdt) then 1 else 0 end))+
                //    ' Years,'+convert(varchar,((DATEDIFF(month,jap.birthdt,EndsOn)  - CASE WHEN DAY(EndsOn)<DAY(jap.birthdt) THEN 1 ELSE 0 END ) % 12))+
                //   ' Months ,'+convert(varchar,( DATEDIFF (day,dateadd(month,(DATEDIFF(month,jap.birthdt,EndsOn)  - CASE WHEN DAY(EndsOn)<DAY(jap.birthdt) THEN 1 ELSE 0 END )  
                //    ,jap.birthdt) ,EndsOn)))+' Days' as Age,ar.tier
                //from dsssbonline_recdapp.dbo.JobApplication jap inner join Job_Advt ja on ja.jid=jap.jid
                //                    inner join advmaster am on ja.adid=am.adid
                //                    inner join Applicant_result ar on jap.applid=ar.applid and ar.flag='V'
                //					inner join edpinfo edp on edp.applid=ar.applid
                //                    where jap.jid=@jid and jap.RegNo=@regNo order by ar.tier DESC";
                SqlParameter[] param1 = new SqlParameter[2];
                param1[0] = new SqlParameter("@jid", SqlDbType.Int);
                param1[0].Value = jid;
                param1[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
                param1[1].Value = regno;
                dt = da.GetDataTableQry(str1, param1);

            }
            return dt;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int InsertEDPinfo(int jid, int applid, string Name, string FatherName, string MotherName, string spousename, string PresentAdd, string ParmaAdd, string PresentPIN, string Mobile, string Email, string DOB, string IP, string rollno, string regno, string age)
    {
        int i = 0;
        int j = 0;

        string str = @"insert into EDPinfo (jid,applid,name,fname,mothername,spousename,address_per,address,PIN_per,mobileno,email,birthdt,IP,rollno,Entry_date,regno,age) 
         values(@jid,@applid,@name,@fname,@mothername,@spousename,@address_per,@address,@PIN_per,@mobileno,@email,@birthdt,@IP,@rollno,getdate(),@regno,@age) Select SCOPE_IDENTITY() ";

        SqlParameter[] param = new SqlParameter[16];

        param[j] = new SqlParameter("@jid", SqlDbType.SmallInt);
        param[j].Value = jid;
        j++;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;


        param[j] = new SqlParameter("@name", SqlDbType.NVarChar);
        if (Name == "" || Name == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Name;
        }
        j++;

        param[j] = new SqlParameter("@fname", SqlDbType.NVarChar);
        if (FatherName == "" || FatherName == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = FatherName;
        }
        j++;
        param[j] = new SqlParameter("@mothername", SqlDbType.NVarChar);
        if (MotherName == "" || MotherName == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = MotherName;
        }
        j++;
        param[j] = new SqlParameter("@spousename", SqlDbType.VarChar);
        if (spousename == "" || spousename == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = spousename;
        }
        j++;
        param[j] = new SqlParameter("@address_per", SqlDbType.NVarChar);
        if (PresentAdd == "" || PresentAdd == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PresentAdd;
        }
        j++;

        param[j] = new SqlParameter("@address", SqlDbType.NVarChar);
        if (ParmaAdd == "" || ParmaAdd == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = ParmaAdd;
        }
        j++;

        param[j] = new SqlParameter("@PIN_per", SqlDbType.VarChar);
        if (PresentPIN == "" || PresentPIN == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PresentPIN;
        }
        j++;



        param[j] = new SqlParameter("@mobileno", SqlDbType.NVarChar);
        if (Mobile == "" || Mobile == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Mobile;
        }
        j++;
        param[j] = new SqlParameter("@email", SqlDbType.VarChar);
        if (Email == "" || Email == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Email;
        }
        j++;



        param[j] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
        if (DOB == "" || DOB == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(DOB);
        }
        j++;


        param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@rollno", SqlDbType.BigInt);
        if (rollno == "" || rollno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = rollno;
        }
        j++;
        param[j] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        if (regno == "" || regno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = regno;
        }
        j++;

        param[j] = new SqlParameter("@age", SqlDbType.VarChar, 50);
        if (age == "" || age == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = age;
        }
        j++;
        try
        {
            i = Convert.ToInt32(da.ExecScaler(str, param));
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int UpdateEDPinfo(int edid, string PresentAdd, string PresentPIN, string IP, string spousename)
    {
        int i = 0;
        int j = 0;

        string str = @"update EDPinfo set address_per=@address_per,PIN_per=@PIN_per,IP=@IP,Entry_date=getdate(),spousename=@spousename where edid=@edid ";

        SqlParameter[] param = new SqlParameter[5];

        param[j] = new SqlParameter("@edid", SqlDbType.Int);
        param[j].Value = edid;
        j++;

        param[j] = new SqlParameter("@address_per", SqlDbType.NVarChar);
        if (PresentAdd == "" || PresentAdd == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PresentAdd;
        }
        j++;
        param[j] = new SqlParameter("@PIN_per", SqlDbType.VarChar);
        if (PresentPIN == "" || PresentPIN == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = PresentPIN;
        }
        j++;

        param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@spousename", SqlDbType.VarChar);
        if (spousename == "" || spousename == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = spousename;
        }
        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public DataTable getcatsubcatdetailsforedossier(string jid, string regno)
    {
        try
        {
            string str = @"select (replace(ja.JobTitle,'[dot]','.') + '(Post Code:' + ja.postcode + ')') as post,
                    ar.rollno,category,dbo.getsubcategory(jap.applid) as subcategory,jap.applid,CastCerApplyState,state,
                    CLCNo,convert(varchar,CLCDate,103) as clcdate,CastCertIssueAuth
                    from  dsssbonline_recdapp.dbo.JobApplication jap inner join Job_Advt ja on ja.jid=jap.jid
                    inner join Applicant_result ar on jap.applid=ar.applid and flag='V'
                    inner join m_state ms on jap.CastCerApplyState=ms.code
                    where jap.jid=@jid and jap.RegNo=@regno";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@jid", SqlDbType.Int);
            param[0].Value = jid;
            param[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
            param[1].Value = regno;
            dt = da.GetDataTableQry(str, param);


            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int InsertEDCatSubcat(int edid, string catcode, string subcatcode, string IP, string ccstate, string certificateno, string certissuedate, string certissuedesig, string certissuedist, string certissuestate, string certissuedistoutsidedelhi)
    {
        int i = 0;
        int j = 0;

        string str = @"insert into edcatdetails (edid,catcode,subcatcode,ip,edate,ccstate,certificateno,certissuedate,certissuedesig,certissuedist,certissuestate,certissuedistoutsidedelhi) values(@edid,@catcode,@subcatcode,@ip,getdate(),@ccstate,@certificateno,@certissuedate,@certissuedesig,@certissuedist,@certissuestate,@certissuedistoutsidedelhi) Select SCOPE_IDENTITY() ";

        SqlParameter[] param = new SqlParameter[11];

        param[j] = new SqlParameter("@edid", SqlDbType.Int);
        param[j].Value = edid;
        j++;

        param[j] = new SqlParameter("@catcode", SqlDbType.VarChar, 5);
        if (catcode == "" || catcode == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = catcode;
        }
        j++;
        param[j] = new SqlParameter("@subcatcode", SqlDbType.VarChar, 5);
        if (subcatcode == "" || subcatcode == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = subcatcode;
        }
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@ccstate", SqlDbType.Char, 1);
        if (ccstate == "" || ccstate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = ccstate;
        }
        j++;
        param[j] = new SqlParameter("@certificateno", SqlDbType.VarChar, 50);
        if (certificateno == "" || certificateno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certificateno;
        }
        j++;
        param[j] = new SqlParameter("@certissuedate", SqlDbType.Date);
        if (certissuedate == "" || certissuedate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedate;
        }
        j++;
        param[j] = new SqlParameter("@certissuedesig", SqlDbType.VarChar, 50);
        if (certissuedesig == "" || certissuedesig == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedesig;
        }
        j++;
        param[j] = new SqlParameter("@certissuedist", SqlDbType.Int);
        if (certissuedist == "" || certissuedist == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedist;
        }
        j++;

        param[j] = new SqlParameter("@certissuestate", SqlDbType.VarChar, 50);
        if (certissuestate == "" || certissuestate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuestate;
        }
        j++;
        param[j] = new SqlParameter("@certissuedistoutsidedelhi", SqlDbType.VarChar, 50);
        if (certissuedistoutsidedelhi == "" || certissuedistoutsidedelhi == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedistoutsidedelhi;
        }

        try
        {
            i = Convert.ToInt32(da.ExecScaler(str, param));
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int UpdateEDCatDetails(int id, string catcode, string certificateno, string certissuedate, string certissuedesig, string certissuedist, string certissuestate, string IP, string certissuedistoutsidedelhi)
    {
        int i = 0;
        int j = 0;

        string str = @"update edcatdetails set certificateno=@certificateno,certissuedate=@certissuedate,certissuedesig=@certissuedesig,certissuedist=@certissuedist,certissuestate=@certissuestate,
                     ip=@ip,edate=getdate(),certissuedistoutsidedelhi=@certissuedistoutsidedelhi where id=@id and catcode=@catcode ";

        SqlParameter[] param = new SqlParameter[9];

        param[j] = new SqlParameter("@id", SqlDbType.Int);
        param[j].Value = id;
        j++;

        param[j] = new SqlParameter("@catcode", SqlDbType.VarChar, 5);
        if (catcode == "" || catcode == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = catcode;
        }
        j++;

        param[j] = new SqlParameter("@certificateno", SqlDbType.VarChar, 50);
        if (certificateno == "" || certificateno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certificateno;
        }
        j++;
        param[j] = new SqlParameter("@certissuedate", SqlDbType.Date);
        if (certissuedate == "" || certissuedate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedate;
        }
        j++;
        param[j] = new SqlParameter("@certissuedesig", SqlDbType.VarChar, 50);
        if (certissuedesig == "" || certissuedesig == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedesig;
        }
        j++;
        param[j] = new SqlParameter("@certissuedist", SqlDbType.Int);
        if (certissuedist == "" || certissuedist == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedist;
        }
        j++;

        param[j] = new SqlParameter("@certissuestate", SqlDbType.VarChar, 50);
        if (certissuestate == "" || certissuestate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuestate;
        }
        j++;


        param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@certissuedistoutsidedelhi", SqlDbType.VarChar, 50);
        if (certissuedistoutsidedelhi == "" || certissuedistoutsidedelhi == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedistoutsidedelhi;
        }

        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int updateEDphsubcatDetails(int id, string subcatcode, string certificateno, string certissuedate, string certissuedesig, string certissuedist, string certissuestate, string IP, string certissueMInst)
    {
        int i = 0;
        int j = 0;

        string str = @"update edcatdetails set certificateno=@certificateno,certissuedate=@certissuedate,certissuedesig=@certissuedesig,certissuedistoutsidedelhi=@certissuedist,certissuestate=@certissuestate
                     ,ip=@ip,edate=getdate(),certissueMInst=@certissueMInst where id=@id and subcatcode=@subcatcode";


        SqlParameter[] param = new SqlParameter[9];

        param[j] = new SqlParameter("@id", SqlDbType.Int);
        param[j].Value = id;
        j++;

        param[j] = new SqlParameter("@subcatcode", SqlDbType.VarChar, 5);
        if (subcatcode == "" || subcatcode == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = subcatcode;
        }
        j++;

        param[j] = new SqlParameter("@certificateno", SqlDbType.VarChar, 50);
        if (certificateno == "" || certificateno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certificateno;
        }
        j++;
        param[j] = new SqlParameter("@certissuedate", SqlDbType.Date);
        if (certissuedate == "" || certissuedate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedate;
        }
        j++;
        param[j] = new SqlParameter("@certissuedesig", SqlDbType.VarChar, 50);
        if (certissuedesig == "" || certissuedesig == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedesig;
        }
        j++;
        param[j] = new SqlParameter("@certissuedist", SqlDbType.VarChar, 50);
        if (certissuedist == "" || certissuedist == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedist;
        }
        j++;

        param[j] = new SqlParameter("@certissuestate", SqlDbType.VarChar, 50);
        if (certissuestate == "" || certissuestate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuestate;
        }
        j++;


        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 50);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@certissueMInst", SqlDbType.VarChar, 100);
        if (certissueMInst == "" || certissueMInst == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissueMInst;
        }
        j++;
        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int updateEDEXsubcatDetails(int id, string subcatcode, string Defservjoindate, string defservdiscdate, string tlendefserv, string discreason, string discrank, string discoffname, string discoffaddress, string IP)
    {
        int i = 0;
        int j = 0;

        string str = @"update edcatdetails set Defservjoindate=@Defservjoindate,defservdiscdate=@defservdiscdate,tlendefserv=@tlendefserv,discreason=@discreason, discrank=@discrank,
                     discoffname=@discoffname,discoffaddress=@discoffaddress,IP=@IP,edate=getdate() where id=@id and subcatcode=@subcatcode ";


        SqlParameter[] param = new SqlParameter[10];

        param[j] = new SqlParameter("@id", SqlDbType.Int);
        param[j].Value = id;
        j++;

        param[j] = new SqlParameter("@subcatcode", SqlDbType.VarChar, 5);
        if (subcatcode == "" || subcatcode == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = subcatcode;
        }
        j++;

        param[j] = new SqlParameter("@Defservjoindate", SqlDbType.Date);
        if (Defservjoindate == "" || Defservjoindate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Defservjoindate;
        }
        j++;
        param[j] = new SqlParameter("@defservdiscdate", SqlDbType.Date);
        if (defservdiscdate == "" || defservdiscdate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = defservdiscdate;
        }
        j++;
        param[j] = new SqlParameter("@tlendefserv", SqlDbType.VarChar, 50);
        if (tlendefserv == "" || tlendefserv == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = tlendefserv;
        }
        j++;
        param[j] = new SqlParameter("@discreason", SqlDbType.VarChar, 200);
        if (discreason == "" || discreason == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = discreason;
        }
        j++;

        param[j] = new SqlParameter("@discrank", SqlDbType.VarChar, 50);
        if (discrank == "" || discrank == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = discrank;
        }
        j++;
        param[j] = new SqlParameter("@discoffname", SqlDbType.VarChar, 100);
        if (discoffname == "" || discoffname == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = discoffname;
        }
        j++;
        param[j] = new SqlParameter("@discoffaddress", SqlDbType.VarChar, 200);
        if (discoffaddress == "" || discoffaddress == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = discoffaddress;
        }
        j++;

        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 50);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;

        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int updateEDDeptcandsubcatDetails(int id, string subcatcode, string currentorgname, string orgaddress, string Wcentralorstate, string orgstatename, string orgministryname, string isAutonomous, string dateofappoint, string currentdesig, string IP)
    {
        int i = 0;
        int j = 0;

        string str = @"update edcatdetails set currentorgname=@currentorgname,orgaddress=@orgaddress,Wcentralorstate=@Wcentralorstate,orgstatename=@orgstatename, orgministryname=@orgministryname
                     ,isAutonomous=@isAutonomous,dateofappoint=@dateofappoint,currentdesig=@currentdesig,IP=@IP,edate=getdate() where id=@id and subcatcode=@subcatcode ";


        SqlParameter[] param = new SqlParameter[11];

        param[j] = new SqlParameter("@id", SqlDbType.Int);
        param[j].Value = id;
        j++;

        param[j] = new SqlParameter("@subcatcode", SqlDbType.VarChar, 5);
        if (subcatcode == "" || subcatcode == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = subcatcode;
        }
        j++;

        param[j] = new SqlParameter("@currentorgname", SqlDbType.VarChar, 100);
        if (currentorgname == "" || currentorgname == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = currentorgname;
        }
        j++;
        param[j] = new SqlParameter("@orgaddress", SqlDbType.VarChar, 200);
        if (orgaddress == "" || orgaddress == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = orgaddress;
        }
        j++;
        param[j] = new SqlParameter("@Wcentralorstate", SqlDbType.VarChar, 10);
        if (Wcentralorstate == "" || Wcentralorstate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Wcentralorstate;
        }
        j++;
        param[j] = new SqlParameter("@orgstatename", SqlDbType.VarChar, 50);
        if (orgstatename == "" || orgstatename == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = orgstatename;
        }
        j++;

        param[j] = new SqlParameter("@orgministryname", SqlDbType.VarChar, 100);
        if (orgministryname == "" || orgministryname == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = orgministryname;
        }
        j++;
        param[j] = new SqlParameter("@isAutonomous", SqlDbType.Char, 1);
        if (isAutonomous == "" || isAutonomous == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = isAutonomous;
        }
        j++;
        param[j] = new SqlParameter("@dateofappoint", SqlDbType.Date);
        if (dateofappoint == "" || dateofappoint == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = dateofappoint;
        }
        j++;
        param[j] = new SqlParameter("@currentdesig", SqlDbType.VarChar, 50);
        if (currentdesig == "" || currentdesig == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = currentdesig;
        }
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 50);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;

        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int InsertEDqualification(int edid, string qid, string percentage, string board, string state, string month, string year, int standard, string IP, string finalresultdate, string govtorpvt, string docproofpvtinst, string edqid, string otherdegreename, string instname)
    {
        int i = 0;
        int j = 0;

        string str = @"insert into edqualidetails (edid,qid,percentage,board,state,month,year,standard,edate,IP,finalresultdate,govtorpvt,docproofpvtinst,edqid,otherdegreename,instname) 
         values(@edid,@qid,@percentage,@board,@state,@month,@year,@standard,getdate(),@IP,@finalresultdate,@govtorpvt,@docproofpvtinst,@edqid,@otherdegreename,@instname) Select SCOPE_IDENTITY() ";

        SqlParameter[] param = new SqlParameter[15];

        param[j] = new SqlParameter("@edid", SqlDbType.Int);
        param[j].Value = edid;
        j++;

        param[j] = new SqlParameter("@qid", SqlDbType.Int);
        if (qid == "" || qid == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = qid;
        }
        j++;

        param[j] = new SqlParameter("@percentage", SqlDbType.Decimal);
        if (percentage == "" || percentage == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = percentage;
        }
        j++;
        param[j] = new SqlParameter("@board", SqlDbType.VarChar, 50);
        if (board == "" || board == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = board;
        }
        j++;

        param[j] = new SqlParameter("@state", SqlDbType.VarChar);
        if (state == "" || state == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = state;
        }
        j++;
        param[j] = new SqlParameter("@year", SqlDbType.VarChar);
        if (year == "" || year == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = year;
        }
        j++;

        param[j] = new SqlParameter("@standard", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;

        param[j] = new SqlParameter("@month", SqlDbType.VarChar);
        if (month == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = month;
        }
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 50);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;

        param[j] = new SqlParameter("@instname", SqlDbType.VarChar, 200);
        if (instname == "" || instname == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = instname;
        }
        j++;
        param[j] = new SqlParameter("@govtorpvt", SqlDbType.Char, 1);
        if (govtorpvt == "" || govtorpvt == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = govtorpvt;
        }
        j++;
        param[j] = new SqlParameter("@finalresultdate", SqlDbType.Date);
        if (finalresultdate == "" || finalresultdate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = finalresultdate;
        }
        j++;


        param[j] = new SqlParameter("@edqid", SqlDbType.Int);
        if (edqid == "" || edqid == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = edqid;
        }
        j++;
        param[j] = new SqlParameter("@otherdegreename", SqlDbType.VarChar, 200);
        if (otherdegreename == "" || otherdegreename == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = otherdegreename;
        }
        j++;
        param[j] = new SqlParameter("@docproofpvtinst", SqlDbType.Int);
        if (docproofpvtinst == "" || docproofpvtinst == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = docproofpvtinst;
        }

        try
        {
            i = Convert.ToInt32(da.ExecScaler(str, param));
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    public DataTable getedqualidetails(string edid)
    {
        try
        {
            string str = @"SELECT  edq.edid, qid, percentage, board, instname, edq.state as Stateid, month, year, edq.standard,convert(varchar, finalresultdate,103) as finalresultdate, govtorpvt,
                          edq.id,sm.standard as stnd,te.name,ms.State,(month + '/' + YEAR) as myear,edq.edqid,otherdegreename,
                          edmid,docproofpvtinst,case edq.edqid when '99' then otherdegreename+' (Not Available in the RR)' else edqname end as edqname,
                            case govtorpvt when 'G' then 'Goverment' when 'P' then 'Private'  end as instgovorpvt
                          FROM edqualidetails edq 
                        inner join standardMaster sm on edq.standard=sm.id 
                        left outer join  tbledu_TRN tr on tr.uid = edq.qid 
                        left outer join tbledu te on te.id=tr.id
                        left outer join m_state ms on ms.code=edq.state
                        inner join EDPinfo edp on edp.edid=edq.edid 
                        inner join EdossierMaster edm on edm.jid=edp.jid 
                        left outer join EdossierQualiMast edqm on edq.edqid=edqm.edqid 
                        where edm.CCategory='D'  and edq.edid=@edid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@edid", SqlDbType.Int);
            param[0].Value = edid;

            dt = da.GetDataTableQry(str, param);


            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int UpdateEDqualidetails(int id, string instname, string finalresultdate, string govtorpvt, string IP, string edqid, string otherdegreename, string docproofpvtinst)
    {
        int i = 0;
        int j = 0;

        string str = @"update edqualidetails set instname=@instname,finalresultdate=@finalresultdate,IP=@IP,edate=getdate(),govtorpvt=@govtorpvt,edqid=@edqid,otherdegreename=@otherdegreename,docproofpvtinst=@docproofpvtinst where id=@id ";

        SqlParameter[] param = new SqlParameter[8];

        param[j] = new SqlParameter("@id", SqlDbType.Int);
        param[j].Value = id;
        j++;

        param[j] = new SqlParameter("@instname", SqlDbType.VarChar, 200);
        if (instname == "" || instname == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = instname;
        }
        j++;
        param[j] = new SqlParameter("@govtorpvt", SqlDbType.Char, 1);
        if (govtorpvt == "" || govtorpvt == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = govtorpvt;
        }
        j++;
        param[j] = new SqlParameter("@finalresultdate", SqlDbType.Date);
        if (finalresultdate == "" || finalresultdate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = finalresultdate;
        }
        j++;

        param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@edqid", SqlDbType.Int);
        if (edqid == "" || edqid == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = edqid;
        }
        j++;
        param[j] = new SqlParameter("@otherdegreename", SqlDbType.VarChar, 200);
        if (otherdegreename == "" || otherdegreename == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = otherdegreename;
        }
        j++;
        param[j] = new SqlParameter("@docproofpvtinst", SqlDbType.Int);
        if (docproofpvtinst == "" || docproofpvtinst == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = docproofpvtinst;
        }

        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public DataTable getedcatsubcatdetails(string edid)
    {
        try
        {
            string str = @"SELECT id, edid, catcode, subcatcode, certificateno,convert(varchar, certissuedate,103) as certissuedate , certissuedesig, certissuedist, certissuestate, certissueMInst,convert(varchar, Defservjoindate,103) as Defservjoindate,convert(varchar, defservdiscdate,103) as defservdiscdate, 
                      tlendefserv, discreason, discrank, discoffname, discoffaddress, currentorgname, orgaddress, Wcentralorstate, orgstatename, orgministryname, isAutonomous, certissuedistoutsidedelhi,
                     convert(varchar, dateofappoint,103) as dateofappoint, currentdesig, edate, ip,docid, certificateno_father,docid,
                 convert(varchar, certissuedate_father,103) as certissuedate_father, certissuedesig_father, certissuedist_father, certissuestate_father, ccstate FROM edcatdetails where edid=@edid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@edid", SqlDbType.Int);
            param[0].Value = edid;

            dt = da.GetDataTableQry(str, param);


            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getendson(string jid)
    {
        string str = @"SELECT convert(varchar,endson,103) as endson FROM advmaster am inner join job_advt ja on am.adid=ja.adid where jid=@jid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.Int);
        param[0].Value = jid;

        dt = da.GetDataTableQry(str, param);
        return dt;
    }
    public long insert_edossierNO(string applid)
    {
        SqlParameter[] param = new SqlParameter[2];
        // int j = 0;


        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[0].Value = 0;
        }
        else
        {
            param[0].Value = Int32.Parse(applid);
        }
        param[1] = new SqlParameter("@innoOut", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;
        try
        {
            int edossierNo = da.ExecuteSql("edossiersfinal_Generate_edossierNo", param);
            //return long.Parse(param[0].Value.ToString());
            return edossierNo;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataTable GetEdossierqualiMaster(string jid)
    {
        string str = @"SELECT edqid, edqname FROM EdossierQualiMast where jid=@jid";

        str += " order by edqname  ";
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
    public DataTable GetEdossierdistict()
    {
        string str = @"SELECT distid, distname_e FROM m_District ";

        str += " order by distname_e  ";

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
    public DataTable GetEdossierstate()
    {
        string str = @"SELECT code, state FROM m_state where code<>'0'";

        str += " order by state  ";

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
    public int Updateexpdocid(int docid, int id)
    {
        int i = 0;
        int j = 0;

        string str = @"update edcatdetails set docid=@docid where id=@id ";

        SqlParameter[] param = new SqlParameter[2];

        param[j] = new SqlParameter("@docid", SqlDbType.Int);
        if (docid == 0 || docid == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = docid;
        }
        j++;
        param[j] = new SqlParameter("@id", SqlDbType.Int);
        param[j].Value = id;
        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int insertEDDeptcandsubcatDetails(int edid, string subcatcode, string currentorgname, string orgaddress, string Wcentralorstate, string orgstatename, string orgministryname, string isAutonomous, string dateofappoint, string currentdesig, string IP)
    {
        int i = 0;
        int j = 0;

        string str = @"insert into edcatdetails (edid,subcatcode,currentorgname,orgaddress,Wcentralorstate,orgstatename,orgministryname,isAutonomous,dateofappoint,currentdesig,IP,edate) 
                      values (@edid,@subcatcode,@currentorgname,@orgaddress,@Wcentralorstate,@orgstatename,@orgministryname,@isAutonomous,@dateofappoint,@currentdesig,@IP,getdate()) Select SCOPE_IDENTITY()";


        SqlParameter[] param = new SqlParameter[11];

        param[j] = new SqlParameter("@edid", SqlDbType.Int);
        param[j].Value = edid;
        j++;

        param[j] = new SqlParameter("@subcatcode", SqlDbType.VarChar, 5);
        if (subcatcode == "" || subcatcode == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = subcatcode;
        }
        j++;

        param[j] = new SqlParameter("@currentorgname", SqlDbType.VarChar, 100);
        if (currentorgname == "" || currentorgname == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = currentorgname;
        }
        j++;
        param[j] = new SqlParameter("@orgaddress", SqlDbType.VarChar, 200);
        if (orgaddress == "" || orgaddress == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = orgaddress;
        }
        j++;
        param[j] = new SqlParameter("@Wcentralorstate", SqlDbType.VarChar, 10);
        if (Wcentralorstate == "" || Wcentralorstate == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Wcentralorstate;
        }
        j++;
        param[j] = new SqlParameter("@orgstatename", SqlDbType.VarChar, 50);
        if (orgstatename == "" || orgstatename == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = orgstatename;
        }
        j++;

        param[j] = new SqlParameter("@orgministryname", SqlDbType.VarChar, 100);
        if (orgministryname == "" || orgministryname == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = orgministryname;
        }
        j++;
        param[j] = new SqlParameter("@isAutonomous", SqlDbType.Char, 1);
        if (isAutonomous == "" || isAutonomous == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = isAutonomous;
        }
        j++;
        param[j] = new SqlParameter("@dateofappoint", SqlDbType.Date);
        if (dateofappoint == "" || dateofappoint == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = dateofappoint;
        }
        j++;
        param[j] = new SqlParameter("@currentdesig", SqlDbType.VarChar, 50);
        if (currentdesig == "" || currentdesig == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = currentdesig;
        }
        j++;
        param[j] = new SqlParameter("@IP", SqlDbType.VarChar, 50);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;

        try
        {
            // i = da.ExecuteParameterizedQuery(str, param);
            i = Convert.ToInt32(da.ExecScaler(str, param));
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    public int insertfathercertdetails(int edid, string certificateno_father, string certissuedate_father, string certissuedesig_father, string certissuedist_father, string certissuestate_father, string IP, string certfatherormother, string certissuedist_father_notdelhi)
    {
        int i = 0;
        int j = 0;

        string str = @"insert into fathercertdetails values (@edid,@certificateno_father,@certissuedate_father,@certissuedesig_father,@certissuedist_father,@certissuestate_father,@certfatherormother,@certissuedist_father_notdelhi,@IP)";

        SqlParameter[] param = new SqlParameter[9];

        param[j] = new SqlParameter("@edid", SqlDbType.Int);
        param[j].Value = edid;
        j++;

        param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@certificateno_father", SqlDbType.VarChar, 50);
        if (certificateno_father == "" || certificateno_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certificateno_father;
        }
        j++;
        param[j] = new SqlParameter("@certissuedate_father", SqlDbType.Date);
        if (certissuedate_father == "" || certissuedate_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(certissuedate_father);
        }
        j++;
        param[j] = new SqlParameter("@certissuedesig_father", SqlDbType.VarChar, 50);
        if (certissuedesig_father == "" || certissuedesig_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedesig_father;
        }
        j++;
        param[j] = new SqlParameter("@certissuedist_father", SqlDbType.Int);
        if (certissuedist_father == "" || certissuedist_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedist_father;
        }
        j++;

        param[j] = new SqlParameter("@certissuestate_father", SqlDbType.Int);
        if (certissuestate_father == "" || certissuestate_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuestate_father;
        }
        j++;
        param[j] = new SqlParameter("@certfatherormother", SqlDbType.Char, 1);
        if (certfatherormother == "" || certfatherormother == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certfatherormother;
        }
        j++;
        param[j] = new SqlParameter("@certissuedist_father_notdelhi", SqlDbType.VarChar, 50);
        if (certissuedist_father_notdelhi == "" || certissuedist_father_notdelhi == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedist_father_notdelhi;
        }

        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    public int Updatewfatherdocid(int edid)
    {
        string str = @"update dbo.CandidateEdossier set wfather='Y' where edid=@edid ";

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@edid", SqlDbType.Int);
        param[0].Value = edid;

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

    public DataTable getfathercertdetails(string edid)
    {
        try
        {
            string str = @"SELECT fcdid,certificateno_father,certfatherormother,remarks,
                         convert(varchar, certissuedate_father,103) as certissuedate_father, certissuedesig_father, certissuedist_father, certissuestate_father,cd.edid as docid
                         FROM fathercertdetails fcd inner join EDPinfo edp on fcd.edid=edp.edid
                         left outer join CandidateEdossier cd on edp.applid=cd.applid and wfather='Y'
                         where fcd.edid=@edid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@edid", SqlDbType.Int);
            param[0].Value = edid;

            dt = da.GetDataTableQry(str, param);


            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int updatefathercertdetails(int fcdid, string certificateno_father, string certissuedate_father, string certissuedesig_father, string certissuedist_father, string certissuestate_father, string certfatherormother, string certissuedist_father_notdelhi)
    {
        int i = 0;
        int j = 0;

        string str = @"update fathercertdetails set certificateno_father=@certificateno_father,certissuedate_father=@certissuedate_father,certissuedesig_father=@certissuedesig_father,
                        certissuedist_father=@certissuedist_father,certissuestate_father=@certissuestate_father,certfatherormother=@certfatherormother,certissuedist_father_notdelhi=@certissuedist_father_notdelhi where fcdid=@fcdid";

        SqlParameter[] param = new SqlParameter[8];

        param[j] = new SqlParameter("@fcdid", SqlDbType.Int);
        param[j].Value = fcdid;
        j++;

        param[j] = new SqlParameter("@certificateno_father", SqlDbType.VarChar, 50);
        if (certificateno_father == "" || certificateno_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certificateno_father;
        }
        j++;
        param[j] = new SqlParameter("@certissuedate_father", SqlDbType.Date);
        if (certissuedate_father == "" || certissuedate_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(certissuedate_father);
        }
        j++;
        param[j] = new SqlParameter("@certissuedesig_father", SqlDbType.VarChar, 50);
        if (certissuedesig_father == "" || certissuedesig_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedesig_father;
        }
        j++;
        param[j] = new SqlParameter("@certissuedist_father", SqlDbType.VarChar, 50);
        if (certissuedist_father == "" || certissuedist_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedist_father;
        }
        j++;

        param[j] = new SqlParameter("@certissuestate_father", SqlDbType.VarChar, 50);
        if (certissuestate_father == "" || certissuestate_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuestate_father;
        }
        j++;

        param[j] = new SqlParameter("@certfatherormother", SqlDbType.Char, 1);
        if (certfatherormother == "" || certfatherormother == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certfatherormother;
        }
        j++;
        param[j] = new SqlParameter("@certissuedist_father_notdelhi", SqlDbType.VarChar, 50);
        if (certissuedist_father_notdelhi == "" || certissuedist_father_notdelhi == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedist_father_notdelhi;
        }

        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public DataTable checkedqualidetails(string edid)
    {
        try
        {
            string str = @"SELECT  convert(varchar, finalresultdate,103) as finalresultdate, govtorpvt,instname,
                         edqid,docproofpvtinst FROM edqualidetails  where edid=@edid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@edid", SqlDbType.Int);
            param[0].Value = edid;

            dt = da.GetDataTableQry(str, param);


            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int update_ApplicationFinallySubmitted(string applid)
    {
        str = @"update jobapplication set final='Y' where applid=@applid";


        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);
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
    public string getjid(string applid)
    {
        try
        {
            string str = @"SELECT jid FROM jobapplication  where applid=@applid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@applid", SqlDbType.Int);
            param[0].Value = applid;

            dt = da.GetDataTableQry(str, param);
            string jid = dt.Rows[0]["jid"].ToString();

            return jid;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int insertOBCfathercertdetails(int applid, string certificateno_father, string certissuedate_father, string certissuedesig_father, string certissuestate_father, string IP, string certfatherormother)
    {
        int i = 0;
        int j = 0;

        string str = @"insert into obcfathercertdetails (applid,certificateno_father,certissuedate_father,certissuedesig_father,certissuestate_father,certfatherormother,IP) values (@applid,@certificateno_father,@certissuedate_father,@certissuedesig_father,@certissuestate_father,@certfatherormother,@IP)";

        SqlParameter[] param = new SqlParameter[7];

        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;

        param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
        if (IP == "" || IP == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = IP;
        }
        j++;
        param[j] = new SqlParameter("@certificateno_father", SqlDbType.VarChar, 50);
        if (certificateno_father == "" || certificateno_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certificateno_father;
        }
        j++;
        param[j] = new SqlParameter("@certissuedate_father", SqlDbType.Date);
        if (certissuedate_father == "" || certissuedate_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(certissuedate_father);
        }
        j++;
        param[j] = new SqlParameter("@certissuedesig_father", SqlDbType.VarChar, 50);
        if (certissuedesig_father == "" || certissuedesig_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedesig_father;
        }
        j++;


        param[j] = new SqlParameter("@certissuestate_father", SqlDbType.Int);
        if (certissuestate_father == "" || certissuestate_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuestate_father;
        }
        j++;
        param[j] = new SqlParameter("@certfatherormother", SqlDbType.Char, 1);
        if (certfatherormother == "" || certfatherormother == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certfatherormother;
        }
        j++;


        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int updateOBCfathercertdetails(int applid, string certificateno_father, string certissuedate_father, string certissuedesig_father, string certissuestate_father, string certfatherormother)
    {
        int i = 0;
        int j = 0;

        string str = @"update obcfathercertdetails set certificateno_father=@certificateno_father,certissuedate_father=@certissuedate_father,certissuedesig_father=@certissuedesig_father,
                        certissuestate_father=@certissuestate_father,certfatherormother=@certfatherormother where applid=@applid";

        SqlParameter[] param = new SqlParameter[6];

        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;

        param[j] = new SqlParameter("@certificateno_father", SqlDbType.VarChar, 50);
        if (certificateno_father == "" || certificateno_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certificateno_father;
        }
        j++;
        param[j] = new SqlParameter("@certissuedate_father", SqlDbType.Date);
        if (certissuedate_father == "" || certissuedate_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(certissuedate_father);
        }
        j++;
        param[j] = new SqlParameter("@certissuedesig_father", SqlDbType.VarChar, 50);
        if (certissuedesig_father == "" || certissuedesig_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuedesig_father;
        }
        j++;


        param[j] = new SqlParameter("@certissuestate_father", SqlDbType.VarChar, 50);
        if (certissuestate_father == "" || certissuestate_father == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certissuestate_father;
        }
        j++;

        param[j] = new SqlParameter("@certfatherormother", SqlDbType.Char, 1);
        if (certfatherormother == "" || certfatherormother == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = certfatherormother;
        }


        try
        {
            i = da.ExecuteParameterizedQuery(str, param);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    public int delete_obcfathercertdetails(int applid)
    {
        string str = "delete from obcfathercertdetails where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@applid", SqlDbType.Int);

        param[j].Value = applid;
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
    public DataTable GetfatherOBCDetails(int applid)
    {
        str = @"SELECT fobcid, applid, certificateno_father,convert(varchar,certissuedate_father,103) as certissuedate_father, certissuedesig_father, certissuestate_father, certfatherormother, ip
                FROM obcfathercertdetails WHERE applid = @applid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;
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

    public long UpdateFinalSumitwithdummy_noTransaction(string applid, string jid, string feereq)
    {

        long Result = 0;
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
                    string str = "";

                    str = @"update jobapplication set final='Y' where applid=@applid";


                    SqlParameter[] param = new SqlParameter[1];

                    param[0] = new SqlParameter("@applid", SqlDbType.Int);
                    param[0].Value = Int32.Parse(applid);


                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param != null)
                    {
                        foreach (SqlParameter param1 in param)
                        {
                            command.Parameters.Add(param1);
                        }

                    }

                    command.ExecuteNonQuery();

                    //second query

                    if (feereq == "N")
                    {
                        SqlParameter[] arraParams = new SqlParameter[3];
                        int j = 0;
                        arraParams[j] = new SqlParameter("@APPLID", SqlDbType.Int);
                        if (applid == "")
                        {
                            arraParams[j].Value = 0;
                        }
                        else
                        {
                            arraParams[j].Value = Int32.Parse(applid);
                        }
                        j++;
                        arraParams[j] = new SqlParameter("@jid", SqlDbType.Int, 4);
                        if (jid == "")
                        {
                            arraParams[j].Value = 0;
                        }
                        else
                        {
                            arraParams[j].Value = Int32.Parse(jid);
                        }
                        j++;
                        arraParams[j] = new SqlParameter("@innoOut", SqlDbType.Int);
                        arraParams[j].Direction = ParameterDirection.Output;

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "JobApplication_GenerateDummyNo";
                        command.CommandTimeout = 2000;
                        command.Parameters.Clear();
                        if (arraParams != null)
                        {
                            foreach (SqlParameter loopParam in arraParams)
                            {
                                command.Parameters.Add(loopParam);
                            }

                        }
                        // int Result;
                        command.ExecuteNonQuery();
                        Result = long.Parse(arraParams[j].Value.ToString());
                        //return Result;



                    }
                    else
                    {
                        Result = 1;
                    }
                    //command.ExecuteNonQuery();
                    transaction.Commit();


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Result = 0;
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        return Result;
    }

    public long InsertFeedetailwithdummy_noTransaction(string applid, string orderno, string amount, string transmode, string trandate, string feerecd, string jid, string feetype)
    {

        long Result = 0;
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
                    string str = "";
                    int j = 0;
                    str = @"insert into feedetails (jid,applid,jrnlno,feerecd,transmode,amount,trandate,feetype) 
         values((select jid from jobapplication where applid=@applid),@applid,@orderno,@feerecd,@transmode,@amount,@trandate,@feetype)";

                    SqlParameter[] param = new SqlParameter[7];

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

                    param[j] = new SqlParameter("@orderno", SqlDbType.VarChar);
                    if (orderno == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = orderno;
                    }
                    j++;
                    param[j] = new SqlParameter("@feerecd", SqlDbType.Char, 1);
                    if (feerecd == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = feerecd;
                    }
                    j++;
                    param[j] = new SqlParameter("@amount", SqlDbType.Decimal);
                    if (amount == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = amount;
                    }
                    j++;

                    param[j] = new SqlParameter("@trandate", SqlDbType.DateTime);
                    if (trandate == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = trandate;
                    }
                    j++;

                    param[j] = new SqlParameter("@transmode", SqlDbType.VarChar, 20);
                    if (transmode == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = transmode;
                    }
                    j++;

                    param[j] = new SqlParameter("@feetype", SqlDbType.VarChar);
                    if (feetype == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = feetype;
                    }

                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param != null)
                    {
                        foreach (SqlParameter param1 in param)
                        {
                            command.Parameters.Add(param1);
                        }

                    }

                    command.ExecuteNonQuery();
                    //  Result = 1;
                    //second query


                    SqlParameter[] arraParams = new SqlParameter[3];
                    int k = 0;
                    arraParams[k] = new SqlParameter("@APPLID", SqlDbType.Int);
                    if (applid == "")
                    {
                        arraParams[k].Value = 0;
                    }
                    else
                    {
                        arraParams[k].Value = Int32.Parse(applid);
                    }
                    k++;
                    arraParams[k] = new SqlParameter("@jid", SqlDbType.Int, 4);
                    if (jid == "")
                    {
                        arraParams[k].Value = 0;
                    }
                    else
                    {
                        arraParams[k].Value = Int32.Parse(jid);
                    }
                    k++;
                    arraParams[k] = new SqlParameter("@innoOut", SqlDbType.Int);
                    arraParams[k].Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "JobApplication_GenerateDummyNo";
                    command.CommandTimeout = 2000;
                    command.Parameters.Clear();
                    if (arraParams != null)
                    {
                        foreach (SqlParameter loopParam in arraParams)
                        {
                            command.Parameters.Add(loopParam);
                        }

                    }
                    // int Result;
                    command.ExecuteNonQuery();
                    Result = long.Parse(arraParams[k].Value.ToString());
                    //  return Result;




                    //command.ExecuteNonQuery();
                    transaction.Commit();


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        return Result;
    }
    public long updateFeedetailwithdummy_noTransaction(string applid, string orderno, string amount, string transmode, string trandate, string feerecd, string jid)
    {

        long Result = 0;
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
                    string str = "";
                    int j = 0;
                    str = @"update feedetails set jid=(select jid from jobapplication where applid=@applid),jrnlno=@orderno,feerecd=@feerecd,transmode=@transmode,
                       amount=@amount,trandate=@trandate where applid=@applid";

                    SqlParameter[] param = new SqlParameter[6];

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

                    param[j] = new SqlParameter("@orderno", SqlDbType.VarChar);
                    if (orderno == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = orderno;
                    }
                    j++;
                    param[j] = new SqlParameter("@feerecd", SqlDbType.Char, 1);
                    if (feerecd == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = feerecd;
                    }
                    j++;
                    param[j] = new SqlParameter("@amount", SqlDbType.Decimal);
                    if (amount == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = amount;
                    }
                    j++;

                    param[j] = new SqlParameter("@trandate", SqlDbType.DateTime);
                    if (trandate == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = trandate;
                    }
                    j++;

                    param[j] = new SqlParameter("@transmode", SqlDbType.VarChar, 20);
                    if (transmode == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = transmode;
                    }


                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param != null)
                    {
                        foreach (SqlParameter param1 in param)
                        {
                            command.Parameters.Add(param1);
                        }

                    }

                    command.ExecuteNonQuery();
                    //  Result = 1;
                    //second query


                    SqlParameter[] arraParams = new SqlParameter[3];
                    int k = 0;
                    arraParams[k] = new SqlParameter("@APPLID", SqlDbType.Int);
                    if (applid == "")
                    {
                        arraParams[k].Value = 0;
                    }
                    else
                    {
                        arraParams[k].Value = Int32.Parse(applid);
                    }
                    k++;
                    arraParams[k] = new SqlParameter("@jid", SqlDbType.Int, 4);
                    if (jid == "")
                    {
                        arraParams[k].Value = 0;
                    }
                    else
                    {
                        arraParams[k].Value = Int32.Parse(jid);
                    }
                    k++;
                    arraParams[k] = new SqlParameter("@innoOut", SqlDbType.Int);
                    arraParams[k].Direction = ParameterDirection.Output;

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "JobApplication_GenerateDummyNo";
                    command.CommandTimeout = 2000;
                    command.Parameters.Clear();
                    if (arraParams != null)
                    {
                        foreach (SqlParameter loopParam in arraParams)
                        {
                            command.Parameters.Add(loopParam);
                        }

                    }
                    // int Result;
                    Result = command.ExecuteNonQuery();
                    Result = long.Parse(arraParams[k].Value.ToString());
                    //  return Result;




                    //command.ExecuteNonQuery();
                    transaction.Commit();


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        return Result;
    }

    public int updatemobile_jobapplication(string rid, string mobile)
    {
        string str = "update jobapplication set mobileno=@mobile where regno=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@mobile", SqlDbType.NVarChar, 12);
        param[1].Value = mobile;

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

    public int updateemail_jobapplication(string rid, string email)
    {
        string str = "update jobapplication set email=@email where regno=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;

        param[1] = new SqlParameter("@email", SqlDbType.VarChar, 50);
        param[1].Value = email;
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

    public DataTable CheckEssentialQuali(int applid, int jid, string groupno)
    {
        //        str = @"select je.qid from JobEducation je left outer join tbledu_TRN tr on tr.uid = je.qid and tr.qtype='E'
        //			    where je.qid is null and applid = @applid";
        str = @"select tr.* from tbledu_TRN tr inner join job_advt ja on ja.reqid=tr.reqid and ja.jid=@jid and (tr.qtype='E' or tr.qtype='G')
                left outer join JobEducation je on tr.uid = je.qid  and je.applid = @applid where je.qid is null ";
        if (groupno != "")
        {
            str += " and tr.groupno=@groupno ";
        }

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[0].Value = applid;
        param[1] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[1].Value = jid;
        param[2] = new SqlParameter("@groupno", SqlDbType.Int, 4);
        if (groupno != "")
        {
            param[2].Value = groupno;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
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

    public bool CheckEssentialQualiNew(int applid, int jid, string deptcode)//rohitxd 22/11/2023
    {
        string q = string.Empty;
        if (deptcode == "COMBD")
        {
            q = @"select * from JobEducation je
                    inner join tbledu_TRN  tr on tr.uid = je.qid
                    where tr.reqid in (select DeptReqId from CombinedEntry where CombdReqid = (select reqid from Job_Advt where jid= @jid)) and
                                             applid = @applid";
        }
        else
        {
            q = @"select * from jobeducation  je inner join tbledu_TRN tr on tr.uid = je.qid 
                        inner join job_advt ja on ja.reqid = tr.reqid
                        where ja.jid = @jid and (tr.qtype = 'E' or tr.qtype = 'G')
                        and
                        applid = @applid";
        }


        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = applid;
        param[1] = new SqlParameter("@jid", SqlDbType.Int);
        param[1].Value = jid;

        try
        {
            dt = da.GetDataTableQry(q, param);
            return dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int get_reqid(string jid)
    {
        str = @"select reqid from job_advt where jid = @jid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[j].Value = jid;
        try
        {
            dt = da.GetDataTableQry(str, param);
            int reqid = Convert.ToInt32(dt.Rows[0]["reqid"].ToString());
            return reqid;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public string get_edmid_pvtinst(string jid)
    {
        str = @"select edmid from EdossierMaster where jid = @jid and CCategory='D'";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[j].Value = jid;
        try
        {
            dt = da.GetDataTableQry(str, param);
            string edmid = dt.Rows[0]["edmid"].ToString();
            return edmid;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataTable CheckExpDetails(string edid, string currentorgname, string dateofappoint, string subcatcode)
    {

        string str = @"SELECT currentorgname, orgaddress, Wcentralorstate, orgstatename, orgministryname, isAutonomous, 
                      convert(varchar, dateofappoint,103) as dateofappoint, currentdesig,edid,id
                        from edcatdetails  
						 where edid=@edid and subcatcode=@subcatcode and currentorgname=@currentorgname and dateofappoint=@dateofappoint ";

        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@edid", SqlDbType.Int);
        if (edid != "")
        {
            param[0].Value = edid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@currentorgname", SqlDbType.VarChar, 100);
        if (currentorgname != "")
        {
            param[1].Value = currentorgname;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }
        param[2] = new SqlParameter("@dateofappoint", SqlDbType.Date, 10);
        if (dateofappoint != "")
        {
            param[2].Value = dateofappoint;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        param[3] = new SqlParameter("@subcatcode", SqlDbType.VarChar, 5);
        if (subcatcode != "")
        {
            param[3].Value = subcatcode;
        }
        else
        {
            param[3].Value = System.DBNull.Value;
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

    public int Insert_JobapplicationTransaction(int jid, int applno, string Name, string FatherName, string MotherName, string PresentAdd, string ParmaAdd, string PresentPIN, string PermanenetPIN,
        string Mobile, string Email, string Nationality, string Gender, string MaritalStatus, string DOB, string Category, List<string> Subcategory, string PHsubCate, string GovtDateJoin,
        string NonCreamyLayerCerNo, string NonCreamyLayerDate, int CastCertApplyState, string ExService, string exServiceFromDate, string ExServiceToDate, string Debard, string debarredDate,
        string debarredYear, string IP, string regno, string feereq, string CertUssueAuth, string contract_duration, string ex_serv_duration, string ph_visual, string ph_hearing, string ph_ortho,
        string physic, string wscribe, string OBCRegion, string spousename, string PHIssuingauthority, string PHcertifNo, string PhcertIssuedate, Byte[] PHFile, int PHIssueState)
    {

        int applid = 0, JScatID = 0, JSScatID = 0;
        SqlCommand command = new SqlCommand();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString_RO"]);
        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
            connection.Open();
            //SqlCommand command = connection.CreateCommand();
            //SqlTransaction transaction = null;


            var RegModel = this.GetRegDetail(regno);

            if (RegModel == null)
            {
                throw new Exception("Model in Null registrationmodel");
            }

            if (RegModel.MobileNumber.Trim() != Mobile.Trim())
            {
                throw new Exception(String.Format("Registration detail mismatch for rid {0} jid {1}", regno, jid));
            }

            using (SqlTransaction transaction = connection.BeginTransaction())
            {

                try
                {
                    //connection.Open();
                    //transaction = connection.BeginTransaction();
                    //command.Transaction = transaction;
                    command.Connection = connection;
                    command.Transaction = transaction;

                    int j = 0;

                    string insrtqry = @"insert into JobApplication (jid,appno,name,fname,mothername,address_per,address,PIN_per,PIN,mobileno,email,nationality,gender,maritalstatus,birthdt,category,GovtJoinDt,CLCNo,CLCDate,CastCerApplyState,exserviceman,ExFromDt,ExToDt,DebardDetails,DebardDt,DebardYr,IP,regno,feereq,CastCertIssueAuth,ContractDuration,ExServiceDuration,physical_relax,wscribe,OBCRegion,spousename) 
         values(@jid,@applno,@name,@fname,@mothername,@address_per,@address,@PIN_per,@PIN,@mobileno,@email,@nationality,@gender,@maritalstatus,@birthdt,@category,@GovtJoinDt,@CLCNo,@CLCDate,@CastCerApplyState,@exserviceman,@ExFromDt,@ExToDt,@DebardDetails,@DebardDt,@DebardYr,@IP,@regno,@feereq,@CertUssueAuth,@ContractDuration,@exServiceDuration,@physic,@wscribe,@OBCRegion,@spousename) Select SCOPE_IDENTITY() ";

                    SqlParameter[] param = new SqlParameter[36];

                    param[j] = new SqlParameter("@jid", SqlDbType.SmallInt);
                    param[j].Value = jid;
                    j++;
                    param[j] = new SqlParameter("@applno", SqlDbType.Int);
                    param[j].Value = applno;
                    j++;


                    param[j] = new SqlParameter("@name", SqlDbType.NVarChar);
                    if (Name == "" || Name == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Name;
                    }
                    j++;

                    param[j] = new SqlParameter("@fname", SqlDbType.NVarChar);
                    if (FatherName == "" || FatherName == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = FatherName;
                    }
                    j++;
                    param[j] = new SqlParameter("@mothername", SqlDbType.NVarChar);
                    if (MotherName == "" || MotherName == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = MotherName;
                    }
                    j++;

                    param[j] = new SqlParameter("@address_per", SqlDbType.NVarChar);
                    if (PresentAdd == "" || PresentAdd == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = PresentAdd;
                    }
                    j++;

                    param[j] = new SqlParameter("@address", SqlDbType.NVarChar);
                    if (ParmaAdd == "" || ParmaAdd == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = ParmaAdd;
                    }
                    j++;

                    param[j] = new SqlParameter("@PIN_per", SqlDbType.VarChar);
                    if (PresentPIN == "" || PresentPIN == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = PresentPIN;
                    }
                    j++;

                    param[j] = new SqlParameter("@PIN", SqlDbType.VarChar);
                    if (PermanenetPIN == "" || PermanenetPIN == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = PermanenetPIN;
                    }
                    j++;

                    param[j] = new SqlParameter("@mobileno", SqlDbType.NVarChar);
                    if (Mobile == "" || Mobile == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Mobile;
                    }
                    j++;
                    param[j] = new SqlParameter("@email", SqlDbType.VarChar);
                    if (Email == "" || Email == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Email;
                    }
                    j++;

                    param[j] = new SqlParameter("@nationality", SqlDbType.NVarChar);
                    if (Nationality == "" || Nationality == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Nationality;
                    }
                    j++;
                    param[j] = new SqlParameter("@gender", SqlDbType.Char);
                    if (Gender == "" || Gender == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Gender;
                    }
                    j++;

                    param[j] = new SqlParameter("@maritalstatus", SqlDbType.Char);
                    if (MaritalStatus == "" || MaritalStatus == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = MaritalStatus;
                    }
                    j++;

                    param[j] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
                    if (DOB == "" || DOB == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(DOB);
                    }
                    j++;

                    param[j] = new SqlParameter("@category", SqlDbType.VarChar);
                    if (Category == "" || Category == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Category;
                    }
                    j++;


                    param[j] = new SqlParameter("@GovtJoinDt", SqlDbType.DateTime, 8);
                    if (GovtDateJoin == "" || GovtDateJoin == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(GovtDateJoin);
                    }
                    j++;

                    param[j] = new SqlParameter("@CLCNo", SqlDbType.NVarChar);
                    if (NonCreamyLayerCerNo == "" || NonCreamyLayerCerNo == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = NonCreamyLayerCerNo;
                    }
                    j++;

                    param[j] = new SqlParameter("@CLCDate", SqlDbType.DateTime);
                    if (NonCreamyLayerDate == "" || NonCreamyLayerDate == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(NonCreamyLayerDate);
                    }
                    j++;

                    param[j] = new SqlParameter("@CastCerApplyState", SqlDbType.Int);
                    if (CastCertApplyState.ToString() == "" || Category == "UR")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = CastCertApplyState;
                    }
                    j++;

                    param[j] = new SqlParameter("@exserviceman", SqlDbType.VarChar);
                    if (ExService.ToString() == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = ExService;
                    }
                    j++;
                    param[j] = new SqlParameter("@ExFromDt", SqlDbType.DateTime);
                    if (exServiceFromDate == "" || exServiceFromDate == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(exServiceFromDate);
                    }
                    j++;
                    param[j] = new SqlParameter("@ExToDt", SqlDbType.DateTime);
                    if (ExServiceToDate == "" || ExServiceToDate == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(ExServiceToDate);
                    }
                    j++;
                    param[j] = new SqlParameter("@DebardDetails", SqlDbType.VarChar);
                    if (Debard == "" || Debard == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Debard;
                    }
                    j++;
                    param[j] = new SqlParameter("@DebardDt", SqlDbType.DateTime);
                    if (debarredDate == "" || debarredDate == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(debarredDate);
                    }
                    j++;

                    param[j] = new SqlParameter("@DebardYr", SqlDbType.Int);
                    if (Convert.ToString(debarredYear) == "")
                    {
                        param[j].Value = 0;
                    }
                    else
                    {
                        param[j].Value = Int32.Parse(debarredYear);
                    }
                    j++;
                    param[j] = new SqlParameter("@IP", SqlDbType.VarChar);
                    if (IP == "" || IP == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = IP;
                    }
                    j++;
                    param[j] = new SqlParameter("@regno", SqlDbType.VarChar);
                    if (regno == "" || regno == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = regno;
                    }
                    j++;
                    param[j] = new SqlParameter("@feereq", SqlDbType.VarChar);
                    if (feereq == "" || feereq == null)
                    {
                        param[j].Value = "Y";
                    }
                    else
                    {
                        param[j].Value = feereq;
                    }
                    j++;
                    param[j] = new SqlParameter("@CertUssueAuth", SqlDbType.VarChar);
                    if (CertUssueAuth == "" || CertUssueAuth == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = CertUssueAuth;
                    }
                    j++;
                    param[j] = new SqlParameter("@ContractDuration", SqlDbType.VarChar);
                    if (contract_duration == "" || contract_duration == null)
                    {
                        param[j].Value = 0;
                    }
                    else
                    {
                        param[j].Value = Int32.Parse(contract_duration);
                    }
                    j++;
                    param[j] = new SqlParameter("@exServiceDuration", SqlDbType.VarChar);
                    if (ex_serv_duration == "" || ex_serv_duration == null)
                    {
                        param[j].Value = 0;
                    }
                    else
                    {
                        param[j].Value = Int32.Parse(ex_serv_duration);
                    }

                    j++;

                    param[j] = new SqlParameter("@physic", SqlDbType.VarChar);
                    if (physic == "" || physic == null)
                    {
                        param[j].Value = "";
                    }
                    else
                    {
                        param[j].Value = physic;
                    }
                    j++;
                    param[j] = new SqlParameter("@wscribe", SqlDbType.VarChar);
                    if (wscribe == "" || wscribe == null)
                    {
                        param[j].Value = "";
                    }
                    else
                    {
                        param[j].Value = wscribe;
                    }
                    j++;
                    param[j] = new SqlParameter("@OBCRegion", SqlDbType.Char, 1);
                    if (OBCRegion == "" || OBCRegion == null || Category != "OBC")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = OBCRegion;
                    }
                    j++;
                    param[j] = new SqlParameter("@spousename", SqlDbType.VarChar);
                    if (spousename == "" || spousename == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = spousename;
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
                    applid = Convert.ToInt32(command.ExecuteScalar());
                    //command.ExecuteNonQuery();
                    //start Insert values in table  PhCertifiacteDetails for 11 coloumns
                    //string strPHC = " insert into PhCertifiacteDetails (applid,SubCat_code,edate,userid,ipaddress) values (@applid,@SubCat_code,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";
                    if (Subcategory.Contains("PH"))
                    {
                        string strPHC = @" INSERT INTO PhCertifiacteDetail (applid,jid,RegNo,PhSubcat ,PhCertiNo ,PhCertIssueAuth ,PhCertIssueDate,PhIssueState,PhCertificateFile,IPAddress,Entrydate)VALUES
           (@applid,@jid, @RegNo,@PhSubcat,@PhCertiNo,@PhCertIssueAuth,@PhCertIssueDate,@PhIssueState, @PhCertificateFile, @IPAddress, getdate())";
                        SqlParameter[] paramPHC = new SqlParameter[10];


                        paramPHC[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                        paramPHC[0].Value = applid;

                        paramPHC[1] = new SqlParameter("@jid", SqlDbType.Int, 10);
                        paramPHC[1].Value = jid;


                        paramPHC[2] = new SqlParameter("@RegNo", SqlDbType.VarChar, 50);
                        paramPHC[2].Value = regno;

                        paramPHC[3] = new SqlParameter("@PhSubcat", SqlDbType.VarChar, 10);
                        paramPHC[3].Value = "PH";


                        paramPHC[4] = new SqlParameter("@PhCertiNo", SqlDbType.VarChar, 20);
                        paramPHC[4].Value = PHcertifNo;

                        paramPHC[5] = new SqlParameter("@PhCertIssueAuth", SqlDbType.VarChar, 50);
                        paramPHC[5].Value = PHIssuingauthority;

                        paramPHC[6] = new SqlParameter("@PhCertIssueDate", SqlDbType.DateTime, 8);
                        paramPHC[6].Value = Utility.formatDate(PhcertIssuedate);

                        paramPHC[7] = new SqlParameter("@PhIssueState", SqlDbType.Int, 4);
                        if (PHIssueState == 0)
                        {
                            paramPHC[7].Value = System.DBNull.Value;
                        }
                        else
                        {
                            paramPHC[7].Value = PHIssueState;
                        }


                        paramPHC[8] = new SqlParameter("@PhCertificateFile", SqlDbType.Image, PHFile.Length);
                        paramPHC[8].Value = PHFile;

                        paramPHC[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
                        paramPHC[9].Value = IP;


                        command.CommandType = CommandType.Text;
                        command.CommandText = strPHC;
                        command.Parameters.Clear();


                        if (paramPHC != null)
                        {
                            foreach (SqlParameter param3PHC in paramPHC)
                            {
                                command.Parameters.Add(param3PHC);
                            }

                        }
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    //end Insert values in table  PhCertifiacteDetails
                    string[] a = { "," };
                    if (Subcategory.Count > 0)
                    {

                        foreach (var SubCat_code in Subcategory)
                        {
                            string strscat = " insert into JapplicantScat (applid,SubCat_code,edate,userid,ipaddress) values (@applid,@SubCat_code,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";

                            SqlParameter[] param2 = new SqlParameter[4];


                            param2[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                            param2[0].Value = applid;

                            param2[1] = new SqlParameter("@SubCat_code", SqlDbType.VarChar, 4);
                            param2[1].Value = SubCat_code;

                            param2[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                            param2[2].Value = regno;

                            param2[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                            param2[3].Value = IP;

                            command.CommandType = CommandType.Text;
                            command.CommandText = strscat;
                            command.Parameters.Clear();


                            if (param2 != null)
                            {
                                foreach (SqlParameter param3 in param2)
                                {
                                    command.Parameters.Add(param3);
                                }

                            }
                            command.Transaction = transaction;
                            JScatID = Convert.ToInt32(command.ExecuteScalar());
                            if (PHsubCate != "" && SubCat_code == "PH")
                            {
                                string SScatid = "";
                                string[] subsubcat = PHsubCate.Split(a, StringSplitOptions.RemoveEmptyEntries);
                                for (int k = 0; k < subsubcat.Length; k++)
                                {
                                    SScatid = subsubcat[k];
                                    str = " insert into JapplicantSScat (JScatID,SScatid,edate,userid,ipaddress) values (@JScatID,@SScatid,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";

                                    SqlParameter[] param4 = new SqlParameter[4];


                                    param4[0] = new SqlParameter("@JScatID", SqlDbType.Int, 4);
                                    param4[0].Value = JScatID;

                                    param4[1] = new SqlParameter("@SScatid", SqlDbType.Int, 4);
                                    param4[1].Value = SScatid;

                                    param4[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                                    param4[2].Value = regno;

                                    param4[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                                    param4[3].Value = IP;

                                    command.CommandType = CommandType.Text;
                                    command.CommandText = str;
                                    command.Parameters.Clear();


                                    if (param4 != null)
                                    {
                                        foreach (SqlParameter param5 in param4)
                                        {
                                            command.Parameters.Add(param5);
                                        }

                                    }
                                    command.Transaction = transaction;
                                    JSScatID = Convert.ToInt32(command.ExecuteScalar());

                                    string SSScatid = "";
                                    if (SScatid == "1" && ph_visual != "")
                                    {
                                        SSScatid = ph_visual;
                                    }
                                    else if (SScatid == "2" && ph_hearing != "")
                                    {
                                        SSScatid = ph_hearing;
                                    }
                                    else if (SScatid == "3" && ph_ortho != "")
                                    {
                                        SSScatid = ph_ortho;
                                    }
                                    if (SSScatid != "")
                                    {
                                        string str1 = " insert into JapplicantSSScat (JSScatID,SSScatid,edate,userid,ipaddress) values (@JSScatID,@SSScatid,getdate(),@userid,@ipaddress)";

                                        SqlParameter[] param6 = new SqlParameter[4];


                                        param6[0] = new SqlParameter("@JSScatID", SqlDbType.Int, 4);
                                        param6[0].Value = JSScatID;

                                        param6[1] = new SqlParameter("@SSScatid", SqlDbType.Int, 4);
                                        param6[1].Value = SSScatid;

                                        param6[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                                        param6[2].Value = regno;

                                        param6[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                                        param6[3].Value = IP;

                                        command.CommandType = CommandType.Text;
                                        command.CommandText = str1;
                                        command.Parameters.Clear();


                                        if (param6 != null)
                                        {
                                            foreach (SqlParameter param7 in param6)
                                            {
                                                command.Parameters.Add(param7);
                                            }

                                        }
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                    }


                    //command.ExecuteNonQuery();
                    transaction.Commit();
                    //throw new Exception();
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
        return applid;
    }

    public int Update_JobapplicationTransaction(int applid, string Name, string FatherName, string MotherName, string PresentAdd, string ParmaAdd, string PresentPIN, string PermanenetPIN, string Mobile,
        string Email, string Nationality, string Gender, string MaritalStatus, string DOB, string Category, List<string> Subcategory, string PHsubCate, string GovtDateJoin, string NonCreamyLayerCerNo,
        string NonCreamyLayerDate, int CastCertApplyState, string ExService, string exServiceFromDate, string ExServiceToDate, string Debard, string debarredDate, string debarredYear, string feereq,
        string CertIssueAuth, string contract_duration, string ex_serv_duration, string ph_visual, string ph_hearing, string ph_ortho, string physic, string wscribe, string OBCRegion, string regno,
        string IP, string spousename, string PHIssuingauthority, string PHcertifNo, string PhcertIssuedate, Byte[] PHFile, int PHIssueState)
    {

        var isUploaded = this.FetchPHCertificateDetail(applid.ToString());
        int jid = this.FetchJIDFromApplid(applid);

        int JScatID = 0, JSScatID = 0;
        SqlCommand command = new SqlCommand();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString_RO"]);
        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
            connection.Open();
            //SqlCommand command = connection.CreateCommand();
            //SqlTransaction transaction = null;


            var RegModel = this.GetRegDetail(regno);

            if (RegModel == null)
            {
                throw new Exception("Model in Null registrationmodel");
            }

            if (RegModel.MobileNumber.Trim() != Mobile.Trim())
            {
                throw new Exception(String.Format("Registration detail mismatch for rid {0} applid {1}", regno, applid));
            }


            using (SqlTransaction transaction = connection.BeginTransaction())
            {

                try
                {
                    //connection.Open();
                    //transaction = connection.BeginTransaction();
                    //command.Transaction = transaction;
                    command.Connection = connection;
                    command.Transaction = transaction;

                    string str = @"update JobApplication set name=@name,fname=@fname,mothername=@mothername,address_per=@address_per,address=@address,PIN_per=@PIN_per,PIN=@PIN,
                                   mobileno=@mobileno, email=@email,nationality=@nationality,gender=@gender,maritalstatus=@maritalstatus,birthdt=@birthdt,category=@category,
                                   GovtJoinDt=@GovtJoinDt,CLCNo=@CLCNo,CLCDate=@CLCDate,CastCerApplyState=@CastCerApplyState,exserviceman=@exserviceman,ExFromDt=@ExFromDt,
                                   ExToDt=@ExToDt,DebardDetails=@DebardDetails,DebardDt=@DebardDt,DebardYr=@DebardYr,feereq=@feereq,CastCertIssueAuth =@CertIssueAuth,
                                  ContractDuration=@ContractDuration,ExServiceDuration=@exServiceDuration,physical_relax=@physic,wscribe=@wscribe,OBCRegion=@OBCRegion,spousename=@spousename
                                  where applid=@applid ";

                    SqlParameter[] param = new SqlParameter[33];
                    int j = 0;

                    param[j] = new SqlParameter("@applid", SqlDbType.Int);
                    param[j].Value = applid;
                    j++;


                    param[j] = new SqlParameter("@name", SqlDbType.NVarChar);
                    if (Name == "" || Name == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Name;
                    }
                    j++;

                    param[j] = new SqlParameter("@fname", SqlDbType.NVarChar);
                    if (FatherName == "" || FatherName == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = FatherName;
                    }
                    j++;
                    param[j] = new SqlParameter("@mothername", SqlDbType.NVarChar);
                    if (MotherName == "" || MotherName == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = MotherName;
                    }
                    j++;
                    param[j] = new SqlParameter("@address_per", SqlDbType.NVarChar);
                    if (PresentAdd == "" || PresentAdd == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = PresentAdd;
                    }
                    j++;
                    param[j] = new SqlParameter("@address", SqlDbType.NVarChar);
                    if (ParmaAdd == "" || ParmaAdd == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = ParmaAdd;
                    }
                    j++;


                    param[j] = new SqlParameter("@PIN_per", SqlDbType.VarChar);
                    if (PresentPIN == "" || PresentPIN == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = PresentPIN;
                    }
                    j++;
                    param[j] = new SqlParameter("@PIN", SqlDbType.VarChar);
                    if (PermanenetPIN == "" || PermanenetPIN == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = PermanenetPIN;
                    }
                    j++;
                    param[j] = new SqlParameter("@mobileno", SqlDbType.NVarChar);
                    if (Mobile == "" || Mobile == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Mobile;
                    }
                    j++;
                    param[j] = new SqlParameter("@email", SqlDbType.VarChar);
                    if (Email == "" || Email == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Email;
                    }

                    j++;
                    param[j] = new SqlParameter("@nationality", SqlDbType.NVarChar);
                    if (Nationality == "" || Nationality == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Nationality;
                    }
                    j++;
                    param[j] = new SqlParameter("@gender", SqlDbType.Char);
                    if (Gender == "" || Gender == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Gender;
                    }
                    j++;
                    param[j] = new SqlParameter("@maritalstatus", SqlDbType.Char);
                    if (MaritalStatus == "" || MaritalStatus == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = MaritalStatus;
                    }
                    j++;
                    param[j] = new SqlParameter("@birthdt", SqlDbType.DateTime, 8);
                    if (DOB == "" || DOB == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(DOB);
                    }
                    j++;
                    param[j] = new SqlParameter("@category", SqlDbType.VarChar);
                    if (Category == "" || Category == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Category;
                    }
                    j++;

                    param[j] = new SqlParameter("@GovtJoinDt", SqlDbType.DateTime, 8);
                    if (GovtDateJoin == "" || GovtDateJoin == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(GovtDateJoin);
                    }
                    j++;
                    param[j] = new SqlParameter("@CLCNo", SqlDbType.NVarChar);
                    if (NonCreamyLayerCerNo == "" || NonCreamyLayerCerNo == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = NonCreamyLayerCerNo;
                    }
                    j++;
                    param[j] = new SqlParameter("@CLCDate", SqlDbType.DateTime);
                    if (NonCreamyLayerDate == "" || NonCreamyLayerDate == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(NonCreamyLayerDate);
                    }
                    j++;
                    param[j] = new SqlParameter("@CastCerApplyState", SqlDbType.Int);
                    if (CastCertApplyState.ToString() == "" || Category == "UR")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = CastCertApplyState;
                    }
                    j++;
                    param[j] = new SqlParameter("@exserviceman", SqlDbType.VarChar);
                    if (ExService.ToString() == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = ExService;
                    }
                    j++;
                    param[j] = new SqlParameter("@ExFromDt", SqlDbType.DateTime);
                    if (exServiceFromDate == "" || exServiceFromDate == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(exServiceFromDate);
                    }
                    j++;
                    param[j] = new SqlParameter("@ExToDt", SqlDbType.DateTime);
                    if (ExServiceToDate == "" || ExServiceToDate == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(ExServiceToDate);
                    }
                    j++;
                    param[j] = new SqlParameter("@DebardDetails", SqlDbType.VarChar);
                    if (Debard == "" || Debard == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Debard;
                    }
                    j++;
                    param[j] = new SqlParameter("@DebardDt", SqlDbType.DateTime);
                    if (debarredDate == "" || debarredDate == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = Utility.formatDate(debarredDate);
                    }
                    j++;

                    param[j] = new SqlParameter("@DebardYr", SqlDbType.Int);
                    if (Convert.ToString(debarredYear) == "")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = debarredYear;
                    }
                    j++;

                    param[j] = new SqlParameter("@feereq", SqlDbType.VarChar);
                    if (feereq == "" || feereq == null)
                    {
                        param[j].Value = "Y";
                    }
                    else
                    {
                        param[j].Value = feereq;
                    }
                    j++;

                    param[j] = new SqlParameter("@CertIssueAuth", SqlDbType.VarChar);
                    if (CertIssueAuth == "" || CertIssueAuth == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = CertIssueAuth;
                    }
                    j++;

                    param[j] = new SqlParameter("@ContractDuration", SqlDbType.VarChar);
                    if (contract_duration == "" || contract_duration == null)
                    {
                        param[j].Value = 0;
                    }
                    else
                    {
                        param[j].Value = Int32.Parse(contract_duration);
                    }
                    j++;
                    param[j] = new SqlParameter("@exServiceDuration", SqlDbType.VarChar);
                    if (ex_serv_duration == "" || ex_serv_duration == null)
                    {
                        param[j].Value = 0;
                    }
                    else
                    {
                        param[j].Value = Int32.Parse(ex_serv_duration);
                    }
                    j++;

                    param[j] = new SqlParameter("@physic", SqlDbType.VarChar);
                    if (physic == "" || physic == null)
                    {
                        param[j].Value = "";
                    }
                    else
                    {
                        param[j].Value = physic;
                    }
                    j++;
                    param[j] = new SqlParameter("@wscribe", SqlDbType.VarChar);
                    if (wscribe == "" || wscribe == null)
                    {
                        param[j].Value = "";
                    }
                    else
                    {
                        param[j].Value = wscribe;
                    }
                    j++;
                    param[j] = new SqlParameter("@OBCRegion", SqlDbType.Char, 1);
                    if (OBCRegion == "" || OBCRegion == null || Category != "OBC")
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = OBCRegion;
                    }
                    j++;

                    param[j] = new SqlParameter("@spousename", SqlDbType.VarChar);
                    if (spousename == "" || spousename == null)
                    {
                        param[j].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[j].Value = spousename;
                    }
                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param != null)
                    {
                        foreach (SqlParameter param1 in param)
                        {
                            command.Parameters.Add(param1);
                        }
                        // int ChallengeID=da.GetDataTableQry
                    }

                    command.ExecuteNonQuery();

                    //for PH Sub-Catogry Praveen 06/10/2021
                    //command.ExecuteNonQuery();
                    //start Insert values in table  PhCertifiacteDetails for 11 coloumns
                    //string strPHC = " insert into PhCertifiacteDetails (applid,SubCat_code,edate,userid,ipaddress) values (@applid,@SubCat_code,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";


                    //is certificate uploaded


                    if (Subcategory.Contains("PH"))
                    {
                        if (isUploaded) 
                        {
                            string strPHC = @" UPDATE PhCertifiacteDetail SET  PhSubcat=@PhSubcat,  PhCertiNo=@PhCertiNo, PhCertIssueAuth=@PhCertIssueAuth,
                                      PhCertIssueDate=@PhCertIssueDate,PhIssueState=@PhIssueState, PhCertificateFile=@PhCertificateFile,IPAddress=@IPAddress, Entrydate=getdate() WHERE applid = @applid";
                            SqlParameter[] paramPHC = new SqlParameter[8];


                            paramPHC[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                            paramPHC[0].Value = applid;

                            paramPHC[1] = new SqlParameter("@PhSubcat", SqlDbType.VarChar, 10);
                            paramPHC[1].Value = "PH";


                            paramPHC[2] = new SqlParameter("@PhCertiNo", SqlDbType.VarChar, 20);
                            paramPHC[2].Value = PHcertifNo;

                            paramPHC[3] = new SqlParameter("@PhCertIssueAuth", SqlDbType.VarChar, 50);
                            paramPHC[3].Value = PHIssuingauthority;

                            paramPHC[4] = new SqlParameter("@PhCertIssueDate", SqlDbType.DateTime, 8);
                            paramPHC[4].Value = Utility.formatDate(PhcertIssuedate);

                            paramPHC[5] = new SqlParameter("@PhIssueState", SqlDbType.Int, 4);
                            if (PHIssueState == 0)
                            {
                                paramPHC[5].Value = System.DBNull.Value;
                            }
                            else
                            {
                                paramPHC[5].Value = PHIssueState;
                            }


                            paramPHC[6] = new SqlParameter("@PhCertificateFile", SqlDbType.Image, PHFile.Length);
                            paramPHC[6].Value = PHFile;

                            paramPHC[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
                            paramPHC[7].Value = IP;


                            command.CommandType = CommandType.Text;
                            command.CommandText = strPHC;
                            command.Parameters.Clear();


                            if (paramPHC != null)
                            {
                                foreach (SqlParameter param3PHC in paramPHC)
                                {
                                    command.Parameters.Add(param3PHC);
                                }

                            }
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            string strPHC = @" INSERT INTO PhCertifiacteDetail (applid,jid,RegNo,PhSubcat ,PhCertiNo ,PhCertIssueAuth ,PhCertIssueDate,PhIssueState,PhCertificateFile,IPAddress,Entrydate)VALUES
           (@applid,@jid, @RegNo,@PhSubcat,@PhCertiNo,@PhCertIssueAuth,@PhCertIssueDate,@PhIssueState, @PhCertificateFile, @IPAddress, getdate())";
                            SqlParameter[] paramPHC = new SqlParameter[10];


                            paramPHC[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                            paramPHC[0].Value = applid;

                            paramPHC[1] = new SqlParameter("@jid", SqlDbType.Int, 10);
                            paramPHC[1].Value = jid;


                            paramPHC[2] = new SqlParameter("@RegNo", SqlDbType.VarChar, 50);
                            paramPHC[2].Value = regno;

                            paramPHC[3] = new SqlParameter("@PhSubcat", SqlDbType.VarChar, 10);
                            paramPHC[3].Value = "PH";


                            paramPHC[4] = new SqlParameter("@PhCertiNo", SqlDbType.VarChar, 20);
                            paramPHC[4].Value = PHcertifNo;

                            paramPHC[5] = new SqlParameter("@PhCertIssueAuth", SqlDbType.VarChar, 50);
                            paramPHC[5].Value = PHIssuingauthority;

                            paramPHC[6] = new SqlParameter("@PhCertIssueDate", SqlDbType.DateTime, 8);
                            paramPHC[6].Value = Utility.formatDate(PhcertIssuedate);

                            paramPHC[7] = new SqlParameter("@PhIssueState", SqlDbType.Int, 4);
                            if (PHIssueState == 0)
                            {
                                paramPHC[7].Value = System.DBNull.Value;
                            }
                            else
                            {
                                paramPHC[7].Value = PHIssueState;
                            }


                            paramPHC[8] = new SqlParameter("@PhCertificateFile", SqlDbType.Image, PHFile.Length);
                            paramPHC[8].Value = PHFile;

                            paramPHC[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
                            paramPHC[9].Value = IP;


                            command.CommandType = CommandType.Text;
                            command.CommandText = strPHC;
                            command.Parameters.Clear();


                            if (paramPHC != null)
                            {
                                foreach (SqlParameter param3PHC in paramPHC)
                                {
                                    command.Parameters.Add(param3PHC);
                                }

                            }
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }
                        
                    }
                    else if (isUploaded)
                    {
                        string strPHC = @" delete from PhCertifiacteDetail WHERE applid = @applid";
                        SqlParameter[] paramPHC = new SqlParameter[1];


                        paramPHC[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                        paramPHC[0].Value = applid;

                        command.CommandType = CommandType.Text;
                        command.CommandText = strPHC;
                        command.Parameters.Clear();


                        if (paramPHC != null)
                        {
                            foreach (SqlParameter param3PHC in paramPHC)
                            {
                                command.Parameters.Add(param3PHC);
                            }

                        }
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    //end Insert values in table  PhCertifiacteDetails
                    /// string[] a = { "," };
                    //if (Subcategory != "")



                    str = @"delete from JapplicantSSScat where JSScatid in(Select JSScatid from JapplicantSScat where JScatid in(select JScatid from JapplicantScat where applid=@applid))";
                    SqlParameter[] param12 = new SqlParameter[1];
                    param12[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                    param12[0].Value = applid;

                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param12 != null)
                    {
                        foreach (SqlParameter param13 in param12)
                        {
                            command.Parameters.Add(param13);
                        }
                        // int ChallengeID=da.GetDataTableQry
                    }

                    command.ExecuteNonQuery();

                    str = @"delete from JapplicantSScat where JScatid in(select JScatid from JapplicantScat where applid=@applid)";
                    SqlParameter[] param10 = new SqlParameter[1];
                    param10[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                    param10[0].Value = applid;

                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param10 != null)
                    {
                        foreach (SqlParameter param11 in param10)
                        {
                            command.Parameters.Add(param11);
                        }
                        // int ChallengeID=da.GetDataTableQry
                    }

                    command.ExecuteNonQuery();

                    str = @"delete from JapplicantScat where applid=@applid";
                    SqlParameter[] param8 = new SqlParameter[1];
                    param8[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                    param8[0].Value = applid;

                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param8 != null)
                    {
                        foreach (SqlParameter param9 in param8)
                        {
                            command.Parameters.Add(param9);
                        }
                        // int ChallengeID=da.GetDataTableQry
                    }

                    command.ExecuteNonQuery();
                    //
                    string[] a = { "," };
                    if (Subcategory.Count > 0)
                    {
                        foreach (var SubCat_code in Subcategory)
                        {
                            str = " insert into JapplicantScat (applid,SubCat_code,edate,userid,ipaddress) values (@applid,@SubCat_code,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";

                            SqlParameter[] param2 = new SqlParameter[4];


                            param2[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                            param2[0].Value = applid;

                            param2[1] = new SqlParameter("@SubCat_code", SqlDbType.VarChar, 4);
                            param2[1].Value = SubCat_code;

                            param2[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                            param2[2].Value = regno;

                            param2[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                            param2[3].Value = IP;

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
                            command.Transaction = transaction;
                            JScatID = Convert.ToInt32(command.ExecuteScalar());
                            if (PHsubCate != "" && SubCat_code == "PH")
                            {
                                string SScatid = "";
                                int SScatids = 10;
                                string[] subsubcat = new string[SScatids];

                                subsubcat = PHsubCate.Split(a, StringSplitOptions.RemoveEmptyEntries);
                                // subsubcat = subcat;
                                for (int k = 0; k < subsubcat.Length; k++)
                                {
                                    SScatid = subsubcat[k];
                                    str = " insert into JapplicantSScat (JScatID,SScatid,edate,userid,ipaddress) values (@JScatID,@SScatid,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";

                                    SqlParameter[] param4 = new SqlParameter[4];


                                    param4[0] = new SqlParameter("@JScatID", SqlDbType.Int, 4);
                                    param4[0].Value = JScatID;

                                    param4[1] = new SqlParameter("@SScatid", SqlDbType.Int, 4);
                                    param4[1].Value = SScatid;

                                    param4[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                                    param4[2].Value = regno;

                                    param4[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                                    param4[3].Value = IP;

                                    command.CommandType = CommandType.Text;
                                    command.CommandText = str;
                                    command.Parameters.Clear();


                                    if (param4 != null)
                                    {
                                        foreach (SqlParameter param5 in param4)
                                        {
                                            command.Parameters.Add(param5);
                                        }

                                    }
                                    command.Transaction = transaction;
                                    JSScatID = Convert.ToInt32(command.ExecuteScalar());

                                    string SSScatid = "";
                                    if (SScatid == "1" && ph_visual != "")
                                    {
                                        SSScatid = ph_visual;
                                    }
                                    else if (SScatid == "2" && ph_hearing != "")
                                    {
                                        SSScatid = ph_hearing;
                                    }
                                    else if (SScatid == "3" && ph_ortho != "")
                                    {
                                        SSScatid = ph_ortho;
                                    }
                                    if (SSScatid != "")
                                    {
                                        string str1 = " insert into JapplicantSSScat (JSScatID,SSScatid,edate,userid,ipaddress) values (@JSScatID,@SSScatid,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";

                                        SqlParameter[] param6 = new SqlParameter[4];


                                        param6[0] = new SqlParameter("@JSScatID", SqlDbType.Int, 4);
                                        param6[0].Value = JSScatID;

                                        param6[1] = new SqlParameter("@SSScatid", SqlDbType.Int, 4);
                                        param6[1].Value = SSScatid;

                                        param6[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                                        param6[2].Value = regno;

                                        param6[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                                        param6[3].Value = IP;

                                        command.CommandType = CommandType.Text;
                                        command.CommandText = str1;
                                        command.Parameters.Clear();


                                        if (param6 != null)
                                        {
                                            foreach (SqlParameter param7 in param6)
                                            {
                                                command.Parameters.Add(param7);
                                            }

                                        }
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                    }


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
        return applid;
    }
    public DataTable getcandsubcategory(string applid)
    {
        str = @"select js.SubCat_code,SubCat_name from JapplicantScat js inner join SubCat_Master scm on js.SubCat_code=scm.SubCat_code where applid = @applid ";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;
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

    private bool FetchPHCertificateDetail(string applid)
    {
        str = @"select applid from PhCertifiacteDetail where applid = @applid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;
        try
        {
            dt = da.GetDataTableQry(str, param);
            return dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int FetchJIDFromApplid(int applid)
    {
        string q = @"select jid from jobapplication where applid = @applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;
        try
        {
            dt = da.GetDataTableQry(q, param);
            if(dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["jid"]);
            }
            else
            {
                throw new Exception("No row corresponding to applid");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable CheckWhetherqualiexmpted(string jid)
    {
        str = @"select eqvalidationexmpt,feeexmpt,agevalidationexmpt from reopenadvt ra inner join reopenpost rp on ra.newadid=rp.newadid 
                where jid=@jid and CAST(CONVERT(VARCHAR,ra.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
                and CAST(CONVERT(VARCHAR,ra.EndsOn,101) AS DATETIME) >= cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) and ra.ReleaseStatus ='Y'";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@jid", SqlDbType.Int, 4);
        param[j].Value = jid;
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


    //by Manindra 27-04-2018
    public DataTable getF_namefromRegistration(string regno)
    {
        str = "select fname from registration where rid=@rid ";


        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
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
    //by manindra 

    public DataTable getbatchdetails(string applid)
    {
        str = @"select  jap.batchid,batchname,convert(varchar,examdate,103) as examdate,examtime,jap.acconsent,bas.radmitcard,jap.acstatid 
                from JobApplication jap inner join BatchMaster bm on jap.batchid=bm.batchid 
                inner join BatchAllocationStatus bas on bm.batchid=bas.BatchId
                where applid=@applid ";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;
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
    public DataTable Checkbatchstatus(string applid)
    {
        str = @"select radmitcard,batchid from BatchAllocationStatus where BatchId=(select min(batchid) from BatchMaster 
               where examid=(select examid from job_advt where jid=(select jid from JobApplication where applid=@applid))) 
			   and radmitcard='Y'";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[j].Value = applid;
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

    public DataTable getpostforstatus(string regno)
    {
        str = @"select Job_Advt.[jid] jid,applid as applid
            ,replace([JobTitle],'[dot]','.')+' Post Code :('+postcode+')' post
            from Job_Advt 
            inner join JobApplication on JobApplication.jid=Job_Advt.jid
            where RegNo=@regno and dummy_no is not null and final='Y'";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar);
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

    public DataTable getdataforapplid(string applid)
    {
        str = @"select dummy_no,convert(varchar,receive_dt,103) as appldt,feereq,convert(varchar,trandate,103) as feedt from JobApplication jap 
                left outer join FeeDetails fd on jap.applid=fd.applid where jap.applid=@applid";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[0].Value = DBNull.Value;
        }
        else
        {
            param[0].Value = applid;
        }
        DataTable dt = da.GetDataTableQry(str, param);
        return dt;
    }

    public DataTable get_postforfinalsubmit(string regno)
    {

        str = @"select Job_Advt.[jid] jid,Job_Advt.[reqid] reqid, applid as applid
                    ,replace([JobTitle]+' Post Code :('+postcode+')','[dot]','.') as post
                    from Job_Advt 
                    inner join JobApplication on JobApplication.jid=Job_Advt.jid
                    where 
                    (
	                    adid in 
	                    (select adid from advmaster

	                    where CAST(CONVERT(VARCHAR,advMaster.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
	                    and CAST(CONVERT(VARCHAR,advMaster.EndsOn,101) AS DATETIME) >=
	                    cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)
	                    )
                    )
                    and RegNo=@regno  
                    and                     
                    dummy_no is null
                       --final is null 
                    union

                    select reopenpost.[jid] jid,Job_Advt.[reqid] reqid, applid as applid
                    ,replace([JobTitle]+' Post Code :('+postcode+')','[dot]','.') as post
                    from reopenpost 
                    inner join Job_Advt on Job_Advt.jid=reopenpost.jid
                    inner join JobApplication on JobApplication.jid=reopenpost.jid
                    where
	                     newadid in
	                    (
	                    select reopenadvt.newadid from reopenadvt
	                    inner join advMaster on advMaster.adid=reopenadvt.adid and (reopenadvt.reopenstatus='C'";

        if (System.Web.HttpContext.Current.Session["intraflag"] != null)
        {
            str = str + " or reopenadvt.reopenstatus='D'";
        }
        str = str + @" )and  reopenadvt.ReleaseStatus='Y'
	                    and AdvMaster.Reopened='Y' 
                        inner join reopenpost on reopenpost.newadid=reopenadvt.newadid
                        inner join Job_Advt on Job_Advt.jid=reopenpost.jid
	                    where CAST(CONVERT(VARCHAR,reopenadvt.StartsFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
	                    and CAST(CONVERT(VARCHAR,reopenadvt.EndsOn,101) AS DATETIME) >=
	                    CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 	
	                    )
                    and RegNo=@regno  
                    and 
                    dummy_no is null
                    --final is null";



        SqlParameter[] param = new SqlParameter[1];
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
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;

    }

    public DataTable FillDDLQuali(string reqid, string standard, string qtype)
    {

        str = @"SELECT id,name from tbledu where  id in(select id from tbledu_TRN where reqid=@reqid)";
        if (qtype == "E")
        {
            str += " and (qtype='E' or qtype='G') ";
        }
        else
        {
            str += " and qtype=@qtype ";
        }
        if (standard != "")
        {
            str += " and stid=@stid";
        }

        // str += " order by name";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@stid", SqlDbType.Int);
        if (standard == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
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
    public DataTable getgroupquali(string reqid)
    {

        str = @"select etrn.id,name,groupno from tbledu_TRN etrn inner join tbledu edu on etrn.id=edu.id where qtype='G' and reqid=@reqid order by groupno";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
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
    public DataTable Getgroupquali(string reqid, string groupno, string uidlist)
    {

        str = @"select uid from tbledu_TRN where reqid=@reqid and groupno=@groupno and uid not in( " + uidlist + @" ) and uid not in (select uid from 
				tbledu_TRN etrn inner join tbledu ed on etrn.id=ed.id and stid=7 and reqid=@reqid)  ";


        // str += " order by name";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
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
    public DataTable Getexpyears(string reqid, string groupno)
    {

        // str = @"select etrn.reqid,exp_noofyears from tbledu_TRN etrn left outer join QExpMaster qexp on etrn.groupno=qexp.groupno and etrn.reqid=@reqid
        //     where exp_noofyears is not null ";
        //        str = @"select etrn.reqid,exp_noofyears from tbledu_TRN etrn left outer join QExpMaster qexp on etrn.groupno=qexp.groupno and etrn.reqid=qexp.reqid
        //                 where etrn.reqid=@reqid and exp_noofyears is not null ";
        str = @"select etrn.reqid,case when ja.exp_noofyears is null then qexp.exp_noofyears else ja.exp_noofyears end as exp_noofyears  
               from tbledu_TRN etrn left outer join QExpMaster qexp on etrn.groupno=qexp.groupno and etrn.reqid=qexp.reqid
               inner join job_advt ja on etrn.reqid=ja.reqid
			   where etrn.reqid=@reqid ";

        if (groupno != "")
        {
            str += " and qexp.groupno=@groupno ";
        }
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
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
    public DataTable Getexpdetails(string applid)
    {

        str = @"select convert(varchar,datefrom,103) as datefrom,convert(varchar,dateto,103) as dateto from JobExperience where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = applid;
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
    public DataTable GetGroupexp(string reqid, string groupno)
    {
        str = @"select etrn.reqid,exp_noofyears,qexp.essential_exp  
                from tbledu_TRN etrn inner join QExpMaster qexp on etrn.groupno=qexp.groupno and etrn.reqid=qexp.reqid
                where etrn.reqid=@reqid and etrn.groupno=@groupno ";

        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
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

    public DataTable getEdossierfinalEmail(string applid)
    {

        str = @"select ei.email,ei.applid,ef.edossierNo from EDPinfo ei join edossiersfinal ef on ei.applid=ef.applid where ei.applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = applid;
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


    public DataTable checkValidRecalledForUploadingEdossier(string regno, string jid)
    {
        string con = "";
        string str1 = "select issupplimentry from Applicant_result ar join Job_Advt jad on ar.postcode=jad.postcode where jad.jid=@jid and ar.regno=@regno";
        SqlParameter[] param1 = new SqlParameter[2];
        param1[0] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param1[0].Value = jid;
        }
        else
        {
            param1[0].Value = System.DBNull.Value;
        }
        param1[1] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param1[1].Value = regno;
        DataTable dt1 = da.GetDataTableQry(str1, param1);
        if (Convert.ToString(dt1.Rows[0]["issupplimentry"]) != null && Convert.ToString(dt1.Rows[0]["issupplimentry"]) != "")
        {
            con = "rvid in (select rvid from Applicant_result ar join Job_Advt jad on ar.postcode=jad.postcode where jad.jid=@jid and ar.regno=@regno)";
        }
        else
        {
            con = "jid=@jid and rvid is null ";
        }

        string str = "select * from EdossierSchedule where " + con + " and ((CAST(CONVERT(VARCHAR,fromdt,101) AS DATETIME) <= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)) and (CAST(CONVERT(VARCHAR,todt,101) AS DATETIME) >= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)))";
        //string str = "select * from EdossierSchedule where " + con + " and fromdt <=  GETDATE() and todt >= GETDATE()";
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
        param[1] = new SqlParameter("@jid", SqlDbType.Int, 4);
        if (jid != "")
        {
            param[1].Value = jid;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
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

    public int updatePostCardphoto(string appno, byte[] image, string ip, string UPDATEDT)
    {
        string str = "update jobApplicationPostCardPhoto set PostCardPhoto=@PostCardPhoto,IPAdress=@IPAdress,UpdateDate=@UpdateDate where ApplId=@ApplId";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@PostCardPhoto", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IPAdress", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@UpdateDate", SqlDbType.SmallDateTime, 4);
        param[j].Value = UPDATEDT;
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
    public int insertPostCardphoto(string appno, byte[] image, string ip, string entryDate)
    {

        string str = "insert into jobApplicationPostCardPhoto (ApplId, PostCardPhoto, IPAdress, EntryDate) values (@ApplId,@PostCardPhoto, @IPAdress, @EntryDate)";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = appno;
        j++;
        param[j] = new SqlParameter("@PostCardPhoto", SqlDbType.Image, image.Length);
        param[j].Value = image;
        j++;
        param[j] = new SqlParameter("@IPAdress", SqlDbType.VarChar, 20);
        param[j].Value = ip;
        j++;
        param[j] = new SqlParameter("@EntryDate", SqlDbType.SmallDateTime, 4);
        param[j].Value = entryDate;
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
    public DataTable checkPostCardPhoto(string applid)
    {
        string instr = "select ISNULL(DATALENGTH(PostCardPhoto),0) PostCardPhoto from jobApplicationPostCardPhoto where ApplId=@ApplId";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@ApplId", SqlDbType.Int, 4);
        param[j].Value = Int32.Parse(applid);
        try
        {
            DataTable dt = da.GetDataTableQry(instr, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /* public int InsertIntoCandidateAcivityLog(string userID, string activityStatus, string activityDone, string ipAddress)
     {

         string str = "insert into candidateactivitylog (userID, activityStatus, activityDone, entryDate, ipAddress) values (@userID, @activityStatus, @activityDone, getdate(), @ipAddress)";
         SqlParameter[] param = new SqlParameter[4];
         param[0] = new SqlParameter("@userID", SqlDbType.VarChar, 50);
         param[0].Value = userID;
         param[1] = new SqlParameter("@activityStatus", SqlDbType.VarChar, 10);
         param[1].Value = activityStatus;
         param[2] = new SqlParameter("@activityDone", SqlDbType.VarChar, 100);
         param[2].Value = activityDone;
         param[3] = new SqlParameter("@ipAddress", SqlDbType.VarChar, 20);
         param[3].Value = ipAddress;
         try
         {
             int id = da.ExecuteParameterizedQuery(str, param);
             return id;

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }*/

    public DataTable getProofOfIdentity()
    {
        string instr = "select * from proofOfIdentityDocumentMaster";
        try
        {
            DataTable dt = da.GetDataTable(instr);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getIDProofPostCardPhotoStatus(string applid, string dummy_no)
    {
        str = @"select japcp.PhotoID,japcp.PostCardPhoto,reg.aadharno,reg.nameonAadhar,reg.nameOnIDProof,reg.proofOfID 
                from jobapplication jap left join jobApplicationPostCardPhoto japcp on jap.applid=japcp.applid join registration reg on jap.regno=reg.rid
                where jap.applid=@applid and jap.dummy_no=@dummy_no and jap.jid in(select jid from job_advt where adid in (26,27,28,29,30)  and postcode !='89/20')";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[0].Value = DBNull.Value;
        }
        else
        {
            param[0].Value = applid;
        }
        param[1] = new SqlParameter("@dummy_no", SqlDbType.Int);
        if (dummy_no == "")
        {
            param[1].Value = DBNull.Value;
        }
        else
        {
            param[1].Value = dummy_no;
        }
        DataTable dt = da.GetDataTableQry(str, param);
        return dt;
    }
    public DataTable getStatusIfCandidateIn116(string rid)
    {
        String str = @"select applid,dummy_no from jobapplication where jid in(select jid from job_advt where adid in (26,27,28,29,30) and postcode !='89/20') 
                       and RegNo=@rid and dummy_no is not null";
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
    public int insert_IDProof_Document(string rid, string aadharNo, string nameOnAadhar, string nameOnIDProof, int proofOfID, string pofIDNum, byte[] pofIDDoc)
    {
        int temp = 0;
        SqlCommand command = new SqlCommand();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString_RO"]);
        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    command.Connection = connection;
                    command.Transaction = transaction;

                    string str = @"update registration set aadharNo=@aadharNo, nameOnAadhar=@nameOnAadhar,nameOnIDProof=@nameOnIDProof,proofOfID=@proofOfID where rid=@rid";

                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@aadharNo", SqlDbType.NVarChar);
                    if (aadharNo != "")
                    {
                        param[0].Value = aadharNo;
                    }
                    else
                    {
                        param[0].Value = System.DBNull.Value;
                    }

                    param[1] = new SqlParameter("@nameOnAadhar", SqlDbType.VarChar);
                    if (nameOnAadhar != "")
                    {
                        param[1].Value = nameOnAadhar;
                    }
                    else
                    {
                        param[1].Value = System.DBNull.Value;
                    }

                    param[2] = new SqlParameter("@nameOnIDProof", SqlDbType.VarChar);
                    if (nameOnIDProof != "")
                    {
                        param[2].Value = nameOnIDProof;
                    }
                    else
                    {
                        param[2].Value = System.DBNull.Value;
                    }

                    param[3] = new SqlParameter("@proofOfID", SqlDbType.Int);
                    if (proofOfID != 0)
                    {
                        param[3].Value = Convert.ToInt32(proofOfID);
                    }
                    else
                    {
                        param[3].Value = System.DBNull.Value;
                    }

                    param[4] = new SqlParameter("@rid", SqlDbType.VarChar);
                    param[4].Value = rid;


                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.Parameters.Clear();

                    if (param != null)
                    {
                        foreach (SqlParameter param1 in param)
                        {
                            command.Parameters.Add(param1);
                        }
                    }

                    temp = command.ExecuteNonQuery();
                    if (proofOfID > 0)
                    {
                        temp = 0;
                        string str1 = @"insert into proofOfIDUploaded_Reg (regNo,proofOfId,proofOfIDNo,IDUploaded,entryDate) 
                                   values(@regNo,@proofOfId,@proofOfIDNo,@IDUploaded,getdate())";
                        SqlParameter[] param2 = new SqlParameter[4];

                        param2[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
                        param2[0].Value = rid;
                        param2[1] = new SqlParameter("@proofOfId", SqlDbType.Int);
                        param2[1].Value = proofOfID;
                        param2[2] = new SqlParameter("@proofOfIDNo", SqlDbType.VarChar);
                        param2[2].Value = pofIDNum;
                        param2[3] = new SqlParameter("@IDUploaded", SqlDbType.VarBinary);
                        param2[3].Value = pofIDDoc;

                        command.CommandType = CommandType.Text;
                        command.CommandText = str1;
                        command.Parameters.Clear();

                        if (param2 != null)
                        {
                            foreach (SqlParameter param3 in param2)
                            {
                                command.Parameters.Add(param3);
                            }
                        }
                        temp = command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    temp = 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        return temp;
    }

    public DataTable getLinkEnableDisableStatus(string linkId)
    {
        String str = @"select * from linkenabledisable where
                    (
                      CAST(CONVERT(VARCHAR,dtFrom,101) AS DATETIME)<= CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME) 
                      and CAST(CONVERT(VARCHAR,dtTo,101) AS DATETIME) >=
                      cast(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)
                    ) and linkId=@linkId ";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@linkId", SqlDbType.VarChar, 50);
        param[0].Value = linkId;
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
    public DataTable getApplidAndFlagForReverifyAutomatically()
    {
        str = @"select top(1) JobApplication.applid as applid,JobApplication.feereq as feereq from Job_Advt inner join JobApplication on JobApplication.jid=Job_Advt.jid 
                left outer join feedetails on JobApplication.applid=feedetails.applid where adid in 
                (select adid from advmaster where CONVERT(VARCHAR,GETDATE(),111) between CONVERT(VARCHAR,StartsFrom,111) and CONVERT(VARCHAR,FeeLastDate,111)
                union select adid from reopenadvt where CONVERT(VARCHAR,GETDATE(),111) between CONVERT(VARCHAR,StartsFrom,111) and CONVERT(VARCHAR,FeeLastDate,111))
                and dummy_no is null and final='Y' and feerecd is null and feereq='Y'";
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
    public DataTable GetMessageMobileAPP(string flag)
    {
        str = "Select msgid,msg_file,pno,message,convert(varchar,m_edate,103) m_edate,fileexist from MessageMaster where flag=@flag and status='Y' ";

        str += " order by pno ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@flag", SqlDbType.Char, 1);
        param[0].Value = flag;
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
    public DataTable getStatusEdossierCand(string appid) //Ambika 17062021
    {
        String str = @"select er.certificateReq,convert(varchar,cm.edate,103)edate, ef.edossierNo from CandidateEdossier cm inner join edossiermaster er on er.edmid =cm.edmid inner join edossiersfinal ef on ef.applid =cm.applid where cm.applid =@appid";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@appid", SqlDbType.VarChar, 50);
        param[0].Value = appid;
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
    public DataTable get_postApp_eDossier(string regno)
    {
        str = @"select distinct jobtitle + '( ' + ja.postcode + ' )' as post,
 	eds.jid, jap.applid
 	from Job_Advt ja
 	inner join (select * from JobApplication union select * from dsssbonline_recdapp.dbo.JobApplication) jap on jap.jid=ja.jid and jap.regno=@regno
 	inner join EdossierSchedule eds on ja.jid=eds.jid
 	inner join ResultVerification rv on rv.jid=ja.jid and rv.nextexam is null
 	inner join Applicant_result ar on rv.rvid=ar.rvid and flag='V'
 	where ar.RegNo=@regno 
 	and released='Y'";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        if (regno == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = regno;
        }
        DataTable dt = da.GetDataTableQry(str, param);
        return dt;

    }
    public DataTable CheckReopenPostOnUpdateApplication(string reqid, string CatCode)
    {
        //str = @"select * from tbl_CatWiseVacancy where reqid=@reqid and CatCode=@CatCode and CatOrSub='S'";
        str = @"SELECT subcat_master.id,SubCat_code,SubCat_name from subcat_master
                inner join RR_age_relax on RR_age_relax.CatCode=SubCat_Master.SubCat_code
                where RR_age_relax.CatIndCS='S' and reqid=@reqid and SubCat_code=@CatCode";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        param[0].Value = reqid;
        param[1] = new SqlParameter("@CatCode", SqlDbType.Char, 4);
        param[1].Value = CatCode;
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


    public DataTable Get_PhFileDoc(int applid)
    {
        str = @"select PhCertificateFile from PhCertifiacteDetail where applid=@applid ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int, 10);
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

    public DataTable get_boardUniversityList(int stateId)
    {
        String str = @"select boardUnivId,boardUnivName from  m_BoardUniversity where stateid=@stateId";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@stateId", SqlDbType.Int);
        param[0].Value = stateId;
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
    public DataTable GetGroupexp_ForCheckbox(string reqid, List<int> groupno)
    {
        string condn = "";
        if (groupno.Count > 0)
        {
            //condn += " and groupno=@groupno ";
            condn += " and  etrn.groupno in(" + (groupno.Count > 0 ? string.Join(",", groupno.ToArray()) : System.DBNull.Value.ToString()) + ") ";
        }
        else
        {
            //condn += " and groupno=@groupno
        }

        str = @"select etrn.reqid,exp_noofyears,qexp.essential_exp  
                from tbledu_TRN etrn inner join QExpMaster qexp on etrn.groupno=qexp.groupno and etrn.reqid=qexp.reqid
                where etrn.reqid=@reqid" + condn + @" ";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        //param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        //if (groupno == "")
        //{
        //    param[j].Value = System.DBNull.Value;
        //}
        //else
        //{
        //    param[j].Value = groupno;
        //}
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
    public DataTable GetgroupqualiFor_CheckBox(string reqid, List<int> groupno, string uidlist)
    {
        string condn = "";
        if (groupno.Count > 0)
        {
            //condn += " and groupno=@groupno ";
            condn += " and  groupno in(" + (groupno.Count > 0 ? string.Join(",", groupno.ToArray()) : System.DBNull.Value.ToString()) + ") ";
        }
        else
        {


            //condn += " and groupno=@groupno
        }

        str = @"select uid from tbledu_TRN where reqid=@reqid " + condn + @"  and uid not in( " + uidlist + @" ) and uid not in (select uid from 
				tbledu_TRN etrn inner join tbledu ed on etrn.id=ed.id and stid=7 and reqid=@reqid)  ";


        // str += " order by name";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        //param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        //if (groupno == "")
        //{
        //    param[j].Value = System.DBNull.Value;
        //}
        //else
        //{
        //    param[j].Value = groupno;
        //}

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

    public DataTable GetessentialqualiForCheck(string reqid, string uidlist)
    {



        str = @"select uid from tbledu_TRN where reqid=@reqid  and uid not in( " + uidlist + @" ) and uid not in (select uid from 
				tbledu_TRN etrn inner join tbledu ed on etrn.id=ed.id and stid=7 and reqid=@reqid)  ";


        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;


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



    /*Inserting Registered Candidate's updated Name in Reg_Update_PD*/
    public int update_Newname(string rid, string name, int C_ounter, DateTime M_Date)
    {
        string str = "insert into Reg_Update_PD (reg_id,Applicant_Name,C_ounter,M_Date)values(@reg_id,@Applicant_Name,@C_ounter,@M_Date)";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@reg_id", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@Applicant_Name", SqlDbType.VarChar, 50);
        param[1].Value = name;
        param[2] = new SqlParameter("@C_ounter", SqlDbType.Int, 50);
        param[2].Value = C_ounter;
        param[3] = new SqlParameter("@M_Date", SqlDbType.DateTime, 50);
        param[3].Value = M_Date;
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

    /*Inserting Registered Candidate's updated FatherName in Reg_Update_PD */
    public int update_NewFname(string rid, string fname, int C_ounter, DateTime M_Date)
    {
        string str = "insert into Reg_Update_PD (reg_id,Father_Name,C_ounter,M_Date)values(@reg_id,@Father_Name,@C_ounter,@M_Date)";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@reg_id", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@Father_Name", SqlDbType.VarChar, 50);
        param[1].Value = fname;
        param[2] = new SqlParameter("@C_ounter", SqlDbType.Int, 50);
        param[2].Value = C_ounter;
        param[3] = new SqlParameter("@M_Date", SqlDbType.DateTime, 50);
        param[3].Value = M_Date;
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
    /*Inserting Registered Candidate's updated MotherName in Reg_Update_PD */
    public int update_NewMname(string rid, string mname, int C_ounter, DateTime M_Date)
    {
        string str = "insert into Reg_Update_PD (reg_id,Mother_Name,C_ounter,M_Date)values(@reg_id,@Mother_Name,@C_ounter,@M_Date)";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@reg_id", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@Mother_Name", SqlDbType.VarChar, 50);
        param[1].Value = mname;
        param[2] = new SqlParameter("@C_ounter", SqlDbType.Int, 50);
        param[2].Value = C_ounter;
        param[3] = new SqlParameter("@M_Date", SqlDbType.DateTime, 50);
        param[3].Value = M_Date;
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
    /*Inserting Registered Candidate's updated Gender in Reg_Update_PD */
    public int update_Newgender(string rid, string gender, int C_ounter, DateTime M_Date)
    {
        string str = "insert into Reg_Update_PD (reg_id,Gender,C_ounter,M_Date)values(@reg_id,@Gender,@C_ounter,@M_Date)";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@reg_id", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@Gender", SqlDbType.VarChar, 50);
        param[1].Value = gender;
        param[2] = new SqlParameter("@C_ounter", SqlDbType.Int, 50);
        param[2].Value = C_ounter;
        param[3] = new SqlParameter("@M_Date", SqlDbType.DateTime, 50);
        param[3].Value = M_Date;
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
    /*Inserting Registered Candidate's updated Id in Reg_Update_PD */
    public int update_NewIdentity(string rid, string id, int C_ounter, DateTime M_Date)
    {
        string str = "insert into Reg_Update_PD (reg_id,Id_Card,C_ounter,M_Date)values(@reg_id,@Id_Card,@C_ounter,@M_Date)";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@reg_id", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@Id_Card", SqlDbType.VarChar, 50);
        param[1].Value = id;
        param[2] = new SqlParameter("@C_ounter", SqlDbType.Int, 50);
        param[2].Value = C_ounter;
        param[3] = new SqlParameter("@M_Date", SqlDbType.DateTime, 50);
        param[3].Value = M_Date;
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
    /*Modify aadhar details of registered candidates in registration table*/
    public int update_Identity(string rid, string id)
    {
        string str = "update registration set aadharNo=@aadharNo where rid=@rid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        param[1] = new SqlParameter("@aadharNo", SqlDbType.NVarChar, 50);
        param[1].Value = id;

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
    /*counter checking for modification of registered candidate's data*/
    //    public int getcountervalue(string regno)
    //    {
    //        try
    //        {
    //            string str = @"select * from Reg_Update_PD where reg_id=@regno
    //                           AND C_ounter!=@C_ounter";
    //            SqlParameter[] param = new SqlParameter[2];
    //            param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
    //            param[0].Value = regno;
    //            param[1] = new SqlParameter("@C_ounter", SqlDbType.Int, 50);
    //            param[1].Value = 0;

    //            dt = da.GetDataTableQry(str, param);
    //            int i = dt.Rows.Count;
    //            return i;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    /*  personal details updation*/
    //public int insert_PD(string regno, string name, string fname, string mname, string gender, string id)
    //{
    //    string str = "insert into Reg_Update_PD (reg_id,Applicant_Name,Father_Name,Mother_Name,Gender,Id_Card,C_ounter,M_Date) values (@regno,@name,@fname,@mname,@gender,@id,@C_ounter,@modifydate)";
    //    SqlParameter[] param = new SqlParameter[8];
    //    param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
    //    param[0].Value = regno;
    //    param[1] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
    //    param[1].Value = name;
    //    param[2] = new SqlParameter("@fname", SqlDbType.NVarChar, 50);
    //    param[2].Value = fname;
    //    param[3] = new SqlParameter("@mname", SqlDbType.NVarChar, 50);
    //    param[3].Value = mname;
    //    param[4] = new SqlParameter("@gender", SqlDbType.NVarChar, 50);
    //    param[4].Value = gender;
    //    param[5] = new SqlParameter("@id", SqlDbType.NVarChar, 50);
    //    param[5].Value = id;
    //    param[6] = new SqlParameter("@C_ounter", SqlDbType.Int, 50);
    //    param[6].Value = 0;
    //    param[7] = new SqlParameter("@modifydate", SqlDbType.DateTime, 50);
    //    param[7].Value = DateTime.Now;
    //    try
    //    {
    //        int i = da.ExecuteParameterizedQuery(str, param);
    //        return i;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}

    //Added BY AnkitaSingh on Dated:17-02-2023 for Certificate issuing date check
    public DataTable GetAdvtEndDate(string postcode)
    {
        str = @"select EndsOn from AdvMaster where adid in (select adid from Job_Advt where postcode=@postcode) ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@postcode", SqlDbType.VarChar);
        if (postcode == "")
        {
            param[0].Value = System.DBNull.Value;
        }
        else
        {
            param[0].Value = postcode;
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

    //updated on 27th jan 2023
    public DataTable getgender(string rid)
    {

        String str = @"select  case gender when 'F' then 'female' when 'M' then 'male' end as gender from registration where rid=@rid";
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
    //Dated: 01-03-2023
    public DataTable CategoryDetails(int applid)
    {
        str = @"select * from JapplicantScat where applid=@applid ";

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

    public DataTable subCategoryDetails(string JScatID)
    {
        str = @"select * from JapplicantSScat where JScatID=@JScatID ";

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@JScatID", SqlDbType.Int);
        param[0].Value = JScatID;

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

    public DataTable subsubCategoryDetails(string JSScatid)
    {
        str = @"select * from JapplicantSSScat where JSScatid=@JSScatid ";

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@JSScatid", SqlDbType.Int);
        param[0].Value = JSScatid;

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
    public int delete_Category(int applid)
    {
        string str = "delete from JapplicantScat where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@applid", SqlDbType.Int);

        param[j].Value = applid;
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
    public int Updatecat(int applid, string cat)
    {

        string str = "update JobApplication set category=@cat where applid=@applid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = applid;
        param[1] = new SqlParameter("@cat", SqlDbType.VarChar, 10);
        param[1].Value = cat;
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

    public int delete_subCategory(string JScatID)
    {
        string str = "delete from JapplicantSScat where JScatID=@JScatID";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@JScatID", SqlDbType.Int);

        param[j].Value = JScatID;
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

    public int delete_subsubCategory(string JSScatid)
    {
        string str = "delete from JApplicantSSScat where JSScatid=@JSScatid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;

        param[j] = new SqlParameter("@JSScatid", SqlDbType.Int);

        param[j].Value = JSScatid;
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

    //07/03/2023
    public int delete_Edu(int applid)
    {
        string str = "delete from JobEducation where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = applid;
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



    //Added by AnkitaSingh on 24-03-2023 for 90/09
    public DataTable CheckVerification(string rid)
    {
        String str = @"select * from oldpostmatching where regno=@regno";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[0].Value = rid;
        //param[1] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        //param[1].Value = serial_no;
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

    //Added by AnkitaSingh for 90/09 Dated:04-10-2022
    public void oldpostEntry(string serial_no, string postcode, string consent)
    {

        string str = "insert into oldpostmatching (serial_no, postcode, Consent) values (@serial_no,@postcode,@consent) ";
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        param[0].Value = serial_no;

        param[1] = new SqlParameter("@postcode", SqlDbType.VarChar, 50);
        param[1].Value = postcode;

        param[2] = new SqlParameter("@consent", SqlDbType.Char, 1);
        param[2].Value = consent;
        try
        {
            da.ExecuteParameterizedQuery(str, param);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int uploadDoc(string regno, string serial_no, string postcode, char consent, byte[] file1, string doc)//AnkitaSingh for 90/09 doc upload
    {
        string qry = @"delete from oldpostmatching where serial_no=@serial_no ;
                   Insert into oldpostmatching(regno, serial_no, postcode, Consent, DocFile, UploadedDoc,tstamp) values(@regno, @serial_no, @postcode, @Consent, @DocFile, @doc, @tstamp)";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
        param[0].Value = regno;
        param[1] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        param[1].Value = serial_no;
        param[2] = new SqlParameter("@postcode", SqlDbType.VarChar, 50);
        param[2].Value = postcode;
        param[3] = new SqlParameter("@Consent", SqlDbType.Char, 1);
        param[3].Value = consent;
        param[4] = new SqlParameter("@DocFile", SqlDbType.Image, file1.Length);
        param[4].Value = file1;
        param[5] = new SqlParameter("@doc", SqlDbType.VarChar, 50);
        param[5].Value = doc;

        param[6] = new SqlParameter("@tstamp", SqlDbType.DateTime);
        param[6].Value = DateTime.Now;

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

    //Added by AnkitaSingh on 02-06-2023 for 90/09
    public DataTable VerifyRecord(string sno)
    {
        String str = @"select * from oldpostmatching where serial_no=@serial_no";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        param[0].Value = sno;
        //param[1] = new SqlParameter("@serial_no", SqlDbType.VarChar, 50);
        //param[1].Value = serial_no;
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

    //rohitxd => 21/11/2023
    public void InsertFeeResponseLog(string response)
    {
        string query = @"INSERT INTO EpayFeeResponse
                           (ResTimeStamp
                           ,ResText)
                     VALUES
                           (@tstamp, @text)";
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@tstamp", SqlDbType.DateTime);
        param[0].Value = DateTime.Now;
        param[1] = new SqlParameter("@text", SqlDbType.NVarChar);
        param[1].Value = response;
        try
        {
            da.ExecuteParameterizedQuery(query, param);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataTable SelectPH_combd(int reqid)//21/09/2023
    {
        str = @"SELECT distinct PH_Code,PH_Cat_Desc
            FROM SubCat_Master_PH scm inner join SubSubCatWiseVacancy vac on scm.PH_Cat_Desc=vac.PH_SubCatCode
            and reqid in (select DeptReqId from CombinedEntry where CombdReqid=@reqid) ";


        string str1 = @"SELECT distinct PH_Code,PH_Cat_Desc
            FROM SubCat_Master_PH scm inner join SubSubCatWiseVacancy vac on scm.PH_Cat_Desc=vac.PH_SubCatCode
            and reqid = @reqid ";
        // str += " order by name";


        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        param[j].Value = reqid;

        SqlParameter[] param1 = new SqlParameter[1];
        int k = 0;
        param1[k] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        param1[k].Value = reqid;

        try
        {
            DataTable dt = da.GetDataTableQry(str, param);
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
    public DataTable age_relax_combd(int reqid)//21/09/2023 COMBD
    {

        str = @"SELECT  distinct isnull(ar.catcode,'0') catcode, isnull(ar.CatIndCS,'0') catindcs, CM, D_Year,isnull(fe.fee_exmp,'N') Fee_exmp
                FROM    RR_age_relax ar
                full outer join fee_exemption fe on fe.catcode=ar.catcode where ar.reqid in (select DeptReqId from CombinedEntry where CombdReqid=@reqid)";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        if (reqid == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;

    }
    public DataTable age_relax_combdforfinal(int reqid, int applid)//21/09/2023 COMBD
    {

        str = @"SELECT  distinct isnull(ar.catcode,'0') catcode, isnull(ar.CatIndCS,'0') catindcs, CM, D_Year,isnull(fe.fee_exmp,'N') Fee_exmp
                FROM    RR_age_relax ar
                full outer join fee_exemption fe on fe.catcode=ar.catcode 
                inner join JapplicantScat js on ar.CatCode=js.SubCat_code and ar.CatIndCS='S' 
                where ar.reqid in (select DeptReqId from CombinedEntry where CombdReqid=@reqid) and js.applid=@applid";
        int j = 0;
        SqlParameter[] param = new SqlParameter[2];
        param[j] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        if (reqid == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@applid", SqlDbType.Int, 4);
        if (applid == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = applid;
        }
        DataTable age_relax_flag = da.GetDataTableQry(str, param);
        return age_relax_flag;

    }
    public DataTable Getdeptcode(string reqid)
    {

        str = @"select deptcode from dept_job_request where reqid=@reqid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[j].Value = reqid;
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
    //rohitxd 
    public DateTime GetDobTofromDB(string reqid)
    {
        //string str = "select DOBTO from Job_Advt where reqid = @reqid";
        string str = @" select endson from AdvMaster amv where adid = 
                             (select distinct adid from Job_Advt where reqid =
						     (select top 1 CombdReqid from CombinedEntry where DeptReqId= @reqid)) ";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[j].Value = reqid;
        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 1)
            {
                return (DateTime)dt.Rows[0]["endson"];
            }
            else
            {
                throw new Exception("GetDobTofromDB method returned more than one row");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_Deptname(string reqid)
    {
        str = @"select reqid,djr.deptcode as deptcode,deptname,JobTitle,MaxAge,MinAge,gender,REPLACE (essential_qual,'[dot]','.') as essential_qual,REPLACE(desire_qual,'[dot]','.') as desire_qual,essential_exp,desire_exp 
                from dept_job_request djr inner join deptmaster dm on dm.deptcode= djr.deptcode where  
                reqid in (select DeptReqId from CombinedEntry where CombdReqid = @reqid)";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@reqid", SqlDbType.Int);
        param[0].Value = Convert.ToInt32(reqid);
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

    public DataTable get_saveddpt(string applid)
    {
        str = @"select distinct DepartmentName,MaxAge,MinAge,gender,essential_qual,desire_qual,essential_exp,desire_exp from dept_job_request djr
				inner join CombdCandDeptDetails ccdd on ccdd.DeptReqId=djr.reqid where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Convert.ToInt32(applid);
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
    public DataTable getcanddetail(string applid)
    {
        str = @"select ExFromDt,ExToDt,gender,birthdt,category,SubCat_code,OBCRegion from JobApplication ja left join JapplicantScat js on js.applid=ja.applid where ja.applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.VarChar, 50);
        param[j].Value = applid;
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
    public DataTable getcandsubcatdetail(string applid)//27/02/2023
    {
        str = @"select SubCat_code from JapplicantScat where applid=@applid order by SubCat_code DESC";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.VarChar, 50);
        param[j].Value = applid;
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
    public DataTable agerelax(string reqid)
    {
        str = @"SELECT distinct isnull(catcode,'0') catcode, isnull(CatIndCS,'0') catindcs, CM, D_Year FROM RR_age_relax ar where reqid = @reqid";
        //in(select DeptReqId from CombinedEntry where CombdReqid=@reqid)";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[j].Value = reqid;
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
    public int Insert_deptdetails(string applid, string Combdreqid, string Deptreqid, string DeptName, string DeptCode)
    {
        string str = @"insert into CombdCandDeptDetails (applid, Combdreqid, Deptreqid, DepartmentName, DeptCode) 
         values(@applid, @Combdreqid, @Deptreqid, @DeptName, @DeptCode)";

        SqlParameter[] param = new SqlParameter[5];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = applid;
        param[1] = new SqlParameter("@Combdreqid", SqlDbType.Int);
        param[1].Value = Combdreqid;
        param[2] = new SqlParameter("@Deptreqid", SqlDbType.Int);
        param[2].Value = Deptreqid;
        param[3] = new SqlParameter("@DeptName", SqlDbType.VarChar);
        param[3].Value = DeptName;
        param[4] = new SqlParameter("@DeptCode", SqlDbType.VarChar);
        param[4].Value = DeptCode;

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
    public int DeleteOnUpdateCombd(string applid, string DeptReqId)
    {
        str = @"delete from CombdCandDeptDetails where applid = @applid and DeptReqId=@DeptReqId";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        param[j].Value = DeptReqId;
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
    public DataTable GetCandSelectedDept(string Combdreqid, string applid)
    {
        str = @"select DepartmentName,DeptReqId from CombdCandDeptDetails where CombdReqId=@Combdreqid and applid=@applid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Combdreqid", SqlDbType.Int);
        param[0].Value = Combdreqid;
        param[1] = new SqlParameter("@applid", SqlDbType.Int);
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

    public DataTable check_detail(string applid)
    {
        str = @"select  distinct DeptReqId from CombdCandDeptDetails where applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
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

    public DataTable GetCombdCand_Education(string applid, string reqid, string qtype, string stan_type)
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (tr.qtype='E' or tr.qtype='G') ";
        }
        else
        {
            condn = " and tr.qtype='E' ";
        }
        string str = @"select distinct applid,sm.standard as stnd,standard,tr.uid,te.name,tr.groupno
                    from standardMaster sm 
					 left outer join tbledu te on te.stid=sm.id
                    left outer join  tbledu_TRN tr on tr.id = te.id                    
					inner join CombdCandDeptDetails comdept on comdept.DeptReqId=tr.reqid
                    where  applid=@applid and tr.reqid=@reqid  ";
        if (qtype != "")
        {
            str += condn;
        }
        if (stan_type != "")
        {
            str += " and sm.type='G'";
        }
        str += " order by standard";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);
        param[1] = new SqlParameter("@reqid", SqlDbType.Int);
        param[1].Value = Int32.Parse(reqid);

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
    public DataTable get_sub_ph_combd(string phcat_code, int reqid)//21/09/2023
    {
        str = @"SELECT distinct phsm.phsubcatid,PH_CatCode,PH_SubCatCode,PH_SubCatCodeDesc
                FROM PHSubCatMaster phsm inner join SubSubSplCatVacancies vac on phsm.phsubcatid=vac.phsubcatid where PH_CatCode=@phcat_code and reqid in (select DeptReqId from CombinedEntry where CombdReqid=@reqid)";

        string str1 = @"SELECT distinct phsm.phsubcatid,PH_CatCode,PH_SubCatCode,PH_SubCatCodeDesc
                FROM PHSubCatMaster phsm inner join SubSubSplCatVacancies vac on phsm.phsubcatid=vac.phsubcatid where PH_CatCode=@phcat_code and reqid = @reqid";

        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@phcat_code", SqlDbType.VarChar, 50);
        param[j].Value = phcat_code;
        j++;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        param[j].Value = reqid;

        SqlParameter[] param1 = new SqlParameter[2];
        int k = 0;
        param1[k] = new SqlParameter("@phcat_code", SqlDbType.VarChar, 50);
        param1[k].Value = phcat_code;
        k++;
        param1[k] = new SqlParameter("@reqid", SqlDbType.Int);
        param1[k].Value = reqid;
        try
        {
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
    //**************************************************************************
    public DataTable getcombdgroupquali(string reqid) // For Combined 31/08/2023
    {

        //str = @"select etrn.id,name,groupno from tbledu_TRN etrn inner join tbledu edu on etrn.id=edu.id where qtype='G' and reqid=@reqid order by groupno";
        str = @"select distinct etrn.id,name,ISNULL(groupno ,0) as groupno from tbledu_TRN etrn inner join tbledu edu on etrn.id=edu.id where -- qtype='G' and 
                reqid in (select DeptReqId from CombdCandDeptDetails where DeptReqId = @reqid) order by groupno";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
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

    public DataTable fill_standard_combd(string reqid, string qtype, string groupno) // For Combined 31/08/2023
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        if (groupno != "" && groupno != "0")
        {
            condn += " and groupno=@groupno ";
        }
        str = "select ID,standard from standardMaster where  ID in (select stid from tbledu where id in (select id from tbledu_TRN where reqid in (select DeptReqId from CombdCandDeptDetails where DeptReqId = @reqid " + condn + @" )))";
        // str = "select ID,standard from standardMaster where ID in (select stid from tbledu where id in (select id from tbledu_TRN where reqid=@reqid and qtype=@qtype))";


        int j = 0;
        SqlParameter[] param = new SqlParameter[3];
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        param[j].Value = Int32.Parse(reqid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno != "")
        {
            param[j].Value = Int32.Parse(groupno);
        }
        else
        {
            param[j].Value = System.DBNull.Value;
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


    public DataTable fill_standard_check_comb(string reqid, string qtype, List<int> groupno) // For Combined 31/08/2023
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        //if (groupno.ToString() != "")
        if (groupno.Count > 0)
        {
            //condn += " and groupno=@groupno ";
            condn += " and  groupno in(" + (groupno.Count > 0 ? string.Join(",", groupno.ToArray()) : System.DBNull.Value.ToString()) + ") ";
        }
        else
        {


            //condn += " and groupno=@groupno
        }

        str = "select ID,standard from standardMaster where type='G' and ID in (select stid from tbledu where id in (select id from tbledu_TRN  where reqid in (select DeptReqId from CombdCandDeptDetails where DeptReqId = @reqid " + condn + @" )))";
        // str = "select ID,standard from standardMaster where ID in (select stid from tbledu where id in (select id from tbledu_TRN where reqid=@reqid and qtype=@qtype))";


        int j = 0;
        SqlParameter[] param = new SqlParameter[2];
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        param[j].Value = Int32.Parse(reqid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        //param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        //if (groupno != "")
        //{
        //    param[j].Value = Int32.Parse(groupno);
        //}
        //else
        //{
        //    param[j].Value = System.DBNull.Value;
        //}
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

    public int GetCombdreqid(string reqid)//31-08-2023
    {
        string str = @"	select distinct CombdReqid from CombinedEntry where CombdReqid = @reqid ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[0].Value = reqid;
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
    public DataTable GetEducationMinimumClass_Combd(string reqid, string standard, string qtype, string groupno)//31-08-2023
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        str = @"SELECT distinct uid,name,reqid,tbledu_trn.id,groupno from tbledu_TRN 
        inner join tbledu on tbledu_trn.id=tbledu.id 
        where reqid in (select DeptReqId from CombdCandDeptDetails where DeptReqId = @reqid) ";
        if (qtype != "")
        {
            str += condn;
        }

        if (standard != "")
        {
            str += " and stid=@stid";
            //groupno = "";
        }
        if (groupno != "")
        {
            //str += " and groupno=@groupno and stid <>'7' ";
            //  str += " and stid <>'7' ";
            str += " and groupno=@groupno and stid <>'7'";
        }
        // str += " order by name";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@stid", SqlDbType.Int);
        if (standard == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (qtype == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = qtype;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
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
    public DataTable GetCombdDepart(string reqid, string applid)//05-09-2023
    {
        string str = @"select distinct DepartmentName + '('+JobTitle+')' as DepartmentName ,DeptReqId from CombdCandDeptDetails ccd inner join dept_job_request djr on djr.reqid=ccd.DeptReqId where CombdReqId=@reqid and ccd.applid=@applid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[0].Value = reqid;
        param[1] = new SqlParameter("@applid", SqlDbType.VarChar, 50);
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
    public DataTable CheckReopenCombdPostOnUpdateApplication(string reqid, string CatCode)//25/08/2023
    {
        //str = @"select * from tbl_CatWiseVacancy where reqid=@reqid and CatCode=@CatCode and CatOrSub='S'";
        str = @"SELECT distinct subcat_master.id,SubCat_code,SubCat_name from subcat_master
                inner join RR_age_relax on RR_age_relax.CatCode=SubCat_Master.SubCat_code
                where RR_age_relax.CatIndCS='S' and reqid in(select reqid from CombinedEntry where CombdReqid=@reqid) and SubCat_code=@CatCode";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@reqid", SqlDbType.Int, 4);
        param[0].Value = reqid;
        param[1] = new SqlParameter("@CatCode", SqlDbType.Char, 4);
        param[1].Value = CatCode;
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

    public DataTable GetComb_Groupexp_ForCheckbox(string expreqid, List<int> groupno) // 11/09/2023
    {
        string condn = "";
        if (groupno.Count > 0)
        {
            //condn += " and groupno=@groupno ";
            condn += " and  etrn.groupno in(" + (groupno.Count > 0 ? string.Join(",", groupno.ToArray()) : System.DBNull.Value.ToString()) + ") ";
        }
        else
        {
            //condn += " and groupno=@groupno
        }

        str = @"select etrn.reqid,exp_noofyears,qexp.essential_exp  
                from tbledu_TRN etrn inner join QExpMaster qexp on etrn.groupno=qexp.groupno and etrn.reqid=qexp.reqid
                where etrn.reqid=@expreqid" + condn + @" ";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@expreqid", SqlDbType.Int);
        if (expreqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = expreqid;
        }
        j++;
        //param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        //if (groupno == "")
        //{
        //    param[j].Value = System.DBNull.Value;
        //}
        //else
        //{
        //    param[j].Value = groupno;
        //}
        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count == 0)
            {
                str = @"select desire_exp,essential_exp,exp_noofyears from dept_job_request where reqid = @expreqid";

                SqlParameter[] param1 = new SqlParameter[1];
                int k = 0;
                param1[k] = new SqlParameter("@expreqid", SqlDbType.Int);
                if (expreqid == "")
                {
                    param1[k].Value = System.DBNull.Value;
                }
                else
                {
                    param1[k].Value = expreqid;
                }
                k++;
                try
                {
                    dt = da.GetDataTableQry(str, param1);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return dt;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetComb_DesireExp(string expreqid) // 11/09/2023
    {
        str = @"select desire_exp from dept_job_request where reqid = @expreqid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@expreqid", SqlDbType.Int);
        if (expreqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = expreqid;
        }
        j++;
        try
        {
            string exp = da.ExecScaler(str, param);
            return exp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getedu(string applid, string groupno, string DeptReqId, int v_qid)//25/09/2023
    {
        string query;
        query = @"select * from Jobeducation where applid = @applid and DeptReqId = @DeptReqId and groupno=@groupno and qid=@qid";
        //query= @"Delete from Jobeducation where applid = @applid and DeptReqId = @DeptReqId and groupno not in (@groupno)";
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[0].Value = 0;
        }
        else
        {
            param[0].Value = Int32.Parse(applid);
        }
        param[1] = new SqlParameter("@DeptReqId", SqlDbType.VarChar);
        if (DeptReqId == "")
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = DeptReqId;
        }
        param[2] = new SqlParameter("@groupno", SqlDbType.VarChar);
        if (groupno == "")
        {
            param[2].Value = System.DBNull.Value;
        }
        else
        {
            param[2].Value = groupno;
        }
        param[3] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[3].Value = 0;
        }
        else
        {
            param[3].Value = v_qid;
        }
        dt = da.GetDataTableQry(query, param);
        return dt;
    }
    public DataTable geteduforcheck(string applid, string DeptReqId, string groupno)//25/09/2023
    {
        string query;
        query = @"select * from Jobeducation where applid = @applid and DeptReqId = @DeptReqId and groupno=@groupno and qid=@qid";
        //query= @"Delete from Jobeducation where applid = @applid and DeptReqId = @DeptReqId and groupno not in (@groupno)";
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[0].Value = 0;
        }
        else
        {
            param[0].Value = Int32.Parse(applid);
        }
        param[1] = new SqlParameter("@DeptReqId", SqlDbType.VarChar);
        if (DeptReqId == "")
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = DeptReqId;
        }
        param[2] = new SqlParameter("@groupno", SqlDbType.VarChar);
        if (groupno == "")
        {
            param[2].Value = System.DBNull.Value;
        }
        else
        {
            param[2].Value = groupno;
        }
        dt = da.GetDataTableQry(query, param);
        return dt;
    }
    public int delete_edu(string applid, string groupno, string DeptReqId)//25/09/2023
    {
        string query;
        //query = @"select * from Jobeducation where applid = @applid and DeptReqId = @DeptReqId and groupno=@groupno";
        query = @"Delete from Jobeducation where applid = @applid and DeptReqId = @DeptReqId and groupno not in (@groupno)";
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        if (applid == "")
        {
            param[0].Value = 0;
        }
        else
        {
            param[0].Value = Int32.Parse(applid);
        }
        param[1] = new SqlParameter("@DeptReqId", SqlDbType.VarChar);
        if (DeptReqId == "")
        {
            param[1].Value = System.DBNull.Value;
        }
        else
        {
            param[1].Value = DeptReqId;
        }
        param[2] = new SqlParameter("@groupno", SqlDbType.VarChar);
        if (groupno == "")
        {
            param[2].Value = System.DBNull.Value;
        }
        else
        {
            param[2].Value = groupno;
        }

        int delete = da.ExecuteParameterizedQuery(query, param);
        return delete;
    }
    public int updateCombdJobApplication_ED(string applid, int v_qid, float percentage, string board, string state, string year, int standard, string extraquli, string month, string DeptReqId, string groupno)//12/09/2023
    {
        int j = 0;
        string qy;

        qy = @"update JobEducation  set percentage=@percentage,board=@board,state=@state,year=@year,standard=@stand,Extraquli=@extraquli,month=@month
              where DeptReqId=@DeptReqId and groupno=@groupno and qid=@qid and applid=@applid";
        SqlParameter[] param = new SqlParameter[11];
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
        param[j] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@percentage", SqlDbType.Decimal);
        if (percentage == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = percentage;
        }
        j++;
        param[j] = new SqlParameter("@board", SqlDbType.VarChar);
        if (board == "" || board == null)
        {
            param[j].Value = "Delhi";
        }
        else
        {
            param[j].Value = board;
        }
        j++;
        param[j] = new SqlParameter("@state", SqlDbType.VarChar);
        if (state == "" || state == null)
        {
            param[j].Value = "7";
        }
        else
        {
            param[j].Value = state;
        }
        j++;
        param[j] = new SqlParameter("@year", SqlDbType.VarChar);
        if (year == "" || year == null)
        {
            param[j].Value = 2013;
        }
        else
        {
            param[j].Value = year;
        }
        j++;

        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@extraquli", SqlDbType.VarChar);
        if (extraquli == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = extraquli;
        }
        j++;
        param[j] = new SqlParameter("@month", SqlDbType.VarChar);
        if (month == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = month;
        }
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.VarChar);
        if (DeptReqId == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DeptReqId;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.VarChar);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
        }
        try
        {
            int temp = da.ExecuteParameterizedQuery(qy, param);
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int InsertCombdJobApplication_ED(string applid, int v_qid, float percentage, string board, string state, string year, int standard, string extraquli, string month, string DeptReqId, string groupno)//12/09/2023
    {
        int j = 0;
        string qy;

        string data = @" select  ";

        qy = @"insert into JobEducation (applid,qid,percentage,board,state,year,standard,Extraquli,month,DeptReqId,groupno) 
                   values(@applid,@qid,@percentage,@board,@state,@year,@stand,@extraquli,@month,@DeptReqId,@groupno)";
        SqlParameter[] param = new SqlParameter[11];
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
        param[j] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@percentage", SqlDbType.Decimal);
        if (percentage == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = percentage;
        }
        j++;
        param[j] = new SqlParameter("@board", SqlDbType.VarChar);
        if (board == "" || board == null)
        {
            param[j].Value = "Delhi";
        }
        else
        {
            param[j].Value = board;
        }
        j++;
        param[j] = new SqlParameter("@state", SqlDbType.VarChar);
        if (state == "" || state == null)
        {
            param[j].Value = "7";
        }
        else
        {
            param[j].Value = state;
        }
        j++;
        param[j] = new SqlParameter("@year", SqlDbType.VarChar);
        if (year == "" || year == null)
        {
            param[j].Value = 2013;
        }
        else
        {
            param[j].Value = year;
        }
        j++;

        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@extraquli", SqlDbType.VarChar);
        if (extraquli == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = extraquli;
        }
        j++;
        param[j] = new SqlParameter("@month", SqlDbType.VarChar);
        if (month == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = month;
        }
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.VarChar);
        if (DeptReqId == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DeptReqId;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.VarChar);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
        }
        try
        {
            int temp = da.ExecuteParameterizedQuery(qy, param);
            return temp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getedudetail(string reqid, string applid)//12-09-2023
    {
        string str = @"select * from JobEducation where DeptReqId = @reqid and applid=@applid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[0].Value = reqid;
        param[1] = new SqlParameter("@applid", SqlDbType.VarChar, 50);
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
    public DataTable GetJobApplicationCombd_Education(string applid, string qtype, string stan_type, string depreqid)//12/09/2023
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (tr.qtype='E' or tr.qtype='G') ";
        }
        else
        {
            condn = " and tr.qtype=@qtype ";
        }
        string str = @"select je.id,je.applid,sm.standard as stnd,je.standard,qid,te.name,Extraquli,percentage,board,ms.code as Stateid,ms.State,month,YEAR,(month + '/' + YEAR) as myear,tr.groupno
                    from JobEducation je 
                    inner join standardMaster sm on je.standard=sm.id 
                    left outer join  tbledu_TRN tr on tr.uid = je.qid 
                    left outer join tbledu te on te.id=tr.id
                    left outer join m_state ms on ms.code=je.state   
                    inner join JobApplication ja on ja.applid =je.applid 
                    inner join Job_Advt jadvt on jadvt.jid=ja.jid
                    inner join AdvMaster adm on adm.adid=jadvt.adid
                    where  je.applid=@applid and je.DeptReqId=@depreqid ";
        if (qtype != "")
        {
            str += condn;
        }
        if (stan_type != "")
        {
            str += " and sm.type=@stype";
        }
        str += " order by standard";
        int j = 0;
        SqlParameter[] param = new SqlParameter[4];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        param[j] = new SqlParameter("@stype", SqlDbType.VarChar);
        param[j].Value = stan_type;
        j++;
        param[j] = new SqlParameter("@depreqid", SqlDbType.VarChar);
        param[j].Value = depreqid;
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
    public DataTable Get_fill_combdreqid(string applid)//12/09/2023
    {

        string str = @"select Job_Advt.reqid,JobTitle,postcode,CONVERT(varchar,EndsOn,103) EndsOn,CONVERT(varchar,Cut_OffDate,103) Cut_OffDate
                        from JobApplication ja inner join Job_Advt on ja.jid = Job_Advt.jid
                        inner join AdvMaster on job_Advt.adid = AdvMaster.adid
                        where applid=@applid";

        //Devesh
        string str1 = @"select dm.deptname  + '('+ja.JobTitle+')' DepartmentName, ja.reqid,CONVERT(varchar,Cut_OffDate,103) Cut_OffDate from dept_job_request djr
                        inner join deptmaster dm on djr.deptcode = dm.deptcode
                        inner join Job_Advt ja on djr.reqid = ja.reqid
                        inner join JobApplication jap on ja.jid  = jap.jid
                        inner join AdvMaster am on ja.adid = am.adid
                        where jap.applid = @applid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);

        SqlParameter[] param1 = new SqlParameter[1];
        param1[0] = new SqlParameter("@applid", SqlDbType.Int);
        param1[0].Value = Int32.Parse(applid);
        try
        {
            dt = da.GetDataTableQry(str, param);
            if(dt.Rows.Count == 0)
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
    public int deleteCombd_Education_full(int applid, string qtype, string DeptReqId)//12/09/2023
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " qtype=@qtype ";
        }
        string str = "delete from JobEducation where applid=@applid and DeptReqId=@DeptReqId and qid in (select uid from tbledu_TRN where " + condn + @")";
        SqlParameter[] param = new SqlParameter[3];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.VarChar);
        param[j].Value = DeptReqId;
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
    public DataTable checkedu(string applid)//12/09/2023
    {
        string str = @"select distinct DepartmentName + '('+JobTitle+')' as DepartmentName,je.DeptReqId as DeptReqId from JobEducation je inner join CombdCandDeptDetails ccd on je.DeptReqId=ccd.DeptReqId 
                       inner join dept_job_request djr on djr.reqid=ccd.DeptReqId where je.applid = @applid";
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
    public DataTable GetCombdLabelJobApplication_Education(string applid) // 13/09/2023
    {
        string str = @" select DepartmentName+ '('+ja.JobTitle+')' as DepartmentName from CombdCandDeptDetails ccd 
                        inner join job_advt ja on ja.reqid = ccd.DeptReqId where applid=@applid";


        //Devesh
        string str1 = @"select dm.deptname  + '('+ja.JobTitle+')' DepartmentName, ja.reqid from dept_job_request djr
                        inner join deptmaster dm on djr.deptcode = dm.deptcode
                        inner join Job_Advt ja on djr.reqid = ja.reqid
                        inner join JobApplication jap on ja.jid  = jap.jid 
                        where jap.applid = @applid";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);

        SqlParameter[] param1 = new SqlParameter[1];
        param1[0] = new SqlParameter("@applid", SqlDbType.Int);
        param1[0].Value = Int32.Parse(applid);
        try
        {
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

    public DataTable GetCombdJobApplication_Education(string applid, string qtype, string stan_type) // 13/09/2023
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (tr.qtype='E' or tr.qtype='G') ";
        }
        else
        {
            condn = " and tr.qtype=@qtype ";
        }

        string str = @"select distinct je.id,ccdd.DepartmentName + '('+djr.JobTitle+')' as DepartmentName,je.deptreqid as deptreqid ,je.applid,sm.standard as stnd,je.standard,qid,te.name,Extraquli,percentage,board,ms.code as Stateid,ms.State,month,YEAR,(month + '/' + YEAR) as myear,tr.groupno
                    from JobEducation je 
                    inner join standardMaster sm on je.standard=sm.id 
                    left outer join  tbledu_TRN tr on tr.uid = je.qid 
                    left outer join tbledu te on te.id=tr.id
                    left outer join m_state ms on ms.code=je.state   
                    inner join JobApplication ja on ja.applid =je.applid 
                    inner join Job_Advt jadvt on jadvt.jid=ja.jid
                    inner join AdvMaster adm on adm.adid=jadvt.adid
					inner join CombdCandDeptDetails ccdd on ccdd.DeptReqId=je.DeptReqId
                    inner join  dept_job_request djr on djr.reqid=ccdd.DeptReqId
                    where  je.applid=@applid ";

        //Devesh
        string str1 = @"select dm.deptname  + '('+ja.JobTitle+')' DepartmentName, ja.reqid,je.applid,sm.standard as stnd,je.standard,qid,te.name,Extraquli,percentage,board,ms.code as Stateid,ms.State,month,YEAR,(month + '/' + YEAR) as myear,tr.groupno
                        from dept_job_request djr
                        inner join deptmaster dm on djr.deptcode = dm.deptcode
                        inner join Job_Advt ja on djr.reqid = ja.reqid
                        inner join JobApplication jap on ja.jid  = jap.jid 
                        inner join JobEducation je on je.applid = jap.applid
                        inner join standardMaster sm on je.standard=sm.id 
                        left outer join  tbledu_TRN tr on tr.uid = je.qid 
                        left outer join tbledu te on te.id=tr.id
                        left outer join m_state ms on ms.code=je.state   
                        inner join AdvMaster adm on adm.adid=ja.adid
                        where jap.applid = @applid";
        if (qtype != "")
        {
            str += condn;
            str1 += condn;
        }
        if (stan_type != "")
        {
            str += " and sm.type=@stype";
            str1 += " and sm.type=@stype";
        }
        str += " order by deptreqid";
        str1 += " order by deptreqid";
        int j = 0;
        SqlParameter[] param = new SqlParameter[3];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param[j].Value = qtype;
        j++;
        param[j] = new SqlParameter("@stype", SqlDbType.VarChar);
        param[j].Value = stan_type;

        SqlParameter[] param1 = new SqlParameter[3];
        param1[0] = new SqlParameter("@applid", SqlDbType.Int);
        param1[0].Value = Int32.Parse(applid);
        param1[1] = new SqlParameter("@qtype", SqlDbType.VarChar);
        param1[1].Value = qtype;
        param1[2] = new SqlParameter("@stype", SqlDbType.VarChar);
        param1[2].Value = stan_type;

        try
        {
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
    public DataTable GetEducationMinimumClass_special_combd(string reqid, string standard, string qtype, string groupno)//22/09/2023
    {
        string condn = "";
        if (qtype == "E")
        {
            condn = " and (qtype='E' or qtype='G') ";
        }
        else
        {
            condn = " and qtype=@qtype ";
        }
        str = @"SELECT standard,uid,name from tbledu_TRN 
                inner join tbledu on tbledu_trn.id=tbledu.id 
                inner join standardMaster on standardMaster.id=tbledu.stid and standardMaster.TYPE='S' 
                where reqid in (select DeptReqId from CombdCandDeptDetails where DeptReqId=@reqid) " + condn;
        //if (qtype=="D")
        //{
        //    str = @"SELECT  standard,uid,name from  tbledu_TRN  inner join tbledu on tbledu_TRN.id =tbledu.id  inner join standardMaster on standardMaster.id=tbledu.stid where tbledu_TRN.reqid= @reqid AND tbledu_TRN.qtype='G'";

        //}

        if (standard != "")
        {
            str += " and stid=@stid";
        }
        if (groupno != "")
        {
            str += " and groupno=@groupno ";
        }
        // str += " order by name";
        SqlParameter[] param = new SqlParameter[4];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        if (reqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = reqid;
        }
        j++;
        param[j] = new SqlParameter("@stid", SqlDbType.Int);
        if (standard == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (qtype == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = qtype;
        }
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        if (groupno == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = groupno;
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
    public int updateJobApplicationCombd_ExpD_special(string applid, int v_eid, string DesirableExperience, string deptreqid, string action)//for combined 10/10/2023
    {
        int i = 0;
        int j = 0;
        string str = "";
        if (action == "insert")
        {
            str = @"insert into jobdesirable_master (applid,desirableExp,DesirableExperience,DeptReqId) values(@applid,@desirableExp,@DesirableExperience,@deptreqid)";
        }
        if (action == "update")
        {
            str = @"update jobdesirable_master set desirableExp =@desirableExp, DesirableExperience = @DesirableExperience where applid = @applid and DeptReqid = @deptreqid";
        }


        SqlParameter[] param = new SqlParameter[4];

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
        param[j] = new SqlParameter("@desirableExp", SqlDbType.Int);
        if (v_eid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_eid;
        }
        j++;
        param[j] = new SqlParameter("@DesirableExperience", SqlDbType.NVarChar);
        if (DesirableExperience == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DesirableExperience;
        }
        j++;
        param[j] = new SqlParameter("@deptreqid", SqlDbType.VarChar);
        if (deptreqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = deptreqid;
        }
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
    public DataTable Getdesirableexpcombd(string applid)//for combined 10/10/2023
    {

        string str = @"select distinct desirableExperience, case
						when desirableExp=1 then'yes' 
					
						when desirableExp=2 Then 'No'
						end as desirableExp
						from jobdesirable_master
                    where  applid=@applid  ";

        str += " order by DeptReqId";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);


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
    public DataTable Getdesirable1_combd(string applid)// for combd 07/11/2023
    {

        string str = @"select distinct desirableExperience, case
						when desirableExp=1 then'yes' 				
						when desirableExp=2 Then 'No'
						end as desirableExp,DepartmentName + '('+djr.JobTitle+')' as DepartmentName
						from jobdesirable_master jdm inner join CombdCandDeptDetails ccdd on ccdd.DeptReqId=jdm.DeptReqId
                        inner join dept_job_request djr on djr.reqid=ccdd.DeptReqId
                    where jdm.applid=@applid and desirableExperience is not null";

        // str += " order by date";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);


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
    public DataTable Getdesirable_combd(string applid)// for combd 04/12/2024
    {
        string str = @"select distinct DesirableQualification, case
						when desirable=1 then'yes' 				
						when desirable=2 Then 'No'
						end as desirable,DepartmentName + '('+djr.JobTitle+')' as DepartmentName
						from jobdesirable_master jdm inner join CombdCandDeptDetails ccdd on ccdd.DeptReqId=jdm.DeptReqId
                        inner join dept_job_request djr on djr.reqid=ccdd.DeptReqId
                        where jdm.applid=@applid and DesirableQualification is not null";

        // str += " order by date";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);


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
    public int delete_edu(string applid, string DeptReqId)//12/09/2023
    {
        string str = @"delete from JobEducation where applid=@applid and DeptReqId=@DeptReqId";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.VarChar);
        param[j].Value = DeptReqId;
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
    public int delete_desireexp(string applid, string DeptReqId)//08/11/2023 //for desire exp
    {
        string str = @"delete from jobdesirable_master where applid=@applid and DeptReqId=@DeptReqId";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.VarChar);
        param[j].Value = DeptReqId;
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
    public DataTable GetDesire_Exp_check(string applid, string DeptReqId)//12/10/2023 -- to check desireable exp
    {
        string str = @"	select distinct desirableExp from jobdesirable_master where applid = @applid and DeptReqId=@DeptReqId";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);
        param[1] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        param[1].Value = Int32.Parse(DeptReqId);
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

    public DataTable GetDesire_Exp(string applid)//12/10/2023
    {
        string str = @"	select distinct DeptReqId from jobdesirable_master where applid = @applid";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);


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

    //new provision to update education detail in advertisement 02/23******* 04/10/2023****//
    public DataTable Get_detail(string rid)
    {
        str = @"select distinct ja.applid as Applid,ja.jid as jid,CONCAT(postcode, ' ', REPLACE(JobTitle, '[dot]', '.')) AS Postcode, CASE 
        WHEN je.applid IS NOT NULL THEN 'Filled'
        ELSE 'Not Filled'
        END AS Status from JobApplication ja inner join Job_Advt jad on jad.jid=ja.jid LEFT JOIN 
        JobEducation je ON ja.applid = je.applid where adid=41 and dummy_no is not null and RegNo=@regno";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar);
        param[0].Value = rid;
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
    //new provision to check experience detail in advertisement 02/23******* 09/10/2023****//
    public DataTable exp_required(string jid)
    {
        str = @"select distinct postcode,JobTitle from Job_Advt where reqid in (select reqid from QExpMaster) and jid=@jid ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@jid", SqlDbType.VarChar);
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
    //new provision to check experience detail in advertisement 02/23******* 09/10/2023****//
    public DataTable check_exp(string applid)
    {
        str = @"SELECT 
    CASE 
        WHEN EXISTS (SELECT 1 FROM JobExperience WHERE applid = @applid) THEN 'Y'
        ELSE 'N'
    END AS expstatus; ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.VarChar);
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
    //19/12/2023
    public int InsertJobApplication_Exp_D_combd(string appid, string post, string datefrom, string dateto, string emp_name, string emp_addr, string emp_contactno, string DeptReqId)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into JobExperience (applid,post,datefrom,dateto,emp_name,emp_addr,emp_contactno,DeptReqId) 
         values(@appid,@post,@datefrom,@dateto,@emp_name,@emp_addr,@emp_contactno,@DeptReqId)";

        SqlParameter[] param = new SqlParameter[8];

        param[j] = new SqlParameter("@appid", SqlDbType.Int);
        if (appid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Convert.ToInt32(appid);
        }
        j++;
        param[j] = new SqlParameter("@post", SqlDbType.VarChar);
        if (post == "" || post == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = post;
        }
        j++;
        param[j] = new SqlParameter("@datefrom", SqlDbType.DateTime);
        if (datefrom == "" || datefrom == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(datefrom);
        }
        j++;
        param[j] = new SqlParameter("@dateto", SqlDbType.DateTime);
        if (dateto == "" || dateto == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(dateto);
        }
        j++;
        param[j] = new SqlParameter("@emp_name", SqlDbType.VarChar);
        if (emp_name == "" || emp_name == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_name;
        }
        j++;
        param[j] = new SqlParameter("@emp_addr", SqlDbType.VarChar);
        if (emp_addr == "" || emp_addr == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_addr;
        }
        j++;
        param[j] = new SqlParameter("@emp_contactno", SqlDbType.VarChar);
        if (emp_contactno == "" || emp_contactno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_contactno;
        }
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        if (DeptReqId == "" || DeptReqId == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DeptReqId;
        }
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
    public int delete_JobApplication_Exp_D_full_combd(int applid, string DeptReqId)
    {
        string str = "delete from JobExperience where applid=@applid";
        SqlParameter[] param = new SqlParameter[2];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        param[j].Value = DeptReqId;

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
    public DataTable GetJobApplication_Exp_Combd(string applid)
    {
        string str = @"select distinct je.id,je.applid,post,convert(varchar,datefrom,103) datefrom ,convert(varchar,dateto,103) dateto,emp_name,emp_contactno,emp_addr,ccd.DepartmentName as DepartmentName
                       from JobExperience je
					   inner join CombdCandDeptDetails ccd on ccd.DeptReqId = je.DeptReqId
                       where je.applid =@applid";
        int j = 0;
        SqlParameter[] param = new SqlParameter[1];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = Int32.Parse(applid);


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
    //19/12/2023
    public string Getdeptcode1(string jid)
    {
        str = @"select deptcode from Job_Advt where jid=@jid";
        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@jid", SqlDbType.VarChar, 50);
        param[j].Value = jid;
        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["deptcode"].ToString().Trim();
            }
            else
            {
                throw new Exception("No deptcode found");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool isApplicationFinal(string applid)
    {
        string q = "select final from jobapplication where applid = @applid and final is not null";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.VarChar);
        param[0].Value = applid;
        try
        {
            DataTable dt = da.GetDataTableQry(q, param);
            return dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int InsertJobApplication_EDcombd_special(string applid, int v_qid, int standard, string DeptReqId)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into JobEducation (applid,qid,standard,DeptReqId,percentage,board,state,month,year) values(@applid,@qid,@stand,@DeptReqId,NULL,NULL,NULL,NULL,NULL)";

        SqlParameter[] param = new SqlParameter[4];

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
        param[j] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        if (DeptReqId == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DeptReqId;
        }
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

    public int DeleteJobApplication_EDcombd_special(string applid, int v_qid, int standard, string DeptReqId)
    {
        int i = 0;
        int j = 0;
        string str = @"delete from JobEducation where qid=@qid and standard=@stand and applid=@applid and DeptReqId=@DeptReqId";

        SqlParameter[] param = new SqlParameter[4];

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
        param[j] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        if (DeptReqId == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DeptReqId;
        }
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
    public int JobApplicationCombd_EduD_special(string applid, int v_eid, string DesirableQualification, string deptreqid, string action)//for combined 10/10/2023
    {
        int i = 0;
        int j = 0;
        string str = "";
        if (action == "insert")
        {
            str = @"insert into jobdesirable_master (applid,desirable,DesirableQualification,DeptReqId) values(@applid,@desirable,@DesirableQualification,@deptreqid)";
        }
        if (action == "update")
        {
            str = @"update jobdesirable_master set desirable = @desirable, DesirableQualification = @DesirableQualification where applid = @applid and DeptReqid = @deptreqid";
        }

        SqlParameter[] param = new SqlParameter[4];

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
        param[j] = new SqlParameter("@desirable", SqlDbType.Int);
        if (v_eid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_eid;
        }
        j++;
        param[j] = new SqlParameter("@DesirableQualification", SqlDbType.NVarChar);
        if (DesirableQualification == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = DesirableQualification;
        }
        j++;
        param[j] = new SqlParameter("@deptreqid", SqlDbType.VarChar);
        if (deptreqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = deptreqid;
        }
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

    public int delete_EssentialeduGen(string applid, string DeptReqId, string standard)//12/09/2023
    {
        string str = @"delete from JobEducation where applid=@applid and DeptReqId=@DeptReqId and standard = @standard";
        SqlParameter[] param = new SqlParameter[3];
        int j = 0;
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        param[j].Value = DeptReqId;
        j++;
        param[j] = new SqlParameter("@standard", SqlDbType.Int);
        param[j].Value = standard;
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
    public DataTable getessentialEduQual(string applid, string DeptReqId, string standard)
    {
        string str = @"select qid from JobEducation where applid = @applid and DeptReqId = @DeptReqId and standard=@standard";
        int j = 0;
        SqlParameter[] param = new SqlParameter[3];
        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;
        param[j] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        param[j].Value = Int32.Parse(DeptReqId);
        j++;
        param[j] = new SqlParameter("@standard", SqlDbType.Int);
        param[j].Value = Int32.Parse(standard);
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
    public string GetComb_DesireEdu(string expreqid) // 11/09/2023
    {
        str = @"select desire_qual from dept_job_request where reqid = @expreqid";

        SqlParameter[] param = new SqlParameter[1];
        int j = 0;
        param[j] = new SqlParameter("@expreqid", SqlDbType.Int);
        if (expreqid == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = expreqid;
        }
        j++;
        try
        {
            string exp = da.ExecScaler(str, param);
            return exp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetDesire_edu_check(string applid, string DeptReqId)//12/10/2023 -- to check desireable education
    {
        string str = @"	select distinct desirable from jobdesirable_master where applid = @applid and DeptReqId=@DeptReqId";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
        param[0].Value = Int32.Parse(applid);
        param[1] = new SqlParameter("@DeptReqId", SqlDbType.Int);
        param[1].Value = Int32.Parse(DeptReqId);
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
    public DataTable getedudetail1(string reqid, string applid)//12-09-2023
    {
        string str = @"select * from JobEducation where DeptReqId = @reqid and applid=@applid and standard != 7";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[0].Value = reqid;
        param[1] = new SqlParameter("@applid", SqlDbType.VarChar, 50);
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
    private RegistrationModel GetRegDetail(string rid)
    {
        string q = "select * from registration where rid = @rid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
        param[0].Value = rid;
        try
        {
            dt = da.GetDataTableQry(q, param);
            if (dt.Rows.Count == 1)
            {
                var dr = dt.Rows[0];
                return new RegistrationModel
                {
                    Name = dr["name"].ToString(),
                    FatherName = dr["fname"].ToString(),
                    MotherName = dr["mothername"].ToString(),
                    MobileNumber = dr["mobileno"].ToString(),
                    Email = dr["email"].ToString()
                };
            }
            else
            {
                throw new Exception(String.Format("zero or more rows found with rid {0}", rid));
            }
        }
        catch (Exception ex)
        {
            //LogException(ex);
            return null;
        }
    }

    public int GetMaxAge(string reqid)
    {
        string q = "select maxage from job_advt where reqid = @reqid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@reqid", SqlDbType.VarChar);
        param[0].Value = reqid;
        try
        {
            dt = da.GetDataTableQry(q, param);
            if (dt.Rows.Count == 1)
            {
                return Convert.ToInt32(dt.Rows[0]["maxage"]);
            }
            return -1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable vacancy(string reqid)//19-03-2023
    {
        string str = @"select * from tbl_CatWiseVacancy where reqid=@reqid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@reqid", SqlDbType.VarChar, 50);
        param[0].Value = reqid;
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



    //19/04/2024 shahid

    public DataTable Get_PCodeDdlList(string rid)
    {
        str = @"SELECT DISTINCT CONCAT(jt.postcode, ' ', REPLACE(jt.JobTitle, '[dot]', '.')) AS Postcode, ja.applid
FROM JobApplication ja
LEFT JOIN JobEducation je ON ja.applid = je.applid
INNER JOIN job_advt jt ON ja.jid = jt.jid
WHERE ja.RegNo = @regno
AND je.applid IS NULL
AND ja.final = 'Y'
AND ja.dummy_no IS NOT NULL";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar);
        param[0].Value = rid;
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


    public DataTable check_Category_Skipped(string ReqId)//Check Category Skipped or not
    {
        string str = @"select * from JobApplication where category = 'Select' and dummy_no is not null and RegNo=@ReqId";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ReqId", SqlDbType.VarChar);
        param[0].Value = ReqId;

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

    public DataTable FillDdlForSkippedCategory(string ReqId)//Check Category Skipped or not
    {
        string str = @"select concat(jt.jobtitle,'(', jt.postcode ,')') as PostName, ja.applid,postcode from JobApplication ja
inner join job_advt jt on ja.jid = jt.jid where category = 'select' and dummy_no is not null and RegNo=@ReqId";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ReqId", SqlDbType.VarChar);
        param[0].Value = ReqId;

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

    public int Update_SkippedCategory(string ctgry, string CLCNo, string CLCDate, string CastCerApplyState, string CertUssueAuth, string applid)
    {
        string str = "update JobApplication set category = @category  , CLCNo = @CLCNo , CLCDate = @CLCDate ,CastCerApplyState = @CastCerApplyState ,CastCertIssueAuth = @CertUssueAuth where applid = @applid ";

        SqlParameter[] param;


        param = new SqlParameter[6];


        param[0] = new SqlParameter("@category", SqlDbType.VarChar, 50);
        param[0].Value = ctgry;

        param[1] = new SqlParameter("@CLCNo", SqlDbType.VarChar, 20);
        param[1].Value = CLCNo;

        param[2] = new SqlParameter("@CLCDate", SqlDbType.DateTime);
        param[2].Value = CLCDate;

        param[3] = new SqlParameter("@CastCerApplyState", SqlDbType.TinyInt);
        param[3].Value = CastCerApplyState;

        param[4] = new SqlParameter("@CertUssueAuth", SqlDbType.VarChar, 50);
        param[4].Value = CertUssueAuth;

        param[5] = new SqlParameter("@applid", SqlDbType.VarChar, 20);
        param[5].Value = applid;


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

    public int Update_SkippedCategory_ForUR(string ctgry, string applid)
    {
        string str = "update JobApplication set category = @category  where applid = @applid ";

        SqlParameter[] param;

        param = new SqlParameter[2];
        param[0] = new SqlParameter("@category", SqlDbType.VarChar, 50);
        param[0].Value = ctgry;
        param[1] = new SqlParameter("@applid", SqlDbType.VarChar, 20);
        param[1].Value = applid;


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

    public int Insert_SkippedCategory(
    int applid, string regId, string category, string clcNo, string clcDate, string CertIssueAuthority, string CastCerApplyState)
    {
        int j = 0;
        string str = @"INSERT INTO [dbo].[Updated_Category]([applid],[RegNo],[category],[CastCerApplyState],[CLCNo],[CLCDate],[CastCertIssueAuth],[UpdatedDate]) VALUES(@applid,@RegNo,@category,@CastCerApplyState,@clcNo,@CLCDate,@CertIssueAuthority,getdate())";
        SqlParameter[] param = new SqlParameter[7];

        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;

        param[j] = new SqlParameter("@RegNo", SqlDbType.VarChar, 50); // Adjust the size as needed
        param[j].Value = regId;

        j++;

        param[j] = new SqlParameter("@category", SqlDbType.VarChar, 50); // Adjust the size as needed
        param[j].Value = category;

        j++;

        param[j] = new SqlParameter("@CastCerApplyState", SqlDbType.TinyInt, 50); // Adjust the size as needed
        param[j].Value = CastCerApplyState;

        j++;

        param[j] = new SqlParameter("@clcNo", SqlDbType.VarChar, 50); // Adjust the size as needed
        param[j].Value = clcNo;

        j++;

        param[j] = new SqlParameter("@clcDate", SqlDbType.DateTime, 50); // Adjust the size as needed
        param[j].Value = clcDate;

        j++;

        param[j] = new SqlParameter("@CertIssueAuthority", SqlDbType.VarChar, 50); // Adjust the size as needed
        param[j].Value = CertIssueAuthority;

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




    public int Insert_SkippedCategory_For_UR(
   int applid, string regId, string category)
    {
        int j = 0;
        string str = @"INSERT INTO [dbo].[Updated_Category]([applid],[RegNo],[category],[UpdatedDate]) VALUES(@applid,@RegNo,@category,getdate())";
        SqlParameter[] param = new SqlParameter[3];

        param[j] = new SqlParameter("@applid", SqlDbType.Int);
        param[j].Value = applid;
        j++;

        param[j] = new SqlParameter("@RegNo", SqlDbType.VarChar, 50); // Adjust the size as needed
        param[j].Value = regId;

        j++;

        param[j] = new SqlParameter("@category", SqlDbType.VarChar, 50); // Adjust the size as needed
        param[j].Value = category;

       

      
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

    public DataTable CheckSkipSubCategory(string rid) // Devesh
    {
        string str = @"select applid from JobApplication jap inner join Job_Advt ja on ja.jid=jap.jid 
                inner join AdvMaster adm on adm.adid=ja.adid 
                where jap.applid not in (select applid from JapplicantScat) and jap.applid in (select applid from PhCertifiacteDetail) 
                and dummy_no is not null and regno = @rid";
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

    public DataTable SkippedSubCatDDL(string rid) // Devesh
    {
        string str = @"select concat(jt.jobtitle,'(', jt.postcode ,')') as PostName, ja.applid,postcode, js.SubCat_code from JobApplication ja
                        inner join job_advt jt on ja.jid = jt.jid
                        left outer join JapplicantScat js on ja.applid = js.applid
                        where js.SubCat_code is null and 
                        dummy_no is not null and RegNo=@rid";
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

    public DataTable CheckSubCatSkipped(string rid) // Devesh
    {
        string str = @"select jt.reqid,ja.* from JobApplication ja
                        inner join job_advt jt on ja.jid = jt.jid
                        left outer join JapplicantScat js on ja.applid = js.applid
                        where js.SubCat_code is null and 
                        dummy_no is not null and RegNo=@rid";
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

    public DataTable SkipSubCatReqid(int applid) // Devesh
    {
        string str = @"select jt.reqid,ja.jid,ja.applid from JobApplication ja
                        inner join job_advt jt on ja.jid = jt.jid
                        left outer join JapplicantScat js on ja.applid = js.applid
                        where js.SubCat_code is null and 
                        dummy_no is not null and ja.applid=@applid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int);
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

    public int UpdateSkipSubCatPH(int applid, string SubCat_code, string regno, string IP, string PHsubCate, string ph_visual, string ph_hearing, string ph_ortho,
                                    string PHIssuingauthority, string PHcertifNo, string PhcertIssuedate, Byte[] PHFile, int PHIssueState, string jid)
    {
        int JScatID = 0;
        int JSScatID = 0;
        SqlCommand command = new SqlCommand();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString_RO"]);
        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    command.Transaction = transaction;
                    string[] a = { "," };
                    string strscat = " insert into JapplicantScat (applid,SubCat_code,edate,userid,ipaddress) values (@applid,@SubCat_code,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";

                    SqlParameter[] param2 = new SqlParameter[4];


                    param2[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                    param2[0].Value = applid;

                    param2[1] = new SqlParameter("@SubCat_code", SqlDbType.VarChar, 4);
                    param2[1].Value = SubCat_code;

                    param2[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                    param2[2].Value = regno;

                    param2[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                    param2[3].Value = IP;

                    command.CommandType = CommandType.Text;
                    command.CommandText = strscat;
                    command.Parameters.Clear();


                    if (param2 != null)
                    {
                        foreach (SqlParameter param3 in param2)
                        {
                            command.Parameters.Add(param3);
                        }

                    }
                    command.Connection = connection;
                    command.Transaction = transaction;
                    JScatID = Convert.ToInt32(command.ExecuteScalar());

                    //PH Certificate Upload

                    string str21 = @"select * from  PhCertifiacteDetail where applid = @applid";
                    SqlParameter[] param21 = new SqlParameter[1];
                    param21[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                    param21[0].Value = applid;
                    DataTable dt1 = da.GetDataTableQry(str21, param21);
                    if (dt1.Rows.Count == 0)
                    {
                        string strPHC = @" INSERT INTO PhCertifiacteDetail (applid,jid,RegNo,PhSubcat ,PhCertiNo ,PhCertIssueAuth ,PhCertIssueDate,PhIssueState,PhCertificateFile,IPAddress,Entrydate)VALUES
                        (@applid,@jid, @RegNo,@PhSubcat,@PhCertiNo,@PhCertIssueAuth,@PhCertIssueDate,@PhIssueState, @PhCertificateFile, @IPAddress, getdate())";
                        SqlParameter[] paramPHC = new SqlParameter[10];


                        paramPHC[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                        paramPHC[0].Value = applid;

                        paramPHC[1] = new SqlParameter("@jid", SqlDbType.Int, 10);
                        paramPHC[1].Value = jid;


                        paramPHC[2] = new SqlParameter("@RegNo", SqlDbType.VarChar, 50);
                        paramPHC[2].Value = regno;

                        paramPHC[3] = new SqlParameter("@PhSubcat", SqlDbType.VarChar, 10);
                        paramPHC[3].Value = SubCat_code;


                        paramPHC[4] = new SqlParameter("@PhCertiNo", SqlDbType.VarChar, 20);
                        paramPHC[4].Value = PHcertifNo;

                        paramPHC[5] = new SqlParameter("@PhCertIssueAuth", SqlDbType.VarChar, 50);
                        paramPHC[5].Value = PHIssuingauthority;

                        paramPHC[6] = new SqlParameter("@PhCertIssueDate", SqlDbType.DateTime, 8);
                        paramPHC[6].Value = Utility.formatDate(PhcertIssuedate);

                        paramPHC[7] = new SqlParameter("@PhIssueState", SqlDbType.Int, 4);
                        if (PHIssueState == null)
                        {
                            paramPHC[7].Value = System.DBNull.Value;
                        }
                        else
                        {
                            paramPHC[7].Value = PHIssueState;
                        }


                        paramPHC[8] = new SqlParameter("@PhCertificateFile", SqlDbType.Image, PHFile.Length);
                        paramPHC[8].Value = PHFile;

                        paramPHC[9] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
                        paramPHC[9].Value = IP;


                        command.CommandType = CommandType.Text;
                        command.CommandText = strPHC;
                        command.Parameters.Clear();


                        if (paramPHC != null)
                        {
                            foreach (SqlParameter param3PHC in paramPHC)
                            {
                                command.Parameters.Add(param3PHC);
                            }

                        }
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        string strPHC = @" update PhCertifiacteDetail set PhSubcat = @PhSubcat ,PhCertiNo = @PhCertiNo ,PhCertIssueAuth = @PhCertIssueAuth,PhCertIssueDate = @PhCertIssueDate,PhIssueState = @PhIssueState,PhCertificateFile = @PhCertificateFile,IPAddress = @IPAddress,Entrydate = getdate() where applid = @applid";
                        SqlParameter[] paramPHC = new SqlParameter[8];
                        paramPHC[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
                        paramPHC[0].Value = applid;
                        paramPHC[1] = new SqlParameter("@PhSubcat", SqlDbType.VarChar, 10);
                        paramPHC[1].Value = SubCat_code;
                        paramPHC[2] = new SqlParameter("@PhCertiNo", SqlDbType.VarChar, 20);
                        paramPHC[2].Value = PHcertifNo;
                        paramPHC[3] = new SqlParameter("@PhCertIssueAuth", SqlDbType.VarChar, 50);
                        paramPHC[3].Value = PHIssuingauthority;
                        paramPHC[4] = new SqlParameter("@PhCertIssueDate", SqlDbType.DateTime, 8);
                        paramPHC[4].Value = Utility.formatDate(PhcertIssuedate);
                        paramPHC[5] = new SqlParameter("@PhIssueState", SqlDbType.Int, 4);
                        if (PHIssueState == null)
                        {
                            paramPHC[5].Value = System.DBNull.Value;
                        }
                        else
                        {
                            paramPHC[5].Value = PHIssueState;
                        }
                        paramPHC[6] = new SqlParameter("@PhCertificateFile", SqlDbType.Image, PHFile.Length);
                        paramPHC[6].Value = PHFile;
                        paramPHC[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50);
                        paramPHC[7].Value = IP;
                        command.CommandType = CommandType.Text;
                        command.CommandText = strPHC;
                        command.Parameters.Clear();
                        if (paramPHC != null)
                        {
                            foreach (SqlParameter param3PHC in paramPHC)
                            {
                                command.Parameters.Add(param3PHC);
                            }

                        }
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }

                    if (!string.IsNullOrEmpty(PHsubCate) && SubCat_code == "PH")
                    {
                        string SScatid = "";
                        string[] subsubcat = PHsubCate.Split(a, StringSplitOptions.RemoveEmptyEntries);
                        for (int k = 0; k < subsubcat.Length; k++)
                        {
                            SScatid = subsubcat[k];
                            str = " insert into JapplicantSScat (JScatID,SScatid,edate,userid,ipaddress) values (@JScatID,@SScatid,getdate(),@userid,@ipaddress) Select SCOPE_IDENTITY()";

                            SqlParameter[] param4 = new SqlParameter[4];


                            param4[0] = new SqlParameter("@JScatID", SqlDbType.Int, 4);
                            param4[0].Value = JScatID;

                            param4[1] = new SqlParameter("@SScatid", SqlDbType.Int, 4);
                            param4[1].Value = SScatid;

                            param4[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                            param4[2].Value = regno;

                            param4[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                            param4[3].Value = IP;

                            command.CommandType = CommandType.Text;
                            command.CommandText = str;
                            command.Parameters.Clear();


                            if (param4 != null)
                            {
                                foreach (SqlParameter param5 in param4)
                                {
                                    command.Parameters.Add(param5);
                                }

                            }
                            command.Transaction = transaction;
                            JSScatID = Convert.ToInt32(command.ExecuteScalar());

                            string SSScatid = "";
                            if (!string.IsNullOrEmpty(ph_visual))
                            {
                                SSScatid = ph_visual;
                            }
                            else if (!string.IsNullOrEmpty(ph_hearing))
                            {
                                SSScatid = ph_hearing;
                            }
                            else if (!string.IsNullOrEmpty(ph_ortho))
                            {
                                SSScatid = ph_ortho;
                            }
                            if (SSScatid != "")
                            {
                                string str1 = " insert into JapplicantSSScat (JSScatID,SSScatid,edate,userid,ipaddress) values (@JSScatID,@SSScatid,getdate(),@userid,@ipaddress)";

                                SqlParameter[] param6 = new SqlParameter[4];


                                param6[0] = new SqlParameter("@JSScatID", SqlDbType.Int, 4);
                                param6[0].Value = JSScatID;

                                param6[1] = new SqlParameter("@SSScatid", SqlDbType.Int, 4);
                                param6[1].Value = SSScatid;

                                param6[2] = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                                param6[2].Value = regno;

                                param6[3] = new SqlParameter("@ipaddress", SqlDbType.VarChar, 50);
                                param6[3].Value = IP;

                                command.CommandType = CommandType.Text;
                                command.CommandText = str1;
                                command.Parameters.Clear();


                                if (param6 != null)
                                {
                                    foreach (SqlParameter param7 in param6)
                                    {
                                        command.Parameters.Add(param7);
                                    }

                                }
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    //}
                    //}

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
                if (JScatID == 0)
                {
                    transaction.Rollback();
                }
            }

        }
        return JScatID;

    }

    public DataTable GetPHDocId(int applid)
    {
        string str = @"select id from PhCertifiacteDetail where applid = @applid";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[0].Value = applid;
        DataTable dt = da.GetDataTableQry(str, param);
        return dt;
    }

    public int InsertSubCatAudit(int applid, string regno, string SubCat_code, int Id, string IP)
    {
        string str = @"Insert into Updated_SubCategoryLog (applid,regno,Sub_Category,DocId,UpdateDate,IpAddress) values (@applid, @regno, @SubCat_code, @id,getdate(), @ip)";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@applid", SqlDbType.VarChar);
        param[0].Value = applid;
        param[1] = new SqlParameter("@regno", SqlDbType.VarChar);
        param[1].Value = regno;
        param[2] = new SqlParameter("@SubCat_code", SqlDbType.VarChar);
        param[2].Value = SubCat_code;
        param[3] = new SqlParameter("@id", SqlDbType.VarChar);
        param[3].Value = Id;
        param[4] = new SqlParameter("@ip", SqlDbType.VarChar);
        param[4].Value = IP;

        return da.ExecuteParameterizedQuery(str, param);

    }

    public int Insert_EducationUpdateLog(string applid, int v_qid, float percentage, string board, string state, string year, int standard, string extraquli, string month)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into EducationUpdate_Log (applid,qid,percentage,board,state,year,standard,Extraquli,month,Update_date) 
         values(@applid,@qid,@percentage,@board,@state,@year,@stand,@extraquli,@month,getdate())";

        SqlParameter[] param = new SqlParameter[9];

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
        param[j] = new SqlParameter("@qid", SqlDbType.NVarChar);
        if (v_qid == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = v_qid;
        }
        j++;
        param[j] = new SqlParameter("@percentage", SqlDbType.Decimal);
        if (percentage == 0)
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = percentage;
        }
        j++;
        param[j] = new SqlParameter("@board", SqlDbType.VarChar);
        if (board == "" || board == null)
        {
            param[j].Value = "Delhi";
        }
        else
        {
            param[j].Value = board;
        }
        j++;
        param[j] = new SqlParameter("@state", SqlDbType.VarChar);
        if (state == "" || state == null)
        {
            param[j].Value = "7";
        }
        else
        {
            param[j].Value = state;
        }
        j++;
        param[j] = new SqlParameter("@year", SqlDbType.VarChar);
        if (year == "" || year == null)
        {
            param[j].Value = 2013;
        }
        else
        {
            param[j].Value = year;
        }
        j++;

        param[j] = new SqlParameter("@stand", SqlDbType.Int);
        if (standard == 0)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = standard;
        }
        j++;
        param[j] = new SqlParameter("@extraquli", SqlDbType.VarChar);
        if (extraquli == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = extraquli;
        }
        j++;
        param[j] = new SqlParameter("@month", SqlDbType.VarChar);
        if (month == "")
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = month;
        }



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

    public int InsertExpUpdate_Log(string appid, string post, string datefrom, string dateto, string emp_name, string emp_addr, string emp_contactno)
    {
        int i = 0;
        int j = 0;
        string str = @"insert into JobExperience (applid,post,datefrom,dateto,emp_name,emp_addr,emp_contactno) 
         values(@appid,@post,@datefrom,@dateto,@emp_name,@emp_addr,@emp_contactno)";

        SqlParameter[] param = new SqlParameter[7];

        param[j] = new SqlParameter("@appid", SqlDbType.Int);
        if (appid == "")
        {
            param[j].Value = 0;
        }
        else
        {
            param[j].Value = Convert.ToInt32(appid);
        }
        j++;
        param[j] = new SqlParameter("@post", SqlDbType.VarChar);
        if (post == "" || post == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = post;
        }
        j++;
        param[j] = new SqlParameter("@datefrom", SqlDbType.DateTime);
        if (datefrom == "" || datefrom == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(datefrom);
        }
        j++;
        param[j] = new SqlParameter("@dateto", SqlDbType.DateTime);
        if (dateto == "" || dateto == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = Utility.formatDate(dateto);
        }
        j++;
        param[j] = new SqlParameter("@emp_name", SqlDbType.VarChar);
        if (emp_name == "" || emp_name == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_name;
        }
        j++;
        param[j] = new SqlParameter("@emp_addr", SqlDbType.VarChar);
        if (emp_addr == "" || emp_addr == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_addr;
        }
        j++;
        param[j] = new SqlParameter("@emp_contactno", SqlDbType.VarChar);
        if (emp_contactno == "" || emp_contactno == null)
        {
            param[j].Value = System.DBNull.Value;
        }
        else
        {
            param[j].Value = emp_contactno;
        }

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

    //Devesh => Added stid
    public DataTable Get_Education_Percentage(int reqid, string groupno, string std)
    {
        string str = @"select  distinct tt.id,tt.MinPercent,tt.reqid from tbledu_TRN tt inner join tbledu te on tt.id=te.id where tt.reqid =@reqid  and  groupno in(@groupno) and te.stid = @std";
        SqlParameter[] param = new SqlParameter[3];
        int j = 0;
        param[j] = new SqlParameter("@reqid", SqlDbType.Int);
        param[j].Value = reqid;
        j++;
        param[j] = new SqlParameter("@groupno", SqlDbType.Int);
        param[j].Value = groupno;
        j++;
        param[j] = new SqlParameter("@std", SqlDbType.Int);
        param[j].Value = std;
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








