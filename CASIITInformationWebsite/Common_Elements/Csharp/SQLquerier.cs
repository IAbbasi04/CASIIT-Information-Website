using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
    public class SQLQuerier
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

        public static int[] ToArray(string values, int defaultValue)
        {
            string[] valuesAsArray = values.Split(',');
            int numValues = valuesAsArray.Length;
            int[] output = new int[numValues];
            for (int i = 0; i < numValues; i++)
            {
                int temp = 0;
                if (int.TryParse(valuesAsArray[i], out temp))
                {
                    output[i] = int.Parse(valuesAsArray[i]);
                } else
                {
                    output[i] = defaultValue;
                }
            }
            return output;
        }

        public static double[] ToArray(string values, double defaultValue)
        {
            string[] valuesAsArray = values.Split(',');
            int numValues = valuesAsArray.Length;
            double[] output = new double[numValues];
            for (int i = 0; i < numValues; i++)
            {
                double temp = 0;
                if (double.TryParse(valuesAsArray[i], out temp))
                {
                    output[i] = double.Parse(valuesAsArray[i]);
                }
                else
                {
                    output[i] = defaultValue;
                }
            }

            return output;
        }


        public void Insert(Class course)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                String insert = "" +
                    "INSERT INTO courses (id, " +
                    "course_name, " +
                    "course_weight, " +
                    "description, " +
                    "dual_enrolled, " +
                    "hs_credit, " +
                    "college_credit, " +
                    "prerequisites) VALUES" +
                    "(" + course.name + ", " +
                    course.weight + ", " +
                    course.description + ", " +
                    course.dual_enrolled + ", " +
                    course.hs_credit + ", " +
                    course.college_credit + ", " +
                    course.prerequisites.toString() + ")";

                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    command.Execute(insert);
                }
            }
        }
    }
}