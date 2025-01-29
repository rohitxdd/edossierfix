<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print_applicant.aspx.cs" Inherits="print_applicant" %>
<%@ Register Src="~/usercontrols/print.ascx" TagName="print" TagPrefix="no" %>
 <link rel="stylesheet" type="text/css" href="CSS/Applicant.css" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body onload='windows.print()'> 
    <form id="form1" runat="server">
    <center>
    <table>
        <tr>
            <td >
                <no:print ID="print" runat="server" />
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </center>
    </form>
</body>
</html>
