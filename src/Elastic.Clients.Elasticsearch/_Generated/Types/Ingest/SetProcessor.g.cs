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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Ingest;

public sealed partial class SetProcessor
{
	/// <summary>
	/// <para>The origin field which will be copied to `field`, cannot set `value` simultaneously.<br/>Supported data types are `boolean`, `number`, `array`, `object`, `string`, `date`, etc.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("copy_from")]
	public Elastic.Clients.Elasticsearch.Field? CopyFrom { get; set; }
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// <para>The field to insert, upsert, or update.<br/>Supports template snippets.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }
	[JsonInclude, JsonPropertyName("if")]
	public string? If { get; set; }

	/// <summary>
	/// <para>If `true` and `value` is a template snippet that evaluates to `null` or the empty string, the processor quietly exits without modifying the document.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ignore_empty_value")]
	public bool? IgnoreEmptyValue { get; set; }
	[JsonInclude, JsonPropertyName("ignore_failure")]
	public bool? IgnoreFailure { get; set; }

	/// <summary>
	/// <para>The media type for encoding `value`.<br/>Applies only when value is a template snippet.<br/>Must be one of `application/json`, `text/plain`, or `application/x-www-form-urlencoded`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("media_type")]
	public string? MediaType { get; set; }
	[JsonInclude, JsonPropertyName("on_failure")]
	public ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>If `true` processor will update fields with pre-existing non-null-valued field.<br/>When set to `false`, such fields will not be touched.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("override")]
	public bool? Override { get; set; }
	[JsonInclude, JsonPropertyName("tag")]
	public string? Tag { get; set; }

	/// <summary>
	/// <para>The value to be set for the field.<br/>Supports template snippets.<br/>May specify only one of `value` or `copy_from`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("value")]
	public object? Value { get; set; }

	public static implicit operator Processor(SetProcessor setProcessor) => Ingest.Processor.Set(setProcessor);
}

