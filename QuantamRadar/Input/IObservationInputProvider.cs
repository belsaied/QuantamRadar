using QuantamRadar.Models;

namespace QuantamRadar.Input
{
    public interface IObservationInputProvider
    {
        CarObservation? ReadNext();
    }
}
