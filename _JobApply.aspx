<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JobApply.aspx.cs" Inherits="JobApply" Title="Untitled Page" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">



<%--<%@ Register Assembly="WebControlCaptcha" Namespace="WebControlCaptcha" TagPrefix="cc1" %>--%>



    <script src="Jscript/validations.js" type="text/javascript"></script>

    <script language="JavaScript" type="text/javascript">

        window.onerror = null;
        var bName = navigator.appName;
        var bVer = parseInt(navigator.appVersion);
        var NS4 = (bName == "Netscape" && bVer >= 4);
        var IE4 = (bName == "Microsoft Internet Explorer"
 && bVer >= 4);
        var NS3 = (bName == "Netscape" && bVer < 4);
        var IE3 = (bName == "Microsoft Internet Explorer"
 && bVer < 4);
        var blink_speed = 200;
        var i = 0;

        if (NS4 || IE4) {
            if (navigator.appName == "Netscape") {
                layerStyleRef = "layer.";
                layerRef = "document.layers";
                styleSwitch = "";
            } else {
                layerStyleRef = "layer.style.";
                layerRef = "document.all";
                styleSwitch = ".style";
            }
        }

        //BLINKING
        function Blink(layerName) {
            if (NS4 || IE4) {
                if (i % 2 == 0) {
                    eval(layerRef + '["' + layerName + '"]' +
 styleSwitch + '.visibility="visible"');
                }
                else {
                    eval(layerRef + '["' + layerName + '"]' +
 styleSwitch + '.visibility="hidden"');
                }
            }
            if (i < 1) {
                i++;
            }
            else {
                i--
            }
            setTimeout("Blink('" + layerName + "')", blink_speed);
        }
        function CopyAddress() {
            document.getElementById("<%=txtAddress_per.ClientID %>").value = document.getElementById("<%=txtAddress.ClientID %>").value;
            document.getElementById("<%=ddlDistrict_per.ClientID %>").value = document.getElementById("<%=ddlDistrict.ClientID %>").value;
            document.getElementById("<%=txtPIN_per.ClientID %>").value = document.getElementById("<%=txtPIN.ClientID %>").value;
        }
        function f_popup(aURL, aWinName) {
            var wOpen;
            var sOptions;
            sOptions = 'status=no,menubar=no,scrollbars=yes,resizable=yes,toolbar=no';
            var wWidth = screen.width - 50;
            sOptions = sOptions + ',width=' + wWidth.toString();
            var wHeight = screen.height - 50;
            sOptions = sOptions + ',height=' + wHeight.toString();
            sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';
            //alert(sOptions);
            wOpen = window.open('', aWinName);
            //		alert(aURL);
            wOpen.location = aURL;
            wOpen.focus();
            return wOpen;
        }
        //  End -->
    </script>


<link href="CSS/Applicant.css" rel="stylesheet" type="text/css">
<body>
    <form id="form1"  style=" text-align:center" class="form">
    
    <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px; height: 100%;">
        
        <tr height="70%">
            <td>
                <table border="0" width="100%" height="100%">
                    <tr>
                        <td colspan="1" style="width: 100%; height: 20px; text-align: left;" class="trLightBlue">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="Left">
                                        <asp:Label ID="LblAppOnline" runat="server" CssClass="ariallightgrey" Width="60%"
                                            meta:resourcekey="LblAppOnlineResource1">Apply Online</asp:Label>
                                    </td>
                                    <td align="right">
                                        || <a href="images/form-help.pdf" id="A2" class="ariallightgrey" target="_blank"
                                            runat="server">Form Instruction</a> || <a href="Home.aspx" id="lnkHome" class="ariallightgrey"
                                                runat="server">HOME</a> ||
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" style="width: 100%; height: 10px">
                            <asp:Label ID="lblADVT_SHOW" runat="server" Text="Label" Visible="False" meta:resourcekey="lblADVT_SHOWResource1"></asp:Label>
                        </td>
                        <asp:Label ID="lblAdvtId" runat="server" CssClass="ariallightgrey" Visible="False"
                            meta:resourcekey="lblAdvtIdResource1"></asp:Label><asp:Label ID="lblAdvtYear" runat="server"
                                CssClass="ariallightgrey" Visible="False" meta:resourcekey="lblAdvtYearResource1"></asp:Label><asp:Label
                                    ID="LblJobSource" runat="server" CssClass="ariallightgrey" Visible="False" meta:resourcekey="LblJobSourceResource1"></asp:Label><asp:Label
                                        ID="LblJobSourceId" runat="server" CssClass="ariallightgrey" Visible="False"
                                        meta:resourcekey="LblJobSourceIdResource1"></asp:Label><asp:Label ID="lblSeat_General"
                                            runat="server" CssClass="ariallightgrey" Visible="False" meta:resourcekey="lblSeat_GeneralResource1"></asp:Label></tr>
                    <tr>
                        <td align="center">
                            <table cellspacing="1" cellpadding="0" width="100%" class="frame">
                                <tr>
                                    <td valign="top">
                                        <table width="100%" cellpadding="2">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label Visible="false" Style="color: Red; font-size: medium;" ID="LblMsg" runat="server"
                                                        Text=""
                                                        CssClass="ariallightgrey" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="ariallightgrey">
                                                    <marquee onMouseOver="this.stop();" onMouseOut="this.start();">
                                                        Read the
                                                        <asp:HyperLink ID="hlnkAdvt" runat="server" Target="_blank" meta:resourcekey="hlnkAdvtResource1"><b>Advertisment</b></asp:HyperLink>
                                                        and <a href="images/form-help.pdf" id="link1" target="_blank" runat="server">How to
                                                            Apply</a> before applying</marquee>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="fieldarea">
                                                    <asp:Label ID="lblApplPost" runat="server" CssClass="ariallightgrey" meta:resourcekey="lblApplPostResource1">Application for Posts ::</asp:Label>
                                                </td>
                                                <td width="85%">
                                                    <asp:Label ID="LblPost" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                                        meta:resourcekey="LblPostResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="fieldarea">
                                                    <asp:Label ID="lbl2" runat="server" CssClass="ariallightgrey" meta:resourcekey="lbl2Resource1">Advt No ::</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAdvtNo" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                                        meta:resourcekey="lblAdvtNoResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table align="center" border="0" rules="none" width="100%">
                                                        <tr>
                                                            <td colspan="1" valign="top" class="ariallightgrey" style="height: 71px">
                                                                <div >
                                                                    <font color="#ad0c03"><b>Note:</b></font></div>
                                                            </td>
                                                            <td class="ariallightgrey" style="height: 71px">
                                                                <div >
                                                                    - Special Characters not allowed &lt;%&lt;&gt;'()&amp;+-;!&gt;,.-* etc.
                                                                    <br />
                                                                    - <span class="required">*</span> indicates mandatory field.
                                                                    <br />
                                                                    - Your Birth Date should be between
                                                                    <asp:Label ID="lblJobFirstdate" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                                                        meta:resourcekey="lblJobFirstdateResource1"></asp:Label>
                                                                    and
                                                                    <asp:Label ID="lblJobLastdate" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                                                                        meta:resourcekey="lblJobLastdateResource1"></asp:Label>, if you are not entitled
                                                                    for age relaxation. For details about age relaxation please see the advertisement.
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                            </table>
                            <tr>
