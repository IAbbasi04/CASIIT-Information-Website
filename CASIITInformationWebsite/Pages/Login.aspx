<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CASIITInformationWebsite.Pages.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .sign-up-button {
            border-radius:50px;
            border-width:1px;
            background-color:lightgray;
            width:200px;
            height:200px;
        }

        .sign-up-button:hover {
            background-color:aliceblue;
        }
    </style>

    <div class="jumbotron">
        <div>
            <asp:Table runat="server" Visible="true" ID="PersonTypeTable">
                <asp:TableRow>
                    <asp:TableCell Width="200px">
                        <asp:Button runat="server" Text="Sign up as a Student" OnClick="PersonType_Click" ID="Student" class="sign-up-button"/>
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Button runat="server" Text="Sign up as a Counselor" OnClick="PersonType_Click" ID="Counselor" class="sign-up-button"/>
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Button runat="server" Text="Sign up as an Admin" OnClick="PersonType_Click" ID="Admin" class="sign-up-button"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:LinkButton runat="server" ForeColor="Purple" Font-Size="Large" OnClick="SignIn_Click" Width="400px">Already have an account? Sign in</asp:LinkButton>
                        <asp:LinkButton runat="server" ForeColor="Purple" Font-Size="Large" OnClick="Guest_Click" Width="400px">Continue as guest</asp:LinkButton>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableFooterRow>
                </asp:TableFooterRow>
            </asp:Table>

            <asp:Table runat="server" Visible="false" ID="SignUpTable">
                <asp:TableRow>
                    <asp:TableCell Width="100px">
                        <h>First Name</h>
                        <asp:TextBox runat="server" ID="FirstNameTextBox"></asp:TextBox>
                        <asp:Label runat="server" Font-Size="Smaller" ForeColor="IndianRed" ID="SignUpFirstNameLabel" Visible="false">Enter a valid name</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="100px"/>
                    <asp:TableCell Width="100px">
                        <h>Last Name</h>
                        <asp:TextBox runat="server" ID="LastNameTextBox"></asp:TextBox>
                        <asp:Label runat="server" Font-Size="Smaller" ForeColor="IndianRed" ID="SignUpLastNameLabel" Visible="false">Enter a valid name</asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Height="50px"/>
                <asp:TableRow>
                </asp:TableRow>
                <asp:TableRow Height="50px"/>
                <asp:TableRow>
                    <asp:TableCell Width="100px">
                        <h>Email</h>
                        <asp:TextBox runat="server" ID="SignUpEmailTextBox"></asp:TextBox>
                        <asp:Label runat="server" Font-Size="Smaller" ForeColor="IndianRed" ID="SignUpEmailLabel" Visible="false">Enter a valid email</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="100px"/>
                    <asp:TableCell Width="100px">
                        <h>Password</h>
                        <asp:TextBox runat="server" ID="SignUpPasswordTextBox" TextMode="Password"></asp:TextBox>
                        <asp:Label runat="server" Font-Size="Smaller" ForeColor="IndianRed" ID="SignUpPasswordLabel" Visible="false">Enter a valid password</asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Height="50px"/>
                <asp:TableRow>
                </asp:TableRow>
                <asp:TableRow Height="50px"/>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button runat="server" Text="Submit" BorderWidth="1" BackColor="LightGray" OnClick="SignUp_Click"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table runat="server" Visible="false" ID="LoginTable">
                <asp:TableRow>
                    <asp:TableCell Width="100px">
                        <h>Email</h>
                        <asp:TextBox runat="server" ID="LoginEmailBox"/>
                        <asp:Label runat="server" ID="LoginEmailLabel" ForeColor="IndianRed" Font-Size="Smaller" Visible="false">Not a valid email</asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="100px">
                        <h>Password</h>
                        <asp:TextBox runat="server" ID="LoginPasswordBox" TextMode="Password"/>
                        <asp:Label runat="server" id="PasswordLabel" ForeColor="IndianRed" Font-Size="Smaller" Visible="false">Incorrect Password</asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Height="50px" />
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button runat="server" Text="Submit" BorderWidth="1" BackColor="LightGray" OnClick="Login_Click"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
</asp:Content>