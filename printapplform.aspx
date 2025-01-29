<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="printapplform.aspx.cs" Inherits="printapplform" Title="Print Application" %>
<%@ Register Src="~/usercontrols/printAppl.ascx" TagName="printAppl" TagPrefix="no" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server" >
<table>
<tr id="trfrm" runat="server">
<td>
<table style="background-color:#DDDDFF"  >
        <tr id="truser" runat="server" >
            <td style="height: 129px" colspan ="4">
                <no:printAppl ID="ddl_applid" runat="server"  />
            </td>
            
        </tr>
        <tr id="Tr1" runat="server" visible="true">
            <td colspan="2" style="height: 20px">
                &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                    Text="Print Application" Width="124px" CssClass="cssbutton" /></td>
        </tr>
        <tr id="trbtn" runat="server" visible="false">

            <td style="height: 20px">
                &nbsp;</td>
        </tr>  
        <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </table>
</td>
</tr>
<tr>
<td>
                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Text=""
                    Visible="False"></asp:Label>
</td>
</tr>
</table>
    

</asp:Content>

