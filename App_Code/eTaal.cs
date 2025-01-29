using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
/// <summary>
/// Summary description for eTaal
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class eTaal : System.Web.Services.WebService {

    public eTaal () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]
    public DataSet eTransactionCount_Registration(string QryDate, string UserID, string Password)
    {
        string ServiceSqlStr = "";
        Int32 dt = ValidateUser(UserID, Password);

        if (dt > 0)
        {
            //if (QryDate != "")
            //{
            //    //char[] splitchar = { '/' };

            //    string[] condate = new string[8];
            //    condate = QryDate.Split('/');

            //    string NewQryDate = condate[2] + condate[1] + condate[0];

            //    QryDate = NewQryDate;
            //}
            //ServiceSqlStr = "select SubServiceCode as 'ServiceCode',NoofApplications as 'ServiceCount' from cenmon.centralEPPService where convert(varchar,PDate,112)=@QryDate";
            ServiceSqlStr = @"select 'A098189006042' as 'ServiceCode',COUNT(*) as 'ServiceCount' from registration 
                                where  convert(varchar,rdate,103)=@QryDate group by convert(varchar,rdate,103)";

            SqlCommand ServiceCommand = new SqlCommand(ServiceSqlStr);
            ServiceCommand.Parameters.AddWithValue("@QryDate", QryDate);
            return GetDataSet(ServiceCommand);
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public DataSet eTransactionCount_Application(string QryDate, string UserID, string Password)
    {
        string ServiceSqlStr = "";
        Int32 dt = ValidateUser(UserID, Password);

        if (dt > 0)
        {
            //if (QryDate != "")
            //{
            //    //char[] splitchar = { '/' };

            //    string[] condate = new string[8];
            //    condate = QryDate.Split('/');

            //    string NewQryDate = condate[2] + condate[1] + condate[0];

            //    QryDate = NewQryDate;
            //}
            //ServiceSqlStr = "select SubServiceCode as 'ServiceCode',NoofApplications as 'ServiceCount' from cenmon.centralEPPService where convert(varchar,PDate,112)=@QryDate";
            ServiceSqlStr = @"select 'A098191806044' as 'ServiceCode',COUNT(*) as 'ServiceCount' from JobApplication 
                        where  convert(varchar,receive_dt,103)=@QryDate group by convert(varchar,receive_dt,103)";

            SqlCommand ServiceCommand = new SqlCommand(ServiceSqlStr);
            ServiceCommand.Parameters.AddWithValue("@QryDate", QryDate);
            return GetDataSet(ServiceCommand);
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public DataSet eTransactionCount_Fee(string QryDate, string UserID, string Password)
    {
        string ServiceSqlStr = "";
        Int32 dt = ValidateUser(UserID, Password);

        if (dt > 0)
        {
            //if (QryDate != "")
            //{
            //    //char[] splitchar = { '/' };

            //    string[] condate = new string[8];
            //    condate = QryDate.Split('/');

            //    string NewQryDate = condate[2] + condate[1] + condate[0];

            //    QryDate = NewQryDate;
            //}
            //ServiceSqlStr = "select SubServiceCode as 'ServiceCode',NoofApplications as 'ServiceCount' from cenmon.centralEPPService where convert(varchar,PDate,112)=@QryDate";
            ServiceSqlStr = @"select 'A098189006045' as 'ServiceCode',COUNT(*) as 'ServiceCount' from FeeDetails 
                            where  convert(varchar,trandate,103)=@QryDate group by convert(varchar,trandate,103)";

            SqlCommand ServiceCommand = new SqlCommand(ServiceSqlStr);
            ServiceCommand.Parameters.AddWithValue("@QryDate", QryDate);
            return GetDataSet(ServiceCommand);
        }
        else
        {
            return null;
        }
    }



    public Int32 ValidateUser(string userName, string password)
    {
        Int32 ReturnVal = 0;
        StringBuilder SelectQuery = new StringBuilder("select pwd from UserMaster where UserID=@uname");
        SqlCommand SelectCmd = new SqlCommand(SelectQuery.ToString());
        SelectCmd.Parameters.AddWithValue("@uname", userName);
        DataSet usrdtset = GetDataSet(SelectCmd);
        DataTable usr = usrdtset.Tables[0];

        if (usr.Rows.Count > 0)
        {
            string pass = usr.Rows[0]["pwd"].ToString().Trim();
            //  pass = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "md5").ToString().ToLower();
            //password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5").ToString();

            password = MD5Util.getMd5Hash(password);
            if (password.Equals(pass))
            {
                pass = null;
                password = null;
                ReturnVal = 1;
                return ReturnVal;
            }
            else
                return ReturnVal;
        }
        else
        {
            return ReturnVal;
        }
    }

    private DataSet GetDataSet(SqlCommand cmd)
    {
        string sqlcon = ConfigurationManager.AppSettings["ConnectionString_RO"];
        //string sqlcon = ConfigurationManager.AppSettings["ConnectionString_RO"].ConnectionStrings["ConnectionString_RO"].ToString(); //"Server=10.128.5.250;Port=5432;User Id=postgres;Password=postgres;Database=kfdepds";//
        cmd.CommandTimeout = 50000;
        DataSet dt = new DataSet();
        using (SqlConnection con = new SqlConnection(sqlcon))
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();
        }
        return dt;
    }

}

