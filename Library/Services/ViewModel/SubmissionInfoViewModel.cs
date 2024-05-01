using CourseBueno.Library.Services;
using CourseBueno.Models;

namespace CourseBueno.Maui.ViewModels;

public class SubmissionInfoViewModel {
    private string text;
    private string name;
    private string grade;
    public readonly Guid CourseID;
    public readonly Guid AssignmentID;
    public readonly Guid StudentID;
    public SubmissionInfoViewModel(Guid cid, Guid aid, Guid sid) {
        CourseID = cid;
        AssignmentID = aid;
        StudentID = sid;
        LoadSubmission();
        text??=string.Empty;
        name??=string.Empty;
        grade??=string.Empty;
    }
    public void LoadSubmission() {
        if(CourseID == Guid.Empty || AssignmentID == Guid.Empty) return;
        if(CourseService.Current.GetSubmission(CourseID, AssignmentID,StudentID) is Submission s) {
            Text = s.Text;
            Name = s.Name;
            Grade = s.Grade.ToString();
            return;
        }
        return;
    }

    public void SubmitClicked() {
        if(Grade==string.Empty) return;
        if(int.TryParse(Grade, out int _grade)
        && CourseService.Current.GetSubmission(CourseID, AssignmentID, StudentID) is Submission submission) {
            submission.Grade = _grade;
            submission.IsGraded = true;
        }
        Shell.Current.Navigation.PopAsync();
    }

    public string Grade {
        get { return grade; }
        set { grade = value; }
    }
    public string Text {
        get { return text; }
        set { text = value; }
    }
    public string Name {
        get { return name; }
        set { name = value; }
    }

}
