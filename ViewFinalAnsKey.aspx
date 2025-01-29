<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewFinalAnsKey.aspx.cs" Inherits="ViewFinalAnsKey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <table style="text-align: center" width="100%">
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblHead" runat="server" Text="Final Answer Key"
                    CssClass="formheading"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" ><span style="color: Red">* - Revised Answer</span></td></tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblappno" runat="server" Text="Select Exam." CssClass="formheading"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlexam" runat="server" OnSelectedIndexChanged="ddlexam_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnsubmit" runat="server" Text="Submit"
                    CssClass="buttonFormLevel" OnClick="btnsubmit_Click" />
            </td></tr>
        <tr id="trques" runat="server" visible="false" valign="top" >
            <td align="left" valign="top">
                <asp:Label ID="Label1" runat="server" Text="Select Question" CssClass="formheading"></asp:Label><br/>
                 <asp:DropDownList ID="ddlques" runat="server" OnSelectedIndexChanged="ddlques_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td align="left" valign="top">
                <asp:Label ID="Label2" runat="server" Text="Final Answer" CssClass="formheading"></asp:Label><br />
                <asp:Label ID="lblans" runat="server" ></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfexamid" runat="server" />
    <asp:HiddenField ID="hfbatchid" runat="server" />
</asp:Content>

