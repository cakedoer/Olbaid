using Microsoft.EntityFrameworkCore;
using Olbaid.Models.Creatures;
using Olbaid.Models.UI;

namespace Olbaid.Models;

public class Game(Player player, GameContext db)
{
    private readonly List<Creature> _monsters = db.RockMonsters
        .Include(r => r.Archetype)
        .ToList<Creature>()
        .Select(m => { m.Setup(); return m; })
        .ToList();
    
    private readonly Map.Map _map = new(10, 10);
    private string _statusMessage = "";

    public GameState Start()
    {
        // test monster vals
        foreach (var m in _monsters)
            Console.WriteLine($"Monster: {m.GetType().Name}, Archetype: {m.Archetype?.Name ?? "NULL"}, STR: {m.Strength}, HP: {m.CurrHealth}");
        Console.ReadKey(true);
        
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape) return GameState.Mainmenu;
        
            InputResult result = HandleInput(keyInfo); // pass keyInfo in
            if (result == InputResult.Blocked) _statusMessage = "Blocked!";
            
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
            Console.SetCursorPosition(player.X, player.Y + 2);
            Console.Write('@');
            
            foreach (Creature monster in _monsters)
            {
                Console.SetCursorPosition(monster.X, monster.Y + 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('R');
                Console.ResetColor();
            }
            
            Console.SetCursorPosition(0, _map.Height + 3);
            Console.Write(_statusMessage);
            
            // always render action menu below status message
            Console.SetCursorPosition(0, _map.Height + 4);
            Console.WriteLine("Choose action:");
            Console.SetCursorPosition(0, _map.Height + 5);
            Console.Write("  [Enter] Attack");
        }
    }

    private InputResult HandleInput(ConsoleKeyInfo keyInfo)
    {
        _statusMessage = "";
    
        switch (keyInfo.Key)
        {
            // cardinal directions
            case ConsoleKey.W: return player.Move(0, -1,  _map.Width, _map.Height) ? InputResult.TurnConsumed : InputResult.Blocked;
            case ConsoleKey.A: return player.Move(-1, 0,  _map.Width, _map.Height) ? InputResult.TurnConsumed : InputResult.Blocked;
            case ConsoleKey.S: return player.Move(0, 1,   _map.Width, _map.Height) ? InputResult.TurnConsumed : InputResult.Blocked;
            case ConsoleKey.D: return player.Move(1, 0,   _map.Width, _map.Height) ? InputResult.TurnConsumed : InputResult.Blocked;
            // diagonals
            case ConsoleKey.Q: return player.Move(-1, -1, _map.Width, _map.Height) ? InputResult.TurnConsumed : InputResult.Blocked;
            case ConsoleKey.E: return player.Move(1, -1,  _map.Width, _map.Height) ? InputResult.TurnConsumed : InputResult.Blocked;
            case ConsoleKey.Z: return player.Move(-1, 1,  _map.Width, _map.Height) ? InputResult.TurnConsumed : InputResult.Blocked;
            case ConsoleKey.X: return player.Move(1, 1,   _map.Width, _map.Height) ? InputResult.TurnConsumed : InputResult.Blocked;
            case ConsoleKey.Enter:  return RunAttack() ? InputResult.TurnConsumed : InputResult.None;
            case ConsoleKey.Escape: return InputResult.None; // handled separately
        }

        return InputResult.None;
    }
    
    private bool RunAttack()
    {
        // look for monsters in range
        List<Creature> targets = [];
        
        for (int y = player.Y - player.CurrRange; y <= player.Y + player.CurrRange; y++)
        {
            for (int x = player.X - player.CurrRange; x <= player.X + player.CurrRange; x++)
            {
                if (x < 0 || x >= _map.Width || y < 0 || y >= _map.Height) continue;
                
                Creature? found = _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
                if (found != null) targets.Add(found);
            }
        }
        
        if (targets.Count == 0)
        {
            _statusMessage = "No monsters in range.";
            return false; // turn not consumed
        }
        
        // build target selection menu
        IMenuRow[] rows = targets
            .Select(m => (IMenuRow)new SelectableRow($"{m.GetType().Name} (HP: {m.CurrHealth}/{m.MaxHealth})"))
            .Append(new SelectableRow("Cancel"))
            .ToArray();

        CompositeMenu targetMenu = new CompositeMenu(() => "Select Target", rows);
        int selectedIndex = targetMenu.ShowAndGetSelection();

        // last index is always Cancel
        if (selectedIndex == rows.Length - 1)
        {
            _statusMessage = "";
            return false; // turn not consumed
        }
        
        Creature target = targets[selectedIndex];
        
        bool killed = player.Attack(target);
        
        if (killed)
        {
            _monsters.Remove(target);
            player.KillCount++;
            db.Players.Update(player);
            db.SaveChanges();
            _statusMessage = $"{target.GetType().Name} defeated!";
        }
        else
        {  _statusMessage = $"Hit! {target.GetType().Name} has" +
                            $"{target.CurrHealth}/{target.MaxHealth} HP remaining."; }
        
        return true; // turn consumed
    }
}