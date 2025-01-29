<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EdossierOtherDoc.aspx.cs" Inherits="EdossierOtherDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table id="tbldata" runat="server" style="width: 90%; height: 352px; border-right: blue thin solid;
        border-top: blue thin solid; border-left: blue thin solid; border-bottom: blue thin solid;">
        <tr>
            <td align="left">
                <strong>Post:&nbsp;<asp:Label ID="lblpostcode" runat="server"></asp:Label>&nbsp;|&nbsp;Roll
                    No.:&nbsp;<asp:Label ID="lblrollno" runat="server"></asp:Label>
                </strong>
            </td>
        </tr>
           <tr>
            <td id="tredno" runat="server" visible="false">
                <asp:Label ID="lbledno" runat="server" CssClass="formheading" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td  id="trmisc1" runat="server" visible="false">
                <asp:Label ID="lblmisdoc" runat="server" CssClass="formheading" Text="List of Other Miscellaneous Documents (if any) (Maximum 7)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  id="trmisc2" runat="server" visible="false" align="left">
                <span style="color: red">
                    <asp:Label ID="Label23" runat="server" Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>
                </span>
            </td>
        </tr>
        <tr id="trmisc" runat="server" visible="false">
            <td align="center" colspan="3">
                <asp:GridView ID="grdmisc" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,othermiscdoc,remarks,editflag" Width="100%"
                    OnRowCommand="grdmisc_RowCommand" OnRowDataBound="grdmisc_RowDataBound" OnRowUpdating="grdmisc_RowUpdating"
                    OnRowCancelingEdit="grdmisc_RowCancelingEdit" OnRowDeleting="grdmisc_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details of Additional Document">
                            <ItemTemplate>
                                <%# Eval("othermiscdoc")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtcertificateReq" runat="server" Visible="false" AutoComplete="off"
                                    TextMode="MultiLine" Width="300px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revcertificateReq" runat="server" ControlToValidate="txtcertificateReq"
                                    Display="None" ErrorMessage="Invalid character in Details of Additional Document" ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$"
                                    ValidationGroup="2"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvcertificateReq" runat="server" Display="None"
                                    ControlToValidate="txtcertificateReq" ErrorMessage="Please Enter Details of Additional Document"
                                    ValidationGroup="2"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Certificate Type">
                            <ItemTemplate>
                                <%# Eval("ctypename")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false"
                                    Target="_blank" CausesValidation="true" />
                                <%-- <asp:HyperLink ID="hyviewdoc" runat="server" Text="View Document" Visible="false" Target="_blank"></asp:HyperLink>--%>
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:FileUpload ID="fileupload" runat="server" Visible="false" />
                                <asp:RequiredFieldValidator ID="rfvfile" runat="server" Display="None" ControlToValidate="fileupload"
                                    ErrorMessage="Please select file" ValidationGroup="2"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="400px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label>
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine"
                                    Width="95%" Height="80px" ToolTip="Maximum 200 characters allowed" Visible="false">
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine"
                                    Width="95%" Height="80px" ToolTip="Maximum 200 characters allowed" Visible="false">
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <%--  <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="Save"></asp:LinkButton>--%>
                                <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="false"></asp:LinkButton>
                                <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                <asp:LinkButton ID="lbremove" runat="server" CausesValidation="False" CommandName="Delete"
                                    Visible="false" Text="Remove" OnClientClick="return confirm('Are you sure to delete this record?');"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkadd" runat="server" CommandName="Add" Text="Add Another Document"
                                    ValidationGroup="2"></asp:LinkButton>
                                <asp:LinkButton ID="lnkIn" runat="server" CommandName="Insert" Text="Save" Visible="false"
                                    CausesValidation="true" ValidationGroup="2"></asp:LinkButton>
                                <asp:LinkButton ID="lnkC" runat="server" CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="2" ShowMessageBox="true"
                                    ShowSummary="false" />
                            </FooterTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table class="validatorstyles" border="1" width="80%">
                            <tr class="gridheading">
                                <td>
                                    Details of Additional Document
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Remarks
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtcertificateReq" runat="server" AutoComplete="off" TextMode="MultiLine"
                                        Width="300px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revcertificateReq" runat="server" ControlToValidate="txtcertificateReq"
                                        Display="None" ErrorMessage="Invalid character in Details of Additional Document" ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$"
                                        ValidationGroup="3"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvcertificateReq" runat="server" Display="None"
                                        ControlToValidate="txtcertificateReq" ErrorMessage="Please Enter Details of Additional Document"
                                        ValidationGroup="3"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileupload" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvfile" runat="server" Display="None" ControlToValidate="fileupload"
                                        ErrorMessage="Please select file" ValidationGroup="3"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine"
                                        Width="95%" Height="80px" ToolTip="Maximum 200 characters allowed" Visible="true">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                        Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                        ValidationGroup="1"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkIn" runat="server" CommandName="EInsert" Text="Save" CausesValidation="true"
                                        ValidationGroup="3"></asp:LinkButton>
                                    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="3" ShowMessageBox="true"
                                        ShowSummary="false" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
                <asp:HiddenField ID="HiddenField4" runat="server" Visible="false" />
            </td>
        </tr>
          <%-- created by heena 15/12/2022--%>
         <tr>
            <td align="left">
                <asp:Label ID="Label4" runat="server" Text="NOTE" Font-Bold="true" ></asp:Label>
            </td>
        </tr>
         <tr >
            <td align="left">
                <asp:Label ID="lbl_note2" runat="server" Text="1. All the three erstwhile corporations of Delhi have been unified w.e.f. 22.05.2022 as Municipal Corporation of Delhi. Hence, all the results which are to be declared by DSSSB for the requisition of either NDMC, SDMC or EDMC will be issued/declared in the name of Municipal Corporation of Delhi (MCD) as requested by MCD vide Letter No. AO (Estt.)/S.O-V/CED/MCD/2022/1889 dated 06.09.2022." ></asp:Label>
            </td>
        </tr>
         <tr >
            <td align="left">
                <asp:Label ID="lbl_note1" runat="server" Text="2. The Preference of departments is to be filled in order of priority i.e. first preference should be given for the department in which you want to be considered first for selection." ></asp:Label>
            </td>
        </tr>
         <tr >
            <td align="left">
                <asp:Label ID="lbl_note3" runat="server" Text="3. Candidates should give the preference for department posts for which they are eligible as per Recruitment Rules for the post, as on last date of submission of the application form. Options once exercised shall be treated as final / irreversible and will not be changed subsequently under any circumstances. Therefore, Candidates must be careful in excercise of such options." ></asp:Label>
            </td>
        </tr>
         <tr >
            <td align="left">
                <asp:Label ID="lbl_note4" runat="server" Text="4. Final Selection of Candidates, will be made on the basis of 'overall performance in tier-II Examination' and 'preference of posts' exercised by then. Once the candidate has been given his first available preference, as per his merit, he will not be considered for any other option. Candidates are, therefore, required to exercise preference very carefully. Subsequently request for change of allocation / Department by candidates will not be entertained under any circumstances / reasons." ></asp:Label>
            </td>
        </tr>
         <tr >
            <td align="left">
                <asp:Label ID="lbl_note5" runat="server" Text="5. The DSSSB shall make final allotment of posts on the basis of merit cum preference of posts given by the candidates and once a post is allotted, no change of posts will be made by the DSSSB due to non-fulfillment of any post specific requirements of age/physical/medical/educational standards, etc. For example if a candidate has given a preference for a post and is allotted that postby DSSSB as per his merit and subsequentlyhis candidature is rejected by the User Department on account of failure to meet the age/medical/physical/educational standards, the candidate shall not be considered by DSSSB for allotment from other remaining preferences." ></asp:Label>
            </td>
        </tr>
        <tr >
            <td align="left">
                <asp:Label ID="lbl_note6" runat="server" Text="6. Order of preference can not be changed after clicking on the button 'Submit Final preference' ." ></asp:Label>
            </td>
        </tr>
         <tr>
            <td align="left">
                <asp:Label ID="Lbl_prefer" runat="server" CssClass="formheading" Text="List of Departments for the preferences" visible="false"></asp:Label>
            </td>
        </tr>
         <tr>
            <td  id="td_msg" runat="server" visible="false" align="left">
                <span style="color: red">
                    <asp:Label ID="Label3" runat="server" Text="First Preference is Mandatory."></asp:Label>
                </span>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:GridView ID="grdpreferdept" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" Width="100%" OnRowDataBound="grdpreferdept_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="preference">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                               <asp:DropDownList ID="ddl_dept" runat="server"></asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td  id="Td1" runat="server" visible="false">
                <asp:Label ID="Label2" runat="server" CssClass="formheading" Text="List of selected department"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:GridView ID="grdselecteddept" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Preference">
                            <ItemTemplate>
                                <%# Eval("preference")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                               <%# Eval("Preferdepartmentname")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Button  ID="btn_save" Text="Save" runat="server" CssClass="buttonFormLevel" Visible="false" Height="28px" Width="88px" OnClick="btn_save_Click"  />
                 &nbsp;
                <asp:Button  ID="btn_edit" Text="Edit" runat="server" CssClass="buttonFormLevel" Visible="false" Height="28px" Width="88px" OnClick="btn_edit_Click"  />
                 &nbsp;
                 <asp:Button ID="Btn_finalsubmit" Text="Submit Final preference" runat="server" CssClass="buttonFormLevel" Height="28px" Width="160px" Visible="false" OnClick="Btn_finalsubmit_Click" OnClientClick= "return confirm('Are you sure you want to proceed.')" />
                 &nbsp;
                <asp:Button  ID="btn_viewdownload" Text="View/Download" runat="server" CssClass="buttonFormLevel" Visible="false" Height="28px" Width="107px" OnClick="btn_viewdownload_Click"  />
                &nbsp;
                <asp:Button ID="btn_up" Text="Upload" runat="server" CssClass="buttonFormLevel" Height="28px" Width="88px" OnClick="btn_up_Click" visible="false"/>
                  &nbsp;
                <asp:FileUpload ID="fu_upload" runat="server" visible="false"/>
                &nbsp;
                <asp:Button ID="btn_upload" Text="Upload" runat="server" CssClass="buttonFormLevel" Height="28px" Width="88px" OnClick="btn_upload_Click" visible="false"/>
            </td>
        </tr>
         <tr>
            <td align="left">
               <asp:GridView ID="grdviewdoc" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" Width="100%" OnRowDataBound="grdviewdoc_RowDataBound" Visible="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Uploaded Preference">
                            <ItemTemplate >
                                <asp:HyperLink ID="hyviewdoc1" runat="server" ImageUrl="~/Images/pdf.png" 
                                    Target="_blank" CausesValidation="true" />
                                 </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
            </td>
        </tr>
         <%-- created by heena 15/12/2022--%>
         <tr><td align="left"><asp:CheckBox ID="chkdis" runat="server" /><b>Declaration</b><br />
        i) That the information provided above is true to the best of my knowledge and belief.<br />
        ii) That I fulfill all the eligibility conditions as prescribed in the Advertisement for which I am claiming my candidature.<br />
        iii) That I also understand that in case any information is found to be wrong/misleading or any documents uploaded by me found to be forged or incorrect or not in coherence
        with the prescribed requirement,then my candidature is liable to be rejected,besides warranting legal/criminal action,if any.</td></tr>
        <tr>
            <td colspan="3">
                <asp:Button Text="Final Submit" runat="server" ID="btnfinal" OnClick="btnfinal_Click"
                    CssClass="buttonFormLevel" OnClientClick="return confirm('Are you sure to Final Submit eDossier , No further editing will be allowed after Final Submit?');"
                    Visible="false" />
            </td>
        </tr>
        <tr class="formlabel" id="trfinalnote" runat="server" visible="false">
            <td align="left" colspan="3">
                <span style="color: red">#</span><asp:Label ID="Label1" runat="server" CssClass="formlabel"
                    Text=" After Uploading all the documents, it is mandatory to click on the Final Submit Button. Failing which, your eDossier will not be valid."></asp:Label>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
