<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewStatus.aspx.cs" Inherits="ViewStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table width="100%" align="center">
        <tr align="center">
            <td>
                <asp:Label ID="lbljob" runat="server" Text="Select Post Applied" Font-Bold="True"
                    CssClass="formheading" Width="132px"></asp:Label>
                <asp:DropDownList ID="ddjob" runat="server" CssClass="ddl" Width="400px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvjob" runat="server" ControlToValidate="ddjob"
                    ErrorMessage="Please Select Post"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="center">
            <td align="center">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" Width="54px"
                    CssClass="cssbutton" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="50%" align="center" id="tbl1" runat="server" visible="false">
                    <tr>
                        <td align="right" style="width: 50%">
                            Application Number :&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="lblano" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 50%">
                            Date of Submission of Application :&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="lbldt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trfee" runat="server" visible="false">
                        <td align="right" style="width: 50%">
                            Date of Fee Payment :&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="lblfeedt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr1" runat="server" visible="false">
                        <td align="right" style="width: 50%">
                            Identity Proof Detail :&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="lbltr1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr2" runat="server" visible="false">
                        <td align="right" style="width: 50%">
                            Post card Size Photograph :&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="lbltr2" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
