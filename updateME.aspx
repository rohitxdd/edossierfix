<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updateME.aspx.cs" Inherits="updateME" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" href="css/Applicant.css" />
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server" style="vertical-align:middle; text-align:center">
    <div>
    
        <table width="70%" class="border_gray">
            <tr class="darkblue">
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" ForeColor="Red" runat="server" Text="Please update your EmailId/Mobile No."></asp:Label>
                    </td>
            </tr>
            <tr class="darkblue">
                <td align="left">
                    <asp:Label ID="Label2" runat="server" Text="RegNo."></asp:Label>
                    </td>
                <td align="left">
    
                    <asp:Label ID="lbl_regno" runat="server"></asp:Label>
    
                    </td>
            </tr>
            <tr class="darkblue">
                <td align="left">
                    <asp:Label ID="lbl_mob" runat="server" Text="Mobile No."></asp:Label>
                    </td>
                <td align="left">
    
        <asp:TextBox ID="txt_mob" runat="server" MaxLength="10"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvmob" runat="server" Display="none" ControlToValidate="txt_mob"
            ValidationGroup="1" ErrorMessage="Please Enter Mobile No.">
            </asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="revmob" runat="server" Display="None" ControlToValidate="txt_mob"
                ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                    ValidationGroup="1">
                    </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="REVMobile" runat="server" ControlToValidate="txt_mob"
                    ValidationExpression=".{10}.*" ErrorMessage="Enter Minimum 10 Digit" Display="none" ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
            </tr>
            <tr class="darkblue">
                <td align="left">
                    <asp:Label ID="lbl_email" runat="server" Text="Email Id"></asp:Label>
                    </td>
                <td align="left">
    <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvmail" runat="server" Display="none" ControlToValidate="txt_email"
            ValidationGroup="1" ErrorMessage="Please Enter EMail-Id"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revmail" runat="server" Display="None" ControlToValidate="txt_email"
  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Characters In EmailId"
  ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
            </tr>
            <tr>
                <td colspan="2">
    <asp:Button ID="btnrsubmit" runat="server" onclick="btnrsubmit_Click" Text="Update" />
                    </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
