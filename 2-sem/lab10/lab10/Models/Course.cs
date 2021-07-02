using System.Collections.Generic;

namespace lab10
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseNumber { get; set; }
        public virtual ICollection<Spec> Specs { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public Course()
        {
            Specs = new List<Spec>();
            Groups = new List<Group>();
        }
    }
}
