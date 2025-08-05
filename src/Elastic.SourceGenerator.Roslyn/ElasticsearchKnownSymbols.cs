using Microsoft.CodeAnalysis;

namespace Elastic.SourceGenerator.Roslyn;

public sealed class ElasticsearchKnownSymbols(Compilation compilation) :
	KnownSymbols(compilation)
{
	public const string ElasticsearchTypeAttributeName = "Elastic.Clients.Elasticsearch.SourceGeneration.ElasticsearchTypeAttribute";

	public INamedTypeSymbol? ElasticsearchTypeAttribute => GetOrResolveType(ElasticsearchTypeAttributeName, ref _elasticsearchTypeAttribute);
	private Option<INamedTypeSymbol?> _elasticsearchTypeAttribute;

	public INamedTypeSymbol? ElasticsearchTypeKind => GetOrResolveType("Elastic.Clients.Elasticsearch.SourceGeneration.ElasticsearchTypeKind", ref _elasticsearchTypeKind);
	private Option<INamedTypeSymbol?> _elasticsearchTypeKind;
}
