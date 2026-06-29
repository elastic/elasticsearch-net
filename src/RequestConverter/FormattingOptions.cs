// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace RequestConverter;

/// <summary>
/// Controls how <see cref="CodeWriter"/> renders generated C# code.
/// </summary>
public sealed record FormattingOptions
{
	/// <summary>The default options: four-space indentation, <c>\n</c> line endings, object-initializer syntax.</summary>
	public static FormattingOptions Default { get; } = new();

	/// <summary>The string used for a single level of indentation. Defaults to four spaces.</summary>
	public string IndentString { get; init; } = "    ";

	/// <summary>The line ending appended by <see cref="CodeWriter.WriteLine(string)"/>. Defaults to <c>\n</c>.</summary>
	public string NewLine { get; init; } = "\n";

	/// <summary>Which C# syntax flavor to emit. Only <see cref="SyntaxMode.ObjectInitializer"/> is implemented today.</summary>
	public SyntaxMode SyntaxMode { get; init; } = SyntaxMode.ObjectInitializer;

	/// <summary>How object initializers name their constructor. Defaults to target-typed <c>new()</c>.</summary>
	public ConstructorStyle ConstructorStyle { get; init; } = ConstructorStyle.TargetTyped;

	/// <summary>How type names are rendered. Defaults to <see cref="TypeNameStyle.Simplified"/> (short identifiers,
	/// relying on the using directives the caller adds from the returned namespaces).</summary>
	public TypeNameStyle TypeNameStyle { get; init; } = TypeNameStyle.Simplified;
}

/// <summary>
/// Controls how <see cref="CodeWriter"/> spells type names. The accompanying set of referenced namespaces is reported
/// regardless of the style, so a caller using <see cref="Simplified"/> can add the matching <c>using</c> directives.
/// </summary>
public enum TypeNameStyle
{
	/// <summary>
	/// Short identifiers (e.g. <c>Dictionary&lt;string, Aggregation&gt;</c>), assuming the referenced namespaces are
	/// imported. A name that is ambiguous against the imported set falls back to its <see cref="GlobalFqn"/> form so the
	/// output always compiles.
	/// </summary>
	Simplified = 0,

	/// <summary>Fully-qualified names without the <c>global::</c> prefix (e.g. <c>System.Collections.Generic.Dictionary&lt;...&gt;</c>).</summary>
	Fqn = 1,

	/// <summary>Fully-qualified names with the <c>global::</c> prefix (e.g. <c>global::System.Collections.Generic.Dictionary&lt;...&gt;</c>).</summary>
	GlobalFqn = 2
}

/// <summary>
/// Controls how <see cref="CodeWriter.BeginObjectInitializer(string)"/> renders the constructor.
/// </summary>
public enum ConstructorStyle
{
	/// <summary>Target-typed, e.g. <c>new() { ... }</c> (relies on the assignment target's type).</summary>
	TargetTyped = 0,

	/// <summary>Explicit, e.g. <c>new SearchRequest() { ... }</c> (self-contained, given the right usings).</summary>
	Explicit = 1
}

/// <summary>
/// The C# syntax flavor emitted by the request converter.
/// </summary>
public enum SyntaxMode
{
	/// <summary>Object-initializer syntax, e.g. <c>new() { Prop = value }</c>.</summary>
	ObjectInitializer = 0,

	// TODO(WS7): Descriptor (fluent lambda) syntax, e.g. `.Query(q => q.Match(...))`.
	// Reserved so the emitter/writer can branch on syntax mode without an API change later.
}
