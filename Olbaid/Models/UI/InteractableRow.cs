namespace Olbaid.Models.UI;

public class InteractableRow(string label, PointPool pool, int initial) : IMenuRow
{
    private readonly int _initial = initial;
    public string Label        { get; } = label;
    public int    Value        { get; private set; } = initial;
    public string DisplayValue => $"◄ {Value} ►";

    public void OnLeft()
    {
        if (Value <= _initial) return;
        pool.Return(); Value--;
    }
    public void OnRight()
    {
        if (pool.Spend())
            Value++;
    }
    
}    