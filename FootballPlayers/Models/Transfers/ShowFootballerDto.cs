using System;

namespace FootballPlayers.Models.Transfers
{
    public class ShowFootballerDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
