namespace Olbaid.Models.Creatures;

public class Player : Creature
{
    public int X { get; private set; }
    public int Y { get; private set; }
    
    // maybe add class here
    public Player(int initialX, int initialY)
    {
        X = initialX;
        Y = initialY;
    }
    
    public bool Move(int deltaX, int deltaY, int mapWidth, int mapHeight)
    {
        // X += deltaX;
        // Y += deltaY;
        
        int newX = X + deltaX;
        int newY = Y + deltaY;
        
        if (newX < 0 || newX >= mapWidth || newY < 0 || newY >= mapHeight)
            return false;
        
        X = newX;
        Y = newY;
        return true;
    }
}