using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using Elastic.SourceGenerator.Roslyn.IncrementalTypes;
using Elastic.SourceGenerator.Roslyn.Models;

using Microsoft.CodeAnalysis;

namespace Elastic.SourceGenerator.Roslyn.Parser;

public sealed partial class ElasticsearchTypeParser :
	BaseParser
{
	private ElasticsearchTypeParser(ISymbol generationScope, ElasticsearchKnownSymbols knownSymbols, CancellationToken cancellationToken) :
		base(generationScope, knownSymbols, cancellationToken)
	{
	}

	public static ImmutableArray<ParserValue<ElasticsearchType>> ParseFromTreeNodeAttributes(ImmutableArray<GeneratorAttributeSyntaxContext> contexts,
		ElasticsearchKnownSymbols knownSymbols, CancellationToken cancellationToken)
	{
		var parser = new ElasticsearchTypeParser(knownSymbols.Compilation.Assembly, knownSymbols, cancellationToken);
		return parser.ParseAllAnnotatedTypes(contexts).ToImmutableArray();
	}

	public IEnumerable<ParserValue<ElasticsearchType>> ParseAllAnnotatedTypes(ImmutableArray<GeneratorAttributeSyntaxContext> contexts)
	{
		if (Debugger.IsAttached)
		{
			Debugger.Break();
		}

		var items = contexts.Select(ParseCandidate).ToArray();

		foreach (var item in items.Where(x => x.HasDiagnostics))
		{
			CancellationToken.ThrowIfCancellationRequested();
			yield return item.DiagnosticsBuilder!.ToDiagnostics();
		}

		foreach (var item in items.Where(x => x.HasValue).Select(x => x.Value!))
		{
			yield return new ElasticsearchInterfaceType
			{
				Type = DeclaredTypeModel.Create(item.Syntax, item.Symbol)
			};
		}
	}
}
