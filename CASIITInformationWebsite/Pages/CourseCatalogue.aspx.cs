using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite
{
    public partial class Contact : Page
    {
        private String concentrationFilter;
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

            //for(int i = 0; i < NUMCLASSES; i++)
            //{
            //    rows[i] = Class1;
            //}
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
            //foreach (TableRow tr in rows)
            //{
                
            //}
            Class1.Visible = ((Class1Concentration.Text.Equals(concentrationFilter) || concentrationFilter == "None") && Double.Parse(Class1MinGPA.Text) < gpaFilter && int.Parse(Class1MinYear.Text) <= yearFilter) || concentrationFilter == "None";
        }
    }
}