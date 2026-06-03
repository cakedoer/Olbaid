using Microsoft.EntityFrameworkCore;
using Olbaid.Models.Archetypes;
using Olbaid.Models.Creatures;

namespace Olbaid.Models;

public class GameContext : DbContext
{
    public DbSet<Archetype> Archetypes { get; set; }
    public DbSet<Creature>  Creatures  { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=game.db");
    
    // temporary db seed method
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Warrior>().HasData(new Warrior { Name = "Warrior",
            Id = 1, Strength = 3, Dexterity = 3, Intelligence = 0, BaseRange = 1 });
        modelBuilder.Entity<Rogue>().HasData(new Rogue { Name = "Rogue",
            Id = 2, Strength = 2, Dexterity = 4, Intelligence = 0, BaseRange = 2 });
        modelBuilder.Entity<Sorcerer>().HasData(new Sorcerer { Name = "Sorcerer",
            Id = 3, Strength = 2, Dexterity = 1, Intelligence = 3, BaseRange = 3 });
    }
}