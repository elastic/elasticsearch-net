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

internal sealed partial class GeoLineAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropIncludeSort = System.Text.Json.JsonEncodedText.Encode("include_sort");
	private static readonly System.Text.Json.JsonEncodedText PropPoint = System.Text.Json.JsonEncodedText.Encode("point");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");
	private static readonly System.Text.Json.JsonEncodedText PropSort = System.Text.Json.JsonEncodedText.Encode("sort");
	private static readonly System.Text.Json.JsonEncodedText PropSortOrder = System.Text.Json.JsonEncodedText.Encode("sort_order");

	public override Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propIncludeSort = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint> propPoint = default;
		LocalJsonValue<int?> propSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort> propSort = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SortOrder?> propSortOrder = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIncludeSort.TryReadProperty(ref reader, options, PropIncludeSort, null))
			{
				continue;
			}

			if (propPoint.TryReadProperty(ref reader, options, PropPoint, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
			{
				continue;
			}

			if (propSort.TryReadProperty(ref reader, options, PropSort, null))
			{
				continue;
			}

			if (propSortOrder.TryReadProperty(ref reader, options, PropSortOrder, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IncludeSort = propIncludeSort.Value,
			Point = propPoint.Value,
			Size = propSize.Value,
			Sort = propSort.Value,
			SortOrder = propSortOrder.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIncludeSort, value.IncludeSort, null, null);
		writer.WriteProperty(options, PropPoint, value.Point, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteProperty(options, PropSort, value.Sort, null, null);
		writer.WriteProperty(options, PropSortOrder, value.SortOrder, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationConverter))]
public sealed partial class GeoLineAggregation
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLineAggregation(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint point, Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort sort)
	{
		Point = point;
		Sort = sort;
	}
#if NET7_0_OR_GREATER
	public GeoLineAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GeoLineAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeoLineAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// When <c>true</c>, returns an additional array of the sort values in the feature properties.
	/// </para>
	/// </summary>
	public bool? IncludeSort { get; set; }

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint Point { get; set; }

	/// <summary>
	/// <para>
	/// The maximum length of the line represented in the aggregation.
	/// Valid sizes are between 1 and 10000.
	/// </para>
	/// </summary>
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// The name of the numeric field to use as the sort key for ordering the points.
	/// When the <c>geo_line</c> aggregation is nested inside a <c>time_series</c> aggregation, this field defaults to <c>@timestamp</c>, and any other value will result in error.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort Sort { get; set; }

	/// <summary>
	/// <para>
	/// The order in which the line is sorted (ascending or descending).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SortOrder? SortOrder { get; set; }
}

public readonly partial struct GeoLineAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLineAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLineAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation(Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// When <c>true</c>, returns an additional array of the sort values in the feature properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument> IncludeSort(bool? value = true)
	{
		Instance.IncludeSort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument> Point(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint value)
	{
		Instance.Point = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument> Point(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument>> action)
	{
		Instance.Point = Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum length of the line represented in the aggregation.
	/// Valid sizes are between 1 and 10000.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the numeric field to use as the sort key for ordering the points.
	/// When the <c>geo_line</c> aggregation is nested inside a <c>time_series</c> aggregation, this field defaults to <c>@timestamp</c>, and any other value will result in error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort value)
	{
		Instance.Sort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the numeric field to use as the sort key for ordering the points.
	/// When the <c>geo_line</c> aggregation is nested inside a <c>time_series</c> aggregation, this field defaults to <c>@timestamp</c>, and any other value will result in error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument> Sort(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLineSortDescriptor<TDocument>> action)
	{
		Instance.Sort = Elastic.Clients.Elasticsearch.Aggregations.GeoLineSortDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The order in which the line is sorted (ascending or descending).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument> SortOrder(Elastic.Clients.Elasticsearch.SortOrder? value)
	{
		Instance.SortOrder = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GeoLineAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLineAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLineAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation(Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// When <c>true</c>, returns an additional array of the sort values in the feature properties.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor IncludeSort(bool? value = true)
	{
		Instance.IncludeSort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor Point(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint value)
	{
		Instance.Point = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor Point(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor> action)
	{
		Instance.Point = Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor Point<T>(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<T>> action)
	{
		Instance.Point = Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum length of the line represented in the aggregation.
	/// Valid sizes are between 1 and 10000.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the numeric field to use as the sort key for ordering the points.
	/// When the <c>geo_line</c> aggregation is nested inside a <c>time_series</c> aggregation, this field defaults to <c>@timestamp</c>, and any other value will result in error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor Sort(Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort value)
	{
		Instance.Sort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the numeric field to use as the sort key for ordering the points.
	/// When the <c>geo_line</c> aggregation is nested inside a <c>time_series</c> aggregation, this field defaults to <c>@timestamp</c>, and any other value will result in error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor Sort(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLineSortDescriptor> action)
	{
		Instance.Sort = Elastic.Clients.Elasticsearch.Aggregations.GeoLineSortDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the numeric field to use as the sort key for ordering the points.
	/// When the <c>geo_line</c> aggregation is nested inside a <c>time_series</c> aggregation, this field defaults to <c>@timestamp</c>, and any other value will result in error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor Sort<T>(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLineSortDescriptor<T>> action)
	{
		Instance.Sort = Elastic.Clients.Elasticsearch.Aggregations.GeoLineSortDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The order in which the line is sorted (ascending or descending).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor SortOrder(Elastic.Clients.Elasticsearch.SortOrder? value)
	{
		Instance.SortOrder = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}