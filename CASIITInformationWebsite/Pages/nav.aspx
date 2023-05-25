<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nav.aspx.cs" Inherits="CASIITInformationWebsite.Pages.nav" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
        <%--<link rel="stylesheet" href="css/Test.css"/>--%>
    <style>
        body {
}
:root {
    --sidebar-width: 100%;
    --toggler-size: 30px;
    --toggler-line-number: 3;
    --toggler-line-size: calc(var(--toggler-size) / (var(--toggler-line-number) + var(--toggler-line-number) - 1));
    --toggler-offset-left: 20px;
    --toggler-offset-top: 70px;
    --toggler-color: Purple;
    --toggler-color-hover: Purple;
}

* {
    padding: 0;
    margin: 0;
    box-sizing: border-box;
}

html {
    font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", "Roboto", "Oxygen", "Ubuntu", "Cantarell", "Fira Sans", "Droid Sans", "Helvetica Neue", "sans-serif";
    font-size: 0.8rem;
}

@media screen and (min-width: 20em) {
    html {
        font-size: calc(0.8rem + 0.4 * (100vw - 20em) / 55);
    }
}

@media screen and (min-width: 75em) {
    html {
        font-size: 1.2rem;
    }
}

.wrapper {
    min-width: 100vw;
    background: AliceBlue;
    display: flex;
}

.sidebar,
.content {
    transition: all 0.25s ease-out;
}

.sidebar {
    width: var(--sidebar-width);
    transform: translateX(calc(var(--sidebar-width) * -1));
    background: linear-gradient(to bottom right, Plum, MediumOrchid);
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    top: 0;
    left: 0;
    height: 100vh;
    opacity: 0.5;
}

.content {
    width: 100%;
    display: block;
    align-items: center;
    justify-content: center;
    flex-direction: column;
}

.menu {
    list-style-type: none;
}

.menu__link {
    color: white;
    font-weight: 100;
    text-decoration: none;
    font-size: 10vmin;
    line-height: 15vmin;
    transition: all 0.25s ease-out;
}

    .menu__link:hover, .menu__link:focus, .menu__link:active {
        color: mediumpurple;
    }

.menu-toggler {
    border-radius: calc(var(--toggler-line-size) / 2);
    display: block;
    width: var(--toggler-size);
    height: var(--toggler-size);
    position: fixed;
    top: var(--toggler-offset-top);
    left: var(--toggler-offset-left);
    cursor: pointer;
    z-index: 1;
}

.menu-toggler__line {
    height: var(--toggler-line-size);
    background: var(--toggler-color);
    position: absolute;
    border-radius: calc(var(--toggler-line-size) / 2);
    left: 0;
    right: 0;
    transition: all 0.25s ease-out;
}

    .menu-toggler__line:nth-child(1) {
        top: calc(var(--toggler-line-size) * 1 + (var(--toggler-line-size) * (1 - 2)));
    }

    .menu-toggler__line:nth-child(2) {
        top: calc(var(--toggler-line-size) * 2 + (var(--toggler-line-size) * (2 - 2)));
    }

    .menu-toggler__line:nth-child(3) {
        top: calc(var(--toggler-line-size) * 3 + (var(--toggler-line-size) * (3 - 2)));
    }

.input-toggler {
    position: absolute;
    left: -100%;
}

    .input-toggler:focus ~ .menu-toggler {
        outline: 1px dotted;
    }

    .input-toggler:checked ~ .menu-toggler .menu-toggler__line:not(:first-child):not(:last-child) {
        opacity: 0;
    }

    .input-toggler:checked ~ .menu-toggler .menu-toggler__line:first-child,
    .input-toggler:checked ~ .menu-toggler .menu-toggler__line:last-child {
        background-color: var(--toggler-color-hover);
    }

    .input-toggler:checked ~ .menu-toggler .menu-toggler__line:first-child {
        transform: translateY(calc(var(--toggler-line-size) * (var(--toggler-line-number) - 1))) rotate(45deg);
    }

    .input-toggler:checked ~ .menu-toggler .menu-toggler__line:last-child {
        transform: translateY(calc(-1 * var(--toggler-line-size) * (var(--toggler-line-number) - 1))) rotate(-45deg);
    }

    .input-toggler:checked ~ .sidebar {
        transform: translateX(0);
        opacity: 0.98;
    }

.wrapper {
    /*height: calc(100vh - 50px);*/
}

.sb-link {
    display: flex;
    height: 50px;
    align-content: center;
    align-items: center;
    justify-content: center;
    text-decoration: none;
    color: #d01bf5;
    transition: background 0.3s;
}

    .sb-link:hover,
    .sb-link:focus,
    .sb-link:active {
        background: #d01bf5;
    }

    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
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