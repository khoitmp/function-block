namespace FunctionBlock;

internal interface ICompiler
{
    void AddMetadataReference(MetadataReference metadataReference);
    Assembly CompileToAssembly(string name, string code);
}