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
/// Summary description for ApplicantData
/// </summary>
public class ApplicantData
{
	public ApplicantData()
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

        OpenConnection_RO();
        //Set Command Object Properties
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = strsql;
        mDataCom.CommandTimeout = 2000;
        int Result;
        Result = mDataCom.ExecuteNonQuery();
        ClosedConnection();
        DisposeConnection();
        return Result;
    }
    public int ExecuteSql(String strSPname, SqlParameter[] arraParams)
    {
        OpenConnection_RO();
        //Set Command Object Properties
        mDataCom.CommandType = CommandType.StoredProcedure;
        mDataCom.CommandText = strSPname;
        mDataCom.CommandTimeout = 2000;
        mDataCom.Parameters.Clear();
        if (arraParams != null)
        {
            foreach (SqlParameter loopParam in arraParams)
            {
                mDataCom.Parameters.Add(loopParam);
            }

        }
        int Result;
        Result = mDataCom.ExecuteNonQuery();
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
        OpenConnection_RO();
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
        OpenConnection_RO();
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
        OpenConnection_RO();
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
        OpenConnection_RO();
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = strsql;
        mDataCom.CommandTimeout = 2000;
        int Result = (int)mDataCom.ExecuteScalar();
        ClosedConnection();
        DisposeConnection();
        if (Result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public DataTable GetDataTable(String strsql)
    {
        OpenConnection_RO();
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
        OpenConnection_RO();
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
        OpenConnection_RO();
        mDataCom.CommandType = CommandType.Text;
        mDataCom.CommandText = sqltext;
        mDataCom.CommandTimeout = 2000;
        DataTable dt = new DataTable();
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
        mDa = new SqlDataAdapter();
        mDa.SelectCommand = mDataCom;
        mDa.Fill(dt);
        mDataCom.Parameters.Clear();
        ClosedConnection();
        DisposeConnection();
        return dt;
    }
    public int ExecuteSqlTransaction(string[] strsqls)
    {

        OpenConnection_RO();
        int Result = 0;
        //Set Command Object Properties
        using (SqlTransaction transaction = mCon.BeginTransaction())
        {
            try
            {
                for (int i = 0; strsqls[i] != null; i++)
                {
                    mDataCom.CommandType = CommandType.Text;
                    mDataCom.CommandText = strsqls[i];
                    mDataCom.CommandTimeout = 2000;
                    mDataCom.Transaction = transaction;
                    Result = mDataCom.ExecuteNonQuery();
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

    #endregion
    #region all private methods
    private void OpenConnection_RO()
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
    private void OpenConnection_RW()
    {

        if (mCon == null)
        {
            mCon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString_RW"]);

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
