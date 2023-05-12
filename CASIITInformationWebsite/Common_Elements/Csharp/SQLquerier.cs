using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{

    public static class SQLQuerier
    {
        public static String name;
        public const string CONNECTION_STRING = "server=p3nlmysql165plsk.secureserver.net;uid=CSIsAwesome;pwd=Casiit2117Class;database=battlefield_casiit";

        public static string[] Parse(string item)
        {
            string rc = "";
            string query = "SELECT " + item + " FROM block7_table";
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++) rc += reader.GetString(i) + "|";
                        rc = rc.Substring(0, rc.Length - 1);
                        rc += ",";
                    }


                    rc = rc.Substring(0, rc.Length - 1);
                }
            }

            int indeces = item.Split(',').Length;
            string[] elements = new string[indeces];
            string[] outputs = rc.Split(',');
            
            for (int j = 0; j < outputs.Length; j++)
            {
                string[] splitOutputs = outputs[j].Split('|');
                for (int i = 0; i < indeces; i++)
                {
                    elements[i] += splitOutputs[i] + ", ";
                }
            }

            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i].Substring(0, elements[i].Length - 2);
            }
            return elements;
        }

        public static void testMethod()
        {
            string rc = "";

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                conn.ConnectionString = CONNECTION_STRING;
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

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                conn.ConnectionString = CONNECTION_STRING;
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