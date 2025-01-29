<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" EnableSessionState="True"
    Debug="true" %>

<%@ Register Src="~/usercontrols/MainHeader.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/AdmitCardConsent.ascx" TagName="acc" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <script language="javascript" type="text/javascript" src="JS/JScript.js">    
    </script>
    <script language="javascript" type="text/javascript" src="JS/md5.js">    
    </script>
    <script language="javascript" type="text/javascript">
    function validate()
    {
    if (CheckLogin("form1","User Name  cant be left Blank","txtusername")== false) return false;
     if (CheckPwd("form1","Password  cant be left Blank","txtpass")== false) return false;
    }
    
    </script>

       <script language="javascript" type="text/javascript">
           function SignValidate() {
               if (!Page_ClientValidate()) {
                   return false;
               }

               var pwd = document.getElementById("<%=txtpass.ClientID%>").value;
               var pwdhash = MD5(pwd);
               
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

    <style type="text/css">
        .style1
        {
            width: 216px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" method="post" onsubmit="return validate()">
        <div style="text-align: center">
            <span style="text-decoration: underline"><span><span style="font-size: 22pt"><span
                style="color: blue">
                <br />
            </span></span></span></span>
            <table width="900" align="center">
                <tr>
                    <td colspan="2">
                        <uc1:top ID="Top1" runat="server" />
                    </td>
                </tr>
              <%--  <tr>
                    <td>
                        If you are a new user.Please register yourself first.Click on given Create Account
                        Link.
                    
                   
                   <asp:HyperLink ID="a" runat="server" Text="Create User" NavigateUrl="~/Candidatedetailformwmp.aspx"></asp:HyperLink>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                        <table align="center" style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; width: 47%; border-bottom: #000000 thin solid; height: 98px">
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <span style="font-size: 16pt">&nbsp; &nbsp; &nbsp; &nbsp;<span> &nbsp; </span><strong>
                                        <span style="color: #4E9258; text-decoration: underline; font-size: 12pt;">Authenticate
                                            Yourself<br />
                                        </span></strong></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 27px">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/login.gif" Height="30px"
                                        Width="80px" /></td>
                                <td align="left" style="height: 27px">
                                    <asp:TextBox ID="txtusername" runat="server" Width="100px" MaxLength="12" AutoCompleteType="None"
                                        autofill="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvuser" runat="server" ControlToValidate="txtusername"
                                        Display="None" ErrorMessage="Please enter username." ValidationGroup="1" />
                                    <asp:RegularExpressionValidator ID="revuser" runat="server" ControlToValidate="txtusername"
                                        ValidationExpression="^[a-zA-Z0-9]*$" Display="None" ErrorMessage="Username can contain only alphanumeric characters."
                                        ValidationGroup="1">
                                    </asp:RegularExpressionValidator>
                                </td>
                               
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="left">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/password.gif" Height="30px"
                                        Width="120px" /></td>
                                <td align="left">
                                    <asp:TextBox ID="txtpass" runat="server" TextMode="Password" Width="100px" MaxLength="12"
                                        AutoCompleteType="None" autofill="false"></asp:TextBox>
                                    <asp:HiddenField ID="txtrandomno" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvpwd" runat="server" ControlToValidate="txtpass"
                                        Display="None" ErrorMessage="Please enter password." ValidationGroup="1" /></td>
                                <asp:RegularExpressionValidator ID="revpwd" runat="server" ControlToValidate="txtpass"
                                    ValidationExpression="[a-zA-Z0-9@#&]{3,}$" Display="None" ErrorMessage="Password must be of atleast 3 characters Or Invalid Characters."
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator></tr>
                                <tr><td colspan="2">
                         <table>
                   <tr align="left">
                        <td class="style1"  >
                             <span style="font-size: 8pt"></span>
                             <strong><span style="color: #5858FA; text-decoration: none;
                                    font-size: 15pt; font-style:italic;">Visual Code</span></strong></td>
                        <td valign="top">
                            &nbsp;<img width="150" height="50" alt="Visual verification" title="Please enter the security code as shown in the image."
                                src="JpegImage_CS.aspx?r=<%= System.Guid.NewGuid().ToString("N") %>" vspace="5" />
                                </td>
                        <td colspan="2" align="left">
                            <asp:ImageButton ToolTip="Click here to load a new Image" runat="server" ImageUrl="~/images/refresh.jpg"
                                ID="ibtnRefresh" OnClick="ibtnRefresh_Click" OnClientClick="return SignValidateRefresh();"/>
                                
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="color: #ff0066; height: 42px;">
                            Type the code shown&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                           <asp:TextBox AutoCompleteType="None" oncopy="return false" oncut="return false"
                                onpaste="return false" ToolTip="Enter Above Characters in the Image" autocomplete="off"
                                MaxLength="10" ID="txtCode" runat="server" />
                               <span style="font-size: 10pt; color:Blue;"><em>(Tip: Above Visual Code is not Case Sensitive)</em></span>
                               
                            <asp:RequiredFieldValidator ID="RFVCaptcha" runat="server" ControlToValidate="txtCode"
                                ErrorMessage="Enter Security Code" ToolTip="Password is required." ValidationGroup="1"
                                SetFocusOnError="True" Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table></td></tr>
                            <tr><td>&nbsp;</td>
                            <td align="left">
                            
                            <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" ValidationGroup="1"
                            OnClientClick="return SignValidate();" />
                        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="1" ShowMessageBox="true"
                            ShowSummary="false" />
                            </td></tr>
                        </table>
                        <br /><br />
                        
                        &nbsp;
                    </td>
                </tr>
            </table>
            
           
        </div>
         <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </form>
</body>
</html>
