using System.Collections.ObjectModel;
using CourseBueno.Library.Services;
using CourseBueno.Models;

namespace CourseBueno.Maui.ViewModels;

public class ModuleViewViewModel {
    public Guid CourseID;
    public Guid ModuleID;
    public ModuleViewViewModel(Guid cid, Guid aid) {
        CourseID = cid;
        ModuleID = aid;
    }

    public string Title {
        get { return CourseService.Current.GetModule(CourseID, ModuleID)?.ToString() ?? string.Empty; }
    }

    public ObservableCollection<ContentItem> Contents {
        get { 
            var c = CourseService.Current.GetModule(CourseID, ModuleID)?.Contents ?? [];
            return new ObservableCollection<ContentItem>(c);
        }
    }

}
