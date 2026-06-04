using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public class Player : Creature
{
    protected Player(){} // parameterless constructor only for EF
    
    public Player(int initialX, int initialY, Archetype archetype, int stren,  int dexte, int intel)
    {
        X = initialX;
        Y = initialY;
        Archetype = archetype;
        Strength     = archetype.Strength     + stren;
        Dexterity    = archetype.Dexterity    + dexte;
        Intelligence = archetype.Intelligence + intel;
        Setup();
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