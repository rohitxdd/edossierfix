<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AdmitCard_entry.aspx.cs" Inherits="AdmitCard_entry" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table width="100%">
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lbl" runat="server" Text="Print Your Admit Card" CssClass="formlabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 30%">
                <asp:Label ID="lbl_appno" runat="server" Text="Enter Application No."></asp:Label>
            </td>
            <td align="left" style="width: 70%">
                <asp:TextBox ID="txt_appno" runat="server" Text="" MaxLength="8"></asp:TextBox>
                <a href="know_appno.aspx" target="_blank" style="height: 200px; width: 200px">Know Your
                    Application No.</a>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 30%">
                <asp:Label ID="lbl_dob" runat="server" Text="Enter Date of Birth"></asp:Label>
            </td>
            <td align="left" style="width: 70%">
                <asp:TextBox ID="txt_dob" runat="server" Text="" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbl_captcha" runat="server" Text="Security Code"></asp:Label>
            </td>
            <td align="left">
                <img alt="Visual verification" height="50" src='JpegImage_CS.aspx?r=<%= System.Guid.NewGuid().ToString("N") %>'
                    title="Please enter the Visual Code as shown in the image." vspace="5" width="150" />
                <asp:ImageButton ID="ibtnRefresh" runat="server" ImageUrl="~/images/refresh.jpg"
                    OnClick="ibtnRefresh_Click" OnClientClick="return SignValidateRefresh();" ToolTip="Click here to load a new Image" />
                Type the code shown
                <asp:TextBox ID="txtCode" runat="server" autocomplete="off" AutoCompleteType="None"
                    MaxLength="10" oncopy="return false" oncut="return false" onpaste="return false"
                    TabIndex="7" ToolTip="Enter Above Characters in the Image" />
                &nbsp;<asp:RequiredFieldValidator ID="RFVCaptcha" runat="server" ControlToValidate="txtCode"
                    Display="None" ErrorMessage="Enter Visual Code" SetFocusOnError="True" ToolTip="Visual Code"
                    ValidationGroup="1" Width="150px">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
            <td align="left">
                <asp:Button Text="Submit" ID="btn_submit" runat="server" ValidationGroup="1" OnClick="btn_submit_Click" />
        </tr>
    </table>
</asp:Content>
