using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Remoting.Services;
using System.Security.Cryptography;
using System.Web;
using System.Web.DynamicData;
using System.Web.Management;
using Microsoft.Ajax.Utilities;
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
                        reader.GetInt32("id"),                                          //id
                        reader.GetString("course_name"),                                //name
                        reader.GetDouble("course_weight"),                              //weight
                        reader.GetString("description"),                                //description
                        reader.GetString("concentration"),                              //concentration
                        reader.GetInt16("dual_enrolled"),                               //dual_enrolled
                        reader.GetDouble("hs_credit"),                                  //hs_credit
                        reader.GetDouble("college_credit"),                             //college credit
                        Prerequisite.readFromJSON(reader.GetString("prerequisites"))) ;               //prerequisite
                }
            }
        }

        /// <summary> Finds all classes a user has taken, and returns them as an array of classes. </summary> 
        /// <param name="user">user to correlate classes with</param>
        public static  Class[] PreviousClasses(UserInfo user)
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
                           reader.GetString("concentration"),
                           reader.GetInt16("dual_enrolled"),                                   //dual_enrolled
                           reader.GetDouble("hs_credit"),                                      //hs_credit
                           reader.GetDouble("college_credit"),                                 //college credit
                           Prerequisite.readFromJSON(reader.GetString("prerequisites"))) ;      //prerequisite
                        index++;
                    }
                }
            }
            return classes;
        }

        public static int[] PreviousClassIDs(UserInfo user)
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

            int[] classes = new int[numRows];
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                string query =
                    "SELECT id " +
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
                        classes[index] = reader.GetInt32("id");                             
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
            int rowsAffected = 0;
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                String insert = "" +
                    "UPDATE courses " +
                    "SET prerequisites = '" + Prerequisite.writeJSON(prereq) + "' "+ 
                    "WHERE id = " + course_id;

                using (MySqlCommand command = new MySqlCommand(insert, connection))
                {
                    
                    try{
                        rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Prerequisite for course #" + course_id + " Added Succesfully");
                    }
                    catch
                    {
                        Console.WriteLine("Update Failed on course #" + course_id);
                    }

                }
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

        /// <summary>
        /// Creates a student Object using a student ID using row data from students table
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns></returns>
        public static Student SelectStudentParse(int studentID)
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

        public static Student SelectStudent(int studentID)
        {
            Student output;
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
                    output = new Student(
                        reader.GetString("first_name"),                 //first name
                        reader.GetString("last_name"),                  //last name
                        reader.GetInt32("id"),                          //userId
                        DateTime.Now.Year - reader.GetInt32("year"),    //year
                        reader.GetDouble("gpa"),                        //gpa
                        reader.GetInt32("counselor_id"));               //counselorID
                }
            }
            return output;
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

        /// <summary>
        /// Creates a temporary table filled with course IDs
        /// </summary>
        /// <param name="table_name"></param>
        public static void CreateTempCourseTable( string table_name )
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                String sql = ""+
                    "CREATE TABLE " + table_name + "( " +
                    "course_id int(5) primary key" +
                    " )";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Selects all the available classes a student will be able to take
        /// </summary>
        /// <returns></returns>
        public static List<Class> AllAvailableClasses(UserInfo user)
        {
            List<Class> allClasses = AllClassIDs();
            List<Class> availableClasses = new List<Class>();
            // goes through every class, if the user meets the requirements of a class then it is added to the list of available classes
            foreach( Class course in allClasses)
            {
                if (course.MeetsRequisites(user)) availableClasses.Add(course);
            }
            return availableClasses;
        }

        /// <summary>
        /// Returns all classes in course table as Class objects
        /// </summary>
        /// <returns></returns>
        public static List<Class> AllClassIDs()
        {
            List<Class> courses = new List<Class>();
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
                           reader.GetInt32("id"),                                              //id
                           reader.GetString("course_name"),                                    //name
                           reader.GetDouble("course_weight"),                                  //weight
                           reader.GetString("description"),                                    //description
                           reader.GetString("concentration"),                                  //concentration
                           reader.GetInt16("dual_enrolled"),                                   //dual_enrolled
                           reader.GetDouble("hs_credit"),                                      //hs_credit
                           reader.GetDouble("college_credit"),                                 //college credit
                           Prerequisite.readFromJSON(reader.GetString("prerequisites")))      //prerequisite
                        );
                    }
                }
            }
            return courses;
        }

        /// <summary>
        /// Drops a table from the database
        /// </summary>
        /// <param name="table_name"></param>
        public static void DropTable(string table_name)
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

        private static List<Class> FilterMinYear( List<Class> courses , int min_year )
        {
            List<Class> filteredResult = new List<Class>();
            foreach( Class course in courses)
            {
                if(course.prerequisite.min_year >= min_year) filteredResult.Add(course);
            }
            return filteredResult;
        }

        private static List<Class> FilterMinGPA(List<Class> courses, double min_GPA)
        {
            List<Class> filteredResult = new List<Class>();
            foreach (Class course in courses)
            {
                if (course.prerequisite.min_GPA >= min_GPA) filteredResult.Add(course);
            }
            return filteredResult;
        }

        private static List<Class> FilterMaxGPA(List<Class> courses, double max_GPA)
        {
            List<Class> filteredResult = new List<Class>();
            foreach (Class course in courses)
            {
                if (course.prerequisite.min_GPA <= max_GPA) filteredResult.Add(course);
            }
            return filteredResult;
        }

        private static List<Class> FilterMaxYear(List<Class> courses, int maxYear)
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
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                //Query without dual enrolled filter
                string query =
                    "SELECT * " +
                    "FROM courses " +
                    "WHERE course weight >= " + courseWeightMin + " AND " +
                    "course weight <= " + courseWeightMax + " AND " +
                    "hs_credit >= " + hsCrediMin + " AND " +
                    "hs_credit <= " + hsCreditMax + " AND " +
                    "college_credit >= " + collegeCreditMin + " AND " +
                    "college_credit <= " + collegeCreditMax;
                 
                //With dual enrolled filter
                if (isDualEnrolled)
                {
                    query += " AND dual_enrolled = 1";
                }
                if( concentration.Length != 0)
                {
                    query += " AND concentration LIKE '" + concentration + "'"; 
                }

                connection.Open();
                using (MySqlDataReader reader = new MySqlCommand(query, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Class(
                           reader.GetInt32("id"),                                               //id
                           reader.GetString("course_name"),                                     //name
                           reader.GetDouble("course_weight"),                                   //weight
                           reader.GetString("description"),                                     //description
                           reader.GetString("concentration"),                                   //concentration
                           reader.GetInt16("dual_enrolled"),                                    //dual_enrolled
                           reader.GetDouble("hs_credit"),                                       //hs_credit
                           reader.GetDouble("college_credit"),                                  //college credit
                           Prerequisite.readFromJSON(reader.GetString("prerequisites")))        //prerequisite
                        );
                    }
                }
            }

            courses = FilterMinGPA( courses, gpaMin);
            courses = FilterMaxGPA(courses, gpaMax);
            courses = FilterMinYear(courses, yearMin);
            courses = FilterMaxYear(courses, yearMax);

            return courses;
        }
    }



}