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

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial interface IAggregate
{
}

[JsonConverter(typeof(AggregateDictionaryConverter))]
public partial class AggregateDictionary : IsAReadOnlyDictionary<string, IAggregate>
{
	public AggregateDictionary(IReadOnlyDictionary<string, IAggregate> backingDictionary) : base(backingDictionary)
	{
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregate? GetAdjacencyMatrix(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregate? GetAutoDateHistogram(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregate? GetAverage(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.AverageAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.BoxplotAggregate? GetBoxplot(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.BoxplotAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.BucketMetricValueAggregate? GetBucketMetricValue(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.BucketMetricValueAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.CardinalityAggregate? GetCardinality(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.CardinalityAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.ChildrenAggregate? GetChildren(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.ChildrenAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregate? GetComposite(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.CumulativeCardinalityAggregate? GetCumulativeCardinality(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.CumulativeCardinalityAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.DateHistogramAggregate? GetDateHistogram(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.DateHistogramAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.DateRangeAggregate? GetDateRange(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.DateRangeAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregate? GetDerivative(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.DoubleTermsAggregate? GetDoubleTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.DoubleTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.ExtendedStatsAggregate? GetExtendedStats(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.ExtendedStatsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.ExtendedStatsBucketAggregate? GetExtendedStatsBucket(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.ExtendedStatsBucketAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.FilterAggregate? GetFilter(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.FilterAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.FiltersAggregate? GetFilters(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.FiltersAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.FrequentItemSetsAggregate? GetFrequentItemSets(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.FrequentItemSetsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.GeoBoundsAggregate? GetGeoBounds(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.GeoBoundsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.GeoCentroidAggregate? GetGeoCentroid(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.GeoCentroidAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.GeoDistanceAggregate? GetGeoDistance(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.GeoDistanceAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.GeohashGridAggregate? GetGeohashGrid(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.GeohashGridAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.GeohexGridAggregate? GetGeohexGrid(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregate? GetGeoLine(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregate? GetGeotileGrid(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregate? GetGlobal(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.HdrPercentileRanksAggregate? GetHdrPercentileRanks(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.HdrPercentileRanksAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.HdrPercentilesAggregate? GetHdrPercentiles(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.HdrPercentilesAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.HistogramAggregate? GetHistogram(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.HistogramAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.InferenceAggregate? GetInference(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.InferenceAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.IpPrefixAggregate? GetIpPrefix(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.IpPrefixAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregate? GetIpRange(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.LongRareTermsAggregate? GetLongRareTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.LongRareTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.LongTermsAggregate? GetLongTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.LongTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.MatrixStatsAggregate? GetMatrixStats(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.MatrixStatsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.MaxAggregate? GetMax(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.MaxAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregate? GetMedianAbsoluteDeviation(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.MinAggregate? GetMin(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.MinAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.MissingAggregate? GetMissing(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.MissingAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.MultiTermsAggregate? GetMultiTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.MultiTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.NestedAggregate? GetNested(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.NestedAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.ParentAggregate? GetParent(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.ParentAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.PercentilesBucketAggregate? GetPercentilesBucket(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.PercentilesBucketAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.RangeAggregate? GetRange(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.RangeAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.RateAggregate? GetRate(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.RateAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.ReverseNestedAggregate? GetReverseNested(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.ReverseNestedAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.SamplerAggregate? GetSampler(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.SamplerAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.ScriptedMetricAggregate? GetScriptedMetric(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.ScriptedMetricAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantLongTermsAggregate? GetSignificantLongTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.SignificantLongTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantStringTermsAggregate? GetSignificantStringTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.SignificantStringTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.SimpleValueAggregate? GetSimpleValue(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.SimpleValueAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.StatsAggregate? GetStats(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.StatsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.StatsBucketAggregate? GetStatsBucket(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.StatsBucketAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.StringRareTermsAggregate? GetStringRareTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.StringRareTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.StringStatsAggregate? GetStringStats(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.StringStatsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.StringTermsAggregate? GetStringTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.StringTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.SumAggregate? GetSum(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.SumAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.TDigestPercentileRanksAggregate? GetTDigestPercentileRanks(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.TDigestPercentileRanksAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.TDigestPercentilesAggregate? GetTDigestPercentiles(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.TDigestPercentilesAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.TopHitsAggregate? GetTopHits(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.TopHitsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.TopMetricsAggregate? GetTopMetrics(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.TTestAggregate? GetTTest(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.TTestAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.UnmappedRareTermsAggregate? GetUnmappedRareTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.UnmappedRareTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.UnmappedSamplerAggregate? GetUnmappedSampler(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.UnmappedSamplerAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.UnmappedSignificantTermsAggregate? GetUnmappedSignificantTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.UnmappedSignificantTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.UnmappedTermsAggregate? GetUnmappedTerms(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.UnmappedTermsAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.ValueCountAggregate? GetValueCount(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.ValueCountAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.VariableWidthHistogramAggregate? GetVariableWidthHistogram(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.VariableWidthHistogramAggregate>(key);
	public Elastic.Clients.Elasticsearch.Aggregations.WeightedAverageAggregate? GetWeightedAverage(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.WeightedAverageAggregate>(key);
	private T? TryGet<T>(string key) where T : class, IAggregate => BackingDictionary.TryGetValue(key, out var value) ? value as T : null;
}

internal sealed partial class AggregateDictionaryConverter : JsonConverter<AggregateDictionary>
{
	public override AggregateDictionary Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var dictionary = new Dictionary<string, IAggregate>();
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException($"Expected {JsonTokenType.StartObject} but read {reader.TokenType}.");
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType != JsonTokenType.PropertyName)
				throw new JsonException($"Expected {JsonTokenType.PropertyName} but read {reader.TokenType}.");
			var name = reader.GetString();
			reader.Read();
			ReadItem(ref reader, options, dictionary, name);
		}

		return new AggregateDictionary(dictionary);
	}

	public override void Write(Utf8JsonWriter writer, AggregateDictionary value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("'AggregateDictionary' is a readonly type, used only on responses and does not support being written to JSON.");
	}

	public static void ReadItem(ref Utf8JsonReader reader, JsonSerializerOptions options, Dictionary<string, IAggregate> dictionary, string name)
	{
		var nameParts = name.Split('#');
		if (nameParts.Length != 2)
			throw new JsonException($"Unable to parse typed-key '{name}'.");
		var type = nameParts[0];
		switch (type)
		{
			case "adjacency_matrix":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "auto_date_histogram":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "avg":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.AverageAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "box_plot":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.BoxplotAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "bucket_metric_value":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.BucketMetricValueAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "cardinality":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.CardinalityAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "children":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.ChildrenAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "composite":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "simple_long_value":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.CumulativeCardinalityAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "date_histogram":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.DateHistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "date_range":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.DateRangeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "derivative":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "dterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.DoubleTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "extended_stats":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.ExtendedStatsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "extended_stats_bucket":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.ExtendedStatsBucketAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "filter":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.FilterAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "filters":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.FiltersAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "frequent_item_sets":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.FrequentItemSetsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "geo_bounds":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeoBoundsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "geo_centroid":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeoCentroidAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "geo_distance":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeoDistanceAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "geohash_grid":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeohashGridAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "geohex_grid":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "geo_line":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeoLineAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "geotile_grid":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeotileGridAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "global":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "hdr_percentile_ranks":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.HdrPercentileRanksAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "hdr_percentiles":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.HdrPercentilesAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "histogram":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.HistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "inference":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.InferenceAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "ip_prefix":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.IpPrefixAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "ip_range":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "lrareterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.LongRareTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "lterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.LongTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "matrix_stats":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.MatrixStatsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "max":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.MaxAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "median_absolute_deviation":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "min":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.MinAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "missing":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.MissingAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "multi_terms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.MultiTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "nested":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.NestedAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "parent":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.ParentAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "percentiles_bucket":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.PercentilesBucketAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "range":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.RangeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "rate":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.RateAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "reverse_nested":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.ReverseNestedAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "sampler":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.SamplerAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "scripted_metric":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.ScriptedMetricAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "siglterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.SignificantLongTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "sigsterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.SignificantStringTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "simple_value":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.SimpleValueAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "stats":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.StatsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "stats_bucket":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.StatsBucketAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "srareterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.StringRareTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "string_stats":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.StringStatsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "sterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.StringTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "sum":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.SumAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "tdigest_percentile_ranks":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TDigestPercentileRanksAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "tdigest_percentiles":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TDigestPercentilesAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "top_hits":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TopHitsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "top_metrics":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "t_test":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TTestAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "umrareterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.UnmappedRareTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "unmapped_sampler":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.UnmappedSamplerAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "umsigterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.UnmappedSignificantTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "umterms":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.UnmappedTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "value_count":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.ValueCountAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "variable_width_histogram":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.VariableWidthHistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			case "weighted_avg":
				{
					var item = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.WeightedAverageAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], item);
					break;
				}

			default:
				throw new NotSupportedException($"The tagged variant '{type}' is currently not supported.");
		}
	}
}