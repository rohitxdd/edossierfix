<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Application.aspx.cs" Inherits="Application" MasterPageFile="~/MasterPage.master" Title="Print Application" %>
<%@ Register Src="~/usercontrols/print.ascx" TagName="print" TagPrefix="no" %>

<%--<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body>
    <table>
        <tr>
            <td >
                <no:print ID="print" runat="server" />
            </td>
        </tr>
        <tr><td>
            &nbsp;<asp:Button ID="btnprint" runat="server" OnClick="btnprint_Click"  Text="Print This Page" CssClass="cssbutton"/></td></tr>
    </table>

</body>
</html>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <table>
        <tr>
            <td style="height: 129px" >
                <no:print ID="print" runat="server" />
            </td>
        </tr>
        <tr>
            <td >
                </td>
        </tr>
        <tr><td>
            &nbsp;<asp:Button ID="btnprint" runat="server" OnClick="btnprint_Click"  Text="Print This Page" CssClass="cssbutton"/></td></tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>

