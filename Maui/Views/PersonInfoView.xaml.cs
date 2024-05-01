using CourseBueno.Maui.ViewModels;
namespace CourseBueno.Maui.Views;

public partial class PersonInfoView : ContentPage {
    private Guid id;
    private readonly bool isStudent;
    public PersonInfoView(Guid _id, bool is_student) {
        InitializeComponent();
        ID = _id;
        isStudent = is_student;
    }

    public Guid ID {
        get { return id; } 
        set { id = value; }
    }

    private void SubmitClicked(object sender, EventArgs e) {
        (BindingContext as PersonInfoViewModel)!.AddorUpdate();
    }

    private void CancelClicked(object sender, EventArgs e) {
        Shell.Current.Navigation.PopAsync();
    }

    private void OnMount(object sender, EventArgs e) {
        BindingContext = new PersonInfoViewModel(ID, isStudent);
    }

    private void OnUnMount(object sender, NavigatedFromEventArgs e) {
        BindingContext = null;
    }

}
