<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ApplyInsert.aspx.cs" Inherits="ApplyInsert" %>


<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<table>
<tr>
<td> 
    <asp:Label ID="InsertMsg" runat="server" CssClass="formlabel" Font-Bold="True"></asp:Label>

</td>
</tr>
<tr>
<td align ="left"> 

 <asp:Label ID="Label2" runat="server" CssClass="formlabel" Font-Bold="True" >Instructions:  <br />1. Submit your Educational Qualification and Experience, if any  <asp:HyperLink ID="hlquli" runat="server" CssClass="cssbutton" Width="202px">Qualification/ Experience</asp:HyperLink> <br />2. Upload your Photo and Signature from the above link.<br />3. Confirm your Details by clicking the above link<br /> 4. Depositing the Required Fee in the Bank after generating the Bank Challan by clicking the link Print Challan above.</asp:Label>
 
 <%-- <asp:Label ID="Label1" runat="server" CssClass="formlabel" Font-Bold="True" >Instructions:  <br />1. Submit your Educational Qualification and Experience, if any  <asp:HyperLink ID="hlquli" runat="server" CssClass="cssbutton" Width="202px">Qualification/ Experience</asp:HyperLink> <br />2. Upload your Photo and Signature </asp:Label>--%>
 <%--<asp:Label ID="Label2" runat="server" CssClass="formlabel" Font-Bold="True" >Instructions:  <br />1. Submit your Educational Qualification and Experience, if any  <asp:HyperLink ID="HyperLink1" runat="server" CssClass="cssbutton" Width="202px">Qualification/ Experience</asp:HyperLink> <br />2. Upload your Photo and Signature from the above link.<br />3. Confirm your Details by clicking the above link<br /> 4. Depositing the Required Fee in the Bank after generating the Bank Challan by clicking the link Print Challan above.</asp:Label>--%>
<%--<asp:HyperLink ID="hlphoto" runat="server" CssClass="cssbutton" Width="202px">Click to Upload Photo and Signature</asp:HyperLink> --%>
</tr>


</table>
     <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    
</asp:Content>
