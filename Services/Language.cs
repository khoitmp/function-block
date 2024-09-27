namespace FunctionBlock;

internal class Language : ILanguage
{
    private static readonly LanguageVersion MaxLanguageVersion = Enum.GetValues(typeof(LanguageVersion))
                                                                        .Cast<LanguageVersion>()
                                                                        .Max();
    private readonly IReadOnlyCollection<MetadataReference> _references = new[] {
        MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location),
        MetadataReference.CreateFromFile(typeof(ValueTuple<>).GetTypeInfo().Assembly.Location)
    };

    public SyntaxTree ParseText(string sourceCode, SourceCodeKind kind)
    {
        var options = new CSharpParseOptions(kind: kind, languageVersion: MaxLanguageVersion);

        return CSharpSyntaxTree.ParseText(sourceCode, options);
    }

    public Compilation CreateLibraryCompilation(string assemblyName, bool enableOptimisations)
    {
        var options = new CSharpCompilationOptions(
            OutputKind.DynamicallyLinkedLibrary,
            optimizationLevel: enableOptimisations ? OptimizationLevel.Release : OptimizationLevel.Debug,
            allowUnsafe: true
        );

        return CSharpCompilation.Create(assemblyName, options: options, references: _references);
    }
}