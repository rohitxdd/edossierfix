<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="challengeanswersheet.aspx.cs" Inherits="challengeanswersheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkoption3.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>
    <table style="text-align: center" width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="lblappno" runat="server" Text="Select Exam." CssClass="formheading"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlexam" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlexam_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tratndnc" runat="server" visible="false">
            <td colspan="2" align="center" cssclass="formheading">
                You have not Appeared in this Exam.
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table id="tbl1" width="100%" runat="server" visible="false">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Your Challenges of Answer Key for Selected Exam"
                                CssClass="formheading"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="Post Applied " CssClass="formheading"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="lblposts" runat="server" Text="" CssClass="formheading"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="Grdalrdchlge" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                CssClass="gridfont" Width="100%" DataKeyNames="ChallengeID,exambookletid,CPdID,status,cstatus"
                                OnRowDataBound="Grdalrdchlge_RowDataBound" OnRowCommand="Grdalrdchlge_RowCommand">
                                <HeaderStyle VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Question Booklet No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBooklet" runat="server" Text='<%# Eval("bookletcode")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Question No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestionNo" runat="server" Text='<%# Eval("QuestionNo")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="50px" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Answer as per DSSSB">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRightAns" runat="server" Text='<%# Eval("ans")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Suggested Answer as per Candidate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOptions" runat="server" Text='<%# Eval("sanswer")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supported Document">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hldoc" runat="server" Text="Download" Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fee Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeeStatus" runat="server" Text='<%# Eval("feestatus")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Challenge Status" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcStatus" runat="server" Text='<%# Eval("cstatus1")%>'></asp:Label>
                                            <br /><asp:Label ID="lblrefundstatus" runat="server" Visible="false" Text="Refund Processed"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkverify" runat="server" CommandName="Verify" Text="Re-Verify"
                                                Visible="false" CommandArgument='<%#Container.DataItemIndex%>'></asp:LinkButton>
                                            
                                            <br>
                                            
                                            </br>
                                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Del" Text="Delete" Visible="false"
                                                OnClientClick="return confirm('Are you sure you want to delete this record');"
                                                CommandArgument='<%#Container.DataItemIndex%>'></asp:LinkButton>
                                            <br>
                                            </br>
                                            <asp:LinkButton ID="lnkPrintAck" runat="server" CommandName="PrintAck" Text="Print Acknowledgement"
                                                Visible="false" CommandArgument='<%#Container.DataItemIndex%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trFeeAmount" runat="server" visible="false">
                        <td align="left">
                            <asp:Label ID="lblFeeamt" runat="server" Text="Fee Per Question : Rs " CssClass="formheading"></asp:Label>
                            <asp:Label ID="lblamt" runat="server" CssClass="formheading"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="total" runat="server" Text="Total Amount To be Paid : Rs " CssClass="formheading"></asp:Label>
                            <asp:Label ID="lbltotal" runat="server" CssClass="formheading"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnchallenge" runat="server" Text="Submit Request to Challenge Answer Key"
                                CssClass="buttonFormLevel" OnClick="btnchallenge_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnPay" runat="server" Text="Proceed For Payment" Visible="false"
                                CssClass="buttonFormLevel" OnClick="btnPay_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr id="trDesc" runat="server" visible="false">
                        <td colspan="2" align="left">
                            <p align="justify" style="color: Red" cssclass="formheading">
                                Disclaimer: If you have any objection in any of the Answer Keys, you can raise objections
                                for the same. You have to provide the answer, you think is correct by selecting
                                any one of the options provided. You can provide your remarks within 300 characters
                                and also you can upload scanned copies of supporting documents for each objection
                                (in PDF/JPEG format with max size upto 1MB). You will be charged a fee of Rs 1000/-
                                for each objection, which is refundable to you throught RTGS/NEFT without any interest
                                in case your objection is found valid and which will be forfeited if your objection
                                is found invalid.
                            </p>
                            <asp:CheckBox ID="chkDesclaimer" runat="server" CssClass="formheading" Text="I Agree"
                                align="left" OnCheckedChanged="chkDesclaimer_CheckedChanged" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <table width="100%" runat="server" id="tbl2" align="center" visible="false">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblHd" runat="server" Text="Submit Your Challenge" CssClass="formheading"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 300px">
                            <asp:Label ID="Label5" runat="server" Text="Select the Question Booklet No" CssClass="formheading"></asp:Label>
                        </td>
                        <td align="left" valign="top" colspan="2">
                            <asp:DropDownList ID="ddlquesbookno" runat="server" ValidationGroup="1" CssClass="formheading"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlquesbookno_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvQbook" runat="server" ControlToValidate="ddlquesbookno"
                                Display="None" ErrorMessage="Select Question Booklet No." ValidationGroup="1"></asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/pdf.png" Height="30px" 
                                Width="30px" OnClick="ImageButton1_Click" ToolTip="Click here to View/Download"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label7" runat="server" Text="Select Question No" CssClass="formheading"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:DropDownList ID="ddlquesno" runat="server" ValidationGroup="1" CssClass="formheading"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlquesno_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvquesno" runat="server" ControlToValidate="ddlquesno"
                                Display="None" ErrorMessage="Select Question No." ValidationGroup="1"></asp:RequiredFieldValidator>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label8" runat="server" Text="According to DSSSB the Answer is" CssClass="formheading"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:Label ID="lbldsssbans" CssClass="formheading" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="troption" runat="server" visible="false">
                        <td align="left" valign="top">
                            <asp:Label ID="lblRadioList" runat="server" Text="Select Options " CssClass="formheading"></asp:Label>
                        </td>
                        <td align="left" width="40%">
                            <asp:RadioButtonList ID="rbloptions" CssClass="formheading" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="rbloptions_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="Options Other than the Answer Key"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Multiple Answers"></asp:ListItem>
                                <asp:ListItem Value="3" Text="None of the Options given"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvoption" runat="server" ErrorMessage="Select atleast one Option"
                                ControlToValidate="rbloptions" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left" valign="top">
                            <asp:RadioButtonList ID="rbloptions2" CssClass="formheading" runat="server" RepeatLayout="Flow"
                                RepeatDirection="Horizontal" Visible="false" ValidationGroup="1">
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select atleast one Option"
                                ControlToValidate="rbloptions2" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator><br />
                            <asp:CheckBoxList ID="chkoption3" runat="server" Visible="false" CssClass="formheading"
                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 300px" valign="top">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="formheading"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtRemarks" CssClass="formheading" runat="server" TextMode="MultiLine"
                                Height="86px" Width="307px" MaxLength="300"></asp:TextBox>
                            <asp:Label ID="lblvalid" runat="server" Text="(Maximum Characters is 300)" ForeColor="Red"></asp:Label>
                            <asp:RequiredFieldValidator ID="RfRemarks" runat="server" ErrorMessage="Please enter Remarks"
                                ControlToValidate="txtRemarks" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="revremarks" runat="server" ControlToValidate="txtRemarks"
                                                Display="None" ErrorMessage="Please Enter Valid Character's in Remarks." ValidationExpression="^[a-zA-Z0-9&amp;.(),\s]{0,300}$"
                                                ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 300px">
                            <asp:Label ID="lblUpload" runat="server" Text="Upload Document In Support" CssClass="formheading"> </asp:Label>
                            <br />
                            <asp:Label ID="lblupld" runat="server" Text="(In PDF/JPEG Format)" CssClass="formheading">
                            </asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:FileUpload ID="uploaddoc" runat="server" />
                            <asp:Label ID="lbluploadd" runat="server" Text="(Maximum Size is 1 Mb)" ForeColor="Red"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfileupload" runat="server" ControlToValidate="uploaddoc"
                                Display="none" ErrorMessage="Please Upload Document In Support" ValidationGroup="1"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="1" OnClick="btnSubmit_Click"
                                CssClass="buttonFormLevel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btncancel" runat="server" Text="Cancel" Visible="false" CssClass="buttonFormLevel"
                    OnClick="btncancel_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
        ValidationGroup="1" />
    <asp:HiddenField ID="hdnapplid" runat="server" />
    <asp:HiddenField ID="hfsletterissue" runat="server" />
    <asp:HiddenField ID="hfexamid" runat="server" />
    <asp:HiddenField ID="hfbatchid" runat="server" />
</asp:Content>
