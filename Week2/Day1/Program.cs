using System;

class Program
{
    static void Main()
    {
        Repository<Student> students = new Repository<Student>();
        students.Add(new Student { Name = "Ali" });
        students.Add(new Student { Name = "Sara" });

        Repository<Course> courses = new Repository<Course>();
        courses.Add(new Course { Title = "C#" });
        courses.Add(new Course { Title = "ASP.NET" });

        Console.WriteLine("Students:");
        foreach (var student in students.GetAll())
        {
            Console.WriteLine(student.Name);
        }

        Console.WriteLine();

        Console.WriteLine("Courses:");
        foreach (var course in courses.GetAll())
        {
            Console.WriteLine(course.Title);
        }

        Console.WriteLine();

        var result = students.Find(s => s.Name == "Ali");
        Console.WriteLine("Found Student: " + result.Name);

        // GetAll returns IReadOnlyList<T>
        // so the caller cannot modify the collection.
        // students.GetAll().Add(new Student()); // Error
    }
}