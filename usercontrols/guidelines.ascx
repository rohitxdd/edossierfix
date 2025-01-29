<%@ Control Language="C#" AutoEventWireup="true" CodeFile="guidelines.ascx.cs" Inherits="usercontrols_guidelines" %>
<table border="0">
<tr>
<td valign="top" align="center">
                            <table cellspacing="0" style="width: 100%">
                           
                                <tr>
                                    <td style="height: 50px">
                                        <table id="tblScrollingNw" cellspacing="0" cellpadding="0" width="100%" border="0"
                                            runat="server" fontsize="2">
                                            <tr id="rowScrollingNw" runat="server">
                                                <td style="width: 100%">
                                                    <%--<marquee id="Marquee1" direction="up" onmouseout="this.start();"
                                                        onmouseover="this.stop();" scrollamount="1" scrolldelay="10" style="text-align: center"
                                                        width="320"><DIV style="TEXT-ALIGN: left"><SPAN style="COLOR: darkcyan"></SPAN><SPAN style="COLOR: darkslategray"></SPAN><DIV><TABLE style="FONT-SIZE: 9pt; WIDTH: 100%; COLOR: #333333; FONT-FAMILY: Calibri; BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="4" rules="all" border="1"><tbody></tbody></table></div>
                                                       
                                                      --%> 
                                                       <asp:GridView id="GridView_message" runat="server" AutoGenerateColumns="False"
                                                         BorderWidth="0" ShowHeader="false" GridLines="None" DataKeyNames="msgid,fileexist" OnRowDataBound="GridView_message_RowDataBound">
                                                        <Columns>
                                                         <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                         <ItemTemplate>
                                                         <asp:Image runat="server" ID="img_grid" ImageUrl="~/Images/blu_bull.gif" Height="12px" />
                                                          </ItemTemplate> 
                                                           </asp:TemplateField>                                                         
                                       <asp:TemplateField ItemStyle-HorizontalAlign="Left"> 
                                       <ItemTemplate>   
                                       <asp:HyperLink ID="hypl" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "message")%>'></asp:HyperLink>                           
                                
                            </ItemTemplate></asp:TemplateField>
        
                                                         </Columns> 
                                                        </asp:GridView>
                                                        <%--/div></marquee>--%>
                                                   <%-- </font>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
</tr>
</table>