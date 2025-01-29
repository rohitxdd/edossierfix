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

public partial class admitcard_test : BasePage
{
    DataTable dt = new DataTable();
    CandidateData obcd = new CandidateData();
    // postwise objpost = new postwise();
    MD5Util md5util = new MD5Util();
    string applid = "";
    string examid = "";
    string rbtvalue = "";
    string flagfromintra = "";
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["applid"] != null)
        {
            //if (MD5Util.Decrypt(Request.QueryString["admiit"].ToString(), true) == "1")
            //{
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
            examid = MD5Util.Decrypt(Request.QueryString["examid"].ToString(), true);
            if (Request.QueryString["rbtvalue"] != null)
            {
                rbtvalue = MD5Util.Decrypt(Request.QueryString["rbtvalue"].ToString(), true);
            }
            if (Request.QueryString["flagfromintradssb"] != null)
            {
                flagfromintra = MD5Util.Decrypt(Request.QueryString["flagfromintradssb"].ToString(), true);
            }
            // int acstatid = Convert.ToInt32(MD5Util.Decrypt(Request.QueryString["acstatid"].ToString(), true));
            if (flagfromintra == "1")
            {
                lbl_sample.Text = "Sample ";
            }
            else if (flagfromintra == "2")
            {
                lbl_sample.Text = "Reprinted ";
            }
            if (rbtvalue == "2")
            {
                lblexamtype.Text = "(Tier-II Exam)";
                lblexamtype.Visible = true;
            }
            else if (rbtvalue == "3")
            {
                lblexamtype.Text = "(Physical Endurance Test)";
                lblexamtype.Visible = true;
            }
            else if (rbtvalue == "5")
            {
                lblexamtype.Text = "(Skill Test)";
                lblexamtype.Visible = true;
            }
            else
            {
                lblexamtype.Text = "";
                lblexamtype.Visible = false;
            }
           // dt = obcd.getAdmitcarddetails(applid, examid, flagfromintra, rbtvalue);
            dt = obcd.getAdmitcarddetails_test(applid, examid, flagfromintra);
            if (dt.Rows.Count > 0)
            {

                string post = "";
                string postcode = "";
                //string refr="";
               // hfgender.Value = dt.Rows[0]["gender"].ToString();
                Page.ClientScript.RegisterStartupScript(e.GetType(), "MyScript", "javascript:window.print()", true);
                lblname.Text = dt.Rows[0]["name"].ToString();
                lbladdress.Text = Utility.getstring(dt.Rows[0]["address"].ToString());
                lblcategory.Text = dt.Rows[0]["category"].ToString();
                if (dt.Rows[0]["SubCategory"].ToString() != "")
                {
                    lblsubcate.Visible = true;
                    lbl_scat.Visible = true;
                    lblsubcate.Text = dt.Rows[0]["SubCategory"].ToString();
                    if (dt.Rows[0]["phsubcat"].ToString() != "")
                    {
                        lblphsubcat.Text = "(" + dt.Rows[0]["phsubcat"].ToString() + ")";
                    }
                    else
                    {
                        lblphsubcat.Text = "";
                    }
                }
                else
                {
                    lblsubcate.Visible = false;
                    lbl_scat.Visible = false;
                }
              
               // string url_instr = md5util.CreateTamperProofURL("PrintInstruction.aspx", null, "examid=" + MD5Util.Encrypt(examid, true) + "&gender=" + MD5Util.Encrypt(hfgender.Value, true));
               //// ifinstruction.ResolveClientUrl(url_instr);
               // ifinstruction.Attributes["src"] = Server.HtmlDecode(url_instr);
              //  string url_h = md5util.CreateTamperProofURL("InsHandler.ashx", null, "examid=" + MD5Util.Encrypt(examid, true) + "&gender=" + MD5Util.Encrypt(hfgender.Value, true));
               // imgins.ImageUrl = url_h;
                lblhusband.Text = Utility.getstring(dt.Rows[0]["fname"].ToString());
                lbldate.Text = dt.Rows[0]["dateofexam"].ToString();
                lbltm.Text = dt.Rows[0]["timeofexam"].ToString();
                lblrollno.Text = dt.Rows[0]["rollno"].ToString();
               // lblcntr.Text = dt.Rows[0]["center_code"].ToString() + " <br/>" + dt.Rows[0]["centername"].ToString(); //+ "</br>" + dt.Rows[0]["centeraddress"].ToString();
                lblcntr.Text = dt.Rows[0]["centername"].ToString(); 
                lblrpt.Text = dt.Rows[0]["reportingtime"].ToString();
                string url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true));
                img_photo.ImageUrl = url;
                url = md5util.CreateTamperProofURL("ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true));
                img_sign.ImageUrl = url;
                // string examid=dt.Rows[0]["examid"].ToString();
                DataTable dt_post = obcd.getAdmitcarddetails_post(applid, examid, flagfromintra, rbtvalue);
                for (int i = 0; i < dt_post.Rows.Count; i++)
                {
                    post = post + dt_post.Rows[i]["jobtitle"].ToString() + ",";
                    //post = "Multiple Posts..";
                    postcode = postcode + " " + dt_post.Rows[i]["postcode"].ToString();
                    if (dt_post.Rows[i]["acstatid"].ToString() == "2")
                    {
                        postcode += " (Provisional) ";
                    }
                    postcode += ",";
                    //refr = refr + dt.Rows[i]["dummy_no"].ToString() + ",";
                }
                if (dt_post.Rows.Count > 1)
                {
                    post = "Multiple Posts as per Post Codes..";
                }
                post = post.Substring(0, post.Length - 1);
                postcode = postcode.Substring(0, postcode.Length - 1);
                //refr = refr.Substring(0, refr.Length - 1);
                lblpost.Text = Utility.getstring(post).ToUpper();
                lblpstcode.Text = postcode;


                if (dt.Rows[0]["acstatid"].ToString() == "2")
                {
                    DataTable dtoano = obcd.get_OAnumber(applid);
                    if (dtoano.Rows[0]["OAnumber"].ToString().ToUpper() == "NIL")
                    {
                        lbloanunber.Text = "";
                        lbloanunber.Visible = false;
                    }
                    else
                    {
                        lbloanunber.Text = "O.A.Number : " + dtoano.Rows[0]["OAnumber"].ToString();
                        lbloanunber.Visible = true;
                    }
                    lbl_prov.Visible = true;


                }
                else
                {
                    lbl_prov.Visible = false;
                    lbloanunber.Visible = false;
                    lbloanunber.Text = "";
                }
                //lbl_ref.Text = refr;
                //}
              
            }
        }     
    }
    //protected void btnprntinst_Click(object sender, EventArgs e)
    //{
    //    examid = MD5Util.Decrypt(Request.QueryString["examid"].ToString(), true);
    //    string url_instr = md5util.CreateTamperProofURL("PrintInstruction.aspx", null, "examid=" + MD5Util.Encrypt(examid, true) + "&gender=" + MD5Util.Encrypt(hfgender.Value, true));
    //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url_instr + "');", true);
       
        
    //}
}