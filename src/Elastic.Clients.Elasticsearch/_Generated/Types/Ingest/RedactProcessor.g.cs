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

public sealed partial class RedactProcessor
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
	/// The field to be redacted
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

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
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ignore_missing")]
	public bool? IgnoreMissing { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("on_failure")]
	public ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }
	[JsonInclude, JsonPropertyName("pattern_definitions")]
	public IDictionary<string, string>? PatternDefinitions { get; set; }

	/// <summary>
	/// <para>
	/// A list of grok expressions to match and redact named captures with
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("patterns")]
	public ICollection<string> Patterns { get; set; }

	/// <summary>
	/// <para>
	/// Start a redacted section with this token
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prefix")]
	public string? Prefix { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c> and the current license does not support running redact processors, then the processor quietly exits without modifying the document
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("skip_if_unlicensed")]
	public bool? SkipIfUnlicensed { get; set; }

	/// <summary>
	/// <para>
	/// End a redacted section with this token
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("suffix")]
	public string? Suffix { get; set; }

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
	/// If <c>true</c> then ingest metadata <c>_ingest._redact._is_redacted</c> is set to <c>true</c> if the document has been redacted
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("trace_redact")]
	public bool? TraceRedact { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.Processor(RedactProcessor redactProcessor) => Elastic.Clients.Elasticsearch.Ingest.Processor.Redact(redactProcessor);
}

public sealed partial class RedactProcessorDescriptor<TDocument> : SerializableDescriptor<RedactProcessorDescriptor<TDocument>>
{
	internal RedactProcessorDescriptor(Action<RedactProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RedactProcessorDescriptor() : base()
	{
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private bool? IgnoreMissingValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument> OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }
	private IDictionary<string, string>? PatternDefinitionsValue { get; set; }
	private ICollection<string> PatternsValue { get; set; }
	private string? PrefixValue { get; set; }
	private bool? SkipIfUnlicensedValue { get; set; }
	private string? SuffixValue { get; set; }
	private string? TagValue { get; set; }
	private bool? TraceRedactValue { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to be redacted
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to be redacted
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to be redacted
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> IgnoreMissing(bool? ignoreMissing = true)
	{
		IgnoreMissingValue = ignoreMissing;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public RedactProcessorDescriptor<TDocument> OnFailure(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument> descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public RedactProcessorDescriptor<TDocument> OnFailure(Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public RedactProcessorDescriptor<TDocument> OnFailure(params Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	public RedactProcessorDescriptor<TDocument> PatternDefinitions(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		PatternDefinitionsValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of grok expressions to match and redact named captures with
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> Patterns(ICollection<string> patterns)
	{
		PatternsValue = patterns;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Start a redacted section with this token
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> Prefix(string? prefix)
	{
		PrefixValue = prefix;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and the current license does not support running redact processors, then the processor quietly exits without modifying the document
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> SkipIfUnlicensed(bool? skipIfUnlicensed = true)
	{
		SkipIfUnlicensedValue = skipIfUnlicensed;
		return Self;
	}

	/// <summary>
	/// <para>
	/// End a redacted section with this token
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> Suffix(string? suffix)
	{
		SuffixValue = suffix;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> then ingest metadata <c>_ingest._redact._is_redacted</c> is set to <c>true</c> if the document has been redacted
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor<TDocument> TraceRedact(bool? traceRedact = true)
	{
		TraceRedactValue = traceRedact;
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

		if (PatternDefinitionsValue is not null)
		{
			writer.WritePropertyName("pattern_definitions");
			JsonSerializer.Serialize(writer, PatternDefinitionsValue, options);
		}

		writer.WritePropertyName("patterns");
		JsonSerializer.Serialize(writer, PatternsValue, options);
		if (!string.IsNullOrEmpty(PrefixValue))
		{
			writer.WritePropertyName("prefix");
			writer.WriteStringValue(PrefixValue);
		}

		if (SkipIfUnlicensedValue.HasValue)
		{
			writer.WritePropertyName("skip_if_unlicensed");
			writer.WriteBooleanValue(SkipIfUnlicensedValue.Value);
		}

		if (!string.IsNullOrEmpty(SuffixValue))
		{
			writer.WritePropertyName("suffix");
			writer.WriteStringValue(SuffixValue);
		}

		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (TraceRedactValue.HasValue)
		{
			writer.WritePropertyName("trace_redact");
			writer.WriteBooleanValue(TraceRedactValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class RedactProcessorDescriptor : SerializableDescriptor<RedactProcessorDescriptor>
{
	internal RedactProcessorDescriptor(Action<RedactProcessorDescriptor> configure) => configure.Invoke(this);

	public RedactProcessorDescriptor() : base()
	{
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private bool? IgnoreMissingValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] OnFailureDescriptorActions { get; set; }
	private IDictionary<string, string>? PatternDefinitionsValue { get; set; }
	private ICollection<string> PatternsValue { get; set; }
	private string? PrefixValue { get; set; }
	private bool? SkipIfUnlicensedValue { get; set; }
	private string? SuffixValue { get; set; }
	private string? TagValue { get; set; }
	private bool? TraceRedactValue { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to be redacted
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to be redacted
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to be redacted
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor IgnoreMissing(bool? ignoreMissing = true)
	{
		IgnoreMissingValue = ignoreMissing;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public RedactProcessorDescriptor OnFailure(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public RedactProcessorDescriptor OnFailure(Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public RedactProcessorDescriptor OnFailure(params Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	public RedactProcessorDescriptor PatternDefinitions(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		PatternDefinitionsValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of grok expressions to match and redact named captures with
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor Patterns(ICollection<string> patterns)
	{
		PatternsValue = patterns;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Start a redacted section with this token
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor Prefix(string? prefix)
	{
		PrefixValue = prefix;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and the current license does not support running redact processors, then the processor quietly exits without modifying the document
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor SkipIfUnlicensed(bool? skipIfUnlicensed = true)
	{
		SkipIfUnlicensedValue = skipIfUnlicensed;
		return Self;
	}

	/// <summary>
	/// <para>
	/// End a redacted section with this token
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor Suffix(string? suffix)
	{
		SuffixValue = suffix;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> then ingest metadata <c>_ingest._redact._is_redacted</c> is set to <c>true</c> if the document has been redacted
	/// </para>
	/// </summary>
	public RedactProcessorDescriptor TraceRedact(bool? traceRedact = true)
	{
		TraceRedactValue = traceRedact;
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

		if (PatternDefinitionsValue is not null)
		{
			writer.WritePropertyName("pattern_definitions");
			JsonSerializer.Serialize(writer, PatternDefinitionsValue, options);
		}

		writer.WritePropertyName("patterns");
		JsonSerializer.Serialize(writer, PatternsValue, options);
		if (!string.IsNullOrEmpty(PrefixValue))
		{
			writer.WritePropertyName("prefix");
			writer.WriteStringValue(PrefixValue);
		}

		if (SkipIfUnlicensedValue.HasValue)
		{
			writer.WritePropertyName("skip_if_unlicensed");
			writer.WriteBooleanValue(SkipIfUnlicensedValue.Value);
		}

		if (!string.IsNullOrEmpty(SuffixValue))
		{
			writer.WritePropertyName("suffix");
			writer.WriteStringValue(SuffixValue);
		}

		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (TraceRedactValue.HasValue)
		{
			writer.WritePropertyName("trace_redact");
			writer.WriteBooleanValue(TraceRedactValue.Value);
		}

		writer.WriteEndObject();
	}
}