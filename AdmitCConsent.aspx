<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmitCConsent.aspx.cs" Inherits="AdmitCConsent" %>
<%@ Register Src="~/usercontrols/MainHeader.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/AdmitCardConsent.ascx" TagName="acc" TagPrefix="uc2" %>
<link href="CSS/Applicant.css" rel="stylesheet" type="text/css" />

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div width="60%">
     <table align="center"  width="60%" id="tbl1">
     <tr>
     <td>
      <uc1:top ID="Top2" runat="server" />
     </td>
     </tr>
     
     <tr>
     <td>
           <uc2:acc ID="Top3" runat="server" />
     </td>
     </tr>

     </table>
     <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </div>
    </form>
</body>
</html>
