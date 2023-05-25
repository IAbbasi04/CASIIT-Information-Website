<%@ Page Title="CourseCatalog" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseCatalogue.aspx.cs" Inherits="CASIITInformationWebsite.CourseCatalogue" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 style="font-size:100px; font-weight:bold; vertical-align:bottom; color:black" align="center">
        Course Catalog
    </h1>

    <p style="font-size:30px; font-weight:bold; color:black; text-underline-position:from-font;" align="left">
        Select Filters
    </p>

    <asp:Label ID="Label2" runat="server" Text="Concentration: " Font-Bold="true">
    </asp:Label>

    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>

    <asp:Label ID="Label5" runat="server" Text="Year: " Font-Bold="true">
    </asp:Label>

    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:Label ID="Label1" runat="server" Text="Enter GPA: " Font-Bold="true">
    </asp:Label>

    <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged">
    </asp:TextBox>

    <asp:Label ID="Label3" runat="server" Text="Enter a valid GPA between 0 and 5" ForeColor = "Red" Visible="false">
    </asp:Label>

    <asp:CheckBox ID="CheckBox1" runat="server">
    </asp:CheckBox>

    <asp:Label ID="Label4" runat="server" Text="Show Classes I can take">
    </asp:Label>

    <p>
    </p>

    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Button ID="GoButton" runat="server" Text="GO" BorderWidth="1" BackColor="LightGray" Width="100px" Height="50px" OnClick="GoButton_Click"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <p>
    </p>

    

    <asp:Table ID="Table6" runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell Height="50px" Width="250px" BorderWidth="2" BackColor="LightGray" HorizontalAlign="Center" VerticalAlign="Middle">
                    <p style="color:blueviolet; font-weight:bold;  ">
                        Course Name
                    </p>
                </asp:TableCell>
                <asp:TableCell Height="50px" Width="350px" BorderWidth="2" BackColor="LightGray" HorizontalAlign="Center" VerticalAlign="Middle">
                    <p style="color:blueviolet; font-weight:bold;  ">
                        Course Description
                    </p>
                </asp:TableCell>
                <asp:TableCell Height="50px" Width="100px" BorderWidth="2" BackColor="LightGray" HorizontalAlign="Center" VerticalAlign="Middle">
                    <p style="color:blueviolet; font-weight:bold;  ">
                        Minimum Year
                    </p>
                </asp:TableCell>
                <asp:TableCell Height="50px" Width="100px" BorderWidth="2" BackColor="LightGray" HorizontalAlign="Center" VerticalAlign="Middle">
                    <p style="color:blueviolet; font-weight:bold;  ">
                        Minimum GPA
                    </p>
                </asp:TableCell>
                <asp:TableCell Height="50px" Width="100px" BorderWidth="2" BackColor="LightGray" HorizontalAlign="Center" VerticalAlign="Middle">
                    <p style="color:blueviolet; font-weight:bold;  ">
                        Concentration
                    </p>
                </asp:TableCell>
                <asp:TableCell Height="50px" Width="250px" BorderWidth="2" BackColor="LightGray" HorizontalAlign="Center" VerticalAlign="Middle">
                    <p style="color:blueviolet; font-weight:bold;  ">
                        Prerequisites
                    </p>
                </asp:TableCell>
            </asp:TableRow>
     </asp:Table>

</asp:Content>
