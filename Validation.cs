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
/// Summary description for Validation
/// </summary>
public class Validation
{
    //public static string[] blackList = {"--",";--",";","/*","*/","@@","@","'",
    //                                       "char","nchar","varchar","nvarchar",
    //                                       "alter","begin","cast","create","cursor","declare","delete","drop","end","exec","execute",
    //                                       "fetch","insert","kill","open",
    //                                       "select", "sys","sysobjects","syscolumns",
    //                                       "table","update"};
    //public static string[] blackList1 = {"--",";--",";","/*","*/","'",
    //                                       "char","nchar","varchar","nvarchar",
    //                                       "alter","begin","cast","create","cursor","declare","delete","drop","end","exec","execute",
    //                                       "fetch","insert","kill","open",
    //                                       "select", "sys","sysobjects","syscolumns",
    //                                       "table","update"};

    //&.(),-(6)
    public static string[] sLevel0 ={ "~", "`", "!", "@", "#", "$", "%", "^", "*", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "/", "''", "--", ";--", "/*", "*/", "@@" };
    // @ # &(3)
    public static string[] sLevel1 ={ "~", "`", "!", "$", "%", "^", "*", "(", ")", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ".", "?", "/", ",", "''", "--", ";--", "/*", "*/", "@@" };
    // - / .(3) 
    public static string[] sLevel2 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ".", "?", ",", "''", "--", ";--", "/*", "*/", "@@" };
    //.?_/(4)
    public static string[] sLevel3 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ",", "''", "--", ";--", "/*", "*/", "@@" };
    //&.()-/,(7)
    public static string[] sLevel4 ={ "~", "`", "!", "@", "#", "$", "%", "^", "*", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "''", "--", ";--", "/*", "*/", "@@" };
    // /()(3)
    public static string[] sLevel5 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ".", "?", ",", "''", "--", ";--", "/*", "*/", "@@" };
    //.(),/(5)
    public static string[] sLevel6 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "''", "--", ";--", "/*", "*/", "@@" };
    //.,-/()(6)
    public static string[] sLevel7 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "''", "--", ";--", "/*", "*/", "@@" };
    // ()./  (4)  
    public static string[] sLevel8 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", ",", "''", "--", ";--", "/*", "*/", "@@" };
    // @&.(),-/(8)
    public static string[] sLevel9 ={ "~", "`", "!", "#", "$", "%", "^", "*", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", ",", "''", "--", ";--", "/*", "*/", "@@" };
    //,(1)
    public static string[] sLevel10 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ".", "?", "/", "''", "--", ";--", "/*", "*/", "@@" };
    // /.(2)
    public static string[] sLevel11 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", ",", "''", "--", ";--", "/*", "*/", "@@" };
    // /(1)
    public static string[] sLevel12 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ".", "?", ",", "''", "--", ";--", "/*", "*/", "@@" };
    
    //-/(2)
    public static string[] sLevel13 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ".", "?", ",", "''", "--", ";--", "/*", "*/", "@@" };
    //.(1)
    public static string[] sLevel15 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "/", ",", "''", "--", ";--", "/*", "*/", "@@" };
    // @&.()-/ (7)
    public static string[] sLevel16 ={ "~", "`", "!", "#", "$", "%", "^", "*", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", ",", "''", "--", ";--", "/*", "*/", "@@" };
    // .,/-(4)
    public static string[] sLevel14 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "''", "--", ";--", "/*", "*/", "@@" };
    // \&.(),-/ (8)
    public static string[] sLevel17 ={ "~", "`", "!", "@", "#", "$", "%", "^", "*", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "''", ";--", "/*", "*/", "@@" };

    // @\&.(),-/(9)
    public static string[] sLevel18 ={ "~", "`", "!", "#", "$", "%", "^", "*", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "''", "--", ";--", "/*", "*/", "@@" };


    // ,- (2)
    public static string[] sLevel19 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ".", "?", "/", "''", "--", ";--", "/*", "*/", "@@" };
    //, (1)
    public static string[] sLevel20 ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "/", "''", "--", ";--", "/*", "*/", "@@" };
    //-_.@  "."
    public static string[] sLevel21 ={ "~", "`", "!", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", "?", "/", "''", "--", ";--", "/*", "*/", "@@","," };
    //Nothing is allowed
    public static string[] sLevel ={ "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "{", "}", "[", "]", "|", ":", ";", "'", "<", ">", ".", "?", "/", ",", "''", "--", ";--", "/*", "*/", "@@" };


    public const string msg = "Invalid Characters";
    public static string[] blackList = { "--", ";--", ";", "/*", "*/", "@@", "@", "'" };
    public static string[] blackList1 = { "--", ";--", ";", "/*", "*/", "'" };




    public Validation()
    {

    }

    
    public static string chklogin(string input)
    {
        string val = "Pass";
        for (int i = 0; i < blackList.Length; i++)
        {
            if ((input.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
            {
                val = "Error";
                break;
            }
        }
        return val;
    }

    public static string chkpwd(string input)
    {
        string val = "Pass";
        for (int i = 0; i < blackList1.Length; i++)
        {
            if ((input.IndexOf(blackList1[i], StringComparison.OrdinalIgnoreCase) >= 0))
            {
                val = "Error";
                break;
            }
        }
        return val;
    }

    public static bool chkescape(string input)
    {
       bool val = false; 
        
        for (int i = 0; i < blackList.Length; i++)
        {
            if ((input.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool check_essential(string input)
    {
        bool val = false;
        if (input != "")
        {
            val = true;
        }
        return val;
    }
    public static bool chkescape1(string input)
    {
        bool val = false;
        for (int i = 0; i < blackList1.Length; i++)
        {
            if ((input.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel.Length; i++)
        {
            if (input.IndexOf(sLevel[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }

    public static bool chkLevel0(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel0.Length; i++)
        {
            if (input.IndexOf(sLevel0[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel1(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel1.Length; i++)
        {
            if (input.IndexOf(sLevel1[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel2(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel2.Length; i++)
        {
            if (input.IndexOf(sLevel2[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel3(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel3.Length; i++)
        {
            if (input.IndexOf(sLevel3[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel4(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel4.Length; i++)
        {
            if (input.IndexOf(sLevel4[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel5(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel5.Length; i++)
        {
            if (input.IndexOf(sLevel5[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel6(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel6.Length; i++)
        {
            if (input.IndexOf(sLevel6[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel7(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel7.Length; i++)
        {
            if (input.IndexOf(sLevel7[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel8(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel8.Length; i++)
        {
            if (input.IndexOf(sLevel8[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel9(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel9.Length; i++)
        {
            if (input.IndexOf(sLevel9[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel10(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel10.Length; i++)
        {
            if (input.IndexOf(sLevel10[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel11(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel11.Length; i++)
        {
            if (input.IndexOf(sLevel11[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel12(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel12.Length; i++)
        {
            if (input.IndexOf(sLevel12[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel13(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel13.Length; i++)
        {
            if (input.IndexOf(sLevel13[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel14(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel14.Length; i++)
        {
            if (input.IndexOf(sLevel14[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel15(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel15.Length; i++)
        {
            if (input.IndexOf(sLevel15[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel16(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel16.Length; i++)
        {
            if (input.IndexOf(sLevel16[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel17(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel17.Length; i++)
        {
            if (input.IndexOf(sLevel17[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel18(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel18.Length; i++)
        {
            if (input.IndexOf(sLevel18[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel19(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel19.Length; i++)
        {
            if (input.IndexOf(sLevel19[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel20(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel20.Length; i++)
        {
            if (input.IndexOf(sLevel20[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
    public static bool chkLevel21(string input)
    {
        bool val = false;
        for (int i = 0; i < sLevel21.Length; i++)
        {
            if (input.IndexOf(sLevel21[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                val = true;
                break;
            }
        }
        return val;
    }
}
