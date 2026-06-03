namespace Olbaid.Models.UI;

public class SimpleMenu(string menuTitle, string[] options)
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
                    _selectedIndex = (_selectedIndex == 0) ? options.Length - 1 : _selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    _selectedIndex = (_selectedIndex == options.Length - 1) ? 0 : _selectedIndex + 1;
                    break;
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
        Console.WriteLine("Use Up/Down Arrow keys and press Enter:\n");

        for (int i = 0; i < options.Length; i++)
        {
            if (i == _selectedIndex)
            {
                Console.Write(" > ");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" {options[i]} ");
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"    {options[i]}");
            }
        }
    }
}