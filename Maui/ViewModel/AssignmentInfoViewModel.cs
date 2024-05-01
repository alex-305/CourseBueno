using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CourseBueno.Library.Services;
using CourseBueno.Maui.Views;
using CourseBueno.Models;
namespace CourseBueno.Maui.ViewModels;

public class AssignmentInfoViewModel : INotifyPropertyChanged {

    public readonly Guid CourseID;
    public readonly Guid AssignmentID;
    private DateTime dueDate;
    private string name;
    private string description;
    private string totalPoints;
    private Submission? selectedSubmission;
    public readonly bool IsExistingAssignment;
    public AssignmentInfoViewModel(Guid _courseID,Guid _assignmentID) {
        CourseID = _courseID;
        AssignmentID = _assignmentID;
        IsExistingAssignment = LoadAssignment(CourseID, AssignmentID);
        name??=string.Empty;
        description??=string.Empty;
        totalPoints??=string.Empty;
        DueDate = DueDate == DateTime.MinValue ? DateTime.Now : DueDate;
    }

    public bool LoadAssignment(Guid cid, Guid aid) {
        if(CourseID==Guid.Empty || AssignmentID==Guid.Empty) return false;
        var cs = CourseService.Current;
        if(cs.GetAssignment(cid, aid) is Assignment assignment) {
            Name = assignment.Name;
            Description = assignment.Description;
            TotalPoints = assignment.TotalAvailablePoints.ToString();
            DueDate = assignment.DueDate;
            return true;
        }
    return false;
    }

    public void AddorUpdate() {
        if(IsExistingAssignment) {
            CourseService.Current.UpdateAssignment(CourseID, AssignmentID, name, description);
        } else {
            CourseService.Current.Add(CourseID, new Assignment{
                Name=name, Description=description, TotalAvailablePoints = int.TryParse(totalPoints, out int r) ? r : 0, DueDate = DueDate
            });
        }
    }
    public void ViewSubmission() {
        if(SelectedSubmission==null) return;
        Shell.Current.Navigation.PushAsync(new SubmissionInfoView(CourseID,AssignmentID, SelectedSubmission.StudentID));
    }

    public string TotalPoints {
        get { return totalPoints; }
        set { totalPoints = value; }
    }

    public Submission? SelectedSubmission {
        get { return selectedSubmission; }
        set { selectedSubmission = value; }
    }

    public string Name {
        get { return name; }
        set { name = value; }
    }
    public DateTime DueDate {
        get { return dueDate; }
        set { dueDate = value; }
    }
    public string Description {
        get { return description; }
        set { description = value; }
    }

    public ObservableCollection<Submission> Submissions {
        get { 
            var r = CourseService.Current.GetAssignment(CourseID, AssignmentID)?.Submissions ?? [];
            return new ObservableCollection<Submission>(r); }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public void Refresh() {
        NotifyPropertyChanged(nameof(DueDate));
        NotifyPropertyChanged(nameof(TotalPoints));
        NotifyPropertyChanged(nameof(Description));
        NotifyPropertyChanged(nameof(Name));
    }
}
