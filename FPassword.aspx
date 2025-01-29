<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPassword.aspx.cs" Inherits="FPassword" Title="Forget Password" %>
<%@ Register Src="~/usercontrols/MainHeader.ascx" TagName="top" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv='cache-control' content='no-cache'/>
<meta http-equiv='expires' content='0'/>
<meta http-equiv='pragma' content='no-cache'/>

<link href="CSS/Applicant.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="Jscript/JScript.js">
</script>
<script type="text/javascript" language="javascript" src="Jscript/md5.js">
</script>
    
<script language="javascript" type="text/javascript">
    
       function SignValidate()
       { 
      
//         if (!Page_ClientValidate()) 
//            {
//          
//            return false;           
//            }   
     
          var validated = Page_ClientValidate('1');
 
           //if it is valid
           if (validated)
           {
              //valid the main group
              validated = Page_ClientValidate('2');
           } 
           
            if (validated)
           {
              //valid the main group
              validated = Page_ClientValidate('3');
           }
           
 
           //remove the flag to block the submit if it was raised
           Page_BlockSubmit = false;
 
           //return the results         
            return validated; 
               
        }  
        
        function md5()
        {
          var npwd=document.getElementById('<%= txtpassword.ClientID %>').value;         
          var npwdhash = MD5(npwd);      
          document.getElementById('<%= txtpassword.ClientID %>').value=npwdhash; 
          
          var re_npwd=document.getElementById('<%= txt_re_password.ClientID %>').value;         
          var re_npwdhash = MD5(re_npwd);      
          document.getElementById('<%= txt_re_password.ClientID %>').value=re_npwdhash;
        }    
          
    </script>
