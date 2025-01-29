<%@ Page Language="C#" AutoEventWireup="true" CodeFile="challan.aspx.cs" Inherits="challan" Title="Untitled Page" %>
<%@ Register Src="~/usercontrols/challan.ascx" TagName="challan" TagPrefix="no" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link rel="stylesheet" type="text/css" href="./CSS/Applicant.css" />
    <link href="./CSS/print.css" rel="stylesheet" type="text/css" media="print" />
</head>
<body>
<div id="div_print">
     
     
   <div id="footer" style="background-color:White;">
   </div>
      </div>
 <table align="left" style="width: 100%; margin-left:0px;" class="formlabel">
  
        <tr>
        <td style="width: 50%;">        
         <table>
            <tr>
            <td  align="center">Cash Voucher/Bank Copy</td></tr>
            <tr>
                <td  >
                    <no:challan ID="cha" runat="server" />
        
                </td>
                
            </tr>
          
            </table>
           
         </td>
            <td align="center" style="width: 3%;">
            <img alt="cutter" id="cutter" runat="server" src="Images/Cutter.png" style="height:900px;"/>   
            </td>
            
            <td style="width: 50%;" >
            <table >
            <tr><td  align="center">Cash Voucher/Candidate Copy</td></tr>
            <tr>
            <td>
                <no:challan ID="Challan1" runat="server" />
            </td> 
            </tr>
           </table>
        </td>
       </tr>
     
        </table>
         <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
        </body>

</html>
