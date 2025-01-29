<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditApplication.aspx.cs"
    EnableSessionState="True" MasterPageFile="~/MasterPage.master" Inherits="EditApplication"
    Title="Edit Candidate" %>

<%@ Register Src="~/usercontrols/print.ascx" TagName="print" TagPrefix="pr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table>
        <tr>
            <td>
                <pr:print ID="edit_print" runat="server" OnLoad="edit_print_Load" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtapplid" runat="server" Visible="False"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr align="left">
            <td style="height: 16px">
                <asp:Label ID="lbl_step" runat="server" Text="Step 4/5" ForeColor="DarkGreen" Font-Bold="True" Font-Size="Large"
                    Font-Italic="True" Visible="true">
                </asp:Label>
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
