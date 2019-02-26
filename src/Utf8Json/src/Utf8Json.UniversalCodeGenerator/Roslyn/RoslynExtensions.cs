using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Utf8Json.UniversalCodeGenerator
{
    // Utility and Extension methods for Roslyn
    internal static class RoslynExtensions
    {
        public static Compilation GetCompilationFromProject(IEnumerable<string> inputFiles, IEnumerable<string> inputDirectories, string[] preprocessorSymbols)
        {
            var parseOptions = new CSharpParseOptions(LanguageVersion.Default, DocumentationMode.Parse, SourceCodeKind.Regular, preprocessorSymbols ?? new string[0]);
            var syntaxTrees = new List<SyntaxTree>();
            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.Serialization.DataMemberAttribute).Assembly.Location),
            };

            foreach (var dir in inputDirectories ?? new string[0])
            {
                var files = Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var text = File.ReadAllText(file);
                    syntaxTrees.Add(CSharpSyntaxTree.ParseText(text, parseOptions));
                }
            }

            foreach (var path in inputFiles ?? new string[0])
            {
                if (path.EndsWith(".cs", StringComparison.Ordinal))
                {
                    var text = File.ReadAllText(path);
                    syntaxTrees.Add(CSharpSyntaxTree.ParseText(text, parseOptions));
                }
                else if (path.EndsWith(".dll", StringComparison.Ordinal))
                {
                    references.Add(MetadataReference.CreateFromFile(path));
                }
            }

            CSharpCompilation compilation = CSharpCompilation.Create(
                "Assembly-CSharp",
                syntaxTrees: syntaxTrees,
                references: references
            );
            return compilation;
        }

        public static IEnumerable<INamedTypeSymbol> GetNamedTypeSymbols(this Compilation compilation)
        {
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var semModel = compilation.GetSemanticModel(syntaxTree);

                foreach (var item in syntaxTree.GetRoot()
                    .DescendantNodes()
                    .Select(x => semModel.GetDeclaredSymbol(x))
                    .Where(x => x != null))
                {
                    var namedType = item as INamedTypeSymbol;
                    if (namedType != null)
                    {
                        yield return namedType;
                    }
                }
            }
        }

        public static IEnumerable<INamedTypeSymbol> EnumerateBaseType(this ITypeSymbol symbol)
        {
            var t = symbol.BaseType;
            while (t != null)
            {
                yield return t;
                t = t.BaseType;
            }
        }

        public static AttributeData FindAttribute(this IEnumerable<AttributeData> attributeDataList, string typeName)
        {
            return attributeDataList
                .Where(x => x.AttributeClass.ToDisplayString() == typeName)
                .FirstOrDefault();
        }

        public static AttributeData FindAttributeShortName(this IEnumerable<AttributeData> attributeDataList, string typeName)
        {
            return attributeDataList
                .Where(x => x.AttributeClass.Name == typeName || x.AttributeClass.Name == typeName + "Attribute")
                .FirstOrDefault();
        }

        public static AttributeData FindAttributeIncludeBasePropertyShortName(this IPropertySymbol property, string typeName)
        {
            do
            {
                var data = FindAttributeShortName(property.GetAttributes(), typeName);
                if (data != null) return data;
                property = property.OverriddenProperty;
            } while (property != null);

            return null;
        }

        public static AttributeSyntax FindAttribute(this BaseTypeDeclarationSyntax typeDeclaration, SemanticModel model, string typeName)
        {
            return typeDeclaration.AttributeLists
                .SelectMany(x => x.Attributes)
                .Where(x => model.GetTypeInfo(x).Type?.ToDisplayString() == typeName)
                .FirstOrDefault();
        }

        public static INamedTypeSymbol FindBaseTargetType(this ITypeSymbol symbol, string typeName)
        {
            return symbol.EnumerateBaseType()
                .Where(x => x.OriginalDefinition?.ToDisplayString() == typeName)
                .FirstOrDefault();
        }

        public static object GetSingleNamedArgumentValueFromSyntaxTree(this AttributeData attribute, string key)
        {
            if (attribute == null) return null;

            var ctxxx = attribute.ApplicationSyntaxReference.GetSyntax();
            foreach (var p in ctxxx.DescendantNodes().OfType<AttributeArgumentSyntax>())
            {
                if (p.NameEquals.Name.Identifier.ValueText == key)
                {
                    return (p.Expression as LiteralExpressionSyntax).Token.ValueText;
                }
            }

            return null;
        }

        public static bool IsNullable(this INamedTypeSymbol symbol)
        {
            if (symbol.IsGenericType)
            {
                if (symbol.ConstructUnboundGenericType().ToDisplayString() == "T?")
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<ISymbol> GetAllMembers(this ITypeSymbol symbol)
        {
            var t = symbol;
            while (t != null)
            {
                foreach (var item in t.GetMembers())
                {
                    yield return item;
                }
                t = t.BaseType;
            }
        }

        public static IEnumerable<ISymbol> GetAllInterfaceMembers(this ITypeSymbol symbol)
        {
            return symbol.GetMembers()
                .Concat(symbol.AllInterfaces.SelectMany(x => x.GetMembers()));
        }
    }
}
