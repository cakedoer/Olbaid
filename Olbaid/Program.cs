using Olbaid.Models;
using Olbaid.Models.Archetypes;
using Olbaid.Models.Creatures;
using Olbaid.Models.UI;

namespace Olbaid;

internal class Program
{
    
    public static void Main(string[] args)
    {
        // Instantiate context with a "using" statement, does memory cleanup
        using var context = new GameContext();
        // Force EF Core to create the db file and execute OnModelCreating seeds
        context.Database.EnsureCreated();
        
        // set console to UTF8 and disable cursor until game loop is exited
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        
        bool quit = false;
        GameState gameState = GameState.Mainmenu;
        
        // SimpleMenu mainMenu = new SimpleMenu("Main Menu", ["New Game", "Load Game", "Quit"]);
        // composite pattern menus for point allocation etc.
        CompositeMenu mainMenu = new CompositeMenu(() => "MAIN MENU",
        [
            new SelectableRow("New Game"),
            new SelectableRow("Load Game"),
            new SelectableRow("Quit")
        ]);
            
        while (!quit)
        {
            switch (gameState)
            {
                case GameState.Mainmenu:
                    int mainSelection = mainMenu.ShowAndGetSelection();
                    gameState = mainSelection switch
                    {
                        0 => GameState.Newgame,
                        // todo hasSaves - new query for available Players in db every time main menu is loaded
                        // 1 => hasSaves ? GameState.Loadgame : GameState.Quit,
                        1 => GameState.Loadgame,
                        2 => GameState.Quit,
                        _ => gameState
                    };
                    break;

                case GameState.Newgame:
                {
                    using GameContext db = new GameContext();
                    Archetype chosen = RunArchetypeSelection(db);
                    var (stren, dexte, intel) = RunStatAllocation(chosen);
                    gameState = new Game(new Player(1, 1, chosen, stren, dexte, intel), context).Start();
                    break;
                }

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
    
    private static Archetype RunArchetypeSelection(GameContext db)
    {
        List<Archetype> archetypes = db.Archetypes.ToList();
    
        CompositeMenu archetypeMenu = new CompositeMenu(() => "CHOOSE CLASS",
            // archetypes.Select(a => (IMenuRow)new SelectableRow(a.Name)).ToArray()
            archetypes.Select(IMenuRow (a) => new SelectableRow(a.Name)).ToArray()
        );
    
        int selectedIndex = archetypeMenu.ShowAndGetSelection();
        return archetypes[selectedIndex];
    }
    
    private static (int Strength, int Dexterity, int Intelligence) RunStatAllocation(Archetype archetype)
    {
        PointPool pool = new PointPool(3);

        InteractableRow strRow = new InteractableRow("Strength",     pool, archetype.Strength);
        InteractableRow dexRow = new InteractableRow("Dexterity",    pool, archetype.Dexterity);
        InteractableRow intRow = new InteractableRow("Intelligence", pool, archetype.Intelligence);
    
        CompositeMenu statMenu = new CompositeMenu(() => $"Allocate Stats — Points remaining: {pool.Remaining}", [strRow, dexRow, intRow]);
        statMenu.ShowAndGetSelection();
    
        return (strRow.Value, dexRow.Value, intRow.Value);
    }
}