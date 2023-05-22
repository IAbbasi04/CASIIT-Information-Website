<%@ Page Title="FacultyAndStaff" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FacultyAndStaff.aspx.cs" Inherits="CASIITInformationWebsite.FacultyAndStaff" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="nav-placeholder">

    </div>

    <script>
        $(function () {
            $("#nav-placeholder").load("nav.aspx");
        });
    </script>

    <asp:Table ID="Table1" CellPadding="30" runat="server">
        <asp:TableRow>
            <asp:TableCell cellspacing="10px" BorderWidth="20px" BorderColor="White" cellpadding="10px" RowSpan="4">
                <asp:Image Height="370" Width="300" ID="Image1" runat="server" src="../images/BishopGD.jpg" /></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label1" Font-Size="XX-Large" runat="server" Text="Name"></asp:Label>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right" cellspacing="10px" BorderWidth="20px" BorderColor="White" cellpadding="10px" RowSpan="4">
                <asp:Image Height="370" Width="300" ID="Image2" runat="server" src="../images/BishopGD.jpg" /></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label5" Font-Size="XX-Large" runat="server" Text="Name"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" Text="Courses"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label6" runat="server" Text="Courses"></asp:Label></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label7" runat="server" Text="Email"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label4" runat="server" Text="Phone number"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label8" runat="server" Text="Phone number"></asp:Label></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell cellspacing="10px" BorderWidth="20px" BorderColor="White" cellpadding="10px" RowSpan="4">
                <asp:Image Height="370" Width="300" ID="Image3" runat="server" src="../images/BishopGD.jpg" /></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label9" Font-Size="XX-Large" runat="server" Text="Name"></asp:Label>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right" cellspacing="10px" BorderWidth="20px" BorderColor="White" cellpadding="10px" RowSpan="4">
                <asp:Image Height="370" Width="300" ID="Image4" runat="server" src="../images/BishopGD.jpg" /></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label10" Font-Size="XX-Large" runat="server" Text="Name"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label11" runat="server" Text="Courses"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label12" runat="server" Text="Courses"></asp:Label></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label13" runat="server" Text="Email"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label14" runat="server" Text="Email"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label15" runat="server" Text="Phone number"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label16" runat="server" Text="Phone number"></asp:Label></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell cellspacing="10px" BorderWidth="20px" BorderColor="White" cellpadding="10px" RowSpan="4">
                <asp:Image Height="370" Width="300" ID="Image5" runat="server" src="../images/BishopGD.jpg" /></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label17" Font-Size="XX-Large" runat="server" Text="Name"></asp:Label>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right" cellspacing="10px" BorderWidth="20px" BorderColor="White" cellpadding="10px" RowSpan="4">
                <asp:Image Height="370" Width="300" ID="Image6" runat="server" src="../images/BishopGD.jpg" /></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label18" Font-Size="XX-Large" runat="server" Text="Name"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label19" runat="server" Text="Courses"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label20" runat="server" Text="Courses"></asp:Label></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label21" runat="server" Text="Email"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label22" runat="server" Text="Email"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label23" runat="server" Text="Phone number"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label24" runat="server" Text="Phone number"></asp:Label></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell cellspacing="10px" BorderWidth="20px" BorderColor="White" cellpadding="10px" RowSpan="4">
                <asp:Image Height="370" Width="300" ID="Image7" runat="server" src="../images/BishopGD.jpg" /></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label25" Font-Size="XX-Large" runat="server" Text="Name"></asp:Label>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right" cellspacing="10px" BorderWidth="20px" BorderColor="White" cellpadding="10px" RowSpan="4">
                <asp:Image Height="370" Width="300" ID="Image8" runat="server" src="../images/BishopGD.jpg" /></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label26" Font-Size="XX-Large" runat="server" Text="Name"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label27" runat="server" Text="Courses"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label28" runat="server" Text="Courses"></asp:Label></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label29" runat="server" Text="Email"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label30" runat="server" Text="Email"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label31" runat="server" Text="Phone number"></asp:Label></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label32" runat="server" Text="Phone number"></asp:Label></asp:TableCell>
        </asp:TableRow>
    </asp:Table>



</asp:Content>
