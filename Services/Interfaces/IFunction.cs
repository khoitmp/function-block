namespace FunctionBlock;

internal interface IFunction
{
    public string Id { get; }
    void SetId(string id);
    void SetVariable(IVariable variable);
    Task ExecuteAsync();
}