using Microsoft.CodeAnalysis;

namespace Elastic.SourceGenerator.Roslyn.Parser;

public sealed partial class ElasticsearchTypeParser
{
	private const string DiagnosticsCategory = "Elastic.SourceGenerator";

	private static DiagnosticDescriptor TypeNotPartial { get; } = new DiagnosticDescriptor(
		id: "ES0001",
		title: $"Type annotated with '{ElasticsearchKnownSymbols.ElasticsearchTypeAttributeName}' is not partial.",
		messageFormat: $"The type '{{0}}' has been annotated with '{ElasticsearchKnownSymbols.ElasticsearchTypeAttributeName}' but it or one of its parent types are not partial.",
		category: DiagnosticsCategory,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	private static DiagnosticDescriptor TypeIsStatic { get; } = new DiagnosticDescriptor(
		id: "ES0002",
		title: $"Types annotated with '{ElasticsearchKnownSymbols.ElasticsearchTypeAttributeName}' cannot be static.",
		messageFormat: $"The type '{{0}}' that has been annotated with '{ElasticsearchKnownSymbols.ElasticsearchTypeAttributeName}' cannot be static.",
		category: DiagnosticsCategory,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true);
}
