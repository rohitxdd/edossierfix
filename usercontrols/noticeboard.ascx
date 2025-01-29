<%@ Control Language="C#" AutoEventWireup="true" CodeFile="noticeboard.ascx.cs" Inherits="UserControl_Menu_noticeboard" %>
<table>
   
    <tr>
        <td align="center">
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="370" Width="100%">
                <asp:GridView ID="GridView_message" runat="server" AutoGenerateColumns="False" BorderWidth="0"
                    ShowHeader="false" GridLines="None" DataKeyNames="msgid,fileexist,m_edate" OnRowDataBound="GridView_message_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Image runat="server" ID="img_grid" ImageUrl="~/Images/new2.gif" />
                                <asp:Image runat="server" ID="img_arrow" Visible="false" ImageUrl="~/Images/arrow1.jpg" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--  ControlStyle-ForeColor="navy"--%>
                        <asp:TemplateField ControlStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:HyperLink ID="hypl" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "message")%>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
