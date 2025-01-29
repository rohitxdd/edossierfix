<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="AuditDetail.aspx.cs" Inherits="AuditDetail" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<table style="width: 100%" align="center">
           
 <%--          <tr>
    <td>
<table width="100%" class="validatorstyles">
<tr>
        <td style="width: 426px">
      
            <asp:Label ID="Label4" runat="server" Text="User Id" Width="107px"></asp:Label>&nbsp;<asp:DropDownList ID="ddlUserId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserId_SelectedIndexChanged">
            </asp:DropDownList></td>     
        <td>
            <asp:Label ID="Label5" runat="server" EnableViewState="False"
                    Font-Bold="False" TabIndex="7" Text="Successful LogIn" Width="160px"></asp:Label>&nbsp;<asp:DropDownList
                        ID="ddlActive" runat="server" Width="110px" AutoPostBack="True" CssClass="txt"
                        TabIndex="8" OnSelectedIndexChanged="ddlActive_SelectedIndexChanged">
                        <asp:ListItem Value="">All</asp:ListItem>
                      
                         <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:DropDownList>
                </td> </tr>
    <tr><td colspan="2" align="center" style="height: 54px">
        &nbsp;&nbsp;
    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" ValidationGroup="1"/></td></tr>
    </table>
    </td>
    </tr>--%>
           <tr>

                            <td style="width: 895px">
                    <asp:GridView ID="grdaudit" runat="server" CssClass="gridfonts" PageSize="20" AllowPaging="true"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="grdaudit_PageIndexChanging" >
                   <HeaderStyle VerticalAlign="Top" BorderColor="White" BorderWidth="1px" CssClass="gridfonts"
                        BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
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
                           <asp:BoundField DataField="Action" HeaderText="Action " /> 
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            
        </table>
        
<asp:HiddenField ID="HiddenField" runat="server" />
<input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>

