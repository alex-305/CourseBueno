using System.Globalization;
using Classifications;
abstract public class Initializers {
    public static Course Course() {
        Console.Clear();
        Console.WriteLine("==Course Creation Menu==");
        Console.WriteLine("==Name:");
        var name = Console.ReadLine();
        Console.WriteLine("==Description:");
        var description = Console.ReadLine();
        return new Course{Name = name, Description = description};
    }
    public static Faculty Admin() {
        Console.Clear();
        Console.WriteLine("==Character Creation Menu==");
        Console.WriteLine("==First Name:");
        var firstName = Console.ReadLine();
        Console.WriteLine("==Last Name:");
        var lastName = Console.ReadLine();
        return new Faculty {FirstName=firstName, LastName=lastName, Classification=FacultyClassification.administrator};
    }
    public static Faculty Professor() {
        Console.Clear();
        Console.WriteLine("==Professor Creation Menu==");
        Console.WriteLine("==First Name:");
        var firstName = Console.ReadLine();
        Console.WriteLine("==Last Name:");
        var lastName = Console.ReadLine();
        bool successfullParse;
        FacultyClassification facultyClassification;
        do {
            Console.WriteLine("==Classification(Professor or Administrator):");
            String? userInput = Console.ReadLine();
            userInput = userInput?.ToLower();

            successfullParse = Enum.TryParse(userInput, out FacultyClassification classification);
            facultyClassification = classification;

            if (!successfullParse) {
                Console.WriteLine("Parse failed. Try again.");
            }
        } while(!successfullParse);
        return new Faculty { FirstName = firstName,
        LastName = lastName,
        Classification = facultyClassification };
    }
    public static Student Student() {
        Console.Clear();
        Console.WriteLine("==Student Creation Menu==");
        Console.WriteLine("==First Name:");
        var firstName = Console.ReadLine();
        Console.WriteLine("==Last Name:");
        var lastName = Console.ReadLine();
        bool successfullParse;
        StudentClassification studentClassification;
        do {
            Console.WriteLine("==Classification(Freshman, Sophomore, Junior, Senior):");
            String? userInput = Console.ReadLine();
            userInput = userInput?.ToLower();

            successfullParse = Enum.TryParse(userInput, out StudentClassification classification);
            studentClassification = classification;

            if (!successfullParse) {
                Console.WriteLine("Parse failed. Try again.");
            }
        } while(!successfullParse);
        return new Student { FirstName = firstName, 
        LastName = lastName, 
        Classification = studentClassification };

    }
    public static Module Module() {
        Console.Clear();
        Console.WriteLine("==Module Creation Menu==");
        Console.WriteLine("==Name:");
        var name = Console.ReadLine();
        Console.WriteLine("==Description:");
        var description = Console.ReadLine();
        return new Module {Name = name, Description = description};
    }
    public static Assignment Assignment() {
        Console.Clear();
        Console.WriteLine("==Assignment Creation Menu==");
        Console.WriteLine("==Name:");
        var name = Console.ReadLine();
        Console.WriteLine("==Description:");
        var description = Console.ReadLine();
        Console.WriteLine("==Available Points:");
        var availablePoints = Console.ReadLine();
        int points = 0;
        if(Int32.TryParse(availablePoints, out int totalAvailablePoints)) {
            points = totalAvailablePoints;
        } else {
            Console.WriteLine("Something went wrong. Initializing total points to 0.");
        }

        Console.WriteLine("==Due Date(mm/dd/yy):");
        var dateInput = Console.ReadLine();
        var date = DateTime.Parse(dateInput ?? "0/0/0", new CultureInfo("en-US"));

        return new Assignment{Name = name, 
        Description = description, 
        TotalAvailablePoints = points,
        DueDate = date
        };
    }

    public static ContentItem ContentItem() {
        Console.Clear();
        Console.WriteLine("==Content Creation Menu==");
        Console.WriteLine("==Name:");
        var name = Console.ReadLine();
        Console.WriteLine("==Description:");
        var description = Console.ReadLine();
        return new ContentItem { Name = name, Description = description };
    }
}