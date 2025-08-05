using System.Diagnostics;
using System.Reflection;

using Elastic.SourceGenerator.Roslyn;
using Elastic.SourceGenerator.Roslyn.IncrementalTypes;
using Elastic.SourceGenerator.Roslyn.Models;
using Elastic.SourceGenerator.Roslyn.Parser;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RequestConverter.SourceGenerator;

[Generator]
internal sealed class SourceGenerator :
	IIncrementalGenerator
{
	public static string SourceGeneratorName { get; } = typeof(SourceGenerator).FullName!;
	public static string SourceGeneratorVersion { get; } = typeof(SourceGenerator).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "0.0.0.0";

	public SourceGenerator()
	{
#if DEBUG
		if (!Debugger.IsAttached)
		{
			Debugger.Launch();
		}
#endif
	}

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var knownSymbols = context.CompilationProvider
			.Select((compilation, _) => new ElasticsearchKnownSymbols(compilation));

		//context.SyntaxProvider
		//	.ForAttributeWithMetadataName(
		//		ElasticsearchKnownSymbols.ElasticsearchTypeAttributeName,
		//		predicate: static (node, _) => node is ClassDeclarationSyntax or StructDeclarationSyntax or RecordDeclarationSyntax or EnumDeclarationSyntax,
		//		transform: static (context, _) => context)
		//	.con

		var trees = context.SyntaxProvider
			.ForAttributeWithMetadataName(
				ElasticsearchKnownSymbols.ElasticsearchTypeAttributeName,
				predicate: static (node, _) => node is ClassDeclarationSyntax or StructDeclarationSyntax or RecordDeclarationSyntax,
				transform: static (context, _) => context)
			.Collect()
			.Combine(knownSymbols)
			.Select((tuple, cancellationToken) => ElasticsearchTypeParser.ParseFromTreeNodeAttributes(tuple.Left, tuple.Right, cancellationToken))
			.SelectMany((values, _) => values);

		context.RegisterSourceOutput(trees, GenerateSource);
	}

	private static void GenerateSource(SourceProductionContext context, ParserValue<ElasticsearchType> model)
	{
		foreach (var diagnostic in model.Diagnostics)
		{
			context.ReportDiagnostic(diagnostic.CreateDiagnostic());
		}

		if (!model.HasValue)
		{
			return;
		}

		SourceFormatter.GenerateSourceFiles(context, model.Value!);
	}
}
