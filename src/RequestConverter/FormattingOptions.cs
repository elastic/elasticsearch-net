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
