using QuantamRadar.Models;

namespace QuantamRadar.Rules
{
    public interface IViolationRule
    {
        string RuleName { get; }
        Violation? Evaluate (CarObservation carObservation);
    }
}
