using Olbaid.Models.AI;
using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public class Goblin : Creature
{
    public Goblin() {} // parameterless constructor only for EF
    
    public Goblin(int x, int y, Archetype archetype) : base(archetype)
    {
        X         = x;
        Y         = y;
        Alignment = new HostileAlignment();
        Ai        = new ChaseAndAttackAi(Alignment);
        base.Setup();
    }
}