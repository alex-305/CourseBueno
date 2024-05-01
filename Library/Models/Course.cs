
namespace CourseBueno.Models {
    public class Course {
        public Guid ID { get; }
        private string name;
        private string description;
        public List<Student> Roster;
        public List<Assignment> Assignments;
        public List<Module> Modules;
        public Faculty? Professor;
        public string Name { 
            get { return name; } 
            set { name = value; } 
        }
        public string Description { 
            get { return description; } 
            set { description = value; } 
        }

        public override string ToString() {
            return $"{Name}: {Description}";
        }
        public Course() {
            ID = Guid.NewGuid();
            Roster ??= [];
            Assignments ??= [];
            Modules ??= [];
            name??= string.Empty;
            description??=string.Empty;
        }
    }
    }