using System;
using System.Collections.Generic;

namespace FootballPlayers.Models
{
    public class Team<T>:IdModel
    {
        public Team() :base() 
        {
            Players = new List<T>();
        }

        public string Name { get; set; }
        public IEnumerable<T> Players { get; set; }
    }
}
