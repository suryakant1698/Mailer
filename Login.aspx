<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="text-align:right">
        <tr>
            <td><asp:Label ID="lblUsername" runat="server" Text="username/email" ></asp:Label></td>
            <td><asp:TextBox ID="tbxUsername" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label></td>
            <td><asp:TextBox ID="tbxPassword" runat="server" TextMode="Password" ></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Button Text="Log In" runat="server" ID="btnLogin" OnClick="btnLogin_Click" /></td>
        </tr>
        <tr>
            <td><h1>if not already registered</h1></td>
            <td><asp:Button runat="server" ID="btnRegister" Text="Register" OnClick="btnRegister_Click" /></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
