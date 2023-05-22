<%@ Page Title="AccountVerification" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountVerification.aspx.cs" Inherits="CASIITInformationWebsite.Pages.AccountVerification" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>


    </div>

    <%--<div class="jumbotron">
        <div>
            <asp:Table runat="server" Visible="true" ID="PersonTypeTable">
                <asp:TableRow>
                    <asp:TableCell Width="200px">
                        <asp:Button runat="server" Text="Student" BorderWidth="1" BackColor="LightGray" OnClick="PersonType_Click" ID="Student" Width="200px" Height="200px" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Button runat="server" Text="Counselor" BorderWidth="1" BackColor="LightGray" OnClick="PersonType_Click" ID="Counselor" Width="200px" Height="200px"/>
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Button runat="server" Text="Admin" BorderWidth="1" BackColor="LightGray" OnClick="PersonType_Click" ID="Admin" Width=" 200px" Height="200px"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:LinkButton runat="server" ForeColor="LightBlue" Font-Size="Smaller" OnClick="SignIn_Click">Already have an account? Sign in</asp:LinkButton>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableFooterRow>
                    <asp:TableCell>
                        <asp:LinkButton runat="server" ForeColor="LightBlue" Font-Size="Smaller" OnClick="Guest_Click">Continue as guest</asp:LinkButton>
                    </asp:TableCell>
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
    </div>--%>
</asp:Content>
