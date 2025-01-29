<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<head id="Head1" runat="server">

<link rel="stylesheet" type="text/css" href="./CSS/Applicant.css" />
<script type="text/javascript" language="javascript" src="Jscript/JScript.js">
</script>
<script type="text/javascript" language="javascript" src="Jscript/md5.js">
</script>
<script language="javascript" type="text/javascript"> 
function checkJavaScriptValidity()
{ 
document.getElementById("jsEnabled").style.visibility = 'hidden'; 
document.getElementById("jsDisabled").style.visibility = 'hidden';

document.getElementById('<%= btnrsubmit.ClientID %>').disabled=false;
} 
</script>

 <script language="javascript" type="text/javascript">
    

       function SignValidate()
       { 
         if (!Page_ClientValidate()) 
            {
            return false; 
            }
           
         var npwd=document.getElementById('<%= txtpassword.ClientID %>').value;
          var npwdhash = MD5(npwd);  
          document.getElementById('<%= txtpassword.ClientID %>').value=npwdhash; 
          var npwd=document.getElementById('<%= txt_re_password.ClientID %>').value;
          var re_npwdhash = MD5(npwd);  
          document.getElementById('<%= txt_re_password.ClientID %>').value=re_npwdhash; 
        
        }    
    </script>
    <script type="text/javascript" language="javascript">
