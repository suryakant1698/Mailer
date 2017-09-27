<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css files/LoginPage.css" />
    <title></title>
</head>
<body>
    <div class="body">
        <h1>Login Page</h1>
        <form id="form1" runat="server">
            <div class="imageContainer">
                <img src="images/avatara.png" alt="Avatar" class="avatar" />
            </div>
            <div class="inputArea">
                <div class="mainBody">
                    <b>
                        <asp:Label ID="lblUsername" CssClass="lbl" runat="server" Text="username/email"></asp:Label></b>
                </div>
                <div>
                    <asp:TextBox ID="tbxUsername" CssClass="tbx" runat="server" placeholder="Enter Username or email Here"></asp:TextBox>
                </div>
                <div>
                    <b>
                        <asp:Label ID="lblPassword" CssClass="lbl" runat="server" Text="Password"></asp:Label></b>
                </div>
                <div>
                    <asp:TextBox ID="tbxPassword" CssClass="tbx" runat="server" placeholder="Enter Password Here" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div style="width:400px">
                <asp:Button Text="Log In" CssClass="btnLogin" runat="server" ID="btnLogin" OnClick="btnLogin_Click" />    
                </div>

                <div style="margin-top:10px">
                              
            <a href="register.aspx"><input class="btnLogin"  type="button" name="button" value="Register"/></a>
        </div>
                </form>
    </div>
</body>
</html>
