using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CourseBueno.Library.Services;
using CourseBueno.Models;
using CourseBueno.Maui.Views;
namespace CourseBueno.Maui.ViewModels;

public class ModuleInfoViewModel : INotifyPropertyChanged {

    public readonly Guid CourseID;
    public readonly Guid ModuleID;
    public readonly bool IsExistingModule;
    private string name;
    private string description;
    private ContentItem? selectedItem;
    public ModuleInfoViewModel(Guid _courseID, Guid _moduleID) {
        CourseID = _courseID;
        ModuleID = _moduleID;
        IsExistingModule = LoadModule(CourseID, ModuleID);
        name??=string.Empty;
        description??=string.Empty;
    }

    private bool LoadModule(Guid cid, Guid mid) {
        if(CourseID==Guid.Empty || ModuleID==Guid.Empty) return false;
        var cs = CourseService.Current;
        if(cs.GetModule(cid, mid) is Module module) {
            name = module.Name;
            description = module.Description;
            return true;
        }
        return false;
    }
    public void AddItem() {
        Shell.Current.Navigation.PushAsync(new ContentInfoView(CourseID, ModuleID));
        SelectedItem=null;

    }
    public void EditItem() {
        if(SelectedItem==null) return;
        Shell.Current.Navigation.PushAsync(new ContentInfoView(CourseID, ModuleID, SelectedItem.ID));
        SelectedItem = null;
    }
    public void RemoveItem() {
        if(SelectedItem == null) return;
        CourseService.Current.RemoveItem(CourseID, ModuleID, SelectedItem.ID);
        SelectedItem = null;
        Refresh();
    }

    public void AddorUpdate() {
        if(IsExistingModule) {
            CourseService.Current.UpdateModule(CourseID, ModuleID, name, description);
        } else {
            CourseService.Current.Add(CourseID, new Module{Name=name, Description=description});
        }
    }

    public ObservableCollection<ContentItem> Contents {
        get {
            var i = CourseService.Current.GetModule(CourseID, ModuleID)?.Contents ?? [];
            return new ObservableCollection<ContentItem>(i);
        }
    }

    public string Name {
        get { return name; }
        set { name=value; }
    }
    public string Description {
        get { return description; }
        set { description=value; }
    }
    public ContentItem? SelectedItem {
        get { return selectedItem; }
        set { selectedItem = value; }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Refresh() {
        NotifyPropertyChanged(nameof(Contents));
        NotifyPropertyChanged(nameof(Description));
        NotifyPropertyChanged(nameof(Name));
    }
}
