using CourseBueno.Maui.ViewModels;

namespace CourseBueno.Maui.Views;

public partial class ModuleInfoView : ContentPage {
	public ModuleInfoView(Guid cid, Guid? mid=null) {
		InitializeComponent();
		BindingContext = new ModuleInfoViewModel(cid, mid ?? Guid.Empty);
	} 
	public void AddItem(object sender, EventArgs e) {
		(BindingContext as ModuleInfoViewModel)!.AddItem();
	} 
	public void EditItem(object sender, EventArgs e) {
		(BindingContext as ModuleInfoViewModel)!.EditItem();
	} 
	public void RemoveItem(object sender, EventArgs e) {
		(BindingContext as ModuleInfoViewModel)!.RemoveItem();
	} 
	public void SaveClicked(object sender, EventArgs e) {
		(BindingContext as ModuleInfoViewModel)!.AddorUpdate();
		Shell.Current.Navigation.PopAsync();
	}
	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.Navigation.PopAsync();
	}
	public void OnMount(object sender, EventArgs e) {
		(BindingContext as ModuleInfoViewModel)!.Refresh();
	}
}