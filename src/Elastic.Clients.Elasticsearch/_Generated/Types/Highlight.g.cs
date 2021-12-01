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
namespace Elastic.Clients.Elasticsearch
{
	public partial class Highlight
	{
		[JsonInclude]
		[JsonPropertyName("fields")]
		public Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.HighlightField> Fields { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public Elastic.Clients.Elasticsearch.HighlighterType? Type { get; set; }

		[JsonInclude]
		[JsonPropertyName("boundary_chars")]
		public string? BoundaryChars { get; set; }

		[JsonInclude]
		[JsonPropertyName("boundary_max_scan")]
		public int? BoundaryMaxScan { get; set; }

		[JsonInclude]
		[JsonPropertyName("boundary_scanner")]
		public Elastic.Clients.Elasticsearch.BoundaryScanner? BoundaryScanner { get; set; }

		[JsonInclude]
		[JsonPropertyName("boundary_scanner_locale")]
		public string? BoundaryScannerLocale { get; set; }

		[JsonInclude]
		[JsonPropertyName("encoder")]
		public Elastic.Clients.Elasticsearch.HighlighterEncoder? Encoder { get; set; }

		[JsonInclude]
		[JsonPropertyName("fragmenter")]
		public Elastic.Clients.Elasticsearch.HighlighterFragmenter? Fragmenter { get; set; }

		[JsonInclude]
		[JsonPropertyName("fragment_offset")]
		public int? FragmentOffset { get; set; }

		[JsonInclude]
		[JsonPropertyName("fragment_size")]
		public int? FragmentSize { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_fragment_length")]
		public int? MaxFragmentLength { get; set; }

		[JsonInclude]
		[JsonPropertyName("no_match_size")]
		public int? NoMatchSize { get; set; }

		[JsonInclude]
		[JsonPropertyName("number_of_fragments")]
		public int? NumberOfFragments { get; set; }

		[JsonInclude]
		[JsonPropertyName("order")]
		public Elastic.Clients.Elasticsearch.HighlighterOrder? Order { get; set; }

		[JsonInclude]
		[JsonPropertyName("post_tags")]
		public IEnumerable<string>? PostTags { get; set; }

		[JsonInclude]
		[JsonPropertyName("pre_tags")]
		public IEnumerable<string>? PreTags { get; set; }

		[JsonInclude]
		[JsonPropertyName("require_field_match")]
		public bool? RequireFieldMatch { get; set; }

		[JsonInclude]
		[JsonPropertyName("tags_schema")]
		public Elastic.Clients.Elasticsearch.HighlighterTagsSchema? TagsSchema { get; set; }

		[JsonInclude]
		[JsonPropertyName("highlight_query")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? HighlightQuery { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_analyzed_offset")]
		public Union<string?, int?>? MaxAnalyzedOffset { get; set; }
	}

	public sealed partial class HighlightDescriptor<T> : DescriptorBase<HighlightDescriptor<T>>
	{
		public HighlightDescriptor()
		{
		}

		internal HighlightDescriptor(Action<HighlightDescriptor<T>> configure) => configure.Invoke(this);
		internal Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.HighlightField> FieldsValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.HighlighterType? TypeValue { get; private set; }

		internal string? BoundaryCharsValue { get; private set; }

		internal int? BoundaryMaxScanValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.BoundaryScanner? BoundaryScannerValue { get; private set; }

		internal string? BoundaryScannerLocaleValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.HighlighterEncoder? EncoderValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.HighlighterFragmenter? FragmenterValue { get; private set; }

		internal int? FragmentOffsetValue { get; private set; }

		internal int? FragmentSizeValue { get; private set; }

		internal int? MaxFragmentLengthValue { get; private set; }

		internal int? NoMatchSizeValue { get; private set; }

		internal int? NumberOfFragmentsValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.HighlighterOrder? OrderValue { get; private set; }

		internal IEnumerable<string>? PostTagsValue { get; private set; }

		internal IEnumerable<string>? PreTagsValue { get; private set; }

		internal bool? RequireFieldMatchValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.HighlighterTagsSchema? TagsSchemaValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? HighlightQueryValue { get; private set; }

		internal Union<string?, int?>? MaxAnalyzedOffsetValue { get; private set; }

		internal QueryDsl.QueryContainerDescriptor<T> HighlightQueryDescriptor { get; private set; }

		internal Action<QueryDsl.QueryContainerDescriptor<T>> HighlightQueryDescriptorAction { get; private set; }

		public HighlightDescriptor<T> Fields(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.HighlightField>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.HighlightField>> selector) => Assign(selector, (a, v) => a.FieldsValue = v?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.HighlightField>()));
		public HighlightDescriptor<T> Type(Elastic.Clients.Elasticsearch.HighlighterType? type) => Assign(type, (a, v) => a.TypeValue = v);
		public HighlightDescriptor<T> BoundaryChars(string? boundaryChars) => Assign(boundaryChars, (a, v) => a.BoundaryCharsValue = v);
		public HighlightDescriptor<T> BoundaryMaxScan(int? boundaryMaxScan) => Assign(boundaryMaxScan, (a, v) => a.BoundaryMaxScanValue = v);
		public HighlightDescriptor<T> BoundaryScanner(Elastic.Clients.Elasticsearch.BoundaryScanner? boundaryScanner) => Assign(boundaryScanner, (a, v) => a.BoundaryScannerValue = v);
		public HighlightDescriptor<T> BoundaryScannerLocale(string? boundaryScannerLocale) => Assign(boundaryScannerLocale, (a, v) => a.BoundaryScannerLocaleValue = v);
		public HighlightDescriptor<T> Encoder(Elastic.Clients.Elasticsearch.HighlighterEncoder? encoder) => Assign(encoder, (a, v) => a.EncoderValue = v);
		public HighlightDescriptor<T> Fragmenter(Elastic.Clients.Elasticsearch.HighlighterFragmenter? fragmenter) => Assign(fragmenter, (a, v) => a.FragmenterValue = v);
		public HighlightDescriptor<T> FragmentOffset(int? fragmentOffset) => Assign(fragmentOffset, (a, v) => a.FragmentOffsetValue = v);
		public HighlightDescriptor<T> FragmentSize(int? fragmentSize) => Assign(fragmentSize, (a, v) => a.FragmentSizeValue = v);
		public HighlightDescriptor<T> MaxFragmentLength(int? maxFragmentLength) => Assign(maxFragmentLength, (a, v) => a.MaxFragmentLengthValue = v);
		public HighlightDescriptor<T> NoMatchSize(int? noMatchSize) => Assign(noMatchSize, (a, v) => a.NoMatchSizeValue = v);
		public HighlightDescriptor<T> NumberOfFragments(int? numberOfFragments) => Assign(numberOfFragments, (a, v) => a.NumberOfFragmentsValue = v);
		public HighlightDescriptor<T> Order(Elastic.Clients.Elasticsearch.HighlighterOrder? order) => Assign(order, (a, v) => a.OrderValue = v);
		public HighlightDescriptor<T> PostTags(IEnumerable<string>? postTags) => Assign(postTags, (a, v) => a.PostTagsValue = v);
		public HighlightDescriptor<T> PreTags(IEnumerable<string>? preTags) => Assign(preTags, (a, v) => a.PreTagsValue = v);
		public HighlightDescriptor<T> RequireFieldMatch(bool? requireFieldMatch = true) => Assign(requireFieldMatch, (a, v) => a.RequireFieldMatchValue = v);
		public HighlightDescriptor<T> TagsSchema(Elastic.Clients.Elasticsearch.HighlighterTagsSchema? tagsSchema) => Assign(tagsSchema, (a, v) => a.TagsSchemaValue = v);
		public HighlightDescriptor<T> HighlightQuery(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? highlightQuery)
		{
			HighlightQueryDescriptor = null;
			HighlightQueryDescriptorAction = null;
			return Assign(highlightQuery, (a, v) => a.HighlightQueryValue = v);
		}

		public HighlightDescriptor<T> HighlightQuery(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainerDescriptor<T> descriptor)
		{
			HighlightQueryValue = null;
			HighlightQueryDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.HighlightQueryDescriptor = v);
		}

		public HighlightDescriptor<T> HighlightQuery(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryContainerDescriptor<T>> configure)
		{
			HighlightQueryValue = null;
			HighlightQueryDescriptorAction = null;
			return Assign(configure, (a, v) => a.HighlightQueryDescriptorAction = v);
		}

		public HighlightDescriptor<T> MaxAnalyzedOffset(Union<string?, int?>? maxAnalyzedOffset) => Assign(maxAnalyzedOffset, (a, v) => a.MaxAnalyzedOffsetValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
			if (TypeValue is not null)
			{
				writer.WritePropertyName("type");
				JsonSerializer.Serialize(writer, TypeValue, options);
			}

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

			if (FragmenterValue is not null)
			{
				writer.WritePropertyName("fragmenter");
				JsonSerializer.Serialize(writer, FragmenterValue, options);
			}

			if (FragmentOffsetValue.HasValue)
			{
				writer.WritePropertyName("fragment_offset");
				writer.WriteNumberValue(FragmentOffsetValue.Value);
			}

			if (FragmentSizeValue.HasValue)
			{
				writer.WritePropertyName("fragment_size");
				writer.WriteNumberValue(FragmentSizeValue.Value);
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

			if (OrderValue is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, OrderValue, options);
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

			if (HighlightQueryDescriptor is not null)
			{
				writer.WritePropertyName("highlight_query");
				JsonSerializer.Serialize(writer, HighlightQueryDescriptor, options);
			}
			else if (HighlightQueryDescriptorAction is not null)
			{
				writer.WritePropertyName("highlight_query");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor<T>(HighlightQueryDescriptorAction), options);
			}
			else if (HighlightQueryValue is not null)
			{
				writer.WritePropertyName("highlight_query");
				JsonSerializer.Serialize(writer, HighlightQueryValue, options);
			}

			if (MaxAnalyzedOffsetValue is not null)
			{
				writer.WritePropertyName("max_analyzed_offset");
				JsonSerializer.Serialize(writer, MaxAnalyzedOffsetValue, options);
			}

			writer.WriteEndObject();
		}
	}
}