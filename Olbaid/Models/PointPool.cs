namespace Olbaid.Models;

public class PointPool(int total)
{
    public int Remaining { get; private set; } = total;
    public bool Spend()   { if (Remaining == 0) return false; Remaining--; return true; }
    public void Return()  { Remaining++; }
}