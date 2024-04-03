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

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class CompositeAggregation
{
	/// <summary>
	/// <para>When paginating, use the `after_key` value returned in the previous response to retrieve the next page.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("after")]
	public IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>? After { get; set; }
	[JsonInclude, JsonPropertyName("meta")]
	public IDictionary<string, object>? Meta { get; set; }
	[JsonInclude, JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// <para>The number of composite buckets that should be returned.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }

	/// <summary>
	/// <para>The value sources used to build composite buckets.<br/>Keys are returned in the order of the `sources` definition.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("sources")]
	public ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregationSource>>? Sources { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Aggregation(CompositeAggregation compositeAggregation) => Elastic.Clients.Elasticsearch.Aggregations.Aggregation.Composite(compositeAggregation);
}

public sealed partial class CompositeAggregationDescriptor<TDocument> : SerializableDescriptor<CompositeAggregationDescriptor<TDocument>>
{
	internal CompositeAggregationDescriptor(Action<CompositeAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public CompositeAggregationDescriptor() : base()
	{
	}

	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>? AfterValue { get; set; }
	private IDictionary<string, object>? MetaValue { get; set; }
	private string? NameValue { get; set; }
	private int? SizeValue { get; set; }
	private ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregationSource>>? SourcesValue { get; set; }

	/// <summary>
	/// <para>When paginating, use the `after_key` value returned in the previous response to retrieve the next page.</para>
	/// </summary>
	public CompositeAggregationDescriptor<TDocument> After(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>> selector)
	{
		AfterValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>());
		return Self;
	}

	public CompositeAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public CompositeAggregationDescriptor<TDocument> Name(string? name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>The number of composite buckets that should be returned.</para>
	/// </summary>
	public CompositeAggregationDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>The value sources used to build composite buckets.<br/>Keys are returned in the order of the `sources` definition.</para>
	/// </summary>
	public CompositeAggregationDescriptor<TDocument> Sources(ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregationSource>>? sources)
	{
		SourcesValue = sources;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AfterValue is not null)
		{
			writer.WritePropertyName("after");
			JsonSerializer.Serialize(writer, AfterValue, options);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (!string.IsNullOrEmpty(NameValue))
		{
			writer.WritePropertyName("name");
			writer.WriteStringValue(NameValue);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (SourcesValue is not null)
		{
			writer.WritePropertyName("sources");
			JsonSerializer.Serialize(writer, SourcesValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class CompositeAggregationDescriptor : SerializableDescriptor<CompositeAggregationDescriptor>
{
	internal CompositeAggregationDescriptor(Action<CompositeAggregationDescriptor> configure) => configure.Invoke(this);

	public CompositeAggregationDescriptor() : base()
	{
	}

	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>? AfterValue { get; set; }
	private IDictionary<string, object>? MetaValue { get; set; }
	private string? NameValue { get; set; }
	private int? SizeValue { get; set; }
	private ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregationSource>>? SourcesValue { get; set; }

	/// <summary>
	/// <para>When paginating, use the `after_key` value returned in the previous response to retrieve the next page.</para>
	/// </summary>
	public CompositeAggregationDescriptor After(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>> selector)
	{
		AfterValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldValue>());
		return Self;
	}

	public CompositeAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public CompositeAggregationDescriptor Name(string? name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>The number of composite buckets that should be returned.</para>
	/// </summary>
	public CompositeAggregationDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>The value sources used to build composite buckets.<br/>Keys are returned in the order of the `sources` definition.</para>
	/// </summary>
	public CompositeAggregationDescriptor Sources(ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregationSource>>? sources)
	{
		SourcesValue = sources;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AfterValue is not null)
		{
			writer.WritePropertyName("after");
			JsonSerializer.Serialize(writer, AfterValue, options);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (!string.IsNullOrEmpty(NameValue))
		{
			writer.WritePropertyName("name");
			writer.WriteStringValue(NameValue);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (SourcesValue is not null)
		{
			writer.WritePropertyName("sources");
			JsonSerializer.Serialize(writer, SourcesValue, options);
		}

		writer.WriteEndObject();
	}
}