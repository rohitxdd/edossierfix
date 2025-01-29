<%@ Control Language="C#" AutoEventWireup="true" CodeFile="challan.ascx.cs" Inherits="usercontrols_challan"%>
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />
<table width="90%" class="formlabel">
    <tr>
        <td style="width: 121px">
        <img src="../Images/dsssb.jpg"  id="dsssb" runat="server" alt="dsssb" />       
        </td>
        <td style="width: 234px">
        <img src="../Images/sbi2.jpg"  id="sbi" runat="server" style="width: 251px; height: 86px" alt="sbi" />
        </td>
    </tr>
    <tr>
    <td colspan ="2" align="center" style="font-size: 70%">CASH CAN BE TENDERED AT ANY SBI BRANCH</td>
    </tr>
       <tr>
    <td colspan ="2" align="left" style="font-size: 70%">Fee for the Post of 
        <asp:Label ID="lbljtitle" runat="server" Text="Label"></asp:Label>
    </td>
    </tr>    
</table>
<table  width="90%" border="2" style="border-collapse: separate;border-color:Black" class="formlabel">
    <tr>
        <td style="height: 26px">
       Candidate Name: 
        </td>
        <td style="height: 26px" ><asp:Label ID="lblnm" runat="server"></asp:Label></td>
    </tr>
        <tr>
        <td style="height: 26px">
       Post Code: 
        </td>
        <td style="height: 26px" ><asp:Label ID="lblapp_pcode" runat="server"></asp:Label></td>
    </tr>
     <tr>
        <td>
        Mobile No:
        </td>
        <td><asp:Label ID="lblmob" runat="server"></asp:Label></td>
    </tr>
     <tr>
        <td>
        Email Id: 
        </td>
        <td><asp:Label ID="lblmail" runat="server"></asp:Label></td>
    </tr>
       <tr>
        <td>
       Amount:
        </td>
        <td><asp:Label ID="lblamt" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>
       Total Amounts in Words::
        </td>
        <td><asp:Label ID="lbltamnt" runat="server"></asp:Label></td>
    </tr>
</table>
<table width="90%"   style="border-collapse: separate;border-color:Black" class="formlabel"><br /> <br />
    <tr>
        <td colspan="2" align="right" style="font-size:smaller; ">
            Signature of Depositor
         
        </td>
         </tr>
        <tr>
        <td colspan="2" align="left" style="font-size:smaller; ">
            
         Note : Challan can be deposited minimum one day after the generation of Challan and You may verify the receipt of your fees online after two working days of deposit of fees.
        </td>
         </tr>
        
        </table>
         

<table  width="90%" border="2" style="border-collapse: separate;border-color:Black" class="formlabel">
    <tr align="left">
        <td colspan="2"><b><U>
        For Bank Use Only : </U></b>
        
    </tr>
    <tr align="left">
        <td>
        Use CBS Screen No: 
        </td>
        <td>8888</td>
    </tr>
     <tr>
        <td >
        Fee Type:
        </td>
        <td >
            131</td>
    </tr>
     <tr>
        <td>
            Reference No</td>
        <td><asp:Label ID="lblref" runat="server"  ></asp:Label></td>
    </tr>
       <tr>
        <td >
       Date of Birth:
        </td>
        <td><asp:Label ID="lblbrth" runat="server" ></asp:Label></td>
    </tr>
           
    </table>    
    
<br />
<table  width="90%"  border="2" style="border-collapse: separate;border-color:Black" class="formlabel"><tr><td>Journal No.</td></tr></table>
<br />
<br />


<table width="90%" class="formlabel">
    
    

<tr>
    <td valign="top" align="left" style="font-size: small; width: 407px; height: 21px;" >
            Seal/Date<br />
        <br />
        <br />
    </td>
                <td valign="top" align="right" style="font-size:smaller; width: 407px; ">
                    Authorised  Signatory
                </td></tr>
                <tr><td colspan="2" style="font-size:x-small; height: 32px;">
                  <strong>Fee can only be deposited at the bank counter upto:<asp:Label ID="lbl_date"
                      runat="server"></asp:Label>
                      (during bank working hours).</strong></td></tr>
            
            <tr><td colspan="2" align="CENTER" style="text-decoration: underline;font-size:medium">
                <strong>IMPORTANT INSTRUCTIONS TO SBI BRANCHES</strong></td></tr>
              <tr><td colspan="2" style="font-size:x-small; height: 32px;">
                  <strong>1.Under no circumstances the branches should issue 
                     Draft/IOI/Banker cheque against the challan.</strong></td></tr>
                       <tr><td colspan="2" style="font-size:x-small; height: 32px;">
                  <strong>2.In case of any problem branch should immediately contact
the Host Branch (01187) on phone number 011-23766856
.</strong></td></tr>
                     <tr><td colspan="2" style="font-size:x-small; height: 32px;">
                  <strong>3.Branches should not refuse to accept the challan.</strong></td></tr>
                    <tr><td colspan="2" style="font-size:x-small; height: 32px;">
                  <strong>4.In Case data is not displayes in Screen no.8888,branches should run"Host Data Sync Update(Complete)"and then post the challan.</strong></td></tr>
                    <tr><td colspan="2" style="font-size:x-small; height: 32px;">
                  <strong>5.Please note to write the Journal Number in all the challans.</strong></td></tr>
                    <%--<tr><td colspan="2" style="font-size:x-small; height: 32px;">
                  <strong>6.Please feed the Application No.in REG ID/Ref No.column.</strong></td></tr>--%>
    <tr>
        <td colspan="2" align="CENTER" style="text-decoration: underline;font-size:medium">
                <strong>IMPORTANT INSTRUCTIONS TO CANDIDATES</strong></td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: x-small; height: 32px"><strong>1.You will receive a sms/email after confirmation of fee deposit received from SBI.</strong>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: x-small; height: 32px"><strong>2.Your online application stands received only with confirmation of fee deposit
            from SBI( Approximately within 48 hours of deposit in SBI)</strong>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: x-small; height: 32px">
            <strong>3.Please print the challan and deposit at any SBI branch.</strong></td>
    </tr>
</table>