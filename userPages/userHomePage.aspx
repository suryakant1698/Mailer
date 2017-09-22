<%@ Page Language="C#" MasterPageFile="~/userPages/userLoggedIn.master" AutoEventWireup="true" CodeFile="userHomePage.aspx.cs" Inherits="users" %>




<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" runat="Server">

    <div>
        <asp:Label runat="server" ID="Label1" Text="welcome ">
        </asp:Label><br />
        <table style="caption-side: top">
            <tr>
                <td colspan="2" style="text-align: center">
                    <h1>Add Category</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCategoryAdder" runat="server" Text="Category"></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxCategoryName" ValidationGroup="Category"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVtbxCategoryName" ErrorMessage="mendatory field" ControlToValidate="tbxCategoryName" ForeColor="Red" runat="server" ValidationGroup="Category"></asp:RequiredFieldValidator>
                </td>
                <asp:Label ID="lblCategoryID" runat="server"></asp:Label>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCategoryAdder" runat="server" Text="Submit" OnClick="btnCategoryAdder_Click" />"</td>
            </tr>
        </table>
    </div>


    <div>
        <table style="width: 80%; al">
            <caption>
                <h1>Add Recipients</h1>
            </caption>
            <tr>
                <td style="width: 50%">
                    <asp:Label runat="server" ID="lblRecipientNAme" Text="Input Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxRecipientName" placeholder="name" runat="server" ValidationGroup="Recipient"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="Recipient" ErrorMessage="mendatory field" ControlToValidate="tbxRecipientName" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblRecipientEmail" Text="Input Email"></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxRecipientEmail" ValidationGroup="Recipient"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="Recipient" ErrorMessage="mendatory field" ControlToValidate="tbxRecipientEmail" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegexEmail" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" ControlToValidate="tbxRecipientEmail" ErrorMessage="Invalid Email" ForeColor="red"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblRecipientCategory" Text="Input Cattegory name"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategoryName" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" Text="Submit" ID="RecipientAdder" OnClick="RecipientAdder_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


