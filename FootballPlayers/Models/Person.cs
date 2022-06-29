using System;

namespace FootballPlayers.Models
{
    public class Person: IdModel
    {
        public Person() : base() { }
        public Person(Guid id) : base(id) { }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
