<%@ Control Language="C#" AutoEventWireup="true" CodeFile="appnumber.ascx.cs" Inherits="usercontrols_appnumber" %>
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />
<script runat="server">

    
</script>

<table>
    <tr>
        <td align="left">
            <asp:Label ID="lblappno" runat="server" Text="Select Post Applied" CssClass="formheading"></asp:Label>
        </td>
        <td colspan="2" align="left">
            <asp:DropDownList ID="DropDownList_post" runat="server" CssClass="ddl" Width="500px">
            </asp:DropDownList>
            </td>
    </tr>
             
</table>

