<%@ Page Title="ManySnathan" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManySnathon.aspx.cs" Inherits="CasiitCourses.ManySnathan" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<link href="styles.css" rel="stylesheet" />
	<%--Acts as a variable that we can set with javascript and use in c sharp--%>
    <asp:HiddenField ID="keyPressedPlayer1" runat="server" Value=""></asp:HiddenField>
    <asp:HiddenField ID="keyPressedPlayer2" runat="server" Value=""></asp:HiddenField> 
    <asp:HiddenField ID="keyPressedPlayer3" runat="server" Value=""></asp:HiddenField> 
    <asp:HiddenField ID="keyPressedPlayer4" runat="server" Value=""></asp:HiddenField> 
    
    <script type="text/javascript">
        document.addEventListener("keydown",
            function (event) {
                var keyCode = event.keyCode;
                // assign the input to the correct player
                if (keyCode == 87 || keyCode == 65 || keyCode == 83 || keyCode == 68) // in (87, 65, 83, 68)) // (W, A, S, D)
                {
                    // player 1
                    document.getElementById("<%= lKeyPressedPlayer1.ClientID %>").textContent = "" + keyCode;
                    document.getElementById("<%= keyPressedPlayer1.ClientID %>").value = "" + keyCode;
                }
                else if (keyCode == 104 || keyCode == 100 || keyCode == 101 || keyCode == 102) // in (104, 100, 101, 102)) // NUMLOCK ON: (8, 4, 5, 6) on number pad
                {
                    // player 2
                    document.getElementById("<%= lKeyPressedPlayer2.ClientID %>").textContent = "" + keyCode;
                    document.getElementById("<%= keyPressedPlayer2.ClientID %>").value = "" + keyCode;
                }
                else if (keyCode == 73 || keyCode == 74 || keyCode == 75 || keyCode == 76) // in (73, 74, 75, 76)) // (I, J, K, L)
                {
                    // player 3
                    document.getElementById("<%= lKeyPressedPlayer3.ClientID %>").textContent = "" + keyCode;
                    document.getElementById("<%= keyPressedPlayer3.ClientID %>").value = "" + keyCode;
                }
                else if (keyCode == 38 || keyCode == 37 || keyCode == 40 || keyCode == 39) // in (38, 37, 40, 39)) // Arrow Keys: (UP, LEFT, DOWN, RIGHT)
                {
                    // player 4
                    document.getElementById("<%= lKeyPressedPlayer4.ClientID %>").textContent = "" + keyCode;
                    document.getElementById("<%= keyPressedPlayer4.ClientID %>").value = "" + keyCode;
                }
            });

        setInterval( // reload the table every time the interval occurs
            function () {
                __doPostBack('<%= tGameArea.ClientID %>', ''); // reload only the table; not the entire page
                //__doPostBack('<%= UpdatePanel.ClientID %>', ''); // reload only the update pannel; not the entire page
            }
            , 400
        );

    </script>


    <div id="divGameHeader" runat="server" style="height:contain; display: flex; justify-content: center;">
        <asp:Label ID="lGameHeader" runat="server" Text="Welcome To Snathan!" Font-Bold="true" Font-Size="XX-Large" ForeColor="Green"/>
    </div>



    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>

            <div id="divGameArea" runat="server">
                <asp:Table ID="tGameArea" runat="server"  BackColor="WhiteSmoke" BorderColor="Black" BorderWidth="2" HorizontalAlign="Center" GridLines="Both" CssClass="GameTableStyle" />
            </div>

    
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="tTimeSurvived" EventName="Tick" />--%>
        </Triggers>

    </asp:UpdatePanel>




    <div id="divScoreBoard" runat="server" style="position:absolute; right:200px; top: 150px;">
        <asp:Label ID="lScoreBoard" runat="server" Text="Score Board" Font-Bold="true" Font-Size="XX-Large" ForeColor="Green" />
        <div>
            <asp:UpdatePanel ID="upScoreBoard" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Label ID="lPointsScored" runat="server" Text="Cole's Love For Nathan " Font-Bold="true" Font-Size="X-Large" ForeColor="Green" />
                    <b style="font: bold; font-size: x-large; color: green"> <%: pointsCollected%> </b> 
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <%--<style> NOT SURE WHAT <b> is BUT IT ALLOWS ME TO CHANGE THE STYLE OF MY VALUE SO YAY
            </style>--%>
            <%--<hr Style="font-size:x-large"><%: pointsCollected%> <hr />--%>
        </div>
        <div>
            <asp:Label ID="lTimeSurvived" runat="server" Text="Time Survived " Font-Bold="true" Font-Size="X-Large" ForeColor="Green" />
        </div>
        <div>
            <asp:Label ID="lKeyPressedPlayer1" runat="server" Text="Key Pressed " Font-Bold="true" Font-Size="X-Large" ForeColor="Green" />
            <asp:Label ID="lKeyPressedPlayer2" runat="server" Text="Key Pressed " Font-Bold="true" Font-Size="X-Large" ForeColor="Green" />
            <asp:Label ID="lKeyPressedPlayer3" runat="server" Text="Key Pressed " Font-Bold="true" Font-Size="X-Large" ForeColor="Green" />
            <asp:Label ID="lKeyPressedPlayer4" runat="server" Text="Key Pressed " Font-Bold="true" Font-Size="X-Large" ForeColor="Green" />
        </div>
    </div>


    <div id="divGameControls" runat="server" style="position:absolute; left: 150px; top: 150px" >
        <asp:Label ID="lControls" runat="server" Text="Controls" Font-Bold="true" Font-Size="XX-Large" ForeColor="Green" />
        <div id="divPlayerOneControls" runat="server" style="position:relative;" >
            <div>
                <asp:Label ID="lPlayerOneControls" runat="server" Text="Player One" Font-Bold="true" Font-Size="X-Large" ForeColor="Green" Style="top:0px" />
            </div>
            <div>
                <asp:Image ID="imgWASDKeys" runat="server" ImageUrl="~/Images/WASD Keys.png" CssClass="GameControlsStyleFormat WASD-Image"/>
            </div>
        </div>
        <%--<div id="divPlayerTwoControls" runat="server" style="position:relative;" >
            <div>
                <asp:Label ID="lPlayerTwoControls" runat="server" Text="Player Two" Font-Bold="true" Font-Size="X-Large" ForeColor="Green" Style="top:0px" />
            </div>
            <div>
                <asp:Image ID="imgArrowKeys" runat="server" ImageUrl="~/Images/Arrow Keys.png" CssClass="GameControlsStyleFormat Arrow-Keys-Image"/>
            </div>
        </div>--%>
    </div>


    
    

    <%--<div style="display: grid; grid-template-columns: repeat(3, 1fr); gap: 10px;">
        <div style="background-color: red; color: white; padding: 10px;">Div 1</div>
        <div style="background-color: blue; color: white; padding: 10px;">Div 2</div>
        <div style="background-color: green; color: white; padding: 10px;">Div 3</div>
        <div style="background-color: orange; color: white; padding: 10px;">Div 4</div>
        <div style="background-color: purple; color: white; padding: 10px;">Div 5</div>
        <div style="background-color: yellow; color: black; padding: 10px;">Div 6</div>
    </div>--%>
    
    <%--<div style="display: grid; grid-template-columns: repeat(3, 1fr); gap: 10px;">
        <div>Div 1</div>
        <div>Div 2</div>
        <div>Div 3</div>
        <div>Div 4</div>
        <div>Div 5</div>
        <div>Div 6</div>
    </div>--%>
    
</asp:Content>


<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"></asp:UpdatePanel>--%>
