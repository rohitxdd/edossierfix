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
using System.Globalization;

public partial class Edossier_PerInfo : BasePage
{
    DataTable dt = new DataTable();
    CandidateData objCandD = new CandidateData();
    message msg = new message();
    MD5Util md5util = new MD5Util();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fill_ddlPost();

        }
    }
    private void fill_ddlPost()
    {
        string regno = Session["rid"].ToString();
        dt = objCandD.get_post_eDossier(regno);
            DropDownList_post.Items.Clear();
            DropDownList_post.DataTextField = "post";
            DropDownList_post.DataValueField = "jid";
            DropDownList_post.DataSource = dt;
            DropDownList_post.DataBind();
            ListItem l1 = new ListItem();
            l1.Text = "--Select--";
            l1.Value = "";
            DropDownList_post.Items.Insert(0, l1);
            if (dt.Rows.Count == 0)
            {
                tblpost.Visible = false;
                lblmsg.Visible = true;
            }
            else
            {
                tblpost.Visible = true;
                lblmsg.Visible = false;
            }
            
      
       
    }
    private void checkforediting(string jid)
    {
        DataTable dt1 = objCandD.check_post_foruploadeDossier(Session["rid"].ToString(), jid);
        fill_application_data(jid, Session["rid"].ToString());
        DataTable dtget = objCandD.Getedossiersfinal(hfapplid.Value);
        if (dtget.Rows.Count > 0)
        {
            hffinal.Value = "Y";
        }
        else
        {
            hffinal.Value = "N";
        }
        if (dt1.Rows.Count > 0)
        {
            hfschedule.Value = "Y";
        }
        else
        {
            hfschedule.Value = "N";
        }

        if (hfschedule.Value == "Y")
        //if (hfschedule.Value == "Y" && (Session["rid"].ToString() == "0804199787806812012" || Session["rid"].ToString() == "2405199261334222008" || Session["rid"].ToString() == "2912198961251392006" || Session["rid"].ToString() == "3006199521696622011"))
        {

            if (hffinal.Value == "Y")
            {
                btnnext.Visible = false;//true;
                btnsave.Visible = true;
                txtadd1.Enabled = false;
                txtspouse.Enabled = false;
                txtpincode1.Enabled = false;
                lbledno.Text = "Your Edossier No is : " + dtget.Rows[0]["edossierNo"].ToString();
                tredno.Visible = true;
                tbldata.Visible = true;

            }
            else
            {
                btnnext.Visible = false;
                btnsave.Visible = true;
                txtadd1.Enabled = true;
                //if (txtspouse.Text == "")
                //{
                //    txtspouse.Enabled = true;
                //}
                //else
                //{
                //    txtspouse.Enabled = false;
                //}
                txtspouse.Enabled = true;
                txtpincode1.Enabled = true;
                tredno.Visible = false;
                tbldata.Visible = true;
            }
        }
        else
        {

            if (hffinal.Value == "Y")
            {

                btnnext.Visible = false;//true;
                btnsave.Visible = true;
                txtadd1.Enabled = false;
                txtspouse.Enabled = false;
                txtpincode1.Enabled = false;
                lbledno.Text = "Your Edossier No is : " + dtget.Rows[0]["edossierNo"].ToString();
                tredno.Visible = true;
                tbldata.Visible = true;
            }
            else
            {
                msg.Show("Last Date has been over for Uploading eDossier");
                tbldata.Visible = false;
                return;
            }
            //msg.Show("Last Date has been over for Uploading eDossier");
            //tbldata.Visible = false;
            //return;
        }






    }

    public void fill_application_data(string jid, string regno)
    {
        try
        {
            dt = objCandD.getperinfoedossier(jid, regno);
            if (dt.Rows.Count > 0)
            {

                lblpostcode.Text = dt.Rows[0]["post"].ToString();
                lblrollno.Text = dt.Rows[0]["rollno"].ToString();
                lblname1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["name"].ToString()));
                lblfat1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["fname"].ToString()));
                lblmth1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["mothername"].ToString()));
                txtadd1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["address_per"].ToString()));
                txtpincode1.Text = dt.Rows[0]["pin_per"].ToString();
                lblperadd1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["address"].ToString()));
                lblbrth1.Text = dt.Rows[0]["birthdt"].ToString();
                lblmob1.Text = dt.Rows[0]["mobileno"].ToString();
                lblemail1.Text = dt.Rows[0]["email"].ToString();
                DataTable dtendson = objCandD.getendson(jid);
                lblage.Text = "Age as on " + dtendson.Rows[0]["endson"].ToString();
                lblage1.Text = dt.Rows[0]["Age"].ToString();
                txtspouse.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["spousename"].ToString()));
               
                hfapplid.Value = dt.Rows[0]["applid"].ToString();
                hfjid.Value = jid;
                hfedid.Value = dt.Rows[0]["edid"].ToString();
                //checkforediting(jid);
            }
            else
            {

                msg.Show("Data Not Found");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button_Vaidate_Click(object sender, EventArgs e)
    {
        string jid = DropDownList_post.SelectedValue;

        if (jid != "")
        {

            tblpost.Visible = false;
            // fill_application_data(jid, Session["rid"].ToString());
            checkforediting(jid);
        }
        else
        {
            msg.Show("Invalid Post select.");
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int temp = 0;
        string IP = GetIPAddress();
        if (hfedid.Value == "")
        {
            temp = objCandD.InsertEDPinfo(Convert.ToInt32(hfjid.Value), Convert.ToInt32(hfapplid.Value), lblname1.Text, lblfat1.Text, lblmth1.Text, txtspouse.Text, txtadd1.Text, lblperadd1.Text, txtpincode1.Text, lblmob1.Text, lblemail1.Text, lblbrth1.Text, IP, lblrollno.Text, Session["rid"].ToString(), lblage1.Text);
        }
        else
        {
            temp = objCandD.UpdateEDPinfo(Convert.ToInt32(hfedid.Value), txtadd1.Text, txtpincode1.Text, IP, txtspouse.Text);
        }
        if (temp > 0)
        {
            // fill_application_data(hfjid.Value, Session["rid"].ToString());
            checkforediting(hfjid.Value);
            // msg.Show("Saved Successfully");
            string url = md5util.CreateTamperProofURL("Edossier_uploaddoc.aspx", null, "jid=" + MD5Util.Encrypt(hfjid.Value, true) + "&edid=" + MD5Util.Encrypt(hfedid.Value, true) + "&applid=" + MD5Util.Encrypt(hfapplid.Value, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
            Server.Transfer(url);
        }

    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        string url = md5util.CreateTamperProofURL("Edossier_uploaddoc.aspx", null, "jid=" + MD5Util.Encrypt(hfjid.Value, true) + "&edid=" + MD5Util.Encrypt(hfedid.Value, true) + "&applid=" + MD5Util.Encrypt(hfapplid.Value, true) + "&rollno=" + MD5Util.Encrypt(lblrollno.Text, true) + "&post=" + MD5Util.Encrypt(lblpostcode.Text, true));
        Server.Transfer(url);
    }


    protected void btnreplace_Click(object sender, EventArgs e)
    {
        try
        {
            string jid = DropDownList_post.SelectedValue;
            if (jid == "")
            {
                msg.Show("Please select Post Applied");
                return;
            }

            else
            {
                checkforediting(jid);
                if (hffinal.Value == "Y")
                {
                    string url = md5util.CreateTamperProofURL("ReplaceRecallDoc.aspx", null, "applid=" + MD5Util.Encrypt(hfapplid.Value, true) + "&jid=" + MD5Util.Encrypt(jid, true));
                    Response.Redirect(url);
                }
                else
                {
                    msg.Show("You have not finally submitted the eDossier");
                    return;
                }

            }
        }
        catch (System.Threading.ThreadAbortException ext)
        {
        }
        catch (Exception ex)
        {
            msg.Show("Something went wrong");
        }
    }
}