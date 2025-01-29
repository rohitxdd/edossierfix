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

public partial class Control_DatePicker : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            for (int i = 1; i <= 12; i++)
            {
                if (i < 10)
                    ddlMonth.Items.Add(new ListItem(GetMonthFromNo(i), "0" + i.ToString()));
                else
                    ddlMonth.Items.Add(new ListItem(GetMonthFromNo(i), i.ToString()));
            }
            for (int i = 1950; i <= 3050; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            if (DateTime.Now.Month < 10)
            {
                ddlMonth.SelectedValue = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
        }
    }
    protected string GetMonthFromNo(int i)
    {
        string Month = string.Empty;

        switch (i)
        {
            case 1:
                Month = "January";
                break;
            case 2:
                Month = "February";
                break;
            case 3:
                Month = "March";
                break;
            case 4:
                Month = "April";
                break;
            case 5:
                Month = "May";
                break;
            case 6:
                Month = "June";
                break;
            case 7:
                Month = "July";
                break;
            case 8:
                Month = "August";
                break;
            case 9:
                Month = "September";
                break;
            case 10:
                Month = "October";
                break;
            case 11:
                Month = "November";
                break;
            case 12:
                Month = "December";
                break;
        }
        return Month;
    }

   
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Calendar1.TodaysDate = Convert.ToDateTime(ddlMonth.SelectedValue + "/01" + "/" + ddlYear.SelectedValue);
        
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Calendar1.TodaysDate = Convert.ToDateTime("01/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue);
    }

    protected void Calendar1_SelectionChanged1(object sender, EventArgs e)
    {
        string dt1 = "", dt = "";
        if (Request.QueryString["days"] != null)
        {
            int days = Convert.ToInt32(Request.QueryString["days"]);
            dt1 = getdateinDMY(Calendar1.SelectedDate.AddDays(days));
        }

        dt = getdateinDMY(Calendar1.SelectedDate);
        if (Validation.chkLevel13(dt))
        {
            message msg = new message();
            msg.Show("Invalid Inputs");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "CalValue", "<script>SetDate('" + dt + "','" + dt1 + "')</script>");
        }
    }

    public string getdateinDMY(DateTime dt)
    {

        string day = dt.Day.ToString();
        string month = dt.Month.ToString();
        string year = dt.Year.ToString();
        
        if (day.Length == 1)
        {
            day = "0" + day;
        }
        if (month.Length == 1)
        {
            month = "0" + month;
        }

        string date = day + "/" + month + "/" + year;
        return date;
    }
}
