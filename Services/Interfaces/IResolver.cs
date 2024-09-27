namespace FunctionBlock;

internal interface IResolver
{
    public IFunction CreateInstance(string content);
}