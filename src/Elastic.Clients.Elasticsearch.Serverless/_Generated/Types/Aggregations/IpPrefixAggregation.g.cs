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

public sealed partial class IpPrefixAggregation
{
	/// <summary>
	/// <para>
	/// Defines whether the prefix length is appended to IP address keys in the response.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("append_prefix_length")]
	public bool? AppendPrefixLength { get; set; }

	/// <summary>
	/// <para>
	/// The IP address field to aggregation on. The field mapping type must be <c>ip</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Defines whether the prefix applies to IPv6 addresses.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("is_ipv6")]
	public bool? IsIpv6 { get; set; }

	/// <summary>
	/// <para>
	/// Minimum number of documents in a bucket for it to be included in the response.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("min_doc_count")]
	public long? MinDocCount { get; set; }

	/// <summary>
	/// <para>
	/// Length of the network prefix. For IPv4 addresses the accepted range is [0, 32].
	/// For IPv6 addresses the accepted range is [0, 128].
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prefix_length")]
	public int PrefixLength { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation(IpPrefixAggregation ipPrefixAggregation) => Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation.IpPrefix(ipPrefixAggregation);
}

public sealed partial class IpPrefixAggregationDescriptor<TDocument> : SerializableDescriptor<IpPrefixAggregationDescriptor<TDocument>>
{
	internal IpPrefixAggregationDescriptor(Action<IpPrefixAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public IpPrefixAggregationDescriptor() : base()
	{
	}

	private bool? AppendPrefixLengthValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private bool? IsIpv6Value { get; set; }
	private long? MinDocCountValue { get; set; }
	private int PrefixLengthValue { get; set; }

	/// <summary>
	/// <para>
	/// Defines whether the prefix length is appended to IP address keys in the response.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor<TDocument> AppendPrefixLength(bool? appendPrefixLength = true)
	{
		AppendPrefixLengthValue = appendPrefixLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The IP address field to aggregation on. The field mapping type must be <c>ip</c>.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The IP address field to aggregation on. The field mapping type must be <c>ip</c>.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The IP address field to aggregation on. The field mapping type must be <c>ip</c>.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines whether the prefix applies to IPv6 addresses.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor<TDocument> IsIpv6(bool? isIpv6 = true)
	{
		IsIpv6Value = isIpv6;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Minimum number of documents in a bucket for it to be included in the response.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor<TDocument> MinDocCount(long? minDocCount)
	{
		MinDocCountValue = minDocCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Length of the network prefix. For IPv4 addresses the accepted range is [0, 32].
	/// For IPv6 addresses the accepted range is [0, 128].
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor<TDocument> PrefixLength(int prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AppendPrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("append_prefix_length");
			writer.WriteBooleanValue(AppendPrefixLengthValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (IsIpv6Value.HasValue)
		{
			writer.WritePropertyName("is_ipv6");
			writer.WriteBooleanValue(IsIpv6Value.Value);
		}

		if (MinDocCountValue.HasValue)
		{
			writer.WritePropertyName("min_doc_count");
			writer.WriteNumberValue(MinDocCountValue.Value);
		}

		writer.WritePropertyName("prefix_length");
		writer.WriteNumberValue(PrefixLengthValue);
		writer.WriteEndObject();
	}
}

public sealed partial class IpPrefixAggregationDescriptor : SerializableDescriptor<IpPrefixAggregationDescriptor>
{
	internal IpPrefixAggregationDescriptor(Action<IpPrefixAggregationDescriptor> configure) => configure.Invoke(this);

	public IpPrefixAggregationDescriptor() : base()
	{
	}

	private bool? AppendPrefixLengthValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private bool? IsIpv6Value { get; set; }
	private long? MinDocCountValue { get; set; }
	private int PrefixLengthValue { get; set; }

	/// <summary>
	/// <para>
	/// Defines whether the prefix length is appended to IP address keys in the response.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor AppendPrefixLength(bool? appendPrefixLength = true)
	{
		AppendPrefixLengthValue = appendPrefixLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The IP address field to aggregation on. The field mapping type must be <c>ip</c>.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The IP address field to aggregation on. The field mapping type must be <c>ip</c>.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The IP address field to aggregation on. The field mapping type must be <c>ip</c>.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines whether the prefix applies to IPv6 addresses.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor IsIpv6(bool? isIpv6 = true)
	{
		IsIpv6Value = isIpv6;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Minimum number of documents in a bucket for it to be included in the response.
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor MinDocCount(long? minDocCount)
	{
		MinDocCountValue = minDocCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Length of the network prefix. For IPv4 addresses the accepted range is [0, 32].
	/// For IPv6 addresses the accepted range is [0, 128].
	/// </para>
	/// </summary>
	public IpPrefixAggregationDescriptor PrefixLength(int prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AppendPrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("append_prefix_length");
			writer.WriteBooleanValue(AppendPrefixLengthValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (IsIpv6Value.HasValue)
		{
			writer.WritePropertyName("is_ipv6");
			writer.WriteBooleanValue(IsIpv6Value.Value);
		}

		if (MinDocCountValue.HasValue)
		{
			writer.WritePropertyName("min_doc_count");
			writer.WriteNumberValue(MinDocCountValue.Value);
		}

		writer.WritePropertyName("prefix_length");
		writer.WriteNumberValue(PrefixLengthValue);
		writer.WriteEndObject();
	}
}