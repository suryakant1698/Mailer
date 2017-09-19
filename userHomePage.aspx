<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userHomePage.aspx.cs" Inherits="users" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label runat="server" ID="Label1" Text="welcome ">
    </asp:Label><br />
        <table style="caption-side:top" >
            
            <tr>
                <td><asp:Label ID="lblCategoryAdder" runat="server" Text="Category" ></asp:Label>  </td>
                <td><asp:TextBox runat="server" ID="tbxCategoryName"></asp:TextBox>  </td>
                
                 </tr>
            <tr>
                <td><asp:Button ID="btnCategoryAdder" runat="server" Text="Submit" OnClick="btnCategoryAdder_Click"  />"</td>
            </tr>
        </table>
        <table style="width:50%; al " >  
            <caption><h1>Add Recipients</h1></caption>
            <tr>
                <td style="width:50%" > <asp:Label runat="server" ID="lblRecipientNAme" Text="Input Name"></asp:Label>  </td>
                <td><asp:TextBox ID="tbxRecipientName" runat="server"  ></asp:TextBox></td>

            
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblRecipientEmail" Text="Input Email"></asp:Label> </td>
                <td> <asp:TextBox runat="server" ID="tbxRecipientEmail" ></asp:TextBox> </td>
            </tr>
            <tr>
                <td> <asp:Label runat="server" ID="lblRecipientCategory" Text="Input Cattegory ID" ></asp:Label> </td>
                <td><asp:TextBox runat="server" db="tbxRecipientCategory"></asp:TextBox></td>
            </tr>
            <tr>
               <td><asp:Button runat="server" Text="Submit" ID="RecipientAdder" /> </td>
            </tr>
        </table>
        <asp:Button runat="server" ID="logoutButton" Text="Logout" OnClick="logoutButton_Click" />

    </div>
    </form>
</body>
</html>
