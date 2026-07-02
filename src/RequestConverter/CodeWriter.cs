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

	// ---- modal rendering state --------------------------------------------
	//
	// Four mechanisms modify how downstream writes render; they are distinct on purpose:
	// - _forceExplicitConstructor (one-shot): set by WriteValue's ICodeFormattable dispatch, consumed by the
	//   NEXT BeginObjectInitializer only. Cannot be a scope: it must apply to the dispatched value's own
	//   initializer but not to initializers nested inside it, and the generated FormatCode signature offers
	//   no parameter to thread it through.
	// - _forceExplicitConstructorDepth (scope): every initializer in the scope renders an explicit
	//   constructor; used for value arguments to overloaded fluent setters (CS0121).
	// - _forceObjectInitializerDepth (scope): overrides EffectiveSyntaxMode for a subtree rendered as a
	//   plain value inside descriptor output.
	// - Options.ConstructorStyle (global): the caller-selected default.
	// BeginObjectInitializer's explicit-constructor decision reads its own parameter, then the one-shot flag,
	// then _forceExplicitConstructorDepth, then Options.ConstructorStyle; any being set forces the explicit
	// form. _forceObjectInitializerDepth is separate: it feeds EffectiveSyntaxMode, not that decision.
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

	// Count of line endings emitted so far. A descriptor-lambda body starts every fluent call on its own line, so a body
	// that grows this counter spans multiple lines and its enclosing call's closing ")" must drop to its own line (rather
	// than collapse to ")))"). Comparing the counter before/after a body, instead of counting that body's own calls,
	// cascades the decision up the nesting for free: a wrapped descendant's line endings lie within every ancestor body's
	// span, so each ancestor on the single-call path also closes on its own line.
	private int _lineCount;

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
		_lineCount++;
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
				return WriteImplicitArray(AsObjects(enumerable), static (w, item) => w.WriteValue(item), InferElementTypeExpression(enumerable));
			default:
				return WritePrimitive(value);
		}
	}

	private static IEnumerable<object?> AsObjects(System.Collections.IEnumerable source)
	{
		foreach (var item in source)
			yield return item;
	}

	// The runtime element type of a dispatch-reached collection, so an empty collection still renders a
	// typed Array.Empty<T>() that binds to the target's implicit array conversion. Falls back to "string"
	// (the infer wrappers' element type) when no IEnumerable<T> is implemented. Reads only the interfaces
	// of a live instance's type, which trimming preserves.
	private static string InferElementTypeExpression(System.Collections.IEnumerable enumerable)
	{
		foreach (var iface in enumerable.GetType().GetInterfaces())
		{
			if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				var expression = new StringBuilder();
				AppendTypeExpression(expression, iface.GetGenericArguments()[0], null);
				return expression.ToString();
			}
		}

		return "string";
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

	/// <summary>
	/// Writes a value whose static type is <c>object</c> (e.g. a template parameter in an
	/// <c>IDictionary&lt;string, object&gt;</c>). A scalar renders as its plain C# literal - quoted string,
	/// number, boolean, or <c>null</c> - so the output reads naturally rather than as a
	/// <c>Deserialize&lt;JsonElement&gt;("…")</c> call. Objects, arrays, and numbers whose C# literal would not
	/// reserialize to the exact original token (see <see cref="TryWriteObjectNumber"/>) keep the
	/// <see cref="JsonElement"/> form. A non-<see cref="JsonElement"/> value (the static type is <c>object</c> but the
	/// runtime value was materialized otherwise) falls back to the general <see cref="WriteValue(object?)"/> dispatch.
	/// </summary>
	public CodeWriter WriteObjectValue(object? value)
	{
		if (value is not JsonElement element)
			return WriteValue(value);

		switch (element.ValueKind)
		{
			case JsonValueKind.String:
				return WriteString(element.GetString());
			case JsonValueKind.True:
				return Write("true");
			case JsonValueKind.False:
				return Write("false");
			case JsonValueKind.Null:
				return Write("null");
			case JsonValueKind.Number when TryWriteObjectNumber(element):
				return this;
			default:
				return WriteJsonElement(element);
		}
	}

	/// <summary>
	/// Writes a JSON number boxed into an <c>object</c> as a C# numeric literal, but only when that literal reserializes
	/// to the exact original token. An <see cref="long"/> integer is emitted verbatim (boxes as a value whose JSON form is
	/// the same digits). A non-integer is emitted as a <c>double</c> literal (<c>d</c> suffix) only when the parsed
	/// <see cref="double"/>'s round-trippable (<c>G17</c>) form equals the raw token, which is exactly what the
	/// transport serializer writes for a boxed <see cref="double"/>. So <c>1.5</c>, <c>-2.5</c>, <c>123.456</c> qualify,
	/// while a token whose boxed-double form differs (<c>1.1</c> → <c>1.1000000000000001</c>, <c>0.835526591</c>,
	/// <c>1.0</c>'s dropped zero, <c>6.022e23</c>) does not. Returns <c>false</c> in that case so the caller keeps the
	/// lossless <see cref="JsonElement"/> form. (A few faithful tokens like <c>2.0</c> are conservatively rejected.)
	/// </summary>
	private bool TryWriteObjectNumber(JsonElement element)
	{
		var raw = element.GetRawText();

		if (element.TryGetInt64(out var integer))
		{
			WriteIndentIfNeeded();
			_builder.Append(integer.ToString(CultureInfo.InvariantCulture));
			return true;
		}

		// A bare C# floating literal binds to double; the transport serializer writes a boxed double in round-trippable
		// (G17) form, so the value reproduces the original token only when that form already equals it. The 'd' suffix
		// pins the literal to double so it never silently widens or binds differently.
		if (element.TryGetDouble(out var number)
			&& string.Equals(number.ToString("G17", CultureInfo.InvariantCulture), raw, StringComparison.Ordinal))
		{
			WriteIndentIfNeeded();
			_builder.Append(raw).Append('d');
			return true;
		}

		return false;
	}

	/// <summary>
	/// Renders a generic request's document body. The parameter is <see cref="object"/> because the generated
	/// <c>FormatCode</c> body runs on the open generic request (the document is typed as the open <c>TDocument</c>
	/// parameter there); at runtime the materialized value is always a <see cref="JsonElement"/>. When
	/// <see cref="FormattingOptions.UseStronglyTypedDocument"/> is set and the value is a JSON object, emits a
	/// strongly-typed initializer against the placeholder document type (<c>new MyDocument { Key = value, ... }</c>,
	/// nested objects target-typed as <c>new() { ... }</c>); otherwise falls back to the default value dispatch so the
	/// default and non-object cases are byte-for-byte unchanged.
	/// </summary>
	public void WriteDocument(object? value)
	{
		if (Options.UseStronglyTypedDocument && value is JsonElement { ValueKind: JsonValueKind.Object } document)
		{
			WriteTypedDocument(document, Options.DocumentTypeName);
			return;
		}

		WriteValue(value);
	}

	// Recursively renders a JSON value as an object-initializer tree. The top-level object names the placeholder type
	// (typeName); nested objects are target-typed (new()), matching how the surrounding generated code reads.
	private void WriteTypedDocument(JsonElement value, string? typeName)
	{
		switch (value.ValueKind)
		{
			case JsonValueKind.Object:
				WriteTypedDocumentObject(value, typeName);
				break;
			case JsonValueKind.Array:
				WriteImplicitArray(value.EnumerateArray(), static (w, item) => w.WriteTypedDocument(item, null));
				break;
			case JsonValueKind.String:
				WriteString(value.GetString());
				break;
			case JsonValueKind.Number:
				// Preserve the source token verbatim (e.g. integer vs. decimal, exponent) rather than reparsing.
				Write(value.GetRawText());
				break;
			case JsonValueKind.True:
				Write("true");
				break;
			case JsonValueKind.False:
				Write("false");
				break;
			default:
				Write("null");
				break;
		}
	}

	private void WriteTypedDocumentObject(JsonElement value, string? typeName)
	{
		if (!string.IsNullOrEmpty(typeName))
			Write("new ").Write(typeName!).Write("()");
		else
			Write("new()");

		using var enumerator = value.EnumerateObject().GetEnumerator();
		if (!enumerator.MoveNext())
			return;

		var initializer = new ObjectInitializer(this);
		do
		{
			var member = enumerator.Current;
			initializer.Property(FieldPath.ToPropertyName(member.Name));
			WriteTypedDocument(member.Value, null);
		}
		while (enumerator.MoveNext());

		initializer.Dispose();
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
		var explicitConstructor = forceExplicitConstructor || _forceExplicitConstructor || _forceExplicitConstructorDepth > 0 || Options.ConstructorStyle == ConstructorStyle.Explicit;
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

	// ---- descriptor (fluent) syntax ---------------------------------------

	// Descriptor mode falls back to object-initializer rendering for any value whose fluent shape is not
	// (yet) handled (dictionaries, unions, factory members, ...). A fluent value cannot be a standalone
	// argument because a descriptor-mode FormatCode emits a receiver-less ".Method(arg)…" chain suffix,
	// not a "new T { … }" expression. Forcing object-init for that one subtree yields the regular value
	// expression the fluent setter accepts. Nesting count > 0 suppresses descriptor mode for the subtree.
	private int _forceObjectInitializerDepth;

	/// <summary>
	/// The syntax mode in effect right now. Equals <see cref="FormattingOptions.SyntaxMode"/> normally, but is forced to
	/// <see cref="SyntaxMode.ObjectInitializer"/> while inside a <see cref="ForceObjectInitializer"/> scope (a descriptor
	/// value rendered through the object-initializer fallback). Descriptor-aware <c>FormatCode</c> bodies and the infer
	/// partials branch on this, not on <see cref="FormattingOptions.SyntaxMode"/>, so a forced subtree renders correctly.
	/// </summary>
	public SyntaxMode EffectiveSyntaxMode =>
		_forceObjectInitializerDepth > 0 ? SyntaxMode.ObjectInitializer : Options.SyntaxMode;

	/// <summary>
	/// Forces <see cref="EffectiveSyntaxMode"/> to <see cref="SyntaxMode.ObjectInitializer"/> until the returned scope is
	/// disposed, so a value emitted inside renders as a plain <c>new T { … }</c> expression even in descriptor mode.
	/// </summary>
	public ForceObjectInitializerScope ForceObjectInitializer() => new(this);

	/// <summary>
	/// Writes a constructor for a value whose type would otherwise be inferred from the assignment target via a
	/// target-typed <c>new()</c>. In a descriptor fallback position (inside <see cref="ForceObjectInitializer"/> with the
	/// option set to <see cref="SyntaxMode.Descriptor"/>) the value is passed to a fluent setter that also has an
	/// <c>Action&lt;Descriptor&gt;</c> overload of the same name, where <c>new()</c> is ambiguous (CS0121); emit
	/// <c>new <paramref name="fullyQualifiedTypeName"/>()</c> there. Otherwise the target-typed <c>new()</c> is kept so
	/// object-initializer output is unchanged.
	/// </summary>
	public CodeWriter WriteValueConstructor(string fullyQualifiedTypeName)
	{
		if (_forceObjectInitializerDepth > 0 && Options.SyntaxMode == SyntaxMode.Descriptor)
			return Write("new ").WriteTypeRef(fullyQualifiedTypeName).Write("()");

		return Write("new()");
	}

	/// <summary>
	/// Opens a constructor call for a value passed by argument: <c>new(</c> normally, or <c>new <paramref
	/// name="fullyQualifiedTypeName"/>(</c> in a descriptor fallback position (inside <see cref="ForceObjectInitializer"/>
	/// with the option set to <see cref="SyntaxMode.Descriptor"/>), where a target-typed <c>new(...)</c> would be
	/// ambiguous against an overloaded fluent setter (CS0121). The caller writes the arguments and the closing <c>)</c>.
	/// </summary>
	public CodeWriter WriteArgsConstructorStart(string fullyQualifiedTypeName)
	{
		if (_forceObjectInitializerDepth > 0 && Options.SyntaxMode == SyntaxMode.Descriptor)
			return Write("new ").WriteTypeRef(fullyQualifiedTypeName).Write("(");

		return Write("new(");
	}

	/// <summary>Disposable returned by <see cref="ForceObjectInitializer"/>; restores the descriptor mode on dispose.</summary>
	public readonly struct ForceObjectInitializerScope : IDisposable
	{
		private readonly CodeWriter _writer;

		internal ForceObjectInitializerScope(CodeWriter writer)
		{
			_writer = writer;
			writer._forceObjectInitializerDepth++;
		}

		public void Dispose() => _writer._forceObjectInitializerDepth--;
	}

	// While set, every BeginObjectInitializer renders an explicit "new T()" rather than a target-typed "new()". Used to
	// disambiguate a value passed to a fluent setter that also has an Action<Descriptor> overload of the same arity (CS0121).
	private int _forceExplicitConstructorDepth;

	/// <summary>
	/// Forces <see cref="BeginObjectInitializer"/> to emit an explicit <c>new T()</c> (never a target-typed <c>new()</c>)
	/// until the returned scope is disposed. Used in descriptor mode for the empty-value fallback of an incremental
	/// dictionary add (<c>.AddX(key, new T())</c>), where a target-typed <c>new()</c> is ambiguous against the setter's
	/// <c>Action&lt;Descriptor&gt;</c> overload.
	/// </summary>
	public ForceExplicitConstructorScope ForceExplicitConstructor() => new(this);

	/// <summary>Disposable returned by <see cref="ForceExplicitConstructor"/>; restores the constructor style on dispose.</summary>
	public readonly struct ForceExplicitConstructorScope : IDisposable
	{
		private readonly CodeWriter _writer;

		internal ForceExplicitConstructorScope(CodeWriter writer)
		{
			_writer = writer;
			writer._forceExplicitConstructorDepth++;
		}

		public void Dispose() => _writer._forceExplicitConstructorDepth--;
	}

	// The current descriptor-lambda nesting depth, used to allocate non-shadowing parameter names
	// (d0, d1, ...). Bumped while a descriptor lambda body is being written.
	private int _descriptorDepth;

	/// <summary>
	/// The parameter name for the descriptor lambda currently being written, e.g. <c>d0</c> at the top level. Valid only
	/// inside a <see cref="WriteFluentDescriptorCall"/> body; a descriptor-mode <c>FormatCode</c> appends its fluent
	/// chain onto this receiver implicitly (each <see cref="WriteFluentCall"/> starts with <c>.</c>).
	/// </summary>
	public string CurrentDescriptorParameter => "d" + _descriptorDepth;

	/// <summary>
	/// Writes one fluent call <c>.<paramref name="method"/>(args)</c> on its own line at the current indent, so a chain
	/// reads one call per line. <paramref name="writeArgs"/> emits the argument list (omit for a no-arg call). Arguments
	/// are value-position, so they render in object-initializer mode by default; pass
	/// <paramref name="forceObjectInitializerArgs"/> <c>false</c> for the setters whose argument must observe descriptor
	/// mode itself (<c>Field</c>/<c>PropertyName</c>/<c>Fields</c>, which emit bare expression lambdas there).
	/// </summary>
	public CodeWriter WriteFluentCall(string method, Action<CodeWriter>? writeArgs = null, bool forceObjectInitializerArgs = true)
	{
		WriteLine();
		Write(".").Write(method).Write("(");
		if (writeArgs is not null)
		{
			if (forceObjectInitializerArgs)
			{
				using var _ = ForceObjectInitializer();
				writeArgs(this);
			}
			else
			{
				writeArgs(this);
			}
		}

		return Write(")");
	}

	/// <summary>
	/// Writes a descriptor-configuration lambda <c>dN =&gt; dN&lt;body&gt;</c> with a fresh depth-allocated parameter,
	/// the body indented one level. When the body emits nothing, the bare <c>dN =&gt; dN</c> receiver is not a
	/// valid lambda, so the builder is rewound to <paramref name="rewindTo"/> and <c>false</c> is returned; the
	/// caller emits its fallback. Rewinding is safe only because an empty body appended no text and therefore
	/// no type-ref placeholders, which the assertion enforces.
	/// </summary>
	private bool TryWriteDescriptorLambda(int rewindTo, Action<CodeWriter> writeBody, out bool multiline)
	{
		var typeRefsBefore = _typeRefs.Count;
		_descriptorDepth++;
		var parameter = CurrentDescriptorParameter;
		Write(parameter).Write(" => ").Write(parameter);
		var afterReceiver = _builder.Length;

		var linesBefore = _lineCount;
		using (Indent())
		{
			writeBody(this);
		}

		_descriptorDepth--;
		multiline = _lineCount > linesBefore;

		if (_builder.Length != afterReceiver)
			return true;

		System.Diagnostics.Debug.Assert(_typeRefs.Count == typeRefsBefore, "An empty descriptor body must not record type refs.");
		_builder.Length = rewindTo;
		return false;
	}

	/// <summary>
	/// Writes a fluent call whose arguments are a <c>params</c> list of scalar values, one per item:
	/// <c>.<paramref name="method"/>(v1, v2, …)</c>. Used for a collection-of-scalar member whose fluent setter takes
	/// <c>params E[]</c> (e.g. <c>.Uids("a", "b")</c>). Each value is written by <paramref name="writeItem"/>; an empty
	/// collection renders the bare <c>.<paramref name="method"/>()</c>, which the params overload accepts as an empty array.
	/// </summary>
	public CodeWriter WriteFluentParams<T>(string method, IEnumerable<T> items, Action<CodeWriter, T> writeItem)
	{
		WriteLine();
		Write(".").Write(method).Write("(");

		var first = true;
		foreach (var item in items)
		{
			if (!first)
				Write(", ");

			first = false;
			writeItem(this, item);
		}

		return Write(")");
	}

	/// <summary>
	/// Writes a fluent call whose single argument is a descriptor-configuration lambda:
	/// <c>.<paramref name="method"/>(dN =&gt; dN.A(..).B(..))</c>. <paramref name="writeBody"/> emits the nested chain
	/// (typically a nested value's <c>FormatCode</c>), which appends onto the fresh parameter <c>dN</c> allocated by
	/// nesting depth so lambdas never shadow. When the body emits nothing (an empty nested object, e.g. a no-field
	/// variant), the identity lambda <c>dN =&gt; dN</c> would be invalid C#: if <paramref name="writeEmpty"/> is supplied it
	/// emits that argument instead (the value form, for a setter with no no-arg overload), otherwise this collapses to the
	/// no-arg overload <c>.<paramref name="method"/>()</c> (which exists for a configurable-optional member).
	/// </summary>
	public CodeWriter WriteFluentDescriptorCall(string method, Action<CodeWriter> writeBody, Action<CodeWriter>? writeEmpty = null)
	{
		WriteLine();
		Write(".").Write(method).Write("(");

		if (!TryWriteDescriptorLambda(_builder.Length, writeBody, out var multiline))
		{
			if (writeEmpty is not null)
			{
				// The empty fallback is a value argument to a setter that also has an Action<Descriptor>
				// overload, so it must render as an object-initializer value with an explicit constructor
				// (a target-typed new() would be ambiguous, CS0121).
				using var _oi = ForceObjectInitializer();
				using var _ec = ForceExplicitConstructor();
				writeEmpty(this);
			}

			return Write(")");
		}

		// A non-empty body always starts its first call on a new line, so it spans multiple lines: close ")" on its own line
		// at this call's indent (the body's Indent() block has exited). This cascades up the nesting for free - a wrapped
		// descendant's line endings lie within every ancestor body's span - so parens never collapse to ")))".
		return multiline ? WriteLine().Write(")") : Write(")");
	}

	/// <summary>
	/// Writes a fluent call whose arguments are a <c>params</c> array of descriptor-configuration lambdas, one per item:
	/// <c>.<paramref name="method"/>(dN =&gt; dN…, dN =&gt; dN…)</c>. Used for collection-of-complex members, whose fluent
	/// setter takes <c>params Action&lt;ItemDescriptor&gt;[]</c>. Each lambda body is written by
	/// <paramref name="writeItem"/>; an item whose body is empty collapses to a bare <c>dN =&gt; dN</c> identity, which is
	/// invalid, so such an item instead emits the item's object-initializer value via <paramref name="writeFallback"/>
	/// (the <c>params ItemValue[]</c> overload also exists, so a value arg is assignment-compatible).
	/// </summary>
	public CodeWriter WriteFluentDescriptorParams<T>(
		string method,
		IEnumerable<T> items,
		Action<CodeWriter, T> writeItem,
		Action<CodeWriter, T> writeFallback,
		Action<CodeWriter>? writeEmpty = null)
	{
		WriteLine();
		Write(".").Write(method).Write("(");

		// An empty collection would render as a bare `.Method()`, which is ambiguous between the params-value and
		// params-Action overloads of the fluent setter; emit an explicitly-typed empty value instead (e.g. an empty typed
		// array binds unambiguously to the value overload).
		if (writeEmpty is not null)
		{
			using var enumerator = items.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				writeEmpty(this);
				return Write(")");
			}
		}

		var first = true;
		var linesBefore = _lineCount;
		foreach (var item in items)
		{
			if (!first)
				Write(", ");

			first = false;

			if (!TryWriteDescriptorLambda(_builder.Length, w => writeItem(w, item), out _))
			{
				// Empty configuration: replace the invalid identity lambda with the item's value form, which is
				// value-position and so must render in object-initializer mode.
				using var _oi = ForceObjectInitializer();
				writeFallback(this, item);
			}
		}

		// Close on its own line when any item lambda spanned multiple lines (each starts its first call on a new line);
		// otherwise stay inline. The line-count delta cascades upward, so an ancestor whose only call is this one also wraps.
		return _lineCount > linesBefore ? WriteLine().Write(")") : Write(")");
	}

	/// <summary>
	/// Writes one incremental fluent add call per entry of a dictionary or key-value-pair collection whose value builds
	/// fluently: <c>.<paramref name="method"/>(key, dN =&gt; dN…)</c> for each entry, each on its own line. Used as the body
	/// of a whole-collection <c>.Property(dN =&gt; dN…)</c> lambda, so each <c>.<paramref name="method"/></c> call appends
	/// onto the lambda's helper receiver. <paramref name="writeKey"/> emits the key argument; <paramref name="writeValue"/>
	/// emits the value's descriptor chain onto the fresh parameter <c>dN</c> (allocated by nesting depth so lambdas never
	/// shadow). When an entry's value body is empty (a no-field value), the identity lambda <c>dN =&gt; dN</c> would be
	/// invalid, so that entry instead emits its value form via <paramref name="writeValueFallback"/> (the scalar
	/// <c>.<paramref name="method"/>(key, value)</c> overload also exists, so a value arg is assignment-compatible).
	/// </summary>
	public CodeWriter WriteFluentDictionaryAdds<T>(
		string method,
		IEnumerable<T> entries,
		Action<CodeWriter, T> writeKey,
		Action<CodeWriter, T> writeValue,
		Action<CodeWriter, T> writeValueFallback)
	{
		foreach (var entry in entries)
		{
			WriteLine();
			Write(".").Write(method).Write("(");
			// The key is a value argument, so it renders in object-initializer mode.
			using (ForceObjectInitializer())
			{
				writeKey(this, entry);
			}

			Write(", ");

			if (!TryWriteDescriptorLambda(_builder.Length, w => writeValue(w, entry), out var multiline))
			{
				// Empty configuration: replace the invalid identity lambda with the entry's value form, inline. The
				// value is a setter argument that also has an Action<Descriptor> overload, so it renders as an
				// object-initializer value with an explicit constructor (a target-typed new() would be ambiguous, CS0121).
				using var _oi = ForceObjectInitializer();
				using var _ec = ForceExplicitConstructor();
				writeValueFallback(this, entry);
				Write(")");
			}
			else
			{
				// The value lambda spanned multiple lines: close ")" on its own line at the entry call's indent.
				_ = multiline ? WriteLine().Write(")") : Write(")");
			}
		}

		return this;
	}

	/// <summary>
	/// Writes one variant-keyed fluent add call for a single entry of a backed (variant-keyed) dictionary descriptor:
	/// <c>.<paramref name="variant"/>(key, dN =&gt; dN…)</c>, where the method name is the entry value's runtime variant
	/// (e.g. <c>.Keyword("email", dN =&gt; dN…)</c> on a <c>PropertiesDescriptor</c>). Used inside a whole-collection
	/// <c>.Property(dN =&gt; dN…)</c> lambda, so the call appends onto that lambda's descriptor receiver.
	/// <paramref name="writeKey"/> emits the key argument; <paramref name="writeValue"/> emits the value's descriptor
	/// chain onto the fresh parameter <c>dN</c> (allocated by nesting depth so lambdas never shadow). When the value body
	/// is empty the identity lambda <c>dN =&gt; dN</c> would be invalid, so this collapses to the parameterless
	/// <c>.<paramref name="variant"/>(key)</c> overload, which exists exactly for a variant with no required field.
	/// </summary>
	public CodeWriter WriteFluentVariantAdd(string variant, Action<CodeWriter> writeKey, Action<CodeWriter> writeValue)
	{
		WriteLine();
		Write(".").Write(variant).Write("(");
		writeKey(this);

		var beforeSeparator = _builder.Length;
		Write(", ");

		if (!TryWriteDescriptorLambda(beforeSeparator, writeValue, out var multiline))
		{
			// Empty value: keep just `.Variant(key)`; the parameterless overload exists exactly for a
			// variant with no required field.
			return Write(")");
		}

		// The value lambda always starts its first call on a new line, so it spans multiple lines: close ")" on its own line.
		// The line-count delta cascades up the nesting, so the enclosing whole-collection lambda also drops its own ")".
		return multiline ? WriteLine().Write(")") : Write(")");
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
	/// array literal assigns to them. Emits <c>Array.Empty&lt;<paramref name="emptyElementType"/>&gt;()</c>
	/// when there are no items so the expression always compiles with a concrete element type; the
	/// default suits the string-based infer wrappers.
	/// </summary>
	public CodeWriter WriteImplicitArray<T>(IEnumerable<T> items, Action<CodeWriter, T> writeItem, string emptyElementType = "string")
	{
		using var enumerator = items.GetEnumerator();
		if (!enumerator.MoveNext())
			return WriteTypeRef("System.Array").Write(".Empty<").WriteTypeRef(emptyElementType).Write(">()");

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
				else if (Options.UseStronglyTypedDocument && IsDocumentTypeParameter(token))
					_builder.Append(Options.DocumentTypeName); // e.g. IndexRequest<TDocument> -> IndexRequest<MyDocument>
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

		// In strongly-typed-document mode a generic request closed over JsonElement (e.g. IndexRequest<JsonElement>) is
		// rendered against the placeholder document type instead, so the declared variable reads IndexRequest<MyDocument>.
		var documentSubstitution = Options.UseStronglyTypedDocument ? Options.DocumentTypeName : null;
		AppendTypeExpression(expression, type, documentSubstitution);
		return WriteTypeRef(expression.ToString());
	}

	private static void AppendTypeExpression(StringBuilder builder, Type type, string? documentSubstitution)
	{
		// JsonElement (or JsonElement?, e.g. UpdateRequest<JsonElement?, JsonElement?>) is the materialized stand-in for an
		// unspecified document/partial-document type argument; substitute the placeholder type name so the rendered generic
		// argument matches the typed document body.
		if (documentSubstitution is not null
			&& (type == typeof(JsonElement) || Nullable.GetUnderlyingType(type) == typeof(JsonElement)))
		{
			builder.Append(documentSubstitution);
			return;
		}

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

				AppendTypeExpression(builder, args[i], documentSubstitution);
			}

			builder.Append('>');
		}
		else
		{
			builder.Append((type.FullName ?? type.Name).Replace('+', '.'));
		}
	}

	// The open document/source type parameters (ConverterType.Source slots: request documents, Get/Hit results, EQL
	// events). In strongly-typed-document mode they render as the placeholder document type so an explicit constructor
	// reads e.g. `new IndexRequest<MyDocument>()` rather than leaking the open `TDocument` token.
	private static bool IsDocumentTypeParameter(string token) =>
		token is "TDocument" or "TPartialDocument" or "TEvent";

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
				default:
					if (RequiresUnicodeEscape(c))
						_builder.Append("\\u").Append(((int)c).ToString("X4", CultureInfo.InvariantCulture));
					else
						_builder.Append(c);
					break;
			}
		}
	}

	// Control characters are unreadable in a literal; U+0085/U+2028/U+2029 are line terminators to the C#
	// lexer, so a raw occurrence splits the literal and breaks compilation; the private-use area contains
	// the type-ref placeholder sentinels (RefOpen/RefClose), which must never reach the builder as text.
	private static bool RequiresUnicodeEscape(char c) =>
		c < '\u0020'
		|| c == '\u007F'
		|| c == '\u0085'
		|| c == '\u2028'
		|| c == '\u2029'
		|| (c >= '\uE000' && c <= '\uF8FF');

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
