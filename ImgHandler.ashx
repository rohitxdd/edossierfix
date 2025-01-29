<%@ WebHandler Language="C#" Class="ImgHandler" %>

using System;
using System.Web;

public class ImgHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        MD5Util md5 = new MD5Util();
        string qry = "";
        string applid = "";
        string flagfromboard = "", datatransfer = "" ;
        string str = System.Configuration.ConfigurationManager.AppSettings["ConnectionString_RO"];
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(str);

        string V_type = MD5Util.Decrypt(context.Request.QueryString["type"].ToString(), true);
        if (context.Request.QueryString["flagfromboard"] != null)
        {
            flagfromboard = MD5Util.Decrypt(context.Request.QueryString["flagfromboard"].ToString(), true);
        }
        if (context.Request.QueryString["id"].ToString() != null)
        {
            applid = MD5Util.Decrypt(context.Request.QueryString["id"].ToString(), true);
        }
        if (context.Request.QueryString["datatransfer"] != null)
        {
            if (context.Request.QueryString["datatransfer"].ToString() != null)
            {
                datatransfer = MD5Util.Decrypt(context.Request.QueryString["datatransfer"].ToString(), true);
            }
        }
        if (V_type == "s")
        {
            if (flagfromboard == "Y")
            {
                qry = "select signature from AdmitCardPhoto where ApplId= " + applid;
            }
            else
            {
                if (datatransfer == "Y")
                {
                    qry = "select signature from dsssbonline_recdapp.dbo.JobApplicationPhoto where ApplId= " + applid;
                }
                else
                {
                    qry = "select signature from JobApplicationPhoto where ApplId= " + applid;
                }
            }
        }
        else if (V_type == "l")
        {
            if (flagfromboard == "Y")
            {
                qry = "select LTI from AdmitCardPhoto where ApplId= " + applid;
            }
            else
            {
                if (datatransfer == "Y")
                {
                    qry = "select LTI from dsssbonline_recdapp.dbo.JobApplicationPhoto where ApplId= " + applid;
                }
                else
                {
                    qry = "select LTI from JobApplicationPhoto where ApplId= " + applid;
                }
            }
        }
        else if (V_type == "r")
        {
            if (flagfromboard == "Y")
            {
                qry = "select RTI from AdmitCardPhoto where ApplId= " + applid;
            }
            else
            {
                if (datatransfer == "Y")
                {
                    qry = "select RTI from dsssbonline_recdapp.dbo.JobApplicationPhoto where ApplId= " + applid;
                }
                else
                {
                    qry = "select RTI from JobApplicationPhoto where ApplId= " + applid;
                }
            }
        }
        else
        {
            if (flagfromboard == "Y")
            {
                qry = "select OLEModule from AdmitCardPhoto where ApplId= " + applid;
            }
            else
            {
                if (datatransfer == "Y")
                {
                    qry = "select OLEModule from dsssbonline_recdapp.dbo.JobApplicationPhoto where ApplId= " + applid;
                }
                else
                {
                    qry = "select OLEModule from JobApplicationPhoto where ApplId= " + applid;
                }
            }

        }
        System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand(qry, con);
        System.Data.SqlClient.SqlDataReader rdr = null;
        con.Open();
        rdr = cmd1.ExecuteReader();
        if (V_type == "p")
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
        else if (V_type == "s")
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
        else if (V_type == "l")
        {
            while (rdr.Read())
            {
                if (rdr["LTI"] != System.DBNull.Value)
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryWrite((byte[])rdr["LTI"]);
                }
            }
        }
        else
        {
            while (rdr.Read())
            {
                if (rdr["RTI"] != System.DBNull.Value)
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryWrite((byte[])rdr["RTI"]);
                }
            }
        }
        con.Close();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}