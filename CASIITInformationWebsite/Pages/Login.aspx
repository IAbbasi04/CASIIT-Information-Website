<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CASIITInformationWebsite.Pages.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div>
            <asp:Table runat="server" id="LoginTable">
                <asp:TableRow>
                    <asp:TableCell Width="200">
                        <h style="font-size:large">Login to an existing account</h>
                    </asp:TableCell>
                    <asp:TableCell Width="500">
                    </asp:TableCell>
                    <asp:TableCell Width="200">
                        <h style="font-size:large">Create a new account</h>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <h>Username</h>
                        <asp:TextBox runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell>
                        <h>First Name</h>
                        <asp:Textbox runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <h>Password</h>
                        <asp:TextBox runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="PasswordLabel" runat="server" Visible="false" Font-Size="Small" ForeColor="IndianRed">Incorrect Password</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell>
                        <h>Last Name</h>
                        <asp:Textbox runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Height="50px">
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell>
                        <h>Email</h>
                        <asp:TextBox runat="server" ID="EmailTextBox"/>
                        <asp:Label ID="EmailLabel" runat="server" Visible="false" Font-Size="Small" ForeColor="IndianRed">Not a valid email</asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button runat="server" Text="Submit" BorderWidth="1" BackColor="LightGray" OnClick="Login_Click"/>
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                     <asp:TableCell>
                        <asp:Button runat="server" Text="Submit" BorderWidth="1" BackColor="LightGray" OnClick="SignUp_Click"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
</asp:Content>