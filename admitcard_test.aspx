<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admitcard_test.aspx.cs" Inherits="admitcard_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admit Card</title>
    <link href="App_Themes/MainStyles.css" rel="stylesheet" type="text/css" />
    <link href="CSS/dsssbstylesheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            height: 27px;
            color: Black;
        }
        .style5
        {
            width: 374px;
            color: Black;
            color: Black;
        }
        .style6
        {
            height: 27px;
            width: 374px;
            color: Black;
        }
        .style7
        {
            height: 120px;
            width: 374px;
            color: Black;
        }
        .style11
        {
            color: Black;
            font-weight: bold;
            font-size: 18px;
        }
        .style13
        {
            height: 120px;
            width: 428px;
            color: Black;
        }
        .style14
        {
            width: 100%;
            color: Black;
        }
        .style18
        {
            height: 121px;
            color: Black;
        }
        
        .style19
        {
            color: Black;
            font-weight: bold;
            font-size: 16px;
            width: 428px;
        }
        .style20
        {
            width: 428px;
        }
        .style20
        {
            width: 428px;
        }
        .style21
        {
            color: Black;
            font-weight: bold;
            font-size: 16px;
            width: 374px;
        }
        
        .break
        {
            page-break-before: always;
        }
    </style>
    <%--<style type="text/css">
..break { page-break-before: always; }
</style>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <table width="80%" border="1" cellpadding="1" cellspacing="1" style="border-left-color: #ffcccc;
                border-bottom-color: #ffcccc; border-top-style: solid; border-top-color: #ffcccc;
                border-right-style: solid; border-left-style: solid; border-right-color: #ffcccc;
                border-bottom-style: solid;">
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/dsssblogo.jpg" />
                                </td>
                                <td align="center" style="font-size: 18px; font-weight: bold; width: 80%">
                                    Goverment of NCT of Delhi
                                    <br />
                                    DELHI SUBORDINATE SERVICES SELECTION BOARD
                                    <br />
                                    FC-18,INSTITUTIONAL AREA,KARKARDOOMA,DELHI
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" class="style11">
                                    <asp:Label ID="lbl_sample" runat="server"></asp:Label>
                                    e - Admit Card
                                    <asp:Label ID="lbl_prov" runat="server" Text=" (Provisional) " Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" class="style11">
                                    <asp:Label ID="lblexamtype" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" class="style11">
                                    <asp:Label ID="lbloanunber" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="height: auto">
                    <td align="left" class="style19">
                        <strong>Post Name</strong> :
                        <asp:Label ID="lblpost" runat="server" Font-Bold="False" CssClass="style11"></asp:Label>
                    </td>
                    <td align="left" class="style21">
                        <strong>Post Code </strong>:&nbsp;
                        <asp:Label ID="lblpstcode" runat="server" Font-Bold="False"></asp:Label>
                    </td>
                    <td align="center" rowspan="2">
                        <asp:Image ID="img_photo" runat="server" Height="180px" Width="170px" />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="left" class="style20">
                        <strong class="style11">Exam Date & Time:</strong>&nbsp;&nbsp;<asp:Label ID="lbldate"
                            runat="server" Font-Bold="False" class="style11"></asp:Label><br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            ID="lbltm" runat="server" Font-Bold="False" class="style11"></asp:Label><br />
                    </td>
                    <td align="left" class="style21">
                        <strong>Roll No:&nbsp; </strong>
                        <asp:Label ID="lblrollno" runat="server" Font-Bold="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style19">
                        <strong>Candidate Name :</strong>
                        <asp:Label ID="lblname" runat="server" Font-Bold="False"></asp:Label>
                    </td>
                    <td align="left" class="style6">
                        <strong class="style11">Category : </strong>
                        <asp:Label ID="lblcategory" runat="server" Font-Bold="False" class="style11"></asp:Label><br />
                        <strong class="style11">
                            <asp:Label ID="lbl_scat" runat="server" Text="SubCategory : "></asp:Label>
                        </strong>
                        <asp:Label ID="lblsubcate" runat="server" Font-Bold="False" class="style11"></asp:Label><asp:Label
                            ID="lblphsubcat" runat="server" Font-Bold="False" class="style11"></asp:Label>
                    </td>
                    <td align="center" class="style3" rowspan="2">
                        <%--<strong style="font-size:14px">
                    Affix your recent passport size photograph
                    </strong>--%>
                        <asp:Image ID="img_photo0" ImageUrl="~/Images/affix_photo.gif" runat="server" Height="180px"
                            Width="170px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style13">
                        <strong class="style11">Father's/Husband's Name : </strong>
                        <br />
                        <asp:Label ID="lblhusband" runat="server" Font-Bold="false" class="style11"></asp:Label>
                    </td>
                    <td align="left" colspan="1" class="style7">
                        <strong class="style11">Address : </strong>
                        <asp:Label ID="lbladdress" runat="server" Font-Bold="False" class="style11"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" class="style19">
                        <strong class="style11">Examination Center:</strong><br />
                        <asp:Label ID="lblcntr" runat="server" Font-Bold="False" class="style11"></asp:Label>
                    </td>
                    <td valign="top" style="font-size: 12px;" align="left" class="style5">
                        <strong class="style11">Reporting Time : </strong>
                        <br />
                        <asp:Label ID="lblrpt" runat="server" Font-Bold="False" class="style11"></asp:Label>
                    </td>
                    <td align="center" style="height: 90px;">
                        &nbsp;<asp:Image ID="img_sign" runat="server" Height="90px" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top"  colspan="2" style="font-size: 14px">
                        <table class="style14" border="1" cellpadding="1" cellspacing="1" style="border-left-color: #ffcccc;
                            border-bottom-color: #ffcccc; border-top-color: #ffcccc; border-right-color: #ffcccc;">
                            <tr>
                                <td valign="bottom" align="center">
                                    Signature of Observer
                                </td>
                                <td valign="bottom" align="center">
                                    <strong>
                                        <img id="Img1" runat="server" src="~/Images/imgbr1.jpg" style="width: 170px; height: 50px;" />
                                        <br />
                                        Authorized Signatory<br />
                                        (Controller of Examination)</strong>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="bottom" align="center" style="height: 90px;color: Black;">
                        <strong style="font-size: 14px">Candidate Signature</strong>
                    </td>
                </tr>
                <tr style="height: 110px;color: Black;">
                    <td colspan="2">&nbsp;
                    </td>
                    <td valign="bottom" align="center" style="font-size: 12px;">
                        <strong style="font-size: 14px">Candidate's thumb impression :</strong>
                    </td>
                    <%--<td>&nbsp;</td>--%>
                </tr>
                <%--  <tr>
                  <td colspan="3" align="center">
                     <img id="Img1" runat="server" src="~/Images/ins.jpg" width="1050" height="500" />
                   </td>
              </tr>--%>
                <tr visible="false" runat="server">
                    <td colspan="3" style="page-break-before: always;">
                        <%--<iframe id="ifinstruction" runat="server" style="height:2900px;width:100%"></iframe>--%>
                       <%--<asp:Image ID="imgins" runat="server" Height="1000px" Width="100%" />--%>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="3" align="center">
                    
                        <input type="button" value="Print Admit Card " class="cssbutton" onclick="window.print();"  />
                        <asp:Button id="btnprntinst" runat="server" Text="Print Instruction" 
                            onclick="btnprntinst_Click"/>
                        <asp:HiddenField ID="hfgender" runat="server" Visible="false" />
                    </td>
                </tr>--%>
            </table>
        </center>
        <input id="csrftoken" runat="server" name="csrftoken" type="hidden" />
    </div>
    </form>
</body>
</html>
