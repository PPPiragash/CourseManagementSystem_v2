using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem_v2
{
      public class Program
     {
        static void Main(string[] args)
        {
            CourseRepository repository = new CourseRepository();
            repository.InitializeDatabase();
            int choice;

            do
            {
                Console.WriteLine("Course Management System: ");
                Console.WriteLine("1. Add a Course ");
                Console.WriteLine("2. View All Courses ");
                Console.WriteLine("3. Update a Course ");
                Console.WriteLine("4. Delete a Course ");
                Console.WriteLine("5. Exit ");
                Console.Write("Choose an option: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Clear();

                        Console.Write("Enter Course ID : ");
                        string courseId = Console.ReadLine();

                        Console.Write("Enter Course Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter Course Duration: ");
                        string duration = Console.ReadLine();

                        Console.Write("Enter Course price: ");
                        decimal price = decimal.Parse(Console.ReadLine());

                        repository.AddCourse(new Course(courseId, title, duration, price));
                        Console.WriteLine("Course added successfully.");
                        break;

                    case 2:
                        Console.Clear();

                        Console.WriteLine("List of Courses:");
                        foreach (var course in repository.GetAllCourses())
                        {
                            Console.WriteLine(course);
                        }
                        break;

                    case 3:
                        Console.Clear();

                        Console.Write("Enter the Course ID to update: ");
                        string updateId = Console.ReadLine();
                        Console.Write("Enter new Title: ");
                        string newTitle = Console.ReadLine();
                        Console.Write("Enter new Duration: ");
                        string newDuration = Console.ReadLine();
                        Console.Write("Enter new Price: ");
                        decimal newPrice = decimal.Parse(Console.ReadLine());
                        var existingCourse = repository.GetCourseById(updateId);
                        if (existingCourse != null)
                        {
                            existingCourse.Title = newTitle;
                            existingCourse.Duration = newDuration;
                            existingCourse.Price = newPrice;
                            Console.WriteLine("Course updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Course Not found");
                        }
                        break;

                    case 4:
                        Console.Clear();

                        Console.Write("Enter the Course ID to delete: ");
                        string deleteId = Console.ReadLine();

                        repository.DeleteCourse(deleteId);
                        Console.WriteLine("Course deleted successfully.");
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("Exiting....");
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid Option. Please Try Again.");
                        break;
                }

            } while (choice != 5);
        }
     }
}
