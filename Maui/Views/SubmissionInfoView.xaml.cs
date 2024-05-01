using CourseBueno.Maui.ViewModels;

namespace CourseBueno.Maui.Views;
public partial class SubmissionInfoView : ContentPage {
	public SubmissionInfoView(Guid cid, Guid aid, Guid studentID) {
		InitializeComponent();
		BindingContext = new SubmissionInfoViewModel(cid, aid, studentID);
	}
	public void SubmitClicked(object sender, EventArgs e) {
		(BindingContext as SubmissionInfoViewModel)!.SubmitClicked();
	}
	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.Navigation.PopAsync();
	}
	public void HandleNumericText(object sender, TextChangedEventArgs e) {
		string text = new(e.NewTextValue.Where(char.IsDigit).ToArray());
		if(text!=e.NewTextValue) ((Entry)sender).Text = text;
	}
}