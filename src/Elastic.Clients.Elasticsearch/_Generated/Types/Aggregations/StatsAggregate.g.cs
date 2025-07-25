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

internal sealed partial class StatsAggregateConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.StatsAggregate>
{
	private static readonly System.Text.Json.JsonEncodedText PropAvg = System.Text.Json.JsonEncodedText.Encode("avg");
	private static readonly System.Text.Json.JsonEncodedText PropAvgAsString = System.Text.Json.JsonEncodedText.Encode("avg_as_string");
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropMax = System.Text.Json.JsonEncodedText.Encode("max");
	private static readonly System.Text.Json.JsonEncodedText PropMaxAsString = System.Text.Json.JsonEncodedText.Encode("max_as_string");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropMin = System.Text.Json.JsonEncodedText.Encode("min");
	private static readonly System.Text.Json.JsonEncodedText PropMinAsString = System.Text.Json.JsonEncodedText.Encode("min_as_string");
	private static readonly System.Text.Json.JsonEncodedText PropSum = System.Text.Json.JsonEncodedText.Encode("sum");
	private static readonly System.Text.Json.JsonEncodedText PropSumAsString = System.Text.Json.JsonEncodedText.Encode("sum_as_string");

	public override Elastic.Clients.Elasticsearch.Aggregations.StatsAggregate Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propAvg = default;
		LocalJsonValue<string?> propAvgAsString = default;
		LocalJsonValue<long> propCount = default;
		LocalJsonValue<double?> propMax = default;
		LocalJsonValue<string?> propMaxAsString = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<double?> propMin = default;
		LocalJsonValue<string?> propMinAsString = default;
		LocalJsonValue<double> propSum = default;
		LocalJsonValue<string?> propSumAsString = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAvg.TryReadProperty(ref reader, options, PropAvg, static double? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<double>(o)))
			{
				continue;
			}

			if (propAvgAsString.TryReadProperty(ref reader, options, PropAvgAsString, null))
			{
				continue;
			}

			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propMax.TryReadProperty(ref reader, options, PropMax, static double? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<double>(o)))
			{
				continue;
			}

			if (propMaxAsString.TryReadProperty(ref reader, options, PropMaxAsString, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propMin.TryReadProperty(ref reader, options, PropMin, static double? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<double>(o)))
			{
				continue;
			}

			if (propMinAsString.TryReadProperty(ref reader, options, PropMinAsString, null))
			{
				continue;
			}

			if (propSum.TryReadProperty(ref reader, options, PropSum, null))
			{
				continue;
			}

			if (propSumAsString.TryReadProperty(ref reader, options, PropSumAsString, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.StatsAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Avg = propAvg.Value,
			AvgAsString = propAvgAsString.Value,
			Count = propCount.Value,
			Max = propMax.Value,
			MaxAsString = propMaxAsString.Value,
			Meta = propMeta.Value,
			Min = propMin.Value,
			MinAsString = propMinAsString.Value,
			Sum = propSum.Value,
			SumAsString = propSumAsString.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.StatsAggregate value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAvg, value.Avg, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, double? v) => w.WriteNullableValue<double>(o, v));
		writer.WriteProperty(options, PropAvgAsString, value.AvgAsString, null, null);
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropMax, value.Max, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, double? v) => w.WriteNullableValue<double>(o, v));
		writer.WriteProperty(options, PropMaxAsString, value.MaxAsString, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropMin, value.Min, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, double? v) => w.WriteNullableValue<double>(o, v));
		writer.WriteProperty(options, PropMinAsString, value.MinAsString, null, null);
		writer.WriteProperty(options, PropSum, value.Sum, null, null);
		writer.WriteProperty(options, PropSumAsString, value.SumAsString, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Statistics aggregation result. <c>min</c>, <c>max</c> and <c>avg</c> are missing if there were no values to process
/// (<c>count</c> is zero).
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.StatsAggregateConverter))]
public sealed partial class StatsAggregate : Elastic.Clients.Elasticsearch.Aggregations.IAggregate
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StatsAggregate(double? avg, long count, double? max, double? min, double sum)
	{
		Avg = avg;
		Count = count;
		Max = max;
		Min = min;
		Sum = sum;
	}
#if NET7_0_OR_GREATER
	public StatsAggregate()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public StatsAggregate()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal StatsAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	double? Avg { get; set; }
	public string? AvgAsString { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Count { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double? Max { get; set; }
	public string? MaxAsString { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Meta { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double? Min { get; set; }
	public string? MinAsString { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Sum { get; set; }
	public string? SumAsString { get; set; }

	string Elastic.Clients.Elasticsearch.Aggregations.IAggregate.Type => "stats";
}