namespace MessageHandler.Runtime
{
    public interface IVariableSource
    {
        dynamic GetVariables(string scopeType, string scopeId);
    }
}