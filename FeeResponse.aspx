<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FeeResponse.aspx.cs" Inherits="FeeResponse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table width="100%">
        <tr id="trcha" runat="server">
            <td align="center">
                <asp:Label ID="LabelNote" runat="server" CssClass="formheading" ForeColor="blue"></asp:Label>
            </td>
        </tr>
       
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
