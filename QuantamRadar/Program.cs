using QuantamRadar;
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
var observations = new List<CarObservation>
{
    new CarObservation("ABC1234", DateTime.Now,  94, false , CarType.Private),
    new CarObservation("TRK5678" , DateTime.Now , 55 , true , CarType.Truck),
    // with violation
    new CarObservation("TRK5678" , DateTime.Now , 72 , true , CarType.Truck),
    new CarObservation("BUS0099" , DateTime.Now , 65 , false , CarType.Bus)

};

Console.WriteLine("---------------------Processing the Radar Observation-----------------------------");
foreach (var observation in observations)
{
    var fine = radar.ProcessObservation(observation);
    if(fine != null)
    {
        fine.ToString();
        Console.WriteLine(); 
    }
    else
    {
        Console.WriteLine($"Car {observation.PlateNumber} has no violations detected");
    }
}

Console.WriteLine("----------------------------All fines grouped by plateNumber------------------------");
foreach(var(plateNumber, totalAmount) in radar.GetAllFines())
{
    Console.WriteLine($"{plateNumber} : {totalAmount}  EGP");
}

Console.WriteLine("-----------------------------count of violated Rules------------------------------");
foreach(var (ruleName , count) in radar.GetViolatedRulesCount())
{
    Console.WriteLine($"{ruleName} : {count} times");
}

        



