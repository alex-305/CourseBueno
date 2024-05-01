using CourseBueno.Maui.ViewModels;

namespace CourseBueno.Maui.Views;

public partial class CourseView : ContentPage {
	public CourseView(Guid cid, Guid? sid) {
		InitializeComponent();
		BindingContext = new CourseViewViewModel(cid, sid);
	}

	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.Navigation.PopAsync();
	}

	public void ViewAssignment(object sender, EventArgs e) {
		(BindingContext as CourseViewViewModel)!.ViewAssignment();
	}
	public void ViewModule(object sender, EventArgs e) {
		(BindingContext as CourseViewViewModel)!.ViewModule();
	}
	public void OnMount(object sender, EventArgs e) {
		(BindingContext as InstructorViewViewModel)!.Refresh();
	}
}