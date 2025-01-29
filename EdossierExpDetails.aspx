<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EdossierExpDetails.aspx.cs" Inherits="EdossierExpDetails" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
<script type="text/javascript" language="javascript" src="Jscript/JScript.js">
</script>
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" >
        </cc1:ToolkitScriptManager>
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
            <td id="trexp1" runat="server" visible="false">
                <asp:Label ID="lblexpdoc" runat="server" CssClass="formheading" Text="Experience Details"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="trexp2" runat="server" visible="false" align="left">
                <span style="color: red">
                    <asp:Label ID="Label23" runat="server" Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>
                </span>
            </td>
        </tr>
        <tr id="trexp" runat="server" visible="false">
            <td align="center">
                <asp:GridView ID="grdexp" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,remarks,editflag,docid,Wcentralorstate,isAutonomous,id"
                    Width="100%" OnRowCommand="grdexp_RowCommand" OnRowDataBound="grdexp_RowDataBound"
                    OnRowUpdating="grdexp_RowUpdating" OnRowCancelingEdit="grdexp_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name of current Government office/Organization where employed">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblcurrentoffname" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"currentorgname") %>'></asp:Label>
                                <asp:TextBox ID="txtcurrentoffname" runat="server" CssClass="gridfont" Visible="false"
                                    Text='<%# DataBinder.Eval(Container.DataItem,"currentorgname") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtcurrentoffname" runat="server" Display="Dynamic"
                                    ControlToValidate="txtcurrentoffname" ErrorMessage="Name of current Government office/Organization where employed"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtcurrentoffname" runat="server" ControlToValidate="txtcurrentoffname"
                                    Display="Dynamic" ErrorMessage="Invalid character in current Government office Name"
                                    ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtcurrentoffname" runat="server" CssClass="gridfont" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtcurrentoffname" runat="server" Display="None"
                                    ControlToValidate="txtcurrentoffname" ErrorMessage="Name of current Government office/Organization where employed"
                                    ValidationGroup="2"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtcurrentoffname" runat="server" ControlToValidate="txtcurrentoffname"
                                    Display="None" ErrorMessage="Invalid character in current Government office Name"
                                    ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="2"></asp:RegularExpressionValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address of the current Govt. office/Organization where employed">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblcurrentoffadd" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"orgaddress") %>'></asp:Label>
                                <asp:TextBox ID="txtcurrentoffadd" runat="server" CssClass="gridfont" Visible="false"
                                    Text='<%# DataBinder.Eval(Container.DataItem,"orgaddress") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtcurrentoffadd" runat="server" Display="Dynamic"
                                    ControlToValidate="txtcurrentoffadd" ErrorMessage="Address of current Government office/Organization where employed"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtcurrentoffadd" runat="server" ControlToValidate="txtcurrentoffadd"
                                    Display="Dynamic" ErrorMessage="Invalid character in current Government office Address"
                                    ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtcurrentoffadd" runat="server" CssClass="gridfont" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtcurrentoffadd" runat="server" Display="None"
                                    ControlToValidate="txtcurrentoffadd" ErrorMessage="Address of current Government office/Organization where employed"
                                    ValidationGroup="2"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtcurrentoffadd" runat="server" ControlToValidate="txtcurrentoffadd"
                                    Display="None" ErrorMessage="Invalid character in current Government office Address"
                                    ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="2"></asp:RegularExpressionValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether in Central Govt. or State Govt.">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblcentralorstate" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"Wcentralorstate") %>'></asp:Label>
                                </br>
                                 <asp:Label ID="lblstat" runat="server" Text="State Name-" Visible="false"></asp:Label>
                                <asp:Label ID="lblorgstatename" runat="server" Text='<%# Eval("orgstatename")%>'
                                    Visible="false"></asp:Label><br />
                                    <asp:Label ID="lblmin" runat="server" Text="Ministry Name-" Visible="false"></asp:Label>
                                <asp:Label ID="lblorgministryname" runat="server" Text='<%# Eval("orgministryname")%>'
                                    Visible="false"></asp:Label>
                                <asp:RadioButtonList ID="rbtcentralorstate" runat="server" RepeatDirection="Horizontal"
                                    CssClass="formlabel" Visible="false" OnSelectedIndexChanged="rbtcentralorstate_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="central">Central</asp:ListItem>
                                    <asp:ListItem Value="state">State</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbtcentralorstate" runat="server" Display="Dynamic"
                                    ControlToValidate="rbtcentralorstate" ErrorMessage="Select Whether in Central Govt. or State Govt."
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                </br><asp:Label ID="lblstate" runat="server" Text="State Name" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtstategovtname" runat="server" CssClass="gridfont" Visible="false" Text='<%# Eval("orgstatename")%>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtstategovtname" runat="server" ControlToValidate="txtstategovtname"
                                    Display="Dynamic" ErrorMessage="Please enter Name of the State" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblministry" runat="server" Text="Ministry Name" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtministryname" runat="server" CssClass="gridfont" Visible="false" Text='<%# Eval("orgministryname")%>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtministryname" runat="server" ControlToValidate="txtministryname"
                                    Display="Dynamic" ErrorMessage="Please enter Name of the Ministry" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:RadioButtonList ID="rbtcentralorstate" runat="server" RepeatDirection="Horizontal"
                                    CssClass="formlabel" Visible="false" OnSelectedIndexChanged="rbtcentralorstate_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="central">Central</asp:ListItem>
                                    <asp:ListItem Value="state">State</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbtcentralorstate" runat="server" Display="None"
                                    ControlToValidate="rbtcentralorstate" ErrorMessage="Select Whether in Central Govt. or State Govt."
                                    ValidationGroup="2"></asp:RequiredFieldValidator>
                                </br><asp:Label ID="lblstate" runat="server" Text="State Name" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtstategovtname" runat="server" CssClass="gridfont" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtstategovtname" runat="server" ControlToValidate="txtstategovtname"
                                    Display="None" ErrorMessage="Please enter Name of the State" ValidationGroup="2"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblministry" runat="server" Text="Ministry Name" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtministryname" runat="server" CssClass="gridfont" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtministryname" runat="server" ControlToValidate="txtministryname"
                                    Display="None" ErrorMessage="Please enter Name of the Ministry" ValidationGroup="2"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether the Office/Organization is Autonomous Body">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblWheAutonomous" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"isAutonomous") %>'></asp:Label>
                                <asp:RadioButtonList ID="rbtWheAutonomous" runat="server" RepeatDirection="Horizontal"
                                    CssClass="formlabel" Visible="false">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbtWheAutonomous" runat="server" Display="Dynamic"
                                    ControlToValidate="rbtWheAutonomous" ErrorMessage="Select Whether the Office/Organization is Autonomous Body"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:RadioButtonList ID="rbtWheAutonomous" runat="server" RepeatDirection="Horizontal"
                                    CssClass="formlabel" Visible="false">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbtWheAutonomous" runat="server" Display="None"
                                    ControlToValidate="rbtWheAutonomous" ErrorMessage="Select Whether the Office/Organization is Autonomous Body"
                                    ValidationGroup="2"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of substantive appointment on regular Basis (Attach copy of appoinment order)">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblappointmentdate" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"dateofappoint") %>'></asp:Label>
                                <asp:TextBox ID="txtappointmentdate" runat="server" CssClass="gridfont" Visible="false"
                                    Text='<%# DataBinder.Eval(Container.DataItem,"dateofappoint") %>' ></asp:TextBox>
                                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtappointmentdate"> </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtappointmentdate" runat="server" Display="Dynamic"
                                    ControlToValidate="txtappointmentdate" ErrorMessage="Please enter Date of substantive appointment on regular Basis (Attach copy of appoinment order)"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtappointmentdate" runat="server" ControlToValidate="txtappointmentdate"
                                    Display="Dynamic" ErrorMessage="Date of substantive appointment on regular Basis (Attach copy of appoinment order) should be in DD/MM/YYYY "
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtappointmentdate" runat="server" CssClass="gridfont" Visible="false" ></asp:TextBox>
                                 <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtappointmentdate"> </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtappointmentdate" runat="server" Display="None"
                                    ControlToValidate="txtappointmentdate" ErrorMessage="Please enter Date of substantive appointment on regular Basis (Attach copy of appoinment order)"
                                    ValidationGroup="2"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtappointmentdate" runat="server" ControlToValidate="txtappointmentdate"
                                    Display="None" ErrorMessage="Date of substantive appointment on regular Basis (Attach copy of appoinment order) should be in DD/MM/YYYY "
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                    ValidationGroup="2"></asp:RegularExpressionValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation of the current post">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblcurrentdesig" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"currentdesig") %>'></asp:Label>
                                <asp:TextBox ID="txtcurrentdesig" runat="server" CssClass="gridfont" Visible="false"
                                    Text='<%# DataBinder.Eval(Container.DataItem,"currentdesig") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtcurrentdesig" runat="server" Display="Dynamic"
                                    ControlToValidate="txtcurrentdesig" ErrorMessage="Please enter Designation of the current post"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtcurrentdesig" runat="server" CssClass="gridfont" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtcurrentdesig" runat="server" Display="None"
                                    ControlToValidate="txtcurrentdesig" ErrorMessage="Please enter Designation of the current post"
                                    ValidationGroup="2"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
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
                                    Display="None" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="2"></asp:RegularExpressionValidator>
                            </FooterTemplate>
                            <HeaderStyle Width="400px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <%--  <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="Save"></asp:LinkButton>--%>
                                <asp:LinkButton ID="lbupdate" runat="server" CommandName="Update"
                                    Text="Update" Visible="false" ValidationGroup="1" CausesValidation="False"></asp:LinkButton>
                                <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                <%--<asp:LinkButton ID="lbremove" runat="server" CausesValidation="False" CommandName="Delete"
                                    Visible="false" Text="Remove" OnClientClick="return confirm('Are you sure to delete this record?');"></asp:LinkButton>--%>
                                <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                      <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="1" ShowMessageBox="true"
                                    ShowSummary="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkadd" runat="server" CommandName="Add" Text="Add More" ValidationGroup="2"></asp:LinkButton>
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
                                    Name of current Government office/Organization where employed
                                </td>
                                <td>
                                    Address of the current Govt. office/Organization where employed
                                </td>
                                <td>
                                    Whether in Central Govt. or State Govt.
                                </td>
                                <td>
                                    Whether the Office/Organization is Autonomous Body
                                </td>
                                <td>
                                    Date of substantive appointment on regular Basis (Attach copy of appoinment order)
                                </td>
                                <td>
                                    Designation of the current post
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Remarks
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:TextBox ID="txtcurrentoffname" runat="server" CssClass="gridfont"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtcurrentoffname" runat="server" Display="None"
                                        ControlToValidate="txtcurrentoffname" ErrorMessage="Name of current Government office/Organization where employed"
                                        ValidationGroup="3"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtcurrentoffname" runat="server" ControlToValidate="txtcurrentoffname"
                                        Display="None" ErrorMessage="Invalid character in current Government office Name"
                                        ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="3"></asp:RegularExpressionValidator>
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtcurrentoffadd" runat="server" CssClass="gridfont"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtcurrentoffadd" runat="server" Display="None"
                                        ControlToValidate="txtcurrentoffadd" ErrorMessage="Address of current Government office/Organization where employed"
                                        ValidationGroup="3"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtcurrentoffadd" runat="server" ControlToValidate="txtcurrentoffadd"
                                        Display="None" ErrorMessage="Invalid character in current Government office Address"
                                        ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="3"></asp:RegularExpressionValidator>
                                </td>
                                <td valign="top">
                                    <asp:RadioButtonList ID="rbtcentralorstate" runat="server" RepeatDirection="Horizontal"
                                        CssClass="formlabel" OnSelectedIndexChanged="rbtcentralorstate_SelectedIndexChanged1"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="central">Central</asp:ListItem>
                                        <asp:ListItem Value="state">State</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvrbtcentralorstate" runat="server" Display="None"
                                        ControlToValidate="rbtcentralorstate" ErrorMessage="Select Whether in Central Govt. or State Govt."
                                        ValidationGroup="3"></asp:RequiredFieldValidator>
                                    <br></br>
                                    <asp:Label ID="lblstate" runat="server" Text="State Name" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtstategovtname" runat="server" CssClass="gridfont" Visible="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtstategovtname" runat="server" ControlToValidate="txtstategovtname"
                                        Display="None" ErrorMessage="Please enter Name of the State" ValidationGroup="3"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:Label ID="lblministry" runat="server" Text="Ministry Name" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtministryname" runat="server" CssClass="gridfont" Visible="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtministryname" runat="server" ControlToValidate="txtministryname"
                                        Display="None" ErrorMessage="Please enter Name of the Ministry" ValidationGroup="3"></asp:RequiredFieldValidator>
                                    <br></br>
                                </td>
                                <td valign="top">
                                    <asp:RadioButtonList ID="rbtWheAutonomous" runat="server" RepeatDirection="Horizontal"
                                        CssClass="formlabel">
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvrbtWheAutonomous" runat="server" Display="None"
                                        ControlToValidate="rbtWheAutonomous" ErrorMessage="Select Whether the Office/Organization is Autonomous Body"
                                        ValidationGroup="3"></asp:RequiredFieldValidator>
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtappointmentdate" runat="server" CssClass="gridfont" ></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtappointmentdate"> </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtappointmentdate" runat="server" Display="None"
                                        ControlToValidate="txtappointmentdate" ErrorMessage="Please enter Date of substantive appointment on regular Basis (Attach copy of appoinment order)"
                                        ValidationGroup="3"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtappointmentdate" runat="server" ControlToValidate="txtappointmentdate"
                                        Display="None" ErrorMessage="Date of substantive appointment on regular Basis (Attach copy of appoinment order) should be in DD/MM/YYYY "
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                        ValidationGroup="3"></asp:RegularExpressionValidator>
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtcurrentdesig" runat="server" CssClass="gridfont"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtcurrentdesig" runat="server" Display="None"
                                        ControlToValidate="txtcurrentdesig" ErrorMessage="Please enter Designation of the current post"
                                        ValidationGroup="3"></asp:RequiredFieldValidator>
                                </td>
                                <td valign="top">
                                    <asp:FileUpload ID="fileupload" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvfile" runat="server" Display="None" ControlToValidate="fileupload"
                                        ErrorMessage="Please select file" ValidationGroup="3"></asp:RequiredFieldValidator>
                                </td>
                                <td valign="top">
                                    <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine"
                                        Width="95%" Height="80px" ToolTip="Maximum 200 characters allowed" Visible="true">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                        Display="None" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                        ValidationGroup="3"></asp:RegularExpressionValidator>
                                </td>
                                <td valign="top">
                                    <asp:LinkButton ID="lnkIn" runat="server" CommandName="EInsert" Text="Save" CausesValidation="true"
                                        ValidationGroup="3"></asp:LinkButton>
                                    <asp:ValidationSummary ID="vs3" runat="server" ValidationGroup="3" ShowMessageBox="true"
                                        ShowSummary="false" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
                <asp:HiddenField ID="hfedmid" runat="server" Visible="false" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnnext" runat="server" Text="Next>>" Width="100px" CssClass="cssbutton"
                    OnClick="btnnext_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
