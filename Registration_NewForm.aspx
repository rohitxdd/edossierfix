<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration_NewForm.aspx.cs"
    Inherits="Registration_NewForm" %>

<%@ Register Src="~/usercontrols/Header.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/Footer.ascx" TagName="WebUserControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="./CSS/Applicant.css" />
    <script src="JS/jquery.min.js" type="text/javascript"></script>
    <script src="JS/validate-aadhar.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="Jscript/JScript.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.17.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="Jscript/md5.js"></script>
    <link rel="stylesheet" type="text/css" href="CSS/jquery-ui.css" />
    <style>
        *
        {
            box-sizing: border-box;
        }
        
        .zoom
        {
            /* padding: 50px;
            background-color: green;*/
            transition: transform .2s;
            width: 200px;
            height: 200px;
            margin: 0 auto;
        }
        
        .zoom:hover
        {
            -ms-transform: scale(5); /* IE 9 */
            -webkit-transform: scale(5); /* Safari 3-8 */
            transform: scale(5);
        }
    </style>
    <style>
        .divIDProofDoc
        {
            color: #003366;
        }
    </style>
    <script>
        function checkAddress() {
            debugger;
            var textBox = $.trim($('input[type=text]').val())
            if (textBox == "") {
                CheckBoxdisclaimer.checked = false;
                chkPreview.checked = false;
                alert("Complete your form before preview.");
            }
            else if (!CheckBoxdisclaimer.checked) {
                alert("Check to agree with the UNDERTAKING before preview.");
            }
            else {
                window.print();
            }
        }
    </script>
 <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8);
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("lblAadharVal").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>  
    <script type="text/javascript">
        //preview fileupload control selected image
        function ShowImagePreview(input) {
            debugger;
            if (input.files && input.files[0]) {
                var ImageDir = new FileReader();
                ImageDir.onload = function (e) {
                    $('#imgPicture').attr('src', e.target.result);
                }
                ImageDir.readAsDataURL(input.files[0]);
            }
        }  
    </script>

    <script>
        history.pushState(null, document.title, location.href);
        window.addEventListener('popstate', function (event) {
            history.pushState(null, document.title, location.href);
        });
    </script>
    <script language="javascript" type="text/javascript">
        function checkJavaScriptValidity() {
            document.getElementById("jsEnabled").style.visibility = 'hidden';
            document.getElementById("jsDisabled").style.visibility = 'hidden';

            document.getElementById('<%= btnrsubmit.ClientID %>').disabled = false;
        } 
    </script>
    <script language="javascript" type="text/javascript">
        function SignValidate() {
            debugger;
            if (!Page_ClientValidate()) {
                CheckBoxdisclaimer.checked = false;
                chkPreview.checked = false;
                return false;
            }
            var npwd = document.getElementById('<%= txtpassword.ClientID %>').value;
            var npwdhash = MD5(npwd);
            document.getElementById('<%= txtpassword.ClientID %>').value = npwdhash;
            var npwd = document.getElementById('<%= txt_re_password.ClientID %>').value;
            var re_npwdhash = MD5(npwd);
            document.getElementById('<%= txt_re_password.ClientID %>').value = re_npwdhash;
        }    
    </script>
    <script type="text/javascript" language="javascript">
        function PassValidate() {

            var exppwd = /[\^|\*|\@|\~|\!|\#|\(|\)|\{|\}|=|\[|\]|\:|\;|\.|\'|\?|\/|\$|\_|\,|\-|\+|\%]+/;
            var expMassage = '(\",%,;,:,~,!,#,^,{,},_,-,(,),*\)';
            var flag = false;


            var TxtPassStr = document.getElementById('<%=txtpassword.ClientID%>').value
            if (TxtPassStr.length < 8) {
                alert("Password should be at least 8 characters long.\nPlease re-enter the Password")
                document.getElementById('<%=txtpassword.ClientID%>').value = "";

                return false;
            }
            if (TxtPassStr != "") {
                if (exppwd.test(TxtPassStr) == false) {
                    alert("Atleast one of the Special characters like\n" + expMassage + " \nmust be in the New Password.");
                    document.getElementById('<%=txtpassword.ClientID%>').value = "";
                    return false;
                }

                var re = /[0-9]/;
                if (re.test(TxtPassStr) == false) {
                    alert("Error: password must contain at least one number (0-9)!");
                    document.getElementById('<%=txtpassword.ClientID%>').value = "";

                    return false;
                }

                re = /[a-z]/;
                if (re.test(TxtPassStr) == false) {
                    alert("Error: password must contain at least one lowercase letter (a-z)!");
                    document.getElementById('<%=txtpassword.ClientID%>').value = "";

                    return false;
                }

                re = /[A-Z]/;
                if (re.test(TxtPassStr) == false) {
                    alert("Error: password must contain at least one uppercase letter (A-Z)!");
                    document.getElementById('<%=txtpassword.ClientID%>').value = "";

                    return false;
                }

            }

        }
    </script>
    <title>DSSSBOnline</title>
    <style type="text/css">
        .style2
        {
            width: 268435408px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="OFF">
    <div id="dialog" title="Dialog Title" runat="server">
        <table width="100%" align="center" class="border_gray">
            <tr>
                <td>
                    <uc1:WebUserControl ID="Top1" runat="server" />
                </td>
            </tr>
            <tr id="trnewreg" runat="server" visible="true">
                <td align="center">
                    <table width="950" class="border_gray">
                        <tr>
                            <td align="center" colspan="4">
                                <span class="formlabel" style="font-weight: bold; font-size: initial;" font-bold="True">
                                    For New Registration on OARS Portal
                                    <br />
                                    (only for those applicants who have not registered on OARS portal earlier)</span>
                                <asp:Label ID="Label7" runat="server" CssClass="formlabel" Text=""></asp:Label>
                                <%--</td>
                            <td align="right" colspan="1">--%>
                                <asp:HyperLink ID="HyperLink1" Style="float: right;" runat="server" CssClass="formlabel"
                                    NavigateUrl="~/Default.aspx">Back</asp:HyperLink>
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="center" colspan="6">
                                <asp:Label ID="Label26" runat="server" CssClass="formlabel" Text="(Information mention in bold letters is newly introduced in registration form)"
                                    Font-Bold="True"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr class="formlabel">
                            <td align="left" colspan="4">
                                <u>
                                    <asp:Label ID="Label3" runat="server" Text="Instructions for candidates:" Style="color: red;
                                        font-weight: bold; font-size: larger;"></asp:Label></u>
                                <br />
                                <span style="color: red">1 :</span>The fields with <span style="color: red">*</span>
                                mark are mandatory.&nbsp;<br />
                                <span style="color: red">2 :</span><asp:Label ID="Label18" runat="server" CssClass="formlabel"
                                    Text="In case, Roll No. of Class X is in Alphanumeric then use only numeric characters of the Roll No. For example, if your Roll No. is 12CSC0204, then use 120204. Further Please do not enter/ prefix zero in class X roll no as system will truncate all leading zeros from left automatically. Eg. if roll number is 00123456012 than use 12345012 only as class X roll number"></asp:Label>
                                <br />
                                <span style="color: red">3 :</span><asp:Label ID="Label6" runat="server" CssClass="formlabel"
                                    Text="Candidate can apply for various posts only after registration. After registration, candidate is required to quote his/her registration number as login ID and password for further accessing the online system."></asp:Label>
                                <br />
                                <span style="color: red">4 :</span><asp:Label ID="Label14" runat="server" CssClass="formlabel"
                                    Text="The Id proof that is entered by the applicant will be cross checked at the time of examination and submission of e-dossier, if shortlisted."></asp:Label>
                            </td>
                        </tr>
                        <tr class="formlabel">
                            <td align="left" colspan="4">
                                <span style="color: red">Note :</span><asp:Label ID="lblNote" runat="server" CssClass="formlabel"
                                    Text="Applicants are requested to fill the registration form carefully as details on Registration form once finally submitted will be treated as final and no changes will be allowed."></asp:Label>
                            </td>
                        </tr>
                        <tr css="tr">
                            <td colspan="4" class="tr" align="center">
                                Registration Details
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label13" runat="server" CssClass="Label13"
                                    Text="Name of Applicant"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txt_name" runat="server" Width="35%" MaxLength="50" TabIndex="1"></asp:TextBox>
                                <asp:Label ID="Label20" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                    Text="(Do not prefix Mr/Mrs/Km/Sh/Smt/Dr/Prof etc)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="rfvname" runat="server" Display="Dynamic" ControlToValidate="txt_name"
                                    ValidationGroup="1" ErrorMessage="Please Enter Name"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revnm" runat="server" ControlToValidate="txt_name"
                                    Display="Dynamic" ValidationExpression="[a-zA-Z ]{1,50}$" ErrorMessage="Invalid characters in Name"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label21" runat="server" CssClass="Label13"
                                    Text="Re-enter Name of Applicant"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtReName" runat="server" Width="35%" MaxLength="50" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="txtReName" ValidationGroup="1" ErrorMessage="Please Re-enter Name"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtReName"
                                    ValidationExpression="[a-zA-Z ]{1,50}$" ErrorMessage="Invalid characters in Name."
                                    Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator2" ControlToCompare="txt_name" ValueToCompare="txtReName"
                                    Display="Dynamic" ControlToValidate="txtReName" runat="server" ErrorMessage="The value is not same as above name field.">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red; vertical-align: top">*</span><asp:Label ID="Label29" runat="server"
                                    CssClass="Label13" Text="DOB(As per Certificate of Class X)"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txt_DOB" runat="server" MaxLength="10" Width="35%" AutoPostBack="True"
                                    OnTextChanged="txt_DOB_TextChanged" TabIndex="3"></asp:TextBox>(dd/MM/yyyy)
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="rfvdob" runat="server" ControlToValidate="txt_DOB"
                                    Display="Dynamic" ValidationGroup="1" ErrorMessage="Please Enter DOB.">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPIN" runat="server"
                                    ControlToValidate="txt_DOB" ValidationExpression=".{10}.*" ErrorMessage="Enter Valid DOB(DD/MM/YYYY)"
                                    ValidationGroup="1" Display="Dynamic"> 
                                </asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="REV_date" runat="server" ControlToValidate="txt_DOB"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date of Birth." ValidationExpression="(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d"
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red; vertical-align: top">*</span><asp:Label ID="Label2" runat="server"
                                    CssClass="Label13" Text="Re-enter DOB"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtReDOB" runat="server" MaxLength="10" Width="35%" OnTextChanged="txt_DOB_TextChanged"
                                    TabIndex="4"></asp:TextBox>(dd/MM/yyyy)
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                            </td>
                            <td colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtReDOB"
                                    ValidationGroup="1" ErrorMessage="Please Re-Enter DOB." Display="Dynamic">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                    ControlToValidate="txtReDOB" ValidationExpression=".{10}.*" ErrorMessage="Enter Valid DOB(DD/MM/YYYY)"
                                    ValidationGroup="1" Display="Dynamic"> 
                                </asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                    ControlToValidate="txtReDOB" Display="Dynamic" ErrorMessage="Enter Valid Re-enter DOB."
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d"
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txt_DOB" ValueToCompare="txtReDOB"
                                    Display="Dynamic" ControlToValidate="txtReDOB" runat="server" ErrorMessage="The value is not same as above DOB field">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td align="left" colspan="2">
                                <span style="color: #ff0000">*</span><asp:Label ID="Label8" runat="server" Text="Enter Roll No. of Class X"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txt_roll_no" runat="server" Width="35%" ToolTip="Please do not enter or prefix zero in class X roll no as system will truncate all leading zero from left automatically"
                                    TabIndex="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="rfv_roll_no" runat="server" ControlToValidate="txt_roll_no"
                                    Display="Dynamic" ErrorMessage="Enter Roll No. of Class X" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rev_rollno" runat="server" ControlToValidate="txt_roll_no"
                                    Display="Dynamic" ErrorMessage="The roll number can be numeric only." ValidationExpression="[0-9]*$"
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="revroll_limit" runat="server" ControlToValidate="txt_roll_no"
                                    ValidationExpression=".{1,15}.*" ErrorMessage="Minimum 3 digit and Maximum 15 digit are allowed in Roll No"
                                    Display="Dynamic" ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:Label ID="lblRollNoValid" runat="server" Visible="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td align="left" colspan="2">
                                <span style="color: #ff0000">*</span><asp:Label ID="Label4" runat="server" Text="Re-enter Roll No. of Class X"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtReEntRollNo" runat="server" Width="35%" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtReEntRollNo"
                                    Display="Dynamic" ErrorMessage="Re-enter Roll No. of Class X" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                    ControlToValidate="txtReEntRollNo" Display="Dynamic" ErrorMessage="Re-enter roll number can be numeric only"
                                    ValidationExpression="^[0-9]*$" ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                    ControlToValidate="txtReEntRollNo" ValidationExpression=".{1,15}.*" ErrorMessage="Minimum 3 digit and Maximum 15 digit are allowed in Roll No"
                                    Display="Dynamic" ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator4" ControlToCompare="txt_roll_no" ValueToCompare="txtReEntRollNo"
                                    Display="Dynamic" ControlToValidate="txtReEntRollNo" runat="server" ErrorMessage="The value is not same as above roll number.">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: #ff0000">*</span><asp:Label ID="Label9" runat="server" Text="Select year of passing of Class X"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddl_pass_year" Width="35%" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_pass_year_SelectedIndexChanged" TabIndex="7">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="rfv_passyear" runat="server" ControlToValidate="ddl_pass_year"
                                    Display="Dynamic" ErrorMessage="Select year of passing of Class X" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: #ff0000">*</span><asp:Label ID="Label33" runat="server" Text="Re-select year of passing of Class X"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddl_reenter_year" runat="server" Width="35%" TabIndex="8">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_reenter_year"
                                    Display="Dynamic" ErrorMessage="Re-select year of passing of Class X" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator5" ControlToCompare="ddl_pass_year" ValueToCompare="ddl_reenter_year"
                                    Display="Dynamic" ControlToValidate="ddl_reenter_year" runat="server" ErrorMessage="The value is not same as above calss X passing year.">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label27" runat="server" CssClass="Label13"
                                    Text="Select Gender"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="RadioButtonList_mf" runat="server" Width="35%" TabIndex="9">
                                    <asp:ListItem Text="--Select--" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Transgender" Value="T"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RadioButton
                                List ID="" runat="server" RepeatDirection="Horizontal"
                                    Height="16px" CssClass="ariallightgrey">
                                    
                                </asp:RadioButtonList>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="RadioButtonList_mf"
                                    Display="Dynamic" ErrorMessage="Select Gender" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label34" runat="server" CssClass="Label13"
                                    Text="Re-select Gender"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="rbtReSelectGend" runat="server" Width="35%" TabIndex="10">
                                    <asp:ListItem Text="--Select--" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Transgender" Value="T"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rbtReSelectGend"
                                    Display="Dynamic" ErrorMessage="Re-select Gender"></asp:RequiredFieldValidator>
                                <%--  <asp:CompareValidator ID="CompareValidator6" ControlToCompare="RadioButtonList_mf"
                                    ValueToCompare="rbtReSelectGend" Display="Dynamic" ControlToValidate="rbtReSelectGend"
                                    runat="server" ErrorMessage="The value is not same as selected gender.">
                                </asp:CompareValidator>--%>
                                <asp:Label ID="lblGender" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label35" runat="server" CssClass="Label13"
                                    Text="Nationality"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="DDL_Nationality" runat="server" Height="18px" Width="35%" TabIndex="11">
                                    <asp:ListItem Text="Indian" Value="Indian"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DDL_Nationality"
                                    ValidationGroup="1" ErrorMessage="Select Nationality"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                &nbsp;
                                <asp:Label ID="Label15" runat="server" Text="Father's Name" CssClass="Label13"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txt_fh_name" runat="server" Width="35%" MaxLength="50" TabIndex="12"></asp:TextBox>
                                <asp:Label ID="Label37" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                    Text="(Do not prefix Mr/Mrs/Km/Sh/Smt/Dr/Prof etc)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RegularExpressionValidator ID="revfname" runat="server" ControlToValidate="txt_fh_name"
                                    Display="Dynamic" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Only alphabate characters allowed."
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                &nbsp;
                                <asp:Label ID="Label36" runat="server" Text="Re-Enter Father's Name" CssClass="Label13"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtReFname" runat="server" Width="35%" MaxLength="50" TabIndex="13"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtReFname"
                                    ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Only alphabate characters allowed."
                                    Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator7" ControlToCompare="txt_fh_name" ValueToCompare="txtReFname"
                                    Display="Dynamic" ControlToValidate="txtReFname" runat="server" ErrorMessage="The value is not same as father's name.">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                &nbsp;
                                <asp:Label ID="Label16" runat="server" Text="Mother's Name" CssClass="Label13"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txt_mothername" runat="server" Width="35%" MaxLength="50" TabIndex="14"></asp:TextBox>
                                <asp:Label ID="Label22" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                    Text="(Do not prefix Mr/Mrs/Km/Sh/Smt/Dr/Prof etc)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_mothername"
                                    ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Only alphabate characters allowed."
                                    Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                &nbsp;
                                <asp:Label ID="Label38" runat="server" Text="Re-enter Mother's Name" CssClass="Label13"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtReMName" runat="server" Width="35%" MaxLength="50" TabIndex="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtReMName"
                                    ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Only alphabate characters allowed."
                                    Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator8" ControlToCompare="txt_mothername" ValueToCompare="txtReMName"
                                    Display="Dynamic" ControlToValidate="txtReMName" runat="server" ErrorMessage="The value is not same as mother's name.">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                &nbsp;
                                <asp:Label ID="Label19" runat="server" Text="Spouse Name" CssClass="Label13"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtspouse" runat="server" Width="35%" MaxLength="50" TabIndex="16"></asp:TextBox>
                                <asp:Label ID="Label40" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                    Text="(Do not prefix Mr/Mrs/Km/Sh/Smt/Dr/Prof etc)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RegularExpressionValidator ID="revtxtspouse" runat="server" ControlToValidate="txtspouse"
                                    ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Only alphabate characters allowed."
                                    ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                &nbsp;
                                <asp:Label ID="Label39" runat="server" Text="Re-enter Spouse Name" CssClass="Label13"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtRSpouse" runat="server" Width="35%" MaxLength="50" TabIndex="17"></asp:TextBox>
                                <asp:Label ID="Label41" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                    Text="(Do not prefix Mr/Mrs/Km/Sh/Smt/Dr/Prof etc)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtRSpouse"
                                    ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Only alphabate characters allowed."
                                    Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator9" ControlToCompare="txtspouse" ValueToCompare="txtRSpouse"
                                    Display="Dynamic" ControlToValidate="txtRSpouse" runat="server" ErrorMessage="The value is not same as spouse name.">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span>
                                <asp:Label ID="Label23" runat="server" CssClass="Label13" Text="Mobile No."></asp:Label>
                                <br />
                                <asp:Label ID="Labelmsg" runat="server" ForeColor="#C00000" Text="(10 Digits No without any 0,91 etc.)"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txt_mob" runat="server" Width="35%" MaxLength="10" AutoPostBack="true"
                                    OnTextChanged="txt_mob_TextChanged" TabIndex="18"></asp:TextBox>
                                <asp:Label ID="Label42" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                    Text="(If mobile no. already exists in OARS then registration will not be done.)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblMobEnter" runat="server" Visible="false" Style="color: red"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvmob" runat="server" ControlToValidate="txt_mob"
                                    Display="Dynamic" ValidationGroup="1" ErrorMessage="Please Enter Mobile No."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revmob" runat="server" Display="Dynamic" ControlToValidate="txt_mob"
                                    ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="revnum" runat="server" ControlToValidate="txt_mob"
                                    ValidationExpression=".{10}.*" ErrorMessage="Maximum 10 digit numbers are allowed."
                                    Display="Dynamic" ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span>
                                <asp:Label ID="Label43" runat="server" CssClass="Label13" Text="Re-enter Mobile No."></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtReMobNo" runat="server" Width="35%" MaxLength="10" TabIndex="19"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblReMob" runat="server" Visible="false" Style="color: red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtReMobNo"
                                    Display="Dynamic" ValidationGroup="1" ErrorMessage="Please Enter Mobile No."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                                    ControlToValidate="txtReMobNo" ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtReMobNo"
                                    ValidationExpression=".{10}.*" ErrorMessage="Maximum 10 digit numbers are allowed."
                                    Display="Dynamic" ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator10" ControlToCompare="txt_mob" ValueToCompare="txtReMobNo"
                                    Display="Dynamic" ControlToValidate="txtReMobNo" runat="server" ErrorMessage="The value is not same as mobile number.">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label24" CssClass="Label13" runat="server"
                                    Text="Email"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txt_email" runat="server" style="width:35%;margin-top: 8px;" AutoPostBack="true" OnTextChanged="txt_email_TextChanged"
                                    TabIndex="20"></asp:TextBox>
                                <asp:Label ID="Label44" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                    Text="(If email already exists in OARS, then registration will not be done.)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblemail" runat="server" Visible="false" Style="color: red"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvmail" runat="server" ControlToValidate="txt_email"
                                    Display="Dynamic" ValidationGroup="1" ErrorMessage="Enter EMail-Id"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revmail" runat="server" ControlToValidate="txt_email"
                                    Display="Dynamic" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    ErrorMessage="Invalid characters in email-id" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label45" CssClass="Label13" runat="server"
                                    Text="Re-enter Email"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtReEntEmail" runat="server" Width="35%" TabIndex="21"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblReemail" runat="server" Visible="false" Style="color: red"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReEntEmail"
                                    Display="Dynamic" ValidationGroup="1" ErrorMessage="Re-Enter EMail-Id"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtReEntEmail"
                                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    ErrorMessage="Invalid characters in email-id" ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator11" ControlToCompare="txt_email" ValueToCompare="txtReEntEmail"
                                    Display="Dynamic" ControlToValidate="txtReEntEmail" runat="server" ErrorMessage="The value is not same as email.">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <div id="validateOTP" runat="server">
                            <tr align="left" class="darkblue" style="background-color: #fdff70;">
                                <td colspan="2">
                                    <asp:Label ID="Label53" runat="server" CssClass="divIDProofDoc" Text="Enter OTP"></asp:Label>
                                    <%--<asp:Label ID="Label54" runat="server" ForeColor="#C00000" Text="(Verify your mobile)"></asp:Label>--%>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtMOTP" runat="server" Width="35%" MaxLength="10" placeholder="Enter Mobile OTP"
                                        ToolTip="Enter Mobile OTP" TextMode="Password" TabIndex="22"></asp:TextBox>
                                    <asp:TextBox ID="txtEOTP" runat="server" Width="35%" MaxLength="10" placeholder="Enter Email OTP"
                                        ToolTip="Enter Email OTP" TextMode="Password" TabIndex="23"></asp:TextBox>
                                    <%--</td>
                                <td colspan="2">--%><br />
                                    <asp:Label ID="Label56" runat="server" ForeColor="#C00000" Text="(separate OTP will be received in Email and Mobile no both) "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btnOTP" runat="server" CssClass="cssbutton" Text="Get OTP" Visible="true"
                                        Style="height: 22px; width: 85px; border-radius: 10px;" OnClick="btnOTP_Click" />
                                    <asp:Button ID="btnVerifyOTP" runat="server" CssClass="cssbutton" Visible="false"
                                        Text="Verify OTP" Style="height: 22px; width: 85px; border-radius: 10px;" OnClick="btnVerifyOTP_Click" />
                                </td>
                            </tr>
                            <asp:HiddenField ID="hdnfMOTP" runat="server" />
                            <asp:HiddenField ID="hdnfEOTP" runat="server" />
                            <asp:HiddenField ID="OTPVerified" runat="server" />
                            <asp:HiddenField ID="hdnfOTPVerifiedMEB" runat="server" />
                        </div>
                        <tr css="tr">
                            <td colspan="4" class="tr" align="center">
                                Identity Proof Details
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td align="center" colspan="4">
                                <asp:Label ID="Label31" runat="server" class="Label13" Style="color: #C00000;" Text="For ensuring the genuineness of photo identity card which needs to be submitted by the candidates."
                                    Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="lblAadhaarMsg" align="left" class="darkblue">
                            <td colspan="4">
                                <span style="color: red">Note :</span><asp:Label ID="lblAadhaarMsg1" runat="server"
                                    CssClass="Label13" ForeColor="Navy" Text="In case Aadhaar number is provided, then no scanned ID proof is required to be uploaded."></asp:Label>
                            </td>
                            <%--<td colspan="4">
                                <asp:Label ID="lblAadhaarMsg" runat="server" ForeColor="#C00000" CssClass="Label13"
                                    Text="Note: "></asp:Label>
                            </td>--%>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label25" runat="server" CssClass="Label13"
                                    Text="Do you want to give Aadhaar as an ID proof"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rbtHaveAdhar" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rbtHaveAdhar_SelectedIndexChanged"
                                    Style="height: 26px">
                                    <asp:ListItem Text="Yes" Value="Yes" Selected="True" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <div id="divAadhaar" runat="server" visible="true">
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                    <span style="color: red">*</span><asp:Label ID="Label28" runat="server" CssClass="Label13"
                                        Text="Enter Aadhaar No."></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtAdharNo" runat="server" Width="35%" MaxLength="14" AutoPostBack="true" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        OnTextChanged="txtAdharNo_TextChanged" TabIndex="24"></asp:TextBox><br />
                                    <asp:Label ID="lblAadharVal" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td align="left" colspan="2">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                                        Display="Dynamic" ControlToValidate="txtAdharNo" ValidationExpression="[ 0-9]*$"
                                        ErrorMessage="Only number is allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                    <span style="color: red">*</span>
                                    <asp:Label ID="Label30" runat="server" CssClass="Label13" Text="Re-enter Aadhaar No."></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtReAdharNo" runat="server" Width="35%" MaxLength="14" TabIndex="25" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        OnTextChanged="txtReAdharNo_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td align="left" colspan="2">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                                        Display="Dynamic" ControlToValidate="txtReAdharNo" ValidationExpression="[ 0-9]*$"
                                        ErrorMessage="Only number is allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:CompareValidator ID="CompareValidator3" ControlToCompare="txtAdharNo" ValueToCompare="txtReAdharNo"
                                        Display="Dynamic" ControlToValidate="txtReAdharNo" runat="server" ErrorMessage="The Aadhaar re-enter field is not the same as Aadhaar.">
                                    </asp:CompareValidator>
                                </td>
                            </tr>
                            <tr align="left" class="darkblue" visible="false">
                                <td colspan="2">
                                    <span style="color: red">*</span><asp:Label ID="lblAdN" runat="server" CssClass="Label13"
                                        Text="Enter Name as on Aadhaar"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtNameAdhar" runat="server" Width="35%" TabIndex="26"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td align="left" colspan="2">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                        Display="Dynamic" ControlToValidate="txtNameAdhar" ValidationExpression="[a-zA-Z ]*$"
                                        ErrorMessage="Only alphabate characters allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr align="left" class="darkblue" visible="false">
                                <td colspan="2">
                                    <span style="color: red">*</span><asp:Label ID="lblReNad" runat="server" CssClass="Label13"
                                        Text="Re-enter Name as on Aadhaar"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtReNameAdhar" runat="server" Width="35%" TabIndex="27"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td align="left" colspan="2">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                                        Display="Dynamic" ControlToValidate="txtReNameAdhar" ValidationExpression="[a-zA-Z ]*$"
                                        ErrorMessage="Only alphabate characters allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:CompareValidator ID="CompareValidator12" ControlToCompare="txtNameAdhar" ValueToCompare="txtReNameAdhar"
                                        Display="Dynamic" ControlToValidate="txtReNameAdhar" runat="server" ErrorMessage="The name not the same as above.">
                                    </asp:CompareValidator>
                                </td>
                            </tr>
                        </div>
                        <div id="divIDProofDoc" runat="server" visible="true">
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                    <span style="color: red; vertical-align: top">*</span><asp:Label ID="Label32" runat="server"
                                        CssClass="divIDProofDoc" Text="Select type of ID proof"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlIdDoc" Width="35%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdDoc_SelectedIndexChanged"
                                        TabIndex="28">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <div runat="server" id="divtxtIdNum">
                                <tr align="left" class="darkblue">
                                    <td colspan="2">
                                        <span style="color: red">*</span><asp:Label ID="Label49" runat="server" CssClass="divIDProofDoc"
                                            Text="Enter ID Proof No."></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtIdNumber" runat="server" Width="35%" OnTextChanged="txtIdNumber_TextChanged"
                                          AutoPostBack="true"  TabIndex="29"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtIdNumber"
                                            ValidationExpression="[a-zA-Z0-9]*$" ErrorMessage="Only alphanumeric characters allowed."
                                            Display="Dynamic" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr align="left" class="darkblue">
                                    <td colspan="2">
                                        <span style="color: red">*</span><asp:Label ID="Label50" runat="server" CssClass="divIDProofDoc"
                                            Text="Re-Enter ID Proof No."></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtReIdNumber" runat="server" Width="35%" TabIndex="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                            Display="Dynamic" ControlToValidate="txtReIdNumber" ValidationExpression="[a-zA-Z0-9]*$"
                                            ErrorMessage="Only alphanumeric characters allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator13" ControlToCompare="txtIdNumber" ValueToCompare="txtReIdNumber"
                                            Display="Dynamic" ControlToValidate="txtReIdNumber" runat="server" ErrorMessage="The value not the same as above.">
                                        </asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr align="left" class="darkblue">
                                    <td colspan="2">
                                        <span style="color: red">*</span><asp:Label ID="Label51" runat="server" CssClass="divIDProofDoc"
                                            Text="Enter your name as on ID Proof"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtNameIDProof" runat="server" Width="35%" TabIndex="31"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                            Display="Dynamic" ControlToValidate="txtNameIDProof" ValidationExpression="[a-zA-Z ]*$"
                                            ErrorMessage="Only alphabate characters allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr align="left" class="darkblue">
                                    <td colspan="2">
                                        <span style="color: red">*</span><asp:Label ID="Label52" runat="server" CssClass="divIDProofDoc"
                                            Text="Re-Enter your name as on ID Proof"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtReNameIDProof" runat="server" Width="35%" TabIndex="32"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                            Display="Dynamic" ControlToValidate="txtReNameIDProof" ValidationExpression="[a-zA-Z ]*$"
                                            ErrorMessage="Only alphabate characters allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator14" ControlToCompare="txtNameIDProof" ValueToCompare="txtReNameIDProof"
                                            Display="Dynamic" ControlToValidate="txtReNameIDProof" runat="server" ErrorMessage="The value not the same as above.">
                                        </asp:CompareValidator>
                                    </td>
                                </tr>
                            </div>
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                    <asp:Image ID="imgPicture" Style="height: 180px; width: 240px; float: right;" runat="server"
                                        Visible="true" />
                                    <%--class="zoom" this be aded for zoom effect on mouse hover on image --%>
                                </td>
                                <td colspan="2" class="style2">
                                    <asp:FileUpload ID="fupIDProofDoc" CssClass="divIDProofDoc" runat="server" onchange="ShowImagePreview(this);"
                                        Width="36%" TabIndex="33" />
                                </td>
                            </tr>
                            <tr align="left" class="darkblue">
                                <td colspan="4">
                                    <asp:Label ID="Label17" runat="server" ForeColor="#C00000" CssClass="Label13" Text="[upload scanned copy of id Proof (15kb to 60kb, min resolution for PANCARD/DRIVING LICENSE - 300*200(width*height), for voter-ID card 200*300(width*height), for passport 450*300(width*height) pixel in JPEG/JPG format only.]"></asp:Label>
                                </td>
                            </tr>
                        </div>
                        <tr align="left" class="darkblue">
                            <td colspan="4">
                                <asp:Label ID="Label55" runat="server" ForeColor="#C00000" CssClass="Label13" Text="Note: The ID Proof as entered by the applicant will be cross checked at the time of examination and submission of e-dossier, if shortlisted."></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: #ff0000">*</span><asp:Label ID="Label1" runat="server" CssClass="Label13"
                                    Text="Password"></asp:Label>
                                <br />
                                <span style="color: #ff0000">Note your password for login in OARS account</span>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtpassword" runat="server" MaxLength="16" Width="35%" TextMode="Password"
                                    AUTOCOMPLETE="OFF" TabIndex="34"></asp:TextBox>
                                <asp:Label ID="Label46" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                    Text="(Password must contain at least eight
                                            characters including one uppercase(A-Z), one lowercase(a-z), one digit(0-9), one
                                            special character [!$%^*@#&].)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:RequiredFieldValidator ID="rfvp" runat="server" ControlToValidate="txtpassword"
                                    Display="Dynamic" ErrorMessage="Please Enter Password" ValidationGroup="1"></asp:RequiredFieldValidator>&nbsp;
                                <asp:RegularExpressionValidator ID="revpwd" runat="server" ControlToValidate="txtpassword"
                                    Display="Dynamic" ErrorMessage="Please Enter Valid Password." ValidationExpression="[a-zA-Z0-9!$%^*@#&]{6,}$"
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label47" CssClass="Label13" runat="server"
                                    Text="Re-enter Password"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txt_re_password" runat="server" MaxLength="16" Width="35%" TextMode="Password"
                                    AUTOCOMPLETE="OFF" TabIndex="35"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="2">
                                <asp:CompareValidator ID="cfv_passwd" runat="server" ControlToCompare="txtpassword"
                                    ValueToCompare="txt_re_password" ControlToValidate="txt_re_password" Display="Dynamic"
                                    ErrorMessage="Password is not matching." ValidationGroup="1"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="rfvrpass" runat="server" ControlToValidate="txt_re_password"
                                    Display="Dynamic" ErrorMessage="Please Enter ReType  Password" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rexppwd" runat="server" ControlToValidate="txt_re_password"
                                    ValidationExpression="[a-zA-Z0-9!$%^*@#&]{6,}$" Display="Dynamic" ErrorMessage="Re-enter valid password."
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="1">
                                <asp:CheckBox ID="CheckBoxdisclaimer" runat="server" EnableTheming="True" CssClass="formlabel"
                                    TabIndex="36" />
                                <asp:Label ID="Label12" runat="server" Text="UNDERTAKING :" CssClass="ariallightgrey"
                                    ForeColor="#C00000"></asp:Label>
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="4">
                                <ol style="color: #C00000;">
                                    <%--<li>I do hereby declare that the above information is correct and complete to the best
                                        of my knowledge and belief. If above information are found wrong, Board's decision
                                        will be final and binding on me." </li>--%>
                                    <li>I hereby certify that all statement made in this application are true, complete
                                        and correct to the best of my knowledge and belief and have been filled by me.
                                    </li>
                                    <li>I understand that in the event of information being found false or incorrect at
                                        any stage or any ineligibility being detected before or after the examination ,
                                        my candidature/selection/appointment is liable to be cancelled/terminated automatically
                                        without any notice to me and action can be taken against me by DSSSB. </li>
                                    <li>The information submitted herein shall be treated as final in respect of my candidature
                                    </li>
                                    <li>I also declare that I have informed my head of office /Department in writing (For
                                        Government Employee only) </li>
                                    <li>I hereby certify that this is my sole application for registration and that any
                                        additional registartion would lead to cancelation of all such multiple registartion.
                                    </li>
                                </ol>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="4" align="center">
                                <asp:Button ID="btnpopup" CssClass="cssbutton" Width="90px" Height="30px" Text="Preview"
                                    ValidationGroup="1" TabIndex="37" runat="server" OnClientClick="checkAddress(); return false;" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="4">
                                <asp:CheckBox ID="chkPreview" runat="server" EnableTheming="True" CssClass="formlabel"
                                    TabIndex="38" />
                                <asp:Label ID="Label26" runat="server" Text="I have seen the preview and verified that all the details filled by me are correct.
                                        I will not claim any change in my details after final submission of registration
                                        form." CssClass="ariallightgrey" ForeColor="#C00000"></asp:Label>
                            </td>
                        </tr>
                        <%-- <tr align="left" class="darkblue">
                            <td colspan="4">
                                <ol style="color: #C00000;">
                                    <li>I have seen the preview and verified that all the details filled by me are correct.
                                        I will not claim any change in my details after final submission of registration
                                        form. </li>
                                </ol>
                            </td>
                        </tr>--%>
                        <tr align="left" class="darkblue">
                            <td colspan="4" align="center">
                                <asp:Button ID="btnrsubmit" runat="server" CssClass="cssbutton" OnClick="btnrsubmit_Click"
                                    ToolTip="You can preview your form before final submission." OnClientClick="return SignValidate();"
                                    Text="Submit" Width="90px" Height="30px" ValidationGroup="1" TabIndex="39" />
                                <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="1" />
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="4">
                                <tr align="left" class="darkblue">
                                    <td style="height: 21px; width: 131px;">
                                    </td>
                                    <td style="width: 257px; height: 21px;">
                                    </td>
                                    <td align="left" style="width: 128px; height: 21px;">
                                    </td>
                                    <td style="height: 21px">
                                    </td>
                                </tr>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <div id="jsEnabled" style="visibility: hidden">
                                    JavaScript is enabled :Yor can register.
                                </div>
                                <div id="jsDisabled" style="visibility: visible">
                                    <span style="color: red; font-size: xx-large">JavaScript is disabled : Please enable
                                        it in Browser settings to get register.</span>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <%--</asp:Panel>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Panel ID="pnlscript" runat="server" Width="100%">--%>
                </td>
            </tr>
            <tr id="trreg" runat="server" visible="false" style="height: 400px">
                <td align="center" valign="middle">
                    <table>
                        <tr>
                            <td align="center" style="font-size: 18pt; font-weight: bold; text-decoration: underline;">
                                <asp:Label ID="lbl" runat="server" Font-Bold="true" ForeColor="DarkBlue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 18pt;">
                                <asp:Label ID="lblmsg" runat="server" CssClass="cssbutton" Font-Size="X-Large" Visible="true"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="center" style="font-size: 18pt">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_r1" runat="server" CssClass="cssbutton" Font-Size="X-Large"></asp:Label>
                                        </td>
                                        <td style="width: 125px">
                                            <asp:Label ID="lbl_r2" runat="server" CssClass="cssbutton" Font-Size="X-Large"></asp:Label>
                                        </td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lbl_r3" runat="server" CssClass="cssbutton" Font-Size="X-Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="DarkRed">(DOB)</asp:Label>
                                        </td>
                                        <td style="width: 125px">
                                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="DarkRed">(Roll No.)</asp:Label>
                                        </td>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="DarkRed">(Year)</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="center" style="font-size: 18pt;">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="DarkBlue" Visible="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 18pt;">
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Width="60%" ForeColor="DarkRed"
                                    Visible="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 18pt;">
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="DarkRed"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button runat="server" ID="btnprceed" Text="Proceed" CssClass="cssbutton" Width="80px"
                                    Font-Bold="true" OnClick="btnprceed_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="border_gray" width="950px" align="center" id="tblshow" runat="server"
                        visible="false">
                        <tr>
                            <td colspan="2" class="formlabel" align="left" style="border-bottom: 2px; border-bottom-color: steelblue;
                                border-bottom-style: solid;">
                                You are already registered.Please find below your registration details.
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabel" align="left" width="50%">
                                Registration No :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblcandregno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabel" align="left">
                                Name :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblcandname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabel" align="left">
                                Father Name :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblcandfname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabel" align="left">
                                Date of Birth :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblcanddob" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: 2px; border-top-color: steelblue; border-top-style: solid;"
                                colspan="2" align="left">
                                <asp:CheckBox ID="chkagree" runat="server" />
                                I hereby undertake that I am the person whose details are displayed at both places.
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnagree" runat="server" CssClass="cssbutton" Text="Agree" Width="70px"
                                    Font-Bold="true" OnClick="btnagree_Click" />&nbsp;
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="btndisagree" runat="server" CssClass="cssbutton" Text="Not Agree"
                                    Width="75px" Font-Bold="true" OnClick="btndisagree_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:WebUserControl ID="Footer" runat="server" />
                </td>
            </tr>
        </table>
        <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </div>
    </form>
    <script>
        function space(el, after) {
            var v = el.value;
            if (v.match(/^\d{4}$/) !== null) {
                el.value = v + ' ';
            } else if (v.match(/^\d{4}\ \d{4}$/) !== null) {
                el.value = v + ' ';
            }
        }
        var el = document.getElementById('txtAdharNo');
        el.addEventListener('keyup', function () {
            space(this, 4);
        });
        var rel = document.getElementById('txtReAdharNo');
        rel.addEventListener('keyup', function () {
            space(this, 4);
        });
    </script>
</body>
</html>
