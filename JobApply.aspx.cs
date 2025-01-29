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


public partial class JobApply : System.Web.UI.Page
{
    CandidateData objCandD = new CandidateData();
    DataTable dt1 = new DataTable();
    message msg = new message();

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BtnInsertUpdate.Visible = true;

            }
            string SID = Request.QueryString["JobSourceID"].ToString();
            string YR = Request.QueryString["AdvtYear"].ToString();
            string ANO = Request.QueryString["AdvtNo"].ToString();
            try
            {
                //SID = Encryption64.Decrypt(Request.Params["sid"].Replace(" ", "+"), "!#$a54?3");
                //YR = Encryption64.Decrypt(Request.Params["yr"].Replace(" ", "+"), "!#$a54?3");
                //ANO = Encryption64.Decrypt(Request.Params["ano"].Replace(" ", "+"), "!#$a54?3");
                if (SID != "" & YR != "" & ANO != "")
                {

                    DataTable dt = new DataTable();
                    dt = objCandD.SelectJobSource(SID);
                    LblJobSource.Text = dt.Rows[0]["name"].ToString();
                    objCandD = null;
                    LblJobSourceId.Text = SID;
                    lblAdvtYear.Text = YR;
                    lblAdvtId.Text = ANO;
                    lblAdvtNo.Text = LblJobSource.Text + "/" + lblAdvtYear.Text + "/" + lblAdvtId.Text;
                    EnableGovtService(false);
                }
                else
                {
                    Response.Redirect("GenericErrorsPage.aspx?ErrorNo=1");
                }

            }
            catch (System.Threading.ThreadAbortException ext)
            {
            }
            catch (Exception ex)
            {
                //Response.Redirect("GenericErrorsPage.aspx?ErrorNo=1")
                throw ex;
            }
            if (LblJobSourceId.Text == "" || lblAdvtYear.Text == "" || lblAdvtId.Text == "")
            {
                Response.Redirect("AdvtList.aspx");
            }

            try
            {
                if (SID != "" & YR != "" & ANO != "")
                {
                    CandidateData objCandD = new CandidateData();
                     dt1 = new DataTable();
                    dt1 = objCandD.Find(SID, YR, ANO);


                }
                else
                {
                    Response.Redirect("GenericErrorsPage.aspx?ErrorNo=1");
                }
            }
            catch (System.Threading.ThreadAbortException ext)
            {
            }
            catch (Exception ex)
            {
                Response.Redirect("GenericErrorsPage.aspx?ErrorNo=1");
            }
            //if (Convert.ToInt32(LblJobSourceId.Text) == Utility.Department.DSSSB) {
            //    Label1.Text = "1)I know the above details are correct and if wrong, Board's decision will be final and binding on me.<br/><font color=red>Note: Your online form after verification by the Commission of the competitive examination / interview will be eligible for. Confirm this website only after your application is not considered a valid application.";
            //} else {
            Label1.Text = "1)I know the above details are correct and if wrong, Board's decision will be final and binding on me.<br/><font color=red>Note:  Confirm this, your online form  will be eligible only after verification by the Board of the competitive examination / interview.";
            //}

            LblPost.Text = dt1.Rows[0]["JobTitle"].ToString();
            lblSeat_General.Text = dt1.Rows[0]["Seat_Gen"].ToString();
            if (dt1.Rows[0]["PDF"].ToString() != "")
            {
                hlnkAdvt.NavigateUrl = "AdvtDetailFiles/" + dt1.Rows[0]["PDF"].ToString();
            }


            lblJobFirstdate.Text = Utility.formatDate(dt1.Rows[0]["DOBFrom"].ToString());

            lblJobLastdate.Text = Utility.formatDate(dt1.Rows[0]["DOBTO"].ToString());
            
            //if (dt1.Rows[0]["Seat_Gen"].ToString() == "0")
            //{
            //    //trGenCategorySel.Visible = false;
            //    ddlCategory.Items.Remove("General");
            //    if (dt1.Rows[0]["Seat_SC"].ToString() == "0")
            //    {
            //        // trGenCategorySel.Visible = True
            //        ddlCategory.Items.Remove("SC");
            //    }
            //    if (dt1.Rows[0]["Seat_SEBC"].ToString() == "0")
            //    {
            //        //  trGenCategorySel.Visible = True
            //        ddlCategory.Items.Remove("SEBC");
            //    }
            //    if (dt1.Rows[0]["Seat_ST"].ToString() == "0")
            //    {
            //        //trGenCategorySel.Visible = True
            //        ddlCategory.Items.Remove("ST");
            //    }
            //}

            if (Convert.ToBoolean(dt1.Rows[0]["Education"]) == false)
            {
                panEdu.Visible = false;
            }
            else
            {
                panEdu.Visible = true;
            }

           

            

            if (Convert.ToBoolean(dt1.Rows[0]["Experience"]) == false)
            {
                PanExperience.Visible = false;
            }
            else
            {
                PanExperience.Visible = true;
            }

            // apPayment.Visible = False
            ViewState["ADVT_SHOW"] = false;

          


          
            if (dt1.Rows[0]["gender"].ToString() == "M")
            {

                rbGender.Items[1].Enabled = false;
            }
            else if (dt1.Rows[0]["gender"].ToString() == "F")
            {
                rbGender.Items[0].Enabled = false;
            }
            else
            {
                //rbGender.Items.Item(1).Enabled = False
            }
            //CreamyLayerCertificateStatus(False)
            
           
            
           
            





            if (!IsPostBack)
            {
                populateCasteCategory();
                populateDistrict();
                ExServiceManEnable(false);
                populateState();
                populateEdu();
                fillEduClass();
            }
            EnableDebard(false);
           
            
           
         


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


			//If rbApplyfor.SelectedValue = 1 Then
            //if (lblAdvtId.Text == "9" | lblAdvtId.Text == "12" | lblAdvtId.Text == "20" | lblAdvtId.Text == "21") {
            //    string js = "if(!confirm('You have selected " + rbApplyfor.SelectedItem.Text + " as a Subject for Exam Question Paper No. 1')) return false;";
            //    BtnInsertUpdate.Attributes.Add("onclick", js);
            //    //BtnInsertUpdate.Attributes.Add("onclick", "confirm('You have selected " & rbApplyfor.SelectedItem.Text & " as a Exam Question Paper Subject')")
            //}
			//==================     AUDIT DETAILS =========================
            //objAudit.Ip_Address = Request.UserHostAddress;
            //objAudit.Web_Page = "JobApply.aspx";
            //objAudit.Login_Id = 0;
            //objAudit.OfficeId = 0;
		//==============================================================
		
	

	#region "Functions"
   



	private void populateMinClass()
	{
		try {
            CandidateData objCandD = new CandidateData();
            DataTable dt2 = new DataTable();

            dt2 = objCandD.GetEducationMinimumClass(LblJobSourceId.Text, lblAdvtYear.Text, lblAdvtId.Text);
			
			FillDropDown(ddlMinClass, dt2, "SUBSTD", "MinClass");
			dt2 = null;

			//For Add Education 
          
		} catch (Exception ex) {
			throw ex;
		}
	}
    //public bool checkClass(Array Aclass, string eclass, string minclass)
    //{
    //    bool functionReturnValue = false;
    //    int index = 0;
    //    if (string.IsNullOrEmpty(minclass))
    //    {
    //        index = Array.IndexOf(Aclass, "pass");
    //    }
    //    else
    //    {
    //        index = Array.IndexOf(Aclass, minclass);
    //    }

    //    int i = 0;
    //    for (i = 0; i <= index; i++)
    //    {
    //        if (Aclass[i] == eclass)
    //        {
    //            return true;
    //            return functionReturnValue;
    //        }
    //    }
    //    return false;
    //    return functionReturnValue;
    //}
	public void fillEduClass()
	{
		//Utility.populateEducationClass(ddlClass1)
		//Utility.populateEducationClass(ddlclass2)
		Utility.populateEducationClass(ddlClass3);
        //Utility.populateEducationClass(ddlClass4);
        //Utility.populateEducationClass(ddlClass5);
        //Utility.populateEducationClass(ddlClass6);
	}
   
   
	
    //public void FillDropDown_Add(DropDownList ddl, DataTable dt, string textfield, string valuefield)
    //{
    //    ddl.Items.Clear();
    //    ddl.DataTextField = textfield;
    //    ddl.DataValueField = valuefield;
    //    ddl.DataSource = dt;
    //    ddl.DataBind();
    //    ddl.Items.Insert(0, new ListItem("Select"));
       

    //}
	public void FillDropDown(DropDownList ddl, DataTable dt, string textfield, string valuefield)
	{
		ddl.Items.Clear();
		ddl.DataTextField = textfield;
		ddl.DataValueField = valuefield;
		ddl.DataSource = dt;
		ddl.DataBind();
		ddl.Items.Insert(0, new ListItem("Select"));
		//If CInt(LblJobSourceId.Text) <> Utility.Department.GPSSB Then
		//ddl.Items.Add(New ListItem("Not in List", 9999))
		// Else
		ddl.AutoPostBack = false;
		//  End If
	}
	private void populateDistrict()
	{
		try {

            CandidateData objCandD = new CandidateData();
			DataTable dt = new DataTable();
			dt = objCandD.SelectDistrict();
			
			ddlDistrict.Items.Clear();
			ddlDistrict.DataTextField = "distname_e";
			ddlDistrict.DataValueField = "distid";
			ddlDistrict.DataSource = dt;
			ddlDistrict.DataBind();
			ddlDistrict.Items.Insert(0, new ListItem("Select"));

			ddlDistrict_per.Items.Clear();
			ddlDistrict_per.DataTextField = "distname_e";
			ddlDistrict_per.DataValueField = "distid";
			ddlDistrict_per.DataSource = dt;
			ddlDistrict_per.DataBind();
			ddlDistrict_per.Items.Insert(0, new ListItem("Select"));
		// dt = Nothing
		} catch (Exception ex) {
			throw ex;
		}
	}
	
	public void populateCasteCategory()
	{
        CandidateData objCandD = new CandidateData();
        DataTable dt = new DataTable();

        dt = objCandD.SelectCasteType();
		
		ddlCategory.Items.Clear();
		ddlCategory.DataTextField = "castetype";
		ddlCategory.DataValueField = "castetype";
		ddlCategory.DataSource = dt;
		ddlCategory.DataBind();
		ddlCategory.Items.Insert(0, new ListItem("Select"));
		//ddlCategory.Items.Insert(1, New ListItem("SC", 1))
		//ddlCategory.Items.Insert(2, New ListItem("ST", 2))
		//ddlCategory.Items.Insert(3, New ListItem("SEBC", 3))
		//ddlCategory.Items.Insert(4, New ListItem("General", 4))
		dt = null;
	}
	private void populateState()
	{
		try {
            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();

            dt = objCandD.SelectState();
			
			FillDropDown(ddlState, dt, "state", "code");
			ddlState.SelectedValue = "7";
			dt = null;
		} catch (Exception ex) {
			throw ex;
		}
	}
	private void populateEdu()
	{
		try {
            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();

            dt = objCandD.GetEducationMinimumClass(LblJobSourceId.Text, lblAdvtYear.Text, lblAdvtId.Text);
			
			FillDropDown(ddlEdu, dt, "name", "substd");
            //FillDropDown(ddlEdu4, dt, "name", "substd");
            //FillDropDown(ddlEdu5, dt, "name", "substd");
            //FillDropDown(ddlEdu6, dt, "name", "substd");
			dt = null;


		} catch (Exception ex) {
			throw ex;
		}
	}
	private void populatePH(Int16 JobSourceID, Int32 AdvtYear, Int32 AdvtNo)
	{
		try {
            CandidateData objCandD = new CandidateData();
            DataTable dt = new DataTable();
            dt = objCandD.SelectPH(LblJobSourceId.Text, lblAdvtYear.Text, lblAdvtId.Text);
			
			rbPh.Items.Clear();
			rbPh.DataTextField = "ph";
			rbPh.DataValueField = "ph";
			rbPh.DataSource = dt;
			rbPh.DataBind();
		} catch (Exception ex) {
			throw ex;
		}
	}

	
	
   

    
	public void EnableGovtService(bool flag)
	{
		txtGovtJoinDt.Text = "";
		txtGovtJoinDt.Enabled = flag;
		rbFeder.SelectedValue = "false";
		rbFeder.Enabled = flag;
		if (flag == true) {
			txtGovtJoinDt.BackColor = System.Drawing.Color.White;
		} else {
			txtGovtJoinDt.BackColor = System.Drawing.Color.Black;
		}


	}
	public void ExServiceManEnable(bool state)
	{
		txtExFromDt.Text = "";
		txtExFromDt.Enabled = state;
		if (state == true) {
			txtExFromDt.BackColor = System.Drawing.Color.White;
			txtExToDt.BackColor = System.Drawing.Color.White;
		} else {
			txtExFromDt.BackColor = System.Drawing.Color.Black;
			txtExToDt.BackColor = System.Drawing.Color.Black;
		}
		txtExToDt.Text = "";
		txtExToDt.Enabled = state;
	}
 
	public void EnableDebard(bool flag)
	{
		txtDebardDt.Text = "";
		txtDebardYr.Text = "";
		txtDebardDt.Enabled = flag;
		txtDebardYr.Enabled = flag;
		if (flag == true) {
			txtDebardDt.BackColor = System.Drawing.Color.White;
			txtDebardYr.BackColor = System.Drawing.Color.White;
		} else {
			txtDebardDt.BackColor = System.Drawing.Color.Black;
			txtDebardYr.BackColor = System.Drawing.Color.Black;
		}
	}
	#endregion

	#region "Button Click"

	

	

    //private void btnAddEdu_Click(object sender, System.EventArgs e)
    //{
    //    if (edu4.Visible == false) {
    //        //If (ddlEdu.SelectedIndex <> 0 And txtPerc3.Text.Trim <> "" And ddlClass3.SelectedIndex <> 0 And txtYear3.Text.Trim <> "") Then
    //        if ((ddlEdu.SelectedIndex != 0 & ddlClass3.SelectedIndex != 0 & txtYear3.Text!="")) {
    //            edu4.Visible = true;
    //            return;
    //        } else {
    //            lblErrorMsg.Text = "Enter First Education Data Properly.";
    //            return;
    //        }
    //    }

    //    if (edu5.Visible == false) {
    //        //If (ddlEdu4.SelectedIndex <> 0 And txtPerc4.Text.Trim <> "" And ddlClass4.SelectedIndex <> 0 And txtYear4.Text.Trim <> "") Then
    //        if ((ddlEdu4.SelectedIndex != 0 & ddlClass4.SelectedIndex != 0 & txtYear4.Text != ""))
    //        {
    //            edu5.Visible = true;
    //            return;
    //        } else {
    //            lblErrorMsg.Text = "Enter Second Job Education Data Properly..";
    //            return;
    //        }
    //    }
    //    if (edu6.Visible == false) {
    //        //If (ddlEdu5.SelectedIndex <> 0 And txtPerc5.Text.Trim <> "" And ddlClass5.SelectedIndex <> 0 And txtYear5.Text.Trim <> "") Then
    //        if ((ddlEdu5.SelectedIndex != 0 & ddlClass5.SelectedIndex != 0 & txtYear5.Text != ""))
    //        {
    //            edu6.Visible = true;
    //            btnAddEdu.Enabled = false;
    //        } else {
    //            lblErrorMsg.Text = "Enter Third Job Education Data Properly..";
    //            return;
    //        }
    //    }
    //}

	private void btnAddExperience_Click(object sender, System.EventArgs e)
	{
		if (trExper2.Visible == false) {
			if ((!string.IsNullOrEmpty(txtEmpName1.Text) & !string.IsNullOrEmpty(txtPost1.Text) & (txtDayFrom1.Text!="") & (txtDayTo1.Text!=""))) {
				trExper2.Visible = true;
				return;
			} else {
				lblErrorMsg.Text = "Enter First Job Experience Data Properly.";
				return;
			}
		}
		if (trExper3.Visible == false) {
			if ((!string.IsNullOrEmpty(txtEmpName2.Text) & !string.IsNullOrEmpty(txtPost2.Text) & (txtDayFrom2.Text!="") & (txtDayTo2.Text!=""))) {
				trExper3.Visible = true;
				return;
			} else {
				lblErrorMsg.Text = "Enter Second Job Experience Data Properly.";
				return;
			}
		}
		if (trExper4.Visible == false) {
			if ((!string.IsNullOrEmpty(txtEmpName3.Text) & !string.IsNullOrEmpty(txtPost3.Text) & (txtDayFrom3.Text!="") & (txtDayTo3.Text!=""))) {
				trExper4.Visible = true;
				return;
			} else {
				lblErrorMsg.Text = "Enter Third Job Experience Data Properly.";
				return;
			}
		}
		if (trExper5.Visible == false) {
			if ((!string.IsNullOrEmpty(txtEmpName4.Text) & !string.IsNullOrEmpty(txtPost4.Text) & (txtDayFrom4.Text!="") & (txtDayTo4.Text!=""))) {
				trExper5.Visible = true;
				return;
			} else {
				lblErrorMsg.Text = "Enter Fourth Job Experience Data Properly.";
				return;
			}
		}
		if (trExper6.Visible == false) {
			if ((!string.IsNullOrEmpty(txtEmpName4.Text) & !string.IsNullOrEmpty(txtPost4.Text) & (txtDayFrom4.Text!="") & (txtDayTo4.Text!=""))) {
				trExper6.Visible = true;
				btnAddExperience.Enabled = false;
				return;
			} else {
				lblErrorMsg.Text = "Enter Fifth Job Experience Data Properly.";
				return;
			}
		}
	}

	#endregion

	#region "SelectedIndexChanged Event"
	protected void ddlCategory_SelectedIndexChanged(System.Object sender, System.EventArgs e)
	{
		//populateSCST()
		if (ddlCategory.SelectedValue == "SEBC") {
		// CreamyLayerCertificateStatus(True)
		} else {
			// CreamyLayerCertificateStatus(False)

		}
	
	}
	
	private void rblGeneralApply_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		//If rblGeneralApply.SelectedValue = "0" Then
		//    apPayment.Visible = False
		//Else
		//    apPayment.Visible = True
		//End If
	}
	
	
	
	private void ddlEdu_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		if (ddlEdu.SelectedValue == "9999") {
			txtEduOther.Visible = true;
		} else {
			txtEduOther.Visible = false;
		}
	}
    //private void ddlEdu4_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    if (ddlEdu4.SelectedValue == "9999") {
    //        txtEduOther4.Visible = true;
    //    } else {
    //        txtEduOther4.Visible = false;
    //    }
    //}
    //private void ddlEdu5_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    if (ddlEdu5.SelectedValue == "9999")
    //    {
    //        txtEduOther5.Visible = true;
    //    }
    //    else
    //    {
    //        txtEduOther5.Visible = false;
    //    }
    //}
    //private void ddlEdu6_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    if (ddlEdu6.SelectedValue == "9999") {
    //        txtEduOther6.Visible = true;
    //    } else {
    //        txtEduOther6.Visible = false;
    //    }
    //}
	
	private void rbAgreement_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		//If rbAgreement.SelectedValue = 1 Then
		//    ' trCaptcha.Visible = True
		//    BtnInsertUpdate.Visible = True
		//Else
		//    BtnInsertUpdate.Visible = False
		//    ' trCaptcha.Visible = False
		//End If
	}


	
	
	#endregion

	#region "DataGrid Events"
	protected void grdDistrict_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header) {
			e.Row.Cells[1].Visible = false;
		}
		if (e.Row.RowType == DataControlRowType.DataRow) {
			e.Row.Cells[1].Visible = false;
		}

	}
	#endregion

	protected void rbPh_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (rbPh.SelectedValue == "Not Applicable") {
			phPercentage.Visible = false;
		} else {
			phPercentage.Visible = true;
		}

		
	}


	protected void txtRegDt_TextChanged(object sender, EventArgs e)
	{
	}

	protected void rbGender_SelectedIndexChanged(object sender, EventArgs e)
	{
		
	}




    protected void BtnInsertUpdate_Click(object sender, EventArgs e)
    {
        CandidateData objCandD = new CandidateData();
        int tmp = 0;
        if (cbLang_eng_read.Checked == true)
        {
            cbLang_eng_read.Text = "true";

        }
        else
        {
            cbLang_eng_read.Text = "false";
        }
        if (cbLang_eng_Write.Checked == true)
        {
            cbLang_eng_Write.Text = "true";

        }
        else
        {
            cbLang_eng_Write.Text = "false";
        }
        if (cbLang_eng_Speak.Checked == true)
        {
            cbLang_eng_Speak.Text = "true";

        }
        else
        {
            cbLang_eng_Speak.Text = "false";
        }
        if (cbLang_hin_read.Checked == true)
        {
            cbLang_hin_read.Text = "true";

        }
        else
        {
            cbLang_hin_read.Text = "false";
        }
        if (cbLang_hin_Write.Checked == true)
        {
            cbLang_hin_Write.Text = "true";

        }
        else
        {
            cbLang_hin_Write.Text = "false";
        }
        if (cbLang_hin_Speak.Checked == true)
        {
            cbLang_hin_Speak.Text = "true";

        }
        else
        {
            cbLang_hin_Speak.Text = "false";
        }
      

        
        int max_app_id=objCandD.get_max_app_no()+1;
        if(!String.IsNullOrEmpty(rbPh.SelectedValue))
        {
            //tmp = objCandD.InsertJobApplication(Convert.ToInt16(LblJobSourceId.Text), Convert.ToInt32(lblAdvtYear.Text), Convert.ToInt32(lblAdvtId.Text), max_app_id, ddlSalute.SelectedItem.Text, txtFirstName.Text, txtLastName.Text, txtSurname.Text, txtMotherName.Text, txtAddress.Text, Convert.ToInt32(ddlDistrict.SelectedValue), txtAddress_per.Text, Convert.ToInt32(ddlDistrict_per.SelectedValue), txtPIN.Text, txtPIN_per.Text, txtState.Text, txtNationality.Text, Convert.ToInt64(txtMobileNo.Text), Convert.ToDateTime(txtBirthDt.Text), rbGender.SelectedValue, rbMeritalStatus.SelectedValue, ddlCategory.SelectedItem.ToString(), rbPh.SelectedItem.ToString(), (rbExservice.SelectedValue), txtExFromDt.Text, txtExToDt.Text, Convert.ToInt32(rbWidow.SelectedValue), true, txtCLCDate.Text, txtCLCNo.Text, txtEmail.Text, rbDisqualify.SelectedValue, txtDebardDt.Text, txtDebardYr.Text, cbLang_eng_read.Text, cbLang_eng_Write.Text, cbLang_eng_Speak.Text, cbLang_hin_read.Text, cbLang_hin_Write.Text, cbLang_hin_Speak.Text, rbGovtEmployee.SelectedValue, txtGovtJoinDt.Text, rbFeder.SelectedValue);
        }
        else
        {
            //tmp = objCandD.InsertJobApplication(Convert.ToInt16(LblJobSourceId.Text), Convert.ToInt32(lblAdvtYear.Text), Convert.ToInt32(lblAdvtId.Text), max_app_id, ddlSalute.SelectedItem.Text, txtFirstName.Text, txtLastName.Text, txtSurname.Text, txtMotherName.Text, txtAddress.Text, Convert.ToInt32(ddlDistrict.SelectedValue), txtAddress_per.Text, Convert.ToInt32(ddlDistrict_per.SelectedValue), txtPIN.Text, txtPIN_per.Text, txtState.Text, txtNationality.Text, Convert.ToInt64(txtMobileNo.Text), Convert.ToDateTime(txtBirthDt.Text), rbGender.SelectedValue, rbMeritalStatus.SelectedValue, ddlCategory.SelectedItem.ToString(), "", (rbExservice.SelectedValue), txtExFromDt.Text, txtExToDt.Text, Convert.ToInt32(rbWidow.SelectedValue), true, txtCLCDate.Text, txtCLCNo.Text, txtEmail.Text, rbDisqualify.SelectedValue, txtDebardDt.Text, txtDebardYr.Text, cbLang_eng_read.Text, cbLang_eng_Write.Text, cbLang_eng_Speak.Text, cbLang_hin_read.Text, cbLang_hin_Write.Text, cbLang_hin_Speak.Text, rbGovtEmployee.SelectedValue, txtGovtJoinDt.Text, rbFeder.SelectedValue);
        }

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        int j=0,k=0;
        j=objCandD.insert_education(tmp, 8000, ddlEdu.SelectedItem.Text, 0, 100, txtPerc3.Text, ddlClass3.Text, txtBoard3.Text, txtState3.Text, txtYear3.Text, ip, Utility.formatDate(DateTime.Now),0, 0, 0, null);       
        //k=objCandD.insert_exp(tmp,txtPost1.Text,txtDayFrom1.Text,txtDayTo1.Text,txtEmpName1.Text,txtEmpAddr1.Text,txtEmpContact1.Text,txtEmpPin1.Text,ip,Utility.formatDate(DateTime.Now),0,"","","","",txtEmpSal1.Text);
        if (tmp > 0 && j > 0 && k > 0)
        {
            msg.Show("Record inserted successfully");
        }
        else
        {
            msg.Show("Not able to Insert Data");
        }
    }

    protected void rbphC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbphC.SelectedItem.Text == "Yes")
        {
            rbPh.Visible = true;
            populatePH(Convert.ToInt16(LblJobSourceId.Text), Convert.ToInt32(lblAdvtYear.Text), Convert.ToInt32(lblAdvtId.Text));

        }
        else
        {
            rbPh.Visible = false;
        }
    }

    protected void rbExservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbExservice.SelectedValue == "true")
        {
            ExServiceManEnable(true);
        }
        else
        {
            ExServiceManEnable(false);
        }
    }

    protected void rbDisqualify_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbDisqualify.SelectedValue == "true")
        {
            EnableDebard(true);
        }
        else
        {
            EnableDebard(false);
        }

    }

    protected void rbGovtEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbGovtEmployee.SelectedValue == "true")
        {
            EnableGovtService(true);
        }
        else
        {
            EnableGovtService(false);
        }
    }
    protected void rbFeder_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
