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

internal sealed partial class SignificantTermsAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropBackgroundFilter = System.Text.Json.JsonEncodedText.Encode("background_filter");
	private static readonly System.Text.Json.JsonEncodedText PropChiSquare = System.Text.Json.JsonEncodedText.Encode("chi_square");
	private static readonly System.Text.Json.JsonEncodedText PropExclude = System.Text.Json.JsonEncodedText.Encode("exclude");
	private static readonly System.Text.Json.JsonEncodedText PropExecutionHint = System.Text.Json.JsonEncodedText.Encode("execution_hint");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropGnd = System.Text.Json.JsonEncodedText.Encode("gnd");
	private static readonly System.Text.Json.JsonEncodedText PropInclude = System.Text.Json.JsonEncodedText.Encode("include");
	private static readonly System.Text.Json.JsonEncodedText PropJlh = System.Text.Json.JsonEncodedText.Encode("jlh");
	private static readonly System.Text.Json.JsonEncodedText PropMinDocCount = System.Text.Json.JsonEncodedText.Encode("min_doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropMutualInformation = System.Text.Json.JsonEncodedText.Encode("mutual_information");
	private static readonly System.Text.Json.JsonEncodedText PropPercentage = System.Text.Json.JsonEncodedText.Encode("percentage");
	private static readonly System.Text.Json.JsonEncodedText PropScriptHeuristic = System.Text.Json.JsonEncodedText.Encode("script_heuristic");
	private static readonly System.Text.Json.JsonEncodedText PropShardMinDocCount = System.Text.Json.JsonEncodedText.Encode("shard_min_doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropShardSize = System.Text.Json.JsonEncodedText.Encode("shard_size");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");

	public override Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propBackgroundFilter = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristic?> propChiSquare = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.TermsExclude?> propExclude = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint?> propExecutionHint = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristic?> propGnd = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.TermsInclude?> propInclude = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.EmptyObject?> propJlh = default;
		LocalJsonValue<long?> propMinDocCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristic?> propMutualInformation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristic?> propPercentage = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristic?> propScriptHeuristic = default;
		LocalJsonValue<long?> propShardMinDocCount = default;
		LocalJsonValue<int?> propShardSize = default;
		LocalJsonValue<int?> propSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBackgroundFilter.TryReadProperty(ref reader, options, PropBackgroundFilter, null))
			{
				continue;
			}

			if (propChiSquare.TryReadProperty(ref reader, options, PropChiSquare, null))
			{
				continue;
			}

			if (propExclude.TryReadProperty(ref reader, options, PropExclude, null))
			{
				continue;
			}

			if (propExecutionHint.TryReadProperty(ref reader, options, PropExecutionHint, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propGnd.TryReadProperty(ref reader, options, PropGnd, null))
			{
				continue;
			}

			if (propInclude.TryReadProperty(ref reader, options, PropInclude, null))
			{
				continue;
			}

			if (propJlh.TryReadProperty(ref reader, options, PropJlh, null))
			{
				continue;
			}

			if (propMinDocCount.TryReadProperty(ref reader, options, PropMinDocCount, null))
			{
				continue;
			}

			if (propMutualInformation.TryReadProperty(ref reader, options, PropMutualInformation, null))
			{
				continue;
			}

			if (propPercentage.TryReadProperty(ref reader, options, PropPercentage, null))
			{
				continue;
			}

			if (propScriptHeuristic.TryReadProperty(ref reader, options, PropScriptHeuristic, null))
			{
				continue;
			}

			if (propShardMinDocCount.TryReadProperty(ref reader, options, PropShardMinDocCount, null))
			{
				continue;
			}

			if (propShardSize.TryReadProperty(ref reader, options, PropShardSize, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BackgroundFilter = propBackgroundFilter.Value,
			ChiSquare = propChiSquare.Value,
			Exclude = propExclude.Value,
			ExecutionHint = propExecutionHint.Value,
			Field = propField.Value,
			Gnd = propGnd.Value,
			Include = propInclude.Value,
			Jlh = propJlh.Value,
			MinDocCount = propMinDocCount.Value,
			MutualInformation = propMutualInformation.Value,
			Percentage = propPercentage.Value,
			ScriptHeuristic = propScriptHeuristic.Value,
			ShardMinDocCount = propShardMinDocCount.Value,
			ShardSize = propShardSize.Value,
			Size = propSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBackgroundFilter, value.BackgroundFilter, null, null);
		writer.WriteProperty(options, PropChiSquare, value.ChiSquare, null, null);
		writer.WriteProperty(options, PropExclude, value.Exclude, null, null);
		writer.WriteProperty(options, PropExecutionHint, value.ExecutionHint, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropGnd, value.Gnd, null, null);
		writer.WriteProperty(options, PropInclude, value.Include, null, null);
		writer.WriteProperty(options, PropJlh, value.Jlh, null, null);
		writer.WriteProperty(options, PropMinDocCount, value.MinDocCount, null, null);
		writer.WriteProperty(options, PropMutualInformation, value.MutualInformation, null, null);
		writer.WriteProperty(options, PropPercentage, value.Percentage, null, null);
		writer.WriteProperty(options, PropScriptHeuristic, value.ScriptHeuristic, null, null);
		writer.WriteProperty(options, PropShardMinDocCount, value.ShardMinDocCount, null, null);
		writer.WriteProperty(options, PropShardSize, value.ShardSize, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationConverter))]
