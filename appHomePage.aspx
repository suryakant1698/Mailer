<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appHomePage.aspx.cs" Inherits="appHomePage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css files/appHomePage.css"
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <svg viewBox="0 0 600 600">
                <polygon points="150,50 150,240 450,240 450,50" fill="#eee" />
                <polygon points="150,50 300,180 450,50" fill="grey" />
            </svg>
            <div class="box">
                <a href="Login.aspx" class="login"><input class="login" type="button" name="button" onclick="" value="Login"/>    	
  </a>
             <a href="register.aspx" class="register">   
              <input class="register"  type="url"  name="button" value="Register" />
             </a>
        </div>
    </form>
</body>
</html>
