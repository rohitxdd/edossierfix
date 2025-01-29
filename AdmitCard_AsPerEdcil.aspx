<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmitCard_AsPerEdcil.aspx.cs"
    Inherits="AdmitCard_AsPerEdcil" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .style1
        {
            width: 268435456px;
        }
        ul
        {
            line-height: 20px;
        }
        .style
        {
            font-family: Arial;
            font-size: 16px;
        }
        .style2
        {
            height: 100px;
        }
        .style3
        {
            width: 300px;
            height: 100px;
        }
    </style>
    <script type="text/javascript">
        //preview fileupload control selected image
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var ImageDir = new FileReader();
                ImageDir.onload = function (e) {
                    $('#ctl00_body_imgPostcardPP').attr('src', e.target.result);
                }
                ImageDir.readAsDataURL(input.files[0]);
            }
        }
        function ShowImgSingPreview(input) {
            if (input.files && input.files[0]) {
                var ImageDir = new FileReader();
                ImageDir.onload = function (e) {
                    $('#ctl00_body_SigImage').attr('src', e.target.result);
                }
                ImageDir.readAsDataURL(input.files[0]);
            }
        }
        function printDiv(printableArea) {
            var printContents = document.getElementById(printableArea).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="printableArea">
        <center class="style">
            <table width="950px" border="1" cellpadding="1" cellspacing="1" style="border-collapse: collapse;">
                <tr>
                    <td align="right" colspan="3">
                        <b style="background-color: #a9a9a969;">CANDIDATE'S COPY</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/dsssb.jpg" />
                                </td>
                                <td align="center" style="font-size: 22px; font-weight: bold; float: left; margin-left: 10%">
                                    <span>Government of NCT of Delhi</span>
                                    <br />
                                    <span style="font-size: 16px;">DELHI SUBORDINATE SERVICES SELECTION BOARD</span>
                                    <br />
                                    <span style="font-size: 16px;">FC-18, INSTITUTIONAL AREA, KARKARDOOMA, DELHI</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="background-color: #a9a9a969;">
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="center" class="style11" style="font-size: 20px; font-weight: bold;">
                                    (ई-प्रवेश पत्र / E-ADMIT CARD)
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="center" class="style11" style="font-weight: bold; height: 35px;">
                                    <%--<asp:PlaceHolder ID="barCodePlaceholder" runat="server"></asp:PlaceHolder>--%>
                                    <asp:Image ID="Image2" runat="server" Height="24px" Width="400px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="99%" id="btlInfo" cellpadding="1" cellspacing="1" style="border-collapse: collapse;
                            margin-left: auto; margin-top: 8px;">
                            <tr>
                                <td colspan="6" class="style1">
                                    <table width="99%" id="tblCand" border="1" cellpadding="3" cellspacing="1" style="border-collapse: collapse;
                                        height: 459px;">
                                        <tr>
                                            <td colspan="1" align="left" valign="top" style="height: 15px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblname" runat="server" Text="उम्मीदवार का नाम /" Font-Bold="True"
                                                    CssClass="formlabel"></asp:Label><br />
                                                Name Of The Candidate
                                            </td>
                                            <td colspan="10" align="left" style="height: 15px">
                                                <asp:Label ID="lblname1" runat="server" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1" align="left" valign="top" style="height: 15px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblfat" runat="server" Text="पिता / स्पाउस का नाम /" Font-Bold="True"
                                                    CssClass="formlabel"></asp:Label><br />
                                                Father’s/Spouse Name
                                            </td>
                                            <td colspan="10" align="left" style="height: 15px">
                                                <asp:Label ID="lblfName" runat="server" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" style="height: 15px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblRoll" runat="server" Text="उम्मीदवार का अनुक्रमांक / " Font-Bold="True"
                                                    CssClass="formlabel"></asp:Label><br />
                                                Candidate’s Roll. No
                                            </td>
                                            <td style="height: 15px" align="left">
                                                <asp:Label ID="lblRollno" runat="server" Text="" Style="height: 15px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" style="height: 15px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblReg" runat="server" Text="पंजीकरण आईडी / " Font-Bold="True"
                                                    CssClass="formlabel"></asp:Label><br />
                                                   Registration ID
                                            </td>
                                            <td style="height: 15px" align="left">
                                                <asp:Label ID="lblRegistration" runat="server" Text="" Style="height: 15px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" style="height: 15px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblbrth" runat="server" Text="जन्म तिथि / " Font-Bold="True" CssClass="formlabel"></asp:Label><br />
                                                D.O.B.
                                            </td>
                                            <td align="left" style="height: 15px">
                                                <asp:Label ID="lblbrth1" runat="server" Style="height: 15px" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" class="auto-style17" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblpost" runat="server" Text="आवेदित पद /" Font-Bold="True" CssClass="formlabel"></asp:Label><br />
                                                Post Applied
                                            </td>
                                            <td style="height: 15px" align="left">
                                                <asp:Label ID="lblPostTitle" runat="server" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" class="auto-style17" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblpostcod" runat="server" Text="पद कोड /" Font-Bold="True" CssClass="formlabel"></asp:Label><br />
                                                Post Code
                                            </td>
                                            <td style="height: 15px" align="left">
                                                <asp:Label ID="lblPostCodeN" runat="server" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" class="auto-style17" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblcat" runat="server" Text="वर्ग /" Font-Bold="True" CssClass="formlabel"></asp:Label><br />
                                                Category
                                            </td>
                                            <td style="height: 15px" align="left">
                                                <asp:Label ID="lblcatName" runat="server" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" class="auto-style17" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblSubCat" runat="server" Text="उप-वर्ग /" Font-Bold="True" CssClass="formlabel"></asp:Label><br />
                                                Sub-Category
                                            </td>
                                            <td style="height: 15px" align="left">
                                                <asp:Label ID="lblSubCatName" runat="server" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" class="auto-style16" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblgen" runat="server" Text="लिंग /" Font-Bold="True" CssClass="formlabel"></asp:Label><br />
                                                Gender
                                            </td>
                                            <td align="left" style="height: 15px">
                                                <asp:Label ID="lblGender" runat="server" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr visible="true" runat="server" id="tr_regid">
                                            <td valign="top" align="left" class="auto-style18" style="background-color: #a9a9a969;">
                                                <asp:Label ID="appid" runat="server" Text="आवेदन सं0 /" Font-Bold="True" CssClass="formlabel"></asp:Label><br />
                                                Application No
                                            </td>
                                            <td align="left" style="height: 15px">
                                                <asp:Label ID="lblappidNumber" runat="server" CssClass="gridfont"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" class="auto-style17" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblIType" runat="server" Text="पहचान प्रमाण /" CssClass="formlabel"
                                                    Font-Bold="True"></asp:Label><br />
                                                ID Proof Type
                                            </td>
                                            <td align="left" style="height: 15px">
                                                <asp:Label ID="lblIDName" runat="server" CssClass="formlabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" class="auto-style17" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblIDno" runat="server" Text="पहचान पत्र संख्या /" CssClass="formlabel"
                                                    Font-Bold="True"></asp:Label><br />
                                                ID Proof No
                                            </td>
                                            <td align="left" style="height: 15px">
                                                <asp:Label ID="lblIdNumber" runat="server" CssClass="formlabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" class="style2" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblCentre" runat="server" Text="परीक्षा केंद्र का नाम एवं पता /" CssClass="formlabel"
                                                    Font-Bold="True"></asp:Label><br />
                                                Name And Address Of Test / Examination Centre
                                            </td>
                                            <td align="left" class="style3">
                                                <asp:Label ID="lblCenterName" runat="server" Style="height: 15px; width: 300px;"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="4">
                                    <div id="divPhoto" runat="server" style="width: 440px; height: 629px; overflow: hidden;">
                                        <asp:Image ID="imgPostcardPP" runat="server" Width="431px" Height="629.px" />
                                    </div>
                                    <div>
                                        <asp:Image ID="SigImage" runat="server" Style="height: 35px; width: 200px; border-width: 0px;
                                            margin-left: 22%; margin-top: 4px;" />
                                    </div>
                                    <%-- <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2" align="Center" style="height: 30px">
                                               
                                                <asp:Image ID="SigImage" runat="server" Height="50px" Width="99%" />
                                            </td>
                                        </tr>
                                    </table>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <table width="99%" id="tblExam" border="1" cellpadding="1" cellspacing="1" style="border-collapse: collapse;">
                                        <tr>
                                            <td align="center" style="height: 15px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblExatDate" runat="server" Font-Bold="True" Text="परीक्षा की तिथि व समय /"
                                                    Style="text-align: center"></asp:Label><br />
                                                Date & Time of Exam
                                            </td>
                                            <td align="center" style="height: 15px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblReportingTime" runat="server" Font-Bold="True" Text="उपस्थित होने का समय /"
                                                    Style="text-align: center"></asp:Label><br />
                                                Reporting Time
                                            </td>
                                            <td align="center" style="height: 15px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblGateClose" runat="server" Font-Bold="True" Text="गेट बंद होने का समय /"
                                                    Style="text-align: center"></asp:Label><br />
                                                Gate Closing Time
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="height: 35px">
                                                <asp:Label ID="lblExamdate" runat="server" Text="" CssClass="formlabel" Style="height: 25px"></asp:Label>
                                            </td>
                                            <td align="center" style="height: 35px">
                                                <asp:Label ID="lblExamReporting" runat="server" Text="" CssClass="formlabel" Style="height: 25px"></asp:Label>
                                            </td>
                                            <td align="center" style="height: 35px">
                                                <asp:Label ID="lblGateClosing" runat="server" Text="" Style="height: 25px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <table width="99%" border="1" cellpadding="1" cellspacing="1" style="border-collapse: collapse; max-height:150px">
                                        <tr>
                                            <td align="center" width="25%" class="auto-style14">
                                                <asp:Label ID="lblCanSing" runat="server" CssClass="formlabel"></asp:Label>
                                            </td>
                                            <td align="center" width="25%" class="auto-style13">
                                                <asp:Label ID="lblThumbCand" runat="server" CssClass="formlabel"></asp:Label>
                                            </td>
                                            <td align="center" width="25%" class="auto-style15">
                                                <asp:Label ID="lblVeriPhoto" runat="server" Font-Bold="True" Text="प्रमाणपत्र : मैंने उम्मीदवार के चहरे के साथ ऊपर मुद्रीत फोटो सत्यापित किया है"
                                                    CssClass="formlabel"></asp:Label><br />
                                                Certificate : I have verified the photo printed above with the face of the appearing
                                                candidate
                                            </td>
                                            <td align="center" style="height: 35px" width="25%">
                                                <%--<asp:Label ID="lblAuthSign" runat="server" CssClass="formlabel"></asp:Label>--%>
                                                <asp:Image ID="imgExamContSign" runat="server" Height="35px" Width="200px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="25%" class="auto-style14" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblCandSignInv" runat="server" Font-Bold="True" Text="(उम्मीदवार के हस्ताक्षर)"
                                                    Style="text-align: center"></asp:Label><br />
                                                (Signature of candidate) [To be done in presence of Invigilator]
                                            </td>
                                            <td align="center" width="25%" class="auto-style13" style="background-color: #a9a9a969;">
                                                <asp:Label ID="lblCandThumbInv" runat="server" Font-Bold="True" Text="(उम्मीदवार के अंगूठे का प्रभाव)"
                                                    Style="text-align: center"></asp:Label><br />
                                                (Thumb Impression of Candidate) [To be done in presence of Invigilator]
                                            </td>
                                            <td align="center" width="25%" class="auto-style15" style="padding-top: 40px; background-color: #a9a9a969;">
                                                <asp:Label ID="lblInvSign" runat="server" Font-Bold="True" Text="(निरीक्षक के हस्ताक्षर)"
                                                    Style="text-align: center"></asp:Label><br />
                                                (Invigilator’s Signature)
                                            </td>
                                            <td align="center" style="height: 35px; background-color: #a9a9a969;" width="25%">
                                                <asp:Label ID="lblAuthSignExam" runat="server" Font-Bold="True" Text="(अधिकृत हस्ताक्षरकर्ता)"
                                                    Style="text-align: center"></asp:Label><br />
                                                (Authorized signatory) Controller of Examination
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <table width="99%" border="1" cellpadding="1" cellspacing="1" style="border-collapse: collapse;">
                                        <tr>
                                            <td>
                                                <span style="font-weight: bold">Note:-</span>
                                                 <asp:Label ID="LblAdditional" runat="server" Visible="false">
                                                <ul>
                                                     <span style="font-weight: bold">Candidates are directed to bring the following documents at Examination Center:</span> 
                                                    <li>Original and 02 Photocopies of valid HMV Driving License issued on or before 20/02/2015.</li>
                                                    <li>Original and 02 Photocopies of Aadhaar Card/Other Govt. issued Photo IDs.</li>
                                                    <li>Three Colour Passport Size Photographs.</li> 
                                                    <li>Original Admit Card." </li> 
                                                </ul>
                                                 </asp:Label>
                                                <ul>
                                                    <li>Candidates are advised to bring e-Admit Card in the examination center alongwith
                                                        one original photo ID proof as mentioned above. The candidate should retain this
                                                        1st page of e-Admit Card for all future references.</li>
                                                    <li>Candidates physical appearance in the examination center should match with the photo
                                                        on e-admit card.</li>
                                                    <li>Candidates are advised to follow the dress code strictly as given in the General
                                                        Instructions to the candidates.</li>
                                                    <li>The Candidate has to submit the Self-Declaration (i.e. 3rd page of e-admit card)
                                                        regarding COVID-19 to invigilator.</li>
                                                    <li>Candidates to follow COVID 19 norms of 'social distancing' as well as 'personal
                                                        hygiene' inside the Examination Halls/Rooms as well as in the premises of the Exam
                                                        Center as per SOP issued by MoHFW.</li>
                                                </ul>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="border: none;">
                                <td colspan="7" align="right" style="padding-right: 10px;">
                                    Page 1 of 3
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div style="width: 1130px;">
                <%--<img src="~/Images/AC_Instruct_SkillTest.jpg" runat="server" style="width: 100%;" />--%>
                <img src="Images/Final%20Instruction%20to%20Applicant_page-0001.jpg" runat="server" style="width: 100%;" />
            </div>
            <div style="width: 1010px;">
                <img id="Img1" src="~/Images/AC_ADINS_SkillTest.jpg" runat="server" style="width: 100%;" />
            </div>
        </center>
        <input id="csrftoken" runat="server" name="csrftoken" type="hidden" />
    </div>
    <div align="center">
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="printDiv('printableArea')" />
    </div>
    </form>
</body>
</html>
