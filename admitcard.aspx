<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admitcard.aspx.cs" Inherits="admitcard" %>

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
            font-size: 22px;
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
            font-size: 22px;
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
            font-size: 22px;
            width: 374px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <table width="950" border="1" cellpadding="1" cellspacing="1" style="border-left-color: #ffcccc;
                border-bottom-color: #ffcccc; border-top-style: solid; border-top-color: #ffcccc;
                border-right-style: solid; border-left-style: solid; border-right-color: #ffcccc;
                border-bottom-style: solid; top: 5px;">
                <tr>
                <td align="right" colspan="3"> 
               CANDIDATE'S COPY 
                </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/dsssblogo.jpg" />
                                </td>
                                <td align="center" style="font-size: 22px; font-weight: bold; width: 80%">
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
                        <asp:Image ID="img_photo0" ImageUrl="~/Images/affix_photo.gif" runat="server" Height="180px"
                            Width="170px" />
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
                    <td align="centre" class="style3" rowspan="2">
                        <%--<strong style="font-size:14px">
                    Affix your recent passport size photograph
                    </strong>--%>
                        <asp:Image ID="img_photo" runat="server" Height="180px" Width="170px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style13">
                        <strong class="style11">Father's/Husband's Name : </strong>
                        <br />
                        <asp:Label ID="lblhusband" runat="server" Font-Bold="false" class="style11"></asp:Label>
                    </td>
                    <td align="left" colspan="1" class="style7">
                        <strong class="style11">Reporting Time : </strong>
                        <br />
                        <asp:Label ID="lblrpt" runat="server" Font-Bold="False" class="style11"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" class="style19" colspan="2">
                        <strong class="style11">Examination Center:</strong><br />
                        <asp:Label ID="lblcntr" runat="server" Font-Bold="False" class="style11"></asp:Label>
                    </td>
                    <td align="center" style="height: 90px;">
                        &nbsp;<asp:Image ID="img_sign" runat="server" Height="90px" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" class="style11" colspan="2" rowspan="3">
                        <table class="style14" border="1" cellpadding="1" cellspacing="1" style="border-left-color: #ffcccc;
                            border-bottom-color: #ffcccc; border-top-color: #ffcccc; border-right-color: #ffcccc;">
                            <tr>
                                <td valign="bottom" align="center">
                                    Signature of Invigilator
                                    - I</td>
                                <td valign="bottom" align="center">
                                    Signature of Invigilator
                                    - II</td>
                                <td valign="bottom" align="center">
                                    <strong>
                                        <img id="Img1" runat="server" style="width: 200px; height: 71px;" />
                                        <br />
                                        Authorized Signatory<br />
                                        (<asp:Label ID="lbldesig" runat="server"></asp:Label>)</strong>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="bottom" align="center" class="style18">
                        <strong style="font-size: 14px">Candidate&#39;s Signature</strong>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 0px;">
                    </td>
                </tr>
                <tr class="style18">
                    <td valign="bottom" align="center" style="font-size: 12px; height: 100px;">
                        <strong style="font-size: 14px">Candidate's thumb impression </strong>&nbsp;<br />
                    </td>
                    <%--<td>&nbsp;</td>--%>
                </tr>
                
                         <tr>
                    <td colspan="3" align="left" class="style11">
                        <strong>Note:</strong>Candidates are advised to bring pages 1 and 2 of this 
                        admit card in the
                        examination hall along with one original photo id proof like Voter ID,Adhar card,Driving
                        license etc. Mobile, Pager, Calculator, etc. are not allowed in the examination hall.
                    </td>
                </tr>
                    <tr>
                    <td colspan="3" align="center">
                        <img id="Img2" runat="server" src="~/Images/ins.jpg" width="1050" height="520" alt="Instructions" />
                    </td>
                </tr>


