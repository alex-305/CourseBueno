using CourseBueno.Library.Services;
using CourseBueno.Models;

namespace CourseBueno.Maui.ViewModels;

public class AssignmentViewViewModel {
    public readonly Guid CourseID;
    public readonly Guid AssignmentID;
    public readonly Guid StudentID;
    private string name;
    private string description;
    public bool IsGraded;
    private string grade;
    private string submissionText;
    public AssignmentViewViewModel(Guid cid, Guid aid, Guid sid) {
        CourseID = cid;
        AssignmentID = aid;
        StudentID = sid;
        LoadAssignment();
        LoadSubmission();
        submissionText = string.Empty;
        grade??="Grade: N/A";
        name??=string.Empty;
        description??=string.Empty;
        

    }
    public void LoadAssignment() {
        if(CourseService.Current.GetAssignment(CourseID, AssignmentID) is Assignment a) {
            name = a.Name;
            description = a.Description;
        }
    }
    public void LoadSubmission() {
        if(CourseService.Current.GetSubmission(CourseID, AssignmentID, StudentID) is Submission s) {
            SubmissionText = s.Text;
            Grade = s.Grade.ToString();
            IsGraded = s.IsGraded;
        }
    }

    public void SubmitClicked() {
        if(submissionText==string.Empty) return;
        if(CourseService.Current.GetSubmission(CourseID, AssignmentID, StudentID) == null) {
            CourseService.Current.Add(CourseID, AssignmentID, new Submission(StudentID, SubmissionText));
        } else {
            CourseService.Current.UpdateSubmission(CourseID, AssignmentID, StudentID, submissionText);
        }
        Shell.Current.Navigation.PopAsync();
    }

    public string SubmissionText {
        get { return submissionText; }
        set { submissionText = value ?? string.Empty; }
    }
    public string Assignment {
        get { return name + ": " + description; }
    }
    public string Grade {
        get { return grade; }
        set { grade = "Grade: " + value; }
    }
}
