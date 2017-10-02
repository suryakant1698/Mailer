<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appHomePage.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css files/appHomePage.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="login-box">
  <div class="left">
    <h1>Sign up</h1>
    
    <%--<input type="text" name="username" placeholder="Username" />
    <input type="text" name="email" placeholder="E-mail" />
    <input type="password" name="password" placeholder="Password" />
    <input type="password" name="password2" placeholder="Retype password" />
    
    <input type="submit" name="signup_submit" value="Sign me up" />--%>
      <asp:TextBox ID="tbxUsername" CssClass="textboxes" runat="server" placeholder="Username"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFValidatorUsername" ControlToValidate="tbxUsername" runat="server" ErrorMessage="*Mandatory field" ForeColor="red" ValidationGroup="register"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbxEmail" CssClass="textboxes" runat="server" placeholder="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFValidatorEmail" runat="server" ControlToValidate="tbxEmail" ErrorMessage="*Mandatory field" ValidationGroup="register" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Display="dynamic" ID="validateEmail" runat="server" ForeColor="Red" ErrorMessage="*Invalid email." ValidationGroup="register" ControlToValidate="tbxEmail" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                           <asp:TextBox ID="tbxFullName" CssClass="textboxes" runat="server" placeholder="Full Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="tbxFullName" Display="dynamic" ForeColor="Red" ID="RFValidatorFullName" ValidationGroup="register" runat="server" ErrorMessage="*Mandatory field"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbxPassword" CssClass="textboxes" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValidatorPassword" runat="server" Display="Dynamic" ControlToValidate="tbxPassword" ValidationGroup="register" ErrorMessage="*Mandatory field" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegExp1" runat="server" Display="Dynamic" Font-Size="small" ErrorMessage="*Password should contain atleasst:one upper case,one lower case alphabet.one number and one special symbol" ValidationGroup="register" ControlToValidate="tbxPassword" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{7,10}$" />
                        <asp:TextBox ID="tbxComfrimPassword" CssClass="textboxes" runat="server" ValidationGroup="register" placeholder="ConfirmPassword" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValidatorConPassword" ValidationGroup="register" runat="server" Display="Dynamic" ControlToValidate="tbxComfrimPassword" ErrorMessage="*Mandatory field" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:CompareValidator Display="Dynamic" ValidationGroup="register" ControlToValidate="tbxComfrimPassword" ErrorMessage="*Password did not match" ForeColor="Red" ControlToCompare="tbxPassword" ID="cmValidator" runat="server"></asp:CompareValidator>
                        <asp:Button ID="btnSubmit" CssClass="button" Text="submit" runat="server" OnClick="btnSubmit_Click" />
                        
  </div>
  
  <div class="right">
      <h1>Login</h1>
     <asp:TextBox ID="tbxLoginUsername"  CssClass="textboxes" runat="server" placeholder="Enter Username or email Here"></asp:TextBox>
     <asp:RequiredFieldValidator ValidationGroup="login" ID="RFValidatorUSer" runat="server" ControlToValidate="tbxLoginUsername" ErrorMessage="*Mandatory Field" ForeColor="Red"></asp:RequiredFieldValidator>
                 <asp:TextBox ID="tbxLoginPassword" CssClass="textboxes" runat="server" placeholder="Enter Password Here" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="login" ID="RFValidatorPass" runat="server" ControlToValidate="tbxLoginPassword" ErrorMessage="*Mandatory Field" ForeColor="Red"></asp:RequiredFieldValidator>
               <asp:Button CssClass="button" ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
   
  </div>
  <div class="or">OR</div>
</div>
    </form>
</body>
</html>
