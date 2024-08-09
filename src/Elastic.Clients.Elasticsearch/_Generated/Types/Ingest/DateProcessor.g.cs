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

public sealed partial class DateProcessor
{
	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The field to get the date from.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// An array of the expected date formats.
	/// Can be a java time pattern or one of the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("formats")]
	public ICollection<string> Formats { get; set; }

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("if")]
	public string? If { get; set; }

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ignore_failure")]
	public bool? IgnoreFailure { get; set; }

	/// <summary>
	/// <para>
	/// The locale to use when parsing the date, relevant when parsing month names or week days.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("locale")]
	public string? Locale { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("on_failure")]
	public ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tag")]
	public string? Tag { get; set; }

	/// <summary>
	/// <para>
	/// The field that will hold the parsed date.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("target_field")]
	public Elastic.Clients.Elasticsearch.Field? TargetField { get; set; }

	/// <summary>
	/// <para>
	/// The timezone to use when parsing the date.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("timezone")]
	public string? Timezone { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.Processor(DateProcessor dateProcessor) => Elastic.Clients.Elasticsearch.Ingest.Processor.Date(dateProcessor);
}

public sealed partial class DateProcessorDescriptor<TDocument> : SerializableDescriptor<DateProcessorDescriptor<TDocument>>
{
	internal DateProcessorDescriptor(Action<DateProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DateProcessorDescriptor() : base()
	{
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private ICollection<string> FormatsValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private string? LocaleValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument> OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }
	private string? TagValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }
	private string? TimezoneValue { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to get the date from.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to get the date from.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to get the date from.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// An array of the expected date formats.
	/// Can be a java time pattern or one of the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> Formats(ICollection<string> formats)
	{
		FormatsValue = formats;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The locale to use when parsing the date, relevant when parsing month names or week days.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> Locale(string? locale)
	{
		LocaleValue = locale;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public DateProcessorDescriptor<TDocument> OnFailure(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument> descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public DateProcessorDescriptor<TDocument> OnFailure(Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public DateProcessorDescriptor<TDocument> OnFailure(params Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that will hold the parsed date.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that will hold the parsed date.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> TargetField<TValue>(Expression<Func<TDocument, TValue>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that will hold the parsed date.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> TargetField(Expression<Func<TDocument, object>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The timezone to use when parsing the date.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor<TDocument> Timezone(string? timezone)
	{
		TimezoneValue = timezone;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WritePropertyName("formats");
		JsonSerializer.Serialize(writer, FormatsValue, options);
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

		if (!string.IsNullOrEmpty(LocaleValue))
		{
			writer.WritePropertyName("locale");
			writer.WriteStringValue(LocaleValue);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>(OnFailureDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorActions is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			foreach (var action in OnFailureDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (OnFailureValue is not null)
		{
			writer.WritePropertyName("on_failure");
			JsonSerializer.Serialize(writer, OnFailureValue, options);
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

		if (!string.IsNullOrEmpty(TimezoneValue))
		{
			writer.WritePropertyName("timezone");
			writer.WriteStringValue(TimezoneValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class DateProcessorDescriptor : SerializableDescriptor<DateProcessorDescriptor>
{
	internal DateProcessorDescriptor(Action<DateProcessorDescriptor> configure) => configure.Invoke(this);

	public DateProcessorDescriptor() : base()
	{
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private ICollection<string> FormatsValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private string? LocaleValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] OnFailureDescriptorActions { get; set; }
	private string? TagValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }
	private string? TimezoneValue { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to get the date from.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to get the date from.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to get the date from.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// An array of the expected date formats.
	/// Can be a java time pattern or one of the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor Formats(ICollection<string> formats)
	{
		FormatsValue = formats;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The locale to use when parsing the date, relevant when parsing month names or week days.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor Locale(string? locale)
	{
		LocaleValue = locale;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public DateProcessorDescriptor OnFailure(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public DateProcessorDescriptor OnFailure(Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public DateProcessorDescriptor OnFailure(params Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that will hold the parsed date.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that will hold the parsed date.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor TargetField<TDocument, TValue>(Expression<Func<TDocument, TValue>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field that will hold the parsed date.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor TargetField<TDocument>(Expression<Func<TDocument, object>> targetField)
	{
		TargetFieldValue = targetField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The timezone to use when parsing the date.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public DateProcessorDescriptor Timezone(string? timezone)
	{
		TimezoneValue = timezone;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WritePropertyName("formats");
		JsonSerializer.Serialize(writer, FormatsValue, options);
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

		if (!string.IsNullOrEmpty(LocaleValue))
		{
			writer.WritePropertyName("locale");
			writer.WriteStringValue(LocaleValue);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor(OnFailureDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorActions is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			foreach (var action in OnFailureDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (OnFailureValue is not null)
		{
			writer.WritePropertyName("on_failure");
			JsonSerializer.Serialize(writer, OnFailureValue, options);
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

		if (!string.IsNullOrEmpty(TimezoneValue))
		{
			writer.WritePropertyName("timezone");
			writer.WriteStringValue(TimezoneValue);
		}

		writer.WriteEndObject();
	}
}