using QuantamRadar.Models;

namespace QuantamRadar.Rules
{
    public class SeatbeltRule : IViolationRule
    {
        public string RuleName => "SeatbeltRule";
        private const decimal FineFee = 100;

        public Violation? Evaluate(CarObservation carObservation)
        {
            if (!carObservation.SeatbeltFastened)
            {
                return new Violation(RuleName, "Seatbelt is not fastened", FineFee);
            }
            return null;
        }
    }
}
