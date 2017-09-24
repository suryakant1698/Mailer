<%@ Page Language="C#" MasterPageFile="~/userPages/userLoggedIn.master" AutoEventWireup="true" CodeFile="composeEmail.aspx.cs" Inherits="userPages_compose" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="Server">
   

    <asp:Repeater runat="server" ID="htmlTemplateDisplay"></asp:Repeater>
    <div>
        <table style="width: 807px">
            <tr>
                <td colspan="3" style="text-align: center">
                    <h1>Mail Sending Portion</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label Text="Email" runat="server" ID="lblRecipientEmail"></asp:Label></td>
                <td style="width: auto">
                    <asp:TextBox ID="tbxRecipientEmail" placeholder="Enter Recipient's email here" runat="server" Width="562px"></asp:TextBox>
                </td>
                <td style="width: 416px">
                    <asp:RequiredFieldValidator ID="RFValidatorEmail" runat="server" ControlToValidate="tbxRecipientEmail" ErrorMessage="mandatory field" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Subject</td>
                <td><asp:TextBox ID="tbxSubject" runat="server" Width="558px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RFValidatorSubject" runat="server" ControlToValidate="tbxSubject" ErrorMessage="Mandatory Field" ForeColor="Red"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblMailBody" runat="server" Text="Body"></asp:Label></td>
                <td><asp:TextBox TextMode="MultiLine" Height="200px" placeholder="Enter mail's body here" runat="server" ID="tbxMailBody" Width="562px"></asp:TextBox></td>
                <td style="width: 416px"><asp:RequiredFieldValidator ID="RFValidatorbody" ErrorMessage="mandatory field" ForeColor="red" runat="server" ControlToValidate="tbxMailBody"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Password</td>
                <td><asp:TextBox ID="tbxPassword" runat="server" placeholder="Enter your registered mail's password here" Width="561px"></asp:TextBox></td>
                <td style="width: 416px"><asp:RequiredFieldValidator ErrorMessage="Mandatory field" ForeColor="Red" ID="RFValidator" runat="server" ControlToValidate="tbxPassword"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center"><asp:Button ID="btnSend" Width="100px" Height="50px" Text="Send" runat="server" OnClick="btnSend_Click" /></td>
            </tr>
        </table>

    </div>
</asp:Content>
