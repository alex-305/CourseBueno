using CourseBueno.Maui.ViewModels;

namespace CourseBueno.Maui.Views;

public partial class AssignmentView : ContentPage {
	public AssignmentView(Guid cid, Guid aid, Guid sid) {
		InitializeComponent();
		BindingContext = new AssignmentViewViewModel(cid, aid, sid);
	}
	public void SubmitClicked(object sender, EventArgs e) {
		(BindingContext as AssignmentViewViewModel)!.SubmitClicked();
	}
	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.Navigation.PopAsync();
	}
}