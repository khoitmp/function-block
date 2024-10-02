internal class GraphBuilder
{
    private readonly IEnumerable<ConnectionModel> _connections;
    private IList<List<string>> _levelBlocks;

    internal GraphBuilder(IEnumerable<ConnectionModel> connections)
    {
        _connections = connections;
        _levelBlocks = new List<List<string>>();
    }

    internal GraphBuilder BuildGraph()
    {
        var graph = new Dictionary<string, List<string>>();

        // To store how many inputs for a specific function block
        var inDegree = new Dictionary<string, int>();

        // Build the graph and in-degree counts
        foreach (var connection in _connections)
        {
            var from = connection.From;
            var to = connection.To;

            if (!graph.ContainsKey(from))
                graph[from] = new List<string>();

            graph[from].Add(to);

            if (!inDegree.ContainsKey(from))
                inDegree[from] = 0;

            if (!inDegree.ContainsKey(to))
                inDegree[to] = 0;

            inDegree[to]++;
        }

        // Prepare for BFS
        var currentLevel = new Queue<string>();

        // Enqueue nodes with zero in-degrees (Level 1)
        foreach (var node in inDegree.Keys)
        {
            if (inDegree[node] == 0)
                currentLevel.Enqueue(node);
        }

        // Process levels
        while (currentLevel.Count > 0)
        {
            var levelList = new List<string>();
            int levelSize = currentLevel.Count;

            for (int i = 0; i < levelSize; i++)
            {
                var node = currentLevel.Dequeue();

                levelList.Add(node);

                if (graph.ContainsKey(node))
                {
                    foreach (var neighbor in graph[node])
                    {
                        inDegree[neighbor]--;

                        // Blocks will be processed in the next level > 0
                        if (inDegree[neighbor] == 0)
                            currentLevel.Enqueue(neighbor);
                    }
                }
            }

            _levelBlocks.Add(levelList);
        }

        return this;
    }

    internal IList<List<string>> GetGraph()
    {
        return _levelBlocks;
    }
}