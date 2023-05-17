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
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Home</a></li>
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Blog</a></li>
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Portfolio</a></li>
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">About</a></li>
      <li class="menu__item" role"none"><a class="menu__link" href="#" role="menuitem" tabindex="-1">Contact</a></li>
    </ul>
  </nav>
  <main class="content">
    <h1>CSS sidebar toggle</h1>
    <p>Pure CSS solution for hiding and showing sidebar.</p>
  </main>
</div>
<a class="sb-link" href="//silvestar.codes/articles/css-sidebar-toggle/">silvestar.codes</a>
        </div>
    </form>
</body>
</html>
