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
using System.Collections.Generic;


/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
	public DataAccess()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region All Class level Variables
        SqlConnection mCon;
        SqlCommand mDataCom;
        SqlDataAdapter mDa;
    #endregion

    #region All Public Methods
    
    /// <summary>
    /// <return>Int the no. of rows affected</return>
    /// </summary>
    /// <param name="strsql"></param>
    /// <returns></returns>
    public int ExecuteSql(string strsql)
    {
       
        OpenConnection();
        //Set Command Object Properties
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText =strsql ;
        mDataCom.CommandTimeout=2000;
        int Result;
        Result =mDataCom.ExecuteNonQuery();
        ClosedConnection();
        DisposeConnection();
        return Result;
    }
    public int ExecuteSql(String strSPname, SqlParameter[] arraParams)
    {
        OpenConnection();
        //Set Command Object Properties
        mDataCom.CommandType = CommandType.StoredProcedure;
        mDataCom.CommandText = strSPname;
        mDataCom.CommandTimeout=2000;
        mDataCom.Parameters.Clear();
        if (arraParams != null)
        {
            foreach (SqlParameter loopParam in arraParams)
            {
                mDataCom.Parameters.Add(loopParam);
            }

        }
        int Result;
        Result =mDataCom.ExecuteNonQuery();
        ClosedConnection();
        DisposeConnection();
        return Result;
     }

    /// <summary>
    /// execute query and returns the no. of rows affected
    /// </summary>
    /// <param name="sqlqry"></param>
    /// <param name="arraParams"></param>
    /// <returns></returns>
    public int ExecuteParameterizedQuery(String sqlqry, SqlParameter[] arraParams)
    {
        OpenConnection();
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = sqlqry;
        DataTable dt = new DataTable();
        if (arraParams != null)
        {
            foreach (SqlParameter param in arraParams)
            {
                mDataCom.Parameters.Add(param);
            }
        }
        int Result;
        Result = mDataCom.ExecuteNonQuery();
        ClosedConnection();
        DisposeConnection();
        return Result;                          
    }
    /// <summary>
    /// execute query and returns the first column of first row
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="arrparams"></param>
    /// <returns></returns>
    public string ExecScaler(string strSql)
    {
        //to open the connection
        OpenConnection();
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = strSql;
        string val;
        val = mDataCom.ExecuteScalar().ToString();
        ClosedConnection();
        DisposeConnection();
        return val;
    }

    /// <summary>
    /// execute query and returns the first column of first row
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="arrparams"></param>
    /// <returns></returns>
    public string ExecScaler(string strSql, SqlParameter[] arrparams)
    {
        //to open the connection
        OpenConnection();
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = strSql;
        if (arrparams != null)
        {
            foreach (SqlParameter param in arrparams)
            {
                mDataCom.Parameters.Add(param);
            }
        }
        string val;
        val = mDataCom.ExecuteScalar().ToString();
        ClosedConnection();
        DisposeConnection();
        return val;
    }


    //This is used for read Data from the table
    public bool IsExist(string strsql)
    {
        OpenConnection();
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = strsql;
        mDataCom.CommandTimeout = 2000;
        int Result = (int)mDataCom.ExecuteScalar();
        ClosedConnection();
        DisposeConnection();
        if(Result >0)
        {
            return  true ;
        }
        else
        { 
            return false ;
        }

    }
    public DataTable GetDataTable(String strsql)
    {
        OpenConnection();
        DataTable dt = new DataTable();
        mDa = new SqlDataAdapter();
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = strsql;
        mDataCom.CommandTimeout = 2000;
        mDa.SelectCommand = mDataCom;
        mDa.Fill(dt);
        ClosedConnection();
        DisposeConnection();
        return dt;
    }
    public DataTable GetDataTable(string strSPName, SqlParameter[] arraParmas)
    {
        OpenConnection();
        DataTable dt = new DataTable();
        mDataCom.CommandType = CommandType.StoredProcedure;
        mDataCom.CommandText = strSPName;
        if (mDataCom.Parameters.Count > 0)
        {
            mDataCom.Parameters.Clear();
        }
        if (arraParmas != null)
        {
            foreach (SqlParameter param in arraParmas)
            {
                mDataCom.Parameters.Add(param);
            }
        }
        mDa.SelectCommand = mDataCom;
        mDa.Fill(dt);
        ClosedConnection();
        DisposeConnection();
        return dt;
    }
    /// <summary>
    /// returns datatable for a parameterized query
    /// </summary>
    /// <param name="sqltext"></param>
    /// <param name="arraParmas"></param>
    /// <returns></returns>
    public DataTable GetDataTableQry(string sqltext, SqlParameter[] arraParmas)
    {
        OpenConnection();
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = sqltext;
        DataTable dt = new DataTable();
        if (arraParmas != null)
        {
            foreach (SqlParameter param in arraParmas)
            {
                mDataCom.Parameters.Add(param);
            }
        }
        mDa = new SqlDataAdapter();
        mDa.SelectCommand = mDataCom;
        mDa.Fill(dt);
        ClosedConnection();
        DisposeConnection();
        return dt;        
    }
    public int ExecuteSqlTransaction(Dictionary<int,dict> temp_dict)
    {

        OpenConnection();
        int Result = 0;
        //Set Command Object Properties
        using (SqlTransaction transaction = mCon.BeginTransaction())
        {
            try
            {
                foreach(var i in temp_dict)
                {
                    mDataCom.CommandType = CommandType.Text;
                    dict temp=(dict)i.Value;
                    mDataCom.CommandText = temp.QString;
                    SqlParameter[] param = temp.param;
                    mDataCom.CommandTimeout = 2000;
                    
                    if (param != null)
                    {
                        foreach (SqlParameter par in param)
                        {
                            mDataCom.Parameters.Add(par);
                        }
                    }
                    mDataCom.Transaction = transaction;
                   Result = Result+mDataCom.ExecuteNonQuery();
                    mDataCom.Parameters.Clear();
                }
                
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Result = 0;
            }
        }
        ClosedConnection();
        DisposeConnection();
        return Result;
    }
    public int call_challan_proc(DataTable dt)
    {
        OpenConnection();
        //Create a command object that calls the stored procedure
        SqlCommand cmdCustomer = new SqlCommand("Update_challan", mCon);
        cmdCustomer.CommandTimeout = 2500;
        cmdCustomer.CommandType = CommandType.StoredProcedure;
        //Create a parameter using the new SQL DB type viz. Structured to pass as table value parameter
        SqlParameter paramCustomer = cmdCustomer.Parameters.Add("@TargetUDT", SqlDbType.Structured);
        SqlParameter param = new SqlParameter("@innoOut", SqlDbType.Int);
        param.Direction = ParameterDirection.Output;
        cmdCustomer.Parameters.Add(param);
        paramCustomer.Value = dt;
        //Execute the query
       int i=cmdCustomer.ExecuteNonQuery();

        ClosedConnection();
        DisposeConnection();
        return i;
    }

    #endregion
    #region all private methods
    private void OpenConnection()
    {
        
            if (mCon == null)
            {
                mCon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString_RO"]);
        
                if (mCon.State == ConnectionState.Closed)
                {

                    mCon.Open();
                    mDataCom = new SqlCommand();
                    mDataCom.Connection = mCon;
                }
            }
        
    }
    private void ClosedConnection()
    {
        if (mCon.State == ConnectionState.Open)
        {
            mCon.Close();
        }
    }
    private void DisposeConnection()
    {
        if (mCon != null)
        {
            mCon.Dispose();
            mCon = null;
        }
    }
    #endregion
}
