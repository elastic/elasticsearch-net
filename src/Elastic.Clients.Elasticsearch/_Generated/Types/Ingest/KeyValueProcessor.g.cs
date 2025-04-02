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

internal sealed partial class KeyValueProcessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropExcludeKeys = System.Text.Json.JsonEncodedText.Encode("exclude_keys");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFieldSplit = System.Text.Json.JsonEncodedText.Encode("field_split");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMissing = System.Text.Json.JsonEncodedText.Encode("ignore_missing");
	private static readonly System.Text.Json.JsonEncodedText PropIncludeKeys = System.Text.Json.JsonEncodedText.Encode("include_keys");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropPrefix = System.Text.Json.JsonEncodedText.Encode("prefix");
	private static readonly System.Text.Json.JsonEncodedText PropStripBrackets = System.Text.Json.JsonEncodedText.Encode("strip_brackets");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");
	private static readonly System.Text.Json.JsonEncodedText PropTargetField = System.Text.Json.JsonEncodedText.Encode("target_field");
	private static readonly System.Text.Json.JsonEncodedText PropTrimKey = System.Text.Json.JsonEncodedText.Encode("trim_key");
	private static readonly System.Text.Json.JsonEncodedText PropTrimValue = System.Text.Json.JsonEncodedText.Encode("trim_value");
	private static readonly System.Text.Json.JsonEncodedText PropValueSplit = System.Text.Json.JsonEncodedText.Encode("value_split");

	public override Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propExcludeKeys = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<string> propFieldSplit = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<bool?> propIgnoreMissing = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propIncludeKeys = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<string?> propPrefix = default;
		LocalJsonValue<bool?> propStripBrackets = default;
		LocalJsonValue<string?> propTag = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propTargetField = default;
		LocalJsonValue<string?> propTrimKey = default;
		LocalJsonValue<string?> propTrimValue = default;
		LocalJsonValue<string> propValueSplit = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propExcludeKeys.TryReadProperty(ref reader, options, PropExcludeKeys, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propFieldSplit.TryReadProperty(ref reader, options, PropFieldSplit, null))
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

			if (propIncludeKeys.TryReadProperty(ref reader, options, PropIncludeKeys, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propOnFailure.TryReadProperty(ref reader, options, PropOnFailure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propPrefix.TryReadProperty(ref reader, options, PropPrefix, null))
			{
				continue;
			}

			if (propStripBrackets.TryReadProperty(ref reader, options, PropStripBrackets, null))
			{
				continue;
			}

			if (propTag.TryReadProperty(ref reader, options, PropTag, null))
			{
				continue;
			}

			if (propTargetField.TryReadProperty(ref reader, options, PropTargetField, null))
			{
				continue;
			}

			if (propTrimKey.TryReadProperty(ref reader, options, PropTrimKey, null))
			{
				continue;
			}

			if (propTrimValue.TryReadProperty(ref reader, options, PropTrimValue, null))
			{
				continue;
			}

			if (propValueSplit.TryReadProperty(ref reader, options, PropValueSplit, null))
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
		return new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			ExcludeKeys = propExcludeKeys.Value,
			Field = propField.Value,
			FieldSplit = propFieldSplit.Value,
			If = propIf.Value,
			IgnoreFailure = propIgnoreFailure.Value,
			IgnoreMissing = propIgnoreMissing.Value,
			IncludeKeys = propIncludeKeys.Value,
			OnFailure = propOnFailure.Value,
			Prefix = propPrefix.Value,
			StripBrackets = propStripBrackets.Value,
			Tag = propTag.Value,
			TargetField = propTargetField.Value,
			TrimKey = propTrimKey.Value,
			TrimValue = propTrimValue.Value,
			ValueSplit = propValueSplit.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropExcludeKeys, value.ExcludeKeys, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFieldSplit, value.FieldSplit, null, null);
		writer.WriteProperty(options, PropIf, value.If, null, null);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, null);
		writer.WriteProperty(options, PropIgnoreMissing, value.IgnoreMissing, null, null);
		writer.WriteProperty(options, PropIncludeKeys, value.IncludeKeys, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropPrefix, value.Prefix, null, null);
		writer.WriteProperty(options, PropStripBrackets, value.StripBrackets, null, null);
		writer.WriteProperty(options, PropTag, value.Tag, null, null);
		writer.WriteProperty(options, PropTargetField, value.TargetField, null, null);
		writer.WriteProperty(options, PropTrimKey, value.TrimKey, null, null);
		writer.WriteProperty(options, PropTrimValue, value.TrimValue, null, null);
		writer.WriteProperty(options, PropValueSplit, value.ValueSplit, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorConverter))]
public sealed partial class KeyValueProcessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeyValueProcessor(Elastic.Clients.Elasticsearch.Field field, string fieldSplit, string valueSplit)
	{
		Field = field;
		FieldSplit = fieldSplit;
		ValueSplit = valueSplit;
	}
