<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nav.aspx.cs" Inherits="CASIITInformationWebsite.Pages.nav" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
        <link rel="stylesheet" href="css/Test.css"/>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css"/>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstFruits]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="wrapper">
  <input type="checkbox" id="menuToggler" class="input-toggler" value="1" autofocus="true"/>
  <label for="menuToggler" id="menuTogglerLabel" class="menu-toggler" role="button" aria-pressed="false" aria-expanded="false" aria-label="Navigation button">
    <span class="menu-toggler__line"></span>
    <span class="menu-toggler__line"></span>
    <span class="menu-toggler__line"></span>
  </label>


  <nav id="sidebar" class="sidebar" role="navigation" aria-labelledby="menuTogglerLabel" aria-hidden="true">
    <ul id="menubar" class="menu" role="menubar" aria-orientation="vertical">
        <li class="menu__item"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Select Courses</a></li>
        <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple">
            <asp:ListItem Text="IT Fundementals DE"></asp:ListItem>
            <asp:ListItem Text="Data Structures and Algorithms"></asp:ListItem>
            <asp:ListItem Text="ADV Computer Studies"></asp:ListItem>
            <asp:ListItem Text="AP Computer Science A"></asp:ListItem>
            <asp:ListItem Text="Adv Computer Math"></asp:ListItem>
            <asp:ListItem Text="Graphic Design I"></asp:ListItem>
        </asp:ListBox>
      <li class="menu__item"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Select Upcoming Grade</a></li>
        <asp:RadioButtonList runat="server">
            <asp:ListItem>9th</asp:ListItem>
            <asp:ListItem>10th</asp:ListItem>
            <asp:ListItem>11th</asp:ListItem>
            <asp:ListItem>12th</asp:ListItem>
        </asp:RadioButtonList>
      <li class="menu__item"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Math Completed</a></li>
        <asp:CheckBoxList runat="server">
            <asp:ListItem>Geometry</asp:ListItem>
            <asp:ListItem>Algebra</asp:ListItem>
        </asp:CheckBoxList>
      <li class="menu__item"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Current GPA</a></li>
        <asp:TextBox runat="server"></asp:TextBox>
    </ul>
  </nav>

</div>
        </div>
    </form>
</body>
</html>