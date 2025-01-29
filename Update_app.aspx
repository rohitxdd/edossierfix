<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Update_app.aspx.cs" Inherits="Update_app" Title="Untitled Page" %>
<%@ Register Src="~/usercontrols/appnumber.ascx" TagName="jid" TagPrefix="no" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="height: 58px" align="center">
              <no:jid ID="ddl_applid" runat="server" />
                 </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="Button_Vaidate" runat="server" 
                    Text="Edit Application" Width="131px" onclick="Button_Vaidate_Click" CssClass="cssbutton" />
                <%--<asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Text="Nothing Pending"
                    Visible="False"></asp:Label>--%>
                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Text=""
                    Visible="False"></asp:Label>
        </tr>
    </table>
     <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>

