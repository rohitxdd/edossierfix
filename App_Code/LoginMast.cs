using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization; //26042021

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
        string str = @"select rid,password,initial,name,fname,mothername,gender,convert(varchar,birthdt,103) birthdt,nationality,mobileno,email,spousename,aadharNo,nameOnIDProof,proofOfID,chngpwd_date,rdate 
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
    public DataTable GetUser_regno_details(string regno)
    {
        string str = @"select rid,password,initial,name,fname,mothername,gender,convert(varchar,birthdt,103) birthdt,nationality,mobileno,email,spousename 
        from registration where rid=@rid and active='Y'";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
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

    public int comparepwds(string inputpwd, string dbpwd, string randomno)
    {
        string encptdpwd = MD5Util.md5(dbpwd + randomno);
        if (inputpwd == encptdpwd)
            return 1;
        else
            return 0;
    }


    //    public DataTable IsExist_Applicant(string regno)
    //    {
    //        string qry = @" select rid,name,fname,convert(varchar,birthdt,103) as birthdt from registration where 
    //                        rid=@regno";
    //        SqlParameter[] param = new SqlParameter[1];
    //        int j = 0;
    //        param[j] = new SqlParameter("@regno", SqlDbType.VarChar);
    //        param[j++].Value = regno;
    //        try
    //        {

    //            dt = da.GetDataTableQry(qry, param);
    //            return dt;

    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }


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



    public int insert_registration(string rid, string password, string uid, string name, string fhname, string mothername, string gender, string dob, string nationality, string mobil, string mail, string ip, string active, string rdate, string rollno, string passing_year, string spousename)
    {

        string str = @"insert into registration (rid,password,uid,name,fname,mothername,gender,birthdt,nationality,mobileno,email,um_ipaddress,active,rdate,rollno,passing_year,spousename) 
         values(@rid,@password,@um_logid,@name,@fname,@mothername,@gender,@birthdt,@nationality,@mobileno,@email,@IP,@active,@rdate,@rollno,@passing_year,@spousename)";
        SqlParameter[] param = new SqlParameter[17];
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
        if (fhname == "")
        {
            param[4].Value = System.DBNull.Value;
        }
        else
        {
            param[4].Value = fhname;
        }
        param[5] = new SqlParameter("@mothername", SqlDbType.NVarChar, 50);
        if (mothername == "")
        {
            param[5].Value = System.DBNull.Value;
        }
        else
        {
            param[5].Value = mothername;
        }

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
        param[14].Value = Int64.Parse(rollno);
        param[15] = new SqlParameter("@passing_year", SqlDbType.Int);
        param[15].Value = Int32.Parse(passing_year);
        param[16] = new SqlParameter("@spousename", SqlDbType.VarChar, 50);
        if (spousename == "")
        {
            param[16].Value = System.DBNull.Value;
        }
        else
        {
            param[16].Value = spousename;
        }
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
    public int Inserterrorlog_pag(string username, string regno, string errorname, string pagename, string functionname, string ip, string browsername)
    {
        string InsetAudit = "Insert into mst_errorlog(username,regno,errorname,pagename,functionname,ip,browsername) values(@username,@regno,@errorname,@pagename,@functionname,@ip,@browsername";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@username", SqlDbType.NVarChar);
        param[0].Value = username;
        param[1] = new SqlParameter("@regno", SqlDbType.NVarChar);
        param[1].Value = regno;
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
    public DataTable checkserialno(string regno)
    {
        //string str = @"select serial_no,regno from oldpostmapping where regno=@regno";
string str = @"select serial_no,regno from oldpostmatching where regno=@regno and DocFile is not null"; //Modified on Dated: 16-02-2023 by AnkitaSingh
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar);
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

   
    public int insert_ResetPasswordThruNewMobEmail(string regNo, string requestReferanceNo, string txt_DOB, string txt_roll_no, string ddl_pass_year, string postCode, string txt_name, string txt_fh_name, string txt_mothername, string txtspouse, string txtUid, string txt_mob, string txt_email, byte[] bytesCert10File, byte[] bytesIDProof, byte[] documentGovID2, string rdate, string ip)
    {
        DateTime txtDOBDate = DateTime.ParseExact(txt_DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
        DateTime txtrDate = DateTime.ParseExact(rdate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);

        string str = @"insert into resetPasswordThruNewMobEmail (regNo,requestReferanceNo,dob,rollNo,passingYear,postCode,name,fatherName,motherName,spouseName,uidNumber,newMobile,newEmail,tenthDocument,idProofDoc,entryByCandidatedate,candidateIPAddess,idProofDocSecond) 
         values(@regNo,@requestReferanceNo,@dob,@rollNo,@passingYear,@postCode,@name,@fatherName,@motherName,@spouseName,@uidNumber,@newMobile,@newEmail,@tenthDocument,@idProofDoc,@entryByCandidatedate,@candidateIPAddess,@idProofDocSecond)";
        SqlParameter[] param = new SqlParameter[18];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar, 50);
        param[0].Value = regNo;
        param[1] = new SqlParameter("@dob", SqlDbType.DateTime);
        param[1].Value = txtDOBDate;
        param[2] = new SqlParameter("@rollNo", SqlDbType.VarChar, 50);
        param[2].Value = txt_roll_no;
        param[3] = new SqlParameter("@passingYear", SqlDbType.Int);
        param[3].Value = Int32.Parse(ddl_pass_year);
        param[4] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
        param[4].Value = txt_name;
        param[5] = new SqlParameter("@fatherName", SqlDbType.NVarChar, 50);
        param[5].Value = txt_fh_name;
        param[6] = new SqlParameter("@motherName", SqlDbType.NVarChar, 50);
        param[6].Value = txt_mothername;
        param[7] = new SqlParameter("@spouseName", SqlDbType.NVarChar, 50);
        param[7].Value = txtspouse;
        param[8] = new SqlParameter("@newMobile", SqlDbType.NVarChar, 12);
        param[8].Value = txt_mob;
        param[9] = new SqlParameter("@newEmail", SqlDbType.VarChar, 50);
        param[9].Value = txt_email;
        param[10] = new SqlParameter("@tenthDocument", SqlDbType.VarBinary);
        param[10].Value = bytesCert10File;
        param[11] = new SqlParameter("@idProofDoc", SqlDbType.VarBinary);
        param[11].Value = bytesIDProof;
        param[12] = new SqlParameter("@entryByCandidatedate", SqlDbType.DateTime);
        param[12].Value = txtrDate ;
        param[13] = new SqlParameter("@candidateIPAddess", SqlDbType.NVarChar, 20);
        param[13].Value = ip;
        param[14] = new SqlParameter("@requestReferanceNo", SqlDbType.VarChar, 50);
        param[14].Value = requestReferanceNo;
        param[15] = new SqlParameter("@uidNumber", SqlDbType.VarChar, 50);
        param[15].Value = txtUid;
        param[16] = new SqlParameter("@postCode", SqlDbType.VarChar, 50);
         if (postCode == "" || postCode == null)
        {
            param[16].Value = System.DBNull.Value;
        }
        else
        {
            param[16].Value = postCode;
        }
        param[17] = new SqlParameter("@idProofDocSecond", SqlDbType.VarBinary);
        param[17].Value = documentGovID2;

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

    public DataTable findEntryExistForResetPasswordThruNewMobEmail(string regNo)
    {
        string str = "select top(1)* from resetPasswordThruNewMobEmail where regNo=@regNo  order by entryByCandidatedate desc";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
        param[0].Value = regNo;
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

    public DataTable getRejectedSatusCountEntry(string regNo)
    {
        string str = " select * from resetPasswordThruNewMobEmail where regNo=@regNo and approvalStatus='REJECTED' ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
        param[0].Value = regNo;
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


    public int update_ResetPasswordThruNewMobEmail(string regNo, string requestReferanceNo, string txt_DOB, string txt_roll_no, string ddl_pass_year, string postCode, string txt_name, string txt_fh_name, string txt_mothername, string txtspouse, string txtUid, string txt_mob, string txt_email, byte[] bytesCert10File, byte[] bytesIDProof, string rdate, string ip)
    {
        string str = @"Update resetPasswordThruNewMobEmail set requestReferanceNo=@requestReferanceNo,dob=@dob,rollNo=@rollNo,passingYear=@passingYear,postCode=@postCode,name=@name,
                       fatherName=@fatherName,motherName=@motherName,spouseName=@spouseName,uidNumber=@uidNumber,newMobile=@newMobile,newEmail=@newEmail,tenthDocument=@tenthDocument,
                       idProofDoc=@idProofDoc, entryByCandidatedate=@entryByCandidatedate,candidateIPAddess=@candidateIPAddess where regNo=@regNo";
        SqlParameter[] param = new SqlParameter[17];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar, 50);
        param[0].Value = regNo;
        param[1] = new SqlParameter("@dob", SqlDbType.DateTime);
        param[1].Value = Utility.formatDate(txt_DOB);
        param[2] = new SqlParameter("@rollNo", SqlDbType.VarChar, 50);
        param[2].Value = txt_roll_no;
        param[3] = new SqlParameter("@passingYear", SqlDbType.Int);
        param[3].Value = Int32.Parse(ddl_pass_year);
        param[4] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
        param[4].Value = txt_name;
        param[5] = new SqlParameter("@fatherName", SqlDbType.NVarChar, 50);
        param[5].Value = txt_fh_name;
        param[6] = new SqlParameter("@motherName", SqlDbType.NVarChar, 50);
        param[6].Value = txt_mothername;
        param[7] = new SqlParameter("@spouseName", SqlDbType.NVarChar, 50);
        param[7].Value = txtspouse;
        param[8] = new SqlParameter("@newMobile", SqlDbType.NVarChar, 12);
        param[8].Value = txt_mob;
        param[9] = new SqlParameter("@newEmail", SqlDbType.VarChar, 50);
        param[9].Value = txt_email;
        param[10] = new SqlParameter("@tenthDocument", SqlDbType.VarBinary);
        param[10].Value = bytesCert10File;
        param[11] = new SqlParameter("@idProofDoc", SqlDbType.VarBinary);
        param[11].Value = bytesIDProof;
        param[12] = new SqlParameter("@entryByCandidatedate", SqlDbType.DateTime);
        param[12].Value = rdate;
        param[13] = new SqlParameter("@candidateIPAddess", SqlDbType.NVarChar, 20);
        param[13].Value = ip;
        param[14] = new SqlParameter("@requestReferanceNo", SqlDbType.VarChar, 50);
        param[14].Value = requestReferanceNo;
        param[15] = new SqlParameter("@uidNumber", SqlDbType.VarChar, 50);
        param[15].Value = txtUid;
        param[16] = new SqlParameter("@postCode", SqlDbType.VarChar, 50);
        param[16].Value = postCode;

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
    public DataTable getEntryListForResetPasswordThruNewMobEmail()
    {
        //string str = "select *,CONCAT(requestReferanceNo,'/',cast(entryByCandidatedate as date)) as refNoDateRequest from resetPasswordThruNewMobEmail where approvalStatus is null or approvalStatus ='' order by entryByCandidatedate desc";
        string str = "select *,CONCAT(requestReferanceNo,'/',cast(entryByCandidatedate as date)) as refNoDateRequest from resetPasswordThruNewMobEmail order by entryByCandidatedate desc";
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


    public int updateActionRemarkStatus_ResetPasswordThruNewMobEmail(string regNo, string approvalSubmittedBy, string approvalStatus, string approvalRemark, int otpSend, string reqReferenceno)
    {
        string str = @"Update resetPasswordThruNewMobEmail set approvalSubmittedBy=@approvalSubmittedBy,approvalSubmitDate=getdate(),approvalStatus=@approvalStatus,
                    approvalRemark=@approvalRemark,otpSend=@otpSend,otpSendDate=getdate() where regNo=@regNo and requestReferanceNo=@requestReferanceNo";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@approvalSubmittedBy", SqlDbType.VarChar, 50);
        param[0].Value = approvalSubmittedBy;
        param[1] = new SqlParameter("@approvalStatus", SqlDbType.VarChar, 10);
        param[1].Value = approvalStatus;
        param[2] = new SqlParameter("@approvalRemark", SqlDbType.VarChar, 200);
        param[2].Value = approvalRemark;
        param[3] = new SqlParameter("@otpSend", SqlDbType.VarChar, 50);
        param[3].Value = otpSend;
        param[4] = new SqlParameter("@regNo", SqlDbType.NVarChar, 50);
        param[4].Value = regNo;
        param[5] = new SqlParameter("@requestReferanceNo", SqlDbType.NVarChar, 50);
        param[5].Value = reqReferenceno;

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

    public int updateRegistrationForgetPassTable(string regNo, string newMobile, string newEmail)
    {
        string str = @"Update registration set mobileno=@mobileno,email=@email where rid=@rid";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@mobileno", SqlDbType.NVarChar, 12);
        param[0].Value = newMobile;
        param[1] = new SqlParameter("@email", SqlDbType.VarChar, 50);
        param[1].Value = newEmail;
        param[2] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[2].Value = regNo;

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
    public DataTable CheckRegnoDuplicateinForgetPass(string edid)
    {

        string str = "select rid,date,randomno from forgetpass where rid=@rid and expired='N' ";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = edid;

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

    public DataTable getDetailsResetPasswordThruNewMobEmail(string registNo)
    {
        string str = "select * from resetPasswordThruNewMobEmail where regNo=@regNo";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar, 50);
        param[0].Value = registNo;

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
    public DataTable getDetailsOARS(string registNo)
    {
        string str = "select name,fname,mothername,spousename,uid from registration where rid=@rid";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@rid", SqlDbType.VarChar, 50);
        param[0].Value = registNo;

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

    public DataTable getPostCodeForSelectedYear(string year)
    {
        string str = "select postcode from Job_Advt ja inner join dept_job_request djr on ja.reqid = djr.reqid inner join AdvMaster am on ja.adid=am.adid where adyear=@adyear order by postcode";
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@adyear", SqlDbType.VarChar, 50);
        param[0].Value = year;

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

    public DataTable getEntryListForResetPasswordThruNewMobEmailddlStatus(char ddlStatus, string fromDate, string toDate)
    {
        string str = "select *,CONCAT(requestReferanceNo,'/',cast(entryByCandidatedate as date)) as refNoDateRequest from resetPasswordThruNewMobEmail where 1=1 "; //order by entryByCandidatedate desc";


        if (ddlStatus == '2')
        {
            str = str + " and approvalStatus='ACCEPTED'";
        }
        else if (ddlStatus == '3')
        {
            str = str + " and approvalStatus='REJECTED'";
        }

        if ((fromDate != null && fromDate != "") && (toDate != null && toDate != ""))
        {
            DateTime fromDateD = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
            DateTime toDateD = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            str += " and entryByCandidatedate between " + "'" + fromDateD + "'" + " and " + "'" + toDateD.AddDays(1) + "'";
        }
        str = str + " order by entryByCandidatedate desc";
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
    /*    public DataTable getDebardStatusForLogin(string regId)
        {
            string str = "Select * from debarCandidateEntry where registrationID=@regId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@regId", SqlDbType.VarChar);
            param[0].Value = regId;
            try
            {
                dt = da.GetDataTableQry(str, param);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }*/

    public int insert_registration_new(string rid, string password, string uid, string name, string fhname, string mothername, string gender, string dob, string nationality, string mobil, string mail, string ip, string active, string rdate, string rollno, string passing_year, string spousename, string aadharNo, string nameOnAadhar, string nameOnIDProof, int proofOfID, string pofIDNum, string hdnfOTPVerifiedMEB, byte[] pofIDDoc)
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

                    //                    string str = @"insert into registration_new (rid,password,uid,name,fname,mothername,gender,birthdt,nationality,mobileno,email,um_ipaddress,active,rdate,rollno,passing_year,spousename,aadharNo,proofOfID) 
                    //                                   values(@rid,@password,@um_logid,@name,@fname,@mothername,@gender,@birthdt,@nationality,@mobileno,@email,@IP,@active,@rdate,@rollno,@passing_year,@spousename,@aadharNo,@proofOfID)";
                    string str = @"insert into registration(rid,password,uid,name,fname,mothername,gender,birthdt,nationality,mobileno,email,um_ipaddress,active,rdate,rollno,passing_year,spousename,aadharNo,nameOnAadhar,nameOnIDProof,verificationMode,proofOfID) 
                                   values(@rid,@password,@um_logid,@name,@fname,@mothername,@gender,@birthdt,@nationality,@mobileno,@email,@IP,@active,@rdate,@rollno,@passing_year,@spousename,@aadharNo,@nameOnAadhar,@nameOnIDProof,@hdnfOTPVerifiedMEB,@proofOfID)";

                    SqlParameter[] param = new SqlParameter[22];
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
                    if (fhname == "")
                    {
                        param[4].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[4].Value = fhname;
                    }
                    param[5] = new SqlParameter("@mothername", SqlDbType.NVarChar, 50);
                    if (mothername == "")
                    {
                        param[5].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[5].Value = mothername;
                    }

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
                    param[14].Value = Int64.Parse(rollno);
                    param[15] = new SqlParameter("@passing_year", SqlDbType.Int);
                    param[15].Value = Int32.Parse(passing_year);
                    param[16] = new SqlParameter("@spousename", SqlDbType.VarChar, 50);
                    if (spousename == "")
                    {
                        param[16].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[16].Value = spousename;
                    }
                    param[17] = new SqlParameter("@aadharNo", SqlDbType.NVarChar);
                    if (aadharNo == "")
                    {
                        param[17].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[17].Value = aadharNo;
                    }

                    param[18] = new SqlParameter("@proofOfID", SqlDbType.Int);
                    if (proofOfID == 0)
                    {
                        param[18].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[18].Value = proofOfID;
                    }

                    param[19] = new SqlParameter("@nameOnIDProof", SqlDbType.VarChar, 50);
                    if (nameOnIDProof == "")
                    {
                        param[19].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[19].Value = nameOnIDProof;
                    }


                    param[20] = new SqlParameter("@nameOnAadhar", SqlDbType.VarChar, 50);
                    if (nameOnAadhar == "")
                    {
                        param[20].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[20].Value = nameOnAadhar;
                    }
                    param[21] = new SqlParameter("@hdnfOTPVerifiedMEB", SqlDbType.Char, 2);
                    if (hdnfOTPVerifiedMEB == "")
                    {
                        param[21].Value = System.DBNull.Value;
                    }
                    else
                    {
                        param[21].Value = Convert.ToChar(hdnfOTPVerifiedMEB);
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

                    temp = command.ExecuteNonQuery();
                    if (proofOfID > 0)
                    {
                        temp = 0;
                        string str1 = @"insert into proofOfIDUploaded_Reg (regNo,proofOfId,proofOfIDNo,IDUploaded,entryDate) 
                                   values(@regNo,@proofOfId,@proofOfIDNo,@IDUploaded,@entryDate)";
                        SqlParameter[] param2 = new SqlParameter[5];

                        param2[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
                        param2[0].Value = rid;
                        param2[1] = new SqlParameter("@proofOfId", SqlDbType.Int);
                        param2[1].Value = proofOfID;
                        param2[2] = new SqlParameter("@proofOfIDNo", SqlDbType.VarChar);
                        param2[2].Value = pofIDNum;
                        param2[3] = new SqlParameter("@IDUploaded", SqlDbType.VarBinary);
                        param2[3].Value = pofIDDoc;
                        param2[4] = new SqlParameter("@entryDate", SqlDbType.DateTime);
                        param2[4].Value = rdate;

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

    public DataTable IsExist_Applicant(string regno, string mobNo, string email, string aadhar, string proofOfIDNo)
    {
        string qry = @" select rid,name,fname,convert(varchar,birthdt,103) as birthdt from registration where ";
        if (regno != "")
        {
            qry += " rid=@regno and ";
        }
        if (mobNo != "")
        {
            qry += " mobileno=@mobNo and ";
        }
        if (email != "")
        {
             qry += " email=@email and ";
            //qry += " REPLACE((REPLACE(email,'.','[dot]')), '@','[at]')=@email and ";
        }
        if (aadhar != "")
        {
            qry += " aadharNo=@aadhar and ";
        }
        if (proofOfIDNo != "")
        {
            qry = string.Empty;
            qry = @"select rid,name,fname,convert(varchar,birthdt,103) as birthdt from registration reg 
                    join proofOfIDUploaded_Reg prf on reg.rid=prf.regNo where";
            qry += " prf.proofOfIDNo=@proofOfIDNo and ";
        }

        qry += " active='Y'";

        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@regno", SqlDbType.VarChar);
        if (regno != "")
        {
            param[0].Value = regno;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }

        param[1] = new SqlParameter("@mobNo", SqlDbType.NVarChar);
        if (mobNo != "")
        {
            param[1].Value = mobNo;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }
        param[2] = new SqlParameter("@email", SqlDbType.VarChar);

        if (email != "")
        {
            param[2].Value = email;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
        }
        param[3] = new SqlParameter("@aadhar", SqlDbType.NVarChar);
        if (aadhar != "")
        {
            param[3].Value = aadhar;
        }
        else
        {
            param[3].Value = System.DBNull.Value;
        }
        param[4] = new SqlParameter("@proofOfIDNo", SqlDbType.VarChar);
        if (proofOfIDNo != "")
        {
            param[4].Value = proofOfIDNo;
        }
        else
        {
            param[4].Value = System.DBNull.Value;
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
    /// <summary>
    /// Created By: Sohan Yadav
    /// Created On: 22/Dec/2020
    /// Purpose: insert Adhar no of Candidate while logging in.
    /// </summary>
    /// <param name="regId"></param>
    /// <returns></returns>
    public int AdharInsertion(string regId, string AdharNum)
    {
        string strupdtAdhar = "Update registration set aadharNo = @AdharNum where rid = @regId ";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@regId", SqlDbType.NVarChar);
        param[0].Value = regId;
        param[1] = new SqlParameter("@AdharNum", SqlDbType.NVarChar);
        param[1].Value = AdharNum;

        try
        {
            int id = da.ExecuteParameterizedQuery(strupdtAdhar, param);
            return id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable selectPostCodeFromAdvtNo(string advtNo)
    {
        string qry = @" select postcode,postcode+' : '+JobTitle as postCodeTitle, JobTitle,ja.jid from Job_Advt ja join AdvMaster am on ja.adid=am.adid where ja.adid=@advtNo order by postCode ";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@advtNo", SqlDbType.VarChar);
        if (advtNo != "")
        {
            param[0].Value = advtNo;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
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

    //public DataTable verifyApplicantFromAdvtNo(string jid, string applNo)
    public DataTable verifyApplicantFromAdvtNo(string jid, string applNo, string regNO)
    {
        string qry = @" select name,convert(nvarchar(10),birthdt,103)as birthdt,applid from JobApplication where dummy_no=@applNo and jid=@jid and regNo=@regNo ";

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@jid", SqlDbType.VarChar);
        if (jid != "")
        {
            param[0].Value = jid;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@applNo", SqlDbType.VarChar);
        if (applNo != "")
        {
            param[1].Value = applNo;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
        }
        param[2] = new SqlParameter("@regNO", SqlDbType.VarChar);
        if (regNO != "")
        {
            param[2].Value = regNO;
        }
        else
        {
            param[2].Value = System.DBNull.Value;
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
    public DataTable getDetailIfDocUploaded(string regNo)
    {
        string qry = @"select aadharNo,nameOnAadhar,nameOnIDProof,proofOfID from registration where rid=@regNo and (aadharNo is not null or nameOnIDProof IS NOT NULL or proofOfID IS NOT NULL)";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
        if (regNo != "")
        {
            param[0].Value = regNo;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
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

    //public int updateRegDoc_InsertIDProofDoc(string rid, string aadharNo, string nameOnAadhar, string nameOnIDProof, int proofOfID, string pofIDNum, byte[] pofIDDoc)
    public int updateRegDoc_InsertIDProofDoc(string rid, int proofOfID)
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

                    string str = @"update registration set aadharNo=NULL, nameOnAadhar=NULL,nameOnIDProof=NULL,proofOfID=NULL where rid=@rid";
                    string strPCSP = @"delete from jobapplicationpostcardphoto where applid in(
                                    select applid from jobapplication where regno=@rid and dummy_no is not null and jid in 
                                    (select jid from  job_advt where adid in (26,27,28,29,30) and postcode !='89/20'))";
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
                    param[0].Value = rid;

                    command.CommandType = CommandType.Text;
                    command.CommandText = str +' '+ strPCSP;
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
                        string str1 = @"delete from proofOfIDUploaded_Reg where regNo=@regNo and proofOfId=@proofOfId";
                        SqlParameter[] param2 = new SqlParameter[2];

                        param2[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
                        param2[0].Value = rid;
                        param2[1] = new SqlParameter("@proofOfId", SqlDbType.Int);
                        param2[1].Value = proofOfID;

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
    public DataTable getDetailIfPostCardPhotoUploaded(string regNo)
    {
        string qry = @" select * from jobApplicationPostCardPhoto where ApplId in (select applid from jobapplication where jid in(select jid from job_advt where adid in (26,27,28,29,30) and postcode !='89/20') 
                        and RegNo=@regNo and dummy_no is not null)";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
        if (regNo != "")
        {
            param[0].Value = regNo;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
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

    public DataTable getApplidDummyNo(string regNo)
    {
        string qry = @" select applid,dummy_no,jad.postcode,jad.JobTitle from jobapplication jap join job_advt jad on jap.jid=jad.jid 
                        where jap.jid in(select jid from job_advt where adid in (26,27,28,29,30) and postcode !='89/20') and RegNo=@regNo and dummy_no is not null";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
        if (regNo != "")
        {
            param[0].Value = regNo;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
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
    

    public DataTable CheckIfWithin1by20to116by20PostCode(string regNo)
    {
        string qry = @"select * from jobapplication where jid in (select jid from  job_advt where adid in (26,27,28,29,30) and postcode !='89/20') and dummy_no is not null
                        and regno=@regNo";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
        if (regNo != "")
        {
            param[0].Value = regNo;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
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
   // Ambika created for App
    public DataTable AppGetUserAuth(string username, string pwd)
    {
        string str = @"select rid,password,initial,name,fname,mothername,gender,convert(varchar,birthdt,103) birthdt,nationality,mobileno,email,spousename,aadharNo,nameOnIDProof,proofOfID 
        from registration where rid=@rid and active='Y'";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
        param[0].Value = username;
        try
        {
            dt = da.GetDataTableQry(str, param);
            if (dt.Rows.Count > 0)
            {
                int flag = Appcomparepwds(pwd, dt.Rows[0]["password"].ToString());
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
   // Ambika Created for APP
    public int Appcomparepwds(string inputpwd, string dbpwd)
    {
        string encptdpwd = dbpwd;
        if (inputpwd == encptdpwd)
            return 1;
        else
            return 0;
    }

    public DataTable validateCandidateAppliedForSelectedPostCode(string regNo, string postCode, string type)
    {
        string qry = string.Empty;
        if (type == "PCode")
        {
            qry = @"select * from JobApplication  where RegNo=@regNo and jid in(select jid  from Job_Advt where postcode=@postCode)
                       union 
                       select * from dsssbonline_recdapp.dbo.jobapplication where RegNo=@regNo and jid in(select jid  from Job_Advt where postcode=@postCode)";
        }
        else if (type == "GetOldMobEmail")
        {
            qry = @"select mobileno,email from registration where rid=@regNo";
        }
        else
        {
            qry = @"select * from JobApplication  where RegNo=@regNo
                       union 
                       select * from dsssbonline_recdapp.dbo.jobapplication where RegNo=@regNo";
        }
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar);
        if (regNo != "")
        {
            param[0].Value = regNo;
        }
        else
        {
            param[0].Value = System.DBNull.Value;
        }
        param[1] = new SqlParameter("@postCode", SqlDbType.VarChar);
        if (postCode != "")
        {
            param[1].Value = postCode;
        }
        else
        {
            param[1].Value = System.DBNull.Value;
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
  public int insert_ResetPasswordThruNewMobEmailAppUsers(string regNo, string requestReferanceNo, string txt_DOB, string txt_roll_no, string ddl_pass_year, string postCode, string txt_name, string txt_fh_name, string txt_mothername, string txtspouse, string txtUid, string txt_mob, string txt_email, byte[] bytesCert10File, byte[] bytesIDProof, byte[] documentGovID2, string rdate, string ip)
      {
        DateTime txtDOBDate = DateTime.ParseExact(txt_DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
        DateTime txtrDate = DateTime.ParseExact(rdate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);

        string str = @"insert into resetPasswordThruNewMobEmail (regNo,requestReferanceNo,dob,rollNo,passingYear,postCode,name,fatherName,motherName,spouseName,uidNumber,newMobile,newEmail,tenthDocument,idProofDoc,entryByCandidatedate,candidateIPAddess,idProofDocSecond) 
         values(@regNo,@requestReferanceNo,@dob,@rollNo,@passingYear,@postCode,@name,@fatherName,@motherName,@spouseName,@uidNumber,@newMobile,@newEmail,@tenthDocument,@idProofDoc,@entryByCandidatedate,@candidateIPAddess,@idProofDocSecond)";
        SqlParameter[] param = new SqlParameter[18];
        param[0] = new SqlParameter("@regNo", SqlDbType.VarChar, 50);
        param[0].Value = regNo;
        param[1] = new SqlParameter("@dob", SqlDbType.DateTime);
        param[1].Value = txtDOBDate;
        param[2] = new SqlParameter("@rollNo", SqlDbType.VarChar, 50);
        param[2].Value = txt_roll_no;
        param[3] = new SqlParameter("@passingYear", SqlDbType.Int);
        param[3].Value = Int32.Parse(ddl_pass_year);
        param[4] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
        param[4].Value = txt_name;
        param[5] = new SqlParameter("@fatherName", SqlDbType.NVarChar, 50);
        param[5].Value = txt_fh_name;
        param[6] = new SqlParameter("@motherName", SqlDbType.NVarChar, 50);
        param[6].Value = txt_mothername;
        param[7] = new SqlParameter("@spouseName", SqlDbType.NVarChar, 50);
        param[7].Value = txtspouse;
        param[8] = new SqlParameter("@newMobile", SqlDbType.NVarChar, 12);
        param[8].Value = txt_mob;
        param[9] = new SqlParameter("@newEmail", SqlDbType.VarChar, 50);
        param[9].Value = txt_email;
        param[10] = new SqlParameter("@tenthDocument", SqlDbType.VarBinary);
        param[10].Value = bytesCert10File;
        param[11] = new SqlParameter("@idProofDoc", SqlDbType.VarBinary);
        param[11].Value = bytesIDProof;
        param[12] = new SqlParameter("@entryByCandidatedate", SqlDbType.DateTime);
        param[12].Value = txtrDate;
        param[13] = new SqlParameter("@candidateIPAddess", SqlDbType.NVarChar, 20);
        param[13].Value = ip;
        param[14] = new SqlParameter("@requestReferanceNo", SqlDbType.VarChar, 50);
        param[14].Value = requestReferanceNo;
        param[15] = new SqlParameter("@uidNumber", SqlDbType.VarChar, 50);
        param[15].Value = txtUid;
        param[16] = new SqlParameter("@postCode", SqlDbType.VarChar, 50);
        param[16].Value = postCode;
        param[17] = new SqlParameter("@idProofDocSecond", SqlDbType.VarBinary);
        param[17].Value = documentGovID2;

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
 public DataTable AppGetUserDetails(string regno)
   {
       string str = @"select rid,password,initial,name,fname,mothername,gender,convert(varchar,birthdt,103) birthdt,nationality,mobileno,email,spousename,aadharNo,nameOnIDProof,proofOfID
        from registration where rid=@rid and active='Y'";
       SqlParameter[] param = new SqlParameter[1];
       param[0] = new SqlParameter("@rid", SqlDbType.VarChar);
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
public DataTable getDebardStatusForLogin(string regId)//added on 13/04/2023 for debarment of candidate
 {
     string str = "Select debardTillDate from debarCandidateEntry where registrationID=@regId ";
     SqlParameter[] param = new SqlParameter[1];
     param[0] = new SqlParameter("@regId", SqlDbType.VarChar);
     param[0].Value = regId;
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

//Added by Ankitasingh for 90/09 dated: 04-10-2022
public int updateOldpostEntry(string regNo, string serialno)
{
    string str = @"Update oldpostmatching set regno=@regno where serial_no=@serialno";
    SqlParameter[] param = new SqlParameter[2];
    param[0] = new SqlParameter("@regno", SqlDbType.VarChar, 50);
    param[0].Value = regNo;
    param[1] = new SqlParameter("@serialno", SqlDbType.VarChar, 50);
    param[1].Value = serialno;


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

       public DataTable getRegistrationDetail(string regId ,string name)//added on 28/02/2024 for masked registration detail
    {
        string str = "SELECT rid,name,fname,birthdt,rid,CONCAT(LEFT(mobileno, 2),REPLICATE('X', LEN(mobileno) - 4),RIGHT(mobileno, 4)) AS MaskedPhoneNumber,CONCAT(LEFT(REPLACE(REPLACE(Email, '[at]', '@'), '[dot]', '.'), 4), 'XXXX',RIGHT(REPLACE(REPLACE(Email, '[at]', '@'), '[dot]', '.'), LEN(REPLACE(REPLACE(Email, '[at]', '@'), '[dot]', '.')) - CHARINDEX('@', REPLACE(REPLACE(Email, '[at]', '@'), '[dot]', '.')) +3) ) AS MaskedEmail FROM registration WHERE rid =@regId and name=@name";

       
        
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@regId", SqlDbType.VarChar);
        param[0].Value = regId;

        param[1] = new SqlParameter("@name", SqlDbType.VarChar);
        param[1].Value = name;
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
