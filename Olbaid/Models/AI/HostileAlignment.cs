using Olbaid.Models.Creatures;

namespace Olbaid.Models.AI;

public class HostileAlignment : IAlignmentBehavior
{
    public bool IsHostileTo(Creature target) => target is Player;
}