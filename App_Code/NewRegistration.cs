using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for NewRegistration
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class NewRegistration : System.Web.Services.WebService {

    public NewRegistration () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public DataTable IsApplicantExist(string dob, string rollno, string passyear)
    {
        //DataTable dt = null;
        //string regno = dob.Replace("/", "") + rollno + passyear;
        //LoginMast objmast = new LoginMast();
        //dt = objmast.IsExist_Applicant(regno);
        //return dt;
        return null;
    }

    [WebMethod]
    public int insert_registration(string rid, string password, string uid, string name, string fhname, string mothername, string gender, string dob, string nationality, string mobil, string mail, string ip, string active, string rdate, string rollno, string passing_year, string spousename)
    {
        //int t = 0;
        //LoginMast objmast = new LoginMast();
        //t = objmast.insert_registration(rid, password, uid, Utility.putstring(name), Utility.putstring(fhname), Utility.putstring(mothername), gender, dob, nationality, mobil, Utility.putstring(mail), ip, active, rdate, rollno, passing_year, spousename);
        //return t;
        return 0;
    }
}
