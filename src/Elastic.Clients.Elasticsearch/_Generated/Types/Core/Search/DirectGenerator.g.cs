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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Search;

public sealed partial class DirectGenerator
{
	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// The maximum edit distance candidate suggestions can have in order to be considered as a suggestion.
	/// Can only be <c>1</c> or <c>2</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_edits")]
	public int? MaxEdits { get; set; }

	/// <summary>
	/// <para>
	/// A factor that is used to multiply with the shard_size in order to inspect more candidate spelling corrections on the shard level.
	/// Can improve accuracy at the cost of performance.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_inspections")]
	public float? MaxInspections { get; set; }

	/// <summary>
	/// <para>
	/// The maximum threshold in number of documents in which a suggest text token can exist in order to be included.
	/// This can be used to exclude high frequency terms — which are usually spelled correctly — from being spellchecked.
	/// Can be a relative percentage number (for example <c>0.4</c>) or an absolute number to represent document frequencies.
	/// If a value higher than 1 is specified, then fractional can not be specified.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_term_freq")]
	public float? MaxTermFreq { get; set; }

	/// <summary>
	/// <para>
	/// The minimal threshold in number of documents a suggestion should appear in.
	/// This can improve quality by only suggesting high frequency terms.
	/// Can be specified as an absolute number or as a relative percentage of number of documents.
	/// If a value higher than 1 is specified, the number cannot be fractional.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("min_doc_freq")]
	public float? MinDocFreq { get; set; }

	/// <summary>
	/// <para>
	/// The minimum length a suggest text term must have in order to be included.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("min_word_length")]
	public int? MinWordLength { get; set; }

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the generated tokens before they are passed to the actual phrase scorer.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("post_filter")]
	public string? PostFilter { get; set; }

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the tokens passed to this candidate generator.
	/// This filter is applied to the original token before candidates are generated.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pre_filter")]
	public string? PreFilter { get; set; }

	/// <summary>
	/// <para>
	/// The number of minimal prefix characters that must match in order be a candidate suggestions.
	/// Increasing this number improves spellcheck performance.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prefix_length")]
	public int? PrefixLength { get; set; }

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// Controls what suggestions are included on the suggestions generated on each shard.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("suggest_mode")]
	public Elastic.Clients.Elasticsearch.SuggestMode? SuggestMode { get; set; }
}

