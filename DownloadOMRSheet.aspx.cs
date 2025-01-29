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
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Text;
using CaptchaDLL;
public partial class DownloadOMRSheet : BasePage
{
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    message msg = new message();
    public bool _showcaptcha;
    CandidateData objcanddata = new CandidateData();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBoxRollNo.Focus();
        if (!IsPostBack)
        {
            Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6);
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            //getfile();

        }
        else
        {
            if (((Request.Form["csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
        
    }

    public bool getfile(string Rollno)
    {
        DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/DownloadOMR"));
        int counter = 0;
        bool flag = false;
        foreach (FileInfo file in directory.GetFiles())
        {
            //HyperLink link = new HyperLink();
            //link.ID = "Link" + counter++;
            //link.Text = file.Name;
            //link.NavigateUrl = "OMRDownload.aspx?name="+file.Name;
            
            //Page.Controls.Add(link);
            //Page.Controls.Add(new LiteralControl("<br/>"));

            if (file.Name.Contains(Rollno))
            {
                flag = true;
                break;
            }
            else
            {
                flag = false;
            }
        }
        return flag;
    }

    protected void Click(object sender, EventArgs e)
    {
        Response.Redirect("OMRDownload.aspx");
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Validation.chkLevel(TextBoxRollNo.Text))
        {
            msg.Show("Invalid Character in Roll No.");
        }
        else if (Validation.chkLevel13(txt_DOB.Text))
        {
            msg.Show("Invalid Character in Date of Birth");
        }
        else
        {
            if (txtCode.Text != "")
            {

                if (Session["CaptchaImageText"] != null && txtCode.Text.ToLower() == Session["CaptchaImageText"].ToString().ToLower())
                {
                    string Rollno = MD5Util.Encrypt(TextBoxRollNo.Text + ".jpg", true);
                    string RollNoText = TextBoxRollNo.Text;
                    string Dob = txt_DOB.Text;

                    DataTable dt = objcanddata.OMRRollVerification(RollNoText, txt_DOB.Text);
                    if (dt.Rows.Count > 0)
                    {

                        if (getfile(TextBoxRollNo.Text))
                        {

                            ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script language=\"javascript\" type=\"text/javascript\">window.open('OMRDownload.aspx?name=" + Rollno + "','name');</script>");
                            clear();

                        }
                        else
                        {
                            msg.Show("Enter Valid Roll No.");
                        }
                    }
                    else
                    {
                        clear();

                        msg.Show("Roll No or DOB doesn't Match");
                    }


                }
                else
                {
                    clear();
                    msg.Show("The security code you entered is incorrect. Enter the security code as shown in the image.");
                    return;
                }
            }
            else
            {
                clear();
                msg.Show("Please Enter Visual Code");
            }

            //string WindowOpen = "window.open('OMRDownload.aspx?name=" + Rollno + "', 'theWin', 'width=200,height=200,toolbar=0,menubar=0');";

        }
    }

    protected void ibtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    private void clear()
    {
        Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6); //generate new string
        txtCode.Text = "";
        TextBoxRollNo.Text = "";
        txt_DOB.Text = "";
        //txtusername.Text = "";
    }
 


  
}
