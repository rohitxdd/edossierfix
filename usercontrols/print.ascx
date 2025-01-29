<%@ Control Language="C#" AutoEventWireup="true" CodeFile="print.ascx.cs" Inherits="usercontrols_print" %>
<link href="../CSS/Applicant.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    // <!CDATA[

    function Button1_onclick() {

    }

    // ]]>
</script>

<center>
    <table style="width: 90%; height: 352px; border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid; border-bottom: blue thin solid;">
        <tr>
            <td align ="center" colspan="2" class="tr">
                <strong>
            Application For the Post Of </strong>
                <asp:Label ID="lbladvt" runat="server"></asp:Label><strong>
                <asp:Label ID="lblid" runat="server"></asp:Label></strong></td>
        </tr>
        <tr>
            <td style="width: 429px; height: 278px;" align="right">
                <table width="100%" style="border-top-width: thin; border-left-width: thin; border-left-color: blue; border-bottom-width: thin; border-bottom-color: blue; border-top-color: blue; border-right-width: thin; border-right-color: blue">
                    <tr visible="false" runat="server" id="tr_regid">
                        <td valign="top" style="width: 194px; height: 18px;" align="left">
                        <asp:Label ID="appid" runat="server" Text="Application No" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td align="left" style="height: 18px">
                        <asp:Label ID="lblappid1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td valign="top" style="width: 194px" align="left" >
                        <asp:Label ID="lblname" runat="server" Text="Name" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td align="left">
                        <asp:Label ID="lblname1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                        <tr >
                        <td align="left" valign="top"  style="width: 194px; height: 16px">
                        <asp:Label ID="lblgen" runat="server" Text="Gender" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td align="left" style="height: 16px">
                        <asp:Label ID="lblgen1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                        <tr>
                        <td  align="left" valign="top" style="width: 194px; height: 16px">
                        <asp:Label ID="lblbrth" runat="server" Text="Date Of Birth" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td align="left" style="height: 16px">
                        <asp:Label ID="lblbrth1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                     </tr>
             <tr align="left">
            <td style="height: 43px">
                <asp:Label ID="lblAgeOn" runat="server" CssClass="formlabel"  Font-Bold="True"  Text="AGE"></asp:Label>
              
            </td>

            <td>
                <asp:Label ID="lblCandidateAge" runat="server"  Font-Bold="True"  CssClass="formlabel"></asp:Label>
            </td>
        </tr>
             <tr >
                        <td align="left" valign="top"  style="width: 194px; height: 16px">
                        <asp:Label ID="lblmstatushead" runat="server" Text="Marital Status" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td align="left" style="height: 16px">
                        <asp:Label ID="lblmstatus" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" valign="top" style="width: 194px; height: 16px" >
                        <asp:Label ID="lblfat" runat="server" Text="Father's Name" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td  align="left" style="height: 16px">
                        <asp:Label ID="lblfat1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td valign="top" style="width: 194px; height: 16px" align="left" >
                        <asp:Label ID="lblmth" runat="server" Text="Mother's Name" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblmth1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 194px; height: 16px" align="left" >
                        <asp:Label ID="lblspouse" runat="server" Text="Spouse Name" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblspouse1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="height: 16px" align="left" >
                        <asp:Label ID="lbladd" runat="server" Text="Postal Address" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lbladd1" runat="server" Width="94%" Height="40px" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" style="width: 194px" >
                        <asp:Label ID="lblemail" runat="server" Text="Email-Id" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblemail1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>   
        
                    <tr>
                        <td valign="top" align="left" style="width: 194px" >
                        <asp:Label ID="lblMob" runat="server" Text="Mobile Number" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblMobilno" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>                    
                    <tr>
                        <td valign="top"  align="left" style="width: 194px" >
                        <asp:Label ID="lblnat" runat="server" Text="Nationality" Font-Bold="True" CssClass="formlabel"></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblnat1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                       <%--<tr>
                        <td valign="top" align="left" style="width: 194px" >
                        <asp:Label ID="lblphy" runat="server" Text="Physically Handicapped" Font-Bold="True" CssClass="formlabel"></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblphy1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>--%>
                        <tr>
                        <td valign="top" align="left" style="width: 194px" >
                        <asp:Label ID="lblcat" runat="server" Text="Category" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblcat1" runat="server" CssClass="gridfont"></asp:Label>
                            <asp:Label ID="lblmobile" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblfeeldate" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                     <tr id="trcert1" runat="server" visible="false">
                        <td valign="top" align="left" style="width: 194px">
                        <asp:Label ID="lblcatcertno" runat="server" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblcatcertno1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trcert2" runat="server" visible="false">
                        <td valign="top" align="left" style="width: 194px">
                        <asp:Label ID="lblcatcertdt" runat="server" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblcatcertdt1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                     <tr id="trcert3" runat="server" visible="false">
                        <td valign="top" align="left" style="width: 194px">
                        <asp:Label ID="lblcatcertauth" runat="server" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblcatcertauth1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" style="width: 194px">
                        <asp:Label ID="lblser" runat="server" Text="Sub-Category" Font-Bold="True" CssClass="formlabel" ></asp:Label>
                        </td>
                        <td style="height: 16px" align="left">
                        <asp:Label ID="lblser1" runat="server" CssClass="gridfont"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 194px" valign="top">
                            <asp:Label ID="Label_fee" runat="server" Text="Fee Status" CssClass="formlabel" 
                                Font-Bold="True"></asp:Label></td>
                        <td align="left" style="height: 16px">
                            <asp:Label ID="lbl_fee" runat="server" CssClass="formlabel" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                       <tr>
            <td align="left" style="width: 194px" valign="top">
                <asp:Label ID="depapplforlbl" runat="server" Text="Department Applied For" CssClass="formlabel"
                    Font-Bold="True"></asp:Label></td>
            <td align="left" style="height: 16px">
                <asp:Label ID="depapplfor" runat="server" CssClass="formlabel" Font-Bold="True"></asp:Label>
            </td>
        </tr>     
                </table>
                </td>
            <td style="height: 278px; width: 313px;" align="right" valign="top">
            <table style="height: 190px" width="100%" >          
            <tr>
                <td valign="top" align="left" style="height: 22px; width: 317px;">
                    <asp:Image ID="img" runat="server" Height="94px" Width="114px" />&nbsp;<br />
                    <asp:LinkButton ID="lbphoto" runat="server" OnClick="lbphoto_Click" Visible="False">Uplode Photo</asp:LinkButton>
                    <br />
                    <asp:Image ID="img2" runat="server" Height="50px" Width="114px" />&nbsp;<br />
                    <asp:LinkButton ID="lbsign" runat="server" OnClick="lbsign_Click" Visible="False">Uplode Signature</asp:LinkButton>
                    <br />
                    <asp:Image ID="imgLTI" runat="server" Height="94px" Width="114px" />&nbsp;<br />
                    <asp:LinkButton ID="lbLTI" runat="server" OnClick="lbLTI_Click" Visible="False">Uplode Left Thumb Impression</asp:LinkButton>
                    <br />
                    <asp:Image ID="imgRTI" runat="server" Height="94px" Width="114px" />&nbsp;<br />
                    <asp:LinkButton ID="lbRTI" runat="server" OnClick="lbRTI_Click" Visible="False">Uplode Right Thumb Impression</asp:LinkButton>
                    </td>
            </tr>
            <tr>
            <td align="left" valign="top">
            </td>
            </tr>
              <tr>
            <td align="left" valign="bottom">
                &nbsp;</td>
            </tr>

        </table></td>
        </tr>
    </table>
        </center>
