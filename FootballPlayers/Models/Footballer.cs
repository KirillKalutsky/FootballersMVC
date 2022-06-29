using System;

namespace FootballPlayers.Models
{
    public class Footballer:Person
    {
        public Footballer() : base() { }
        public Footballer(Guid id) : base(id) { }
        public Team<Footballer> Team{ get;set; }
        public Country Country { get;set; }
        public Guid TeamId { get;set; }
    }
}
