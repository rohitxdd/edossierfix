<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeRequest.aspx.cs" Inherits="ChangeRequest" %>

<%@ Register Src="~/usercontrols/Header.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/Footer.ascx" TagName="WebUserControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Applicant.css" />
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 274px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" align="center" class="border_gray">
            <tr>
                <td>
                    <uc1:WebUserControl ID="Top1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table class="border_gray" width="950" align="center">
                        <tr>
                            <td align="right" colspan="2">
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/splcandidate.aspx">Back</asp:HyperLink>
                                <%-- <asp:Button ID="btn_back" runat="server" OnClick="btn_back_Click" Text="Back" Visible="False" />--%>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" class="tr">
                                <asp:Label ID="lblhead" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="formlabel">
                            <td align="right" colspan="2">
                                The fields with <span style="color: red">*</span> mark are mandatory.&nbsp;
                            </td>
                        </tr>
                        <tr class="formlabel" align="center" id="tridno" runat="server" visible="false">
                            <td align="right" class="style1">
                                <span style="color: red">*</span><asp:Label ID="Label1" runat="server" Text="ID No :"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtserialno" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_txtserialno" runat="server" ControlToValidate="txtserialno"
                                    Display="None" ErrorMessage="Please Enter ID No." ValidationGroup="1" />
                                <asp:RegularExpressionValidator ID="rev_txtserialno" runat="server" ControlToValidate="txtserialno"
                                    Display="None" ErrorMessage="Enter Valid ID No." ValidationExpression="^[0-9]*$"
                                    ValidationGroup="1">
                                </asp:RegularExpressionValidator>
                                <asp:Button ID="btncheck" runat="server" CssClass="cssbutton" Text="Check ID No."
                                    Width="91px" ValidationGroup="1" OnClick="btncheck_Click" />
                            </td>
                        </tr>
                        <tr class="formlabel">
                            <td colspan="2">
                                <table width="100%" id="tbl1" runat="server" visible="false">
                                    <tr class="formlabel">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label2" runat="server" Text="Date of Birth(dd/mm/yyyy) :"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtdob" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvdob" runat="server" Display="none" ControlToValidate="txtdob"
                                                ValidationGroup="2" ErrorMessage="Please Enter DOB.">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPIN" runat="server"
                                                ControlToValidate="txtdob" ValidationExpression=".{10}.*" ErrorMessage="Enter Valid DOB(DD/MM/YYYY)"
                                                ValidationGroup="2" Display="None"> 
                                            </asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="REV_date" runat="server" ControlToValidate="txtdob"
                                                Display="None" ErrorMessage="Enter Valid Date of Birth." ValidationExpression="(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d"
                                                ValidationGroup="2">
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr class="formlabel">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label3" runat="server" Text="10th Class Certificate :"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:FileUpload ID="fulcerti" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvfulcerti" runat="server" Display="none" ControlToValidate="fulcerti"
                                                ValidationGroup="2" ErrorMessage="Please Upload 10th Class Certificate.">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="formlabel">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label4" runat="server" Text="Acknowledgement Receipt :"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:FileUpload ID="fulack" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvfulack" runat="server" Display="none" ControlToValidate="fulack"
                                                ValidationGroup="2" ErrorMessage="Please Upload Acknowledgement Receipt.">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="formlabel">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label5" runat="server" Text="Mobile No. :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_mob" runat="server" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvmob" runat="server" Display="none" ControlToValidate="txt_mob"
                                                ValidationGroup="2" ErrorMessage="Please Enter Mobile No."></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revmob" runat="server" Display="None" ControlToValidate="txt_mob"
                                                ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                                                ValidationGroup="2">
                                            </asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="revnum" runat="server" ControlToValidate="txt_mob"
                                                ValidationExpression=".{10}.*" ErrorMessage="Maximum 10 digit numbers are allowed."
                                                Display="none" ValidationGroup="2">
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr class="formlabel" visible="false" id="trname" runat="server">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label6" runat="server" Text="Name :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvname" runat="server" Display="none" ControlToValidate="txtname"
                                                ValidationGroup="2" ErrorMessage="Please Enter Name.">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="formlabel" visible="false" id="trfname" runat="server">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label7" runat="server" Text="Father's Name :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtfname" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvfname" runat="server" Display="none" ControlToValidate="txtfname"
                                                ValidationGroup="2" ErrorMessage="Please Enter Father's Name.">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="formlabel" visible="false" id="trpostcode" runat="server">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label8" runat="server" Text="Post Code :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlpost" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvpost" runat="server" Display="none" ControlToValidate="ddlpost"
                                                ValidationGroup="2" ErrorMessage="Please Select Post Code.">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnsubmit" runat="server" CssClass="cssbutton" Text="Submit" Width="91px"
                                                ValidationGroup="2" OnClick="btnsubmit_Click" />
                                            <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                ValidationGroup="2" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Button ID="btnclear" runat="server" CssClass="cssbutton" Text="Reset" Width="91px"
                                                OnClick="btnclear_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:WebUserControl ID="Footer" runat="server" />
                </td>
            </tr>
        </table>
        <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </div>
    </form>
</body>
</html>