<center>
    
    
    <table width="90%">
        <tr>
            <td colspan="5" align="left" class="tr">
                Educational Qualification</td>
        </tr>
<tr>
<td align="right">
<asp:GridView ID="gvquli" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Width="100%" ForeColor="#333333" GridLines="None" DataKeyNames="groupno">
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserial" runat="server" CssClass="griditem" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Department Name" DataField="DepartmentName">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridfont" Width="150px" />
                        </asp:BoundField>
                                <asp:BoundField HeaderText="Qualification" DataField="stnd">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" Width="150px"/>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Discipline" DataField="name">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Percentage(upto 2 decimals)" DataField="percentage">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="Division" DataField="educlass">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>--%>
                                <asp:BoundField HeaderText="Board/ University" DataField="board">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="State" DataField="state">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                   <asp:BoundField HeaderText="Passing Year" DataField="YEAR">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                            </Columns>
                            <%--<HeaderStyle BackColor="#507CD1" CssClass="gridheader" Font-Bold="True" ForeColor="White" />--%>
                               <HeaderStyle CssClass="gridheading" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView></td>
</tr>
    </table>
    </center>
    <table style="width:90%; margin-left:49px;" id="Desirable" runat="server" visible="false">
        <tr>
            <td colspan="5" align="left" class="tr">
                Desirable Qualification</td>
        </tr>
        <tr>
            <td  colspan="5" align="right">
    <asp:GridView ID="grd_desirableEdu" runat="server" AutoGenerateColumns="False" CellPadding="4"
        Width="100%" ForeColor="#333333" GridLines="None">
        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <asp:Label ID="lblserial" runat="server" CssClass="griditem" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gridheader" />
                <ItemStyle VerticalAlign="Top" />    
            </asp:TemplateField>
             <asp:BoundField HeaderText="Department Name" DataField="DepartmentName">
                <HeaderStyle CssClass="gridheader" />
                <ItemStyle CssClass="gridfont" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Desirable Education" DataField="DesirableQualification">
                <HeaderStyle CssClass="gridheader" />
                <ItemStyle CssClass="gridfont" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Status" DataField="desirable">
                <HeaderStyle CssClass="gridheader" />
                <ItemStyle CssClass="gridfont" />
            </asp:BoundField>
        </Columns>
          <HeaderStyle CssClass="gridheading" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </td>
        </tr>

    </table>
        <br />
         <table style="width:90%; margin-left:49px;" id="DesirableExp" runat="server" visible="false">
        <tr>
            <td colspan="5" align="left" class="tr">
                Desirable Experience</td>
        </tr>
              <tr>
                    <td  colspan="5" align="right">
                        <asp:GridView ID="grd_desirableExp" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Width="100%" ForeColor="#333333" GridLines="None">
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserial" runat="server" CssClass="griditem" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle VerticalAlign="Top" />    
                                </asp:TemplateField>
                                 <asp:BoundField HeaderText="Department Name" DataField="DepartmentName">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Desirable Experience" DataField="desirableExperience">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="desirableExp">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                            </Columns>
                           <%-- <HeaderStyle BackColor="#507CD1" CssClass="gridheader" Font-Bold="True" ForeColor="White" />--%>
                              <HeaderStyle CssClass="gridheading" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        </td>
                </tr>
             </table>
       

