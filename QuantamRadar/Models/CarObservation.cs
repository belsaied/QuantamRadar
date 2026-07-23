namespace QuantamRadar.Models
{
    public sealed record CarObservation(
        string PlateNumber,
        DateTime Date,
        int Speed,
        bool SeatbeltFastened,
        CarType CarType )
    {

    }
}
