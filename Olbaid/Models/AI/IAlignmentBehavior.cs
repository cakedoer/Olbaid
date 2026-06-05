using Olbaid.Models.Creatures;

namespace Olbaid.Models.AI;

public interface IAlignmentBehavior
{
    bool IsHostileTo(Creature target);
}