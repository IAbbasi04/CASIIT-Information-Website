using CASIITInformationWebsite.Common_Elements;
using CASIITInformationWebsite.Common_Elements.Csharp;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite
{
    public partial class CourseCatalogue : Page
    {
        private string concentrationFilter = string.Empty;
        private double gpaFilter;
        private int yearFilter;

        private const int NUMCLASSES = 1;
        private TableRow[] rows = new TableRow[NUMCLASSES];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropDownList();
            }
            
        }

            //for(int i = 0; i < NUMCLASSES; i++)
            //{
            //    rows[i] = Class1;
            //}

        public class Filter
        {

            public List<Func<Class, bool>> filter = new List<Func<Class, bool>>();
            private double minWeight;
            private double maxWeight;
            private bool dualEnrolled;
            private double hsCreditMin = 0;
            private double hsCreditMax = 10;
            private double collegeCreditMin = 0;
            private double collegeCreditMax = 10;
            private double minGpa = 0;
            private double maxGpa = 5;
            private int minYear = 0;
            private int maxYear = 4;

            public Filter(
                double courseWeightRangeMin = 0,
                double courseWeightRangeMax = 5.0,
                bool isDualEnrolled = false,
                double hsCreditRangeMin = 0,
                double hsCreditRangeMax = 10,
                double collegeCreditRangeMin = 0,
                double collegeCreditRangeMax = 10,
                double gpaRangeMin = 0.0,
                double gpaRangeMax = 10,
                int yearRangeMin = 0,
                int yearRangeMax = 4)
            {
                minWeight = courseWeightRangeMin;
                maxWeight = courseWeightRangeMax;
                dualEnrolled = isDualEnrolled;
                hsCreditMin = hsCreditRangeMin;
                hsCreditMax = hsCreditRangeMax;
                collegeCreditMin = collegeCreditRangeMin;
                collegeCreditMax = collegeCreditRangeMax;
                minGpa = gpaRangeMin;
                maxGpa = gpaRangeMax;
                minYear = yearRangeMin;
                maxYear = yearRangeMax;

                filter.Add(new Func<Class, bool>(CourseWeightRange));
                filter.Add(new Func<Class, bool>(DualEnrolledSelector));
                filter.Add(new Func<Class, bool>(HSCreditRange));
                filter.Add(new Func<Class, bool>(CollegeCreditRange));
                filter.Add(new Func<Class, bool>(GPARange));
                filter.Add(new Func<Class, bool>(YearRange));

            }

            private bool CourseWeightRange(Class course)
            {
                double value = course.course_weight;
                return (minWeight <= value) && ( value <= maxWeight );
            }

            private bool DualEnrolledSelector(Class course)
            {
                if (dualEnrolled) return (course.dual_enrolled == 1);
                else return true;
            }

            private bool HSCreditRange(Class course)
            {
                double value = course.hs_credit;
                return (hsCreditMin <= value) && (value <= hsCreditMax);
            }

            private bool CollegeCreditRange(Class course)
            {
                double value = course.college_credit;
                return (collegeCreditMin <= value) && (value <= collegeCreditMax);
            }

            private bool GPARange( Class course)
            {
                double value = course.prerequisite.min_GPA;
                return (minGpa <= value) && (maxGpa <= hsCreditMax);
            }

            private bool YearRange( Class course)
            {
                double value = course.prerequisite.min_year;
                return (minYear <= value) && (value <= maxYear);
            }

        }
        private void PopulateDropDownList()
        {
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add(new ListItem("None", "0"));
            DropDownList1.Items.Add(new ListItem("Interactive Technology", "1"));
            DropDownList1.Items.Add(new ListItem("Applied Sciences", "2"));
            DropDownList1.Items.Add(new ListItem("Information Technology", "3"));

            DropDownList2.Items.Add(new ListItem("9", "0"));
            DropDownList2.Items.Add(new ListItem("10", "1"));
            DropDownList2.Items.Add(new ListItem("11", "2"));
            DropDownList2.Items.Add(new ListItem("12", "3"));
        }
        //protected void ddlMyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string filterValue = DropDownList1.SelectedValue;
        //    // Replace this with your own code to filter your data.
        //    // You can use a SQL query or LINQ to filter your data.
        //}

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            concentrationFilter = DropDownList1.SelectedItem.Text;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gpaFilter = Double.Parse(TextBox1.Text);
                Label3.Visible = gpaFilter < 0.0 || gpaFilter > 5.0;
            }
            catch
            {
                Label3.Visible = true;
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            yearFilter = int.Parse(DropDownList2.SelectedValue);
        }

        protected void GoButton_Click(object sender, EventArgs e)
        {
            PropogateList();

            //Class1.Visible = ((Class1Concentration.Text.Equals(concentrationFilter) || concentrationFilter == "None") && Double.Parse(Class1MinGPA.Text) < gpaFilter && int.Parse(Class1MinYear.Text) <= yearFilter) || concentrationFilter == "None";
        }

        private void PropogateList()
        {
            List<Class> courses = new List<Class>();
            try
            {
                gpaFilter = Double.Parse(TextBox1.Text);
            }
            catch
            {
                gpaFilter = 0;
            }
            if (DropDownList1.SelectedItem.Text.Equals("None"))
            {
                concentrationFilter = "";
            }
            else concentrationFilter = DropDownList1.SelectedItem.Text;

            //if (CheckBox1.Checked && Pages.Login.currentUser != null) courses = SQLQuerier.AllAvailableClasses(Pages.Login.currentUser);
             courses = SQLQuerier.FilterSelect(gpaMin: gpaFilter, yearMin: DropDownList2.SelectedIndex, concentration: concentrationFilter );
            //Class1.Visible = ((Class1Concentration.Text.Equals(concentrationFilter) ||
            //concentrationFilter == "None") &&
            //Double.Parse(Class1MinGPA.Text) < gpaFilter &&
            //int.Parse(Class1MinYear.Text) <= yearFilter) ||
            //concentrationFilter == "None";

            foreach (Class course in courses)
            {
                TableRow courseRow = new TableRow();

                //name
                {
                    TableCell cell = new TableCell();
                    cell.BorderWidth = 2;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.BackColor = Color.White;
                    cell.Text = "" + course.course_name;
                    courseRow.Cells.Add(cell);
                }
                // description
                {
                    TableCell cell = new TableCell();
                    cell.BorderWidth = 2;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.BackColor = Color.White;
                    cell.Text = "" + course.description;
                    courseRow.Cells.Add(cell);
                }
                // minimum year
                {
                    TableCell cell = new TableCell();
                    cell.BorderWidth = 2;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.BackColor = Color.White;
                    cell.Text = "" + (9 + course.prerequisite.min_year);
                    courseRow.Cells.Add(cell);
                }
                // minimum gpa
                {
                    TableCell cell = new TableCell();
                    cell.BorderWidth = 2;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.BackColor = Color.White;
                    cell.Text = "" + course.prerequisite.min_GPA;
                    courseRow.Cells.Add(cell);
                }
                // concentration
                {
                    TableCell cell = new TableCell();
                    cell.BorderWidth = 2;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.BackColor = Color.White;
                    cell.Text = "" + course.concentration;
                    courseRow.Cells.Add(cell);
                }
                // prerequisites
                {
                    TableCell cell = new TableCell();
                    cell.BorderWidth = 2;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.BackColor = Color.White;
                    cell.Text = "" + course.prerequisite.required_classes;
                    courseRow.Cells.Add(cell);
                }

                Table6.Rows.Add(courseRow);
            }
        }
    }
    
}