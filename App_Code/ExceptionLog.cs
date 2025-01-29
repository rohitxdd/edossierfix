using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///Designed & Developed by AnkitaSingh for ExceptionLog handling
///Dated:14-12-2022
/// </summary>
public class ExceptionLog
{
    DataAccess da = new DataAccess();
    //DataTable dt = new DataTable();

    public ExceptionLog()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void SaveException(Exception ex, string page, string clientIP, string userID, string browserInfo)
    {
        string ExceptionDateTime = DateTime.Now.ToString();
        string ExceptionMessage = "Error is :: " + ex.Message.ToString()
        + ">>  Function Is :: " + ex.TargetSite.ToString()
        + ">>  Assemble Is :: " + ex.Source.ToString()
        + ">>  Error Type :: " + ex.GetType().Name.ToString()
        + ">>  Error on Page:: " + page
        + ">>  StackTrace :: " + ex.StackTrace;


        string qry = @"Insert into ExceptionLog (ClientIP,UserID,BrowserInfo,ExceptionDateTime,ExceptionMessage) values 
                        (@ClientIP,@UserID,@BrowserInfo,@ExceptionDateTime,@ExceptionMessage) ";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@ClientIP", SqlDbType.VarChar);

        if (string.IsNullOrEmpty(clientIP))
        {
            param[0].Value = DBNull.Value;
        }
        else
        {
            param[0].Value = clientIP;
        }
        param[1] = new SqlParameter("@UserID", SqlDbType.VarChar);

        if (string.IsNullOrEmpty(userID))
        {
            param[1].Value = DBNull.Value;
        }
        else
        {
            param[1].Value = userID;
        }
       
        param[2] = new SqlParameter("@BrowserInfo", SqlDbType.VarChar);
        if(string.IsNullOrEmpty(browserInfo))
        {
            param[2].Value = DBNull.Value;
        }
        else
        {
            param[2].Value = browserInfo;
        }
        param[3] = new SqlParameter("@ExceptionDateTime", SqlDbType.DateTime);
        param[3].Value = System.DateTime.Now;
        param[4] = new SqlParameter("@ExceptionMessage", SqlDbType.VarChar);
        param[4].Value = ExceptionMessage;
        da.ExecuteParameterizedQuery(qry, param);
    }
}