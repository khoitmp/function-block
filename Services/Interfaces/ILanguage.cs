namespace FunctionBlock;

internal interface ILanguage
{
    SyntaxTree ParseText(string sourceCode, SourceCodeKind kind);
    Compilation CreateLibraryCompilation(string assemblyName, bool enableOptimisations);
}