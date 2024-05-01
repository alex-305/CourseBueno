using CourseBueno.Library.Services;

namespace CourseBueno.Models {
    public class Submission {
        public Guid StudentID;
        private string name;
        private string text;
        private int grade;
        private bool isGraded;

        public override string ToString() { return Name; }
        public Submission(Guid studentID, string t) {
            text= t ?? string.Empty;
            StudentID = studentID;
            IsGraded = false;
            name = StudentService.Current.Get(StudentID)?.Name ?? "N/A";
        }
        public bool IsGraded {
            get { return isGraded; }
            set { isGraded = value; }
        }
        public string Name {
            get { return name; }
            set { name =  value; }
        }
        public string Text {
            get { return text; }
            set { text = value;}
        }
        public int Grade {
            get { return grade; }
            set { grade = value; }
        }


    }
}