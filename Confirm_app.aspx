<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    EnableSessionState="True" CodeFile="Confirm_app.aspx.cs" Inherits="Confirm_app"
    Title="Untitled Page" %>

<%--<%@ Register Src="~/usercontrols/appnumber.ascx" TagName="appno" TagPrefix="no" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table style="width: 100%" id="tblcon" runat="server">
        <tr>
            <td>
                <strong>Confirm Application</strong>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblappno" runat="server" Text="Select Post Applied" CssClass="formheading"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList_post" runat="server" CssClass="ddl" Width="500px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lbl" runat="server" CssClass="ariallightgrey" Style="font-weight: 700"
                    Visible="False"></asp:Label>
                <br />
                <asp:Button ID="btn_confirm" runat="server" OnClick="btn_confirm_Click" Text="Submit"
                    Visible="true" Width="118px" CssClass="cssbutton" />

            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:ImageButton ID="img_btn_prev" runat="server" Height="34px" ImageUrl="~/Images/prev.jpg"
                    Width="52px" Visible="true" OnClick="img_btn_prev_Click" />
            </td>
        </tr>
        <tr align="left">
            <td style="height: 16px">
                <asp:Label ID="lbl_step" runat="server" Text="Step 4/5" ForeColor="DarkGreen" Font-Bold="True"
                    Font-Italic="True" Visible="false">
                </asp:Label>
            </td>
        </tr>
    </table>
    <%--<asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Text="Nothing Pending"
                    Visible="False"></asp:Label>--%>
    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Text=""
        Visible="False"></asp:Label>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
