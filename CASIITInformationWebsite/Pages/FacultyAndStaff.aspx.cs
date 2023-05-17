using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CASIITInformationWebsite
{
    public partial class FacultyAndStaff : Page
    {
        void testMethod()
        {
            string myConnectionString = "server=p3nlmysql165plsk.secureserver.net;uid=CSIsAwesome;pwd=Casiit2117Class;database=battlefield_casiit";
            string rc = "";

            using (MySqlConnection conn = new MySqlConnection(myConnectionString))
            {
                conn.ConnectionString = myConnectionString;
                conn.Open();

                String sql = "SELECT id, name FROM block7_table";

                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rc += reader.GetInt32(0) + reader.GetString(1) + ";";
                        }
                        Console.WriteLine(rc);
                    }

                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}