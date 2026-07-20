#nullable enable

/*
1. Write a console program with at least 3 value-type and 3 reference-type variables, printing each one's type using 
"GetType()".
2. Write a method that demonstrates the value-vs-reference copy behavior, printing before and after a mutation.
3. Write a grade-classifier method using a switch expression, covering at least 4 score ranges.
4. Write a small program that reads user input and handles a possibly-null value safely, with nullable reference types 
enabled.
5. Commit the day's work to your GitHub repository with a clear, descriptive commit message.
*/

// Value types
int age = 10;
bool isStudent = true;
double average = 92.8;

// Reference types
string firstname = "Sondos";
string lastname = "Zah";
int[] marks = { 90, 91, 80 };

Console.WriteLine("===== Variables Types =====");

Console.WriteLine($"Age: {age}, Type: {age.GetType()}");
Console.WriteLine($"Is Student: {isStudent}, Type: {isStudent.GetType()}");
Console.WriteLine($"Average: {average}, Type: {average.GetType()}");

Console.WriteLine($"First Name: {firstname}, Type: {firstname.GetType()}");
Console.WriteLine($"Last Name: {lastname}, Type: {lastname.GetType()}");
Console.WriteLine($"Marks Type: {marks.GetType()}");

// ==========================================
// Value type and reference type copy behavior

Console.WriteLine("\n===== Copy Behavior =====");

CopyBehavior();

// ==========================================
// Grade classifier

Console.WriteLine("\n===== Grade Classifier =====");

Console.Write("Enter your grade: ");
string? gradeInput = Console.ReadLine();

if (int.TryParse(gradeInput, out int grade))
{
    if (grade >= 0 && grade <= 100)
    {
        string result = GradeClassifier(grade);

        Console.WriteLine($"Your grade classification is: {result}");
    }
    else
    {
        Console.WriteLine("Grade must be between 0 and 100.");
    }
}
else
{
    Console.WriteLine("Invalid grade.");
}

// ==========================================
// Nullable user input

Console.WriteLine("\n===== Nullable User Input =====");

Console.Write("Enter your name: ");
string? userName = Console.ReadLine();

if (string.IsNullOrWhiteSpace(userName))
{
    Console.WriteLine("You did not enter a valid name.");
}
else
{
    Console.WriteLine($"Welcome, {userName}!");
}

// ==========================================
// Methods

static void CopyBehavior()
{
    // Value type copy
    int firstNumber = 10;
    int secondNumber = firstNumber;

    Console.WriteLine("Value type before mutation:");
    Console.WriteLine($"First number: {firstNumber}");
    Console.WriteLine($"Second number: {secondNumber}");

    secondNumber = 20;

    Console.WriteLine("Value type after mutation:");
    Console.WriteLine($"First number: {firstNumber}");
    Console.WriteLine($"Second number: {secondNumber}");

    // Reference type copy
    int[] firstArray = { 10, 20, 30 };
    int[] secondArray = firstArray;

    Console.WriteLine("\nReference type before mutation:");
    Console.WriteLine($"First array value: {firstArray[0]}");
    Console.WriteLine($"Second array value: {secondArray[0]}");

    secondArray[0] = 100;

    Console.WriteLine("Reference type after mutation:");
    Console.WriteLine($"First array value: {firstArray[0]}");
    Console.WriteLine($"Second array value: {secondArray[0]}");
}

static string GradeClassifier(int grade)
{
    return grade switch
    {
        >= 90 => "Excellent",
        >= 80 => "Very Good",
        >= 70 => "Good",
        >= 60 => "Pass",
        _ => "Fail"
    };
}