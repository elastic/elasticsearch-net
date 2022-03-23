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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	[JsonConverter(typeof(AggregationContainerConverter))]
	public partial class AggregationContainer : IContainer
	{
		public AggregationContainer(AggregationBase variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
		internal AggregationBase Variant { get; }
	}

	internal sealed class AggregationContainerConverter : JsonConverter<AggregationContainer>
	{
		public override AggregationContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var readerCopy = reader;
			readerCopy.Read();
			if (readerCopy.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException();
			}

			var propertyName = readerCopy.GetString();
			if (propertyName == "adjacency_matrix")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.AdjacencyMatrixAggregation?>("adjacency_matrix", ref reader, options);
			}

			if (propertyName == "auto_date_histogram")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation?>("auto_date_histogram", ref reader, options);
			}

			if (propertyName == "avg")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation?>("avg", ref reader, options);
			}

			if (propertyName == "boxplot")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.BoxplotAggregation?>("boxplot", ref reader, options);
			}

			if (propertyName == "cardinality")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.CardinalityAggregation?>("cardinality", ref reader, options);
			}

			if (propertyName == "children")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.ChildrenAggregation?>("children", ref reader, options);
			}

			if (propertyName == "composite")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.CompositeAggregation?>("composite", ref reader, options);
			}

			if (propertyName == "cumulative_cardinality")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.CumulativeCardinalityAggregation?>("cumulative_cardinality", ref reader, options);
			}

			if (propertyName == "date_histogram")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.DateHistogramAggregation?>("date_histogram", ref reader, options);
			}

			if (propertyName == "date_range")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.DateRangeAggregation?>("date_range", ref reader, options);
			}

			if (propertyName == "derivative")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregation?>("derivative", ref reader, options);
			}

			if (propertyName == "extended_stats")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.ExtendedStatsAggregation?>("extended_stats", ref reader, options);
			}

			if (propertyName == "filters")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.FiltersAggregation?>("filters", ref reader, options);
			}

			if (propertyName == "global")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation?>("global", ref reader, options);
			}

			if (propertyName == "histogram")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.HistogramAggregation?>("histogram", ref reader, options);
			}

			if (propertyName == "ip_range")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.IpRangeAggregation?>("ip_range", ref reader, options);
			}

			if (propertyName == "matrix_stats")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.MatrixStatsAggregation?>("matrix_stats", ref reader, options);
			}

			if (propertyName == "max")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.MaxAggregation?>("max", ref reader, options);
			}

			if (propertyName == "median_absolute_deviation")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation?>("median_absolute_deviation", ref reader, options);
			}

			if (propertyName == "min")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.MinAggregation?>("min", ref reader, options);
			}

			if (propertyName == "missing")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.MissingAggregation?>("missing", ref reader, options);
			}

			if (propertyName == "multi_terms")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.MultiTermsAggregation?>("multi_terms", ref reader, options);
			}

			if (propertyName == "nested")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation?>("nested", ref reader, options);
			}

			if (propertyName == "parent")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.ParentAggregation?>("parent", ref reader, options);
			}

			if (propertyName == "range")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.RangeAggregation?>("range", ref reader, options);
			}

			if (propertyName == "rate")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.RateAggregation?>("rate", ref reader, options);
			}

			if (propertyName == "reverse_nested")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.ReverseNestedAggregation?>("reverse_nested", ref reader, options);
			}

			if (propertyName == "sampler")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.SamplerAggregation?>("sampler", ref reader, options);
			}

			if (propertyName == "scripted_metric")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.ScriptedMetricAggregation?>("scripted_metric", ref reader, options);
			}

			if (propertyName == "stats")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.StatsAggregation?>("stats", ref reader, options);
			}

			if (propertyName == "string_stats")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.StringStatsAggregation?>("string_stats", ref reader, options);
			}

			if (propertyName == "sum")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.SumAggregation?>("sum", ref reader, options);
			}

			if (propertyName == "t_test")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.TTestAggregation?>("t_test", ref reader, options);
			}

			if (propertyName == "terms")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregation?>("terms", ref reader, options);
			}

			if (propertyName == "top_hits")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.TopHitsAggregation?>("top_hits", ref reader, options);
			}

			if (propertyName == "top_metrics")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsAggregation?>("top_metrics", ref reader, options);
			}

			if (propertyName == "value_count")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.ValueCountAggregation?>("value_count", ref reader, options);
			}

			if (propertyName == "variable_width_histogram")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.VariableWidthHistogramAggregation?>("variable_width_histogram", ref reader, options);
			}

			if (propertyName == "weighted_avg")
			{
				return AggregationContainerSerializationHelper.ReadContainer<Elastic.Clients.Elasticsearch.Aggregations.WeightedAverageAggregation?>("weighted_avg", ref reader, options);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, AggregationContainer value, JsonSerializerOptions options)
		{
			if (value is null)
			{
				writer.WriteNullValue();
			}
			else if (value.SerializeFluent is not null)
			{
				value.SerializeFluent(writer, options);
			}
			else if (value.Variant is not null)
			{
				var type = value.Variant.GetType();
				JsonSerializer.Serialize(writer, value.Variant, type, options);
			}
			else
			{
				throw new JsonException("Invalid container cannot be serialised");
			}
		}
	}

	public sealed partial class AggregationContainerDescriptor<TDocument> : DescriptorBase<AggregationContainerDescriptor<TDocument>>
	{
		internal AggregationContainerDescriptor(Action<AggregationContainerDescriptor<TDocument>> configure) => configure.Invoke(this);
		public AggregationContainerDescriptor() : base()
		{
		}

		public AggregationContainerDescriptor<TDocument> AdjacencyMatrix(string name, Action<AdjacencyMatrixAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("adjacency_matrix", configure));
		}

		public AggregationContainerDescriptor<TDocument> AutoDateHistogram(string name, Action<AutoDateHistogramAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("auto_date_histogram", configure));
		}

		public AggregationContainerDescriptor<TDocument> Avg(string name, Action<AverageAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("avg", configure));
		}

		public AggregationContainerDescriptor<TDocument> Boxplot(string name, Action<BoxplotAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("boxplot", configure));
		}

		public AggregationContainerDescriptor<TDocument> Cardinality(string name, Action<CardinalityAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("cardinality", configure));
		}

		public AggregationContainerDescriptor<TDocument> Children(string name, Action<ChildrenAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("children", configure));
		}

		public AggregationContainerDescriptor<TDocument> Composite(string name, Action<CompositeAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("composite", configure));
		}

		public AggregationContainerDescriptor<TDocument> CumulativeCardinality(string name, Action<CumulativeCardinalityAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("cumulative_cardinality", configure));
		}

		public AggregationContainerDescriptor<TDocument> DateHistogram(string name, Action<DateHistogramAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("date_histogram", configure));
		}

		public AggregationContainerDescriptor<TDocument> DateRange(string name, Action<DateRangeAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("date_range", configure));
		}

		public AggregationContainerDescriptor<TDocument> Derivative(string name, Action<DerivativeAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("derivative", configure));
		}

		public AggregationContainerDescriptor<TDocument> ExtendedStats(string name, Action<ExtendedStatsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("extended_stats", configure));
		}

		public AggregationContainerDescriptor<TDocument> Filters(string name, Action<FiltersAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("filters", configure));
		}

		public AggregationContainerDescriptor<TDocument> Global(string name, Action<GlobalAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("global", configure));
		}

		public AggregationContainerDescriptor<TDocument> Histogram(string name, Action<HistogramAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("histogram", configure));
		}

		public AggregationContainerDescriptor<TDocument> IpRange(string name, Action<IpRangeAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("ip_range", configure));
		}

		public AggregationContainerDescriptor<TDocument> MatrixStats(string name, Action<MatrixStatsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("matrix_stats", configure));
		}

		public AggregationContainerDescriptor<TDocument> Max(string name, Action<MaxAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("max", configure));
		}

		public AggregationContainerDescriptor<TDocument> MedianAbsoluteDeviation(string name, Action<MedianAbsoluteDeviationAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("median_absolute_deviation", configure));
		}

		public AggregationContainerDescriptor<TDocument> Min(string name, Action<MinAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("min", configure));
		}

		public AggregationContainerDescriptor<TDocument> Missing(string name, Action<MissingAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("missing", configure));
		}

		public AggregationContainerDescriptor<TDocument> MultiTerms(string name, Action<MultiTermsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("multi_terms", configure));
		}

		public AggregationContainerDescriptor<TDocument> Nested(string name, Action<NestedAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("nested", configure));
		}

		public AggregationContainerDescriptor<TDocument> Parent(string name, Action<ParentAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("parent", configure));
		}

		public AggregationContainerDescriptor<TDocument> Range(string name, Action<RangeAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("range", configure));
		}

		public AggregationContainerDescriptor<TDocument> Rate(string name, Action<RateAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("rate", configure));
		}

		public AggregationContainerDescriptor<TDocument> ReverseNested(string name, Action<ReverseNestedAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("reverse_nested", configure));
		}

		public AggregationContainerDescriptor<TDocument> Sampler(string name, Action<SamplerAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("sampler", configure));
		}

		public AggregationContainerDescriptor<TDocument> ScriptedMetric(string name, Action<ScriptedMetricAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("scripted_metric", configure));
		}

		public AggregationContainerDescriptor<TDocument> Stats(string name, Action<StatsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("stats", configure));
		}

		public AggregationContainerDescriptor<TDocument> StringStats(string name, Action<StringStatsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("string_stats", configure));
		}

		public AggregationContainerDescriptor<TDocument> Sum(string name, Action<SumAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("sum", configure));
		}

		public AggregationContainerDescriptor<TDocument> TTest(string name, Action<TTestAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("t_test", configure));
		}

		public AggregationContainerDescriptor<TDocument> Terms(string name, Action<TermsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("terms", configure));
		}

		public AggregationContainerDescriptor<TDocument> TopHits(string name, Action<TopHitsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("top_hits", configure));
		}

		public AggregationContainerDescriptor<TDocument> TopMetrics(string name, Action<TopMetricsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("top_metrics", configure));
		}

		public AggregationContainerDescriptor<TDocument> ValueCount(string name, Action<ValueCountAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("value_count", configure));
		}

		public AggregationContainerDescriptor<TDocument> VariableWidthHistogram(string name, Action<VariableWidthHistogramAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("variable_width_histogram", configure));
		}

		public AggregationContainerDescriptor<TDocument> WeightedAvg(string name, Action<WeightedAverageAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("weighted_avg", configure));
		}
	}

	public sealed partial class AggregationContainerDescriptor : DescriptorBase<AggregationContainerDescriptor>
	{
		internal AggregationContainerDescriptor(Action<AggregationContainerDescriptor> configure) => configure.Invoke(this);
		public AggregationContainerDescriptor() : base()
		{
		}

		public AggregationContainerDescriptor AdjacencyMatrix(string name, Action<AdjacencyMatrixAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("adjacency_matrix", configure));
		}

		public AggregationContainerDescriptor AdjacencyMatrix<TDocument>(string name, Action<AdjacencyMatrixAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("adjacency_matrix", configure));
		}

		public AggregationContainerDescriptor AutoDateHistogram(string name, Action<AutoDateHistogramAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("auto_date_histogram", configure));
		}

		public AggregationContainerDescriptor AutoDateHistogram<TDocument>(string name, Action<AutoDateHistogramAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("auto_date_histogram", configure));
		}

		public AggregationContainerDescriptor Avg(string name, Action<AverageAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("avg", configure));
		}

		public AggregationContainerDescriptor Avg<TDocument>(string name, Action<AverageAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("avg", configure));
		}

		public AggregationContainerDescriptor Boxplot(string name, Action<BoxplotAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("boxplot", configure));
		}

		public AggregationContainerDescriptor Boxplot<TDocument>(string name, Action<BoxplotAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("boxplot", configure));
		}

		public AggregationContainerDescriptor Cardinality(string name, Action<CardinalityAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("cardinality", configure));
		}

		public AggregationContainerDescriptor Cardinality<TDocument>(string name, Action<CardinalityAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("cardinality", configure));
		}

		public AggregationContainerDescriptor Children(string name, Action<ChildrenAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("children", configure));
		}

		public AggregationContainerDescriptor Children<TDocument>(string name, Action<ChildrenAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("children", configure));
		}

		public AggregationContainerDescriptor Composite(string name, Action<CompositeAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("composite", configure));
		}

		public AggregationContainerDescriptor Composite<TDocument>(string name, Action<CompositeAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("composite", configure));
		}

		public AggregationContainerDescriptor CumulativeCardinality(string name, Action<CumulativeCardinalityAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("cumulative_cardinality", configure));
		}

		public AggregationContainerDescriptor DateHistogram(string name, Action<DateHistogramAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("date_histogram", configure));
		}

		public AggregationContainerDescriptor DateHistogram<TDocument>(string name, Action<DateHistogramAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("date_histogram", configure));
		}

		public AggregationContainerDescriptor DateRange(string name, Action<DateRangeAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("date_range", configure));
		}

		public AggregationContainerDescriptor DateRange<TDocument>(string name, Action<DateRangeAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("date_range", configure));
		}

		public AggregationContainerDescriptor Derivative(string name, Action<DerivativeAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("derivative", configure));
		}

		public AggregationContainerDescriptor ExtendedStats(string name, Action<ExtendedStatsAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("extended_stats", configure));
		}

		public AggregationContainerDescriptor ExtendedStats<TDocument>(string name, Action<ExtendedStatsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("extended_stats", configure));
		}

		public AggregationContainerDescriptor Filters(string name, Action<FiltersAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("filters", configure));
		}

		public AggregationContainerDescriptor Filters<TDocument>(string name, Action<FiltersAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("filters", configure));
		}

		public AggregationContainerDescriptor Global(string name, Action<GlobalAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("global", configure));
		}

		public AggregationContainerDescriptor Global<TDocument>(string name, Action<GlobalAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("global", configure));
		}

		public AggregationContainerDescriptor Histogram(string name, Action<HistogramAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("histogram", configure));
		}

		public AggregationContainerDescriptor Histogram<TDocument>(string name, Action<HistogramAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("histogram", configure));
		}

		public AggregationContainerDescriptor IpRange(string name, Action<IpRangeAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("ip_range", configure));
		}

		public AggregationContainerDescriptor IpRange<TDocument>(string name, Action<IpRangeAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("ip_range", configure));
		}

		public AggregationContainerDescriptor MatrixStats(string name, Action<MatrixStatsAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("matrix_stats", configure));
		}

		public AggregationContainerDescriptor MatrixStats<TDocument>(string name, Action<MatrixStatsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("matrix_stats", configure));
		}

		public AggregationContainerDescriptor Max(string name, Action<MaxAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("max", configure));
		}

		public AggregationContainerDescriptor Max<TDocument>(string name, Action<MaxAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("max", configure));
		}

		public AggregationContainerDescriptor MedianAbsoluteDeviation(string name, Action<MedianAbsoluteDeviationAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("median_absolute_deviation", configure));
		}

		public AggregationContainerDescriptor MedianAbsoluteDeviation<TDocument>(string name, Action<MedianAbsoluteDeviationAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("median_absolute_deviation", configure));
		}

		public AggregationContainerDescriptor Min(string name, Action<MinAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("min", configure));
		}

		public AggregationContainerDescriptor Min<TDocument>(string name, Action<MinAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("min", configure));
		}

		public AggregationContainerDescriptor Missing(string name, Action<MissingAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("missing", configure));
		}

		public AggregationContainerDescriptor Missing<TDocument>(string name, Action<MissingAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("missing", configure));
		}

		public AggregationContainerDescriptor MultiTerms(string name, Action<MultiTermsAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("multi_terms", configure));
		}

		public AggregationContainerDescriptor MultiTerms<TDocument>(string name, Action<MultiTermsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("multi_terms", configure));
		}

		public AggregationContainerDescriptor Nested(string name, Action<NestedAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("nested", configure));
		}

		public AggregationContainerDescriptor Nested<TDocument>(string name, Action<NestedAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("nested", configure));
		}

		public AggregationContainerDescriptor Parent(string name, Action<ParentAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("parent", configure));
		}

		public AggregationContainerDescriptor Parent<TDocument>(string name, Action<ParentAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("parent", configure));
		}

		public AggregationContainerDescriptor Range(string name, Action<RangeAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("range", configure));
		}

		public AggregationContainerDescriptor Range<TDocument>(string name, Action<RangeAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("range", configure));
		}

		public AggregationContainerDescriptor Rate(string name, Action<RateAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("rate", configure));
		}

		public AggregationContainerDescriptor Rate<TDocument>(string name, Action<RateAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("rate", configure));
		}

		public AggregationContainerDescriptor ReverseNested(string name, Action<ReverseNestedAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("reverse_nested", configure));
		}

		public AggregationContainerDescriptor ReverseNested<TDocument>(string name, Action<ReverseNestedAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("reverse_nested", configure));
		}

		public AggregationContainerDescriptor Sampler(string name, Action<SamplerAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("sampler", configure));
		}

		public AggregationContainerDescriptor Sampler<TDocument>(string name, Action<SamplerAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("sampler", configure));
		}

		public AggregationContainerDescriptor ScriptedMetric(string name, Action<ScriptedMetricAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("scripted_metric", configure));
		}

		public AggregationContainerDescriptor ScriptedMetric<TDocument>(string name, Action<ScriptedMetricAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("scripted_metric", configure));
		}

		public AggregationContainerDescriptor Stats(string name, Action<StatsAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("stats", configure));
		}

		public AggregationContainerDescriptor Stats<TDocument>(string name, Action<StatsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("stats", configure));
		}

		public AggregationContainerDescriptor StringStats(string name, Action<StringStatsAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("string_stats", configure));
		}

		public AggregationContainerDescriptor StringStats<TDocument>(string name, Action<StringStatsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("string_stats", configure));
		}

		public AggregationContainerDescriptor Sum(string name, Action<SumAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("sum", configure));
		}

		public AggregationContainerDescriptor Sum<TDocument>(string name, Action<SumAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("sum", configure));
		}

		public AggregationContainerDescriptor TTest(string name, Action<TTestAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("t_test", configure));
		}

		public AggregationContainerDescriptor TTest<TDocument>(string name, Action<TTestAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("t_test", configure));
		}

		public AggregationContainerDescriptor Terms(string name, Action<TermsAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("terms", configure));
		}

		public AggregationContainerDescriptor Terms<TDocument>(string name, Action<TermsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("terms", configure));
		}

		public AggregationContainerDescriptor TopHits(string name, Action<TopHitsAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("top_hits", configure));
		}

		public AggregationContainerDescriptor TopHits<TDocument>(string name, Action<TopHitsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("top_hits", configure));
		}

		public AggregationContainerDescriptor TopMetrics(string name, Action<TopMetricsAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("top_metrics", configure));
		}

		public AggregationContainerDescriptor TopMetrics<TDocument>(string name, Action<TopMetricsAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("top_metrics", configure));
		}

		public AggregationContainerDescriptor ValueCount(string name, Action<ValueCountAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("value_count", configure));
		}

		public AggregationContainerDescriptor ValueCount<TDocument>(string name, Action<ValueCountAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("value_count", configure));
		}

		public AggregationContainerDescriptor VariableWidthHistogram(string name, Action<VariableWidthHistogramAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("variable_width_histogram", configure));
		}

		public AggregationContainerDescriptor VariableWidthHistogram<TDocument>(string name, Action<VariableWidthHistogramAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("variable_width_histogram", configure));
		}

		public AggregationContainerDescriptor WeightedAvg(string name, Action<WeightedAverageAggregationDescriptor> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("weighted_avg", configure));
		}

		public AggregationContainerDescriptor WeightedAvg<TDocument>(string name, Action<WeightedAverageAggregationDescriptor<TDocument>> configure)
		{
			return SetContainer(name, AggregationContainer.CreateWithAction("weighted_avg", configure));
		}
	}
}