<script type="text/javascript" language="javascript">
function PassValidate()
{ 
     
        var exppwd =/[\^|\*|\@|\~|\!|\#|\(|\)|\{|\}|=|\[|\]|\:|\;|\.|\'|\?|\/|\$|\_|\,|\-|\+|\%]+/;
        var expMassage = '[!$%^*@#&]';
        var flag=false;
   
        
        var TxtPassStr= document.getElementById('<%=txtpassword.ClientID%>').value
        if(TxtPassStr.length<6)
        {
            alert("Password should be at least 6 characters long.\nPlease re-enter the Password")
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

<script type="text/javascript" language="javascript">
        function call() 
        {
            //Page_ClientValidate();
             var validated = Page_ClientValidate('1');
 
           //if it is valid
           if (validated)
           {
              //valid the main group
              validated = Page_ClientValidate('2');
           }
            if (validated)
           {
              //valid the main group
              validated = Page_ClientValidate('3');
           }
           //remove the flag to block the submit if it was raised
           Page_BlockSubmit = false;
         
           //return the results
           return validated;
                }
        </script>
 
<script type="text/javascript" language="javascript">
/*
Auto tabbing script http://codingcluster.blogspot.in/
*/
function autoTab(current,next){
if (current.getAttribute&&current.value.length==current.getAttribute("maxlength"))
next.focus()
}

</script>
<script language="javascript" type="text/javascript">
        function searchKeyPress(e)
        {
        
                // look for window.event in case event isn't passed in
                if (window.event) { e = window.event; }
                if (e.keyCode == 13)
                {
                        document.getElementById('ButtonResetPass').click();
                }
        }
</script>
<script type="text/javascript">
    if (top != self) {
        top.location.replace(location);
    }
    function noBack() { window.history.forward(); }
    noBack();
    //window.onload=noBack;
    window.onpageshow = function (evt) { if (evt.persisted) noBack() }
    window.onunload = function () { void (0) }
</script>
<title>Forget Password
</title>
</head>

<body title="Forget Password" onkeypress="return searchKeyPress(event);">  
    <form id="form1" runat="server" >   
    <div>

<table border="1" align="center"><tr><td>
<table border="0" align="center" >

 <tr>
    <td colspan ="2">
     <uc1:top ID="Top1" runat="server" />
        
    </tr>
    <tr class="darkblue">
        <td align="right" colspan="2" style="height: 21px">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="default.aspx">Back</asp:HyperLink>
        <asp:Button ID="btn_back" runat="server" OnClick="btn_back_Click" Text="Back" Visible="False" />
        </td>
    </tr>
    <tr class="darkblue">
        <td align="center" colspan="2">
            <asp:Label ID="Label2" runat="server" Text="Forget Password" Font-Size="13pt" Font-Bold="True" Width="220px" ></asp:Label>
            <br />
        </td>
    </tr>
    
<tr class="darkblue">
<td>
    <asp:Label ID="Label1" runat="server" Text="Enter Your Registration Number">
    </asp:Label>
</td>
<td id="TDTextbox" runat="server" style="width:800px;">
        
      <asp:TextBox ID="txt_dd" runat="server" Width="21px" ViewStateMode="Disabled" AutoCompleteType="Disabled" MaxLength="2" ValidationGroup="1" onkeyup="autoTab(this, document.form1.txt_mm)" onfocus="javascript:this.value=''">
      </asp:TextBox>

<asp:TextBox ID="txt_mm" runat="server" Width="20px" MaxLength="2" ViewStateMode="Disabled" AutoCompleteType="Disabled" ValidationGroup="1" onkeyup="autoTab(this, document.form1.txt_yyyy)" onfocus="javascript:this.value=''">
</asp:TextBox>

<asp:TextBox ID="txt_yyyy" runat="server" MaxLength="4" ViewStateMode="Disabled" AutoCompleteType="Disabled"   ValidationGroup="1" Width="44px"  onkeyup="autoTab(this, document.form1.txt_regno)" onfocus="javascript:this.value=''">
</asp:TextBox>

<asp:TextBox ID="txt_regno" runat="server" ValidationGroup="1" ViewStateMode="Disabled" AutoCompleteType="Disabled" MaxLength="15" Width="125px" onfocus="javascript:this.value=''"></asp:TextBox>

<asp:DropDownList ID="DropDownList_year" runat="server" ValidationGroup="1">
</asp:DropDownList><br />
                    &nbsp;DD &nbsp;MM &nbsp; YYYY &nbsp;&nbsp; Roll No.(10th) &nbsp; &nbsp; Passing Year<br />
                    &nbsp;(Date of Birth) 
        </td>       
        </tr>
        
<tr class="darkblue">
<td>
    <asp:Label ID="lbl_mobile" runat="server" Text="Enter Your Mobile Number"></asp:Label>
</td>
<td id="TDTextbox_mob" runat="server" style="width:800px;">
        
      <asp:TextBox ID="txt_mobile" runat="server" MaxLength="10"></asp:TextBox>
        </td>       
        </tr>
        
<tr class="darkblue">
<td>
    <asp:Label ID="lbl_email" runat="server" Text="Enter Your Email Address"></asp:Label>
</td>
<td id="TDTextbox_email" runat="server" style="width:800px;">
        
      <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
        </td>       
        </tr>
        
        <tr class="darkblue">

 <td colspan="2" align="center" >
 

<asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_dd" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="revDD" runat="server" ControlToValidate="txt_dd"
         Display="None" ErrorMessage="Enter 2 values in DD" ValidationExpression=".{2}.*"
         ValidationGroup="1"></asp:RegularExpressionValidator>
     <asp:RegularExpressionValidator ID="REVDD1" runat="server" ControlToValidate="txt_dd"
         Display="None" ErrorMessage="Enter only Integer Values in DD" ValidationExpression="^[0-9]*$"
         ValidationGroup="1"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RFV2" runat="server" ErrorMessage="Enter Month" ControlToValidate="txt_mm" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="REVMM" runat="server" ControlToValidate="txt_mm"
         Display="None" ErrorMessage="Enter 2 values in MM" ValidationExpression=".{2}.*"
         ValidationGroup="1"></asp:RegularExpressionValidator>
     <asp:RegularExpressionValidator ID="REVmm1" runat="server" ControlToValidate="txt_mm"
         Display="None" ErrorMessage="Enter only Integer values in MM" ValidationExpression="^[0-9]*$"
         ValidationGroup="1"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RFV3" runat="server" ErrorMessage="Enter Year of Birth" ControlToValidate="txt_yyyy" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="revyy" runat="server" ControlToValidate="txt_yyyy"
         Display="None" ErrorMessage="Enter 4 values in YYYY" ValidationExpression=".{4}.*"
         ValidationGroup="1"></asp:RegularExpressionValidator>
     <asp:RegularExpressionValidator ID="revyy1" runat="server" ControlToValidate="txt_yyyy"
         Display="None" ErrorMessage="Enter only Integer values in YYYY" ValidationExpression="^[0-9]*$"
         ValidationGroup="1"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RFV4" runat="server" ErrorMessage="Enter Roll Number" ControlToValidate="txt_regno" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RFV5" runat="server" ErrorMessage="Enter Passing Year" ControlToValidate="DropDownList_year" Display="None" ValidationGroup="1" InitialValue="-1"></asp:RequiredFieldValidator>&nbsp;

</td>
</tr>
<%--
<tr>
<td colspan="2" align="center">
    <asp:Button ID="Button1" runat="server" Text="Get Security Code on your Registered Mobile" CssClass="buttons" ValidationGroup="1" OnClick="Button1_Click" Width="267px" />
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="1" />
    
</td>
</tr>

<tr class="darkblue">
<td align="left" style="width: 165px">
 <asp:Label ID="Label3" runat="server" Text="Enter Security Code Recieved on your  Registered Mobile No." Width="432px"></asp:Label>
</td>
<td>
 <asp:TextBox ID="TextBoxSecurityCode" runat="server" ViewStateMode="Disabled" AutoCompleteType="Disabled" MaxLength="4" Width="40px" ValidationGroup="2" EnableViewState="False"></asp:TextBox>
    &nbsp;<span style="color: #c00000; height: 21px">[Security Code remains valid for the entire day.]</span><asp:RequiredFieldValidator ID="RFV_SecCode" runat="server" ErrorMessage="Enter Security Code" ControlToValidate="TextBoxSecurityCode" Display="None" ValidationGroup="2"></asp:RequiredFieldValidator>  
    <asp:RegularExpressionValidator ID="REV_SecuCode" runat="server" ControlToValidate="TextBoxSecurityCode"
        Display="None" ErrorMessage="Enter only Integer Values" ValidationExpression="^[0-9]{1,4}$"
        ValidationGroup="2"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="revlimit" runat="server" ControlToValidate="TextBoxSecurityCode"
        Display="None" ErrorMessage="Enter 4 digit Security Code Recieved on your  Registered Mobile No." ValidationExpression=".{4}.*"
        ValidationGroup="2"></asp:RegularExpressionValidator>
        
        </td>
</tr>--%>
<tr>
<td colspan="2" align="center">
    <asp:Button ID="ButtonConfirmCode" runat="server" Text="Reset Password" CssClass="buttons" OnClientClick="return call();" ValidationGroup="2" Width="103px" OnClick="ButtonConfirmCode_Click" />

<asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="2" ShowMessageBox="true"
ShowSummary="false" />
</td>
</tr>
<tr class="darkblue" align="left" id="TRPassword" runat="server">
<td style="width: 165px">
 <asp:Label ID="Label4" runat="server" Text="Enter New Password" Width="223px"></asp:Label>
</td>
<td>
  <asp:TextBox ID="txtpassword" runat="server" ViewStateMode="Disabled" AutoCompleteType="Disabled" ValidationGroup="3" TextMode="Password" AUTOCOMPLETE="OFF"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RFV_NewPass" runat="server" ControlToValidate="txtpassword"
        Display="None" ErrorMessage="Enter New Password" ValidationGroup="3"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
                    ID="revpwd" runat="server" ControlToValidate="txtpassword" Display="None" ErrorMessage="Please Enter Valid Password."
                    ValidationExpression="[a-zA-Z0-9!$%^*@#&]{6,}$" ValidationGroup="1">
                            </asp:RegularExpressionValidator>
        </td>
</tr>


<tr class="darkblue" align="left" id="TRconfirmPass" runat="server">
<td style="width: 165px">
 <asp:Label ID="Label5" runat="server" Text="Retype New Password" Width="219px"></asp:Label>
</td>
<td>
  <asp:TextBox ID="txt_re_password" runat="server" ViewStateMode="Disabled" AutoCompleteType="Disabled" ValidationGroup="3" TextMode="Password" AUTOCOMPLETE="OFF"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RFV_ConfmPassword" runat="server" ControlToValidate="txt_re_password"
        Display="None" ErrorMessage="Retype New Password" ValidationGroup="3">
        </asp:RequiredFieldValidator>  
        <asp:RegularExpressionValidator ID="rexppwd" runat="server" ControlToValidate="txt_re_password"
                                ValidationExpression="[a-zA-Z0-9!$%^*@#&]{6,}$" Display="None" ErrorMessage="Please Enter Valid Re-Password."
                                ValidationGroup="1">
                            </asp:RegularExpressionValidator>
                 <asp:CompareValidator ID="cfv_passwd" runat="server" ControlToCompare="txtpassword"
                    ControlToValidate="txt_re_password" Display="None" ErrorMessage="Password is not matching"
                    ValidationGroup="1">
                    </asp:CompareValidator>    
        </td>
    
</tr>
 <tr align="left">
            <td colspan="2" style="color: #c00000; height: 21px">
                [Password must contain at least eight characters including one uppercase(A-Z), one
                lowercase(a-z), one digit(0-9), one special character [!$%^*@#&]</td>
        </tr>

<tr runat="server" id="TRButton">
<td colspan="2" align="center">
   <asp:Button ID="ButtonResetPass" runat="server" Text="Submit" CssClass="buttons" OnClientClick="return (SignValidate() && md5());" OnClick="ButtonResetPass_Click" 
   ValidationGroup="3" Width="60px" CausesValidation="False" />
<asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="3" ShowMessageBox="true"
ShowSummary="false" />

</td>
</tr>

</table>
</td></tr></table>
<input id="csrftoken" name="csrftoken" runat="server" type="hidden" /></div>        
    </form>
</body>
</html>