<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="FeeVerification.aspx.cs" Inherits="FeeVerification" Title="Candidate Fee Verification" %>

<%@ Register Src="~/usercontrols/callletter.ascx" TagName="callletter" TagPrefix="call" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <table width="100%">
<tr align="center">
<td align="center">
<asp:Panel ID="pnlverify" runat="server" Width="100%" HorizontalAlign="Center">
<table width="100%" >
<tr id="TRHead" runat="server" align="center">
 <td align="center" style="height: 22px;" >
     &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
     &nbsp; &nbsp;&nbsp; &nbsp; 
     <table width="100%" id="tblconf" runat="server">
         <tr align="center">
             <td >
     <asp:Label ID="lbljob" runat="server" Text="Select Post Applied" Font-Bold="True" CssClass="formheading" Width="132px"></asp:Label>
           <asp:DropDownList ID="ddjob" runat="server" CssClass="ddl" Width="400px"  ></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rfvjob" runat="server"  ControlToValidate="ddjob"
             ErrorMessage="Please Select Post"></asp:RequiredFieldValidator></td>
         </tr>
         <tr align="center">
             <td align="center">
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
        Text="Submit" Width="54px" CssClass="cssbutton" />
             </td>
         </tr>
     </table>
     &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        </td>
        <td  align="left" style="height: 22px" >
            &nbsp;
        </td>
        
</tr>
<tr runat="server" id="TR1">
<td style="height: 22px" colspan="2">
    &nbsp;</td>
</tr>
    <tr align="center">
        <td colspan="2">
         <br />
          <br />
          
              <asp:Label ID="LabelNote" runat="server" Visible="False" CssClass="formheading" ForeColor="#C00000"></asp:Label><br />
            <asp:Label ID="lbl_status" runat="server" CssClass="formheading" Text="Status" Visible="False"></asp:Label><br />
            <table width="70%" id="tbl_confirm"  runat="server" class="formlabel" visible="false" border="1" cellpadding="1" cellspacing="1">
                <tr align="left">
                    <td style="width: 202px" >
                      Status of Application </td>
                    <td >
                        <asp:Label ID="lbl_confirm" runat="server" ></asp:Label> &nbsp;
                            <asp:Button ID="btnverifystatus" runat="server" OnClick="btnverifystatus_Click" Text="* Re-Verify Payment Status"
                                CssClass="cssbutton" ToolTip="If already paid,then Re-Verify Payment Status"
                                Width="190px" Visible="false" /><br />
                                 <asp:Label ID="lblverfy" Visible="false" runat="server" ForeColor="Red" Text="* Click to Re-Verify Payment Status,if already deducted from A/C"></asp:Label></td>
                </tr>
            </table>
            <table width="70%" id="tbl_status" runat="server" visible="false" class="formlabel" border="1" cellpadding="1" cellspacing="1">
                <tr align="left">
                    <td style="width: 202px" >
                        Education Upload</td>
                    <td>
                        <asp:Label ID="lbl_edu" runat="server" ></asp:Label></td>
                </tr>
                <tr align="left">
                    <td style="width: 202px" >
                        Experience Upload</td>
                    <td >
                        <asp:Label ID="lbl_exp" runat="server" ></asp:Label></td>
                </tr>
                <tr align="left">
                    <td style="width: 202px" >
                        Photo Upload</td>
                    <td >
                        <asp:Label ID="lbl_photo" runat="server" ></asp:Label></td>
                </tr>
                <tr align="left">
                    <td style="width: 202px; height: 20px;" >
                        Signature Upload</td>
                    <td style="height: 20px" >
                        <asp:Label ID="lbl_sign" runat="server" ></asp:Label></td>
                </tr>
            </table>
           <%-- <table width="70%" id="tbl_fee" runat="server" visible="false" class="formlabel" border="1" cellpadding="1" cellspacing="1">
                <tr align="left">
                    <td style="width: 203px">
                        Receipt of Fee at DSSSB</td>
                    <td>
                        <asp:Label ID="lbl_fee" runat="server" ></asp:Label>
                                </td>
                </tr>
            </table>--%>
            <table width="70%" id="tbl_exam" runat="server" visible="false" class="formlabel" border="1" cellpadding="1" cellspacing="1">
                
                <tr align="left">
                    <td style="width: 203px; height: 20px;">
                        Exam Scheduled</td>
                    <td style="height: 20px">
                        <asp:Label ID="lbl_exam" runat="server"></asp:Label></td>
                </tr>
                <tr align="left" id="tr_exam" runat="server">
                    <td style="width: 203px" >
                        Date of Exam</td>
                    <td >
                        <asp:Label ID="lbl_exam_date" runat="server"></asp:Label></td>
                </tr>
                <tr align="left">
                    <td style="width: 203px; height: 20px;" >
                        Admit Card prepared for Tier-1 Exam</td>
                    <td style="height: 20px" >
                        <asp:Label ID="lbl_admit_card" runat="server"></asp:Label></td>
                </tr>
                 <tr align="left" id="trbatchid" runat="server" visible="false">
                    <td style="width: 203px; height: 20px;" >
                        Batch ID for Tier-1 Exam</td>
                    <td style="height: 20px" >
                        <asp:Label ID="lblbatchid" runat="server"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Panel>
</td>
</tr>
<tr>
<td>

<asp:Panel ID="pnldata" runat="server" Width="100%" Visible="false">
<table width="100%">
<tr id="conmsg" runat="server" visible="false">
<td >
Receipt of your Fee at Board is confirmed.
    
</td>
</tr>
<tr id="penmsg" runat="server" visible="false">
<td>
Receipt of your Fee at Board is pending yet.The fee submission till date & time(___________) has been updated. If you have deposited the fee before the above date. You are requested to contact either on TollFree NO____________. or send E-Mail______________ to  alongwith a copy of the Challan.
</td>
</tr>
</table>
</asp:Panel>
</td>
</tr>


</table>
<input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
