<%@ Page Title="FAQ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="CASIITInformationWebsite.FAQ" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <div id="nav-placeholder">

    </div>

    <script>
    $(function(){
        $("#nav-placeholder").load("nav.aspx");
    });
    </script>
</asp:Content>



