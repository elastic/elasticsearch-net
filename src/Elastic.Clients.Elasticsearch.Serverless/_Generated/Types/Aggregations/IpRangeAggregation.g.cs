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

namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;

public sealed partial class IpRangeAggregation
{
	/// <summary>
	/// <para>The date field whose values are used to build ranges.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field? Field { get; set; }
	[JsonInclude, JsonPropertyName("meta")]
	public IDictionary<string, object>? Meta { get; set; }
	[JsonInclude, JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// <para>Array of IP ranges.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ranges")]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRange>? Ranges { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation(IpRangeAggregation ipRangeAggregation) => Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation.IpRange(ipRangeAggregation);
}

public sealed partial class IpRangeAggregationDescriptor<TDocument> : SerializableDescriptor<IpRangeAggregationDescriptor<TDocument>>
{
	internal IpRangeAggregationDescriptor(Action<IpRangeAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public IpRangeAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Field? FieldValue { get; set; }
	private IDictionary<string, object>? MetaValue { get; set; }
	private string? NameValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRange>? RangesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor RangesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor> RangesDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor>[] RangesDescriptorActions { get; set; }

	/// <summary>
	/// <para>The date field whose values are used to build ranges.</para>
	/// </summary>
	public IpRangeAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date field whose values are used to build ranges.</para>
	/// </summary>
	public IpRangeAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date field whose values are used to build ranges.</para>
	/// </summary>
	public IpRangeAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public IpRangeAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public IpRangeAggregationDescriptor<TDocument> Name(string? name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>Array of IP ranges.</para>
	/// </summary>
	public IpRangeAggregationDescriptor<TDocument> Ranges(ICollection<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRange>? ranges)
	{
		RangesDescriptor = null;
		RangesDescriptorAction = null;
		RangesDescriptorActions = null;
		RangesValue = ranges;
		return Self;
	}

	public IpRangeAggregationDescriptor<TDocument> Ranges(Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor descriptor)
	{
		RangesValue = null;
		RangesDescriptorAction = null;
		RangesDescriptorActions = null;
		RangesDescriptor = descriptor;
		return Self;
	}

	public IpRangeAggregationDescriptor<TDocument> Ranges(Action<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor> configure)
	{
		RangesValue = null;
		RangesDescriptor = null;
		RangesDescriptorActions = null;
		RangesDescriptorAction = configure;
		return Self;
	}

	public IpRangeAggregationDescriptor<TDocument> Ranges(params Action<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor>[] configure)
	{
		RangesValue = null;
		RangesDescriptor = null;
		RangesDescriptorAction = null;
		RangesDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
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

		if (RangesDescriptor is not null)
		{
			writer.WritePropertyName("ranges");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, RangesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (RangesDescriptorAction is not null)
		{
			writer.WritePropertyName("ranges");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor(RangesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (RangesDescriptorActions is not null)
		{
			writer.WritePropertyName("ranges");
			writer.WriteStartArray();
			foreach (var action in RangesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (RangesValue is not null)
		{
			writer.WritePropertyName("ranges");
			JsonSerializer.Serialize(writer, RangesValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class IpRangeAggregationDescriptor : SerializableDescriptor<IpRangeAggregationDescriptor>
{
	internal IpRangeAggregationDescriptor(Action<IpRangeAggregationDescriptor> configure) => configure.Invoke(this);

	public IpRangeAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Field? FieldValue { get; set; }
	private IDictionary<string, object>? MetaValue { get; set; }
	private string? NameValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRange>? RangesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor RangesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor> RangesDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor>[] RangesDescriptorActions { get; set; }

	/// <summary>
	/// <para>The date field whose values are used to build ranges.</para>
	/// </summary>
	public IpRangeAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date field whose values are used to build ranges.</para>
	/// </summary>
	public IpRangeAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date field whose values are used to build ranges.</para>
	/// </summary>
	public IpRangeAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public IpRangeAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public IpRangeAggregationDescriptor Name(string? name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>Array of IP ranges.</para>
	/// </summary>
	public IpRangeAggregationDescriptor Ranges(ICollection<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRange>? ranges)
	{
		RangesDescriptor = null;
		RangesDescriptorAction = null;
		RangesDescriptorActions = null;
		RangesValue = ranges;
		return Self;
	}

	public IpRangeAggregationDescriptor Ranges(Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor descriptor)
	{
		RangesValue = null;
		RangesDescriptorAction = null;
		RangesDescriptorActions = null;
		RangesDescriptor = descriptor;
		return Self;
	}

	public IpRangeAggregationDescriptor Ranges(Action<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor> configure)
	{
		RangesValue = null;
		RangesDescriptor = null;
		RangesDescriptorActions = null;
		RangesDescriptorAction = configure;
		return Self;
	}

	public IpRangeAggregationDescriptor Ranges(params Action<Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor>[] configure)
	{
		RangesValue = null;
		RangesDescriptor = null;
		RangesDescriptorAction = null;
		RangesDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
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

		if (RangesDescriptor is not null)
		{
			writer.WritePropertyName("ranges");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, RangesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (RangesDescriptorAction is not null)
		{
			writer.WritePropertyName("ranges");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor(RangesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (RangesDescriptorActions is not null)
		{
			writer.WritePropertyName("ranges");
			writer.WriteStartArray();
			foreach (var action in RangesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Aggregations.IpRangeAggregationRangeDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (RangesValue is not null)
		{
			writer.WritePropertyName("ranges");
			JsonSerializer.Serialize(writer, RangesValue, options);
		}

		writer.WriteEndObject();
	}
}