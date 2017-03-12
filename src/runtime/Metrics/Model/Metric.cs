namespace MessageHandler.Runtime
{
    public class Metric
    {
        public double Value { get; private set; }
        public string Name { get; private set; }
        public Metric(string name, double value)
        {
            Value = value;
            Name = name;
        }
    }
}