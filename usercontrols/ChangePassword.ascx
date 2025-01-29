<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.ascx.cs" Inherits="UserControls_ChangePassword" %>
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />

 <script language="javascript" type="text/javascript" src="../JS/JScript.js">    
    </script>
<script language="javascript" type="text/javascript" src="../JS/md5.js"></script>


<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    
       function SignValidate()
       { 
         if (!Page_ClientValidate()) 
            {
            return false; 
            }
           
        var npwd=document.getElementById('<%= NewPassword.ClientID %>').value;
          var npwdhash = MD5(npwd);
          var cpwd=document.getElementById('<%= ConfirmNewPassword.ClientID %>').value;
          var cpwdhash = MD5(cpwd);
        
          document.getElementById('<%= NewPassword.ClientID %>').value=npwdhash; 
          document.getElementById('<%= ConfirmNewPassword.ClientID %>').value=cpwdhash;   
          
          var opwd=document.getElementById('<%= CurrentPassword.ClientID %>').value;
          var opwdhash = MD5(opwd);
          var rand =document.getElementById('<%= txtrandomno.ClientID %>').value;
          var saltedHash = MD5(opwdhash+rand);       
        document.getElementById('<%= CurrentPassword.ClientID %>').value=saltedHash;
        document.getElementById('<%= txtrandomno.ClientID %>').value=null;   
      
        }    
    </script>
    <script type="text/javascript" language="javascript">
function PassValidate()
{ 
     
        var exppwd =/[\^|\*|\@|\~|\!|\#|\(|\)|\{|\}|=|\[|\]|\:|\;|\.|\'|\?|\/|\$|\_|\,|\-|\+|\%]+/;
        var expMassage = '(\",%,;,:,~,!,#,^,{,},_,-,(,),*\)';
        var flag=false;
   
        
        var TxtPassStr= document.getElementById('<%=NewPassword.ClientID%>').value
        if(TxtPassStr.length<8)
        {
            alert("Password should be at least 6 characters long.\nPlease re-enter the Password")
            document.getElementById('<%=NewPassword.ClientID%>').value="";
            //document.getElementById('<%=NewPassword.ClientID%>').focus();
            return false;
        }
        if(TxtPassStr!="")
          {
                if(exppwd.test(TxtPassStr)==false)
                    {
                        alert("Atleast one of the Special characters like\n" +expMassage+ " \nmust be in the New Password.");
                        document.getElementById('<%=NewPassword.ClientID%>').value="";
                         return false;
                    }
                    
                  var re = /[0-9]/;
                    if(re.test(TxtPassStr)==false)
                   {
                    alert("Error: password must contain at least one number (0-9)!");
                    document.getElementById('<%=NewPassword.ClientID%>').value="";
                    //document.getElementById('<%=NewPassword.ClientID%>').focus();                    
                    return false;
                    }
  
                    re = /[a-z]/;
                  if(re.test(TxtPassStr)==false)
                   {
                    alert("Error: password must contain at least one lowercase letter (a-z)!");
                    document.getElementById('<%=NewPassword.ClientID%>').value="";
                    //document.getElementById('<%=NewPassword.ClientID%>').focus();                   
                    return false;
                    }
                   
                   re = /[A-Z]/;
                if(re.test(TxtPassStr)==false)
                 {
                    alert("Error: password must contain at least one uppercase letter (A-Z)!");
                    document.getElementById('<%=NewPassword.ClientID%>').value="";
                    //document.getElementById('<%=NewPassword.ClientID%>').focus();                    
                    return false;
                } 
                
          }  
   
}
</script>
<%--<script language="javascript" type="text/javascript">
        function searchKeyPress(e)
        {
        
                // look for window.event in case event isn't passed in
                if (window.event) { e = window.event; }
                if (e.keyCode == 13)
                { 
                        document.getElementById('ctl00$body$ChangePassword$ChangePasswordButton').click();
                }
        }
 </script>--%>

 <table border="0"  width="100%" class="formlabel" align="center">
                <tr class="darkblue">
                    <td >
                        <table border="1" id="TABLE1" class="darkblue" align="center">
                        
                              <tr id="trcurntpwd" runat="server">
                                <td align="left" style="height: 24px; width: 209px;">
                                   Old Password&nbsp;</td>
                                <td style="width: 300px; height: 24px;" align="left">
                                                    
                                    <asp:TextBox ID="CurrentPassword" MaxLength="16" runat="server" Font-Size="0.8em" TextMode="Password" AutoCompleteType="None" autocomplete="off" autofill="false" Width="150px" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" Display="None" ControlToValidate="CurrentPassword"
                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="1"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;
                                    <%--[a-zA-Z0-9@#&]*.{3,}--%></td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 25px; width: 209px;">
                                    &nbsp;New Password</td>
                                <td style="width: 300px; height: 25px" align="left">
                                    <asp:TextBox ID="NewPassword" MaxLength="16" runat="server" Font-Size="0.8em" AutoCompleteType="None" autocomplete="off" autofill="false" TextMode="Password" EnableViewState="True" style="left: 0px; position: relative; top: 0px" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                        ErrorMessage="New Password is required." Display ="none" ToolTip="New Password is required."
                                        ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revpwd" runat="server" ControlToValidate="NewPassword" ValidationExpression="^[a-zA-Z0-9!$%^*@#&]{6,}$" Display="None" ErrorMessage="New Password must be of atleast 6 characters Or Invalid characters." ValidationGroup="1">
                        </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 209px">
                                    Confirm New Password.</td>
                                <td style="width: 300px" align="left">
                                    <asp:TextBox ID="ConfirmNewPassword" MaxLength="16" runat="server" Font-Size="0.8em" autocomplete="off" TextMode="Password" AutoCompleteType="None" autofill="false" EnableViewState="True" style="left: 0px; position: relative; top: 0px"  Width="150px"></asp:TextBox>
                                    <asp:RegularExpressionValidator
                                        ID="refconpwd" runat="server" ControlToValidate="ConfirmNewPassword"
                                        Display="None" ErrorMessage="Confirm New Password must be of atleast 6 characters Or Invalid characters." ValidationExpression="^[a-zA-Z0-9!$%^*@#&]{6,}$"
                                        ValidationGroup="1"></asp:RegularExpressionValidator></td>
                                    <asp:RequiredFieldValidator ID="rfvpwd" runat="server" ControlToValidate="ConfirmNewPassword" Display="None" ErrorMessage="Please enter confirm password." ValidationGroup="1" /></tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                        ControlToValidate="ConfirmNewPassword" Display="none" ErrorMessage="Confirm Password must match the New Password"
                                        ValidationGroup="1" ></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 20px">
                                <asp:HiddenField ID="txtrandomno" runat="server" />
                                    &nbsp;
                                    </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 23px">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" Visible="False" Width="268px">Password changed Successfully</asp:Label></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color: red; height: 17px;">
                                    
                
                                       
                                        </td>
                                        
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                   <asp:Button ID="ChangePasswordButton" runat="server" BackColor="White" ForeColor="#284E98" Text="Change Password" ValidationGroup="1" OnClick="ChangePasswordButton_Click" OnClientClick="return SignValidate();" Width="125px"/>
                                </td>
                               <%-- <td>
                                    <asp:Button ID="CancelButton" runat="server" BackColor="White" CausesValidation="False" ForeColor="#284E98" Text="Cancel" OnClick="CancelButton_Click" /></td>--%>
                            </tr>
                            <tr>
                                <td align="right" colspan="2" style="height: 23px">
                                    <input id="csrftoken" runat="server" name="csrftoken" type="hidden" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="1" />
                                    
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
            </table>
