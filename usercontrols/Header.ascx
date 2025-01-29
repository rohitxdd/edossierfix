<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="usercontrols_Header" %>

<!-- Google tag (gtag.js) -->
<script async src="https://www.googletagmanager.com/gtag/js?id=G-NT3BG90CW2"></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'G-NT3BG90CW2');
</script>

<link href="CSS/dsssbstylesheet.css" rel="stylesheet" type="text/css" />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
    <td class="headerbg">&nbsp;</td>
        <td class="headerbg" width="900px">
            <table width="100%" border="0" align="center" cellpadding="2" cellspacing="2">
                <tr>
                  <%--  <td width="15%">
                        &nbsp;
                    </td>--%>
                    <td width="40%">
                        <table width="100%" border="0" cellspacing="2" cellpadding="2">
                            <tr>
                                <td align="left" width="320">
                                    <img src="Images/oarslogo.jpg" width="320" height="100" />
                                </td>
                            </tr>
                            <tr>
                                <td class="logotitle" align="left" width="320">
                                    Online Application Registration System
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <table width="100%" border="0" cellspacing="2" cellpadding="2">
                            <tr>
                                <td align="right">
                                    <img src="Images/dsssbtext1.jpg" width="542" height="33" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdright" align="right">
                                    Government of NCT of Delhi &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="10%">
                        <img src="Images/dsssblogo.jpg" width="129" height="119" />
                    </td>
                   <%-- <td width="15%">
                        &nbsp;
                    </td>--%>
                </tr>
            </table>
        </td>
        <td class="headerbg">&nbsp;</td>
    </tr>
    <tr>
    <td  class="yellowbg" align="left"> <asp:Label ID="lblname" runat="server" CssClass="datetimelabletext"></asp:Label></td>
        <td class="yellowbg" align="right" width="1000px" >
       
            <asp:Label ID="lbldatetime" runat="server" CssClass="datetimelabletext"></asp:Label>
        </td>
        <td  class="yellowbg" >&nbsp;</td>
    </tr>
</table>