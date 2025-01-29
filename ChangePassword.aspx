<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
<%@ Register Src="~/usercontrols/Change_Password.ascx" TagName="Change_Password" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="JS/md5.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lbltitle" runat="server" CssClass="validatorstyles" Font-Bold="True">
                Change Password
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="1">
                <uc2:change_password id="uccp" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
