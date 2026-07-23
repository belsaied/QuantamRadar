using QuantamRadar.Models;

namespace QuantamRadar.Input
{
    public class ConsoleObservationInputProvider : IObservationInputProvider
    {
        public CarObservation? ReadNext()
        {
            Console.WriteLine();

            if (!ReadYesNo("Add a new observation (y/n) "))
            {
                return null;
            }

            string plateNumber = ReadPlateNumber( );
             CarType carType = ReadCarType();
            int speed =ReadSpeed();
            bool seatbeltFastened = ReadYesNo("Is the seatbelt fastened? (y/n)  ");

            return new CarObservation(plateNumber, DateTime.Now, speed, seatbeltFastened, carType);

        }

        private static string ReadPlateNumber()
        {
            while (true)
            {
                Console.Write("Plate Number :");
                var input = Console.ReadLine();
                if(!string.IsNullOrEmpty(input))
                {
                    return input.Trim();
                }
                  Console.WriteLine("Plate number cannot be empty. Try again.");
            }
        }
        private static CarType ReadCarType()
        {
            var choices = Enum.GetNames<CarType>();
            while (true)
            {
                Console.Write($"Car type ({string.Join(" / ", choices)}): ");
                var input = Console.ReadLine();
                if (Enum.TryParse<CarType>(input, ignoreCase: true, out var carType))
                  {
                     return carType;
                  }
                Console.WriteLine($"Invalid car type. Please enter one of: {string.Join(", ", choices)}");
            }
        }
        private static int ReadSpeed()
        {
            while (true)
            {
                Console.WriteLine("enter Speed :");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int speed) && speed >= 0)
                {
                    return speed;
                }
                Console.WriteLine("Speed must be a whole, non-negative number. Try again.");
            }
        }
        private static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine()?.Trim();
                  if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase)) return true;
              if (string.Equals(input, "n", StringComparison.OrdinalIgnoreCase)) return false;
                Console.WriteLine("Please answer with 'y' or 'n'.");
            }
        }
    }
}
