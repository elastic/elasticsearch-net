using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Elastic.SourceGenerator.Roslyn.Helpers;

public static class RoslynHelpers
{
	public static bool IsGenericTypeDefinition(this ITypeSymbol type)
	{
		return type is INamedTypeSymbol { IsGenericType: true } namedType &&
			   SymbolEqualityComparer.Default.Equals(namedType.OriginalDefinition, type);
	}

	public static bool ContainsLocation(this Compilation compilation, Location location)
	{
		return (location.SourceTree is not null) && compilation.ContainsSyntaxTree(location.SourceTree);
	}

	/// <summary>
	/// Get a location object that doesn't capture a reference to Compilation.
	/// </summary>
	public static Location GetLocationTrimmed(this Location location)
	{
		return Location.Create(location.SourceTree?.FilePath ?? string.Empty, location.SourceSpan, location.GetLineSpan().Span);
	}

	/// <summary>
	/// Returns the kind keyword corresponding to the specified declaration syntax node.
	/// </summary>
	public static string GetTypeKindKeyword(this BaseTypeDeclarationSyntax typeDeclaration)
	{
		return typeDeclaration.Kind() switch
		{
			SyntaxKind.ClassDeclaration => "class",
			SyntaxKind.InterfaceDeclaration => "interface",
			SyntaxKind.StructDeclaration => "struct",
			SyntaxKind.RecordDeclaration => "record",
			SyntaxKind.RecordStructDeclaration => "record struct",
			SyntaxKind.EnumDeclaration => "enum",
			SyntaxKind.DelegateDeclaration => "delegate",
			_ => throw new InvalidOperationException()
		};
	}

	public static string FormatTypeDeclarationHeader(BaseTypeDeclarationSyntax typeDeclaration, ITypeSymbol typeSymbol)
	{
		var sb = new StringBuilder();

		foreach (var modifier in typeDeclaration.Modifiers)
		{
			sb.Append(modifier.Text);
			sb.Append(' ');
		}

		sb.Append(typeDeclaration.GetTypeKindKeyword());
		sb.Append(' ');

		var typeName = typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
		sb.Append(typeName);

		return sb.ToString();
	}

	public static string[] GetTypeDeclarationHeaders(BaseTypeDeclarationSyntax declarationSyntax, SemanticModel semanticModel, CancellationToken cancellationToken)
	{
		Stack<string> result = new();

		for (var declaration = declarationSyntax; declaration is not null; declaration = declaration.Parent as BaseTypeDeclarationSyntax)
		{
			ITypeSymbol symbol = semanticModel.GetDeclaredSymbol(declaration, cancellationToken)!;
			var header = FormatTypeDeclarationHeader(declaration, symbol);
			result.Push(header);
		}

		return result.ToArray();
	}

	public static string[] GetTypeDeclarationHeaders(BaseTypeDeclarationSyntax syntax, INamedTypeSymbol symbol)
	{
		Stack<string> result = new();

		var current = syntax;
		while (current is not null)
		{
			var header = FormatTypeDeclarationHeader(current, symbol);
			result.Push(header);

			current = current.Parent as BaseTypeDeclarationSyntax;
			symbol = symbol.ContainingType;
		}

		return result.ToArray();
	}

	public static bool IsPartialHierarchy(BaseTypeDeclarationSyntax declarationSyntax)
	{
		var result = true;

		var current = declarationSyntax;
		while (result && current is not null)
		{
			result &= current.Modifiers.Any(SyntaxKind.PartialKeyword);
			current = current.Parent as BaseTypeDeclarationSyntax;
		}

		return result;
	}

	public static bool IsAccessibleFromOtherAssemblies(this INamedTypeSymbol symbol)
	{
		var result = IsAccessible(symbol.DeclaredAccessibility);

		while (symbol.ContainingType is not null)
		{
			result &= IsAccessible(symbol.ContainingType.DeclaredAccessibility);
			symbol = symbol.ContainingType;
		}

		return result;

		static bool IsAccessible(Accessibility accessibility)
		{
			return accessibility is Accessibility.Protected or Accessibility.ProtectedOrInternal or Accessibility.Public;
		}
	}

	public static string GetAccessibilityString(this Accessibility accessibility)
	{
		return accessibility switch
		{
			Accessibility.NotApplicable => string.Empty,
			Accessibility.Private => "private",
			Accessibility.ProtectedAndInternal => "private protected",
			Accessibility.Protected => "protected",
			Accessibility.Internal => "internal",
			Accessibility.ProtectedOrInternal => "protected internal",
			Accessibility.Public => "public",
			_ => throw new ArgumentOutOfRangeException(nameof(accessibility), accessibility, null)
		};
	}
}
