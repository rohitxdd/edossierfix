<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"
    Debug="true" ValidateRequest="false" %>

<%@ Register Src="~/usercontrols/Header.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/Footer.ascx" TagName="WebUserControl" TagPrefix="uc2" %>
<%@ Register Src="~/usercontrols/noticeboard.ascx" TagName="WebUserControl" TagPrefix="uc3" %>
<%@ Register Src="~/usercontrols/latestannounce.ascx" TagName="WebUserControl" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="CSS/dsssbstylesheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Jscript/JScript.js">    
    </script>
    <script type="text/javascript" language="javascript">
        function GoBack() {
            // alert('goback');
            //window.history.go(+1);
            window.history.forward(1);

        }
    </script>
    <script language="javascript" type="text/javascript" src="Jscript/md5.js">    
    </script>
    <script language="javascript" type="text/javascript">
        function SignValidate() {
            if (!Page_ClientValidate()) {
                return false;
            }

            var pwd = document.getElementById("<%=txtpass.ClientID%>").value;
            var pwdhash = MD5(pwd);
            //alert(pwdhash);
            var rand = document.getElementById("<%=txtrandomno.ClientID%>").value;
            var saltedHash = MD5(pwdhash + rand);

            document.getElementById("<%=txtpass.ClientID%>").value = saltedHash;
            document.getElementById("<%=txtrandomno.ClientID%>").value = null;

        }
    </script>
    <script language="javascript" type="text/javascript">
        function SignValidateRefresh() {
            var pwd = document.getElementById("<%=txtpass.ClientID%>").value;
            var pwdhash = MD5(pwd);
            var rand = document.getElementById("<%=txtrandomno.ClientID%>").value;
            var saltedHash = MD5(pwdhash + rand);
            document.getElementById("<%=txtpass.ClientID%>").value = saltedHash;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function autoTab(current, next) {
            if (current.getAttribute && current.value.length == current.getAttribute("maxlength"))
                next.focus()
        }

    </script>
    <script type="text/javascript" language="javascript">
        function noBack() {
            window.history.forward();
        }
    </script>
</head>
<body id="body" onload="noBack();" onpageshow="if (event.persisted) noBack();">
    <form id="form1" runat="server" method="post">
        <div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="bodybg">
                <tr>
                    <td colspan="3">
                        <uc1:WebUserControl ID="WebUserControl1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="950px" cellpadding="2" cellspacing="2" border="0" align="center" class="bodybg">
                            <tr>
                                <%--<td colspan="7">
                                <marquee onmouseover="javascript:this.setAttribute('scrollamount',0,0);this.stop();"
                                    onmouseout="javascript:this.setAttribute('scrollamount',4,0);this.start();"> 
                               <asp:Label ID="lblMarque" style="font-weight:bold;color: red;font-size: large;" runat="server" Text="All candidates who have applied for Advt. no. 1/20 to 5/20 (post code 1/20 to 116/20 except 89/20) need to update their photo identity details in registration form and upload postcard size photograph ( 5*7 inch)"></asp:Label></marquee>
                            </td>--%>
                                <td align="right" colspan="3" valign="top">
                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" NavigateUrl="DownloadOMRSheet.aspx"
                                        Target="_blank" Visible="False">Download OMR</asp:HyperLink>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="350px" valign="top">
                                    <table width="350px" border="0" cellspacing="0" cellpadding="0" align="center">
                                        <tr>
                                            <td width="9">
                                                <img src="Images/buleleftconner.png" width="9" height="48" />
                                            </td>
                                            <td width="100%" background="Images/bulebgcolorcenter.png" class="whiteheadings"
                                                align="left">Current Vacancies
                                            </td>
                                            <td width="9">
                                                <img src="Images/bulerightconner.png" width="9" height="48" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor" height="350" valign="top">
                                                <uc4:WebUserControl ID="WebUserControl4" runat="server" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" width="300">
                                    <table width="300" border="0" cellspacing="2" cellpadding="2">
                                        <tr runat="server" visible="true">
                                            <td class="yellowheader" width="300" align="left">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="40">
                                                            <img src="Images/new2.gif" runat="server" />
                                                        </td>
                                                        <td width="260">
                                                            <asp:HyperLink ID="hladmitcard" runat="server" Text="Generate/Print eAdmit Card"
                                                                NavigateUrl="~/AdmitCardEntry.aspx" CssClass="yellowheadertext"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                        <td class="yellowheader" width="300" align="left">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                        <td width="40"><img src="Images/about.png" width="40" height="35" /></td>
                                        <td width="260">
                                            <asp:Label ID="aboutqars" runat="server" CssClass="yellowheadertext" Text="About Us..."></asp:Label>
                                            </td>
                                        </tr>
                                        </table>
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="aboutstext">
                                             On the 50th Anniversary year of the Indian Independence, the Government of
                                            National Capital Territory of Delhi has instituted the Delhi Subordinate Services
                                            Selection Board. The Board has been incorporated with the purpose of recruiting
                                            capable, competent, highly skilled individuals by conducting written tests, professional
                                            tests and personal interviews wherever as desired. The Board shall hereby committed
                                            to develop selection and recruitment procedures that confirm to the global standards
                                            in testing, and promise selections by all fair means, of the most competent, capable,
                                            and skilled individuals for user departments.
                                        </td>
                                    </tr>--%>
                                        <%-- <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>--%>
                                        <tr>
                                            <td valign="top" width="300">
                                                <table width="300" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="300" align="left" class="yellowheader" valign="top">
                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="40">
                                                                        <img src="Images/new2.gif" runat="server" />
                                                                        <%--<img src="Images/news.png" width="35" height="35" />--%>
                                                                    </td>
                                                                    <td width="260">
                                                                        <asp:HyperLink ID="hl_redirect" runat="server" Text="Click here to check the Latest Update, Notices, Results & Other Relevant Information."
                                                                            NavigateUrl="https://dsssb.delhi.gov.in/" CssClass="yellowheadertext" Font-Size="Small"></asp:HyperLink>
                                                                        <%-- <asp:Label ID="lblNews" runat="server" CssClass="yellowheadertext" Text="News & Events..."></asp:Label>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdbggreecolor">
                                                            <uc3:WebUserControl ID="WebUserControl3" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdbggreecolor">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdbggreecolor">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdbggreecolor">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="300" valign="top">
                                    <table width="300" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="9">
                                                <img src="Images/buleleftconner.png" width="9" height="48" />
                                            </td>
                                            <td width="100%" background="Images/bulebgcolorcenter.png" class="whiteheadings">Registration
                                            </td>
                                            <td width="9">
                                                <img src="Images/bulerightconner.png" width="9" height="48" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor" align="center">
                                                <asp:ImageButton ID="btnnewreg" runat="server" Width="250px" ImageUrl="~/Images/btn.png"
                                                    OnClick="btnnewreg_Click" />
                                                <%-- <asp:Button ID="btnReg" CssClass="myButton" Width="250px" runat="server" Text="Click for New Registration" />--%>
                                            &nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <%-- Added by AnkitaSingh for 90/09 entry in OARS Dated:11-08-2022
                                        <td class="tdbggreecolor" align="center">
                                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/splcandidate.aspx" ForeColor="Red"
                                                Font-Size="14px">
                                            Registration for post code 90/09</asp:HyperLink>

                                        </td>--%>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor" align="center">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trSigInbtn">
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor" align="center">
                                                <asp:Button ID="btnSignIn" CssClass="btnSignIn" runat="server" Text="Click to Sign In"
                                                    OnClick="btnSignIn_Click" />
                                                <%--<asp:ImageButton ID="ImageButton1" runat="server" Width="25px" ImageUrl="~/Images/btn.png"
                                                />--%>
                                                <%-- <asp:Button ID="btnReg" CssClass="myButton" Width="250px" runat="server" Text="Click for New Registration" />--%>
                                            &nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr runat="server" id="divSignIn" visible="false">
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor" align="center">
                                                <table align="center" class="box" width="250px" cellpadding="2" cellspacing="2" bgcolor="white">
                                                    <tr>
                                                        <td align="center" class="redheadertext" colspan="3">Registered User Sign In
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" class="datetimelabletext" colspan="3">Enter Registration No.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="300px" align="center" cellpadding="1" cellspacing="1">
                                                                <tr>
                                                                    <td align="left" class="datetimelabletext" colspan="2">Date Of Birth&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                                                                        (DD MM YYYY)
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox autofill="false" autocomplete="false" placeholder="DD" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                                                            ID="txt_dd" runat="server" Width="20px" MaxLength="2" onkeyup="autoTab(this, document.form1.txt_mm)"
                                                                            onfocus="javascript:this.value=''" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox autofill="false" autocomplete="false" placeholder="MM" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                                                            ID="txt_mm" runat="server" Width="20px" MaxLength="2" onkeyup="autoTab(this, document.form1.txt_yyyy)"
                                                                            onfocus="javascript:this.value=''" TabIndex="2"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox autofill="false" autocomplete="false" placeholder="YYYY" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                                                            ID="txt_yyyy" runat="server" Width="50px" MaxLength="4" onkeyup="autoTab(this, document.form1.txt_regno)"
                                                                            onfocus="javascript:this.value=''" TabIndex="3"></asp:TextBox>
                                                                    </td>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtpass"
                                                                        ValidationExpression="[a-zA-Z0-9!$%^*@#&]{6,}$" Display="None" ErrorMessage=""
                                                                        ValidationGroup="1">
                                                                    </asp:RegularExpressionValidator>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="datetimelabletext" colspan="2">Xth roll No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                                                    </td>
                                                                    <td align="left" colspan="3">
                                                                        <asp:TextBox autofill="false" autocomplete="false" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                                                            ID="txt_regno" runat="server" Width="80%" onfocus="javascript:this.value=''"
                                                                            MaxLength="15" TabIndex="4"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="datetimelabletext" colspan="2">Xth pasing Year :
                                                                    </td>
                                                                    <td align="left" colspan="3">
                                                                        <asp:DropDownList ID="DropDownList_year" runat="server" TabIndex="5" Width="85%">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                <td align="left" class="datetimelabletext">
                                                                    DD
                                                                </td>
                                                                <td align="left" class="datetimelabletext">
                                                                    MM
                                                                </td>
                                                                <td align="left" class="datetimelabletext">
                                                                    YYYY
                                                                </td>
                                                                <td align="left" class="datetimelabletext">
                                                                    10th Roll No.
                                                                </td>
                                                                <td align="left" class="datetimelabletext">
                                                                    10th Passing Year
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" class="datetimelabletext" align="left">
                                                                    (Date of Birth)
                                                                </td>
                                                            </tr>--%>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="datetimelabletext" colspan="1">Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox ID="txtpass" runat="server" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                                                TextMode="Password" Width="80%" autocomplete="off" autofill="false"
                                                                TabIndex="6"></asp:TextBox><asp:RequiredFieldValidator ID="rfvpwd" runat="server"
                                                                    ControlToValidate="txtpass" Display="None" ErrorMessage="" ValidationGroup="1" />
                                                        </td>
                                                        <asp:RegularExpressionValidator ID="revpwd" runat="server" ControlToValidate="txtpass"
                                                            ValidationExpression="[a-zA-Z0-9!$%^*@#&]{6,}$" Display="None" ErrorMessage=""
                                                            ValidationGroup="1">
                                                        </asp:RegularExpressionValidator>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="datetimelabletext" colspan="1">Visual Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                                        </td>
                                                        <td valign="top" align="left" colspan="2">
                                                            <img width="150" height="50" alt="Visual verification" title="Please enter the Visual Code as shown in the image."
                                                                src="JpegImage_CS.aspx?r=<%= System.Guid.NewGuid().ToString("N") %>" vspace="5" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:ImageButton ToolTip="Click here to load a new Image" runat="server" ImageUrl="~/images/refresh.jpg"
                                                                ID="ibtnRefresh" OnClick="ibtnRefresh_Click" OnClientClick="return SignValidateRefresh();" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="datetimelabletext" align="left" colspan="1">Type the code shown&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox AutoCompleteType="None" oncopy="return false" oncut="return false" onpaste="return false"
                                                                ToolTip="Enter Above Characters in the Image" autocomplete="off" MaxLength="10"
                                                                ID="txtCode" runat="server" TabIndex="7" />
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RFVCaptcha" runat="server" ControlToValidate="txtCode"
                                                                ErrorMessage="Enter Visual Code" ToolTip="Visual Code" ValidationGroup="1" SetFocusOnError="True"
                                                                Display="Dynamic">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="1">&nbsp;
                                                        </td>
                                                        <td align="right">
                                                            <%--<asp:Button ID="btnsignin" CssClass="myButton" runat="server" Text="Sign In" />--%>
                                                            <asp:ImageButton ID="Button1" runat="server" OnClick="Button1_Click" ValidationGroup="9"
                                                                OnClientClick="return SignValidate();" TabIndex="8" Width="100px" ImageUrl="~/Images/singinbtn.png" />
                                                            <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="" ControlToValidate="txt_dd"
                                                                Display="None" ValidationGroup="1">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revDD" runat="server" ControlToValidate="txt_dd"
                                                                Display="None" ErrorMessage="" ValidationExpression=".{2}.*" ValidationGroup="1">
                                                            </asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="REVDD1" runat="server" ControlToValidate="txt_dd"
                                                                Display="None" ErrorMessage="" ValidationExpression="^[0-9]*$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RFV2" runat="server" ErrorMessage="" ControlToValidate="txt_mm"
                                                                Display="None" ValidationGroup="1">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="REVMM" runat="server" ControlToValidate="txt_mm"
                                                                Display="None" ErrorMessage="" ValidationExpression=".{2}.*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="REVmm1" runat="server" ControlToValidate="txt_mm"
                                                                Display="None" ErrorMessage="" ValidationExpression="^[0-9]*$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RFV3" runat="server" ErrorMessage="" ControlToValidate="txt_yyyy"
                                                                Display="None" ValidationGroup="1">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revyy" runat="server" ControlToValidate="txt_yyyy"
                                                                Display="None" ErrorMessage="" ValidationExpression=".{4}.*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="revyy1" runat="server" ControlToValidate="txt_yyyy"
                                                                Display="None" ErrorMessage="" ValidationExpression="^[0-9]*$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RFV4" runat="server" ErrorMessage="" ControlToValidate="txt_regno"
                                                                Display="None" ValidationGroup="1">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="RFV5" runat="server" ErrorMessage="" ControlToValidate="DropDownList_year"
                                                                Display="None" ValidationGroup="1" InitialValue="-1">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1" visible="false">
                                            <td colspan="1">
                                                <img src="Images/new2.gif" style="margin-top: -90px; margin-right: -42px;">
                                            </td>
                                            <td class="tdbggreecolor" colspan="4" align="center">
                                                <%-- <asp:Button ID="btnReg" CssClass="btnSignIn" Width="250px" runat="server" Text="Upload Documents" />--%>
                                                <%--<asp:LinkButton ID="Button2" CssClass="btnSignIn" Style="background-color: #e4f3f3;
                                                color: red;" runat="server" Text="Click for updating postcode code 1/20 to 116/20"
                                                OnClick="Button2_Click"></asp:LinkButton>--%>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Style="border-width: 0px; width: 250px; border-width: 0px; border-radius: 10px; margin-right: 10px;"
                                                    OnClick="Button2_Click"
                                                    ImageUrl="~/Images/Updatepostcode.jpg" />
                                                <br />
                                                <%--<span style="font-size: medium; color: blue;">(Updation for Identity proof details and uploading of postcard size photograph.)</span>
                                            <asp:ImageButton ID="ImageButton1" runat="server" Width="25px" ImageUrl="~/Images/btn.png" Click for updation of identity proof and uploading of postcard size photograph for advt no. 1/20 to 5/20 (Post code 1/20 to 116/20 
                                                />--%>
                                                <%-- <asp:Button ID="btnReg" CssClass="myButton" Width="250px" runat="server" Text="Click for New Registration" />--%>
                                            &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor" align="center">
                                                <%--<asp:Button ID="btnforgetpass" CssClass="myButton" runat="server" Text="Forget Password"
                                                Width="200px" />--%>
                                                <asp:ImageButton ID="ButtonForgetPass" Text="Forgot Password" OnClick="ButtonForgetPass_Click"
                                                    runat="server" ImageUrl="~/Images/btnforget.png" Width="246px" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr2">
                                            <td>&nbsp;
                                            </td>
                                            <td class="tdbggreecolor" align="center">
                                                <asp:Button ID="KnowRgstrn" CssClass="btnSignIn" runat="server" Text="Know Your Registration Detail"
                                                    OnClick="knowRgstrn_Click" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top">
                                    <asp:HiddenField ID="txtrandomno" runat="server" />
                                    <asp:ValidationSummary ID="vs" HeaderText="Login failed; Invalid user ID or Password."
                                        runat="server" ValidationGroup="8" ShowMessageBox="true" ShowSummary="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc2:WebUserControl ID="WebUserControl2" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </form>
</body>
</html>
