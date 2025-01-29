using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;


/// <summary>
/// Summary description for MD5Util
/// </summary>
public class MD5Util
{
    String secretSalt = "nic$#sla@citizen";
    string[] StrArray = null;
	public MD5Util()
	{
        secretSalt = "nic$#sla@citizen";
		//
		// TODO: Add constructor logic here
		//
	}
    public String GetDigest(String temperProofParams)
    {
        string Digest = null;
        string input = secretSalt+temperProofParams + secretSalt;
        byte[] hashedDataBytes = null;
        Encoding encoder = ASCIIEncoding.Unicode;
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        hashedDataBytes = md5.ComputeHash(encoder.GetBytes(input));
        Digest = Convert.ToBase64String(hashedDataBytes).TrimEnd("=".ToCharArray());

        return Digest;
    }

    public static string Encrypt(string toEncrypt, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        System.Configuration.AppSettingsReader settingsReader =
                                            new AppSettingsReader();
        // Get the key from config file

        string key = (string)settingsReader.GetValue("SecurityKey",
                                                         typeof(String));
        //System.Windows.Forms.MessageBox.Show(key);
        //If hashing use get hashcode regards to your key
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            // of the Cryptographic service provide. Best Practice

            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes.
        //We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        //transform the specified region of bytes array to resultArray
        byte[] resultArray =
          cTransform.TransformFinalBlock(toEncryptArray, 0,
          toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor
        tdes.Clear();
        //Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string Decrypt(string cipherString, bool useHashing)
    {
        byte[] keyArray;
        String flag = "Error";
        byte[] toEncryptArray = null;
        //get the byte code of the string

        try
        {
            string stringToDecrypt = cipherString.Replace(" ", "+");
            toEncryptArray = Convert.FromBase64String(stringToDecrypt);
        }
        catch (Exception e)
        {
            return flag;
        }

        System.Configuration.AppSettingsReader settingsReader =
                                            new AppSettingsReader();
        //Get your key from config file to open the lock!
        string key = (string)settingsReader.GetValue("SecurityKey",
                                                     typeof(String));

        if (useHashing)
        {
            //if hashing was used get the hash code with regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //release any resource held by the MD5CryptoServiceProvider

            hashmd5.Clear();
        }
        else
        {
            //if hashing was not implemented get the byte code of the key
            keyArray = UTF8Encoding.UTF8.GetBytes(key);
        }

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes. 
        //We choose ECB(Electronic code Book)

        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(
                             toEncryptArray, 0, toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor                
        tdes.Clear();
        //return the Clear decrypted TEXT
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    //public String GetEncrypt(String temperProofParams)
    //{   
    //    Encryption.Symmetric sym = New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael);
    //    //Dim key As New Encryption.Data("My Password");
    //    //Dim encryptedData As Encryption.Data;
    //    //encryptedData = sym.Encrypt(New Encryption.Data("Secret Sauce"), key);
    //    //Dim base64EncryptedString as String = encryptedData.ToBase64;
    //}

    //public String GetDecrypt(String temperProofParams)
    //{
    //        Dim sym As New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
    //        Dim key As New Encryption.Data("My Password")
    //        Dim encryptedData As New Encryption.Data
    //        encryptedData.Base64 = base64EncryptedString
    //        Dim decryptedData As Encryption.Data
    //        decryptedData = sym.Decrypt(encryptedData, key)
    //        Console.WriteLine(decryptedData.ToString)
    //}

    /*
     * Uses : 
     * CreateTamperProofURL("TamperProofURLs.B.aspx","NonTamperProof=1", "TP1=Scott&TP2=27&TP3=False")
     * */
    public String CreateTamperProofURL(String url, String nonTamperProofParams, String tamperProofParams)
    {
        String tmpURL = url;
        if (nonTamperProofParams!=null || tamperProofParams != null)
        {
            url += "?";
        }
        if (nonTamperProofParams != null && nonTamperProofParams.Length > 0)
        {
            url += nonTamperProofParams;

        }
        if (nonTamperProofParams != null && tamperProofParams != null && tamperProofParams.Length > 0)
            url += "&";
        if (tamperProofParams != null &&  tamperProofParams.Length > 0)
            url += tamperProofParams;
        if (tamperProofParams != null && tamperProofParams.Length > 0)
            url += "&Digest="+GetDigest(tamperProofParams);
        return url;
    }

    public String CreateEncodeUrlParam(String urlParam)
    {
        String tmpURL = urlParam;
        
        if (tmpURL != null && tmpURL.Length > 0)
            urlParam = GetDigest(tmpURL);
        return urlParam;
    }

    public Boolean IsURLTampered(String tamperProofParams, String digest)
    {
        Boolean flag = true;
        String receivedDigest = null;
        String expectedDigest = null;

        if (digest == null || !digest.Contains("="))
        {
            return flag;
        }
        try
        {
            expectedDigest = GetDigest(tamperProofParams);
            receivedDigest = digest.Substring(digest.IndexOf("=") + 1);

            if (receivedDigest != null)
            {
                receivedDigest = receivedDigest.Replace(" ", "+");
                if (receivedDigest.CompareTo(expectedDigest) == 0)
                    flag = false;
            }
        }
        catch (Exception e)
        {
            flag = true;
        }
        return flag;
    }
     /*
    public String IsURLTampered(String tamperProofParams, String digest)
    {
        String flag = "true";
        String expectedDigest = GetDigest(tamperProofParams);
        String receivedDigest = digest;

        if (receivedDigest != null)
        {
            receivedDigest = receivedDigest.Replace(" ", "+");
            if (receivedDigest.CompareTo(expectedDigest) == 0)
                flag = "false";
        }
        return flag;
    }*/

    public int CheckSubServiceString(string strin, string UserType,string DeptCode)
    {
        PopulateDDsubService(UserType,DeptCode);
        int strOut = 0;
        
        //for (int k=0;k<=StrArray.Length;k++)
        //{
        //    if (strin== StrArray[k])
        //    {
        //        strOut = 1;
        //        break;
        //    }
        //}

        foreach (string strWord in StrArray)
        {
            if (strin.IndexOf(strWord, 0) >= 0)
            {
                strOut = 1;
                break;
            }
        }
        return strOut;
    }

    public int CheckMonthParameter(string strin)
    {
        string[] MonthArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };      
        int strOut = 0;

        for (int k = 0; k <= MonthArray.Length; k++)
        {
            if (strin == MonthArray[k])
            {
                strOut = 1;
                break;
            }
        }
        //foreach (string strWord in MonthArray)
        //{
        //    if (strin.IndexOf(strWord, 0) >= 0)
        //    {
        //        strOut = 1;
        //        break;
        //    }
        //}
        return strOut;
    }

    public int CheckYearParameter(string strin)
    {
        string[] MonthArray = new string[] { "2009","2010","2011"};
        int strOut = 0;

        for (int k = 0; k <= MonthArray.Length; k++)
        {
            if (strin == MonthArray[k])
            {
                strOut = 1;
                break;
            }
        }
        //foreach (string strWord in MonthArray)
        //{
        //    if (strin.IndexOf(strWord, 0) >= 0)
        //    {
        //        strOut = 1;
        //        break;
        //    }
        //}
        return strOut;
    }

    public void PopulateDDsubService(string UserType,string Deptcode)
    {
        String StrConn = null;
        StrConn = ConfigurationManager.AppSettings["SqlConnect1"].ToString();
        SqlConnection SqlConn = new SqlConnection(StrConn);
        String query = null;

        if (UserType == "Monitor" || UserType == "Admin" || UserType == "Guest")
            query = @"select distinct SubServiceName,deptcode from subservicemaster WHERE active = 'Y' 
                        order by deptcode";
        else
            query = @"select distinct SubServiceName,deptcode from subservicemaster WHERE active = 'Y' 
                        and deptCode = " + Deptcode.ToString() + " order by deptcode";

        if (SqlConn.State == ConnectionState.Closed)
            SqlConn.Open();

            SqlCommand SqlCmd1 = new SqlCommand(query, SqlConn);
            SqlDataAdapter adapter = new SqlDataAdapter(SqlCmd1);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            //ListItem lstItem = new ListItem();
            //lstItem.Text = "All";
            //lstItem.Value = "All";

            int RowCount = dt.Rows.Count;
            int iStr = 0;

            string Strs = "";
            StrArray=new string[RowCount+1];
            StrArray[iStr] = "All";

            foreach (DataRow row in dt.Rows)
            {
                //ListItem l = new ListItem();
                //l.Text = row["SubServiceName"].ToString().Trim();
                //l.Value = row["SubServiceName"].ToString().Trim();
                //ddSubService.Items.Add(l);

                iStr = iStr + 1;
                StrArray[iStr] = row["SubServiceName"].ToString().Trim();

            }
            //if (Session["TypeApplDb"] != null)
            //{
            //    ddSubService.SelectedValue = Session["TypeApplDb"].ToString();
            //}
    }

    public static string getMd5Hash(string input)
    {
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        byte[] data = null;

        Encoding encoder = Encoding.ASCII;
        //Encoding encoder = Encoding.Unicode;

        data = md5Hasher.ComputeHash(encoder.GetBytes(input));
        //data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();

    }
    public static string md5(string sPassword)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
    }
}
