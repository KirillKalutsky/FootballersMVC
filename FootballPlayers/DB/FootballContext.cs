using Microsoft.EntityFrameworkCore;
using FootballPlayers.Models;

namespace FootballPlayers.DB
{
    public class FootballContext : DbContext
    {
        public DbSet<Footballer> Footballers{get;set;}
        public DbSet<Team<Footballer>> Teams{get;set;}
        public FootballContext(DbContextOptions<FootballContext> context)
            : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Footballer>()
                .HasOne(footballer => footballer.Team)
                .WithMany(team => team.Players)
                .HasForeignKey(footballer => footballer.TeamId);
        }
    }
}
