using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for StringUtil
/// </summary>
public class StringUtil
{
	public StringUtil()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static String GetQueryString(String url)
    {
        String queryString = null;
        if (url.Contains("?"))
        {
            try
            {
                queryString = url.Substring(url.IndexOf('?') + 1);
            }
            catch (ArgumentException ex)
            {
                queryString = null;
            }
        }
        return queryString;
    }

    public static String GetWithoutDigest(String queryString)
    {
        String queryStringTmp = null;
        if (queryString.Contains("&Digest"))
        {
            try
            {
                queryStringTmp = queryString.Substring(0,queryString.IndexOf("&Digest"));
            }
            catch (ArgumentException ex)
            {
                queryStringTmp = null;
            }
        }
        return queryStringTmp;
    }

    public static String GetDigest(String queryString)
    {
        String queryStringTmp = null;
        if (queryString.Contains("&Digest"))
        {
            try
            {
                queryStringTmp = queryString.Substring(queryString.IndexOf("&Digest") + 1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                queryStringTmp = null;
            }
        }
        return queryStringTmp;
    }
    public static bool InIntergerRange(String p_value)
    {
        int value = 0;

        try
        {
            value = int.Parse(p_value);
        }
        catch (Exception ex)
        {
            return false;
        }

        if (value < 0 || value > 1000)
            return false;

        return true;
    }
}
