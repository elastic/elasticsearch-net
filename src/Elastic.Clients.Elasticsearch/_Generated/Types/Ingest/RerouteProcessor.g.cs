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

internal sealed partial class RerouteProcessorConverter : System.Text.Json.Serialization.JsonConverter<RerouteProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropDataset = System.Text.Json.JsonEncodedText.Encode("dataset");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropDestination = System.Text.Json.JsonEncodedText.Encode("destination");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropNamespace = System.Text.Json.JsonEncodedText.Encode("namespace");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");

	public override RerouteProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<ICollection<string>?> propDataset = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<string?> propDestination = default;
		LocalJsonValue<string?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<ICollection<string>?> propNamespace = default;
		LocalJsonValue<ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<string?> propTag = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDataset.TryRead(ref reader, options, PropDataset, typeof(SingleOrManyMarker<ICollection<string>?, string>)))
			{
				continue;
			}

			if (propDescription.TryRead(ref reader, options, PropDescription))
			{
				continue;
			}

			if (propDestination.TryRead(ref reader, options, PropDestination))
			{
				continue;
			}

			if (propIf.TryRead(ref reader, options, PropIf))
			{
				continue;
			}

			if (propIgnoreFailure.TryRead(ref reader, options, PropIgnoreFailure))
			{
				continue;
			}

			if (propNamespace.TryRead(ref reader, options, PropNamespace, typeof(SingleOrManyMarker<ICollection<string>?, string>)))
			{
				continue;
			}

			if (propOnFailure.TryRead(ref reader, options, PropOnFailure))
			{
				continue;
			}

			if (propTag.TryRead(ref reader, options, PropTag))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new RerouteProcessor
		{
			Dataset = propDataset.Value
,
			Description = propDescription.Value
,
			Destination = propDestination.Value
,
			If = propIf.Value
,
			IgnoreFailure = propIgnoreFailure.Value
,
			Namespace = propNamespace.Value
,
			OnFailure = propOnFailure.Value
,
			Tag = propTag.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, RerouteProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDataset, value.Dataset, null, typeof(SingleOrManyMarker<ICollection<string>?, string>));
		writer.WriteProperty(options, PropDescription, value.Description);
		writer.WriteProperty(options, PropDestination, value.Destination);
		writer.WriteProperty(options, PropIf, value.If);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure);
		writer.WriteProperty(options, PropNamespace, value.Namespace, null, typeof(SingleOrManyMarker<ICollection<string>?, string>));
		writer.WriteProperty(options, PropOnFailure, value.OnFailure);
		writer.WriteProperty(options, PropTag, value.Tag);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(RerouteProcessorConverter))]
