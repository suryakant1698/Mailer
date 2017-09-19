<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="practiceRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <style>
         #mainTable
         {
             margin-left: 30%;
             margin-top:10%;
         }
     </style>
    <title></title>
</head>
   
<body>
    
    <form id="form1" runat="server">
    <div id="mainTable">
     <table style="caption-side:top "> 
        <tr >
            <td><asp:Label runat="server" ID="lblUsername" Text="Username" ></asp:Label></td>
            <td><asp:TextBox  ID="tbxUsername" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxUsername" ErrorMessage="mendatory field" ForeColor="Red"></asp:RequiredFieldValidator>
                     </td>        
            
        </tr>
        <tr>
               <td><asp:Label ID="lblEmail" runat="server" Text="Email" ></asp:Label></td>
            <td><asp:TextBox ID="tbxEmail" runat="server" ></asp:TextBox>
                 <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxEmail" ErrorMessage="mendatory field" ForeColor="Red"></asp:RequiredFieldValidator> 
                 </td>
           </tr>
        <tr>
            <td><asp:Label ID="lblFullName" runat="server" Text="Full Name" ></asp:Label></td>
            <td><asp:TextBox ID="tbxFullName" runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="mendatory field" ControlToValidate="tbxFullName" ForeColor="red"></asp:RequiredFieldValidator>
            </td>
        </tr>
           
        <tr>
            <td><asp:Label runat="server" ID="lblPassword" Text="Password" ></asp:Label></td>
            <td><asp:TextBox ID="tbxPassword" runat="server"   TextMode="Password" ></asp:TextBox>
             <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxPassword" ErrorMessage="mendatory field" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label runat="server" ID="lblConfirmPassword" Text="Confirm Password" ></asp:Label></td>
            <td><asp:TextBox id="tbxConfirmPassword" runat="server"  ViewStateMode="Disabled" TextMode="Password" EnableViewState="False"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxConfirmPassword"  ErrorMessage="mendatory field" ForeColor="Red"></asp:RequiredFieldValidator>
           </td>
            <td>   
           <%--  <asp:Label ID="lblCompareValidator" runat="server" Text=" "></asp:Label>--%>
                      </td>
            s
            <td>&nbsp;</td>
    </tr>
         <tr>
             <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /></td>
         </tr>
           
    </table>
    </div>
    </form>
</body>
</html>
