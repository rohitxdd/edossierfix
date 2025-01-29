// JScript File


function PopupPicker(ctl,w,h)
{

    var PopupWindow=null;
    settings='width='+ w + ',height='+ h + ',location=no,directories=no,menubar=no,toolbar=no,status=no,scrollbars=no,resizable=no,dependent=no';        
    PopupWindow=window.open('DatePicker.aspx?Ctl=' + ctl,'DatePicker',settings);
    PopupWindow.focus();
}


function show(str)
{
if (str=='')
{
document.getElementById("txtHint").innerHTML='';
}
else
{
document.getElementById("txtHint").visible=true;
xmlhttp=GetXmlHttpObject();
if (xmlhttp==null)
  {
  alert ("Your browser does not support AJAX!");
  return;
  }
var url="time.asp?q="+str;
//url=url+"?q="+str;
//url=url+"&sid="+Math.random();
//alert(url);
xmlhttp.onreadystatechange=stateChanged;
xmlhttp.open("GET",url,true);
xmlhttp.send(null);
}
}
function stateChanged()
{
if (xmlhttp.readyState==4)
  {
  document.getElementById("txtHint").innerHTML=xmlhttp.responseText;
  }
}

function GetXmlHttpObject()
{
if (window.XMLHttpRequest)
  {
  // code for IE7+, Firefox, Chrome, Opera, Safari
  return new XMLHttpRequest();
  }
if (window.ActiveXObject)
  {
  // code for IE6, IE5
  return new ActiveXObject("Microsoft.XMLHTTP");
  }
return null;
}
