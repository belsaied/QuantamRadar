using QuantamRadar.Models;
using QuantamRadar.Rules;

namespace QuantamRadar
{
    public class QuRadar
    {
        private readonly List<IViolationRule> _rules;
        private readonly List<Fine> _issuedFines = new();

        public QuRadar(List<IViolationRule> rules)
        {
            _rules = rules;
        }

        public Fine? ProcessObservation(CarObservation carObservation)
        {
            var violations = new List<Violation>();
            foreach (var rule in _rules)
            {
                var violation = rule.Evaluate(carObservation);
                if (violation != null)
                {
                    violations.Add(violation);
                }
            }
            if (violations.Count == 0)
            {
                return null;
            }

            var fine = new Fine(carObservation.PlateNumber, carObservation.Date, violations);
            _issuedFines.Add(fine);
            return fine;
        }

        public List<(string PlateNumber, decimal TotalAmount)> GetAllFines()
        {
            return _issuedFines
                .GroupBy(fine => fine.PlateNumber)
                .Select(group => (PlateNumber: group.Key, TotalAmount: group.Sum(f => f.TotalAmount))).ToList();
        }

        public Dictionary<string, int> GetViolatedRulesCount()
        {
            var count = new Dictionary<string, int>();
            foreach(var fine in _issuedFines)
            {
                foreach(var violation  in fine.Violations)
                {
                    if (!count.ContainsKey(violation.RuleName))
                    {
                        count[violation.RuleName] = 0;
                    }
                    count[violation.RuleName]++;
                }
            }
            return count;
        }

        public IReadOnlyList<Fine> GetIssuedFines()
            => _issuedFines.AsReadOnly();
    }
}
