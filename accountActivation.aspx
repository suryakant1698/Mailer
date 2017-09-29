<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accountActivation.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <br />
        <h1>click here to loginn</h1>
        <asp:Button ID="btnRedirect" runat="server" OnClick="redirect" Text="click" />
    </div>
    </form>
</body>
</html>
