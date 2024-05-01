using System.Collections.ObjectModel;
using CourseBueno.Library.Services;
using CourseBueno.Maui.Views;
using CourseBueno.Models;

namespace CourseBueno.Maui.ViewModels;

public class CourseViewViewModel {

    public string? Name;
    public string? Description;
    public Faculty? Professor;
    private Assignment? selectedAssignment;
    private Module? selectedModule;
    public string? ProfessorName {
        get { return "Professor: " + Professor?.Name ?? string.Empty; }
    }
    public readonly Guid CourseID;
    public readonly Guid StudentID;
    private string query;

    public CourseViewViewModel(Guid cid, Guid? sid) {
        CourseID = cid;
        StudentID = sid ?? Guid.Empty;
        LoadCourse(CourseID);
        query = string.Empty;
    }

    private bool LoadCourse(Guid id) {
        if(CourseService.Current.Get(id) is Course course) {
            Name = course.Name;
            Description = course.Description;
            Professor = course.Professor;
            return true;
        }
        return false;
    }

    public ObservableCollection<Module> Modules {
        get {
            var moduleList = CourseService.Current.Get(CourseID)?.Modules.Where(
                m => m!=null && m.Name != null
                && m.Name.ToLower().Contains(Query.ToLower(), StringComparison.CurrentCultureIgnoreCase)
            ) ?? [];

            return new ObservableCollection<Module>(moduleList);
        }
    }
    public ObservableCollection<Assignment> Assignments {
        get {
            var assignmentList = CourseService.Current.Get(CourseID)?.Assignments.Where(
                m => m!=null && m.Name != null
                && m.Name.ToLower().Contains(Query.ToLower(), StringComparison.CurrentCultureIgnoreCase)
            ) ?? [];

            return new ObservableCollection<Assignment>(assignmentList);
        }
    }

    public void ViewAssignment() {
        if(SelectedAssignment == null) return;
        Shell.Current.Navigation.PushAsync(new AssignmentView(CourseID,SelectedAssignment.ID, StudentID));
    }

    public void ViewModule() {
        if(SelectedModule == null) return;
        Shell.Current.Navigation.PushAsync(new ModuleView(CourseID, SelectedModule.ID));
    }

    public Assignment? SelectedAssignment {
        get { return selectedAssignment; }
        set { selectedAssignment = value; }
    }
    public Module? SelectedModule {
        get { return selectedModule; }
        set { selectedModule = value; }
    }

    public string Query {
        get { return query; }
        set { query = value; }
    }

}
