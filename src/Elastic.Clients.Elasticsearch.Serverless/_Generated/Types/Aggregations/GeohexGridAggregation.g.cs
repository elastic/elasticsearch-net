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

public sealed partial class GeohexGridAggregation
{
	/// <summary>
	/// <para>
	/// Bounding box used to filter the geo-points in each bucket.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("bounds")]
	public Elastic.Clients.Elasticsearch.Serverless.GeoBounds? Bounds { get; set; }

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geohex_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Integer zoom of the key used to defined cells or buckets
	/// in the results. Value should be between 0-15.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("precision")]
	public int? Precision { get; set; }

	/// <summary>
	/// <para>
	/// Number of buckets returned from each shard.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("shard_size")]
	public int? ShardSize { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of buckets to return.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation(GeohexGridAggregation geohexGridAggregation) => Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation.GeohexGrid(geohexGridAggregation);
}

public sealed partial class GeohexGridAggregationDescriptor<TDocument> : SerializableDescriptor<GeohexGridAggregationDescriptor<TDocument>>
{
	internal GeohexGridAggregationDescriptor(Action<GeohexGridAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GeohexGridAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.GeoBounds? BoundsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private int? PrecisionValue { get; set; }
	private int? ShardSizeValue { get; set; }
	private int? SizeValue { get; set; }

	/// <summary>
	/// <para>
	/// Bounding box used to filter the geo-points in each bucket.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor<TDocument> Bounds(Elastic.Clients.Elasticsearch.Serverless.GeoBounds? bounds)
	{
		BoundsValue = bounds;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geohex_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geohex_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geohex_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Integer zoom of the key used to defined cells or buckets
	/// in the results. Value should be between 0-15.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor<TDocument> Precision(int? precision)
	{
		PrecisionValue = precision;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Number of buckets returned from each shard.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor<TDocument> ShardSize(int? shardSize)
	{
		ShardSizeValue = shardSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maximum number of buckets to return.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoundsValue is not null)
		{
			writer.WritePropertyName("bounds");
			JsonSerializer.Serialize(writer, BoundsValue, options);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (PrecisionValue.HasValue)
		{
			writer.WritePropertyName("precision");
			writer.WriteNumberValue(PrecisionValue.Value);
		}

		if (ShardSizeValue.HasValue)
		{
			writer.WritePropertyName("shard_size");
			writer.WriteNumberValue(ShardSizeValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class GeohexGridAggregationDescriptor : SerializableDescriptor<GeohexGridAggregationDescriptor>
{
	internal GeohexGridAggregationDescriptor(Action<GeohexGridAggregationDescriptor> configure) => configure.Invoke(this);

	public GeohexGridAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.GeoBounds? BoundsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private int? PrecisionValue { get; set; }
	private int? ShardSizeValue { get; set; }
	private int? SizeValue { get; set; }

	/// <summary>
	/// <para>
	/// Bounding box used to filter the geo-points in each bucket.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor Bounds(Elastic.Clients.Elasticsearch.Serverless.GeoBounds? bounds)
	{
		BoundsValue = bounds;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geohex_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geohex_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geohex_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Integer zoom of the key used to defined cells or buckets
	/// in the results. Value should be between 0-15.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor Precision(int? precision)
	{
		PrecisionValue = precision;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Number of buckets returned from each shard.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor ShardSize(int? shardSize)
	{
		ShardSizeValue = shardSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maximum number of buckets to return.
	/// </para>
	/// </summary>
	public GeohexGridAggregationDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoundsValue is not null)
		{
			writer.WritePropertyName("bounds");
			JsonSerializer.Serialize(writer, BoundsValue, options);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (PrecisionValue.HasValue)
		{
			writer.WritePropertyName("precision");
			writer.WriteNumberValue(PrecisionValue.Value);
		}

		if (ShardSizeValue.HasValue)
		{
			writer.WritePropertyName("shard_size");
			writer.WriteNumberValue(ShardSizeValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}