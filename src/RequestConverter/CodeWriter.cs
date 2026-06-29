// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

	// Every type reference is written as a placeholder rather than its final spelling, so the concrete
	// form (short identifier, FQN, or global::-FQN) can be decided in ToString once the full set of
	// references is known - shortening requires global knowledge to detect ambiguous simple names.
	private readonly List<TypeRef> _typeRefs = new();

	// Private-use sentinels delimiting a placeholder slot index in the builder. Real C# source never
	// contains these, and the only free-form text we emit verbatim (JSON in raw-string literals) does
	// not carry private-use code points in practice.
	private const char RefOpen = '\uE000';
	private const char RefClose = '\uE001';

	public CodeWriter(FormattingOptions? options = null) => Options = options ?? FormattingOptions.Default;

	public FormattingOptions Options { get; }

	/// <summary>
	/// The distinct namespaces of every type referenced by the emitted code. A caller rendering with
	/// <see cref="TypeNameStyle.Simplified"/> adds these as <c>using</c> directives so the short identifiers resolve.
	/// </summary>
	public IReadOnlyCollection<string> Namespaces =>
		_typeRefs
			.Select(r => r.Namespace)
			.Where(ns => ns.Length > 0)
			.Distinct(StringComparer.Ordinal)
			.OrderBy(ns => ns, StringComparer.Ordinal)
			.ToArray();

	private readonly record struct TypeRef(string Namespace, string SimpleName, string FullName);

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
				return WriteJsonElement(jsonElement);
			case string s:
				return WriteString(s);
			case bool b:
				return Write(b ? "true" : "false");
			case DateTimeOffset dateTimeOffset:
				return WriteTypeRef("System.DateTimeOffset").Write(".Parse(")
					.WriteString(dateTimeOffset.ToString("O", CultureInfo.InvariantCulture))
					.Write(", ").WriteTypeRef("System.Globalization.CultureInfo").Write(".InvariantCulture, ")
					.WriteTypeRef("System.Globalization.DateTimeStyles").Write(".RoundtripKind)");
			case DateTime dateTime:
				return WriteTypeRef("System.DateTime").Write(".Parse(")
					.WriteString(dateTime.ToString("O", CultureInfo.InvariantCulture))
					.Write(", ").WriteTypeRef("System.Globalization.CultureInfo").Write(".InvariantCulture, ")
					.WriteTypeRef("System.Globalization.DateTimeStyles").Write(".RoundtripKind)");
			case Enum enumValue:
				// Reached only via runtime dispatch (e.g. a union arm); statically-typed enum properties
				// use a generated static formatter. Render fully-qualified member access (valid C#).
				return WriteTypeRef((enumValue.GetType().FullName ?? enumValue.GetType().Name).Replace('+', '.'))
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

	// Re-indented from scratch (2-space, rooted at column 0) rather than emitting the source document's own
	// whitespace, so the raw-string literal reads as cleanly nested JSON regardless of how the example was formatted.
	private static readonly JsonSerializerOptions IndentedJsonOptions = new() { WriteIndented = true };

	/// <summary>
	/// Emits a <see cref="JsonElement"/> as a <c>Deserialize&lt;JsonElement&gt;("…")</c> call. Objects and arrays are
	/// re-serialized with canonical indentation and rendered as a raw string literal (<c>"""</c>) whose content lines and
	/// closing fence sit at the current indent, so C# strips exactly the wrapper indent and leaves clean 2-space JSON.
	/// Scalars keep the compact escaped form. Newlines are normalized to <c>\n</c>.
	/// </summary>
	private CodeWriter WriteJsonElement(JsonElement value)
	{
		var json = JsonSerializer.Serialize(value, IndentedJsonOptions).Replace("\r\n", "\n").Replace('\r', '\n');

		WriteTypeRef("System.Text.Json.JsonSerializer").Write(".Deserialize<")
			.WriteTypeRef("System.Text.Json.JsonElement").Write(">(");

		if (json.IndexOf('\n') < 0)
			return WriteString(json).Write(")");

		// The fence must be longer than any run of quotes in the content so it can never be mistaken for a delimiter.
		var fence = new string('"', Math.Max(3, LongestQuoteRun(json) + 1));

		WriteLine(fence);
		foreach (var line in json.Split('\n'))
			WriteLine(line);

		return Write(fence).Write(")");
	}

	private static int LongestQuoteRun(string text)
	{
		int longest = 0, current = 0;
		foreach (var c in text)
		{
			current = c == '"' ? current + 1 : 0;
			if (current > longest)
				longest = current;
		}

		return longest;
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
			Write("new ").WriteTypeRef(typeName!).Write("()");
		}
		else
		{
			Write("new()");
		}

		return new ObjectInitializer(this);
	}

	/// <summary>
	/// Begins an object-initializer block for a constructor the caller has <em>already</em> written (e.g. a
	/// <c>new BulkIndexOperation&lt;object&gt;(document)</c> with its argument). The returned
	/// <see cref="ObjectInitializer"/> appends the multi-line <c>{ … }</c> property block, or nothing when no property
	/// is added.
	/// </summary>
	public ObjectInitializer BeginInitializer() => new(this);

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
	/// Writes a multi-line brace-block list (the collection/dictionary analogue of the object initializer): the opening
	/// brace, each item on its own indented line with a trailing comma, and the closing brace, e.g.
	/// <code>
	/// {
	///     item0,
	///     item1
	/// }
	/// </code>
	/// The caller writes the constructor (e.g. <c>new Dictionary&lt;…&gt;()</c>) first; an empty list renders inline as
	/// <c> { }</c> so the surrounding expression stays valid.
	/// </summary>
	public CodeWriter WriteBlockList<T>(IEnumerable<T> items, Action<CodeWriter, T> writeItem)
	{
		using var enumerator = items.GetEnumerator();
		if (!enumerator.MoveNext())
			return Write(" { }");

		WriteLine();
		WriteLine("{");
		using (Indent())
		{
			writeItem(this, enumerator.Current);
			while (enumerator.MoveNext())
			{
				WriteLine(",");
				writeItem(this, enumerator.Current);
			}

			WriteLine();
		}

		return Write("}");
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
			return WriteTypeRef("System.Array").Write(".Empty<string>()");

		Write("new[] { ");
		writeItem(this, enumerator.Current);
		while (enumerator.MoveNext())
		{
			Write(", ");
			writeItem(this, enumerator.Current);
		}

		return Write(" }");
	}

	// ---- type references --------------------------------------------------

	/// <summary>
	/// Writes a C# type expression - a possibly-generic, possibly-nullable, possibly-array type, with every named type
	/// fully qualified (no <c>global::</c>), e.g. <c>System.Collections.Generic.IReadOnlyCollection&lt;Elastic.Clients.Elasticsearch.IndexName&gt;</c>.
	/// Each qualified name is recorded (so its namespace is reported and its final spelling deferred to
	/// <see cref="ToString"/>); structural tokens (<c>&lt; &gt; , [ ] ?</c>) and keywords/primitives (<c>string</c>,
	/// <c>object</c>, open type parameters) pass through verbatim.
	/// </summary>
	public CodeWriter WriteTypeRef(string typeExpression)
	{
		WriteIndentIfNeeded();

		var i = 0;
		while (i < typeExpression.Length)
		{
			var c = typeExpression[i];
			if (char.IsLetter(c) || c == '_')
			{
				var start = i;
				while (i < typeExpression.Length && (char.IsLetterOrDigit(typeExpression[i]) || typeExpression[i] is '_' or '.'))
					i++;

				var token = typeExpression.Substring(start, i - start);
				if (token.IndexOf('.') >= 0)
					AppendTypeRefPlaceholder(token);
				else
					_builder.Append(token); // keyword / primitive / open type parameter
			}
			else
			{
				_builder.Append(c);
				i++;
			}
		}

		return this;
	}

	/// <summary>
	/// Writes a C# type name for a runtime <see cref="Type"/>, recursing into generic arguments. Used to render the
	/// concrete type argument of an open generic (e.g. <c>Buckets&lt;TBucket&gt;</c> whose <c>TBucket</c> is only known
	/// at runtime). AOT-safe: uses only <see cref="Type.FullName"/> / <see cref="Type.GetGenericArguments"/>.
	/// </summary>
	public CodeWriter WriteTypeName(Type type)
	{
		var expression = new StringBuilder();
		AppendTypeExpression(expression, type);
		return WriteTypeRef(expression.ToString());
	}

	private static void AppendTypeExpression(StringBuilder builder, Type type)
	{
		if (type.IsGenericType)
		{
			var definition = type.GetGenericTypeDefinition();
			var raw = definition.FullName ?? definition.Name;
			var tick = raw.IndexOf('`');
			if (tick >= 0)
				raw = raw[..tick];

			builder.Append(raw.Replace('+', '.')).Append('<');
			var args = type.GetGenericArguments();
			for (var i = 0; i < args.Length; i++)
			{
				if (i > 0)
					builder.Append(", ");

				AppendTypeExpression(builder, args[i]);
			}

			builder.Append('>');
		}
		else
		{
			builder.Append((type.FullName ?? type.Name).Replace('+', '.'));
		}
	}

	private void AppendTypeRefPlaceholder(string fullName)
	{
		var lastDot = fullName.LastIndexOf('.');
		var ns = lastDot >= 0 ? fullName[..lastDot] : string.Empty;
		var simpleName = lastDot >= 0 ? fullName[(lastDot + 1)..] : fullName;

		_builder.Append(RefOpen).Append(_typeRefs.Count).Append(RefClose);
		_typeRefs.Add(new TypeRef(ns, simpleName, fullName));
	}

	/// <summary>
	/// Produces the final source, resolving every recorded type reference to its concrete spelling per
	/// <see cref="FormattingOptions.TypeNameStyle"/>. Pure: it does not mutate the builder, so it can be called more
	/// than once.
	/// </summary>
	public override string ToString()
	{
		if (_typeRefs.Count == 0)
			return _builder.ToString();

		var style = Options.TypeNameStyle;
		var shortenable = style == TypeNameStyle.Simplified ? ComputeShortenableNames() : null;

		var result = new StringBuilder(_builder.Length);
		for (var i = 0; i < _builder.Length; i++)
		{
			var c = _builder[i];
			if (c != RefOpen)
			{
				result.Append(c);
				continue;
			}

			var end = i + 1;
			while (_builder[end] != RefClose)
				end++;

			var index = int.Parse(_builder.ToString(i + 1, end - i - 1), CultureInfo.InvariantCulture);
			result.Append(RenderTypeRef(_typeRefs[index], style, shortenable));
			i = end;
		}

		return result.ToString();
	}

	private static string RenderTypeRef(TypeRef typeRef, TypeNameStyle style, HashSet<string>? shortenable) => style switch
	{
		// Shorten only when the simple name is unambiguous across the imported namespaces; otherwise fall back to the
		// global::-qualified name, which always resolves regardless of the using directives in scope.
		TypeNameStyle.Simplified => shortenable!.Contains(typeRef.SimpleName) ? typeRef.SimpleName : "global::" + typeRef.FullName,
		TypeNameStyle.Fqn => typeRef.FullName,
		TypeNameStyle.GlobalFqn => "global::" + typeRef.FullName,
		_ => "global::" + typeRef.FullName
	};

	// The simple names that resolve unambiguously when every referenced namespace is imported: exactly one visible type
	// (across those namespaces) carries the name. Counting all types in the imported namespaces - not just the ones the
	// snippet references - is what makes the shortened output guaranteed to compile under those usings.
	private HashSet<string> ComputeShortenableNames()
	{
		var importedNamespaces = _typeRefs
			.Select(r => r.Namespace)
			.Where(ns => ns.Length > 0)
			.Distinct(StringComparer.Ordinal);

		var counts = new Dictionary<string, int>(StringComparer.Ordinal);
		foreach (var ns in importedNamespaces)
		{
			if (!NamespaceTypeNames.Value.TryGetValue(ns, out var names))
				continue;

			foreach (var name in names)
				counts[name] = counts.TryGetValue(name, out var n) ? n + 1 : 1;
		}

		var shortenable = new HashSet<string>(StringComparer.Ordinal);
		foreach (var entry in counts)
		{
			if (entry.Value == 1)
				shortenable.Add(entry.Key);
		}

		return shortenable;
	}

	// namespace -> simple type names declared in it (generic arity suffix stripped), across all loaded assemblies.
	// Built once: the set of types visible per namespace does not change during a run.
	private static readonly Lazy<Dictionary<string, HashSet<string>>> NamespaceTypeNames = new(BuildNamespaceTypeNames);

	private static Dictionary<string, HashSet<string>> BuildNamespaceTypeNames()
	{
		var map = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);

		foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			Type[] types;
			try
			{
				types = assembly.GetExportedTypes();
			}
			catch (System.Reflection.ReflectionTypeLoadException ex)
			{
				types = ex.Types.Where(t => t is not null).ToArray()!;
			}
			catch
			{
				continue;
			}

			foreach (var type in types)
			{
				if (type.Namespace is not { Length: > 0 } ns)
					continue;

				var simpleName = type.Name;
				var tick = simpleName.IndexOf('`');
				if (tick >= 0)
					simpleName = simpleName[..tick];

				if (!map.TryGetValue(ns, out var names))
					map[ns] = names = new HashSet<string>(StringComparer.Ordinal);

				names.Add(simpleName);
			}
		}

		return map;
	}

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
