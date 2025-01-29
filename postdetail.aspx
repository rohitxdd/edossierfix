<%@ Page Language="C#" AutoEventWireup="true" CodeFile="postdetail.aspx.cs" Inherits="postdetail"
    Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="./CSS/Applicant.css" />
    <title>DSSSBOnline</title>
</head>
<body>
    <form id="form1" runat="server">
        <div><center>
            <table  width="900px">
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4" class="headingstyle">
                        <asp:Label ID="lblPost" Text=" Recruitment Rules for Post: " runat="server" Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblJobName" runat="server" CssClass="formlabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <strong>Classification :</strong></td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblclas" runat="server" CssClass="formlabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <strong>Scale of Pay: </strong>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblpay" runat="server" CssClass="formlabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <strong>Qualification :</strong>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left" class="formlabel">
                        <strong>Essential Qualification:</strong></td>
                    <td align="left">
                        <asp:Label ID="lblessq" runat="server" CssClass="formlabel"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" class="formlabel">
                        <strong>Desirable Qualification:</strong></td>
                    <td align="left">
                        <asp:Label ID="lbldesiredq" runat="server" CssClass="formlabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <strong>Age (In years):</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="formlabel">
                        <strong>
                        Minimum Age:</strong></td>
                    <td align="left">
                        <asp:Label ID="lblminage" runat="server" CssClass="formlabel"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" class="formlabel">
                        <strong>
                        Maximum Age:</strong></td>
                    <td align="left">
                        <asp:Label ID="lblmaxage" runat="server" CssClass="formlabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left" >
                        <strong>Probation Period (In Years):</strong>
                    </td>
                
                    <td align="left" >
                        <asp:Label ID="lblprobper" runat="server" CssClass="formlabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <strong>Experience :</strong>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left" class="formlabel">
                        <strong>Essential Experience:</strong></td>
                    <td align="left">
                        <asp:Label ID="lblessnexperience" runat="server" CssClass="formlabel"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" class="formlabel">
                        <strong>Desirable Experience:</strong></td>
                    <td align="left">
                        <asp:Label ID="lbldesexpr" runat="server" CssClass="formlabel"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                    </td>
                </tr>
             <tr id="tr" runat="server" visible="false">
                    <td align="left">
                        Experience(In Years)</td>
                    <td>
                        <asp:Label ID="lblexpr" runat="server" CssClass="formlabel"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="headingstyle" align="left" colspan="2">
                        <strong>Fee for the Post :</strong>
                        <asp:Label ID="lblfee" runat="server" CssClass="formlabel"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="headingstyle" align="left" colspan="2">
                        <strong>Age Relaxation :</strong></td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:GridView ID="grdAgeRelax" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                            CssClass="formlabel" DataKeyNames="id,CatCode,CatIndCS,CM,Fee_exmp,CatCode1,jid">
                            <Columns>
                                <asp:TemplateField HeaderText="Sno">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Category /Special Category">
                                    <ItemTemplate>
                                        <%# Eval("CatIndCS")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                               <asp:TemplateField HeaderText="Category /Sub category">
                                    <ItemTemplate>
                                        <%# Eval("CatCode")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Relaxation">
                                    <ItemTemplate>
                                        <%# Eval("CM")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Whether Fee Exemption">
                                    <ItemTemplate>
                                        <%# Eval("Fee_exmp")%>
                                    </ItemTemplate> <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <HeaderStyle BackColor="LightSlateGray" HorizontalAlign="Left" />
                        </asp:GridView>
                    </td>
                </tr>
            </table></center>
            <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
        </div>
    </form>
</body>
</html>
