<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="practiceRegister" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css files/RegisterPage.css" />
    <title></title>
    <style type="text/css">
        #rfv {
            height: 46px;
            margin-top: 31px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <div id="login-box">
                    <div class="left">
                        <h1>Sign up</h1>
                        <asp:TextBox ID="tbxUsername" CssClass="textboxes" runat="server" placeholder="Username"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValidatorUsername" ControlToValidate="tbxUsername" runat="server" ErrorMessage="mendatory field" ForeColor="red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbxEmail" CssClass="textboxes" runat="server" placeholder="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValidatorEmail" runat="server" ControlToValidate="tbxEmail" ErrorMessage="mendatory field" ForeColor="Red"></asp:RequiredFieldValidator>
                      <%--  <asp:RegularExpressionValidator ID="RegexvalidatorEmail" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w))+)$" runat="server" ControlToValidate="tbxEmail" ErrorMessage="Invalid Email" ForeColor="red"></asp:RegularExpressionValidator>
                     --%>   <asp:TextBox ID="tbxFullName" CssClass="textboxes" runat="server" placeholder="Full Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="tbxFullName" ID="RFValidatorFullName" runat="server" ErrorMessage="mendatory field"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbxPassword" CssClass="textboxes" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValidatorPassword" runat="server" ControlToValidate="tbxPassword" ErrorMessage="mendatory field" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegExp1" runat="server" ErrorMessage="Password too weak" ControlToValidate="tbxPassword" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{7,10}$" />
                        <asp:TextBox ID="tbxComfrimPassword" CssClass="textboxes" runat="server" placeholder="ConfirmPassword" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValidatorConPassword" runat="server" ControlToValidate="tbxComfrimPassword" ErrorMessage="mendatory field" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate="tbxComfrimPassword" ControlToCompare="tbxPassword" ID="cmValidator" runat="server"></asp:CompareValidator>
                        <asp:Button ID="btnSubmit" CssClass="button" Text="submit" runat="server" OnClick="btnSubmit_Click" />
                        
           <a href="Login.aspx"><input class="button" type="button" name="button" value="Login"/></a>
                    </div>
                </div>
        </div>
    </form>
</body>
</html>
