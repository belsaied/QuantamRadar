# QuantamRadar

A console-based traffic radar simulator built in C#. It records live car observations, evaluates them against a set of configurable traffic rules, and issues fines for violations such as speeding or not wearing a seatbelt.

## Features

- **Live observation entry** via the console (plate number, car type, speed, seatbelt status)
- **Pluggable rule engine** — violation rules implement a common `IViolationRule` interface, so new rules can be added without touching existing logic
- **Built-in rules**
  - `SeatbeltRule` — flags cars where the seatbelt isn't fastened
  - `SpeedLimitRule` — flags cars exceeding the max speed for their car type (configurable per `CarType`)
- **Fine calculation** — each violation carries a fee, and fines are aggregated per car
- **Reporting**
  - Total fines grouped by plate number
  - Count of how many times each rule was violated

## Project Structure

```
QuantamRadar/
├── Input/
│   ├── IObservationInputProvider.cs   # Abstraction for reading observations
│   └── ConsoleObservationInputProvider.cs  # Console-based input implementation
├── Models/
│   ├── CarObservation.cs   # A single recorded observation
│   ├── CarType.cs          # Private / Truck / Bus
│   ├── Violation.cs        # A single rule violation
│   └── Fine.cs             # Aggregated violations + total amount for a car
├── Rules/
│   ├── IViolationRule.cs   # Rule contract
│   ├── SeatbeltRule.cs
│   └── SpeedLimitRule.cs
├── QuRadar.cs               # Core engine: applies rules, tracks issued fines
└── Program.cs                # Entry point / console loop
```

## How It Works

1. `Program.cs` configures the max allowed speed per `CarType` and registers the active rules (`SeatbeltRule`, `SpeedLimitRule`).
2. The `QuRadar` engine is created with these rules.
3. In a loop, the console prompts for a new observation (plate number, car type, speed, seatbelt status).
4. Each observation is run through every registered rule via `QuRadar.ProcessObservation`.
5. If any rule is violated, a `Fine` is created and printed; otherwise the car is reported as violation-free.
6. When the user stops entering observations, the app prints:
   - Total fine amount per plate number
   - How many times each rule was triggered

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run the project

```bash
git clone https://github.com/belsaied/QuantamRadar.git
cd QuantamRadar
dotnet run --project QuantamRadar
```

Follow the console prompts to add observations. Enter `n` when asked to add a new observation to stop and see the summary.

### Example session

```
-------------------------live Observation entry--------------------------

Add a new observation (y/n)
y
Plate Number :ABC123
Car type (Private / Truck / Bus): Private
enter Speed :
95
Is the seatbelt fastened? (y/n)
n

Traffic for Car ABC123
Total Amount 400 EGP
Violations:
- speed of 95 exceeded the max allowed speed 80 : 300 EGP
- Seatbelt not fastned : 100 EGP

Add a new observation (y/n)
n

----------------------------All fines grouped by plateNumber------------------------
ABC123 : 400  EGP
-----------------------------count of violated Rules------------------------------
SpeedLimitRule : 1 times
SeatbeltRule : 1 times
```

## Extending the Rules

To add a new violation rule:

1. Implement `IViolationRule`:
   ```csharp
   public class MyNewRule : IViolationRule
   {
       public string RuleName => "MyNewRule";

       public Violation? Evaluate(CarObservation carObservation)
       {
           // return a Violation if broken, otherwise null
       }
   }
   ```
2. Register it in the `rules` list in `Program.cs`.

No changes to `QuRadar` or the reporting logic are needed.

## Tech Stack

- C# / .NET 10
- Console application, no external dependencies

## License

No license specified yet — add one (e.g. MIT) if you'd like others to freely use or contribute to this project.
