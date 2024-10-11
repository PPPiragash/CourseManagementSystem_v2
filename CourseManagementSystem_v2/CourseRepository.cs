using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

                using (SqlCommand cmd = new SqlCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }


            }
        }
    }
}
