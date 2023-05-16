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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropDownList();
            }
        }
        private void PopulateDropDownList()
        {
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add(new ListItem("Concentrations", ""));
            DropDownList1.Items.Add(new ListItem("Interactive Technology", "1"));
            DropDownList1.Items.Add(new ListItem("Applied Sciences", "2"));
            DropDownList1.Items.Add(new ListItem("Information Technology", "3"));
        }
        protected void ddlMyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterValue = DropDownList1.SelectedValue;
            // Replace this with your own code to filter your data.
            // You can use a SQL query or LINQ to filter your data.
        }

    }
}