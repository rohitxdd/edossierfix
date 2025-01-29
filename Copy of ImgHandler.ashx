<%@ WebHandler Language="C#" Class="ImgHandler" %>

using System;
using System.Web;

public class ImgHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        string qry="";
        string str = System.Configuration.ConfigurationManager.AppSettings["ConnectionString_RO"];
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(str);
        con.Open();
        if (context.Request.QueryString["type"].ToString() == "s")
        {
            qry = "select signature from JobApplicationPhoto where ApplId= " + context.Request.QueryString["appid"].ToString();
        }
        else
        {
            qry = "select OLEModule from JobApplicationPhoto where ApplId= " + context.Request.QueryString["appid"].ToString();

        }
        System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand(qry, con);
        System.Data.SqlClient.SqlDataReader rdr = null;
        rdr = cmd1.ExecuteReader();
        if (context.Request.QueryString["type"].ToString() == "p")
        {
            while (rdr.Read())
            {
                if (rdr["OLEModule"] != System.DBNull.Value)
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryWrite((byte[])rdr["OLEModule"]);
                }
            }
        }
        else
        {
            while (rdr.Read())
            {
                if (rdr["signature"] != System.DBNull.Value)
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryWrite((byte[])rdr["signature"]);
                }
            } 
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}