<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdmitCCMaster.aspx.cs" Inherits="AdmitCCMaster" %>
<%@ Register Src="~/usercontrols/AdmitCardConsent.ascx" TagName="acc" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <table width="90%">
<tr>
<td>
   <uc2:acc ID="Top3" runat="server" />

</td>
</tr>

</table>
<input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>

