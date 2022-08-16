namespace LearnStateMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var carStateMachine = new Stateless.StateMachine<Car.State, Car.Action>(Car.State.Stopped);

            carStateMachine.Configure(Car.State.Stopped)
                .Permit(Car.Action.Start, Car.State.Started);

            var accelerateWithSpeed = carStateMachine.SetTriggerParameters<int>(Car.Action.Accelerate);

            carStateMachine.Configure(Car.State.Started)
                .Permit(Car.Action.Accelerate, Car.State.Running)
                .Permit(Car.Action.Stop, Car.State.Stopped)
                .OnEntry(s => Console.WriteLine($"On entry : source - {s.Source} & destination - {s.Destination}"))
                .OnExit(s => Console.WriteLine($"On Exit : source - {s.Source} & destination - {s.Destination}"));

            carStateMachine.Configure(Car.State.Running)
                .SubstateOf(Car.State.Started)
                .Permit(Car.Action.Stop, Car.State.Stopped)
                .OnEntryFrom(accelerateWithSpeed, speed => Console.WriteLine($"speed : {speed}"))
                .InternalTransition(Car.Action.Start, () => Console.WriteLine("Start called while running"));


            Console.WriteLine($"Initial State: {carStateMachine.State}");

            Console.WriteLine($"Invoke start action");
            carStateMachine.Fire(Car.Action.Start);
            Console.WriteLine($"New State After start action invoke: {carStateMachine.State}");

            Console.WriteLine($"Invoke Accelerate action with 80 speed");
            carStateMachine.Fire(accelerateWithSpeed,80);
            Console.WriteLine($"New State After Accelerate action invoke: {carStateMachine.State}");

            Console.WriteLine($"Invoke Start action");
            carStateMachine.Fire(Car.Action.Start);
            Console.WriteLine($"New State After Start action invoke: {carStateMachine.State}");

            Console.WriteLine($"Invoke Stop action");
            carStateMachine.Fire(Car.Action.Stop);
            Console.WriteLine($"New State After Stop action invoke: {carStateMachine.State}");

            Console.WriteLine(carStateMachine.IsInState(Car.State.Running));
        }
    }
}