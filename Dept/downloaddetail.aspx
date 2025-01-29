<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="downloaddetail.aspx.cs" Inherits="downloaddetail" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <table width="100%" align="center" class="validatorstyles">
        <tr>
            <td style="height: 19px">
            </td>
        </tr>
        <tr><td><asp:RadioButtonList ID="rbttype" runat="server" 
                RepeatDirection="Horizontal" CssClass="validatorstyles" AutoPostBack="True" 
                onselectedindexchanged="rbttype_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="C">Current</asp:ListItem>
        <asp:ListItem  Value="A">Archieve</asp:ListItem></asp:RadioButtonList></td></tr>
        <tr>
            <td>
                <asp:GridView ID="grddetails"  runat="server" AutoGenerateColumns="False" CssClass="validatorstyles" DataKeyNames="dateofsending,slot" Width="60%"  CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="#000040" BorderStyle="Solid" BorderWidth="2px" OnRowCommand="grddetails_RowCommand" OnRowDataBound="grddetails_RowDataBound"  >
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="Datesent">
                            <ItemTemplate>
                                <%--<%# Eval("dateofsending")%>--%>
                                <asp:Label ID="lbldate" runat="server" ></asp:Label>
                                
                            </ItemTemplate>                          
                        </asp:TemplateField> 
                          <asp:TemplateField HeaderText="File No.">
                            <ItemTemplate>
                                <%# Eval("slot")%>
                            </ItemTemplate>                          
                        </asp:TemplateField> 
                        
                         <asp:TemplateField HeaderText="No. of Records">
                            <ItemTemplate>
                                
                                <asp:HyperLink ID="hytotal" runat="server" Target="_blank" Text='<%# Eval("total_count")%>'></asp:HyperLink>
                            </ItemTemplate>                          
                        </asp:TemplateField>
                        
                       <%--  <asp:TemplateField HeaderText="Fee Received Count">
                            <ItemTemplate>
                                
                                <asp:HyperLink ID="hyfee" runat="server" Target="_blank" Text='<%# Eval("fee_receive_count")%>'></asp:HyperLink>
                            </ItemTemplate>                          
                        </asp:TemplateField>--%>
                        
                         <%--<asp:TemplateField HeaderText="DownloadFile">
                            <ItemTemplate>
                               <asp:LinkButton ID="lnksend" Text="Send Bank Data" runat="server" CommandName="send" CommandArgument='<% # Container.DataItemIndex %>'></asp:LinkButton>                              
                            </ItemTemplate>                          
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="DownloadFile">
                            <ItemTemplate>
                              <asp:LinkButton ID="lnkresend" Text="Resend Data" runat="server" CommandName="resend" CommandArgument='<% # Container.DataItemIndex %>'></asp:LinkButton>
                            </ItemTemplate>                          
                        </asp:TemplateField> --%>
                        
                        <asp:TemplateField HeaderText="DownloadFile">
                            <ItemTemplate>
                               <asp:LinkButton ID="lnkdwnload"  runat="server"  CommandArgument='<% # Container.DataItemIndex %>'></asp:LinkButton>                              
                            </ItemTemplate>                          
                        </asp:TemplateField> 
                        
                        
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                       
                </asp:GridView>
            </td>
        </tr>
    </table>
    <input id="csrftoken" runat="server" name="csrftoken" type="hidden" />
</asp:Content>

