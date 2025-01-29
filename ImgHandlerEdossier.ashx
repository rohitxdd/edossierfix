<%@ WebHandler Language="C#" Class="ImgHandlerEdossier" %>

using System;
using System.Web;

public class ImgHandlerEdossier : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        MD5Util md5 = new MD5Util();
        string qry = "";
        string edid = "";
        string str = System.Configuration.ConfigurationManager.AppSettings["ConnectionString_RO"];
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(str);

        //string V_type = MD5Util.Decrypt(context.Request.QueryString["type"].ToString(), true);

        if (context.Request.QueryString["edid"].ToString() != null)
        {

            edid = MD5Util.Decrypt(context.Request.QueryString["edid"].ToString(), true);
        }
        //if (V_type == "s")
        //{
        qry = "select doc from CandidateEdossier where edid= " + edid;
        //}
        //else
        //{
        //    qry = "select OLEModule from JobApplicationPhoto where ApplId= " + applid;

        //}
        System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand(qry, con);
        System.Data.SqlClient.SqlDataReader rdr = null;
        con.Open();
        rdr = cmd1.ExecuteReader();
        //if (V_type == "p")
        //{
            while (rdr.Read())
            {
                if (rdr["doc"] != System.DBNull.Value)
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryWrite((byte[])rdr["doc"]);
                }
            }
        //}
        //else
        //{
        //    while (rdr.Read())
        //    {
        //        if (rdr["signature"] != System.DBNull.Value)
        //        {
        //            context.Response.ContentType = "image/jpg";
        //            context.Response.BinaryWrite((byte[])rdr["signature"]);
        //        }
        //    }
        //}
        con.Close();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}