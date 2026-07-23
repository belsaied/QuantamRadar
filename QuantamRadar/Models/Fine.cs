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
            var lines = new List<string>
            {
                $"Trafic of Car {PlateNumber}",
                $"Total Amount {TotalAmount} EGP",
                "Violations : "
            };

            lines.AddRange(Violations.Select(v => $"{v.Description} : {v.Fee} EGP"));
            return string.Join(Environment.NewLine, lines);
        }
    }
}
