<%@ Control Language="C#" AutoEventWireup="true" CodeFile="edossierpost.ascx.cs" Inherits="usercontrols_edossierpost" %>
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />
<script runat="server">

    
</script>

<table>
    <tr>
        <td align="left">
            <asp:Label ID="lblappno" runat="server" Text="Select Post " CssClass="formheading"></asp:Label>
        </td>
        <td colspan="2" align="left">
            <asp:DropDownList ID="DropDownList_post" runat="server" CssClass="ddl" Width="500px">
            </asp:DropDownList>
             <asp:HiddenField ID="hfjid" runat="server" Visible="false" />  
            
            </td>
    </tr>
           
</table>