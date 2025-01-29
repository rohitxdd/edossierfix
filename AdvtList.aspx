<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdvtList.aspx.cs" Inherits="AdvtList" Title="Advertisement Lists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    <script type="text/javascript">
        function Confirm(url) {
            var result = confirm("You have already applied for this post.Click \"OK\" to check Status")
            
            if (result) {
             window.location=url;
            }
            else {
             return false;   
            }
        }

    </script>
    <table cellpadding="0"  cellspacing="0" style="width: 1000px; height: 100%" border="0" align="center">
<tr>
                        <td valign="top" style="height:100%" align="center">
                            <br />
                            <div id="Div1" runat="server">
                                
                                <asp:GridView ID="grdsplpost" runat="server" AutoGenerateColumns="False" Height="100%"
                                    Width="100%" DataKeyNames="advt_no,postcode,jobtitle,dobfrom,dobto,jobid,endson,AdYear,AdNo,adid,flag,gender,JobDescription,reqid,endson_org,agevalidationexmpt" 
                                      OnRowEditing="grdsplpost_RowEditing"  CssClass="gridfont" 
                                    OnRowDataBound="grdsplpost_RowDataBound" 
                                    Caption="Old Vacancies">
                                     
                                    <Columns>
                                    <asp:BoundField DataField="JobSourceID" HeaderText="JobSourceID" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AdYear" HeaderText="AdvtYear" Visible="False">
                                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AdNo" HeaderText="AdvtNo" Visible="False">
                                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                  
                                    <asp:BoundField DataField="advt_no" HeaderText="Advt No" Visible="false">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />                                    
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Adv. Details">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%"  VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Center"/>
                                    <ItemTemplate>
                                   <asp:HyperLink ID="hyplimage" runat="server" ImageUrl="~/Images/pdf.png" Target="_blank"></asp:HyperLink>
                                   <%-- <asp:ImageButton id="img" runat="server" ImageUrl="~/Images/pdf.png" AlternateText="Download Advt"/>                                                                                                                                        --%>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Post Code">
                                    <ItemTemplate>
                                     <asp:HyperLink ID="hypadv" runat="server" Text='<%# Bind("PostCode") %>' Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>  <ControlStyle CssClass="gridfonts" />
                                    <ItemStyle CssClass="gridfonts" VerticalAlign="Top" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="JobTitle" HeaderText="Name of Post">
                                      <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="StartsFrom" HeaderText="Opening Date (dd/mm/yyyy)">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndsOn" HeaderText="Closing Date (dd/mm/yyyy)">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FeelastDate" HeaderText="Last Date of Fee Depositing by Challan (dd/mm/yyyy)" Visible="false">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fee" HeaderText="Fees" Visible="false">
                                     <HeaderStyle HorizontalAlign="Left" Width="10%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="essential_qual" HeaderText="Essential Qualification" Visible="False">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="essential_exp" HeaderText="Essential Experience" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    
                                    
                                    <asp:TemplateField >
                                    <HeaderStyle HorizontalAlign="Left" Width="10%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    <ItemTemplate>
                                    <asp:Button ID="BtnApply" runat="server" CommandName="Edit" Text="Apply Now" CssClass="cssbutton" Visible="false">
                                    </asp:Button>
                                    </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    </Columns>
                                  <HeaderStyle CssClass="gridheading" />
                                    </asp:GridView>
                            </div>
                        </td>
                    </tr>
       
                    <tr>
                        <td valign="top" style="height:100%" align="center">
                            <br />
                            <div id="AppList" runat="server">
                                
                                <asp:GridView ID="dgJobList" runat="server" AutoGenerateColumns="False" Height="100%"
                                    Width="100%" DataKeyNames="advt_no,postCode,jobtitle,dobfrom,dobto,jobid,endson,AdYear,AdNo,adid,flag,gender,JobDescription,reqid,endson_org" 
                                    OnRowCommand="dgJobList_RowCommand" OnRowEditing="dgJobList_RowEditing"  CssClass="gridfont" 
                                    OnRowDataBound="dgJobList_RowDataBound" Caption="Current Notifications">
                                     
                                    <Columns>
                                    <asp:BoundField DataField="JobSourceID" HeaderText="JobSourceID" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AdYear" HeaderText="AdvtYear" Visible="False">
                                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AdNo" HeaderText="AdvtNo" Visible="False">
                                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                  
                                    <asp:BoundField DataField="advt_no" HeaderText="Advt No" Visible="false">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />                                    
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Adv. Details">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%"  VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Center"/>
                                    <ItemTemplate>
                                   <asp:HyperLink ID="hyplimage" runat="server" ImageUrl="~/Images/pdf.png" Target="_blank"></asp:HyperLink>
                                   <%-- <asp:ImageButton id="img" runat="server" ImageUrl="~/Images/pdf.png" AlternateText="Download Advt"/>                                                                                                                                        --%>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Post Code">
                                    <ItemTemplate>
                                     <asp:HyperLink ID="hypadv" runat="server" Text='<%# Bind("PostCode") %>' Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>  <ControlStyle CssClass="gridfonts" />
                                    <ItemStyle CssClass="gridfonts" VerticalAlign="Top" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="JobTitle" HeaderText="Name of Post">
                                      <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="StartsFrom" HeaderText="Opening Date (dd/mm/yyyy)">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndsOn" HeaderText="Closing Date (dd/mm/yyyy)">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FeelastDate" HeaderText="Last Date of Fee Depositing by Challan (dd/mm/yyyy)">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fee" HeaderText="Fees">
                                     <HeaderStyle HorizontalAlign="Left" Width="10%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="essential_qual" HeaderText="Essential Qualification" Visible="False">
                                     <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="essential_exp" HeaderText="Essential Experience" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" Width="15%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    </asp:BoundField>
                                    
                                    
                                    <asp:TemplateField >
                                    <HeaderStyle HorizontalAlign="Left" Width="10%"  VerticalAlign="Top" />
                                    <ItemStyle CssClass="gridfont" />
                                    <ItemTemplate>
                                    <asp:Button ID="BtnApply" runat="server"  CommandName="Edit" Text="Apply Now" CssClass="cssbutton">
                                    </asp:Button>
                                    </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    </Columns>
                                  <HeaderStyle CssClass="gridheading" />
                                    </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                 <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
</asp:Content>

