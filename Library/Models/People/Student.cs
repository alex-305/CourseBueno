using CourseBueno.Models.Abstracts;
namespace CourseBueno.Models {
    public class Student : Person {
        public override string ToString() {
            return $"{base.ToString()}({Classification})";
        }
        public Student() { }
    }
}