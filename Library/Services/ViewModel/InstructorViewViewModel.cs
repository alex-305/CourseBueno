using CourseBueno.Models;
using CourseBueno.Library.Services;
using System.Collections.ObjectModel;
using CourseBueno.Maui.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CourseBueno.Maui.ViewModels;
public class InstructorViewViewModel : INotifyPropertyChanged {
    private readonly Guid ID;
    private readonly bool IsAdmin;
    public InstructorViewViewModel(Guid id, bool isAdmin=false) {
        query = string.Empty;
        ID = id;
        IsAdmin = isAdmin;
        LoadProfessor();
    }
    private Faculty? faculty;
    private string query;
    private Student? selectedStudent;
    private Course? selectedCourse;

    private void LoadProfessor() {
        faculty = FacultyService.Current.Get(ID);
    }
    public string Query {
        get {
            return query;
        } set {
            query = value;
            NotifyPropertyChanged(nameof(Student));
        }
    }

    public Student? SelectedStudent { get{
        return selectedStudent;
    } set {
        selectedStudent = value;
    } }
    public Course? SelectedCourse { get {
        return selectedCourse;
    } set {
        selectedCourse = value;
    } }

    public ObservableCollection<Student> Students {

        get {
            var studentList = StudentService.Current.Students.Where(
                s=> s!=null && s.Name.ToLower().Contains(Query.ToLower(), StringComparison.CurrentCultureIgnoreCase)
            );
            return new ObservableCollection<Student>(studentList);
        }
    }

    public ObservableCollection<Course> Courses {
        get {
            var courseList = CourseService.Current.Courses.Where(
                c => c!=null && faculty!=null && c.Professor != null && c.Professor.ID==faculty.ID
                && c.Name.ToLower().Contains(Query.ToLower(), StringComparison.CurrentCultureIgnoreCase)
            );
            return new ObservableCollection<Course>(courseList);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName="") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddCourseClicked() {
        Shell.Current.Navigation.PushAsync(new CourseInfoView(Guid.Empty, faculty));
    }
    public void RemoveCourseClicked() {
        if(selectedCourse==null) return;
        CourseService.Current.Remove(selectedCourse);
        selectedCourse = null;
        Refresh();
    }
    public void EditCourseClicked() {
        if(selectedCourse==null) { return; }
        Shell.Current.Navigation.PushAsync(new CourseInfoView(selectedCourse.ID, faculty));
    }
    public void AddStudentClicked() {
        Shell.Current.Navigation.PushAsync(new PersonInfoView(Guid.Empty, true));
    }
    public void RemoveStudentClicked() {
        if(selectedStudent==null) return;
        StudentService.Current.Remove(selectedStudent);
        selectedStudent = null;
        Refresh();
        
    }
    public void EditStudentClicked() {
        if(selectedStudent == null) { return; }
        var id = selectedStudent.ID;
        Shell.Current.Navigation.PushAsync(new PersonInfoView(id, true));
    }

    public void Refresh() {
        NotifyPropertyChanged(nameof(Students));
        NotifyPropertyChanged(nameof(Courses));
    }
    public void Reset() {
        Query = string.Empty;
        NotifyPropertyChanged(nameof(Query));
    }
}