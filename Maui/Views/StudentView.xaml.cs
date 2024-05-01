using CourseBueno.MAUI.ViewModels;

namespace CourseBueno.Maui.Views;

public partial class StudentView : ContentPage {

    public StudentView(Guid id) {
        InitializeComponent();
        BindingContext = new StudentViewViewModel(id);
    }

    public void BackClicked(object sender, EventArgs e) {
        Shell.Current.Navigation.PopAsync();;
    }
    public void ViewClicked(object sender, EventArgs e) {
        (BindingContext as StudentViewViewModel)!.ViewClicked();
    }
}

