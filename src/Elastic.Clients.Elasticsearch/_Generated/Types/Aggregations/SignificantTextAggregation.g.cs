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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class SignificantTextAggregation : Aggregations.BucketAggregationBase, IAggregationContainerVariant
	{
		public SignificantTextAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "significant_text";
		[JsonInclude]
		[JsonPropertyName("background_filter")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? BackgroundFilter { get; set; }

		[JsonInclude]
		[JsonPropertyName("chi_square")]
		public Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristic? ChiSquare { get; set; }

		[JsonInclude]
		[JsonPropertyName("exclude")]
		public Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? Exclude { get; set; }

		[JsonInclude]
		[JsonPropertyName("execution_hint")]
		public Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? ExecutionHint { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public string? Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("filter_duplicate_text")]
		public bool? FilterDuplicateText { get; set; }

		[JsonInclude]
		[JsonPropertyName("gnd")]
		public Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristic? Gnd { get; set; }

		[JsonInclude]
		[JsonPropertyName("include")]
		public string? Include { get; set; }

		[JsonInclude]
		[JsonPropertyName("min_doc_count")]
		public long? MinDocCount { get; set; }

		[JsonInclude]
		[JsonPropertyName("mutual_information")]
		public Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristic? MutualInformation { get; set; }

		[JsonInclude]
		[JsonPropertyName("percentage")]
		public Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristic? Percentage { get; set; }

		[JsonInclude]
		[JsonPropertyName("script_heuristic")]
		public Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristic? ScriptHeuristic { get; set; }

		[JsonInclude]
		[JsonPropertyName("shard_min_doc_count")]
		public long? ShardMinDocCount { get; set; }

		[JsonInclude]
		[JsonPropertyName("shard_size")]
		public int? ShardSize { get; set; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public int? Size { get; set; }

		[JsonInclude]
		[JsonPropertyName("source_fields")]
		public Elastic.Clients.Elasticsearch.Fields? SourceFields { get; set; }
	}

	public sealed partial class SignificantTextAggregationDescriptor<T> : DescriptorBase<SignificantTextAggregationDescriptor<T>>
	{
		public SignificantTextAggregationDescriptor()
		{
		}

		internal SignificantTextAggregationDescriptor(Action<SignificantTextAggregationDescriptor<T>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? BackgroundFilterValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristic? ChiSquareValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? ExcludeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? ExecutionHintValue { get; private set; }

		internal string? FieldValue { get; private set; }

		internal bool? FilterDuplicateTextValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristic? GndValue { get; private set; }

		internal string? IncludeValue { get; private set; }

		internal long? MinDocCountValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristic? MutualInformationValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristic? PercentageValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristic? ScriptHeuristicValue { get; private set; }

		internal long? ShardMinDocCountValue { get; private set; }

		internal int? ShardSizeValue { get; private set; }

		internal int? SizeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Fields? SourceFieldsValue { get; private set; }

		internal QueryDsl.QueryContainerDescriptor<T> BackgroundFilterDescriptor { get; private set; }

		internal ChiSquareHeuristicDescriptor ChiSquareDescriptor { get; private set; }

		internal GoogleNormalizedDistanceHeuristicDescriptor GndDescriptor { get; private set; }

		internal MutualInformationHeuristicDescriptor MutualInformationDescriptor { get; private set; }

		internal PercentageScoreHeuristicDescriptor PercentageDescriptor { get; private set; }

		internal ScriptedHeuristicDescriptor ScriptHeuristicDescriptor { get; private set; }

		internal Action<QueryDsl.QueryContainerDescriptor<T>> BackgroundFilterDescriptorAction { get; private set; }

		internal Action<ChiSquareHeuristicDescriptor> ChiSquareDescriptorAction { get; private set; }

		internal Action<GoogleNormalizedDistanceHeuristicDescriptor> GndDescriptorAction { get; private set; }

		internal Action<MutualInformationHeuristicDescriptor> MutualInformationDescriptorAction { get; private set; }

		internal Action<PercentageScoreHeuristicDescriptor> PercentageDescriptorAction { get; private set; }

		internal Action<ScriptedHeuristicDescriptor> ScriptHeuristicDescriptorAction { get; private set; }

		public SignificantTextAggregationDescriptor<T> BackgroundFilter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? backgroundFilter)
		{
			BackgroundFilterDescriptor = null;
			BackgroundFilterDescriptorAction = null;
			return Assign(backgroundFilter, (a, v) => a.BackgroundFilterValue = v);
		}

		public SignificantTextAggregationDescriptor<T> BackgroundFilter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainerDescriptor<T> descriptor)
		{
			BackgroundFilterValue = null;
			BackgroundFilterDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.BackgroundFilterDescriptor = v);
		}

		public SignificantTextAggregationDescriptor<T> BackgroundFilter(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryContainerDescriptor<T>> configure)
		{
			BackgroundFilterValue = null;
			BackgroundFilterDescriptorAction = null;
			return Assign(configure, (a, v) => a.BackgroundFilterDescriptorAction = v);
		}

		public SignificantTextAggregationDescriptor<T> ChiSquare(Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristic? chiSquare)
		{
			ChiSquareDescriptor = null;
			ChiSquareDescriptorAction = null;
			return Assign(chiSquare, (a, v) => a.ChiSquareValue = v);
		}

		public SignificantTextAggregationDescriptor<T> ChiSquare(Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristicDescriptor descriptor)
		{
			ChiSquareValue = null;
			ChiSquareDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.ChiSquareDescriptor = v);
		}

		public SignificantTextAggregationDescriptor<T> ChiSquare(Action<Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristicDescriptor> configure)
		{
			ChiSquareValue = null;
			ChiSquareDescriptorAction = null;
			return Assign(configure, (a, v) => a.ChiSquareDescriptorAction = v);
		}

		public SignificantTextAggregationDescriptor<T> Exclude(Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? exclude) => Assign(exclude, (a, v) => a.ExcludeValue = v);
		public SignificantTextAggregationDescriptor<T> ExecutionHint(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? executionHint) => Assign(executionHint, (a, v) => a.ExecutionHintValue = v);
		public SignificantTextAggregationDescriptor<T> Field(string? field) => Assign(field, (a, v) => a.FieldValue = v);
		public SignificantTextAggregationDescriptor<T> FilterDuplicateText(bool? filterDuplicateText = true) => Assign(filterDuplicateText, (a, v) => a.FilterDuplicateTextValue = v);
		public SignificantTextAggregationDescriptor<T> Gnd(Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristic? gnd)
		{
			GndDescriptor = null;
			GndDescriptorAction = null;
			return Assign(gnd, (a, v) => a.GndValue = v);
		}

		public SignificantTextAggregationDescriptor<T> Gnd(Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristicDescriptor descriptor)
		{
			GndValue = null;
			GndDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.GndDescriptor = v);
		}

		public SignificantTextAggregationDescriptor<T> Gnd(Action<Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristicDescriptor> configure)
		{
			GndValue = null;
			GndDescriptorAction = null;
			return Assign(configure, (a, v) => a.GndDescriptorAction = v);
		}

		public SignificantTextAggregationDescriptor<T> Include(string? include) => Assign(include, (a, v) => a.IncludeValue = v);
		public SignificantTextAggregationDescriptor<T> MinDocCount(long? minDocCount) => Assign(minDocCount, (a, v) => a.MinDocCountValue = v);
		public SignificantTextAggregationDescriptor<T> MutualInformation(Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristic? mutualInformation)
		{
			MutualInformationDescriptor = null;
			MutualInformationDescriptorAction = null;
			return Assign(mutualInformation, (a, v) => a.MutualInformationValue = v);
		}

		public SignificantTextAggregationDescriptor<T> MutualInformation(Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristicDescriptor descriptor)
		{
			MutualInformationValue = null;
			MutualInformationDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.MutualInformationDescriptor = v);
		}

		public SignificantTextAggregationDescriptor<T> MutualInformation(Action<Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristicDescriptor> configure)
		{
			MutualInformationValue = null;
			MutualInformationDescriptorAction = null;
			return Assign(configure, (a, v) => a.MutualInformationDescriptorAction = v);
		}

		public SignificantTextAggregationDescriptor<T> Percentage(Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristic? percentage)
		{
			PercentageDescriptor = null;
			PercentageDescriptorAction = null;
			return Assign(percentage, (a, v) => a.PercentageValue = v);
		}

		public SignificantTextAggregationDescriptor<T> Percentage(Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristicDescriptor descriptor)
		{
			PercentageValue = null;
			PercentageDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.PercentageDescriptor = v);
		}

		public SignificantTextAggregationDescriptor<T> Percentage(Action<Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristicDescriptor> configure)
		{
			PercentageValue = null;
			PercentageDescriptorAction = null;
			return Assign(configure, (a, v) => a.PercentageDescriptorAction = v);
		}

		public SignificantTextAggregationDescriptor<T> ScriptHeuristic(Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristic? scriptHeuristic)
		{
			ScriptHeuristicDescriptor = null;
			ScriptHeuristicDescriptorAction = null;
			return Assign(scriptHeuristic, (a, v) => a.ScriptHeuristicValue = v);
		}

		public SignificantTextAggregationDescriptor<T> ScriptHeuristic(Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristicDescriptor descriptor)
		{
			ScriptHeuristicValue = null;
			ScriptHeuristicDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.ScriptHeuristicDescriptor = v);
		}

		public SignificantTextAggregationDescriptor<T> ScriptHeuristic(Action<Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristicDescriptor> configure)
		{
			ScriptHeuristicValue = null;
			ScriptHeuristicDescriptorAction = null;
			return Assign(configure, (a, v) => a.ScriptHeuristicDescriptorAction = v);
		}

		public SignificantTextAggregationDescriptor<T> ShardMinDocCount(long? shardMinDocCount) => Assign(shardMinDocCount, (a, v) => a.ShardMinDocCountValue = v);
		public SignificantTextAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSizeValue = v);
		public SignificantTextAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.SizeValue = v);
		public SignificantTextAggregationDescriptor<T> SourceFields(Elastic.Clients.Elasticsearch.Fields? sourceFields) => Assign(sourceFields, (a, v) => a.SourceFieldsValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (BackgroundFilterDescriptor is not null)
			{
				writer.WritePropertyName("background_filter");
				JsonSerializer.Serialize(writer, BackgroundFilterDescriptor, options);
			}
			else if (BackgroundFilterDescriptorAction is not null)
			{
				writer.WritePropertyName("background_filter");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor<T>(BackgroundFilterDescriptorAction), options);
			}
			else if (BackgroundFilterValue is not null)
			{
				writer.WritePropertyName("background_filter");
				JsonSerializer.Serialize(writer, BackgroundFilterValue, options);
			}

			if (ChiSquareDescriptor is not null)
			{
				writer.WritePropertyName("chi_square");
				JsonSerializer.Serialize(writer, ChiSquareDescriptor, options);
			}
			else if (ChiSquareDescriptorAction is not null)
			{
				writer.WritePropertyName("chi_square");
				JsonSerializer.Serialize(writer, new ChiSquareHeuristicDescriptor(ChiSquareDescriptorAction), options);
			}
			else if (ChiSquareValue is not null)
			{
				writer.WritePropertyName("chi_square");
				JsonSerializer.Serialize(writer, ChiSquareValue, options);
			}

			if (ExcludeValue is not null)
			{
				writer.WritePropertyName("exclude");
				JsonSerializer.Serialize(writer, ExcludeValue, options);
			}

			if (ExecutionHintValue is not null)
			{
				writer.WritePropertyName("execution_hint");
				JsonSerializer.Serialize(writer, ExecutionHintValue, options);
			}

			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (FilterDuplicateTextValue.HasValue)
			{
				writer.WritePropertyName("filter_duplicate_text");
				writer.WriteBooleanValue(FilterDuplicateTextValue.Value);
			}

			if (GndDescriptor is not null)
			{
				writer.WritePropertyName("gnd");
				JsonSerializer.Serialize(writer, GndDescriptor, options);
			}
			else if (GndDescriptorAction is not null)
			{
				writer.WritePropertyName("gnd");
				JsonSerializer.Serialize(writer, new GoogleNormalizedDistanceHeuristicDescriptor(GndDescriptorAction), options);
			}
			else if (GndValue is not null)
			{
				writer.WritePropertyName("gnd");
				JsonSerializer.Serialize(writer, GndValue, options);
			}

			if (!string.IsNullOrEmpty(IncludeValue))
			{
				writer.WritePropertyName("include");
				writer.WriteStringValue(IncludeValue);
			}

			if (MinDocCountValue.HasValue)
			{
				writer.WritePropertyName("min_doc_count");
				writer.WriteNumberValue(MinDocCountValue.Value);
			}

			if (MutualInformationDescriptor is not null)
			{
				writer.WritePropertyName("mutual_information");
				JsonSerializer.Serialize(writer, MutualInformationDescriptor, options);
			}
			else if (MutualInformationDescriptorAction is not null)
			{
				writer.WritePropertyName("mutual_information");
				JsonSerializer.Serialize(writer, new MutualInformationHeuristicDescriptor(MutualInformationDescriptorAction), options);
			}
			else if (MutualInformationValue is not null)
			{
				writer.WritePropertyName("mutual_information");
				JsonSerializer.Serialize(writer, MutualInformationValue, options);
			}

			if (PercentageDescriptor is not null)
			{
				writer.WritePropertyName("percentage");
				JsonSerializer.Serialize(writer, PercentageDescriptor, options);
			}
			else if (PercentageDescriptorAction is not null)
			{
				writer.WritePropertyName("percentage");
				JsonSerializer.Serialize(writer, new PercentageScoreHeuristicDescriptor(PercentageDescriptorAction), options);
			}
			else if (PercentageValue is not null)
			{
				writer.WritePropertyName("percentage");
				JsonSerializer.Serialize(writer, PercentageValue, options);
			}

			if (ScriptHeuristicDescriptor is not null)
			{
				writer.WritePropertyName("script_heuristic");
				JsonSerializer.Serialize(writer, ScriptHeuristicDescriptor, options);
			}
			else if (ScriptHeuristicDescriptorAction is not null)
			{
				writer.WritePropertyName("script_heuristic");
				JsonSerializer.Serialize(writer, new ScriptedHeuristicDescriptor(ScriptHeuristicDescriptorAction), options);
			}
			else if (ScriptHeuristicValue is not null)
			{
				writer.WritePropertyName("script_heuristic");
				JsonSerializer.Serialize(writer, ScriptHeuristicValue, options);
			}

			if (ShardMinDocCountValue.HasValue)
			{
				writer.WritePropertyName("shard_min_doc_count");
				writer.WriteNumberValue(ShardMinDocCountValue.Value);
			}

			if (ShardSizeValue.HasValue)
			{
				writer.WritePropertyName("shard_size");
				writer.WriteNumberValue(ShardSizeValue.Value);
			}

			if (SizeValue.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(SizeValue.Value);
			}

			if (SourceFieldsValue is not null)
			{
				writer.WritePropertyName("source_fields");
				JsonSerializer.Serialize(writer, SourceFieldsValue, options);
			}

			writer.WriteEndObject();
		}
	}
}