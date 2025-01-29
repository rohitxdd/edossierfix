<%@ WebHandler Language="C#" Class="SigntrHandler" %>

using System;
using System.Web;

public class SigntrHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        MD5Util md5 = new MD5Util();
        string qry = "";
        string str = System.Configuration.ConfigurationManager.AppSettings["ConnectionString_RO"];
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(str);

        string sigid = "", flagfromboard="";
        if (context.Request.QueryString["sigid"]!= null)
        {
            sigid = MD5Util.Decrypt(context.Request.QueryString["sigid"].ToString(), true);
        }
        if (context.Request.QueryString["flagfromboard"] != null)
        {
            flagfromboard = MD5Util.Decrypt(context.Request.QueryString["flagfromboard"].ToString(), true);
        }
        if (sigid != "")
        {
            if (flagfromboard == "Y")
            {
                qry = "select CSig from AdmitCardSignature where SigId= " + sigid;
            }
            else
            {
                qry = "select CSig from SignatureMaster where SigId= " + sigid;
            }

            System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand(qry, con);
            System.Data.SqlClient.SqlDataReader rdr = null;
            con.Open();
            rdr = cmd1.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["CSig"] != System.DBNull.Value)
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryWrite((byte[])rdr["CSig"]);
                }
            }
            con.Close();
            con.Dispose();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}