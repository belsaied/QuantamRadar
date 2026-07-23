using QuantamRadar.Models;

namespace QuantamRadar.Rules
{
    public class SpeedLimitRule : IViolationRule
    {
        public string RuleName => "SpeedLimitRule";
        private readonly Dictionary<CarType, int> _maxAllowedSpeedByCarType;
        private readonly decimal _fineFee;
        public SpeedLimitRule(Dictionary<CarType, int> maxAllowedSpeedByCarType , decimal fineFee = 300)
        {
            _maxAllowedSpeedByCarType = maxAllowedSpeedByCarType;
            _fineFee = fineFee; 
        }

        public Violation? Evaluate(CarObservation carObservation)
        {
           if(_maxAllowedSpeedByCarType.TryGetValue(carObservation.CarType, out int value))
            {
                if(carObservation.speed > value)
                {
                    string description = $"speed of {carObservation.speed} exceeded the max allowed speed {value}";
                    return new Violation(RuleName, description, _fineFee);
                }
            }
           return null;
        }
    }
}
