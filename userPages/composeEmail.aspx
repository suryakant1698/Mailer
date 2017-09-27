<%@ Page Language="C#" MasterPageFile="~/userPages/userLoggedIn.master" AutoEventWireup="true" CodeFile="composeEmail.aspx.cs" Inherits="userPages_compose" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="Server">


    <asp:Repeater runat="server" ID="htmlTemplateDisplay"></asp:Repeater>
    <div>


        <h1>Mail Sending Portion</h1>




        <h3>Select Recipients</h3>

        <div style="border-color:white; border:4px;">
        <asp:Repeater ID="rptrCategory" runat="server">
            <ItemTemplate>
                <%#  Eval("categoryName")%>
                <%# Eval("ID") %>
                <table style="width: 600px; border: double solid; border-color: white">


                    <tr>
                        <td>
                            <asp:Label Width="50%" ID="lblCategoryName" Text='<%# Eval("categoryName") %>' runat="server"></asp:Label></td>
                        <td>
                            <asp:CheckBoxList ID="cblRecipients" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="ID"></asp:CheckBoxList>
                            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:RegistrationConnectionString %>' SelectCommand="SELECT [ID], [name] FROM [tblRecipients] WHERE ([CategoryId] = @CategoryId)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="hiddenfield" PropertyName="Value" Name="CategoryId"></asp:ControlParameter>
                                </SelectParameters>
                                <SelectParameters>
                                </SelectParameters>

                            </asp:SqlDataSource>
                            <asp:HiddenField runat="server" ID="hiddenfield" Value='<%# Eval("ID") %>' />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
            </div>
        <table style="width: 807px">
            <tr>
                <td style="width: 79px; text-align: right">
                    <asp:Label Text="Email" runat="server" ID="lblRecipientEmail"></asp:Label></td>
                <td style="width: auto">
                    <asp:TextBox ID="tbxRecipientEmail" placeholder="Enter Recipient's email here" runat="server" Width="562px"></asp:TextBox></td>
                <td style="width: 416px">
                    <asp:RequiredFieldValidator ID="RFValidatorEmail" runat="server" ControlToValidate="tbxRecipientEmail" ErrorMessage="mandatory field" ValidationGroup="mailCredentials" ForeColor="Red"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 79px; text-align: right">Subject</td>
                <td>
                    <asp:TextBox ID="tbxSubject" runat="server" placeholder="Enter subject here" Width="562px"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RFValidatorSubject" runat="server" ControlToValidate="tbxSubject" ErrorMessage="Mandatory Field" ValidationGroup="mailCredentials" ForeColor="Red"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 79px; text-align: right">
                    <asp:Label ID="lblMailBody" runat="server" Text="Body"></asp:Label></td>
                <td>
                    <asp:TextBox TextMode="MultiLine" Height="200px" placeholder="Enter mail's body here" runat="server" ID="tbxMailBody" Width="562px"></asp:TextBox></td>
                <td style="width: 416px">
                    <asp:RequiredFieldValidator ID="RFValidatorbody" ErrorMessage="mandatory field" ForeColor="red" runat="server" ValidationGroup="mailCredentials" ControlToValidate="tbxMailBody"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 79px; text-align: right">Password</td>
                <td>
                    <asp:TextBox ID="tbxPassword" TextMode="Password" runat="server" placeholder="Enter your registered mail's password here" Width="561px"></asp:TextBox></td>
                <td style="width: 416px">
                    <asp:RequiredFieldValidator ErrorMessage="Mandatory field" ForeColor="Red" ID="RFValidator" runat="server" ValidationGroup="mailCredentials" ControlToValidate="tbxPassword"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 79px">Select Template</td>
                <td>
                    <asp:DropDownList ID="ddlTemplateSelector" Width="100px" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="btnSend" Width="100px" Height="50px" Text="Send" runat="server" OnClick="btnSend_Click" /></td>
            </tr>
        </table>

    </div>
</asp:Content>
