using Microsoft.EntityFrameworkCore;
using Olbaid.Models.Creatures;

namespace Olbaid.Models;

public class Game(Player player, GameContext db)
{
    private readonly List<Creature> _monsters = db.RockMonsters
        .Include(r => r.Archetype)
        .ToList<Creature>();
    
    private readonly Map.Map _map = new(10, 10);
    private string _statusMessage = "";

    public GameState Start()
    {
        while (true)
        {
            GameState? result = HandleInput();
            if (result.HasValue) return result.Value;
            
            Console.Clear();
            
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"HP: {player.CurrHealth}/{player.MaxHealth}  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"MP: {player.CurrMana}/{player.MaxMana}  ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            
            _map.Draw();
            Console.SetCursorPosition(player.X, player.Y+2);
            Console.Write('@'); // Player character
            
            foreach (Creature monster in _monsters)
            {
                Console.SetCursorPosition(monster.X, monster.Y + 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('R');
                Console.ResetColor();
            }
            
            // write status message here so that it doesn't get immediately overwritten
            Console.SetCursorPosition(0, _map.Height + 3);
            Console.Write(_statusMessage);
        }
    }

    private GameState? HandleInput()
    {
        // on successful move:
        _statusMessage = "";
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        bool moved = false;
    
        switch (keyInfo.Key)
        {
            // cardinal directions
            case ConsoleKey.W: moved = player.Move(0, -1,  _map.Width, _map.Height); break;
            case ConsoleKey.A: moved = player.Move(-1, 0,  _map.Width, _map.Height); break;
            case ConsoleKey.S: moved = player.Move(0, 1,   _map.Width, _map.Height); break;
            case ConsoleKey.D: moved = player.Move(1, 0,   _map.Width, _map.Height); break;
            //diagonals
            case ConsoleKey.Q: moved = player.Move(-1, -1, _map.Width, _map.Height); break;
            case ConsoleKey.E: moved = player.Move(1, -1,  _map.Width, _map.Height); break;
            case ConsoleKey.Z: moved = player.Move(-1, 1,  _map.Width, _map.Height); break;
            case ConsoleKey.X: moved = player.Move(1, 1,   _map.Width, _map.Height); break;
            //exit
            case ConsoleKey.Escape: return GameState.Mainmenu;
        }

        if (moved || keyInfo.Key == ConsoleKey.Escape) return null;
        Console.SetCursorPosition(0, _map.Height + 1);
        _statusMessage = "Blocked!";

        return null;
    }
}