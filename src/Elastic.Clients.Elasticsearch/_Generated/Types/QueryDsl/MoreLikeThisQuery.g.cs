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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class MoreLikeThisQuery : SearchQuery
{
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }
	[JsonInclude, JsonPropertyName("analyzer")]
	public string? Analyzer { get; set; }
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }
	[JsonInclude, JsonPropertyName("boost_terms")]
	public double? BoostTerms { get; set; }
	[JsonInclude, JsonPropertyName("fail_on_unsupported_field")]
	public bool? FailOnUnsupportedField { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public Fields? Fields { get; set; }
	[JsonInclude, JsonPropertyName("include")]
	public bool? Include { get; set; }
	[JsonInclude, JsonPropertyName("like"), SingleOrManyCollectionConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.Like))]
	public ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like> Like { get; set; }
	[JsonInclude, JsonPropertyName("max_doc_freq")]
	public int? MaxDocFreq { get; set; }
	[JsonInclude, JsonPropertyName("max_query_terms")]
	public int? MaxQueryTerms { get; set; }
	[JsonInclude, JsonPropertyName("max_word_length")]
	public int? MaxWordLength { get; set; }
	[JsonInclude, JsonPropertyName("min_doc_freq")]
	public int? MinDocFreq { get; set; }
	[JsonInclude, JsonPropertyName("min_term_freq")]
	public int? MinTermFreq { get; set; }
	[JsonInclude, JsonPropertyName("min_word_length")]
	public int? MinWordLength { get; set; }
	[JsonInclude, JsonPropertyName("minimum_should_match")]
	public Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatch { get; set; }
	[JsonInclude, JsonPropertyName("per_field_analyzer")]
	public IDictionary<Elastic.Clients.Elasticsearch.Field, string>? PerFieldAnalyzer { get; set; }
	[JsonInclude, JsonPropertyName("routing")]
	public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }
	[JsonInclude, JsonPropertyName("stop_words")]
	[JsonConverter(typeof(StopWordsConverter))]
	public ICollection<string>? StopWords { get; set; }
	[JsonInclude, JsonPropertyName("unlike"), SingleOrManyCollectionConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.Like))]
	public ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like>? Unlike { get; set; }
	[JsonInclude, JsonPropertyName("version")]
	public long? Version { get; set; }
	[JsonInclude, JsonPropertyName("version_type")]
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get; set; }

	public static implicit operator Query(MoreLikeThisQuery moreLikeThisQuery) => QueryDsl.Query.MoreLikeThis(moreLikeThisQuery);
}

