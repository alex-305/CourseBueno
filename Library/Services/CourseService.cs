using System.Text;
using CourseBueno.Models;
using CourseBueno.Models.Abstracts;

namespace CourseBueno.Library.Services;
public class CourseService {
    public List<Course> Courses {
        get {
            return courses;
        }
    }
    private List<Course> courses = [];
    private static CourseService? instance;
    public static CourseService Current {
        get { 
            return instance ??= new CourseService();
        }
    }
    private CourseService() {}

    public void Add(Course course) {
        Courses.Add(course);
    }
    public void Add(Guid id, Person p) {
        if(StudentService.Current.Get(p.ID) is Student student
        && Get(id) is Course course) {
            course.Roster.Add(student);
            student.Courses.Add(course);
        }
    }

    public void Add(Guid id, Module m) {
        if(Get(id) is Course course) {
            course.Modules.Add(m);
        }
    }

    public void Add(Guid id, Assignment a) {
        if(Get(id) is Course course) {
            course.Assignments.Add(a);
        }
    }
    public void Add(Guid cid, Guid mid, ContentItem c) {
        if(GetModule(cid, mid) is Module module) {
            module.Contents.Add(c);
        }
    }

    public void Add(Guid cid, Guid aid, Submission s) {
        if(GetAssignment(cid, aid) is Assignment assignment) {
            assignment.Submissions.Add(s);
        }
    }

    public void Update(Guid id, string? name, string? description) {
        if(Get(id) is Course course) {
            course.Name = name ?? course.Name;
            course.Description = description ?? course.Description;
        }
    }

    public void UpdateAssignment(Guid cid, Guid id, string? name, string? description) {
        if(GetAssignment(cid, id) is Assignment assignment) {
            assignment.Name = name ?? assignment.Name;
            assignment.Description = description ?? assignment.Description;
        }
    }

    public void UpdateModule(Guid cid, Guid mid, string? name, string? description) {
        if(GetModule(cid, mid) is Module module) {
            module.Name = name ?? module.Name;
            module.Description = description ?? module.Description;
        }
    }

    public void UpdateContentItem(Guid cid, Guid mid, Guid ciid, string? name=null, string? description=null) {
        if(GetContentItem(cid, mid, ciid) is ContentItem contentItem) {
            contentItem.Name = name ?? contentItem.Name;
            contentItem.Description = description ?? contentItem.Description;
        }
    }

    public void UpdateSubmission(Guid cid, Guid aid, Guid studentid, string text) {
        if(GetSubmission(cid, aid, studentid) is Submission submission) {
            submission.Text = text;
        }
    }

    public void Remove(Course course) {
        Courses.Remove(course);
    }
    public void RemoveStudent(Guid cid, Guid pid) {
        if(StudentService.Current.Get(pid) is Student student
        && Get(cid) is Course course) {
            course.Roster.Remove(student);
            student.Courses.Remove(course);
        }
    }
    public void RemoveModule(Guid cid, Guid mid) {
        if(Get(cid) is Course course && GetModule(cid, mid) is Module module) {
            course.Modules.Remove(module);
        }
    }
    public void RemoveAssignment(Guid cid, Guid aid) {
        if(Get(cid) is Course course && GetAssignment(cid, aid) is Assignment assignment) {
            course.Assignments.Remove(assignment);
        }
    }
    public void RemoveItem(Guid cid, Guid mid, Guid iid) {
        if(GetModule(cid, mid) is Module module && GetContentItem(cid, mid, iid) is ContentItem content) {
            module.Contents.Remove(content);
        }
    }

    public Course? Get(Guid id) {
        return Courses.FirstOrDefault(c => c.ID == id);
    }

    public Module? GetModule(Guid cid, Guid mid) {
        return Get(cid)?.Modules.FirstOrDefault(m => m.ID == mid);
    }

    public ContentItem? GetContentItem(Guid cid, Guid mid, Guid ciid) {
        return GetModule(cid, mid)?.Contents.FirstOrDefault(c => c.ID == ciid);
    }

    public Assignment? GetAssignment(Guid cid, Guid aid) {
        return Get(cid)?.Assignments.FirstOrDefault(a => a.ID == aid);
    }

    public Submission? GetSubmission(Guid cid, Guid aid, Guid sid) {
        return GetAssignment(cid, aid)?.Submissions.FirstOrDefault(s=>s.StudentID==sid);
    }

    public IEnumerable<Course?> Search(string query) {
        return Courses.Where(c=>(c != null) && c.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase));
    }
}