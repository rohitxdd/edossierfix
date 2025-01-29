<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Src="~/usercontrols/noticeboard.ascx" TagName="WebUserControl" TagPrefix="uc3" %>
<%@ Register Src="~/usercontrols/latestannounce.ascx" TagName="WebUserControl" TagPrefix="uc4" %>
<%@ Register Src="~/usercontrols/guidelines.ascx" TagName="instruction" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <link href="CSS/dsssbstylesheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Jscript/JScript.js">    
    </script>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="bodybg">
        <tr>
            <td>
                <table width="950px" cellpadding="2" cellspacing="2" border="0" align="center" class="bodybg">
                    <tr>
                        <td width="350px" valign="top">
                            <table width="350px" border="0" cellspacing="0" cellpadding="0" align="center">
                                <tr>
                                    <td width="9">
                                        <img src="Images/buleleftconner.png" width="9" height="48" />
                                    </td>
                                    <td width="100%" background="Images/bulebgcolorcenter.png" class="whiteheadings"
                                        align="left">
                                        Current Vacancies
                                    </td>
                                    <td width="9">
                                        <img src="Images/bulerightconner.png" width="9" height="48" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="tdbggreecolor" height="350" valign="top">
                                        <uc4:WebUserControl ID="WebUserControl4" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" width="300">
                            <table width="300" border="0" cellspacing="2" cellpadding="2">
                                <%--                                    <tr>
                                        <td class="yellowheader" width="300" align="left" valign="middle">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                        <td width="40"><img src="Images/about.png" width="40" height="35" /></td>
                                        <td width="260">
                                            <asp:Label ID="aboutqars" runat="server" CssClass="yellowheadertext" Text="About Us..."></asp:Label>
                                            </td>
                                        </tr>
                                        </table>
                                        
                                        &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="aboutstext">
                                             On the 50th Anniversary year of the Indian Independence, the Government of 
                                             National Capital Territory of Delhi has instituted the Delhi Subordinate 
                                             Services Selection Board. The Board has been incorporated with the purpose of 
                                             recruiting capable, competent, highly skilled individuals by conducting written 
                                             tests, professional tests and personal interviews wherever as desired. The Board 
                                             shall hereby committed to develop selection and recruitment procedures that 
                                             confirm to the global standards in testing, and promise selections by all fair 
                                             means, of the most competent, capable, and skilled individuals for user 
                                             departments.
                                        </td>
                                    </tr>
                                --%><%-- <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>--%>
                                <tr>
                                    <td valign="top" width="300">
                                        <table width="300" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="300" align="left" class="yellowheader" valign="top">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="35">
                                                                <img src="Images/news.png" width="35" height="35" />
                                                            </td>
                                                            <td width="265">
                                                                <asp:Label ID="lblNews" runat="server" CssClass="yellowheadertext" Text="News & Events..."></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr id="trUploadDocPCSP" runat="server" visible="false">
                                                <td align="left" colspan="4" class="darkblue">
                                                    <asp:Image runat="server" ID="img_grid" ImageUrl="~/Images/new2.gif" />
                                                    <asp:LinkButton ID="txtblnk" runat="server" ForeColor="Red" Font-Size="larger" Font-Bold="True"
                                                        OnClick="txtblnk_Click"><u>Link to update the registration details and uploading of post card size photograph for all
those candidates who have applied for advt. No. 1/20,2/20,3/20,4/20,5/20</u> </asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdbggreecolor">
                                                    <uc3:WebUserControl ID="WebUserControl3" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="300" valign="top">
                            <table width="300" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="9">
                                        <img src="Images/buleleftconner.png" width="9" height="48" />
                                    </td>
                                    <td width="100%" background="Images/bulebgcolorcenter.png" class="whiteheadings">
                                        Guidelines
                                    </td>
                                    <td width="9">
                                        <img src="Images/bulerightconner.png" width="9" height="48" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="tdbggreecolor" align="center">
                                        <uc5:instruction ID="WebUserControl5" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="tdbggreecolor">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="tdbggreecolor">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="tdbggreecolor">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="tdbggreecolor">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="tdbggreecolor">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="tdbggreecolor">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="300" align="left" class="yellowheader" valign="middle" colspan="3">
                                        <asp:Label ID="lblhelpdesk" runat="server" CssClass="Headings" Text="Helpdesk"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblhelpno" runat="server" Text="Help Desk No. : 011-22379204, 011-22370307"
                                            CssClass="helpdesktext"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="300" align="left" class="yellowheader" valign="middle" colspan="3">
                                        <asp:Label ID="lblsupport" runat="server" CssClass="Headings" Text="Support"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="hyperlinktext"
                                            NavigateUrl="http://get.adobe.com/reader/">Adobe Acrobat Reader </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <asp:HiddenField ID="txtrandomno" runat="server" />
                            <asp:ValidationSummary ID="vs" HeaderText="Login failed; Invalid user ID or Password."
                                runat="server" ValidationGroup="8" ShowMessageBox="true" ShowSummary="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
