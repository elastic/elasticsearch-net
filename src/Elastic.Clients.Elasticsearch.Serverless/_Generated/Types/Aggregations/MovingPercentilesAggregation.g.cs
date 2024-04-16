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

public sealed partial class MovingPercentilesAggregation
{
	/// <summary>
	/// <para>Path to the buckets that contain one set of values to correlate.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("buckets_path")]
	public Elastic.Clients.Elasticsearch.Serverless.Aggregations.BucketsPath? BucketsPath { get; set; }

	/// <summary>
	/// <para>`DecimalFormat` pattern for the output value.<br/>If specified, the formatted value is returned in the aggregation’s `value_as_string` property.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("format")]
	public string? Format { get; set; }

	/// <summary>
	/// <para>Policy to apply when gaps are found in the data.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("gap_policy")]
	public Elastic.Clients.Elasticsearch.Serverless.Aggregations.GapPolicy? GapPolicy { get; set; }

	/// <summary>
	/// <para>By default, the window consists of the last n values excluding the current bucket.<br/>Increasing `shift` by 1, moves the starting window position by 1 to the right.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("shift")]
	public int? Shift { get; set; }

	/// <summary>
	/// <para>The size of window to "slide" across the histogram.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("window")]
	public int? Window { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation(MovingPercentilesAggregation movingPercentilesAggregation) => Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation.MovingPercentiles(movingPercentilesAggregation);
}

public sealed partial class MovingPercentilesAggregationDescriptor : SerializableDescriptor<MovingPercentilesAggregationDescriptor>
{
	internal MovingPercentilesAggregationDescriptor(Action<MovingPercentilesAggregationDescriptor> configure) => configure.Invoke(this);

	public MovingPercentilesAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Aggregations.BucketsPath? BucketsPathValue { get; set; }
	private string? FormatValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Aggregations.GapPolicy? GapPolicyValue { get; set; }
	private int? ShiftValue { get; set; }
	private int? WindowValue { get; set; }

	/// <summary>
	/// <para>Path to the buckets that contain one set of values to correlate.</para>
	/// </summary>
	public MovingPercentilesAggregationDescriptor BucketsPath(Elastic.Clients.Elasticsearch.Serverless.Aggregations.BucketsPath? bucketsPath)
	{
		BucketsPathValue = bucketsPath;
		return Self;
	}

	/// <summary>
	/// <para>`DecimalFormat` pattern for the output value.<br/>If specified, the formatted value is returned in the aggregation’s `value_as_string` property.</para>
	/// </summary>
	public MovingPercentilesAggregationDescriptor Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	/// <summary>
	/// <para>Policy to apply when gaps are found in the data.</para>
	/// </summary>
	public MovingPercentilesAggregationDescriptor GapPolicy(Elastic.Clients.Elasticsearch.Serverless.Aggregations.GapPolicy? gapPolicy)
	{
		GapPolicyValue = gapPolicy;
		return Self;
	}

	/// <summary>
	/// <para>By default, the window consists of the last n values excluding the current bucket.<br/>Increasing `shift` by 1, moves the starting window position by 1 to the right.</para>
	/// </summary>
	public MovingPercentilesAggregationDescriptor Shift(int? shift)
	{
		ShiftValue = shift;
		return Self;
	}

	/// <summary>
	/// <para>The size of window to "slide" across the histogram.</para>
	/// </summary>
	public MovingPercentilesAggregationDescriptor Window(int? window)
	{
		WindowValue = window;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BucketsPathValue is not null)
		{
			writer.WritePropertyName("buckets_path");
			JsonSerializer.Serialize(writer, BucketsPathValue, options);
		}

		if (!string.IsNullOrEmpty(FormatValue))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(FormatValue);
		}

		if (GapPolicyValue is not null)
		{
			writer.WritePropertyName("gap_policy");
			JsonSerializer.Serialize(writer, GapPolicyValue, options);
		}

		if (ShiftValue.HasValue)
		{
			writer.WritePropertyName("shift");
			writer.WriteNumberValue(ShiftValue.Value);
		}

		if (WindowValue.HasValue)
		{
			writer.WritePropertyName("window");
			writer.WriteNumberValue(WindowValue.Value);
		}

		writer.WriteEndObject();
	}
}