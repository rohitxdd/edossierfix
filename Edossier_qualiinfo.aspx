<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Edossier_qualiinfo.aspx.cs" Inherits="Edossier_qualiinfo" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <%--<script type="text/javascript" language="javascript" src="Jscript/JScript.js">
</script>
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" >
        </cc1:ToolkitScriptManager>--%>
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
            <td align="center" class="tr">
                <strong>Details of Educational Qualification </strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvquali" runat="server" DataKeyNames="id,qid,State,YEAR,standard,month,govtorpvt,edqid,otherdegreename,edmid,docproofpvtinst,deptreqid"
                    AutoGenerateColumns="False" Font-Names="Arial" Width="100%" EnableTheming="False"
                    CssClass="gridfont" OnRowCommand="gvquali_RowCommand" OnRowEditing="gvquali_RowEditing"
                    OnRowDataBound="gvquali_RowDataBound" OnRowUpdating="gvquali_RowUpdating" OnRowCancelingEdit="gvquali_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl No:">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qualification">
                            <ItemTemplate>
                                <asp:Label ID="lblstand" runat="server" Text='<%# Bind("stnd") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Discipline">
                            <ItemTemplate>
                                <asp:Label ID="lblquali" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Percentage(upto 2 decimals)">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("percentage") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Board/ University">
                            <ItemTemplate>
                                <asp:Label ID="lblboard" runat="server" Text='<%# Bind("board") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Passing Month/Year">
                            <ItemTemplate>
                                <asp:Label ID="lblyear" runat="server" Text='<%# Bind("myear") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qualification Name">
                            <ItemTemplate>
                                <asp:Label ID="lblqualiname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"edqname") %>'></asp:Label>
                                <asp:DropDownList ID="ddldegreename" runat="server" CssClass="gridfont" OnSelectedIndexChanged="ddldegreename_SelectedIndexChanged"
                                    AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvddldegreename" runat="server" Display="Dynamic"
                                    ControlToValidate="ddldegreename" ErrorMessage="Please select Name of the Degree/Course"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtotherdegree" runat="server" Text='<%# Bind("otherdegreename") %>'
                                    Visible="false" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtotherdegree" runat="server" Display="Dynamic"
                                    ControlToValidate="txtotherdegree" ErrorMessage="Please enter other Degree Name"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of Declaration of Final Result">
                            <ItemTemplate>
                                <asp:Label ID="lblfinalresultdate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"finalresultdate") %>'></asp:Label>
                                <asp:TextBox ID="txtfresultdt" runat="server" Text='<%# Bind("finalresultdate") %>'
                                    Visible="false"></asp:TextBox>
                                <%--<cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtfresultdt"> </cc1:CalendarExtender>--%>
                                <asp:RequiredFieldValidator ID="rfvtxtfresultdt" runat="server" Display="Dynamic"
                                    ControlToValidate="txtfresultdt" ErrorMessage="Please enter Date of Declaration of Final Result"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtfresultdt" runat="server" ControlToValidate="txtfresultdt"
                                    Display="Dynamic" ErrorMessage="Date of Declaration of Final Result should be in DD/MM/YYYY "
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Institute Name">
                            <ItemTemplate>
                                <asp:Label ID="lblinstname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"instname") %>'></asp:Label>
                                <asp:TextBox ID="txtinstname" runat="server" Width="200px" MaxLength="200" TextMode="MultiLine"
                                    Text='<%# Bind("instname") %>' Height="80px" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtinstname" runat="server" Display="Dynamic"
                                    ControlToValidate="txtinstname" ErrorMessage="Please enter Institute" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether the Institute is Govt/Private">
                            <ItemTemplate>
                                <asp:Label ID="lblinstgovorpvt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"instgovorpvt") %>'></asp:Label>
                                <asp:RadioButtonList ID="rbtgovorpvt" runat="server" RepeatDirection="Horizontal"
                                    CssClass="gridfont" OnSelectedIndexChanged="rbtgovorpvt_SelectedIndexChanged"
                                    AutoPostBack="true" Visible="false">
                                    <asp:ListItem Value="G">Govt</asp:ListItem>
                                    <asp:ListItem Value="P">Private</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvrbtgovorpvt" runat="server" Display="Dynamic"
                                    ControlToValidate="rbtgovorpvt" ErrorMessage="Please select Whether the Institute is Govt/Private"
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                <br></br>
                                <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false"
                                    Target="_blank" />
                                <asp:Label ID="lblpvtdocproof" runat="server" Visible="false" Text="Documentary Proof of Pvt Institute(Document should be in PDF format only and Maximum size is 2MB)"></asp:Label>
                                <br></br>
                                <asp:FileUpload ID="fileupload" runat="server" Visible="false" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbsave" runat="server" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                    Text="Save" ValidationGroup="1" CausesValidation="False"></asp:LinkButton>
                                <asp:LinkButton ID="lbupdate" runat="server" CommandName="Update" Text="Update" Visible="false"
                                    ValidationGroup="1" CausesValidation="False"></asp:LinkButton>
                                <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="1" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                    <PagerStyle CssClass="gridpage" />
                    <EmptyDataTemplate>
                        <table class="gridfont" width="100%" border="1">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Qualification
                                </td>
                                <td>
                                    Discipline
                                </td>
                               
                                <td>
                                    Percentage(upto 2 decimals)
                                </td>
                                <td>
                                    Board/ University
                                </td>
                                <td>
                                    State
                                </td>
                                <td>
                                    Passing Month/Year
                                </td>
                                <td>
                                    Qualification Name
                                </td>
                                <td>
                                    Date of Declaration of Final Result
                                </td>
                                <td>
                                    Institute Name
                                </td>
                                <td>
                                    Whether the Institute is Govt/Private
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlstande" Visible="true" runat="server" OnSelectedIndexChanged="ddl_SelectedIndexFooterChangedE"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvstande" runat="server" Display="Dynamic" ControlToValidate="ddlstande"
                                        ValidationGroup="5" ErrorMessage="Please Enter Standard"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList_qe" Visible="true" runat="server" 
                                        AutoPostBack="true" Width="400px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvqulie" runat="server" Display="Dynamic" ControlToValidate="DropDownList_qe"
                                        ValidationGroup="5" ErrorMessage="Please Enter Qualification"></asp:RequiredFieldValidator>
                                </td>
                              <%--  <td id="tdquli_e" runat="server">
                                    <asp:TextBox ID="txt_qulie" Visible="false" runat="server"></asp:TextBox>
                                    <asp:Label ID="tble_quli" runat="server" Text="Enter Qualification:" Visible="false"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvquliother" runat="server" Display="none" ControlToValidate="txt_qulie"
                                        ValidationGroup="5" ErrorMessage="Please Enter Qualification"></asp:RequiredFieldValidator>
                                </td>--%>
                                <td>
                                    <asp:TextBox ID="txt_pere" Visible="true" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvper" runat="server" Display="Dynamic" ControlToValidate="txt_pere"
                                        ValidationGroup="5" ErrorMessage="Please Enter Percentage(upto 2 decimals)"></asp:RequiredFieldValidator>
                                    <%--             <asp:RegularExpressionValidator
                                    ID="revper" runat="server" Display="None" ControlToValidate="txt_pere" ValidationExpression="^[0-9.%]*$"
                                    ErrorMessage="Enter Valid Percentage" ValidationGroup="6"></asp:RegularExpressionValidator>--%>
                                    <asp:RegularExpressionValidator ID="revper" runat="server" Display="Dynamic" ControlToValidate="txt_pere"
                                        ValidationExpression="^(?=.*[1-9].*$)\d{0,2}(?:\.\d{0,2})?$" ErrorMessage="Enter Valid Percentage"
                                        ValidationGroup="5"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_ex_bodye" Visible="true" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvcontact" runat="server" Display="Dynamic" ControlToValidate="txt_ex_bodye"
                                        ValidationGroup="5" ErrorMessage="Please Enter board"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revboard" runat="server" Display="Dynamic" ControlToValidate="txt_ex_bodye"
                                        ValidationExpression="[a-zA-Z][a-zA-Z ]+" ErrorMessage="Invalid characters in Board/ University"
                                        ValidationGroup="5"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="revboardlf" runat="server" ControlToValidate="txt_ex_bodye"
                                        ValidationExpression=".{2,50}.*" ErrorMessage="Minimum 2 character and Maximum 50 character are allowed  in Board/ University"
                                        Display="Dynamic" ValidationGroup="5">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList_edu_statee" Visible="true" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvstatee" runat="server" Display="Dynamic" ControlToValidate="DropDownList_edu_statee"
                                        ValidationGroup="5" ErrorMessage="Please Enter State"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList_monthe" Visible="true" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DropDownList_yeare" Visible="true" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvmonth" runat="server" Display="Dynamic" ControlToValidate="DropDownList_monthe"
                                        ValidationGroup="5" ErrorMessage="Please Select month"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvyear" runat="server" Display="Dynamic" ControlToValidate="DropDownList_yeare"
                                        ValidationGroup="5" ErrorMessage="Please Select year"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddldegreename" runat="server" CssClass="gridfont" OnSelectedIndexChanged="ddldegreename_SelectedIndexChanged"
                                        AutoPostBack="true" Width="300px">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvddldegreename" runat="server" Display="Dynamic"
                                        ControlToValidate="ddldegreename" ErrorMessage="Please select Name of the Degree/Course"
                                        ValidationGroup="5"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtotherdegree" runat="server" Text='<%# Bind("otherdegreename") %>'
                                        Visible="false" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtotherdegree" runat="server" Display="Dynamic"
                                        ControlToValidate="txtotherdegree" ErrorMessage="Please enter other Degree Name"
                                        ValidationGroup="5"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtfresultdt" runat="server" Text='<%# Bind("finalresultdate") %>'></asp:TextBox>
                                    <%--<cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Animated="true"
                        TargetControlID="txtfresultdt"> </cc1:CalendarExtender>--%>
                                    <asp:RequiredFieldValidator ID="rfvtxtfresultdt" runat="server" Display="Dynamic"
                                        ControlToValidate="txtfresultdt" ErrorMessage="Please enter Date of Declaration of Final Result"
                                        ValidationGroup="5"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtfresultdt" runat="server" ControlToValidate="txtfresultdt"
                                        Display="Dynamic" ErrorMessage="Date of Declaration of Final Result should be in DD/MM/YYYY "
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                        ValidationGroup="5"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtinstname" runat="server" Width="200px" MaxLength="200" TextMode="MultiLine"
                                        Text='<%# Bind("instname") %>' Height="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtinstname" runat="server" Display="Dynamic"
                                        ControlToValidate="txtinstname" ErrorMessage="Please enter Institute" ValidationGroup="5"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtgovorpvt" runat="server" RepeatDirection="Horizontal"
                                        CssClass="gridfont" OnSelectedIndexChanged="rbtgovorpvt_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="G">Govt</asp:ListItem>
                                        <asp:ListItem Value="P">Private</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvrbtgovorpvt" runat="server" Display="Dynamic"
                                        ControlToValidate="rbtgovorpvt" ErrorMessage="Please select Whether the Institute is Govt/Private"
                                        ValidationGroup="5"></asp:RequiredFieldValidator>
                                    <br></br>
                                    <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false"
                                        Target="_blank" />
                                    <asp:Label ID="lblpvtdocproof" runat="server" Visible="false" Text="Documentary Proof of Pvt Institute(Document should be in PDF format only and Maximum size is 2MB)"></asp:Label>
                                    <br></br>
                                    <asp:FileUpload ID="fileupload" runat="server" Visible="false" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkIn" runat="server" CommandName="EInsert" Text="Insert" Visible="true"
                                        ValidationGroup="5"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkC" runat="server" CommandName="Cancel" Text="Cancel" Visible="true"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr id="trdocgrd1" runat="server" visible="false">
            <td>
                <asp:Label runat="server" ID="lbledudoc" CssClass="formheading" Text="List of Documents related to Educational Qualifications"></asp:Label>
            </td>
        </tr>
        <tr id="trdocgrd2" runat="server" visible="false">
            <td align="left">
                <span style="color: red">
                    <asp:Label ID="Label19" runat="server" Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>
                </span>
            </td>
        </tr>
        <tr id="trdocgrd" runat="server" visible="false">
            <td align="center">
                <asp:GridView ID="grddoc" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,certificateReq,remarks" Width="100%"
                    OnRowCommand="grddoc_RowCommand" OnRowEditing="grddoc_RowEditing" OnRowDataBound="grddoc_RowDataBound"
                    OnRowUpdating="grddoc_RowUpdating" OnRowCancelingEdit="grddoc_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Required">
                            <ItemTemplate>
                                <%# Eval("certificateReq")%>
                                <%--   <asp:Label runat="server" ID="lb1" Text="(Maximum size is 2MB)" ForeColor="Red"></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Type">
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
                        <asp:TemplateField HeaderText="Subjects" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblsubjects" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"subjects") %>'></asp:Label>
                                <asp:TextBox runat="server" ID="txtsubjects" ToolTip="Maximum 200 characters allowed"
                                    MaxLength="200" TextMode="MultiLine" Visible="false" Height="80px" Text='<%# DataBinder.Eval(Container.DataItem,"subjects") %>'>
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revtxtsubjects" runat="server" ControlToValidate="txtsubjects"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In Subjects" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Max Marks" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblmaxmarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"maxmarks") %>'></asp:Label>
                                <asp:TextBox runat="server" ID="txtmaxmarks" ToolTip="Maximum 200 characters allowed"
                                    MaxLength="200" TextMode="MultiLine" Visible="false" Height="80px" Text='<%# DataBinder.Eval(Container.DataItem,"maxmarks") %>'>
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revtxtmaxmarks" runat="server" ControlToValidate="txtmaxmarks"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In max marks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marks Obtained" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblmarksobtained" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"marksobtained") %>'></asp:Label>
                                <asp:TextBox runat="server" ID="txtmarksobtained" ToolTip="Maximum 200 characters allowed"
                                    MaxLength="200" TextMode="MultiLine" Visible="false" Height="80px" Text='<%# DataBinder.Eval(Container.DataItem,"marksobtained") %>'>
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revtxtmarksobtained" runat="server" ControlToValidate="txtmarksobtained"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In marks obtained" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label>
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" ToolTip="Maximum 200 characters allowed"
                                    MaxLength="200" TextMode="MultiLine" Visible="false" Height="80px">
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                    Display="Dynamic" ErrorMessage="Invalid Characters In Doc Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
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
                    OnClick="btnsave_Click" />
                <asp:Button ID="btnnext" runat="server" Text="Next>>" Width="100px" CssClass="cssbutton"
                    Visible="false" OnClick="btnnext_Click" />
                <asp:HiddenField ID="hffinal" runat="server" />
                <asp:HiddenField ID="hfedid" runat="server" />
                <asp:HiddenField ID="hfdocid" runat="server" />
                <asp:HiddenField ID="hfallowedit" runat="server" />
                <asp:HiddenField ID="hfreqid" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
