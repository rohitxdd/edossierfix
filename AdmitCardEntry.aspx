<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmitCardEntry.aspx.cs" Inherits="AdmitCardEntry" %>

<%@ Register Src="~/usercontrols/Header.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/Footer.ascx" TagName="WebUserControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="css/dsssbstylesheet.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/Applicant.css" />
    <link href="CSS/Applicant.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="bodybg">
            <tr>
                <td colspan="2">
                    <uc1:WebUserControl ID="WebUserControl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="950px" align="center">
                        <tr>
                            <td colspan="2" align="right">
                                <a href="Default.aspx" target="_self">Home</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right" valign="top" width="50%">
                                <asp:CheckBox ID="chk1" runat="server" Text="Click here to Generate Provisional Admit Card, only in case, allowed by the Board to get Provisional."
                                    AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" ForeColor="#666666" Font-Bold="true" Font-Size="Small" />
                                <asp:Label ID="lblprovmsg" runat="server" Text="# Provisional Admit Card can only be Printed from the Main Site."
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="grid_admit" runat="server" AutoGenerateColumns="False" CssClass="gridfont"
                                    DataKeyNames="jid" Height="100%" Width="100%" Font-Size="Large">
                                    <Columns>
                                        <%-- <asp:buttonfield buttontype="Link" commandname="Click" datatextfield="Postcode" />   --%>
                                        <asp:BoundField DataField="Postcode" HeaderText="Post Code" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jobtitle" HeaderText="Post Name" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dateofexam" HeaderText="Exam Date" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="gv1tierpet" runat="server" AutoGenerateColumns="False" Caption="e-Admit Card available for the Posts for First Tier PET/Skill Test/Online Exam"
                                    CssClass="gridfont" DataKeyNames="jid,examid" Height="100%" Width="100%" OnRowDataBound="gv1tierpet_RowDataBound" Font-Size="Large">
                                    <Columns>
                                        <%-- <asp:buttonfield buttontype="Link" commandname="Click" datatextfield="Postcode" />   --%>
                                        <asp:BoundField DataField="Postcode" HeaderText="Post Code" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jobtitle" HeaderText="Post Name" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblexamdate" runat="server"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle">
                                <asp:Label ID="lbl_msg" runat="server" Text=""
                                    Visible="False" ForeColor="#CC3300"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="grid2tier" runat="server" AutoGenerateColumns="False" Caption="e-Admit Card available for the Posts for Second Tier Exam"
                                    CssClass="gridfont" DataKeyNames="jid" Height="100%" Width="100%" Font-Size="Large">
                                    <Columns>
                                        <%-- <asp:buttonfield buttontype="Link" commandname="Click" datatextfield="Postcode" />   --%>
                                        <asp:BoundField DataField="postcode" HeaderText="Post Code" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jobtitle" HeaderText="Post Name" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dateofexam" HeaderText="Exam Date" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="grid2tiertest" runat="server" AutoGenerateColumns="False" Caption="e-Admit Card available for the Posts for Second Tier PET/Skill Test"
                                    CssClass="gridfont" DataKeyNames="jid,examid" Height="100%" Width="100%" OnRowDataBound="grid2tiertest_RowDataBound" Font-Size="Large">
                                    <Columns>
                                        <%-- <asp:buttonfield buttontype="Link" commandname="Click" datatextfield="Postcode" />   --%>
                                        <asp:BoundField DataField="postcode" HeaderText="Post Code" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jobtitle" HeaderText="Post Name" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblexamdate" runat="server"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="gvtier3exam" runat="server" AutoGenerateColumns="False" Caption="e-Admit Card available for the Posts for Third Tier Exam"
                                    CssClass="gridfont" DataKeyNames="jid" Height="100%" Width="100%" Font-Size="Large">
                                    <Columns>
                                        <%-- <asp:buttonfield buttontype="Link" commandname="Click" datatextfield="Postcode" />   --%>
                                        <asp:BoundField DataField="postcode" HeaderText="Post Code" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jobtitle" HeaderText="Post Name" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dateofexam" HeaderText="Exam Date" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="gvtier3test" runat="server" AutoGenerateColumns="False" Caption="e-Admit Card available for the Posts for Third Tier PET/Skill Test"
                                    CssClass="gridfont" DataKeyNames="jid,examid" Height="100%" Width="100%" OnRowDataBound="gvtier3test_RowDataBound" Font-Size="Large">
                                    <Columns>
                                        <%-- <asp:buttonfield buttontype="Link" commandname="Click" datatextfield="Postcode" />   --%>
                                        <asp:BoundField DataField="postcode" HeaderText="Post Code" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jobtitle" HeaderText="Post Name" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblexamdate" runat="server"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemStyle CssClass="gridfont" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridheading" />
                                </asp:GridView>
                            </td>
                        </tr>
                         <tr id="trprint1" runat="server" visible="false">
                                        <td colspan="4" align="center" valign="middle">
                                            <asp:Label ID="lbl" runat="server" Text="Generate Your e-Admit Card" CssClass="formlabel"></asp:Label>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr id="trprint2" runat="server" visible="false">
                                        <td colspan="4" align="center">
                                            <asp:RadioButtonList ID="rbtexamtype" runat="server" AutoPostBack="true" Font-Bold="True"
                                                CssClass="formheading" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtexamtype_SelectedIndexChanged">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                        <tr>
                            <td colspan="2" align="center" valign="middle">
                                <table width="100%" runat="server" id="tbl" class="bodybg" visible="false">
                                   
                                    <tr id="trappno" runat="server">
                                        <td align="left" style="width: 20%" valign="top">
                                            <asp:Label ID="lbl_appno" runat="server" Text="Enter Application No."></asp:Label>
                                        </td>
                                        <td align="left" style="width: 15%" valign="top">
                                            <asp:TextBox ID="txt_appno" runat="server" Text="" MaxLength="8"></asp:TextBox>
                                        </td>
                                        <td align="left" width="15%" valign="top">
                                            <a href="know_appno.aspx" target="_blank" style="height: 200px; width: 200px">Know Your
                                                Application No.</a>
                                        </td>
                                    </tr>
                                    <tr id="trregno" runat="server" visible="false">
                                        <td align="left" style="width: 20%" valign="top">
                                            <asp:Label ID="Label2" runat="server" Text="Enter Registration No."></asp:Label>
                                        </td>
                                        <td align="left" style="width: 80%" colspan="3" valign="top">
                                            <asp:TextBox ID="txtregno" runat="server" AutoPostBack="true" OnTextChanged="txtregno_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trpostcode" runat="server" visible="false">
                                        <td align="left" style="width: 20%" valign="top">
                                            <asp:Label ID="Label3" runat="server" Text="Select Post"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 80%" colspan="3" valign="top">
                                            <asp:DropDownList ID="ddlpost" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 20%" valign="top">
                                            <asp:Label ID="lbl_dob" runat="server" Text="Enter Date of Birth"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 80%" colspan="3" valign="top">
                                            <asp:TextBox ID="txt_dob" runat="server" Text="" MaxLength="10"></asp:TextBox>
                                            <asp:Label ID="Label1" runat="server" Text="(DD/MM/YYYY)"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvdiv4" runat="server" ControlToValidate="txt_dob"
                                                ErrorMessage="Please Enter Date of Birth" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revetime" runat="server" ControlToValidate="txt_dob"
                                                ErrorMessage="Date of Birth should be in DD/MM/YYYY " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lbl_captcha" runat="server" Text="Security Code"></asp:Label>
                                        </td>
                                        <td align="left" colspan="3" valign="middle">
                                            <img alt="Visual verification" height="50" src='JpegImage_CS.aspx?r=<%= System.Guid.NewGuid().ToString("N") %>'
                                                title="Please enter the Visual Code as shown in the image." vspace="5" width="150" />
                                            <asp:ImageButton ID="ibtnRefresh" runat="server" ImageUrl="~/images/refresh.jpg"
                                                OnClick="ibtnRefresh_Click" OnClientClick="return SignValidateRefresh();" ToolTip="Click here to load a new Image" />
                                            Type the code shown
                                            <asp:TextBox ID="txtCode" runat="server" autocomplete="off" AutoCompleteType="None"
                                                MaxLength="10" oncopy="return false" oncut="return false" onpaste="return false"
                                                TabIndex="7" ToolTip="Enter Above Characters in the Image" />
                                            &nbsp;<asp:RequiredFieldValidator ID="RFVCaptcha" runat="server" ControlToValidate="txtCode"
                                                ErrorMessage="Enter Visual Code" SetFocusOnError="True" ToolTip="Visual Code"
                                                ValidationGroup="1" Width="150px">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            &nbsp;
                                        </td>
                                        <td align="left" colspan="3" valign="top">
                                            <asp:Button Text="Click to Generate e-Admit Card" ID="btn_submit" runat="server" ValidationGroup="1" OnClick="btn_submit_Click"  Font-Bold="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" valign="middle">
                                <table width="100%" runat="server" id="tbltier2" class="bodybg" visible="false">
                                    <tr>
                                        <td align="left" style="width: 30%; vertical-align: top" valign="top">
                                            <asp:Label ID="lbltierrollno" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 70%; vertical-align: top" valign="top">
                                            <asp:TextBox ID="txtrollno" runat="server" Text="" MaxLength="15"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revtxtrollno" runat="server" ControlToValidate="txtrollno"
                                                Display="None" ErrorMessage="Only Integers are allowed in Roll No" ValidationExpression="^[0-9]*$"
                                                ValidationGroup="2"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="rfvtxtrollno" runat="server" ControlToValidate="txtrollno"
                                                ErrorMessage="Please Enter Rollno" Display="None" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                     <tr id="tr2tierpost" runat="server" visible="false">
                                        <td align="left" style="width: 20%" valign="top">
                                            <asp:Label ID="Label4" runat="server" Text="Select Post"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 80%" colspan="3" valign="top">
                                            <asp:DropDownList ID="ddl2tierpost" runat="server">
                                            </asp:DropDownList> <asp:RequiredFieldValidator ID="rfvddl2tierpost" runat="server" ControlToValidate="ddl2tierpost"
                                                ErrorMessage="Please Select Post" Display="None" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btntier2submit" runat="server" Text="Click to Generate e-Admit Card" OnClick="btntier2submit_Click"
                                                ValidationGroup="2"  Font-Bold="true" />&nbsp;
                                            <%--<asp:Button
                                                ID="btnprintinst" runat="server" Text="Print Instruction" 
                                                onclick="btnprintinst_Click" ValidationGroup="2" Visible="false" />--%>
                                            <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="2" ShowMessageBox="true"
                                                ShowSummary="false" />
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
                    <uc2:WebUserControl ID="WebUserControl2" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
