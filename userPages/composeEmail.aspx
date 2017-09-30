<%@ Page Language="C#" MasterPageFile="~/userPages/userLoggedIn.master" AutoEventWireup="true" CodeFile="composeEmail.aspx.cs" Inherits="userPages_compose" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="Server">
    
    <div>
        <h1>Mail Sending Portion</h1>
       
        <div style="border-color: white; border:4px; margin-bottom:40px; ">
           <table style="width: 600px; border: double solid; margin-left:7%;  border-color: white; background-color:cadetblue; text-transform:capitalize">
               <caption style="font-family: 'Lobster', cursive; font-size:20px"><b>Select Recipients</b></caption>
             <asp:Repeater ID="rptrCategory" runat="server">
                <ItemTemplate>
                    
                    
                        <tr>
                            <td style="font-family: 'Comfortaa', cursive;">
                                <asp:Label Width="100%" ID="lblCategoryName" Text='<%# Eval("categoryName") %>' runat="server"></asp:Label></td>
                            <td style="font-family: 'Source Code Pro', monospace;" >
                                <asp:CheckBoxList  ToolTip="" ID="cblRecipients" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="ID" RepeatDirection="Horizontal" RepeatColumns="4"></asp:CheckBoxList>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:RegistrationConnectionString %>' SelectCommand="SELECT [ID], [name],[email] FROM [tblRecipients] WHERE ([CategoryId] = @CategoryId)">
                                    
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hiddenfield" PropertyName="Value" Name="CategoryId"></asp:ControlParameter>
                                    </SelectParameters>
                                    <SelectParameters>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:HiddenField runat="server" ID="hiddenfield" Value='<%# Eval("ID") %>' />
                            </td>
                        </tr>
                    
                </ItemTemplate>
            </asp:Repeater>
            </table>
        </div>
        <table style="width: 807px">
            <caption style="font-family: 'Lobster', cursive;"><b>Enter Mail Credentials</b></caption>
            <tr>
                <td style="width: 165px; text-align: right">Subject</td>
                <td>
                    <asp:TextBox ID="tbxSubject" runat="server" placeholder="Enter subject here" Width="562px"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RFValidatorSubject" runat="server" ControlToValidate="tbxSubject" ErrorMessage="Mandatory Field" ValidationGroup="mailCredentials" ForeColor="Red"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 165px; text-align: right">
                    <asp:Label ID="lblMailBody" runat="server" Text="Body"></asp:Label></td>
                <td>
                    <asp:TextBox TextMode="MultiLine" Height="200px" placeholder="Enter mail's body here" runat="server" ID="tbxMailBody" Width="562px"></asp:TextBox></td>
                <td style="width: 416px">
                    <asp:RequiredFieldValidator ID="RFValidatorbody" ErrorMessage="mandatory field" ForeColor="red" runat="server" ValidationGroup="mailCredentials" ControlToValidate="tbxMailBody"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 165px; text-align: right">Password</td>
                <td>
                    <asp:TextBox ID="tbxPassword" TextMode="Password" runat="server" placeholder="Enter your registered mail's password here" Width="561px"></asp:TextBox></td>
                <td style="width: 416px">
                    <asp:RequiredFieldValidator ErrorMessage="Mandatory field" ForeColor="Red" ID="RFValidator" runat="server" ValidationGroup="mailCredentials" ControlToValidate="tbxPassword"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 165px">Select Template</td>
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
