using static Olbaid.Models.Map.TileType;

namespace Olbaid.Models.Map;

public class Map
{
    // [,] = 2d array
    private readonly char[,] _grid;
    public int Width  { get; }
    public int Height { get; }

    public Map(int width, int height)
    {
        Width  = width;
        Height = height;
        _grid  = new char[width, height];
        GenerateMap();
    }
    
    private void GenerateMap()
    {
        for (int y = 0; y < _grid.GetLength(1); y++)
        {
            for (int x = 0; x < _grid.GetLength(0); x++)
            { _grid[x, y] = Floor; }
        }
    }
    
    public void Draw()
    {
        for (int y = 0; y < _grid.GetLength(1); y++)
        {
            for (int x = 0; x < _grid.GetLength(0); x++)
            { Console.Write(_grid[x, y]); }
            Console.WriteLine();
        }
    }
}