public sealed partial class RerouteProcessor
{
	/// <summary>
	/// <para>
	/// Field references or a static value for the dataset part of the data stream name.
	/// In addition to the criteria for index names, cannot contain - and must be no longer than 100 characters.
	/// Example values are nginx.access and nginx.error.
	/// </para>
	/// <para>
	/// Supports field references with a mustache-like syntax (denoted as {{double}} or {{{triple}}} curly braces).
	/// When resolving field references, the processor replaces invalid characters with _. Uses the &lt;dataset> part
	/// of the index name as a fallback if all field references resolve to a null, missing, or non-string value.
	/// </para>
	/// <para>
	/// default {{data_stream.dataset}}
	/// </para>
	/// </summary>
	public ICollection<string>? Dataset { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// A static value for the target. Can’t be set when the dataset or namespace option is set.
	/// </para>
	/// </summary>
	public string? Destination { get; set; }

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public string? If { get; set; }

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public bool? IgnoreFailure { get; set; }

	/// <summary>
	/// <para>
	/// Field references or a static value for the namespace part of the data stream name. See the criteria for
	/// index names for allowed characters. Must be no longer than 100 characters.
	/// </para>
	/// <para>
	/// Supports field references with a mustache-like syntax (denoted as {{double}} or {{{triple}}} curly braces).
	/// When resolving field references, the processor replaces invalid characters with _. Uses the &lt;namespace> part
	/// of the index name as a fallback if all field references resolve to a null, missing, or non-string value.
	/// </para>
	/// <para>
	/// default {{data_stream.namespace}}
	/// </para>
	/// </summary>
	public ICollection<string>? Namespace { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public string? Tag { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.Processor(RerouteProcessor rerouteProcessor) => Elastic.Clients.Elasticsearch.Ingest.Processor.Reroute(rerouteProcessor);
}

public sealed partial class RerouteProcessorDescriptor<TDocument> : SerializableDescriptor<RerouteProcessorDescriptor<TDocument>>
{
	internal RerouteProcessorDescriptor(Action<RerouteProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RerouteProcessorDescriptor() : base()
	{
	}

	private ICollection<string>? DatasetValue { get; set; }
	private string? DescriptionValue { get; set; }
	private string? DestinationValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private ICollection<string>? NamespaceValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument> OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }
	private string? TagValue { get; set; }

	/// <summary>
	/// <para>
	/// Field references or a static value for the dataset part of the data stream name.
	/// In addition to the criteria for index names, cannot contain - and must be no longer than 100 characters.
	/// Example values are nginx.access and nginx.error.
	/// </para>
	/// <para>
	/// Supports field references with a mustache-like syntax (denoted as {{double}} or {{{triple}}} curly braces).
	/// When resolving field references, the processor replaces invalid characters with _. Uses the &lt;dataset> part
	/// of the index name as a fallback if all field references resolve to a null, missing, or non-string value.
	/// </para>
	/// <para>
	/// default {{data_stream.dataset}}
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor<TDocument> Dataset(ICollection<string>? dataset)
	{
		DatasetValue = dataset;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A static value for the target. Can’t be set when the dataset or namespace option is set.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor<TDocument> Destination(string? destination)
	{
		DestinationValue = destination;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor<TDocument> If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field references or a static value for the namespace part of the data stream name. See the criteria for
	/// index names for allowed characters. Must be no longer than 100 characters.
	/// </para>
	/// <para>
	/// Supports field references with a mustache-like syntax (denoted as {{double}} or {{{triple}}} curly braces).
	/// When resolving field references, the processor replaces invalid characters with _. Uses the &lt;namespace> part
	/// of the index name as a fallback if all field references resolve to a null, missing, or non-string value.
	/// </para>
	/// <para>
	/// default {{data_stream.namespace}}
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor<TDocument> Namespace(ICollection<string>? value)
	{
		NamespaceValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor<TDocument> OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public RerouteProcessorDescriptor<TDocument> OnFailure(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument> descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public RerouteProcessorDescriptor<TDocument> OnFailure(Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public RerouteProcessorDescriptor<TDocument> OnFailure(params Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] configure)
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
	public RerouteProcessorDescriptor<TDocument> Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DatasetValue is not null)
		{
			writer.WritePropertyName("dataset");
			SingleOrManySerializationHelper.Serialize<string>(DatasetValue, writer, options);
		}

		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (!string.IsNullOrEmpty(DestinationValue))
		{
			writer.WritePropertyName("destination");
			writer.WriteStringValue(DestinationValue);
		}

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

		if (NamespaceValue is not null)
		{
			writer.WritePropertyName("namespace");
			SingleOrManySerializationHelper.Serialize<string>(NamespaceValue, writer, options);
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

		writer.WriteEndObject();
	}
}

public sealed partial class RerouteProcessorDescriptor : SerializableDescriptor<RerouteProcessorDescriptor>
{
	internal RerouteProcessorDescriptor(Action<RerouteProcessorDescriptor> configure) => configure.Invoke(this);

	public RerouteProcessorDescriptor() : base()
	{
	}

	private ICollection<string>? DatasetValue { get; set; }
	private string? DescriptionValue { get; set; }
	private string? DestinationValue { get; set; }
	private string? IfValue { get; set; }
	private bool? IgnoreFailureValue { get; set; }
	private ICollection<string>? NamespaceValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailureValue { get; set; }
	private Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor OnFailureDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor> OnFailureDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] OnFailureDescriptorActions { get; set; }
	private string? TagValue { get; set; }

	/// <summary>
	/// <para>
	/// Field references or a static value for the dataset part of the data stream name.
	/// In addition to the criteria for index names, cannot contain - and must be no longer than 100 characters.
	/// Example values are nginx.access and nginx.error.
	/// </para>
	/// <para>
	/// Supports field references with a mustache-like syntax (denoted as {{double}} or {{{triple}}} curly braces).
	/// When resolving field references, the processor replaces invalid characters with _. Uses the &lt;dataset> part
	/// of the index name as a fallback if all field references resolve to a null, missing, or non-string value.
	/// </para>
	/// <para>
	/// default {{data_stream.dataset}}
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor Dataset(ICollection<string>? dataset)
	{
		DatasetValue = dataset;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A static value for the target. Can’t be set when the dataset or namespace option is set.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor Destination(string? destination)
	{
		DestinationValue = destination;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor If(string? value)
	{
		IfValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
	{
		IgnoreFailureValue = ignoreFailure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field references or a static value for the namespace part of the data stream name. See the criteria for
	/// index names for allowed characters. Must be no longer than 100 characters.
	/// </para>
	/// <para>
	/// Supports field references with a mustache-like syntax (denoted as {{double}} or {{{triple}}} curly braces).
	/// When resolving field references, the processor replaces invalid characters with _. Uses the &lt;namespace> part
	/// of the index name as a fallback if all field references resolve to a null, missing, or non-string value.
	/// </para>
	/// <para>
	/// default {{data_stream.namespace}}
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor Namespace(ICollection<string>? value)
	{
		NamespaceValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public RerouteProcessorDescriptor OnFailure(ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? onFailure)
	{
		OnFailureDescriptor = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureValue = onFailure;
		return Self;
	}

	public RerouteProcessorDescriptor OnFailure(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor descriptor)
	{
		OnFailureValue = null;
		OnFailureDescriptorAction = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptor = descriptor;
		return Self;
	}

	public RerouteProcessorDescriptor OnFailure(Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor> configure)
	{
		OnFailureValue = null;
		OnFailureDescriptor = null;
		OnFailureDescriptorActions = null;
		OnFailureDescriptorAction = configure;
		return Self;
	}

	public RerouteProcessorDescriptor OnFailure(params Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] configure)
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
	public RerouteProcessorDescriptor Tag(string? tag)
	{
		TagValue = tag;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DatasetValue is not null)
		{
			writer.WritePropertyName("dataset");
			SingleOrManySerializationHelper.Serialize<string>(DatasetValue, writer, options);
		}

		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (!string.IsNullOrEmpty(DestinationValue))
		{
			writer.WritePropertyName("destination");
			writer.WriteStringValue(DestinationValue);
		}

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

		if (NamespaceValue is not null)
		{
			writer.WritePropertyName("namespace");
			SingleOrManySerializationHelper.Serialize<string>(NamespaceValue, writer, options);
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

		writer.WriteEndObject();
	}
}