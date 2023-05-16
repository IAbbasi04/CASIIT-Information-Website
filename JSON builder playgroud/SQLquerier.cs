using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{

    public static class SQLquerier
    {
        static String name;
        static string myConnectionString = "server=p3nlmysql165plsk.secureserver.net;uid=CSIsAwesome;pwd=Casiit2117Class;database=battlefield_casiit";

        public static void testMethod()
        {
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
                            rc += reader.GetInt32(0) + reader.GetString(1);
                        }
                        command.Parameters.AddWithValue("name", rc);
                    }
                }
            }

            // Now we can do something here with the data in rc.  Or whatever.
        }

        static void testMethod2()
        {
            string rc = "";

            using (MySqlConnection conn = new MySqlConnection(myConnectionString))
            {
                conn.ConnectionString = myConnectionString;
                conn.Open();

                String sql = "SELECT id, name FROM block7_table WHERE id = 34543";

                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                    }
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            rc += reader.GetInt32(0) + reader.GetString(1);
                        }
                    }
                }
            }

            // Now we can do something here with the data in rc.  Or whatever.

        }

    }
}