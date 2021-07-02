using System.Collections.Generic;

namespace lab10
{
    public class Spec
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public Spec()
        {
            Courses = new List<Course>();
            Groups = new List<Group>();
        }
    }
}