public sealed partial class SetProcessorDescriptor<TDocument> : SerializableDescriptor<SetProcessorDescriptor<TDocument>>
{
	internal SetProcessorDescriptor(Action<SetProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SetProcessorDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? CopyFromValue { get; set; }
	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreEmptyValueValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private string? MediaTypeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private ProcessorDescriptor<TDocument> OnFailureDescriptor { get; set; }
	private Action<ProcessorDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }
	private Action<ProcessorDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }
	private bool? OverrideValue { get; set; }
	private string? TagValue { get; set; }
	private object? ValueValue { get; set; }

	/// <summary>
	/// <para>The origin field which will be copied to `field`, cannot set `value` simultaneously.<br/>Supported data types are `boolean`, `number`, `array`, `object`, `string`, `date`, etc.</para>
	/// </summary>
	public SetProcessorDescriptor<TDocument> CopyFrom(Elastic.Clients.Elasticsearch.Field? copyFrom)
	{
		CopyFromValue = copyFrom;
		return Self;
	}

	/// <summary>
	/// <para>The origin field which will be copied to `field`, cannot set `value` simultaneously.<br/>Supported data types are `boolean`, `number`, `array`, `object`, `string`, `date`, etc.</para>
	/// </summary>
	public SetProcessorDescriptor<TDocument> CopyFrom<TValue>(Expression<Func<TDocument, TValue>> copyFrom)
	{
		CopyFromValue = copyFrom;
		return Self;
	}

	public SetProcessorDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>The field to insert, upsert, or update.<br/>Supports template snippets.</para>
	/// </summary>
	public SetProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field to insert, upsert, or update.<br/>Supports template snippets.</para>
	/// </summary>
	public SetProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public SetProcessorDescriptor<TDocument> If(string? ifValue)
	{
		IfValue = ifValue;
		return Self;
	}

	/// <summary>
	/// <para>If `true` and `value` is a template snippet that evaluates to `null` or the empty string, the processor quietly exits without modifying the document.</para>
	/// </summary>
	public SetProcessorDescriptor<TDocument> IgnoreEmptyValue(bool? ignoreEmptyValue = true)
	{
		IgnoreEmptyValueValue = ignoreEmptyValue;
		return Self;
	}

	public SetProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>The media type for encoding `value`.<br/>Applies only when value is a template snippet.<br/>Must be one of `application/json`, `text/plain`, or `application/x-www-form-urlencoded`.</para>
	/// </summary>
	public SetProcessorDescriptor<TDocument> MediaType(string? mediaType)
	{
		MediaTypeValue = mediaType;
		return Self;
	}

	public SetProcessorDescriptor<TDocument> OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public SetProcessorDescriptor<TDocument> OnFailure(ProcessorDescriptor<TDocument> descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public SetProcessorDescriptor<TDocument> OnFailure(Action<ProcessorDescriptor<TDocument>> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public SetProcessorDescriptor<TDocument> OnFailure(params Action<ProcessorDescriptor<TDocument>>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>If `true` processor will update fields with pre-existing non-null-valued field.<br/>When set to `false`, such fields will not be touched.</para>
	/// </summary>
	public SetProcessorDescriptor<TDocument> Override(bool? overrideValue = true)
	{
		OverrideValue = overrideValue;
		return Self;
	}

	public SetProcessorDescriptor<TDocument> Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>The value to be set for the field.<br/>Supports template snippets.<br/>May specify only one of `value` or `copy_from`.</para>
	/// </summary>
	public SetProcessorDescriptor<TDocument> Value(object? value)
	{
		ValueValue = value;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CopyFromValue is not null)
		{
			writer.WritePropertyName("copy_from");
			JsonSerializer.Serialize(writer, CopyFromValue, options);
		}

		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (!string.IsNullOrEmpty(IfValue))
		{
			writer.WritePropertyName("if");
			writer.WriteStringValue(IfValue);
		}

		if (IgnoreEmptyValueValue.HasValue)
		{
			writer.WritePropertyName("ignore_empty_value");
			writer.WriteBooleanValue(IgnoreEmptyValueValue.Value);
		}

		if (IgnoreFailureValue.HasValue)
		{
			writer.WritePropertyName("ignore_failure");
			writer.WriteBooleanValue(IgnoreFailureValue.Value);
		}

		if (!string.IsNullOrEmpty(MediaTypeValue))
		{
			writer.WritePropertyName("media_type");
			writer.WriteStringValue(MediaTypeValue);
		}

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

		if (OverrideValue.HasValue)
		{
			writer.WritePropertyName("override");
			writer.WriteBooleanValue(OverrideValue.Value);
		}

		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (ValueValue is not null)
		{
			writer.WritePropertyName("value");
			JsonSerializer.Serialize(writer, ValueValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class SetProcessorDescriptor : SerializableDescriptor<SetProcessorDescriptor>
{
	internal SetProcessorDescriptor(Action<SetProcessorDescriptor> configure) => configure.Invoke(this);

	public SetProcessorDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? CopyFromValue { get; set; }
	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreEmptyValueValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private string? MediaTypeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private ProcessorDescriptor OnFailureDescriptor { get; set; }
	private Action<ProcessorDescriptor> OnFailureDescriptorAction { get; set; }
	private Action<ProcessorDescriptor>[] OnFailureDescriptorActions { get; set; }
	private bool? OverrideValue { get; set; }
	private string? TagValue { get; set; }
	private object? ValueValue { get; set; }

	/// <summary>
	/// <para>The origin field which will be copied to `field`, cannot set `value` simultaneously.<br/>Supported data types are `boolean`, `number`, `array`, `object`, `string`, `date`, etc.</para>
	/// </summary>
	public SetProcessorDescriptor CopyFrom(Elastic.Clients.Elasticsearch.Field? copyFrom)
	{
		CopyFromValue = copyFrom;
		return Self;
	}

	/// <summary>
	/// <para>The origin field which will be copied to `field`, cannot set `value` simultaneously.<br/>Supported data types are `boolean`, `number`, `array`, `object`, `string`, `date`, etc.</para>
	/// </summary>
	public SetProcessorDescriptor CopyFrom<TDocument, TValue>(Expression<Func<TDocument, TValue>> copyFrom)
	{
		CopyFromValue = copyFrom;
		return Self;
	}

	/// <summary>
	/// <para>The origin field which will be copied to `field`, cannot set `value` simultaneously.<br/>Supported data types are `boolean`, `number`, `array`, `object`, `string`, `date`, etc.</para>
	/// </summary>
	public SetProcessorDescriptor CopyFrom<TDocument>(Expression<Func<TDocument, object>> copyFrom)
	{
		CopyFromValue = copyFrom;
		return Self;
	}

	public SetProcessorDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>The field to insert, upsert, or update.<br/>Supports template snippets.</para>
	/// </summary>
	public SetProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field to insert, upsert, or update.<br/>Supports template snippets.</para>
	/// </summary>
	public SetProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field to insert, upsert, or update.<br/>Supports template snippets.</para>
	/// </summary>
	public SetProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public SetProcessorDescriptor If(string? ifValue)
	{
		IfValue = ifValue;
		return Self;
	}

	/// <summary>
	/// <para>If `true` and `value` is a template snippet that evaluates to `null` or the empty string, the processor quietly exits without modifying the document.</para>
	/// </summary>
	public SetProcessorDescriptor IgnoreEmptyValue(bool? ignoreEmptyValue = true)
	{
		IgnoreEmptyValueValue = ignoreEmptyValue;
		return Self;
	}

	public SetProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>The media type for encoding `value`.<br/>Applies only when value is a template snippet.<br/>Must be one of `application/json`, `text/plain`, or `application/x-www-form-urlencoded`.</para>
	/// </summary>
	public SetProcessorDescriptor MediaType(string? mediaType)
	{
		MediaTypeValue = mediaType;
		return Self;
	}

	public SetProcessorDescriptor OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public SetProcessorDescriptor OnFailure(ProcessorDescriptor descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public SetProcessorDescriptor OnFailure(Action<ProcessorDescriptor> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public SetProcessorDescriptor OnFailure(params Action<ProcessorDescriptor>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>If `true` processor will update fields with pre-existing non-null-valued field.<br/>When set to `false`, such fields will not be touched.</para>
	/// </summary>
	public SetProcessorDescriptor Override(bool? overrideValue = true)
	{
		OverrideValue = overrideValue;
		return Self;
	}

	public SetProcessorDescriptor Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>The value to be set for the field.<br/>Supports template snippets.<br/>May specify only one of `value` or `copy_from`.</para>
	/// </summary>
	public SetProcessorDescriptor Value(object? value)
	{
		ValueValue = value;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CopyFromValue is not null)
		{
			writer.WritePropertyName("copy_from");
			JsonSerializer.Serialize(writer, CopyFromValue, options);
		}

		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (!string.IsNullOrEmpty(IfValue))
		{
			writer.WritePropertyName("if");
			writer.WriteStringValue(IfValue);
		}

		if (IgnoreEmptyValueValue.HasValue)
		{
			writer.WritePropertyName("ignore_empty_value");
			writer.WriteBooleanValue(IgnoreEmptyValueValue.Value);
		}

		if (IgnoreFailureValue.HasValue)
		{
			writer.WritePropertyName("ignore_failure");
			writer.WriteBooleanValue(IgnoreFailureValue.Value);
		}

		if (!string.IsNullOrEmpty(MediaTypeValue))
		{
			writer.WritePropertyName("media_type");
			writer.WriteStringValue(MediaTypeValue);
		}

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

		if (OverrideValue.HasValue)
		{
			writer.WritePropertyName("override");
			writer.WriteBooleanValue(OverrideValue.Value);
		}

		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (ValueValue is not null)
		{
			writer.WritePropertyName("value");
			JsonSerializer.Serialize(writer, ValueValue, options);
		}

		writer.WriteEndObject();
	}
}