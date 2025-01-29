<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadIDProof.aspx.cs" Inherits="UploadIDProof" %>

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
        }    
    </script>
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
                                    For Uploading ID Proof on OARS Portal </span>
                                <asp:Label ID="Label7" runat="server" CssClass="formlabel" Text=""></asp:Label>
                                <%--</td>
                            <td align="right" colspan="1">--%>
                                <asp:HyperLink ID="HyperLink1" Style="float: right;" runat="server" CssClass="formlabel"
                                    NavigateUrl="~/AdvtList.aspx">Back</asp:HyperLink>
                            </td>
                        </tr>
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
                        <div id="divSelect" runat="server" visible="true">
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
                        </div>
                        <div id="divAadhaar" runat="server" visible="true">
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                    <span style="color: red">*</span><asp:Label ID="Label28" runat="server" CssClass="Label13"
                                        Text="Enter Aadhaar No."></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtAdharNo" runat="server" Width="35%" MaxLength="14" AutoPostBack="true"
                                        OnTextChanged="txtAdharNo_TextChanged" TabIndex="24"></asp:TextBox><br />
                                    <asp:Label ID="lblAadharVal" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                </td>
                                <td colspan="2">
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
                                    <asp:TextBox ID="txtReAdharNo" runat="server" Width="35%" MaxLength="14" TabIndex="25"
                                        OnTextChanged="txtReAdharNo_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                </td>
                                <td colspan="2">
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
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                </td>
                                <td colspan="2">
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
                            <tr align="left" class="darkblue">
                                <td colspan="2">
                                </td>
                                <td colspan="2">
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
                                            AutoPostBack="true" TabIndex="29"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr align="left" class="darkblue">
                                    <td colspan="2">
                                    </td>
                                    <td colspan="2">
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
                                <tr align="left" class="darkblue">
                                    <td colspan="2">
                                    </td>
                                    <td colspan="2">
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
                                <tr align="left" class="darkblue">
                                    <td colspan="2">
                                    </td>
                                    <td colspan="2">
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
                                <tr align="left" class="darkblue">
                                    <td colspan="2">
                                    </td>
                                    <td colspan="2">
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
                            <td colspan="4" align="center">
                                <asp:Button ID="btnrsubmit" runat="server" CssClass="cssbutton" OnClick="btnrsubmit_Click"
                                    ToolTip="You can preview your form before final submission." OnClientClick="return SignValidate();"
                                    Text="Submit" Width="120px" Height="30px" ValidationGroup="1" TabIndex="39" />
                                <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="1" />
                            </td>
                        </tr>
                    </table>
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
