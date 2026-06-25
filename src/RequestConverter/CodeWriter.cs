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
/// recursively emit C# source code for a materialized request. Centralizes value dispatch, string
/// escaping, indentation and object-initializer rendering.
/// </summary>
public sealed class CodeWriter
{
	private readonly StringBuilder _builder = new();
	private int _indentLevel;
	private bool _atLineStart = true;

	// When set, the next BeginObjectInitializer emits an explicit "new T()" instead of a target-typed
	// "new()". Set by WriteValue when dispatching to an ICodeFormattable through a loosely-typed
	// (object/interface) context, where a target-typed new() cannot bind to the concrete type.
	private bool _forceExplicitConstructor;

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
				// Reached through a loosely-typed (object/interface) context, so a target-typed new() in the
				// value's own initializer cannot bind to the concrete type. Force its explicit constructor.
				_forceExplicitConstructor = true;
				formattable.FormatCode(this);
				_forceExplicitConstructor = false;
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
			case Enum enumValue:
				// Reached only via runtime dispatch (e.g. a union arm); statically-typed enum properties
				// use a generated static formatter. Render fully-qualified member access (valid C#).
				return Write("global::")
					.Write((enumValue.GetType().FullName ?? enumValue.GetType().Name).Replace('+', '.'))
					.Write(".")
					.Write(enumValue.ToString());
			case System.Collections.IEnumerable enumerable:
				// A collection reached via runtime dispatch (e.g. a union arm); render as an array literal
				// so a target type with an implicit array conversion accepts it.
				return WriteImplicitArray(AsObjects(enumerable), static (w, item) => w.WriteValue(item));
			default:
				return WritePrimitive(value);
		}
	}

	private static IEnumerable<object?> AsObjects(System.Collections.IEnumerable source)
	{
		foreach (var item in source)
			yield return item;
	}

	/// <inheritdoc cref="WriteValue(object?)"/>
	public CodeWriter WriteValue<T>(T value) => WriteValue((object?)value);

	/// <summary>
	/// Whether <paramref name="value"/> should be rendered as a property assignment. Filters out
	/// <c>null</c> and the "Undefined" <see cref="JsonElement"/> (<c>default(JsonElement)</c>), which
	/// arises for an unset open-generic value-type property, e.g. a generic request's
	/// <c>TDocument</c>/<c>TPartialDocument</c> materialized as <see cref="JsonElement"/>, where the
	/// nullable annotation is erased so the unset value is a non-null default that would throw on
	/// <see cref="JsonElement.GetRawText"/>.
	/// </summary>
	public static bool ShouldFormat(object? value) =>
		value is not null && value is not JsonElement { ValueKind: JsonValueKind.Undefined };

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
	/// Renders <c>new <paramref name="typeName"/>()</c> when <paramref name="forceExplicitConstructor"/>
	/// is set (e.g. for variant members assigned through an interface, where a target-typed
	/// <c>new()</c> cannot resolve) or when <see cref="FormattingOptions.ConstructorStyle"/> is
	/// <see cref="ConstructorStyle.Explicit"/>; otherwise the target-typed <c>new()</c>.
	/// </summary>
	public ObjectInitializer BeginObjectInitializer(string? typeName = null, bool forceExplicitConstructor = false)
	{
		var explicitConstructor = forceExplicitConstructor || _forceExplicitConstructor || Options.ConstructorStyle == ConstructorStyle.Explicit;
		_forceExplicitConstructor = false; // consume: applies to this initializer only, not nested ones
		if (explicitConstructor && !string.IsNullOrEmpty(typeName))
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

	/// <summary>
	/// Writes an array-creation expression (<c>new[] { a, b }</c>) for the "single or many" infer
	/// wrapper types (<c>Indices</c>, <c>Fields</c>, <c>Names</c>, ...). Those types are not
	/// collection-expression constructible but define implicit conversions from arrays, so a real
	/// array literal assigns to them. Emits a typed empty array when there are no items so the
	/// expression always compiles (and infers an element type).
	/// </summary>
	public CodeWriter WriteImplicitArray<T>(IEnumerable<T> items, Action<CodeWriter, T> writeItem)
	{
		using var enumerator = items.GetEnumerator();
		if (!enumerator.MoveNext())
			return Write("global::System.Array.Empty<string>()");

		Write("new[] { ");
		writeItem(this, enumerator.Current);
		while (enumerator.MoveNext())
		{
			Write(", ");
			writeItem(this, enumerator.Current);
		}

		return Write(" }");
	}

	/// <summary>
	/// Writes a fully-qualified (<c>global::</c>) C# type name for a runtime <see cref="Type"/>,
	/// recursing into generic arguments. Used to render the concrete type argument of an open generic
	/// (e.g. <c>Buckets&lt;TBucket&gt;</c> whose <c>TBucket</c> is only known at runtime). AOT-safe:
	/// uses only <see cref="Type.FullName"/> / <see cref="Type.GetGenericArguments"/>.
	/// </summary>
	public CodeWriter WriteTypeName(Type type)
	{
		WriteIndentIfNeeded();
		AppendTypeName(type);
		return this;
	}

	private void AppendTypeName(Type type)
	{
		if (type.IsGenericType)
		{
			var definition = type.GetGenericTypeDefinition();
			var raw = definition.FullName ?? definition.Name;
			var tick = raw.IndexOf('`');
			if (tick >= 0)
				raw = raw[..tick];

			_builder.Append("global::").Append(raw.Replace('+', '.')).Append('<');
			var args = type.GetGenericArguments();
			for (var i = 0; i < args.Length; i++)
			{
				if (i > 0)
					_builder.Append(", ");

				AppendTypeName(args[i]);
			}

			_builder.Append('>');
		}
		else
		{
			_builder.Append("global::").Append((type.FullName ?? type.Name).Replace('+', '.'));
		}
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
