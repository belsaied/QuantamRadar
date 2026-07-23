namespace QuantamRadar.Models
{
    public sealed record CarObservation(
        string PlateNumber,
        DateTime Date,
        int speed,
        bool SeatbeltFastened,
        CarType CarType )
    {

    }
}