<td>
                             <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Red"
                                meta:resourcekey="lblErrorMsgResource1"></asp:Label>

                            <script language="javascript">                                Blink('lblErrorMsg'); function TABLE1_onclick() { }</script>

                        </td>
                    </tr>
                    <tr height="550px">
                        <td valign="top" style="width: 85%">
                            
                            <table border="0" width="100%">
                               
                                <tr>
                                    <td>
                                    <asp:Panel ID="Pan1" runat="server">
                                            
                                                        <div align="left" style="background-color:Aqua">
                                                            Personal Details </div>
                                                        <div align="right">
                                                            <b></b></div>
                                                    
                                                    
                                                        <table bgcolor="#d2e2b7" border="0" cellpadding="1" cellspacing="0" class="greydotRED"
                                                            width="100%">
                                                            <tbody>
                                                                <tr class="trFloral">
                                                                    <td width="201" colspan="1" align="left" style="width: 200px">                                                                    </td>
                                                                    <td width="479" colspan="1" align="left" style="width: 220px">
                                                                        <asp:Label ID="lblSurname" runat="server" CssClass="ariallightgrey" Text="Surname"
                                                                            meta:resourcekey="lblSurnameResource1"></asp:Label>
                                                                        <span class="required">*</span>                                                                  </td>
                                                                    <td width="288" colspan="1" align="left" style="width: 197px">
                                                                        <asp:Label ID="lblFname" runat="server" CssClass="ariallightgrey" Text="First Name"
                                                                            meta:resourcekey="lblFnameResource1"></asp:Label>
                                                                        <span class="required">*</span>                                                                  </td>
                                                                    <td width="229" colspan="1" align="left">
                                                                        <asp:Label ID="lblLname" runat="server" CssClass="ariallightgrey" Text="Father/Husband Name"
                                                                            meta:resourcekey="lblLnameResource1"></asp:Label>                                                                  </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td align="left" colspan="1" style="width: 200px" >
                                                                        <asp:DropDownList ID="ddlSalute" runat="server"  TabIndex="1"
                                                                            Width="80px" meta:resourcekey="ddlSaluteResource1">
                                                                            <asp:ListItem meta:resourcekey="ListItemResource1">Mr.</asp:ListItem>
                                                                            <asp:ListItem meta:resourcekey="ListItemResource2">Mrs.</asp:ListItem>
                                                                            <asp:ListItem meta:resourcekey="ListItemResource3">Ms.</asp:ListItem>
                                                                            <asp:ListItem meta:resourcekey="ListItemResource4">Dr.</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSalute"
                                                                            ErrorMessage="Select Title" SetFocusOnError="True" Width="1px" meta:resourcekey="RequiredFieldValidator10Resource1">*</asp:RequiredFieldValidator>                                                                    </td>
                                                                    <td align="left" colspan="1" style="width: 220px">
                                                                        <asp:TextBox ID="txtSurname" runat="server" TabIndex="2" Width="192px"
                                                                            meta:resourcekey="txtSurnameResource1"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSurname"
                                                                            ErrorMessage="Enter Surname" SetFocusOnError="True" Width="1px" meta:resourcekey="RequiredFieldValidator1Resource1">*</asp:RequiredFieldValidator>                                                                    </td>
                                                                    <td align="left" colspan="1" style="width: 197px">
                                                                        <asp:TextBox ID="txtFirstName" runat="server"  TabIndex="3" Width="176px"
                                                                            meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName"
                                                                            ErrorMessage="Enter First Name" SetFocusOnError="True" Width="1px" meta:resourcekey="RequiredFieldValidator2Resource1">*</asp:RequiredFieldValidator>                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:TextBox ID="txtLastName" runat="server"  TabIndex="4" Width="150px"
                                                                            meta:resourcekey="txtLastNameResource1"></asp:TextBox>                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td align="left" style="width: 200px">
                                                                        <asp:Label ID="lblMotherName" runat="server" CssClass="ariallightgrey" Text="Mother's Name"
                                                                            meta:resourcekey="lblMotherNameResource1"></asp:Label>
                                                                        <span class="required">*</span>                                                                    </td>
                                                                    <td align="left" colspan="4" style="text-align: left">
                                                                        <asp:TextBox ID="txtMotherName" runat="server" TabIndex="5" Width="300px"
                                                                            meta:resourcekey="txtMotherNameResource1"></asp:TextBox>
                                                                        &nbsp;
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtMotherName"
                                                                            ErrorMessage="Enter Mother's Name" SetFocusOnError="True" Width="1px" meta:resourcekey="RequiredFieldValidator21Resource1">*</asp:RequiredFieldValidator>                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td colspan="4">
                                                                        <table  class="table" width="100%" >
                                                                            <tr class="trFloral">
                                                                                <td>
                                                                                    <table border="0" width="100%">
                                                                                        <tr>
                                                                                            <td align="left" style="width: 200px">
                                                                                                <asp:Label ID="lblPresentAddr" runat="server" CssClass="ariallightgrey" Text="Present Address"
                                                                                                    meta:resourcekey="lblPresentAddrResource1"></asp:Label>
                                                                                                <span class="required">*</span>                                                                                            </td>
                                                                                            <td abbr="left">
                                                                                                <asp:TextBox ID="txtAddress" runat="server" TabIndex="5" Width="300px"
                                                                                                    TextMode="MultiLine" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                                                                                                <asp:Label ID="lblspchar" CssClass="ariallightgrey" runat="server" Text="Characters allowed A-Z,a-z,0-9,/-\."></asp:Label>
                                                                                                &nbsp;
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress"
                                                                                                    ErrorMessage="Enter Present Address" SetFocusOnError="True" Width="1px" meta:resourcekey="RequiredFieldValidator5Resource1">*</asp:RequiredFieldValidator>                                                                                            </td>
                                                                                            <td rowspan="4">
                                                                                                <input type="button" id="copyaddr" runat="server" value="Copy" onClick="javascript:CopyAddress();" />                                                                                            </td>
                                                                                            <td align="left" style="width: 150px">
                                                                                                <asp:Label ID="lblPerAddr" runat="server" CssClass="ariallightgrey" Text="Permanent Address"
                                                                                                    meta:resourcekey="lblPerAddrResource1"></asp:Label>
                                                                                                <span class="required">*</span>                                                                                            </td>
                                                                                            <td abbr="left">
                                                                                                <asp:TextBox ID="txtAddress_per" runat="server"  TabIndex="8" Width="300px"
                                                                                                    TextMode="MultiLine" meta:resourcekey="txtAddress_perResource1"></asp:TextBox>
                                                                                                <asp:Label ID="lblspchar2" CssClass="ariallightgrey" runat="server" Text="Characters allowed A-Z,a-z,0-9,/-\."></asp:Label>
                                                                                                &nbsp;
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtAddress_per"
                                                                                                    ErrorMessage="Enter Permanent Address" SetFocusOnError="True" Width="1px" meta:resourcekey="RequiredFieldValidator22Resource1">*</asp:RequiredFieldValidator>                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                                <asp:Label ID="lblDistrict" runat="server" CssClass="ariallightgrey" Text="District"
                                                                                                    meta:resourcekey="lblDistrictResource1"></asp:Label>
                                                                                                <span class="required">*</span>                                                                                            </td>
                                                                                            <td align="left" colspan="1">
                                                                                                <asp:DropDownList ID="ddlDistrict" runat="server"  Font-Names="Arial Unicode MS"
                                                                                                    TabIndex="6" Width="150px" meta:resourcekey="ddlDistrictResource1" 
                                                                                                    AutoPostBack="True">                                                                                                </asp:DropDownList>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="ddlDistrict"
                                                                                                    runat="server" SetFocusOnError="True" InitialValue="0" ErrorMessage="Select District">*</asp:RequiredFieldValidator>                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label ID="lblDistrictPer" runat="server" CssClass="ariallightgrey" Text="District"
                                                                                                    meta:resourcekey="lblDistrictPerResource1"></asp:Label>                                                                                            </td>
                                                                                            <td align="left" colspan="1">
                                                                                                <asp:DropDownList ID="ddlDistrict_per" runat="server"  Font-Names="Arial Unicode MS"
                                                                                                    TabIndex="9" Width="150px" meta:resourcekey="ddlDistrict_perResource1" 
                                                                                                    AutoPostBack="True">                                                                                                </asp:DropDownList>                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                                <asp:Label ID="lblPin1" runat="server" CssClass="ariallightgrey" Text="PIN Code"
                                                                                                    meta:resourcekey="lblPin1Resource1"></asp:Label>                                                                                            </td>
                                                                                            <td align="left" colspan="1">
                                                                                                <asp:TextBox ID="txtPIN" runat="server"  MaxLength="6" TabIndex="7"
                                                                                                    Width="145px" meta:resourcekey="txtPINResource1"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPIN"
                                                                                                    ErrorMessage="Enter PIN Code" Display="Dynamic" SetFocusOnError="True" Width="1px"
                                                                                                    meta:resourcekey="RequiredFieldValidator11Resource1">*</asp:RequiredFieldValidator>
                                                                                                <asp:RangeValidator ID="rngvalidate1" ControlToValidate="txtPIN" ErrorMessage="Enter Correct PIN"
                                                                                                    MaximumValue="999999" MinimumValue="100000" runat="server" Type="Integer" meta:resourcekey="rngvalidate1Resource1"></asp:RangeValidator>                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Label ID="lblPin2" runat="server" CssClass="ariallightgrey" Text="Pin Code"
                                                                                                    meta:resourcekey="lblPin2Resource1"></asp:Label>                                                                                            </td>
                                                                                            <td align="left" colspan="1">
                                                                                                <asp:TextBox ID="txtPIN_per" runat="server" MaxLength="6" TabIndex="10"
                                                                                                    Width="150px" meta:resourcekey="txtPIN_perResource1"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="rfvPIN2" runat="server" ControlToValidate="txtPIN"
                                                                                                    ErrorMessage="Enter PIN Code" Display="Dynamic" SetFocusOnError="True" Width="1px"
                                                                                                    meta:resourcekey="rfvPIN2Resource1">*</asp:RequiredFieldValidator>
                                                                                                <asp:RangeValidator ID="rvPin" ControlToValidate="txtPIN_per" ErrorMessage="Enter Correct PIN"
                                                                                                    MaximumValue="999999" MinimumValue="100000" runat="server" Type="Integer" meta:resourcekey="rvPinResource1"></asp:RangeValidator>                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>                                                                                </td>
                                                                            </tr>
                                                                        </table>                                                                    </td>
                                                                </tr>
                                                                <!-----------------------Cast-------------------------------------->
                                                                <tr class="trFloral">
                                                                    <td colspan="4">                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                  <td colspan="4" align="left"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                      <td><table
                                                            width="100%" border="0" cellpadding="1" cellspacing="0" bgcolor="#d2e2b7" class="greydotRED">
                                                                        <tbody>
                                                                          <tr class="trFloral">
                                                                            <td align="left" colspan="1" style="width: 200px"  valign="top"><asp:Label ID="lblMobileNO" runat="server" CssClass="ariallightgrey" Text="Mobile No"
                                                                            meta:resourcekey="lblMobileNOResource1"></asp:Label>
                                                                            </td>
                                                                            <td align="left" colspan="1" valign="top"><asp:TextBox ID="txtMobileNo" runat="server"  TabIndex="11" Width="100px"
                                                                            MaxLength="10" meta:resourcekey="txtMobileNoResource1"></asp:TextBox>
                                                                                <asp:Label ID="lblmobmsg" meta:resourcekey="lblmobmsgResource1" runat="server" CssClass="ariallightgrey"
                                                                            Text="To get SMS alert"></asp:Label>
                                                                                <asp:CompareValidator ID="check" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Enter valide Mobile No"
                                                                            Operator="DataTypeCheck" Type="Double" Display="Dynamic" meta:resourcekey="checkResource1">*</asp:CompareValidator>
                                                                                <asp:RegularExpressionValidator ID="revtelephone" runat="server" ControlToValidate="txtMobileNo"
                                                                            Display="Dynamic" ValidationExpression="^[0-9\s\-\+]{10}$" SetFocusOnError="True"
                                                                            ErrorMessage="Enter valide Mobile No" meta:resourcekey="revtelephoneResource1">*</asp:RegularExpressionValidator>
                                                                            </td>
                                                                            <td align="left"><asp:Label ID="lblEmail" runat="server" CssClass="ariallightgrey" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                                            </td>
                                                                            <td align="left"><asp:TextBox ID="txtEmail" runat="server"  TabIndex="12" 
                                                                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Email Address is not Valid" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                            </td>
                                                                          </tr>
                                                                          <tr class="trFloral">
                                                                            <td align="left" style="width: 200px"><asp:Label ID="lblState" runat="server" CssClass="ariallightgrey" Text="State" meta:resourcekey="lblStateResource1"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 220px"><asp:TextBox ID="txtState" runat="server"  Text="Delhi" abIndex="13" Width="150px"
                                                                            meta:resourcekey="txtStateResource1">Delhi</asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtState"
                                                                            ErrorMessage="Enter State" SetFocusOnError="True" Width="1px" meta:resourcekey="RequiredFieldValidator12Resource1">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td align="left" colspan="1"><asp:Label ID="lblNationality" runat="server" CssClass="ariallightgrey" Text="Nationality"
                                                                            meta:resourcekey="lblNationalityResource1"></asp:Label>
                                                                            </td>
                                                                            <td align="left" colspan="1"><asp:TextBox ID="txtNationality" runat="server"  TabIndex="14"
                                                                            Width="150px" meta:resourcekey="txtNationalityResource1">Indian</asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtNationality"
                                                                            ErrorMessage="Enter Nationality" SetFocusOnError="True" Width="1px" meta:resourcekey="RequiredFieldValidator13Resource1">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                          </tr>
                                                                          <tr class="trFloral">
                                                                            <td align="left" style="width: 200px"><asp:Label ID="lblGender" runat="server" CssClass="ariallightgrey" Text="Gender"
                                                                            meta:resourcekey="lblGenderResource1"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 210px"><asp:RadioButtonList ID="rbGender" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                                                                            TabIndex="15" CssClass="ariallightgrey" meta:resourcekey="rbGenderResource1">
                                                                                <asp:ListItem Value="M"  meta:resourcekey="ListItemResource5">Male</asp:ListItem>
                                                                                <asp:ListItem Value="F" meta:resourcekey="ListItemResource6">Female</asp:ListItem>
                                                                              </asp:RadioButtonList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rbGender"
                                                                            ErrorMessage="Select Gender" SetFocusOnError="True" Width="1px">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td align="left" colspan="1"><asp:Label ID="lblMeritalStatus" runat="server" CssClass="ariallightgrey" Text="Marital Status"
                                                                            meta:resourcekey="lblMeritalStatusResource1"></asp:Label>
                                                                            </td>
                                                                            <td align="left" colspan="1"><asp:RadioButtonList ID="rbMeritalStatus" 
                                                                                    runat="server" RepeatDirection="Horizontal"
                                                                            TabIndex="16" CssClass="ariallightgrey" 
                                                                                    meta:resourcekey="rbMeritalStatusResource1" AutoPostBack="True">
                                                                                <asp:ListItem Selected="True" Value="M" meta:resourcekey="ListItemResource7">Married</asp:ListItem>
                                                                                <asp:ListItem Value="U" meta:resourcekey="ListItemResource8">Unmarried</asp:ListItem>
                                                                                <asp:ListItem Value="S" meta:resourcekey="ListItemResource9">Separated</asp:ListItem>
                                                                              </asp:RadioButtonList>
                                                                            </td>
                                                                          </tr>
                                                                          <tr class="trFloral">
                                                                            <td align="left" style="width: 200px" valign="top"><asp:Label ID="lblDOB" runat="server" CssClass="ariallightgrey" Text="Date of Birth"
                                                                            meta:resourcekey="lblDOBResource1"></asp:Label>
                                                                                <span class="required">*</span> </td>
                                                                            <td align="left" style="width: 220px" valign="top"><asp:TextBox ID="txtBirthDt" runat="server" TabIndex="17" Width="70px"></asp:TextBox>
                                                                                <asp:Label ID="lblDayFormat" runat="server" CssClass="ariallightgrey" meta:resourcekey="lblDayFormatResource1">(dd/mm/yyyyy)</asp:Label>
                                                                                <asp:RequiredFieldValidator ID="reqDob" ControlToValidate="txtBirthDt" Display="Dynamic"
                                                                            runat="server" ErrorMessage="Enter Birth Date">*</asp:RequiredFieldValidator>
                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtBirthDt"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                                                            meta:resourcekey="CompareValidator1Resource1">*</asp:CompareValidator>
                                                                            </td>
                                                                            <td></td>
                                                                            <td></td>
                                                                          </tr>
                                                                          <!-----------------------Cast-------------------------------------->
                                                                          <!-----------------------End Cast---------------------------------->
                                                                          <!-----------------Debard by DSSSB----------------------------------->
                                                                        </tbody>
                                                                      </table></td>
                                                                    </tr>
                                                                  </table></td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td align="left" style="width: 200px">
                                                                        <asp:Label ID="lblCasteCategory" runat="server" CssClass="ariallightgrey" Text="Caste Category"
                                                                            meta:resourcekey="lblCasteCategoryResource1"></asp:Label>
                                                                        <span class="required">*</span>                                                                    </td>
                                                                    <td align="left" style="width: 220px" colspan="3">
                                                                        <asp:DropDownList ID="ddlCategory" AutoPostBack="True" runat="server" CssClass="ComboBox"
                                                                            Font-Names="Arial Unicode MS" TabIndex="18" Width="150px" 
                                                                            meta:resourcekey="ddlCategoryResource1">                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlCategory"
                                                                            runat="server" SetFocusOnError="True" InitialValue="0" ErrorMessage="Select Category">*</asp:RequiredFieldValidator>                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td colspan="4">
                                                                        <table width="100%" border="0" style="width: 100%">
                                                                            <tr>
                                                                                <td align="left" colspan="1">
                                                                                    <asp:Label ID="lblCLCNo" runat="server" CssClass="ariallightgrey" Text="Non-Creamy Layer Certificate No."
                                                                                        meta:resourcekey="lblCLCNoResource1"></asp:Label>                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:TextBox ID="txtCLCNo" runat="server" TabIndex="20" Width="70px" meta:resourcekey="txtCLCNoResource1"></asp:TextBox>                                                                                </td>
                                                                                <td align="left" colspan="1" style="width: 370px; text-align: right">
                                                                                    <asp:Label ID="lblCLCDate" runat="server" CssClass="ariallightgrey" Text="Non-Creamy Layer Certificate Date"
                                                                                        meta:resourcekey="lblCLCDateResource1"></asp:Label>                                                                                </td>
                                                                                <td align="left" colspan="1">
                                                                                    <asp:TextBox ID="txtCLCDate" runat="server" TabIndex="21" Width="70px" meta:resourcekey="txtCLCDateResource1"></asp:TextBox>
                                                                                    
                                                                                    <asp:Label ID="Label34" runat="server" CssClass="ariallightgrey" meta:resourcekey="Label34Resource1">(dd/mm/yyyyy)</asp:Label>
                                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtCLCDate"
                                                                                        ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                                                                        meta:resourcekey="CompareValidator3Resource1">*</asp:CompareValidator>                                                                                </td>
                                                                                <td></td>
                                                                            </tr>
                                                                      </table>                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td colspan="4">
                                                                        <table width="100%" border="0" style="width: 100%">
                                                                            <tr>
                                                                                <td align="left" width="25%">
                                                                                    <asp:Label ID="lblCerIssuingState" runat="server" CssClass="ariallightgrey" Text="Caste Certificate issuing State"></asp:Label>                                                                                </td>
                                                                                <td align="left">
                                                                                    &nbsp;
                                                                                    <asp:DropDownList ID="ddlState" runat="server" Font-Names="Arial Unicode MS"
                                                                                        Width="150px" AutoPostBack="True">                                                                                    </asp:DropDownList>                                                                                </td>
                                                                            </tr>
                                                                      </table>                                                                    </td>
                                                                </tr>
                                                                <!-----------------------End Cast---------------------------------->
                                                                
                                                              
                                                                
                                                               
                                                                <tr class="trFloral">
                                                                    <td align="left" style="width: 200px" valign="top">
                                                                        <asp:Label ID="lblPHChallenaged" runat="server" CssClass="ariallightgrey" Text="Physically Challenged"
                                                                            meta:resourcekey="lblPHChallenagedResource1"></asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="lblPHChallenaged1" runat="server" CssClass="ariallightgrey" Text="(As Per Published Advertisement Percentage)"
                                                                            meta:resourcekey="lblPHChallenaged1Resource1"></asp:Label>                                                                    </td>
                                                                    
                                                                    <td align="left" style="width: 450px">
                                                                    <table width="100%">
                                                                    <tr>
                                                                    <td><asp:RadioButtonList ID="rbphC" runat="server" RepeatDirection="Horizontal" 
                                                                            AutoPostBack="True" 
                                                                            onselectedindexchanged="rbphC_SelectedIndexChanged">
                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td>
                                                                        <asp:CheckBoxList ID="rbPh" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                                                                            TabIndex="25" RepeatColumns="3" CssClass="ariallightgrey" meta:resourcekey="rbPhResource1" Visible="false">
                                                                          <%--  <asp:ListItem Value="Orthopedics" meta:resourcekey="ListItemResource12">Orthopedics(OA, OL, BL, OAL, BLV, HH)</asp:ListItem>
                                                                            <asp:ListItem Value="Deaf and Dumb">Deaf and Dumb</asp:ListItem>
                                                                            <asp:ListItem Value="Blindness" meta:resourcekey="ListItemResource14">Blindness</asp:ListItem>
                                                                            <asp:ListItem Value="Not Applicable" Selected="True" meta:resourcekey="ListItemResource15">Not Applicable</asp:ListItem>--%>
                                                                        </asp:CheckBoxList>                                                                      </td>
                                                                      </tr>
                                                                      </table>                                                                    </td>
                                                                    <td align="left" colspan="1" style="width: 290px; text-align: right">
                                                                        <asp:Label ID="lblBasicKnowledge" runat="server" CssClass="ariallightgrey" Text="Basic Knowledge of Computer"
                                                                            meta:resourcekey="lblBasicKnowledgeResource1"></asp:Label>                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <table border="0" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rbCCC" runat="server" RepeatDirection="Horizontal" TabIndex="26"
                                                                                        CssClass="ariallightgrey" meta:resourcekey="rbCCCResource1" 
                                                                                        AutoPostBack="True">
                                                                                        <asp:ListItem Value="1" Selected="True" meta:resourcekey="ListItemResource16">Yes</asp:ListItem>
                                                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource17">No</asp:ListItem>
                                                                                    </asp:RadioButtonList>                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;<%--<a href="#">Show GR</a>--%>                                                                                </td>
                                                                            </tr>
                                                                        </table>                                                                    </td>
                                                                </tr>
                                                                <tr id="phPercentage" visible="false" runat="server" class="trFloral">
                                                                    <td align="left" colspan="3" style="width: 500px;" >
                                                                        <asp:Label ID="Label4" CssClass="ariallightgrey" runat="server"
                                                                            Text="Enter % level of PH"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtPercLevelPH" Width="50px" runat="server"></asp:TextBox></td>
                                                                    <td>                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td align="left" style="width: 200px">
                                                                        <asp:Label ID="lblWidow" runat="server" CssClass="ariallightgrey" Text="Widow" meta:resourcekey="lblWidowResource1"></asp:Label>                                                                    </td>
                                                                    <td align="left" colspan="4">
                                                                        <asp:RadioButtonList ID="rbWidow" runat="server" RepeatDirection="Horizontal" TabIndex="27"
                                                                            CssClass="ariallightgrey" meta:resourcekey="rbWidowResource1" 
                                                                            AutoPostBack="True">
                                                                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource18">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource19">No</asp:ListItem>
                                                                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource20">Remarriage</asp:ListItem>
                                                                            <asp:ListItem Value="3" Selected="True" meta:resourcekey="ListItemResource40">Not Applicable</asp:ListItem>
                                                                        </asp:RadioButtonList>                                                                    </td>
                                                                    <%-- <td align="left" colspan="1" style="width: 290px; text-align: right">
                                                                        </td>
                                                                        <td align="left" colspan="1">
                                                                        </td>--%>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td colspan="5">
                                                                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                                                            <ContentTemplate>--%>
                                                                        <table width="100%" border="0">
                                                                            <tr>
                                                                                <td align="left" style="width: 350px">
                                                                                    <asp:Label ID="lblExservice" runat="server" CssClass="ariallightgrey" Text="Ex-serviceman"
                                                                                        meta:resourcekey="lblExserviceResource1"></asp:Label>                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:RadioButtonList ID="rbExservice" runat="server" RepeatDirection="Horizontal"
                                                                                        TabIndex="28" CssClass="ariallightgrey" AutoPostBack="True" 
                                                                                        meta:resourcekey="rbExserviceResource1" 
                                                                                        onselectedindexchanged="rbExservice_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="true" meta:resourcekey="ListItemResource21">Yes</asp:ListItem>
                                                                                        <asp:ListItem Value="false" Selected="True" meta:resourcekey="ListItemResource22">No</asp:ListItem>
                                                                                    </asp:RadioButtonList>                                                                                </td>
                                                                                <td align="left" colspan="1">
                                                                                    <asp:Label ID="lblExFromDt" runat="server" CssClass="ariallightgrey" Text="From Date"
                                                                                        meta:resourcekey="lblExFromDtResource1"></asp:Label>                                                                                </td>
                                                                                <td align="left" colspan="1">
                                                                                    <asp:TextBox ID="txtExFromDt" runat="server"  TabIndex="29" Width="80px"
                                                                                        meta:resourcekey="txtExFromDtResource1"></asp:TextBox>
                                                                                    <%--<asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                                                        MaskType="Date" TargetControlID="txtExFromDt" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                                        CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                                    </asp:MaskedEditExtender>--%>                                                                                </td>
                                                                                <td align="left" colspan="1">
                                                                                    <asp:Label ID="lblExToDt" runat="server" CssClass="ariallightgrey" Text="To Date"
                                                                                        meta:resourcekey="lblExToDtResource1"></asp:Label>                                                                                </td>
                                                                                <td align="left" colspan="1">
                                                                                    <asp:TextBox ID="txtExToDt" runat="server" CssClass="TextBox" TabIndex="30" Width="80px"
                                                                                        meta:resourcekey="txtExToDtResource1"></asp:TextBox>
                                                                                   <%-- <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
                                                                                        MaskType="Date" TargetControlID="txtExToDt" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                                        CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                                    </asp:MaskedEditExtender>--%>                                                                                </td>
                                                                            </tr>
                                                                      </table>
                                                                        <%--</ContentTemplate>
                                                                        </asp:UpdatePanel>--%>                                                                    </td>
                                                                </tr>
                                                                <!-----------------Debard by DSSSB----------------------------------->
                                                                <tr class="trFloral">
                                                                    <td colspan="5">
                                                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                                                                            <ContentTemplate>--%>
                                                                                <table width="100%" border="0">
                                                                                    <tr>
                                                                                        <td align="left" style="width: 350px">
                                                                                            <asp:Label ID="lblDebard" runat="server" CssClass="ariallightgrey" Text="Have you ever been Debarred by Board OR any other agency?"
                                                                                                meta:resourcekey="lblDebardResource1"></asp:Label>                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:RadioButtonList ID="rbDisqualify" TabIndex="58" runat="server" CssClass="ariallightgrey"
                                                                                                RepeatDirection="Horizontal" AutoPostBack="True" 
                                                                                                meta:resourcekey="rbDisqualifyResource1" onselectedindexchanged="rbDisqualify_SelectedIndexChanged" 
                                                                                                >
                                                                                                <asp:ListItem Value="true" Text="Yes" meta:resourcekey="ListItemResource32"></asp:ListItem>
                                                                                                <asp:ListItem Value="false" Text="No" Selected="True" meta:resourcekey="ListItemResource33"></asp:ListItem>
                                                                                            </asp:RadioButtonList>                                                                                        </td>
                                                                                        <td align="left" colspan="1">
                                                                                            <asp:Label ID="lblOrderDt" runat="server" CssClass="ariallightgrey" Text="Order Date"
                                                                                                meta:resourcekey="lblOrderDtResource1"></asp:Label>                                                                                        </td>
                                                                                        <td align="left" colspan="1">
                                                                                            <asp:TextBox ID="txtDebardDt" runat="server" TabIndex="59" Width="70px" meta:resourcekey="txtDebardDtResource1"></asp:TextBox>
                                                                                            <asp:Label ID="Label71" runat="server" CssClass="ariallightgrey" meta:resourcekey="Label71Resource1">(dd/mm/yyyyy)</asp:Label>
                                                                                            <asp:CompareValidator ID="CompareValidator20" runat="server" ControlToValidate="txtDebardDt"
                                                                                                ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                                                                                meta:resourcekey="CompareValidator20Resource1">*</asp:CompareValidator>
                                                                                            <%--<asp:MaskedEditExtender ID="MaskedEditExtender22" runat="server" Mask="99/99/9999"
                                                                                                MaskType="Date" TargetControlID="txtDebardDt" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                                                CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                                            </asp:MaskedEditExtender>--%>                                                                                        </td>
                                                                                        <td align="right" colspan="1">
                                                                                            <asp:Label ID="lblNoYear" runat="server" CssClass="ariallightgrey" Text="No. of Years"
                                                                                                meta:resourcekey="lblNoYearResource1"></asp:Label>                                                                                        </td>
                                                                                        <td align="left" colspan="1">
                                                                                            <asp:TextBox ID="txtDebardYr" runat="server" CssClass="TextBox" TabIndex="2" Width="60px"
                                                                                                meta:resourcekey="txtDebardYrResource1"></asp:TextBox>                                                                                        </td>
                                                                                    </tr>
                                                                      </table>
                                                                            <%--</ContentTemplate>
                                                                        </asp:UpdatePanel>--%>                                                                    </td>
                                                                </tr>
                                                                                              <tr class="trFloral">
                                                                    <td colspan="5" align="left">
                                                                        <%--<asp:UpdatePanel ID="UpdatePanel222" runat="server" RenderMode="Inline">
                                                                            <ContentTemplate>--%>
                                                                                <table bgcolor="#d2e2b7" border="0" cellpadding="1" cellspacing="0" class="greydotRED"
                                                                                    width="100%">
                                                                                    <tbody>
                                                                                        <tr class="trLightBlue">
                                                                                            <td>
                                                                                                <asp:Label ID="lblCurrentGovtEmp" runat="server" CssClass="ariallightgrey" Text="For the Delhi State Govt. employee"
                                                                                                    meta:resourcekey="lblCurrentGovtEmpResource1"></asp:Label>                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trFloral">
                                                                                            <td>
                                                                                                <table width="100%" border="0">
                                                                                                    <tr>
                                                                                                        <td align="right" style="width: 350px">
                                                                                                            <asp:Label ID="lblGovtEmp" runat="server" CssClass="ariallightgrey" Text="Currently working as Delhi State Goverment Employee"
                                                                                                                meta:resourcekey="lblGovtEmpResource1"></asp:Label>                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <asp:RadioButtonList ID="rbGovtEmployee" runat="server" RepeatDirection="Horizontal"
                                                                                                                TabIndex="31" CssClass="ariallightgrey" AutoPostBack="True" meta:resourcekey="rbGovtEmployeeResource1">
                                                                                                                <asp:ListItem Value="1" meta:resourcekey="ListItemResource23">Yes</asp:ListItem>
                                                                                                                <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource24">No</asp:ListItem>
                                                                                                            </asp:RadioButtonList>                                                                                                        </td>
                                                                                                        <td align="left" colspan="1">
                                                                                                            <asp:Label ID="lblGovtJoinDt" runat="server" CssClass="ariallightgrey" Text="   Joining Date in Goverment Service"
                                                                                                                meta:resourcekey="lblGovtJoinDtResource1"></asp:Label>                                                                                                        </td>
                                                                                                        <td align="left" colspan="1">
                                                                                                            <asp:TextBox ID="txtGovtJoinDt" runat="server" CssClass="TextBox" TabIndex="32" Width="80px"
                                                                                                                meta:resourcekey="txtGovtJoinDtResource1"></asp:TextBox>
                                                                                                           <%-- <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                                                                                                MaskType="Date" TargetControlID="txtGovtJoinDt" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                                                                CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                                                            </asp:MaskedEditExtender>--%>                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="right" style="width: 350px">
                                                                                                            <asp:Label ID="lblFeeder" runat="server" CssClass="ariallightgrey" Text="Are you in feeder cader of respective post?"
                                                                                                                meta:resourcekey="lblFeederResource1"></asp:Label>                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <asp:RadioButtonList ID="rbFeder" runat="server" RepeatDirection="Horizontal" TabIndex="33"
                                                                                                                CssClass="ariallightgrey" AutoPostBack="True" meta:resourcekey="rbFederResource1">
                                                                                                                <asp:ListItem Value="1" meta:resourcekey="ListItemResource25">Yes</asp:ListItem>
                                                                                                                <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource26">No</asp:ListItem>
                                                                                                            </asp:RadioButtonList>                                                                                                        </td>
                                                                                                        <td colspan="2">                                                                                                        </td>
                                                                                                    </tr>
                                                                                              </table>                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                      </table>
                                                                            <%--</ContentTemplate>
                                                                        </asp:UpdatePanel>--%>                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td colspan="4">
                                                                        <table width="100%" border="0">
                                                                            <tr>
                                                                                <td width="119" align="left" valign="top" style="width: 300px">
                                                                                    <asp:Label ID="lblLangProf" runat="server" CssClass="ariallightgrey" Text=" Language Proficiency"
                                                                                        meta:resourcekey="lblLangProfResource1"></asp:Label>                                                                              </td>
                                                                                <td width="1074" align="left">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr class="trLightBlue">
                                                                                            <td>
                                                                                                <asp:Label ID="lblLang" runat="server" Text="Language" CssClass="ariallightgrey"
                                                                                                    meta:resourcekey="lblLangResource1"></asp:Label>                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:Label ID="lblRead" runat="server" Text="Read" CssClass="ariallightgrey" meta:resourcekey="lblReadResource1"></asp:Label>                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:Label ID="lblWrite" runat="server" Text="Write" CssClass="ariallightgrey" meta:resourcekey="lblWriteResource1"></asp:Label>                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:Label ID="lblSpeak" runat="server" Text="Speak" CssClass="ariallightgrey" meta:resourcekey="lblSpeakResource1"></asp:Label>                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trFloral">
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtLang_eng" runat="server" Width="70px" ReadOnly="True" Text="English"
                                                                                                    meta:resourcekey="txtLang_engResource1"></asp:TextBox>                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:CheckBox ID="cbLang_eng_read" runat="server" meta:resourcekey="cbLang_eng_readResource1" />                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:CheckBox ID="cbLang_eng_Write" runat="server" meta:resourcekey="cbLang_eng_WriteResource1" />                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:CheckBox ID="cbLang_eng_Speak" runat="server" meta:resourcekey="cbLang_eng_SpeakResource1" />                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trFloral">
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtLang_hin" runat="server" Width="70px" ReadOnly="True" Text="Hindi"
                                                                                                    meta:resourcekey="txtLang_hinResource1"></asp:TextBox>                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:CheckBox ID="cbLang_hin_read" runat="server" meta:resourcekey="cbLang_hin_readResource1" />                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:CheckBox ID="cbLang_hin_Write" runat="server" meta:resourcekey="cbLang_hin_WriteResource1" />                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:CheckBox ID="cbLang_hin_Speak" runat="server" meta:resourcekey="cbLang_hin_SpeakResource1" />                                                                                            </td>
                                                                                        </tr>
                                                                                  </table>                                                                              </td>
                                                                            </tr>
                                                                      </table>                                                                    </td>
                                                                </tr>
                                                                                                                           </tbody>
                                                        </table>
                                               
                                                
