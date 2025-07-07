using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace UniDAL.Core.Repositories
{
    /// <summary>
    /// Генератор репозиториев
    /// </summary>
    [Generator]
    public class RepositoryGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SyntaxReceiver receiver)
                return;

            foreach (var classDecl in receiver.EntityClasses)
            {
                var model = context.Compilation.GetSemanticModel(classDecl.SyntaxTree);
                var symbol = model.GetDeclaredSymbol(classDecl) as INamedTypeSymbol;

                if (symbol == null || !symbol.Interfaces.Any(i => i.Name == "IEntity"))
                    continue;

                var keyType = symbol.Interfaces
                    .First(i => i.Name == "IEntity")
                    .TypeArguments[0]
                    .ToDisplayString();

                var source = GenerateRepositoryClass(symbol, keyType);
                context.AddSource($"{symbol.Name}Repository.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }

        /// <summary>
        /// Генерация класса репозитория
        /// </summary>
        /// <param name="symbol">Символ</param>
        /// <param name="keyType">Тип ключа</param>
        /// <returns>Сгенерированный класс в виде строки</returns>
        private string GenerateRepositoryClass(INamedTypeSymbol symbol, string keyType)
        {
            var namespaceName = symbol.ContainingNamespace.ToDisplayString();
            var entityName = symbol.Name;
            var tableName = symbol.GetAttributes()
                .FirstOrDefault(a => a.AttributeClass?.Name == "TableAttribute")?
                .ConstructorArguments[0].Value?.ToString() ?? entityName;

            return $@"
                using System.Linq;
                using {namespaceName};

                namespace UniDAL.Generated
                {{
                    public sealed class {entityName}Repository : BaseRepository<{entityName}, {keyType}, IDbContext>
                    {{
                        public {entityName}Repository(IDbContext context) : base(context) {{ }}

                        public override {entityName} GetById({keyType} id)
                        {{
                            // Автогенерированная реализация
                            return Context.Set<{entityName}>().Find(id);
                        }}

                        // Другие методы...
                    }}
                }}";
        }

        private class SyntaxReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax> EntityClasses { get; } = new();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax classDecl &&
                    classDecl.BaseList?.Types.Any(t => t.Type.ToString().Contains("IEntity")) == true)
                {
                    EntityClasses.Add(classDecl);
                }
            }
        }
    }
}
