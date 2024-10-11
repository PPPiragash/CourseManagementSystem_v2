using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem_v2
{
    public class CourseRepository
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;";

        public void InitializeDatabase()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string createDatabaseQuery = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name='CourseManagement')" +
                                             "BEGIN CREATE DATABASE CourseManagement END";
                string createTableQuery = @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Courses')
                                          BEGIN
                                                CREATE TABLE Courses (
                                                   CourseId NVARCHAR(100) PRIMARY KEY,
                                                   Title NVARCHAR(255) NOT NULL,
                                                   Duration NVARCHAR(100) NOT NULL,
                                                   Price DECIMAL(18,2) NOT NULL
                                                );
                                            ";



                conn.Open();

                using(SqlCommand cmd = new SqlCommand(createDatabaseQuery,conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand(createTableQuery,conn))
                {
                    cmd.ExecuteNonQuery ();
                }


            }
        }


        public void AddCourse(Course course)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=CourseManagement;Trusted_Connection=True;"))
            {
                string query = "INSERT INTO Courses (CourseId,Title,Duration,Price) VALUES (@CourseId,@Title,@Duration,@Price)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseId", course{CourseId});
                    cmd.Parameters.AddWithValue("@Title", course{Title});
                    cmd.Parameters.AddWithValue("@Duration", course{Duration});
                    cmd.Parameters.AddWithValue("@Price", course{Price});
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
        }


        public List<Course> GetAllCourses ()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=CourseManagement;Trusted_Connection=True;"))
            {
                
                string query = "Select * From Courses";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            (
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDecimal(3)
                            );
                        }
                    }
                }
                return courses;
            }
        }



        public void UpdateCourse (Course course)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=CourseManagement;Trusted_Connection=True;"))
            {
                string query = "Update Courses Set Title=@Title,Duration=@Duration,Price=@Price Where CourseId=@CourseId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseId", course{CourseId});
                    cmd.Parameters.AddWithValue("@Title", course{Title});
                    cmd.Parameters.AddWithValue("@Duration", course{Duration});
                    cmd.Parameters.AddWithValue("@Price", course{Price});
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }


            }
        }


        public void DeleteCourse(string courseId)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=CourseManagement;Trusted_Connection=True;"))
            {
                string query = "Delete From Courses Where CourseId = @CourseId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseId", CourseId);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }


        public Course GetCourseById (string courseId)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=CourseManagement;Trusted_Connection=True;"))
            {
                string query = "Select * from Courses where CourseId=@CourseId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Course(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDecimal(3)
                                );
                        }
                    }
                }

            }

            return null;
            

        }

        public string CapitalizeTitle (string title)
        {
            if(string.IsNullOrEmpty(title))
                return title;

            var words = title.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", words); 

            
        }




        public bool ValidateCoursePrice(decimal price)
        {
            if (price <= 0)
            {
                Console.WriteLine("Price must be a Positive Value. Please Enter a Valid Price: ");
                return false;
            }
            return true;
        }


    }
}
