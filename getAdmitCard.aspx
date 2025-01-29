<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="getAdmitCard.aspx.cs" Inherits="getAdmitCard" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <table style="text-align:center">
    <tr>
        <td align="left">
            <asp:Label ID="lblappno" runat="server" Text="Select Post Applied" CssClass="formheading"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="DropDownList_post" runat="server" CssClass="ddl" Width="500px">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
    <td colspan="2">
    <asp:Button Text="Submit" runat="server" ID="btn_submit" 
            onclick="btn_submit_Click" />
    
    </td>
    </tr>
             
    <tr>
    <td colspan="2">
        <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="#FF3300" 
            Text="No e-Admit Card available at this time." Visible="False"></asp:Label>
    
    </td>
    </tr>
             
</table>
</asp:Content>

