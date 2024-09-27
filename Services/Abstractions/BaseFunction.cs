namespace FunctionBlock;

public abstract class BaseFunction : IFunction
{
    private IVariable _variable;
    protected IVariable fb => _variable;

    public void SetVariable(IVariable variable)
    {
        _variable = variable;
    }

    public abstract Task ExecuteAsync();
}