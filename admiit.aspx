<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="admiit.aspx.cs" Inherits="admiit" Title="Admit card" %>
<%@ Register Src="~/usercontrols/callletter.ascx" TagName="callletter" TagPrefix="no" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <table>
<tr>
<td>
 <table style="background-color:#DDDDFF" id="tbladmit" runat="server">
 <tr><td colspan="2" align="center"><asp:RadioButtonList ID="rbtexamtype" 
         runat="server" AutoPostBack="true" Font-Bold="True" CssClass="formheading" 
         RepeatDirection="Horizontal" 
         onselectedindexchanged="rbtexamtype_SelectedIndexChanged" >
 <asp:ListItem Value="1" Selected="True">Tier-1 Exam</asp:ListItem>
 <asp:ListItem Value="2" >Tier-2 Exam</asp:ListItem>
 </asp:RadioButtonList></td></tr>
        <tr id="truser" runat="server" >
        
            
         <td align="right" style="height: 20px" >
            <asp:Label ID="lbljob" runat="server" Text="Select Post Applied" Font-Bold="True" CssClass="formheading"></asp:Label>
        </td>
        <td  align="left" style="height: 20px" >
           <asp:DropDownList ID="ddjob" runat="server" CssClass="ddl" Width="400px"  ></asp:DropDownList> 
             <asp:RequiredFieldValidator ID="rfvjob" runat="server"  ControlToValidate="ddjob"
             ErrorMessage="Please Select Job"></asp:RequiredFieldValidator>
            
        </td>
            
        </tr>
        <tr id="Tr1" runat="server" visible="true">
            <td colspan="2" style="height: 20px">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                    Text="Print Admit Card" Width="124px" CssClass="cssbutton" />
                    </td>
        </tr>
      <tr id="Tr2" runat="server" visible="true">
          <td colspan="2" style="height: 20px">
          <br />
          <br />
              &nbsp;<br />
              <br />
              <br />
              </td>
      </tr>
      </table>
</td>
</tr>
<tr>
<td>
          
              <asp:Label ID="LabelNote" runat="server" Visible="False" CssClass="formheading" ForeColor="#C00000"></asp:Label></td>
</tr>
</table>

 
   <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>

