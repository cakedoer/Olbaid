using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Olbaid.Models.Archetypes;
using Olbaid.Models.Creatures;

namespace Olbaid.Models;

public static class CreatureFactory
{
    public static List<Creature> LoadFromDb(GameContext db)
    {
        List<Creature> monsters =
        [
            ..db.RockMonsters.Include(r => r.Archetype).ToList<Creature>(),
            ..db.Goblins.Include(g => g.Archetype).ToList<Creature>()
        ];
        
        foreach (Creature m in monsters)
            m.Setup();
        
        return monsters;
    }
    
    public static Goblin SpawnGoblin(int x, int y, Archetype archetype)
        => new Goblin(x, y, archetype);
    
    public static (int x, int y) GetRandomUnoccupiedTile(
        Map.Map map,
        List<Creature> occupants)
    {
        Random rng = Random.Shared;
        int x, y;
        
        do
        {
            x = rng.Next(0, map.Width);
            y = rng.Next(0, map.Height);
        } while (occupants.Any(c => c.X == x && c.Y == y));
        
        return (x, y);
    }
}