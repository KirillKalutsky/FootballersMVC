using System;
using System.ComponentModel.DataAnnotations;

namespace FootballPlayers.Models
{
    public class IdModel
    {
        [Key]
        public Guid Id { get; private set; }
        public IdModel()
        {
            Id = Guid.NewGuid();
        }

        public IdModel(Guid id)
        {
            Id = id;
        }
    }
}
