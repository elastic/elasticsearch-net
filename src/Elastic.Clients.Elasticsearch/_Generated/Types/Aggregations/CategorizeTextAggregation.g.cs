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

internal sealed partial class CategorizeTextAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropCategorizationAnalyzer = System.Text.Json.JsonEncodedText.Encode("categorization_analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropCategorizationFilters = System.Text.Json.JsonEncodedText.Encode("categorization_filters");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropMaxMatchedTokens = System.Text.Json.JsonEncodedText.Encode("max_matched_tokens");
	private static readonly System.Text.Json.JsonEncodedText PropMaxUniqueTokens = System.Text.Json.JsonEncodedText.Encode("max_unique_tokens");
	private static readonly System.Text.Json.JsonEncodedText PropMinDocCount = System.Text.Json.JsonEncodedText.Encode("min_doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropShardMinDocCount = System.Text.Json.JsonEncodedText.Encode("shard_min_doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropShardSize = System.Text.Json.JsonEncodedText.Encode("shard_size");
	private static readonly System.Text.Json.JsonEncodedText PropSimilarityThreshold = System.Text.Json.JsonEncodedText.Encode("similarity_threshold");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");

	public override Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer?> propCategorizationAnalyzer = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propCategorizationFilters = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<int?> propMaxMatchedTokens = default;
		LocalJsonValue<int?> propMaxUniqueTokens = default;
		LocalJsonValue<int?> propMinDocCount = default;
		LocalJsonValue<int?> propShardMinDocCount = default;
		LocalJsonValue<int?> propShardSize = default;
		LocalJsonValue<int?> propSimilarityThreshold = default;
		LocalJsonValue<int?> propSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCategorizationAnalyzer.TryReadProperty(ref reader, options, PropCategorizationAnalyzer, null))
			{
				continue;
			}

			if (propCategorizationFilters.TryReadProperty(ref reader, options, PropCategorizationFilters, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propMaxMatchedTokens.TryReadProperty(ref reader, options, PropMaxMatchedTokens, null))
			{
				continue;
			}

			if (propMaxUniqueTokens.TryReadProperty(ref reader, options, PropMaxUniqueTokens, null))
			{
				continue;
			}

			if (propMinDocCount.TryReadProperty(ref reader, options, PropMinDocCount, null))
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

			if (propSimilarityThreshold.TryReadProperty(ref reader, options, PropSimilarityThreshold, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CategorizationAnalyzer = propCategorizationAnalyzer.Value,
			CategorizationFilters = propCategorizationFilters.Value,
			Field = propField.Value,
			MaxMatchedTokens = propMaxMatchedTokens.Value,
			MaxUniqueTokens = propMaxUniqueTokens.Value,
			MinDocCount = propMinDocCount.Value,
			ShardMinDocCount = propShardMinDocCount.Value,
			ShardSize = propShardSize.Value,
			SimilarityThreshold = propSimilarityThreshold.Value,
			Size = propSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCategorizationAnalyzer, value.CategorizationAnalyzer, null, null);
		writer.WriteProperty(options, PropCategorizationFilters, value.CategorizationFilters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropMaxMatchedTokens, value.MaxMatchedTokens, null, null);
		writer.WriteProperty(options, PropMaxUniqueTokens, value.MaxUniqueTokens, null, null);
		writer.WriteProperty(options, PropMinDocCount, value.MinDocCount, null, null);
		writer.WriteProperty(options, PropShardMinDocCount, value.ShardMinDocCount, null, null);
		writer.WriteProperty(options, PropShardSize, value.ShardSize, null, null);
		writer.WriteProperty(options, PropSimilarityThreshold, value.SimilarityThreshold, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// A multi-bucket aggregation that groups semi-structured text into buckets. Each text
/// field is re-analyzed using a custom analyzer. The resulting tokens are then categorized
/// creating buckets of similarly formatted text values. This aggregation works best with machine
/// generated text like system logs. Only the first 100 analyzed tokens are used to categorize the text.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationConverter))]
public sealed partial class CategorizeTextAggregation
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public CategorizeTextAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CategorizeTextAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The categorization analyzer specifies how the text is analyzed and tokenized before being categorized.
	/// The syntax is very similar to that used to define the analyzer in the <a href="https://www.elastic.co/guide/en/elasticsearch/reference/8.0/indices-analyze.html">Analyze endpoint</a>. This property
	/// cannot be used at the same time as categorization_filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer? CategorizationAnalyzer { get; set; }

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? CategorizationFilters { get; set; }

	/// <summary>
	/// <para>
	/// The semi-structured text field to categorize.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of token positions to match on before attempting to merge categories. Larger
	/// values will use more memory and create narrower categories. Max allowed value is 100.
	/// </para>
	/// </summary>
	public int? MaxMatchedTokens { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of unique tokens at any position up to max_matched_tokens. Must be larger than 1.
	/// Smaller values use less memory and create fewer categories. Larger values will use more memory and
	/// create narrower categories. Max allowed value is 100.
	/// </para>
	/// </summary>
	public int? MaxUniqueTokens { get; set; }

	/// <summary>
	/// <para>
	/// The minimum number of documents in a bucket to be returned to the results.
	/// </para>
	/// </summary>
	public int? MinDocCount { get; set; }

	/// <summary>
	/// <para>
	/// The minimum number of documents in a bucket to be returned from the shard before merging.
	/// </para>
	/// </summary>
	public int? ShardMinDocCount { get; set; }

	/// <summary>
	/// <para>
	/// The number of categorization buckets to return from each shard before merging all the results.
	/// </para>
	/// </summary>
	public int? ShardSize { get; set; }

	/// <summary>
	/// <para>
	/// The minimum percentage of tokens that must match for text to be added to the category bucket. Must
	/// be between 1 and 100. The larger the value the narrower the categories. Larger values will increase memory
	/// usage and create narrower categories.
	/// </para>
	/// </summary>
	public int? SimilarityThreshold { get; set; }

	/// <summary>
	/// <para>
	/// The number of buckets to return.
	/// </para>
	/// </summary>
	public int? Size { get; set; }
}

/// <summary>
/// <para>
/// A multi-bucket aggregation that groups semi-structured text into buckets. Each text
/// field is re-analyzed using a custom analyzer. The resulting tokens are then categorized
/// creating buckets of similarly formatted text values. This aggregation works best with machine
/// generated text like system logs. Only the first 100 analyzed tokens are used to categorize the text.
/// </para>
/// </summary>
public readonly partial struct CategorizeTextAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CategorizeTextAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CategorizeTextAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The categorization analyzer specifies how the text is analyzed and tokenized before being categorized.
	/// The syntax is very similar to that used to define the analyzer in the <a href="https://www.elastic.co/guide/en/elasticsearch/reference/8.0/indices-analyze.html">Analyze endpoint</a>. This property
	/// cannot be used at the same time as categorization_filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> CategorizationAnalyzer(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer? value)
	{
		Instance.CategorizationAnalyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The categorization analyzer specifies how the text is analyzed and tokenized before being categorized.
	/// The syntax is very similar to that used to define the analyzer in the <a href="https://www.elastic.co/guide/en/elasticsearch/reference/8.0/indices-analyze.html">Analyze endpoint</a>. This property
	/// cannot be used at the same time as categorization_filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> CategorizationAnalyzer(System.Func<Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzerBuilder, Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer> action)
	{
		Instance.CategorizationAnalyzer = Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzerBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> CategorizationFilters(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.CategorizationFilters = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> CategorizationFilters()
	{
		Instance.CategorizationFilters = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> CategorizationFilters(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.CategorizationFilters = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> CategorizationFilters(params string[] values)
	{
		Instance.CategorizationFilters = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The semi-structured text field to categorize.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The semi-structured text field to categorize.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of token positions to match on before attempting to merge categories. Larger
	/// values will use more memory and create narrower categories. Max allowed value is 100.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> MaxMatchedTokens(int? value)
	{
		Instance.MaxMatchedTokens = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of unique tokens at any position up to max_matched_tokens. Must be larger than 1.
	/// Smaller values use less memory and create fewer categories. Larger values will use more memory and
	/// create narrower categories. Max allowed value is 100.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> MaxUniqueTokens(int? value)
	{
		Instance.MaxUniqueTokens = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum number of documents in a bucket to be returned to the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> MinDocCount(int? value)
	{
		Instance.MinDocCount = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum number of documents in a bucket to be returned from the shard before merging.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> ShardMinDocCount(int? value)
	{
		Instance.ShardMinDocCount = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of categorization buckets to return from each shard before merging all the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> ShardSize(int? value)
	{
		Instance.ShardSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum percentage of tokens that must match for text to be added to the category bucket. Must
	/// be between 1 and 100. The larger the value the narrower the categories. Larger values will increase memory
	/// usage and create narrower categories.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> SimilarityThreshold(int? value)
	{
		Instance.SimilarityThreshold = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of buckets to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

/// <summary>
/// <para>
/// A multi-bucket aggregation that groups semi-structured text into buckets. Each text
/// field is re-analyzed using a custom analyzer. The resulting tokens are then categorized
/// creating buckets of similarly formatted text values. This aggregation works best with machine
/// generated text like system logs. Only the first 100 analyzed tokens are used to categorize the text.
/// </para>
/// </summary>
public readonly partial struct CategorizeTextAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CategorizeTextAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CategorizeTextAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The categorization analyzer specifies how the text is analyzed and tokenized before being categorized.
	/// The syntax is very similar to that used to define the analyzer in the <a href="https://www.elastic.co/guide/en/elasticsearch/reference/8.0/indices-analyze.html">Analyze endpoint</a>. This property
	/// cannot be used at the same time as categorization_filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor CategorizationAnalyzer(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer? value)
	{
		Instance.CategorizationAnalyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The categorization analyzer specifies how the text is analyzed and tokenized before being categorized.
	/// The syntax is very similar to that used to define the analyzer in the <a href="https://www.elastic.co/guide/en/elasticsearch/reference/8.0/indices-analyze.html">Analyze endpoint</a>. This property
	/// cannot be used at the same time as categorization_filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor CategorizationAnalyzer(System.Func<Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzerBuilder, Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer> action)
	{
		Instance.CategorizationAnalyzer = Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzerBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor CategorizationFilters(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.CategorizationFilters = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor CategorizationFilters()
	{
		Instance.CategorizationFilters = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor CategorizationFilters(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.CategorizationFilters = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// This property expects an array of regular expressions. The expressions are used to filter out matching
	/// sequences from the categorization field values. You can use this functionality to fine tune the categorization
	/// by excluding sequences from consideration when categories are defined. For example, you can exclude SQL
	/// statements that appear in your log files. This property cannot be used at the same time as categorization_analyzer.
	/// If you only want to define simple regular expression filters that are applied prior to tokenization, setting
	/// this property is the easiest method. If you also want to customize the tokenizer or post-tokenization filtering,
	/// use the categorization_analyzer property instead and include the filters as pattern_replace character filters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor CategorizationFilters(params string[] values)
	{
		Instance.CategorizationFilters = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The semi-structured text field to categorize.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The semi-structured text field to categorize.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of token positions to match on before attempting to merge categories. Larger
	/// values will use more memory and create narrower categories. Max allowed value is 100.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor MaxMatchedTokens(int? value)
	{
		Instance.MaxMatchedTokens = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of unique tokens at any position up to max_matched_tokens. Must be larger than 1.
	/// Smaller values use less memory and create fewer categories. Larger values will use more memory and
	/// create narrower categories. Max allowed value is 100.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor MaxUniqueTokens(int? value)
	{
		Instance.MaxUniqueTokens = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum number of documents in a bucket to be returned to the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor MinDocCount(int? value)
	{
		Instance.MinDocCount = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum number of documents in a bucket to be returned from the shard before merging.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor ShardMinDocCount(int? value)
	{
		Instance.ShardMinDocCount = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of categorization buckets to return from each shard before merging all the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor ShardSize(int? value)
	{
		Instance.ShardSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum percentage of tokens that must match for text to be added to the category bucket. Must
	/// be between 1 and 100. The larger the value the narrower the categories. Larger values will increase memory
	/// usage and create narrower categories.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor SimilarityThreshold(int? value)
	{
		Instance.SimilarityThreshold = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of buckets to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}