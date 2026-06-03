namespace Olbaid.Models;

public class CompositeMenu(string menuTitle, IMenuRow[] rows)
{
    private int _selectedIndex = 0;
    
    public T ShowAndGetSelection<T>() where T : struct, Enum
    {
        var values = Enum.GetValues<T>();
        
        bool selecting = true;
        while (selecting)
        {
            Render();
            
            // only has up and down for now
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    //_selectedIndex = (_selectedIndex == 0) ? options.Length - 1 : _selectedIndex - 1;
                    _selectedIndex = (_selectedIndex == 0) ? rows.Length - 1 : _selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    //_selectedIndex = (_selectedIndex == options.Length - 1) ? 0 : _selectedIndex + 1;
                    _selectedIndex = (_selectedIndex == rows.Length - 1) ? 0 : _selectedIndex + 1;
                    break;
                case ConsoleKey.LeftArrow:  rows[_selectedIndex].OnLeft();  break;
                case ConsoleKey.RightArrow: rows[_selectedIndex].OnRight(); break;
                case ConsoleKey.Enter:
                    selecting = false;
                    break;
            }
        }

        return values[_selectedIndex];
    }

    private void Render()
    {
        // Console.SetCursorPosition(0, 0); // leaves artefacts
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"=== {menuTitle.ToUpper()} ===");
        Console.ResetColor();
        Console.WriteLine("Use Arrow Keys to navigate and Enter to Continue:\n");

        for (int i = 0; i < rows.Length; i++)
        {
            if (i == _selectedIndex)
            {
                Console.Write(" > ");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" {rows[i].Label} {rows[i].DisplayValue}");
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"    {rows[i].Label}  {rows[i].DisplayValue}");
            }
        }
    }
}