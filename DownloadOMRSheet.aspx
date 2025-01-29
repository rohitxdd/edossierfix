<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownloadOMRSheet.aspx.cs" Inherits="DownloadOMRSheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/usercontrols/MainHeader.ascx" TagName="callletter" TagPrefix="header" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <title>DownLoad OMR</title>
   <link rel="stylesheet" type="text/css" href="./CSS/Applicant.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table align="center">
    <tr>
    <td colspan="2" style="height: 124px">
      <header:callletter ID="ddl_applid" runat="server"  />
    </td> 
    </tr>
        <tr>
            <td align="center" colspan="2">
                &nbsp;<asp:Label ID="Label3" runat="server" CssClass="darkblue" Font-Size="Large" Text="Download OMR Answer Sheet"
                    Width="453px"></asp:Label><br /><br />
            </td>
        </tr>
    
      <tr>
    <td width="30%" align="left">
        <asp:Label ID="Label1" runat="server" Text="Enter Your Roll No." Width="158px" CssClass="darkblue"></asp:Label>
    </td>
    <td width="70%" align="left">
        <asp:TextBox ID="TextBoxRollNo" runat="server" Width="168px" ValidationGroup="1"></asp:TextBox><asp:RequiredFieldValidator ID="RFV_RollNo" runat="server" ControlToValidate="TextBoxRollNo"
            Display="None" ErrorMessage="Please Enter Roll Number" ValidationGroup="1"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="REVRollno" runat="server" ControlToValidate="TextBoxRollNo"
            Display="None" ErrorMessage="Enter Only Integer Values" ValidationExpression="^[0-9]*$"
            ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
            
    </tr>
         <tr class="darkblue">
             <td align="left" width="30%">
                 <asp:Label ID="Label2" runat="server" CssClass="darkblue" Text="Date of Birth(As per Xth Certificate)"
                     Width="248px"></asp:Label></td>
             <td align="left" width="70%">
                 <asp:TextBox ID="txt_DOB" runat="server" MaxLength="10" Width="95px" ValidationGroup="1"></asp:TextBox>(dd/MM/yyyy)
                    <%-- <img id="Img1" alt="DatePicker" onclick="PopupPicker('txt_DOB', 250, 250)" src="Images/calendar.bmp"
                      style="width: 25px; height: 25px" runat="server" />--%>
                      <asp:RequiredFieldValidator ID="rfvdob" runat="server" Display="none" ControlToValidate="txt_DOB"
                    ValidationGroup="1" ErrorMessage="Please Enter DOB.">
                    </asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="REV_date" runat="server" ControlToValidate="txt_DOB"
                        Display="None" ErrorMessage="Enter Valid Date of Birth." ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="1">
                        </asp:RegularExpressionValidator></td>
         </tr>
      <tr>
            <td colspan="2">
            <table width="100%">
                   <tr align="left" >
                        <td width="30%">
                             <span style="font-size: 8pt"></span>
                             <strong><span style="color: #5858FA; text-decoration: none;
                                    font-size: 12pt; font-style:italic;">Visual Code</span></strong></td>
                        <td valign="top" width="20%">
                            <img width="150" height="50" alt="Visual verification" title="Please enter the security code as shown in the image."
                                src="JpegImage_CS.aspx?r=<%= System.Guid.NewGuid().ToString("N") %>" vspace="5" />
                                </td>
                        <td align="left">
                            <asp:ImageButton ToolTip="Click here to load a new Image" runat="server" ImageUrl="~/images/refresh.jpg"
                                ID="ibtnRefresh" OnClick="ibtnRefresh_Click"   OnClientClick="return SignValidateRefresh();"/>
                                
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
                </table>
                </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="Button2" runat="server" Text="Download" CssClass="cssbutton" Width="70px" OnClick="Button2_Click" ValidationGroup="1" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="1" />
            </td>
        </tr>
    
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    
    </div>
    </form>
</body>
</html>
