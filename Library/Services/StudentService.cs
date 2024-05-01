using CourseBueno.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;

namespace CourseBueno.Library.Services;
public class StudentService {
    private List<Student> students;
    public List<Student> Students {
            get {
                return students;
            }
        }

    private static StudentService? instance;
    public static StudentService Current {
        get {
            return instance ??= new StudentService();
        }
    }
    private StudentService() {
        students = [];
        var response = new WebRequestHandler().Get("/student").Result;
        
        //students = JsonConvert.DeserializeObject<List<Student>>(response) ?? [];
    }

    public void Add(Student student) {
        students.Add(student);
    }
    public void Update(Student students, string name, string classification) {
        students.Name = name ?? students.Name;
        students.Classification = classification ?? students.Classification;
    }

    public void Remove(Student student) {
        Students.Remove(student);
    }

    public void Remove(Guid sid, Guid cid) {
        if(Get(sid) is Student student
            && CourseService.Current.Get(cid) is Course course) {
                course.Roster.Remove(student);
                student.Courses.Remove(course);
        }
    }

    public void Add(Guid sid, Guid cid) {
        if(Get(sid) is Student student
            && CourseService.Current.Get(cid) is Course course) {
                course.Roster.Add(student);
                student.Courses.Add(course);
        }
    }

    public IEnumerable<Student> Search(string query) {
        return Students.Where(s=>(s != null) && s.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ?? [];
    }

    public Student? Get(Guid? id) {
        return Students.FirstOrDefault(p => p.ID == id);
    }

}