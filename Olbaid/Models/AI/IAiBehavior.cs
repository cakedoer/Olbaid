using System.Collections.Generic;
using Olbaid.Models.Creatures;

namespace Olbaid.Models.AI;

public interface IAiBehavior
{
    void TakeTurn(Creature self, List<Creature> allCreatures, Map.Map map);
}