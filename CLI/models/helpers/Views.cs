abstract public class Views {

    public static void Course(Course course) {
        Console.Clear();
        Console.WriteLine("+++ " + course.Name + " +++");
        Console.WriteLine("Professor: " + course.Professor ?? "empty");
        Console.WriteLine("Description: " + course.Description);

        Console.WriteLine("__Assignments__");
        ListHelpers.ListList(course.Assignments);

        Console.WriteLine("__Modules__");
        ListHelpers.ListList(course.Modules);

        Console.WriteLine("__Roster__");
        ListHelpers.ListList(course.Roster);

        Console.WriteLine("Actions: 1. Update Course\t0. (<) Back");
        var userInput = Console.ReadLine();
        if(userInput=="1") {
            UpdateFields.UpdateCourse(course);
        } else {
            return;
        }
    }

    public static void Student(Student student) {
        Console.Clear();
        Console.WriteLine(student);
        Console.WriteLine("__Courses__");
        ListHelpers.ListList(student.Courses);

        Console.WriteLine("Actions: 1. Update Student\t0. (<) Back");
        var userInput = Console.ReadLine();
        if(userInput=="1") {
            UpdateFields.UpdatePerson(student);
        } else {
            return;
        }
    }

    public static void Professor(Faculty professor) {
        Console.Clear();
        Console.WriteLine(professor);
        Console.WriteLine("__Courses__");
        ListHelpers.ListList(professor.Courses);

        Console.WriteLine("Actions: 1. Update Professor\t0. (<) Back");
        var userInput = Console.ReadLine();
        if(userInput=="1") {
            UpdateFields.UpdatePerson(professor);
        } else {
            return;
        }
    }


    public static void Module(Module module) {
        Console.Clear();
        Console.WriteLine(module);
        Console.WriteLine("Contents>");
        ListHelpers.ListList(module.Contents);

        Console.WriteLine("Actions: 1. Update Module\t0. (<) Back");
        var userInput = Console.ReadLine();
        if(userInput=="1") {
            UpdateFields.UpdateModule(module);
        } else {
            return;
        }

    }
}