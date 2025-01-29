using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class CPayOnline2 : BasePage
{
    challengeansheet cans = new challengeansheet();
    string cpid = "",flag="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["CPdID"] != null)
            {
                cpid = MD5Util.Decrypt(Request.QueryString["CPdID"].ToString(), true);
            }
            if (Request.QueryString["flag"] != null)
            {
                flag = MD5Util.Decrypt(Request.QueryString["flag"].ToString(), true);
            }
            encriptData(cpid, flag);
        }
    }
    
    public void encriptData(string cpid, string flag)
    {
        string schemeid = ConfigurationManager.AppSettings["CSchemeid"].ToString();
        string key = ConfigurationManager.AppSettings["CKeytoPay"].ToString();
        string Name = "", MobileNo = "", amount = "", regno = "", encparam = "",returnurl="";
        if (cpid != "")
        {
            DataTable dtdetails = cans.getdataforpayment(Convert.ToInt32(cpid));
            if (dtdetails.Rows.Count > 0)
            {
                Name = dtdetails.Rows[0]["name"].ToString();
                MobileNo = dtdetails.Rows[0]["mobileno"].ToString();
                regno = dtdetails.Rows[0]["cregno"].ToString();
                amount = dtdetails.Rows[0]["camt"].ToString();

                returnurl = "https://dsssbonline.nic.in/CFeeResponse.aspx";

                encparam = cpid + "|" + amount + "|" + returnurl + "|" + MobileNo + "||||Regno|" + regno + "|Name|" + Name + "|Mobile No|" + MobileNo;

                SchemeId.Value = schemeid;

                EncParam.Value = PayOnline.EncryptforPay(encparam, true, key);
                Flag.Value = flag;               
            }
        }

    }

}