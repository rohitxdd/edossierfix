using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.IdentityModel;
using System.Security;
using System.Security.Cryptography;

/// <summary>
/// Summary description for DsssbAppWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class DsssbAppWebService : System.Web.Services.WebService 
  {
    LoginMast ObjMast = new LoginMast();
    forgetpass objforgetpass = new forgetpass();

    public DsssbAppWebService () 
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    } 
    [WebMethod]
    public string LoginDsssb(string userid, string password)
    {
        string AudInsert = "";
        //UserLogin Log = new UserLogin();

        
        ClsAudit objClsAudit = new ClsAudit();
        Loginact ValidUser = new Loginact();
        LoginMast objLogin = new LoginMast();
        string ipaddress = HttpContext.Current.Request.UserHostAddress;  //17-June-2020
        string logindate = Utility.formatDatewithtime(DateTime.Now);

        //string encryptnewpass = MD5Util.md5(password); //encrpting the pwd to match in the DB 

        DataTable dtuser = objLogin.UserValidate(userid);
          DataTable ds = new DataTable();

          if (dtuser.Rows.Count > 0)
          {
              string Active = (dtuser.Rows[0]["active"]).ToString();
              if (Active == "Y")
              {

                  ds = objLogin.AppGetUserAuth(userid, password);
                  int count = ds.Rows.Count;
                  if (count > 0)
                  {

                      AudInsert = objClsAudit.InsertAudit(userid, ipaddress, logindate, 'Y', 'S'); //S for Successfull logininsert in AuditLog_mob Table
                  }
                  else
                  {
                      AudInsert = objClsAudit.InsertAudit(userid, ipaddress, logindate, 'N', 'U');  //U for Unsuccessfull login
                      return AudInsert = "-1";  //invalid user which 'instrength=0'
                  }
              }
              else
              {
                  return AudInsert = "-1";
              }
          }

 
        return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);

    }
    [WebMethod]
    public string EditMobile(string rid, string mobile, string email)
    {
        CandidateData objcd = new CandidateData();
      //  DataTable ds = new DataTable();

        int tmp = objcd.updatemobile(rid, mobile, email);
       // int count = ds.Rows.Count;
        if (tmp > 0)
        {
            return "1";
        }
        else
        {
            return "0";   
        }

         
    }
    [WebMethod]
  public string ForgetPassword(string rid)
    {
   
        //  DataTable ds = new DataTable();
        Int32 UniqueRandomNumber = 0;
        Random randObj = new Random();
        UniqueRandomNumber = randObj.Next(1000, 9999);
        DataTable dtReg = objforgetpass.VerifyRegno(rid);
        message msg = new message();
        DataTable dt = new DataTable();
        Sms objsms = new Sms();
     
        string ipaddress = HttpContext.Current.Request.UserHostAddress;
        string SecurityCode;
        Utility Utlity = new Utility();
       
        bool emailsent = false;

        // int count = ds.Rows.Count;
        if (dtReg.Rows.Count > 0)
        {
            DataTable dtFP = objforgetpass.GetOTP(rid);

            if (dtFP.Rows.Count > 0)
            {

                String secno = dtFP.Rows[0]["randomno"].ToString();
                UniqueRandomNumber = Convert.ToInt32(secno);

                //String expired = rows[0]["expired"].ToString();
            }

   
         
            else
            {
                int i = objforgetpass.ForgetPassRandom(rid, UniqueRandomNumber, ipaddress);
            }
            SecurityCode = Convert.ToString(UniqueRandomNumber);

            string mobile = dtReg.Rows[0]["mobileno"].ToString();
            string code = "OTP for resetting password of your DSSSB online login account is " + SecurityCode; //"OTP for Reset Password is : " + SecurityCode;
            string mail_code = "Dear Candidate, \nThe OTP for Reset Password is : " + SecurityCode + "\n\n Instructions:\n1. This OTP is valid for today only. \n2. This is auto generated mail. Please do not reply to it.\n\n--DSSSB";
            string email = Utility.getstring(dtReg.Rows[0]["email"].ToString());

            //Response.Redirect("http://10.128.65.106/sms/default.aspx?mobile=" + mobile + "&msg=" + code);
            //Server.Transfer("http://10.128.65.106/sms/default.aspx?mobile="+mobile+"&msg="+code);

            //objsms.sendmsg(mobile, code); commented on 06042021 by RKP top implement new TRAI rule for sms
            string templateID = "1007161562148943825";
            objsms.sendmsgNew(mobile, code, templateID);
            Email obj_email = new Email();
            int cnt1 = 0;
            //string a = obj_email.sendMail(email, "", "", "DSSSB-DONOTREPLY@", "Security Code", mail_code, mail_code);

            if (email != "")
            {
                emailsent = obj_email.sendMail(email, "", "", "sadsssb.delhi@nic.in", "Security Code", mail_code, "");
                if (emailsent)
                {
                    cnt1++;
                }
            }

         return "OTP Sent Successfully.";
        }

        return "Error";
    }
        [WebMethod]
    public string getCurrentVaccancies()
    {
        
       DataTable dt = new DataTable();
        CandidateData ObjCandD = new CandidateData();
        //dt = ObjCandD.getannouncement();
        dt = ObjCandD.GetJobAdvt("");

        return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);

    }
    [WebMethod]
    public string getNewsEvents()
    {
        DataTable dt = new DataTable();
        CandidateData ObjCandD = new CandidateData();
        //dt = ObjCandD.GetMessage("N"); //cmntd on 09062021 n Replaced by below line
         dt = ObjCandD.GetMessageMobileAPP("N");
        if (dt.Rows.Count > 0)
        {
            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);    
        }
        return "No Data Found";

    }
    /// <summary>
    /// created On: 19042021
    /// Checked that Candidate is existing or not []
    /// </summary>
    /// <param name="regno"></param>
    /// <returns></returns>
    [WebMethod]
    public string isExistingCandidate(string regno)
    {
        DataTable dt = new DataTable();
        dt = ObjMast.IsExist_Applicant(regno, "", "", "", "");
        try
        {
            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);  
        }
        catch(Exception ex)
        {
            throw ex;
        }
       
    }
    /// <summary>
    ///Created On: 19042021 [0 for True and 1 for False]
    /// </summary>
    /// <param name="regNo"></param>
    /// <returns></returns>
    [WebMethod]
    public string CheckExistingEntryToResetPwdThroughNewEmailMobile(string regNo)
    {
        //DataTable dt = ObjMast.IsExist_Applicant(regNo, "", "", "", "");
        
        DataTable dtChkNewEntry = ObjMast.findEntryExistForResetPasswordThruNewMobEmail(regNo);
        try
        {
            return JsonConvert.SerializeObject(dtChkNewEntry, Newtonsoft.Json.Formatting.Indented);  
          
        }
        catch (Exception ex)
        {
            throw ex;
        }       
         
    }
    /// <summary>
    /// Created On: 20042021
    /// Purpose: Checked the enter mobile exists or not in case of foregt mobileno 
    /// </summary>
    /// <param name="mobileno"></param>
    /// <returns></returns>
    [WebMethod]
    public string checkExistingMobileNo( string mobileno)
    {
        DataTable dt = new DataTable();
        dt = ObjMast.IsExist_Applicant("", mobileno, "", "", "");
        try
        {
                return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);            

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    /// <summary>
    /// created On: 20042021
    /// purpose:Checked the enter emailid is exists or not in case of foregt emailid
    /// </summary>
    /// <param name="emailid"></param>
    /// <returns></returns>
    [WebMethod]
    public string checkExistingEmailId(string emailid)
    {
        DataTable dt = new DataTable();
        dt = ObjMast.IsExist_Applicant("", "", emailid, "", "");
        try
        {
                return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented); 
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// </summary>
    /// Created On: 22042021
    /// Purpose: Save the detail if lost mobile n emailid.
    /// <param name="regNo"></param>
    /// <param name="requestReferanceNo"></param>
    /// <param name="txt_DOB"></param>
    /// <param name="txt_roll_no"></param>
    /// <param name="ddl_pass_year"></param>
    /// <param name="postCode"></param>
    /// <param name="txt_name"></param>
    /// <param name="txt_fh_name"></param>
    /// <param name="txt_mothername"></param>
    /// <param name="txtspouse"></param>
    /// <param name="txtUid"></param>
    /// <param name="txt_mob"></param>
    /// <param name="txt_email"></param>
    /// <param name="bytesCert10File"></param>
    /// <param name="bytesIDProof"></param>
    /// <param name="documentGovID2"></param>
    /// <param name="rdate"></param>

     [WebMethod]
    public string saveNewRegWithNewMobileAndEmail(string regNo, string requestReferanceNo, string txt_DOB, string txt_roll_no, string ddl_pass_year, string postCode, string txt_name, string txt_fh_name, string txt_mothername, string txtspouse, string txtUid, string txt_mob, string txt_email, string bytesCert10File, string bytesIDProof, string rdate, string ip)
    {
        int i = 0;
        ip = HttpContext.Current.Request.UserHostAddress;
        byte[] bytesCert10File_new = Convert.FromBase64String(bytesCert10File);
        byte[] bytesIDProofnew = Convert.FromBase64String(bytesIDProof);
        //byte[] DocGovtId = Convert.FromBase64String(documentGovID2);


        try
        {
            //i = ObjMast.insert_ResetPasswordThruNewMobEmailApp(regNo, requestReferanceNo, txt_DOB, txt_roll_no, ddl_pass_year, postCode, txt_name, txt_fh_name, txt_mothername, txtspouse, txtUid, txt_mob, txt_email, bytesCert10File_new, bytesIDProofnew, rdate, ip);
            //i=ObjMast.insert_ResetPasswordThruNewMobEmailApp(regNo, requestReferanceNo, txt_DOB, txt_roll_no, ddl_pass_year, postCode, txt_name, txt_fh_name, txt_mothername, txtspouse, txtUid, txt_mob, txt_email, bytesCert10File_new, bytesIDProofnew, documentGovID2, rdate, ip);
	    //if (i > 0)
            //{
             //   return "1";
           // }
          //  else
           // {
                return "0";
           // }
        }
        catch (Exception ex)
        {
            throw ex; ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rid"></param>
    /// <param name="otp"></param>
    /// <returns></returns>
    [WebMethod]
    public string validOtp(string rid, string otp)
    {

        //dt = ObjCandD.getannouncement();
        DataTable dtFP = objforgetpass.GetOTP(rid);
        if (dtFP.Rows.Count > 0)
        {
            String secno = dtFP.Rows[0]["randomno"].ToString();
            if (secno != otp)
            {
                return "Error";
            }

            else
            {
                return "Verified OTP";

            }
        }
        else
        {
            return "Error";


        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rid"></param>
    /// <param name="password"></param>
    /// <param name="otp"></param>
    /// <returns></returns>
    [WebMethod]
    public string ResetPassword(string rid, string password, string otp)
    {
        string ipaddress = HttpContext.Current.Request.UserHostAddress;


        int i = objforgetpass.updatePassword(rid, password, ipaddress);
        if (i > 0)
        {
            objforgetpass.Updateusername(rid, otp);
            return "Success";

        }
        else
        {
            return "Error";
        }


    }

 [WebMethod]
    public string postcodeyearwise(string year)
    {
        DataTable dt = new DataTable();

        dt = ObjMast.getPostCodeForSelectedYear(year);

        if (dt.Rows.Count > 0)
        {

            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);

        }
        else
        {
            return "Error";
        }


    }
  // new file added
    [WebMethod]
    public string getEntryExistForResetPasswordThruNewMobEmailAPP(string regno)
    {
        DataTable dt = ObjMast.IsExist_Applicant(regno, "", "", "", "");
        int count = dt.Rows.Count;
        if (count > 0)
        {
            DataTable dt1 = ObjMast.findEntryExistForResetPasswordThruNewMobEmail(regno);
            DataTable dtRejectCount = ObjMast.getRejectedSatusCountEntry(regno);
            if (dt1.Rows.Count > 0 && dt1.Rows[0]["approvalStatus"].ToString() != "REJECTED")
            {
                return "EntryExists";
            }
            else if (dtRejectCount.Rows.Count > 3)
            {
                return "Limit exceeded";
            }
            else
            {
                return "Success";
            }
        }
        else
        {

            return "Details Not Found";

        }

    }


    [WebMethod]
    public string validateCandidateAppliedForSelectedPostCodeAPP(string regno, string postcode)
    {
       DataTable dt = ObjMast.validateCandidateAppliedForSelectedPostCode(regno, postcode, "PCode");
        if (dt.Rows.Count <= 0)
        {
            return "Error";  
        
        }
        return "Success";

    }

    [WebMethod]
     public string saveNewRegWithNewMobileAndEmailAPP(string regNo, string requestReferanceNo, string txt_DOB, string txt_roll_no, string ddl_pass_year, string postCode, string txt_name, string txt_fh_name, string txt_mothername, string txtspouse, string txtUid, string txt_mob, string txt_email, string bytesCert10File, string bytesIDProof, string documentGovID2)
 
  //public string ResetPasswordThruNewMobEmailApp(string regno, string requestReferanceNo,string dob,string rollno,string passingyear, string postcode,string name, string fathername,string mothername,string spousename,string uid,string mobile,string email,byte document1,byte govid1,byte govid2)
    {
        Sms objsms = new Sms();
        Email sendmail = new Email();

          int i = 0;
                 string  ip = HttpContext.Current.Request.UserHostAddress;
                    DateTime date = DateTime.Now;
                    string rdate = date.ToString("dd/MM/yyyy");
        byte[] bytesCert10File_new = Convert.FromBase64String(bytesCert10File);
        byte[] bytesIDProofnew = Convert.FromBase64String(bytesIDProof);
        byte[] DocGovtId = Convert.FromBase64String(documentGovID2);
                    DataTable dt = ObjMast.findEntryExistForResetPasswordThruNewMobEmail(regNo);
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dtRejectCount = ObjMast.getRejectedSatusCountEntry(regNo);
                            if (dtRejectCount.Rows.Count < 3)
                            {
                                //if ((Convert.ToDateTime(rdate) - Convert.ToDateTime(dt.Rows[0]["entryByCandidatedate"])).TotalHours > 24)
                                if (dt.Rows[0]["approvalStatus"].ToString() == "REJECTED")
                                {

                                      i = ObjMast.insert_ResetPasswordThruNewMobEmailAppUsers(regNo, requestReferanceNo, txt_DOB, txt_roll_no, ddl_pass_year, postCode, txt_name, txt_fh_name, txt_mothername, txtspouse, txtUid, txt_mob, txt_email, bytesCert10File_new, bytesIDProofnew, DocGovtId, rdate, ip);

                                      return "Success";
                                         }
                                else
                                {
    
                                    return "Your request has already been registered to update mobile number/email id.";
                                }
                            }
                            else
                            {
                                return "Your have exceeded the opertunity limit (3) to update your email and mobile number.";
                            }
                        }
                        else
                        {
                            //int i = ObjMast.insert_ResetPasswordReqEntry(rids, "", "0", Utility.putstring(txt_name.Text), Utility.putstring(txt_fh_name.Text), Utility.putstring(txt_mothername.Text), "", txt_DOB.Text, "", Utility.putstring(txt_mob.Text), Utility.putstring(txt_email.Text), ip, "Y", rdate, txt_roll_no.Text, ddl_pass_year.SelectedValue, Utility.putstring(txtspouse.Text));
                            //i = ObjMast.insert_ResetPasswordThruNewMobEmail(regNo, requestReferanceNo, txt_DOB.Text, txt_roll_no.Text, ddl_pass_year.SelectedValue, ddlPostCode.SelectedValue, Utility.putstring(txt_name.Text), Utility.putstring(txt_fh_name.Text), Utility.putstring(txt_mothername.Text), Utility.putstring(txtspouse.Text), Utility.putstring(txtuid.Text), Utility.putstring(txt_mob.Text), Utility.putstring(txt_email.Text), document, documentGovID, rdate, ip);
                          i = ObjMast.insert_ResetPasswordThruNewMobEmailAppUsers(regNo, requestReferanceNo, txt_DOB, txt_roll_no, ddl_pass_year, postCode, txt_name, txt_fh_name, txt_mothername, txtspouse, txtUid, txt_mob, txt_email, bytesCert10File_new, bytesIDProofnew, DocGovtId, rdate, ip);
                        
                        
                        }

                       if (i > 0)
                        {
                            Type cstype = this.GetType();
                            string message = @"A request to update your mobile and email has been received vide reference number " + requestReferanceNo + ". Your request will be processed by DSSSB within two weeks. Once your request is approved on the basis of documents submitted by you, you will be informed on the new mobile/email entered by you. Based on the same you need to further reset your password.";
                           
                            string templateID = "1007162124167280760";
                            objsms.sendmsgNew(txt_mob, message, templateID);
                   
                            //objsms.sendmsg(txt_mob.Text.Trim(), message);
                         
                           
                            sendmail.sendMail(txt_email.Trim(), "", "", "sadsssb.delhi@nic.in", "DSSSB online registration", message, "");

                            regNo = txt_DOB.Replace("/", "") + txt_roll_no + ddl_pass_year;
                            DataTable dtOME = ObjMast.validateCandidateAppliedForSelectedPostCode(regNo, "", "GetOldMobEmail");
                            if (dtOME.Rows.Count > 0)
                            {
                                string msg = "A request to change DSSSB OARS login account has been received vide reference number " + requestReferanceNo + ". If the request has not been made by you, kindly inform immediately at email helpdesk-dsssb@nic.in, mentioning your reference number, name, father's name, mother's name, Date of Birth (DOB), mobile number and email address. If no request is received within 24 hours of receipt of this message/email, it would be presumed that you have nothing to say and the request for updating of mobile/email would be responded accordingly.";
                                string templateID1 = "1007162124172114403";
                                objsms.sendmsgNew(dtOME.Rows[0]["mobileno"].ToString(), msg, templateID1);
                                //objsms.sendmsg(dtOME.Rows[0]["mobileno"].ToString(), msg);
                                sendmail.sendMail(Utility.getstring(dtOME.Rows[0]["email"].ToString()), "", "", "sadsssb.delhi@nic.in", "DSSSB online registration", msg, "");
                            }

                            return "Success";
                        }
                        else
                        {
                           return "Data Not Updated";
                        }
        
                         }

 [WebMethod]
    public string checkpostapplied(string regno)
    {
        DataTable dt = new DataTable();
        dt = ObjMast.validateCandidateAppliedForSelectedPostCode(regno, "", "RbtYN");
        if (dt.Rows.Count > 0)
        {
            return "Already Applied";
        }
      
        else
        {
            return "Success";
        }

    }
    [WebMethod]
    public string getpostforstatusapp(string regno)
    {

        CandidateData objcd = new CandidateData();
        DataTable dt = new DataTable();
        dt = objcd.getpostforstatus(regno);
        if (dt.Rows.Count > 0)
        {
            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        }

        else
        {
            return "No Data Found";
        }

    }
        [WebMethod]
    public string viewstatusapplicant(string applid)
    {

          CandidateData objcd = new CandidateData();
        DataTable dt = objcd.getdataforapplid(applid);
        if (dt.Rows.Count > 0)
        {
       
             return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            }

        else
        {
            return "No Data Found";
        }

    }
 [WebMethod]
    public string advtpdf(string adid)
    {

          CandidateData objcd = new CandidateData();
        DataTable dt = new DataTable();
        dt = objcd.Get_advt_pdf(adid);
        if (dt.Rows.Count > 0)
        {
       
           return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        }

        else
        {
            return "No Data Found";
        }

    }
 [WebMethod]
    public string getDetailsUserbyreg(String regno)
    {
        DataTable dt = new DataTable();
        LoginMast objLoginMast = new LoginMast();
        dt = objLoginMast.AppGetUserDetails(regno);
        if (dt.Rows.Count > 0)
        {
            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        }
        else
        {
        return "No Data Found";
        }

    }

    [WebMethod]
    public string EditMobileAPP(string rid, string mobile)
    {
        CandidateData objcd = new CandidateData();
        //  DataTable ds = new DataTable();

        DataTable dt = ObjMast.IsExist_Applicant("", mobile, "", "", "");
        if (dt.Rows.Count > 0)
        {
        return "Mobile number entered is already registered in OARS";
           
        }
        else
        {
            int tmp = objcd.update_mobileno(rid, mobile);
            if (tmp > 0)
            {
                

                return "Updated successfully";
              
            }
            else
            {
                return "Error";
            }

        }
     

    }

    [WebMethod]
    public string EditEmailAPP(string rid, string email)
    {
        CandidateData objcd = new CandidateData();
        //  DataTable ds = new DataTable();

        DataTable dt = ObjMast.IsExist_Applicant("", "", Utility.putstring(email), "", "");
        if (dt.Rows.Count > 0)
        {
           
            return "Email address entered is already registered in OARS.";
        }
        else
        {
            int tmp = objcd.update_email(rid, email);
            if (tmp > 0)
            {
               
               
             return "Updated successfully";
               
            }
            else
            {
                return "Error";
            }
        }

    }

 [WebMethod]
        public string fillpersonaldetails(int applid)
        {

            CandidateData objCandD = new CandidateData();
            DataTable dt = objCandD.fill_personal_data(applid);
            if (dt.Rows.Count > 0)
            {

                return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            }

            else
            {
                return "No Data Found";
            }

        }

        [WebMethod]
        public string filleducationuser(string applid)
        {

            CandidateData objCandD = new CandidateData();
            DataTable dt = objCandD.GetJobApplication_Education(applid, "", "");
            if (dt.Rows.Count > 0)
            {

                return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            }

            else
            {
                return "No Data Found";
            }

        }
        [WebMethod]
        public string fillexperienceuser(string applid)
        {

            CandidateData objCandD = new CandidateData();

            DataTable dt = objCandD.GetJobApplication_Exp(applid);

            if (dt.Rows.Count > 0)
            {

                return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            }

            else
            {
                return "No Data Found";
            }

        }
 [WebMethod]
    public string getexamdetails(string regno)
    {

        Marks objmarks = new Marks();

        DataTable dt = objmarks.getexamtoViewMarks(regno);

        if (dt.Rows.Count > 0)
        {

            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        }

        else
        {
            return "No Data Found";
        }

    }

    [WebMethod]
    public string  getmarks(string regno, string examid)
    {

        Marks objmarks = new Marks();
        string FlagCount_P_Applied = "";
        challengeansheet objchallenge = new challengeansheet();
        bool wpresent = objmarks.checkexamattendance(examid, regno);
        if (wpresent)
        {
            DataTable dtTier = objchallenge.getExamTier(examid);
            string tierval = "", isPETmarksreq = "";
            if (dtTier.Rows.Count > 0)
            {
                tierval = dtTier.Rows[0]["tier"].ToString();
                isPETmarksreq = dtTier.Rows[0]["isPETmarksreq"].ToString();
            }
            DataTable dt = objmarks.GetPostCode(examid, regno, tierval);

            string posts = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    posts += ",";
                }
                posts += Utility.getstring(dt.Rows[i]["post"].ToString());
            }
            //lblposts.Text = posts;
            if (dt.Rows.Count == 1)
            {
                FlagCount_P_Applied = "S";
            }
            else
            {
                FlagCount_P_Applied = "M";
            }



            DataTable dt1 = objmarks.GetMarks(FlagCount_P_Applied, examid, regno, tierval);
            if (dt1.Rows.Count > 0 || isPETmarksreq == "N")
            {

                if (dt1.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt1, Newtonsoft.Json.Formatting.Indented);

                }
                else
                {
                    String rollno = objmarks.getattenrollno(examid, regno);
                    return rollno;
                }
            }

            else
            {
                return "No Data Found";
            }

        }


        else
        {
            return "No marks Found ";
        }



    }

    [WebMethod]
    public string getpostsforViewMarks(string regno, string examid)
    {

        Marks objmarks = new Marks();
   
        challengeansheet objchallenge = new challengeansheet();
        bool wpresent = objmarks.checkexamattendance(examid, regno);
        if (wpresent)
        {
            DataTable dtTier = objchallenge.getExamTier(examid);
            string tierval = "", isPETmarksreq = "";
            if (dtTier.Rows.Count > 0)
            {
                tierval = dtTier.Rows[0]["tier"].ToString();
                isPETmarksreq = dtTier.Rows[0]["isPETmarksreq"].ToString();
            }
            DataTable dt = objmarks.GetPostCode(examid, regno, tierval);

            string posts = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    posts += ",";
                }
                posts += Utility.getstring(dt.Rows[i]["post"].ToString());
            }

            return posts;
        }


        else
        {
            return "No posts Found";
        }



    }
    [WebMethod]
     public string edossierstatus(String applid)
     {
          DataTable dt = new DataTable();
          CandidateData objCandData = new CandidateData();
          dt = objCandData.getStatusEdossierCand(applid);
          if (dt.Rows.Count > 0)
          {
              return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
          }
          else
          {
              return "No Data Found";
          }

     }

 [WebMethod]
    public string getFeedetails(int applid)
    {

        CandidateData objCandD = new CandidateData();

       DataTable dt = objCandD.CheckFee(applid);

        if (dt.Rows.Count > 0)
        {

            string feereq = dt.Rows[0]["feereq"].ToString();
            if (dt.Rows[0]["feereq"].ToString() != "N")
            {
                if (dt.Rows[0]["feerecd"].ToString() == "Y")
                {
                    return "Confirmed at DSSSB";
                }
                else
                {
                    return "Pending";
                }
            }
            else
            {
               return  "Exempted";
            }
        }

        else
        {
            return "No Data Found";
        }

    }
    [WebMethod]
    public string getpost_edossier(string regno)
    {

        CandidateData objcd = new CandidateData();
        DataTable dt = new DataTable();
        dt = objcd.get_postApp_eDossier(regno);
        if (dt.Rows.Count > 0)
        {
            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        }

        else
        {
            return "No Data Found";
        }

    }
}
