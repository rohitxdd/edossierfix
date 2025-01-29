<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message_reg.aspx.cs" Inherits="message_reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="CSS/Applicant.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table align="center" style="left:500px; top:400;">
    <tr><td align="center" style="font-size:18pt; font-weight:bold; text-decoration:underline;"><asp:Label ID="lbl" runat="server" Font-Bold="true" ForeColor="DarkBlue"></asp:Label></td></tr>
    <tr><td align="center" style="font-size:18pt;"><asp:Label ID="lblmsg" runat="server" ForeColor="darkred"></asp:Label></td></tr>
    <tr><td align="center" style="font-size:18pt;"><asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="DarkBlue"></asp:Label></td></tr>
    <tr><td align="center" style="font-size:18pt;"><asp:Label ID="Label2" runat="server" Font-Bold="true" ForeColor="darkred"></asp:Label></td></tr>
    <tr><td></td></tr>
    <tr>
    <td align="center">
    <asp:Button runat="server" ID="popupclose" Text="OK" CssClass="buttons" Width="50px" Font-Bold="true" Visible="False" />
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