<center>
    
        
            <table width="90%">
                <tr runat="server" id="trExperience" visible="false">
                    <td colspan="5" align="left" class="tr">
                      Experience</td>
                </tr>
                <tr>
                    <td  colspan="5" align="right">
                        <asp:GridView ID="gvexp" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Width="100%" ForeColor="#333333" GridLines="None">
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserial" runat="server" CssClass="griditem" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle VerticalAlign="Top" />    
                                </asp:TemplateField>
                                 <asp:BoundField HeaderText="Department Name" DataField="DepartmentName">
                                 <HeaderStyle CssClass="gridheader" />
                                 <ItemStyle CssClass="gridfont" Width="150px" />
                                 </asp:BoundField>
                                <asp:BoundField HeaderText="Name of Post" DataField="Post">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Date From" DataField="datefrom">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Date To" DataField="dateto">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Employer Name" DataField="emp_name">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Employer Contact No" DataField="emp_contactno">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Employer Address" DataField="emp_addr">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>
                            </Columns>
                           <%-- <HeaderStyle BackColor="#507CD1" CssClass="gridheader" Font-Bold="True" ForeColor="White" />--%>
                              <HeaderStyle CssClass="gridheading" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnedit_personal" runat="server" Text="Edit Personal Details" CssClass="cssbutton" OnClick="btnedit_personal_Click" Width="150px" Visible="False" />

                    </td>
                    <td>
                        <asp:Button ID="btn_editphoto" runat="server" Text="Edit Photo,Signature,LTI & RTI" CssClass="cssbutton" OnClick="btn_editphoto_Click" Width="250px" Visible="False" />
                        <asp:Button ID="btn_addphoto" runat="server" Text="ADD Photo,Signature,LTI & RTI" CssClass="cssbutton" OnClick="btn_addphoto_Click" Width="250px" Visible="False" />
                    </td>
                    <td>
                    <asp:Button ID="btn_editexp" runat="server" Text="Edit Qualification & Experience" CssClass="cssbutton" OnClick="btn_editexp_Click" Width="200px" Visible="False" />
                    <td>
                        <asp:Button ID="btn_addexp" runat="server" Text="Add Qualification & Experience" CssClass="cssbutton" OnClick="btn_addexp_Click" Width="200px" Visible="False" />
                </tr>
                <tr cssclass="ariallightgrey" style="color: #C00000">
            <td colspan="4" align="left">
                <asp:CheckBox ID="chk_decl" runat="server" CssClass="formlabel" />
                DECLARATION :<br />
                a) I hereby certify that all statement made in this application are true, complete
            and correct to the best of my knowledge and belief and have been filled by me..<br />
                b) I also declare that I have submitted only one application for one post code in
            response to the advertisement.<br />
                c) I have read all the provisions mentioned in the advertisement/notice of examination
            carefully as published on the website of DSSSB and I hereby under take to abide
            by them.<br />
                d) I understand that in the event of information being found false or incorrect
            at any stage prescribed in the notice or any ineligibility being detected before
            or after the examination, my candidature/selection/appointment is liable to be cancelled/terminated
            automatically without any notice to me and action can be taken against me by the
            DSSSB.<br />
                e) The information submitted herein shall be treated as final in respect of my candidature
            for the post applied for through this application form.<br />
                f) I also declare that I have informed my Head of office/Department in writing that
            I am applying for this post/examination (for Government Employees only).
            <br />
            g) I <asp:Label ID="Lbl_name" runat="server"></asp:Label> S/O /  D/O /  W/O <asp:Label ID="Lbl_guardian" runat="server"></asp:Label> do hereby undertake that I will check https://dsssbonline.nic.in called as OARS Portal in a regular reasonable interval to update myself about DSSSB guidelines and examinations.
