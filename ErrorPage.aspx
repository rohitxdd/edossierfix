<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" Title="Untitled Page" EnableSessionState="True" %>
<%--MasterPageFile="~/MasterPage.master"--%>
 
<%--<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
<script type="text/javascript" language="javascript">     
    function GoBack()    
    {
   // alert('goback');
    //window.history.go(+1);
    window.history.forward(1);
    
    } 
    GoBack();
    </script>


<table width="100%" align="center">
            <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblmsg" runat="server" Font-Size="30px" ForeColor="red" Text="Some Error occured. Please Contact your System Administrator"></asp:Label>
</td>
            </tr>
    <tr>
        <td align="center" colspan="2" style="height: 21px">
            <asp:hyperlink id="HyperLink1" runat="server" font-bold="True" navigateurl="~/Default.aspx">Please Login again</asp:hyperlink>
        </td>
    </tr>
            </table>
</body>
</html>
<%--</asp:Content>--%>

