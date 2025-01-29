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
/// Summary description for DropdownUtility
/// </summary>
public class DropdownUtility
{
    DataAccess da = new DataAccess();
    DataTable dt = new DataTable();
   
    string str;
    public DropdownUtility()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable fillddlpost()
    {
        try
        {
            str = "select pcode,pname from postmaster order by pname";
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable fillddlpayscale()
    {
        try
        {
            str = "select paycode,payscale from payscale order by payscale";
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable fillddlclass()
    {
        try
        {
            str = "select classcode,classification from classification order by classification";
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable fillddlvacancy()
    {
        try
        {
            str = "select vacancycode,vacancytype from vacancytype order by vacancytype";
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable fillddldept()
    {
        try
        {
            str = "select deptcode,deptname from deptmaster order by deptname";
            dt = da.GetDataTable(str);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable FillDdlPermission(string usertype)
    {
        string strsql = "Select utypecode,utypename from usertype where 1=1";
        if (usertype != "1")
        {
            strsql = strsql + " and utypecode in (5,8,9)";
        }
        strsql = strsql + " order by utypename";
        DataTable dt = new DataTable();
        try
        {
            dt = da.GetDataTable(strsql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }

    
}
