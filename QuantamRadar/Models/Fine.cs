using System.Text;

namespace QuantamRadar.Models
{
    public record Fine (
        string PlateNumber,
        DateTime Date,
        IReadOnlyList<Violation> Violations)
    {
        public decimal TotalAmount => Violations.Sum(v => v.Fee);
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Traffic for Car {PlateNumber}");
            builder.AppendLine($"Total Amount {TotalAmount} EGP");
            builder.AppendLine("Violations:");

            foreach (var violation in Violations)
            {
                builder.AppendLine($"- {violation.Description} : {violation.Fee} EGP");
            }

            return builder.ToString().TrimEnd();
        }
    }
}
