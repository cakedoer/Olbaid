namespace Olbaid.Models;

public class SelectableRow(string label) : IMenuRow
{
    public string Label        { get; } = label;
    public string DisplayValue { get; } = "";
    public void OnLeft()  { }
    public void OnRight() { }
}