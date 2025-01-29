<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Edossier_uploaddoc.aspx.cs" Inherits="Edossier_uploaddoc" %>

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
        <tr id="trothdocgrd1" runat="server" visible="false">
            <td>
                <asp:Label runat="server" ID="lblotherdoc" CssClass="formheading" Text="Upload Documents/Certificates"></asp:Label>
            </td>
        </tr>
        <tr id="trothdocgrd" runat="server" visible="false">
            <td align="center">
                <asp:GridView ID="grdother" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,certificateReq,remarks" Width="100%"
                    OnRowCommand="grdother_RowCommand" OnRowEditing="grdother_RowEditing" OnRowDataBound="grdother_RowDataBound"
                    OnRowUpdating="grdother_RowUpdating" OnRowCancelingEdit="grdother_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate/Document Required">
                            <ItemTemplate>
                                <%# Eval("certificateReq")%>
                                <%--  <asp:Label runat="server" ID="lb2" ForeColor="Red"></asp:Label>--%>
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
                                <asp:Image ID="img" runat="server" Width="114px" Height="94px" Visible="false" />
                                <asp:Image ID="img2" runat="server" Height="50px" Width="114px" Visible="false" />
                                <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false"
                                    Target="_blank" />
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" />
                                <asp:Label runat="server" ID="lb2" ForeColor="Red"></asp:Label>
                                <%--<asp:Label ID="lbladhar" runat="server" Visible="false" Text="Adhar No."></asp:Label>
                                <asp:TextBox ID="txtadharno" runat="server" Visible="false"></asp:TextBox>--%>
                               <%-- <asp:RegularExpressionValidator ID="revtxtadharno" runat="server" ControlToValidate="txtadharno"
                                    Display="None" ErrorMessage="Invalid character in Adhar No" ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$"
                                    ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                <%--<asp:Label ID="lbladharno" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"adharno") %>'></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label>
                                <asp:TextBox runat="server" ID="txtboxremarksothervalue" MaxLength="200" TextMode="MultiLine"
                                    ToolTip="Maximum 200 characters allowed" Width="95%" Height="80px" Visible="false">
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksothervalue"
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
                                <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="1" ShowMessageBox="true"
                                    ShowSummary="false" />
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
            <td align="center">
                <asp:Button ID="btnnext" runat="server" Text="Next>>" Width="100px" CssClass="cssbutton"
                    OnClick="btnnext_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
