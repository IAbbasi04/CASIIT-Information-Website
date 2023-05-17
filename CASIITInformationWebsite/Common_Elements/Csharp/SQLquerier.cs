using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Services;
using System.Security.Cryptography;
using System.Web;
using System.Web.Management;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
    public class SQLQuerier
    {
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

        /// <summary>
        /// Separates rows of data from delimited strings
        /// </summary>
        /// <param name="values">string to be parsed</param>
        /// <param name="defaultValue">value if section of the string is null</param>
        /// <returns></returns>
        public static int[] ToArray(string values, int defaultValue)
        {
            string[] valuesAsArray = values.Split(',');
            int numValues = valuesAsArray.Length;
            int[] output = new int[numValues];
            for (int i = 0; i < numValues; i++)
            {
                if (int.TryParse(valuesAsArray[i], out _))
                {
                    output[i] = int.Parse(valuesAsArray[i]);
                } else
                {
                    output[i] = defaultValue;
                }
            }
            return output;
        }

        /// <summary>
        /// Separates rows of data from delimited strings
        /// </summary>
        /// <param name="values">string to be parsed</param>
        /// <param name="defaultValue">value if section of the string is null</param>
        /// <returns></returns>
        public static double[] ToArray(string values, double defaultValue)
        {
            string[] valuesAsArray = values.Split(',');
            int numValues = valuesAsArray.Length;
            double[] output = new double[numValues];
            for (int i = 0; i < numValues; i++)
            {
                if (double.TryParse(valuesAsArray[i], out _))
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

        /// <summary>
        /// Inserts a Class object into the database
        /// </summary>
        /// <param name="course">class to be inserted</param>
        public static void InsertClass(Class course)
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
                    "(" + course.id + ", " +
                    course.course_weight + ", " +
                    course.description + ", " +
                    course.dual_enrolled + ", " +
                    course.hs_credit + ", " +
                    course.college_credit + ", " +
                    course.prerequisite.ToString() + ")";

                using (MySqlCommand command = new MySqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Returns a specific class based on its class id
        /// </summary>
        /// <param name="class_id"></param>
        /// <returns></returns>
        public static Class SelectClass(int class_id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                string query = "SELECT * FROM block7_table WHERE id = " + class_id;
                connection.Open();
                using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                {
                    return new Class(
                        reader.GetInt32(0),                                 //id
                        reader.GetString(1),                                //name
                        reader.GetDouble(2),                                //weight
                        reader.GetString(3),                                //description
                        reader.GetInt16(4),                                 //dual_enrolled
                        reader.GetDouble(5),                                //hs_credit
                        reader.GetDouble(6),                                //college credit
                        Prerequisite.readFromJSON(reader.GetString(7)));    //prerequisite
                }
            }
        }

        /// <summary> Finds all classes a user can take, and returns them as an array of classes. </summary> 
        /// <param name="user">user to correlate classes with</param>
        public static  Class[] SelectAvailableClasses(UserInfo user)
        {
            int numRows = 0;
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                string query = "SELECT COUNT(id) " +
                    "FROM courses " +
                    "WHERE class_id IN(" +
                        "SELECT class_id" +
                        "FROM track_courses" +
                        "WHERE user_id = " + user.UserId + 
                        " AND track_id = " + user.currentSelectedTrack + 
                        " )";
                connection.Open();
                using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                {
                    numRows = reader.GetInt32(0);
                }
            }

            Class[] classes = new Class[numRows];
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                string query =
                    "SELECT * " +
                    "FROM courses " +
                    "WHERE class_id IN(" +
                        "SELECT class_id" +
                        "FROM track_courses" +
                        "WHERE user_id = " + user.UserId +
                        " AND track_id = " + user.currentSelectedTrack +
                        " )";
                connection.Open();
                using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        classes[index] = new Class(
                           reader.GetInt32("id"),                                              //id
                           reader.GetString("course_name"),                                    //name
                           reader.GetDouble("course_weight"),                                  //weight
                           reader.GetString("description"),                                    //description
                           reader.GetInt16("dual_enrolled"),                                   //dual_enrolled
                           reader.GetDouble("hs_credit"),                                      //hs_credit
                           reader.GetDouble("college_credit"),                                 //college credit
                           Prerequisite.readFromJSON(reader.GetString("prerequisites")));      //prerequisite
                        index++;
                    }
                }
            }
            return classes;
        }

        /// <summary>
        /// Adds a prerequisite object to a pre-existing class
        /// </summary>
        /// <param name="course">course id to add prereq to</param>
        /// <param name="prereq"></param>
        public static void AddPrereqsToCourse(int course_id, Prerequisite prereq)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                String insert = "" +
                    "UPDATE courses" +
                    "SET prerequisites = " + Prerequisite.writeJSON(prereq) + 
                    "WHERE course_id = " + course_id;

                using (MySqlCommand command = new MySqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Adds a prerequisite object to a pre-existing class
        /// </summary>
        /// <param name="course">course to add prereq to</param>
        /// <param name="prereq"></param>
        public static void AddPrereqsToCourse(Class course, Prerequisite prereq)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                String insert = "" +
                    "UPDATE courses" +
                    "SET prerequisites = " + Prerequisite.writeJSON(prereq) +
                    "WHERE course_id = " + course.id;

                using (MySqlCommand command = new MySqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Creates a student Object using a student ID using row data from students table
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns></returns>
        public static Student SelectStudent(int studentID)
        {
            string[] result = Parse("id, name");
            int studentIndex = result[0].IndexOf("" + studentID);

            Student student = new Student(
                result[0],                  // name
                int.Parse(result[1]),       // student ID
                int.Parse(result[2]),       // grade level 
                double.Parse(result[3]),    // gpa
                int.Parse(result[4])        // counselor ID
            );

            return student;
        }

        /// <summary>
        /// Takes the email and password given and return the id of the user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int getUID(string email, string password)
        {
            int uid = -1;
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                string query = "" +
                    "SELECT id" +
                    "FROM users" +
                    "WHERE email = " + email +
                    "AND password = " + password;
                connection.Open();
                using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                {
                    try
                    {
                        uid = reader.GetInt32("id");
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
            return uid;
        }

        /// <summary>
        /// Creates a new track to store classes in
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="trackname"></param>
        public static void CreateTrack( int uid, string trackname)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                String insert = "" +
                    "INSERT INTO tracks" +
                    "(user_id, track_name) VALUES " +
                    "" + uid + ", " + 
                    trackname;

                using (MySqlCommand command = new MySqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Adds a class to a student's history
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="track_id"></param>
        /// <param name="course_id"></param>
        public static void InsertClassIntoTrack( int uid, int track_id, int course_id )
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                String insert = "" +
                    "INSERT INTO track_courses" +
                    "(user_id, track_id, course_id) VALUES " +
                    uid + ", " +
                    track_id + ", " +
                    course_id;

                using (MySqlCommand command = new MySqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}