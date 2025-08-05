using Elastic.SourceGenerator.Roslyn.Helpers;
using Elastic.SourceGenerator.Roslyn.IncrementalTypes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Elastic.SourceGenerator.Roslyn.Models;

public sealed record DeclaredTypeModel
{
	public required string Name { get; init; }
	public required string? Namespace { get; init; }
	public required string FullyQualifiedName { get; init; }
	public required bool IsStatic { get; init; }
	public required bool IsAbstract { get; init; }
	public required bool IsSealed { get; init; }
	public required bool IsAccessibleFromOtherAssemblies { get; init; }
	public required ImmutableEquatableArray<string> DeclarationHeaders { get; init; }

	public static DeclaredTypeModel Create(BaseTypeDeclarationSyntax syntax, INamedTypeSymbol symbol)
	{
		return new DeclaredTypeModel
		{
			Name = symbol.Name,
			Namespace = symbol.ContainingNamespace.IsGlobalNamespace
				? null
				: symbol.ContainingNamespace.ToDisplayString(),
			FullyQualifiedName = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
			IsStatic = symbol.IsStatic,
			IsAbstract = symbol.IsAbstract,
			IsSealed = symbol.IsSealed,
			IsAccessibleFromOtherAssemblies = symbol.IsAccessibleFromOtherAssemblies(),
			DeclarationHeaders = RoslynHelpers.GetTypeDeclarationHeaders(syntax, symbol).ToImmutableEquatableArray()
		};
	}
}