<tr>
<td colspan="3" align="right" style="font-size:small;">
Page 1 of 2
</td>
</tr>

                <tr style="page-break-before: always">
                <td align="right" colspan="3"> 
               INVIGILATOR'S COPY 
                </td>
                </tr>

                <tr>
                    <td colspan="3">
                        <table width="100%" align="center">
                            <tr>
                                <td colspan="4" align="center">
                           <br />
                                    <div style="border: 1px; border-style: solid; width: 664px; height: 996px;">
                                    <%-- <b> <-------------------------- 4"----------------------------></b>--%>
                                    <table width="664px" height="950px" align="center">
                                        <tr>
                                            <td align="center" valign="middle">
                                             
                                              <h4>  Please paste a recent Postcard Size (6&quot; x 
                                                4&quot;) Photograph here.<br />
                                                (The Candidate and the Invigilator to sign across the photograph 
                                                as indicated in the instructions below.)</h4>
                                    
                                                <asp:Image ID="imgPostCardPhoto" runat="server" Height="800px" Width="600px" />
                                           </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <p align="center" style="width: 222px; color:Black;letter-spacing:2px; font-size:small;" >
                                        Candidate to put his/her Left 
                                        hand Thumb Impression here in presence of Invigilator
                                    </p>
                                </td>
                                <td width="200px" colspan="2">
                                    <img src="Images/arrow.jpg" />
                                </td>
                                <td align="left" valign="top" style="color:Black">
                                    <div style="border: 1px; border-style: solid; width: 250px; height: 80px">
                                    </div>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" colspan="4" style="font-weight:bold; font-size:small;">
                                    Roll No. : <asp:Label ID="lblrno" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" colspan="4" style="font-weight:bold; font-size:small;">
                                    Name :&nbsp; <asp:Label ID="lblcname" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" colspan="2" style="font-weight:bold; font-size:small;">
                                    Post Code :&nbsp; <asp:Label ID="lblpostcode" runat="server"></asp:Label>
                                </td>
                                <td align="right" valign="middle" colspan="2">
                                    ___________________________
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Signature of the Candidate</td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" colspan="2" style="font-weight:bold; font-size:small;">
                                    Date of Exam : <asp:Label ID="lbldateofexam" runat="server"></asp:Label>
                                </td>
                                <td align="right" valign="middle" colspan="2" style="font-weight:bold; font-size:small;">
                                    Name&nbsp;&nbsp; :___________________________________________</td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle" colspan="4" style="font-weight:bold; font-size:small;">
                                    Roll No.:&nbsp;&nbsp;__________________________________________<br />
                                    </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle" colspan="4" style="font-weight:bold; font-size:small;">
                                    (In 
                                    Candidate&#39;s handwriting in presence of Invigilator)</td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left" style="color:Black">
                                <br />_____________________________<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Signature of the Invigilator
                                </td>
                            </tr>
                            <tr>
                            <td colspan="4" align="left" style="color:Black;font-size:14px">
                                INSTRUCTIONS FOR CANDIDATES:<br />
                            a) The Candidate to paste a latest colored postcard size (6" x 
                                4") photograph of his/her own in the designated space.<br />
                                b) Invigilator shall ensure that photograph and signature on this page matches 
                                with photograph and signature of Candidate on Page 1 of<br />
                                Admit Card.<br />
                                c) The Candidate to sign across the photograph on left side and put his/her Left&nbsp; 
                                hand Thumb Impression in the designated space, <u>in the presence of the Invigilator</u>.<br />
                                d) The Invigilator 
                                should sign across the photograph of the candidate on the right side.
                                <br />
                                e) &nbsp;It is mandatory for the candidate to bring this page of the Admit Card with 
                                pasted photograph.<u> If he/she doesn&#39;t bring this , then he/she will not be 
                                allowed to enter the examination centre</u>.<br />
                                f) It is mandatory to handover this page to the Invigilator.</td>
                            </tr>
                        </table>
                    </td>
                </tr>
              <tr>
              <td align="right" colspan="3" style="font-size:small;">
              Page 2 of 2
              </td>
              </tr>


                  <tr>
                    <td colspan="3" align="center">
                        <input type="button" value="Print Admit Card " class="cssbutton" onclick="window.print();" />
                    </td>
                </tr>

            </table>
        </center>
        <input id="csrftoken" runat="server" name="csrftoken" type="hidden" />
    </div>
    </form>
</body>
</html>
