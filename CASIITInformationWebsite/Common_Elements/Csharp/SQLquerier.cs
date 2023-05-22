using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Remoting.Services;
using System.Security.Cryptography;
using System.Web;
using System.Web.DynamicData;
using System.Web.Management;
using System.Web.UI;
using K4os.Compression.LZ4.Streams.Abstractions;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Org.BouncyCastle.Utilities.Collections;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
    public class SQLQuerier
    {
        public const string CONNECTION_STRING = "server=p3nlmysql165plsk.secureserver.net;uid=CSIsAwesome;pwd=Casiit2117Class;database=battlefield_casiit";

        private const int ID_COL = 0;
        private const int FIRST_NAME_COL = 1;
        private const int LAST_NAME_COL = 2;
        private const int EMAIL_COL = 3;
        private const int PASSWORD_COL = 4;
        private const int VERIFIED_COL = 5;
        private const int ROLE_ID_COL = 6;

        public static string[] GetRow(int row)
        {
            string query = "SELECT * FROM users LIMIT " + row + ",1";
            List<string> output = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (MySqlDataReader reader = new MySqlCommand(query, conn).ExecuteReader())
                {
                    if (reader.Read())
                    {
                        for(int i = 0; i < reader.FieldCount; i++)
                        {
                            if (i != 5)
                            {
                                output.Add(reader.GetString(i));
                            }
                        }
                    }
                }
            }

            return output.ToArray();
        }

        public static string[] GetColNames(string table)
        {
            string query = "SELECT * FROM users";
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader rdr = cmd.ExecuteReader();
            DataTable schema = rdr.GetSchemaTable();
            conn.Close();

            List<string> cols = new List<string>();

            foreach (DataRow rdrColumn in schema.Rows)
            {
                String columnName = rdrColumn[schema.Columns["ColumnName"]].ToString();
                String dataType = rdrColumn[schema.Columns["DataType"]].ToString();
                cols.Add(columnName);
            }

            return cols.ToArray();
        }

        public static string[] GetAllStudents()
        {
            string query = "SELECT first_name FROM users";
            List<string> names = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++) names.Add(reader.GetString(i));
                    }
                }
            }

            return names.ToArray();
        }

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

        public static string[] Parse(string item, string table)
        {
            string rc = "";
            string query = "SELECT " + item + " FROM " + table;
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
            if (item == "*")
            {
                indeces = 6;
            }
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
            try
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
                        Prerequisite.writeJSON(course.prerequisite) + ")";

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        /// Returns a specific class based on its class id
        /// </summary>
        /// <param name="class_id"></param>
        /// <returns></returns>
        public static Class SelectClass(int class_id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query = "SELECT * FROM courses WHERE id = " + class_id;
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read()) return new Class(
                            reader.GetInt32("id"),
                            reader.IsDBNull(reader.GetOrdinal("course_name")) ? string.Empty : reader.GetString("course_name"),
                            reader.IsDBNull(reader.GetOrdinal("course_weight")) ? 0.0 : reader.GetDouble("course_weight"),
                            reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString("description"),
                            reader.IsDBNull(reader.GetOrdinal("concentration")) ? string.Empty : reader.GetString("concentration"),
                            reader.IsDBNull(reader.GetOrdinal("dual_enrolled")) ? (short)0 : reader.GetInt16("dual_enrolled"),
                            reader.IsDBNull(reader.GetOrdinal("hs_credit")) ? 0.0 : reader.GetDouble("hs_credit"),
                            reader.IsDBNull(reader.GetOrdinal("college_credit")) ? 0.0 : reader.GetDouble("college_credit"),
                            Prerequisite.readFromJSON(reader.IsDBNull(reader.GetOrdinal("prerequisites")) ? string.Empty : reader.GetString("prerequisites")));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
            return new Class();
        }

        /// <summary> Finds all classes a user has taken, and returns them as an array of classes. </summary> 
        /// <param name="user">user to correlate classes with</param>
        public static  Class[] PreviousClasses(UserInfo user)
        {
            int numRows = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query = "SELECT COUNT(id) " +
                        "FROM courses " +
                        "WHERE id IN(" +
                            "SELECT course_id " +
                            "FROM track_courses " +
                            "WHERE user_id = " + user.UserId +
                            " AND track_id = " + user.currentSelectedTrack +
                            " )";
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read()) numRows = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
                return new Class[0];
            }

            Class[] classes = new Class[numRows];
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query =
                        "SELECT * " +
                        "FROM courses " +
                        "WHERE id IN(" +
                            "SELECT course_id " +
                            "FROM track_courses " +
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
                                reader.GetInt32("id"),
                                reader.IsDBNull(reader.GetOrdinal("course_name")) ? string.Empty : reader.GetString("course_name"),
                                reader.IsDBNull(reader.GetOrdinal("course_weight")) ? 0.0 : reader.GetDouble("course_weight"),
                                reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString("description"),
                                reader.IsDBNull(reader.GetOrdinal("concentration")) ? string.Empty : reader.GetString("concentration"),
                                reader.IsDBNull(reader.GetOrdinal("dual_enrolled")) ? (short)0 : reader.GetInt16("dual_enrolled"),
                                reader.IsDBNull(reader.GetOrdinal("hs_credit")) ? 0.0 : reader.GetDouble("hs_credit"),
                                reader.IsDBNull(reader.GetOrdinal("college_credit")) ? 0.0 : reader.GetDouble("college_credit"),
                                Prerequisite.readFromJSON(reader.IsDBNull(reader.GetOrdinal("prerequisites")) ? string.Empty : reader.GetString("prerequisites")));     //prerequisite
                            index++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
            return classes;
        }

        public static int[] PreviousClassIDs(UserInfo user)
        {
            int numRows = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query = "SELECT COUNT(id) " +
                        "FROM courses " +
                        "WHERE id IN(" +
                            "SELECT course_id " +
                            "FROM track_courses " +
                            "WHERE user_id = " + user.UserId +
                            " AND track_id = " + user.currentSelectedTrack +
                            " )";
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read()) numRows = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
                return new int[0];
            }

            int[] classes = new int[numRows];
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query =
                        "SELECT id " +
                        "FROM courses " +
                        "WHERE id IN(" +
                            "SELECT course_id " +
                            "FROM track_courses " +
                            "WHERE user_id = " + user.UserId +
                            " AND track_id = " + user.currentSelectedTrack +
                            " )";
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        int index = 0;
                        while (reader.Read())
                        {
                            classes[index] = reader.GetInt32("id");
                            index++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
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
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "UPDATE courses " +
                        "SET prerequisites = '" + Prerequisite.writeJSON(prereq) + "' " +
                        "WHERE id = " + course_id;

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {

                        try
                        {
                            rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine("Prerequisite for course #" + course_id + " Added Succesfully");
                        }
                        catch
                        {
                            Console.WriteLine("Update Failed on course #" + course_id);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
                return;
            }

            Console.WriteLine("Rows Affected: " + rowsAffected);
            Console.WriteLine("JSON: " + Prerequisite.writeJSON(prereq) + "\n");
        }

        /// <summary>
        /// Adds a prerequisite object to a pre-existing class
        /// </summary>
        /// <param name="course">course to add prereq to</param>
        /// <param name="prereq"></param>
        public static void AddPrereqsToCourse(Class course, Prerequisite prereq)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "UPDATE courses " +
                        "SET prerequisites = '" + Prerequisite.writeJSON(prereq) + "' " +
                        "WHERE id = " + course.id;

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        /// Inserts an instance of a Student into the database
        /// </summary>
        /// <param name="user"></param>
        public static void InsertStudent(Student user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "INSERT INTO users" +
                        "(first_name, last_name, email, password, role_id) VALUES ('" +
                        user.FirstName + "' , '" +
                        user.LastName + "' , '" +
                        user.email + "' , '" +
                        user.password + "', 1 )";
                    Console.WriteLine(insert);
                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                int user_id;
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String query = "SELECT id FROM users WHERE " +
                        "email LIKE '" + user.email + "' AND " +
                        "password LIKE '" + user.password + "'";
                    Console.WriteLine(query);
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user_id = reader.GetInt32("id");
                        }
                        else user_id = -1;
                    }
                }
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "INSERT INTO students" +
                        "(id, gpa, year) VALUES (" +
                        user_id + " , " +
                        user.GPA + " , " +
                        user.Year + ")";
                    Console.WriteLine(insert);
                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        /// Creates a student Object using a student ID using row data from students table
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns></returns>
        public static Student SelectStudent(int studentID)
        {
            Student output = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query = "" +
                        "SELECT * " +
                        "FROM users " +
                        "JOIN students ON users.id = students.user_id " +
                        "WHERE id = " + studentID;
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            output = new Student(
                                reader.IsDBNull(reader.GetOrdinal("first_name")) ? string.Empty : reader.GetString("first_name"),                 //first name
                                reader.IsDBNull(reader.GetOrdinal("last_name")) ? string.Empty : reader.GetString("last_name"),                  //last name
                                reader.GetInt32("id"),                                                                                  //userId
                                reader.IsDBNull(reader.GetOrdinal("year")) ? 0 : DateTime.Now.Year - reader.GetInt32("year"),    //year
                                reader.IsDBNull(reader.GetOrdinal("gpa")) ? 0.0 : reader.GetDouble("gpa"),                        //gpa
                                reader.IsDBNull(reader.GetOrdinal("counselor_id")) ? 0 : reader.GetInt32("counselor_id"),
                                reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString("email"),                 //first name
                                reader.IsDBNull(reader.GetOrdinal("password")) ? string.Empty : reader.GetString("password"));               //counselorID
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
            return output;
        }

        /// <summary>
        /// Deletes a student's record from the database
        /// </summary>
        /// <param name="uid"></param>
        public static void DeleteStudent( int uid) 
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string delete = "" +
                        "DELETE " +
                        "FROM users " +
                        "JOIN students ON users.id = students.user_id " +
                        "WHERE id = " + uid;
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(delete, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        /// Inserts and instance of a Counselor into the database
        /// </summary>
        /// <param name="user"></param>
        public static void InsertCounselor(Counselor user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "INSERT INTO users" +
                        "(first_name, last_name, email, password, year, gpa) VALUES ('" +
                        user.FirstName + "' , '" +
                        user.LastName + "' , '" +
                        user.email + "' , '" +
                        user.password + "' , 2)";

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                int user_id;
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String query = "SELECT id FROM users WHERE " +
                        "email LIKE '" + user.email + "' AND " +
                        "password LIKE '" + user.password + "'";

                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user_id = reader.GetInt32("id");
                        }
                        else user_id = -1;
                    }
                }
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "INSERT INTO counselors" +
                        "(id, name_range_start, name_range_end) VALUES (" +
                        user_id + " , " +
                        user.NameRangeStart + " , " +
                        user.NameRangeEnd + ")";

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        ///Creates a Counselor Object from and instance in the database
        /// </summary>
        /// <param name="counselorID"></param>
        /// <returns></returns>
        public static Counselor SelectCounselor(int counselorID)
        {
            Counselor output = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query = "" +
                        "SELECT * " +
                        "FROM users " +
                        "JOIN counselors ON users.id = counselors.user_id " +
                        "WHERE id = " + counselorID;
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read()) output = new Counselor(
                            reader.IsDBNull(reader.GetOrdinal("first_name")) ? string.Empty : reader.GetString("first_name"),                 //first name
                            reader.IsDBNull(reader.GetOrdinal("last_name")) ? string.Empty : reader.GetString("last_name"),
                            reader.GetInt32("id"),                          //userId
                            reader.IsDBNull(reader.GetOrdinal("name_range_start")) ? "A" : reader.GetString("name_range_start"),           //name range start
                            reader.IsDBNull(reader.GetOrdinal("name_range_end")) ? "A" : reader.GetString("name_range_emd"),
                            reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString("email"),                 //first name
                            reader.IsDBNull(reader.GetOrdinal("password")) ? string.Empty : reader.GetString("password"));
                        else return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
            return output;
        }

        /// <summary>
        /// Deletes a counselor's record from the database
        /// </summary>
        /// <param name="uid"></param>
        public static void DeleteCounselor(int uid)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string delete = "" +
                        "DELETE " +
                        "FROM users " +
                        "JOIN counselors ON users.id = counselors.user_id " +
                        "WHERE id = " + uid;
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(delete, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        /// Inserts an instance of an Admin into the database
        /// </summary>
        /// <param name="user"></param>
        public static void InsertAdmin(Admin user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "INSERT INTO users" +
                        "(first_name, last_name, email, password, year, gpa) VALUES ('" +
                        user.FirstName + "' , '" +
                        user.LastName + "' , '" +
                        user.email + "' , '" +
                        user.password + "' , 3)";

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                int user_id;
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String query = "SELECT id FROM users WHERE " +
                        "email LIKE '" + user.email + "' AND " +
                        "password LIKE '" + user.password + "'";

                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user_id = reader.GetInt32("id");
                        }
                        else user_id = -1;
                    }
                }
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "INSERT INTO admins" +
                        "(id) VALUES (" +
                        user_id + ")";

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        ///Creates an Admin Object from and instance in the database
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns></returns>
        public static Admin SelectAdmin( int adminID)
        {
            Admin output = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query = "" +
                        "SELECT * " +
                        "FROM users " +
                        "JOIN admins ON users.id = admins.user_id " +
                        "WHERE id = " + adminID;
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        if (reader.Read()) output = new Admin(
                            reader.IsDBNull(reader.GetOrdinal("first_name")) ? string.Empty : reader.GetString("first_name"),                 //first name
                            reader.IsDBNull(reader.GetOrdinal("last_name")) ? string.Empty : reader.GetString("last_name"),
                            reader.GetInt32("id"),
                            reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString("email"),                 //first name
                            reader.IsDBNull(reader.GetOrdinal("password")) ? string.Empty : reader.GetString("password"));               //counselorID
                        else return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
            return output;
        }

        /// <summary>
        /// Deletes an Admin's record from the database
        /// </summary>
        /// <param name="uid"></param>
        public static void DeleteAdmin(int uid)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string delete = "" +
                        "DELETE " +
                        "FROM users " +
                        "JOIN admins ON users.id = admins.user_id " +
                        "WHERE id = " + uid;
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(delete, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        /// Takes the email and password given and return the id of the user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int GetUID(string email, string password)
        {
            int uid = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query = "" +
                        "SELECT id " +
                        "FROM users " +
                        "WHERE email LIKE '" + email +
                        "' AND password LIKE '" + password + "'";
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        reader.Read();
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
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
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
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "INSERT INTO tracks " +
                        "(user_id, track_name) VALUES " +
                        "" + uid + ", " +
                        trackname;

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
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
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "INSERT INTO track_courses " +
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
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        public static void DeleteClassFromTrack( int uid, int track_id, int course_id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String insert = "" +
                        "DELETE FROM track_courses " +
                        "WHERE user_id = " + uid + " AND " +
                        "track_id = " + track_id + " AND " +
                        "course_id = " + course_id;

                    using (MySqlCommand command = new MySqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        /// Creates a temporary table filled with course IDs
        /// </summary>
        /// <param name="table_name"></param>
        public static void CreateTempCourseTable( string table_name )
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String sql = "" +
                        "CREATE TABLE " + table_name + "( " +
                        "course_id int(5) primary key" +
                        " )";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        /// <summary>
        /// Selects all the available classes a student will be able to take
        /// </summary>
        /// <returns></returns>
        public static List<Class> AllAvailableClasses(Student user)
        {
            int[] previouslyTakenCourses = PreviousClassIDs(user);
            List<Class> allClasses = AllClasses();
            List<Class> availableClasses = new List<Class>();
            // goes through every class, if the user meets the requirements of a class then it is added to the list of available classes
            foreach( Class course in allClasses)
            {
                if (course.MeetsRequisites(user) && !previouslyTakenCourses.Contains(course.id)) availableClasses.Add(course);
            }
            return availableClasses;
        }

        public static List<Class> AllAvailableClasses(UserInfo user)
        {
            int[] previouslyTakenCourses = PreviousClassIDs(user);
            List<Class> allClasses = AllClasses();
            List<Class> availableClasses = new List<Class>();
            // goes through every class, if the user meets the requirements of a class then it is added to the list of available classes
            foreach (Class course in allClasses)
            {
                if (course.MeetsRequisites(user) && !previouslyTakenCourses.Contains(course.id)) availableClasses.Add(course);
            }
            return availableClasses;
        }

        /// <summary>
        /// Returns a List of all the classes a student can take with their current status, filtered by a certain grade level
        /// </summary>
        /// <param name="user"></param>
        /// <param name="year">the year to filter by</param>
        /// <returns></returns>
        public static List<Class> AllAvailableYearClasses(Student user, int year)
        {
            List<Class> courses = AllAvailableClasses(user);
            courses = FilterMinYear(courses, year);
            return courses = FilterMaxYear(courses, year);
        }

        /// <summary>
        /// Returns a List of all the classes a student can take in the coming year
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Class> AllAvailableNextYearClasses(Student user)
        {
            List<Class> courses = AllAvailableClasses(user);
            courses = FilterMinYear(courses, user.Year + 1);
            return courses = FilterMaxYear(courses, user.Year + 1);
        }

        /// <summary>
        /// Returns all classes in course table as Class objects
        /// </summary>
        /// <returns></returns>
        public static List<Class> AllClasses()
        {
            List<Class> courses = new List<Class>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query =
                        "SELECT * " +
                        "FROM courses ";
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Class(
                                reader.GetInt32("id"),
                                reader.IsDBNull(reader.GetOrdinal("course_name")) ? string.Empty : reader.GetString("course_name"),
                                reader.IsDBNull(reader.GetOrdinal("course_weight")) ? 0.0 : reader.GetDouble("course_weight"),
                                reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString("description"),
                                reader.IsDBNull(reader.GetOrdinal("concentration")) ? string.Empty : reader.GetString("concentration"),
                                reader.IsDBNull(reader.GetOrdinal("dual_enrolled")) ? (short)0 : reader.GetInt16("dual_enrolled"),
                                reader.IsDBNull(reader.GetOrdinal("hs_credit")) ? 0.0 : reader.GetDouble("hs_credit"),
                                reader.IsDBNull(reader.GetOrdinal("college_credit")) ? 0.0 : reader.GetDouble("college_credit"),
                                Prerequisite.readFromJSON(reader.IsDBNull(reader.GetOrdinal("prerequisites")) ? string.Empty : reader.GetString("prerequisites"))
                            ));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
            return courses;
        }

        /// <summary>
        /// Returns all classes in course table as Class objects
        /// </summary>
        /// <returns></returns>
        public static List<int> AllClassIDs()
        {
            List<int> courses = new List<int>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    string query =
                        "SELECT * " +
                        "FROM courses ";
                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(
                               reader.GetInt32("id"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
            return courses;
        }


        /// <summary>
        /// Drops a table from the database
        /// </summary>
        /// <param name="table_name"></param>
        public static void DropTable(string table_name)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    String sql = "DROP TABLE " + table_name;

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }
        }

        private static List<Class> FilterMinYear( List<Class> courses , int min_year = 0)
        {
            List<Class> filteredResult = new List<Class>();
            foreach( Class course in courses)
            {
                if(course.prerequisite.min_year >= min_year) filteredResult.Add(course);
            }
            return filteredResult;
        }

        private static List<Class> FilterMinGPA(List<Class> courses, double min_GPA = 0)
        {
            List<Class> filteredResult = new List<Class>();
            foreach (Class course in courses)
            {
                if (course.prerequisite.min_GPA >= min_GPA) filteredResult.Add(course);
            }
            return filteredResult;
        }

        private static List<Class> FilterMaxGPA(List<Class> courses, double max_GPA = 5)
        {
            List<Class> filteredResult = new List<Class>();
            foreach (Class course in courses)
            {
                if (course.prerequisite.min_GPA <= max_GPA) filteredResult.Add(course);
            }
            return filteredResult;
        }

        private static List<Class> FilterMaxYear(List<Class> courses, int maxYear = 4)
        {
            List<Class> filteredResult = new List<Class>();
            foreach (Class course in courses)
            {
                if (course.prerequisite.min_year <= maxYear) filteredResult.Add(course);
            }
            return filteredResult;
        }

        /// <summary>
        /// Returns a list of classes from the database that conform to the set of parameters. Parameters are optional, and can be entered as null if necessary.
        /// Enter parameters in this format: SQLQuerier.FilterSelect(courseWeightMin: *Value* , gpaMin *Value* , etc. );
        /// </summary>
        /// <param name="courseWeightMin"></param>
        /// <param name="courseWeightMax"></param>
        /// <param name="isDualEnrolled"></param>
        /// <param name="hsCrediMin"></param>
        /// <param name="hsCreditMax"></param>
        /// <param name="collegeCreditMin"></param>
        /// <param name="collegeCreditMax"></param>
        /// <param name="gpaMin"></param>
        /// <param name="gpaMax"></param>
        /// <param name="yearMin"></param>
        /// <param name="yearMax"></param>
        /// <returns></returns>
        public static List<Class> FilterSelect(
                double courseWeightMin = 0,
                double courseWeightMax = 5.0,
                bool isDualEnrolled = false,
                double hsCrediMin = 0,
                double hsCreditMax = 10,
                double collegeCreditMin = 0,
                double collegeCreditMax = 10,
                double gpaMin = 0.0,
                double gpaMax = 5,
                int yearMin = 0,
                int yearMax = 4,
                string concentration = "")
        {
            List<Class> courses = new List<Class>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
                {
                    //Query without dual enrolled filter
                    string query =
                        "SELECT * " +
                        "FROM courses " +
                        "WHERE IFNULL(course_weight, 0) >= " + courseWeightMin + " AND " +
                        "IFNULL(course_weight, 0) <= " + courseWeightMax + " AND " +
                        "IFNULL(hs_credit, 0) >= " + hsCrediMin + " AND " +
                        "IFNULL(hs_credit, 0) <= " + hsCreditMax + " AND " +
                        "IFNULL(college_credit, 0) >= " + collegeCreditMin + " AND " +
                        "IFNULL(college_credit, 0) <= " + collegeCreditMax;
                    //With dual enrolled filter
                    if (isDualEnrolled)
                    {
                        query += " AND dual_enrolled = 1";
                    }
                    if (concentration.Length != 0)
                    {
                        query += " AND concentration LIKE '" + concentration + "'";
                    }

                    connection.Open();
                    using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Class(reader.GetInt32("id"),
                            reader.IsDBNull(reader.GetOrdinal("course_name")) ? string.Empty : reader.GetString("course_name"),
                            reader.IsDBNull(reader.GetOrdinal("course_weight")) ? 0.0 : reader.GetDouble("course_weight"),
                            reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString("description"),
                            reader.IsDBNull(reader.GetOrdinal("concentration")) ? string.Empty : reader.GetString("concentration"),
                            reader.IsDBNull(reader.GetOrdinal("dual_enrolled")) ? (short)0 : reader.GetInt16("dual_enrolled"),
                            reader.IsDBNull(reader.GetOrdinal("hs_credit")) ? 0.0 : reader.GetDouble("hs_credit"),
                            reader.IsDBNull(reader.GetOrdinal("college_credit")) ? 0.0 : reader.GetDouble("college_credit"),
                            Prerequisite.readFromJSON(reader.IsDBNull(reader.GetOrdinal("prerequisites")) ? string.Empty : reader.GetString("prerequisites"))
                            ));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL error: " + e.Message);
            }

            courses = FilterMinGPA(courses, gpaMin);
            courses = FilterMaxGPA(courses, gpaMax);
            courses = FilterMinYear(courses, yearMin);
            courses = FilterMaxYear(courses, yearMax);

            return courses;
        }

    }



}