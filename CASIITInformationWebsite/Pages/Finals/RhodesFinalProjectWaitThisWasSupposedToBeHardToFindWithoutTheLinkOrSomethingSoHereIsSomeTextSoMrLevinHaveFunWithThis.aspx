<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="RhodesFinalProjectWaitThisWasSupposedToBeHardToFindWithoutTheLinkOrSomethingSoHereIsSomeTextSoMrLevinHaveFunWithThis.aspx.cs" Inherits="CASIITInformationWebsite.Pages.Finals.RhodesFinalProjectWaitThisWasSupposedToBeHardToFindWithoutTheLinkOrSomethingSoHereIsSomeTextSoMrLevinHaveFunWithThis" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        :root{
            --panel-width: 280px;
            --panel-height: 300px;
        }

        .FlipContainer {
            perspective: 1000px;
        }

        .FlipContainer.flip .Flip {
            transform: rotateY(180deg);
        }

        .FlipContainer, .Front, .Back {
            width: var(--panel-width);
            height: var(--panel-height);
        }

        .Flip {
            transition: 0.6s;
            transform-style: preserve-3d;
        }

        .Front, .Back {
            backface-visibility: hidden;
            position: absolute;
            top: 0;
            left: 0;
        }

        .Front {
            z-index: 2;
            transform: rotateY(0deg);
        }

        .Back {
            transform: rotateY(180deg);
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/canvas-confetti@1.6.0/dist/confetti.browser.min.js"></script>
    <script type="text/javascript">
        var lights = false;
        function randomInRange(min, max) {
            return Math.random() * (max - min) + min;
        }
        function confettiGo() {
            var duration = 120 * 1000;
            var animationEnd = Date.now() + duration;
            var skew = 1;

            function randomInRange(min, max) {
                return Math.random() * (max - min) + min;
            }

            (function frame() {
                var timeLeft = animationEnd - Date.now();
                var ticks = Math.max(500, 3000 * (timeLeft / duration));
                skew = Math.max(0.8, skew - 0.001);
                var time = Date.now();

                confetti({
                    particleCount: 60,
                    startVelocity: 4,
                    spread: 360,
                    ticks: ticks,
                    origin: {
                        x: Math.random(),
                        // since particles fall down, skew start toward the top
                        y: (Math.random() * skew) - 0.2
                    },
                    gravity: randomInRange(0.4, 0.6),
                    scalar: randomInRange(0.4, 1),
                    drift: randomInRange(-0.4, 0.4)
                });
                confetti({
                    particleCount: 60,
                    startVelocity: 4,
                    spread: 360,
                    ticks: ticks,
                    origin: {
                        x: Math.random(),
                        // since particles fall down, skew start toward the top
                        y: (Math.random() * skew) - 0.2
                    },
                    gravity: randomInRange(0.4, 0.6),
                    scalar: randomInRange(0.4, 1),
                    drift: randomInRange(-0.4, 0.4)
                });
                confetti({
                    particleCount: 60,
                    startVelocity: 4,
                    spread: 360,
                    ticks: ticks,
                    origin: {
                        x: Math.random(),
                        // since particles fall down, skew start toward the top
                        y: (Math.random() * skew) - 0.2
                    },
                    gravity: randomInRange(0.4, 0.6),
                    scalar: randomInRange(0.4, 1),
                    drift: randomInRange(-0.4, 0.4)
                });
                confetti({
                    particleCount: 60,
                    startVelocity: 4,
                    spread: 360,
                    ticks: ticks,
                    origin: {
                        x: Math.random(),
                        // since particles fall down, skew start toward the top
                        y: (Math.random() * skew) - 0.2
                    },
                    gravity: randomInRange(0.4, 0.6),
                    scalar: randomInRange(0.4, 1),
                    drift: randomInRange(-0.4, 0.4)
                });

                if (time - Date.now() > 100) {
                    timeLeft = 0;
                }
                if (timeLeft > 0) {
                    requestAnimationFrame(frame);
                }
            }());
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Label ID="Label2" runat="server" Text="Hello, Tyler!" style="position:absolute;font-size:70px;left:300px"></asp:Label>

            <asp:Button ID="ConfettiButton" runat="server" Text="Confetti?" style="position:absolute;bottom:0px;left:380px" OnClientClick="confettiGo();return false;"/>
            <asp:Label ID="Label1" runat="server" Text="You really shouldn't..." style="position:absolute;bottom:3px;left:460px"></asp:Label>

            <asp:Panel runat="server" ID="FlipContainer" style="position:absolute;top:10px;left:10px" CssClass="FlipContainer">
                <asp:Panel runat="server" ID="Flip" CssClass="Flip">
                    <asp:Panel runat="server" ID="Front" CssClass="Front">
                        <asp:Button ID="Button1A" runat="server" Text="About" OnClientClick="document.querySelector('#MainContent_FlipContainer').classList.toggle('flip');return false;" Style="font-size:xx-large; width: 100%; height: 100%;" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Back" CssClass="Back">
                        <asp:Button ID="Button1B" runat="server" Text="-Born in Washington, D.C.
-Lives with parents and one sibling
(older sister)
-Only person in family who is a
Virginia native" OnClientClick="document.querySelector('#MainContent_FlipContainer').classList.toggle('flip');return false;" Style="font-size:larger; width: 100%; height: 100%;" />
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="PopoutPanel" runat="server" style="position:absolute; top:320px; left:10px; width:var(--panel-width); height:var(--panel-height)">
                <asp:Button ID="Button4A" runat="server" Text="Hobbies/Interests" style="font-size:xx-large; position:absolute; width:var(--panel-width); height:var(--panel-height);"  OnClientClick="document.querySelector('#MainContent_Button4B').style.visibility = 'Visible';return false;"/>
                <asp:Button ID="Button4B" runat="server" Text="-Robotics
-Ceating Things
-Video Games" style="font-size:larger; position:absolute; width:180px; height:180px; left:var(--panel-width); top:60px; visibility:hidden" OnClientClick="document.querySelector('#MainContent_Button4B').style.visibility = 'Hidden';return false;"/>
            </asp:Panel>

            <asp:Panel ID="LightPanel" runat="server" style="position:absolute; top:10px; left:700px; width:var(--panel-width); height:var(--panel-height)">
                <asp:Button ID="Button3A" runat="server" Text="Fun Facts" style="font-size:xx-large; position:absolute; width:var(--panel-width); height:var(--panel-height)" OnClientClick="document.querySelector('#MainContent_Button3A').style.visibility = 'Hidden';document.querySelector('#MainContent_Button3B').style.visibility = 'Visible';return false;"/>
                <asp:Button ID="Button3B" runat="server" Text="-Has used both the oven and stove
sucsessfully without supervision
before becoming an adult
-Learned to drive in a stick
shift car
-Has been programming since 5
years old" style="font-size:larger; position:absolute; width:var(--panel-width); height:var(--panel-height); border-style:groove; border-color:yellow; border-width:5px; visibility:hidden" OnClientClick="document.querySelector('#MainContent_Button3B').style.visibility = 'Hidden';document.querySelector('#MainContent_Button3A').style.visibility = 'Visible';return false;"/>
            </asp:Panel>

            <asp:Panel ID="SwipeContainer" runat="server" style="position:absolute; top:320px; left:700px; overflow:hidden; width:var(--panel-width); height:var(--panel-height)">
                <asp:Panel ID="Swipe" runat="server" style="position:absolute; width:var(--panel-width); left:0px; bottom:0px">
                    <asp:Button ID="Button2A" runat="server" Text="-Plans to persue a carreer in
Electrical Engineering
-Plans to use programming for
programmable circuits and as
a backup carreer
-Wants to go to space" style="font-size:larger; width:var(--panel-width); height:var(--panel-height)"  OnClientClick="var i = +(document.querySelector('#MainContent_SwipeContainer').clientHeight); function shift(){document.querySelector('#MainContent_Swipe').style.bottom = -i+'px';if(i>0){i--;setTimeout(shift,1)};}setTimeout(shift,1);return false;"/>
                    <asp:Button ID="Button2B" runat="server" Text="Future" style="font-size:xx-large; width:var(--panel-width); height:var(--panel-height)" OnClientClick="var i = 0; function shift(){document.querySelector('#MainContent_Swipe').style.bottom = -i+'px';if(i< +(document.querySelector('#MainContent_SwipeContainer').clientHeight)){i++;setTimeout(shift,1)};}setTimeout(shift,1);return false;"/>
                </asp:Panel>
            </asp:Panel>

            <!--<img src="../Finals/Images/worlds-best-css-developer-trophy.png" style="max-height: 300px" alt="Worlds Best CSS Developer Trophy (that was definetly designed with css)" /><!--test image im probably going to forget to remove at some point-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
