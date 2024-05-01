using CourseBueno;

abstract public class CourseHelpers {
    public static bool AdminMenu() {
        Console.Clear();
        Console.WriteLine("***Administrator Menu***");
        Console.WriteLine($"==Admin: {Program.admin}==");
        Console.WriteLine("1. Create Course\n2. Create Professor\n3. Create Student\n4. View Courses");
        Console.WriteLine("5. View Professors\n6. View Students\n7. Search\n0. Exit");
        switch(Console.ReadLine()) {
            case "0":
                return false;
            case "1":
                Program.courses.Add(Initializers.Course());
                break;
            case "2":
                Program.professors.Add(Initializers.Professor());
                break;
            case "3":
                Program.students.Add(Initializers.Student());
                break;
            case "4":
                Console.Clear();
                Console.WriteLine("__Courses__");
                if(Program.courses.Count > 0)
                    Views.Course(ListHelpers.SelectItem(Program.courses));
                else {
                    ListHelpers.ListList(Program.courses);
                    Console.WriteLine("Return to continue");
                    Console.ReadLine();
                }
                break;
            case "5":
                Console.Clear();
                Console.WriteLine("__Professors__");
                if(Program.professors.Count > 0) {
                    Faculty professor = ListHelpers.SelectItem(Program.professors);
                    Views.Professor(professor);
                } else {
                    ListHelpers.ListList(Program.professors);
                    Console.WriteLine("Return to continue");
                    Console.ReadLine();
                }
                break;
            case "6":
                Console.Clear();
                Console.WriteLine("__Students__");
                if(Program.students.Count > 0) {
                    Student student = ListHelpers.SelectItem(Program.students);
                    Views.Student(student);
                } else {
                    ListHelpers.ListList(Program.students);
                    Console.WriteLine("Return to continue");
                    Console.ReadLine();
                }
                break;
            case "7":
                Console.Clear();
                Console.WriteLine("Search: ");
                var searchTerm = Console.ReadLine();

                if(searchTerm!=null)
                    search(searchTerm);

                break;

        }
        return true;
    }



    public static void search(string searchTerm) {
        Console.Clear();
        int element = 0;
        List<object> foundList = [];

        string courseString = SearchCourses(Program.courses, foundList ,searchTerm, ref element);

        if(courseString!="") {
            Console.WriteLine("__Courses__");
            Console.WriteLine(courseString);
        }

        string studentString = ListHelpers.SearchStudents(Program.students, foundList, searchTerm, ref element);

        if(studentString!="") {
           Console.WriteLine("__Students__");
           Console.WriteLine(studentString);
        }

        string professorString = ListHelpers.SearchProfessors(Program.professors, foundList, searchTerm, ref element);

        if(professorString!="") {
           Console.WriteLine("__Professors__");
           Console.WriteLine(professorString);
        }

        if(element==0) {
            Console.WriteLine("No search results were found.");
            Console.WriteLine("Return to continue");
            Console.ReadLine();
            return;
        } else {
            Console.WriteLine("Select an element: ");
            var selection = Console.ReadLine();
            if(selection != null)
            SelectSearch(selection, foundList);
        }

    }

    public static void SelectSearch(string selection, List<object> foundList) {
        if(Int32.TryParse(selection, out int selectionNum) 
        && selectionNum > 0 
        && selectionNum <= foundList.Count) {
            Console.Clear();
            if(foundList[selectionNum-1] is Course) {
                Views.Course(foundList[selectionNum-1] as Course);
            } else if(foundList[selectionNum-1] is Faculty) {
                Views.Professor(foundList[selectionNum-1] as Faculty);
            } else if(foundList[selectionNum-1] is Student) {
                Views.Student(foundList[selectionNum-1] as Student);
            } else {
                Console.WriteLine("Error. Something went wrong.");
                Console.WriteLine("Return to continue");
                Console.ReadLine();
            }
        } else {
            Console.WriteLine("Error. Something went wrong.");
            Console.WriteLine("Return to continue");
            Console.ReadLine();
        }
    }

    public static string SearchCourses(List<Course> courses, List<object> foundList, string searchTerm ,ref int elementCount) {

        string courseString="";

        foreach(Course course in courses) {
            if(course.Name != null 
            && course.Name.ToLower().Contains(searchTerm.ToLower()) 
            || course.Description != null
            && course.Description.ToLower().Contains(searchTerm.ToLower())) {
                courseString += ++elementCount + ". " + course + '\n';
                foundList.Add(course);
            }
        }

        return courseString;

    }
}