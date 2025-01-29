using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{
    
    public Utility()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static  ListItem ddl_Zero_Value()
    {
        ListItem item = new ListItem();
        item.Text = "--All--";
        item.Value = "";
        return item;
    }
    public static DropDownList populateEducationClass(DropDownList ddl)
    {
        ListItem Items=new ListItem();
        ddl.Items.Clear();
            ddl.Items.Insert(0,"--Select--");
        
            ddl.Items.Insert(1, "Distinction");
            ddl.Items.Insert(2, "First");
            ddl.Items.Insert(3, "Second");
            ddl.Items.Insert(4, "Pass");
            ddl.Items.Insert(5, "Not Applicable");
            return ddl;
    }
    public static ListItem ddl_Select_Value()
    {
        ListItem item = new ListItem();
        item.Text = "--Select--";
        item.Value = "";
        return item;
    }

    public static ListItem ddl_Select_Value(string text)
    {
        ListItem item = new ListItem();
        item.Text = text;
        item.Value = "";
        return item;
    }

    public static ListItem year_Select_Value()
    {
        ListItem item = new ListItem();
        item.Text = "--Year--";
        item.Value = "";
        return item;
    }

    public static string smallDatetime(string dt)
    {
        string data = dt;
        string[] parts = data.Split('/');
        string date2 = Convert.ToInt32(parts[2]) + "/" + Convert.ToInt32(parts[1]) + "/" + Convert.ToInt32(parts[0]);
        return date2;
    }
    public static bool DateBetween(DateTime start, DateTime end, DateTime date)
    {
        if (date <= start && date >= end)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IslessthanEqualto(string  FirstDate, DateTime secondtime)
    {
        DateTime Cdate = Convert.ToDateTime(smallDatetime(FirstDate.ToString()));
        int fDay = Cdate.Day;
        int fMonth = Cdate.Month;
        int fYear = Cdate.Year;

        int sDay = secondtime.Day;
        int sMonth = secondtime.Month;
        int sYear = secondtime.Year;

        if (fDay <= sDay && fMonth <= sMonth && fYear <= sYear)
        {
            return true ;
        }
        else
        {
            return  false ;
        }

        
    }

    public static string formatDate(string pickDate)
    {
        if (pickDate != null && pickDate != "")
        {
            string getdate;
            string[] condate = new string[8];
            if (pickDate.Length > 10)
            {
                pickDate = pickDate.Remove(10);
            }
            condate = pickDate.Split('-');
            if (condate.Length == 3)
            {
                getdate = condate[2] + "/" + condate[1] + "/" + condate[0];
            }
            else
            {
                condate = new string[8];
                condate = pickDate.Split('/');
                getdate = condate[2] + "/" + condate[1] + "/" + condate[0];
            }

            return getdate;
        }
        else
        { 
         return "";
        }
    }

    public static string formatDate(DateTime pickDate)
    {
        string day = pickDate.Day.ToString();
        string month = pickDate.Month.ToString();
        string year = pickDate.Year.ToString();

        if (day.Length == 1)
        {
            day = "0" + day;
        }
        if (month.Length == 1)
        {
            month = "0" + month;
        }

        string getdate = year + "/" + month + "/" + day;  

        return getdate;
    }

    public static string formatDateinDMY(string InputDate)
    {
        string getdate = "";
        DateTime pickDate;
        if (InputDate != null && InputDate !="")
        {
            pickDate=Convert.ToDateTime(InputDate);
            string day = pickDate.Day.ToString();
            string month = pickDate.Month.ToString();
            string year = pickDate.Year.ToString();

            if (day.Length == 1)
            {
                day = "0" + day;
            }
            if (month.Length == 1)
            {
                month = "0" + month;
            }

           getdate = day + "/" + month + "/" + year;
        }
       
        return getdate;
    }

    public static string formatDateinDMY(DateTime InputDate)
    {
        string getdate = "";

        string day = InputDate.Day.ToString();
        string month = InputDate.Month.ToString();
        string year = InputDate.Year.ToString();

        if (day.Length == 1)
        {
            day = "0" + day;
        }
        if (month.Length == 1)
        {
            month = "0" + month;
        }

        getdate = day + "/" + month + "/" + year;
        return getdate;
    }

    public static string formatDateinDMYWithTime(string InputDate)
    {
        string getdate = "";
        DateTime pickDate;
        if (InputDate != null && InputDate != "")
        {
            pickDate = Convert.ToDateTime(InputDate);
            string day = pickDate.Day.ToString();
            string month = pickDate.Month.ToString();
            string year = pickDate.Year.ToString();
            string time = pickDate.TimeOfDay.ToString();

            if (day.Length == 1)
            {
                day = "0" + day;
            }
            if (month.Length == 1)
            {
                month = "0" + month;
            }

            getdate = day + "/" + month + "/" + year + " " + time;
        }

        return getdate;
    }
    public static string formatDateinMDY(string InputDate)
    {
        string getdate = "";
        DateTime pickDate;
        if (InputDate != null && InputDate != "")
        {
            pickDate = Convert.ToDateTime(InputDate);
            string day = pickDate.Day.ToString();
            string month = pickDate.Month.ToString();
            string year = pickDate.Year.ToString();

            if (day.Length == 1)
            {
                day = "0" + day;
            }
            if (month.Length == 1)
            {
                month = "0" + month;
            }

            getdate = month + "/" + day + "/" + year;
        }

        return getdate;
    }
    public static string formatDatewithtime(DateTime pickDate)
    {
        string getdate = "";
        if (pickDate != null)
        {
            string day = pickDate.Day.ToString();
            string month = pickDate.Month.ToString();
            string year = pickDate.Year.ToString();
            string time = pickDate.TimeOfDay.ToString();

            if (day.Length == 1)
            {
                day = "0" + day;
            }
            if (month.Length == 1)
            {
                month = "0" + month;
            }

            getdate = year + "/" + month + "/" + day + " " + time;
        }
        return getdate;
    }

    /// <summary>
    /// Compares dt1 is less than, greater than or equal to dt2. 
    /// </summary>
    /// <param name="dt1"></param>
    /// <param name="dt2"></param>
    /// <returns>Zero for equal, 1 for dt1 is greater and -1 for dt1 is smaller</returns>
    public static int comparedatesDMY(string dt1, string dt2)
    {
        int day1 = Convert.ToInt32(dt1.Substring(0, 2));
        int day2 = Convert.ToInt32(dt2.Substring(0, 2));
        int month1 = Convert.ToInt32(dt1.Substring(3, 2));
        int month2 = Convert.ToInt32(dt2.Substring(3, 2));
        int yr1 = Convert.ToInt32(dt1.Substring(6, 4));
        int yr2 = Convert.ToInt32(dt2.Substring(6, 4));

        int flag = 0;
        if (yr1 > yr2)
        {
            flag = 1;
        }
        else if (yr1 < yr2)
        {
            flag = -1;
        }
        else
        {
            if (month1 > month2)
            {
                flag = 1;
            }
            else if (month1 < month2)
            {
                flag = -1;
            }
            else
            {
                if (day1 > day2)
                {
                    flag = 1;
                }
                else if (day1 < day2)
                {
                    flag = -1;
                }
            }
        }

        return flag;
    }

    public static string putstring(string headline)
    {
        string str = headline;


        if (str.Contains(";"))
        {
            str = str.Replace(";", " semicolon ");
        }

        if (str.Contains("'"))
        {
            str = str.Replace("'", " quote ");
        }
        if (str.Contains("@"))
        {
            str = str.Replace("@", "[at]");
        }
        if (str.Contains("."))
        {
            str = str.Replace(".", "[dot]");
        }

        return str;

    }
    public static string getstring(string headline)
    {
        string str = headline;


        if (str.Contains(" semicolon "))
        {
            str = str.Replace(" semicolon ", ";");
        }

        if (str.Contains(" quote "))
        {
            str = str.Replace(" quote ", "'");
        }
        if (str.Contains("[at]"))
        {
            str = str.Replace("[at]", "@");
        }
        if (str.Contains("[dot]"))
        {
            str = str.Replace("[dot]", ".");
        }


        return str;





    }
    public static string adddate(string pickDate)
    {
        string dob = pickDate;
        string[] condate = new string[8];

        condate[2] = dob.Substring(dob.Length - 4);
        string tst = dob.Remove(dob.Length - 4);
        condate[1] = tst.Substring(tst.Length - 2);
        condate[0] = tst.Remove(tst.Length - 2);//tst.Substring(tst.Length-2);
        dob = condate[2] + "/" + condate[1] + "/" + condate[0];
        return dob;


    }

public static byte[] CreateBarcode(string data)
    {
        if (string.IsNullOrEmpty(data)) { return null; }

        string barcodeData = "*" + data + "*";
        Bitmap barcode = new Bitmap(1, 1);
        Font threeOfNine = new Font("Free 3 of 9 Extended", 31, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        Font arial = new Font("Arial", 13, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        Graphics graphics = Graphics.FromImage(barcode);
        SizeF dataSize = graphics.MeasureString(barcodeData, threeOfNine);
        dataSize.Height = 70;
        barcode = new Bitmap(barcode, dataSize.ToSize());
        graphics = Graphics.FromImage(barcode);
        graphics.Clear(Color.White);
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
        graphics.DrawString(barcodeData, threeOfNine, new SolidBrush(Color.Black), 0, 0);
        graphics.DrawString(data, arial, new SolidBrush(Color.Black), 50, 40);
        graphics.Flush();
        threeOfNine.Dispose();
        graphics.Dispose();
        MemoryStream ms = new MemoryStream();
        barcode.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        return ms.ToArray();
    }

public Int64 GetRequestID(Int64 examid)
{
    DateTime oldTime = new DateTime(1970, 01, 01, 00, 00, 00);
    DateTime currentTime = DateTime.Now;
    TimeSpan structTimespan = currentTime - oldTime;
    string MrtTxnID = structTimespan.TotalMilliseconds.ToString();
    MrtTxnID = MrtTxnID.Replace(".", "");

      MrtTxnID = examid + MrtTxnID;
      return Convert.ToInt64(MrtTxnID);
   

    }

public Int64 GetRequestID()
{
    DateTime oldTime = new DateTime(1970, 01, 01, 00, 00, 00);
    DateTime currentTime = DateTime.Now;
    TimeSpan structTimespan = currentTime - oldTime;
    string MrtTxnID = structTimespan.TotalMilliseconds.ToString();
    MrtTxnID = MrtTxnID.Replace(".", "");
    return Convert.ToInt64(MrtTxnID);
}
public static DateTime converttodatetime(string pickDate)
{
    int day = Convert.ToInt32(pickDate.Substring(0, 2));
    int month = Convert.ToInt32(pickDate.Substring(3, 2));
    int yr = Convert.ToInt32(pickDate.Substring(6, 4));

    DateTime dt = new DateTime(yr, month, day);

    return dt;
}
    public static string formatDateTimeinDMY(DateTime InputDate)
    {
        string getdate = "";

        string day = InputDate.Day.ToString();
        string month = InputDate.Month.ToString();
        string year = InputDate.Year.ToString();

        if (day.Length == 1)
        {
            day = "0" + day;
        }
        if (month.Length == 1)
        {
            month = "0" + month;
        }

        getdate = day + "/" + month + "/" + year;
        return getdate;
    }

}
