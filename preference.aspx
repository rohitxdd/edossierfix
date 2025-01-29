<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="preference.aspx.cs" Inherits="preference" Title="Untitled Page" %>
<%@ Register Src="~/usercontrols/callletter.ascx" TagName="callletter" TagPrefix="no" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<table style="background-color:#DDDDFF"  >
        <tr >
            <td style="height: 129px" colspan ="4">
                <no:callletter ID="clltr" runat="server" />
            </td>
            
        </tr>
        </table>
         <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>