function PassValidate()
{ 
     
        var exppwd =/[\^|\*|\@|\~|\!|\#|\(|\)|\{|\}|=|\[|\]|\:|\;|\.|\'|\?|\/|\$|\_|\,|\-|\+|\%]+/;
        var expMassage = '(\",%,;,:,~,!,#,^,{,},_,-,(,),*\)';
        var flag=false;
   
        
        var TxtPassStr= document.getElementById('<%=txtpassword.ClientID%>').value
        if(TxtPassStr.length<8)
        {
            alert("Password should be at least 8 characters long.\nPlease re-enter the Password")
            document.getElementById('<%=txtpassword.ClientID%>').value="";
         
            return false;
        }
        if(TxtPassStr!="")
          {
                if(exppwd.test(TxtPassStr)==false)
                    {
                        alert("Atleast one of the Special characters like\n" +expMassage+ " \nmust be in the New Password.");
                        document.getElementById('<%=txtpassword.ClientID%>').value="";
                         return false;
                    }
                    
                  var re = /[0-9]/;
                    if(re.test(TxtPassStr)==false)
                   {
                    alert("Error: password must contain at least one number (0-9)!");
                    document.getElementById('<%=txtpassword.ClientID%>').value="";
                          
                    return false;
                    }
  
                    re = /[a-z]/;
                  if(re.test(TxtPassStr)==false)
                   {
                    alert("Error: password must contain at least one lowercase letter (a-z)!");
                    document.getElementById('<%=txtpassword.ClientID%>').value="";
                                    
                    return false;
                    }
                   
                   re = /[A-Z]/;
                if(re.test(TxtPassStr)==false)
                 {
                    alert("Error: password must contain at least one uppercase letter (A-Z)!");
                    document.getElementById('<%=txtpassword.ClientID%>').value="";
                               
                    return false;
                } 
                
          }  
   
}
</script>

<script language="javascript" type="text/javascript">
var popupWindow = null;
function centeredPopup(url,winName,w,h,scroll)
{
LeftPosition = (screen.width) ? (screen.width-w)/2 : 0;
TopPosition = (screen.height) ? (screen.height-h)/2 : 0;
settings ='height='+h+',width='+w+',top='+TopPosition+',left='+LeftPosition+',scrollbars='+scroll+',resizable'
popupWindow = window.open(url,winName,settings)
popupWindow.focus();
}
</script>
<script language="javascript" type="text/javascript">

function centered_Popup(pageURL, title, popupWidth, popupHeight) 
    {
       
        var left = (screen.width / 2) - (popupWidth / 2);
        var top = (screen.height / 2) - (popupHeight / 2);
        window.showModalDialog(pageURL, 'dialog', 'dialogwidth:500px;dialogheight:200px;');        
    }
</script>
<script language="javascript" type="text/javascript">
        function searchKeyPress(e)
        {
        
                // look for window.event in case event isn't passed in
                if (window.event) { e = window.event; }
                if (e.keyCode == 13)
                {
                        document.getElementById('btnrsubmit').click();
                }
        }
</script>



    <title>DSSSBOnline</title>  
</head>
<body onkeypress="return searchKeyPress(event);">  
    <form id="form1" runat="server" AUTOCOMPLETE="OFF">   
    <div>
   
        
    <table width="900" align="center" class="border_gray">
    <tr>
    <td>
     <uc1:top ID="Top1" runat="server" />

    </td>
    </tr>

    <tr id="trnewreg" runat="server" visible="true">
     <td>
     <%--<asp:Panel ID="panlentry" runat="server" Width="100%" Enabled="false">--%>

    <table width="100%" class="border_gray">
    <tr>
    <td align="right" colspan="4">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Back</asp:HyperLink>
        <asp:Button ID="btn_back" runat="server" OnClick="btn_back_Click" Text="Back" Visible="False" />&nbsp;</td >
    </tr>
        <tr class="formlabel">
            <td align="left" colspan="4">
              
               <asp:Label ID="Label3" runat="server" CssClass="formlabel" Text="Instructions:" Width="84px" Font-Bold="True"></asp:Label>
               <br />
                 <span style="color: red">1 :</span>The fields with <span style="color: red">*</span> mark are mandatory.&nbsp;<br />
                  
                  <span style="color: red">2 :</span><asp:Label ID="Label6" runat="server" CssClass="formlabel" Text="Candidate can apply for various posts only after registration. Aftrer registration, candidate is required to the quote regn. no. and his/her password for further accessing the online system." ></asp:Label>
                 </td>
                  
                 
        </tr>
        
        <tr css="tr" >
            <td colspan="4" class="tr" align="center">
              Registration Details
        
            </td>
        </tr>
        <tr class="darkblue" visible="false" runat="server" id="tr_a">
            <td align="center" colspan="4" style="height: 22px; width:950px" >                
                <asp:CheckBox ID="chk_decl" runat="server" CssClass="formlabel" /><asp:Label ID="lbl_note" runat="server" CssClass="ariallightgrey" Text="I certify that I have not yet applied for any Post online in DSSSB and this is my first Registration."
                    Width="65%" ForeColor="#C00000"></asp:Label>
                    </td>
        </tr>
        <tr align="left" class="darkblue">
        <td style="width: 131px">
          <span style="color: red">*</span><asp:Label ID="Label13" runat="server" CssClass="Label13" Text="Name"></asp:Label>
          </td>
            <td colspan="3">
                <asp:TextBox ID="txt_name" runat="server" Width="24%" MaxLength="50"></asp:TextBox>
                <asp:Label ID="Label20" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                    Text="(Name written as in educational certificates. Don't use Mr.,Ms.,Dr. etc.)"></asp:Label>
          
              <asp:RequiredFieldValidator ID="rfvname" runat="server" Display="none" ControlToValidate="txt_name"
            ValidationGroup="1" ErrorMessage="Please Enter Name" Width="11px"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="revnm" runat="server" Display="None" ControlToValidate="txt_name"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in Name"
                    ValidationGroup="1" Width="51px"></asp:RegularExpressionValidator></td>
        </tr>
       
        <tr align="left" class="darkblue">
            <td style="width: 131px"  >
               <span style="color: red">*</span><asp:Label ID="Label15" runat="server" Text="Father's Name" 
                    CssClass="Label13"></asp:Label>
            </td>
            <td style="width: 257px" >
                <asp:TextBox ID="txt_fh_name" runat="server" Width="70%" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="rfvfname" runat="server" Display="none" ControlToValidate="txt_fh_name"
                    ValidationGroup="1" ErrorMessage="Please Enter Father Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revfname" runat="server" Display="None" ControlToValidate="txt_fh_name"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in FatherName or"
                    ValidationGroup="1"></asp:RegularExpressionValidator></td>
            <td align="left" style="width: 128px">
               <span style="color: red;vertical-align:top">*</span><asp:Label ID="Label16" runat="server" Text="Mother's Name" 
                    CssClass="Label13" Width="61%"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_mothername" runat="server" Width="70%" MaxLength="50"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvmname" runat="server" Display="none" ControlToValidate="txt_mothername"
            ValidationGroup="1" ErrorMessage="Please Enter Mother Name"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revmname" runat="server" Display="None" ControlToValidate="txt_mothername"
                    ValidationExpression="^[a-zA-Z.\s]{1,50}$" ErrorMessage="Invalid characters in MotherName"
                    ValidationGroup="1"></asp:RegularExpressionValidator></td>
        </tr>
        
        <tr align="left" class="darkblue" >
            <td align="left" style="width: 131px" >
               <span style="color: red">*</span><asp:Label ID="Label27" runat="server" CssClass="Label13" Text="Gender"></asp:Label>
            </td>
            <td align="left" style="width: 257px" >
                <asp:RadioButtonList ID="RadioButtonList_mf" runat="server"  
                    RepeatDirection="Horizontal" Height="16px" CssClass="ariallightgrey"> 
                <asp:ListItem Text="Male" Value="M"></asp:ListItem>               
                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                </asp:RadioButtonList>
                 <asp:RequiredFieldValidator ID="rfvgen" runat="server" Display="none" ControlToValidate="RadioButtonList_mf"
  ValidationGroup="1" ErrorMessage="Please Select Gender"></asp:RequiredFieldValidator>

            </td>
            <td >
               <span style="color: red; vertical-align:top">*</span>
               <asp:Label ID="Label29" runat="server" CssClass="Label13" Text="Date of Birth(As per 10th Certificate)" Width="124px"></asp:Label>
               </td>
            <td valign="middle">
                  <asp:TextBox ID="txt_DOB" runat="server" MaxLength="10" Width="95px" AutoPostBack="True" OnTextChanged="txt_DOB_TextChanged" ></asp:TextBox>(dd/MM/yyyy)
                    <%-- <img id="Img1" alt="DatePicker" onclick="PopupPicker('txt_DOB', 250, 250)" src="Images/calendar.bmp"
                      style="width: 25px; height: 25px" runat="server" />--%>
                      <asp:RequiredFieldValidator ID="rfvdob" runat="server" Display="none" ControlToValidate="txt_DOB"
                    ValidationGroup="1" ErrorMessage="Please Enter DOB.">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPIN" runat="server" ControlToValidate="txt_DOB"
                    ValidationExpression=".{10}.*" ErrorMessage="Enter Valid DOB(DD/MM/YYYY)" ValidationGroup="1" Display="None" > 
                    </asp:RegularExpressionValidator>
                 <asp:RegularExpressionValidator ID="REV_date" runat="server" ControlToValidate="txt_DOB"
                        Display="None" ErrorMessage="Enter Valid Date of Birth." ValidationExpression="(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d"                    
                        
                        ValidationGroup="1">
                        </asp:RegularExpressionValidator>
                        
                        </td>
        </tr>
        <tr align="left" class="darkblue">
            <td align="left" >
                <span style="color: #ff0000">*</span><asp:Label ID="Label8" runat="server" Text="Roll no. of 10th Exam" Width="142px"></asp:Label></td>
            <td align="left" style="width: 257px">
                <asp:TextBox ID="txt_roll_no" runat="server" MaxLength="15"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_roll_no" runat="server" ControlToValidate="txt_roll_no"
                    Display="none" ErrorMessage="Please Enter Xth Roll No." ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev_rollno" runat="server" ControlToValidate="txt_roll_no"
                        Display="None" ErrorMessage="Enter Valid Roll No." ValidationExpression="^[0-9]*$"
                        ValidationGroup="1">
                        </asp:RegularExpressionValidator>
                           <asp:RegularExpressionValidator ID="revroll_limit" runat="server" ControlToValidate="txt_roll_no"
                    ValidationExpression=".{3,15}.*" ErrorMessage="Minimum 3 digit and Maximum 15 digit are allowed in Roll No" Display="none" ValidationGroup="1">
                    </asp:RegularExpressionValidator>
                        
                        </td>
            <td><span style="color: red">*</span><asp:Label ID="Label9" runat="server" Text="10th Passing Year"></asp:Label>
               </td>
            <td valign="middle">
                <asp:DropDownList ID="ddl_pass_year" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_passyear" runat="server" ControlToValidate="ddl_pass_year"
                    Display="none" ErrorMessage="Please Select Xth Passing Year" ValidationGroup="1"></asp:RequiredFieldValidator></td>
        </tr>
        <tr align="left" class="darkblue">
            <td align="left" style="width: 131px">
                <span style="color: red">*</span>
                <asp:Label ID="Label26" CssClass="darkblue" runat="server" Text="Nationality" Width="75px" ></asp:Label></td>
            <td align="left" style="width: 257px">
                &nbsp;<asp:DropDownList ID="DDL_Nationality" runat="server" Height="18px" 
                    Width="88px">
                    <asp:ListItem Text="Indian" Value="Indian"></asp:ListItem>
                     <asp:ListItem Text="Other" Value="Other"></asp:ListItem>                    
                </asp:DropDownList></td>
            <td style="width: 128px">
                &nbsp;
           <asp:Label ID="Label4" runat="server" Text="UID Number" CssClass="Label13"></asp:Label></td>
            <td valign="middle">
                <asp:TextBox ID="txtuid" runat="server" Width="40%" MaxLength="12"></asp:TextBox>
                       <asp:Label ID="Label2" runat="server" CssClass="ariallightgrey" ForeColor="#C00000"
                           Text="(If obtained)"></asp:Label>
                     <asp:RegularExpressionValidator ID="revuid" runat="server" ControlToValidate="txtuid"
                        Display="None" ErrorMessage="Enter Valid UID No" ValidationExpression="^[0-9]*$"
                        ValidationGroup="1">
                        </asp:RegularExpressionValidator></td>
        </tr>
        <tr align="left" class="darkblue">
            <td >
               <span style="color: red">*</span> <asp:Label ID="Label23" runat="server" CssClass="Label13" Text="Mobile No."></asp:Label>
                <br />
               <asp:Label ID="Labelmsg" runat="server" ForeColor="#C00000"
                    Text="(10 Digits No without any 0,91 etc.)"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_mob" runat="server" Width="50%" MaxLength="10"></asp:TextBox>
                
                 <asp:RequiredFieldValidator ID="rfvmob" runat="server" Display="none" ControlToValidate="txt_mob"
            ValidationGroup="1" ErrorMessage="Please Enter Mobile No."></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="revmob" runat="server" Display="None" ControlToValidate="txt_mob"
                ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                    ValidationGroup="1">
                    </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="revnum" runat="server" ControlToValidate="txt_mob"
                    ValidationExpression=".{10}.*" ErrorMessage="Maximum 10 digit numbers are allowed." Display="none" ValidationGroup="1">
                    </asp:RegularExpressionValidator>
                    </td>
            <td align="left" style="width: 128px; height: 35px;">
                <span style="color: red">*</span><asp:Label ID="Label24" CssClass="Label13" runat="server" Text="Email"></asp:Label></td>
            <td style="height: 35px">
                <asp:TextBox ID="txt_email" runat="server" Width="90%"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvmail" runat="server" Display="none" ControlToValidate="txt_email"
            ValidationGroup="1" ErrorMessage="Please Enter EMail-Id"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revmail" runat="server" Display="None" ControlToValidate="txt_email"
  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Characters In EmailId"
  ValidationGroup="1"></asp:RegularExpressionValidator></td>
            
        </tr>
 <%--       <tr align="left" class="darkblue">
            <td style="height: 15px">
                <span style="color: #ff0000"><asp:Label ID="Label10" runat="server" CssClass="darkblue"
                    Text="Get your Security Code on your Mobile" Width="235px"></asp:Label></span></td>
            <td style="height: 15px">
                <asp:Button ID="Button1" runat="server" CssClass="cssbutton" OnClick="Button1_Click"
                    Text="Get code" />
                <asp:HiddenField ID="Hidden_SecCode" runat="server" />
            </td>
            <td style="height: 15px">
                <span style="color: #ff0000">*<asp:Label ID="Label11" runat="server" CssClass="darkblue"
                    Text="Enter Your Code"></asp:Label></span></td>
            <td style="height: 15px">
                <asp:TextBox ID="txtcode" runat="server" MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVCode" runat="server" ControlToValidate="txtcode"
                    Display="none" ErrorMessage="Please Enter Code" ValidationGroup="1"></asp:RequiredFieldValidator></td>
        </tr>--%>
        <tr align="left" class="darkblue">
            <td style="height: 21px; width: 131px;">
                <span style="color: #ff0000">*</span><asp:Label ID="Label1" runat="server" CssClass="Label13"
                    Text="Registration Password"></asp:Label></td>
            <td style="width: 257px; height: 21px;">
            <asp:TextBox ID="txtpassword" runat="server" MaxLength="16" TextMode="Password" AUTOCOMPLETE="OFF"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvp" runat="server" ControlToValidate="txtpassword"
                Display="None" ErrorMessage="Please Enter Password" ValidationGroup="1"></asp:RequiredFieldValidator>&nbsp;
                <asp:RegularExpressionValidator
                    ID="revpwd" runat="server" ControlToValidate="txtpassword" Display="None" ErrorMessage="Please Enter Valid Password."
                    ValidationExpression="[a-zA-Z0-9!$%^*@#&]{6,}$" ValidationGroup="1">
                            </asp:RegularExpressionValidator></td>
            <td align="left" style="width: 128px; height: 21px;">
                <span style="color: #ff0000">*
                    <asp:Label ID="Label7" runat="server" CssClass="darkblue" Text="Retype Password" ></asp:Label></span></td>
            <td style="height: 21px">
                <asp:TextBox ID="txt_re_password" runat="server" MaxLength="16" TextMode="Password" AUTOCOMPLETE="OFF"> 
                </asp:TextBox>
                <asp:CompareValidator ID="cfv_passwd" runat="server" ControlToCompare="txtpassword"
                    ControlToValidate="txt_re_password" Display="None" ErrorMessage="Password is not matching"
                    ValidationGroup="1"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="rfvrpass" runat="server" ControlToValidate="txt_re_password"
                    Display="None" ErrorMessage="Please Enter ReType  Password" ValidationGroup="1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rexppwd" runat="server" ControlToValidate="txt_re_password"
                                ValidationExpression="[a-zA-Z0-9!$%^*@#&]{6,}$" Display="None" ErrorMessage="Please Enter Valid Re-Password."
                                ValidationGroup="1">
                            </asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="height: 26px">
            <span style="COLOR: #c00000; HEIGHT: 21px"><strong>
                [Password must contain at least eight characters including one uppercase(A-Z), one
                lowercase(a-z), one digit(0-9), one special character [!$%^*@#&]</strong></span></td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                <asp:Label ID="Label12" runat="server" Text="Undertaking : I know the above details are correct and if wrong, Board's decision will be final and binding on me." CssClass="ariallightgrey" ForeColor="#C00000"></asp:Label>
                <asp:CheckBox ID="CheckBoxdisclaimer" runat="server" EnableTheming="True" CssClass="formlabel" />
            </td>
            
        </tr>
        <tr align="left" class="darkblue">
            <td colspan="4" style="color: #c00000; height: 21px">
                </td>
        </tr>
        
       
        <tr>
     
        <td colspan="4" align="center">
            <asp:Button ID="btnrsubmit" runat="server" CssClass="cssbutton" OnClick="btnrsubmit_Click" OnClientClick="return SignValidate();"
                Text="Submit" Width="91px" ValidationGroup="1"/>
                   <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="1" />
                </td>
               
        </tr>
        <tr>
            <td align="center" colspan="4">
            <div id="jsEnabled" style="visibility: hidden">
             JavaScript is enabled :Yor can register.
         </div>
         <div id="jsDisabled" style="visibility: visible">
            <span style="color: red; font-size:xx-large">JavaScript is disabled : Please enable it in Browser settings to get register.</span>
         </div>
            </td>
        </tr>
      
    </table>
    
    <%--</asp:Panel>--%>
         
    </td>

    
    </tr>
    <tr>
    <td>
    <%--<asp:Panel ID="pnlscript" runat="server" Width="100%">--%>
    </td>
    </tr>
    <tr id="trreg" runat="server" visible="false" style="height:400px">
    <td align="center" valign="middle">
    <table>
    <tr><td align="center" style="font-size:18pt; font-weight:bold; text-decoration:underline;"><asp:Label ID="lbl" runat="server" Font-Bold="true" ForeColor="DarkBlue"></asp:Label></td></tr>
    <tr><td align="center" style="font-size:18pt;"><asp:Label ID="lblmsg" runat="server" CssClass="cssbutton" Font-Size="X-Large" Visible="False"></asp:Label></td></tr>
        <tr>
            <td align="center" style="font-size: 18pt">
                <table>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lbl_r1" runat="server" CssClass="cssbutton" Font-Size="X-Large"></asp:Label></td>
                        <td style="width: 125px">
                            <asp:Label ID="lbl_r2" runat="server" CssClass="cssbutton" Font-Size="X-Large"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="lbl_r3" runat="server" CssClass="cssbutton" Font-Size="X-Large"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="DarkRed">(DOB)</asp:Label></td>
                        <td style="width: 125px">
                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="DarkRed">(Roll No.)</asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="DarkRed">(Year)</asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    <tr><td align="center" style="font-size:18pt;"><asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="DarkBlue" Visible="False"></asp:Label></td></tr>
    <tr><td align="center" style="font-size:18pt;"><asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="DarkRed" Visible="False"></asp:Label></td></tr>
    <tr><td></td></tr>
    <tr>
    <td align="center">
    <asp:Button runat="server" ID="btnprceed" Text="Proceed" CssClass="cssbutton" Width="80px" Font-Bold="true" OnClick="btnprceed_Click" />
    </td>
    </tr>
    </table>
    
    </td>
    </tr>
    </table>
    
     <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
        </div>
    </form>
</body>
</html>
