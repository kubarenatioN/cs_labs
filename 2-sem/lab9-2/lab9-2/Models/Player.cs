namespace lab9
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int? TeamId { get; set; }

        public Team Team { get; set; }
    }
}
