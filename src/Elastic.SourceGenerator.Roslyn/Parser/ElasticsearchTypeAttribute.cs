namespace Elastic.SourceGenerator.Roslyn.Parser;

internal sealed class ElasticsearchTypeAttribute
{
	public string? Kind { get; init; }
	public bool? IsUsedInRequest { get; init; }
}
