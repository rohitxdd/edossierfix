<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatePicker.aspx.cs" Inherits="Control_DatePicker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Date Picker</title>
    <link href="../App_Themes/MainStyles.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
        function SetDate(dateValue,dateValue1)
        {
            var pos = 0;
            // retrieve from the querystring the value of the Ctl param,
            // that is the name of the input control on the parent form
            // that the user want to set with the clicked date
            var length = 0;
            length = window.location.search.substr(1).indexOf('&');
            if(length>0)
            {
                ctl = window.location.search.substr(1).substring(4,length);
                var startindex = 4+length+2;
                length = window.location.search.substr(1).lastIndexOf('&'); 
                cntrl = window.location.search.substr(1).substring(startindex,length);
                
            }
            else
            {
                ctl = window.location.search.substr(1).substring(4);
                cntrl=-1;
            }
            for(i=0;i<window.opener.document.forms[0].elements.length;i++)
            {
                if(window.opener.document.forms[0].elements[i].id.toString().indexOf(ctl) > 0)
                    {
                        pos = i;
                        break;
                    }
            }
            // set the value of that control with the passed date
            thisForm = window.opener.document.forms[0].elements[pos].value = dateValue;
            if(cntrl!=-1)
            {
            for(i=0;i<window.opener.document.forms[0].elements.length;i++)
            {
                if(window.opener.document.forms[0].elements[i].id.toString().indexOf(cntrl) > 0)
                    {
                        pos = i;
                        break;
                    }
            }
            // set the value of that control with the passed date
            thisForm = window.opener.document.forms[0].elements[pos].value = dateValue1;
            }
            // close this popup
            self.close();
        }
        </script>
</head>
<body style="margin-top :0;">
    <form id="form1" runat="server">
    <div>
      <!-- Main Table (start) -->
       <table id="tblDatePicker" cellpadding="0" cellspacing="0" align="center" border =0>
         <tr align="center">
           <td align="center">
             <!-- Inner table (start) -->
              <table id="tblInnerDatePicker" cellpadding="0" cellspacing="0" width="100%" border =0>
                <tr>
                  <td class="fonts" style="height: 33px">Year<asp:DropDownList AutoPostBack="true" ID="ddlYear" CssClass="fonts" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="65px">
                     </asp:DropDownList>
                  </td>
                  <td class="fonts" style="height: 33px">
                 <!--     Month<asp:DropDownList AutoPostBack="true" CssClass="fonts" ID="ddlMonth" runat="server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Width="100px">
                     </asp:DropDownList>-->
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                   <!-- Calender (start) -->
                     <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC"
                          BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana"
                          Font-Size="8pt" ForeColor="#003399" Height="200px" OnSelectionChanged="Calendar1_SelectionChanged1"
                          Width="220px">
                          <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                          <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                          <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                          <WeekendDayStyle BackColor="#CCCCFF" />
                          <OtherMonthDayStyle ForeColor="#999999" />
                          <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                          <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                          <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                              Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                      </asp:Calendar>
                    <!-- Calender (end) -->
                  </td>
                </tr>
              </table>
             <!-- Inner table (end) -->
           </td>
         </tr>
       </table>
       <!-- Main Table (end) -->
      </div>
    </form>
</body>
</html>