The onus of checking https://dsssbonline.nic.in in on me is being applicant of posts in DSSSB. I also understand that the mail/SMS facility provided to the candidate (s) is an additional facility provided by the DSSSB in addition to OARS portal
            </td>
        </tr>
                 <tr cssclass="ariallightgrey" style="color: #C00000">
            <td colspan="4" align="left">
                <asp:CheckBox ID="chkPreiview" runat="server" CssClass="formlabel" />
                DECLARATION :<br />
                I have seen the preview and verified all the details entered by me are correct. I will not claim any changes in my details after final submission of application
                </td></tr>

                <tr>
                <td colspan="3"  align="center">
                    &nbsp;<asp:TextBox ID="txtjid" runat="server" Visible="False" Width="18px"></asp:TextBox>
                    &nbsp;<asp:Button ID="btnconform" runat="server" CssClass="cssbutton"  OnClick="btnconform_Click"                   
                        Text="Submit Final Application" />
                         <asp:Button ID="btnpay" runat="server" CssClass="cssbutton" 
                        Text="Pay Online and Submit Final Application" Visible="false" 
                        onclick="btnpay_Click"/><br />
                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#C00000" Font-Size="Larger"  Text="Please Complete Entry"
                        Visible="False"></asp:Label>&nbsp;
                    <asp:HiddenField ID="hf_expnoofyear" runat="server" />
                    <asp:HiddenField ID="hfreqid" runat="server" />
                    <asp:HiddenField ID="hfgroupno" runat="server" />
                </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbl_warning"  runat="server" ForeColor="#C00000" Text="Note:The online application will be received on provisional basis only, the eligibility of candidate shall be determined strictly on the basis of RR of the concerned post."
                            Width="100%" Visible="False"></asp:Label></td>
                </tr>
                <%--<asp:BoundField HeaderText="Division" DataField="educlass">
                                    <HeaderStyle CssClass="gridheader" />
                                    <ItemStyle CssClass="gridfont" />
                                </asp:BoundField>--%>
            </table>
    </center>
