<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReplaceRecallDoc.aspx.cs" Inherits="ReplaceRecallDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<table style="text-align: center">
 <tr >
            <td align="center">
                <asp:GridView ID="grdother" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,certificateReq"  Width="100%" Caption="List of Recalled  Documents/Certificates to Replace"
                   
                    onrowdatabound="grdother_RowDataBound" onrowupdating="grdother_RowUpdating"
                    >
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
                                <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Target="_blank" />
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" /> 
                                 <asp:Label ID="lbladhar" runat="server" Visible="false" Text="Adhar No."></asp:Label>
                                <asp:TextBox ID="txtadharno" runat="server" Visible="false" ></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="revtxtadharno" runat="server" ControlToValidate="txtadharno" Display="None" 
                                  ErrorMessage="Invalid character in Adhar No" ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:Label ID="lbladharno" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"adharno") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                               
                                    <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="true"></asp:LinkButton>
                                     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="1" ShowMessageBox="true"
             ShowSummary="false" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
                <asp:HiddenField ID="hfdummy_no" runat="server" Visible="false" />
            </td>
        </tr></table>
</asp:Content>

