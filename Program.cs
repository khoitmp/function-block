class Program
{
    static async Task Main(string[] args)
    {
        var functionBlocks = new List<FunctionBlockModel>()
        {
            new FunctionBlockModel("in1", "files/in1"),
            new FunctionBlockModel("in2", "files/in2"),
            new FunctionBlockModel("in3", "files/in3"),
            new FunctionBlockModel("in4", "files/in4"),
            new FunctionBlockModel("out1", "files/out1"),
            new FunctionBlockModel("out2", "files/out2"),
            new FunctionBlockModel("fb1", "files/fb1"),
            new FunctionBlockModel("fb2", "files/fb2"),
            new FunctionBlockModel("fb3", "files/fb3")
        };

        var connections = new List<ConnectionModel>
        {
            new ConnectionModel("in1", "fb1"),
            new ConnectionModel("in2", "fb1"),
            new ConnectionModel("in3", "fb2"),
            new ConnectionModel("in4", "fb2"),
            new ConnectionModel("in5", "fb3"),
            new ConnectionModel("fb1", "fb3"),
            new ConnectionModel("fb2", "fb3"),
            new ConnectionModel("fb3", "out1"),
            new ConnectionModel("fb3", "out2")
        };

        var blockBuilder = new BlockBuilder(functionBlocks);
        var graphBuilder = new GraphBuilder(connections);

        var instanceBlocks = blockBuilder.BuildInstances().GetInstances();
        var levelBlocks = graphBuilder.BuildGraph().GetGraph();

        foreach (var levelBlock in levelBlocks)
        {
            var tasks = instanceBlocks.Where(i => levelBlock.Contains(i.Id)).Select(i => i.ExecuteAsync()).ToList();
            await Task.WhenAll(tasks);
        }
    }
}