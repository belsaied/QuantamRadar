using QuantamRadar;
using QuantamRadar.Input;
using QuantamRadar.Models;
using QuantamRadar.Rules;

var maxSpeeds = new Dictionary<CarType, int>
{
    {CarType.Private , 80 },
    {CarType.Truck , 60  },
    {CarType.Bus , 70 }
};

var rules = new List<IViolationRule> {
    new SeatbeltRule(),
    new SpeedLimitRule(maxSpeeds)
};

var radar = new QuRadar(rules);
IObservationInputProvider inputProvider = new ConsoleObservationInputProvider();

Console.WriteLine("-------------------------live Observation entry--------------------------");

while (true)
{
    CarObservation? observation;
    try
    {
        observation = inputProvider.ReadNext();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"That observation could not be recorded: {ex.Message}");
        continue;
    }
    if(observation is null)
    {
        break;
    }

    var fine = radar.ProcessObservation(observation);
    Console.WriteLine();
    if(fine is not null)
    {
        Console.WriteLine(fine);
    }
    else
    {
        Console.WriteLine($"Car {observation.PlateNumber} has no violations detected.");
    }
}

Console.WriteLine();

Console.WriteLine("----------------------------All fines grouped by plateNumber------------------------");
foreach (var (plateNumber, totalAmount) in radar.GetAllPossibleFines())
{
    Console.WriteLine($"{plateNumber} : {totalAmount}  EGP");
}

Console.WriteLine("-----------------------------count of violated Rules------------------------------");
foreach (var (ruleName, count) in radar.GetViolatedRulesCount())
{
    Console.WriteLine($"{ruleName} : {count} times");
}





