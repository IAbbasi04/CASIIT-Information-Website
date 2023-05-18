<%@ Page Title="CourseTree" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseTree.aspx.cs" Inherits="CASIITInformationWebsite.CourseTree" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- style sheet -->
    <style>
        :root{
            --purple: rebeccapurple;
            --panel-background: white;
            --button-background: lightgray;
        }
        body {background-color: var(--purple)} 
        @media(prefers-color-scheme: dark){
            :root{
                --panel-background: #363636;
                --button-background: dimgray;
            }
        }
    </style>
    <!-- panzoom import -->
    <script src="https://unpkg.com/@panzoom/panzoom@4.5.1/dist/panzoom.min.js" type="text/javascript"></script>
    <!-- update panel for buttons to keep reloads down -->
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <!-- button to reset tree view -->
            <asp:Button ID="ResetViewButton" runat="server" Text="Reset View" style="position:absolute;left:50px;background-color:var(--button-background)" OnClientClick="pz.zoom(0.3,{animate:true});pz.pan(-1314,104,{animate:true});"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- background panel, acts as frame for panzoom -->
    <asp:Panel ID="Panel1" runat="server" style="position:absolute;left:0px;top:90px;background-color:var(--panel-background)">
        <!-- tree panel, is the panzoom element -->
        <asp:Panel ID="Panel2" runat="server">
            <!-- svg panel to draw lines -->
            <svg id="LinePanel" style="position:absolute;width:100%;height:100%"></svg>
            <!-- update panel to hold the tree elemeents and reduce reloads -->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="position:absolute" UpdateMode="Conditional">
                <ContentTemplate>
                    <!-- hidden button to clear user selection when background is clicked -->
                    <asp:Button ID="ClearSelectionButton" runat="server" style="height:0px;width:0px;background-color:transparent;border-color:transparent" OnClick="ClearSelectionButton_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
