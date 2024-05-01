using CourseBueno.Models.Abstracts;
using CourseBueno.Models;
using CourseBueno.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CourseBueno.Maui.Views;

namespace CourseBueno.Maui.ViewModels;

public class PersonSelectionViewModel : INotifyPropertyChanged {
    private string query;
    private Person? selectedPerson;
    public readonly bool IsStudent;
    public readonly bool IsAdd;
    public readonly Guid ID;

    public PersonSelectionViewModel(bool _isStudent, Guid? _guid = null, bool _isAdd=true) {
        IsStudent = _isStudent;
        query = string.Empty;
        ID = _guid ?? Guid.Empty;
        IsAdd = _isAdd;

    }

    public void SubmitClicked() {
        if(selectedPerson==null) return;
        var n = Shell.Current.Navigation;

        if(IsStudent) {
            if(ID != Guid.Empty && CourseService.Current.Get(ID) is Course) {
                CourseService.Current.Add(ID, selectedPerson);
                n.PopAsync();
            } else {
                n.PushAsync(new StudentView(selectedPerson.ID));
            }
        } else {
            if(selectedPerson.Classification!="Administrator") {
                n.PushAsync(new InstructorView(selectedPerson.ID));
            } else {
                n.PushAsync(new InstructorView(selectedPerson.ID, true));
            }
        }
    }
    
    public void CreateClicked() {
        Shell.Current.Navigation.PushAsync(new PersonInfoView(Guid.Empty, IsStudent));
    }

    public string Query {
        get {
            return query;
        } set {
            query = value;
            NotifyPropertyChanged(nameof(People));
        }
    }

    public ObservableCollection<Person> People {
        get {
            List<Person> peopleList;
            if(IsStudent) {
                peopleList = StudentService.Current.Students.Cast<Person>().ToList() ?? [];;
            } else {
                peopleList = FacultyService.Current.Faculty.Cast<Person>().ToList() ?? [];
            }
            peopleList = peopleList.Where(c => c!=null && c.Name.Contains(Query?.ToUpper()??string.Empty, StringComparison.CurrentCultureIgnoreCase)).ToList() ?? [];
            return new ObservableCollection<Person>(peopleList);
        }
    }

    public void Refresh() {
        NotifyPropertyChanged(nameof(People));
    }
    public Person? SelectedPerson {
        get { return selectedPerson; }
        set { selectedPerson = value; }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
