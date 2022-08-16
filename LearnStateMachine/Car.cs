namespace LearnStateMachine
{
    public class Car
    {
        public enum State 
        {
            Stopped,
            Started,
            Running
        }
        public enum Action 
        {
            Stop,
            Start,
            Accelerate
        }
    }
}
