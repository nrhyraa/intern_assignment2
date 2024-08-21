using System;
using System.Collections.Generic;

namespace StudentManagementSystem
{
    class Program
    {
        static int maxStudents = 50;
        static List<Student> students = new List<Student>();
        static int studentCount = 0;

        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("Welcome to the Student Management System");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Add a New Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search for a Student by ID");
                Console.WriteLine("4. Remove a Student by ID");
                Console.WriteLine("5. Update a Student's Grade");
                Console.WriteLine("6. Display Average Grade");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine();

                    switch (choice)
                    {
                        case 1:
                            AddStudent();
                            break;
                        case 2:
                            ViewAllStudents();
                            break;
                        case 3:
                            SearchStudentByID();
                            break;
                        case 4:
                            RemoveStudentByID();
                            break;
                        case 5:
                            UpdateStudentGrade();
                            break;
                        case 6:
                            DisplayAverageGrade();
                            break;
                        case 7:
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine();
            } while (choice != 7);
        }

        static void AddStudent()
        {
            if (students.Count >= maxStudents)
            {
                Console.WriteLine("Cannot add more students. The list is full.");
                return;
            }

            Console.Write("Enter Student ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // Check if the ID already exists
                if (FindStudentByID(id) != null)
                {
                    Console.WriteLine("A student with this ID already exists.");
                    return;
                }

                Console.Write("Enter Student Name: ");
                string? name = Console.ReadLine();

                Console.Write("Enter Student Age: ");
                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    Console.Write("Enter Student Grade: ");
                    if (double.TryParse(Console.ReadLine(), out double grade))
                    {
                        name = CapitalizeWords(name);
                        students.Add(new Student(id, name ?? "Unknown", age, grade));
                        studentCount++;
                        Console.WriteLine("Student added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for grade.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for age.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for ID.");
            }
        }

        static void ViewAllStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students to display.");
                return;
            }

            foreach (var student in students)
            {
                student.DisplayDetails();
                Console.WriteLine(student.IsPassing() ? " - Passing" : " - Failing");
            }
        }

        static void SearchStudentByID()
        {
            Console.Write("Enter Student ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var student = FindStudentByID(id);
                if (student != null)
                {
                    student.DisplayDetails();
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for ID.");
            }
        }

        static void RemoveStudentByID()
        {
            Console.Write("Enter Student ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var student = FindStudentByID(id);
                if (student != null)
                {
                    students.Remove(student);
                    studentCount--;
                    Console.WriteLine("Student removed successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for ID.");
            }
        }

        static void UpdateStudentGrade()
        {
            Console.Write("Enter Student ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var student = FindStudentByID(id);
                if (student != null)
                {
                    Console.Write("Enter new Grade: ");
                    if (double.TryParse(Console.ReadLine(), out double grade))
                    {
                        student.Grade = grade;
                        Console.WriteLine("Student grade updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for grade.");
                    }
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for ID.");
            }
        }

        static void DisplayAverageGrade()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students to calculate average grade.");
                return;
            }

            double totalGrade = 0;
            foreach (var student in students)
            {
                totalGrade += student.Grade;
            }
            double averageGrade = totalGrade / students.Count;
            Console.WriteLine($"Average grade of all students: {averageGrade:F2}");
        }

        static Student? FindStudentByID(int id)
        {
            return students.Find(s => s.ID == id);
        }

        static string CapitalizeWords(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            string[] words = input.ToLower().Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }
            return string.Join(' ', words);
        }
    }

    class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }

        public Student(int id, string name, int age, double grade)
        {
            ID = id;
            Name = name;
            Age = age;
            Grade = grade;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"ID: {ID}, Name: {Name}, Age: {Age}, Grade: {Grade}");
        }

        public bool IsPassing()
        {
            return Grade >= 60;
        }
    }
}
