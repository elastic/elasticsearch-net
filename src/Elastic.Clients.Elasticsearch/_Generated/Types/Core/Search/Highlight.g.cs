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

public sealed partial class Highlight
{
	[JsonInclude, JsonPropertyName("boundary_chars")]
	public string? BoundaryChars { get; set; }
	[JsonInclude, JsonPropertyName("boundary_max_scan")]
	public int? BoundaryMaxScan { get; set; }
	[JsonInclude, JsonPropertyName("boundary_scanner")]
	public Elastic.Clients.Elasticsearch.Core.Search.BoundaryScanner? BoundaryScanner { get; set; }
	[JsonInclude, JsonPropertyName("boundary_scanner_locale")]
	public string? BoundaryScannerLocale { get; set; }
	[JsonInclude, JsonPropertyName("encoder")]
	public Elastic.Clients.Elasticsearch.Core.Search.HighlighterEncoder? Encoder { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField> Fields { get; set; }
	[JsonInclude, JsonPropertyName("force_source")]
	public bool? ForceSource { get; set; }
	[JsonInclude, JsonPropertyName("fragment_size")]
	public int? FragmentSize { get; set; }
	[JsonInclude, JsonPropertyName("fragmenter")]
	public Elastic.Clients.Elasticsearch.Core.Search.HighlighterFragmenter? Fragmenter { get; set; }
	[JsonInclude, JsonPropertyName("highlight_filter")]
	public bool? HighlightFilter { get; set; }
	[JsonInclude, JsonPropertyName("highlight_query")]
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? HighlightQuery { get; set; }
	[JsonInclude, JsonPropertyName("max_analyzed_offset")]
	public int? MaxAnalyzedOffset { get; set; }
	[JsonInclude, JsonPropertyName("max_fragment_length")]
	public int? MaxFragmentLength { get; set; }
	[JsonInclude, JsonPropertyName("no_match_size")]
	public int? NoMatchSize { get; set; }
	[JsonInclude, JsonPropertyName("number_of_fragments")]
	public int? NumberOfFragments { get; set; }
	[JsonInclude, JsonPropertyName("options")]
	public IDictionary<string, object>? Options { get; set; }
	[JsonInclude, JsonPropertyName("order")]
	public Elastic.Clients.Elasticsearch.Core.Search.HighlighterOrder? Order { get; set; }
	[JsonInclude, JsonPropertyName("phrase_limit")]
	public int? PhraseLimit { get; set; }
	[JsonInclude, JsonPropertyName("post_tags")]
	public ICollection<string>? PostTags { get; set; }
	[JsonInclude, JsonPropertyName("pre_tags")]
	public ICollection<string>? PreTags { get; set; }
	[JsonInclude, JsonPropertyName("require_field_match")]
	public bool? RequireFieldMatch { get; set; }
	[JsonInclude, JsonPropertyName("tags_schema")]
	public Elastic.Clients.Elasticsearch.Core.Search.HighlighterTagsSchema? TagsSchema { get; set; }
	[JsonInclude, JsonPropertyName("type")]
	public Elastic.Clients.Elasticsearch.Core.Search.HighlighterType? Type { get; set; }
}

public sealed partial class HighlightDescriptor<TDocument> : SerializableDescriptor<HighlightDescriptor<TDocument>>
{
	internal HighlightDescriptor(Action<HighlightDescriptor<TDocument>> configure) => configure.Invoke(this);

	public HighlightDescriptor() : base()
	{
	}

