using System.Data;

abstract public class ListHelpers {


    public static void ListList<T>(List<T> list) {
        if(list.Count > 0) {
            int i = 0;
            foreach(T t in list) {
                Console.WriteLine(++i + ". " + t);
            } 
        } else {
            Console.WriteLine("Empty.");
        }
    }

    public static T SelectItem<T>(List<T> list) {
        ListList(list);
        Console.WriteLine("Select an item:");
        int returnNum = 1;
        if(int.TryParse(Console.ReadLine(), out int itemNum) && itemNum > 0 && itemNum <=list.Count) {
            returnNum = itemNum;
        } else {
            Console.WriteLine("Something went wrong.");
            Console.WriteLine("Return to continue");
            Console.ReadLine();
        }
        return list[returnNum-1];
    }

    public static void RemoveItem<T>(List<T> list, T item) {
        if(list.Count > 0) {
            try {
                if(!(list?.Remove(item) ?? false)) {
                    throw new DataException($"Could not remove: '{item}',  does not exist in list of courses.");
                }
            } catch (DataException de) {
                Console.WriteLine(de);
            }
        } else {
            ListHelpers.ListList(list);
            Console.WriteLine("Return to continue.");
            Console.ReadLine();
        }
    }

    public static string SearchStudents (List<Student> students, List<object> foundList, string searchTerm, ref int elementCount) {
        string studentString="";

        foreach(Student student in students) {
            if(student.FirstName != null 
            && student.FirstName.ToLower().Contains(searchTerm.ToLower())
            || student.LastName != null
            && student.LastName.ToLower().Contains(searchTerm.ToLower())) {
                studentString += ++elementCount + ". " + student + '\n';
                foundList.Add(student);
            }
        }
        return studentString;
    }
    public static string SearchProfessors (List<Faculty> professors, List<object> foundList, string searchTerm, ref int elementCount) {
        string professorString="";

        foreach(Faculty professor in professors) {
            if(professor.FirstName != null 
            && professor.FirstName.ToLower().Contains(searchTerm.ToLower())
            || professor.LastName != null
            && professor.LastName.ToLower().Contains(searchTerm.ToLower())) {
                professorString += ++elementCount + ". " + professor + '\n';
                foundList.Add(professor);
            }
        }
        return professorString;
    }

}