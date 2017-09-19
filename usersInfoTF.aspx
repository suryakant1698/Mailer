<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usersInfoTF.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [tblUsers]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
   </div>
         <asp:GridView ID="DataGrid1" runat="server" Height="236px" Width="421px" AutoGenerateColumns="False" DataSourceId="SqlDataSource1" GridLines="Horizontal">
        
    <columns >
        <asp:TemplateField>
            <ItemTemplate>
                <table>
                   <tr>
                       <td>Id</td>
                       <td>
                           <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                       </td>
                   </tr> 
                   <tr>
                       <td>username</td>
                       <td>
                           <asp:Label ID="lblusername" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                       </td>
                   </tr> 
                   <tr>
                       <td>Email</td>
                       <td>
                           <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                       </td>
                   </tr> 
                   <tr>
                       <td>fullname</td>
                       <td>
                           <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("fullname") %>'></asp:Label>
                       </td>
                   </tr> 
                   <tr>
                       <td>Password</td>
                       <td>
                           <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("upassword") %>'></asp:Label>
                       </td>
                   </tr> 
                </table>
            </ItemTemplate>
        </asp:TemplateField>
   </columns>
        </asp:GridView>
         
        
    </form>
</body>
</html>
