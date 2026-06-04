using Olbaid.Models.Archetypes;

namespace Olbaid.Models.Creatures;

public class Player : Creature
{
    public Player(){} // parameterless constructor only for EF
    
    private readonly int _stren, _dexte, _intel;
    
    public Player(int x, int y, Archetype archetype, int stren, int dexte, int intel) : base(archetype)
    {
        X      = x;
        Y      = y;
        _stren = stren;
        _dexte = dexte;
        _intel = intel;
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
    
    public bool Attack(Creature target)
    {
        target.CurrHealth -= this.Power;
        return target.CurrHealth <= 0;
    }
    
    public sealed override void Setup()
    {
        Strength     = Archetype.Strength     + _stren;
        Dexterity    = Archetype.Dexterity    + _dexte;
        Intelligence = Archetype.Intelligence + _intel;
        base.Setup();
    }
}