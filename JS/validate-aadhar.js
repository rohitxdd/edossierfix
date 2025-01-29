// multiplication table d
var d = [
    [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
    [1, 2, 3, 4, 0, 6, 7, 8, 9, 5],
    [2, 3, 4, 0, 1, 7, 8, 9, 5, 6],
    [3, 4, 0, 1, 2, 8, 9, 5, 6, 7],
    [4, 0, 1, 2, 3, 9, 5, 6, 7, 8],
    [5, 9, 8, 7, 6, 0, 4, 3, 2, 1],
    [6, 5, 9, 8, 7, 1, 0, 4, 3, 2],
    [7, 6, 5, 9, 8, 2, 1, 0, 4, 3],
    [8, 7, 6, 5, 9, 3, 2, 1, 0, 4],
    [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]
];
// permutation table p
var p = [
    [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
    [1, 5, 7, 6, 2, 8, 3, 0, 9, 4],
    [5, 8, 0, 3, 7, 9, 6, 1, 4, 2],
    [8, 9, 1, 6, 0, 4, 3, 5, 2, 7],
    [9, 4, 5, 3, 1, 2, 6, 8, 7, 0],
    [4, 2, 8, 6, 5, 7, 3, 9, 0, 1],
    [2, 7, 9, 3, 8, 0, 6, 4, 1, 5],
    [7, 0, 4, 6, 9, 1, 3, 2, 5, 8]
];
// inverse table inv
var inv = [0, 4, 3, 2, 1, 5, 6, 7, 8, 9];
// converts string or number to an array and inverts it
function invArray(array) {
    if (Object.prototype.toString.call(array) === "[object Number]") {
        array = String(array);
    }
    if (Object.prototype.toString.call(array) === "[object String]") {
        array = array.split("").map(Number);
    }
    return array.reverse();
}
// generates checksum
function generate(array) {
    var c = 0;
    var invertedArray = invArray(array);
    for (var i = 0; i < invertedArray.length; i++) {
        c = d[c][p[((i + 1) % 8)][invertedArray[i]]];
    }
    return inv[c];
}
// validates checksum
function validate(array) {
    var c = 0;
    var invertedArray = invArray(array);
    for (var i = 0; i < invertedArray.length; i++) {
        c = d[c][p[(i % 8)][invertedArray[i]]];
    }
    return (c === 0);
}
$(document).ready(function () {

    $('body').bind('cut copy paste', function (e) {
        e.preventDefault();
    });
    $('#txt_DOB').datepicker({
        dateFormat: 'dd/mm/yy',
        changeMonth: true,
        changeYear: true,
        yearRange: '1950:2100'
    });
    $('#txtReDOB').datepicker({
        dateFormat: 'dd/mm/yy',
        changeMonth: true,
        changeYear: true,
        yearRange: '1950:2100'
    });
    $('#txtAdharNo').blur(function (e) {
        var inputVal = $("#" + e.target.id).val();
        inputVal = inputVal.replace(/ /gi, '');
        var adno = $.isNumeric(inputVal);
        if (adno) {
            if ($("#" + e.target.id).val().length == 12) {

                if (validate($("#" + e.target.id).val())) {
                    if ($('#').length == 0) {
                        $("#validAdhar").remove();
                        $($("#" + e.target.id).parent()).append("<span id='validAdhar' style='color:#01c245;'>Valid Aadhar</span>");
                    }
                }
                else {
                    //$($("#" + e.target.id).parent()).remove();
                    $("#validAdhar").remove();
                    $($("#" + e.target.id).parent()).append("<span id='validAdhar' style='color: red;'>Invalid Aadhar</span>");
                }
            }

        }
    });
    //    $('#txtReAdharNo').blur(function (e) {
    //        var adhar = $("#txtAdharNo").val();
    //        adhar = adhar.replace(/ /gi, '');
    //        var readhar = $("#txtReAdharNo").val();
    //        readhar = readhar.replace(/ /gi, '')
    //        var adno = $.isNumeric(readhar);
    //        if (adno) {
    //            if (adhar == readhar) {

    //                $("#validreAdhar").remove();
    //                $($("#" + e.target.id).parent()).append("<span id='validreAdhar' style='color: #01c245;'>Valid Aadhar</span>");
    //            }
    //            //            else {
    //            //                $("#validreAdhar").remove();
    //            //                $($("#" + e.target.id).parent()).append("<span id='validreAdhar' style='color: red;'>The aadhaar number and its confirm field are not the same</span>");
    //        }
    //        //        else {
    //        //            $("#validreAdhar").remove();
    //        //            $($("#" + e.target.id).parent()).append("<span id='validreAdhar' style='color: red;'>The aadhaar number can be numeric only</span>");

    //        //        }
    //    });

    //    $('#txtReNameAdhar').blur(function (e) {
    //        var adName = $("#txtNameAdhar").val();
    //        var readName = $("#txtReNameAdhar").val();
    //        if (readName == adName) {
    //            $("#validADName").remove();
    //        }
    //        else {
    //            $("#validADName").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validADName' style='color: red;'>The value is not same as Aadhaar name.</span>");
    //        }
    //    });

    //    $('#txtReIdNumber').blur(function (e) {
    //        var reId = $("#txtReIdNumber").val();
    //        var id = $("#txtIdNumber").val();
    //        if (reId == id) {
    //            $("#validIdNo").remove();
    //        }
    //        else {
    //            $("#validIdNo").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validIdNo' style='color: red;'>The value is not same as ID Proof.</span>");
    //        }
    //    });

    //    $('#txtReNameIDProof').blur(function (e) {
    //        var reNId = $("#txtReNameIDProof").val();
    //        var NId = $("#txtNameIDProof").val();
    //        if (reNId == NId) {
    //            $("#validNId").remove();
    //        }
    //        else {
    //            $("#validNId").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validNId' style='color: red;'>The value is not same as name on ID Proof.</span>");
    //        }
    //    });

    //    $('#txtReName').blur(function (e) {
    //        var rename = $("#txtReName").val();
    //        var name = $("#txt_name").val();
    //        if (rename == name) {
    //            $("#validRename").remove();
    //        }
    //        else {
    //            $("#validRename").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validRename' style='color: red;'>The the value is not same as above name field.</span>");
    //        }
    //    });

    //    $('#txtReDOB').blur(function (e) {
    //        var dob = $("#txt_DOB").val();
    //        var redob = $("#txtReDOB").val();
    //        if (redob == dob) {
    //            $("#validDOB").remove();
    //        }
    //        else {
    //            $("#validDOB").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validDOB' style='color: red;'>The the value is not same as above DOB field.</span>");
    //        }
    //    });

    //    $('#txtReMName').blur(function (e) {
    //        var mname = $("#txt_mothername").val();
    //        var reMname = $("#txtReMName").val();
    //        if (reMname == mname) {
    //            $("#validMname").remove();
    //        }
    //        else {
    //            $("#validMname").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validMname' style='color: red;'>The value is not same as above Mother's name field.</span>");
    //        }
    //    });

    //    $('#txtRSpouse').blur(function (e) {
    //        var SPN = $("#txtspouse").val();
    //        var reSPN = $("#txtRSpouse").val();
    //        if (reSPN == SPN) {
    //            $("#validSPN").remove();
    //        }
    //        else {
    //            $("#validSPN").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validSPN' style='color: red;'>The value is not same as above Spouce name field.</span>");
    //        }
    //    });

    //    $('#txtReMobNo').blur(function (e) {
    //        var mob = $("#txt_mob").val();
    //        var reMob = $("#txtReMobNo").val();
    //        if (reMob == mob) {
    //            $("#validMob").remove();
    //        }
    //        else {
    //            $("#validMob").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validMob' style='color: red;'>The value is not same as above mobile.</span>");
    //        }
    //    });

    //    $('#txtReEntEmail').blur(function (e) {
    //        var email = $("#txt_email").val();
    //        var reEmail = $("#txtReEntEmail").val();
    //        if (reEmail == email) {
    //            $("#validEmail").remove();
    //        }
    //        else {
    //            $("#validEmail").remove();
    //            $($("#" + e.target.id).parent()).append("<span id='validEmail' style='color: red;'>The value is not same as above email.</span>");
    //        }
    //    });

    $('#txt_roll_no').blur(function (e) {
        var roll = $("#txt_roll_no").val();
        var rollR = roll.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
        roll = rollR.replace(/^0+/, '');
        $("#txt_roll_no").val(roll);
        var validRoll = $.isNumeric(roll);

        if (rollR != roll) {
            $("#txt_roll_no").val("");
            $($("#" + e.target.id).parent()).append("<span id='validRollno' style='color: red;'>Please do not enter or prefix zero in class X roll no as system will truncate all leading zero from left automatically.</span>");
        }
        else {
            $("#validRollno").remove();
        }
    });
    $('#txtReEntRollNo').blur(function (e) {
        var roll = $("#txtReEntRollNo").val();
        var rollR = roll.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
        roll = rollR.replace(/^0+/, '');
        $("#txtReEntRollNo").val(roll);
        var validRoll = $.isNumeric(roll);

        if (rollR != roll) {
            $("#txtReEntRollNo").val("");
            $($("#" + e.target.id).parent()).append("<span id='validReRollno' style='color: red;'>Please do not enter or prefix zero in class X roll no as system will truncate all leading zero from left automatically.</span>");
        }
        else {
            $("#validReRollno").remove();
        }
    });

    $('#rbtReSelectGend').blur(function (e) {
        debugger;
        var rSelGen = $("#rbtReSelectGend").val();
        var SelGen = $("#RadioButtonList_mf").val();
        if (rSelGen != SelGen) {
            var msg = "Selected gender is not same as above";
            $('#lblGender').show();
            $('#lblGender').text(msg);
        }
        else {
            $('#lblGender').hide();
        }
    });
});


