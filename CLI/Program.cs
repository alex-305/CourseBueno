namespace CourseBueno {
    internal class Program {
        public static Faculty? admin;
        public static List<Student> students = [];
        public static List<Faculty> professors = [];
        public static List<Course> courses = [];
        public static void Main() {
            admin = Initializers.Admin();
            while(CourseHelpers.AdminMenu());
        }
    }
}