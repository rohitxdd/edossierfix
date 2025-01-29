<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="CombdFillQualiExp.aspx.cs" Inherits="CombdFillQualiExp" %>

<%@ Register Src="~/usercontrols/appnumber.ascx" TagName="appno" TagPrefix="no" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <script type="text/javascript" language="javascript" src="Jscript/JScript.js">
    </script>
    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete(item) {
            if (confirm("Are you sure to delete: " + item + "?") == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function update() {
            var result = confirm("Do you want to save finally? If Yes, Click OK else Click Cancel.")

            if (result) {
            }
            else {
                return false;
            }


        }

    </script>
     <script type="text/javascript">
         function confirmSelection() {
             if (confirm('are you sure to choose this Qualification Category,If Yes then previous filled Qualification will be Remove?')) {
                 document.getElementById('<%=rbtquali.ClientID %>').click();
                 return true;
             }
             return false;
         }
    </script>
    <style>
        .border-none{
            border : none;
        }
        .auto-style1 {
            height: 16px;
            font-weight: bold;
            font-size: 14px;
            color: White;
            background-color: #003366;
        }
       
        .border-none:focus{
            outline:0;
            border:none;
            box-shadow:none;
            -webkit-box-shadow:none;
        }
    </style>
    <table width="100%">
        <tr>
            <td style="height: 95px" colspan="2">
                <table id="tblconf" runat="server" width="100%">
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="Label14" runat="server" CssClass="formheading" Text="Entry Form for Qualification & Experience"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <no:appno ID="ddl_applid" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                              <%--<asp:Label ID="lblmsg" runat="server" Text="Nothing Pending" Font-Bold="True" ForeColor="#C00000"
                                Visible="False"></asp:Label>--%>
                                <asp:Label ID="lblmsg" runat="server" Text="" Font-Bold="True" ForeColor="#C00000"
                                Visible="False"></asp:Label>
                            <asp:Button ID="Button_Vaidate" runat="server" Text="Submit" Width="70px" OnClick="Button_Vaidate_Click"
                                CssClass="cssbutton" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
              <tr id="trhead" runat="server" >
                            <td align="center" class="auto-style1" style="margin-right:10px ; border:2px solid black">
                                <asp:Label ID="Label9" runat="server"  Text="Select Department To Fill Qualification"></asp:Label>                           
                            </td>
                            <td align="center" class="auto-style1" id="tr_dept" visible="false" style="border:2px solid black">
                                <asp:Label ID="Lbl_dept" runat="server"  Text="Departments for which Education Qualification already filled"></asp:Label>                           
                            </td>
                            
        </tr>
        <tr>
            <td align="center" style="border:2px solid black; border-radius : 5px" >
                <asp:DropDownList ID="ddl_combddpt" runat="server" OnSelectedIndexChanged="ddl_combddpt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
            <td align="center" id="trhead1" runat="server" visible="false" style="border:2px solid black; border-radius : 5px" >
                <asp:ListBox ID="ListBox_dept" runat="server" BackColor="#F0F0F0" EnableTheming="True" CssClass="border-none" Font-Size="Small"></asp:ListBox>
            </td>
        </tr>
        <%-- <tr id="tr_dept" runat="server" visible="false" >
                            <td align="center" class="tr">
                                <asp:Label ID="Lbl_dept" runat="server"  Text="Education filled for Departments"></asp:Label>                           
                            </td>
        </tr>--%>
       <%-- <tr id="trhead1" runat="server" visible="false">
            <td align="center">
                <asp:ListBox ID="ListBox_dept" runat="server"></asp:ListBox>
            </td>
        </tr>--%>
        <tr id="tr_note" runat="server" visible="false">
            <td align="center" colspan="2">
                <span style="color:red; font-size:0.85rem; font-weight:bold" >Note: To Edit Qualification Details, Select Department From Dropdown Above</span>
            </td>
        </tr>
         <tr id="trselectqcat" runat="server" visible="false" >
                            <td align="left" class="tr" colspan="2">
                                <asp:Label ID="Label7" runat="server"  Text="Select Qualification Category(Any One): As mentioned in the Advertisement"></asp:Label>
                              
                            </td>
                        </tr>
        <tr >
            <td colspan="2">
                <asp:RadioButtonList ID="rbtquali" runat="server" RepeatDirection="Vertical" AutoPostBack="true" Style="display:block; width:900px; word-wrap:break-word"
                    OnSelectedIndexChanged="rbtquali_SelectedIndexChanged" CssClass="formheading">
                </asp:RadioButtonList>
                   <%-- <asp:CheckBoxList ID="rbtquali" runat="server" AutoPostBack="True" CellPadding="5" CellSpacing="5" RepeatColumns="2" RepeatDirection="Vertical" RepeatLayout="Flow" TextAlign="Right" OnSelectedIndexChanged="rbtquali_SelectedIndexChanged"></asp:CheckBoxList>--%>
            
            </td>
        </tr>

        <tr>
            <td align="center" colspan="2">
                <asp:Panel ID="pnlquali" runat="server" Width="100%" Visible="false">
                    <table width="100%">
                        <tr>
                            <td align="left" style="height: 19px">
                                <asp:Label ID="lbl" runat="server" CssClass="formheading" Text="Application for the post of :"></asp:Label>
                                <asp:Label ID="lbl_app" runat="server" ForeColor="#C00000" CssClass="ariallightgrey"></asp:Label>
                                ::<asp:Label ID="Label3" runat="server" CssClass="formheading" Text="    Post Code:"></asp:Label>
                                <asp:Label ID="lbl_post_code" runat="server" ForeColor="#C00000" CssClass="ariallightgrey"></asp:Label>
                            </td>
                        </tr>
          <tr align="left"">
            <td colspan="4">
  <span style="color: Red"> * I have checked my eligibility for the post as per the detailed advertisement given by the Board on it website</span>
            </td>
        </tr>
                        <tr>
                            <td align="left" style="height: 19px">
                                <table width="100%">
                                    <tr>
                                        <td style="height: 21px; width: 969px;" valign="top">
                                            <asp:Label ID="lbleq" runat="server" CssClass="formheading" Text="Essential Qualification :"></asp:Label>
                                            <asp:Label ID="lbl_equli" runat="server" CssClass="formlabel"> </asp:Label>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tr" colspan="2">
                                            <asp:Label ID="Label12" runat="server" Text="Essential Educational Details (As per Eligibility of the Post)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2" colspan="2" style="width: 969px">
                                               <asp:GridView ID="gvquali" runat="server" DataKeyNames="id,qid,State,YEAR,standard,month,Stateid"
                                                AutoGenerateColumns="False" Font-Names="Arial" Width="100%" OnRowDataBound="gvquali_RowDataBound"
                                                AutoGenerateEditButton="True" OnRowCancelingEdit="gvquali_RowCancelingEdit" OnRowEditing="gvquali_RowEditing"
                                                OnRowUpdating="gvquali_RowUpdating" OnRowDeleting="gvquali_RowDeleting" AutoGenerateDeleteButton="True"
                                                EnableTheming="False" OnRowCommand="gvquali_RowCommand" ShowFooter="True" CssClass="gridfont" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No:">
                                                        <ItemTemplate>
                                                            <asp:Label ID="slno" runat="server" Text='<%#slno()%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <HeaderStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qualification">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlstand" runat="server" OnSelectedIndexChanged="ddl_SelectedIndexEditChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstand" runat="server" Text='<%# Bind("stnd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlstandf" Visible="false" runat="server" OnSelectedIndexChanged="ddl_SelectedIndexFooterChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvstand" runat="server" ControlToValidate="ddlstandf"
                                                                Display="None" ErrorMessage="Please Enter Standard" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discipline">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="DropDownList_q" Width="500px" runat="server" OnSelectedIndexChanged="ddlquli_SelectedIndexEditChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblquali" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="DropDownList_qf" Visible="false" runat="server" OnSelectedIndexChanged="ddlquli_SelectedIndexFooterChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvqualif" runat="server" ControlToValidate="DropDownList_qf"
                                                                Display="None" ErrorMessage="Please Enter Qualification" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtquli" runat="server">
                                                            </asp:TextBox>
                                                            Enter Qulification:
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%-- <asp:Label ID="lblquali_oth" runat="server" Text='<%# Bind("name") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtqulif" runat="server" Visible="false">
                                                            </asp:TextBox>
                                                            <asp:Label ID="tblqulife" runat="server" Text="Enter Qulification:" Visible="false"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="rfvqualif_oth" runat="server" ControlToValidate="txtqulif"
                                                                Display="None" ErrorMessage="Please Enter Qualification" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Percentage(upto 2 decimals)">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_per" MaxLength="5" runat="server" Text='<%# Bind("percentage") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("percentage") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_perf" Visible="false" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvperf" runat="server" ControlToValidate="txt_perf"
                                                                Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                            <%--                         <asp:RegularExpressionValidator
                                    ID="revpef" runat="server" Display="None" ControlToValidate="txt_perf" ValidationExpression="^[0-9.%]*$"
                                    ErrorMessage="Enter Valid Percentage" ValidationGroup="5"></asp:RegularExpressionValidator>  --%>
                                                            <asp:RegularExpressionValidator ID="revpef" runat="server" Display="None" ControlToValidate="txt_perf"
                                                                ValidationExpression="^(?=.*[1-9].*$)\d{0,2}(?:\.\d{0,2})?$" ErrorMessage="Enter Valid Percentage"
                                                                ValidationGroup="5"></asp:RegularExpressionValidator>
                                                            <%--ErrorMessage="Enter Valid Percentage" ValidationExpression="^[a-zA-Z0-9'+''+?'.]*$"--%>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="State">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="DropDownList_edu_state" runat="server" OnSelectedIndexChanged="DropDownList_edu_state_SelectedIndexChanged1" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="DropDownList_edu_statef" Visible="false" runat="server" OnSelectedIndexChanged="DropDownList_edu_statef_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvstate" runat="server" ControlToValidate="DropDownList_edu_statef"
                                                                Display="None" ErrorMessage="Please Enter State" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Board/ University">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlBoardUniv" runat="server" OnSelectedIndexChanged="ddlBoardUniv_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <br></br>
                                                            <asp:TextBox ID="txt_ex_body" runat="server" Text='<%# Bind("board") %>' MaxLength="200"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblboard" runat="server" Text='<%# Bind("board") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlBoardUnivf" Visible="false" runat="server" OnSelectedIndexChanged="ddlBoardUnivf_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBoardUniv" runat="server" ControlToValidate="ddlBoardUnivf"
                                                                Display="None" ErrorMessage="Please Enter State" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txt_ex_bodyf" Visible="false" runat="server" MaxLength="200"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvboard" runat="server" ControlToValidate="txt_ex_bodyf"
                                                                Display="None" ErrorMessage="Please Enter Board/ University" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                            <%--<asp:RegularExpressionValidator ID="revexam" runat="server" Display="None" ControlToValidate="txt_ex_bodyf"
                                                                ValidationExpression="[a-zA-Z][a-zA-Z ]+" ErrorMessage="Invalid characters in Board/ University"
                                                                ValidationGroup="5"></asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="revboardl" runat="server" ControlToValidate="txt_ex_bodyf"
                                                                ValidationExpression=".{2,50}.*" ErrorMessage="Minimum 2 character and Maximum 50 character are allowed in Board/ University"
                                                                Display="none" ValidationGroup="5">
                                                            </asp:RegularExpressionValidator>--%>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Passing Month/Year">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="DropDownList_month" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DropDownList_year" runat="server">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyear" runat="server" Text='<%# Bind("myear") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="DropDownList_monthf" Visible="false" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DropDownList_yearf" Visible="false" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvmonthf" runat="server" ControlToValidate="DropDownList_monthf"
                                                                Display="None" ErrorMessage="Please Select Passing Month" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvpyear" runat="server" ControlToValidate="DropDownList_yearf"
                                                                Display="None" ErrorMessage="Please Select Passing Year" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lnkadd" runat="server" CommandName="Add" Text="Add" ValidationGroup="5"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkIn" runat="server" CommandName="Insert" Text="Insert" Visible="false"
                                                                ValidationGroup="5"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkC" runat="server" CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridheading" />
                                                <PagerStyle CssClass="gridpage" />
                                                <EmptyDataTemplate>
                                                    <table class="gridfont" width="100%" border="1">
                                                        <tr>
                                                            <td></td>
                                                            <td>Qualification
                                                            </td>
                                                            <td>Discipline
                                                            </td>
                                                            <td id="tdquli" runat="server"></td>
                                                            <td>Percentage(upto 2 decimals)
                                                            </td>
                                                            <td>State
                                                            </td>
                                                            <td>Board/ University
                                                            </td>
                                                            <td>Passing Month/Year
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlstande" Visible="true" runat="server" OnSelectedIndexChanged="ddl_SelectedIndexFooterChangedE"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvstande" runat="server" Display="none" ControlToValidate="ddlstande"
                                                                    ValidationGroup="5" ErrorMessage="Please Enter Standard"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList_qe" Visible="true" runat="server" OnSelectedIndexChanged="ddlquli_SelectedIndexFooterChangedE"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvqulie" runat="server" Display="none" ControlToValidate="DropDownList_qe"
                                                                    ValidationGroup="5" ErrorMessage="Please Enter Qualification"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td id="tdquli_e" runat="server">
                                                                <asp:TextBox ID="txt_qulie" Visible="false" runat="server"></asp:TextBox>
                                                                <asp:Label ID="tble_quli" runat="server" Text="Enter Qualification:" Visible="false"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="rfvquliother" runat="server" Display="none" ControlToValidate="txt_qulie"
                                                                    ValidationGroup="5" ErrorMessage="Please Enter Qualification"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_pere" Visible="true" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvper" runat="server" Display="none" ControlToValidate="txt_pere"
                                                                    ValidationGroup="5" ErrorMessage="Please Enter Percentage(upto 2 decimals)"></asp:RequiredFieldValidator>
                                                                <%--             <asp:RegularExpressionValidator
                                    ID="revper" runat="server" Display="None" ControlToValidate="txt_pere" ValidationExpression="^[0-9.%]*$"
                                    ErrorMessage="Enter Valid Percentage" ValidationGroup="6"></asp:RegularExpressionValidator>--%>
                                                                <asp:RegularExpressionValidator ID="revper" runat="server" Display="None" ControlToValidate="txt_pere"
                                                                    ValidationExpression="^(?=.*[1-9].*$)\d{0,2}(?:\.\d{0,2})?$" ErrorMessage="Enter Valid Percentage"
                                                                    ValidationGroup="5"></asp:RegularExpressionValidator>
                                                                <%--ErrorMessage="Enter Valid Percentage" ValidationExpression="^[a-zA-Z0-9'+''+?'.]*$"--%> 
                                                                
                                                            </td>
                                                            <%--ValidationExpression="^(?=.*[1-9].*$)\d{0,2}(?:\.\d{0,2})?$"--%>

                                                            <td>
                                                                <asp:DropDownList ID="DropDownList_edu_statee" Visible="true" runat="server" OnSelectedIndexChanged="DropDownList_edu_statee_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvstatee" runat="server" Display="none" ControlToValidate="DropDownList_edu_statee"
                                                                    ValidationGroup="5" ErrorMessage="Please Enter State"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_edu_boradee" Visible="true" runat="server" OnSelectedIndexChanged="ddl_edu_boradee_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvboradee" runat="server" Display="none" ControlToValidate="ddl_edu_boradee"
                                                                    ValidationGroup="5" ErrorMessage="Please Enter State"></asp:RequiredFieldValidator>
                                                                <br></br>
                                                                <asp:TextBox ID="txt_ex_bodye" Visible="true" runat="server" MaxLength="200"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvcontact" runat="server" Display="none" ControlToValidate="txt_ex_bodye"
                                                                    ValidationGroup="5" ErrorMessage="Please Enter board"></asp:RequiredFieldValidator>
                                                               <%-- <asp:RegularExpressionValidator ID="revboard" runat="server" Display="None" ControlToValidate="txt_ex_bodye"
                                                                    ValidationExpression="[a-zA-Z][a-zA-Z ]+" ErrorMessage="Invalid characters in Board/ University"
                                                                    ValidationGroup="5"></asp:RegularExpressionValidator>--%>
                                                                <%--<asp:RegularExpressionValidator ID="revboardlf" runat="server" ControlToValidate="txt_ex_bodye"
                                                                    ValidationExpression=".{2,50}.*" ErrorMessage="Minimum 2 character and Maximum 50 character are allowed  in Board/ University"
                                                                    Display="none" ValidationGroup="5">
                                                                </asp:RegularExpressionValidator>--%>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList_monthe" Visible="true" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="DropDownList_yeare" Visible="true" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvmonth" runat="server" Display="none" ControlToValidate="DropDownList_monthe"
                                                                    ValidationGroup="5" ErrorMessage="Please Select month"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvyear" runat="server" Display="none" ControlToValidate="DropDownList_yeare"
                                                                    ValidationGroup="5" ErrorMessage="Please Select year"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <%--  <asp:TextBox ID="txtLac_Add" runat="server" TextMode="MultiLine" Width="260px"></asp:TextBox>--%>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkadd" runat="server" CommandName="EAdd" Text="Add" ValidationGroup="5"
                                                                    Visible="false"></asp:LinkButton>
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
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:CheckBoxList ID="CheckBoxList_special" runat="server" AutoPostBack="True" CssClass="formlabel"
                                    Width="60%" OnSelectedIndexChanged="CheckBoxList_special_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr id="tr_ex" runat="server" visible="false" class="formlabel">
                            <td align="left">
                                <table width="80%">
                                    <tr align="center">
                                        <td>
                                            Qualification
                                        </td>
                                        <td>
                                            Issuing Authority
                                        </td>
                                        <td>
                                            Certificate No.
                                        </td>
                                        <td>
                                            Date of Issue
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:Label runat="server" ID="lbl_ex_degree" Text="Degree"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txt_ex_auth" MaxLength="10"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txt_ex_cer_no" MaxLength="12"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_ex_issue_date" Width="50%" MaxLength="10" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                Animated="true" TargetControlID="txt_ex_issue_date">
                                            </cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtoex" runat="server" Display="none" ControlToValidate="txt_ex_issue_date"
                                                ValidationGroup="1" ErrorMessage="Please Enter ExService TO Date"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revtoex" runat="server" ControlToValidate="txt_ex_issue_date" Display="None"
                                                    ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                     
    				<tr>
                            <td align="left">
                                <asp:Label ID="lbl_msg" runat="server" ForeColor="Red" Font-Bold="true" Font-Italic="true"
                                    Text=" *  Candidate should ensure that he/she is eligible for the post as per qualification given above/in advt.Failing which his/her candidature will be summarily rejected at any stage."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnqualialt" runat="server" CssClass="cssbutton" OnClick="btnqualialt_Click"
                                    OnClientClick="return update();" Text="Do you want to Finally Save" Visible="False" />
                                <asp:Button ID="btnquali" runat="server" CssClass="cssbutton" OnClick="btnquali_Click"
                                    Text="Save Qualification" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 19px" colspan="2">
                                <table width="100% " id ="tbl_desire_qual" runat ="server">
                                     <tr id ="tr_desire_ed_lbl" runat="server">
                                        <td class="tr" colspan="2">
                                            <asp:Label ID="Label10" runat="server" Text="Desirable Educational Details (As per Eligibility of the Post)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td style="height: 19px;" valign="top" >
                                            <asp:Label ID="lbldq" runat="server" CssClass="formheading" Text="Desirable Qualification :"></asp:Label>
                                            <asp:Label ID="lbl_dquli" runat="server" CssClass="formlabel"></asp:Label>
                                            
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                    <tr id="desirable" runat="server"><td style="color:Navy; font-size:16px;">Do you Possess above desirable qualification</td><td style="color:Navy;"><asp:RadioButton id="yes" runat="server" Text="YES" GroupName="check" OnCheckedChanged="yes_CheckedChanged" AutoPostBack="true"/>&nbsp; <asp:RadioButton id="no" runat="server" Text="NO" GroupName="check" OnCheckedChanged="no_CheckedChanged" AutoPostBack="true"/></td></tr>
                                    <tr>
                                        <td colspan="2" rowspan="2">
                                                  <asp:GridView ID="gvquali_desire" runat="server" AutoGenerateColumns="False" AutoGenerateDeleteButton="True"
                                                AutoGenerateEditButton="True" CssClass="gridfont" DataKeyNames="id,qid,State,YEAR,standard,month"
                                                EnableTheming="False" Font-Names="Arial" OnRowCancelingEdit="gvquali_desire_RowCancelingEdit"
                                                OnRowCommand="gvquali_desire_RowCommand" OnRowDataBound="gvquali_desire_RowDataBound"
                                                OnRowDeleting="gvquali_desire_RowDeleting" OnRowEditing="gvquali_desire_RowEditing"
                                                OnRowUpdating="gvquali_desire_RowUpdating" ShowFooter="True" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No:">
                                                        <ItemTemplate>
                                                            <asp:Label ID="slno" runat="server" Text="<%#slno()%>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <HeaderStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qualification">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlstand" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexEditChanged">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstand" runat="server" Text='<%# Bind("stnd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlstandf" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexFooterChanged"
                                                                Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvstand" runat="server" ControlToValidate="ddlstandf"
                                                                Display="None" ErrorMessage="Please Enter Standard" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discipline">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="DropDownList_q" runat="server" Width="500px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlquli_desire_SelectedIndexEditChanged">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblquali" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="DropDownList_qf" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlquli_desire_SelectedIndexFooterChanged"
                                                                Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvqualif" runat="server" ControlToValidate="DropDownList_qf"
                                                                Display="None" ErrorMessage="Please Enter Qualification" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtquli" runat="server">
                                                            </asp:TextBox>
                                                            Enter Qulification:
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%-- <asp:Label ID="lblquali_oth" runat="server" Text='<%# Bind("name") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtqulif" runat="server" Visible="false">
                                                            </asp:TextBox>
                                                            <asp:Label ID="tblqulife" runat="server" Text="Enter Qulification:" Visible="false"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="rfvqualif_oth" runat="server" ControlToValidate="txtqulif"
                                                                Display="None" ErrorMessage="Please Enter Qualification" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Percentage(upto 2 decimals)">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_per" runat="server" MaxLength="5" Text='<%# Bind("percentage") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("percentage") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_perf" runat="server" Visible="false"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvperf" runat="server" ControlToValidate="txt_perf"
                                                                Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                            <%--                         <asp:RegularExpressionValidator
                                    ID="revpef" runat="server" Display="None" ControlToValidate="txt_perf" ValidationExpression="^[0-9.%]*$"
                                    ErrorMessage="Enter Valid Percentage" ValidationGroup="5"></asp:RegularExpressionValidator>  --%>
                                                            <asp:RegularExpressionValidator ID="revpef" runat="server" ControlToValidate="txt_perf"
                                                                Display="None" ErrorMessage="Enter Valid Percentage" ValidationExpression="^(?=.*[1-9].*$)\d{0,2}(?:\.\d{0,2})?$"
                                                                ValidationGroup="5"></asp:RegularExpressionValidator>
                                                            <%--Display="None" ErrorMessage="Enter Valid Percentage" ValidationExpression="^[a-zA-Z0-9'+''+?'.]*$"--%>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="State">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="DropDownList_edu_state" runat="server">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="DropDownList_edu_statef" runat="server" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvstate" runat="server" ControlToValidate="DropDownList_edu_statef"
                                                                Display="None" ErrorMessage="Please Enter State" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Board/ University">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_ex_body" runat="server" MaxLength="50" Text='<%# Bind("board") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblboard" runat="server" Text='<%# Bind("board") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_ex_bodyf" runat="server" MaxLength="50" Visible="false"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvboard" runat="server" ControlToValidate="txt_ex_bodyf"
                                                                Display="None" ErrorMessage="Please Enter Board/ University" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                           <%-- <asp:RegularExpressionValidator ID="revexam" runat="server" ControlToValidate="txt_ex_bodyf"
                                                                Display="None" ErrorMessage="Invalid characters in Board/ University" ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                                                                ValidationGroup="5"></asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="revboardl" runat="server" ControlToValidate="txt_ex_bodyf"
                                                                Display="none" ErrorMessage="Minimum 2 character and Maximum 50 character are allowed in Board/ University"
                                                                ValidationExpression=".{2,50}.*" ValidationGroup="5">
                                                            </asp:RegularExpressionValidator>--%>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Passing Month/Year">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="DropDownList_month" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DropDownList_year" runat="server">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyear" runat="server" Text='<%# Bind("myear") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridfont" />
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="DropDownList_monthf" runat="server" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DropDownList_yearf" runat="server" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvmonthf" runat="server" ControlToValidate="DropDownList_monthf"
                                                                Display="None" ErrorMessage="Please Select Passing Month" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvpyear" runat="server" ControlToValidate="DropDownList_yearf"
                                                                Display="None" ErrorMessage="Please Select Passing Year" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lnkadd" runat="server" CommandName="Add" Text="Add" ValidationGroup="5"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkIn" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="5"
                                                                Visible="false"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkC" runat="server" CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle CssClass="gridfont" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridheading" />
                                                <PagerStyle CssClass="gridpage" />
                                                <EmptyDataTemplate>
                                                    <table border="1" class="gridfont" width="100%">
                                                        <tr>
                                                            <td></td>
                                                            <td>Qualification
                                                            </td>
                                                            <td>Discipline
                                                            </td>
                                                            <td id="tdquli_desire" runat="server"></td>
                                                            <td>Percentage(upto 2 decimals)
                                                            </td>
                                                            <td>State
                                                            </td>
                                                            <td>Board/ University
                                                            </td>

                                                            <td>Passing Month/Year
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlstande" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_desire_SelectedIndexFooterChangedE"
                                                                    Visible="false">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvstande" runat="server" ControlToValidate="ddlstande"
                                                                    Display="none" ErrorMessage="Please Enter Standard" ValidationGroup="6"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList_qe" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlquli_desire_SelectedIndexFooterChangedE"
                                                                    Visible="false">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvqulie" runat="server" ControlToValidate="DropDownList_qe"
                                                                    Display="none" ErrorMessage="Please Enter Qualification" ValidationGroup="6"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td id="tdquli_e" runat="server">
                                                                <asp:TextBox ID="txt_qulie" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:Label ID="tble_quli" runat="server" Text="Enter Qualification:" Visible="false"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="rfvquliother" runat="server" ControlToValidate="txt_qulie"
                                                                    Display="none" ErrorMessage="Please Enter Qualification" ValidationGroup="6"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_pere" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvper" runat="server" ControlToValidate="txt_pere"
                                                                    Display="none" ErrorMessage="Please Enter Percentage(upto 2 decimals)" ValidationGroup="6"></asp:RequiredFieldValidator>
                                                                <%-- <asp:RegularExpressionValidator ID="revper" runat="server" Display="None" ControlToValidate="txt_pere" ValidationExpression="^[0-9.%]*$"
                                                                      ErrorMessage="Enter Valid Percentage" ValidationGroup="6"></asp:RegularExpressionValidator>--%>
                                                                <asp:RegularExpressionValidator ID="revper" runat="server" ControlToValidate="txt_pere"
                                                                    Display="None" ErrorMessage="Enter Valid Percentage" ValidationExpression="^(?=.*[1-9].*$)\d{0,2}(?:\.\d{0,2})?$"
                                                                    ValidationGroup="6"></asp:RegularExpressionValidator>
                                                                <%--Display="None" ErrorMessage="Enter Valid Percentage" ValidationExpression="^[a-zA-Z0-9'+''+?'.]*$"--%>
                                                               
                                                            </td>

                                                            <td>
                                                                <asp:DropDownList ID="DropDownList_edu_statee" runat="server" Visible="false" OnSelectedIndexChanged="DropDownList_edu_statee_SelectedIndexChanged1">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvstatee" runat="server" ControlToValidate="DropDownList_edu_statee"
                                                                    Display="none" ErrorMessage="Select Enter State" ValidationGroup="6"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddledu_Uniboardee" runat="server" Visible="false">
                                                                </asp:DropDownList>
                                                                <br></br>
                                                                <asp:TextBox ID="txt_ex_bodye" runat="server" MaxLength="50" Visible="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvcontact" runat="server" ControlToValidate="txt_ex_bodye"
                                                                    Display="none" ErrorMessage="Please Enter board" ValidationGroup="6"></asp:RequiredFieldValidator>
                                                               <%-- <asp:RegularExpressionValidator ID="revboard" runat="server" ControlToValidate="txt_ex_bodye"
                                                                    Display="None" ErrorMessage="Invalid characters in Board/ University" ValidationExpression="[a-zA-Z][a-zA-Z ]+"
                                                                    ValidationGroup="6"></asp:RegularExpressionValidator>
                                                                <asp:RegularExpressionValidator ID="revboardlf" runat="server" ControlToValidate="txt_ex_bodye"
                                                                    Display="none" ErrorMessage="Minimum 2 character and Maximum 50 character are allowed  in Board/ University"
                                                                    ValidationExpression=".{2,50}.*" ValidationGroup="6">
                                                                </asp:RegularExpressionValidator>--%>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DropDownList_monthe" runat="server" Visible="false">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="DropDownList_yeare" runat="server" Visible="false">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvmonth" runat="server" ControlToValidate="DropDownList_monthe"
                                                                    Display="none" ErrorMessage="Please Select month" ValidationGroup="6"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvyear" runat="server" ControlToValidate="DropDownList_yeare"
                                                                    Display="none" ErrorMessage="Please Select year" ValidationGroup="6"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <%--  <asp:TextBox ID="txtLac_Add" runat="server" TextMode="MultiLine" Width="260px"></asp:TextBox>--%>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkadd" runat="server" CommandName="EAdd" Text="Add" ValidationGroup="6"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkIn" runat="server" CommandName="EInsert" Text="Insert" ValidationGroup="6"
                                                                    Visible="false"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkC" runat="server" CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:CheckBoxList ID="CheckBoxList_desire" runat="server" AutoPostBack="True" CssClass="formlabel"
                                    Width="60%" OnSelectedIndexChanged="CheckBoxList_desire_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnqualialt_desire" runat="server" CssClass="cssbutton" OnClick="btnqualialt_desire_Click"
                                    OnClientClick="return update();" Text="Do you want to Finaly Save" Visible="False" />
                                <asp:Button ID="btnquali_desire" runat="server" CssClass="cssbutton" OnClick="btnquali_desire_Click"
                                    Text="Save Qualification" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                         <td align="left" style="height: 19px" colspan="2">
                        <table width="100% " id ="tbldesirable_exp" runat ="server">
                                     <tr id ="trdesirable_exp" runat="server">
                                        <td class="tr">
                                            <asp:Label ID="lbldesirable" runat="server" Text="Desirable Experience Details (As per Eligibility of the Post)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr  id ="trdesire_ep_1" runat ="server">
                            <td align="left" style="height: 19px">
                                <asp:Label ID="lbldexp" runat="server" CssClass="formheading" Text="Desirable Experience :"></asp:Label>
                                <asp:Label ID="lbl_dexp" runat="server" CssClass="formlabel"></asp:Label>
                            </td>
                                        <td style="width: 405px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                    <tr id="Exp_desirable" runat="server"><td style="color:Navy; font-size:16px;">Do you Possess above desirable Experience</td>
                                        <td style="color:Navy; width: 405px;"><asp:RadioButton id="yes1" runat="server" Text="YES" GroupName="check1" OnCheckedChanged="yes_CheckedChanged1" AutoPostBack="true"/>&nbsp; <asp:RadioButton id="no1" runat="server" Text="NO" GroupName="check1" OnCheckedChanged="no_CheckedChanged1" AutoPostBack="true"/></td>
                                        </tr>
                                   </table> 
                                   </td>
                                   </tr>
                       
                        <tr>

                            <td align="left" style="height: 14px">
                                <caption>
                                    &nbsp;</caption>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnExpalt_desire" runat="server" CssClass="cssbutton" OnClick="btnExpalt_desire_Click"
                                    OnClientClick="return update();" Text="Do you want to Finaly Save" Visible="False" />
                                <asp:Button ID="btnExp_desire" runat="server" CssClass="cssbutton" OnClick="btnExp_desire_Click"
                                    Text="Save Experience" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="PanExperience" runat="server" Width="100%" Visible="false">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblee" runat="server" CssClass="formheading" Text="Essential Experience :"></asp:Label>
                                <asp:Label ID="lbl_eexp" runat="server" CssClass="formlabel"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr id="tr_exp_l" runat="server">
                            <td align="left">
                                <asp:Label ID="lbl_exp" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="tr_exp_h" runat="server">
                            <td class="tr" colspan="4" align="left">
                                <asp:Label ID="Label8" runat="server" Text="Experience Details"></asp:Label>
                            </td>
                        </tr>
                        <tr id="tr_exp" runat="server">
                            <td align="center">
                                <asp:GridView ID="gvexp" runat="server" DataKeyNames="id" AutoGenerateColumns="False"
                                    Font-Names="Arial" Width="100%" OnRowDataBound="gvexp_RowDataBound" AutoGenerateEditButton="True"
                                    OnRowCancelingEdit="gvexp_RowCancelingEdit" OnRowEditing="gvexp_RowEditing" OnRowUpdating="gvexp_RowUpdating"
                                    OnRowDeleting="gvexp_RowDeleting" AutoGenerateDeleteButton="True" EnableTheming="False"
                                    OnRowCommand="gvexp_RowCommand" ShowFooter="True" CssClass="gridfont">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No:">
                                            <ItemTemplate>
                                                <asp:Label ID="slno" runat="server" Text='<%#slno()%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridfont" />
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name of Post">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtpost" runat="server" Text='<%# Bind("post") %>' MaxLength="50"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("post") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridfont" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtpostf" Visible="false" runat="server" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvpostf" runat="server" ControlToValidate="txtpostf"
                                                    Display="None" ErrorMessage="Please Enter Name of Post" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date From">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtdatef" runat="server" Text='<%# Bind("datefrom") %>'></asp:TextBox><asp:Image
                                                    ID="cal_imgfrom1" runat="server" alt="DatePicker" onclick="PopupPicker('txtdatef', 250, 250)"
                                                    src="Images/calendar.bmp" Height="16" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("datefrom") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridfont" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtdatetff" Visible="false" runat="server"></asp:TextBox><asp:Image
                                                    ID="cal_imgfromf" runat="server" Visible="false" alt="DatePicker" onclick="PopupPicker('txtdatetff', 250, 250)"
                                                    src="Images/calendar.bmp" Height="16" />
                                                <asp:RequiredFieldValidator ID="rfvdatef" runat="server" ControlToValidate="txtdatetff"
                                                    Display="None" ErrorMessage="Please Enter Date From" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revday1ee" runat="server" ControlToValidate="txtdatetff"
                                                    Display="None" ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                    ValidationGroup="2"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date To">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtdateto" runat="server" Text='<%# Bind("dateto") %>'></asp:TextBox><asp:Image
                                                    ID="cal_imgto" runat="server" alt="DatePicker" onclick="PopupPicker('txtdateto', 250, 250)"
                                                    src="Images/calendar.bmp" Height="16" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("dateto") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridfont" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtdatetof" Visible="false" runat="server"></asp:TextBox><asp:Image
                                                    ID="cal_imgtof" runat="server" Visible="false" alt="DatePicker" onclick="PopupPicker('txtdatetof', 250, 250)"
                                                    src="Images/calendar.bmp" Height="16" />
                                                <asp:RequiredFieldValidator ID="rfvdatetof" runat="server" ControlToValidate="txtdatetof"
                                                    Display="None" ErrorMessage="Please Enter Date To" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdayf1ee" runat="server" ControlToValidate="txtdatetof"
                                                    Display="None" ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                    ValidationGroup="2"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employer Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtempname" runat="server" Text='<%# Bind("emp_name") %>' MaxLength="50"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("emp_name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridfont" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtempnamef" Visible="false" runat="server" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvempnamef" runat="server" ControlToValidate="txtempnamef"
                                                    Display="None" ErrorMessage="Please Enter Employer Name" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employer Contact No">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtempcno" runat="server" MaxLength="11" Text='<%# Bind("emp_contactno") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("emp_contactno") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridfont" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtempcnof" Visible="false" runat="server" MaxLength="11"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvempcnof" runat="server" ControlToValidate="txtempcnof"
                                                    Display="None" ErrorMessage="Please Enter Employer Contact No" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revcontactef" runat="server" Display="None" ControlToValidate="txtempcnof"
                                                    ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Valid Contact No" ValidationGroup="2"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="revnum" runat="server" ControlToValidate="txtempcnof"
                                                    ValidationExpression=".{10,11}.*" ErrorMessage="Enter Only Valid Contact No" Display="none"
                                                    ValidationGroup="2">
                                                </asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employer Address">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtempadd" runat="server" Text='<%# Bind("emp_addr") %>' MaxLength="200"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("emp_addr") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridfont" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtempaddf" Visible="false" runat="server" MaxLength="200"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvempaddf" runat="server" ControlToValidate="txtempaddf"
                                                    Display="None" ErrorMessage="Please Enter Employer Address" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" CssClass="gridfont" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkadd" runat="server" CommandName="Add" Text="Add" ValidationGroup="2"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkIn" runat="server" CommandName="Insert" Text="Insert" Visible="false"
                                                    ValidationGroup="2"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkC" runat="server" CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="gridfont" />
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
                                                    Name of Post
                                                </td>
                                                <td>
                                                    Date From
                                                </td>
                                                <td>
                                                    Date To
                                                </td>
                                                <td>
                                                    Employer Name
                                                </td>
                                                <td>
                                                    Employer Contact No
                                                </td>
                                                <td>
                                                    Employer Address
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEPoste" runat="server" Visible="false" MaxLength="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvposte" runat="server" Display="none" ControlToValidate="txtEPoste"
                                                        ValidationGroup="3" ErrorMessage="Please Enter Post"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDayFrome" Visible="false" runat="server"></asp:TextBox><asp:Image
                                                        ID="cal_imgfrom" runat="server" Visible="false" alt="DatePicker" onclick="PopupPicker('txtDayFrome', 250, 250)"
                                                        src="Images/calendar.bmp" Height="16" />
                                                    <asp:RequiredFieldValidator ID="rfvdaye" runat="server" Display="none" ControlToValidate="txtDayFrome"
                                                        ValidationGroup="3" ErrorMessage="Please Enter Date Of Post From"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revday1e" runat="server" ControlToValidate="txtDayFrome"
                                                        Display="None" ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                        ValidationGroup="3"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDayToe" Visible="false" runat="server"></asp:TextBox><asp:Image
                                                        ID="cal_imgto" runat="server" Visible="false" alt="DatePicker" onclick="PopupPicker('txtDayToe', 250, 250)"
                                                        src="Images/calendar.bmp" Height="16" />
                                                    <asp:RequiredFieldValidator ID="rfvdayto" runat="server" Display="none" ControlToValidate="txtDayToe"
                                                        ValidationGroup="3" ErrorMessage="Please Enter Date Of Post To"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revdayto" runat="server" ControlToValidate="txtDayToe"
                                                        Display="None" ErrorMessage="From Date should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                        ValidationGroup="3"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpNamee" Visible="false" MaxLength="50" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvEmpName1" runat="server" Display="none" ControlToValidate="txtEmpNamee"
                                                        ValidationGroup="1" ErrorMessage="Please Enter Employee Name"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpContacte" Visible="false" runat="server" MaxLength="11"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvcontact" runat="server" Display="none" ControlToValidate="txtEmpContacte"
                                                        ValidationGroup="3" ErrorMessage="Please Enter Employee Contact"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revcontacte" runat="server" Display="None" ControlToValidate="txtEmpContacte"
                                                        ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Valid Contact No" ValidationGroup="3"></asp:RegularExpressionValidator>
                                                    <asp:RegularExpressionValidator ID="revnum" runat="server" ControlToValidate="txtEmpContacte"
                                                        ValidationExpression=".{10,11}.*" ErrorMessage="Enter Only Valid Contact No" Display="none"
                                                        ValidationGroup="3">
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpAddre" Visible="false" MaxLength="200" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvaddr" runat="server" Display="none" ControlToValidate="txtEmpAddre"
                                                        ValidationGroup="3" ErrorMessage="Please Enter Employee Address"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <%--  <asp:TextBox ID="txtLac_Add" runat="server" TextMode="MultiLine" Width="260px"></asp:TextBox>--%>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkadd" runat="server" CommandName="EAdd" Text="Add" ValidationGroup="3"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkIn" runat="server" CommandName="EInsert" Text="Insert" Visible="false"
                                                        ValidationGroup="3"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkC" runat="server" CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:Button ID="btnexpalt" runat="server" Text="Do you want to Finaly Save" CssClass="cssbutton"
                                    OnClick="btnexpalt_Click" OnClientClick="return update();" Visible="False" />
                                <asp:Button ID="btnexp" runat="server" CssClass="cssbutton" OnClick="btnexp_Click"
                                    Text="Save Experience" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:TextBox ID="txtjid" runat="server" Visible="False"></asp:TextBox>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="3"
                    ShowMessageBox="true" ShowSummary="false" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="2"
                    ShowMessageBox="true" ShowSummary="false" />
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="6"
                    ShowMessageBox="true" ShowSummary="false" />
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="5"
                    ShowMessageBox="true" ShowSummary="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hfcldate" runat="server" />
                <asp:HiddenField ID="hf_expnoofyear" runat="server" />
                <asp:HiddenField ID="hfqualitype" runat="server" />
                <asp:Button ID="btnexit" runat="server" Text="Back" CssClass="cssbutton" OnClick="btnexit_Click"
                    Visible="False" />
            </td>
        </tr>
       <tr>
            <td align="left">
                <asp:ImageButton ID="img_btn_prev" runat="server" Height="34px" ImageUrl="~/Images/prev.jpg"
                    Width="52px" Visible="true" OnClick="img_btn_prev_Click" />
            </td>
            <td align="right">&nbsp;<input id="csrftoken" runat="server" name="csrftoken" type="hidden" />
                <asp:ImageButton ID="img_btn_next" runat="server" Height="30px" ImageUrl="~/Images/next.jpg"
                    Width="50px" Visible="true" OnClick="img_btn_next_Click" />
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="lbl_step" runat="server" Text="Step 3/5" ForeColor="DarkGreen" Font-Bold="True" Font-Size="Large"
                    Font-Italic="True" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
