using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CPayment : BasePage
{
    challengeansheet objcs = new challengeansheet();
    message msg = new message();
    MD5Util md5util = new MD5Util();
    CandidateData objcd = new CandidateData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ChallengeIds"] != null)
            {
                lblregno.Text = Session["rid"].ToString();
                DataTable dt = objcd.getdetail(Session["rid"].ToString());
                lblMobile.Text = dt.Rows[0]["mobileno"].ToString();
                string ChallengeIds = MD5Util.Decrypt(Request.QueryString["ChallengeIds"].ToString(), true);
                HdnChalId.Value = ChallengeIds;
                string[] ids = ChallengeIds.Split(',');
                int a = ids.Length;
                LblNoffQues.Text = a.ToString();
                int b = objcs.GetCFee();
                lblAmount.Text = b.ToString();
                LblTotalAmt.Text = (a * b).ToString();
                fillrefunddtls();
            }
        }

    }

    private void fillrefunddtls()
    {
        DataTable dt = new DataTable();
        dt = objcs.GetRefDetls(Session["rid"].ToString());
        
        if (dt.Rows.Count > 0)
        {
            txtBnkName.Text = dt.Rows[0]["CBankName"].ToString();
            txtBnkBrnch.Text = dt.Rows[0]["CbankBrnch"].ToString();
            txtAccHolName.Text = dt.Rows[0]["Accholdername"].ToString();
            txtBnkAccNo.Text = dt.Rows[0]["CAccNo"].ToString();
            txtBnkIfsc.Text = dt.Rows[0]["CBankIfsc"].ToString();
            rbltype.SelectedValue = dt.Rows[0]["TypeofAcc"].ToString();
            HdnCRid.Value = dt.Rows[0]["CRID"].ToString();
            setcontrols(false);
            btnEdit.Visible = true;
            btnPay.Visible = true;            
            btnSave.Visible = false;
        }
        else
        {
            setcontrols(true);
            btnEdit.Visible = false;
            btnPay.Visible = false;
            btnSave.Visible = true;
        }
        btnCancel.Visible = false;
    }

    private void setcontrols(bool val)
    {
        txtBnkName.Enabled = val;
        txtBnkBrnch.Enabled = val;
        txtAccHolName.Enabled = val;
        txtBnkAccNo.Enabled = val;
        txtBnkIfsc.Enabled = val;
        rbltype.Enabled = val;
    }


    protected void btnPay_Click(object sender, EventArgs e)
    {
        int result = objcs.updateRfdt(HdnCRid.Value, HdnChalId.Value);
        if (result > 0)
        {
            int CPdID = objcs.InsertPayDtls(Session["rid"].ToString(), LblTotalAmt.Text);
            int t = objcs.updatePayId(CPdID, HdnChalId.Value);
            string url = md5util.CreateTamperProofURL("CPayOnline2.aspx", null, "CPdID=" + MD5Util.Encrypt(CPdID.ToString(), true) + "&flag=" + MD5Util.Encrypt("P", true));
            Response.Redirect(url);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        setcontrols(true);
        btnEdit.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnPay.Visible = false;
    }
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    string ip = GetIPAddress();
    //    int result = objcs.UpdateRefDetls(Session["rid"].ToString(), txtBnkIfsc.Text, txtBnkName.Text, txtBnkBrnch.Text, txtBnkAccNo.Text, "", txtAccHolName.Text, rbltype.SelectedValue, "", Utility.formatDatewithtime(DateTime.Now), ip);
    //    if (result > 0)
    //    {
    //        msg.Show("--------Record  Updated  SuccessFully--------");
    //    }
    //    fillrefunddtls();
    //    btnCancel.Visible = false;
    //    btnEdit.Visible = true;
    //    btnPay.Visible = true;
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnEdit.Visible = true;
        btnPay.Visible = true;
        btnCancel.Visible = false;
        btnSave.Visible = false;
        setcontrols(false);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ip = GetIPAddress();
        int CRId = objcs.InsertRefDetls(Session["rid"].ToString(), txtBnkIfsc.Text, txtBnkName.Text, txtBnkBrnch.Text, txtBnkAccNo.Text, "", txtAccHolName.Text, rbltype.SelectedValue, "", Utility.formatDatewithtime(DateTime.Now), ip);
        if (CRId > 0)
        {
            msg.Show("--------Record Inserted SuccessFully--------");
            HdnCRid.Value = CRId.ToString();
            setcontrols(false);
            btnPay.Visible = true;
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }
       

    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        Response.Redirect("updatemobile.aspx");
    }
}