using System.Text;

public class Course
{
    public string Name { get; set; }
    public int Credit { get; set; }
    public int Grade { get; set; }

    public Course(string name, int credit, int grade)
    {
        Name = name;
        Credit = credit;
        Grade = grade;
    }
}

public class Student
{
    public string Name { get; set; }
    public List<Course> Courses { get; set; }

    public Student(string name, List<Course> courses)
    {
        Name = name;
        Courses = courses;
    }

    public decimal CalculateGPA()
    {
        int totalCreditHours = 0;
        int totalGradePoints = 0;

        foreach (var course in Courses)
        {
            totalCreditHours += course.Credit;
            totalGradePoints += course.Credit * course.Grade;
        }

        return (decimal)totalGradePoints / totalCreditHours;
    }
}

public class GPAFormatter
{
    private const string CourseHeaderFormat = "{0,-25} {1,-10} {2,-15}";
    private const string CourseDataFormat = "{0,-25} {1,-10} {2,-15}";

    public string Format(Student student)
    {
        decimal gpa = student.CalculateGPA();
        int leadingDigit = (int)gpa;
        int trailingDigits = (int)(gpa * 100) - (leadingDigit * 100);

        var builder = new StringBuilder();
        builder.AppendLine($"Student: {student.Name}");
        builder.AppendLine(String.Format(CourseHeaderFormat, "Course", "Grade", "Credit Hours"));

        foreach (var course in student.Courses)
        {
            builder.AppendLine(String.Format(CourseDataFormat, course.Name, course.Grade, course.Credit));
        }

        builder.AppendLine($"\nFinal GPA: {leadingDigit}.{trailingDigits}");
        return builder.ToString();
    }
}


public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Grade Point Average Calculator");

        List<Course> courses = new List<Course>();
        string studentName = "";
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nSelect an option: ");
            Console.WriteLine("1. Enter Student Name");
            Console.WriteLine("2. Add Course");
            Console.WriteLine("3. View GPA");
            Console.WriteLine("4. Exit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Enter the student's name: ");
                    studentName = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("Enter the course name: ");
                    string courseName = Console.ReadLine();

                    Console.Write("Enter the course credit: ");
                    int courseCredit = int.Parse(Console.ReadLine());

                    Console.Write("Enter the course grade: ");
                    int courseGrade = int.Parse(Console.ReadLine());

                    courses.Add(new Course(courseName, courseCredit, courseGrade));
                    break;
                case "3":
                    if (!string.IsNullOrWhiteSpace(studentName) && courses.Count > 0)
                    {
                        Student student = new Student(studentName, courses);
                        GPAFormatter formatter = new GPAFormatter();
                        string result = formatter.Format(student);
                        Console.WriteLine(result);
                    }
                    else
                    {
                        Console.WriteLine("Please enter student name and add at least one course before viewing GPA.");
                    }
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select again.");
                    break;
            }
        }
    }
}
