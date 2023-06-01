using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace CASIITInformationWebsite
{
    public partial class CourseTree : Page
    {
        const int minHeightOffset = 130;//the ideal minimum vertical distance to keep elements apart, measured from top point to top point
        const int minWidthOffset = 100;//the ideal minimum horizontal distance to keep elements apart, measured form aproximate end to aproximate end
        const int avgPanelWidth = 200;//the width to assume the panels are
        const int LBT = 30;//the number of characters needed to trigger a line break
        protected void Page_Init(object sender, EventArgs e)
        {
            //on first page load, get js to pickup clientHeight and clientWidth for each panel, then push that to the server and reload, hopefully before clientside is ever displayed so it doesn't look wierd
            bool firstLoad = false;
            if (Request.Form["__EVENTARGUMENT"] == null || !Request.Form["__EVENTARGUMENT"].StartsWith("WHData"))
            {
                firstLoad = true;
            }
            //get rid of the footer for now (its too in the way right now, makes testing hard)
            Page.ClientScript.RegisterStartupScript(GetType(), "NoFooter",
            "document.getElementsByClassName('footer')[0].style.visibility = 'Hidden'\n"
            , true);
            //get rid of the random line on the page
            Page.ClientScript.RegisterStartupScript(GetType(), "LineBGone",
            "document.querySelector('#ctl01 > div.container.body-content > hr').remove()\n"
            , true);
            //load script to init Panzoom
            //Panzoom docs https://github.com/timmywil/panzoom
            Page.ClientScript.RegisterStartupScript(GetType(), "InitPanZoom",
            "elem = document.getElementById(\"MainContent_Panel2\");\n" +
            "pz = Panzoom(elem, {" +//panzoom configs
                "contain: true," +
                "canvas: true," +
                "maxScale: 2," +
                "minScale: 0.25," +
                "step: 0.1" +
            "});\n" +
            "elem.parentElement.addEventListener('wheel', pz.zoomWithWheel);\n"//allow panzoom to work with the mouse wheel
            , true);
            //load script that automatically resizes the view panel to fit the screen
            Page.ClientScript.RegisterStartupScript(GetType(), "AutoSize",
            "function setPanelSize() {\n" +
                "var bw = navigator.appName;\n" +
                "if (bw == \"Microsoft Internet Explorer\")\n" +//don't ask me why this is needed
                "{\n" +
                    "var ht = document.body.clientHeight;\n" +
                    "var wt = document.body.clientWidth;\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                    "var ht = window.innerHeight;\n" +//get window size
                    "var wt = window.innerWidth;\n" +
                "}\n" +
                "var ctl = document.getElementById(\"MainContent_Panel1\");\n" +
                "ctl.style.height = (ht - 90)+\"px\";\n" +//set panel size to window size - some room on top
                "ctl.style.width = (wt)+\"px\";\n" +
            "}\n" +
            "window.onresize=setPanelSize;\n" +
            "window.onload=setPanelSize;\n"
            //load script that relays clicks on the backround panel to the hidden selection clear button
            , true);
            Page.ClientScript.RegisterStartupScript(GetType(), "BindClearButton",
            "var panelId = 'MainContent_Panel1';\n" +
            "var buttonId = 'MainContent_ClearSelectionButton';\n" +
            "document.getElementById(panelId).onclick = function(){\n" +
            "    document.getElementById(buttonId).click();\n" +
            "    return true\n" +
            "};\n"
            , true);




            //create panels TODO: do this using the database or whatever
            List<Panel> panels = new List<Panel>();
            panels.Add(CreateBox("Adv Computer Math", "math bro", 9, null, new string[] { "Algebra 1" }));
            panels.Add(CreateBox("AP Computer Science", "science bro", 10, null, new string[] { "Geometry", "Adv Computer Math" }));
            panels.Add(CreateBox("Data Structures and Algorithms", "data bro", 11, null, new string[] { "AP Computer Science", "Algebra 2" }));
            panels.Add(CreateBox("Adv Computer Studies", "someone remind me to look at these really long descriptions to make sure they don't look weird", 11, null, new string[] { "AP Computer Science" }));
            panels.Add(CreateBox("IT Fundamentals", "it bro", 9, null, null));
            panels.Add(CreateBox("IT Programming DE", "python bro", 10, null, new string[] { "IT Fundamentals" }));
            panels.Add(CreateBox("IT Adv Programming DE", "hey i think i took this class", 10, null, new string[] { "IT Programming DE" }));
            panels.Add(CreateBox("Cybersecurity Systems Tech DE", "security bro", 10, null, new string[] { "IT Fundamentals" }));
            panels.Add(CreateBox("Comp Networking I, II DE", "networks bro", 10, 2.5f, new string[] { "IT Fundamentals" }));
            panels.Add(CreateBox("Comp Networking III, IV DE", "no idea if this is right, mostly just want to see what it does to the tree", 10, 2.5f, new string[] { "Comp Networking I, II DE" }));
            panels.Add(CreateBox("IT Graphics Design", "art bro", 9, null, new string[] { }));
            panels.Add(CreateBox("IT Computer Graphics I", "graphics bro", 10, null, new string[] { "IT Graphics Design:OR:", "Art 1:OR:" }));
            panels.Add(CreateBox("Photography I", "pictures bro", 10, null, new string[] { "IT Graphics Design:OR:", "Art 1:OR:" }));
            panels.Add(CreateBox("IT Web Tech", "web bro", 9, null, new string[] { }));
            panels.Add(CreateBox("Engineering and Robotics", "robots bro", 10, null, new string[] { "Geometry" }));
            panels.Add(CreateBox("Engineering and Robotics II", "did you really expect me to hunt down the descriptions for all these classes? no that's the database people's job", 11, null, new string[] { "Engineering and Robotics" }));
            panels.Add(CreateBox("Sustainability and Renewable Technology", "long name club", 11, null, new string[] {}));


            



            //if it is the first load, don't worry about positioning anything, just get the data and reload
            if (firstLoad)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "getWHData",
                "var whdat = \"WHData:\";\n" +
                "for(let p of document.getElementById(\"MainContent_UpdatePanel1\").children){\n" +
                "    whdat += p.clientWidth + \",\";\n" +
                "    whdat += p.clientHeight + \";\";\n" +
                "}\n" +
                "__doPostBack(null,whdat);\n", true);
                return;
            }


            //find postrequisites
            //double comma is to prevent classses with commas in their name (looking at you, Comp Networking) from being seperated when converting form string to array
            foreach (Panel panel in panels)
            {
                foreach (string prereq in ((HiddenField)panel.FindControl($"{panel.ID}_preq")).Value.Split(new string[] { ",," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Panel item = (Panel)panel.Parent.FindControl(prereq.Replace(":OR:", ""));
                    if (item != null)
                    {
                        ((HiddenField)panel.FindControl($"{item.ID}_ptrq")).Value += panel.ID + ",,";
                    }
                }
            }


            //position panels
            List<Panel> roots = new List<Panel>();
            //y
            double deepest = 0;
            foreach (Panel panel in panels)
            {
                double depth = FindDepth(panel, out bool root);
                if (root)
                {
                    roots.Add(panel);
                }
                if (deepest < depth)
                {
                    deepest = depth;
                }
            }
            foreach (Panel panel in panels)
            {
                double depth = FindDepth(panel, out _);
                panel.Style.Add("top", $"{minHeightOffset * (deepest - depth)}px");
            }
            //x
            PositionRoots(roots);
            foreach (Panel panel in roots)
            {
                AddParentOffsetToChildren(panel);
            }
            //line up panels 
            FixLinePanelRelations(panels);
            //that data that we got from the reload comes up here
            string[] data = Request.Form["__EVENTARGUMENT"].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 3; i < UpdatePanel1.ContentTemplateContainer.Controls.Count; i++)
            {
                if (UpdatePanel1.ContentTemplateContainer.Controls[i] is Panel p)
                {
                    p.Style["left"] = int.Parse(p.Style["left"].Replace("px", "")) + (avgPanelWidth - int.Parse(data[i - 2].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0])) / 2 + "px";
                }
            }


            //get classes to connect with lines
            string lineClasses = "";
            foreach (Panel panel in panels)
            {
                //get the list of prerequisites
                string prereqs = ((HiddenField)panel.FindControl($"{panel.ID}_preq")).Value;
                //for every prerequisite
                foreach (string prereq in prereqs.Split(new string[] { ",," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    //check if it has a tree element, since some may not (ex. geometry)
                    Panel item = (Panel)panel.Parent.FindControl(prereq.Replace(":OR:", ""));
                    if (item != null)
                    {
                        //build a comma seperated list to use for an array
                        lineClasses += "\"" + prereq + "\", ";
                        lineClasses += "\"" + panel.ID + "\", ";
                    }
                }
            }
            //cut off the last comma
            lineClasses = lineClasses.Substring(0, lineClasses.Length - 2);

            //draw lines
            Page.ClientScript.RegisterStartupScript(GetType(), "DrawLines",
            "function makeLines(){\n" +
           $"    const arr = [{lineClasses}];\n" +//create an array using the string
            "    for(let i = 0; i < arr.length-1; i+=2){\n" +
            "        let or = false;\n" +
            "        if(arr[i].endsWith(\":OR:\")){\n" +//read and remove the :OR: tags
            "            arr[i] = arr[i].replace(\":OR:\",\"\");\n" +
            "            or = true;\n" +
            "        }\n" +
            "        if(arr[i+1].endsWith(\":OR:\")){\n" +
            "            arr[i+1] = arr[i+1].replace(\":OR:\",\"\");\n" +
            "            or = true;\n" +
            "        }\n" +
            "        let panel = document.getElementById(\"MainContent_\"+arr[i]);\n" +//find the two elements to connect
            "        let item = document.getElementById(\"MainContent_\"+arr[i+1]);\n" +
            "        var newLine = document.createElementNS('http://www.w3.org/2000/svg', 'line');\n" +//create the line
            "        newLine.setAttribute('id', 'line'+(i/2));\n" +
            "        newLine.setAttribute('x1', (+item.style.left.replace(\"px\", \"\")) + (item.clientWidth / 2) + \"\");\n" +
            "        newLine.setAttribute('y1', (+item.style.top.replace(\"px\", \"\")) + item.clientHeight + \"\");\n" +
            "        newLine.setAttribute('x2', (+panel.style.left.replace(\"px\", \"\")) + (panel.clientWidth / 2) + \"\");\n" +
            "        newLine.setAttribute('y2', (+panel.style.top.replace(\"px\", \"\")) + \"\");\n" +
            "        newLine.setAttribute(\"stroke\", \"black\");\n" +
            "        newLine.setAttribute(\"stroke-width\", \"3\");\n" +
            "        if(or){\n" +//make the line dashed if there was an :OR: tag
            "            newLine.setAttribute(\"stroke-dasharray\", \"5,5\");\n" +
            "        }\n" +
            "        $(document.getElementById(\"LinePanel\")).append(newLine);\n" +//add the line to the svg
            "    }\n" +
            "}\n" +
            "makeLines();\n"
            , true);
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //this is supposed to set the default position and zoom, but only sets the zoom for some reason. also just scrapes the defaults from the reset button
            Page.ClientScript.RegisterStartupScript(GetType(), "PanZoomPosition",
           $"{ResetViewButton.OnClientClick}\n"//take the code from the reset position button and run it
            , true);
        }
        /// <summary>
        /// Similar to GetChildrenOffsetToParents, but instead takes a list of roots and spaces them apropriatly
        /// </summary>
        /// <param name="roots">The list of root elements of the tree (those without parents)</param>
        private void PositionRoots(List<Panel> roots)
        {
            int items = roots.Count;
            if (items == 0)
            {
                return;
            }
            int[] widths = new int[roots.Count];
            int totalNeededWidth = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                Panel item = roots[i];
                widths[i] = GetChildrenOffsetToParent(item);
                totalNeededWidth += minWidthOffset + widths[i];
            }
            totalNeededWidth -= minWidthOffset;
            for (int i = 0; i < roots.Count; i++)
            {
                Panel item = roots[i];
                int prevOffset = 0;
                for (int j = 0; j < i; j++)
                {
                    prevOffset += minWidthOffset + widths[j];
                }
                item.Style.Add("left", $"{(widths[i] / 2) + (avgPanelWidth / 2) + prevOffset - (totalNeededWidth / 2) - (avgPanelWidth / 2)}px");
            }
        }
        /// <summary>
        /// Fix the positions of the tree elements such that none of the values are negative, then set the size of the panel containing the lines to the appropriate size
        /// </summary>
        /// <param name="panels">The list of panels that have been placed on the tree</param>
        private void FixLinePanelRelations(List<Panel> panels)
        {
            int maxX = 0;
            int minX = 0;
            int maxY = 0;
            foreach (Panel panel in panels)
            {
                int left = int.Parse(panel.Style["left"].Replace("px", ""));
                int top = int.Parse(panel.Style["top"].Replace("px", ""));
                if (maxX < left)
                {
                    maxX = left;
                }
                if (minX > left)
                {
                    minX = left;
                }
                if (maxY < top)
                {
                    maxY = top;
                }
            }
            int xshift = 0 - minX;
            maxX += xshift + avgPanelWidth;
            foreach (Panel panel in panels)
            {
                panel.Style["left"] = int.Parse(panel.Style["left"].Replace("px", "")) + xshift + "px";
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "SizeLinePanel",
                $"(document.getElementById(\"LinePanel\")).style.height = \"{maxY}px\";\n" +
                $"(document.getElementById(\"LinePanel\")).style.width = \"{maxX}px\";\n", true);
        }
        /// <summary>
        /// Propigate the offset from a given parent element to its children
        /// </summary>
        /// <param name="panel">The parent element whos offset to propigate</param>
        private void AddParentOffsetToChildren(Panel panel)
        {
            foreach (string pstreq in ((HiddenField)panel.FindControl($"{panel.ID}_ptrq")).Value.Split(new string[] { ",," }, StringSplitOptions.RemoveEmptyEntries))
            {
                Panel item = (Panel)panel.Parent.FindControl(pstreq);
                item.Style["left"] = $"{int.Parse(item.Style["left"].Replace("px", "")) + int.Parse(panel.Style["left"].Replace("px", ""))}px";
                AddParentOffsetToChildren(item);
            }
        }
        /// <summary>
        /// Sets the appropriate offsets on the children elements of the given element such that the children elements do not overlap. Does NOT account for allready present offset, neither parent nor children
        /// </summary>
        /// <param name="panel">The parent of the children to offset</param>
        /// <returns>The width that the given parent takes up</returns>
        private int GetChildrenOffsetToParent(Panel panel)
        {
            //go all the way down the tree
            //once at bottom, offset = 0 and go up 1
            //offset = (widths[i] / 2) + (avgPanelWidth / 2) + prevOffset - (totalNeededWidth / 2) - (avgPanelWidth / 2)
            //go back down the tree later adding parent offset to children
            int items = ((HiddenField)panel.FindControl($"{panel.ID}_ptrq")).Value.Split(new string[] { ",," }, StringSplitOptions.RemoveEmptyEntries).Length;
            if (items == 0)
            {
                return avgPanelWidth;
            }
            string[] array = ((HiddenField)panel.FindControl($"{panel.ID}_ptrq")).Value.Split(new string[] { ",," }, StringSplitOptions.RemoveEmptyEntries);

            int[] widths = new int[array.Length];
            int totalNeededWidth = 0;
            for (int i = 0; i < array.Length; i++)
            {
                string pstreq = array[i];
                Panel item = (Panel)panel.Parent.FindControl(pstreq);
                widths[i] = GetChildrenOffsetToParent(item);
                totalNeededWidth += minWidthOffset +  widths[i];
            }
            totalNeededWidth -= minWidthOffset;
            for (int i = 0; i < array.Length; i++)
            {
                string pstreq = array[i];
                Panel item = (Panel)panel.Parent.FindControl(pstreq);
                int prevOffset = 0;
                for (int j = 0; j < i; j++)
                {
                    prevOffset += minWidthOffset +  widths[j];
                }
                item.Style.Add("left", $"{(widths[i] / 2) + (avgPanelWidth / 2) + prevOffset - (totalNeededWidth / 2) - (avgPanelWidth / 2)}px");
            }
            return totalNeededWidth;
        }

        /// <summary>
        /// Calculates the depth down the tree an element should be, saves it to that element, and returns the value.
        /// </summary>
        /// <param name="panel">The element to calculate the depth of.</param>
        /// <returns>The depth of the element.</returns>
        /// <remarks>Any aditional vertical offset that should be applied to any element should be added here. Output is in double format to allow finer offset to be added</remarks>
        private double FindDepth(Panel panel, out bool root)
        {
            root = false;
            //check if the depth has already been determined
            if (double.TryParse(((HiddenField)panel.FindControl($"{panel.ID}_dpth")).Value.Replace("R",""), out double depth))
            {
                root = ((HiddenField)panel.FindControl($"{panel.ID}_dpth")).Value.Contains("R");
                return depth;
            }
            //get the list of prerequisites
            string prereqs = ((HiddenField)panel.FindControl($"{panel.ID}_preq")).Value;
            //for every prerequisite
            foreach (string prereq in prereqs.Split(new string[] { ",," }, StringSplitOptions.RemoveEmptyEntries))
            {
                //check if it has a tree element, since some may not (ex. geometry)
                Panel item = (Panel)panel.Parent.FindControl(prereq.Replace(":OR:", ""));
                if (item != null)
                {
                    //get the depth of the prerequisite and add 1
                    double d = FindDepth(item, out _) + 1;
                    //make depth the largest found value (in case tree is not balanced)
                    if (d > depth)
                    {
                        depth = d;
                    }
                }
            }
            root = depth == 0;
            //add any aditional offset here

            //save the depth to the panel data
            ((HiddenField)panel.FindControl($"{panel.ID}_dpth")).Value = depth + (root?"R":"");
            return depth;
        }

        /// <summary>
        /// Creates an element and adds it to the visual tree.
        /// </summary>
        /// <param name="className">The name of the class that this element represents, must be unique.</param>
        /// <param name="description">The description of the class that this element represents.</param>
        /// <param name="minGrade">The minimum grade level required to take the class that this element represents. optional, defaults to 9th.</param>
        /// <param name="minGPA">The minimum GPA required to take the class that this element represents. optional.</param>
        /// <param name="prerequisites">The names of the prerequisites required to take the class that this element represents. Note that the names must match in order for them to link correctly. optional.</param>
        /// <returns>The element that was created</returns>
        /// <remarks>This is where the text that shows up on the tree elements is generated</remarks>
        private Panel CreateBox(string className, string description, byte? minGrade, float? minGPA, string[] prerequisites)
        {
            //data defaults
            //double comma is to prevent classses with commas in their name (looking at you, Comp Networking) from being seperated when converting form string to array
            minGrade = minGrade == null ? 9 : minGrade;
            minGPA = minGPA == null ? 0 : minGPA;
            string prereqs = "";
            string prereqsDisplay = "";
            if (prerequisites != null)
            {
                for (int i = 0; i < prerequisites.Length; i++)
                {
                    prereqs += prerequisites[i] + ",,";
                    //prereqsDisplay is a display string, not a data transfer string, so no double commas or :OR: tags here
                    if (prerequisites[i].EndsWith(":OR:") && i + 1 < prerequisites.Length && prerequisites[i + 1].EndsWith(":OR:"))
                    {
                        prereqsDisplay += prerequisites[i].Replace(":OR:", "") + " or ";
                    }
                    else if (i + 1 < prerequisites.Length)
                    {
                        prereqsDisplay += prerequisites[i].Replace(":OR:", "") + ", ";
                    }
                    else
                    {
                        prereqsDisplay += prerequisites[i].Replace(":OR:", "");
                    }
                }
            }

            Panel panel = new Panel();
            panel.ID = className;
            panel.Style.Add("position", "absolute");

            Button button = new Button();
            //format for closed elements is here
            string bText = $"Class: {className}\nMinimum Grade: {minGrade}th" + (minGPA == 0 ? "" : $"\nMinimum GPA: {minGPA}");
            button.Text = WrapText(bText, LBT);
            button.Style.Add("text-align", "left");
            button.Style.Add("width", "100%");
            button.Style.Add("height", "100%");
            button.Style.Add("background-color", "var(--button-background)");

            Button button2 = new Button();
            //format for open elements is here
            string b2Text = $"Class: {className}\nDescription: {description}\nMinimum Grade: {minGrade}th" + (minGPA == 0 ? "" : $"\nMinimum GPA: {minGPA}") + (prereqs == "" ? "" : $"\nPrerequisites: {prereqsDisplay}");
            button2.Text = WrapText(b2Text, LBT);
            button2.Style.Add("text-align", "left");
            button2.Style.Add("width", "100%");
            button2.Style.Add("height", "100%");
            button2.Style.Add("background-color", "var(--button-background)");
            button2.Visible = false;

            Button infobutton = new Button();
            infobutton.Text = "i";
            infobutton.Font.Size = FontUnit.Smaller;
            infobutton.Style.Add("text-align", "center");
            infobutton.Style.Add("position", "absolute");
            infobutton.Style.Add("width", "20px");
            infobutton.Style.Add("height", "20px");
            infobutton.Style.Add("right", "0px");
            infobutton.Style.Add("bottom", "0px");
            infobutton.Style.Add("background-color", "var(--button-background)");

            //note that button and button2 are very similar in both form and function, and any adjustments to format or selection logic, or any visual changes caused by selection or otherwise, should likely be applied to both for visual continuity
            button.Click += (o, e) => { /*TODO:select logic*/ };
            panel.Controls.Add(button);
            button2.Click += (o, e) => { /*TODO:select logic*/ };
            panel.Controls.Add(button2);
            infobutton.Click += (o, e) => { SwitchView(panel); };
            panel.Controls.Add(infobutton);

            //bunch of hidden objects for data storage
            HiddenField nameField = new HiddenField();
            nameField.Value = className;
            nameField.ID = className + "_name";
            panel.Controls.Add(nameField);

            HiddenField descField = new HiddenField();
            descField.Value = description;
            descField.ID = className + "_desc";
            panel.Controls.Add(descField);

            HiddenField mgrdField = new HiddenField();
            mgrdField.Value = minGrade + "";
            mgrdField.ID = className + "_mgrd";
            panel.Controls.Add(mgrdField);

            HiddenField mgpaField = new HiddenField();
            mgpaField.Value = minGPA + "";
            mgpaField.ID = className + "_mgpa";
            panel.Controls.Add(mgpaField);

            HiddenField preqField = new HiddenField();
            preqField.Value = prereqs;
            preqField.ID = className + "_preq";
            panel.Controls.Add(preqField);

            HiddenField ptrqField = new HiddenField();
            ptrqField.ID = className + "_ptrq";
            panel.Controls.Add(ptrqField);

            HiddenField dpthField = new HiddenField();
            if (prereqs == "")
                dpthField.Value = 0 + "R";
            dpthField.ID = className + "_dpth";
            panel.Controls.Add(dpthField);

            UpdatePanel1.ContentTemplateContainer.Controls.Add(panel);
            return panel;
        }
        /// <summary>
        /// Makes sure that every line of the given string is less than the given number of characters long, adding line breaks as needed
        /// </summary>
        /// <param name="text">The text to wrap</param>
        /// <param name="threshold">The maximum characters per line</param>
        /// <returns>The edited text</returns>
        /// <remarks>Likely will not work with thresholds less than 4 or 5</remarks>
        private static string WrapText(string text, int threshold)
        {
            for (int i = 0; i < text.Split('\n').Length; i++)
            {//text wrapping woo. basically just checks character count between line breaks (\n), and if above threshold attempt to split on the nearest space character
                if (text.Split('\n')[i].Length > threshold)
                {
                    if (text.Split('\n')[i].Substring(4, threshold - 4).Contains(' '))
                    {//if there is a space character, split there and add an extra space for indent because it looks nice
                        text = text.Replace(text.Split('\n')[i], text.Split('\n')[i].Insert(text.Split('\n')[i].LastIndexOf(' ', threshold), "\n "));
                    }
                    else
                    {//if there is no space character, break the line on the 29th character and add a hyphen and the indent space
                        text = text.Replace(text.Split('\n')[i], text.Split('\n')[i].Insert(threshold - 1, "-\n  "));
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Switches which element is folded out.
        /// </summary>
        /// <param name="panel">The new element to fold out. optional, null collapses all.</param>
        /// <remarks>closing is not the most consistent for some reason, thus just saving the open one does not work</remarks>
        private void SwitchView(Panel panel)
        {
            //find the currently selected panel
            Panel activeView = Session["TVActive"] == null ? null : (Panel)(UpdatePanel1.ContentTemplateContainer.FindControl((string)Session["TVActive"]));
            //close all open panels (was just the selected one but some got stuck open)
            foreach (Control item in UpdatePanel1.ContentTemplateContainer.Controls)
            {
                if (item as Panel != null)
                {
                    item.Controls[1].Visible = false;
                    item.Controls[0].Visible = true;
                    item.Controls[2].Visible = true;
                }
                activeView = null;
            }//open new panel
            if (panel != null)
            {
                panel.Controls[0].Visible = false;
                panel.Controls[2].Visible = false;
                panel.Controls[1].Visible = true;
                activeView = panel;
            }//save selected panel
            Session["TVActive"] = activeView?.ID;
        }
        /// <summary>
        /// Event handler for clear selection button.
        /// </summary>
        protected void ClearSelectionButton_Click(object sender, EventArgs e)
        {
            SwitchView(null);
            UpdatePanel1.Update();
        }
    }
}