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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Ingest;

public sealed partial class GrokProcessor
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
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

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
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pattern_definitions")]
	public IDictionary<string, string>? PatternDefinitions { get; set; }

	/// <summary>
	/// <para>
	/// An ordered list of grok expression to match and extract named captures with.
	/// Returns on the first expression in the list that matches.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("patterns")]
	public ICollection<string> Patterns { get; set; }

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
	/// When <c>true</c>, <c>_ingest._grok_match_index</c> will be inserted into your matched document’s metadata with the index into the pattern found in <c>patterns</c> that matched.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("trace_match")]
	public bool? TraceMatch { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Ingest.Processor(GrokProcessor grokProcessor) => Elastic.Clients.Elasticsearch.Serverless.Ingest.Processor.Grok(grokProcessor);
}

public sealed partial class GrokProcessorDescriptor<TDocument> : SerializableDescriptor<GrokProcessorDescriptor<TDocument>>
{
	internal GrokProcessorDescriptor(Action<GrokProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GrokProcessorDescriptor() : base()
	{
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private bool? IgnoreMissingValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor<TDocument> OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }
	private IDictionary<string, string>? PatternDefinitionsValue { get; set; }
	private ICollection<string> PatternsValue { get; set; }
	private string? TagValue { get; set; }
	private bool? TraceMatchValue { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> IgnoreMissing(bool? ignoreMissing = true)
	{
		IgnoreMissingValue = ignoreMissing;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> OnFailure(ICollection<Elastic.Clients.Elasticsearch.Serverless.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public GrokProcessorDescriptor<TDocument> OnFailure(Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor<TDocument> descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public GrokProcessorDescriptor<TDocument> OnFailure(Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor<TDocument>> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public GrokProcessorDescriptor<TDocument> OnFailure(params Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor<TDocument>>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> PatternDefinitions(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		PatternDefinitionsValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// An ordered list of grok expression to match and extract named captures with.
	/// Returns on the first expression in the list that matches.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> Patterns(ICollection<string> patterns)
	{
		PatternsValue = patterns;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// When <c>true</c>, <c>_ingest._grok_match_index</c> will be inserted into your matched document’s metadata with the index into the pattern found in <c>patterns</c> that matched.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor<TDocument> TraceMatch(bool? traceMatch = true)
	{
		TraceMatchValue = traceMatch;
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor<TDocument>(OnFailureDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorActions is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			foreach (var action in OnFailureDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor<TDocument>(action), options);
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
		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (TraceMatchValue.HasValue)
		{
			writer.WritePropertyName("trace_match");
			writer.WriteBooleanValue(TraceMatchValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class GrokProcessorDescriptor : SerializableDescriptor<GrokProcessorDescriptor>
{
	internal GrokProcessorDescriptor(Action<GrokProcessorDescriptor> configure) => configure.Invoke(this);

	public GrokProcessorDescriptor() : base()
	{
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private bool? IgnoreMissingValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor>[] OnFailureDescriptorActions { get; set; }
	private IDictionary<string, string>? PatternDefinitionsValue { get; set; }
	private ICollection<string> PatternsValue { get; set; }
	private string? TagValue { get; set; }
	private bool? TraceMatchValue { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to use for grok expression parsing.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor IgnoreMissing(bool? ignoreMissing = true)
	{
		IgnoreMissingValue = ignoreMissing;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor OnFailure(ICollection<Elastic.Clients.Elasticsearch.Serverless.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public GrokProcessorDescriptor OnFailure(Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public GrokProcessorDescriptor OnFailure(Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public GrokProcessorDescriptor OnFailure(params Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor>[] configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A map of pattern-name and pattern tuples defining custom patterns to be used by the current processor.
	/// Patterns matching existing names will override the pre-existing definition.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor PatternDefinitions(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		PatternDefinitionsValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// An ordered list of grok expression to match and extract named captures with.
	/// Returns on the first expression in the list that matches.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor Patterns(ICollection<string> patterns)
	{
		PatternsValue = patterns;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// When <c>true</c>, <c>_ingest._grok_match_index</c> will be inserted into your matched document’s metadata with the index into the pattern found in <c>patterns</c> that matched.
	/// </para>
	/// </summary>
	public GrokProcessorDescriptor TraceMatch(bool? traceMatch = true)
	{
		TraceMatchValue = traceMatch;
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor(OnFailureDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (OnFailureDescriptorActions is not null)
		{
			writer.WritePropertyName("on_failure");
			writer.WriteStartArray();
			foreach (var action in OnFailureDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Ingest.ProcessorDescriptor(action), options);
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
		if (!string.IsNullOrEmpty(TagValue))
		{
			writer.WritePropertyName("tag");
			writer.WriteStringValue(TagValue);
		}

		if (TraceMatchValue.HasValue)
		{
			writer.WritePropertyName("trace_match");
			writer.WriteBooleanValue(TraceMatchValue.Value);
		}

		writer.WriteEndObject();
	}
}