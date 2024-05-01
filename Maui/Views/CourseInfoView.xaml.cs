using CourseBueno.Maui.ViewModels;
using CourseBueno.Models;

namespace CourseBueno.Maui.Views;
	public partial class CourseInfoView : ContentPage {
	public CourseInfoView(Guid id, Faculty? professor) {
		InitializeComponent();
        BindingContext = new CourseInfoViewModel(id, professor);
	}

    private void SubmitClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.AddorUpdate();
        Shell.Current.Navigation.PopAsync();
    }

    private void AddStudentClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.AddStudent();
    }

    private void RemoveStudentClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.RemoveStudent();
    }

    private void AddModuleClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.AddModule();
    }
    private void EditModuleClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.EditModule();
    }
    private void RemoveModuleClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.RemoveModule();
    }
    private void AddAssignmentClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.AddAssignment();
    }
    private void EditAssignmentClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.EditAssignment();
    }
    private void RemoveAssignmentClicked(object sender, EventArgs e) {
        (BindingContext as CourseInfoViewModel)!.RemoveAssignment();
    }
    private void BackClicked(object sender, EventArgs e) {
        Shell.Current.Navigation.PopAsync();
    }
    public void OnMount(object sender, EventArgs e) {
		(BindingContext as CourseInfoViewModel)!.Refresh();
	}

}