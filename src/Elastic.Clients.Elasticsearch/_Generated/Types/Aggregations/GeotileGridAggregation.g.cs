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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class GeotileGridAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropBounds = System.Text.Json.JsonEncodedText.Encode("bounds");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropPrecision = System.Text.Json.JsonEncodedText.Encode("precision");
	private static readonly System.Text.Json.JsonEncodedText PropShardSize = System.Text.Json.JsonEncodedText.Encode("shard_size");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");

	public override Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeoBounds?> propBounds = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propField = default;
		LocalJsonValue<long?> propPrecision = default;
		LocalJsonValue<int?> propShardSize = default;
		LocalJsonValue<int?> propSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBounds.TryReadProperty(ref reader, options, PropBounds, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propPrecision.TryReadProperty(ref reader, options, PropPrecision, null))
			{
				continue;
			}

			if (propShardSize.TryReadProperty(ref reader, options, PropShardSize, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Bounds = propBounds.Value,
			Field = propField.Value,
			Precision = propPrecision.Value,
			ShardSize = propShardSize.Value,
			Size = propSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBounds, value.Bounds, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropPrecision, value.Precision, null, null);
		writer.WriteProperty(options, PropShardSize, value.ShardSize, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationConverter))]
public sealed partial class GeotileGridAggregation
{
#if NET7_0_OR_GREATER
	public GeotileGridAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GeotileGridAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeotileGridAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A bounding box to filter the geo-points or geo-shapes in each bucket.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.GeoBounds? Bounds { get; set; }

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geotile_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	/// <summary>
	/// <para>
	/// Integer zoom of the key used to define cells/buckets in the results.
	/// Values outside of the range [0,29] will be rejected.
	/// </para>
	/// </summary>
	public long? Precision { get; set; }

	/// <summary>
	/// <para>
	/// Allows for more accurate counting of the top cells returned in the final result the aggregation.
	/// Defaults to returning <c>max(10,(size x number-of-shards))</c> buckets from each shard.
	/// </para>
	/// </summary>
	public int? ShardSize { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of buckets to return.
	/// </para>
	/// </summary>
	public int? Size { get; set; }
}

public readonly partial struct GeotileGridAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeotileGridAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeotileGridAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A bounding box to filter the geo-points or geo-shapes in each bucket.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument> Bounds(Elastic.Clients.Elasticsearch.GeoBounds? value)
	{
		Instance.Bounds = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A bounding box to filter the geo-points or geo-shapes in each bucket.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument> Bounds(System.Func<Elastic.Clients.Elasticsearch.GeoBoundsFactory, Elastic.Clients.Elasticsearch.GeoBounds> action)
	{
		Instance.Bounds = Elastic.Clients.Elasticsearch.GeoBoundsFactory.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geotile_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geotile_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Integer zoom of the key used to define cells/buckets in the results.
	/// Values outside of the range [0,29] will be rejected.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument> Precision(long? value)
	{
		Instance.Precision = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Allows for more accurate counting of the top cells returned in the final result the aggregation.
	/// Defaults to returning <c>max(10,(size x number-of-shards))</c> buckets from each shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument> ShardSize(int? value)
	{
		Instance.ShardSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of buckets to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GeotileGridAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeotileGridAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeotileGridAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A bounding box to filter the geo-points or geo-shapes in each bucket.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor Bounds(Elastic.Clients.Elasticsearch.GeoBounds? value)
	{
		Instance.Bounds = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A bounding box to filter the geo-points or geo-shapes in each bucket.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor Bounds(System.Func<Elastic.Clients.Elasticsearch.GeoBoundsFactory, Elastic.Clients.Elasticsearch.GeoBounds> action)
	{
		Instance.Bounds = Elastic.Clients.Elasticsearch.GeoBoundsFactory.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geotile_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field containing indexed <c>geo_point</c> or <c>geo_shape</c> values.
	/// If the field contains an array, <c>geotile_grid</c> aggregates all array values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Integer zoom of the key used to define cells/buckets in the results.
	/// Values outside of the range [0,29] will be rejected.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor Precision(long? value)
	{
		Instance.Precision = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Allows for more accurate counting of the top cells returned in the final result the aggregation.
	/// Defaults to returning <c>max(10,(size x number-of-shards))</c> buckets from each shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor ShardSize(int? value)
	{
		Instance.ShardSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of buckets to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}