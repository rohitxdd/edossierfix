<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Apply.aspx.cs" Inherits="Apply" Title="Candidate Detail's Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript" language="javascript" src="Jscript/JScript.js">
    </script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <table style="width: 110%" border="0" cellpadding="0" cellspacing="0" align="left">
        <tr>
            <td style="height: 30px;" align="left" colspan="4">
                <asp:Label ID="lbl" runat="server" CssClass="formheading" Text="Application for the Post of :"></asp:Label>
                &nbsp;<asp:Label ID="lbl_app" runat="server" ForeColor="#C00000" CssClass="ariallightgrey"></asp:Label>
                ::
                <asp:Label ID="formheading" runat="server" CssClass="formheading" Text="    Post Code:"></asp:Label>
                <asp:Label ID="lbl_post_code" runat="server" ForeColor="#C00000" CssClass="ariallightgrey"></asp:Label>
                <asp:Label ID="Label4" runat="server" CssClass="formheading" Text="Advt No:" Visible="False"></asp:Label>
                <asp:Label ID="lbl_advt" runat="server" ForeColor="#C00000" CssClass="ariallightgrey"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label5" runat="server" Text="Note:" CssClass="ariallightgrey" ForeColor="#C00000"></asp:Label>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="Label6" runat="server" Text="The fields with " CssClass="formheading"></asp:Label>
                <asp:Label ID="Label1" runat="server" ForeColor="red" Text="*" CssClass="formheading"></asp:Label>
                <asp:Label ID="Label7" runat="server" Text=" marks are mandatory.Special Characters[&;%'()+-!.,*~|$#[]^@] are not allowed."
                    CssClass="formheading"></asp:Label>
                <br />
                <span id="id_span" visible="false" runat="server">
                    <asp:Label ID="Label8" runat="server" Text="Your date of birth should be between"
                        CssClass="formheading"></asp:Label>
                    <asp:Label ID="lbl_dob_f" runat="server" ForeColor="#C00000" CssClass="ariallightgrey"></asp:Label>
                    &nbsp;<asp:Label ID="Label10" runat="server" CssClass="formlabel" Text="and"></asp:Label>
                    &nbsp;<asp:Label ID="lbl_dob_t" runat="server" ForeColor="#C00000" CssClass="ariallightgrey"></asp:Label>
                    &nbsp;<asp:Label ID="Label11" runat="server" CssClass="formheading" Text=",if you are not entitles for age relaxation."></asp:Label>
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="for details about age relaxation please see the advertisement."
                        CssClass="formheading"></asp:Label>
                </span>
            </td>
        </tr>
        <tr align="left" class="tr">
            <td style="height: 19px; width: 23%; font-size: medium;" colspan="4" class="tr">
                <asp:Label ID="Label12" runat="server" Text="Personal Details"></asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td style="height: 33px">
                <span style="color: Red">*</span><asp:Label ID="Label13" runat="server" Text="Name "
                    CssClass="formlabel"></asp:Label>
            </td>
            <td style="height: 33px;">
                <asp:TextBox ID="txt_name" runat="server" Width="90%" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvname" runat="server" Display="none" ControlToValidate="txt_name"
                    ValidationGroup="1" ErrorMessage="Please Enter Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revnm" runat="server" Display="None" ControlToValidate="txt_name"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in Name"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td>
                <%--<span style="color: red">*</span>--%><asp:Label ID="Label15" runat="server" Text="Father's Name"
                    CssClass="formlabel"></asp:Label>
            </td>
            <td style="height: 33px;">
                <asp:TextBox ID="txt_fh_name" runat="server" Width="90%" Enabled="false"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="rfvfname" runat="server" Display="none" ControlToValidate="txt_fh_name"
                    ValidationGroup="1" ErrorMessage="Please Enter Father Name"></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator ID="revfname" runat="server" Display="None" ControlToValidate="txt_fh_name"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in FatherName"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr align="left">
            <td style="height: 18px" align="left">
                <%--<span style="color: red">*</span>--%><asp:Label ID="Label16" runat="server" Text="Mother's Name"
                    CssClass="formlabel" Width="90%"></asp:Label>
            </td>
            <td style="height: 18px">
                <asp:TextBox ID="txt_mothername" runat="server" Width="90%" Enabled="false"></asp:TextBox>
                <%--  <asp:RequiredFieldValidator ID="rfvmname" runat="server" Display="none" ControlToValidate="txt_mothername"
                    ValidationGroup="1" ErrorMessage="Please Enter Mother Name"></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator ID="revmname" runat="server" Display="None" ControlToValidate="txt_mothername"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in MotherName"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td align="left">
                <asp:Label ID="Label27" runat="server" CssClass="formlabel" Text="Gender"></asp:Label>
            </td>
            <td align="left" style="width: 257px">
                <asp:RadioButtonList ID="RadioButtonList_mf" runat="server" RepeatDirection="Horizontal"
                    Height="16px" CssClass="formlabel" Enabled="false">
                    <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                    <asp:ListItem Text="Transgender" Value="T"></asp:ListItem>                
                    </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvgen" runat="server" Display="none" ControlToValidate="RadioButtonList_mf"
                    ValidationGroup="1" ErrorMessage="Please Select Gender"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="Label28" runat="server" CssClass="formlabel" Text="Marital Status"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList_m_status" runat="server" Width="90%" RepeatDirection="Horizontal"
                    CssClass="formlabel" OnSelectedIndexChanged="RadioButtonList_m_status_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="Married" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Unmarried" Value="U" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvstatus" runat="server" Display="none" ControlToValidate="RadioButtonList_m_status"
                    ValidationGroup="1" ErrorMessage="Please Select Marriage Status"></asp:RequiredFieldValidator>
            </td>
            <td>
                <span id="spn" runat="server" visible="false" style="color: red">*</span><asp:Label
                    ID="lblspname" runat="server" Text="Spouse Name" CssClass="formlabel" Visible="false"></asp:Label>
            </td>
            <td style="width: 257px">
                <asp:TextBox ID="txtspouse" runat="server" Width="90%" Enabled="false" Visible="false"
                    MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtspouse" runat="server" Display="none" ControlToValidate="txtspouse"
                    ValidationGroup="1" ErrorMessage="Please Enter Spouse Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtspouse" runat="server" Display="None" ControlToValidate="txtspouse"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in Spouse Name"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr align="left">
            <td style="height: 43px">
                <span style="color: Red">*</span><asp:Label ID="Label29" runat="server" CssClass="formlabel"
                    Text="Date of Birth"></asp:Label>
            </td>
            <td style="width: 257px; height: 43px;">
                <asp:HiddenField ID="hidden_jid" runat="server" />
                <asp:TextBox ID="txt_DOB" runat="server" MaxLength="10" Width="50%" Enabled="false"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Animated="true"
                    TargetControlID="txt_DOB">
                </cc1:CalendarExtender>
                <%--  <img id="IMG5" alt="DatePicker" onclick="PopupPicker('txt_ex_t_date', 250, 250)"
                                src="Images/calendar.bmp" />--%>
                <asp:RequiredFieldValidator ID="rfvdob" runat="server" Display="none" ControlToValidate="txt_DOB"
                    ValidationGroup="1" ErrorMessage="Please Enter DOB."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REV_date" runat="server" ControlToValidate="txt_DOB"
                    Display="None" ErrorMessage="Enter Valid Date in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    ValidationGroup="1">
                </asp:RegularExpressionValidator>
            </td>
            <td align="left" style="height: 43px">
                <asp:Label ID="Label26" CssClass="formlabel" runat="server" Text="Nationality"></asp:Label>
            </td>
            <td style="height: 43px">
                <asp:DropDownList ID="DDL_Nationality" runat="server" Height="18px" Width="88px"
                    Enabled="false">
                    <asp:ListItem Text="Indian" Value="Indian"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
             <tr align="left">
            <td style="height: 43px">
                <asp:Label ID="lblAgeOn" runat="server" CssClass="formlabel" Text="AGE AS ON"></asp:Label>
                <asp:Label ID="lblEndDate" runat="server" CssClass="formlabel"></asp:Label>
            </td>

            <td>
                <asp:Label ID="lblCandidateAge" runat="server" CssClass="formlabel"></asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td>
                <span style="color: red">*</span><asp:Label ID="Label23" runat="server" CssClass="formlabel"
                    Text="Mobile No."></asp:Label>
            </td>
            <td style="width: 257px">
                <asp:TextBox ID="txt_mob" runat="server" Width="30%" MaxLength="10" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvmob" runat="server" Display="none" ControlToValidate="txt_mob"
                    ValidationGroup="1" ErrorMessage="Please Enter Mobile No."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revmob" runat="server" Display="None" ControlToValidate="txt_mob"
                    ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td align="left" style="height: 17px">
                <span style="color: red">*</span><asp:Label ID="Label24" CssClass="formlabel" runat="server"
                    Text="Email"></asp:Label>
            </td>
            <td style="height: 17px">
                <span style="color: red"></span>
                <asp:TextBox ID="txt_email" runat="server" Width="90%" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvmail" runat="server" Display="none" ControlToValidate="txt_email"
                    ValidationGroup="1" ErrorMessage="Please Enter EMail-Id"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revmail" runat="server" Display="None" ControlToValidate="txt_email"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Characters In EmailId"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table style="width: 100%; border-color: navy;" border="1" cellpadding="0" cellspacing="0"
                    align="left">
                    <tr>
                        <td>
                            <table align="left">
                                <tr align="left">
                                    <td style="width: 30%;">
                                        <span style="color: red">*</span><asp:Label ID="Label17" CssClass="formlabel" runat="server"
                                            Text="Present Address"></asp:Label>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:TextBox ID="txt_pre_add" runat="server" Width="250px" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvpradd" runat="server" Display="none" ControlToValidate="txt_pre_add"
                                            ValidationGroup="1" ErrorMessage="Please Enter Present Address"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REF_PreAdd" runat="server" ControlToValidate="txt_pre_add"
                                            Display="None" ErrorMessage="Invaild Characters in Present Address  or Address length is more than 100 Characters"
                                            ValidationExpression="^[0-9a-zA-Z-#(),./\s]{1,100}$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td>
                                        <span style="color: red">*</span><asp:Label ID="Label22" CssClass="formlabel" runat="server"
                                            Text="PIN Code"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_pre_pin" runat="server" MaxLength="6" Width="25%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvprepin" runat="server" Display="none" ControlToValidate="txt_pre_pin"
                                            ValidationGroup="1" ErrorMessage="Please Enter Pin"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revdept1" runat="server" Display="None" ControlToValidate="txt_pre_pin"
                                            ValidationExpression="^[0-9]*$" ErrorMessage="Invalid characters in Pin" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPIN" runat="server"
                                            ControlToValidate="txt_pre_pin" ValidationExpression=".{6}.*" ErrorMessage="Enter Minimum 6 Length in Pin"
                                            ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 4%">&nbsp;<%--<asp:Button ID="btn_click" runat="server" Height="20px" 
                OnClick="btn_click_Click" Text="Same" 
                CssClass="cssbutton" />--%>
                            <asp:Label ID="Label19" runat="server" Text="If Same, Check" CssClass="cssbutton"></asp:Label>
                            <asp:CheckBox ID="chkadd" runat="server" OnCheckedChanged="btn_click_Click" AutoPostBack="true" />
                        </td>
                        <td>
                            <table align="left">
                                <tr align="left">
                                    <td style="width: 30%">
                                        <span style="color: red">*</span><asp:Label ID="Label21" CssClass="formlabel" runat="server"
                                            Text="Permanent Address"></asp:Label>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:TextBox ID="txt_par_add" runat="server" Width="250px" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvperadd" runat="server" Display="none" ControlToValidate="txt_par_add"
                                            ValidationGroup="1" ErrorMessage="Please EnterPermanent Address"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REV_parmaAdd" runat="server" ControlToValidate="txt_par_add"
                                            Display="None" ErrorMessage="Invaild Characters in Permanent Address or Address length is more than 100 Characters"
                                            ValidationExpression="^[0-9a-zA-Z-#(),./\s]{1,100}$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td>
                                        <span style="color: red">*</span><asp:Label ID="Label44" CssClass="formlabel" runat="server"
                                            Text="PIN Code"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_per_pin" runat="server" MaxLength="6" Width="25%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvperpin" runat="server" Display="none" ControlToValidate="txt_per_pin"
                                            ValidationGroup="1" ErrorMessage="Please Enter Pin"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revperpin" runat="server" Display="None" ControlToValidate="txt_per_pin"
                                            ValidationExpression="^[0-9]*$" ErrorMessage="Invalid characters in Pin Code"
                                            ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPINPer" runat="server"
                                            ControlToValidate="txt_per_pin" ValidationExpression=".{6}.*" ErrorMessage="Enter Minimum 6 Length in Pin Code"
                                            ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="left">
            <td>
                <span style="color: Red">*</span><asp:Label ID="Label30" runat="server" CssClass="formlabel"
                    Text="Category"></asp:Label>
            </td>
            <td style="width: 257px" colspan="3">
                <asp:DropDownList ID="DropDownList_cat" runat="server" OnSelectedIndexChanged="DropDownList_cat_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvcat" runat="server" Display="none" ControlToValidate="DropDownList_cat"
                    ValidationGroup="1" InitialValue="-1" ErrorMessage="Please Select Category" Width="15px"></asp:RequiredFieldValidator>&nbsp;
                <asp:RadioButtonList ID="rbtobcregion" runat="server" RepeatDirection="Horizontal"
                    CssClass="formlabel" Visible="false" OnSelectedIndexChanged="rbtobcregion_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="D">OBC Delhi</asp:ListItem>
                    <asp:ListItem Value="O">OBC Outside Delhi</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvrbtobcregion" runat="server" Display="none" ControlToValidate="rbtobcregion"
                    ValidationGroup="1" InitialValue="-1" ErrorMessage="Please Select OBC Region"
                    Width="15px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="tr_obc_cert" runat="server" visible="false">
            <td style="width: 23%" align="left">
                <span style="color: red">*</span><asp:Label ID="lbl_issuing_no" runat="server" CssClass="formlabel"
                    Text="Certification No."></asp:Label>
            </td>
            <td style="width: 257px" align="left">
                <asp:TextBox ID="txtbox_noncreamylayer" runat="server" Width="90%" MaxLength="30"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revcreamy" runat="server" Display="none" ValidationExpression="^[a-zA-Z0-9-/]+$"
                    ErrorMessage="Invalid Intput in Certificate No." ControlToValidate="txtbox_noncreamylayer"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td align="left">
                <span style="color: red">*</span><asp:Label ID="lbl_issuing_date" runat="server"
                    CssClass="formlabel" Text="Certification Issuing Date(On or Before cutoff date)"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtbox_noncreamylayerDATE" runat="server" MaxLength="10" Width="50%"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revdat_ecreamy" runat="server" ControlToValidate="txtbox_noncreamylayerDATE"
                    Display="None" ErrorMessage="Certification Issuing Date should be in DD/MM/YYYY "
                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
                <%--<img id="IMG3" alt="DatePicker" onclick="PopupPicker('txtbox_noncreamylayerDATE', 250, 250)"
                                src="Images/calendar.bmp" />--%>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    Animated="true" TargetControlID="txtbox_noncreamylayerDATE">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr id="tr_obc_state" runat="server" visible="false">
            <td style="width: 23%" align="left">
                <span style="color: red">*</span><asp:Label ID="lbl_issuing_state" runat="server"
                    CssClass="formlabel" Text="Certificate issuing State"></asp:Label>
            </td>
            <td style="width: 257px" align="left">
                <asp:DropDownList ID="DropDownList_c_state" runat="server" Width="90%" OnSelectedIndexChanged="DropDownList_c_state_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td style="width: 23%" align="left">
                <span style="color: red">*</span><asp:Label ID="lbl_issue_auth" runat="server" CssClass="formlabel"
                    Text="Certificate issuing Authority"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txt_cert_issue_auth" runat="server" Width="90%">
                </asp:TextBox>
                <asp:RegularExpressionValidator ID="rev_cert_issue_auth" runat="server" Display="none"
                    ValidationExpression="^[a-zA-Z0-9.\s]+$" ErrorMessage="Invalid intput in Certificate issuing Authority"
                    ControlToValidate="txt_cert_issue_auth" ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="trobcf" runat="server" visible="false">
            <td colspan="4" align="left">Give the details of Father/Mother OBC Certificate<span style="color: Red">(Certificate
                    should be issued from Delhi,if not then choose OBC Outside Delhi)</span>
            </td>
        </tr>
        <tr id="trobcform" runat="server" visible="false">
            <td valign="top" align="left">
                <span style="color: Red">*</span><asp:Label ID="Label32" runat="server" CssClass="formlabel"
                    Text="Certificate Holder Father/Mother"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlOBCForM" runat="server" CssClass="gridfont">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="F">Father</asp:ListItem>
                    <asp:ListItem Value="M">Mother</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlOBCForM" runat="server" Display="None" ControlToValidate="ddlOBCForM"
                    ErrorMessage="Please select father/Mother" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="tr_obc_cert_f" runat="server" visible="false">
            <td style="width: 23%" align="left">
                <span style="color: red">*</span><asp:Label ID="lbl_issuing_no_f" runat="server"
                    CssClass="formlabel" Text="Certification No."></asp:Label>
            </td>
            <td style="width: 257px" align="left">
                <asp:TextBox ID="txtbox_noncreamylayer_f" runat="server" Width="90%" MaxLength="30"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtbox_noncreamylayer_f" runat="server" Display="none"
                    ValidationExpression="^[a-zA-Z0-9-/]+$" ErrorMessage="Invalid Intput in Certificate No. of father"
                    ControlToValidate="txtbox_noncreamylayer_f" ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td align="left">
                <span style="color: red">*</span><asp:Label ID="lbl_issuing_date_f" runat="server"
                    CssClass="formlabel" Text="Certification Issuing Date"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtbox_noncreamylayerDATE_f"
                    runat="server" MaxLength="10" Width="50%"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtbox_noncreamylayerDATE_f" runat="server"
                    ControlToValidate="txtbox_noncreamylayerDATE_f" Display="None" ErrorMessage="Certification Issuing Date of father should be in DD/MM/YYYY "
                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
                <%--<img id="IMG3" alt="DatePicker" onclick="PopupPicker('txtbox_noncreamylayerDATE', 250, 250)"
                                src="Images/calendar.bmp" />--%>
                <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    Animated="true" TargetControlID="txtbox_noncreamylayerDATE_f">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr id="tr_obc_state_f" runat="server" visible="false">
            <td style="width: 23%" align="left">
                <span style="color: red">*</span><asp:Label ID="lbl_issuing_state_f" runat="server"
                    CssClass="formlabel" Text="Certificate issuing State"></asp:Label>
            </td>
            <td style="width: 257px" align="left">
                <asp:TextBox ID="txt_c_state_f" runat="server" Width="90%" Text="Delhi" Enabled="false"></asp:TextBox>
            </td>
            <td style="width: 23%" align="left">
                <span style="color: red">*</span><asp:Label ID="lbl_issue_auth_f" runat="server"
                    CssClass="formlabel" Text="Certificate issuing Authority"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txt_cert_issue_auth_f" runat="server" Width="90%" MaxLength="50">
                </asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxt_cert_issue_auth_f" runat="server" Display="none"
                    ValidationExpression="^[a-zA-Z0-9.\s]+$" ErrorMessage="Invalid intput in Certificate issuing Authority"
                    ControlToValidate="txt_cert_issue_auth_f" ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
        </tr>
       
        <tr align="left">
            <td>
                <asp:Label ID="Label67" runat="server" CssClass="formlabel" Text="Sub Category(if any)"></asp:Label>
            </td>

            <td colspan="3">
                <asp:Label ID="lblalert" runat="server" Text="Please check your eligibility from detailed Advertisement before selecting the suitable sub category" style="color: red; font-weight:bold" ></asp:Label>
                <asp:CheckBoxList ID="CheckBoxList_Subcategory" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="CheckBoxList_Subcategory_SelectedIndexChanged" CssClass="formlabel"
                    Width="90%">
                </asp:CheckBoxList>
            </td>
        </tr>

        <tr id="tr_CESD" runat="server" visible="false" style="width: 100%">
            <td style="width: 25%; vertical-align: top; margin-top: 100px; color:Navy; font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 13px;">For Contractual Employee </td>
             <td style="width: 75%">
                 <table  style="width: 100%"><tr>

                     <td>
                         <span style="color: Red">*</span><asp:Label ID="Label33" runat="server" CssClass="formlabel" Text="Upload Certificate"></asp:Label><span style="color: red">(Maximum size of pdf 2MB)</span>
                            <asp:FileUpload ID="uploadcesdfile" runat="server" Width="250px" />
                            <asp:Button ID="btnfile" runat="server" Text="Upload"   CssClass="cssbutton" OnClick="btnfile_Click" />
                     </td>
                      <td>
                             <%--<asp:HyperLink ID="hypViewPHDoc" Text="Preview" runat="server"  Target="_blank"></asp:HyperLink><br /><br />--%>
                          
                            <asp:Button ID="btnview" runat="server" Text="Preview  Doc"  CssClass="cssbutton"  Enabled="false" OnClick="btnview_Click" />
                        </td>
                        </tr></table>
                            
                        </td>
                       
        </tr>




        <tr id="tr_ph" runat="server" visible="false" style="width: 100%">
            <td style="width: 23%; vertical-align: top; margin-top: 100px">
                <asp:Label ID="Label65" runat="server" Text="Select Category of Disability  " CssClass="formlabel"></asp:Label><span style="color: red">(any one)</span></td>
            <td style="width: 77%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; height: 18px;" align="left" colspan="2">
                            <asp:CheckBoxList ID="CheckboxList_PHSubCat" runat="server" RepeatDirection="Horizontal"
                                TabIndex="25" RepeatColumns="6" CssClass="formlabel" OnSelectedIndexChanged="CheckboxList_PHSubCat_SelectedIndexChanged"
                                meta:resourcekey="rbPhResource1" Width="100%" AutoPostBack="true">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%;">
                            <span style="color: Red">*</span>
                            <asp:Label ID="Label35" runat="server" CssClass="formlabel" Text="Certificate No."></asp:Label>
                            <asp:TextBox ID="txtCertificateNo" Width="50%"  runat="server" OnTextChanged="txtCertificateNo_TextChanged">
                            </asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="none"
                                ValidationExpression="^[a-zA-Z0-9-/]+$" ErrorMessage="Invalid Intput in Certificate No. of PH certificate"
                                ControlToValidate="txtCertificateNo" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 50%; height: 18px;" align="left">
                            <span style="color: Red">*</span><asp:Label ID="lblIssueCert" runat="server" CssClass="formlabel" Text="Issuing date"></asp:Label>
                            <asp:TextBox ID="txtIssuedate" MaxLength="10" runat="server" OnTextChanged="txtIssuedate_TextChanged">
                            </asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                Animated="true" TargetControlID="txtIssuedate">
                            </cc1:CalendarExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_ex_f_date"
                                Display="None" ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="none" ControlToValidate="txtIssuedate"
                                ValidationGroup="1" ErrorMessage="Please Enter Issuing Date"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60%;">
                            <span style="color: Red">*</span><asp:Label ID="lblAuthority" runat="server" CssClass="formlabel" Text="Issuing authority"></asp:Label>
                            <asp:TextBox ID="txtIssuingauthority" Width="30%" runat="server" OnTextChanged="txtIssuingauthority_TextChanged">
                            </asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="none"
                                ValidationExpression="^[a-zA-Z0-9.\s]+$" ErrorMessage="Invalid intput in Certificate issuing Authority"
                                ControlToValidate="txtIssuingauthority" ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <span style="color: Red">*</span><asp:Label ID="lblUpload" runat="server" CssClass="formlabel" Text="Upload Certificate"></asp:Label><span style="color: red">(Maximum size of pdf 2MB)</span>
                            <asp:FileUpload ID="PHCertUpload" runat="server" Width="250px" />
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"  CssClass="cssbutton" />
                        </td>
                        <td>
                             <%--<asp:HyperLink ID="hypViewPHDoc" Text="Preview" runat="server"  Target="_blank"></asp:HyperLink><br /><br />--%>
                            <span id="fileNameDisplay" runat="server" style="margin-bottom:5px;font-weight:500; font-size:large"></span>
                            <asp:Button ID="btnViewPHDoc" runat="server" Text="Preview PH Doc" OnClick="btnViewPHDoc_Click" CssClass="cssbutton"  Enabled="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr_vh" runat="server" visible="false" style="background-color: #C8DAFF">
            <td style="width: 23%; height: 40px;" align="left" valign="top">
                <asp:Label ID="Label20" runat="server" Text="Select Sub Category of VH Disability"
                    CssClass="formlabel"></asp:Label>
            </td>
            <td style="width: 67%" align="left">
                <asp:RadioButtonList runat="server" ID="radio_vh" CssClass="formlabel" RepeatDirection="Horizontal" />
            </td>
        </tr>
        <tr id="tr_hh" runat="server" visible="false" style="background-color: #C8DAFF">
            <td style="width: 23%; height: 40px;" align="left" valign="top">
                <asp:Label ID="Label25" runat="server" Text="Select Sub Category of HH Disability"
                    CssClass="formlabel"></asp:Label>
            </td>
            <td style="width: 67%" align="left">
                <asp:RadioButtonList runat="server" ID="radio_hh" CssClass="formlabel" RepeatDirection="Horizontal" />
            </td>
        </tr>
        <tr id="tr_oh" runat="server" visible="false" style="background-color: #C8DAFF">
            <td style="width: 23%; height: 40px;" align="left" valign="top">
                <asp:Label ID="Label31" runat="server" Text="Select Sub Category of OH Disability"
                    CssClass="formlabel"></asp:Label>
            </td>
            <td style="width: 67%" align="left">
                <asp:RadioButtonList runat="server" ID="radio_oh" CssClass="formlabel" RepeatDirection="Horizontal" />
            </td>
        </tr>
        <tr runat="server" visible="true" style="width: 100%; border-style: solid" id="tr_physic">
            <td align="left" style="width: 33%; height: 19px;">
                <asp:Label ID="lbl_physic" runat="server" CssClass="formlabel" Text="Physical Standards required for the Post"></asp:Label>
            </td>
            <td align="left" style="width: 66%; height: 19px;" colspan="3">
                <table border="1" class="gridtable" style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid">
                    <tr>
                        <td align="center" colspan="2">Height
                        </td>
                        <td align="center" colspan="3">Chest
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 201px" align="center">Lower Limit
                        </td>
                        <td style="width: 212px" align="center">Relaxation
                        </td>
                        <td style="width: 192px" align="center">Lower Limit
                        </td>
                        <td style="width: 168px" align="center">Upper Limit
                        </td>
                        <td style="width: 168px" align="center">Relaxation
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 201px" align="center">&nbsp;<asp:Label ID="lblHLLDFt" runat="server"></asp:Label>
                            <asp:Label ID="lblHLLDIn" runat="server"></asp:Label>
                            <asp:Label ID="lblHLLD" runat="server"></asp:Label>
                        </td>
                        <td align="center" style="width: 212px">&nbsp;
                            <asp:Label ID="lblHULDFt" runat="server"></asp:Label>
                            <asp:Label ID="lblHULDIn" runat="server"></asp:Label>
                            <asp:Label ID="lblHULD" runat="server"></asp:Label>
                        </td>
                        <td style="width: 192px" align="center">&nbsp;
                            <asp:Label ID="lblCLLDFt" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblCLLDIn" runat="server"></asp:Label>
                            <asp:Label ID="lblCLLD" runat="server"></asp:Label>
                        </td>
                        <td style="width: 168px" align="center" visible="False">&nbsp;
                            <asp:Label ID="lblCULDFt" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblCULDIn" runat="server"></asp:Label>
                            <asp:Label ID="lblCULD" runat="server"></asp:Label>
                        </td>
                        <td style="width: 168px" align="center" visible="true">&nbsp;
                            <asp:Label ID="lbl_cst_rex_in" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_cst_rex_cm" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5">Other
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5">
                            <asp:CheckBox ID="chkSoundD" runat="server" Enabled="False" Text="Sound health/free from defect/deformity/desease" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5">
                            <asp:CheckBox ID="chkVisionD" runat="server" Enabled="False" Text="Vision 6/6 without glasses both eyes" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5">
                            <asp:CheckBox ID="chkcolorblindD" runat="server" Enabled="False" Text="Free from colour blindness" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5">
                            <asp:CheckBox ID="chk_relax" runat="server" Enabled="true" Text="I am eligible for relaxation in the Physical Standards.(For Hilly Area)"
                                Font-Bold="True" ForeColor="#CC3300" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" visible="false" style="width: 100%; border-style: solid" id="tr_weight">
            <td align="left" style="width: 33%; height: 19px;" valign="top">
                <asp:Label ID="lbl_weight" runat="server" CssClass="formlabel" Text="Weight required for the Post"></asp:Label>
            </td>
            <td align="left" style="width: 66%; height: 19px;" colspan="3">
                <table border="1" class="gridtable" width="30%" style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid">
                    <tr>
                        <td id="td_1_male" runat="server" align="center">
                            <asp:Label ID="lbl_male" runat="server" Text="Male" CssClass="formlabel" />
                        </td>
                        <td id="td_1_female" runat="server" align="center">
                            <asp:Label ID="lbl_female" runat="server" Text="Female" CssClass="formlabel" />
                        </td>
                        <td id="td_1_transgender" runat="server" align="center">
                            <asp:Label ID="lbl_transgender" runat="server" Text="Transgender" CssClass="formlabel" />
                        </td>
                    </tr>
                    <tr>
                        <td id="td_2_male" runat="server" align="center">
                            <asp:Label ID="lbl_w_male" runat="server" Text="" CssClass="formlabel" />
                        </td>
                        <td id="td_2_female" runat="server" align="center">
                            <asp:Label ID="lbl_w_female" runat="server" Text="" CssClass="formlabel" />
                        </td>
                        <td id="td_2_transgender" runat="server" align="center">
                            <asp:Label ID="lbl_w_transgender" runat="server" Text="" CssClass="formlabel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr visible="true" style="width: 100%" id="tr_physic_accept" runat="server">
            <td align="left" style="height: 19px;" colspan="2">
                <asp:Label ID="lbl_note0" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                    Height="16px" Text="Physical Standard Acceptance : I certify that I fullfill above Physical Standards."></asp:Label>
            </td>
            <td align="left">
                <asp:RadioButtonList ID="RadioButtonList_physcic_accept" runat="server" AutoPostBack="True"
                    CssClass="ariallightgrey" OnSelectedIndexChanged="RadioButtonList_d_SelectedIndexChanged"
                    RepeatDirection="Horizontal" Width="22%" Height="16px">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="tr_contract" runat="server" visible="false" style="width: 100%">
            <td align="left" style="width: 33%; height: 19px;">
                <asp:Label ID="Label2" runat="server" CssClass="formlabel" Text="Duration of Contract Service"></asp:Label>
            </td>
            <td align="left" style="width: 66%; height: 19px;" colspan="3">
                <asp:TextBox ID="txt_cont" runat="server" Width="35px" MaxLength="4">
                </asp:TextBox>
                <asp:Label ID="Label3" runat="server" ForeColor="#C00000" CssClass="ariallightgrey"
                    Text="(In Days)"></asp:Label>
            </td>
        </tr>
        <tr id="tr_serv" runat="server" style="width: 100%" visible="false">
            <td align="left" style="width: 33%; height: 14px">
                <asp:Label ID="Label14" runat="server" CssClass="formlabel" Text="Length of Service"></asp:Label>
            </td>
            <td align="left" colspan="3" style="width: 66%; height: 14px">
                <asp:TextBox ID="txt_len_serv" runat="server" Width="34px"></asp:TextBox>
                <asp:Label ID="Label18" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                    Text="(In Days)"></asp:Label>
            </td>
        </tr>
        <tr id="tr_dgs" runat="server" visible="false">
            <td style="width: 23%;" align="left">
                <asp:Label ID="Label66" runat="server" CssClass="formlabel" Text="Joining date in Government Service"></asp:Label>
            </td>
            <td style="width: 257px">
                <asp:TextBox ID="txt_dob_dgs" runat="server" MaxLength="10" Width="50%"></asp:TextBox>
                <%--<img id="IMG2" alt="DatePicker" onclick="PopupPicker('txt_dob_dgs', 250, 250)"
                                src="Images/calendar.bmp" />--%>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    Animated="true" TargetControlID="txt_dob_dgs">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvdoj" runat="server" Display="none" ControlToValidate="txt_dob_dgs"
                    ValidationGroup="1" ErrorMessage="Please Enter DOJ."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfvdojo" runat="server" ControlToValidate="txt_dob_dgs"
                    Display="None" ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr align="left" runat="server" id="tr_ex_serv" visible="false">
            <td style="height: 54px">
                <asp:Label ID="Label37" runat="server" CssClass="formlabel" Text="Ex-Service Period"></asp:Label>
            </td>
            <td style="width: 257px; height: 54px;">&nbsp;
            <asp:Label ID="Label38" runat="server" CssClass="formlabel" Text="From Date"></asp:Label>
                <asp:TextBox ID="txt_ex_f_date" Width="50%" MaxLength="10" runat="server">
                </asp:TextBox>
                <%--     <img id="IMG4" alt="DatePicker" onclick="PopupPicker('txt_ex_f_date', 250, 250)"
                                src="Images/calendar.bmp" />--%>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    Animated="true" TargetControlID="txt_ex_f_date">
                </cc1:CalendarExtender>
                <asp:RegularExpressionValidator ID="revexdate" runat="server" ControlToValidate="txt_ex_f_date"
                    Display="None" ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvdateex" runat="server" Display="none" ControlToValidate="txt_ex_f_date"
                    ValidationGroup="1" ErrorMessage="Please Enter ExService From Date"></asp:RequiredFieldValidator>
            </td>
            <td align="left" runat="server" id="TDExservFromDate" style="height: 54px">&nbsp;<asp:Label ID="Label39" runat="server" CssClass="formlabel" Text="To Date"></asp:Label>
                <asp:TextBox ID="txt_ex_t_date" Width="117%" MaxLength="10" runat="server"></asp:TextBox>
                <%--  <img id="IMG5" alt="DatePicker" onclick="PopupPicker('txt_ex_t_date', 250, 250)"
                                src="Images/calendar.bmp" />--%>
                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    Animated="true" TargetControlID="txt_ex_t_date">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvtoex" runat="server" Display="none" ControlToValidate="txt_ex_t_date"
                    ValidationGroup="1" ErrorMessage="Please Enter ExService TO Date"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="revtoex" runat="server" ControlToValidate="txt_ex_t_date" Display="None"
                        ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td align="left" runat="server" id="TDExservToDate" style="height: 54px">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="Label34" runat="server" CssClass="formlabel" Text="Debarred by Board or any other agency"></asp:Label>
            </td>
            <td style="width: 257px">
                <asp:RadioButtonList ID="RadioButtonList_d" runat="server" Width="90%" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="RadioButtonList_d_SelectedIndexChanged" AutoPostBack="True"
                    CssClass="ariallightgrey">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvboard" runat="server" Display="none" ControlToValidate="RadioButtonList_d"
                    ValidationGroup="1" ErrorMessage="Please Select One from Debarred By Board"></asp:RequiredFieldValidator>
            </td>
            <td align="left" runat="server" id="TDDebbaredDateorder">
                <asp:Label ID="Label40" runat="server" CssClass="formlabel" Text="Debarred Upto Date"></asp:Label>
                <asp:TextBox ID="txt_d_date" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    Animated="true" TargetControlID="txt_d_date">
                </cc1:CalendarExtender>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_d_date"
                    Display="None" ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            <td align="left" style="height: 21px" runat="server" id="TDDebbaredYear" visible="false">
                <asp:Label ID="Label41" runat="server" CssClass="formlabel" Text="No. of Year"></asp:Label>
                <asp:TextBox ID="txt_d_year" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_scrb_accept" runat="server" visible="false">
            <td align="left" style="height: 19px;">
                <asp:Label ID="lbl_note1" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                    Height="16px" Text="Do you need the facility of scribe?"></asp:Label>
            </td>
            <td align="left" style="width: 257px">
                <asp:RadioButtonList ID="RadioButtonList_scrb_accept" runat="server" Width="90%"
                    CssClass="ariallightgrey" RepeatDirection="Horizontal" Height="16px">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <%--<tr>
        <td align="left" colspan="3">
            <asp:Label ID="lbl_note" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                Text="Undertaking : I certify that the above details are correct and if wrong, I shall be liable for necessary action by DSSSB."
                Width="100%"></asp:Label>
        </td>
        <td align="left">
            
        </td>
    </tr>--%>
        <tr cssclass="ariallightgrey" style="color: #C00000">
            <td colspan="4" align="left">
                <asp:CheckBox ID="chk_decl" runat="server" CssClass="formlabel" />
                DECLARATION :<br />
                a) I hereby certify that all statement made in this application are true, complete
            and correct to the best of my knowledge and belief and have been filled by me.<br />
                b) I also declare that I have submitted only one application for one post code in
            response to the advertisement.<br />
                c) I have read all the provisions mentioned in the advertisement/notice of examination
            carefully as published on the website of DSSSB and I hereby under take to abide
            by them.<br />
                d) I understand that in the event of information being found false or incorrect
            at any stage prescribed in the notice or any ineligibility being detected before
            or after the examination, my candidature/selection/appointment is liable to be cancelled/terminated
            automatically without any notice to me and action can be taken against me by the
            DSSSB.<br />
                e) The information submitted herein shall be treated as final in respect of my candidature
            for the post applied for through this application form.<br />
                f) I also declare that I have informed my Head of office/Department in writing that
            I am applying for this post/examination (for Government Employees only).
            <br />
                g) I <asp:Label ID="Lbl_name" runat="server"></asp:Label> S/O /  D/O /  W/O <asp:Label ID="Lbl_guardian" runat="server"></asp:Label> do hereby undertake that I will check https://dsssbonline.nic.in called as OARS Portal in a regular reasonable interval to update myself about DSSSB guidelines and examinations.
The onus of checking https://dsssbonline.nic.in in on me is being applicant of posts in DSSSB. I also understand that the mail/SMS facility provided to the candidate (s) is an additional facility provided by the DSSSB in addition to OARS portal
            </td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" style="width: 80%">
                <asp:Button ID="btn_insert" runat="server" OnClick="btn_insert_Click" Font-Size="14px"
                    Text="SAVE and NEXT" ValidationGroup="1" CssClass="cssbutton" />
                <asp:Button ID="btn_update" runat="server" Text="Update and Next" ValidationGroup="1" OnClick="btn_update_Click"
                    CssClass="cssbutton" />
            </td>
            <td align="left">
                <asp:Label ID="lbl_step" runat="server" Text="Step 1/5" ForeColor="darkGreen" Font-Bold="true" Font-Italic="True"
                    Font-Size="Large">
                </asp:Label>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
        ValidationGroup="1" />
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    <asp:TextBox ID="txt_endson" Visible="false" runat="server">
    
    </asp:TextBox>
    <asp:HiddenField ID="hfjid" runat="server" />
</asp:Content>