public sealed partial class SignificantTermsAggregation
{
#if NET7_0_OR_GREATER
	public SignificantTermsAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public SignificantTermsAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A background filter that can be used to focus in on significant terms within a narrower context, instead of the entire index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? BackgroundFilter { get; set; }

	/// <summary>
	/// <para>
	/// Use Chi square, as described in "Information Retrieval", Manning et al., Chapter 13.5.2, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristic? ChiSquare { get; set; }

	/// <summary>
	/// <para>
	/// Terms to exclude.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? Exclude { get; set; }

	/// <summary>
	/// <para>
	/// Mechanism by which the aggregation should be executed: using field values directly or using global ordinals.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? ExecutionHint { get; set; }

	/// <summary>
	/// <para>
	/// The field from which to return significant terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	/// <summary>
	/// <para>
	/// Use Google normalized distance as described in "The Google Similarity Distance", Cilibrasi and Vitanyi, 2007, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristic? Gnd { get; set; }

	/// <summary>
	/// <para>
	/// Terms to include.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? Include { get; set; }

	/// <summary>
	/// <para>
	/// Use JLH score as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.EmptyObject? Jlh { get; set; }

	/// <summary>
	/// <para>
	/// Only return terms that are found in more than <c>min_doc_count</c> hits.
	/// </para>
	/// </summary>
	public long? MinDocCount { get; set; }

	/// <summary>
	/// <para>
	/// Use mutual information as described in "Information Retrieval", Manning et al., Chapter 13.5.1, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristic? MutualInformation { get; set; }

	/// <summary>
	/// <para>
	/// A simple calculation of the number of documents in the foreground sample with a term divided by the number of documents in the background with the term.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristic? Percentage { get; set; }

	/// <summary>
	/// <para>
	/// Customized score, implemented via a script.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristic? ScriptHeuristic { get; set; }

	/// <summary>
	/// <para>
	/// Regulates the certainty a shard has if the term should actually be added to the candidate list or not with respect to the <c>min_doc_count</c>.
	/// Terms will only be considered if their local shard frequency within the set is higher than the <c>shard_min_doc_count</c>.
	/// </para>
	/// </summary>
	public long? ShardMinDocCount { get; set; }

	/// <summary>
	/// <para>
	/// Can be used to control the volumes of candidate terms produced by each shard.
	/// By default, <c>shard_size</c> will be automatically estimated based on the number of shards and the <c>size</c> parameter.
	/// </para>
	/// </summary>
	public int? ShardSize { get; set; }

	/// <summary>
	/// <para>
	/// The number of buckets returned out of the overall terms list.
	/// </para>
	/// </summary>
	public int? Size { get; set; }
}

