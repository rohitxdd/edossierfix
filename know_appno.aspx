<%@ Page Language="C#" AutoEventWireup="true" CodeFile="know_appno.aspx.cs" Inherits="know_appno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="css/dsssbstylesheet.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/Applicant.css" />
    
    
 <script type="text/javascript" language="javascript">
     /*
     Auto tabbing script http://codingcluster.blogspot.in/
     */
     function autoTab(current, next) {
         if (current.getAttribute && current.value.length == current.getAttribute("maxlength"))
             next.focus()
     }

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table>
            <tr>
                <td align="center">
                    <table width="250px" align="center" cellpadding="1" cellspacing="1">
                        <tr>
                            <td align="left">
                                <asp:TextBox autofill="false" autocomplete="false" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                    ID="txt_dd" runat="server" Width="20px" MaxLength="2" onkeyup="autoTab(this, document.form1.txt_mm)"
                                    onfocus="javascript:this.value=''" TabIndex="1"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:TextBox autofill="false" autocomplete="false" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                    ID="txt_mm" runat="server" Width="20px" MaxLength="2" onkeyup="autoTab(this, document.form1.txt_yyyy)"
                                    onfocus="javascript:this.value=''" TabIndex="2"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:TextBox autofill="false" autocomplete="false" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                    ID="txt_yyyy" runat="server" Width="50px" MaxLength="4" onkeyup="autoTab(this, document.form1.txt_regno)"
                                    onfocus="javascript:this.value=''" TabIndex="3"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:TextBox autofill="false" autocomplete="false" ViewStateMode="Disabled" AutoCompleteType="Disabled"
                                    ID="txt_regno" runat="server" Width="70" onfocus="javascript:this.value=''" MaxLength="15"
                                    TabIndex="4"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownList_year" runat="server" TabIndex="5" Width="70px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="datetimelabletext">
                                DD
                            </td>
                            <td align="left" class="datetimelabletext">
                                MM
                            </td>
                            <td align="left" class="datetimelabletext">
                                YYYY
                            </td>
                            <td align="left" class="datetimelabletext">
                                10th Roll No.
                            </td>
                            <td align="left" class="datetimelabletext">
                                10th Passing Year
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" class="datetimelabletext" align="left">
                                (Date of Birth)
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" class="datetimelabletext" align="center">
                            <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="" ControlToValidate="txt_dd" Display="None" ValidationGroup="1">
            </asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="revDD" runat="server" ControlToValidate="txt_dd"
                     Display="None" ErrorMessage="" ValidationExpression=".{2}.*"
                     ValidationGroup="1">
                     </asp:RegularExpressionValidator>
                 <asp:RegularExpressionValidator ID="REVDD1" runat="server" ControlToValidate="txt_dd"
                     Display="None" ErrorMessage="" ValidationExpression="^[0-9]*$"
                     ValidationGroup="1"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RFV2" runat="server" ErrorMessage="" ControlToValidate="txt_mm" Display="None" ValidationGroup="1">
            </asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="REVMM" runat="server" ControlToValidate="txt_mm"
                     Display="None" ErrorMessage="" ValidationExpression=".{2}.*"
                     ValidationGroup="1"></asp:RegularExpressionValidator>
                 <asp:RegularExpressionValidator ID="REVmm1" runat="server" ControlToValidate="txt_mm" 
                     Display="None" ErrorMessage="" ValidationExpression="^[0-9]*$"
                     ValidationGroup="1"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RFV3" runat="server" ErrorMessage="" ControlToValidate="txt_yyyy" Display="None" ValidationGroup="1">
            </asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="revyy" runat="server" ControlToValidate="txt_yyyy"
                     Display="None" ErrorMessage="" ValidationExpression=".{4}.*"
                     ValidationGroup="1"></asp:RegularExpressionValidator>
                 <asp:RegularExpressionValidator ID="revyy1" runat="server" ControlToValidate="txt_yyyy"
                     Display="None" ErrorMessage="" ValidationExpression="^[0-9]*$"
                     ValidationGroup="1"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RFV4" runat="server" ErrorMessage="" ControlToValidate="txt_regno" Display="None" ValidationGroup="1">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RFV5" runat="server" ErrorMessage="" ControlToValidate="DropDownList_year" Display="None" ValidationGroup="1" InitialValue="-1">
            </asp:RequiredFieldValidator>
                                <asp:Button ID="txt_submit" runat="server" Text="Submit" OnClick="txt_submit_Click" ValidationGroup="1"/>
                                 <asp:ValidationSummary ID="vs" HeaderText="Please enter value."
                                    runat="server" ValidationGroup="1" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                        <tr>
                        <td colspan="5">
                        <asp:GridView ID="grdappno" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                        <asp:BoundField HeaderText="Post" DataField="jobdetails" ItemStyle-Width="70%"/>
                        <asp:BoundField HeaderText="Application No" DataField="dummy_no"/>
                        </Columns>
                        </asp:GridView>
                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