</asp:Panel>  
<asp:Panel ID="panEdu" runat="server">                                              
                                                
                                                        <div align="left" style="background-color:Aqua">
                                                            Education Qualifications</div>
                                                        <div align="right">
                                                            <b></b></div>
                                                    
                                                        <table bgcolor="#d2e2b7" border="0" cellpadding="1" cellspacing="0" class="greydotRED"
                                                            width="100%">
                                                            <tbody>
                                                                <tr class="trFloral">
                                                                    <td align="left">
                                                                        <asp:Label ID="lblEDetails" runat="server" CssClass="ariallightgrey" Text="Education Detail"
                                                                            meta:resourcekey="lblEDetailsResource1"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:Label ID="lblPercentage" runat="server" CssClass="ariallightgrey" Font-Bold="True"
                                                                            Text="Percentage/Percentile" meta:resourcekey="lblPercentageResource1"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:Label ID="lblEduClass" runat="server" CssClass="ariallightgrey" Font-Bold="True"
                                                                            Text="Class" meta:resourcekey="lblEduClassResource1"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:Label ID="lblExamBody" runat="server" meta:resourcekey="lblExamBodyResource1"
                                                                            CssClass="ariallightgrey" Text="Exam body"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:Label ID="lbleducState" runat="server" meta:resourcekey="lbleducStateResource1"
                                                                            CssClass="ariallightgrey" Text="State"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:Label ID="lblPassYear" runat="server" meta:resourcekey="lblPassYearResource1"
                                                                            CssClass="ariallightgrey" Text="Passing Year"></asp:Label>
                                                                        <asp:DropDownList ID="ddlMinClass" runat="server" Visible="false" 
                                                                            AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlEdu" AutoPostBack="True" runat="server" CssClass="ComboBox"
                                                                            Font-Names="Arial Unicode MS" TabIndex="53" 
                                                                            meta:resourcekey="ddlEduResource1">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEdu" ControlToValidate="ddlEdu"
                                                                            runat="server" SetFocusOnError="True" InitialValue="0" ErrorMessage="Education Qualification - Education Details">*</asp:RequiredFieldValidator>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEduOther" runat="server" CssClass="TextBox" Width="120px" Visible="False"
                                                                            meta:resourcekey="txtEduOtherResource1"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:TextBox ID="txtPerc3" runat="server" CssClass="TextBox" TabIndex="54" MaxLength="5"
                                                                            Width="70px" meta:resourcekey="txtPerc3Resource1"></asp:TextBox>
                                                                        <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtPerc3" ErrorMessage="Enter correct Percentage."
                                                                            MinimumValue="00.00" MaximumValue="99.99" runat="server" Type="Double"></asp:RangeValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13455" ControlToValidate="txtPerc3"
                                                                            runat="server" SetFocusOnError="True" InitialValue="0" ErrorMessage="Education Qualification - Percentage">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:DropDownList ID="ddlClass3" runat="server" CssClass="ComboBox" Font-Names="Arial Unicode MS"
                                                                            TabIndex="55" Width="150px" meta:resourcekey="ddlClass3Resource1" 
                                                                            AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator InitialValue="-" ID="RequiredFieldValidator367" ControlToValidate="ddlClass3"
                                                                            runat="server" SetFocusOnError="True" ErrorMessage="Education Qualification - Class">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:TextBox ID="txtBoard3" runat="server" CssClass="TextBox" TabIndex="56" Width="100px"
                                                                            Text="University" meta:resourcekey="txtBoard3Resource1"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtBoard3"
                                                                            runat="server" SetFocusOnError="True" ErrorMessage="Education Qualification - University">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:TextBox ID="txtState3" runat="server" CssClass="TextBox" TabIndex="57" Width="100px"
                                                                            Text=""></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtState3"
                                                                            runat="server" SetFocusOnError="True" ErrorMessage="Education Qualification - State">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:TextBox ID="txtYear3" MaxLength="4" runat="server" CssClass="TextBox" TabIndex="58"
                                                                            Width="100px" meta:resourcekey="txtYear3Resource1"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtYear3"
                                                                            runat="server" SetFocusOnError="True" ErrorMessage="Education Qualification - Passing Year">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr class="trFloral" id="edu4" runat="server" visible="false">
                                                                    <td id="Td1" align="left" colspan="2" runat="server">
                                                                        <asp:DropDownList ID="ddlEdu4" AutoPostBack="false" runat="server" CssClass="ComboBox"
                                                                            Font-Names="Arial Unicode MS" TabIndex="60">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator165" ControlToValidate="ddlEdu4"
                                                                            runat="server" SetFocusOnError="True" InitialValue="0" ErrorMessage="Education Qualification - Education Details">*</asp:RequiredFieldValidator>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEduOther4" runat="server" CssClass="TextBox" TabIndex="61" Width="120px"
                                                                            Visible="False"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td2" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtPerc4" runat="server" CssClass="TextBox" TabIndex="62" MaxLength="5"
                                                                            Width="70px"></asp:TextBox>
                                                                        <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtPerc4" ErrorMessage="Enter correct Percentage."
                                                                            MinimumValue="00.00" MaximumValue="99.99" runat="server" Type="Double"></asp:RangeValidator>
                                                                    </td>
                                                                    <td id="Td3" align="left" colspan="1" runat="server">
                                                                        <asp:DropDownList ID="ddlClass4" runat="server" CssClass="ComboBox" Font-Names="Arial Unicode MS"
                                                                            TabIndex="63" Width="150px">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator InitialValue="-" ID="RequiredFieldValidator1455" ControlToValidate="ddlClass4"
                                                                            runat="server" SetFocusOnError="True" ErrorMessage="Education Qualification - Class">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td id="Td4" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtBoard4" runat="server" CssClass="TextBox" TabIndex="64" Width="100px"
                                                                            Text="University"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1567" ControlToValidate="txtBoard4"
                                                                            runat="server" SetFocusOnError="True" ErrorMessage="Education Qualification - University">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td id="Td5" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtState4" runat="server" CssClass="TextBox" TabIndex="65" Width="100px"
                                                                            Text=""></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1590" ControlToValidate="txtState4"
                                                                            runat="server" SetFocusOnError="True" ErrorMessage="Education Qualification - State">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td id="Td6" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtYear4" MaxLength="4" runat="server" CssClass="TextBox" TabIndex="66"
                                                                            Width="100px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15564" ControlToValidate="txtYear4"
                                                                            runat="server" SetFocusOnError="True" ErrorMessage="Education Qualification - Passing Year">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral" id="edu5" runat="server" visible="False">
                                                                    <td id="Td7" align="left" colspan="2" runat="server">
                                                                        <asp:DropDownList ID="ddlEdu5" AutoPostBack="false" runat="server" CssClass="ComboBox"
                                                                            Font-Names="Arial Unicode MS" TabIndex="67">
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEduOther5" runat="server" CssClass="TextBox" TabIndex="68" Width="120px"
                                                                            Visible="False"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td8" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtPerc5" runat="server" CssClass="TextBox" TabIndex="69" MaxLength="5"
                                                                            Width="70px"></asp:TextBox>
                                                                        <asp:RangeValidator ID="RangeValidator3" ControlToValidate="txtPerc5" ErrorMessage="Enter correct Percentage."
                                                                            MinimumValue="00.00" MaximumValue="99.99" runat="server" Type="Double"></asp:RangeValidator>
                                                                    </td>
                                                                    <td id="Td9" align="left" colspan="1" runat="server">
                                                                        <asp:DropDownList ID="ddlClass5" runat="server" CssClass="ComboBox" Font-Names="Arial Unicode MS"
                                                                            TabIndex="70" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td id="Td10" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtBoard5" runat="server" CssClass="TextBox" TabIndex="71" Width="100px"
                                                                            Text="University"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td11" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtState5" runat="server" CssClass="TextBox" TabIndex="72" Width="100px"
                                                                            Text=""></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td12" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtYear5" MaxLength="4" runat="server" CssClass="TextBox" TabIndex="73"
                                                                            Width="100px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral" id="edu6" runat="server" visible="False">
                                                                    <td id="Td13" align="left" colspan="2" runat="server">
                                                                        <asp:DropDownList ID="ddlEdu6" AutoPostBack="false" runat="server" CssClass="ComboBox"
                                                                            Font-Names="Arial Unicode MS" TabIndex="74">
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEduOther6" runat="server" CssClass="TextBox" TabIndex="75" Width="120px"
                                                                            Visible="False"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td14" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtPerc6" runat="server" CssClass="TextBox" TabIndex="76" MaxLength="5"
                                                                            Width="70px"></asp:TextBox>
                                                                        <asp:RangeValidator ID="RangeValidator4" ControlToValidate="txtPerc6" ErrorMessage="Enter correct Percentage."
                                                                            MinimumValue="00.00" MaximumValue="99.99" runat="server" Type="Double"></asp:RangeValidator>
                                                                    </td>
                                                                    <td id="Td15" align="left" colspan="1" runat="server">
                                                                        <asp:DropDownList ID="ddlClass6" runat="server" CssClass="ComboBox" Font-Names="Arial Unicode MS"
                                                                            TabIndex="77" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td id="Td16" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtBoard6" runat="server" CssClass="TextBox" TabIndex="78" Width="100px"
                                                                            Text="University"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td17" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtState6" runat="server" CssClass="TextBox" TabIndex="79" Width="100px"
                                                                            Text=""></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td18" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtYear6" MaxLength="4" runat="server" CssClass="TextBox" TabIndex="80"
                                                                            Width="100px"></asp:TextBox>
                                                                    </td>
                                                                </tr>--%>
                                                              
                                                                <tr class="trFloral">
                                                                    <td align="left" colspan="6" style="text-align: left;" valign="top">
                                                                        <table border="0">
                                                                            <tr>
                                                                                <td align="left" width="300px">
                                                                                    <asp:Button ID="btnAddEdu" CausesValidation="False" runat="server" Text="Add More Education"
                                                                                        TabIndex="81" Visible="false" CssClass="buttonStyle" meta:resourcekey="btnAddEduResource1" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                
                                                </asp:Panel>
                                                
                                               
                                                <asp:Panel ID="PanExperience" runat="server">
                                              
                                                        <div align="left" style="background-color:Aqua">
                                                            Experience Details (Starting from Old to Recent Most)</div>
                                                        <div align="right">
                                                            <b></b></div>
                                                   
                                                        <table bgcolor="#d2e2b7" border="0" cellpadding="1" cellspacing="0" class="greydotRED"
                                                            width="100%">
                                                            <tbody>
                                                                <tr class="trFloral">
                                                                    <td align="left" colspan="1" style="width: 5%; text-align: right">&nbsp;
                                                                        
                                                                    </td>
                                                                    <td align="left" colspan="1" style="width: 15%">
                                                                        <asp:Label ID="lblPostName" runat="server" CssClass="ariallightgrey" Text="Name of Post"
                                                                            Font-Bold="True" meta:resourcekey="lblPostNameResource1"></asp:Label>
                                                                    </td>
                                                                    <td align="center" colspan="1" style="width: 15%">
                                                                        <asp:Label ID="lblDateFrom" runat="server" CssClass="ariallightgrey" Text="Date From"
                                                                            Font-Bold="True" meta:resourcekey="lblDateFromResource1"></asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="lblDayFrom" runat="server" CssClass="ariallightgrey" meta:resourcekey="lblDayFromResource1">(dd/mm/yyyyy)</asp:Label>
                                                                    </td>
                                                                    <td align="center" colspan="1" style="width: 15%">
                                                                        <asp:Label ID="lblDateTo" runat="server" CssClass="ariallightgrey" Text="Date To"
                                                                            meta:resourcekey="lblDateToResource1"></asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="lblDayTo" runat="server" CssClass="ariallightgrey" meta:resourcekey="lblDayToResource1">(dd/mm/yyyyy)</asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1" style="width: 15%">
                                                                        <asp:Label ID="lblEmpName" runat="server" CssClass="ariallightgrey" Text="Employer Name"
                                                                            meta:resourcekey="lblEmpNameResource1"></asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="lblEmpContactNO" runat="server" CssClass="ariallightgrey" Text="Contact No"
                                                                            meta:resourcekey="lblEmpContactNOResource1"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1" style="width: 15%">
                                                                        <asp:Label ID="lblEmpAddr" runat="server" CssClass="ariallightgrey" Text="Employer Address"
                                                                            meta:resourcekey="lblEmpAddrResource1"></asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="lblEmpPIN" runat="server" CssClass="ariallightgrey" Text="PIN" meta:resourcekey="lblEmpPINResource1"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1" style="width: 15%">
                                                                        <asp:Label ID="Label3" runat="server" CssClass="ariallightgrey" Text="Salary Drwan"
                                                                            Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td align="left" colspan="1" style="text-align: right" valign="top">
                                                                        <asp:Label ID="Label48" runat="server" CssClass="ariallightgrey" Text="1." meta:resourcekey="Label48Resource1"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtPost1" runat="server" CssClass="TextBox" TabIndex="53" Width="120px"
                                                                            meta:resourcekey="txtPost1Resource1"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtDayFrom1" runat="server" TabIndex="54" Width="70px" meta:resourcekey="txtDayFrom1Resource1"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtDayFrom1"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                                                            meta:resourcekey="CompareValidator5Resource1">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayFrom1" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td align="center" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtDayTo1" runat="server" TabIndex="55" Width="70px" meta:resourcekey="txtDayTo1Resource1"></asp:TextBox>
                                                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtDayTo1"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                                                            meta:resourcekey="CompareValidator6Resource1">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayTo1" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td align="left" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtEmpName1" runat="server" CssClass="TextBox" TabIndex="56" Width="130px"
                                                                            meta:resourcekey="txtEmpName1Resource1"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtEmpName1"
                                                                            WatermarkText="Employer Name Here" WatermarkCssClass="watermarked" Enabled="True">
                                                                        </asp:TextBoxWatermarkExtender>--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpContact1" runat="server" CssClass="TextBox" TabIndex="57"
                                                                            Width="100px" meta:resourcekey="txtEmpContact1Resource1"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtEmpContact1"
                                                                            WatermarkText="Contact no Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td align="left" colspan="1">
                                                                        <asp:TextBox ID="txtEmpAddr1" runat="server" CssClass="TextBox" TabIndex="58" Width="130px"
                                                                            meta:resourcekey="txtEmpAddr1Resource1"></asp:TextBox><br />
                                                                        <asp:Label ID="lblSpChar3" CssClass="ariallightgrey" runat="server" Text="Characters allowed A-Z,a-z,0-9,/-\."></asp:Label>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtEmpAddr1"
                                                                            WatermarkText="Employer Address Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpPin1" runat="server" CssClass="TextBox" TabIndex="59" Width="100px"
                                                                            MaxLength="6" meta:resourcekey="txtEmpPin1Resource1"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtEmpPin1"
                                                                            WatermarkText="PIN CODE" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td align="left" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtEmpSal1" runat="server" CssClass="TextBox" TabIndex="58" Width="130px"
                                                                            meta:resourcekey="txtEmpAddr1Resource1"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral" id="trExper2" visible="False" runat="server">
                                                                    <td id="Td19" align="left" colspan="1" style="width: 200px; text-align: right" valign="top"
                                                                        runat="server">
                                                                        <asp:Label ID="Label49" runat="server" CssClass="ariallightgrey" Text="2."></asp:Label>
                                                                    </td>
                                                                    <td id="Td20" align="left" colspan="1" style="width: 233px" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtPost2" runat="server" CssClass="TextBox" TabIndex="60" Width="120px"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td21" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayFrom2" runat="server" TabIndex="61" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtDayFrom2"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender9" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayFrom2" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td22" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayTo2" runat="server" TabIndex="62" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtDayTo2"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender10" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayTo2" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td23" align="left" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtEmpName2" runat="server" CssClass="TextBox" TabIndex="63" Width="130px"></asp:TextBox>
                                                                       <%-- <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtEmpName2"
                                                                            WatermarkText="Emp Name Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpContact2" runat="server" CssClass="TextBox" TabIndex="64"
                                                                            Width="100px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtEmpContact2"
                                                                            WatermarkText="Conatc No Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td id="Td24" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpAddr2" runat="server" CssClass="TextBox" TabIndex="65" Width="130px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtEmpAddr2"
                                                                            WatermarkText="Emp Name Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpPin2" runat="server" CssClass="TextBox" TabIndex="66" Width="100px"
                                                                            MaxLength="6"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtEmpPin2"
                                                                            WatermarkText="PIN CODE" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td align="left" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtEmpSal2" runat="server" CssClass="TextBox" TabIndex="58" Width="130px"
                                                                            meta:resourcekey="txtEmpAddr1Resource1"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral" id="trExper3" visible="False" runat="server">
                                                                    <td id="Td25" align="left" colspan="1" style="width: 200px; text-align: right" valign="top"
                                                                        runat="server">
                                                                        <asp:Label ID="Label50" runat="server" CssClass="ariallightgrey" Text="3."></asp:Label>
                                                                    </td>
                                                                    <td id="Td26" align="left" colspan="1" style="width: 233px" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtPost3" runat="server" CssClass="TextBox" TabIndex="67" Width="120px"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td27" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayFrom3" runat="server" TabIndex="68" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtDayFrom3"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayFrom3" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td28" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayTo3" runat="server" TabIndex="69" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtDayTo3"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                       <%-- <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayTo3" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td29" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpName3" runat="server" CssClass="TextBox" TabIndex="70" Width="130px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtEmpName3"
                                                                            WatermarkText="Emp Name Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpContact3" runat="server" CssClass="TextBox" TabIndex="71"
                                                                            Width="100px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtEmpContact3"
                                                                            WatermarkText="Contact No Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td id="Td30" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpAddr3" runat="server" CssClass="TextBox" TabIndex="72" Width="130px"></asp:TextBox>
                                                                       <%-- <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtEmpAddr3"
                                                                            WatermarkText="Emp Address Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpPin3" runat="server" CssClass="TextBox" TabIndex="73" Width="100px"
                                                                            MaxLength="6"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="txtEmpPin3"
                                                                            WatermarkText="PIN CODE" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td align="left" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtEmpsal3" runat="server" CssClass="TextBox" TabIndex="58" Width="130px"
                                                                            meta:resourcekey="txtEmpAddr1Resource1"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral" id="trExper4" visible="False" runat="server">
                                                                    <td id="Td31" align="left" colspan="1" style="width: 200px; text-align: right" valign="top"
                                                                        runat="server">
                                                                        <asp:Label ID="Label55" runat="server" CssClass="ariallightgrey" Text="4."></asp:Label>
                                                                    </td>
                                                                    <td id="Td32" align="left" colspan="1" style="width: 233px" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtPost4" runat="server" CssClass="TextBox" TabIndex="74" Width="120px"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td33" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayFrom4" runat="server" TabIndex="75" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txtDayFrom4"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender13" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayFrom4" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td34" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayTo4" runat="server" TabIndex="76" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="txtDayTo4"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender14" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayTo4" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td35" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpName4" runat="server" CssClass="TextBox" TabIndex="77" Width="130px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="txtEmpName4"
                                                                            WatermarkText="Emp Name Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpContact4" runat="server" CssClass="TextBox" TabIndex="78"
                                                                            Width="100px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender15" runat="server" TargetControlID="txtEmpContact4"
                                                                            WatermarkText="Conatc No Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td id="Td36" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpAddr4" runat="server" CssClass="TextBox" TabIndex="79" Width="130px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender17" runat="server" TargetControlID="txtEmpAddr4"
                                                                            WatermarkText="Address Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpPin4" runat="server" CssClass="TextBox" TabIndex="80" Width="100px"
                                                                            MaxLength="6"></asp:TextBox>
                                                                       <%-- <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender19" runat="server" TargetControlID="txtEmpPin4"
                                                                            WatermarkText="PIN CODE" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td align="left" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtEmpSal4" runat="server" CssClass="TextBox" TabIndex="58" Width="130px"
                                                                            meta:resourcekey="txtEmpAddr1Resource1"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral" id="trExper5" visible="False" runat="server">
                                                                    <td id="Td37" align="left" colspan="1" style="width: 200px; text-align: right" valign="top"
                                                                        runat="server">
                                                                        <asp:Label ID="Label56" runat="server" CssClass="ariallightgrey" Text="5."></asp:Label>
                                                                    </td>
                                                                    <td id="Td38" align="left" colspan="1" style="width: 233px" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtPost5" runat="server" CssClass="TextBox" TabIndex="81" Width="120px"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td39" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayFrom5" runat="server" TabIndex="82" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtDayFrom5"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender15" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayFrom5" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td40" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayTo5" runat="server" TabIndex="83" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtDayTo5"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                    <%--    <asp:MaskedEditExtender ID="MaskedEditExtender16" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayTo5" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td41" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpName5" runat="server" CssClass="TextBox" TabIndex="84" Width="130px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtEmpName5"
                                                                            WatermarkText="Emp Name Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpContact5" runat="server" CssClass="TextBox" TabIndex="85"
                                                                            Width="100px"></asp:TextBox>
                                                                       <%-- <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender16" runat="server" TargetControlID="txtEmpContact5"
                                                                            WatermarkText="Conatc No Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td id="Td42" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpAddr5" runat="server" CssClass="TextBox" TabIndex="86" Width="130px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender18" runat="server" TargetControlID="txtEmpAddr5"
                                                                            WatermarkText="Address Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpPin5" runat="server" CssClass="TextBox" TabIndex="87" Width="100px"
                                                                            MaxLength="6"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender20" runat="server" TargetControlID="txtEmpPin5"
                                                                            WatermarkText="PIN CODE" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td align="left" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtEmpSal5" runat="server" CssClass="TextBox" TabIndex="58" Width="130px"
                                                                            meta:resourcekey="txtEmpAddr1Resource1"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral" id="trExper6" visible="False" runat="server">
                                                                    <td id="Td43" align="left" colspan="1" style="width: 200px; text-align: right" valign="top"
                                                                        runat="server">
                                                                        <asp:Label ID="lblpost6" runat="server" CssClass="ariallightgrey" Text="6."></asp:Label>
                                                                    </td>
                                                                    <td id="Td44" align="left" colspan="1" style="width: 233px" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtPost6" runat="server" CssClass="TextBox" TabIndex="88" Width="120px"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td45" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayFrom6" runat="server" TabIndex="89" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator15" runat="server" ControlToValidate="txtDayFrom6"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender17" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayFrom6" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td46" align="center" colspan="1" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtDayTo6" runat="server" TabIndex="90" Width="70px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:CompareValidator ID="CompareValidator16" runat="server" ControlToValidate="txtDayTo6"
                                                                            ErrorMessage="Date is not Valid" Operator="DataTypeCheck" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                                                                      <%--  <asp:MaskedEditExtender ID="MaskedEditExtender18" runat="server" Mask="99/99/9999"
                                                                            MaskType="Date" TargetControlID="txtDayTo6" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="&#163;"
                                                                            CultureDateFormat="DMY" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" CultureName="en-GB">
                                                                        </asp:MaskedEditExtender>--%>
                                                                    </td>
                                                                    <td id="Td47" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpName6" runat="server" CssClass="TextBox" TabIndex="91" Width="130px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" TargetControlID="txtEmpName6"
                                                                            WatermarkText="Emp Name Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpContact6" runat="server" CssClass="TextBox" TabIndex="92"
                                                                            Width="100px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender21" runat="server" TargetControlID="txtEmpContact6"
                                                                            WatermarkText="Conatc No Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td id="Td48" align="left" colspan="1" runat="server">
                                                                        <asp:TextBox ID="txtEmpAddr6" runat="server" CssClass="TextBox" TabIndex="93" Width="130px"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtEmpAddr6"
                                                                            WatermarkText="Address Here" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                        <br />
                                                                        <asp:TextBox ID="txtEmpPin6" runat="server" CssClass="TextBox" TabIndex="94" Width="100px"
                                                                            MaxLength="6"></asp:TextBox>
                                                                        <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender23" runat="server" TargetControlID="txtEmpPin6"
                                                                            WatermarkText="PIN CODE" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                                    </td>
                                                                    <td align="left" colspan="1" valign="top">
                                                                        <asp:TextBox ID="txtEmpSal6" runat="server" CssClass="TextBox" TabIndex="58" Width="130px"
                                                                            meta:resourcekey="txtEmpAddr1Resource1"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral">
                                                                    <td colspan="7" style="text-align: left">
                                                                        <asp:Button ID="btnAddExperience" runat="server" Text="Add Experience" CausesValidation="False"
                                                                            TabIndex="95" CssClass="buttonStyle" meta:resourcekey="btnAddExperienceResource1" />
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                              </asp:Panel>
                                              
                                               
                                        `</td>
                                </tr>
                                
                                <tr>
                                    <td align="center">
                                        <table bgcolor="#d2e2b7" border="0" cellpadding="1" cellspacing="0" class="greydotRED"
                                            width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4">
                                                        <table bgcolor="#d2e2b7" border="0" cellpadding="5" cellspacing="0" class="greydotRED"
                                                            width="100%">
                                                            <tbody>
                                                                <tr class="trFloral">
                                                                    <td align="left">
                                                                       
                                                                        <asp:Label ID="Label1" runat="server" CssClass="ariallightgrey" Text="Know the above details are correct and if wrong Board's decision will be final and binding on me."></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:RadioButtonList TabIndex="53" ID="rbAgreement" runat="server" CssClass="ariallightgrey"
                                                                            RepeatDirection="Horizontal" Font-Size="X-Small" meta:resourcekey="rbAgreementResource1"
                                                                            AutoPostBack="True">
                                                                            <asp:ListItem meta:resourcekey="ListItemResource38" Value="1" Text="Yes"></asp:ListItem>
                                                                            <asp:ListItem meta:resourcekey="ListItemResource39" Value="0" Text="No"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr class="trFloral" style="text-align: left">
                                                                    <td colspan="2">
                                                                        <asp:RequiredFieldValidator ID="rfvAgreement" ControlToValidate="rbAgreement" runat="server"
                                                                            ErrorMessage="Agreement required !!!"></asp:RequiredFieldValidator><asp:CompareValidator
                                                                                ID="CompareValidator21" runat="server" ControlToValidate="rbAgreement" ValueToCompare="1"
                                                                                ErrorMessage="You must agree with details you provided."></asp:CompareValidator>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr class="trFloral" style="text-align: left"><td colspan="2">&nbsp;</td></tr>
                                                              
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="BtnInsertUpdate" runat="server" CssClass="button_text" TabIndex="99"
                                            Text="Save" Width="72px" meta:resourcekey="BtnInsertUpdateResource1" 
                                            onclick="BtnInsertUpdate_Click" />
                                        &nbsp;<input class="button_text" tabindex="120" type="reset" value="Clear" id="Reset1"
                                            language="javascript" meta:resourcekey="button_textResource1" onClick="return Reset1_onclick()" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <%--Main area end here--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
      
    </table>
    </form>
</body>


</asp:Content>

