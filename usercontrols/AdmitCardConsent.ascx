<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdmitCardConsent.ascx.cs" Inherits="usercontrols_AdmitCardConsent" %>
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />

 <table style="text-align: left" width="90%"  runat="server" id="table">
        <tr runat="server" id="trddlexam" visible="true">
            <td align="left">
                <asp:Label ID="lblappno" runat="server" Text="Select Exam." CssClass="formheading"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlexam" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlexam_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
       
        <tr runat="server" id="trhead" visible="false">
            <td colspan="2" align="left">
                <table id="tbl1" width="90%" runat="server" visible="true" align="left">
                    <tr>
                        <td colspan="2" align="left" class="formheading">
                        <span style="color:Red;">  Instructions :</span>
                            
                     
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="formheading">

                     <span style="color:Red;">   1. Verification of Mobile No and email id are the 
                            Pre-Requisite for generating the Admit Card for the Post Code:<asp:Label runat="server" ID="lblpostcode" Text=""></asp:Label> &nbsp;to 
                            be Held on <asp:Label runat="server" ID="lblheldon" Text=""></asp:Label><br />2. After generation, your e Admit Card will be available for Download 
                            tentatively from  <asp:Label runat="server" ID="lblreleasedate" Text=""></asp:Label> <br />3. Please note that all Future Correspondance will be made at your email id and Mobile No only.</span>
                          <%--  <asp:Label ID="lblHead0" runat="server" 
                                Text="1. Verification of Mobile No and email id are the Pre-Requisite for generating the Admit Card. </br>2.After generation, your e Admit Card will be available for Download from </br>3.Please note that all Future Correspondance will be made at your email id and Mobile No only." CssClass="formheading"></asp:Label>--%>
                        </td>
                    </tr>
                     <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="Verify Your Mobile No. and email id" CssClass="formheading"></asp:Label>
                       </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" id="TRUC" visible="false" align="center">
            <td colspan="2">
                <div runat="server" id="div1">
                    <table id="td1"  runat="server" class="formheading" border="1" width="90%">
                        <tr>
                            <td align="left" style="width: 220px">
                                Name</td>
                            <td align="left">
                                <asp:Label ID="lblname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 220px">
                                Mobile No.</td>
                            <td align="left">
                <asp:TextBox ID="txt_mob" runat="server" Width="37%" Height="20px" MaxLength="10" CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvmob" runat="server" Display="none" ControlToValidate="txt_mob"
            ValidationGroup="1" ErrorMessage="Please Enter Mobile No."></asp:RequiredFieldValidator>

              <asp:RegularExpressionValidator ID="revmob" runat="server" Display="None" ControlToValidate="txt_mob"
                ValidationExpression="^[0-9]*$" ErrorMessage="Enter Only Numbers In Mobile No"
                    ValidationGroup="1">
                    </asp:RegularExpressionValidator>

                    <asp:RegularExpressionValidator ID="REVMobile" runat="server" ControlToValidate="txt_mob"
                    ValidationExpression=".{10}.*" ErrorMessage="Enter Minimum 10 Digit" Display="none" ValidationGroup="1"></asp:RegularExpressionValidator>
                 
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 220px;">
                                Email ID</td>
                            <td align="left">
                <asp:TextBox ID="txt_email" runat="server" Width="250px" Height="20px"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvmail" runat="server" Display="none" ControlToValidate="txt_email"
            ValidationGroup="1" ErrorMessage="Please Enter EMail-Id"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="revmail" runat="server" Display="None" ControlToValidate="txt_email"
  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Characters In EmailId"
  ValidationGroup="1"></asp:RegularExpressionValidator>

                            </td>
                        </tr>
                         <tr>
                            <td align="left" colspan="2">
                             <asp:CheckBox ID="CheckBoxdisclaimer" runat="server" EnableTheming="True" CssClass="formlabel" />
                            <asp:Label ID="Label12" runat="server" Text="I Certify that I have Verified my Mobile No. and email id"
                                                CssClass="ariallightgrey" ForeColor="#C00000"></asp:Label>
                                           
                               </td>
                        </tr>

                        <tr>
                            <td align="right">
                                <asp:Button ID="btnsubmit" runat="server" onclick="btnsubmit_Click" CssClass="buttonFormLevel"
                                    Text="Submit to Generate Admit Card" ValidationGroup="1" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btncancel" runat="server" CssClass="buttonFormLevel"
                                    Text="Cancel" onclick="btncancel_Click" ValidationGroup="1" />
                            </td>
                        </tr>

                        <tr>
                            <td align="center" colspan="2">
                                 <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="1" />
                    <asp:HiddenField runat="server" ID="hfradmitcard" />
                      <asp:HiddenField runat="server" ID="hfradmitcardphase2" />
                        <asp:HiddenField runat="server" ID="hfacconsent" />
                          <asp:HiddenField runat="server" ID="hfacconsent_phase2" />
</td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>