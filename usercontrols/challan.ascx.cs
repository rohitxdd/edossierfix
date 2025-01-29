using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class usercontrols_challan : System.Web.UI.UserControl
{
    DataTable dt = new DataTable();
    CandidateData ObjCan = new CandidateData();
    protected void Page_Load(object sender, EventArgs e)
    {

        string applids = Session["challan_applid"].ToString();
        int applid = Int32.Parse(applids);
        dt = ObjCan.fill_personal_data(applid);
        if (dt.Rows.Count > 0)
        {
            lbljtitle.Text = dt.Rows[0]["post"].ToString();
            lblnm.Text = dt.Rows[0]["name"].ToString();
            lblapp_pcode.Text = dt.Rows[0]["postcode"].ToString();
            //lblmail.Text = dt.Rows[0]["email"].ToString();
            lblmob.Text = dt.Rows[0]["mobileno"].ToString();
            lblref.Text = dt.Rows[0]["applid"].ToString();
            lblbrth.Text = dt.Rows[0]["birthdt"].ToString();
            //lbljid.Text = dt.Rows[0]["jid"].ToString();
            lblamt.Text = dt.Rows[0]["fee"].ToString();
            lbl_date.Text = dt.Rows[0]["FeeLastDate"].ToString();
            lblmail.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["email"].ToString()));
            int num = Int32.Parse(lblamt.Text);
            string nums = ConvertNumberToWords(num);
            lbltamnt.Text = nums + "Only";

        }


        }

     private static string ConvertNumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus" + ConvertNumberToWords(Math.Abs(number));

            string words = string.Empty;

            if ((number / 1000000) > 0)
            {
                words += ConvertNumberToWords(number / 1000000) + " milllion ";
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words; 

        }

    }

