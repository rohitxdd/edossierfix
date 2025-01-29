<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="jobupload.aspx.cs" Inherits="jobupload" Title="Upload Photo & Signature" %>

<%@ Register Src="~/usercontrols/appnumber.ascx" TagName="appno" TagPrefix="no" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table width="100%">
        <tr align="right">
            <td>
                &nbsp;<%--<iframe src="" style="width: 100%; height:100%;"></iframe>--%>
            </td>
        </tr>
        <tr id="trvalidate" runat="server">
            <td>
                <%--style="height: 758px"--%>
                <table border="1px" width="100%">
                    <tr id="trtext" runat="server">
                        <td align="center" class="tr">
                            <strong>Upload Photograph and Signature</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tblconf" runat="server">
                                <tr>
                                    <td>
                                        <no:appno ID="ddl_applid" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" CssClass="cssbutton" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <table width="100%" id="tblupload" runat="server" visible="true">
                                <tr>
                                    <td colspan="4" align="center" class="tr">
                                        Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4" style="">
                                        <asp:Label ID="lbl" runat="server" CssClass="formheading" Text="Application for the Post of :"></asp:Label>
                                        &nbsp;<asp:Label ID="lbl_app" runat="server" ForeColor="#C00000" CssClass="formlabel"></asp:Label>
                                        ::
                                        <asp:Label ID="Label3" runat="server" CssClass="formheading" Text="    Post Code:"></asp:Label>
                                        <asp:Label ID="lbl_post_code" runat="server" ForeColor="#C00000" CssClass="formlabel"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" CssClass="ariallightgrey" Text="Advt No:" Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_advt" runat="server" ForeColor="#C00000" CssClass="formlabel"
                                            Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 10%">
                                        <asp:Label ID="lbln" runat="server" Text="Name" Font-Bold="True" CssClass="formheading"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 40%">
                                        &nbsp;<asp:Label ID="lblname" runat="server" CssClass="formlabel"></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lbldob" Text="Date of Birth" runat="server" Font-Bold="True" CssClass="formheading"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Label ID="lbldobr" runat="server" CssClass="formlabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 10%">
                                        <asp:Label ID="formheading" runat="server" Text="Father's Name" Font-Bold="True"
                                            CssClass="formheading"></asp:Label>
                                    </td>
                                    <td style="width: 40%">
                                        <asp:Label ID="lblfname" runat="server" CssClass="formlabel"></asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lblmname" Text="Mother's Name" runat="server" Font-Bold="True" CssClass="formheading"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Label ID="lblmthrname" runat="server" CssClass="formlabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4" style="width: 100%">
                                        <asp:Label ID="lbl_pre_msg" runat="server" Text="Previous Existing Photograph,Signature,Left Thumb Impression,Right Thumb Impression"
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="4">
                                        <asp:Image ID="img_s_pic" runat="server" Height="94px" Width="114px" />
                                        <asp:Button ID="btn_upload_s_pic" runat="server" OnClick="btn_upload_s_pic_Click"
                                            Text="Upload Same Photo" Width="118px" />&nbsp;
                                        <asp:Image ID="img_s_sign" runat="server" Height="50px" Width="114px" />&nbsp;
                                        <asp:Button ID="btn_upload_s_sign" runat="server" Text="Upload Same Signature" Width="140px"
                                            OnClick="btn_upload_s_sign_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="4">
                                        <asp:Image ID="img_s_lti" runat="server" Height="94px" Width="114px" />
                                        <asp:Button ID="btn_upload_s_lti" runat="server" OnClick="btn_upload_s_lti_Click"
                                            Text="Upload Same Left Thumb Impression" Width="220px" />&nbsp;
                                        <asp:Image ID="img_s_rti" runat="server" Height="94px" Width="114px" />
                                        <asp:Button ID="btn_upload_s_rti" runat="server" OnClick="btn_upload_s_rti_Click"
                                            Text="Upload Same Right Thumb Impression" Width="220px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="left" style="height: 10px">
                                        <strong><span style="color: #610B0B">(Note : Press the Browse button to select a file.
                                            Upload button to upload a file.)</span></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4" style="width: 686px">
                                        <table width="100%" border="2" bordercolordark="#000000">
                                            <tr>
                                                <td colspan="6" align="center" class="tr">
                                                    Upload PostCard Size Photograph (5*7 inch : Height*Width)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 76px" valign="top">
                                                    <table>
                                                        <tr>
                                                            <td align="center" style="width: 398px; height: 36px;">
                                                                <strong><span style="font-size: 9pt">Upload Photo <strong><span style="font-size: 9pt">
                                                                    Upload Photo</span></strong>
                                                            </td>
                                                            <tr>
                                                                <td style="height: 21px; width: 398px;" align="center">
                                                                    <asp:FileUpload ID="photoupload" runat="server" Width="356px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 398px">
                                                                    <asp:Button ID="btnupphoto" runat="server" Text="Upload" OnClick="btnupphoto_Click"
                                                                        CssClass="cssbutton" />
                                                                </td>
                                                            </tr>
                                                    </table>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://www.freeonlinephotoeditor.com/"
                                                        Target="_blank">Crop Photo Online</asp:HyperLink>
                                                    <br />
                                                    <span style="color: Blue">You may also use MS paint Application to resize image. </span>
                                                    <br />
                                                    <br />
                                                    <span style="color: red; vertical-align: top">Note: </span>
                                                    <br />
                                                    <span style="color: red">*</span><asp:Label ID="Label1" ForeColor="#C00000" runat="server"
                                                        CssClass="Label13" Text="Upload scanned /  digital image of coloured postcard size photograph of the candidate and should be in JPEG format and image should be between 50 kb to 300 kb (required resolution 480x672 pixels, 96 dpi)."></asp:Label>
                                                    <br />
                                                    <br />
                                                    <span style="color: red">*</span><asp:Label ID="Label2" runat="server" Text=" Coloured postcard size photograph ( size 5*7 inch) should be of upper half of body only clearly showing face, both ears and both shoulders. "
                                                        ForeColor="#C00000" CssClass="Label13"></asp:Label>
                                                </td>
                                                <td valign="top">
                                                    <div id="divPCSP" runat="server" style="width: 1px; border: 5px #003366; border-style: solid;
                                                        width: 480px; height: 672px; overflow: hidden;">
                                                        <asp:Image ID="img" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 7px" colspan="4">
                                                    <span><strong><span style="color: #000000">Instructions<br />
                                                    </span></strong><span style="color: #ff0033"><strong><span style="color: #B40404">Width&nbsp;</span>
                                                        <span style="color: #1B2A0A">:480px</span><br />
                                                        <span style="color: #B40404">Height</span> <span style="color: #1B2A0A">:672px</span><br />
                                                        <span style="color: #B40404">Format</span> <span style="color: #1B2A0A">:JPG/JPEG</span><br />
                                                        <span style="color: #B40404">Max.Size</span> <span style="color: #1B2A0A">:300KB</span></strong><br />
                                                        <strong><span style="color: #000000"></span></strong></span></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4" style="width: 686px">
                                        <table width="100%" border="2" bordercolordark="#000000">
                                            <tr>
                                                <td colspan="6" align="center" class="tr">
                                                    Upload Signature
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <strong>Note : Please Sign on White Paper with Dark Pen and Scan it in JPG format.</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 76px" valign="top">
                                                    <table style="width: 92%; height: 98px">
                                                        <tr>
                                                            <td valign="middle" align="center" style="width: 398px; height: 36px;">
                                                                <strong><span style="font-size: 9pt">Upload Signature</span></strong>
                                                            </td>
                                                            <tr>
                                                                <td style="height: 21px; width: 398px;" align="center">
                                                                    <asp:FileUpload ID="signatureupload" runat="server" Width="356px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 398px">
                                                                    <asp:Button ID="btnuploadsig" runat="server" Text="Upload" OnClick="btnuploadsig_Click"
                                                                        CssClass="cssbutton" />
                                                                </td>
                                                            </tr>
                                                    </table>
                                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://www.freeonlinephotoeditor.com/"
                                                        Target="_blank">Crop Signature Online</asp:HyperLink>
                                                    <br />
                                                    <span style="color: Blue">You may also use MS paint Application to resize image. </span>
                                                </td>
                                                <td style="width: 1px" valign="middle">
                                                    <asp:Image ID="img2" runat="server" Height="50px" Width="114px" />
                                                </td>
                                                <td style="width: 7px" colspan="4">
                                                    <strong><span style="font-size: 9pt"></span></strong><span style="color: #ff0033"><strong>
                                                        <span style="color: #000000">Instructions<br />
                                                        </span></strong><span style="color: #ff0033"><strong><span style="color: #B40404">Width&nbsp;</span>
                                                            <span style="color: #1B2A0A">:140px</span><br />
                                                            <span style="color: #B40404">Height</span> <span style="color: #1B2A0A">:110px</span><br />
                                                            <span style="color: #B40404">Format</span> <span style="color: #1B2A0A">:JPG</span><br />
                                                            <span style="color: #B40404">Max.Size</span> <span style="color: #1B2A0A">:40KB</span></strong><br />
                                                        </span></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4" style="width: 686px">
                                        <table width="100%" border="2" bordercolordark="#000000">
                                            <tr>
                                                <td colspan="6" align="center" class="tr">
                                                    Upload Left Thumb Impression
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 76px" valign="top">
                                                    <table style="width: 92%; height: 98px">
                                                        <tr>
                                                            <td valign="middle" align="center" style="width: 398px; height: 36px;">
                                                                <strong><span style="font-size: 9pt">Upload Left Thumb Impression</span></strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 21px; width: 398px;" align="center">
                                                                <asp:FileUpload ID="LTIupload" runat="server" Width="356px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="width: 398px">
                                                                <asp:Button ID="btnupLTI" runat="server" Text="Upload" OnClick="btnupLTI_Click" CssClass="cssbutton" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://www.freeonlinephotoeditor.com/"
                                                        Target="_blank">Crop LTI Online</asp:HyperLink>
                                                    <br />
                                                    <span style="color: Blue">You may also use MS paint Application to resize image. </span>
                                                </td>
                                                <td style="width: 1px" valign="middle">
                                                    <asp:Image ID="imgLTI" runat="server" Height="94px" Width="114px" />
                                                </td>
                                                <td style="width: 7px" colspan="4">
                                                    <strong><span style="font-size: 9pt"></span></strong><span style="color: #ff0033"><strong>
                                                        <span style="color: #000000">Instructions<br />
                                                        </span></strong><span style="color: #ff0033"><strong><span style="color: #B40404">Width&nbsp;</span>
                                                            <span style="color: #1B2A0A">:110px</span><br />
                                                            <span style="color: #B40404">Height</span> <span style="color: #1B2A0A">:140px</span><br />
                                                            <span style="color: #B40404">Format</span> <span style="color: #1B2A0A">:JPG</span><br />
                                                            <span style="color: #B40404">Max.Size</span> <span style="color: #1B2A0A">:40KB</span></strong><br />
                                                        </span></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4" style="width: 686px">
                                        <table width="100%" border="2" bordercolordark="#000000">
                                            <tr>
                                                <td colspan="6" align="center" class="tr">
                                                    Upload Right Thumb Impression
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 76px" valign="top">
                                                    <table style="width: 92%; height: 98px">
                                                        <tr>
                                                            <td valign="middle" align="center" style="width: 398px; height: 36px;">
                                                                <strong><span style="font-size: 9pt">Upload Right Thumb Impression</span></strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 21px; width: 398px;" align="center">
                                                                <asp:FileUpload ID="RTIupload" runat="server" Width="356px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="width: 398px">
                                                                <asp:Button ID="btnupRTI" runat="server" Text="Upload" OnClick="btnupRTI_Click" CssClass="cssbutton" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://www.freeonlinephotoeditor.com/"
                                                        Target="_blank">Crop RTI Online</asp:HyperLink>
                                                    <br />
                                                    <span style="color: Blue">You may also use MS paint Application to resize image. </span>
                                                </td>
                                                <td style="width: 1px" valign="middle">
                                                    <asp:Image ID="imgRTI" runat="server" Height="94px" Width="114px" />
                                                </td>
                                                <td style="width: 7px" colspan="4">
                                                    <strong><span style="font-size: 9pt"></span></strong><span style="color: #ff0033"><strong>
                                                        <span style="color: #000000">Instructions<br />
                                                        </span></strong><span style="color: #ff0033"><strong><span style="color: #B40404">Width&nbsp;</span>
                                                            <span style="color: #1B2A0A">:110px</span><br />
                                                            <span style="color: #B40404">Height</span> <span style="color: #1B2A0A">:140px</span><br />
                                                            <span style="color: #B40404">Format</span> <span style="color: #1B2A0A">:JPG</span><br />
                                                            <span style="color: #B40404">Max.Size</span> <span style="color: #1B2A0A">:40KB</span></strong><br />
                                                        </span></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnexit" runat="server" Text="Back" CssClass="cssbutton" OnClick="btnexit_Click"
                    Visible="False" />&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <%--<asp:Label ID="lblmsg" runat="server" Text="Nothing Pending" ForeColor="#C00000"
                    Font-Bold="True" Visible="False"></asp:Label>--%>
                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="#C00000"
                    Font-Bold="True" Visible="False"></asp:Label>
            </td>
        </tr>
        
        <tr>
        <td align="left">
         <asp:ImageButton ID="img_btn_prev" runat="server" Height="34px" ImageUrl="~/Images/prev.jpg"
                    Width="52px" Visible="true" OnClick="img_btn_prev_Click" />
        </td>
            <td align="right">
                <asp:ImageButton ID="img_btn_next" runat="server" Height="30px" ImageUrl="~/Images/next.jpg"
                    Width="49px" Visible="true" OnClick="img_btn_next_Click" />
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="lbl_step" runat="server" Text="Step 2/5" ForeColor="DarkGreen" Font-Bold="True"  Font-Size="Large"
                    Font-Italic="True" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
