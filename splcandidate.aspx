<%@ Page Language="C#" AutoEventWireup="true" CodeFile="splcandidate.aspx.cs" Inherits="splcandidate" %>

<%@ Register Src="~/usercontrols/Header.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/Footer.ascx" TagName="WebUserControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="./CSS/Applicant.css" />

    <script type="text/javascript" language="javascript" src="Jscript/JScript.js">
    </script>

    <script type="text/javascript" language="javascript" src="Jscript/md5.js">
    </script>

    <%--<script language="javascript" type="text/javascript"> 
function checkJavaScriptValidity()
{ 
document.getElementById("jsEnabled").style.visibility = 'hidden'; 
document.getElementById("jsDisabled").style.visibility = 'hidden';

document.getElementById('<%= btnrsubmit.ClientID %>').disabled=false;
} 
</script>--%>
    <%--<script language="javascript" type="text/javascript">
var popupWindow = null;
function centeredPopup(url,winName,w,h,scroll)
{
LeftPosition = (screen.width) ? (screen.width-w)/2 : 0;
TopPosition = (screen.height) ? (screen.height-h)/2 : 0;
settings ='height='+h+',width='+w+',top='+TopPosition+',left='+LeftPosition+',scrollbars='+scroll+',resizable'
popupWindow = window.open(url,winName,settings)
popupWindow.focus();
}
</script>--%>
    <%--<script language="javascript" type="text/javascript">

function centered_Popup(pageURL, title, popupWidth, popupHeight) 
    {
       
        var left = (screen.width / 2) - (popupWidth / 2);
        var top = (screen.height / 2) - (popupHeight / 2);
        window.showModalDialog(pageURL, 'dialog', 'dialogwidth:500px;dialogheight:200px;');        
    }
</script>--%>
    <%--<script language="javascript" type="text/javascript">
        function searchKeyPress(e)
        {
        
                // look for window.event in case event isn't passed in
                if (window.event) { e = window.event; }
                if (e.keyCode == 13)
                {
                        document.getElementById('btnrsubmit').click();
                }
        }
