using System.Collections.Generic;

namespace lab9
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }

        public ICollection<Player> Players { get; set; }
        public Team()
        {
            Players = new List<Player>();
        }
    }
}
