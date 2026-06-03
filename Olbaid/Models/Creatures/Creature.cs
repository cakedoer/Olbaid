using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public class Creature
{
    public int ArchetypeId { get; set; }
    public Archetype Archetype { get; set; } = null!; // hack that tells the compiler this won't be null apparently
    public int MaxHealth { get; set; }
    public int MaxMana { get; set; }
    public int Attack { get; set; }
    public int CurrHealth { get; set; }
    public int CurrMana { get; set; }
    
    // initialize the creature based on its archetypal stats
    public void Setup()
    {
        MaxHealth = Archetype.Strength     * 5;
        MaxMana   = Archetype.Intelligence * 3;
        Attack    = Archetype.Dexterity    * 2;

        CurrHealth = MaxHealth;
        CurrMana   = MaxMana;
    }
}