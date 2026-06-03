namespace Olbaid.Models;

public class InteractableRow(string label, int min, int max, int initial = 0) : IMenuRow
{
    public string Label        { get; } = label;
    public int    Value        { get; private set; } = initial;
    public string DisplayValue => $"◄ {Value} ►";
    
    public void OnLeft()  { if (Value > min) Value--; }
    public void OnRight() { if (Value < max) Value++; }
}