using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CasiitCourses
{
    public partial class Many_Snathon_Player_Select : System.Web.UI.Page
    {
        private int Number_of_Players = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // DETERMINE THE INITIAL NUMBER OF PLAYERS

            // set the number of players to the number of visible controls in the update panel
            foreach (Control control in Get_Non_Literal_Controls_From_Update_Panel(upPlayerShowcase))
            {
                // for every visible non literal control, increment the number of players
                if (control.Visible)
                    Number_of_Players++;
            }


        }

        protected void bStart_Click(object sender, EventArgs e)
        {
            // store the number of players in a place that the next page can reach
            Session["PlayerCount"] = Number_of_Players;

            // swap page 
            Response.Redirect("~/Snathon/ManySnathon.aspx", false); // the false silents the ThreadAbortException that will always occur when a using Response.Redirect
        }


        protected void bAddPlayer_Click(object sender, EventArgs e)
        {
            Modify_Visible_Players(true); // add player
        }

        protected void bRemovePlayer_Click(object sender, EventArgs e)
        {
            Modify_Visible_Players(false); // remove player
        }

        private List<Control> Get_Non_Literal_Controls_From_Update_Panel(UpdatePanel updatePanel)
        {
            // make a list of only non literal controls stored in the update panel
            // (i guess these literal controls are added by the system for some of the update panels functionality
            // but they were not added explicitly by the programmer)
            List<Control> controls = new List<Control>();
            foreach (Control control in upPlayerShowcase.ContentTemplateContainer.Controls)
            {
                if (!(control is System.Web.UI.LiteralControl)) // if the current control is not a literal control
                    controls.Add((System.Web.UI.Control)control); // add it to a list
                //System.Diagnostics.Debug.WriteLine(control);
            }

            return controls;
        }

        private void Modify_Visible_Players(bool adding)
        {
            // instructions:
            // If adding = true: Make_Next_Player_Image_Visible
            // If adding = false, then removing: Make_Last_Made_Visible_Player_Hidden

            // make a list of only the image controls stored in the update pannel bc there are LiteralControls also stored in update pannels
            List<Image> images = new List<Image>();
            foreach (Control control in upPlayerShowcase.ContentTemplateContainer.Controls)
            {
                if (control is System.Web.UI.WebControls.Image)
                    images.Add((Image)control);
                //System.Diagnostics.Debug.WriteLine(c);
            }

            // find the first visible image where the next image is not visible
            for (int i = 0; i < images.Count; i++)
            {
                // make sure next image is in bounds
                if (i + 1 < images.Count) // there is a valid image proceeding this one
                {
                    if (!images[i + 1].Visible) // the proceeding image is hidden
                    {
                        if (adding)
                        {
                            images[i + 1].Visible = true; // make them visible
                            Number_of_Players++; // increment the number of players
                        }
                        else // removing
                        {
                            if (i != 0) // only hide the current image if the current image is NOT the first image
                            {
                                images[i].Visible = false; // hide the current image
                                Number_of_Players--; // decrement the number of players
                            }
                                
                        }
                            
                        break;
                    }
                }
                else if (!adding) // if removing and the last image is visible, hide the last image (only reachable when all of the possible players are displayed)
                {
                    images[i].Visible = false; // hide the current image
                    Number_of_Players--; // decrement the number of players
                }
            }
        }
    }
}

//private void Make_Next_Player_Image_Visible()
//{
//    // make a list of only the image controls stored in the update pannel bc there are LiteralControls also stored in update pannels
//    List<Image> images = new List<Image>();
//    foreach (Control control in upPlayerShowcase.ContentTemplateContainer.Controls)
//    {
//        if (control is System.Web.UI.WebControls.Image)
//            images.Add((Image)control);
//        //System.Diagnostics.Debug.WriteLine(c);
//    }

//    // find the first visible image where the next image is not visible
//    for (int i = 0; i < images.Count; i++)
//    {
//        // make sure next image is in bounds
//        if (i + 1 < images.Count) // there is a valid control proceeding this one
//        {
//            if (!images[i + 1].Visible) // the proceeding control is hidden
//            {
//                images[i + 1].Visible = true; // make them visible
//                break;
//            }
//        }
//    }
//}
//private void Make_Last_Made_Visible_Player_Hidden()
//{
//    // make a list of only the image controls stored in the update pannel bc there are LiteralControls also stored in update pannels
//    List<Image> images = new List<Image>();
//    foreach (Control c in upPlayerShowcase.ContentTemplateContainer.Controls)
//    {
//        if (c is System.Web.UI.WebControls.Image)
//            images.Add((Image)c);
//        //System.Diagnostics.Debug.WriteLine(c);
//    }

//    // find the first visible image where the next image is not visible
//    for (int i = 0; i < images.Count; i++)
//    {
//        // make sure next image is in bounds
//        if (i + 1 < images.Count) // there is a valid control proceeding this one
//        {
//            if (!images[i + 1].Visible) // the proceeding control is hidden visible
//            {
//                images[i].Visible = false; // hide the current image
//                break;
//            }
//        }
//    }
//}