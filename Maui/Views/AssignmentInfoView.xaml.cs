using CourseBueno.Maui.ViewModels;
using CourseBueno.Models;
namespace CourseBueno.Maui.Views;

public partial class AssignmentInfoView : ContentPage {
	public AssignmentInfoView(Guid courseID, Guid? assignmentID=null) {
		InitializeComponent();
		BindingContext = new AssignmentInfoViewModel(courseID,assignmentID ?? Guid.Empty);
	}

	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.Navigation.PopAsync();
	}
	
	public void SaveClicked(object sender, EventArgs e) {
		(BindingContext as AssignmentInfoViewModel)!.AddorUpdate();
		Shell.Current.Navigation.PopAsync();
	}
	public void ViewClicked(object sender, EventArgs e) {
		(BindingContext as AssignmentInfoViewModel)!.ViewSubmission();
	}

	public void HandleNumericText(object sender, TextChangedEventArgs e) {
		string text = new(e.NewTextValue.Where(char.IsDigit).ToArray());
		if(text!=e.NewTextValue) ((Entry)sender).Text = text;
	}
}