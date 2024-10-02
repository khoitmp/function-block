namespace FunctionBlock;

/// <summary>
/// TODO: Cache the assembly
/// </summary>
internal class Resolver : IResolver
{
    public IFunction CreateInstance(string id, string content)
    {
        var language = new Language();
        var compiler = new Compiler(language);

        var template = @$"
                            using System;
                            using System.Linq;
                            using System.Globalization;
                            using System.Threading.Tasks;
                            using System.Collections.Generic;

                            // Same namespace with the runtime services
                            namespace FunctionBlock;
                            
                            // IFunction
                            public class FunctionWrapper : BaseFunction
                            {{
                                public override async Task ExecuteAsync()
                                {{
                                    {content}
                                }}
                            }}
                        ";

        var assembly = compiler.CompileToAssembly(id, template);
        var instance = CreateIntance(assembly);

        instance.SetId(id);

        return instance;
    }

    private static IFunction CreateIntance(Assembly assembly)
    {
        var type = assembly.GetTypes().FirstOrDefault(t => typeof(IFunction).IsAssignableFrom(t));
        return Activator.CreateInstance(type) as IFunction;
    }
}