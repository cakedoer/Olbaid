namespace Olbaid.Models;

public interface IMenuRow
{
    string Label       { get; }
    string DisplayValue { get; }
    void OnLeft();
    void OnRight();
}