	private string? BoundaryCharsValue { get; set; }
	private int? BoundaryMaxScanValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.BoundaryScanner? BoundaryScannerValue { get; set; }
	private string? BoundaryScannerLocaleValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterEncoder? EncoderValue { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField> FieldsValue { get; set; }
	private bool? ForceSourceValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterFragmenter? FragmenterValue { get; set; }
	private int? FragmentSizeValue { get; set; }
	private bool? HighlightFilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.Query? HighlightQueryValue { get; set; }
	private QueryDsl.QueryDescriptor<TDocument> HighlightQueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor<TDocument>> HighlightQueryDescriptorAction { get; set; }
	private int? MaxAnalyzedOffsetValue { get; set; }
	private int? MaxFragmentLengthValue { get; set; }
	private int? NoMatchSizeValue { get; set; }
	private int? NumberOfFragmentsValue { get; set; }
	private IDictionary<string, object>? OptionsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterOrder? OrderValue { get; set; }
	private int? PhraseLimitValue { get; set; }
	private ICollection<string>? PostTagsValue { get; set; }
	private ICollection<string>? PreTagsValue { get; set; }
	private bool? RequireFieldMatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterTagsSchema? TagsSchemaValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterType? TypeValue { get; set; }

	public HighlightDescriptor<TDocument> BoundaryChars(string? boundaryChars)
	{
		BoundaryCharsValue = boundaryChars;
		return Self;
	}

	public HighlightDescriptor<TDocument> BoundaryMaxScan(int? boundaryMaxScan)
	{
		BoundaryMaxScanValue = boundaryMaxScan;
		return Self;
	}

	public HighlightDescriptor<TDocument> BoundaryScanner(Elastic.Clients.Elasticsearch.Core.Search.BoundaryScanner? boundaryScanner)
	{
		BoundaryScannerValue = boundaryScanner;
		return Self;
	}

	public HighlightDescriptor<TDocument> BoundaryScannerLocale(string? boundaryScannerLocale)
	{
		BoundaryScannerLocaleValue = boundaryScannerLocale;
		return Self;
	}

	public HighlightDescriptor<TDocument> Encoder(Elastic.Clients.Elasticsearch.Core.Search.HighlighterEncoder? encoder)
	{
		EncoderValue = encoder;
		return Self;
	}

	public HighlightDescriptor<TDocument> Fields(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField>> selector)
	{
		FieldsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField>());
		return Self;
	}

	public HighlightDescriptor<TDocument> ForceSource(bool? forceSource = true)
	{
		ForceSourceValue = forceSource;
		return Self;
	}

	public HighlightDescriptor<TDocument> Fragmenter(Elastic.Clients.Elasticsearch.Core.Search.HighlighterFragmenter? fragmenter)
	{
		FragmenterValue = fragmenter;
		return Self;
	}

	public HighlightDescriptor<TDocument> FragmentSize(int? fragmentSize)
	{
		FragmentSizeValue = fragmentSize;
		return Self;
	}

	public HighlightDescriptor<TDocument> HighlightFilter(bool? highlightFilter = true)
	{
		HighlightFilterValue = highlightFilter;
		return Self;
	}

	public HighlightDescriptor<TDocument> HighlightQuery(Elastic.Clients.Elasticsearch.QueryDsl.Query? highlightQuery)
	{
		HighlightQueryDescriptor = null;
		HighlightQueryDescriptorAction = null;
		HighlightQueryValue = highlightQuery;
		return Self;
	}

	public HighlightDescriptor<TDocument> HighlightQuery(QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		HighlightQueryValue = null;
		HighlightQueryDescriptorAction = null;
		HighlightQueryDescriptor = descriptor;
		return Self;
	}

	public HighlightDescriptor<TDocument> HighlightQuery(Action<QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		HighlightQueryValue = null;
		HighlightQueryDescriptor = null;
		HighlightQueryDescriptorAction = configure;
		return Self;
	}

	public HighlightDescriptor<TDocument> MaxAnalyzedOffset(int? maxAnalyzedOffset)
	{
		MaxAnalyzedOffsetValue = maxAnalyzedOffset;
		return Self;
	}

	public HighlightDescriptor<TDocument> MaxFragmentLength(int? maxFragmentLength)
	{
		MaxFragmentLengthValue = maxFragmentLength;
		return Self;
	}

	public HighlightDescriptor<TDocument> NoMatchSize(int? noMatchSize)
	{
		NoMatchSizeValue = noMatchSize;
		return Self;
	}

