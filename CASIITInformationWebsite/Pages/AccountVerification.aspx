<%@ Page Title="AccountVerification" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountVerification.aspx.cs" Inherits="CASIITInformationWebsite.Pages.AccountVerification" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div style="position:relative; left:100px;">
        <asp:UpdatePanel runat="server" ID="upCreateAccount">
            <ContentTemplate>
                <asp:Table runat="server" ID="tableCreateAccount">
                    <%--Context to Page--%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lbAccountVerificationHeader" runat="server" ForeColor="White" Font-Size="XX-Large"><br />Let's Get Your Account Verified</asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <%--Add spacing--%>
                            <div style="padding-bottom:25px;"> 
                                <asp:Label ID="lbAccountVerificationDescription" runat="server" ForeColor="White" Width="1000" Font-Size="Large"><br />An email was sent to [email] with a verification code. Copy the code and paste it into the box below. Click "Confirm Account" with the correct code to use the features in our website. If you did not receive the email, click "Resend Code" to receive another email. You may need to check your spam.</asp:Label>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>

                    <%--User interaction--%>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox ID="tbAccountVerificationInputCode" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="bConfirmAccount" runat="server" Text="Confirm Account" OnClick="bConfirmAccount_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="bResendCode" runat="server" Text="Resend Code" OnClick="bResendCode_Click"/>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
