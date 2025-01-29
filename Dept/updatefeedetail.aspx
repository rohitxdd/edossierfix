<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="updatefeedetail.aspx.cs" Inherits="updatefeedetail" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">

    c<table cellpadding="0" cellspacing="0" style="width: 50%">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:FileUpload ID="FileUpload" runat="server" />
                <asp:Button ID="btn_upload" runat="server" onclick="btn_upload_Click"
                    Text="Upload Data" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center">
                <asp:Label ID="lbl_msg" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>


