using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CourseBueno.Library.Services;
using CourseBueno.Maui.Views;
using CourseBueno.Models;

namespace CourseBueno.MAUI.ViewModels;
public class StudentViewViewModel : INotifyPropertyChanged {
    private readonly Guid ID;
    private string query;
    private readonly Student? student;
    private Course? selectedCourse;

    public StudentViewViewModel(Guid id) {
        query = string.Empty;
        ID = id;
        student = LoadStudent();
    }

    private Student? LoadStudent() {
        return StudentService.Current.Get(ID);
    }

    public void ViewClicked() {
        if(student==null || SelectedCourse==null) return;
        Shell.Current.Navigation.PushAsync(new CourseView(SelectedCourse.ID, student?.ID ?? Guid.Empty));
    }
    public string Query {
        get { return query; } 
        set { query = value; NotifyPropertyChanged(nameof(Student));}
    }
    public ObservableCollection<Course> Courses {
        get {
            var courseList = student?.Courses.Where(
                c => c!=null 
                && c.Name.ToLower().Contains(Query.ToLower(), StringComparison.CurrentCultureIgnoreCase)
            ) ?? [];
            return new ObservableCollection<Course>(courseList);
        }
    }

    public Course? SelectedCourse {
        get { return selectedCourse; }
        set { selectedCourse = value; }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName="") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}