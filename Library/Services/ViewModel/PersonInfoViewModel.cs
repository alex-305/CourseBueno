
using CourseBueno.Library.Services;
using CourseBueno.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CourseBueno.Maui.ViewModels;

public class PersonInfoViewModel : INotifyPropertyChanged {

    private string name;
    private string classification;
    private bool isStudent;
    private bool isFaculty;
    private Guid id;

    public PersonInfoViewModel(Guid id, bool studentOrNot) {
        IsStudent = studentOrNot;
        if(IsStudent) LoadStudent(id);
        else LoadProfessor(id);
        name??=string.Empty;
        classification??=string.Empty;
    }
    private void LoadProfessor(Guid id) {
        if(id==Guid.Empty) return;
        if(FacultyService.Current.Get(id) is Faculty faculty) {
            Name = faculty.Name;
            Classification = faculty.Classification;
            ID = faculty.ID;
        }
        NotifyPropertyChanged(nameof(name));
        NotifyPropertyChanged(nameof(classification));
    }
    private void LoadStudent(Guid id) {
        if(id==Guid.Empty) return;
        if(IsStudent && StudentService.Current.Get(id) is Student person) {
            Name = person.Name;
            Classification = person.Classification;
            ID = person.ID;
        }
        NotifyPropertyChanged(nameof(name));
        NotifyPropertyChanged(nameof(classification));
    }

    public bool IsStudent {
        get { return isStudent; }
        set { isStudent = value; isFaculty = !value; }
    }
    public bool IsFaculty {
        get { return isFaculty; }
        set { isFaculty = value; isStudent = !value;  }
    }

    public string Name {
        get {
            return name;
        } set {
            name = value;
        }
    }

    public string Classification {
        get {
            return classification;
        } set {
            classification = value;
        }
    }

    public Guid ID {
        get {
            return id;
        } set {
            id = value;
        }
    }

    public void AddorUpdate() {
        if(isStudent) {
            if(ID == Guid.Empty) {
                StudentService.Current.Add(new Student{ Name = Name, Classification = Classification});
            } else if (StudentService.Current.Get(id) is Student student) {
                StudentService.Current.Update(student, Name, Classification);
            }
        } else {
            if(ID == Guid.Empty) {
                FacultyService.Current.AddProfessor(new Faculty{ Name = Name, Classification = Classification });
            } else if(FacultyService.Current.Get(id) is Faculty faculty) {
                FacultyService.Current.Update(faculty, Name, Classification);
            }
        }
        Shell.Current.Navigation.PopAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}