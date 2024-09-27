namespace FunctionBlock;

internal interface IFunction
{
    void SetVariable(IVariable variable);
    Task ExecuteAsync();
}