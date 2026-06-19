// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace RequestConverter;

/// <summary>
/// An indentation-aware writer that <see cref="ICodeFormattable"/> implementations use to
/// recursively emit C# source code for a materialized request. Replaces the previous bare
/// <see cref="StringBuilder"/> based approach and centralizes value dispatch, string escaping,
/// indentation and object-initializer rendering.
/// </summary>
public sealed class CodeWriter
{
	private readonly StringBuilder _builder = new();
	private int _indentLevel;
	private bool _atLineStart = true;

	public CodeWriter(FormattingOptions? options = null) => Options = options ?? FormattingOptions.Default;

	public FormattingOptions Options { get; }

	// ---- raw output -------------------------------------------------------

	/// <summary>Writes raw text, prefixing the current indentation if at the start of a line.</summary>
	public CodeWriter Write(string? text)
	{
		if (string.IsNullOrEmpty(text))
			return this;

		WriteIndentIfNeeded();
		_builder.Append(text);
		return this;
	}

	/// <summary>Writes a single raw character, prefixing the current indentation if at the start of a line.</summary>
	public CodeWriter Write(char value)
	{
		WriteIndentIfNeeded();
		_builder.Append(value);
		return this;
	}

	/// <summary>Appends the configured line ending (optionally preceded by <paramref name="text"/>).</summary>
	public CodeWriter WriteLine(string? text = null)
	{
		if (!string.IsNullOrEmpty(text))
			Write(text);

		_builder.Append(Options.NewLine);
		_atLineStart = true;
		return this;
	}

	/// <summary>Writes a quoted, escaped C# string literal, or <c>null</c> when <paramref name="value"/> is null.</summary>
	public CodeWriter WriteString(string? value)
	{
		if (value is null)
			return Write("null");

		WriteIndentIfNeeded();
		_builder.Append('"');
		AppendEscaped(value);
		_builder.Append('"');
		return this;
	}

	// ---- StringBuilder-style convenience ----------------------------------

	/// <summary>Appends raw text (alias for <see cref="Write(string)"/>); convenient for hand-crafted formatters.</summary>
	public CodeWriter Append(string? text) => Write(text);

	/// <summary>Appends a raw character (alias for <see cref="Write(char)"/>).</summary>
	public CodeWriter Append(char value) => Write(value);

	/// <summary>Appends the C# representation of a value (alias for <see cref="WriteValue(object?)"/>).</summary>
	public CodeWriter Append(object? value) => WriteValue(value);

	// ---- indentation ------------------------------------------------------

	/// <summary>Increases the indentation level until the returned scope is disposed.</summary>
	public IndentScope Indent()
	{
		_indentLevel++;
		return new IndentScope(this);
	}

	private void Dedent()
	{
		if (_indentLevel > 0)
			_indentLevel--;
	}

	private void WriteIndentIfNeeded()
	{
		if (!_atLineStart)
			return;

		for (var i = 0; i < _indentLevel; i++)
			_builder.Append(Options.IndentString);

		_atLineStart = false;
	}

	// ---- value dispatch ---------------------------------------------------

	/// <summary>
	/// Writes the C# representation of <paramref name="value"/>: delegates to
	/// <see cref="ICodeFormattable"/>, renders <see cref="JsonElement"/> structurally, quotes strings,
	/// emits boolean/numeric literals, and falls back to an invariant-culture <c>ToString</c>.
	/// </summary>
	public CodeWriter WriteValue(object? value)
	{
		switch (value)
		{
			case null:
				return Write("null");
			case ICodeFormattable formattable:
				formattable.FormatCode(this);
				return this;
			case JsonElement jsonElement:
				// Arbitrary JSON can't be represented as a C# anonymous object (keys may be C#
				// keywords or non-identifiers, and the target is a JsonElement). Parse the raw JSON,
				// which compiles and round-trips exactly.
				return Write("global::System.Text.Json.JsonSerializer.Deserialize<global::System.Text.Json.JsonElement>(")
					.WriteString(jsonElement.GetRawText())
					.Write(")");
			case string s:
				return WriteString(s);
			case bool b:
				return Write(b ? "true" : "false");
			case DateTimeOffset dateTimeOffset:
				return Write("global::System.DateTimeOffset.Parse(")
					.WriteString(dateTimeOffset.ToString("O", CultureInfo.InvariantCulture))
					.Write(", global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.RoundtripKind)");
			case DateTime dateTime:
				return Write("global::System.DateTime.Parse(")
					.WriteString(dateTime.ToString("O", CultureInfo.InvariantCulture))
					.Write(", global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.RoundtripKind)");
			default:
				return WritePrimitive(value);
		}
	}

