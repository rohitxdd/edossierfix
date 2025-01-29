<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeeReciept.aspx.cs" Inherits="FeeReciept" %>

<link rel="stylesheet" type="text/css" href="CSS/Applicant.css" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr align="center">
            <td align="center">
                <asp:Panel ID="pnlverify" runat="server" Width="100%" HorizontalAlign="Center">
                    <table width="40%" id="tbl_fee" runat="server" visible="false" class="formlabel"
                        border="1" cellpadding="1" cellspacing="1">
                        <tr align="left">
                            <td colspan="2"  align="center">
                               <asp:Label ID="lbl_feerecpt" runat="server" CssClass="formheading" Text="Fee Reciept"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 203px">
                                Name of Candidate
                            </td>
                            <td>
                                <asp:Label ID="lblname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 203px">
                                Post Applied
                            </td>
                            <td>
                                <asp:Label ID="lblpost" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 203px">
                                Bank Reference No
                            </td>
                            <td>
                                <asp:Label ID="lblbankrefno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 203px">
                                Amount
                            </td>
                            <td>
                                <asp:Label ID="lblamount" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr align="left">
                            <td style="width: 203px">
                               Payment Mode
                            </td>
                            <td>
                                <asp:Label ID="lblpaymode" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr align="left">
                            <td style="width: 203px">
                                Transaction Date
                            </td>
                            <td>
                                <asp:Label ID="lbltrandate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
