
using CourseBueno.Models;
using CourseBueno.Models.Abstracts;
using CourseBueno.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CourseBueno.Maui.Views;
using System.Collections.ObjectModel;

namespace CourseBueno.Maui.ViewModels;
public class CourseInfoViewModel : INotifyPropertyChanged {

    private Guid id;
    private string name;
    private string description;
    private Faculty? professor;
    private Student? selectedStudent;
    private Module? selectedModule;
    private Assignment? selectedAssignment;
    public readonly bool IsExistingCourse;


    public CourseInfoViewModel(Guid id, Faculty? _professor) {
        IsExistingCourse = LoadByGuid(id);
        name??= string.Empty;
        description??=string.Empty;
        professor??= _professor;
        Refresh();
    }

    private bool LoadByGuid(Guid id) {
        if(id==Guid.Empty) return false;
        if (CourseService.Current.Get(id) is Course course) {
            Name = course.Name;
            Description = course.Description;
            ID = course.ID;
            professor = course.Professor;
        NotifyPropertyChanged(nameof(name));
        NotifyPropertyChanged(nameof(description));
        return true;
        }
        return false;
    }
    public void AddorUpdate() {
        if (IsExistingCourse) {
            CourseService.Current.Update(ID, name, description);
        } else {
            CourseService.Current.Add(new Course{Name=name, Description = description, Professor = professor});
        }
    }

    public void AddStudent() {
        Shell.Current.Navigation.PushAsync(new PersonSelectionView(true, ID, true));
    }

    public void RemoveStudent() {
        if(SelectedStudent == null) return;
        CourseService.Current.RemoveStudent(ID, SelectedStudent.ID);
        SelectedStudent = null;
        Refresh();
    }

    public void AddAssignment() {
        Shell.Current.Navigation.PushAsync(new AssignmentInfoView(ID));
    }
    public void EditAssignment() {
        if(selectedAssignment==null) return;
        Shell.Current.Navigation.PushAsync(new AssignmentInfoView(ID, selectedAssignment.ID));
    }

    public void RemoveAssignment() {
        if(SelectedAssignment == null) return;
        CourseService.Current.RemoveAssignment(ID,SelectedAssignment.ID);
        SelectedAssignment = null;
        Refresh();
    }

    public void AddModule() {
        Shell.Current.Navigation.PushAsync(new ModuleInfoView(ID));
    }

    public void EditModule() {
        if(selectedModule==null) return;
        Shell.Current.Navigation.PushAsync(new ModuleInfoView(ID, selectedModule.ID));
    }

    public void RemoveModule() {
        if(SelectedModule == null) return;
        CourseService.Current.RemoveModule(ID,SelectedModule.ID);
        SelectedModule = null;
        Refresh();
    }

    public Guid ID {
        get { return id; } 
        set { id = value; }
    }
    public Faculty? Professor {
        get { return professor; } 
        set { professor = value; }
    }
    public string Name {
        get { return name; } 
        set { name = value; }
    }

    public string Description {
        get { return description; } 
        set { description = value; }
    }
    public Student? SelectedStudent {
        get { return selectedStudent; }
        set { selectedStudent = value; }
    }
    public Module? SelectedModule {
        get { return selectedModule; }
        set { selectedModule = value; }
    }

    public Assignment? SelectedAssignment {
        get { return selectedAssignment; }
        set { selectedAssignment = value; }
    }
    public ObservableCollection<Student> Roster {
        get { 
            var r = CourseService.Current.Get(ID)?.Roster ?? [];
            return new ObservableCollection<Student>(r); }
    }
    public ObservableCollection<Assignment> Assignments {
        get {
            var a = CourseService.Current.Get(ID)?.Assignments ?? [];
            return new ObservableCollection<Assignment>(a); }
    }
    public ObservableCollection<Module> Modules {
        get { 
            var m = CourseService.Current.Get(ID)?.Modules ?? [];
            return new ObservableCollection<Module>(m); }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Refresh() {
        NotifyPropertyChanged(nameof(Name));
        NotifyPropertyChanged(nameof(Description));
        NotifyPropertyChanged(nameof(Roster));
        NotifyPropertyChanged(nameof(Modules));
        NotifyPropertyChanged(nameof(Assignments));
    }
}