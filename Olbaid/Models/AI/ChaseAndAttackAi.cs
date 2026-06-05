using System;
using System.Collections.Generic;
using System.Linq;
using Olbaid.Models.AI;
using Olbaid.Models.Creatures;

namespace Olbaid.Models.AI;

public class ChaseAndAttackAi(IAlignmentBehavior alignment) : IAiBehavior
{
    public void TakeTurn(Creature self, List<Creature> allCreatures, Map.Map map)
    {
        Creature? target = allCreatures
            .Where(c => alignment.IsHostileTo(c))
            .OrderBy(c => Math.Abs(c.X - self.X) + Math.Abs(c.Y - self.Y))
            .FirstOrDefault();
        
        if (target == null) return;
        
        // attack if in range
        if (Math.Abs(target.X - self.X) <= self.CurrRange &&
            Math.Abs(target.Y - self.Y) <= self.CurrRange)
        {
            target.CurrHealth -= self.Power;
            return;
        }
        
        // moved this to player
        // if (target.CurrHealth <= 0) target.IsDead = true;
        
        // move one step toward target
        int dx = Math.Sign(target.X - self.X);
        int dy = Math.Sign(target.Y - self.Y);
        
        int newX = self.X + dx;
        int newY = self.Y + dy;
        
        // out of bounds check
        if (newX >= 0 && newX < map.Width && newY >= 0 && newY < map.Height)
        {
            self.X = newX;
            self.Y = newY;
        }
    }
}