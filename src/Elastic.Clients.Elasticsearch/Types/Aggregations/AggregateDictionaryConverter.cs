// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed class AggregateDictionaryConverter : JsonConverter<AggregateDictionary>
{
	public override AggregateDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var dictionary = new Dictionary<string, AggregateBase>();

		if (reader.TokenType != JsonTokenType.StartObject)
			return new AggregateDictionary(dictionary);

		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndObject)
				break;

			var name = reader.GetString(); // TODO: Future optimisation, get raw bytes span and parse based on those

			reader.Read();

			var nameParts = name.Split('#');

			if (nameParts.Length != 2)
				throw new JsonException($"Unable to parse typed-key from aggregation name '{name}'");

			// Bucket-based Aggregates

			var aggregateName = nameParts[0];

			switch (aggregateName)
			{
				case "terms":
				case "sterms":
				case "lterms":
					{
						if (TermsAggregateSerializationHelper.TryDeserialiseTermsAggregate(ref reader, options, out var agg))
						{
							dictionary.Add(nameParts[1], agg);
						}

						break;
					}

				case "adjacency_matrix":
					{
						var agg = JsonSerializer.Deserialize<AdjacencyMatrixAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "auto_date_histogram":
					{
						var agg = JsonSerializer.Deserialize<AutoDateHistogramAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "categorize_text":
					throw new Exception("The aggregate in response is not yet supported");

				case "children":
					{
						var agg = JsonSerializer.Deserialize<ChildrenAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "composite":
					{
						var agg = JsonSerializer.Deserialize<CompositeAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "date_histogram":
					{
						var agg = JsonSerializer.Deserialize<DateHistogramAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "date_range":
					{
						var agg = JsonSerializer.Deserialize<DateRangeAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "diversified_sampler":
					throw new Exception("The aggregate in response is not yet supported");

				case "filter":
					{
						var agg = JsonSerializer.Deserialize<FilterAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "filters":
					{
						var agg = JsonSerializer.Deserialize<FiltersAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				//case "geo_distance":
				//	{
				//		var agg = JsonSerializer.Deserialize<GeoDistanceAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "geohash_grid":
				//	{
				//		var agg = JsonSerializer.Deserialize<GeoHashGridAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "geotile_grid":
				//	{
				//		var agg = JsonSerializer.Deserialize<GeoTileGridAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				case "global":
					{
						var agg = JsonSerializer.Deserialize<GlobalAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "histogram":
					{
						var agg = JsonSerializer.Deserialize<HistogramAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "ip_range":
					{
						var agg = JsonSerializer.Deserialize<IpRangeAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "missing":
					{
						var agg = JsonSerializer.Deserialize<MissingAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "multi_terms":
					{
						var agg = JsonSerializer.Deserialize<MultiTermsAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "nested":
					{
						var agg = JsonSerializer.Deserialize<NestedAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "parent":
					{
						var agg = JsonSerializer.Deserialize<ParentAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "range":
					{
						var agg = JsonSerializer.Deserialize<RangeAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "rare_terms":
					throw new Exception("The aggregate in response is not yet supported");

				case "reverse_nested":
					{
						var agg = JsonSerializer.Deserialize<ReverseNestedAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "sampler":
					{
						var agg = JsonSerializer.Deserialize<SamplerAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "significant_terms":
					throw new Exception("The aggregate in response is not yet supported");

				case "significant_text":
					throw new Exception("The aggregate in response is not yet supported");

				case "variable_width_histogram":
					{
						var agg = JsonSerializer.Deserialize<VariableWidthHistogramAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "avg":
					{
						var agg = JsonSerializer.Deserialize<AvgAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "boxplot":
					{
						var agg = JsonSerializer.Deserialize<BoxPlotAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "cardinality":
					{
						var agg = JsonSerializer.Deserialize<CardinalityAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "extended_stats":
					{
						var agg = JsonSerializer.Deserialize<ExtendedStatsAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				//case "geo_bounds":
				//	{
				//		var agg = JsonSerializer.Deserialize<GeoBoundsAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "geo_centroid":
				//	{
				//		var agg = JsonSerializer.Deserialize<GeoCentroidAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "geo_line":
				//	{
				//		var agg = JsonSerializer.Deserialize<GeoLineAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				case "matrix_stats":
					{
						var agg = JsonSerializer.Deserialize<MatrixStatsAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "max":
					{
						var agg = JsonSerializer.Deserialize<MaxAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "median_absolute_deviation":
					{
						var agg = JsonSerializer.Deserialize<MedianAbsoluteDeviationAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "min":
					{
						var agg = JsonSerializer.Deserialize<MinAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "percentile_ranks":
					throw new Exception("The aggregate in response is not yet supported.");

				//case "tdigest_percentile_ranks":
				//	{
				//		var agg = JsonSerializer.Deserialize<TDigestPercentileRanksAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "percentiles":
				//	break;

				case "rate":
					{
						var agg = JsonSerializer.Deserialize<RateAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				//case "scripted_metric":
				//	{
				//		var agg = JsonSerializer.Deserialize<ScriptedMetricAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				case "stats":
					{
						var agg = JsonSerializer.Deserialize<StatsAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "string_stats":
					{
						var agg = JsonSerializer.Deserialize<StringStatsAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "sum":
					{
						var agg = JsonSerializer.Deserialize<SumAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				//case "t_test":
				//	{
				//		var agg = JsonSerializer.Deserialize<TTestAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "top_hits":
				//	{
				//		var agg = JsonSerializer.Deserialize<TopHitsAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "top_metrics":
				//	{
				//		var agg = JsonSerializer.Deserialize<TopMetricsAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				case "value_count":
					{
						var agg = JsonSerializer.Deserialize<ValueCountAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "weighted_avg":
					{
						var agg = JsonSerializer.Deserialize<WeightedAvgAggregate>(ref reader, options);
						dictionary.Add(nameParts[1], agg);
						break;
					}

				case "avg_bucket":
					throw new Exception("The aggregate in response is not yet supported.");
				case "bucket_script":
					throw new Exception("The aggregate in response is not yet supported.");
				case "bucket_count_ks_test":
					throw new Exception("The aggregate in response is not yet supported.");
				case "bucket_correlation":
					throw new Exception("The aggregate in response is not yet supported.");
				case "bucket_selector":
					throw new Exception("The aggregate in response is not yet supported.");
				case "bucket_sort":
					throw new Exception("The aggregate in response is not yet supported.");
				//case "cumulative_cardinality":
				//	{
				//		var agg = JsonSerializer.Deserialize<CumulativeCardinalityAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "cumulative_sum":
				//	break;
				//case "derivative":
				//	{
				//		var agg = JsonSerializer.Deserialize<DerivativeAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "extended_stats_bucket":
				//	{
				//		var agg = JsonSerializer.Deserialize<ExtendedStatsBucketAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				//case "inference":
				//	{
				//		var agg = JsonSerializer.Deserialize<InferenceAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				case "max_bucket":
					throw new Exception("The aggregate in response is not yet supported.");
				case "min_bucket":
					throw new Exception("The aggregate in response is not yet supported.");
				case "moving_avg":
					throw new Exception("The aggregate in response is not yet supported.");
				case "moving_fn":
					throw new Exception("The aggregate in response is not yet supported.");
				case "moving_percentiles":
					throw new Exception("The aggregate in response is not yet supported.");
				case "normalize":
					throw new Exception("The aggregate in response is not yet supported.");

				//case "percentiles_bucket":
				//	{
				//		var agg = JsonSerializer.Deserialize<PercentilesBucketAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				case "serial_diff":
					throw new Exception("The aggregate in response is not yet supported.");

				//case "stats_bucket":
				//	{
				//		var agg = JsonSerializer.Deserialize<StatsBucketAggregate>(ref reader, options);
				//		dictionary.Add(nameParts[1], agg);
				//		break;
				//	}

				case "sum_bucket":
					break;

				default:
					throw new Exception("The aggregate in response is not yet supported.");
			}
		}

		return new AggregateDictionary(dictionary);
	}

	public override void Write(Utf8JsonWriter writer, AggregateDictionary value, JsonSerializerOptions options) => throw new NotImplementedException();
}
