namespace MessageHandler.Runtime
{
    public interface IVariableSource
    {
        object GetVariables(VariableScope scopeType, string scopeId);
    }
}