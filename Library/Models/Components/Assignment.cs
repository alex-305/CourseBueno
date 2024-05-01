using CourseBueno.Models.Abstracts;
namespace CourseBueno.Models {
    public class Assignment : Component {
        private int totalAvailablePoints;
        private DateTime dueDate;
        public List<Submission> Submissions;
        public required DateTime DueDate { 
            get { return dueDate;} 
            set { dueDate = value > DateTime.Now ? value : DateTime.Now; } 
        }
        public required int TotalAvailablePoints { 
            get { return totalAvailablePoints; }
            set { totalAvailablePoints = value > 0 ? value : 0; }
        }

        public override string ToString() {
            return base.ToString() + $"\nDue Date: {dueDate}";
        }

        public Assignment() {
            Submissions = [];
        }
    }
}