using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite.Pages
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        string fName;
        string lName;
        string email;
        string password;
        string confirmPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bCreateAccount_Click(object sender, EventArgs e)
        {
            // save data
            Save_Member_Data();
            
        }

        private void Save_Member_Data()
        {
            // convert info to lowercase so it is saved uniformly only if changing the case doesn't change its meaning
            fName = tbCreateAccountFirstName.Text.ToLower();
            lName = tbCreateAccountLastName.Text.ToLower();
            email = tbCreateAccountEmail.Text.ToLower();

            // case in a password does matter
            password = tbCreateAccountPassword.Text;
            confirmPassword = tbCreateAccountConfirmPassword.Text;

            if (!Indicate_Empty_TextBoxes()) // if there are no empty textboxes that need to be indicated continue
            {
                // idicate to the user if the password and confirm password do not match

            }



        }

        private bool Indicate_Empty_TextBoxes()
        {
            bool emptyTextBoxPresent = false;

            // indicate to the user if a text box is not filled out
            if (fName == "")
            {
                // add indicator
                Label lbMissingInformation = new Label();
                lbMissingInformation.Text = "Missing Values";
                lbMissingInformation.ForeColor = Color.Red;
                lbMissingInformation.Height = 10;

                tableCreateAccount.Rows[0].Cells[0].Controls.AddAt(0, lbMissingInformation);

                //Add_Indicator(lbCreateAccountFirstName);

                emptyTextBoxPresent = true;
            }
            if (lName == "")
            {
                // add indicator
                Label lbMissingInformation = new Label();
                lbMissingInformation.Text = "Missing Values";
                lbMissingInformation.ForeColor = Color.Red;
                lbMissingInformation.Height = 10;

                tableCreateAccount.Rows[1].Cells[0].Controls.Add(lbMissingInformation);
                emptyTextBoxPresent = true;
            }
            if (email == "")
            {
                // add indicator
                Label lbMissingInformation = new Label();
                lbMissingInformation.Text = "Missing Values";
                lbMissingInformation.ForeColor = Color.Red;
                lbMissingInformation.Height = 10;

                tableCreateAccount.Rows[3].Cells[0].Controls.Add(lbMissingInformation);

                emptyTextBoxPresent = true;
            }
            if (password == "")
            {
                // add indicator
                Label lbMissingInformation = new Label();
                lbMissingInformation.Text = "Missing Values";
                lbMissingInformation.ForeColor = Color.Red;
                lbMissingInformation.Height = 10;

                tableCreateAccount.Rows[5].Cells[0].Controls.Add(lbMissingInformation);

                emptyTextBoxPresent = true;
            }
            if (confirmPassword == "")
            {
                // add indicator
                Label lbMissingInformation = new Label();
                lbMissingInformation.Text = "Missing Values";
                lbMissingInformation.ForeColor = Color.Red;
                lbMissingInformation.Height = 10;

                tableCreateAccount.Rows[7].Cells[0].Controls.Add(lbMissingInformation);

                emptyTextBoxPresent = true;
            }

            return emptyTextBoxPresent;
        }

        // ADD INDICATOR WITHOUT HARD CODE
        //private void Add_Indicator(Control control)
        //{
        //    Label indicator = Create_Indicator();
        //    System.Drawing.Point point = Get_Coordinate(tableCreateAccount, control);
        //    if (!point.Equals(new System.Drawing.Point(-1, -1))) // as long as the point exists
        //    {
        //        // add indicator
        //        tableCreateAccount.Rows[point.X].Cells[0].Controls.Add(indicator);
        //    }
        //    else
        //    {
        //        // error
        //    }
        //}
        //private Label Create_Indicator()
        //{
        //    Label lbMissingInformation = new Label();
        //    lbMissingInformation.Text = "Missing Values";
        //    lbMissingInformation.ForeColor = Color.White;
        //    lbMissingInformation.Height = 10;

        //    return lbMissingInformation;
        //}
        //private System.Drawing.Point Get_Coordinate(Table table, Control control)
        //{
        //    // loop through table rows
        //    for (int r = 0; r < table.Rows.Count; r++)
        //    {
        //        // loop through table cells in row
        //        TableRow tr = table.Rows[r];
        //        for (int c = 0; c < tr.Cells.Count; c++)
        //        {
        //            // loop through controls in table cell
        //            TableCell tc = tr.Cells[c];
        //            foreach (Control currControl in tc.Controls)
        //            {
        //                if (currControl == control) // if the current control is the control queried upon, return the coordinate
        //                {
        //                    return new System.Drawing.Point(r, c);
        //                }
        //            }
        //        }
        //    }
        //    // the control queried upon is not in the table
        //    return new System.Drawing.Point(-1, -1);
        //}
    }
}