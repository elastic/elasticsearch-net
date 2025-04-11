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

internal sealed partial class CumulativeSumAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropBucketsPath = System.Text.Json.JsonEncodedText.Encode("buckets_path");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropGapPolicy = System.Text.Json.JsonEncodedText.Encode("gap_policy");

	public override Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<object?> propBucketsPath = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.GapPolicy?> propGapPolicy = default;
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

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BucketsPath = propBucketsPath.Value,
			Format = propFormat.Value,
			GapPolicy = propGapPolicy.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBucketsPath, value.BucketsPath, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropGapPolicy, value.GapPolicy, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationConverter))]
public sealed partial class CumulativeSumAggregation
{
#if NET7_0_OR_GREATER
	public CumulativeSumAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public CumulativeSumAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CumulativeSumAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
}

public readonly partial struct CumulativeSumAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CumulativeSumAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CumulativeSumAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation(Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Path to the buckets that contain one set of values to correlate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationDescriptor BucketsPath(object? value)
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
	public Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationDescriptor Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Policy to apply when gaps are found in the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationDescriptor GapPolicy(Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? value)
	{
		Instance.GapPolicy = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.CumulativeSumAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}