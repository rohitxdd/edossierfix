<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PayOnline.aspx.cs" Inherits="PayOnline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    
    <table>
        <tr id="trcha" runat="server">
            <td>
                <table style="background-color: #DDDDFF" id="tblcha" runat="server">
		    <tr>
                        <td align="left" style="height: 20px;" colspan="2">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Note: Applicants may please click on the Re-verify Payment option/button if you have already paid application fees but the fee status is showing pending." Font-Bold="True"
                                CssClass="formheading"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px;" colspan="2">

                        </td>
                    </tr>
                    <tr id="truser" runat="server">
                        <td align="right" style="width: 240px; height: 20px;">
                            <asp:Label ID="lbljob" runat="server" Text="Select Post Applied" Font-Bold="True"
                                CssClass="formheading"></asp:Label>
                        </td>
                        <td align="left" style="height: 20px">
                            <asp:DropDownList ID="ddjob" runat="server" CssClass="ddl" Width="400px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvjob" runat="server" ControlToValidate="ddjob"
                                ErrorMessage="Please Select Job"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
		    <tr>
                        <td style="height: 20px;" colspan="2">

                        </td>
                    </tr>
                    <tr id="Tr1" runat="server" visible="true">
                        <td align="center" colspan="2">
                             <asp:Button ID="btnverifystatus" runat="server" OnClick="btnverifystatus_Click" Text="Re-Verify Payment Status (If already paid)"
                                CssClass="cssbutton" ToolTip="If already paid,then Re-Verify Payment Status"
                                Width="290px" />
                            &nbsp;
                            <asp:Button ID="btn_print_c" runat="server" OnClick="Button1_Click" Text="Pay Online"
                                Width="98px" CssClass="cssbutton" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="lbl_step" runat="server" Text="Step 5/5" ForeColor="DarkGreen" Font-Bold="True"
                                Font-Italic="True" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="LabelNote" runat="server" Visible="False" CssClass="formheading" ForeColor="#C00000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Text="Fee Exemption(No Need of Payment)"
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
