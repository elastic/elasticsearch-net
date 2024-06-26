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

public sealed partial class DateDistanceFeatureQuery
{
	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }

	/// <summary>
	/// <para>Name of the field used to calculate distances. This field must meet the following criteria:<br/>be a `date`, `date_nanos` or `geo_point` field;<br/>have an `index` mapping parameter value of `true`, which is the default;<br/>have an `doc_values` mapping parameter value of `true`, which is the default.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	/// <summary>
	/// <para>Date or point of origin used to calculate distances.<br/>If the `field` value is a `date` or `date_nanos` field, the `origin` value must be a date.<br/>Date Math, such as `now-1h`, is supported.<br/>If the field value is a `geo_point` field, the `origin` value must be a geopoint.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("origin")]
	public Elastic.Clients.Elasticsearch.Serverless.DateMath Origin { get; set; }

	/// <summary>
	/// <para>Distance from the `origin` at which relevance scores receive half of the `boost` value.<br/>If the `field` value is a `date` or `date_nanos` field, the `pivot` value must be a time unit, such as `1h` or `10d`. If the `field` value is a `geo_point` field, the `pivot` value must be a distance unit, such as `1km` or `12m`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pivot")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration Pivot { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }
}

public sealed partial class DateDistanceFeatureQueryDescriptor<TDocument> : SerializableDescriptor<DateDistanceFeatureQueryDescriptor<TDocument>>
{
	internal DateDistanceFeatureQueryDescriptor(Action<DateDistanceFeatureQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DateDistanceFeatureQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.DateMath OriginValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Duration PivotValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>Name of the field used to calculate distances. This field must meet the following criteria:<br/>be a `date`, `date_nanos` or `geo_point` field;<br/>have an `index` mapping parameter value of `true`, which is the default;<br/>have an `doc_values` mapping parameter value of `true`, which is the default.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Name of the field used to calculate distances. This field must meet the following criteria:<br/>be a `date`, `date_nanos` or `geo_point` field;<br/>have an `index` mapping parameter value of `true`, which is the default;<br/>have an `doc_values` mapping parameter value of `true`, which is the default.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Name of the field used to calculate distances. This field must meet the following criteria:<br/>be a `date`, `date_nanos` or `geo_point` field;<br/>have an `index` mapping parameter value of `true`, which is the default;<br/>have an `doc_values` mapping parameter value of `true`, which is the default.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Date or point of origin used to calculate distances.<br/>If the `field` value is a `date` or `date_nanos` field, the `origin` value must be a date.<br/>Date Math, such as `now-1h`, is supported.<br/>If the field value is a `geo_point` field, the `origin` value must be a geopoint.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor<TDocument> Origin(Elastic.Clients.Elasticsearch.Serverless.DateMath origin)
	{
		OriginValue = origin;
		return Self;
	}

	/// <summary>
	/// <para>Distance from the `origin` at which relevance scores receive half of the `boost` value.<br/>If the `field` value is a `date` or `date_nanos` field, the `pivot` value must be a time unit, such as `1h` or `10d`. If the `field` value is a `geo_point` field, the `pivot` value must be a distance unit, such as `1km` or `12m`.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor<TDocument> Pivot(Elastic.Clients.Elasticsearch.Serverless.Duration pivot)
	{
		PivotValue = pivot;
		return Self;
	}

	public DateDistanceFeatureQueryDescriptor<TDocument> QueryName(string? queryName)
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
		JsonSerializer.Serialize(writer, PivotValue, options);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class DateDistanceFeatureQueryDescriptor : SerializableDescriptor<DateDistanceFeatureQueryDescriptor>
{
	internal DateDistanceFeatureQueryDescriptor(Action<DateDistanceFeatureQueryDescriptor> configure) => configure.Invoke(this);

	public DateDistanceFeatureQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.DateMath OriginValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Duration PivotValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>Name of the field used to calculate distances. This field must meet the following criteria:<br/>be a `date`, `date_nanos` or `geo_point` field;<br/>have an `index` mapping parameter value of `true`, which is the default;<br/>have an `doc_values` mapping parameter value of `true`, which is the default.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Name of the field used to calculate distances. This field must meet the following criteria:<br/>be a `date`, `date_nanos` or `geo_point` field;<br/>have an `index` mapping parameter value of `true`, which is the default;<br/>have an `doc_values` mapping parameter value of `true`, which is the default.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Name of the field used to calculate distances. This field must meet the following criteria:<br/>be a `date`, `date_nanos` or `geo_point` field;<br/>have an `index` mapping parameter value of `true`, which is the default;<br/>have an `doc_values` mapping parameter value of `true`, which is the default.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Date or point of origin used to calculate distances.<br/>If the `field` value is a `date` or `date_nanos` field, the `origin` value must be a date.<br/>Date Math, such as `now-1h`, is supported.<br/>If the field value is a `geo_point` field, the `origin` value must be a geopoint.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor Origin(Elastic.Clients.Elasticsearch.Serverless.DateMath origin)
	{
		OriginValue = origin;
		return Self;
	}

	/// <summary>
	/// <para>Distance from the `origin` at which relevance scores receive half of the `boost` value.<br/>If the `field` value is a `date` or `date_nanos` field, the `pivot` value must be a time unit, such as `1h` or `10d`. If the `field` value is a `geo_point` field, the `pivot` value must be a distance unit, such as `1km` or `12m`.</para>
	/// </summary>
	public DateDistanceFeatureQueryDescriptor Pivot(Elastic.Clients.Elasticsearch.Serverless.Duration pivot)
	{
		PivotValue = pivot;
		return Self;
	}

	public DateDistanceFeatureQueryDescriptor QueryName(string? queryName)
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
		JsonSerializer.Serialize(writer, PivotValue, options);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}