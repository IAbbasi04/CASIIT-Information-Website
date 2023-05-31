using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite.Pages.Finals
{
    public partial class RhodesFinalProjectWaitThisWasSupposedToBeHardToFindWithoutTheLinkOrSomethingSoHereIsSomeTextSoMrLevinHaveFunWithThis : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["seed"] == null)
            {
                Session["seed"] = new Random().Next();
            }

            //get rid of the stuff thats not supposed to be there
            Page.ClientScript.RegisterStartupScript(GetType(), "No",
            "document.querySelector('#ctl01 > div.container.body-content > div.footer').remove();\n" +
            "document.querySelector('#ctl01 > div.navbar.navbar-inverse.navbar-fixed-top').remove();\n" +
            "if(document.querySelector('#ctl01 > div.container.body-content > div')!=null){\n" +
            "    document.querySelector('#ctl01 > div.container.body-content > div').remove();\n" +
            "}\n" +
            "document.querySelector('head > link:nth-child(6)').remove();\n" +
            "document.querySelector('head > link:nth-child(7)').remove();\n" +
            "document.querySelector('#ctl01 > div.container.body-content > hr').remove();\n;"
            , true);
            //typing animation
            Page.ClientScript.RegisterStartupScript(GetType(), "Typing",
            "var text = 'Hello, Tyler!';\n" +
            "var ind = 0;\n" +
            "function type(){\n" +
            "document.querySelector('#MainContent_Label2').textContent = text.substring(0,ind);\n" +
            "if(ind < text.length){\n" +
            "ind++;\n" +
            "setTimeout(type,100);\n" +
            "}\n" +
            "}\n" +
            "document.querySelector('#MainContent_Label2').textContent = '';\n" +
            "setTimeout(type,1000);\n"
            , true);

            //fun fact randomization                                                                                                                                                                                                                                                                                                    //|      aproximate length      |
            string[] funFacts = new string[] { "-Has used both the oven and stove\nsucsessfully without supervision\nbefore becoming an adult","-Learned to drive in a stick\nshift car","-Has been programming since 5\nyears old","-Has built his own light switch","-Was the electronics subteam\nlead for Comet Robotics","-Won the Connect 4 competition\nin Mr. Levin's class"};
            int[] ns = new int[3];
            do
            {
                Random r = new Random((int)Session["seed"]);
                ns[0] = r.Next(funFacts.Length);
                ns[1] = r.Next(funFacts.Length);
                ns[2] = r.Next(funFacts.Length);
            } while (ns[0]==ns[1]||ns[1]==ns[2]||ns[2]==ns[0]);
            string text = $"{funFacts[ns[0]]}\n{funFacts[ns[1]]}\n{funFacts[ns[2]]}";
            Button3B.Text = text;
        }
    }
}