#if NET7_0_OR_GREATER
	public KeyValueProcessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KeyValueProcessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KeyValueProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? ExcludeKeys { get; set; }

	/// <summary>
	/// <para>
	/// The field to be parsed.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Regex pattern to use for splitting key-value pairs.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string FieldSplit { get; set; }

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
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? IncludeKeys { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// Prefix to be added to extracted keys.
	/// </para>
	/// </summary>
	public string? Prefix { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>. strip brackets <c>()</c>, <c>&lt;></c>, <c>[]</c> as well as quotes <c>'</c> and <c>"</c> from extracted values.
	/// </para>
	/// </summary>
	public bool? StripBrackets { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public string? Tag { get; set; }

	/// <summary>
	/// <para>
	/// The field to insert the extracted keys into.
	/// Defaults to the root of the document.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? TargetField { get; set; }

	/// <summary>
	/// <para>
	/// String of characters to trim from extracted keys.
	/// </para>
	/// </summary>
	public string? TrimKey { get; set; }

	/// <summary>
	/// <para>
	/// String of characters to trim from extracted values.
	/// </para>
	/// </summary>
	public string? TrimValue { get; set; }

	/// <summary>
	/// <para>
	/// Regex pattern to use for splitting the key from the value within a key-value pair.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ValueSplit { get; set; }
}

public readonly partial struct KeyValueProcessorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeyValueProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeyValueProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor(Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> ExcludeKeys(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.ExcludeKeys = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> ExcludeKeys()
	{
		Instance.ExcludeKeys = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> ExcludeKeys(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.ExcludeKeys = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> ExcludeKeys(params string[] values)
	{
		Instance.ExcludeKeys = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to be parsed.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to be parsed.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Regex pattern to use for splitting key-value pairs.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> FieldSplit(string value)
	{
		Instance.FieldSplit = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> IncludeKeys(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.IncludeKeys = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> IncludeKeys()
	{
		Instance.IncludeKeys = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> IncludeKeys(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.IncludeKeys = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> IncludeKeys(params string[] values)
	{
		Instance.IncludeKeys = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> OnFailure()
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> OnFailure(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
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
	/// Prefix to be added to extracted keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> Prefix(string? value)
	{
		Instance.Prefix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>. strip brackets <c>()</c>, <c>&lt;></c>, <c>[]</c> as well as quotes <c>'</c> and <c>"</c> from extracted values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> StripBrackets(bool? value = true)
	{
		Instance.StripBrackets = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to insert the extracted keys into.
	/// Defaults to the root of the document.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to insert the extracted keys into.
	/// Defaults to the root of the document.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> TargetField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// String of characters to trim from extracted keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> TrimKey(string? value)
	{
		Instance.TrimKey = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// String of characters to trim from extracted values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> TrimValue(string? value)
	{
		Instance.TrimValue = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Regex pattern to use for splitting the key from the value within a key-value pair.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument> ValueSplit(string value)
	{
		Instance.ValueSplit = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct KeyValueProcessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeyValueProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeyValueProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor(Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor ExcludeKeys(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.ExcludeKeys = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor ExcludeKeys()
	{
		Instance.ExcludeKeys = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor ExcludeKeys(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.ExcludeKeys = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to exclude from document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor ExcludeKeys(params string[] values)
	{
		Instance.ExcludeKeys = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to be parsed.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to be parsed.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Regex pattern to use for splitting key-value pairs.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor FieldSplit(string value)
	{
		Instance.FieldSplit = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor IncludeKeys(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.IncludeKeys = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor IncludeKeys()
	{
		Instance.IncludeKeys = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor IncludeKeys(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.IncludeKeys = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// List of keys to filter and insert into document.
	/// Defaults to including all keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor IncludeKeys(params string[] values)
	{
		Instance.IncludeKeys = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor OnFailure()
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor OnFailure(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor OnFailure<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<T>>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
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
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
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
	/// Prefix to be added to extracted keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor Prefix(string? value)
	{
		Instance.Prefix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>. strip brackets <c>()</c>, <c>&lt;></c>, <c>[]</c> as well as quotes <c>'</c> and <c>"</c> from extracted values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor StripBrackets(bool? value = true)
	{
		Instance.StripBrackets = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to insert the extracted keys into.
	/// Defaults to the root of the document.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to insert the extracted keys into.
	/// Defaults to the root of the document.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor TargetField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// String of characters to trim from extracted keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor TrimKey(string? value)
	{
		Instance.TrimKey = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// String of characters to trim from extracted values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor TrimValue(string? value)
	{
		Instance.TrimValue = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Regex pattern to use for splitting the key from the value within a key-value pair.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor ValueSplit(string value)
	{
		Instance.ValueSplit = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessorDescriptor(new Elastic.Clients.Elasticsearch.Ingest.KeyValueProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}