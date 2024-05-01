using CourseBueno.Maui.ViewModels;

namespace CourseBueno.Maui.Views;

public partial class ContentInfoView : ContentPage {

	public ContentInfoView(Guid CourseID, Guid ModuleID, Guid? ContentID=null) {
		InitializeComponent();
		BindingContext = new ContentInfoViewModel(CourseID, ModuleID, ContentID);
	}

	public void SaveClicked(object sender, EventArgs e) {
		(BindingContext as ContentInfoViewModel)!.AddorUpdate();
		Shell.Current.Navigation.PopAsync();
	}

	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.Navigation.PopAsync();
	}


}