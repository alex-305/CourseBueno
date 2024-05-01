using CourseBueno.Maui.ViewModels;

namespace CourseBueno.Maui.Views;

public partial class InstructorView : ContentPage {
	public InstructorView(Guid id, bool isAdmin = false) {
		InitializeComponent();
		BindingContext = new InstructorViewViewModel(id, isAdmin);
	}

    public void BackClicked(object sender, EventArgs e) {
		if(Navigation!=null && Navigation.NavigationStack.Count > 1) {
			Navigation.PopAsync();
		}
    }

	public void OnMount(object sender, EventArgs e) {
		(BindingContext as InstructorViewViewModel)!.Refresh();
	}

	/*Courses*/
	public void AddCourse(object sender, EventArgs e) {
		(BindingContext as InstructorViewViewModel)!.AddCourseClicked();
	}
	public void RemoveCourse(object sender, EventArgs e) {
		(BindingContext as InstructorViewViewModel)!.RemoveCourseClicked();
	}
	public void EditCourse(object sender, EventArgs e) {
		(BindingContext as InstructorViewViewModel)!.EditCourseClicked();
	}
	/*Students*/
	public void AddStudent(object sender, EventArgs e) {
		(BindingContext as InstructorViewViewModel)!.AddStudentClicked();
	}
	public void RemoveStudent(object sender, EventArgs e) {
		(BindingContext as InstructorViewViewModel)!.RemoveStudentClicked();
	}
	public void EditStudent(object sender, EventArgs e) {
		(BindingContext as InstructorViewViewModel)!.EditStudentClicked();
	}

    

}

