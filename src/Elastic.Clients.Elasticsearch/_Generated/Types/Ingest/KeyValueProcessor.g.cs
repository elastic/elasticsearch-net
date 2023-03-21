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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ingest;
public sealed partial class KeyValueProcessor
{
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; set; }

	[JsonInclude, JsonPropertyName("exclude_keys")]
	public ICollection<string>? ExcludeKeys { get; set; }

	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	[JsonInclude, JsonPropertyName("field_split")]
	public string FieldSplit { get; set; }

	[JsonInclude, JsonPropertyName("if")]
	public string? If { get; set; }

	[JsonInclude, JsonPropertyName("ignore_failure")]
	public bool? IgnoreFailure { get; set; }

	[JsonInclude, JsonPropertyName("ignore_missing")]
	public bool? IgnoreMissing { get; set; }

	[JsonInclude, JsonPropertyName("include_keys")]
	public ICollection<string>? IncludeKeys { get; set; }

	[JsonInclude, JsonPropertyName("on_failure")]
	public ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	[JsonInclude, JsonPropertyName("prefix")]
	public string? Prefix { get; set; }

	[JsonInclude, JsonPropertyName("strip_brackets")]
	public bool? StripBrackets { get; set; }

	[JsonInclude, JsonPropertyName("tag")]
	public string? Tag { get; set; }

	[JsonInclude, JsonPropertyName("target_field")]
	public Elastic.Clients.Elasticsearch.Field? TargetField { get; set; }

	[JsonInclude, JsonPropertyName("trim_key")]
	public string? TrimKey { get; set; }

	[JsonInclude, JsonPropertyName("trim_value")]
	public string? TrimValue { get; set; }

	[JsonInclude, JsonPropertyName("value_split")]
	public string ValueSplit { get; set; }

	public static implicit operator Processor(KeyValueProcessor keyValueProcessor) => Ingest.Processor.Kv(keyValueProcessor);
}

