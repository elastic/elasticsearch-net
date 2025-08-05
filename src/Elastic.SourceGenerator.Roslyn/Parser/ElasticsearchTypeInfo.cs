using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Elastic.SourceGenerator.Roslyn.Parser;

internal sealed class ElasticsearchTypeInfo
{
	/// <summary>
	/// The named type symbol of the tree-node candidate.
	/// </summary>
	public required INamedTypeSymbol Symbol { get; init; }

	/// <summary>
	/// The declaration syntax of the tree-node candidate.
	/// </summary>
	public required BaseTypeDeclarationSyntax Syntax { get; init; }

	/// <summary>
	/// The base-type of the tree-node candidate or <see langword="null"/>, if it directly derives from <see cref="object"/>.
	/// </summary>
	public required INamedTypeSymbol? BaseType { get; init; }

	/// <summary>
	/// Additional properties of the <c>ElasticsearchType</c> attribute.
	/// </summary>
	public required ElasticsearchTypeAttribute Attribute { get; init; }
}
