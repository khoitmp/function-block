namespace FunctionBlock;

internal interface IResolver
{
    public IFunction CreateInstance(string name, string content);
}