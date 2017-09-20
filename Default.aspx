<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
		*{
			background-color: #172f3a;
		}
		.box{
			width: 300px;
			height: 300px;
			position: absolute;
			left: 0;right: 0;top: 0;bottom: 0;margin: auto;
			background: linear-gradient(#29ABE2,#1C667F);
			border-left-width: 24px;
			border-color: white;
			padding: 20px;
			border-radius: 20px
		}
		.login , .register{

			width: 150px;
			height: 50px;
			color: #ccc;
			font-size: 25px;
			font-family: halvetica;
			margin:55px 65px;
			font-family: georgia;
			border-radius: 10px;
			cursor: pointer;
			font-weight: bolder;
			font-style: italic;
		}
		.login:hover, .register:hover {background-color: #33383a; }
		
		}
        </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <svg  viewBox="0 0 600 600" >
	<polygon points="150,50 150,240 450,240 450,50" fill="#eee" />
<polygon points="150,50 300,180 450,50" fill="grey" /></svg>
	
     <div class="box">
     	
         <asp:Button ID="btnLogin" runat="server"  Text="Login" CssClass="login" OnClick="btnLogin_Click"/>.
     	
         <asp:Button CssClass="register" ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
      	
     </div>
    </div>
    </form>
</body>
</html>
