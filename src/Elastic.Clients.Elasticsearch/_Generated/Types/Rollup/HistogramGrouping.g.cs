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

namespace Elastic.Clients.Elasticsearch.Rollup;

public sealed partial class HistogramGrouping
{
	/// <summary>
	/// <para>
	/// The set of fields that you wish to build histograms for.
	/// All fields specified must be some kind of numeric.
	/// Order does not matter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("fields")]
	[JsonConverter(typeof(SingleOrManyFieldsConverter))]
	public Elastic.Clients.Elasticsearch.Fields Fields { get; set; }

	/// <summary>
	/// <para>
	/// The interval of histogram buckets to be generated when rolling up.
	/// For example, a value of <c>5</c> creates buckets that are five units wide (<c>0-5</c>, <c>5-10</c>, etc).
	/// Note that only one interval can be specified in the histogram group, meaning that all fields being grouped via the histogram must share the same interval.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("interval")]
	public long Interval { get; set; }
}

public sealed partial class HistogramGroupingDescriptor<TDocument> : SerializableDescriptor<HistogramGroupingDescriptor<TDocument>>
{
	internal HistogramGroupingDescriptor(Action<HistogramGroupingDescriptor<TDocument>> configure) => configure.Invoke(this);

	public HistogramGroupingDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields FieldsValue { get; set; }
	private long IntervalValue { get; set; }

	/// <summary>
	/// <para>
	/// The set of fields that you wish to build histograms for.
	/// All fields specified must be some kind of numeric.
	/// Order does not matter.
	/// </para>
	/// </summary>
	public HistogramGroupingDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields fields)
	{
		FieldsValue = fields;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The interval of histogram buckets to be generated when rolling up.
	/// For example, a value of <c>5</c> creates buckets that are five units wide (<c>0-5</c>, <c>5-10</c>, etc).
	/// Note that only one interval can be specified in the histogram group, meaning that all fields being grouped via the histogram must share the same interval.
	/// </para>
	/// </summary>
	public HistogramGroupingDescriptor<TDocument> Interval(long interval)
	{
		IntervalValue = interval;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("fields");
		JsonSerializer.Serialize(writer, FieldsValue, options);
		writer.WritePropertyName("interval");
		writer.WriteNumberValue(IntervalValue);
		writer.WriteEndObject();
	}
}

public sealed partial class HistogramGroupingDescriptor : SerializableDescriptor<HistogramGroupingDescriptor>
{
	internal HistogramGroupingDescriptor(Action<HistogramGroupingDescriptor> configure) => configure.Invoke(this);

	public HistogramGroupingDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields FieldsValue { get; set; }
	private long IntervalValue { get; set; }

	/// <summary>
	/// <para>
	/// The set of fields that you wish to build histograms for.
	/// All fields specified must be some kind of numeric.
	/// Order does not matter.
	/// </para>
	/// </summary>
	public HistogramGroupingDescriptor Fields(Elastic.Clients.Elasticsearch.Fields fields)
	{
		FieldsValue = fields;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The interval of histogram buckets to be generated when rolling up.
	/// For example, a value of <c>5</c> creates buckets that are five units wide (<c>0-5</c>, <c>5-10</c>, etc).
	/// Note that only one interval can be specified in the histogram group, meaning that all fields being grouped via the histogram must share the same interval.
	/// </para>
	/// </summary>
	public HistogramGroupingDescriptor Interval(long interval)
	{
		IntervalValue = interval;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("fields");
		JsonSerializer.Serialize(writer, FieldsValue, options);
		writer.WritePropertyName("interval");
		writer.WriteNumberValue(IntervalValue);
		writer.WriteEndObject();
	}
}