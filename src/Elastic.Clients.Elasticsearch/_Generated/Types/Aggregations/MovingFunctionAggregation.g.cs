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

internal sealed partial class MovingFunctionAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropBucketsPath = System.Text.Json.JsonEncodedText.Encode("buckets_path");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropGapPolicy = System.Text.Json.JsonEncodedText.Encode("gap_policy");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");
	private static readonly System.Text.Json.JsonEncodedText PropShift = System.Text.Json.JsonEncodedText.Encode("shift");
	private static readonly System.Text.Json.JsonEncodedText PropWindow = System.Text.Json.JsonEncodedText.Encode("window");

	public override Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<object?> propBucketsPath = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.GapPolicy?> propGapPolicy = default;
		LocalJsonValue<string?> propScript = default;
		LocalJsonValue<int?> propShift = default;
		LocalJsonValue<int?> propWindow = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBucketsPath.TryReadProperty(ref reader, options, PropBucketsPath, null))
			{
				continue;
			}

			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
			{
				continue;
			}

			if (propGapPolicy.TryReadProperty(ref reader, options, PropGapPolicy, null))
			{
				continue;
			}

			if (propScript.TryReadProperty(ref reader, options, PropScript, null))
			{
				continue;
			}

			if (propShift.TryReadProperty(ref reader, options, PropShift, null))
			{
				continue;
			}

			if (propWindow.TryReadProperty(ref reader, options, PropWindow, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BucketsPath = propBucketsPath.Value,
			Format = propFormat.Value,
			GapPolicy = propGapPolicy.Value,
			Script = propScript.Value,
			Shift = propShift.Value,
			Window = propWindow.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBucketsPath, value.BucketsPath, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropGapPolicy, value.GapPolicy, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteProperty(options, PropShift, value.Shift, null, null);
		writer.WriteProperty(options, PropWindow, value.Window, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationConverter))]
public sealed partial class MovingFunctionAggregation
{
#if NET7_0_OR_GREATER
	public MovingFunctionAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public MovingFunctionAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MovingFunctionAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Path to the buckets that contain one set of values to correlate.
	/// </para>
	/// </summary>
	public object? BucketsPath { get; set; }

	/// <summary>
	/// <para>
	/// <c>DecimalFormat</c> pattern for the output value.
	/// If specified, the formatted value is returned in the aggregation’s <c>value_as_string</c> property.
	/// </para>
	/// </summary>
	public string? Format { get; set; }

	/// <summary>
	/// <para>
	/// Policy to apply when gaps are found in the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicy { get; set; }

	/// <summary>
	/// <para>
	/// The script that should be executed on each window of data.
	/// </para>
	/// </summary>
	public string? Script { get; set; }

	/// <summary>
	/// <para>
	/// By default, the window consists of the last n values excluding the current bucket.
	/// Increasing <c>shift</c> by 1, moves the starting window position by 1 to the right.
	/// </para>
	/// </summary>
	public int? Shift { get; set; }

	/// <summary>
	/// <para>
	/// The size of window to "slide" across the histogram.
	/// </para>
	/// </summary>
	public int? Window { get; set; }
}

public readonly partial struct MovingFunctionAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MovingFunctionAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MovingFunctionAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation(Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Path to the buckets that contain one set of values to correlate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor BucketsPath(object? value)
	{
		Instance.BucketsPath = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// <c>DecimalFormat</c> pattern for the output value.
	/// If specified, the formatted value is returned in the aggregation’s <c>value_as_string</c> property.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Policy to apply when gaps are found in the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor GapPolicy(Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? value)
	{
		Instance.GapPolicy = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The script that should be executed on each window of data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor Script(string? value)
	{
		Instance.Script = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// By default, the window consists of the last n values excluding the current bucket.
	/// Increasing <c>shift</c> by 1, moves the starting window position by 1 to the right.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor Shift(int? value)
	{
		Instance.Shift = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The size of window to "slide" across the histogram.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor Window(int? value)
	{
		Instance.Window = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.MovingFunctionAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}