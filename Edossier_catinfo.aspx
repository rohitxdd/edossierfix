<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Edossier_catinfo.aspx.cs" Inherits="Edossier_catinfo" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript" language="javascript" src="Jscript/JScript.js">
</script>
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" >
        </cc1:ToolkitScriptManager>
    <table id="tbldata" runat="server" style="width: 90%; height: 352px; border-right: blue thin solid;
        border-top: blue thin solid; border-left: blue thin solid; border-bottom: blue thin solid;">
        <tr>
            <td align="left" colspan="2">
                <strong>Post:&nbsp;<asp:Label ID="lblpostcode" runat="server"></asp:Label>&nbsp;|&nbsp;Roll
                    No.:&nbsp;<asp:Label ID="lblrollno" runat="server"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="tr">
                <strong>Details of Category and Sub-Category </strong>
            </td>
        </tr>
        <tr><td colspan="2" class="formlabel" align="right">The Fields with <span style="color:Red">*</span> mark are mandatory.</td></tr>
        <tr id="trcategory" runat="server" visible="false">
            <td colspan ="2" valign="top" align="left">
                <asp:Label ID="lblcat" runat="server" Text="Category :   " Font-Bold="True" CssClass="formlabel"></asp:Label>
            
                <asp:Label ID="lblcat1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
       
        <tr id="trcat" runat="server" visible="false">
            <td colspan="2" align="left">
                <table align="center" width="100%">
                <%-- <tr id="trstate" runat="server" visible="false"><td align="left" colspan="2"><asp:RadioButtonList ID="rbtobcregion" RepeatDirection="Horizontal" runat="server" CssClass="gridfont">
                 <asp:ListItem Value="D">OBC Delhi</asp:ListItem>
                  <asp:ListItem Value="O">OBC Outside Delhi</asp:ListItem>
                 </asp:RadioButtonList><span style="color:Red">*</span>
                 <asp:RequiredFieldValidator ID="rfvrbtobcregion" runat="server" Display="None" ControlToValidate="rbtobcregion"
                                ErrorMessage="Please select OBC State." ValidationGroup="1"></asp:RequiredFieldValidator>
                 </td></tr>--%>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcatcertno" runat="server" CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcatcertno" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcatcertno" runat="server" Display="None" ControlToValidate="txtcatcertno"
                                ErrorMessage="Please enter Certificate Number" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcatcertissuedate" runat="server" CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcatcertissuedate" runat="server" CssClass="gridfont" Width="300px" ReadOnly="false" placeholder="dd/mm/yyyy" ToolTip="Enter in dd/mm/yyyy format"></asp:TextBox>
                             <%-- <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtcatcertissuedate">
                    </cc1:CalendarExtender>--%>
                            <asp:RequiredFieldValidator ID="rfvtxtcatcertissuedate" runat="server" Display="None"
                                ControlToValidate="txtcatcertissuedate" ErrorMessage="Please enter Date of issuance of SC/ST/OBC Certificate"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtcatcertissuedate" runat="server" ControlToValidate="txtcatcertissuedate"
                                Display="None" ErrorMessage="Date of issuance of SC/ST/OBC Certificate should be in DD/MM/YYYY "
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcatcertissuedesig" runat="server" Text="iii) Designation of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcatcertissuedesig" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcatcertissuedesig" runat="server" Display="None"
                                ControlToValidate="txtcatcertissuedesig" ErrorMessage="Please enter Designation of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcatcertissuetehsil" runat="server" Text="iv) District of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcatcertissuetehsil" runat="server" CssClass="gridfont" Width="300px" Visible="false"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvtxtcatcertissuetehsil" runat="server" Display="None"
                                ControlToValidate="txtcatcertissuetehsil" ErrorMessage="Please enter Tehsil/District of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlcatcertissuetehsil" runat="server" CssClass="gridfont" ></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlcatcertissuetehsil" runat="server" Display="None"
                                ControlToValidate="ddlcatcertissuetehsil" ErrorMessage="Please select Tehsil/District of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcatcertissuestate" runat="server" Text="v) State of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcatcertissuestate" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcatcertissuestate" runat="server" Display="None"
                                ControlToValidate="txtcatcertissuestate" ErrorMessage="Please enter State of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr id="trcatdoc1" runat="server" visible="false">
            <td colspan="3">
                <asp:Label runat="server" ID="lblcategoryDoc" CssClass="formheading" Text="List of Documents related to Category"></asp:Label>
            </td>
        </tr>
        <tr id="trcatdoc2" runat="server" visible="false">
            <td colspan="3" align="left">
                <span style="color: red">
                    <asp:Label ID="Label21" runat="server" Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>
                </span>
            </td>
        </tr>
        <tr id="trcatdoc" runat="server" visible="false">
            <td align="center" colspan="3">
                <asp:GridView ID="grdcat" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,certificateReq,remarks" Width="100%"
                    OnRowCommand="grdcat_RowCommand" OnRowEditing="grdcat_RowEditing" OnRowDataBound="grdcat_RowDataBound"
                    OnRowUpdating="grdcat_RowUpdating" OnRowCancelingEdit="grdcat_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Required">
                            <ItemTemplate>
                                <%# Eval("category")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Type" Visible="false">
                            <ItemTemplate>
                                <%# Eval("ctypename")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false"
                                    Target="_blank" />
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label>
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine"
                                    Width="95%" ToolTip="Maximum 200 characters allowed" Height="80px" Visible="false">
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                 <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit"
                                    CommandArgument='<%# Container.DataItemIndex %>' Text="Save"></asp:LinkButton>                              
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>                               
                                <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="false"></asp:LinkButton>
                                <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
            </td>
        </tr>
        <tr id="trchkfobc" runat="server" visible="false"><td colspan="2" align="left">
            <asp:CheckBox ID="chkfobc" runat="server" 
                Text="OBC Certificate not issued to Father/Mother" CssClass="formlabel" 
                AutoPostBack="true"  oncheckedchanged="chkfobc_CheckedChanged" /></td></tr>
         <tr id="trcatf" runat="server" visible="false">
            <td colspan="2" align="left">
                <table align="center" width="100%">
                 <tr>
                        <td colspan="2" align="left">
                            Give the details of Father/Mother OBC Certificate(Certificate should be issued from Delhi)
                        </td>
                    </tr>
                     <tr>
                      <td valign="top" align="left">
                            <asp:Label ID="Label3" runat="server" CssClass="formlabel" Text="Certificate Holder Father/Mother"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td  align="left">
                          <asp:DropDownList ID="ddlOBCForM" runat="server" CssClass="gridfont"> 
                          <asp:ListItem Value="">Select</asp:ListItem>
                           <asp:ListItem Value="F">Father</asp:ListItem>
                           <asp:ListItem Value="M">Mother</asp:ListItem>
                           </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvddlOBCForM" runat="server" Display="None" ControlToValidate="ddlOBCForM"
                                ErrorMessage="Please select father/Mother" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcatcertno_f" runat="server" CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcatcertno_f" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcatcertno_f" runat="server" Display="None" ControlToValidate="txtcatcertno_f"
                                ErrorMessage="Please enter Father Certificate Number" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcatcertissuedate_f" runat="server" CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcatcertissuedate_f" runat="server" CssClass="gridfont" Width="300px" ReadOnly="true"></asp:TextBox>
                             <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtcatcertissuedate_f">
                    </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtcatcertissuedate_f" runat="server" Display="None"
                                ControlToValidate="txtcatcertissuedate_f" ErrorMessage="Please enter Date of issuance of Father SC/ST/OBC Certificate"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtcatcertissuedate_f" runat="server" ControlToValidate="txtcatcertissuedate_f"
                                Display="None" ErrorMessage="Date of issuance of Father SC/ST/OBC Certificate should be in DD/MM/YYYY "
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbltxtcatcertissuedesig_f" runat="server" Text="iii) Designation of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcatcertissuedesig_f" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcatcertissuedesig_f" runat="server" Display="None"
                                ControlToValidate="txtcatcertissuedesig_f" ErrorMessage="Please enter Designation of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbltxtcatcertissuestate_f" runat="server" Text="vi) State of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                        <asp:DropDownList ID="ddlcatcertissuestate_f" runat="server" CssClass="gridfont" 
                                onselectedindexchanged="ddlcatcertissuestate_f_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvcatcertissuestate_f" runat="server" Display="None"
                                ControlToValidate="ddlcatcertissuestate_f" ErrorMessage="Please select State of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                           
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbltxtcatcertissuetehsil_f" runat="server" Text="v) District of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                         <asp:DropDownList ID="ddlcatcertissuetehsil_f" runat="server" CssClass="gridfont" ></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvcatcertissuetehsil_f" runat="server" Display="None"
                                ControlToValidate="ddlcatcertissuetehsil_f" ErrorMessage="Please select District of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtcatcertissuetehsil_f" runat="server" CssClass="gridfont" Width="300px" Visible="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcatcertissuetehsil_f" runat="server" Display="None"
                                ControlToValidate="txtcatcertissuetehsil_f" ErrorMessage="Please enter Tehsil/District of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    

                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="Label1" runat="server" Text="Upload Certificate"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="fufathercert" runat="server" /><asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false" 
                                    Target="_blank" /><asp:Label ID="lblviewcert" runat="server" Text="Uploaded Certificate" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="Label2" runat="server" Text="Remarks"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                             <asp:TextBox runat="server" ID="txtfremarks" MaxLength="200" TextMode="MultiLine"
                                    Width="95%" ToolTip="Maximum 200 characters allowed" Height="80px">
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revtxtfremarks" runat="server" ControlToValidate="txtfremarks"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td valign="top" align="left" colspan="2">
                <asp:Label ID="lblsubcat" runat="server" Text="Sub Category : " Font-Bold="True" CssClass="formlabel"></asp:Label>
           
                <asp:Label ID="lblsubcat1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
        <tr id="trph" runat="server" visible="false">
            <td colspan="2" align="left">
                <table align="center" width="100%">
                    <tr>
                        <td>
                            In case of PH(OH/OA/HH)
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblphcertno" runat="server" Text="i) Disability Certificate Number"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtphcertno" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtphcertno" runat="server" Display="None" ControlToValidate="txtphcertno"
                                ErrorMessage="Please enter Disability Certificate Number" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblphcertissuedate" runat="server" Text="ii) Date of Issuance of Disability Certificate(dd/mm/yyyy)"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtphcertissuedate" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtphcertissuedate"> </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtphcertissuedate" runat="server" Display="None"
                                ControlToValidate="txtphcertissuedate" ErrorMessage="Please enter Date of Issuance of Disability Certificate"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtphcertissuedate" runat="server" ControlToValidate="txtphcertissuedate"
                                Display="None" ErrorMessage="Date of issuance of Disability Certificate should be in DD/MM/YYYY "
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblphissuedesig" runat="server" Text="iii) Designation of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtphissuedesig" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtphissuedesig" runat="server" Display="None"
                                ControlToValidate="txtphissuedesig" ErrorMessage="Please enter Designation of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblphissuemedinst" runat="server" Text="iv) Hospital/Medical Institution of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtphissuemedinst" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtphissuemedinst" runat="server" Display="None"
                                ControlToValidate="txtphissuemedinst" ErrorMessage="Please enter Hospital/Medical Institution of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblphcertissuetehsil" runat="server" Text="v) Tehsil/District of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtphcertissuetehsil" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtphcertissuetehsil" runat="server" Display="None"
                                ControlToValidate="txtphcertissuetehsil" ErrorMessage="Please enter Tehsil/District of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblphcertissuestate" runat="server" Text="vi) State of Issuing Authority"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtphcertissuestate" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtphcertissuestate" runat="server" Display="None"
                                ControlToValidate="txtphcertissuestate" ErrorMessage="Please enter State of Issuing Authority"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trexsm" runat="server" visible="false">
            <td colspan="2" align="left">
                <table align="center" width="100%">
                    <tr>
                        <td>
                            In case of EX-Service Men
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbldefservjoindate" runat="server" Text="i) Date of joining of Defence Service"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdefservjoindate" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                             <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtdefservjoindate"> </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtdefservjoindate" runat="server" Display="None"
                                ControlToValidate="txtdefservjoindate" ErrorMessage="Please enter Date of joining of Defence Service"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtdefservjoindate" runat="server" ControlToValidate="txtdefservjoindate"
                                Display="None" ErrorMessage="Date of joining of Defence Service should be in DD/MM/YYYY "
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbldefservdischargedate" runat="server" Text="ii) Date of Discharge/retirement from Defence Services"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdefservdischargedate" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtdefservdischargedate"> </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtdefservdischargedate" runat="server" Display="None"
                                ControlToValidate="txtdefservdischargedate" ErrorMessage="Please enter Date of Discharge/retirement from Defence Services"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtdefservdischargedate" runat="server" ControlToValidate="txtdefservdischargedate"
                                Display="None" ErrorMessage="Date of Discharge/retirement from Defence Services should be in DD/MM/YYYY "
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbllenofdefserv" runat="server" Text="iii) Total length of service rendered in Defence"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtlenofdefserv" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtlenofdefserv" runat="server" Display="None"
                                ControlToValidate="txtlenofdefserv" ErrorMessage="Please enter Total length of service rendered in Defence"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbldischargereason" runat="server" Text="iv) Reason for discharge"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdischargereason" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtdischargereason" runat="server" Display="None"
                                ControlToValidate="txtdischargereason" ErrorMessage="Please enter  Reason for discharge"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbldischargerank" runat="server" Text="v) Rank held at the time of discharge"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdischargerank" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtdischargerank" runat="server" Display="None"
                                ControlToValidate="txtdischargerank" ErrorMessage="Please enter Rank held at the time of discharge"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbldischargeunitname" runat="server" Text="vi) Name of Unit/Office at the time of discharge"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdischargeunitname" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtdischargeunitname" runat="server" Display="None"
                                ControlToValidate="txtdischargeunitname" ErrorMessage="Please enter Name of Unit/Office at the time of discharge"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbldischargeunitadd" runat="server" Text="vii) Address of the Unit/Office at the time of discharge"
                                CssClass="formlabel"></asp:Label><span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdischargeunitadd" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtdischargeunitadd" runat="server" Display="None"
                                ControlToValidate="txtdischargeunitadd" ErrorMessage="Please enter Address of Unit/Office at the time of discharge"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
     <%--   <tr id="trdeptcand" runat="server" visible="false">
            <td colspan="2" align="left">
                <table align="center" width="100%">
                    <tr>
                        <td>
                            In case of Departmental Candidate
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcurrentoffname" runat="server" Text="i) Name of current Government office/Organization where employed "
                                CssClass="formlabel"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcurrentoffname" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcurrentoffname" runat="server" Display="None"
                                ControlToValidate="txtcurrentoffname" ErrorMessage="Name of current Government office/Organization where employed"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcurrentoffadd" runat="server" Text="ii) Address of the current Govt. office/Organization where employed"
                                CssClass="formlabel"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcurrentoffadd" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcurrentoffadd" runat="server" Display="None"
                                ControlToValidate="txtcurrentoffadd" ErrorMessage="Address of current Government office/Organization where employed"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcentralorstate" runat="server" Text="iii) Whether in Central Govt. or State Govt."
                                CssClass="formlabel"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rbtcentralorstate" runat="server" RepeatDirection="Horizontal"
                                CssClass="formlabel">
                                <asp:ListItem Value="central">Central</asp:ListItem>
                                <asp:ListItem Value="state">State</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvrbtcentralorstate" runat="server" Display="None"
                                ControlToValidate="rbtcentralorstate" ErrorMessage="Select Whether in Central Govt. or State Govt."
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblstategovtname" runat="server" Text="iv) If State,Name of the State"
                                CssClass="formlabel"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtstategovtname" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtstategovtname" runat="server" Display="None"
                                ControlToValidate="txtstategovtname" ErrorMessage="Please enter Name of the State"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblministryname" runat="server" Text="v) If Central,Name of the Ministry"
                                CssClass="formlabel"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtministryname" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtministryname" runat="server" Display="None"
                                ControlToValidate="txtministryname" ErrorMessage="Please enter Name of the Ministry"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblWheAutonomous" runat="server" Text="vi) Whether the Office/Organization is Autonomous Body"
                                CssClass="formlabel"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rbtWheAutonomous" runat="server" RepeatDirection="Horizontal"
                                CssClass="formlabel">
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvrbtWheAutonomous" runat="server" Display="None"
                                ControlToValidate="rbtWheAutonomous" ErrorMessage="Select Whether the Office/Organization is Autonomous Body"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblappointmentdate" runat="server" Text="vii) Date of substantive appointment on regular Basis (Attach copy of appoinment order)"
                                CssClass="formlabel"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtappointmentdate" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtappointmentdate" runat="server" Display="None"
                                ControlToValidate="txtappointmentdate" ErrorMessage="Please enter Date of substantive appointment on regular Basis (Attach copy of appoinment order)"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtappointmentdate" runat="server" ControlToValidate="txtappointmentdate"
                                Display="None" ErrorMessage="Date of substantive appointment on regular Basis (Attach copy of appoinment order) should be in DD/MM/YYYY "
                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lblcurrentdesig" runat="server" Text="viii) Designation of the current post"
                                CssClass="formlabel"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcurrentdesig" runat="server" CssClass="gridfont" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtcurrentdesig" runat="server" Display="None"
                                ControlToValidate="txtcurrentdesig" ErrorMessage="Please enter Designation of the current post"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   
                   
                </table>
            </td>
        </tr>--%>
        <tr id="trsubcatdoc1" runat="server" visible="false">
            <td colspan="2">
                <asp:Label runat="server" ID="lblsubcategorydoc" CssClass="formheading" Text="List of Documents related to Sub Category"></asp:Label>
            </td>
        </tr>
        <tr id="trsubcatdoc2" runat="server" visible="false">
            <td colspan="3" align="left">
                <span style="color: red">
                    <asp:Label ID="Label22" runat="server" Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>
                </span>
            </td>
        </tr>
        <tr id="trsubcatdoc" runat="server" visible="false">
            <td align="center" colspan="3">
                <asp:GridView ID="grdsubcat" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,certificateReq,subcategory,remarks"
                    Width="100%" OnRowCommand="grdsubcat_RowCommand" OnRowEditing="grdsubcat_RowEditing"
                    OnRowDataBound="grdsubcat_RowDataBound" OnRowUpdating="grdsubcat_RowUpdating"
                    OnRowCancelingEdit="grdsubcat_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Required">
                            <ItemTemplate>
                                <%# Eval("subcatname")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Type" Visible="false">
                            <ItemTemplate>
                                <%# Eval("ctypename")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false"
                                    Target="_blank" />
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label>
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine"
                                    Width="95%" Height="80px" ToolTip="Maximum 200 characters allowed" Visible="false">
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit"
                                    CommandArgument='<%# Container.DataItemIndex %>' Text="Save"></asp:LinkButton>
                                <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="false"></asp:LinkButton>
                                <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnsave" runat="server" Text="Save & Next>>" Width="131px" CssClass="cssbutton"
                    OnClick="btnsave_Click" ValidationGroup="1" />
                      <asp:Button ID="btnnext" runat="server" Text="Next>>" Width="100px" CssClass="cssbutton"
                    Visible="false" onclick="btnnext_Click" />
                <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="1" />
                <asp:HiddenField ID="hfjid" runat="server" />
                <asp:HiddenField ID="hfedid" runat="server" />
                 <asp:HiddenField ID="hfidcat" runat="server" />
                  <asp:HiddenField ID="hfidph" runat="server" />
                   <asp:HiddenField ID="hfidex" runat="server" />
                    <asp:HiddenField ID="hfiddc" runat="server" />
                    <asp:HiddenField ID="hfsubcatcode" runat="server" />
                    <asp:HiddenField ID="hffcdid" runat="server" />
                    <asp:HiddenField ID="hfobcfdocid" runat="server" />
                     <asp:HiddenField ID="hfschedule" runat="server" />
                    <asp:HiddenField ID="hffinal" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
