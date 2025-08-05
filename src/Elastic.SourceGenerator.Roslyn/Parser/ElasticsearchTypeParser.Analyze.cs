using System.Diagnostics;
using System.Linq;

using Elastic.SourceGenerator.Roslyn.Helpers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Elastic.SourceGenerator.Roslyn.Parser;

public sealed partial class ElasticsearchTypeParser
{
	private Result<ElasticsearchTypeInfo> ParseCandidate(GeneratorAttributeSyntaxContext context)
	{
		Debug.Assert(context.TargetSymbol is INamedTypeSymbol { TypeKind: TypeKind.Class or TypeKind.Struct or TypeKind.Enum });
		Debug.Assert(context.TargetNode is BaseTypeDeclarationSyntax);

		var symbol = (INamedTypeSymbol)context.TargetSymbol;
		var syntax = (BaseTypeDeclarationSyntax)context.TargetNode;
		var attribute = context.Attributes.Single(x => SymbolComparer.Equals(x.AttributeClass, KnownSymbols.ElasticsearchTypeAttribute));

		return ParseCandidate(symbol, syntax, attribute, context.SemanticModel);
	}

	private Result<ElasticsearchTypeInfo> ParseCandidate(INamedTypeSymbol symbol, BaseTypeDeclarationSyntax syntax, AttributeData attribute, SemanticModel semanticModel)
	{
		CancellationToken.ThrowIfCancellationRequested();

		var result = CreateOptional<ElasticsearchTypeInfo>();

		var isPartialHierarchy = (syntax is EnumDeclarationSyntax) || RoslynHelpers.IsPartialHierarchy(syntax);
		if (!isPartialHierarchy)
		{
			result.AddDiagnostic(TypeNotPartial, syntax.Identifier.GetLocation(), symbol.ToDisplayString());
		}

		if (symbol.IsStatic)
		{
			result.AddDiagnostic(TypeIsStatic, syntax.Identifier.GetLocation(), symbol.ToDisplayString());
		}

		if (result.HasDiagnostics)
		{
			return result;
		}

		var baseType = (symbol.BaseType is { SpecialType: not SpecialType.System_Object } bt) ? bt : null;
		var parsedAttribute = ParseTreeNodeAttribute(result, syntax, attribute);

		result.Value = new ElasticsearchTypeInfo
		{
			Symbol = symbol,
			Syntax = syntax,
			BaseType = baseType,
			Attribute = parsedAttribute
		};

		return result;
	}

	private ElasticsearchTypeAttribute ParseTreeNodeAttribute(IDiagnosticsBuilder diagnostics, BaseTypeDeclarationSyntax nodeSyntax, AttributeData attribute)
	{
		return new ElasticsearchTypeAttribute();

		//var namedArguments = attribute.NamedArguments.ToDictionary(k => k.Key, v => v.Value);

		//var generateFactory = true;
		//DeclaredType? factory = null;
		//if (TryGetTypeProperty("Factory", out var factorySymbol))
		//{
		//    factory = ParseFactoryType(factorySymbol);
		//    generateFactory = (factory is not null);
		//}

		//var generateVisitor = true;
		//DeclaredType? visitor = null;
		//if (TryGetTypeProperty("Visitor", out var visitorSymbol))
		//{
		//    visitor = ParseVisitorType(visitorSymbol);
		//    generateVisitor = (visitor is not null);
		//}

		//return new TreeNodeAttribute
		//{
		//    Name = ParseNameProperty(),
		//    GenerateFactory = generateFactory,
		//    Factory = factory,
		//    GenerateVisitor = generateVisitor,
		//    Visitor = visitor
		//};

		//string? ParseNameProperty()
		//{
		//    if (!namedArguments.TryGetValue("Name", out var v) || (v.Value is not string s))
		//    {
		//        return null;
		//    }

		//    if (!SyntaxFacts.IsValidIdentifier(s))
		//    {
		//        AddDiagnostic(NameMustBeIdentifier);
		//        return null;
		//    }

		//    return s.ToPascalCase();
		//}

		//bool TryGetTypeProperty(string name, out INamedTypeSymbol? value)
		//{
		//    value = null;

		//    if (!namedArguments.TryGetValue(name, out var v))
		//    {
		//        return false;
		//    }

		//    value = (v.Kind is TypedConstantKind.Type) ? (INamedTypeSymbol?)v.Value : null;

		//    return true;
		//}

		//DeclaredType? ParseFactoryType(INamedTypeSymbol? symbol)
		//{
		//    if (symbol is null || (symbol.Kind is SymbolKind.ErrorType))
		//    {
		//        return null;
		//    }

		//    var hasError = false;

		//    if (symbol.DeclaringSyntaxReferences.Length is 0)
		//    {
		//        AddDiagnostic(FactoryNotDeclaredInSource, symbol.ToDisplayString());
		//        return null;
		//    }

		//    if (symbol.DeclaringSyntaxReferences[0].GetSyntax(CancellationToken) is not ClassDeclarationSyntax syntax)
		//    {
		//        AddDiagnostic(FactoryNotClass, symbol.ToDisplayString());
		//        return null;
		//    }

		//    if (!RoslynHelpers.IsPartialHierarchy(syntax))
		//    {
		//        hasError = true;
		//        AddDiagnostic(FactoryNotPartial, symbol.ToDisplayString());
		//    }

		//    if (!symbol.IsStatic)
		//    {
		//        hasError = true;
		//        AddDiagnostic(FactoryNotStatic, symbol.ToDisplayString());
		//    }

		//    return hasError ? null : new DeclaredType { Symbol = symbol, Syntax = syntax };
		//}

		//DeclaredType? ParseVisitorType(INamedTypeSymbol? symbol)
		//{
		//    if (symbol is null || (symbol.Kind is SymbolKind.ErrorType))
		//    {
		//        return null;
		//    }

		//    var hasError = false;

		//    if (symbol.DeclaringSyntaxReferences.Length is 0)
		//    {
		//        AddDiagnostic(VisitorNotDeclaredInSource, symbol.ToDisplayString());
		//        return null;
		//    }

		//    if (symbol.DeclaringSyntaxReferences[0].GetSyntax(CancellationToken) is not ClassDeclarationSyntax syntax)
		//    {
		//        AddDiagnostic(VisitorNotClass, symbol.ToDisplayString());
		//        return null;
		//    }

		//    if (!RoslynHelpers.IsPartialHierarchy(syntax))
		//    {
		//        hasError = true;
		//        AddDiagnostic(VisitorNotPartial, symbol.ToDisplayString());
		//    }

		//    if (!symbol.IsAbstract)
		//    {
		//        hasError = true;
		//        AddDiagnostic(VisitorNotAbstract, symbol.ToDisplayString());
		//    }

		//    if (symbol.IsStatic)
		//    {
		//        hasError = true;
		//        AddDiagnostic(VisitorIsStatic, symbol.ToDisplayString());
		//    }

		//    return hasError ? null : new DeclaredType { Symbol = symbol, Syntax = syntax };
		//}

		//void AddDiagnostic(DiagnosticDescriptor descriptor, params object?[] messageArgs)
		//{
		//    var attributeSyntax = attribute.ApplicationSyntaxReference?.GetSyntax(CancellationToken) ?? nodeSyntax;
		//    diagnostics.AddDiagnostic(descriptor, attributeSyntax?.GetLocation(), messageArgs);
		//}
	}
}
