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
        const int minWidthOffset = 250;//the ideal minimum hoorizontal distance to keep elements apart, measured form aproximate end to aproximate end
        const int avgPanelWidth = 200;//the width to assume the panels are
        protected void Page_Init(object sender, EventArgs e)
        {
            //on first page load, get js to pickup clientHeight and clientWidth for each panel, then push that to the server and reload, hopefully before clientside is ever displayed so it doesn't look wierd
            bool firstLoad = false;
            if (Request.Form["__EVENTARGUMENT"] == null || !Request.Form["__EVENTARGUMENT"].StartsWith("WHData"))
            {
                firstLoad = true;
            }



            //load script to init Panzoom
            Page.ClientScript.RegisterStartupScript(GetType(), "InitPanZoom",
            "elem = document.getElementById(\"MainContent_Panel2\");\n" +
            "pz = Panzoom(elem, {" +
                "contain: true," +
                "canvas: true" +
            "});" +
            "elem.parentElement.addEventListener('wheel', pz.zoomWithWheel);\n"
            , true);
            //load script that automatically resizes the view panel to fit the screen
            Page.ClientScript.RegisterStartupScript(GetType(), "AutoSize",
            "function setPanelSize() {\n" +
                "var bw = navigator.appName;\n" +
                "if (bw == \"Microsoft Internet Explorer\")\n" +
                "{\n" +
                    "var ht = document.body.clientHeight;\n" +
                    "var wt = document.body.clientWidth;\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                    "var ht = window.innerHeight;\n" +
                    "var wt = window.innerWidth;\n" +
                "}\n" +
                "var ctl = document.getElementById(\"MainContent_Panel1\");\n" +
                "ctl.style.height = (ht - 90)+\"px\";\n" +
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




            //create panels TODO: do this properly
            List<Panel> panels = new List<Panel>();
            panels.Add(CreateBox("Adv Computer Math", "math bro", 9, null, new string[] { "Algebra 1" }));
            panels.Add(CreateBox("AP Computer Science", "science bro", 10, null, new string[] { "Geometry", "Adv Computer Math" }));
            panels.Add(CreateBox("IT Fundamentals", "it bro", 9, null, null));
            panels.Add(CreateBox("IT Programming DE", "python bro", 10, null, new string[] { "IT Fundamentals" }));
            panels.Add(CreateBox("Cybersecurity Systems Tech DE", "security bro", 10, null, new string[] { "IT Fundamentals" }));
            panels.Add(CreateBox("Comp Networking I, II DE", "networks bro", 10, 2.5f, new string[] { }));
            panels.Add(CreateBox("IT Graphics Design", "art bro", 9, null, new string[] { }));
            panels.Add(CreateBox("IT Computer Graphics I", "graphics bro", 10, null, new string[] { "IT Graphics Design:OR:", "Art 1:OR:" }));
            panels.Add(CreateBox("Photography I", "pictures bro", 10, null, new string[] { "IT Graphics Design:OR:", "Art 1:OR:" }));
            panels.Add(CreateBox("IT Web Tech", "web bro", 9, null, new string[] { }));
            panels.Add(CreateBox("Engineering and Robotics", "robots bro", 9, null, new string[] { "Geometry" }));

            //string myConnectionString = "server=p3nlmysql165plsk.secureserver.net;uid=CSIsAwesome;pwd=Casiit2117Class;database=battlefield_casiit";

            //using (MySqlConnection conn = new MySqlConnection(myConnectionString))
            //{
            //    conn.ConnectionString = myConnectionString;
            //    conn.Open();

            //    String sql = "SELECT id,course_name,course_weight,description,dual_enrolled,hs_credit,college_credit,prerequisites FROM courses";

            //    using (MySqlCommand command = new MySqlCommand(sql, conn))
            //    {
            //        using (MySqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                //JsonConvert.
            //                // reader.GetValue(7)
            //                panels.Add(CreateBox(reader.GetString(1), reader.GetString(3), null, null, null));
            //            }
            //        }
            //    }
            //}



            //if it is the first load, don't worry about positioning anything, just get the data and go
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
            foreach (Panel panel in panels)
            {
                foreach (string prereq in ((HiddenField)panel.FindControl($"{panel.ID}_preq")).Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Panel item = (Panel)panel.Parent.FindControl(prereq.Replace(":OR:", ""));
                    if (item != null)
                    {
                        ((HiddenField)panel.FindControl($"{item.ID}_ptrq")).Value += panel.ID + ", ";
                    }
                }
            }


            //position panels
            List<Panel> roots = new List<Panel>();
            //y
            int deepest = 0;
            foreach (Panel panel in panels)
            {
                int depth = FindDepth(panel);
                if (depth == 0)
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
                int depth = FindDepth(panel);
                panel.Style.Add("top", $"{minHeightOffset * (deepest - depth)}px");
            }
            //x
            foreach (Panel panel in roots)
            {
                GetChildrenOffsetToParent(panel);
            }
            PositionRoots(roots);
            foreach (Panel panel in roots)
            {
                AddParentOffsetToChildren(panel);
            }
            //line up pannels 
            FixLinePanelRelations(panels);
            string[] data = Request.Form["__EVENTARGUMENT"].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 3; i < UpdatePanel1.ContentTemplateContainer.Controls.Count; i++)
            {
                if (UpdatePanel1.ContentTemplateContainer.Controls[i] is Panel p)
                {
                    p.Style["left"] = int.Parse(p.Style["left"].Replace("px", "")) + (avgPanelWidth - int.Parse(data[i - 2].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0])) / 2 + "px";
                }
            }


            //get line classes
            string lineClasses = "";
            foreach (Panel panel in panels)
            {
                //get the list of prerequisites
                string prereqs = ((HiddenField)panel.FindControl($"{panel.ID}_preq")).Value;
                //for every prerequisite
                foreach (string prereq in prereqs.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    //check if it has a tree element, since some may not (ex. geometry)
                    Panel item = (Panel)panel.Parent.FindControl(prereq.Replace(":OR:", ""));
                    if (item != null)
                    {
                        lineClasses += "\"" + prereq + "\", ";
                        lineClasses += "\"" + panel.ID + "\", ";
                    }
                }
            }
            lineClasses = lineClasses.Substring(0, lineClasses.Length - 1);

            //draw lines
            Page.ClientScript.RegisterStartupScript(GetType(), "DrawLines",
            "function makeLines(){\n" +
           $"    const arr = [{lineClasses}];\n" +
            "    for(let i = 0; i < arr.length-1; i+=2){\n" +
            "        let or = false;\n" +
            "        if(arr[i].endsWith(\":OR:\")){\n" +
            "            arr[i] = arr[i].replace(\":OR:\",\"\");" +
            "            or = true;\n" +
            "        }\n" +
            "        if(arr[i+1].endsWith(\":OR:\")){\n" +
            "            arr[i+1] = arr[i+1].replace(\":OR:\",\"\");" +
            "            or = true;\n" +
            "        }\n" +
            "        let panel = document.getElementById(\"MainContent_\"+arr[i]);" +
            "        let item = document.getElementById(\"MainContent_\"+arr[i+1]);" +
            "        var newLine = document.createElementNS('http://www.w3.org/2000/svg', 'line');\n" +
            "        newLine.setAttribute('id', 'line'+(i/2));\n" +
            "        newLine.setAttribute('x1', (+item.style.left.replace(\"px\", \"\")) + (item.clientWidth / 2) + \"\");\n" +
            "        newLine.setAttribute('y1', (+item.style.top.replace(\"px\", \"\")) + item.clientHeight + \"\");\n" +
            "        newLine.setAttribute('x2', (+panel.style.left.replace(\"px\", \"\")) + (panel.clientWidth / 2) + \"\");\n" +
            "        newLine.setAttribute('y2', (+panel.style.top.replace(\"px\", \"\")) + \"\");\n" +
            "        newLine.setAttribute(\"stroke\", \"black\");\n" +
            "        newLine.setAttribute(\"stroke-width\", \"3\");\n" +
            "        if(or){\n" +
            "            newLine.setAttribute(\"stroke-dasharray\", \"5,5\");\n" +
            "        }\n" +
            "        $(document.getElementById(\"LinePanel\")).append(newLine);\n" +
            "    }\n" +
            "}\n" +
            "makeLines();\n"
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
            //if even, offset = (((i-(count-1)/2)<=0?(i-(count-1)/2)-1:(i-(count-1)/2))-0.5)*(minWidthOffset + avgPanelWidth)
            //if odd, offset = ((i-(count-1)/2))*(minWidthOffset + avgPanelWidth)
            if (items % 2 == 0)
            {
                for (int i = 0; i < roots.Count; i++)
                {
                    Panel pstreq = roots[i];
                    GetChildrenOffsetToParent(pstreq);
                    pstreq.Style.Add("left", $"{((i - (items - 1) / 2) - 0.5) * (minWidthOffset + avgPanelWidth)}px");
                }
            }
            else
            {
                for (int i = 0; i < roots.Count; i++)
                {
                    Panel pstreq = roots[i];
                    GetChildrenOffsetToParent(pstreq);
                    pstreq.Style.Add("left", $"{((i - (items - 1) / 2)) * (minWidthOffset + avgPanelWidth)}px");
                }
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
            foreach (string pstreq in ((HiddenField)panel.FindControl($"{panel.ID}_ptrq")).Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
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
        private void GetChildrenOffsetToParent(Panel panel)
        {
            //go all the way down the tree
            //once at bottom, offset = 0 and go up 1
            //offset:
            //if even, offset = (((i-(count-1)/2)<=0?(i-(count-1)/2)-1:(i-(count-1)/2))-0.5)*(minWidthOffset + avgPanelWidth)
            //if odd, offset = ((i-(count-1)/2))*(minWidthOffset + avgPanelWidth)
            //go back down the tree later adding parent offset to children
            int items = ((HiddenField)panel.FindControl($"{panel.ID}_ptrq")).Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Length;
            if (items == 0)
            {
                return;
            }

            if (items % 2 == 0)
            {
                string[] array = ((HiddenField)panel.FindControl($"{panel.ID}_ptrq")).Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < array.Length; i++)
                {
                    string pstreq = array[i];
                    Panel item = (Panel)panel.Parent.FindControl(pstreq);
                    GetChildrenOffsetToParent(item);
                    item.Style.Add("left", $"{((i - (items - 1) / 2) - 0.5) * (minWidthOffset + avgPanelWidth)}px");
                }
            }
            else
            {
                string[] array = ((HiddenField)panel.FindControl($"{panel.ID}_ptrq")).Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < array.Length; i++)
                {
                    string pstreq = array[i];
                    Panel item = (Panel)panel.Parent.FindControl(pstreq);
                    GetChildrenOffsetToParent(item);
                    item.Style.Add("left", $"{((i - (items - 1) / 2)) * (minWidthOffset + avgPanelWidth)}px");
                }
            }
        }

        /// <summary>
        /// Calculates the depth down the tree an element should be, saves it to that element, and returns the value.
        /// </summary>
        /// <param name="panel">The element to calculate the depth of.</param>
        /// <returns>The depth of the element.</returns>
        private int FindDepth(Panel panel)
        {
            //check if the depth has already been determined
            if (int.TryParse(((HiddenField)panel.FindControl($"{panel.ID}_dpth")).Value, out int depth))
            {
                return depth;
            }
            //get the list of prerequisites
            string prereqs = ((HiddenField)panel.FindControl($"{panel.ID}_preq")).Value;
            //for every prerequisite
            foreach (string prereq in prereqs.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
            {
                //check if it has a tree element, since some may not (ex. geometry)
                Panel item = (Panel)panel.Parent.FindControl(prereq.Replace(":OR:", ""));
                if (item != null)
                {
                    //get the depth of the prerequisite and add 1
                    int d = FindDepth(item) + 1;
                    //make depth the largest found value (in case tree is not balanced)
                    if (d > depth)
                    {
                        depth = d;
                    }
                }
            }
            //save the depth to the panel data
            ((HiddenField)panel.FindControl($"{panel.ID}_dpth")).Value = depth + "";
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
        private Panel CreateBox(string className, string description, byte? minGrade, float? minGPA, string[] prerequisites)
        {
            //data defaults
            minGrade = minGrade == null ? 9 : minGrade;
            minGPA = minGPA == null ? 0 : minGPA;
            string prereqs = "";
            string prereqsDisplay = "";
            if (prerequisites != null)
            {
                for (int i = 0; i < prerequisites.Length; i++)
                {
                    prereqs += prerequisites[i] + ", ";
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
            button.Text = $"Class: {className}\nMinimum Grade: {minGrade}th" + (minGPA == 0 ? "" : $"\nMinimum GPA: {minGPA}");
            button.Style.Add("width", "100%");
            button.Style.Add("height", "100%");
            button.Style.Add("background-color", "var(--button-background)");
            Button button2 = new Button();
            button2.Text = $"Class: {className}\nDescription: {description}\nMinimum Grade: {minGrade}th" + (minGPA == 0 ? "" : $"\nMinimum GPA: {minGPA}") + (prereqs == "" ? "" : $"\nPrerequisites: {prereqsDisplay}");
            button2.Style.Add("width", "100%");
            button2.Style.Add("height", "100%");
            button2.Style.Add("background-color", "var(--button-background)");
            button2.Visible = false;
            button.Click += (o, e) => { SwitchView(panel); };
            panel.Controls.Add(button);
            button2.Click += (o, e) => { SwitchView(panel); /*TODO:select logic*/ };
            panel.Controls.Add(button2);

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
                dpthField.Value = 0 + "";
            dpthField.ID = className + "_dpth";
            panel.Controls.Add(dpthField);

            UpdatePanel1.ContentTemplateContainer.Controls.Add(panel);
            return panel;
        }
        /// <summary>
        /// Switches which element is folded out.
        /// </summary>
        /// <param name="panel">The new element to fold out. optional, null collapses all.</param>
        private void SwitchView(Panel panel)
        {
            Panel activeView = Session["TVActive"] == null ? null : (Panel)(UpdatePanel1.ContentTemplateContainer.FindControl((string)Session["TVActive"]));

            if (activeView != null)
            {
                activeView.Controls[1].Visible = false;
                activeView.Controls[0].Visible = true;
                activeView = null;
            }
            if (panel != null)
            {
                panel.Controls[0].Visible = false;
                panel.Controls[1].Visible = true;
                activeView = panel;
            }
            Session["TVActive"] = activeView?.ID;
        }
        /// <summary>
        /// Event handler relay for clear selection button.
        /// </summary>
        protected void ClearSelectionButton_Click(object sender, EventArgs e)
        {
            SwitchView(null);
            UpdatePanel1.Update();
        }
    }
}