class Program
{
    static async Task Main(string[] args)
    {
        var functionBlocks = new List<FunctionBlockModel>()
        {
            new FunctionBlockModel("fb1", "files/fb1"),
            new FunctionBlockModel("fb2", "files/fb2"),
            new FunctionBlockModel("fb3", "files/fb3")
        };

        var resolver = new Resolver();

        var instances = functionBlocks.Select(f => resolver.CreateInstance(f.Content));

        var variable = new Variable();

        foreach (var instance in instances)
        {
            instance.SetVariable(variable);
            await instance.ExecuteAsync();
        }
    }
}