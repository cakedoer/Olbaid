namespace Olbaid.Models;
using System.ComponentModel.DataAnnotations;

public class Archetype
{
    public int Id           { get; set; }
    public int BaseRange    { get; set; }
    public int Strength     { get; set; }
    public int Dexterity    { get; set; }
    public int Intelligence { get; set; }
    [MaxLength(32)]
    public string Name      { get; set; } = string.Empty;

    // Potential future functionality -- multiple actions per round. adds movement and attack "points" i.e. a counter
    // public int BaseMS { get; set; }
    // public int BaseAttacksPerRound { get; set; }
}