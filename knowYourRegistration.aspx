
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="knowYourRegistration.aspx.cs"
    Inherits="knowYourRegistration" %>

<%@ Register Src="~/usercontrols/Header.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/Footer.ascx" TagName="WebUserControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="./CSS/Applicant.css" />
    <script src="JS/jquery.min.js" type="text/javascript"></script>
    <script src="JS/validate-aadhar.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="Jscript/JScript.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.17.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="Jscript/md5.js"></script>
    <link rel="stylesheet" type="text/css" href="CSS/jquery-ui.css" />
    <style>
        * {
            box-sizing: border-box;
        }

        .zoom {
            /* padding: 50px;
            background-color: green;*/
            transition: transform .2s;
            width: 200px;
            height: 200px;
            margin: 0 auto;
        }

            .zoom:hover {
                -ms-transform: scale(5); /* IE 9 */
                -webkit-transform: scale(5); /* Safari 3-8 */
                transform: scale(5);
            }
    </style>
    <style>
        .divIDProofDoc {
            color: #003366;
        }
    </style>
    <script>
        function checkAddress() {
            debugger;
            var textBox = $.trim($('input[type=text]').val())
            if (textBox == "") {
                CheckBoxdisclaimer.checked = false;
                chkPreview.checked = false;
                alert("Complete your form before preview.");
            }
            else if (!CheckBoxdisclaimer.checked) {
                alert("Check to agree with the UNDERTAKING before preview.");
            }
            else {
                window.print();
            }
        }
    </script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8);
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("lblAadharVal").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>
    <script type="text/javascript">
        //preview fileupload control selected image
        function ShowImagePreview(input) {
            debugger;
            if (input.files && input.files[0]) {
                var ImageDir = new FileReader();
                ImageDir.onload = function (e) {
                    $('#imgPicture').attr('src', e.target.result);
                }
                ImageDir.readAsDataURL(input.files[0]);
            }
        }
    </script>

    <script>
        history.pushState(null, document.title, location.href);
        window.addEventListener('popstate', function (event) {
            history.pushState(null, document.title, location.href);
        });
    </script>

    <script>
        function validateInput() {
            var dd = document.getElementById("txt_dd").value;
            var mm = document.getElementById("txt_mm").value;
            var yyyy = document.getElementById("txt_yyyy").value;
            var name = document.getElementById("enterName").value;
            var roll = document.getElementById("txt_rollNo").value;

            if (dd.trim() === '' || mm.trim() === '' || yyyy.trim() === '' || name.trim() === '') {
                alert("All fields are required.");
                return false;
            }


            // Validate txt_dd for days only (1-31)
            if (isNaN(dd) || dd < 1 || dd > 31) {
                alert("Please enter a valid day (1-31) in DD field.");
                return false;
            }

            // Validate txt_mm for months only (1-12)
            if (isNaN(mm) || mm < 1 || mm > 12) {
                alert("Please enter a valid month (1-12) in MM field.");
                return false;
            }

            // Validate txt_yyyy for years only (numeric)
            if (isNaN(yyyy)) {
                alert("Please enter a valid year in YYYY field.");
                return false;
            }
            if (!/^\d+$/.test(roll)) {
                alert("Roll number must contain numbers only.");
                return false;
            }
            // Validate enterName for name only (alphabets and spaces)
            if (!/^[a-zA-Z\s]+$/.test(name)) {
                alert("Please enter a valid name.");
                return false;
            }

            return true; // All validations passed
        }
    </script>
     <script language="javascript" type="text/javascript">
         function SignValidateRefresh() {
             document.getElementById("<%=txtCode.ClientID%>").value = null;
         }
     </script>



    <title>DSSSBOnline</title>
    <style type="text/css">
        .style2 {
            width: 268435408px;
        }

        .auto-style1 {
            width: 446px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="OFF">

        <div id="dialog" title="Dialog Title" runat="server">
            <table width="80%" align="center">
                <tr>
                    <td>
                        <uc1:WebUserControl ID="Top1" runat="server" />
                    </td>

                </tr>
                <tr class="darkblue">
                    <td align="right" colspan="2" style="height: 21px">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="default.aspx">Back</asp:HyperLink>
                        <asp:Button ID="btn_back" runat="server" OnClick="btn_back_Click" Text="Back" Visible="False" />
                    </td>
                </tr>

                <%--////////////////////////////////////--%>

                <table class="border_gray" width="950px" align="center" id="Table1" runat="server"
                    visible="false">
                    <tr>
                        <td><br /></td>
                    </tr>
                    <tr align="center" valign="top" class="darkblue">
                        <td align="right" valign="top" style="font-size: large" class="auto-style1">
                            <asp:Label ID="Label1" runat="server" Text="Enter Your Registration Number : ">
                            </asp:Label>
                        </td>
                        <td id="TDTextbox" runat="server" style="width: 800px;" align="left">

                            <asp:TextBox ID="txt_dd" runat="server" Width="30px" ViewStateMode="Disabled" AutoCompleteType="Disabled" MaxLength="2" ValidationGroup="1" onkeyup="autoTab(this, document.form1.txt_mm)" onfocus="javascript:this.value=''" Height="30px" Style="font-size: 16px;"></asp:TextBox>

                            <asp:TextBox ID="txt_mm" runat="server" Width="30px" MaxLength="2" ViewStateMode="Disabled" AutoCompleteType="Disabled" ValidationGroup="1" onkeyup="autoTab(this, document.form1.txt_yyyy)" onfocus="javascript:this.value=''" Height="30px" Style="font-size: 16px;"></asp:TextBox>

                            <asp:TextBox ID="txt_yyyy" runat="server" MaxLength="4" ViewStateMode="Disabled" AutoCompleteType="Disabled" ValidationGroup="1" Width="60px" Height="30px" onkeyup="autoTab(this, document.form1.txt_rollNo)" onfocus="javascript:this.value=''" Style="font-size: 16px;"></asp:TextBox>

                            <asp:TextBox ID="txt_rollNo" runat="server" ValidationGroup="1" ViewStateMode="Disabled" AutoCompleteType="Disabled" MaxLength="15" Width="150px" onfocus="javascript:this.value=''" Style="font-size: 16px;" Height="30px"></asp:TextBox>

                            <asp:DropDownList ID="DropDownList_year" runat="server" ValidationGroup="1"
                                CssClass="textfield" Width="90px" Height="30px" Style="font-size: 16px;">
                            </asp:DropDownList><br />
                            &nbsp;&nbsp;DD &nbsp;&nbsp; MM &nbsp;&nbsp;&nbsp;&nbsp; YYYY &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Roll No.(10th)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Passing Year<br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Date of Birth) </td>
                    </tr>
                     <tr>
     <td><br /></td>
 </tr>
                    <tr align="center" valign="top" class="darkblue">
                        <td align="right" valign="top" style="font-size: large" class="auto-style1">
                            <asp:Label ID="Label2" runat="server" Text="Enter Your Name : ">
                            </asp:Label>
                        </td>
                        <td id="TD1" runat="server" style="width: 800px;" align="left">

                            <asp:TextBox ID="enterName" runat="server" Width="50%" Height="30px" CausesValidation="True"
                                Enabled="true" Style="font-size: 16px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="darkblue">
                        <td align="right" valign="top" style="font-size: large" class="auto-style1">
                            <asp:Label ID="Label3" runat="server" Text="Visual Code " style="margin-top:50%" >
                            </asp:Label>
                        </td>
                        <td valign="top" align="left" colspan="2">
                            <img width="150" height="50" alt="Visual verification" title="Please enter the Visual Code as shown in the image."
                                src="JpegImage_CS.aspx?r=<%= System.Guid.NewGuid().ToString("N") %>" vspace="5" />
                            <asp:ImageButton ToolTip="Click here to load a new Image" runat="server" ImageUrl="~/images/refresh.jpg"
                                ID="ibtnRefresh" OnClick="ibtnRefresh_Click" />
                        </td>
                    </tr>
                    <tr class="darkblue">
                        <td align="right" valign="top" style="font-size: large" class="auto-style1">
                            <asp:Label ID="Label4" runat="server" Text="Type the code shown ">
                            </asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox AutoCompleteType="None" oncopy="return false" oncut="return false" onpaste="return false"
                                ToolTip="Enter Above Characters in the Image" autocomplete="off" MaxLength="10"
                                ID="txtCode" runat="server" TabIndex="7" Height="20px" Style="font-size: 16px;"/>
                            <br />
                            <asp:RequiredFieldValidator ID="RFVCaptcha" runat="server" ControlToValidate="txtCode"
                                ErrorMessage="Enter Visual Code" ToolTip="Visual Code" ValidationGroup="1" SetFocusOnError="True"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr align="center">
                        <td>

                        </td>
                        <td align="left">
                           <asp:Button ID="btnupdatemobile" runat="server" Text="Get Detail" OnClientClick="return validateInput();" OnClick="searchRgstrDetail_Click"
    ValidationGroup="1" Width="80px" Height="30px" CssClass="cssbutton" />

                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <br />
                        </td>

                    </tr>


                    <%--  <tr align="left" valign="top" class="darkblue">
                        <td align="left" valign="top" style="font-size: large" class="auto-style1">
                            <asp:Label ID="rgstrId" runat="server" Text="Enter Registration Number: "></asp:Label>
                            <asp:TextBox ID="rgstrIdInput" runat="server" Width="25%" Height="30px" CausesValidation="True"
                                Enabled="true" AutoPostBack="true" Style="font-size: 16px;"></asp:TextBox>
                            <asp:Button ID="btnupdatemobile" runat="server" Text="Get Detail" OnClick="searchRgstrDetail_Click"
                                ValidationGroup="1" Width="80px" Height="30px" CssClass="cssbutton" />
                            <asp:RequiredFieldValidator ID="rfvmob" runat="server" Display="Dynamic" ControlToValidate="rgstrIdInput"
                                ValidationGroup="1" ErrorMessage="Please Enter Registration Number"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revmob" runat="server" Display="Dynamic" ControlToValidate="rgstrIdInput"
                                ValidationExpression="^[0-9]+$" ErrorMessage="Enter Only Numbers" ValidationGroup="1"></asp:RegularExpressionValidator>



                        </td>
                    </tr>--%>

                    <tr>
                        <td class="auto-style1">
                            <br />
                        </td>

                    </tr>

                </table>



                <tr>
                    <td>
                        <br>
                    </td>

                </tr>
                <tr>
                    <td>
                        <br>
                    </td>

                </tr>


                <%--////////////////////////////////////--%>
                <tr id="rgstrDetailTbl" runat="server">
                    <td>
                        <table class="border_gray" width="950px" align="center" id="tblshow" runat="server"
                            visible="false" style="font-size: 18px;">
                            <tr>
                                <td colspan="2" class="formlabel" align="center" style="border-bottom: 2px; border-bottom-color: steelblue; border-bottom-style: solid; font-size: 18px; background-color: lightgoldenrodyellow">Your Registration Detail
                                </td>
                            </tr>
                            <tr>
                                <td class="formlabel" align="left" width="50%" style="font-size: 18px">Registration No :
                                </td>
                                <td align="left">
                                    <asp:Label ID="registrationNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="formlabel" align="left" style="font-size: 18px">Name :
                                </td>
                                <td align="left" style="">
                                  <asp:Label ID="Cname" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="formlabel" align="left" style="font-size: 18px">Father's Name :
                                </td>
                                <td align="left">
                                    <asp:Label ID="fname" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="formlabel" align="left" style="font-size: 18px">Date of Birth :
                                </td>
                                <td align="left">
                                    <asp:Label ID="dob" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td class="formlabel" align="left" style="font-size: 18px">Mobile No. :
                                </td>
                                <td align="left">
                                    <asp:Label ID="mobNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="formlabel" align="left" style="font-size: 18px">Email :
                                </td>
                                <td align="left">
                                    <asp:Label ID="email" runat="server"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>

                </tr>
                <tr>
                    <td>
                        <br />
                    </td>

                </tr>
                <tr>
                    <td>
                        <br />
                    </td>

                </tr>
                <tr>
                    <td>
                        <uc2:WebUserControl ID="Footer" runat="server" />
                    </td>
                </tr>
            </table>
            <input id="csrftoken" name="csrftoken" runat="server" type="hidden" />
        </div>
    </form>
    <script>
        function space(el, after) {
            var v = el.value;
            if (v.match(/^\d{4}$/) !== null) {
                el.value = v + ' ';
            } else if (v.match(/^\d{4}\ \d{4}$/) !== null) {
                el.value = v + ' ';
            }
        }
        var el = document.getElementById('txtAdharNo');
        el.addEventListener('keyup', function () {
            space(this, 4);
        });
        var rel = document.getElementById('txtReAdharNo');
        rel.addEventListener('keyup', function () {
            space(this, 4);
        });
    </script>
</body>
</html>
