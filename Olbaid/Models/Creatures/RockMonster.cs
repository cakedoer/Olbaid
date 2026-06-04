using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public class RockMonster : Creature
{
    public RockMonster(){} // parameterless constructor only for EF
    
    public override void Setup()
    {
        Strength     = Archetype.Strength;
        Dexterity    = Archetype.Dexterity;
        Intelligence = Archetype.Intelligence;
        base.Setup();
    }
    
    public RockMonster(int x, int y, Archetype archetype) : base(archetype)
    {
        X = x;
        Y = y;
        Strength     = archetype.Strength;
        Dexterity    = archetype.Dexterity;
        Intelligence = archetype.Intelligence;
        Setup();
    }
}