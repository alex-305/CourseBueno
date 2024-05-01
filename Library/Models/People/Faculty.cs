
using System.Data;
using CourseBueno.Models.Abstracts;
using CourseBueno.Models;

namespace CourseBueno.Models {
    public class Faculty : Person {


        public Faculty() {
        }
        
        public void RemoveCourse(Course course) {
            try {
                if(!(Courses?.Remove(course) ?? false)) {
                    throw new DataException($"Could not remove: '{course}',  does not exist in list of courses.");
                }
            } catch (DataException de) {
                Console.WriteLine(de);
            }
        }

        public override string ToString() {
            return $"{base.ToString()}({Classification})";
        }

    }
}