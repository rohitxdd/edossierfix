<%@ Control Language="C#" AutoEventWireup="true" CodeFile="callletter.ascx.cs" Inherits="usercontrols_callletter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" >
        </cc1:ToolkitScriptManager>
        
<table align="center">

       <tr>
        <td align="right" style="height: 21px"  >
            <asp:Label ID="lbljob" runat="server" Text="Select Post Applied" Font-Bold="True" CssClass="formheading"></asp:Label>
        </td>
        <td  align="left" style="height: 21px" >
           <asp:DropDownList ID="ddjob" runat="server" CssClass="ddl" Width="400px"  ></asp:DropDownList> 
             <asp:RequiredFieldValidator ID="rfvjob" runat="server"  ControlToValidate="ddjob"
             ErrorMessage="Please Select Job"></asp:RequiredFieldValidator></td>
         </tr>
        
       <%-- <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="1" />--%>
                    <asp:Label ID="lblmsg" runat="server">
                    </asp:Label>
                    </table>
