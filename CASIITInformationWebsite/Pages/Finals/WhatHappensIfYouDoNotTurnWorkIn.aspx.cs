using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite.Pages.Finals
{
    public partial class WhatHappensIfYouDoNotTurnWorkIn : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Q1A.CheckedChanged += Q1_Checked;
            Q1B.CheckedChanged += Q1_Checked;
            Q1C.CheckedChanged += Q1_Checked;
            Q1D.CheckedChanged += Q1_Checked;
        }

        private RadioButton GetSelected()
        {
            

            if (Q1A.Checked)
            {
                return Q1A;
            } else if (Q1B.Checked)
            {
                return Q1B;
            } else if (Q1C.Checked)
            {
                return Q1C;
            } else if (Q1D.Checked)
            {
                return Q1D;
            } else
            {
                return null;
            }
        }

        protected void Q1_Checked(object sender, EventArgs e)
        {
            if (GetSelected() != null)
            {
                if (GetSelected().Equals(Q1A))
                {
                    Q1Label.Visible = true;
                } else
                {
                    Q1Label.Visible = false;
                }
            } else
            {
                Q1Label.Visible = false;
            }
        }
    }
}