<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeFile="ViewMarks.aspx.cs" Inherits="ViewMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <table style="text-align: center" width="100%">
        <tr runat="server" id="trddlexam" visible="false">
            <td align="left">
                <asp:Label ID="lblappno" runat="server" Text="Select Exam." CssClass="formheading"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlexam" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlexam_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tratndnc" runat="server" visible="false">
            <td colspan="2" align="center" cssclass="formheading">
                You have not Appeared in this Exam.
            </td>
        </tr>
          <tr id="trnotinmarks" runat="server" visible="false">
            <td colspan="2" align="center" cssclass="formheading">
                --
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table id="tbl1" width="100%" runat="server" visible="false">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Result" CssClass="formheading"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <%-- <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="Post Applied " CssClass="formheading"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="lblposts" runat="server" Text="" CssClass="formheading"></asp:Label>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr runat="server" id="TRUC" visible="false">
            <td colspan="2">
                <div runat="server" id="div1">
                    <table id="td1"  runat="server" class="formheading" border="1">
                        <tr>
                            <td align="left" style="width: 220px">
                                Exam ID
                            </td>
                            <td align="left">
                                <asp:Label ID="lblExamid" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 220px">
                                Post Applied
                            </td>
                            <td align="left">
                                <asp:Label ID="lblpostcode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 220px">
                                Roll Number
                            </td>
                            <td align="left">
                                <asp:Label ID="lblrollno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 220px">
                                Name
                            </td>
                            <td align="left">
                                <asp:Label ID="lblname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 220px">
                                DOB
                            </td>
                            <td align="left">
                                <asp:Label ID="lbldob" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 220px">
                                Marks Obtained
                            </td>
                            <td align="left">
                            <asp:Label ID="lbls1marks" runat="server"></asp:Label><br />
                            <asp:Label ID="lbls2marks" runat="server"></asp:Label><br />
                                <asp:Label ID="lblmrksobtd" runat="server"></asp:Label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 220px">
                                Remarks
                            </td>
                            <td align="left">
                                <asp:Label ID="lblremarks" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="trmessage">
                            <td align="left" style="width: 220px">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr runat="server" id="trmsgnextexam">
                            <td align="left" style="width: 220px">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Label ID="lblmsgnextexam" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="Button1" runat="server" Text="Print" CssClass="buttonFormLevel" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnapplid" runat="server" />
    <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>
