// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Ingest;

internal sealed partial class GrokProcessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.GrokProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropEcsCompatibility = System.Text.Json.JsonEncodedText.Encode("ecs_compatibility");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMissing = System.Text.Json.JsonEncodedText.Encode("ignore_missing");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropPatternDefinitions = System.Text.Json.JsonEncodedText.Encode("pattern_definitions");
	private static readonly System.Text.Json.JsonEncodedText PropPatterns = System.Text.Json.JsonEncodedText.Encode("patterns");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");
	private static readonly System.Text.Json.JsonEncodedText PropTraceMatch = System.Text.Json.JsonEncodedText.Encode("trace_match");

	public override Elastic.Clients.Elasticsearch.Ingest.GrokProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<string?> propEcsCompatibility = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<bool?> propIgnoreMissing = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propPatternDefinitions = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propPatterns = default;
		LocalJsonValue<string?> propTag = default;
		LocalJsonValue<bool?> propTraceMatch = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propEcsCompatibility.TryReadProperty(ref reader, options, PropEcsCompatibility, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propIf.TryReadProperty(ref reader, options, PropIf, null))
			{
				continue;
			}

			if (propIgnoreFailure.TryReadProperty(ref reader, options, PropIgnoreFailure, null))
			{
				continue;
			}

			if (propIgnoreMissing.TryReadProperty(ref reader, options, PropIgnoreMissing, null))
			{
				continue;
			}

			if (propOnFailure.TryReadProperty(ref reader, options, PropOnFailure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propPatternDefinitions.TryReadProperty(ref reader, options, PropPatternDefinitions, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propPatterns.TryReadProperty(ref reader, options, PropPatterns, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propTag.TryReadProperty(ref reader, options, PropTag, null))
			{
				continue;
			}

			if (propTraceMatch.TryReadProperty(ref reader, options, PropTraceMatch, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Ingest.GrokProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			EcsCompatibility = propEcsCompatibility.Value,
			Field = propField.Value,
			If = propIf.Value,
			IgnoreFailure = propIgnoreFailure.Value,
			IgnoreMissing = propIgnoreMissing.Value,
			OnFailure = propOnFailure.Value,
			PatternDefinitions = propPatternDefinitions.Value,
			Patterns = propPatterns.Value,
			Tag = propTag.Value,
			TraceMatch = propTraceMatch.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.GrokProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropEcsCompatibility, value.EcsCompatibility, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropIf, value.If, null, null);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, null);
		writer.WriteProperty(options, PropIgnoreMissing, value.IgnoreMissing, null, null);
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropPatternDefinitions, value.PatternDefinitions, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropPatterns, value.Patterns, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropTag, value.Tag, null, null);
		writer.WriteProperty(options, PropTraceMatch, value.TraceMatch, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.GrokProcessorConverter))]
public sealed partial class GrokProcessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GrokProcessor(Elastic.Clients.Elasticsearch.Field field, System.Collections.Generic.ICollection<string> patterns)
	{
		Field = field;
		Patterns = patterns;
	}
#if NET7_0_OR_GREATER
	public GrokProcessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GrokProcessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GrokProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// Must be disabled or v1. If v1, the processor uses patterns with Elastic
	/// Common Schema (ECS) field names.
	/// </para>
	/// </summary>
	public string? EcsCompatibility { get; set; }

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Script? If { get; set; }

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public bool? IgnoreFailure { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public bool? IgnoreMissing { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, string>? PatternDefinitions { get; set; }

	/// <summary>
	/// <para>
	/// An ordered list of grok expression to match and extract named captures with.
	/// Returns on the first expression in the list that matches.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> Patterns { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public string? Tag { get; set; }

	/// <summary>
	/// <para>
	/// When <c>true</c>, <c>_ingest._grok_match_index</c> will be inserted into your matched document’s metadata with the index into the pattern found in <c>patterns</c> that matched.
	/// </para>
	/// </summary>
	public bool? TraceMatch { get; set; }
}

public readonly partial struct GrokProcessorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.GrokProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GrokProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.GrokProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GrokProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.GrokProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.GrokProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.GrokProcessor(Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Must be disabled or v1. If v1, the processor uses patterns with Elastic
	/// Common Schema (ECS) field names.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> EcsCompatibility(string? value)
	{
		Instance.EcsCompatibility = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> PatternDefinitions(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.PatternDefinitions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> PatternDefinitions()
	{
		Instance.PatternDefinitions = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> PatternDefinitions(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.PatternDefinitions = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> AddPatternDefinition(string key, string value)
	{
		Instance.PatternDefinitions ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.PatternDefinitions.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// An ordered list of grok expression to match and extract named captures with.
	/// Returns on the first expression in the list that matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> Patterns(System.Collections.Generic.ICollection<string> value)
	{
		Instance.Patterns = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An ordered list of grok expression to match and extract named captures with.
	/// Returns on the first expression in the list that matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> Patterns(params string[] values)
	{
		Instance.Patterns = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// When <c>true</c>, <c>_ingest._grok_match_index</c> will be inserted into your matched document’s metadata with the index into the pattern found in <c>patterns</c> that matched.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument> TraceMatch(bool? value = true)
	{
		Instance.TraceMatch = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.GrokProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.GrokProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GrokProcessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.GrokProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GrokProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.GrokProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GrokProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.GrokProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.GrokProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.GrokProcessor(Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Must be disabled or v1. If v1, the processor uses patterns with Elastic
	/// Common Schema (ECS) field names.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor EcsCompatibility(string? value)
	{
		Instance.EcsCompatibility = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor PatternDefinitions(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.PatternDefinitions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor PatternDefinitions()
	{
		Instance.PatternDefinitions = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor PatternDefinitions(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.PatternDefinitions = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor AddPatternDefinition(string key, string value)
	{
		Instance.PatternDefinitions ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.PatternDefinitions.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// An ordered list of grok expression to match and extract named captures with.
	/// Returns on the first expression in the list that matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor Patterns(System.Collections.Generic.ICollection<string> value)
	{
		Instance.Patterns = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An ordered list of grok expression to match and extract named captures with.
	/// Returns on the first expression in the list that matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor Patterns(params string[] values)
	{
		Instance.Patterns = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// When <c>true</c>, <c>_ingest._grok_match_index</c> will be inserted into your matched document’s metadata with the index into the pattern found in <c>patterns</c> that matched.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor TraceMatch(bool? value = true)
	{
		Instance.TraceMatch = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.GrokProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.GrokProcessorDescriptor(new Elastic.Clients.Elasticsearch.Ingest.GrokProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}