	/// <inheritdoc cref="WriteValue(object?)"/>
	public CodeWriter WriteValue<T>(T value) => WriteValue((object?)value);

	private CodeWriter WritePrimitive(object value)
	{
		WriteIndentIfNeeded();
		_builder.Append(value is IFormattable formattable
			? formattable.ToString(null, CultureInfo.InvariantCulture)
			: value.ToString());
		return this;
	}

	// ---- object initializers ---------------------------------------------

	/// <summary>
	/// Begins an object-initializer block (e.g. <c>new() { ... }</c>). Use the returned
	/// <see cref="ObjectInitializer"/> to add properties; the brace block and indentation are only
	/// emitted once a property is added, so a property-less object renders as just the constructor.
	/// </summary>
	public ObjectInitializer BeginObjectInitializer(string? typeName = null)
	{
		if (Options.ConstructorStyle == ConstructorStyle.Explicit && !string.IsNullOrEmpty(typeName))
		{
			Write("new ").Write(typeName).Write("()");
		}
		else
		{
			Write("new()");
		}

		return new ObjectInitializer(this);
	}

	// ---- collections ------------------------------------------------------

	/// <summary>Writes an inline, delimited list, e.g. <c>[a, b, c]</c>.</summary>
	public CodeWriter WriteInlineList<T>(
		IEnumerable<T> items,
		Action<CodeWriter, T> writeItem,
		string open = "[",
		string close = "]",
		string separator = ", ")
	{
		Write(open);

		var first = true;
		foreach (var item in items)
		{
			if (!first)
				Write(separator);

			first = false;
			writeItem(this, item);
		}

		return Write(close);
	}

	public override string ToString() => _builder.ToString();

	private void AppendEscaped(string value)
	{
		foreach (var c in value)
		{
			switch (c)
			{
				case '"': _builder.Append("\\\""); break;
				case '\\': _builder.Append("\\\\"); break;
				case '\n': _builder.Append("\\n"); break;
				case '\r': _builder.Append("\\r"); break;
				case '\t': _builder.Append("\\t"); break;
				default: _builder.Append(c); break;
			}
		}
	}

	/// <summary>A scope that restores the previous indentation level when disposed.</summary>
	public readonly struct IndentScope : IDisposable
	{
		private readonly CodeWriter _writer;

		internal IndentScope(CodeWriter writer) => _writer = writer;

		public void Dispose() => _writer.Dedent();
	}

	/// <summary>
	/// Renders the properties of an object-initializer block. The opening brace, indentation and
	/// closing brace are emitted lazily so an object with no properties renders as just its
	/// constructor (e.g. <c>new()</c>). Every property is written on its own line with a trailing comma.
	/// </summary>
	public sealed class ObjectInitializer : IDisposable
	{
		private readonly CodeWriter _writer;
		private bool _opened;
		private bool _hasProperty;

		internal ObjectInitializer(CodeWriter writer) => _writer = writer;

		/// <summary>
		/// Writes a property prefix (<c>Name = </c>) and returns the writer so the caller can write the
		/// value next. The brace block, indentation and separators are emitted lazily, so an object with
		/// no properties renders as just its constructor.
		/// </summary>
		public CodeWriter Property(string name)
		{
			EnsureOpened();

			if (_hasProperty)
			{
				_writer.Write(",");
				_writer.WriteLine();
			}

			_hasProperty = true;
			return _writer.Write(name).Write(" = ");
		}

		/// <summary>Adds a property whose value is rendered via <see cref="WriteValue(object?)"/>.</summary>
		public ObjectInitializer Property(string name, object? value)
		{
			Property(name).WriteValue(value);
			return this;
		}

		/// <summary>Adds a property whose value is rendered by <paramref name="writeValue"/>.</summary>
		public ObjectInitializer Property(string name, Action<CodeWriter> writeValue)
		{
			writeValue(Property(name));
			return this;
		}

		private void EnsureOpened()
		{
			if (_opened)
				return;

			_opened = true;
			_writer.WriteLine();
			_writer.WriteLine("{");
			_writer._indentLevel++;
		}

		public void Dispose()
		{
			if (!_opened)
				return;

			if (_hasProperty)
				_writer.WriteLine();

			_writer.Dedent();
			_writer.Write("}");
		}
	}
}
