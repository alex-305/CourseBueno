namespace CourseBueno.Models.Abstracts;
abstract public class Person {

    public readonly Guid ID;

    private string classification;
    private string name;


    public string Classification {
        get {
            return classification;
        } set {
            classification = value;
        }
    }
    public required string Name { 
        get {
            return name;
        } set {
            name = Char.ToUpper(value[0]) + value[1..];
        } 
    }


    public readonly List<Course> Courses;

    public override string ToString() {
        return $"{Name}";
    }

    public Person() {
        Courses = [];
        ID = Guid.NewGuid();
        name??=string.Empty;
        classification??=string.Empty;
    }
}