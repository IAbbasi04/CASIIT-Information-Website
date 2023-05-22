<%@ Page Title="ForgotPassword" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="CASIITInformationWebsite.Pages.ForgotPassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <asp:UpdatePanel runat="server" ID="upForgotPassword">
            <ContentTemplate>
                <%--<asp:Label ID="lbForgotPasswordEmail" runat="server" Text="Forgot Password?" </asp:Label>--%>
                    
                <%--<asp:Label ID="tbForgotPasswordEmail" runat="server" Text="Label"></asp:Label>--%>

                <%--<asp:Button ID="bSendForgotPasswordEmailRequest" runat="server" Text="Send" OnClick="bSendForgotPasswordEmailRequest_Click"/>--%>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>