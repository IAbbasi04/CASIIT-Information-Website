<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputTab.aspx.cs" Inherits="CASIITInformationWebsite.Pages.InputTab" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css/Test.css"/>
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
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Select Courses</a></li>
        <asp:DropDownList runat="server">
            <asp:ListItem Text="IT Fundementals DE"></asp:ListItem>
            <asp:ListItem Text="Data Structures and Algorithsm"></asp:ListItem>
            <asp:ListItem Text="ADV Computer Studies"></asp:ListItem>
            <asp:ListItem Text="AP Computer Science A"></asp:ListItem>
            <asp:ListItem Text="Adv Computer Math"></asp:ListItem>
            <asp:ListItem Text="Graphic Design I"></asp:ListItem>
        </asp:DropDownList>
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Select Upcoming Grade</a></li>
        <asp:RadioButtonList runat="server">
            <asp:ListItem>9th</asp:ListItem>
            <asp:ListItem>10th</asp:ListItem>
            <asp:ListItem>11th</asp:ListItem>
            <asp:ListItem>12th</asp:ListItem>
        </asp:RadioButtonList>
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Math Completed</a></li>
        <asp:CheckBoxList runat="server">
            <asp:ListItem>Geometry</asp:ListItem>
            <asp:ListItem>Algebra</asp:ListItem>
        </asp:CheckBoxList>
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Current GPA</a></li>
        <asp:TextBox runat="server"></asp:TextBox>
    </ul>
  </nav>


  <main class="content">
    <h1>Input Tab that'll be included on all pages</h1>
    <p>Click the icon in the upper-right corner for inputs. Note: multi-select dropdown for courses coming very soon</p>
  </main>
</div>
<a class="sb-link" href="//silvestar.codes/articles/css-sidebar-toggle/">silvestar.codes</a>
        </div>
    </form>
</body>
</html>