public sealed partial class KeyValueProcessorDescriptor<TDocument> : SerializableDescriptor<KeyValueProcessorDescriptor<TDocument>>
{
	internal KeyValueProcessorDescriptor(Action<KeyValueProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);
	public KeyValueProcessorDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }

	private ProcessorDescriptor<TDocument> OnFailureDescriptor { get; set; }

	private Action<ProcessorDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }

	private Action<ProcessorDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }

	private string? DescriptionValue { get; set; }

	private ICollection<string>? ExcludeKeysValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

	private string FieldSplitValue { get; set; }

	private string? IfValue { get; set; }

	private bool? IgnoreFailureValue { get; set; }

	private bool? IgnoreMissingValue { get; set; }

	private ICollection<string>? IncludeKeysValue { get; set; }

	private string? PrefixValue { get; set; }

	private bool? StripBracketsValue { get; set; }

	private string? TagValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }

	private string? TrimKeyValue { get; set; }

	private string? TrimValueValue { get; set; }

	private string ValueSplitValue { get; set; }

	public KeyValueProcessorDescriptor<TDocument> OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> OnFailure(ProcessorDescriptor<TDocument> descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> OnFailure(Action<ProcessorDescriptor<TDocument>> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> OnFailure(params Action<ProcessorDescriptor<TDocument>>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> ExcludeKeys(ICollection<string>? excludeKeys)
	{
		ExcludeKeysValue = excludeKeys;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> FieldSplit(string fieldSplit)
	{
		FieldSplitValue = fieldSplit;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> If(string? ifValue)
	{
		IfValue = ifValue;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> IgnoreMissing(bool? ignoreMissing = true)
	{
		IgnoreMissingValue = ignoreMissing;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> IncludeKeys(ICollection<string>? includeKeys)
	{
		IncludeKeysValue = includeKeys;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> Prefix(string? prefix)
	{
		PrefixValue = prefix;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> StripBrackets(bool? stripBrackets = true)
	{
		StripBracketsValue = stripBrackets;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> TargetField<TValue>(Expression<Func<TDocument, TValue>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> TrimKey(string? trimKey)
	{
		TrimKeyValue = trimKey;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> TrimValue(string? trimValue)
	{
		TrimValueValue = trimValue;
		return Self;
	}

	public KeyValueProcessorDescriptor<TDocument> ValueSplit(string valueSplit)
	{
		ValueSplitValue = valueSplit;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (OnFailureDescriptor is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, OnFailureDescriptor, options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorAction is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new ProcessorDescriptor<TDocument>(OnFailureDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorActions is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			foreach (var action in OnFailureDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new ProcessorDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (OnFailureValue is not null)
		{
			writer.WritePropertyName("on_failure");
			JsonSerializer.Serialize(writer, OnFailureValue, options);
		}

		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (ExcludeKeysValue is not null)
		{
			writer.WritePropertyName("exclude_keys");
			JsonSerializer.Serialize(writer, ExcludeKeysValue, options);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WritePropertyName("field_split");
		writer.WriteStringValue(FieldSplitValue);
		if (!string.IsNullOrEmpty(IfValue))
		{
			writer.WritePropertyName("if");
			writer.WriteStringValue(IfValue);
		}

		if (IgnoreFailureValue.HasValue)
		{
			writer.WritePropertyName("ignore_failure");
			writer.WriteBooleanValue(IgnoreFailureValue.Value);
		}

		if (IgnoreMissingValue.HasValue)
		{
			writer.WritePropertyName("ignore_missing");
			writer.WriteBooleanValue(IgnoreMissingValue.Value);
		}

		if (IncludeKeysValue is not null)
		{
			writer.WritePropertyName("include_keys");
			JsonSerializer.Serialize(writer, IncludeKeysValue, options);
		}

		if (!string.IsNullOrEmpty(PrefixValue))
		{
			writer.WritePropertyName("prefix");
			writer.WriteStringValue(PrefixValue);
		}

		if (StripBracketsValue.HasValue)
		{
			writer.WritePropertyName("strip_brackets");
			writer.WriteBooleanValue(StripBracketsValue.Value);
		}

		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (TargetFieldValue is not null)
		{
			writer.WritePropertyName("target_field");
			JsonSerializer.Serialize(writer, TargetFieldValue, options);
		}

		if (!string.IsNullOrEmpty(TrimKeyValue))
		{
			writer.WritePropertyName("trim_key");
			writer.WriteStringValue(TrimKeyValue);
		}

		if (!string.IsNullOrEmpty(TrimValueValue))
		{
			writer.WritePropertyName("trim_value");
			writer.WriteStringValue(TrimValueValue);
		}

		writer.WritePropertyName("value_split");
		writer.WriteStringValue(ValueSplitValue);
		writer.WriteEndObject();
	}
}

public sealed partial class KeyValueProcessorDescriptor : SerializableDescriptor<KeyValueProcessorDescriptor>
{
	internal KeyValueProcessorDescriptor(Action<KeyValueProcessorDescriptor> configure) => configure.Invoke(this);
	public KeyValueProcessorDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }

	private ProcessorDescriptor OnFailureDescriptor { get; set; }

	private Action<ProcessorDescriptor> OnFailureDescriptorAction { get; set; }

	private Action<ProcessorDescriptor>[] OnFailureDescriptorActions { get; set; }

	private string? DescriptionValue { get; set; }

	private ICollection<string>? ExcludeKeysValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

	private string FieldSplitValue { get; set; }

	private string? IfValue { get; set; }

	private bool? IgnoreFailureValue { get; set; }

	private bool? IgnoreMissingValue { get; set; }

	private ICollection<string>? IncludeKeysValue { get; set; }

	private string? PrefixValue { get; set; }

	private bool? StripBracketsValue { get; set; }

	private string? TagValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }

	private string? TrimKeyValue { get; set; }

	private string? TrimValueValue { get; set; }

	private string ValueSplitValue { get; set; }

	public KeyValueProcessorDescriptor OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public KeyValueProcessorDescriptor OnFailure(ProcessorDescriptor descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public KeyValueProcessorDescriptor OnFailure(Action<ProcessorDescriptor> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public KeyValueProcessorDescriptor OnFailure(params Action<ProcessorDescriptor>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	public KeyValueProcessorDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	public KeyValueProcessorDescriptor ExcludeKeys(ICollection<string>? excludeKeys)
	{
		ExcludeKeysValue = excludeKeys;
		return Self;
	}

	public KeyValueProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public KeyValueProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public KeyValueProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public KeyValueProcessorDescriptor FieldSplit(string fieldSplit)
	{
		FieldSplitValue = fieldSplit;
		return Self;
	}

	public KeyValueProcessorDescriptor If(string? ifValue)
	{
		IfValue = ifValue;
		return Self;
	}

	public KeyValueProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	public KeyValueProcessorDescriptor IgnoreMissing(bool? ignoreMissing = true)
	{
		IgnoreMissingValue = ignoreMissing;
		return Self;
	}

	public KeyValueProcessorDescriptor IncludeKeys(ICollection<string>? includeKeys)
	{
		IncludeKeysValue = includeKeys;
		return Self;
	}

	public KeyValueProcessorDescriptor Prefix(string? prefix)
	{
		PrefixValue = prefix;
		return Self;
	}

	public KeyValueProcessorDescriptor StripBrackets(bool? stripBrackets = true)
	{
		StripBracketsValue = stripBrackets;
		return Self;
	}

	public KeyValueProcessorDescriptor Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	public KeyValueProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	public KeyValueProcessorDescriptor TargetField<TDocument, TValue>(Expression<Func<TDocument, TValue>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	public KeyValueProcessorDescriptor TargetField<TDocument>(Expression<Func<TDocument, object>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	public KeyValueProcessorDescriptor TrimKey(string? trimKey)
	{
		TrimKeyValue = trimKey;
		return Self;
	}

	public KeyValueProcessorDescriptor TrimValue(string? trimValue)
	{
		TrimValueValue = trimValue;
		return Self;
	}

	public KeyValueProcessorDescriptor ValueSplit(string valueSplit)
	{
		ValueSplitValue = valueSplit;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (OnFailureDescriptor is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, OnFailureDescriptor, options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorAction is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new ProcessorDescriptor(OnFailureDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorActions is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			foreach (var action in OnFailureDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new ProcessorDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (OnFailureValue is not null)
		{
			writer.WritePropertyName("on_failure");
			JsonSerializer.Serialize(writer, OnFailureValue, options);
		}

		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (ExcludeKeysValue is not null)
		{
			writer.WritePropertyName("exclude_keys");
			JsonSerializer.Serialize(writer, ExcludeKeysValue, options);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WritePropertyName("field_split");
		writer.WriteStringValue(FieldSplitValue);
		if (!string.IsNullOrEmpty(IfValue))
		{
			writer.WritePropertyName("if");
			writer.WriteStringValue(IfValue);
		}

		if (IgnoreFailureValue.HasValue)
		{
			writer.WritePropertyName("ignore_failure");
			writer.WriteBooleanValue(IgnoreFailureValue.Value);
		}

		if (IgnoreMissingValue.HasValue)
		{
			writer.WritePropertyName("ignore_missing");
			writer.WriteBooleanValue(IgnoreMissingValue.Value);
		}

		if (IncludeKeysValue is not null)
		{
			writer.WritePropertyName("include_keys");
			JsonSerializer.Serialize(writer, IncludeKeysValue, options);
		}

		if (!string.IsNullOrEmpty(PrefixValue))
		{
			writer.WritePropertyName("prefix");
			writer.WriteStringValue(PrefixValue);
		}

		if (StripBracketsValue.HasValue)
		{
			writer.WritePropertyName("strip_brackets");
			writer.WriteBooleanValue(StripBracketsValue.Value);
		}

		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (TargetFieldValue is not null)
		{
			writer.WritePropertyName("target_field");
			JsonSerializer.Serialize(writer, TargetFieldValue, options);
		}

		if (!string.IsNullOrEmpty(TrimKeyValue))
		{
			writer.WritePropertyName("trim_key");
			writer.WriteStringValue(TrimKeyValue);
		}

		if (!string.IsNullOrEmpty(TrimValueValue))
		{
			writer.WritePropertyName("trim_value");
			writer.WriteStringValue(TrimValueValue);
		}

		writer.WritePropertyName("value_split");
		writer.WriteStringValue(ValueSplitValue);
		writer.WriteEndObject();
	}
}