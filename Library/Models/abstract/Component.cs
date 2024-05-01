namespace CourseBueno.Models.Abstracts;
abstract public class Component {
    public required string Name { get; set; }
    public required string Description { get; set; }
    public readonly Guid ID;

    public Component() {
        ID = Guid.NewGuid();
        Name??=string.Empty;
        Description??=string.Empty;
    }

    public override string ToString()
    {
        return $"{Name} | {Description}";
    }
}