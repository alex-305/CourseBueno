using CourseBueno.Maui.Views;

namespace CourseBueno.Maui;

public partial class MainPage : ContentPage
{

	public MainPage() {
		InitializeComponent();
	}

	public void StudentClicked(object sender, EventArgs e) {
		Navigation.PushAsync(new PersonSelectionView(true));
	}

	public void InstructorClicked(object sender, EventArgs e) {
		Navigation.PushAsync(new PersonSelectionView(false));
	}

}

