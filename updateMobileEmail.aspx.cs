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

public partial class updateMobileEmail : BasePage
{
    CandidateData objcd = new CandidateData();
    message msg = new message();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    Random randObjCode = new Random();
    Int32 UniqueRandomNumberCode = 0;
    string SecurityCode;
    Sms objsms = new Sms();

    protected void Page_Load(object sender, EventArgs e)
    {
        //txt_mob.Attributes.Add("onblur", "ValidatorValidate(" + valsum.ClientID + ")");
        
        //TRgetCodeMob.Visible = false;
        if (!IsPostBack)
        {
           

            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            //txt_mob.;
            Filldetail();
           
        }
        else
        {
            //TRgetCodeMob.Visible = true;
            if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
    }
    private void Filldetail()
    {
        DataTable dt = objcd.getdetail(Session["rid"].ToString());
        txt_name.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["name"].ToString()));
        txtuid.Text = dt.Rows[0]["um_logid"].ToString();
        txt_fh_name.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["fname"].ToString()));
        txt_mothername.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["mothername"].ToString()));
        lblgender.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["gender"].ToString()));
        txt_reg.Text = dt.Rows[0]["rid"].ToString();       
        lblnation.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["nationality"].ToString()));        
        //txt_mob.Text = dt.Rows[0]["mobileno"].ToString();
        Hidden_txtmob.Value = txt_mob.Text;
        //txt_email.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["email"].ToString()));
       //get data on basis of registration id
    }

    protected void btnrsubmit_Click(object sender, EventArgs e)
    {
        //update mob email
        if (txt_mob.Text == "" || txt_email.Text == "")
        {
            msg.Show("Please Enter Mobile No/email");
        }
        else if(Validation.chkLevel(txt_mob.Text))
        {
            //if (Validation.chkLevel(txt_mob.Text))
            //{
                msg.Show("Invalid Character in Mobile No");
            //}
        }
            else
            {
                //if (Hidden_SecCode.Value == "" && txtcode.Text == "")
                //{
                //    if (Hidden_txtmob.Value != txt_mob.Text)
                //    {
                //        TRgetCodeMob.Visible = true;
                //        msg.Show("Please click on GetCode button and Confirm your Security Code");

                //    }
                //    else
                //    {
                        int tmp = objcd.updatemobile(Session["rid"].ToString(), txt_mob.Text, txt_email.Text);
                        if (tmp > 0)
                        {
                            msg.Show("Updated successfully");
                            Server.Transfer("Home.aspx");
                        }
            

                    //}
              //  }
                
                //if (Hidden_SecCode.Value !="" && txtcode.Text!="")
                //{
                //    if (MD5Util.Decrypt(Hidden_SecCode.Value,true) == txtcode.Text)
                //    {
                //        int tmp = objcd.updatemobile(Session["rid"].ToString(), txt_mob.Text, txt_email.Text);
                //        if (tmp > 0)
                //        {
                //            msg.Show("Updated successfully");
                //            Server.Transfer("Home.aspx");
                //        }
                //    }
                        else
                        {
                            //txtcode.Text = "";
                            msg.Show("Some Error Occured");

                        }
                }
                
            }
        //}
    //}

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    UniqueRandomNumberCode = randObjCode.Next(1000, 9999);
    //    Hidden_SecCode.Value = UniqueRandomNumberCode.ToString();
    //    Hidden_SecCode.Value= MD5Util.Encrypt(Hidden_SecCode.Value, true);
    //    //SecurityCode = "Please Note Your Security Code:" + UniqueRandomNumberCode;
    //    SecurityCode = Convert.ToString(UniqueRandomNumberCode);
    //    objsms.sendmsg(txt_mob.Text, SecurityCode);
    //    msg.Show(SecurityCode);
    //}

    //protected void txt_mob_TextChanged(object sender, EventArgs e)
    //{

    //    string mobile=txt_mob.Text;
    //   if (txt_mob.Text == "" )
    //    {
    //        TRgetCodeMob.Visible = false;
    //        msg.Show("Please enter Mobile Number");

    //    }
    //    else if (mobile.Length < 10)
    //    {
    //        TRgetCodeMob.Visible = false;
    //        msg.Show("Please enter Minimum 10 Integer Values");

    //    }

    //    else if (Hidden_txtmob.Value != txt_mob.Text)
    //    {
    //        //TRgetCodeMob.Visible = true;
    //        //rfvmob.Enabled = true;
    //        //revmob.Enabled = true;
    //        //REVMobile.Enabled = true;
    //        //valsum.Enabled = true;
    //        msg.Show("Please click on GetCode button and Confirm your Security Code");

    //    }

    //    else
    //    {
    //        TRgetCodeMob.Visible = false;
    //    }
    //}
}
