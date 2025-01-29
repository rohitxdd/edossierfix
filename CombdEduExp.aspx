<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="CombdEduExp.aspx.cs" Inherits="CombdEduExp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table style="width: 110%" border="0" cellpadding="0" cellspacing="0" align="left">
        <tr>
            <td colspan="2" align="right">
                 <asp:Button runat="server" ID="btn_back" Text="Back" align="right" CssClass="cssbutton" OnClick="btn_back_Click" />
            </td>
            &nbsp;
        </tr>
        <tr id="trtext" runat="server">
                        <td align="center" class="tr" style="padding-top: 10px; padding-bottom: 10px;">
                            <strong>List Of Department Under Combined Examination </strong>
                        </td>
                    </tr> 
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="grd_Dept" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Width="100%" CssClass="gridfont" DataKeyNames="reqid,deptcode">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblreqid" Text='<%# Bind("reqid") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbldeptcode" Text='<%# Bind("deptcode") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle Width="10px" VerticalAlign="Top" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbldeptname" Text='<%# Bind("deptname") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Name of Post">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_postname" Text='<%# Bind("JobTitle") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Minimum Age">
                            <ItemTemplate>
                                <asp:Label ID="lblminage" runat="server" Text='<%# Bind("MinAge") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Maxmimum Age">
                            <ItemTemplate>
                                <asp:Label ID="lblmaxage" runat="server" Text='<%# Bind("MaxAge") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gender">
                            <ItemTemplate>
                                <asp:Label ID="lbl_gender" runat="server" Text='<%# Bind("gender") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Essential Qualification">
                            <ItemTemplate>
                                <asp:Label ID="lbl_essential_edu" runat="server" Text='<%# Bind("essential_qual") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Desirable Qualification">
                            <ItemTemplate>
                                <asp:Label ID="lbl_desire_edu" runat="server" Text='<%# Bind("desire_qual") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Essential Experience">
                            <ItemTemplate>
                                <asp:Label ID="lbl_essential_exp" runat="server" Text='<%# Bind("essential_exp") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Desirable Experience">
                            <ItemTemplate>
                                <asp:Label ID="lbl_desire_exp" runat="server" Text='<%# Bind("desire_exp") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Please select relevant Department in which you want to apply">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chk_select" OnCheckedChanged="chk_select_CheckedChanged" AutoPostBack="true" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gridfont" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       <tr>
           <td>
               &nbsp;
               &nbsp;
               &nbsp;
           </td>
       </tr>
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="btn_save" Text="Save and Next" align="center" CssClass="cssbutton" OnClick="btn_save_Click" />
                &nbsp; 
                 &nbsp;  
                &nbsp;
                <asp:Button runat="server" ID="Btn_next" Text="Update and Next" align="center" CssClass="cssbutton" OnClick="Btn_next_Click"/>
            </td>
        </tr>
    </table>
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
