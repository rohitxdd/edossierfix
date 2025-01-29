cc=0;
var xmlHttp

function showHint(str)
{ 
//alert(this.event.keyCode);
if (this.event.keyCode != 40) 
{

if (str.length > 0)
{ 
	var url="gethint.aspx?sid=" + Math.random() + "&q=" + str
	xmlHttp=GetXmlHttpObject(stateChanged)
	xmlHttp.open("GET", url , true)
	xmlHttp.send(null)
} 
else
{ 
	
	document.getElementById("txthint").innerHTML=""
	
} 
}
else 
{
//alert(document.getElementById("txtHint2").innerHTML);
//var c=document.getElementById("txtHint2").innerHTML;

//var c=document.getElementById("txtHint2").innerHTML;
//alert(document.getElementById("txtHint2").innerHTML)
//document.getElementById("txtHint1").focus();
//var len=c.length;
//alert(len)
//document.getElementById("txt1").value=c;
//var ch=c.indexOf('$');
//var ch1=c.indexOf('#');

//document.getElementById("txt1").value=c.substring(ch+1,ch1);

/*var col_array=c.split("#");
var part_num=0;
while (part_num < col_array.length)
 {
	//alert(col_array[part_num]);
	var c2 = c.substring(col_array[part_num].indexOf('$')+1,col_array[part_num].length);
	//alert(c2)
	//document.getElementById("txt1").value=
	
  part_num+=1;
  }
// alert(document.getElementById("txtHint1").innerHTML);//style object of currently selected element
 */
 }

}
function stateChanged() 
{ 
if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{ 
		
		var c=xmlHttp.responseText;
		var col_array=c.split("!")
	    document.getElementById("txthint").innerHTML=col_array[0].substring(0,col_array[0].length-2);
	/*alert('Hint 2 :'+document.getElementById("txtHint2").innerHTML)
	var c=xmlHttp.responseText;
	
	var col_array=c.split("!")
	
	var fn=col_array[0].substring(0,col_array[0].length-2)
	alert('fn'+fn);
	document.getElementById("txtHint2").innerHTML=fn;	
	alert('Hint After :'+document.getElementById("txtHint2").innerHTML)*/
	//var s =split(xmlHttp.responseText,'^')
	} 
} 

function GetXmlHttpObject(handler)
{ 
var objXmlHttp=null
if (navigator.userAgent.indexOf("Opera")>=0)
{
	alert("This example doesn't work in Opera") 
	return 
}
if (navigator.userAgent.indexOf("MSIE")>=0)
{ 
var strName="Msxml2.XMLHTTP"
	if (navigator.appVersion.indexOf("MSIE 5.5")>=0)
	{
		strName="Microsoft.XMLHTTP"
	} 
	try
	{ 
		objXmlHttp=new ActiveXObject(strName)
		objXmlHttp.onreadystatechange=handler 
		return objXmlHttp
	} 
	catch(e)
	{ 
		alert("Error. Scripting for ActiveX might be disabled") 
		return 
	} 
} 
if (navigator.userAgent.indexOf("Mozilla")>=0)
{
objXmlHttp=new XMLHttpRequest()
objXmlHttp.onload=handler
objXmlHttp.onerror=handler 
return objXmlHttp
}
} 