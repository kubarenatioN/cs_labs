namespace lab10
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double AvgGrade { get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}
