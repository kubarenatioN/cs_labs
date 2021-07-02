using System.Collections.Generic;

namespace lab10
{
    public class Group
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int CourseId { get; set; }
        public int SpecId { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual Course Course { get; set; }
        public virtual Spec Spec { get; set; }

    }
}
