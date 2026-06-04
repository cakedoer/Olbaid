using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public abstract class Creature
{
    protected Creature(){} // need this for inheritance otherwise compiler moans about child classes' constructors
    // change set; to init; later
    public int Id { get; set; }
    
    public int ArchetypeId { get; set; }
    public Archetype Archetype { get; set; } = null!;
    
    public int X { get; internal set; }
    public int Y { get; internal set; }

    public int Strength     { get; internal set; }
    public int Dexterity    { get; internal set; }
    public int Intelligence { get; internal set; }
    
    public long CreatedAt { get; internal set; } // unix timestamp
    public int KillCount { get; set; }
    
    public int MaxHealth  { get; set; }
    public int MaxMana    { get; set; }
    public int Power     { get; set; }
    public int CurrHealth { get; set; }
    public int CurrMana   { get; set; }
    
    public int CurrRange { get; set; }

    protected Creature(Archetype archetype)
    {
        ArchetypeId = archetype.Id;
        Archetype   = archetype;
    }
    
    // initialize the creature based on its own stats (base archetype; additionally allocated points in Player.cs)
    public virtual void Setup()
    {
        Strength     = Archetype.Strength;
        Dexterity    = Archetype.Dexterity;
        Intelligence = Archetype.Intelligence;
    
        MaxHealth  = Strength     * 5;
        MaxMana    = Intelligence * 3;
        Power      = Dexterity    * 2;
        CurrHealth = MaxHealth;
        CurrMana   = MaxMana;
        CurrRange  = Archetype.BaseRange;
    }
}