using CourseBueno;

abstract public class UpdateFields {

    public static void UpdatePerson(Person person) {
        Console.Clear();
        Console.WriteLine($"{{{{ Editing: {person} }}}}");
        Console.WriteLine("1. (M) First Name\n2. (M) Last Name\n0. (<) Back");

        switch(Console.ReadLine()) {
            case "1":
                Console.WriteLine("Enter first name:");
                person.FirstName = Console.ReadLine();
                break;
            case "2":
                Console.WriteLine("Enter last name:");
                person.LastName = Console.ReadLine();
                break;
            default:
                return;
        }
    }


    public static void UpdateModule(Module module) {
        Console.Clear();
        Console.WriteLine($"{{{{ Editing Module: {module} }}}}");
        Console.WriteLine("1. (+) Add Content Item\n2. (-) Remove Content Item");
        Console.WriteLine("3. Change Module Name\n4. Change Module Description");
        Console.WriteLine("0. (<) Back");

        switch(Console.ReadLine()) {
            case "1":
                module.Contents.Add(Initializers.ContentItem());
                break;
            case "2":
                var contentSelection = ListHelpers.SelectItem(module.Contents);
                ListHelpers.RemoveItem(module.Contents,contentSelection);
                break;
            case "3":
                Console.Clear();
                Console.WriteLine("Enter new name: ");
                module.Name = Console.ReadLine();
                break;
            case "4":
                Console.Clear();
                Console.WriteLine("Enter new description: ");
                module.Description = Console.ReadLine();
                break;
            default:
                return;
        }

    }
    public static void UpdateCourse(Course course) {
        Console.Clear();
        Console.WriteLine($"{{{{ Editing Course: {course} }}}}");
        Console.WriteLine("1. (+) Students\n2. (-) Student");
        Console.WriteLine("3. (+) Assignment\n4. (-) Assignment");
        Console.WriteLine("5. (+) Module\n6. (-) Module");
        Console.WriteLine("7. (+) Professor");
        Console.WriteLine("8. (M) Course Name\n9. (M) Course Description");
        Console.WriteLine("10. (M) Modules");
        Console.WriteLine("0. (<) Back");

        var userChoice = Console.ReadLine();
        
        switch(userChoice) {
            case "1":
                Console.Clear();
                if(Program.students.Count > 0) {
                    course.AddStudent(ListHelpers.SelectItem(Program.students));
                } else {
                    ListHelpers.ListList(Program.students);
                    Console.WriteLine("Return to continue.");
                    Console.ReadLine();
                }
                break;
            case "2":
                Console.Clear();
                var studentSelection = ListHelpers.SelectItem(course.Roster);
                ListHelpers.RemoveItem(course.Roster,studentSelection);
                ListHelpers.RemoveItem(studentSelection.Courses, course);
                break;
            case "3":
                Console.Clear();
                course.Assignments?.Add(Initializers.Assignment());
                break;
            case "4":
                Console.Clear();
                var assignmentSelection = ListHelpers.SelectItem(course.Assignments);
                ListHelpers.RemoveItem(course.Assignments,assignmentSelection);
                break;
            case "5":
                Console.Clear();
                course.Modules?.Add(Initializers.Module());
                break;
            case "6":
                Console.Clear();
                var moduleSelection = ListHelpers.SelectItem(course.Modules);
                ListHelpers.RemoveItem(course.Modules,moduleSelection);
                break;
            case "7":
                Console.Clear();
                if(Program.professors.Count > 0) {
                    course.Professor?.Courses.Remove(course);
                    course.Professor = ListHelpers.SelectItem(Program.professors);
                    course.Professor.Courses.Add(course);
                } else {
                    ListHelpers.ListList(Program.professors);
                    Console.WriteLine("Return to continue.");
                    Console.ReadLine();
                }
                break;
            case "8":
                Console.Clear();
                Console.WriteLine("__Editing Course Name__");
                Console.WriteLine("Enter new name: ");
                course.Name = Console.ReadLine();
                break;
            case "9":
                Console.Clear();
                Console.WriteLine("__Editing Course Description__");
                Console.WriteLine("Enter new description: ");
                course.Description = Console.ReadLine();
                break;
            case "10":
                Console.Clear();
                Console.WriteLine("__Modules__");
                if(course.Modules.Count > 0) {
                    Views.Module(ListHelpers.SelectItem(course.Modules));
                } else {
                    ListHelpers.ListList(course.Modules);
                }
                break;
            default:
                return;
        }
    }

}