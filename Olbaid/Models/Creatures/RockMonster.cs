using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public class RockMonster : Creature
{
    public RockMonster() { }
    
    public RockMonster(int x, int y, Archetype archetype)
    {
        X          = x;
        Y          = y;
        Archetype  = archetype;
        Strength     = archetype.Strength;
        Dexterity    = archetype.Dexterity;
        Intelligence = archetype.Intelligence;
        Setup();
    }
}