<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintChallanForm.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="PrintChallanForm" %>

<%@ Register Src="~/usercontrols/callletter.ascx" TagName="callletter" TagPrefix="no" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server" >
<table>
<tr id="trcha" runat="server">
<td>
  <table style="background-color:#DDDDFF" id="tblcha" runat="server">
        <tr id="truser" runat="server" >
           
           <td align="right" style="width: 240px; height: 20px;" >
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
                <asp:Button ID="btn_print_c" runat="server" OnClick="Button1_Click"
                    Text="Print Challan" Width="98px" CssClass="cssbutton" /></td>
        </tr>
        <tr id="Tr2" runat="server" visible="true">
            <td colspan="2" align="left">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#CC3300" 
                    Text="Instruction:"></asp:Label>
            
                <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" 
                    Text="Please check the Pop-Up settings of your system before click of Print Challan. If Pop-Up blocked change it to allow Pop-Up."></asp:Label>
          <br />
                &nbsp;<br />
              <br />
              <br />
            </td>
        </tr>
       

        <tr align="left">
        <td>
        <asp:Label ID="lbl_step" runat="server" Text="Step 5/5" ForeColor="DarkGreen" Font-Bold="True" Font-Italic="True" Visible="False"></asp:Label>
        </td>
  </tr>
    </table>
          
              <asp:Label ID="LabelNote" runat="server" Visible="False" CssClass="formheading" ForeColor="#C00000"></asp:Label></td>
</tr>
<tr>
<td>
            <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Text="Fee Exemption(No Need of Challan)"
                Visible="False"></asp:Label></td>
</tr>
</table>
  
     <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>