<%@ Control Language="C#" AutoEventWireup="true" CodeFile="latestannounce.ascx.cs" Inherits="UserControl_Menu_latestannounce" %>
<link href="css/dsssbstylesheet.css" rel="stylesheet" type="text/css" />
<table><tr><td>
<%--<div id="scroll">--%>
<asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="400" Width="350px">
<asp:Label ID="lblmsg" runat="server" Visible="false" Text="There is no Current openings..." CssClass="helpdesktext"></asp:Label>
<asp:GridView id="grdsplpost" runat="server" AutoGenerateColumns="False"
          BorderWidth="0" ShowHeader="false" GridLines="None" DataKeyNames="ADVT_NO,announcement" Visible="false">
           <Columns>
            <asp:TemplateField>
                 <ItemTemplate>
            <%--<asp:Image runat="server" ID="img_grid" ImageUrl="~/Images/new.jpg" />--%>
            <asp:Image runat="server" ID="img_grid" ImageUrl="~/Images/arrow1.jpg" />
            </ItemTemplate> 
        </asp:TemplateField>                                                         
        <asp:TemplateField> 
        <ItemTemplate> 
         <asp:Label ID="lblannounc" runat="server" TITLE="To Apply This Post First Register Yourself" Text='<%# Bind("announcement") %>'></asp:Label>   
       <%-- <asp:HyperLink ID="hyplannounce" runat="server" TITLE="To Apply This Post First Register Yourself" Text=' <%# DataBinder.Eval(Container.DataItem, "announcement")%>'></asp:HyperLink>  --%>                             
      </ItemTemplate></asp:TemplateField>
             </Columns> 
                </asp:GridView>
<asp:GridView id="grdannouncement" runat="server" AutoGenerateColumns="False"
          BorderWidth="0" ShowHeader="false" GridLines="None" DataKeyNames="ADVT_NO,announcement" OnRowDataBound="grdannouncement_RowDataBound">
           <Columns>
            <asp:TemplateField>
                 <ItemTemplate>
            <%--<asp:Image runat="server" ID="img_grid" ImageUrl="~/Images/new.jpg" />--%>
            <asp:Image runat="server" ID="img_grid" ImageUrl="~/Images/arrow1.jpg" />
            </ItemTemplate> 
        </asp:TemplateField>                                                         
        <asp:TemplateField> 
        <ItemTemplate> 
         <asp:Label ID="lblannounc" runat="server" TITLE="To Apply This Post First Register Yourself" Text='<%# Bind("announcement") %>'></asp:Label>   
       <%-- <asp:HyperLink ID="hyplannounce" runat="server" TITLE="To Apply This Post First Register Yourself" Text=' <%# DataBinder.Eval(Container.DataItem, "announcement")%>'></asp:HyperLink>  --%>                             
      </ItemTemplate></asp:TemplateField>
             </Columns> 
                </asp:GridView>
                </asp:Panel>
                <%--</div>--%></td></tr></table>