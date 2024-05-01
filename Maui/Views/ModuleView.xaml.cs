using CourseBueno.Maui.ViewModels;

namespace CourseBueno.Maui.Views;

public partial class ModuleView : ContentPage
{
	public ModuleView(Guid cid, Guid mid) {
		InitializeComponent();
		BindingContext = new ModuleViewViewModel(cid, mid);
	}
	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.Navigation.PopAsync();
	}
}