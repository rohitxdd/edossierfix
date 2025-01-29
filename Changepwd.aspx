<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="ChangePwd" Title="Untitled Page" %>
<%@ Register Src="~/usercontrols/ChangePassword.ascx" TagName="ChangePassword" TagPrefix="uc2" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" Runat="Server">

 <script language="javascript" type="text/javascript" src="Jscript/JScript.js">    
    </script>
<script language="javascript" type="text/javascript" src="Jscript/md5.js"></script>
    <script language="javascript" type="text/javascript" ></script>
    <script language="javascript" type="text/javascript">
        function searchKeyPress(e)
        { 
              
                // look for window.event in case event isn't passed in
                var keyPressed;
                if(window.event)
                keyPressed = window.event.keyCode;	// IE
                else
                keyPressed = e.which;	 

//               var e="";
//                if (window.event)
//                 { 
//                 e = window.event;
//                 }
//                 else
//                 {
//                 e=event.which;
//                 }
                 //alert(keyPressed);
                //alert(document.getElementById('ctl00$body$ChangePassword$ChangePasswordButton').id);
                if (keyPressed == 13)
                {              
                     //alert('hi');
                        var chpassword_id=document.getElementById('ctl00_body_ChangePassword_ChangePasswordButton').click();
//                        chpassword_id.click();
                }
        }  
        
 </script> 
 <%--<script type="text/javascript"> 
function stopRKey(evt) { 
  var evt = (evt) ? evt : ((event) ? event : null); 
  var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null); 
  if (evt.keyCode == 13)  {return false;} 
} 
document.onkeypress = stopRKey;
</script> --%>
 <script type="text/javascript">     
    function GoBack()    
    {
   // alert('goback');
    //window.history.go(+1);
    window.history.forward(1);
    
    } 
    GoBack();
    </script>
  
    <table width="100%">
    <tr>
            <td align ="center">
                <asp:Label ID="lbltitle" runat="server" CssClass="validatorstyles" Font-Bold="True"></asp:Label></td>
        </tr>
    <tr><td align="center" colspan="1">
   <uc2:ChangePassword id="ChangePassword" runat="server" OnLoad="ChangePassword_Load" />
   </td></tr>
   </table>

</asp:Content>


