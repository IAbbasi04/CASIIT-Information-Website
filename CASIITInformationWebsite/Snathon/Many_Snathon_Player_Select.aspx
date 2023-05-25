<%@ Page Title="Many_Snathon_Player_Select" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Many_Snathon_Player_Select.aspx.cs" Inherits="CasiitCourses.Many_Snathon_Player_Select" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <div id="divGameHeader" runat="server" style="height:contain; display: flex; justify-content: center;">
        <asp:Label ID="lGameHeader" runat="server" Text="Welcome To Snathan!" Font-Bold="true" Font-Size="XX-Large" ForeColor="Green"/>
    </div>
        
    <div runat="server" style="height:contain; display: flex; justify-content: center;">
        <asp:Label ID="lPlayerSelectTitle" runat="server" Text="Player Select" Font-Bold="true" Font-Size="XX-Large" ForeColor="Green"/>
    </div>

    <div>
        <asp:Button ID="bRemovePlayer" runat="server" Text="Remove Player" Font-Bold="true" Font-Size="X-Large" ForeColor="Green" OnClick="bRemovePlayer_Click"/>
        <asp:Button ID="bAddPlayer" runat="server" Text="Add Player" Font-Bold="true" Font-Size="X-Large" ForeColor="Green" OnClick="bAddPlayer_Click"/>
    </div>


    <div ID="divPlayerShowcase">
        <asp:UpdatePanel ID="upPlayerShowcase" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Image ID="Image1" runat="server" Visible="true" ImageUrl="~/Images/Nathan Head.png" CssClass="PlayerSelectImageStyle" />
                <asp:Image ID="Image2" runat="server" Visible="false" ImageUrl="~/Images/Nathan Head.png" CssClass="PlayerSelectImageStyle" />
                <asp:Image ID="Image3" runat="server" Visible="false" ImageUrl="~/Images/Nathan Head.png" CssClass="PlayerSelectImageStyle" />
                <asp:Image ID="Image4" runat="server" Visible="false" ImageUrl="~/Images/Nathan Head.png" CssClass="PlayerSelectImageStyle" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="bRemovePlayer" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="bAddPlayer" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <div>
        <asp:Button ID="bStart" runat="server" Text="Start Game!" Font-Bold="true" Font-Size="X-Large" ForeColor="Green" OnClick="bStart_Click" />
    </div>
    

</asp:Content>