public sealed partial class MoreLikeThisQueryDescriptor<TDocument> : SerializableDescriptor<MoreLikeThisQueryDescriptor<TDocument>>
{
	internal MoreLikeThisQueryDescriptor(Action<MoreLikeThisQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public MoreLikeThisQueryDescriptor() : base()
	{
	}

	private string? QueryNameValue { get; set; }
	private string? AnalyzerValue { get; set; }
	private float? BoostValue { get; set; }
	private double? BoostTermsValue { get; set; }
	private bool? FailOnUnsupportedFieldValue { get; set; }
	private Fields? FieldsValue { get; set; }
	private bool? IncludeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like> LikeValue { get; set; }
	private int? MaxDocFreqValue { get; set; }
	private int? MaxQueryTermsValue { get; set; }
	private int? MaxWordLengthValue { get; set; }
	private int? MinDocFreqValue { get; set; }
	private int? MinTermFreqValue { get; set; }
	private int? MinWordLengthValue { get; set; }
	private Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, string>? PerFieldAnalyzerValue { get; set; }
	private Elastic.Clients.Elasticsearch.Routing? RoutingValue { get; set; }
	private ICollection<string>? StopWordsValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like>? UnlikeValue { get; set; }
	private long? VersionValue { get; set; }
	private Elastic.Clients.Elasticsearch.VersionType? VersionTypeValue { get; set; }

	public MoreLikeThisQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> BoostTerms(double? boostTerms)
	{
		BoostTermsValue = boostTerms;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> FailOnUnsupportedField(bool? failOnUnsupportedField = true)
	{
		FailOnUnsupportedFieldValue = failOnUnsupportedField;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> Fields(Fields? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> Include(bool? include = true)
	{
		IncludeValue = include;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> Like(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like> like)
	{
		LikeValue = like;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> MaxDocFreq(int? maxDocFreq)
	{
		MaxDocFreqValue = maxDocFreq;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> MaxQueryTerms(int? maxQueryTerms)
	{
		MaxQueryTermsValue = maxQueryTerms;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> MaxWordLength(int? maxWordLength)
	{
		MaxWordLengthValue = maxWordLength;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> MinDocFreq(int? minDocFreq)
	{
		MinDocFreqValue = minDocFreq;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> MinTermFreq(int? minTermFreq)
	{
		MinTermFreqValue = minTermFreq;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> MinWordLength(int? minWordLength)
	{
		MinWordLengthValue = minWordLength;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> PerFieldAnalyzer(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>> selector)
	{
		PerFieldAnalyzerValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>());
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing)
	{
		RoutingValue = routing;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> StopWords(ICollection<string>? stopWords)
	{
		StopWordsValue = stopWords;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> Unlike(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like>? unlike)
	{
		UnlikeValue = unlike;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> Version(long? version)
	{
		VersionValue = version;
		return Self;
	}

	public MoreLikeThisQueryDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? versionType)
	{
		VersionTypeValue = versionType;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (BoostTermsValue.HasValue)
		{
			writer.WritePropertyName("boost_terms");
			writer.WriteNumberValue(BoostTermsValue.Value);
		}

		if (FailOnUnsupportedFieldValue.HasValue)
		{
			writer.WritePropertyName("fail_on_unsupported_field");
			writer.WriteBooleanValue(FailOnUnsupportedFieldValue.Value);
		}

		if (FieldsValue is not null)
		{
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
		}

		if (IncludeValue.HasValue)
		{
			writer.WritePropertyName("include");
			writer.WriteBooleanValue(IncludeValue.Value);
		}

		writer.WritePropertyName("like");
		SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.Like>(LikeValue, writer, options);
		if (MaxDocFreqValue.HasValue)
		{
			writer.WritePropertyName("max_doc_freq");
			writer.WriteNumberValue(MaxDocFreqValue.Value);
		}

		if (MaxQueryTermsValue.HasValue)
		{
			writer.WritePropertyName("max_query_terms");
			writer.WriteNumberValue(MaxQueryTermsValue.Value);
		}

		if (MaxWordLengthValue.HasValue)
		{
			writer.WritePropertyName("max_word_length");
			writer.WriteNumberValue(MaxWordLengthValue.Value);
		}

		if (MinDocFreqValue.HasValue)
		{
			writer.WritePropertyName("min_doc_freq");
			writer.WriteNumberValue(MinDocFreqValue.Value);
		}

		if (MinTermFreqValue.HasValue)
		{
			writer.WritePropertyName("min_term_freq");
			writer.WriteNumberValue(MinTermFreqValue.Value);
		}

		if (MinWordLengthValue.HasValue)
		{
			writer.WritePropertyName("min_word_length");
			writer.WriteNumberValue(MinWordLengthValue.Value);
		}

		if (MinimumShouldMatchValue is not null)
		{
			writer.WritePropertyName("minimum_should_match");
			JsonSerializer.Serialize(writer, MinimumShouldMatchValue, options);
		}

		if (PerFieldAnalyzerValue is not null)
		{
			writer.WritePropertyName("per_field_analyzer");
			JsonSerializer.Serialize(writer, PerFieldAnalyzerValue, options);
		}

		if (RoutingValue is not null)
		{
			writer.WritePropertyName("routing");
			JsonSerializer.Serialize(writer, RoutingValue, options);
		}

		if (StopWordsValue is not null)
		{
			writer.WritePropertyName("stop_words");
			SingleOrManySerializationHelper.Serialize<string>(StopWordsValue, writer, options);
		}

		if (UnlikeValue is not null)
		{
			writer.WritePropertyName("unlike");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.Like>(UnlikeValue, writer, options);
		}

		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		if (VersionTypeValue is not null)
		{
			writer.WritePropertyName("version_type");
			JsonSerializer.Serialize(writer, VersionTypeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class MoreLikeThisQueryDescriptor : SerializableDescriptor<MoreLikeThisQueryDescriptor>
{
	internal MoreLikeThisQueryDescriptor(Action<MoreLikeThisQueryDescriptor> configure) => configure.Invoke(this);

	public MoreLikeThisQueryDescriptor() : base()
	{
	}

	private string? QueryNameValue { get; set; }
	private string? AnalyzerValue { get; set; }
	private float? BoostValue { get; set; }
	private double? BoostTermsValue { get; set; }
	private bool? FailOnUnsupportedFieldValue { get; set; }
	private Fields? FieldsValue { get; set; }
	private bool? IncludeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like> LikeValue { get; set; }
	private int? MaxDocFreqValue { get; set; }
	private int? MaxQueryTermsValue { get; set; }
	private int? MaxWordLengthValue { get; set; }
	private int? MinDocFreqValue { get; set; }
	private int? MinTermFreqValue { get; set; }
	private int? MinWordLengthValue { get; set; }
	private Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, string>? PerFieldAnalyzerValue { get; set; }
	private Elastic.Clients.Elasticsearch.Routing? RoutingValue { get; set; }
	private ICollection<string>? StopWordsValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like>? UnlikeValue { get; set; }
	private long? VersionValue { get; set; }
	private Elastic.Clients.Elasticsearch.VersionType? VersionTypeValue { get; set; }

	public MoreLikeThisQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public MoreLikeThisQueryDescriptor Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	public MoreLikeThisQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public MoreLikeThisQueryDescriptor BoostTerms(double? boostTerms)
	{
		BoostTermsValue = boostTerms;
		return Self;
	}

	public MoreLikeThisQueryDescriptor FailOnUnsupportedField(bool? failOnUnsupportedField = true)
	{
		FailOnUnsupportedFieldValue = failOnUnsupportedField;
		return Self;
	}

	public MoreLikeThisQueryDescriptor Fields(Fields? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public MoreLikeThisQueryDescriptor Include(bool? include = true)
	{
		IncludeValue = include;
		return Self;
	}

	public MoreLikeThisQueryDescriptor Like(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like> like)
	{
		LikeValue = like;
		return Self;
	}

	public MoreLikeThisQueryDescriptor MaxDocFreq(int? maxDocFreq)
	{
		MaxDocFreqValue = maxDocFreq;
		return Self;
	}

	public MoreLikeThisQueryDescriptor MaxQueryTerms(int? maxQueryTerms)
	{
		MaxQueryTermsValue = maxQueryTerms;
		return Self;
	}

	public MoreLikeThisQueryDescriptor MaxWordLength(int? maxWordLength)
	{
		MaxWordLengthValue = maxWordLength;
		return Self;
	}

	public MoreLikeThisQueryDescriptor MinDocFreq(int? minDocFreq)
	{
		MinDocFreqValue = minDocFreq;
		return Self;
	}

	public MoreLikeThisQueryDescriptor MinTermFreq(int? minTermFreq)
	{
		MinTermFreqValue = minTermFreq;
		return Self;
	}

	public MoreLikeThisQueryDescriptor MinWordLength(int? minWordLength)
	{
		MinWordLengthValue = minWordLength;
		return Self;
	}

	public MoreLikeThisQueryDescriptor MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	public MoreLikeThisQueryDescriptor PerFieldAnalyzer(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>> selector)
	{
		PerFieldAnalyzerValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>());
		return Self;
	}

	public MoreLikeThisQueryDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing)
	{
		RoutingValue = routing;
		return Self;
	}

	public MoreLikeThisQueryDescriptor StopWords(ICollection<string>? stopWords)
	{
		StopWordsValue = stopWords;
		return Self;
	}

	public MoreLikeThisQueryDescriptor Unlike(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Like>? unlike)
	{
		UnlikeValue = unlike;
		return Self;
	}

	public MoreLikeThisQueryDescriptor Version(long? version)
	{
		VersionValue = version;
		return Self;
	}

	public MoreLikeThisQueryDescriptor VersionType(Elastic.Clients.Elasticsearch.VersionType? versionType)
	{
		VersionTypeValue = versionType;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (BoostTermsValue.HasValue)
		{
			writer.WritePropertyName("boost_terms");
			writer.WriteNumberValue(BoostTermsValue.Value);
		}

		if (FailOnUnsupportedFieldValue.HasValue)
		{
			writer.WritePropertyName("fail_on_unsupported_field");
			writer.WriteBooleanValue(FailOnUnsupportedFieldValue.Value);
		}

		if (FieldsValue is not null)
		{
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
		}

		if (IncludeValue.HasValue)
		{
			writer.WritePropertyName("include");
			writer.WriteBooleanValue(IncludeValue.Value);
		}

		writer.WritePropertyName("like");
		SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.Like>(LikeValue, writer, options);
		if (MaxDocFreqValue.HasValue)
		{
			writer.WritePropertyName("max_doc_freq");
			writer.WriteNumberValue(MaxDocFreqValue.Value);
		}

		if (MaxQueryTermsValue.HasValue)
		{
			writer.WritePropertyName("max_query_terms");
			writer.WriteNumberValue(MaxQueryTermsValue.Value);
		}

		if (MaxWordLengthValue.HasValue)
		{
			writer.WritePropertyName("max_word_length");
			writer.WriteNumberValue(MaxWordLengthValue.Value);
		}

		if (MinDocFreqValue.HasValue)
		{
			writer.WritePropertyName("min_doc_freq");
			writer.WriteNumberValue(MinDocFreqValue.Value);
		}

		if (MinTermFreqValue.HasValue)
		{
			writer.WritePropertyName("min_term_freq");
			writer.WriteNumberValue(MinTermFreqValue.Value);
		}

		if (MinWordLengthValue.HasValue)
		{
			writer.WritePropertyName("min_word_length");
			writer.WriteNumberValue(MinWordLengthValue.Value);
		}

		if (MinimumShouldMatchValue is not null)
		{
			writer.WritePropertyName("minimum_should_match");
			JsonSerializer.Serialize(writer, MinimumShouldMatchValue, options);
		}

		if (PerFieldAnalyzerValue is not null)
		{
			writer.WritePropertyName("per_field_analyzer");
			JsonSerializer.Serialize(writer, PerFieldAnalyzerValue, options);
		}

		if (RoutingValue is not null)
		{
			writer.WritePropertyName("routing");
			JsonSerializer.Serialize(writer, RoutingValue, options);
		}

		if (StopWordsValue is not null)
		{
			writer.WritePropertyName("stop_words");
			SingleOrManySerializationHelper.Serialize<string>(StopWordsValue, writer, options);
		}

		if (UnlikeValue is not null)
		{
			writer.WritePropertyName("unlike");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.Like>(UnlikeValue, writer, options);
		}

		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		if (VersionTypeValue is not null)
		{
			writer.WritePropertyName("version_type");
			JsonSerializer.Serialize(writer, VersionTypeValue, options);
		}

		writer.WriteEndObject();
	}
}