	public HighlightDescriptor<TDocument> NumberOfFragments(int? numberOfFragments)
	{
		NumberOfFragmentsValue = numberOfFragments;
		return Self;
	}

	public HighlightDescriptor<TDocument> Options(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		OptionsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public HighlightDescriptor<TDocument> Order(Elastic.Clients.Elasticsearch.Core.Search.HighlighterOrder? order)
	{
		OrderValue = order;
		return Self;
	}

	public HighlightDescriptor<TDocument> PhraseLimit(int? phraseLimit)
	{
		PhraseLimitValue = phraseLimit;
		return Self;
	}

	public HighlightDescriptor<TDocument> PostTags(ICollection<string>? postTags)
	{
		PostTagsValue = postTags;
		return Self;
	}

	public HighlightDescriptor<TDocument> PreTags(ICollection<string>? preTags)
	{
		PreTagsValue = preTags;
		return Self;
	}

	public HighlightDescriptor<TDocument> RequireFieldMatch(bool? requireFieldMatch = true)
	{
		RequireFieldMatchValue = requireFieldMatch;
		return Self;
	}

	public HighlightDescriptor<TDocument> TagsSchema(Elastic.Clients.Elasticsearch.Core.Search.HighlighterTagsSchema? tagsSchema)
	{
		TagsSchemaValue = tagsSchema;
		return Self;
	}

	public HighlightDescriptor<TDocument> Type(Elastic.Clients.Elasticsearch.Core.Search.HighlighterType? type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(BoundaryCharsValue))
		{
			writer.WritePropertyName("boundary_chars");
			writer.WriteStringValue(BoundaryCharsValue);
		}

		if (BoundaryMaxScanValue.HasValue)
		{
			writer.WritePropertyName("boundary_max_scan");
			writer.WriteNumberValue(BoundaryMaxScanValue.Value);
		}

		if (BoundaryScannerValue is not null)
		{
			writer.WritePropertyName("boundary_scanner");
			JsonSerializer.Serialize(writer, BoundaryScannerValue, options);
		}

		if (!string.IsNullOrEmpty(BoundaryScannerLocaleValue))
		{
			writer.WritePropertyName("boundary_scanner_locale");
			writer.WriteStringValue(BoundaryScannerLocaleValue);
		}

		if (EncoderValue is not null)
		{
			writer.WritePropertyName("encoder");
			JsonSerializer.Serialize(writer, EncoderValue, options);
		}

		writer.WritePropertyName("fields");
		JsonSerializer.Serialize(writer, FieldsValue, options);
		if (ForceSourceValue.HasValue)
		{
			writer.WritePropertyName("force_source");
			writer.WriteBooleanValue(ForceSourceValue.Value);
		}

		if (FragmenterValue is not null)
		{
			writer.WritePropertyName("fragmenter");
			JsonSerializer.Serialize(writer, FragmenterValue, options);
		}

		if (FragmentSizeValue.HasValue)
		{
			writer.WritePropertyName("fragment_size");
			writer.WriteNumberValue(FragmentSizeValue.Value);
		}

		if (HighlightFilterValue.HasValue)
		{
			writer.WritePropertyName("highlight_filter");
			writer.WriteBooleanValue(HighlightFilterValue.Value);
		}

		if (HighlightQueryDescriptor is not null)
		{
			writer.WritePropertyName("highlight_query");
			JsonSerializer.Serialize(writer, HighlightQueryDescriptor, options);
		}
		else if (HighlightQueryDescriptorAction is not null)
		{
			writer.WritePropertyName("highlight_query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor<TDocument>(HighlightQueryDescriptorAction), options);
		}
		else if (HighlightQueryValue is not null)
		{
			writer.WritePropertyName("highlight_query");
			JsonSerializer.Serialize(writer, HighlightQueryValue, options);
		}

		if (MaxAnalyzedOffsetValue.HasValue)
		{
			writer.WritePropertyName("max_analyzed_offset");
			writer.WriteNumberValue(MaxAnalyzedOffsetValue.Value);
		}

		if (MaxFragmentLengthValue.HasValue)
		{
			writer.WritePropertyName("max_fragment_length");
			writer.WriteNumberValue(MaxFragmentLengthValue.Value);
		}

		if (NoMatchSizeValue.HasValue)
		{
			writer.WritePropertyName("no_match_size");
			writer.WriteNumberValue(NoMatchSizeValue.Value);
		}

		if (NumberOfFragmentsValue.HasValue)
		{
			writer.WritePropertyName("number_of_fragments");
			writer.WriteNumberValue(NumberOfFragmentsValue.Value);
		}

		if (OptionsValue is not null)
		{
			writer.WritePropertyName("options");
			JsonSerializer.Serialize(writer, OptionsValue, options);
		}

		if (OrderValue is not null)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, OrderValue, options);
		}