public sealed partial class DirectGeneratorDescriptor<TDocument> : SerializableDescriptor<DirectGeneratorDescriptor<TDocument>>
{
	internal DirectGeneratorDescriptor(Action<DirectGeneratorDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DirectGeneratorDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private int? MaxEditsValue { get; set; }
	private float? MaxInspectionsValue { get; set; }
	private float? MaxTermFreqValue { get; set; }
	private float? MinDocFreqValue { get; set; }
	private int? MinWordLengthValue { get; set; }
	private string? PostFilterValue { get; set; }
	private string? PreFilterValue { get; set; }
	private int? PrefixLengthValue { get; set; }
	private int? SizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.SuggestMode? SuggestModeValue { get; set; }

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum edit distance candidate suggestions can have in order to be considered as a suggestion.
	/// Can only be <c>1</c> or <c>2</c>.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> MaxEdits(int? maxEdits)
	{
		MaxEditsValue = maxEdits;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A factor that is used to multiply with the shard_size in order to inspect more candidate spelling corrections on the shard level.
	/// Can improve accuracy at the cost of performance.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> MaxInspections(float? maxInspections)
	{
		MaxInspectionsValue = maxInspections;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum threshold in number of documents in which a suggest text token can exist in order to be included.
	/// This can be used to exclude high frequency terms — which are usually spelled correctly — from being spellchecked.
	/// Can be a relative percentage number (for example <c>0.4</c>) or an absolute number to represent document frequencies.
	/// If a value higher than 1 is specified, then fractional can not be specified.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> MaxTermFreq(float? maxTermFreq)
	{
		MaxTermFreqValue = maxTermFreq;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The minimal threshold in number of documents a suggestion should appear in.
	/// This can improve quality by only suggesting high frequency terms.
	/// Can be specified as an absolute number or as a relative percentage of number of documents.
	/// If a value higher than 1 is specified, the number cannot be fractional.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> MinDocFreq(float? minDocFreq)
	{
		MinDocFreqValue = minDocFreq;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The minimum length a suggest text term must have in order to be included.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> MinWordLength(int? minWordLength)
	{
		MinWordLengthValue = minWordLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the generated tokens before they are passed to the actual phrase scorer.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> PostFilter(string? postFilter)
	{
		PostFilterValue = postFilter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the tokens passed to this candidate generator.
	/// This filter is applied to the original token before candidates are generated.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> PreFilter(string? preFilter)
	{
		PreFilterValue = preFilter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of minimal prefix characters that must match in order be a candidate suggestions.
	/// Increasing this number improves spellcheck performance.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> PrefixLength(int? prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Controls what suggestions are included on the suggestions generated on each shard.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor<TDocument> SuggestMode(Elastic.Clients.Elasticsearch.SuggestMode? suggestMode)
	{
		SuggestModeValue = suggestMode;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (MaxEditsValue.HasValue)
		{
			writer.WritePropertyName("max_edits");
			writer.WriteNumberValue(MaxEditsValue.Value);
		}

		if (MaxInspectionsValue.HasValue)
		{
			writer.WritePropertyName("max_inspections");
			writer.WriteNumberValue(MaxInspectionsValue.Value);
		}

		if (MaxTermFreqValue.HasValue)
		{
			writer.WritePropertyName("max_term_freq");
			writer.WriteNumberValue(MaxTermFreqValue.Value);
		}

		if (MinDocFreqValue.HasValue)
		{
			writer.WritePropertyName("min_doc_freq");
			writer.WriteNumberValue(MinDocFreqValue.Value);
		}

		if (MinWordLengthValue.HasValue)
		{
			writer.WritePropertyName("min_word_length");
			writer.WriteNumberValue(MinWordLengthValue.Value);
		}

		if (!string.IsNullOrEmpty(PostFilterValue))
		{
			writer.WritePropertyName("post_filter");
			writer.WriteStringValue(PostFilterValue);
		}

		if (!string.IsNullOrEmpty(PreFilterValue))
		{
			writer.WritePropertyName("pre_filter");
			writer.WriteStringValue(PreFilterValue);
		}

		if (PrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("prefix_length");
			writer.WriteNumberValue(PrefixLengthValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (SuggestModeValue is not null)
		{
			writer.WritePropertyName("suggest_mode");
			JsonSerializer.Serialize(writer, SuggestModeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class DirectGeneratorDescriptor : SerializableDescriptor<DirectGeneratorDescriptor>
{
	internal DirectGeneratorDescriptor(Action<DirectGeneratorDescriptor> configure) => configure.Invoke(this);

	public DirectGeneratorDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private int? MaxEditsValue { get; set; }
	private float? MaxInspectionsValue { get; set; }
	private float? MaxTermFreqValue { get; set; }
	private float? MinDocFreqValue { get; set; }
	private int? MinWordLengthValue { get; set; }
	private string? PostFilterValue { get; set; }
	private string? PreFilterValue { get; set; }
	private int? PrefixLengthValue { get; set; }
	private int? SizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.SuggestMode? SuggestModeValue { get; set; }

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum edit distance candidate suggestions can have in order to be considered as a suggestion.
	/// Can only be <c>1</c> or <c>2</c>.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor MaxEdits(int? maxEdits)
	{
		MaxEditsValue = maxEdits;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A factor that is used to multiply with the shard_size in order to inspect more candidate spelling corrections on the shard level.
	/// Can improve accuracy at the cost of performance.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor MaxInspections(float? maxInspections)
	{
		MaxInspectionsValue = maxInspections;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum threshold in number of documents in which a suggest text token can exist in order to be included.
	/// This can be used to exclude high frequency terms — which are usually spelled correctly — from being spellchecked.
	/// Can be a relative percentage number (for example <c>0.4</c>) or an absolute number to represent document frequencies.
	/// If a value higher than 1 is specified, then fractional can not be specified.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor MaxTermFreq(float? maxTermFreq)
	{
		MaxTermFreqValue = maxTermFreq;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The minimal threshold in number of documents a suggestion should appear in.
	/// This can improve quality by only suggesting high frequency terms.
	/// Can be specified as an absolute number or as a relative percentage of number of documents.
	/// If a value higher than 1 is specified, the number cannot be fractional.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor MinDocFreq(float? minDocFreq)
	{
		MinDocFreqValue = minDocFreq;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The minimum length a suggest text term must have in order to be included.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor MinWordLength(int? minWordLength)
	{
		MinWordLengthValue = minWordLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the generated tokens before they are passed to the actual phrase scorer.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor PostFilter(string? postFilter)
	{
		PostFilterValue = postFilter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the tokens passed to this candidate generator.
	/// This filter is applied to the original token before candidates are generated.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor PreFilter(string? preFilter)
	{
		PreFilterValue = preFilter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of minimal prefix characters that must match in order be a candidate suggestions.
	/// Increasing this number improves spellcheck performance.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor PrefixLength(int? prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Controls what suggestions are included on the suggestions generated on each shard.
	/// </para>
	/// </summary>
	public DirectGeneratorDescriptor SuggestMode(Elastic.Clients.Elasticsearch.SuggestMode? suggestMode)
	{
		SuggestModeValue = suggestMode;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (MaxEditsValue.HasValue)
		{
			writer.WritePropertyName("max_edits");
			writer.WriteNumberValue(MaxEditsValue.Value);
		}

		if (MaxInspectionsValue.HasValue)
		{
			writer.WritePropertyName("max_inspections");
			writer.WriteNumberValue(MaxInspectionsValue.Value);
		}

		if (MaxTermFreqValue.HasValue)
		{
			writer.WritePropertyName("max_term_freq");
			writer.WriteNumberValue(MaxTermFreqValue.Value);
		}

		if (MinDocFreqValue.HasValue)
		{
			writer.WritePropertyName("min_doc_freq");
			writer.WriteNumberValue(MinDocFreqValue.Value);
		}

		if (MinWordLengthValue.HasValue)
		{
			writer.WritePropertyName("min_word_length");
			writer.WriteNumberValue(MinWordLengthValue.Value);
		}

		if (!string.IsNullOrEmpty(PostFilterValue))
		{
			writer.WritePropertyName("post_filter");
			writer.WriteStringValue(PostFilterValue);
		}

		if (!string.IsNullOrEmpty(PreFilterValue))
		{
			writer.WritePropertyName("pre_filter");
			writer.WriteStringValue(PreFilterValue);
		}

		if (PrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("prefix_length");
			writer.WriteNumberValue(PrefixLengthValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (SuggestModeValue is not null)
		{
			writer.WritePropertyName("suggest_mode");
			JsonSerializer.Serialize(writer, SuggestModeValue, options);
		}

		writer.WriteEndObject();
	}
}