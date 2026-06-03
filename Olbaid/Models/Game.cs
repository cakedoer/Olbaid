namespace Olbaid.Models;

public class Game
{
    private readonly Map _map = new(10, 10);
    private readonly Player _player = new(1, 1);
    private string _statusMessage = "";
    
    public GameState Start()
    {
        while (true)
        {
            GameState? result = HandleInput();
            if (result.HasValue) return result.Value;
            
            Console.Clear();
            _map.Draw();
            Console.SetCursorPosition(_player.X, _player.Y);
            Console.Write('@'); // Player character
            
            // write status message here so that it doesn't get immediately overwritten
            Console.SetCursorPosition(0, _map.Height + 1);
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
            case ConsoleKey.W: moved = _player.Move(0, -1,  _map.Width, _map.Height); break;
            case ConsoleKey.A: moved = _player.Move(-1, 0,  _map.Width, _map.Height); break;
            case ConsoleKey.S: moved = _player.Move(0, 1,   _map.Width, _map.Height); break;
            case ConsoleKey.D: moved = _player.Move(1, 0,   _map.Width, _map.Height); break;
            //diagonals
            case ConsoleKey.Q: moved = _player.Move(-1, -1, _map.Width, _map.Height); break;
            case ConsoleKey.E: moved = _player.Move(1, -1,  _map.Width, _map.Height); break;
            case ConsoleKey.Z: moved = _player.Move(-1, 1,  _map.Width, _map.Height); break;
            case ConsoleKey.X: moved = _player.Move(1, 1,   _map.Width, _map.Height); break;
            //exit
            case ConsoleKey.Escape: return GameState.Mainmenu;
        }

        if (moved || keyInfo.Key == ConsoleKey.Escape) return null;
        Console.SetCursorPosition(0, _map.Height + 1);
        _statusMessage = "Blocked!";

        return null;
    }
}