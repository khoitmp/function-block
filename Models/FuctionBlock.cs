public class FunctionBlockModel
{
    public string Id { get; set; }
    public string Content { get; set; }
    public string Path { get; set; }

    public FunctionBlockModel(string id, string path)
    {
        Id = id;
        Path = path;

        ReadContentAsync().Wait();
    }


    private async Task ReadContentAsync()
    {
        var content = await File.ReadAllTextAsync(Path);
        Content = content;
    }
}