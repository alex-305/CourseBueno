using CourseBueno.Maui.ViewModels;
using CourseBueno.Models;

namespace CourseBueno.Maui.Views;

public partial class PersonSelectionView : ContentPage {
	public PersonSelectionView(bool isStudent, Guid? _id = null, bool isAdd=true) {
		InitializeComponent();
		BindingContext = new PersonSelectionViewModel(isStudent, _id, isAdd);
	}

	public void SubmitClicked(object sender, EventArgs e) {
		(BindingContext as PersonSelectionViewModel)!.SubmitClicked();
	}

	public void CreateClicked(object sender, EventArgs e) {
		(BindingContext as PersonSelectionViewModel)!.CreateClicked();
	}
	public void OnMount(object sender, EventArgs e) {
	(BindingContext as PersonSelectionViewModel)!.Refresh();
	}
	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.Navigation.PopAsync();
	}
}