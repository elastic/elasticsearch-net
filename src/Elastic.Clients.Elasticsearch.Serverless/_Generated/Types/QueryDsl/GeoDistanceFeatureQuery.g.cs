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

namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;

public sealed partial class GeoDistanceFeatureQuery
{
	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Date or point of origin used to calculate distances.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>origin</c> value must be a date.
	/// Date Math, such as <c>now-1h</c>, is supported.
	/// If the field value is a <c>geo_point</c> field, the <c>origin</c> value must be a geopoint.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("origin")]
	public Elastic.Clients.Elasticsearch.Serverless.GeoLocation Origin { get; set; }

	/// <summary>
	/// <para>
	/// Distance from the <c>origin</c> at which relevance scores receive half of the <c>boost</c> value.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>pivot</c> value must be a time unit, such as <c>1h</c> or <c>10d</c>. If the <c>field</c> value is a <c>geo_point</c> field, the <c>pivot</c> value must be a distance unit, such as <c>1km</c> or <c>12m</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pivot")]
	public string Pivot { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }
}

public sealed partial class GeoDistanceFeatureQueryDescriptor<TDocument> : SerializableDescriptor<GeoDistanceFeatureQueryDescriptor<TDocument>>
{
	internal GeoDistanceFeatureQueryDescriptor(Action<GeoDistanceFeatureQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GeoDistanceFeatureQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.GeoLocation OriginValue { get; set; }
	private string PivotValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Date or point of origin used to calculate distances.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>origin</c> value must be a date.
	/// Date Math, such as <c>now-1h</c>, is supported.
	/// If the field value is a <c>geo_point</c> field, the <c>origin</c> value must be a geopoint.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor<TDocument> Origin(Elastic.Clients.Elasticsearch.Serverless.GeoLocation origin)
	{
		OriginValue = origin;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Distance from the <c>origin</c> at which relevance scores receive half of the <c>boost</c> value.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>pivot</c> value must be a time unit, such as <c>1h</c> or <c>10d</c>. If the <c>field</c> value is a <c>geo_point</c> field, the <c>pivot</c> value must be a distance unit, such as <c>1km</c> or <c>12m</c>.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor<TDocument> Pivot(string pivot)
	{
		PivotValue = pivot;
		return Self;
	}

	public GeoDistanceFeatureQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WritePropertyName("origin");
		JsonSerializer.Serialize(writer, OriginValue, options);
		writer.WritePropertyName("pivot");
		writer.WriteStringValue(PivotValue);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class GeoDistanceFeatureQueryDescriptor : SerializableDescriptor<GeoDistanceFeatureQueryDescriptor>
{
	internal GeoDistanceFeatureQueryDescriptor(Action<GeoDistanceFeatureQueryDescriptor> configure) => configure.Invoke(this);

	public GeoDistanceFeatureQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.GeoLocation OriginValue { get; set; }
	private string PivotValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Date or point of origin used to calculate distances.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>origin</c> value must be a date.
	/// Date Math, such as <c>now-1h</c>, is supported.
	/// If the field value is a <c>geo_point</c> field, the <c>origin</c> value must be a geopoint.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor Origin(Elastic.Clients.Elasticsearch.Serverless.GeoLocation origin)
	{
		OriginValue = origin;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Distance from the <c>origin</c> at which relevance scores receive half of the <c>boost</c> value.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>pivot</c> value must be a time unit, such as <c>1h</c> or <c>10d</c>. If the <c>field</c> value is a <c>geo_point</c> field, the <c>pivot</c> value must be a distance unit, such as <c>1km</c> or <c>12m</c>.
	/// </para>
	/// </summary>
	public GeoDistanceFeatureQueryDescriptor Pivot(string pivot)
	{
		PivotValue = pivot;
		return Self;
	}

	public GeoDistanceFeatureQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WritePropertyName("origin");
		JsonSerializer.Serialize(writer, OriginValue, options);
		writer.WritePropertyName("pivot");
		writer.WriteStringValue(PivotValue);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}