		if (PhraseLimitValue.HasValue)
		{
			writer.WritePropertyName("phrase_limit");
			writer.WriteNumberValue(PhraseLimitValue.Value);
		}

		if (PostTagsValue is not null)
		{
			writer.WritePropertyName("post_tags");
			JsonSerializer.Serialize(writer, PostTagsValue, options);
		}

		if (PreTagsValue is not null)
		{
			writer.WritePropertyName("pre_tags");
			JsonSerializer.Serialize(writer, PreTagsValue, options);
		}

		if (RequireFieldMatchValue.HasValue)
		{
			writer.WritePropertyName("require_field_match");
			writer.WriteBooleanValue(RequireFieldMatchValue.Value);
		}

		if (TagsSchemaValue is not null)
		{
			writer.WritePropertyName("tags_schema");
			JsonSerializer.Serialize(writer, TagsSchemaValue, options);
		}

		if (TypeValue is not null)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, TypeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class HighlightDescriptor : SerializableDescriptor<HighlightDescriptor>
{
	internal HighlightDescriptor(Action<HighlightDescriptor> configure) => configure.Invoke(this);

	public HighlightDescriptor() : base()
	{
	}

	private string? BoundaryCharsValue { get; set; }
	private int? BoundaryMaxScanValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.BoundaryScanner? BoundaryScannerValue { get; set; }
	private string? BoundaryScannerLocaleValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterEncoder? EncoderValue { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField> FieldsValue { get; set; }
	private bool? ForceSourceValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterFragmenter? FragmenterValue { get; set; }
	private int? FragmentSizeValue { get; set; }
	private bool? HighlightFilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.Query? HighlightQueryValue { get; set; }
	private QueryDsl.QueryDescriptor HighlightQueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor> HighlightQueryDescriptorAction { get; set; }
	private int? MaxAnalyzedOffsetValue { get; set; }
	private int? MaxFragmentLengthValue { get; set; }
	private int? NoMatchSizeValue { get; set; }
	private int? NumberOfFragmentsValue { get; set; }
	private IDictionary<string, object>? OptionsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterOrder? OrderValue { get; set; }
	private int? PhraseLimitValue { get; set; }
	private ICollection<string>? PostTagsValue { get; set; }
	private ICollection<string>? PreTagsValue { get; set; }
	private bool? RequireFieldMatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterTagsSchema? TagsSchemaValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.HighlighterType? TypeValue { get; set; }

	public HighlightDescriptor BoundaryChars(string? boundaryChars)
	{
		BoundaryCharsValue = boundaryChars;
		return Self;
	}

	public HighlightDescriptor BoundaryMaxScan(int? boundaryMaxScan)
	{
		BoundaryMaxScanValue = boundaryMaxScan;
		return Self;
	}

	public HighlightDescriptor BoundaryScanner(Elastic.Clients.Elasticsearch.Core.Search.BoundaryScanner? boundaryScanner)
	{
		BoundaryScannerValue = boundaryScanner;
		return Self;
	}

	public HighlightDescriptor BoundaryScannerLocale(string? boundaryScannerLocale)
	{
		BoundaryScannerLocaleValue = boundaryScannerLocale;
		return Self;
	}

	public HighlightDescriptor Encoder(Elastic.Clients.Elasticsearch.Core.Search.HighlighterEncoder? encoder)
	{
		EncoderValue = encoder;
		return Self;
	}

	public HighlightDescriptor Fields(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField>> selector)
	{
		FieldsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.Search.HighlightField>());
		return Self;
	}

	public HighlightDescriptor ForceSource(bool? forceSource = true)
	{
		ForceSourceValue = forceSource;
		return Self;
	}

	public HighlightDescriptor Fragmenter(Elastic.Clients.Elasticsearch.Core.Search.HighlighterFragmenter? fragmenter)
	{
		FragmenterValue = fragmenter;
		return Self;
	}

	public HighlightDescriptor FragmentSize(int? fragmentSize)
	{
		FragmentSizeValue = fragmentSize;
		return Self;
	}

	public HighlightDescriptor HighlightFilter(bool? highlightFilter = true)
	{
		HighlightFilterValue = highlightFilter;
		return Self;
	}

	public HighlightDescriptor HighlightQuery(Elastic.Clients.Elasticsearch.QueryDsl.Query? highlightQuery)
	{
		HighlightQueryDescriptor = null;
		HighlightQueryDescriptorAction = null;
		HighlightQueryValue = highlightQuery;
		return Self;
	}

	public HighlightDescriptor HighlightQuery(QueryDsl.QueryDescriptor descriptor)
	{
		HighlightQueryValue = null;
		HighlightQueryDescriptorAction = null;
		HighlightQueryDescriptor = descriptor;
		return Self;
	}

	public HighlightDescriptor HighlightQuery(Action<QueryDsl.QueryDescriptor> configure)
	{
		HighlightQueryValue = null;
		HighlightQueryDescriptor = null;
		HighlightQueryDescriptorAction = configure;
		return Self;
	}

	public HighlightDescriptor MaxAnalyzedOffset(int? maxAnalyzedOffset)
	{
		MaxAnalyzedOffsetValue = maxAnalyzedOffset;
		return Self;
	}

	public HighlightDescriptor MaxFragmentLength(int? maxFragmentLength)
	{
		MaxFragmentLengthValue = maxFragmentLength;
		return Self;
	}

	public HighlightDescriptor NoMatchSize(int? noMatchSize)
	{
		NoMatchSizeValue = noMatchSize;
		return Self;
	}

	public HighlightDescriptor NumberOfFragments(int? numberOfFragments)
	{
		NumberOfFragmentsValue = numberOfFragments;
		return Self;
	}

	public HighlightDescriptor Options(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		OptionsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public HighlightDescriptor Order(Elastic.Clients.Elasticsearch.Core.Search.HighlighterOrder? order)
	{
		OrderValue = order;
		return Self;
	}

	public HighlightDescriptor PhraseLimit(int? phraseLimit)
	{
		PhraseLimitValue = phraseLimit;
		return Self;
	}

	public HighlightDescriptor PostTags(ICollection<string>? postTags)
	{
		PostTagsValue = postTags;
		return Self;
	}

	public HighlightDescriptor PreTags(ICollection<string>? preTags)
	{
		PreTagsValue = preTags;
		return Self;
	}

	public HighlightDescriptor RequireFieldMatch(bool? requireFieldMatch = true)
	{
		RequireFieldMatchValue = requireFieldMatch;
		return Self;
	}

	public HighlightDescriptor TagsSchema(Elastic.Clients.Elasticsearch.Core.Search.HighlighterTagsSchema? tagsSchema)
	{
		TagsSchemaValue = tagsSchema;
		return Self;
	}

	public HighlightDescriptor Type(Elastic.Clients.Elasticsearch.Core.Search.HighlighterType? type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(BoundaryCharsValue))
		{
			writer.WritePropertyName("boundary_chars");
			writer.WriteStringValue(BoundaryCharsValue);
		}

		if (BoundaryMaxScanValue.HasValue)
		{
			writer.WritePropertyName("boundary_max_scan");
			writer.WriteNumberValue(BoundaryMaxScanValue.Value);
		}

		if (BoundaryScannerValue is not null)
		{
			writer.WritePropertyName("boundary_scanner");
			JsonSerializer.Serialize(writer, BoundaryScannerValue, options);
		}

		if (!string.IsNullOrEmpty(BoundaryScannerLocaleValue))
		{
			writer.WritePropertyName("boundary_scanner_locale");
			writer.WriteStringValue(BoundaryScannerLocaleValue);
		}

		if (EncoderValue is not null)
		{
			writer.WritePropertyName("encoder");
			JsonSerializer.Serialize(writer, EncoderValue, options);
		}

		writer.WritePropertyName("fields");
		JsonSerializer.Serialize(writer, FieldsValue, options);
		if (ForceSourceValue.HasValue)
		{
			writer.WritePropertyName("force_source");
			writer.WriteBooleanValue(ForceSourceValue.Value);
		}

		if (FragmenterValue is not null)
		{
			writer.WritePropertyName("fragmenter");
			JsonSerializer.Serialize(writer, FragmenterValue, options);
		}

		if (FragmentSizeValue.HasValue)
		{
			writer.WritePropertyName("fragment_size");
			writer.WriteNumberValue(FragmentSizeValue.Value);
		}

		if (HighlightFilterValue.HasValue)
		{
			writer.WritePropertyName("highlight_filter");
			writer.WriteBooleanValue(HighlightFilterValue.Value);
		}

		if (HighlightQueryDescriptor is not null)
		{
			writer.WritePropertyName("highlight_query");
			JsonSerializer.Serialize(writer, HighlightQueryDescriptor, options);
		}
		else if (HighlightQueryDescriptorAction is not null)
		{
			writer.WritePropertyName("highlight_query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor(HighlightQueryDescriptorAction), options);
		}
		else if (HighlightQueryValue is not null)
		{
			writer.WritePropertyName("highlight_query");
			JsonSerializer.Serialize(writer, HighlightQueryValue, options);
		}

		if (MaxAnalyzedOffsetValue.HasValue)
		{
			writer.WritePropertyName("max_analyzed_offset");
			writer.WriteNumberValue(MaxAnalyzedOffsetValue.Value);
		}

		if (MaxFragmentLengthValue.HasValue)
		{
			writer.WritePropertyName("max_fragment_length");
			writer.WriteNumberValue(MaxFragmentLengthValue.Value);
		}

		if (NoMatchSizeValue.HasValue)
		{
			writer.WritePropertyName("no_match_size");
			writer.WriteNumberValue(NoMatchSizeValue.Value);
		}

		if (NumberOfFragmentsValue.HasValue)
		{
			writer.WritePropertyName("number_of_fragments");
			writer.WriteNumberValue(NumberOfFragmentsValue.Value);
		}

		if (OptionsValue is not null)
		{
			writer.WritePropertyName("options");
			JsonSerializer.Serialize(writer, OptionsValue, options);
		}

		if (OrderValue is not null)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, OrderValue, options);
		}

		if (PhraseLimitValue.HasValue)
		{
			writer.WritePropertyName("phrase_limit");
			writer.WriteNumberValue(PhraseLimitValue.Value);
		}

		if (PostTagsValue is not null)
		{
			writer.WritePropertyName("post_tags");
			JsonSerializer.Serialize(writer, PostTagsValue, options);
		}

		if (PreTagsValue is not null)
		{
			writer.WritePropertyName("pre_tags");
			JsonSerializer.Serialize(writer, PreTagsValue, options);
		}

		if (RequireFieldMatchValue.HasValue)
		{
			writer.WritePropertyName("require_field_match");
			writer.WriteBooleanValue(RequireFieldMatchValue.Value);
		}

		if (TagsSchemaValue is not null)
		{
			writer.WritePropertyName("tags_schema");
			JsonSerializer.Serialize(writer, TagsSchemaValue, options);
		}

		if (TypeValue is not null)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, TypeValue, options);
		}

		writer.WriteEndObject();
	}
}