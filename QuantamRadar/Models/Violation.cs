namespace QuantamRadar.Models
{
    public sealed record Violation (
        string RuleName,
        string Description,
        decimal Fee)
    {

    }
}
