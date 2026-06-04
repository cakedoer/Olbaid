using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public abstract class Creature
{
    // change set; to init; later
    public int Id { get; set; }
    
    public int ArchetypeId { get; set; }
    public Archetype Archetype { get; set; } = null!;
    
    public int X { get; internal set; }
    public int Y { get; internal set; }

    protected int Strength     { get; set; }
    protected int Dexterity    { get; set; }
    protected int Intelligence { get; set; }
    
    public int MaxHealth  { get; set; }
    public int MaxMana    { get; set; }
    public int Attack     { get; set; }
    public int CurrHealth { get; set; }
    public int CurrMana   { get; set; }
    
    // initialize the creature based on its own stats (base archetype + allocated points)
    protected void Setup()
    {
        MaxHealth = Strength     * 5;
        MaxMana   = Intelligence * 3;
        Attack    = Dexterity    * 2;

        CurrHealth = MaxHealth;
        CurrMana   = MaxMana;
    }
}