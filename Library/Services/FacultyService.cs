using CourseBueno.Models;
using CourseBueno.Models.Abstracts;

namespace CourseBueno.Library.Services;
public class FacultyService {

    public List<Faculty> Faculty {
        get { return faculty; }
    }
    private List<Faculty> faculty;

    private static FacultyService? instance;
    public static FacultyService Current {
        get {
            return instance ??= new FacultyService();
        }
    }
    private FacultyService() {
        faculty = [];
    }

    public void AddProfessor(Faculty faculty) {
        Faculty.Add(faculty);
    }
    public void Update(Faculty person, string name, string classification) {
        person.Name = name ?? person.Name;
        person.Classification = classification ?? person.Classification;
    }

    public void Remove(Faculty faculty) {
        Faculty.Remove(faculty);
    }

    public IEnumerable<Faculty> Search(string query) {
        return Faculty.Where(f=>(f != null) && f.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ?? [];
    }

    public Faculty? Get(Guid? id) {
        return Faculty.FirstOrDefault(p => p.ID == id);
    }

}