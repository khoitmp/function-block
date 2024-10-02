namespace FunctionBlock;

public abstract class BaseFunction : IFunction
{
    private IVariable _variable;
    protected IVariable fb => _variable;

    public string Id { get; private set; }

    public void SetId(string id)
    {
        Id = id;
    }

    public void SetVariable(IVariable variable)
    {
        _variable = variable;
    }

    public abstract Task ExecuteAsync();
}