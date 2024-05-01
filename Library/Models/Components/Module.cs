using CourseBueno.Models.Abstracts;
namespace CourseBueno.Models {
    public class Module : Component {
        public readonly List<ContentItem> Contents;

        public Module() {
            Contents = [];
        }

        public override string ToString()
        {
            string returnString = base.ToString();
            return returnString;
        }

    }
}