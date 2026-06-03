using Olbaid.Models;
using Olbaid.Models.UI;

namespace Olbaid;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Console.InputEncoding  = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        bool quit = false;
        GameState gameState = GameState.Mainmenu;

        // define menus like this for now, todo composite pattern menus for point allocation etc.
        // SimpleMenu mainMenu = new SimpleMenu("Main Menu", ["New Game", "Load Game", "Quit"]);
        CompositeMenu mainMenu = new CompositeMenu("Main Menu", [
            new SelectableRow("New Game"),
            new SelectableRow("Load Game"),
            new SelectableRow("Quit")
        ]);
            
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
                    // new game logic caller
                    gameState = new Game().Start();
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