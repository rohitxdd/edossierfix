<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AuditDetails.aspx.cs" Inherits="AuditDetails" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<table width="100%" align="center">
<tr>
<td>
                    <asp:GridView ID="grdaudit" runat="server" PageSize="20" AllowPaging="true"
                    BorderStyle="None" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="grdaudit_PageIndexChanging" CssClass="gridfont" >
                   <HeaderStyle VerticalAlign="Top" CssClass="gridheading" Font-Names="Arial" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                         <ItemTemplate>
			                &nbsp;<%#Container.DataItemIndex+1%> 
                        </ItemTemplate>
                    
                </asp:TemplateField>
                 <asp:BoundField DataField="userid" HeaderText="User Id"  />
                     <asp:BoundField DataField="Ipaddress" HeaderText="IP Address"  />   
                     <asp:BoundField DataField="LoginDatetime" HeaderText="LogIn Time"  /> 
                      <asp:BoundField DataField="LogoutDatetime" HeaderText="Logout Time"  /> 
                      <asp:BoundField DataField="Status" HeaderText="Successful LogIn " /> 
                        </Columns>
                    </asp:GridView>
</td>
</tr>
</table>
</asp:Content>

