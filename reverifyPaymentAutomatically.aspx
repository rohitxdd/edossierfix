<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reverifyPaymentAutomatically.aspx.cs" Inherits="reverifyPaymentAutomatically" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script type="text/javascript" language="javascript">

        function submitForm() {
            // 
		//debugger;
            var flg = document.getElementById('<%= Flag.ClientID %>').value;
            //alert(flg);
            if (flg == 'P') {
                                document.form1.action = "https://epayment.delhigovt.nic.in/epay.aspx"
            }
            else if (flg == 'V') {
                
                document.form1.action = "https://epayment.delhigovt.nic.in/everify.aspx"
            }
           
            document.form1.submit();
        }
    </script>
</head>
<body onload="submitForm();">
    <form id="form1" method="post" runat="server">
    <div>
        <input type="hidden" name="SchemeId" id="SchemeId" runat="server" />
        <input type="hidden" name="EncParam" id="EncParam" runat="server" />
        <input type="hidden" name="Flag" id="Flag" runat="server" />
    </div>
    </form>
</body>
</html>
