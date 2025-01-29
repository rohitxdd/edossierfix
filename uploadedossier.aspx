<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="uploadedossier.aspx.cs" Inherits="uploadedossier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table style="text-align: center">
        <tr>
            <td align="left">
                <asp:Label ID="lblappno" runat="server" Text="Select Post Applied" CssClass="formheading"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="DropDownList_post" runat="server" CssClass="ddl" Width="500px">
                </asp:DropDownList>
                
            </td>
            <td style="width:150px;" align="center" >
            <a href="AdvtDetailFiles/Edossier_HelpFile_dsssbonline.pdf" target="_blank" 
            onmousemove="Click here to download e-Dossier Help File"><b style="font-size: small">e-Dossier Help File</b></a>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button Text="View eDossier" runat="server" ID="btn_submit" OnClick="btn_submit_Click" />
                <asp:Button Text="Upload eDossier" runat="server" ID="btnupload" OnClick="btnupload_Click" />
                <asp:Button Text="Replace Recalled Document" runat="server" ID="btnreplace" OnClick="btnreplace_Click" />
            </td>
        </tr>
        <%-- <tr class="formlabel" id="trnote" runat="server" visible="false">
                            <td align="left" colspan="2">
                               
                                <span style="color: red"># <asp:Label ID="Label18" runat="server"
                                    Text=" All Documents except Photo & Signature should be in PDF format only. Photo & Signature 
                                    should be in JPG/JPEG format only."></asp:Label>
                           
                                <br />
                                # 
                                All Documents Maximum size is 2MB.<br />
                                # Photo Max size is 40KB, Max width 110px and Max Height 
                                140px.<br />
                                # Signature Max size is 20KB, 
                                Max width 140px and Max Height 110px.</span></td>
                        </tr>--%>
        <tr>
            <td colspan="3">
                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="#FF3300" Text="No Post available for eDossier at this time."
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr id="trdocgrd1" runat="server" visible="false">
            <td colspan="3">
            <asp:Label runat="server" ID="lbledudoc" CssClass="formheading"  Text="List of Documents related to Educational Qualifications"></asp:Label>
                </td>
        </tr>
        <tr id="trdocgrd2" runat="server" visible="false">
            <td colspan="3" align="left">                            
                                <span style="color: red"> 
                <asp:Label ID="Label19" runat="server"
                                    Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>                          
                                </span></td>
        </tr>
        <tr id="trdocgrd" runat="server" visible="false">
            <td align="center" colspan="3">
                <asp:GridView ID="grddoc" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,certificateReq,remarks"  Width="100%" 
                   onrowcommand="grddoc_RowCommand" onrowediting="grddoc_RowEditing" 
                    onrowdatabound="grddoc_RowDataBound" onrowupdating="grddoc_RowUpdating" onrowcancelingedit="grddoc_RowCancelingEdit"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Required">
                            <ItemTemplate>
                                <%# Eval("certificateReq")%>
                             <%--   <asp:Label runat="server" ID="lb1" Text="(Maximum size is 2MB)" ForeColor="Red"></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Type">
                            <ItemTemplate>
                                <%# Eval("ctypename")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                 <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false" Target="_blank" />
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Top" >
                            <ItemTemplate>
                            <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label> 
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" ToolTip="Maximum 200 characters allowed" MaxLength="200" TextMode="MultiLine" 
                                Width="95%" Visible="false" Height="80px">
                               </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                Display="Dynamic" ErrorMessage="Invalid Characters In Doc Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                ValidationGroup="1"></asp:RegularExpressionValidator>                                                     
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                    Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="false"></asp:LinkButton>
                                     <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                                      <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
                <asp:HiddenField ID="hfdummy_no" runat="server" Visible="false" />
            </td>
        </tr>


        <tr id="trothdocgrd1" runat="server" visible="false">
        <td colspan="3">
        <asp:Label runat="server" ID="lblotherdoc" CssClass="formheading" Text="List of Other  Documents/Certificates"></asp:Label>     
        </td>
        </tr>
       <%-- <tr id="trothdocgrd2" runat="server" visible="false">
        <td colspan="2" align="left">                              
                                <span style="color: red"> 
                <asp:Label ID="Label20" runat="server"                               
                  Text="All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>                    
                            </span>
        </td>
        </tr>--%>
         <tr id="trothdocgrd" runat="server" visible="false">
            <td align="center" colspan="3">
                <asp:GridView ID="grdother" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,certificateReq,remarks"  Width="100%" 
                    onrowcommand="grdother_RowCommand" onrowediting="grdother_RowEditing" 
                    onrowdatabound="grdother_RowDataBound" 
                    onrowupdating="grdother_RowUpdating" onrowcancelingedit="grdother_RowCancelingEdit"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate/Document Required">
                            <ItemTemplate>
                                <%# Eval("certificateReq")%>
                               <%--  <asp:Label runat="server" ID="lb2" ForeColor="Red"></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Type">
                            <ItemTemplate>
                                <%# Eval("ctypename")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                            <asp:Image ID="img" runat="server" Width="114px" Height="94px" Visible="false" />
                            <asp:Image ID="img2" runat="server" Height="50px" Width="114px" Visible="false" />
                                <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false" Target="_blank" />
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" /> 
                                 <asp:Label runat="server" ID="lb2" ForeColor="Red"></asp:Label>
                                 <asp:Label ID="lbladhar" runat="server" Visible="false" Text="Adhar No."></asp:Label>
                                <asp:TextBox ID="txtadharno" runat="server" Visible="false" ></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="revtxtadharno" runat="server" ControlToValidate="txtadharno" Display="None" 
                                  ErrorMessage="Invalid character in Adhar No" ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    <asp:Label ID="lbladharno" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"adharno") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Top" >
                            <ItemTemplate>
                            <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label> 
                                <asp:TextBox runat="server" ID="txtboxremarksothervalue" MaxLength="200" TextMode="MultiLine" ToolTip="Maximum 200 characters allowed"
                                Width="95%" Height="80px" Visible="false">
                               </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksothervalue"
                                Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                ValidationGroup="1"></asp:RegularExpressionValidator>                                                     
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>



                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                    Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="false"></asp:LinkButton>
                                     <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                                     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="1" ShowMessageBox="true"
                                      ShowSummary="false" />
                                       <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>

                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
                <asp:HiddenField ID="HiddenField1" runat="server" Visible="false" />
            </td>
        </tr>
        <tr id="trcatdoc1" runat="server" visible="false">
        <td colspan="3"> 
        <asp:label runat="server" ID="lblcategoryDoc" CssClass="formheading" Text="List of Documents related to Category"></asp:label>
        </td>
        </tr>
        <tr id="trcatdoc2" runat="server" visible="false">
        <td colspan="3" align="left">                               
                                <span style="color: red"> 
                <asp:Label ID="Label21" runat="server"                                
                Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>                        
                              </span>
        </td>
        </tr>
         <tr id="trcatdoc" runat="server" visible="false">
            <td align="center" colspan="3">
                <asp:GridView ID="grdcat" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,certificateReq,remarks"  Width="100%" 
                    onrowcommand="grdcat_RowCommand" onrowediting="grdcat_RowEditing" 
                    onrowdatabound="grdcat_RowDataBound" onrowupdating="grdcat_RowUpdating" onrowcancelingedit="grdcat_RowCancelingEdit"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Required">
                            <ItemTemplate>
                                <%# Eval("category")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Type">
                            <ItemTemplate>
                                <%# Eval("ctypename")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false" Target="_blank" />
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        
                             <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                            <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label> 
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine" Width="95%" 
                                ToolTip="Maximum 200 characters allowed" Height="80px" Visible="false" >
                               </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                ValidationGroup="1"></asp:RegularExpressionValidator>                                                     
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>


                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                    Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="false"></asp:LinkButton>
                                     <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                                     <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
              
            </td>
        </tr>
        <tr id="trsubcatdoc1" runat="server" visible="false">
        <td colspan="2">
        <asp:Label runat="server" ID="lblsubcategorydoc" CssClass="formheading" Text="List of Documents related to Sub Category"></asp:Label>
        </td>
        </tr>
        <tr id="trsubcatdoc2" runat="server" visible="false">
        <td colspan="3" align="left">                             
                <span style="color: red"> 
                <asp:Label ID="Label22" runat="server"             
                Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>                        
               </span>
            </td>
        </tr>
          <tr id="trsubcatdoc" runat="server" visible="false">
            <td align="center" colspan="3">
                <asp:GridView ID="grdsubcat" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" 
                    DataKeyNames="edmid,edid,certificateReq,subcategory,remarks"  Width="100%" 
                 onrowcommand="grdsubcat_RowCommand" onrowediting="grdsubcat_RowEditing" 
                    onrowdatabound="grdsubcat_RowDataBound" 
                    onrowupdating="grdsubcat_RowUpdating" onrowcancelingedit="grdsubcat_RowCancelingEdit"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Required">
                            <ItemTemplate>
                                <%# Eval("subcatname")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Certificate Type">
                            <ItemTemplate>
                                <%# Eval("ctypename")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                 <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false" Target="_blank" />
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                              <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                            <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label> 
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine" Width="95%" Height="80px" 
                                ToolTip="Maximum 200 characters allowed" Visible="false">
                               </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                ValidationGroup="1"></asp:RegularExpressionValidator>                                                     
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>


                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                    Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="false"></asp:LinkButton>
                                     <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                                     <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
              
            </td>
        </tr>
        <tr>
        <td colspan="3" id="trmisc1" runat="server" visible="false"> 
        <asp:Label ID="lblmisdoc" runat="server" CssClass="formheading" Text="List of Other Miscellaneous Documents (if any) (Maximum 7)"></asp:Label>
        </td>
        </tr>
        <tr>
         <td colspan="3" id="trmisc2" runat="server" visible="false" align="left"> 
       
                <span style="color: red"> 
                <asp:Label ID="Label23" runat="server"             
                
                 Text=" All Documents should be in PDF format only and Maximum size is 2MB for each document."></asp:Label>                        
               </span>
       
        </td>
        </tr>

        <tr id="trmisc" runat="server" visible="false">
            <td align="center" colspan="3">           
                <asp:GridView ID="grdmisc" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="gridfont" DataKeyNames="edmid,edid,othermiscdoc,remarks,editflag"  Width="100%" 
                 onrowcommand="grdmisc_RowCommand"  onrowdatabound="grdmisc_RowDataBound" 
                  onrowupdating="grdmisc_RowUpdating" 
                    OnRowCancelingEdit="grdmisc_RowCancelingEdit" 
                    onrowdeleting="grdmisc_RowDeleting"  >
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details of the Document">
                            <ItemTemplate>
                                <%# Eval("othermiscdoc")%>
                            </ItemTemplate>
                              <FooterTemplate>
                               <asp:TextBox ID="txtcertificateReq" runat="server"  Visible="false" AutoComplete="off" TextMode="MultiLine" Width="300px"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="revcertificateReq" runat="server" ControlToValidate="txtcertificateReq" Display="None" 
                         ErrorMessage="Invalid character in Certificate Req" ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="2"></asp:RegularExpressionValidator>
                          <asp:RequiredFieldValidator ID="rfvcertificateReq" runat="server" Display="None" ControlToValidate="txtcertificateReq"
                                    ErrorMessage="Please Enter Certificate Required" ValidationGroup="2"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Certificate Type">
                            <ItemTemplate>
                                <%# Eval("ctypename")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                            <asp:HyperLink ID="hyviewdoc" runat="server" ImageUrl="~/Images/pdf.png" Visible="false" Target="_blank" CausesValidation="true" />
                               <%-- <asp:HyperLink ID="hyviewdoc" runat="server" Text="View Document" Visible="false" Target="_blank"></asp:HyperLink>--%>
                                <asp:Label ID="lblfile" runat="server" Visible="false" Text=' <%# DataBinder.Eval(Container.DataItem,"edid") %>'></asp:Label>
                                <asp:FileUpload ID="fileupload" runat="server" />
                            </ItemTemplate>
                             <FooterTemplate>
                              <asp:FileUpload ID="fileupload" runat="server" Visible="false" />
                               <asp:RequiredFieldValidator ID="rfvfile" runat="server" Display="None" ControlToValidate="fileupload"
                                    ErrorMessage="Please select file" ValidationGroup="2"></asp:RequiredFieldValidator>
                              </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="400px" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                             <asp:Label runat="server" ID="lblremarks" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem,"remarks") %>'></asp:Label> 
                                <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine" Width="95%" Height="80px"
                                  ToolTip="Maximum 200 characters allowed" Visible="false">
                               </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                ValidationGroup="1"></asp:RegularExpressionValidator>                                             
                            </ItemTemplate>

                             <FooterTemplate>
                              <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine" Width="95%" Height="80px"
                                ToolTip="Maximum 200 characters allowed" Visible="false">
                               </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                ValidationGroup="1"></asp:RegularExpressionValidator>  
                              </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>



                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                              <%--  <asp:LinkButton ID="lbsave" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="Save"></asp:LinkButton>--%>
                                    <asp:LinkButton ID="lbupdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="Update" Visible="false"></asp:LinkButton>
                                     <asp:LinkButton ID="lbchange" runat="server" CausesValidation="False" CommandName="Change"
                                    Text="Change" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                                     <asp:LinkButton ID="lbremove" runat="server" CausesValidation="False" CommandName="Delete" Visible="false"
                                    Text="Remove" OnClientClick ="return confirm('Are you sure to delete this record?');"></asp:LinkButton>
                                      <asp:LinkButton ID="lnkbtncancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" Visible="false" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                            </ItemTemplate>
                             <FooterTemplate>
                                <asp:LinkButton ID="lnkadd" runat="server" CommandName="Add" Text="Add Another Document" ValidationGroup="2"></asp:LinkButton>
                              <asp:LinkButton ID="lnkIn" runat="server" CommandName="Insert" Text="Save" Visible="false" CausesValidation="true"
                                                                ValidationGroup="2" ></asp:LinkButton>
                                   

                                <asp:LinkButton ID="lnkC" runat="server" CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton>
                                  <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="2" ShowMessageBox="true"
             ShowSummary="false" />
                            </FooterTemplate>
                            <ItemStyle CssClass="gridfonts" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>


                     <EmptyDataTemplate>
                        <table class="validatorstyles" border="1" width="80%">
                            <tr class="gridheading" >
                                <td>
                                Certificate Required
                                </td>
                              
                               <td>&nbsp;
                               </td>
                               <td>
                               Remarks
                               </td>
                               </tr>
                               <tr>
                                <td >
                                 <asp:TextBox ID="txtcertificateReq" runat="server"  AutoComplete="off" TextMode="MultiLine" Width="300px"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="revcertificateReq" runat="server" ControlToValidate="txtcertificateReq" Display="None" 
                         ErrorMessage="Invalid character in Certificate Req" ValidationExpression="^[\sa-zA-Z0-9&.(),-]*$" ValidationGroup="3"></asp:RegularExpressionValidator>
                          <asp:RequiredFieldValidator ID="rfvcertificateReq" runat="server" Display="None" ControlToValidate="txtcertificateReq"
                                    ErrorMessage="Please Enter Certificate Required" ValidationGroup="3"></asp:RequiredFieldValidator>
                                 </td>
                               <td>  
                               <asp:FileUpload ID="fileupload" runat="server"  />
                               <asp:RequiredFieldValidator ID="rfvfile" runat="server" Display="None" ControlToValidate="fileupload"
                                    ErrorMessage="Please select file" ValidationGroup="3"></asp:RequiredFieldValidator>
                                    </td>
                                     <td>  
                              <asp:TextBox runat="server" ID="txtboxremarksvalue" MaxLength="200" TextMode="MultiLine" Width="95%" Height="80px"
                                  ToolTip="Maximum 200 characters allowed" Visible="true">
                               </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revgrddoc" runat="server" ControlToValidate="txtboxremarksvalue"
                                Display="Dynamic" ErrorMessage="Invalid Characters In Remarks" ValidationExpression="^[\sa-zA-Z0-9&amp;.(),-]*$"
                                ValidationGroup="1"></asp:RegularExpressionValidator>  
                                    </td>
                                <td>
                                
                                
                                   
                               <asp:LinkButton ID="lnkIn" runat="server" CommandName="EInsert" Text="Save"  CausesValidation="true"
                                        ValidationGroup="3"></asp:LinkButton>
                                 
                                   <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="3" ShowMessageBox="true"
             ShowSummary="false" />
                                </td>
                               
                            </tr>
                            
                        </table>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="gridheading" />
                </asp:GridView>
                <asp:HiddenField ID="HiddenField4" runat="server" Visible="false" />
            </td>
        </tr>

         <tr>
            <td colspan="3">
                <asp:Button Text="Final Submit" runat="server" ID="btnfinal" OnClick="btnfinal_Click" CssClass="buttonFormLevel"
                OnClientClick ="return confirm('Are you sure to Final Submit eDossier , No further editing will be allowed after Final Submit?');" Visible="false" />
            </td>
        </tr>
         <tr class="formlabel" id="trfinalnote" runat="server" visible="false">
                            <td align="left" colspan="3">
                               
                                <span style="color: red">#</span><asp:Label ID="Label1" runat="server" CssClass="formlabel"
                                    Text=" After Uploading all the documents, it is mandatory to click on the Final Submit Button. Failing which, your eDossier will not be valid."></asp:Label>
                           
                                <br />
                                </td>
                        </tr>
    </table>
</asp:Content>
