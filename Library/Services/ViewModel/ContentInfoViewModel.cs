using System.Security.Cryptography;
using CourseBueno.Library.Services;
using CourseBueno.Models;

namespace CourseBueno.Maui.ViewModels;

public class ContentInfoViewModel {
    private readonly Guid CourseID;
    private readonly Guid ModuleID;
    private readonly Guid ContentID;
    private string name;
    private string description;
    public readonly bool IsExistingItem;
    public ContentInfoViewModel(Guid courseID, Guid moduleID, Guid? contentID) {
        CourseID = courseID;
        ModuleID = moduleID;
        ContentID = contentID ?? Guid.Empty;
        IsExistingItem = LoadContentItem(CourseID, ModuleID, ContentID);
        name??=string.Empty;
        description??=string.Empty;
    }
    public bool LoadContentItem(Guid cid, Guid mid, Guid ciid) {
		if(CourseID==Guid.Empty || ModuleID==Guid.Empty || ContentID==Guid.Empty) return false;
        var cs = CourseService.Current;
        if(cs.GetContentItem(cid,mid, ciid) is ContentItem contentItem) {
            Name = contentItem.Name;
            Description = contentItem.Description;
            return true;
        }
        return false;
	}

    public void AddorUpdate() {
        if(IsExistingItem) {
            CourseService.Current.UpdateContentItem(CourseID, ModuleID, ContentID);
        } else {
            CourseService.Current.Add(CourseID, ModuleID, new ContentItem { Name=name, Description=description } );
        }
    }

    public string Name  {
        get { return name; }
        set { name = value; }
    }
    public string Description {
        get { return description; }
        set { description = value; }
    }
}
