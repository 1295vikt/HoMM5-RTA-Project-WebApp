using Microsoft.AspNet.Identity.EntityFramework;
using RTA_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL
{
    public class RTADatabaseContext : IdentityDbContext
    {
        public RTADatabaseContext() : base(@"Data Source = .\SQLEXPRESS;integrated security = true; initial catalog = RTA")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Faction> Factions { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentGroup> TournamentGroups { get; set; }
        public DbSet<TournamentPlayer> TournamentPlayers { get; set; }

    }
}
