using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Registryry;

namespace Universityty.Models
{
    public sealed class Dbase
    {
        /// <summary>
        /// Temporary variable to store connection string
        /// utilizes the get connection string method
        /// </summary>
        public static string conn = GetConnectionString();
        
        /// <summary>
        /// Private Constructor
        /// </summary>
        private Dbase()
        {
        }
        /// <summary>
        /// instance of the class
        /// </summary>
        private static Dbase _instance;

        /// <summary>
        /// Properties, making sure that the instance is null 
        /// before returning the instance of the Singleton Class Object
        /// </summary>
        public static Dbase instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Dbase();
                }

                return _instance;
            }
        }

        /// <summary>
        /// This Method is used for performing select statements on the database
        /// to return a DataSet with the select statment ran against the database.
        /// Uses a Data Adapter to fill a fresh DataSet and send it back.
        /// </summary>
        /// <param name="query">The SQL Query</param>
        /// <returns>DataSet with Select Statement ran against the database </returns>
        public DataSet GetResults(string query)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                sqlcon.ConnectionString = conn;
                SqlDataAdapter dap = new SqlDataAdapter(query, sqlcon);
                DataSet StuCour = new DataSet();
                dap.Fill(StuCour);
                return StuCour;
            }
        }

        /// <summary>
        /// Takes the parameters and uses an adapter object and stores non-select command and 
        /// Fills
        /// </summary>
        /// <param name="query">The non select query that the person that uses this method wants
        /// to perform on the Database Data and updates changes to the official database</param>
        public void nonQueryResults(string query)
        {
            bool workDone = false;
            string result;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter();

                    SqlCommand cmd = new SqlCommand(query, sqlcon);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    da.Fill(new DataSet());
                    workDone = true;
                }
            }
            catch (Exception ex)
            {
                
            }

            //If changes were made to the database or not
            if (workDone)
            {
                result = "The Database has been edited.";
                Console.WriteLine(result);
            }
            else
            {
                result = "No changes made to the database";
                Console.WriteLine(result);
            }
        }

        
        /// <summary>
        /// Temporary placeholder method to hold the connection string and fetch it for access
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return "Data Source=demodb.cih8tqeeovcu.us-west-2.rds.amazonaws.com,1433; Initial Catalog=University;Persist Security Info=False; User ID=Devonte; Password=holmes!1;Encrypt=False";
        }

        /// <summary>
        /// If ever neccesary, connect to the database as a connected architecture and prints out the
        /// status of the current connection
        /// </summary>
        public static void openDbConnection()
        {
            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();
            Console.WriteLine("state: {0}", sqlcon.State);
        }


        /// <summary>
        /// A method to add a new student to the database 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool AddStudent(Normal s)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("UpdateStudentDetails", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fName", s.fName);
                cmd.Parameters.AddWithValue(@"lName", s.lName);
                cmd.Parameters.AddWithValue("@Email", s.Email);
                cmd.Parameters.AddWithValue("@Pword", s.Pword);
                int affected = cmd.ExecuteNonQuery();
                if (affected >= 1)
                    {
                        return true;
                    }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// A method to update Student information
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool UpdateStudent(Normal s)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("UpdateStudentDetails", sqlcon);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fName", s.fName);
                cmd.Parameters.AddWithValue(@"lName", s.lName);
                cmd.Parameters.AddWithValue("@Email", s.Email);
                cmd.Parameters.AddWithValue("@Pword", s.Pword);
                int affected = cmd.ExecuteNonQuery();
                if (affected >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
        /// <summary>
        /// A method to get a list of all the student models and return them
        /// </summary>
        /// <returns></returns>
        public List<Normal> GetAllStudents() {
            using (SqlConnection sqlcon = new SqlConnection(conn)) { 
                List<Normal> StudentList = new List<Normal>();
                SqlCommand cmd = new SqlCommand("GetStudents", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    StudentList.Add(
                           new Normal
                           {
                               //StudentId = Convert.ToInt32(row["StudentId"]),
                               fName = Convert.ToString(row["fName"]),
                               lName = Convert.ToString(row["lName"]),
                               Email = Convert.ToString(row["Email"]),
                               Pword = Convert.ToString(row["Pword"])
                           }
                           );
                  }
                return StudentList;
            }
        }
        */

        public bool DeleteStudent(int Sid)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("DeletStudentById", sqlcon);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", Sid);
                int affected = cmd.ExecuteNonQuery();
                if (affected >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        public Normal MatchStudentLogin(string email, string pword)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                Normal matchedStudent = new Normal();
                SqlCommand cmd = new SqlCommand("StudentLogin", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Pword", pword);
                cmd.Connection = sqlcon;
                sqlcon.Open();
                SqlParameter match = new SqlParameter();
                match.Direction  = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(match);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                            matchedStudent.StudentId = (int)reader.GetValue(0);
                            matchedStudent.fName = (string)reader.GetValue(1);
                            matchedStudent.lName = (string)reader.GetValue(2);
                            matchedStudent.Email = (string)reader.GetValue(3);
                            matchedStudent.Pword = (string)reader.GetValue(4);

                    }
                 sqlcon.Close();
                }
                catch (SqlException ex)
                {
                    
                }
                return matchedStudent;
            }
        }
        
        public List<Course> getStudentCourses(Normal Stu)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                List<Course> roster = new List<Course>();
                Normal matchedStudent = new Normal();
                SqlCommand cmd = new SqlCommand("Schedule", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", Stu.StudentId);
                cmd.Connection = sqlcon;
                sqlcon.Open();
                SqlParameter match = new SqlParameter();
                match.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(match);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Course co = new Course();
                        co.CourseId = (int)reader.GetValue(0);
                        co.Name = (string)reader.GetValue(1);
                        co.startTime = (DateTime)reader.GetValue(2);
                        co.creditHour = (DateTime)reader.GetValue(3);
                        co.CourseId = (int)reader.GetValue(5);

                        roster.Add(co);
                    }
                    sqlcon.Close();
                }
                catch (SqlException ex)
                {

                }
                return roster;
            }
        }

        }

   
    }

