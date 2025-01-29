using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reverifyPaymentAutomatically : BasePage
{
    CandidateData objcan = new CandidateData();
    string applid = "", flag = "",feereq="";
    protected void Page_Load(object sender, EventArgs e)
    {
	Session["rid"] = "01RKP@342";
        if (!IsPostBack)
        {
            getApplidandFlag();
        }
    }
    public void getApplidandFlag()
    {
         DataTable dt = objcan.getApplidAndFlagForReverifyAutomatically();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                applid = dt.Rows[i]["applid"].ToString();
                feereq = dt.Rows[i]["feereq"].ToString();
                if (feereq == "Y")
                {
                    encriptData(applid, "V");
                }
            }
        }
    }
    public void encriptData(string applid, string flag)
    {
        string schemeid = ConfigurationManager.AppSettings["Schemeid"].ToString();
        string key = ConfigurationManager.AppSettings["KeytoPay"].ToString();
        string Name = "", Post = "", MobileNo = "", DateofBirth = "", amount = "", Email = "", encparam = "", returnurl = "";
        if (applid != "")
        {
            DataTable dtdetails = objcan.fill_personal_data(Int32.Parse(applid));
            if (dtdetails.Rows.Count > 0)
            {
                Name = dtdetails.Rows[0]["name"].ToString();
                Post = dtdetails.Rows[0]["post"].ToString();
                MobileNo = dtdetails.Rows[0]["mobileno"].ToString();
                DateofBirth = dtdetails.Rows[0]["birthdt"].ToString();
                amount = dtdetails.Rows[0]["fee"].ToString();
                Email = Utility.getstring(Server.HtmlEncode(dtdetails.Rows[0]["email"].ToString()));
                returnurl = "https://dsssbonline.nic.in/FeeResponse.aspx";

                encparam = applid + "|" + amount + "|" + returnurl + "|" + MobileNo + "||||Name|" + Name + "|Post Applied|" + Post + "|Date of Birth|" + DateofBirth + "|Mobile No|" + MobileNo + "|Email|" + Email;


                // string SchemeId = MD5Util.EncryptforPay(schemeid, true);
                SchemeId.Value = schemeid;
                // UniqueId.Value = PayOnline.EncryptforPay(applid, true);
                // Amount.Value = PayOnline.EncryptforPay(amount, true);
                EncParam.Value = PayOnline.EncryptforPay(encparam, true, key);
                Flag.Value = flag;

            }
        }

    }
}