<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updateMobileEmail.aspx.cs" Inherits="updateMobileEmail" Title="Update Mobile/Email"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="stylesheet" type="text/css" href="css/Applicant.css" />
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
</head>
<body>
 <table width="90%" class="border_gray">
    <tr>
    
    </tr>
        <tr>
            <td align="left" colspan="4">
                 </td>
                  
                 
        </tr>
        
        <tr >
            <td colspan="4" class="tr" align="center">
              Registration Details
        
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">                
                    </td>
        </tr>
     <tr align="left" class="formlabel">
         <td colspan="4" align="center">&nbsp;&nbsp;&nbsp; 
                     <asp:Label ID="Label29" runat="server" Font-Bold="true" Text="Registration Number : ">
                     </asp:Label>
             &nbsp; &nbsp;&nbsp;
             <asp:Label ID="txt_reg" runat="server" Font-Bold="true" ></asp:Label></td>
     </tr>
        <tr align="left" class="darkblue">
        <td >
          <span style="color: red"></span><asp:Label ID="Label13" runat="server"  Text="Name"></asp:Label>
          </td>
        <td   align="left">              
                <asp:Label ID="txt_name" runat="server" Width="60%"  ></asp:Label>&nbsp;
        </td>
            <td > <asp:Label ID="Label2" runat="server" Text="UID Number" 
                    ></asp:Label></td>
            <td>
                <asp:Label ID="txtuid" runat="server" Width="40%"></asp:Label>
                     </td>
        </tr>
        <tr align="left" class="darkblue">
            <td style="width: 132px"  >
               <span style="color: red"></span><asp:Label ID="Label15" runat="server" Text="Father's Name" 
                    ></asp:Label>
            </td>
            <td style="width: 257px" >
                <asp:Label ID="txt_fh_name" runat="server" Width="70%" ></asp:Label>
            </td>
            <td align="left" style="width: 128px">
               <span style="color: red"></span> <asp:Label ID="Label16" runat="server" Text="Mother's Name" 
                     Width="90%"></asp:Label>
            </td>
            <td>
                <asp:Label ID="txt_mothername" runat="server" Width="70%" ></asp:Label>&nbsp;
            </td>
        </tr>
        
        <tr align="left" class="darkblue">
            <td align="left" style="width: 132px" >
               <span style="color: red"></span><asp:Label ID="Label27" runat="server"  Text="Gender"></asp:Label>
            </td>
            <td align="left" style="width: 257px" >
               <asp:Label ID="lblgender" runat="server"></asp:Label>

            </td>
            <td style="width: 128px" >
               
              <asp:Label ID="Label26"  runat="server" Text="Nationality" Width="75px" ></asp:Label></td>
            <td valign="middle">
               <asp:Label ID="lblnation" runat="server"></asp:Label><%-- <img id="Img1" alt="DatePicker" onclick="PopupPicker('txt_DOB', 250, 250)" src="Images/calendar.bmp"
                      style="width: 25px; height: 25px" runat="server" />--%></td>
        </tr>
        <tr align="left" class="darkblue">
            <td style="height: 21px; width: 132px;" >
              </td>
            <td style="width: 257px; height: 21px;">
                &nbsp;</td>
            <td align="left" style="width: 128px; height: 21px;">
               <span style="color: red"></span> </td>
            <td style="height: 21px">
                &nbsp;</td>
            
        </tr>
     <tr align="left" >
         <td colspan="4" align="center" style="color:Red">
             <strong>
         Please Update Mobile No. and Email:</strong>
         </td>
     </tr>
     <tr align="left" class="darkblue">
         <td align="center" colspan="4">
         </td>
     </tr>
        <tr align="left" class="darkblue">
            <td valign="top" style="width: 132px" >
               <span style="color: red">*</span> <asp:Label ID="Label23" runat="server"  Text="Mobile No."></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txt_mob" runat="server" Width="37%" MaxLength="10" CausesValidation="True"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvmob" runat="server" Display="none" ControlToValidate="txt_mob"
            ValidationGroup="1" ErrorMessage="Please Enter Mobile No.">
            </asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="revmob" runat="server" Display="None" ControlToValidate="txt_mob"
                ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                    ValidationGroup="1">
                    </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="REVMobile" runat="server" ControlToValidate="txt_mob"
                    ValidationExpression=".{10}.*" ErrorMessage="Enter Minimum 10 Digit" Display="none" ValidationGroup="1"></asp:RegularExpressionValidator>
                <asp:HiddenField ID="Hidden_txtmob" runat="server" />
                    </td>
            <td align="left" valign="top">
                <span style="color: red">*</span><asp:Label ID="Label24"  runat="server" Text="Email"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvmail" runat="server" Display="none" ControlToValidate="txt_email"
            ValidationGroup="1" ErrorMessage="Please Enter EMail-Id"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revmail" runat="server" Display="None" ControlToValidate="txt_email"
  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Characters In EmailId"
  ValidationGroup="1"></asp:RegularExpressionValidator>
  </td>
            
        </tr>
        <%--<tr align="left" runat="server" id="TRgetCodeMob">
            <td>
                <asp:Label ID="Label10" runat="server" CssClass="darkblue" Text="Get your Security Code on your Mobile"
                    Width="235px"></asp:Label></td>
            <td style="width: 257px; height: 21px;">
                <asp:Button ID="Button1" runat="server" CssClass="cssbutton" OnClick="Button1_Click"
                    Text="Get code" /><asp:HiddenField ID="Hidden_SecCode" runat="server" />
            </td>
            <td align="left" style="width: 128px; height: 21px;">
                <span style="color: #ff0000">*</span><asp:Label ID="Label11" runat="server" CssClass="darkblue"
                    Text="Enter Your Code"></asp:Label></td>
            <td style="height: 21px">
                <asp:TextBox ID="txtcode" runat="server" MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVCode" runat="server" ControlToValidate="txtcode"
                    Display="none" ErrorMessage="Please Enter Code" ValidationGroup="1"></asp:RequiredFieldValidator></td>
        </tr>--%>
        <tr align="left">
            <td colspan="4" style="color: #c00000; height: 21px">
                </td>
        </tr>
        
       
        <tr>
     
        <td colspan="4" align="center">
            <asp:Button ID="btnrsubmit" runat="server" CssClass="cssbutton" OnClick="btnrsubmit_Click"
                Text="Update" Width="91px" ValidationGroup="1" />
                   <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="1" />
                </td>
                     
        </tr>

    </table> 
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />

</body>
</html>

