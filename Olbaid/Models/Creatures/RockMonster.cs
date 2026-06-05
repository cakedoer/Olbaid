using Olbaid.Models.Archetypes;
using Olbaid.Models.Creatures;

namespace Olbaid.Models.Creatures;

public class RockMonster : Creature
{
    public RockMonster() {} // parameterless constructor only for EF
    
    public RockMonster(int x, int y, Archetype archetype) : base(archetype)
    {
        X = x;
        Y = y;
        base.Setup();
    }
}