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

namespace Elastic.Clients.Elasticsearch.Core.FieldCaps;

internal sealed partial class FieldCapabilityConverter : System.Text.Json.Serialization.JsonConverter<FieldCapability>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregatable = System.Text.Json.JsonEncodedText.Encode("aggregatable");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropMetadataField = System.Text.Json.JsonEncodedText.Encode("metadata_field");
	private static readonly System.Text.Json.JsonEncodedText PropMetricConflictsIndices = System.Text.Json.JsonEncodedText.Encode("metric_conflicts_indices");
	private static readonly System.Text.Json.JsonEncodedText PropNonAggregatableIndices = System.Text.Json.JsonEncodedText.Encode("non_aggregatable_indices");
	private static readonly System.Text.Json.JsonEncodedText PropNonDimensionIndices = System.Text.Json.JsonEncodedText.Encode("non_dimension_indices");
	private static readonly System.Text.Json.JsonEncodedText PropNonSearchableIndices = System.Text.Json.JsonEncodedText.Encode("non_searchable_indices");
	private static readonly System.Text.Json.JsonEncodedText PropSearchable = System.Text.Json.JsonEncodedText.Encode("searchable");
	private static readonly System.Text.Json.JsonEncodedText PropTimeSeriesDimension = System.Text.Json.JsonEncodedText.Encode("time_series_dimension");
	private static readonly System.Text.Json.JsonEncodedText PropTimeSeriesMetric = System.Text.Json.JsonEncodedText.Encode("time_series_metric");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override FieldCapability Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAggregatable = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propIndices = default;
		LocalJsonValue<IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<bool?> propMetadataField = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propMetricConflictsIndices = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propNonAggregatableIndices = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propNonDimensionIndices = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propNonSearchableIndices = default;
		LocalJsonValue<bool> propSearchable = default;
		LocalJsonValue<bool?> propTimeSeriesDimension = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType?> propTimeSeriesMetric = default;
		LocalJsonValue<string> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregatable.TryRead(ref reader, options, PropAggregatable))
			{
				continue;
			}

			if (propIndices.TryRead(ref reader, options, PropIndices, typeof(SingleOrManyMarker<IReadOnlyCollection<string>?, string>)))
			{
				continue;
			}

			if (propMeta.TryRead(ref reader, options, PropMeta))
			{
				continue;
			}

			if (propMetadataField.TryRead(ref reader, options, PropMetadataField))
			{
				continue;
			}

			if (propMetricConflictsIndices.TryRead(ref reader, options, PropMetricConflictsIndices))
			{
				continue;
			}

			if (propNonAggregatableIndices.TryRead(ref reader, options, PropNonAggregatableIndices, typeof(SingleOrManyMarker<IReadOnlyCollection<string>?, string>)))
			{
				continue;
			}

			if (propNonDimensionIndices.TryRead(ref reader, options, PropNonDimensionIndices))
			{
				continue;
			}

			if (propNonSearchableIndices.TryRead(ref reader, options, PropNonSearchableIndices, typeof(SingleOrManyMarker<IReadOnlyCollection<string>?, string>)))
			{
				continue;
			}

			if (propSearchable.TryRead(ref reader, options, PropSearchable))
			{
				continue;
			}

			if (propTimeSeriesDimension.TryRead(ref reader, options, PropTimeSeriesDimension))
			{
				continue;
			}

			if (propTimeSeriesMetric.TryRead(ref reader, options, PropTimeSeriesMetric))
			{
				continue;
			}

			if (propType.TryRead(ref reader, options, PropType))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new FieldCapability
		{
			Aggregatable = propAggregatable.Value
,
			Indices = propIndices.Value
,
			Meta = propMeta.Value
,
			MetadataField = propMetadataField.Value
,
			MetricConflictsIndices = propMetricConflictsIndices.Value
,
			NonAggregatableIndices = propNonAggregatableIndices.Value
,
			NonDimensionIndices = propNonDimensionIndices.Value
,
			NonSearchableIndices = propNonSearchableIndices.Value
,
			Searchable = propSearchable.Value
,
			TimeSeriesDimension = propTimeSeriesDimension.Value
,
			TimeSeriesMetric = propTimeSeriesMetric.Value
,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, FieldCapability value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregatable, value.Aggregatable);
		writer.WriteProperty(options, PropIndices, value.Indices, null, typeof(SingleOrManyMarker<IReadOnlyCollection<string>?, string>));
		writer.WriteProperty(options, PropMeta, value.Meta);
		writer.WriteProperty(options, PropMetadataField, value.MetadataField);
		writer.WriteProperty(options, PropMetricConflictsIndices, value.MetricConflictsIndices);
		writer.WriteProperty(options, PropNonAggregatableIndices, value.NonAggregatableIndices, null, typeof(SingleOrManyMarker<IReadOnlyCollection<string>?, string>));
		writer.WriteProperty(options, PropNonDimensionIndices, value.NonDimensionIndices);
		writer.WriteProperty(options, PropNonSearchableIndices, value.NonSearchableIndices, null, typeof(SingleOrManyMarker<IReadOnlyCollection<string>?, string>));
		writer.WriteProperty(options, PropSearchable, value.Searchable);
		writer.WriteProperty(options, PropTimeSeriesDimension, value.TimeSeriesDimension);
		writer.WriteProperty(options, PropTimeSeriesMetric, value.TimeSeriesMetric);
		writer.WriteProperty(options, PropType, value.Type);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(FieldCapabilityConverter))]
public sealed partial class FieldCapability
{
	/// <summary>
	/// <para>
	/// Whether this field can be aggregated on all indices.
	/// </para>
	/// </summary>
	public bool Aggregatable { get; init; }

	/// <summary>
	/// <para>
	/// The list of indices where this field has the same type family, or null if all indices have the same type family for the field.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? Indices { get; init; }

	/// <summary>
	/// <para>
	/// Merged metadata across all indices as a map of string keys to arrays of values. A value length of 1 indicates that all indices had the same value for this key, while a length of 2 or more indicates that not all indices had the same value for this key.
	/// </para>
	/// </summary>
	public IReadOnlyDictionary<string, object>? Meta { get; init; }

	/// <summary>
	/// <para>
	/// Whether this field is registered as a metadata field.
	/// </para>
	/// </summary>
	public bool? MetadataField { get; init; }

	/// <summary>
	/// <para>
	/// The list of indices where this field is present if these indices
	/// don’t have the same <c>time_series_metric</c> value for this field.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? MetricConflictsIndices { get; init; }

	/// <summary>
	/// <para>
	/// The list of indices where this field is not aggregatable, or null if all indices have the same definition for the field.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? NonAggregatableIndices { get; init; }

	/// <summary>
	/// <para>
	/// If this list is present in response then some indices have the
	/// field marked as a dimension and other indices, the ones in this list, do not.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? NonDimensionIndices { get; init; }

	/// <summary>
	/// <para>
	/// The list of indices where this field is not searchable, or null if all indices have the same definition for the field.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? NonSearchableIndices { get; init; }

	/// <summary>
	/// <para>
	/// Whether this field is indexed for search on all indices.
	/// </para>
	/// </summary>
	public bool Searchable { get; init; }

	/// <summary>
	/// <para>
	/// Whether this field is used as a time series dimension.
	/// </para>
	/// </summary>
	public bool? TimeSeriesDimension { get; init; }

	/// <summary>
	/// <para>
	/// Contains metric type if this fields is used as a time series
	/// metrics, absent if the field is not used as metric.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Mapping.TimeSeriesMetricType? TimeSeriesMetric { get; init; }
	public string Type { get; init; }
}