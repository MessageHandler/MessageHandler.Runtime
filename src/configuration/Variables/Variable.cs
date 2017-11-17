namespace MessageHandler.Runtime
{
    public class Variable
    {
        public string OwnerIdentifier { get; set; }
        public string ScopeIdentifier { get; set; }
        public string ScopeType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string FullName { get; set; }
    }
}