<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CPayment.aspx.cs" Inherits="CPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table id="tbl1" style="text-align: center" width="60%">
        <tr>
            <td align="left">
                <asp:Label ID="lblRgNo" runat="server" Text="Registration No" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblregno" runat="server" Text="" CssClass="formlabel"></asp:Label>
            </td>
        </tr>
         <tr>
            <td align="left">
                <asp:Label ID="lblmbl" runat="server" Text="Mobile Number" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblMobile" runat="server" Text="" CssClass="formlabel"></asp:Label>&nbsp;
                <asp:Button ID="btn_edit" runat ="server" Text ="Update Mobile No" 
                    CssClass ="button_text" onclick="btn_edit_Click" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblnoofques" runat="server" Text="Total No of Questions Challenged"
                    CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="LblNoffQues" runat="server" Text="" CssClass="formlabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblamt" runat="server" Text="Amount Per Question" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblAmount" runat="server" Text="" CssClass="formlabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lbltotal" runat="server" Text="Total Amount to be Paid" CssClass="formlabel"></asp:Label><br />
                <asp:Label ID="lbltotl" runat="server" Text="(Equal to No of Questions * Amount)"
                    CssClass="formlabel "></asp:Label>
            </td>
            <td align="left" valign="top">
                <asp:Label ID="LblTotalAmt" runat="server" Text="" CssClass="formlabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Label ID="lblRefndtls" runat="server" Text="Enter Refund Details" CssClass="formlabel"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblbnkName" runat="server" Text="Bank Name" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtBnkName" runat="server" CssClass="formtext" Height="19px" Width="154px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblBrnch" runat="server" Text="Name of the Branch" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtBnkBrnch" runat="server" CssClass="formtext" Height="19px" Width="154px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblAccNo" runat="server" Text="Account No" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtBnkAccNo" runat="server" CssClass="formtext" Height="19px" Width="154px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblTypeofAcc" runat="server" Text="Type of Account" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:RadioButtonList ID="rbltype" CssClass="formlabel" runat="server" RepeatLayout="Flow"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="C" Text="Current"></asp:ListItem>
                    <asp:ListItem Value="S" Text="Saving"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblAccHoldername" runat="server" Text="Account Holder's Name" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtAccHolName" runat="server" CssClass="formtext" Height="19px"
                    Width="154px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblifsc" runat="server" Text="IFSC Code" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtBnkIfsc" runat="server" CssClass="formtext" Height="19px" Width="154px"></asp:TextBox>
                <asp:HiddenField ID="HdnCRid" runat="server" Visible="false" />
                 <asp:HiddenField ID="HdnChalId" runat="server" Visible="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Button ID="btnSave" Text="Save" runat="server" Visible="false" CssClass="button_text "
                    OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnEdit" Visible="false" runat="server" Text="Change Refund Details" CssClass="button_text "
                    OnClick="btnEdit_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnPay" Visible="false" runat="server" Text="Pay Now" CssClass="button_text "
                    OnClick="btnPay_Click" />
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_text"
                    Visible="false" OnClick="btnCancel_Click" />
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
