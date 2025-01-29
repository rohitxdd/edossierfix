<%@ Page Language="C#" AutoEventWireup="true" CodeFile="insertAdharByCandidate.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="insertAdharByCandidate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
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
   
         .modal {
    display: none; /* Hidden by default */
    margin-top: 7%;
    position: absolute; /* Stay in place */
    z-index: 1; /* Sit on top */
    padding-top: 50px; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 735px; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
    position: relative;
    background-color: #fefefe;
    margin: auto;
    padding: 0;
    border: 1px solid #888;
    width: 50%;
    height:735px;
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
    -webkit-animation-name: animatetop;
    -webkit-animation-duration: 0.4s;
    animation-name: animatetop;
    animation-duration: 0.4s
}

/* Add Animation */
@-webkit-keyframes animatetop 
{
    from 
    {
    top:-300px; 
    opacity:0
    } 
    to {top:0; opacity:1}
}

@keyframes animatetop {
    from {top:-300px; opacity:0}
    to {top:0; opacity:1}
}


.close {
    color: white;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.close:hover,
.close:focus {
    color: #000;
    text-decoration: none;
    cursor: pointer;
}

.modal-header {
    padding: 0px 1px 0px 0px;
    background-color: #5cb85c;
    color: white;
    height:100%;
}
.thleft{
padding-right: 25px; text-align:right;
}
.thright{
 text-align:left;
}
</style>
    <script type="text/javascript">

        function showMCDModal() {
            debugger;
            var modal = document.getElementById('myModal');
            modal.style.display = "block";
        }
        function lnkbtn_close_Click() {
            debugger;
            var modal = document.getElementById('myModal');
            modal.style.display = "none";
            document.getElementById('<%= btn_close.ClientID %>').click();
            $("#imgPostCard").show();
        }
    
    </script>
    <script>
        function space(el, after) {
            var v = el.value;
            if (v.match(/^\d{4}$/) !== null) {
                el.value = v + ' ';
            } else if (v.match(/^\d{4}\ \d{4}$/) !== null) {
                el.value = v + ' ';
            }
        }
        window.onload = function () {
            debugger;
            var el = document.getElementById('<%= txtAdharNo.ClientID %>'); //document.getElementById('txtAdharNo');
            if (el != null) {
                el.addEventListener('keyup', function () {
                    space(this, 4);
                });
            }

            var rel = document.getElementById('<%= txtReAdharNo.ClientID %>'); //document.getElementById('txtReAdharNo');
            if (rel != null) {
                rel.addEventListener('keyup', function () {
                    space(this, 4);
                });
            }
        }
    </script>
    </script>
    <ajax:ToolkitScriptManager ID="MainSM" runat="server" EnablePageMethods="true" ScriptMode="Release"
        LoadScriptsBeforeUI="true">
    </ajax:ToolkitScriptManager>
    <div>
        <asp:HiddenField ID="hdnApplid" runat="server" />
        <table align="center" width="100%" class="border_gray" autocomplete="false">
            <div id="div2" runat="server" visible="true">
                <tr>
                    <td colspan="7" class="tr" align="center">
                        Submission of ID proof information in registration form
                    </td>
                </tr>
                <tr class="formlabel">
                    <td align="left" colspan="7">
                        <asp:Label ID="Label3" runat="server" CssClass="formlabel" Text="Instructions:" Width="84px"
                            Font-Bold="True"></asp:Label>
                        <br />
                        <span style="color: red">1 :</span>The candidates who are already registered shall
                        provide their ID details like Aadhar or PAN/Passport/Voter ID/DL as an identity
                        proof for future reference.&nbsp;<br />
                        <span style="color: red">2 :</span><asp:Label ID="Label18" runat="server" CssClass="formlabel"
                            Text="Applicant's are requested to fill the identity proof information carefully as this information will be printed on candidates admit card which will be required at the time of examination."></asp:Label>
                        <br />
                        <span style="color: red">3 :</span><asp:Label ID="Label6" runat="server" CssClass="formlabel"
                            Text="The ID Proof as entered by the applicant will be cross checked at the time of examination and submission of e-dossier, if shortlisted."></asp:Label>
                    </td>
                </tr>
            </div>
            <tr align="left" class="darkblue">
                <td colspan="2">
                </td>
                <td colspan="5">
                </td>
            </tr>
            <tr align="left" class="darkblue">
                <td colspan="7">
                    <asp:Label ID="lblShowMsgAdharPcsp" Font-Size="Larger" ForeColor="Red" runat="server"
                        CssClass="Label13" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr align="left" class="darkblue">
                <td colspan="2">
                </td>
                <td colspan="5">
                </td>
            </tr>
            <%--<div id="divAdvtPCAppNo" runat="server" visible="true">
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <span style="color: red">*</span><asp:Label ID="Label1" runat="server" CssClass="Label13"
                            Text="Select Advt. No. : "></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:DropDownList ID="ddlAdvt" runat="server" Width="35%" AutoPostBack="true" OnSelectedIndexChanged="ddlAdvt_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                            <asp:ListItem Value="26">1/20</asp:ListItem>
                            <asp:ListItem Value="27">2/20</asp:ListItem>
                            <asp:ListItem Value="28">3/20</asp:ListItem>
                            <asp:ListItem Value="29">4/20</asp:ListItem>
                            <asp:ListItem Value="30">5/20</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <span style="color: red">*</span><asp:Label ID="Label2" runat="server" CssClass="Label13"
                            Text="Select Post Code : "></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:DropDownList ID="ddlPostCode" Width="35%" runat="server" />
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <span style="color: red">*</span><asp:Label ID="Label4" runat="server" CssClass="Label13"
                            Text="Enter Application No. : "></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtAppNo" runat="server" Width="34%" />
                        <asp:Label ID="Label33" runat="server" ForeColor="Red" CssClass="Label13" Text=""></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAppNo"
                            ValidationExpression="[0-9]*$" ErrorMessage="Only number is allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                    </td>
                    <td colspan="5">
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="7" align="center">
                        <asp:Button ID="btnVerify" Text="Verify Detail" Style="font-weight: bold; height: 30px;
                            width: 12%; border-radius: 5px;" runat="server" CssClass="cssbutton" OnClick="btnVerify_Click" />
                    </td>
                </tr>
            </div>
            <div id="divVerify" runat="server" visible="false">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label5" runat="server" CssClass="Label13" Text="Advt. No. "></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblAdvtNo" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label7" runat="server" CssClass="Label13" Text="Post Code"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblPostCode" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label8" runat="server" CssClass="Label13" Text="Post Name"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblPostName" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label9" runat="server" CssClass="Label13" Text="Application No."></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblAppNo" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label10" runat="server" CssClass="Label13" Text="Applicant Name."></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblAppName" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label11" runat="server" CssClass="Label13" Text="Applicant D.O.B."></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblDob" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="7" align="center">
                        <asp:Button ID="btnProceed" Text="Proceed" Style="font-weight: bold; height: 30px;
                            width: 12%; border-radius: 5px;" runat="server" CssClass="cssbutton" OnClick="btnProceed_Click" />
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="7" align="center">
                        <span style="color: red">* Proceed to enter identity proof details.</span>
                    </td>
                </tr>
            </div>--%>
            <div id="divUploadDoc" runat="server" visible="false">
                <tr css="tr">
                    <td colspan="7" class="tr" align="center">
                        Identity Proof Details
                    </td>
                </tr>
                <tr css="tr">
                    <td align="center" colspan="7">
                        <asp:Label ID="Label34" runat="server" class="Label13" Style="color: #C00000;" Text="For ensuring the genuineness of photo identity card which needs to be submitted by the candidates."
                            Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="lblAadhaarMsg" align="left" class="darkblue">
                    <td colspan="7">
                        <span style="color: red">Note :</span><asp:Label ID="lblAadhaarMsg1" runat="server"
                            CssClass="Label13" ForeColor="Navy" Text="In case Aadhaar number is provided, then no scanned ID proof is required to be uploaded."></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <span style="color: red">*</span><asp:Label ID="Label25" runat="server" CssClass="Label13"
                            Text="Do you want to give Aadhar as an ID proof"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:RadioButtonList ID="rbtHaveAdhar" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rbtHaveAdhar_SelectedIndexChanged">
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
                        <td colspan="5">
                            <asp:TextBox ID="txtAdharNo" runat="server" Width="35%" MaxLength="14" AutoPostBack="true"
                                OnTextChanged="txtAdharNo_TextChanged"></asp:TextBox><br />
                            <asp:Label ID="lblAadharVal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                        </td>
                        <td colspan="5">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                                Display="Dynamic" ControlToValidate="txtAdharNo" ValidationExpression="[ 0-9]*$"
                                ErrorMessage="Only number is allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                            <span style="color: red">*</span><asp:Label ID="Label30" runat="server" CssClass="Label13"
                                Text="Re-enter Aadhaar No."></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtReAdharNo" runat="server" Width="35%" MaxLength="14" OnTextChanged="txtReAdharNo_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                        </td>
                        <td colspan="5">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                                Display="Dynamic" ControlToValidate="txtReAdharNo" ValidationExpression="[ 0-9]*$"
                                ErrorMessage="Only number is allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtAdharNo" ValueToCompare="txtReAdharNo"
                                Display="Dynamic" ControlToValidate="txtReAdharNo" runat="server" ErrorMessage="The Aadhaar re-enter field is not the same as Aadhaar.">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                            <span style="color: red">*</span><asp:Label ID="lblAdN" runat="server" CssClass="Label13"
                                Text="Enter Name as on Aadhaar"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtNameAdhar" runat="server" Width="35%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                        </td>
                        <td align="left" colspan="5">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                Display="Dynamic" ControlToValidate="txtNameAdhar" ValidationExpression="[a-zA-Z ]*$"
                                ErrorMessage="Only alphabate characters allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                            <span style="color: red">*</span><asp:Label ID="lblReNad" runat="server" CssClass="Label13"
                                Text="Re-enter Name as on Aadhaar"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtReNameAdhar" runat="server" Width="35%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                        </td>
                        <td align="left" colspan="5">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                                Display="Dynamic" ControlToValidate="txtReNameAdhar" ValidationExpression="[a-zA-Z ]*$"
                                ErrorMessage="Only alphabate characters allowed." ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator12" ControlToCompare="txtNameAdhar" ValueToCompare="txtReNameAdhar"
                                Display="Dynamic" ControlToValidate="txtReNameAdhar" runat="server" ErrorMessage="The name not the same as above.">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                        </td>
                        <td colspan="5">
                        </td>
                    </tr>
                </div>
                <div id="divIDProofDoc" runat="server" visible="true">
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                            <span style="color: red; vertical-align: top">*</span><asp:Label ID="Label32" runat="server"
                                CssClass="divIDProofDoc" Text="Select type of ID proof"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:DropDownList ID="ddlIdDoc" Width="35%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdDoc_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <div runat="server" id="divtxtIdNum">
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <span style="color: red">*</span><asp:Label ID="Label49" runat="server" CssClass="divIDProofDoc"
                                    Text="Enter ID Proof No."></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtIdNumber" runat="server" Width="35%" AutoPostBack="true" OnTextChanged="txtIdNumber_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="5">
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
                            <td colspan="5">
                                <asp:TextBox ID="txtReIdNumber" runat="server" Width="35%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="5">
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
                                <span style="color: red">*</span>
                                <asp:Label ID="Label51" runat="server" CssClass="divIDProofDoc" Text="Enter your name as on ID Proof"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtNameIDProof" runat="server" Width="35%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="5">
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
                            <td colspan="5">
                                <asp:TextBox ID="txtReNameIDProof" runat="server" Width="35%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                            </td>
                            <td align="left" colspan="5">
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
                                Visible="true" /><%--class="zoom" this be aded for zoom effect on mouse hover on image --%>
                        </td>
                        <td colspan="5" class="style2">
                            <asp:FileUpload ID="fupIDProofDoc" CssClass="divIDProofDoc" runat="server" Width="36%"
                                onchange="this.form.submit();" />
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="7">
                            <asp:Label ID="lbl32" runat="server" ForeColor="#C00000" CssClass="Label13" Text="[upload scanned copy of id Proof (15kb to 60kb, min resolution for PANCARD/DRIVING LICENSE - 300*200(width*height), for voter-ID card 200*300(width*height), for passport 450*300(width*height) pixel in JPEG/JPG format only.]"></asp:Label>
                        </td>
                    </tr>
                </div>
                <tr align="left" class="darkblue">
                    <td colspan="7">
                        <asp:Label ID="Label55" runat="server" ForeColor="#C00000" CssClass="Label13" Text="Note: The ID Proof as entered by the applicant will be cross checked at the time of examination and submission of e-dossier, if shortlisted."></asp:Label>
                    </td>
                </tr>
                <%-- <tr align="left" class="darkblue">
                    <td colspan="1">
                        <asp:CheckBox ID="CheckBoxdisclaimer" runat="server" EnableTheming="True" CssClass="formlabel" />
                        <asp:Label ID="Label12" runat="server" Text="UNDERTAKING :" CssClass="ariallightgrey"
                            ForeColor="#C00000"></asp:Label>
                    </td>
                    <td colspan="5">
                    </td>
                </tr>--%>
                <%--<tr align="left" class="darkblue">
                    <td colspan="6">
                        <ol style="color: #C00000;">
                            <li>I do hereby declare that the above information is correct and complete to the best
                                        of my knowledge and belief. If above information are found wrong, Board's decision
                                        will be final and binding on me." </li>
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
                        </ol>
                    </td>
                </tr>--%>
                <%--<tr align="left" class="darkblue">
                    <td colspan="7">
                        <asp:CheckBox ID="CheckBoxdisclaimer" runat="server" EnableTheming="True" CssClass="formlabel" />
                        <asp:Label ID="Label12" runat="server" Text="UNDERTAKING : 
                                I do hereby declare that the above information is correct and complete to the best of my knowledge and belief. If above information are found wrong, Board's decision will be final and binding on me."
                            CssClass="ariallightgrey Label13" ForeColor="#C00000"></asp:Label>
                    </td>
                </tr>--%>
                <tr align="left" class="darkblue">
                    <td colspan="7" align="center">
                        <asp:Button ID="btnSaveAdharNo" Text="Next" Font-Bold="true" Width="90px" Height="30px"
                            ToolTip="Proceed to upload postcard size photograph." runat="server" CssClass="cssbutton"
                            OnClick="btnSaveAdharNo_Click" />
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="7" align="center">
                        <span style="color: red">* Proceed to upload postcard size photograph.</span>
                    </td>
                </tr>
            </div>
            <div id="divuploadPCPhoto" runat="server" visible="false">
                <tr>
                    <td>
                    </td>
                </tr>
                <div id="div1" runat="server">
                    <tr css="tr">
                        <td colspan="7" class="tr" align="center">
                            Upload Postcard Size Photograph
                        </td>
                    </tr>
                </div>
                <tr>
                    <td colspan="7">
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="7">
                        <asp:GridView ID="grdApplidDummyNo" runat="server" AutoGenerateColumns="False" Caption="Post details applied by candidate"
                            DataKeyNames="applid" CssClass="gridfont" Height="100%" Width="100%" Font-Size="larger">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="postcode" HeaderText="Post Code" Visible="true">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField DataField="JobTitle" HeaderText="Post Name" Visible="true">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dummy_no" HeaderText="Application No." Visible="true">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField DataField="applid" HeaderText="Application ID.">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridheading" />
                        </asp:GridView>
                    </td>
                </tr>
                <%-- <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label13" runat="server" CssClass="Label13" Text="Advt. No. "></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="Label14" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label15" runat="server" CssClass="Label13" Text="Post Code"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="Label16" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label17" runat="server" CssClass="Label13" Text="Post Name"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="Label19" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label20" runat="server" CssClass="Label13" Text="Application No."></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="Label21" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label22" runat="server" CssClass="Label13" Text="Applicant Name."></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="Label23" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>
                <tr align="left" class="darkblue">
                    <td colspan="2">
                        <asp:Label ID="Label24" runat="server" CssClass="Label13" Text="Applicant D.O.B."></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="Label26" runat="server" CssClass="Label13"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="7">
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                    </td>
                </tr>
                <div runat="server" id="divFinalSaveHide">
                    <tr align="left" class="darkblue">
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr align="left" class="darkblue">
                        <td colspan="2">
                            <asp:Label ID="Label27" runat="server" CssClass="formlabel" Text="Upload Postcard Size(5x7 inch) Photograph"></asp:Label>
                        </td>
                        <td colspan="5" class="style2">
                            <asp:FileUpload ID="fuPostCard" CssClass="divIDProofDoc" runat="server" Width="36%"
                                onchange="this.form.submit();" /><%--<span class="divIDProofDoc">Upload Postcard Size(50kb
                                to 300kb) Photograph[JPEG/JPG].</span>--%>
                        </td>
                        <tr align="left" class="darkblue">
                            <td colspan="2">
                                <%--<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="http://www.freeonlinephotoeditor.com/"
                                    Target="_blank">Crop Photo Online</asp:HyperLink><br />--%>
                                <%--<asp:Button ID="Button1" Text="Preview Image." Font-Bold="true" runat="server" CssClass="cssbutton"
                                    OnClick="Button1_Click" Style="font-weight: bold; color: blue; background: white;" />--%>
                            </td>
                            <td colspan="5">
                                <asp:Image ID="imgPostCard" runat="server" Height="94px" Width="114px" />
                                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" Text="Preview Image." Font-Bold="true"
                                    runat="server" CssClass="cssbutton" OnClick="Button1_Click" />
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="7">
                                <span style="color: red; vertical-align: top">Note: </span>
                                <asp:Label ID="Label29" ForeColor="Navy" runat="server" CssClass="Label13" Text="Upload scanned /  digital image of coloured postcard size photograph of the candidate and should be in JPEG format and image should be between 50 kb to 300 kb (required resolution 480x672 pixels). Coloured postcard size photograph ( size 5*7 inch) should be of upper half of body only clearly showing face, both ears and both shoulders. "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <div runat="server" id="divchkUPCSPA" visible="true">
                            <tr align="left">
                                <td colspan="7">
                                    <asp:CheckBox ID="chkUPCSPA" runat="server" EnableTheming="True" CssClass="formlabel" />
                                    <asp:Label ID="Label13" runat="server" Text="I have seen the preview and verify that the upploaded postcard size photograph is as per the instruction given above."
                                        CssClass="ariallightgrey" ForeColor="#C00000"></asp:Label>
                                </td>
                            </tr>
                        </div>
                        <tr align="left" class="darkblue">
                            <td colspan="1">
                                <asp:CheckBox ID="chkUTPCSP" runat="server" EnableTheming="True" CssClass="formlabel" />
                                <asp:Label ID="Label14" runat="server" Text="UNDERTAKING :" CssClass="ariallightgrey"
                                    ForeColor="#C00000"></asp:Label>
                            </td>
                            <td colspan="5">
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="6">
                                <ol style="color: #C00000;">
                                    <li>I have checked that above postcode(s) are applied by me and I am hereby uploading
                                        my postcard size photograph(s) for the above mentioned postcode(s)." </li>
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
                                </ol>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                            </td>
                        </tr>
                        <tr align="left" class="darkblue">
                            <td colspan="7" align="center">
                                   <%-- <asp:Button ID="btnPostCardPic" Text="Submit" Font-Bold="true" Width="90px" Height="30px"
                                    runat="server" CssClass="cssbutton" OnClick="btnPostCardPic_Click" />--%>
                                <asp:Button ID="btnPostCardPic" Text="Submit" Font-Bold="true" Width="90px" Height="30px"
                                    runat="server" CssClass="cssbutton" OnClick="btnPostCardPic_Click" UseSubmitBehavior="false"
                                    OnClientClick="this.disabled='true'; this.value='Please wait...';" />
                            </td>
                        </tr>
                </div>
            </div>
        </table>
        <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </div>
    <div id="myModal" class="modal">
        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" style="background-color: red" onclick="lnkbtn_close_Click()"
                    data-dismiss="modal">
                    &times;</button>
                <div style="border: 5px red; border-style: solid; width: 480px; height: 672px; overflow: hidden;">
                    <table>
                        <tr>
                            <td align="center" valign="middle">
                                <asp:Image ID="imgPostCardPhoto" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btn_close" runat="server" class="btn btn-default" data-dismiss="modal"
                    Text="Close" />
            </div>
        </div>
    </div>
</asp:Content>
