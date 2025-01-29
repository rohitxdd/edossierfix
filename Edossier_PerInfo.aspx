<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Edossier_PerInfo.aspx.cs" Inherits="Edossier_PerInfo" %>


<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table style="width: 100%" id="tblpost" runat="server">
         <tr>
        <td align="left">
            <asp:Label ID="lblappno" runat="server" Text="Select Post " CssClass="formheading"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="DropDownList_post" runat="server" CssClass="ddl" Width="500px">
            </asp:DropDownList>
             
            
            </td>
    </tr>
        <tr >
            <td align="center" colspan="2">
                <asp:Button ID="Button_Vaidate" runat="server" Text="Submit" Width="131px" OnClick="Button_Vaidate_Click"
                    CssClass="cssbutton" />&nbsp;
                    <asp:Button Text="Replace Recalled Document" runat="server" ID="btnreplace" OnClick="btnreplace_Click" CssClass="cssbutton" />
                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Text="Nothing Pending"
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <table id="tbldata" runat="server" visible="false" style="width: 90%; height: 352px;
        border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
        border-bottom: blue thin solid;">
        <tr>
            <td align="left" colspan="2">
                <strong>Post:&nbsp;<asp:Label ID="lblpostcode" runat="server"></asp:Label>&nbsp;|&nbsp;Roll
                    No.:&nbsp;<asp:Label ID="lblrollno" runat="server"></asp:Label>
                </strong>
            </td>
        </tr>
         <tr>
            <td id="tredno" runat="server" visible="false" align="left">
                <asp:Label ID="lbledno" runat="server" CssClass="formheading" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="tr">
                <strong>Personal Information </strong>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblname" runat="server" Text="Name of Candidate" Font-Bold="True"
                    CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblname1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:Label ID="lblfat" runat="server" Text="Father's Name" Font-Bold="True" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblfat1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblmth" runat="server" Text="Mother's Name" Font-Bold="True" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblmth1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:Label ID="lblspouse" runat="server" Text="Spouse Name" Font-Bold="True" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtspouse" runat="server" CssClass="gridfont" Width="250px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblperadd" runat="server" Text="Permanent Address" Font-Bold="True"
                    CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblperadd1" runat="server" Width="94%" Height="40px" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lbladd" runat="server" Text="Correspondence Address" Font-Bold="True"
                    CssClass="formlabel"></asp:Label><span style="color:Red">#</span>
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtadd1" runat="server" Width="94%" Height="40px" CssClass="gridfont"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtadd1" runat="server" Display="None" ControlToValidate="txtadd1"
                    ErrorMessage="Please enter Correspondence Address" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblpincode" runat="server" Text="Pincode" Font-Bold="True" CssClass="formlabel"></asp:Label><span style="color:Red">#</span>
            </td>
            <td valign="top" align="left">
                <asp:TextBox ID="txtpincode1" runat="server" CssClass="gridfont"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtpincode1" runat="server" Display="None" ControlToValidate="txtpincode1"
                    ErrorMessage="Please enter Pincode" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:Label ID="lblmob" runat="server" Text="Mobile No." Font-Bold="True" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblmob1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:Label ID="lblemail" runat="server" Text="Email Id" Font-Bold="True" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblemail1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:Label ID="lblbrth" runat="server" Text="Date of Birth" Font-Bold="True" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblbrth1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
          <tr>
            <td align="left" valign="top">
                <asp:Label ID="lblage" runat="server" Font-Bold="True" CssClass="formlabel"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblage1" runat="server" CssClass="gridfont"></asp:Label>
            </td>
        </tr>
        <tr><td colspan="2" align="left" style="color:Red;"># fields are editable.</td></tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnsave" runat="server" Text="Save & Next>>" Width="131px" CssClass="cssbutton"
                    OnClick="btnsave_Click" ValidationGroup="1" />
                <asp:Button ID="btnnext" runat="server" Text="Next>>" Width="100px" CssClass="cssbutton"
                    Visible="false" onclick="btnnext_Click" />
                <asp:ValidationSummary ID="valsum" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="1" />
                <asp:HiddenField ID="hfjid" runat="server" />
                <asp:HiddenField ID="hfapplid" runat="server" />
                <asp:HiddenField ID="hfedid" runat="server" />
                 <asp:HiddenField ID="hfschedule" runat="server" />
                    <asp:HiddenField ID="hffinal" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