public readonly partial struct SignificantTermsAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SignificantTermsAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SignificantTermsAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A background filter that can be used to focus in on significant terms within a narrower context, instead of the entire index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> BackgroundFilter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.BackgroundFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A background filter that can be used to focus in on significant terms within a narrower context, instead of the entire index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> BackgroundFilter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.BackgroundFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Chi square, as described in "Information Retrieval", Manning et al., Chapter 13.5.2, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> ChiSquare(Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristic? value)
	{
		Instance.ChiSquare = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Chi square, as described in "Information Retrieval", Manning et al., Chapter 13.5.2, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> ChiSquare(System.Action<Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristicDescriptor> action)
	{
		Instance.ChiSquare = Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Terms to exclude.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Exclude(Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? value)
	{
		Instance.Exclude = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Mechanism by which the aggregation should be executed: using field values directly or using global ordinals.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> ExecutionHint(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? value)
	{
		Instance.ExecutionHint = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field from which to return significant terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field from which to return significant terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Google normalized distance as described in "The Google Similarity Distance", Cilibrasi and Vitanyi, 2007, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Gnd(Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristic? value)
	{
		Instance.Gnd = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Google normalized distance as described in "The Google Similarity Distance", Cilibrasi and Vitanyi, 2007, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Gnd()
	{
		Instance.Gnd = Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristicDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Google normalized distance as described in "The Google Similarity Distance", Cilibrasi and Vitanyi, 2007, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Gnd(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristicDescriptor>? action)
	{
		Instance.Gnd = Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Terms to include.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Include(Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? value)
	{
		Instance.Include = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use JLH score as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Jlh(Elastic.Clients.Elasticsearch.EmptyObject? value)
	{
		Instance.Jlh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use JLH score as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Jlh()
	{
		Instance.Jlh = Elastic.Clients.Elasticsearch.EmptyObjectDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Use JLH score as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Jlh(System.Action<Elastic.Clients.Elasticsearch.EmptyObjectDescriptor>? action)
	{
		Instance.Jlh = Elastic.Clients.Elasticsearch.EmptyObjectDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Only return terms that are found in more than <c>min_doc_count</c> hits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> MinDocCount(long? value)
	{
		Instance.MinDocCount = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use mutual information as described in "Information Retrieval", Manning et al., Chapter 13.5.1, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> MutualInformation(Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristic? value)
	{
		Instance.MutualInformation = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use mutual information as described in "Information Retrieval", Manning et al., Chapter 13.5.1, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> MutualInformation()
	{
		Instance.MutualInformation = Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristicDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Use mutual information as described in "Information Retrieval", Manning et al., Chapter 13.5.1, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> MutualInformation(System.Action<Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristicDescriptor>? action)
	{
		Instance.MutualInformation = Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A simple calculation of the number of documents in the foreground sample with a term divided by the number of documents in the background with the term.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Percentage(Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristic? value)
	{
		Instance.Percentage = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A simple calculation of the number of documents in the foreground sample with a term divided by the number of documents in the background with the term.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Percentage()
	{
		Instance.Percentage = Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristicDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A simple calculation of the number of documents in the foreground sample with a term divided by the number of documents in the background with the term.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Percentage(System.Action<Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristicDescriptor>? action)
	{
		Instance.Percentage = Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Customized score, implemented via a script.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> ScriptHeuristic(Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristic? value)
	{
		Instance.ScriptHeuristic = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Customized score, implemented via a script.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> ScriptHeuristic(System.Action<Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristicDescriptor> action)
	{
		Instance.ScriptHeuristic = Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Regulates the certainty a shard has if the term should actually be added to the candidate list or not with respect to the <c>min_doc_count</c>.
	/// Terms will only be considered if their local shard frequency within the set is higher than the <c>shard_min_doc_count</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> ShardMinDocCount(long? value)
	{
		Instance.ShardMinDocCount = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be used to control the volumes of candidate terms produced by each shard.
	/// By default, <c>shard_size</c> will be automatically estimated based on the number of shards and the <c>size</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> ShardSize(int? value)
	{
		Instance.ShardSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of buckets returned out of the overall terms list.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct SignificantTermsAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SignificantTermsAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SignificantTermsAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A background filter that can be used to focus in on significant terms within a narrower context, instead of the entire index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor BackgroundFilter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.BackgroundFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A background filter that can be used to focus in on significant terms within a narrower context, instead of the entire index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor BackgroundFilter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.BackgroundFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A background filter that can be used to focus in on significant terms within a narrower context, instead of the entire index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor BackgroundFilter<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.BackgroundFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Chi square, as described in "Information Retrieval", Manning et al., Chapter 13.5.2, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor ChiSquare(Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristic? value)
	{
		Instance.ChiSquare = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Chi square, as described in "Information Retrieval", Manning et al., Chapter 13.5.2, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor ChiSquare(System.Action<Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristicDescriptor> action)
	{
		Instance.ChiSquare = Elastic.Clients.Elasticsearch.Aggregations.ChiSquareHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Terms to exclude.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Exclude(Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? value)
	{
		Instance.Exclude = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Mechanism by which the aggregation should be executed: using field values directly or using global ordinals.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor ExecutionHint(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? value)
	{
		Instance.ExecutionHint = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field from which to return significant terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field from which to return significant terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Google normalized distance as described in "The Google Similarity Distance", Cilibrasi and Vitanyi, 2007, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Gnd(Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristic? value)
	{
		Instance.Gnd = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Google normalized distance as described in "The Google Similarity Distance", Cilibrasi and Vitanyi, 2007, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Gnd()
	{
		Instance.Gnd = Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristicDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Use Google normalized distance as described in "The Google Similarity Distance", Cilibrasi and Vitanyi, 2007, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Gnd(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristicDescriptor>? action)
	{
		Instance.Gnd = Elastic.Clients.Elasticsearch.Aggregations.GoogleNormalizedDistanceHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Terms to include.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Include(Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? value)
	{
		Instance.Include = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use JLH score as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Jlh(Elastic.Clients.Elasticsearch.EmptyObject? value)
	{
		Instance.Jlh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use JLH score as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Jlh()
	{
		Instance.Jlh = Elastic.Clients.Elasticsearch.EmptyObjectDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Use JLH score as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Jlh(System.Action<Elastic.Clients.Elasticsearch.EmptyObjectDescriptor>? action)
	{
		Instance.Jlh = Elastic.Clients.Elasticsearch.EmptyObjectDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Only return terms that are found in more than <c>min_doc_count</c> hits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor MinDocCount(long? value)
	{
		Instance.MinDocCount = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use mutual information as described in "Information Retrieval", Manning et al., Chapter 13.5.1, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor MutualInformation(Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristic? value)
	{
		Instance.MutualInformation = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Use mutual information as described in "Information Retrieval", Manning et al., Chapter 13.5.1, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor MutualInformation()
	{
		Instance.MutualInformation = Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristicDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Use mutual information as described in "Information Retrieval", Manning et al., Chapter 13.5.1, as the significance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor MutualInformation(System.Action<Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristicDescriptor>? action)
	{
		Instance.MutualInformation = Elastic.Clients.Elasticsearch.Aggregations.MutualInformationHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A simple calculation of the number of documents in the foreground sample with a term divided by the number of documents in the background with the term.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Percentage(Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristic? value)
	{
		Instance.Percentage = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A simple calculation of the number of documents in the foreground sample with a term divided by the number of documents in the background with the term.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Percentage()
	{
		Instance.Percentage = Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristicDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A simple calculation of the number of documents in the foreground sample with a term divided by the number of documents in the background with the term.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Percentage(System.Action<Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristicDescriptor>? action)
	{
		Instance.Percentage = Elastic.Clients.Elasticsearch.Aggregations.PercentageScoreHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Customized score, implemented via a script.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor ScriptHeuristic(Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristic? value)
	{
		Instance.ScriptHeuristic = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Customized score, implemented via a script.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor ScriptHeuristic(System.Action<Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristicDescriptor> action)
	{
		Instance.ScriptHeuristic = Elastic.Clients.Elasticsearch.Aggregations.ScriptedHeuristicDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Regulates the certainty a shard has if the term should actually be added to the candidate list or not with respect to the <c>min_doc_count</c>.
	/// Terms will only be considered if their local shard frequency within the set is higher than the <c>shard_min_doc_count</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor ShardMinDocCount(long? value)
	{
		Instance.ShardMinDocCount = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be used to control the volumes of candidate terms produced by each shard.
	/// By default, <c>shard_size</c> will be automatically estimated based on the number of shards and the <c>size</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor ShardSize(int? value)
	{
		Instance.ShardSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of buckets returned out of the overall terms list.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.SignificantTermsAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}