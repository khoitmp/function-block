internal class BlockBuilder
{
    private readonly IList<FunctionBlockModel> _functionBlocks;
    private IList<IFunction> _instances;
    private IVariable _variable;

    internal BlockBuilder(IList<FunctionBlockModel> functionBlocks)
    {
        _functionBlocks = functionBlocks;
        _variable = new Variable();
    }

    internal BlockBuilder BuildInstances()
    {
        var resolver = new Resolver();

        _instances = _functionBlocks.Select(f => resolver.CreateInstance(f.Id, f.Content)).ToList();

        foreach (var instance in _instances)
        {
            instance.SetVariable(_variable);
        }

        return this;
    }

    internal IList<IFunction> GetInstances()
    {
        return _instances;
    }

    internal IVariable GetVariable()
    {
        return _variable;
    }
}