</script>--%>
    <title>DSSSBOnline</title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="OFF">
    <div>
        <table width="100%" align="center" class="border_gray">
            <tr>
                <td>
                    <uc1:WebUserControl ID="Top1" runat="server" />
                </td>
            </tr>
            <tr id="trnewreg" runat="server" visible="true">
                <td align="center">
                    <%--<asp:Panel ID="panlentry" runat="server" Width="100%" Enabled="false">--%>
                    <table width="950" class="border_gray">
                        <tr>
                            <td align="right" colspan="2">
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Back</asp:HyperLink>
                                <%-- <asp:Button ID="btn_back" runat="server" OnClick="btn_back_Click" Text="Back" Visible="False" />--%>&nbsp;
                            </td>
                        </tr>
                         <tr css="tr">
                            <td colspan="2" class="tr" align="center">
                                 Registration for Special Case
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table align="center" width="70%" class="border_gray" id="tbl1" runat="server">
                                    <tr class="formlabel">
                                        <td align="right" colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr class="formlabel">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label1" runat="server" Text="Enter Your Application Id :"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtserialno" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txtserialno" runat="server" ControlToValidate="txtserialno"
                                                Display="none" ErrorMessage="Please Enter ID No." ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rev_txtserialno" runat="server" ControlToValidate="txtserialno"
                                                Display="None" ErrorMessage="Enter Valid ID No." ValidationExpression="^[0-9,A-Z]*$"
                                                ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr class="formlabel">
                                        <td align="right">
                                            <span style="color: red">*</span><asp:Label ID="Label2" runat="server" Text="Date of Birth(dd/mm/yyyy) :"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtdob" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvdob" runat="server" Display="none" ControlToValidate="txtdob"
                                                ValidationGroup="1" ErrorMessage="Please Enter DOB.">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPIN" runat="server"
                                                ControlToValidate="txtdob" ValidationExpression=".{10}.*" ErrorMessage="Enter Valid DOB(DD/MM/YYYY)"
                                                ValidationGroup="1" Display="None"> 
                                            </asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="REV_date" runat="server" ControlToValidate="txtdob"
                                                Display="None" ErrorMessage="Enter Valid Date of Birth." ValidationExpression="(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d"
                                                ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr class="formlabel">
                                        <td colspan="2" align="center">
                                                
                                              <%-- Commented by AnkitaSingh Dated:11-08-2022
                                                <asp:Label Text="How to Fill" runat="server" ID="lbl_fill" 
                                                Font-Size="Large" ForeColor="Maroon"></asp:Label><br/>
                                                <asp:HyperLink Text="(Hindi)  " runat="server" id="hpr_hindi" NavigateUrl="~/AdvtDetailFiles/doc_dsssb_hindi.pdf" Target="_blank"></asp:HyperLink>
                                                 <asp:HyperLink ID="hpr_eng" Text="(English)" runat="server" NavigateUrl="~/AdvtDetailFiles/doc_dsssb_english.pdf" Target="_blank"></asp:HyperLink>
                                                  <br/><br/>--%>
                                            <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                ValidationGroup="1" />
                                        </td>
                                    </tr>
                                    <%--Added by AnkitaSingh for 90/09 consent Dated:11-08-2022--%>
                                    <tr class="formlabel">
                                        <td colspan="2" align="center">
                                            <asp:Panel ID="PanelConsent" runat="server">
                                                <asp:CheckBox ID="ChkConsent" runat="server" Text="Undertaking to fill the online form to appear for the proposed online exam of Grade-II, 
                                                    DASS for the post code 90/09 to be conducted by the DSSSB. " forecolor="Red" AutoPostBack="True" OnCheckedChanged="ChkConsent_CheckedChanged" />
                                                <br /><br />
                                                <b>NOTE:</b> In Case no response given by the applicant, then the Board would construe it as applicant is not interested 
                                                to apply again for the post code 90/09 and he/she will not get further opportunity in this regard and Decision of board would
                                                be final decision regarding the same.<br /><br />
                                                <asp:Button ID="btnrsubmit" runat="server" CssClass="cssbutton" OnClick="btnrsubmit_Click" Text="Submit" ValidationGroup="1" Width="91px" />
                                                <br /> 
                                                &nbsp; &nbsp; 
                                                </asp:Panel>
                                        </td>
                                    </tr>
                                    
                                    <%--<tr class="formlabel"> Commented by AnkitaSingh on Dated: 20-11-2023 for 90/09
                                        <td colspan="2" align="center">
                                            <asp:HyperLink Text="Performa for Correction" Visible="false" runat="server" id="hpr_correct" NavigateUrl="~/AdvtDetailFiles/app.pdf" Target="_blank"></asp:HyperLink></td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="border_gray" width="80%" align="center" id="tblshow" runat="server"
                                    visible="false">
                                    <tr>
                                        <td class="formlabel" align="left">
                                            ID No :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblcandsrno" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="formlabel" align="left">
                                            Date of Birth :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblcanddob" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="formlabel" align="left">
                                            Name :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblname" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="formlabel" align="left">
                                            Father Name :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblcandfname" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="formlabel" align="left">
                                            Category :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblcat" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="formlabel" align="left">
                                            PostCode :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblpost" runat="server"></asp:Label>
                                            <asp:Label ID="lblpostname" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr><td colspan="2"><table id="tblalredymapped" runat="server" visible="false" class="border_gray" width="100%">
                                     <tr>
                                        <td  colspan="2" align="left">
                                            Yor are already allowed. Please use your registration no. and password to apply for the post.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="formlabel" align="left">Registration No :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblregno" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="btnproceed" runat="server" CssClass="cssbutton"
                                                Text="Proceed" Width="91px" onclick="btnproceed_Click"  />
                                            
                                        </td>
                                    </tr>
                                    </table></td></tr>
                                    <tr id="trreg" runat="server">
                                        <td class="border_gray" colspan="2" align="left">
                                            To fill The Application for the post you have been permitted by the Board, you are required to
                                            register with OARS,<asp:LinkButton ID="lnknewreg" runat="server" Text="Register yourself for OARS"
                                                OnClick="lnknewreg_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
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
            <tr>
                <td>
                    <uc2:WebUserControl ID="Footer" runat="server" />
                </td>
            </tr>
        </table>
        <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
    </div>
    </form>
</body>
</html>
