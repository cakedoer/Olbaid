using Olbaid.Models;

namespace Olbaid;

public enum GameState
{
    Mainmenu,
    Newgame,
    Loadgame,
    Quit
}

public enum MainMenuOption
{
    Newgame,
    Loadgame,
    Quit
}

internal class Program
{
    public static void Main(string[] args)
    {
        Console.CursorVisible = false;
        bool quit = false;
        GameState gameState = GameState.Mainmenu;

        // define menus like this for now, todo composite pattern menus for point allocation etc.
        SimpleMenu mainMenu = new SimpleMenu("Main Menu", ["New Game", "Load Game", "Quit"]);
            
        while (!quit)
        {
            switch (gameState)
            {
                case GameState.Mainmenu:
                    MainMenuOption mainSelection = mainMenu.ShowAndGetSelection<MainMenuOption>();
                    gameState = mainSelection switch
                    {
                        MainMenuOption.Newgame  => GameState.Newgame,
                        MainMenuOption.Loadgame => GameState.Loadgame,
                        MainMenuOption.Quit     => GameState.Quit,
                        _                       => gameState
                    };
                    break;

                case GameState.Newgame:
                    // todo game logic
                    Console.Clear();
                    Console.WriteLine("\nPress any key to continue...");
                        
                    Console.ReadKey(true); 
                        
                    gameState = GameState.Mainmenu; 
                    break;
                    
                case GameState.Loadgame:
                    Console.Clear();
                    Console.WriteLine("\nPress any key to continue...");
                        
                    Console.ReadKey(true); 
                        
                    gameState = GameState.Mainmenu; 
                    break;

                case GameState.Quit:
                    Console.CursorVisible = true;
                    quit = true;
                    break;
            }
        }
    }
}