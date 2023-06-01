<%@ Page Title="All about Abbasi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WhatHappensIfYouDoNotTurnWorkIn.aspx.cs" Inherits="CASIITInformationWebsite.Pages.Finals.WhatHappensIfYouDoNotTurnWorkIn" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .intro-banner {
            /*background-color: cornflowerblue;*/
            /*width: 1000px;*/
            /*height: 200px;*/
            font-size: 24px;
        }
    </style>

    <div class="jumbotron">
        <marquee direction="right" class="intro-banner"><strong>Ibrahim Abbasi</strong></marquee>
    </div>
    <div>
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell Height="200px" ColumnSpan="2">
                    <h1>Did you know ... </h1>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="400px">
                <asp:TableCell Width="200px">
                    <img src="../Pages/Finals/Images/IMG_1052.jpg" height="200" width="200"/>
                </asp:TableCell>
                <asp:TableCell Width="100px"/>
                <asp:TableCell>
                    I am an 18 year old graduate (pretty much a graduate) who is going to attend GMU in the fall of 2023.
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div>
        <p>What school activities have I been a part of?</p>
        <ul>
            <li><asp:RadioButton runat="server" Text="Robotics" GroupName="Q1" ID="Q1A" OnCheckedChanged="Q1_Checked"/></li>
            <li><asp:RadioButton runat="server" Text="Basketball" GroupName="Q1" ID="Q1B" OnCheckedChanged="Q1_Checked"/></li>
            <li><asp:RadioButton runat="server" Text="Baseball" GroupName="Q1" ID="Q1C" OnCheckedChanged="Q1_Checked"/></li>
            <li><asp:RadioButton runat="server" Text="Computer Science Club" GroupName="Q1" ID="Q1D" OnCheckedChanged="Q1_Checked"/></li>
        </ul>

        <asp:Label runat="server" Visible="true" ForeColor="IndianRed" ID="Q1Label">I have been doing robotics since 6th grade, did HOSA med club and msa this year, but I do basketball outside of school (does not count as a school activity). </asp:Label>
    </div>
    <div>
        <p>I also have an interest in becoming a neurosurgeon, which would start with me turning my stuff in on time. Because of this, I will be majoring in neuroscience and hopefully going down the pre-med route at GMU.</p>
    </div>
</asp:Content>