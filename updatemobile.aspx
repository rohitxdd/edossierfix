<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="updatemobile.aspx.cs" Inherits="updatemobile" Title="Update Mobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table width="90%" class="border_gray">
        <tr>
            <td colspan="4" class="tr" align="center">
                Registration Details
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
            </td>
        </tr>
        <tr align="left" class="formlabel">
            <td colspan="4" align="center">
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label29" runat="server" Font-Bold="true" Text="Registration Number : ">
                </asp:Label>
                &nbsp; &nbsp;&nbsp;
                <asp:Label ID="txt_reg" runat="server" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr align="left" class="darkblue">
            <td>
                <span style="color: red"></span>
                <asp:Label ID="Label13" runat="server" Text="Name"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txt_name1" runat="server" MaxLength="50" Enabled="false"></asp:TextBox>
                <%--<asp:Label ID="Label20" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                    Text="(Name written as in educational certificates. Don't use Mr.,Ms.,Dr. etc.)"></asp:Label>--%>
                <asp:RequiredFieldValidator ID="rfvname" runat="server" Display="none" ControlToValidate="txt_name1"
                    ValidationGroup="1" ErrorMessage="Please Enter Name" Width="11px"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revnm" runat="server" Display="None" ControlToValidate="txt_name1"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in Name"
                    ValidationGroup="1" Width="51px"></asp:RegularExpressionValidator>
                <asp:Button ID="btnname" runat="server" Text="Modify" OnClick="btnname_Click" />
                <asp:Button ID="btnupdatename" runat="server" Text="Update" Visible="false" OnClick="btnupdatename_Click"
                    ValidationGroup="1" />
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="UID Number"></asp:Label>
            </td>
            <td>
                <asp:Label ID="txtuid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="left" class="darkblue">
            <td>
                <span style="color: red"></span>
                <asp:Label ID="Label15" runat="server" Text="Father's Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_fh_name1" runat="server" MaxLength="50" Enabled="false"></asp:TextBox><asp:RequiredFieldValidator
                    ID="rfvfname" runat="server" Display="none" ControlToValidate="txt_fh_name1"
                    ValidationGroup="2" ErrorMessage="Please Enter Father Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revfname" runat="server" Display="None" ControlToValidate="txt_fh_name1"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in FatherName or"
                    ValidationGroup="2"></asp:RegularExpressionValidator>
                <asp:Button ID="btnfname" runat="server" Text="Modify" OnClick="btnfname_Click" />
                <asp:Button ID="btnupdatefname" runat="server" Text="Update" Visible="false" OnClick="btnupdatefname_Click"
                    ValidationGroup="2" />
            </td>
            <td align="left">
                <span style="color: red"></span>
                <asp:Label ID="Label16" runat="server" Text="Mother's Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_mothername" runat="server" MaxLength="50" Enabled="false"></asp:TextBox><asp:RequiredFieldValidator
                    ID="rfvtxt_mothername" runat="server" Display="none" ControlToValidate="txt_mothername"
                    ValidationGroup="3" ErrorMessage="Please Enter Mother Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxt_mothername" runat="server" Display="None"
                    ControlToValidate="txt_mothername" ValidationExpression="^[a-zA-Z.\s]{1,50}$"
                    ErrorMessage="Invalid characters in Mother Name" ValidationGroup="3"></asp:RegularExpressionValidator>
                <asp:Button ID="btnmname" runat="server" Text="Modify" OnClick="btnmname_Click" />
                <asp:Button ID="btnupdatemname" runat="server" Text="Update" Visible="false" OnClick="btnupdatemname_Click"
                    ValidationGroup="3" />
                <%-- <asp:Label ID="txt_mothername" runat="server"></asp:Label>&nbsp;--%>
            </td>
        </tr>
        <tr align="left" class="darkblue">
            <td>
                <span style="color: red"></span>
                <asp:Label ID="Label1" runat="server" Text="Spouse Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSpouse" runat="server" MaxLength="50" Enabled="false"></asp:TextBox><asp:RequiredFieldValidator
                    ID="rfvSpouse" runat="server" Display="none" ControlToValidate="txtSpouse" ValidationGroup="4"
                    ErrorMessage="Please Enter Spouse Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtSpouse" runat="server" Display="None" ControlToValidate="txtSpouse"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in Spouse Name"
                    ValidationGroup="4"></asp:RegularExpressionValidator>
                <asp:Button ID="btnsname" runat="server" Text="Modify" OnClick="btnsname_Click" />
                <asp:Button ID="btnupdatesname" runat="server" Text="Update" Visible="false" OnClick="btnupdatesname_Click"
                    ValidationGroup="4" />
            </td>
            <td align="left">
                <span style="color: red"></span>
                <asp:Label ID="Label27" runat="server" Text="Gender"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlgender" runat="server" CssClass="darkblue" Enabled="false">
                    <asp:ListItem Value="M">Male</asp:ListItem>
                    <asp:ListItem Value="F">Female</asp:ListItem>
                    <asp:ListItem  Value="T">Transgender</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlgender" runat="server" Display="none" ControlToValidate="ddlgender"
                    ValidationGroup="1" ErrorMessage="Please Select Gender">
                </asp:RequiredFieldValidator>
                <asp:Button ID="btngender" runat="server" Text="Modify" OnClick="btngender_Click" />
                <asp:Button ID="btnupdategender" runat="server" Text="Update" Visible="false" OnClick="btnupdategender_Click"
                    ValidationGroup="1" />
            </td>
        </tr>
        <tr align="left" class="darkblue">
            <td>
                <asp:Label ID="Label26" runat="server" Text="Nationality"></asp:Label>
            </td>
            <td valign="middle">
                <asp:Label ID="lblnation" runat="server"></asp:Label><%-- <img id="Img1" alt="DatePicker" onclick="PopupPicker('txt_DOB', 250, 250)" src="Images/calendar.bmp"
                      style="width: 25px; height: 25px" runat="server" />--%>
            </td>
        </tr>
        <tr align="left" valign="top" class="darkblue">
            <td>
                <asp:Label ID="Label23" runat="server" Text="Mobile No."></asp:Label>
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txt_mob" runat="server" Width="37%" MaxLength="10" CausesValidation="True"
                    Enabled="false" OnTextChanged="txt_mob_TextChanged"  AutoPostBack="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvmob" runat="server" Display="none" ControlToValidate="txt_mob"
                    ValidationGroup="1" ErrorMessage="Please Enter Mobile No.">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revmob" runat="server" Display="None" ControlToValidate="txt_mob"
                    ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                    ValidationGroup="1">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="REVMobile" runat="server" ControlToValidate="txt_mob"
                    ValidationExpression=".{10}.*" ErrorMessage="Enter Minimum 10 Digit" Display="none"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
                <asp:Button ID="btnmobile" runat="server" Text="Modify" OnClick="btnmobile_Click" />
                <asp:Button ID="btnupdatemobile" runat="server" Text="Update" Visible="false" OnClick="btnupdatemobile_Click"
                    ValidationGroup="1" />
                <asp:HiddenField ID="Hidden_txtmob" runat="server" />
            </td>
            <td align="left" valign="top">
                <asp:Label ID="Label24" runat="server" Text="Email"></asp:Label>
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txt_email" runat="server" Enabled="false"  OnTextChanged="txt_email_TextChanged"  AutoPostBack="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvmail" runat="server" Display="none" ControlToValidate="txt_email"
                    ValidationGroup="1" ErrorMessage="Please Enter EMail-Id"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revmail" runat="server" Display="None" ControlToValidate="txt_email"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Characters In EmailId"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
                <asp:Button ID="btnemail" runat="server" Text="Modify" OnClick="btnemail_Click" />
                <asp:Button ID="btnupdateemail" runat="server" Text="Update" Visible="false" OnClick="btnupdateemail_Click"
                    ValidationGroup="1" />
            </td>
        </tr>
        <tr id="trUploadDocPCSP" runat="server" visible="false">
            <td width="18%">
                <img src="Images/new2.gif"><span style="color: Red; font-weight: bolder; font-size: 16px;">Updation
                    : </span>
            </td>
            <td align="left" colspan="4" class="darkblue">
                <asp:LinkButton ID="txtblnk" runat="server" ForeColor="Red" Font-Size="larger" Font-Bold="True"
                    OnClick="txtblnk_Click"><u>Update registration Details & upload Postcard size photograph for post code 1/20 to 116/20 (except 89/20)</u> </asp:LinkButton>
                <%--<ul>
                    <li>
                        <asp:LinkButton ID="txtblnk" runat="server" ForeColor="Red" Font-Size="larger" Font-Bold="True"
                            OnClick="txtblnk_Click"><span style="color:#072154">Link 1:</span> <u>Update registration Details & upload Postcard size photograph for post code 1/10 to 116/20</u> </asp:LinkButton>
                    </li>
                    <br />
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red" Font-Size="larger"
                            Font-Bold="True" OnClick="LinkButton1_Click"><span style="color:#072154">Link 2:</span> <u>Upload post card size photograph
                        for post codes 1/20 to 116/20</u></asp:LinkButton>
                    </li>
                </ul>--%>
            </td>
        </tr>
        <tr align="left">
            <td colspan="4" style="color: #c00000;">
                <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="1" />
                <asp:ValidationSummary ID="vs2" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="2" />
                <asp:ValidationSummary ID="vs3" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="3" />
                <asp:ValidationSummary ID="vs4" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="4" />
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    <br />
    <table width="90%" >
        <tr>
            <td colspan="4"  align="center">
                
                    <asp:Button ID="btnUpdateNewDetails" runat="server" Text="Modify details" OnClick="btnUpdateNewDetails_Click" BackColor="#000066" Font-Bold="True" ForeColor="White"  Visible="false"/>
                
            </td>
        </tr><br />
        <tr>
            <td>
                <asp:Label ID="Label3" ForeColor="Red" runat="server" Text="NOTE: Any two fields can be modified."  Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
   <br />
    <panel id="newUpdate" runat="server" visible="false">
        <table id="tblnewUpdate" runat="server" width="90%" class="border_gray">
        <tr>
            <td colspan="4" class="tr" align="center">
                Updation of personal Details
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
            </td>
        </tr>
        <tr align="left" class="darkblue">
            <td style="width: 250px">
                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </td>
            <td>
                <span style="color: red"></span>
                <asp:Label ID="Label5" runat="server" Text="Applicant's Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtName" runat="server" MaxLength="50" Enabled="false" ></asp:TextBox>
                <%--<asp:Label ID="Label20" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                    Text="(Name written as in educational certificates. Don't use Mr.,Ms.,Dr. etc.)"></asp:Label>--%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="none" ControlToValidate="txt_name1"
                    ValidationGroup="1" ErrorMessage="Please Enter Name" Width="11px"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ControlToValidate="txt_name1"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in Name"
                    ValidationGroup="1" Width="51px"></asp:RegularExpressionValidator>
                
                <asp:Button ID="btnApplicantName" runat="server" Text="Update" Visible="false" 
                    ValidationGroup="1" OnClick="btnApplicantName_Click" />
            </td>
            
        </tr>
        <tr align="left" class="darkblue">
            <td style="width: 250px">
                <asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </td>
            <td>
                <span style="color: red"></span>
                <asp:Label ID="Label8" runat="server" Text="Father's Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtFname" runat="server" MaxLength="50" Enabled="false" ></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" Display="none" ControlToValidate="txt_fh_name1"
                    ValidationGroup="2" ErrorMessage="Please Enter Father Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None" ControlToValidate="txt_fh_name1"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in FatherName or"
                    ValidationGroup="2"></asp:RegularExpressionValidator>
                <asp:Button ID="btnFatherName" runat="server" Text="Update" Visible="false" 
                    ValidationGroup="2" OnClick="btnFatherName_Click" />
            </td></tr>
            <tr align="left" class="darkblue">
                <td style="width: 250px">
                <asp:CheckBox ID="CheckBox3" runat="server" OnCheckedChanged="CheckBox3_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </td>
            <td align="left">
                <span style="color: red"></span>
                <asp:Label ID="Label9" runat="server" Text="Mother's Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtMname" runat="server" MaxLength="50" Enabled="False"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" Display="none" ControlToValidate="txt_mothername"
                    ValidationGroup="3" ErrorMessage="Please Enter Mother Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="None"
                    ControlToValidate="txt_mothername" ValidationExpression="^[a-zA-Z.\s]{1,50}$"
                    ErrorMessage="Invalid characters in Mother Name" ValidationGroup="3"></asp:RegularExpressionValidator>
                <asp:Button ID="btnMotherName" runat="server" Text="Update" Visible="false" 
                    ValidationGroup="3" OnClick="btnMotherName_Click" />
                <%-- <asp:Label ID="txt_mothername" runat="server"></asp:Label>&nbsp;--%>
            </td>
        </tr>
        <tr align="left" class="darkblue">
           <td style="width: 250px">
                <asp:CheckBox ID="CheckBox4" runat="server" OnCheckedChanged="CheckBox4_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </td>
        
            <td align="left">
                <span style="color: red"></span>
                <asp:Label ID="Label11" runat="server" Text="Gender"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="Ddl_gender" runat="server" CssClass="darkblue" Enabled="false" >
                    <asp:ListItem Value="M">Male</asp:ListItem>
                    <asp:ListItem Value="F">Female</asp:ListItem>
                    <asp:ListItem  Value="T">Transgender</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="none" ControlToValidate="ddlgender"
                    ValidationGroup="1" ErrorMessage="Please Select Gender">
                </asp:RequiredFieldValidator>
                <asp:Button ID="btnUpdatenewGender" runat="server" Text="Update" Visible="false" 
                    ValidationGroup="1" OnClick="btnUpdatenewGender_Click" />
            </td>
        </tr>
        
        <tr align="left" valign="top" class="darkblue">
            <td style="width: 250px">
                <asp:CheckBox ID="CheckBox5" runat="server" OnCheckedChanged="CheckBox5_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </td>
            <td>
                <asp:Label ID="Label17" runat="server" Text="Id Proof"></asp:Label>
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="TxtIdentity" Enabled="false" runat="server" Width="37%" MaxLength="10" CausesValidation="True"
                     ></asp:TextBox>
                <asp:Button ID="btnIdentity" runat="server" Text="Update" Visible="false" 
                    ValidationGroup="1" OnClick="btnIdentity_Click" />
                <asp:HiddenField ID="HiddenField1_1" runat="server" />
            </td>
           
        </tr>
            </table><br />
        <table width="90%" class="border_gray">
            <tr align="left" class="darkblue">
                <td >
                    <asp:CheckBox ID="ChkAgreed" runat="server" AutoPostBack="true" ForeColor="#FF3300" Height="32px" Width="296px" Text="Undertaking by the Applicant" Font-Size="Small" OnCheckedChanged="ChkAgreed_CheckedChanged"></asp:CheckBox>
                    </td>
            </tr>
            <tr align="left" class="darkblue">
                <td style="height: 82px" >
                    <br />
                i)	I understand that in the event of any information being found false or incorrect at any stage, my candidature / selection / appointment is liable to be cancelled / terminated automatically without any notice to me and action can be taken against me by DSSSB.
                <br />
                <br />
                    ii)	The information submitted herein shall be treated as final in respect of my candidature.
                    <br />
                <br />
                    iii)	I further declare that all information filled by me is correct and I will not claim any change in future.
                <br />
                </td>
            </tr>
                  
    </table><br />
        <table width="90%" class="border_gray">
            <tr align="left" class="darkblue">
                            <asp:HiddenField ID="hdnfMOTP" runat="server" />
                            
                            <asp:HiddenField ID="OTPVerified" runat="server" />
                            <asp:HiddenField ID="hdnfOTPVerifiedMEB" runat="server" />
            </tr>
            <tr><td> Enter OTP: </td>
                <td><asp:TextBox ID="txtMOTP" runat="server"></asp:TextBox>
                <asp:Button ID="btnSendOTP" runat="server" Text="Get OTP" OnClick="btnSendOTP_Click"></asp:Button>
                <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" OnClick="btnVerifyOTP_Click"></asp:Button>
                <br />
                </td>
            </tr>
            <tr><td></td><td><asp:Label ID="Label56" runat="server" Text=""></asp:Label></td></tr>
        </table>
        <br />
        <asp:Button ID="BtnFinalSubmit" runat="server" Text="Final Submit" OnClick="BtnFinalSubmit_Click" BackColor="#000066" Font-Bold="True" ForeColor="White"></asp:Button>
    
    <br /></panel>
   <br />
</asp:Content>
