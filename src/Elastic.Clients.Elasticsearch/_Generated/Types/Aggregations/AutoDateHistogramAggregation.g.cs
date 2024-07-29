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

public sealed partial class AutoDateHistogramAggregation
{
	/// <summary>
	/// <para>The target number of buckets.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("buckets")]
	public int? Buckets { get; set; }

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	/// <summary>
	/// <para>The date format used to format `key_as_string` in the response.<br/>If no `format` is specified, the first date format specified in the field mapping is used.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("format")]
	public string? Format { get; set; }

	/// <summary>
	/// <para>The minimum rounding interval.<br/>This can make the collection process more efficient, as the aggregation will not attempt to round at any interval lower than `minimum_interval`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("minimum_interval")]
	public Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval? MinimumInterval { get; set; }

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("missing")]
	public DateTimeOffset? Missing { get; set; }

	/// <summary>
	/// <para>Time zone specified as a ISO 8601 UTC offset.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("offset")]
	public string? Offset { get; set; }
	[JsonInclude, JsonPropertyName("params")]
	public IDictionary<string, object>? Params { get; set; }
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

	/// <summary>
	/// <para>Time zone ID.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("time_zone")]
	public string? TimeZone { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Aggregation(AutoDateHistogramAggregation autoDateHistogramAggregation) => Elastic.Clients.Elasticsearch.Aggregations.Aggregation.AutoDateHistogram(autoDateHistogramAggregation);
}

public sealed partial class AutoDateHistogramAggregationDescriptor<TDocument> : SerializableDescriptor<AutoDateHistogramAggregationDescriptor<TDocument>>
{
	internal AutoDateHistogramAggregationDescriptor(Action<AutoDateHistogramAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public AutoDateHistogramAggregationDescriptor() : base()
	{
	}

	private int? BucketsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private string? FormatValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval? MinimumIntervalValue { get; set; }
	private DateTimeOffset? MissingValue { get; set; }
	private string? OffsetValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private string? TimeZoneValue { get; set; }

	/// <summary>
	/// <para>The target number of buckets.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> Buckets(int? buckets)
	{
		BucketsValue = buckets;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date format used to format `key_as_string` in the response.<br/>If no `format` is specified, the first date format specified in the field mapping is used.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	/// <summary>
	/// <para>The minimum rounding interval.<br/>This can make the collection process more efficient, as the aggregation will not attempt to round at any interval lower than `minimum_interval`.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> MinimumInterval(Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval? minimumInterval)
	{
		MinimumIntervalValue = minimumInterval;
		return Self;
	}

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> Missing(DateTimeOffset? missing)
	{
		MissingValue = missing;
		return Self;
	}

	/// <summary>
	/// <para>Time zone specified as a ISO 8601 UTC offset.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> Offset(string? offset)
	{
		OffsetValue = offset;
		return Self;
	}

	public AutoDateHistogramAggregationDescriptor<TDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public AutoDateHistogramAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public AutoDateHistogramAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public AutoDateHistogramAggregationDescriptor<TDocument> Script(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Time zone ID.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor<TDocument> TimeZone(string? timeZone)
	{
		TimeZoneValue = timeZone;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BucketsValue.HasValue)
		{
			writer.WritePropertyName("buckets");
			writer.WriteNumberValue(BucketsValue.Value);
		}

		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (!string.IsNullOrEmpty(FormatValue))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(FormatValue);
		}

		if (MinimumIntervalValue is not null)
		{
			writer.WritePropertyName("minimum_interval");
			JsonSerializer.Serialize(writer, MinimumIntervalValue, options);
		}

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
		}

		if (!string.IsNullOrEmpty(OffsetValue))
		{
			writer.WritePropertyName("offset");
			writer.WriteStringValue(OffsetValue);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		if (!string.IsNullOrEmpty(TimeZoneValue))
		{
			writer.WritePropertyName("time_zone");
			writer.WriteStringValue(TimeZoneValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class AutoDateHistogramAggregationDescriptor : SerializableDescriptor<AutoDateHistogramAggregationDescriptor>
{
	internal AutoDateHistogramAggregationDescriptor(Action<AutoDateHistogramAggregationDescriptor> configure) => configure.Invoke(this);

	public AutoDateHistogramAggregationDescriptor() : base()
	{
	}

	private int? BucketsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private string? FormatValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval? MinimumIntervalValue { get; set; }
	private DateTimeOffset? MissingValue { get; set; }
	private string? OffsetValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private string? TimeZoneValue { get; set; }

	/// <summary>
	/// <para>The target number of buckets.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor Buckets(int? buckets)
	{
		BucketsValue = buckets;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date format used to format `key_as_string` in the response.<br/>If no `format` is specified, the first date format specified in the field mapping is used.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	/// <summary>
	/// <para>The minimum rounding interval.<br/>This can make the collection process more efficient, as the aggregation will not attempt to round at any interval lower than `minimum_interval`.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor MinimumInterval(Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval? minimumInterval)
	{
		MinimumIntervalValue = minimumInterval;
		return Self;
	}

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor Missing(DateTimeOffset? missing)
	{
		MissingValue = missing;
		return Self;
	}

	/// <summary>
	/// <para>Time zone specified as a ISO 8601 UTC offset.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor Offset(string? offset)
	{
		OffsetValue = offset;
		return Self;
	}

	public AutoDateHistogramAggregationDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public AutoDateHistogramAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public AutoDateHistogramAggregationDescriptor Script(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public AutoDateHistogramAggregationDescriptor Script(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Time zone ID.</para>
	/// </summary>
	public AutoDateHistogramAggregationDescriptor TimeZone(string? timeZone)
	{
		TimeZoneValue = timeZone;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BucketsValue.HasValue)
		{
			writer.WritePropertyName("buckets");
			writer.WriteNumberValue(BucketsValue.Value);
		}

		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (!string.IsNullOrEmpty(FormatValue))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(FormatValue);
		}

		if (MinimumIntervalValue is not null)
		{
			writer.WritePropertyName("minimum_interval");
			JsonSerializer.Serialize(writer, MinimumIntervalValue, options);
		}

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
		}

		if (!string.IsNullOrEmpty(OffsetValue))
		{
			writer.WritePropertyName("offset");
			writer.WriteStringValue(OffsetValue);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		if (!string.IsNullOrEmpty(TimeZoneValue))
		{
			writer.WritePropertyName("time_zone");
			writer.WriteStringValue(TimeZoneValue);
		}

		writer.WriteEndObject();
	}
}