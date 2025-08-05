namespace Elastic.SourceGenerator.Roslyn.Models;

public abstract record ElasticsearchType
{
	public required DeclaredTypeModel Type { get; init; }
}
