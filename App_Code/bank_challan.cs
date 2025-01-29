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
/// <summary>
/// Summary description for bank_challan
/// </summary>
public class bank_challan
{
    DataAccess da = new DataAccess();
	public bank_challan()
	{

    }
    public DataTable get_job_application(string appid)
    {
        try
        {
            string str = @"select mobileno from jobapplication where applid=@applid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@applid", SqlDbType.Int, 4);
            param[0].Value = appid;

            DataTable dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int insert_bankdata(string jobsourceid, string advtyear, string advtno, string appno)
    {
        string str = "Insert into JobApplicationPayment(jobsourceid,advtyear,advtno,appno) values (@jobsourceid,@advtyear,@advtno,@appno)";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@jobsourceid", SqlDbType.Int);
        param[0].Value = jobsourceid;
        param[1] = new SqlParameter("@advtyear", SqlDbType.Int);
        param[1].Value = advtyear;
        param[2] = new SqlParameter("@advtno", SqlDbType.Int);
        param[2].Value = advtno;
        param[3] = new SqlParameter("@appno", SqlDbType.Int);
        param[3].Value = appno;
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
    public DataTable get_csv_data()
    {

        try
        {
            string str = @"select * from JobApplication";


            DataTable dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable getbankdetails(string type)
    {

        try
        {
//            //string str = @"select isnull(slot,'-')as slot,ISNULL(dateofsending,'NotSentYet')as dateofsending,feeid,applid from feedetails order by [dateofsending] desc";
//            //string str = @"select distinct ISNULL(dateofsending,'NotSentYet')as dateofsending, isnull(slot,'-')as slot from feedetails order by [dateofsending] desc,slot desc";
//            string str = @"select ISNULL(a.dateofsending,'NotSentYet')as dateofsending, isnull(a.slot,'-')as slot,a.count_a total_count,isnull(b.count_b,0) fee_receive_count from 
//                            (select [dateofsending] ,slot,COUNT(*) count_a from feedetails
//                            group by [dateofsending] ,slot ) a
//                            left outer join 
//                            (select [dateofsending] ,slot,COUNT(*) count_b from feedetails
//                            where jrnlno is not null 
//                            group by [dateofsending] ,slot )b
//                            on a.dateofsending=b.dateofsending
//                            and a.slot=b.slot
//                            inner join Job_Advt
//                           order by dateofsending desc,slot desc";

            string str = @" select ISNULL(a.dateofsending,'NotSentYet')as dateofsending, isnull(a.slot,'-')as slot,count(*) as total_count from 
                            feedetails a
                           where a.jid in 
                           (select jid from Job_Advt inner join AdvMaster on Job_Advt.adid=AdvMaster.adid ";
                           
                            
                            
            if (type == "C")
            {
                str += "and FeeLastDate>=GETDATE()";
            }
            if (type == "A")
            {
                str += "and FeeLastDate<GETDATE()";
            }

            str+=@")group by dateofsending ,slot
                           order by dateofsending desc,slot desc ";

            DataTable dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable get_csv_data1(string flag, string date, string slot)
    {

        try
        {

//            string str = @"select right('000000'+convert(varchar(6),(select feetype from bankmaster)),6)
//                            +isnull(right('00000000000000000000'+convert(varchar(20), f.applid), 20), SPACE(20))
//                            + isNULL(replace(convert(varchar,birthdt,103),'/',''),SPACE(8))
//                            + right('00000000000000000'+convert(varchar(17),(select bankaccno from BankMaster)),17)
//                            + right('00000000000000000'+convert(varchar(17),(select feeamount from Job_Source)*1000),17)
//                            +right('00000000000000000'+convert(varchar(5),(select homebranchno from bankmaster)), 5)
//                            + isnull(ltrim(rtrim(SUBSTRING(name,1,30))+SPACE(30-LEN(ltrim(rtrim(SUBSTRING(name,1,30)))))),SPACE(30))
//                            + SPACE(20)
//                            +SPACE(20)
//                            +SPACE(30)
//                            +SPACE(30)
//                            +SPACE(30)
//                            +SPACE(30)
//                            +SPACE(8)
//                            as data from JobApplication j 
//                            inner join  FeeDetails f on j.applid=f.applid ";

            string str = @"select right('000000'+convert(varchar(6),(select feetype from bankmaster)),6)
                        +isnull(right('00000000000000000000'+convert(varchar(20), f.applid), 20), SPACE(20))
                        + isNULL(replace(convert(varchar,birthdt,103),'/',''),SPACE(8))
                        + right('00000000000000000'+convert(varchar(17),(select bankaccno from BankMaster)),17)
                        + right('00000000000000000'+convert(varchar(17),(select feeamount from Job_Source)*1000),17)
                        +right('00000000000000000'+convert(varchar(5),(select homebranchno from bankmaster)), 5)
                        + isnull(SUBSTRING(ltrim(rtrim(name)),1,30)+SPACE(30-LEN(SUBSTRING(ltrim(rtrim(name)),1,30))),SPACE(30))
                        + SPACE(20)
                        +SPACE(20)
                        +SPACE(30)
                        +SPACE(30)
                        +SPACE(30)
                        +SPACE(30)
                        +SPACE(8)
                        as data from JobApplication j 
                        inner join  FeeDetails f on j.applid=f.applid ";

            if (flag == "Y")
            {
                str += " where f.dateofsending is null and slot is null";
            }
            else
            {
                str += " where dateofsending=@date and slot=@slot";
            }
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@date", SqlDbType.VarChar, 50);
            if (date == "")
            {
                param[0].Value = System.DBNull.Value;
            }
            else
            {
                param[0].Value = (date);
            }
            param[1] = new SqlParameter("@slot", SqlDbType.VarChar, 50);
            if (slot == "")
            {
                param[1].Value = System.DBNull.Value;
            }
            else
            {
                param[1].Value = slot;
            }
            DataTable dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable checkdatefeedetails()
    {

        try
        {
            //string str = @"select isnull(slot,'-')as slot,ISNULL(dateofsending,'NotSentYet')as dateofsending,feeid,applid from feedetails order by [dateofsending] desc";
            string str = @"select (dateofsending) from feedetails where dateofsending=@dateofsending";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dateofsending", SqlDbType.VarChar, 50);
            param[0].Value = Utility.formatDate(DateTime.Now);
            DataTable dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getmaxslot(string date)
    {

        try
        {
            string str = @"select max(slot)as slot from feedetails where dateofsending=@date";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@date", SqlDbType.VarChar, 50);
            param[0].Value = date;
            DataTable dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int updatedate(string slot)
    {
        string str = "update feedetails set slot=@slot, dateofsending=@date where dateofsending is null and slot is null ";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@slot", SqlDbType.VarChar, 50);
        param[0].Value = slot;
        param[1] = new SqlParameter("@date", SqlDbType.VarChar, 50);
        param[1].Value = Utility.formatDate(DateTime.Now);
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

    public int insert_fee()
    {
        string str = @"insert into FeeDetails(applid,jid) select applid,jid from JobApplication where dummy_no is not null and applid not in(select applid from FeeDetails)";
        try
        {
            int id = da.ExecuteSql(str);

            return id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


//    public int updatefeedetails(string JRNLNO, string TRANMODE, string FROMACCSYS, string FROMACCTNO, string FEETYPE, string appid, string dob, string TRANDATE, string TRANBRANCH, string TRANTIME, string Accountno, string AMOUNT, string candidatename)
//    {
//        string str = @"update FeeDetails set  jrnlno=@jrnlno, trandate=@trandate, tranbra=@tranbra,
//         transmode=@transmode, fromaccsys=@fromaccsys, fromacc=@fromacc, DOB=@DOB,feerecd='Y',
//        transtime=@transtime, feetype=@feetype, accountno=@accountno, amount=@amount, can_name=@can_name
//        where applid=@applid";


//        SqlParameter[] param = new SqlParameter[13];
//        param[0] = new SqlParameter("@jrnlno", SqlDbType.BigInt);
//        param[0].Value = JRNLNO;
//        param[1] = new SqlParameter("@trandate", SqlDbType.DateTime);
//        param[1].Value = TRANDATE;
//        param[2] = new SqlParameter("@tranbra", SqlDbType.BigInt);
//        param[2].Value = TRANBRANCH;
//        param[3] = new SqlParameter("@applid", SqlDbType.Int, 4);
//        param[3].Value = appid;
//        param[4] = new SqlParameter("@transmode", SqlDbType.VarChar, 20);
//        param[4].Value = TRANMODE;
//        param[5] = new SqlParameter("@fromaccsys", SqlDbType.VarChar, 20);
//        param[5].Value = FROMACCSYS;
//        param[6] = new SqlParameter("@fromacc", SqlDbType.VarChar, 20);
//        param[6].Value = FROMACCTNO;
//        param[7] = new SqlParameter("@DOB", SqlDbType.DateTime, 8);
//        param[7].Value = dob;
//        param[8] = new SqlParameter("@transtime", SqlDbType.VarChar, 10);
//        param[8].Value = TRANTIME;
//        param[9] = new SqlParameter("@feetype", SqlDbType.Char, 6);
//        param[9].Value = FEETYPE;
//        param[10] = new SqlParameter("@accountno", SqlDbType.VarChar, 20);
//        param[10].Value = Accountno;
//        param[11] = new SqlParameter("@amount", SqlDbType.Int, 4);
//        param[11].Value = AMOUNT;
//        param[12] = new SqlParameter("@can_name", SqlDbType.VarChar, 20);
//        param[12].Value = candidatename;
//        try
//        {
//            int id = da.ExecuteParameterizedQuery(str, param);

//            return id;
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }
//    }
    public dict updatefeedetails(string JRNLNO, string TRANMODE, string FROMACCSYS, string FROMACCTNO, string FEETYPE, string appid, string dob, string TRANDATE, string TRANBRANCH, string TRANTIME, string Accountno, string AMOUNT, string candidatename)
    {
        //Dictionary<string, SqlParameter[]> temp = new Dictionary<string, SqlParameter[]>();

        string str = @"update FeeDetails set  jrnlno=@jrnlno, trandate=@trandate, tranbra=@tranbra,
         transmode=@transmode, fromaccsys=@fromaccsys, fromacc=@fromacc, DOB=@DOB,feerecd='Y',
        transtime=@transtime, feetype=@feetype, accountno=@accountno, amount=@amount, can_name=@can_name
        where applid=@applid";


        SqlParameter[] param = new SqlParameter[13];
        param[0] = new SqlParameter("@jrnlno", SqlDbType.BigInt);
        param[0].Value = JRNLNO;
        param[1] = new SqlParameter("@trandate", SqlDbType.DateTime);
        param[1].Value = TRANDATE;
        param[2] = new SqlParameter("@tranbra", SqlDbType.BigInt);
        param[2].Value = TRANBRANCH;
        param[3] = new SqlParameter("@applid", SqlDbType.Int, 4);
        param[3].Value = appid;
        param[4] = new SqlParameter("@transmode", SqlDbType.VarChar, 20);
        param[4].Value = TRANMODE;
        param[5] = new SqlParameter("@fromaccsys", SqlDbType.VarChar, 20);
        param[5].Value = FROMACCSYS;
        param[6] = new SqlParameter("@fromacc", SqlDbType.VarChar, 20);
        param[6].Value = FROMACCTNO;
        param[7] = new SqlParameter("@DOB", SqlDbType.DateTime, 8);
        param[7].Value = dob;
        param[8] = new SqlParameter("@transtime", SqlDbType.VarChar, 10);
        param[8].Value = TRANTIME;
        param[9] = new SqlParameter("@feetype", SqlDbType.Char, 6);
        param[9].Value = FEETYPE;
        param[10] = new SqlParameter("@accountno", SqlDbType.VarChar, 20);
        param[10].Value = Accountno;
        param[11] = new SqlParameter("@amount", SqlDbType.Int, 4);
        param[11].Value = AMOUNT;
        param[12] = new SqlParameter("@can_name", SqlDbType.VarChar, 20);
        param[12].Value = candidatename;

        return new dict { QString=str,param=param };
        //temp.Add(str, param);
        //return temp;

        //try
        //{
        //    int id = da.ExecuteParameterizedQuery(str, param);

        //    return id;
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    public DataTable getjobdeails(string appid)
    {
        try
        {
            string str = @"select fd.jid,fd.applid,postcode,JobTitle,mobileno from FeeDetails fd 
                            inner join Job_Advt ja on fd.jid=ja.jid 
                            inner join JobApplication jap on jap.applid=fd.applid where fd.applid=@appid";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@appid", SqlDbType.Int, 4);
            param[0].Value = appid;

            DataTable dt = da.GetDataTableQry(str, param);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public DataTable get_appid(string appid)
    //{
    //    try
    //    {
    //        //string str = @"select applid from feedetails where applid=@appid";
    //        string str = @"select applid from feedetails";
    //        SqlParameter[] param = new SqlParameter[1];
    //        param[0] = new SqlParameter("@appid", SqlDbType.Int, 4);
    //        param[0].Value = appid;

    //        DataTable dt = da.GetDataTableQry(str, param);
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    public DataTable get_appid()
    {
        try
        {
            //string str = @"select applid from feedetails where applid=@appid";
            string str = @"select applid from feedetails";
            

            DataTable dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getCandidateListforbank(string dateofsending, string slot, string flag)
    {
        try
        {
            string str;
            if (flag == "A")
            {
                str = "SELECT FeeDetails.feeid, FeeDetails.applid, FeeDetails.jid, FeeDetails.dateofsending, FeeDetails.slot, FeeDetails.jrnlno," +
                      " JobApplication.name, JobApplication.RegNo, JobApplication.fname, JobApplication.mobileno, JobApplication.dummy_no" +
                      " FROM FeeDetails LEFT OUTER JOIN" +
                      " JobApplication ON FeeDetails.applid = JobApplication.applid where dateofsending='" + dateofsending + "' and slot='" + slot + "'";
            }
            else if (flag == "Y")
            {
                str = "SELECT FeeDetails.feeid, FeeDetails.applid, FeeDetails.jid, FeeDetails.dateofsending, FeeDetails.slot, FeeDetails.jrnlno," +
                      " JobApplication.name, JobApplication.RegNo, JobApplication.fname, JobApplication.mobileno, JobApplication.dummy_no" +
                      " FROM FeeDetails LEFT OUTER JOIN" +
                      " JobApplication ON FeeDetails.applid = JobApplication.applid where dateofsending='" + dateofsending + "' and slot='" + slot + "' and FeeDetails.jrnlno is not null";
            }
            else
            {
                str = "SELECT FeeDetails.feeid, FeeDetails.applid, FeeDetails.jid, FeeDetails.dateofsending, FeeDetails.slot, FeeDetails.jrnlno," +
                      " JobApplication.name, JobApplication.RegNo, JobApplication.fname, JobApplication.mobileno, JobApplication.dummy_no" +
                      " FROM FeeDetails LEFT OUTER JOIN" +
                      " JobApplication ON FeeDetails.applid = JobApplication.applid where dateofsending='" + dateofsending + "' and slot='" + slot + "' and FeeDetails.jrnlno is null";
            }
            DataTable dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string get_proc_status()
    {
        try
        {
            //string str = @"select applid from feedetails where applid=@appid";
            string str = @"SELECT dblock  FROM Job_Source";


            DataTable dt = da.GetDataTable(str);


            return dt.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
