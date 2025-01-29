using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ValidateDropdown
/// </summary>
public class ValidateDropdown
{
    //DataAccess da = new DataAccess();
    ApplicantData da = new ApplicantData();
    DataTable dt;
	public ValidateDropdown()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public SqlConnection mCon;
    public SqlCommand mDataCom;
    public  string ExecScaler(string strSql)
    {


        mCon = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
        mCon.Open();
        mDataCom = new SqlCommand();
        mDataCom.Connection = mCon;
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = strSql;
        string val;
        val = mDataCom.ExecuteScalar().ToString();
        mCon.Close();
        mCon.Dispose();
        return val;
    }

    public static bool validate(string value, string tblname, string fieldname)
    {
        bool flag = true;
        if (value == "" || value == "0")
        {
            flag = true;
        }
        else
        {
            string str = "Select " + fieldname + " from " + tblname + " where " + fieldname + " = '" + value + "'";
            //DataAccess da = new DataAccess();
            ApplicantData da = new ApplicantData();
            DataTable dt = da.GetDataTable(str);
            if (dt.Rows.Count == 0)
            {
                flag = false;
            }
        }
        return flag;
    }

    //public static bool validate_fms(string value, string tblname, string fieldname)
    //{
    //    bool flag = true;
    //    if (value == "" || value == "0")
    //    {
    //        flag = true;
    //    }
    //    else
    //    {
    //        string str = "Select " + fieldname + " from " + tblname + " where " + fieldname + " = '" + value + "'";
    //        //DataAccess da = new DataAccess();
    //        ApplicantData da = new ApplicantData();
    //        DataTable dt = da.GetDataTable_fms(str);
    //        if (dt.Rows.Count == 0)
    //        {
    //            flag = false;
    //        }
    //    }
    //    return flag;
    //}
    //public static bool validate_ltrms(string value, string tblname, string fieldname)
    //{
    //    bool flag = true;
    //    if (value == "" || value == "0")
    //    {
    //        flag = true;
    //    }
    //    else
    //    {
    //        string str = "Select " + fieldname + " from " + tblname + " where " + fieldname + " = '" + value + "'";
    //        //DataAccess da = new DataAccess();
    //        ApplicantData da = new ApplicantData();
    //        DataTable dt = da.GetDataTable_ltrms(str);
    //        if (dt.Rows.Count == 0)
    //        {
    //            flag = false;
    //        }
    //    }
    //    return flag;
    //}
    //public static bool validate_daily(string value, string tblname, string fieldname)
    //{
    //    bool flag = true;
    //    if (value == "" || value == "0")
    //    {
    //        flag = true;
    //    }
    //    else
    //    {
    //        string str = "Select " + fieldname + " from " + tblname + " where " + fieldname + " = '" + value + "'";
    //        //DataAccess da = new DataAccess();
    //        ApplicantData da = new ApplicantData();
    //        DataTable dt = da.GetDataTable_daily(str);
    //        if (dt.Rows.Count == 0)
    //        {
    //            flag = false;
    //        }
    //    }
    //    return flag;
    //}
    //public static bool validate_website(string value, string tblname, string fieldname)
    //{
    //    bool flag = true;
    //    if (value == "" || value == "0")
    //    {
    //        flag = true;
    //    }
    //    else
    //    {
    //        string str = "Select " + fieldname + " from " + tblname + " where " + fieldname + " = '" + value + "'";
    //        //DataAccess da = new DataAccess();
    //        ApplicantData da = new ApplicantData();
    //        DataTable dt = da.GetDataTable_website(str);
    //        if (dt.Rows.Count == 0)
    //        {
    //            flag = false;
    //        }
    //    }
    //    return flag;
    //}
    //public static bool validate_scheduler(string value, string tblname, string fieldname)
    //{
    //    bool flag = true;
    //    if (value == "" || value == "0")
    //    {
    //        flag = true;
    //    }
    //    else
    //    {
    //        string str = "Select " + fieldname + " from " + tblname + " where " + fieldname + " = '" + value + "'";
    //        //DataAccess da = new DataAccess();
    //        ApplicantData da = new ApplicantData();
    //        DataTable dt = da.GetDataTable_scheduler(str);
    //        if (dt.Rows.Count == 0)
    //        {
    //            flag = false;
    //        }
    //    }
    //    return flag;
    //}
    public static bool validate(string value, string[] listofvalues)
    {
        bool flag = true;
        if (value == "")
        {
            flag = true;
        }
        else
        {
            int cnt = 0;
            for (int i = 0; i < listofvalues.Length; i++)
            {
                if (value.Equals(listofvalues[i], StringComparison.OrdinalIgnoreCase))
                {
                    cnt++;
                    break;
                }
            }
            if (cnt == 0)
            {
                flag = false;
            }
        }
        return flag;
    }





    public static bool validateCourt(string vCourtCode)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vCourtCode == "")
        {
            return true;
        }
        else
        {       
        
            string str = "select  isnull(count(srno) ,0)  from courts where srno='" + vCourtCode + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
    public static bool validateMode(string vMode)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vMode == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(mode) ,0)  from modemaster where mode='" + vMode + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
    public static bool validateStatus(string vStatus)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vStatus == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(status) ,0)  from statusmaster where status='" + vStatus + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
    public static bool validateCategory(string vCategory)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vCategory == "")
        {
            return true;
        }
        else
        {
            string str = "select  isnull(count(categ_id) ,0)  from category  where categ_id='" + vCategory + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

    }
    public static bool validateDepartment(string vdeptcode)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vdeptcode == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(lac_id) ,0)  from lac_master where lac_id='" + vdeptcode + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
    public static bool validateSubjectCategory(string vCategory)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vCategory == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(sub_no) ,0)  from Subject_Master where sub_no='" + vCategory + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
    public static bool validateCounselType(string vCounselType)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vCounselType == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(id) ,0)  from ctype where id='" + vCounselType + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
    public static bool validateCaseCategory(string vCaseCategory)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vCaseCategory == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(basiccatid) ,0)  from basiccasecategory where basiccatid='" + vCaseCategory + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
    public static bool validateSubCourt(string vSubCourt,string Court)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vSubCourt == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(subcourtid) ,0)  from Sub_court_master where subcourtid='" + vSubCourt + "' and courtid='" + Court + "'";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
    public static bool validateBranch(string vBranch, string Dept)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vBranch == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(bcode) ,0)  from branch where bcode='" + vBranch + "' and dcode='" + Dept + "'";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
    public static bool validateCouncel(string vCouncel)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vCouncel == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(panelid) ,0) from counsel where panelid='" + vCouncel + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
    public static bool validateUserType(string vUserType)
    {
        ValidateDropdown v = new ValidateDropdown();
        if (vUserType == "")
        {
            return true;
        }
        else
        {

            string str = "select  isnull(count(utypecode) ,0) from usertype where utypecode='" + vUserType + "' ";
            string value = null;
            value = v.ExecScaler(str);
            if (Convert.ToInt32(value) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
    




}
