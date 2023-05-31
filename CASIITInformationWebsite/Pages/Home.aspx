<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CASIITInformationWebsite.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="font-size:130px; font-weight:bold; vertical-align:bottom; color:rebeccapurple"; align="center">
        <asp:Image style="vertical-align:top" ID="CASIIT_logo" runat="server" src="../Images2/casiitlogo.png" Height="130px" Width="130px"/>
        Casiit
    </h1>

    <p style="font-size:18px; font-weight:bold; color:black; text-underline-position:from-font;" align="center">
        | Center for Applied Sciences, Interactive and Information Technology |
    </p>


    <div>
        <asp:Table ID="Table1" runat="server">
            <asp:TableRow BorderWidth="4px" BorderColor="Black" BackColor="DarkViolet">
                <asp:TableCell Height="50" Width="1500">

                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <asp:Table ID="Table3" runat="server" HorizontalAlign="Center">
            <asp:TableRow >
                <asp:TableCell >
                    <asp:Image ID="Image1" runat="server" src="../Images2/bobcat.jpg" Height="100px" Width="200px"/>
                </asp:TableCell>
                <asp:TableCell Width="280px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="black"> Visit Battlefield Website: </asp:Label>
                    <a href="https://battlefieldhs.pwcs.edu/">Click Here to Visit!</a>
                    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="black"> Visit NVCC Website: </asp:Label>
                    <a href="https://www.nvcc.edu/">Click Here to Visit!</a>
                </asp:TableCell>
                <asp:TableCell >
                    <asp:Image ID="Image2" runat="server" src="../Images2/nova_logo.jpg" Height="100px" Width="160px"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    <h2 style="color:blueviolet; font-weight:bold;" align="left">
        What is CASIIT?
    </h2>

    <p>
        The Center for Applied Sciences, Interactive and Information Technology (CASIIT) at Battlefield High School offers students the opportunity to choose from three focus areas: <u>Applied Science, Interactive Technologies, and Information Technology.</u> Within each area students then have the freedom to specialize their learning to a more personal level.

The ever-changing and interactive nature of the technology workplace will also require students to demonstrate highly developed social and technical intelligence. The CASIIT program provides the courses and the foundational knowledge to prepare students for entry into the future world of industry and technology.

Students must be enrolled in at least one CASIIT core class and must complete a sequence of CASIIT classes in grade 9-12.

All students in this program may choose from an extensive list of Advanced Placement core and elective courses to complement the CASIIT courses.

    </p>

    <p></p>

    <h2 style="color:blueviolet; font-weight:bold;" align="left">
        Why?
        <img src="../Images2/circuit.png" height="80" width="150"/>
    </h2>

    <p>
        Information Technology (encompassing Interactive Technology and Computer Science) is used in almost every job today. We are passionate about providing our students with technical skills for all potential careers and futures. Our graduates have reported they excel comparatively in both college and on the job due to their courses in our CASIIT Program.

Technology helps you "Work Smarter". If you want a career in the IT fields, Virginia currently has over 100,000 open jobs.
    </p>

    <h2 style="color:blueviolet; font-weight:bold;" align="left">
        What is Dual Enrollment?
        <img src="../Images2/circuit.png" height="80" width="150"/>
    </h2>

    <asp:Table ID="Table5" runat="server">
            <asp:TableRow >
                <asp:TableCell Height="125px" Width="1300px" HorizontalAlign="Left"> 
                    <asp:Image ID="Image5" runat="server" src="/Images2/graduation.png" Height="120px" Width="160px" ImageAlign="Right"/> 
                    <p>
                        Dual Enrollment at Battlefield High School is an opportunity for students in a high school classroom to earn both high school course credit and college credit. The college credit is awarded by Northern Virginia Community College. The faculty leading these courses are certified by the Virginia Department of Education and by the Virginia Community College System. Course content will cover the requirements of both the high school curriculum and the college curriculum.
                        At this time, the students are receiving college course credits tuition free.
                        At Battlefield, we are offering dual-enrolled courses that lead toward:

                        Database Specialist Certificate,
                        Network Engineering Specialist Certificate,
                        General Studies Certificate,
                        Associates of Science in General Students (will require some work on campus),
                        Associates of Science in Information Technology (will require some work on campus).
                        </p>                   
                </asp:TableCell>
            </asp:TableRow>
     </asp:Table>


    <h2 style="color:blueviolet; font-weight:bold;" align="left">
        Awards
        <img src="../Images2/circuit.png" height="80" width="150"/>
    </h2>

     <asp:Table ID="Table6" runat="server">
            <asp:TableRow >
                <asp:TableCell Height="125px" Width="1300px" HorizontalAlign="Left"> 
                    <asp:Image ID="Image6" runat="server" src="/Images2/ribbon.png" Height="120px" Width="120px" ImageAlign="Right"/> 

                    <p>
                        Students have the chance to win awards when they graduate for recognition of their success and achievements in the CASIIT program. These such awards are comprised of:
                    </p>
                    <p>
                        Students who complete six classes—at least one per year, with an overall and CASIIT GPA of 3.0, will receive a gold CASIIT medal
                    </p>
                    <p>
                        Students who complete four classes—at least one per year, with an overall and CASIIT GPA of 2.5, will receive a silver CASIIT medal
                    </p>
                    <p>
                        Students who complete two classes—at least one per year, with an overall and CASIIT GPA of 2.5, will receive a bronze CASIIT medal
                    </p>

                    
                </asp:TableCell>
            </asp:TableRow>
     </asp:Table>
    
</asp:Content>