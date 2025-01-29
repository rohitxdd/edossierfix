using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using BarcodeLib.Barcode;

public partial class AdmitCard_AsPerEdcil : Page
{
    MD5Util md5util = new MD5Util();
    eAdmitCard objeadmit = new eAdmitCard();
    message msg = new message();
    string flag = "";
    DataTable dt = new DataTable();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);//"7939646"; //"8353744"; "69617";//
            string examid = MD5Util.Decrypt(Request.QueryString["examid"].ToString(), true);
            fill_application_data(Convert.ToInt32(applid), examid);
             //modified on 19/07/2023
            if (examid == "755")
            {
                LblAdditional.Visible = true;
            }
            else
            {
                LblAdditional.Visible = false;
            }
        }
    }
    public void fill_application_data(int applid, string examid)
    {
        try
        {
            dt = objeadmit.fill_personal_data(applid, examid);
            DataTable examcontrolSig = objeadmit.get_ExamControllerSign();
            if (dt.Rows.Count > 0)
            {
                int jid = Convert.ToInt32(dt.Rows[0]["jid"]);
                string dummy_no = dt.Rows[0]["dummy_no"].ToString();
                lblPostCodeN.Text = dt.Rows[0]["postcode"].ToString();
                if(examid == "862" )
                {
                    lblPostTitle.Text = "Personal Assistant (Family Courts)/Personal Assistant (Delhi District Court)";
                }
                else
                {
                    lblPostTitle.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["jobtitle"].ToString()));
                }
                    
		lblCenterName.Text = dt.Rows[0]["centername"].ToString();
                lblname1.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["name"].ToString().ToUpper()));
                if (dt.Rows[0]["can_gender"].ToString() == "F")
                {
                    lblGender.Text = "Female";
                }
                else
                {
                    lblGender.Text = "Male";
                }
                lblRollno.Text = dt.Rows[0]["rollno"].ToString();
                lblRegistration.Text = dt.Rows[0]["regno"].ToString();
                if ((dt.Rows[0]["dateofexam"].ToString() != "" && dt.Rows[0]["dateofexam"].ToString() != null) && (dt.Rows[0]["timeofexam"].ToString() != "" && dt.Rows[0]["timeofexam"].ToString() != null))
                {
                    lblExamdate.Text = Convert.ToDateTime(dt.Rows[0]["dateofexam"]).ToString("dd-MMMM-yyyy") + "  &  " + dt.Rows[0]["timeofexam"].ToString();
                }
                if (dt.Rows[0]["reportingtime"].ToString() != "" && dt.Rows[0]["reportingtime"].ToString() != null)
                {
                    lblExamReporting.Text = dt.Rows[0]["reportingtime"].ToString().Substring(0, 8);
                    lblGateClosing.Text = dt.Rows[0]["reportingtime"].ToString().Substring(11);
                }
                if (dt.Rows[0]["fname"].ToString() != null && dt.Rows[0]["fname"].ToString() != "")
                {
                    lblfName.Text = Utility.getstring(Server.HtmlEncode(dt.Rows[0]["fname"].ToString().ToUpper()));
                }
                else
                {
                    lblfName.Text = dt.Rows[0]["spousename"].ToString().ToUpper();
                }
                if (dt.Rows[0]["category"].ToString() != null && dt.Rows[0]["category"].ToString() != "")
                {
                    lblcatName.Text = dt.Rows[0]["category"].ToString();
                }
                else
                {
                    lblcatName.Text = "----";
                }
                if (dt.Rows[0]["SubCategory"].ToString() != null && dt.Rows[0]["SubCategory"].ToString() != "")
                {
                    lblSubCatName.Text = dt.Rows[0]["SubCategory"].ToString();
                }
                else
                {
                    lblSubCatName.Text = "----";
                }

                lblappidNumber.Text = dt.Rows[0]["dummy_no"].ToString();

                lblbrth1.Text = dt.Rows[0]["birthdt"].ToString();
                if (Convert.ToString(dt.Rows[0]["typeOfID"]) == null && Convert.ToString(dt.Rows[0]["typeOfID"]) == "" || Convert.ToString(dt.Rows[0]["idNumber"]) != null && Convert.ToString(dt.Rows[0]["idNumber"]) != "")
                {
                    lblIDName.Text = dt.Rows[0]["typeOfID"].ToString();
                }
                else
                {
                    lblIDName.Text = "";
                }
                if (Convert.ToString(dt.Rows[0]["idNumber"]) != null && Convert.ToString(dt.Rows[0]["idNumber"]) != "")
                {
                    string aadharNo = Convert.ToString(dt.Rows[0]["idNumber"]);
                    aadharNo = MD5Util.Decrypt(aadharNo, true);
                    lblIdNumber.Text = aadharNo;
                }
                else
                {
                    lblIdNumber.Text = "";
                }
                if ((Convert.ToString(dt.Rows[0]["typeOfID"]) == null || Convert.ToString(dt.Rows[0]["typeOfID"]) == "") && (Convert.ToString(dt.Rows[0]["idNumber"]) == null || Convert.ToString(dt.Rows[0]["idNumber"]) == ""))
                {
                    lblIDName.Text = "";
                    lblIdNumber.Text = "";
                }
                if (dt.Rows[0]["Signature"] != null && dt.Rows[0]["Signature"].ToString() != "")
                {
                    byte[] imgSign = (byte[])dt.Rows[0]["Signature"];
                    string strBase64 = Convert.ToBase64String(imgSign);
                    SigImage.ImageUrl = "data:Image/jpg;base64," + strBase64;
                }
                else
                {
                    SigImage.AlternateText = "Signature not available";
                }
                if (examcontrolSig.Rows[0]["CSig"] != null && examcontrolSig.Rows[0]["CSig"].ToString() != "")
                {
                    byte[] imgExamSign = (byte[])examcontrolSig.Rows[0]["CSig"];
                    string strExamSig = Convert.ToBase64String(imgExamSign);
                    imgExamContSign.ImageUrl = "data:Image/jpg;base64," + strExamSig;
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    if (jid > 713)
                    {
                        if (dt.Rows[0]["OLEModule"] != null && dt.Rows[0]["OLEModule"].ToString() != "")
                        {
                            byte[] imgOLEModule = (byte[])dt.Rows[0]["OLEModule"];
                            memoryStream.Position = 0;
                            memoryStream.Read(imgOLEModule, 0, (int)imgOLEModule.Length);
                            string OLEModule = Convert.ToBase64String(imgOLEModule);
                            imgPostcardPP.ImageUrl = "data:Image/jpg;base64," + OLEModule;
                        }
                    }
                    //else (dt.Rows[0]["PostCardPhoto"] != null || dt.Rows[0]["PostCardPhoto"].ToString() != "")
                    else if (dt.Rows[0]["PostCardPhoto"] != null && dt.Rows[0]["PostCardPhoto"].ToString() != "")
                    {
                        byte[] imgPostCardPhoto = (byte[])dt.Rows[0]["PostCardPhoto"];

                        memoryStream.Position = 0;
                        memoryStream.Read(imgPostCardPhoto, 0, (int)imgPostCardPhoto.Length);
                        string PostCardPhoto = Convert.ToBase64String(imgPostCardPhoto);
                        imgPostcardPP.ImageUrl = "data:Image/jpg;base64," + PostCardPhoto;
                    }
                    else
                    {

                    }
                }
                string barCode = dt.Rows[0]["rollno"].ToString();
                BarcodeLib.Barcode.Linear msi = new BarcodeLib.Barcode.Linear();
                msi.Type = BarcodeType.MSI;
                msi.Data = barCode; //MD5Util.Encrypt(barCode, true);//"1234567890";

                msi.AddCheckSum = true;
                msi.UOM = UnitOfMeasure.PIXEL;
                msi.BarWidth = 1;
                msi.BarHeight = 300;
                msi.LeftMargin = 10;
                msi.RightMargin = 10;
                msi.TopMargin = 10;
                msi.BottomMargin = 10;
                msi.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                //Image2.ImageUrl = msi.ImageFormat.ToString();
                //// save barcode image into your system
                //msi.drawBarcode("c:/msi.png");

                ////BarcodeLib.Barcode.Linear barcode = new BarcodeLib.Barcode.Linear();
                ////barcode.Type = BarcodeType.CODE128;
                ////barcode.Data = MD5Util.Encrypt(barCode,true);
                ////// other barcode settings.

                ////// save barcode image into your system
                //////barcode.drawBarcode("D:/barcode.png");

                MemoryStream ms = new MemoryStream();
                msi.drawBarcode(ms);
                byte[] bmpBytes = ms.ToArray();
                string base64ofByte = Convert.ToBase64String(bmpBytes);
                //Image2.ImageUrl = base64ofByte;
                Image2.ImageUrl = "data:Image/jpg;base64," + base64ofByte;
                ms.Close();

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
}