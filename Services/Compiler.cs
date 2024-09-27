namespace FunctionBlock;

internal class Compiler : ICompiler
{
    private readonly ICollection<MetadataReference> _references;
    private readonly ILanguage _language;

    public Compiler(ILanguage language)
    {
        _language = language;

        var assemblies = new List<string>();

        // Package references
        assemblies.Add("System");
        assemblies.Add("System.Console");
        assemblies.Add("System.Runtime");
        assemblies.Add("System.Linq");
        assemblies.Add("System.Linq.Expressions");
        assemblies.Add("System.Collections");
        assemblies.Add("System.ComponentModel");
        assemblies.Add("System.Text.RegularExpressions");
        assemblies.Add("netstandard");
        assemblies.Add("FunctionBlock");

        _references = assemblies.Select(x => MetadataReference.CreateFromFile(Assembly.Load(x).Location) as MetadataReference).ToList();
    }

    public void AddMetadataReference(MetadataReference metadataReference)
    {
        _references.Add(metadataReference);
    }

    public Assembly CompileToAssembly(string name, string code)
    {
        SyntaxTree syntaxTree = _language.ParseText(code, SourceCodeKind.Regular);

        Compilation compilation = _language.CreateLibraryCompilation(assemblyName: name, enableOptimisations: true)
                                            .AddReferences(_references)
                                            .AddSyntaxTrees(syntaxTree);

        using (var peStream = new MemoryStream())
        using (var pdbStream = new MemoryStream())
        {
            var emitResult = compilation.Emit(peStream, pdbStream);
            if (emitResult.Success)
            {
                return Assembly.Load(peStream.ToArray(), pdbStream.ToArray());
            }
            else
            {
                throw new Exception(string.Join(Environment.NewLine, emitResult.Diagnostics.ToList().Select(d => d.ToString())));
            }
        }
    }
}