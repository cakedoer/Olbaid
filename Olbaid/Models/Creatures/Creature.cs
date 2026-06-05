using System.ComponentModel.DataAnnotations.Schema;
using Olbaid.Models.AI;
using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public abstract class Creature
{
    protected Creature(){} // need this for inheritance otherwise compiler moans about child classes' constructors
    // todo consider changing set; to init; later
    public int Id { get; set; }
    public int ArchetypeId { get; set; }
    public Archetype Archetype { get; set; } = null!;
    public bool? IsDead { get; set; } = null;
    
    // coordinates
    public int X { get; internal set; }
    public int Y { get; internal set; }

    // attributes
    public int Strength     { get; internal set; }
    public int Dexterity    { get; internal set; }
    public int Intelligence { get; internal set; }
    
    // stuff for the tracker
    public long CreatedAt { get; internal set; } // unix timestamp
    public int KillCount  { get; set; }
    
    // derived stats
    public int MaxHealth  { get; set; }
    public int MaxMana    { get; set; }
    public int Power      { get; set; }
    public int CurrHealth { get; set; }
    public int CurrMana   { get; set; }
    public int CurrRange  { get; set; }
    
    // non-player creature stuff
    [NotMapped]
    public IAlignmentBehavior? Alignment { get; set; }
    [NotMapped]
    public IAiBehavior?        Ai        { get; set; }

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