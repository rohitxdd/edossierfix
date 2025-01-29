<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="GrievanceEntry.aspx.cs" Inherits="GrievanceEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td align="left">
                Name :
                <asp:Label ID="lblname" runat="server"></asp:Label>
            </td>
            <td align="left">
                Reg No :
                <asp:Label ID="lblregno" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                Father's Name :
                <asp:Label ID="lblfname" runat="server"></asp:Label>
            </td>
            <td align="left">
                Mother's Name :
                <asp:Label ID="lblmname" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" valign="top">
                Category :
                <asp:DropDownList ID="ddlcat" runat="server" 
                    onselectedindexchanged="ddlcat_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlsubcat" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="trrbt" runat="server" visible="false">
            <td colspan="2" align="left">
                <asp:RadioButtonList ID="rblwise" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rblwise_SelectedIndexChanged">
                    <asp:ListItem Text="Post-Wise" Value="P"></asp:ListItem>
                    <asp:ListItem Text="Exam-Wise" Value="E"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvrblwise" runat="server" Display="None" ControlToValidate="rblwise"
                    ErrorMessage="Please select Post-Wise/Exam-Wise" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trpost" runat="server" visible="false" valign="top">
            <td colspan="2" align="left">
                Post :
                <asp:DropDownList ID="ddlpost" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlpost" runat="server" Display="None" ControlToValidate="ddlpost"
                    ErrorMessage="Please select Post" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trexam" runat="server" visible="false" valign="top">
            <td colspan="2" align="left">
                Exam :
                <asp:DropDownList ID="ddleaxm" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddleaxm" runat="server" Display="None" ControlToValidate="ddleaxm"
                    ErrorMessage="Please select Exam" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left" valign="top">
                Grievance Description :
                <asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine" Height="80px" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtdesc" runat="server" Display="None" ControlToValidate="txtdesc"
                    ErrorMessage="Please enter Grievance Description" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left" valign="top">
                Upload Screenshot, if any :
                <asp:FileUpload ID="fpscreen" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:CheckBox ID="chkaceept" runat="server" Text="I have read the FAQs & the solution doesn't exist there" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="buttonStyle" 
                    ValidationGroup="1" onclick="btnsubmit_Click" />
                <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="1" />
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
