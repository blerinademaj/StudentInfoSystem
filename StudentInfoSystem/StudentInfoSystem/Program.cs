using System;
using MySql.Data.MySqlClient;


namespace StudentInfoSystem
{
    class Program
    {
        static string connectionString = "server=localhost; database=studentdb; user=root";

        static void Main(string[] args)
        {
            Console.WriteLine("Student Information System");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Student");
            Console.WriteLine("3. List Student");
            Console.WriteLine("4. List Courses");

            Console.WriteLine("Select an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    ViewStudent();
                    break;
                case 3:
                    ListStudents();
                    break;
                case 4:
                    ListCourses();
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

        }

        //krijimi i metodes Addstudent
        static void AddStudent()
        {
            Console.WriteLine("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter last name: ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter you age: ");
            int age = int.Parse(Console.ReadLine());

            //Tani bejme nderlidhje e stringConnect, pra MySQL dhe C#
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                //fillojme me insertimin e te dhenave te studenteve ne databaze permes C#
                string query = "INSERT INTO students (FirstName, LastName, Age) VALUES (@firstName, @lastName, @age)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@age", age);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Student Added Succesfully!");
                    }
                    else
                    {
                        Console.WriteLine("Error process in Adding Students to Database!");
                    }
                }

            }
        }
            static void ViewStudent()
            {
                Console.WriteLine("Enter Student ID: ");

                int studentID = int.Parse(Console.ReadLine());
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT FROM students WHERE StudentID = @studentID";
                    using(MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentID", studentID);
                        using(MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Student ID: { reader["StudentID"]}");
                                Console.WriteLine($"First Name: { reader["FirstName"]}");
                                Console.WriteLine($"Last Name: { reader["LastName"]}");
                                Console.WriteLine($"Age: { reader["Age"]}");
                            }
                            else
                            {
                                Console.WriteLine("Student not found...");
                            }
                        }
                    }
                }
            }

            static void ListStudents()
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM students";

                    using(MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using(MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("List of Students: ");
                            while (reader.Read())
                            {
                                Console.WriteLine($"Student ID: {reader["StudentID"]}, Name:{reader["FirstName"]} {reader["LastName"]}, Age: {reader["Age"]}");

                            }
                        }
                    }

                }
            }

            static void ListCourses()
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Courses";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using(MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("List of Courses: ");
                            while (reader.Read())
                            {
                                int courseID = reader.GetInt32("CourseID");
                                string courseName = reader.GetString("CourseName: ");
                                Console.WriteLine($"Course ID: {courseID}, Name: {courseName}");
                            }
                        }
                    }
                }
            }

        }
    }
