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

using Elastic.Clients.Elasticsearch.Experimental;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public class AnalyzeIndexRequestParameters : RequestParameters<AnalyzeIndexRequestParameters>
	{
	}

	public partial class AnalyzeIndexRequest : PlainRequestBase<AnalyzeIndexRequestParameters>
	{
		public AnalyzeIndexRequest()
		{
		}

		public AnalyzeIndexRequest(Elastic.Clients.Elasticsearch.IndexName? index) : base(r => r.Optional("index", index))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementAnalyze;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonInclude]
		[JsonPropertyName("analyzer")]
		public string? Analyzer { get; set; }

		[JsonInclude]
		[JsonPropertyName("attributes")]
		public IEnumerable<string>? Attributes { get; set; }

		[JsonInclude]
		[JsonPropertyName("char_filter")]
		public IEnumerable<Union<string?, Elastic.Clients.Elasticsearch.Analysis.CharFilters?>>? CharFilter { get; set; }

		[JsonInclude]
		[JsonPropertyName("explain")]
		public bool? Explain { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public string? Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("filter")]
		public IEnumerable<Union<string?, Elastic.Clients.Elasticsearch.Analysis.TokenFilters?>>? Filter { get; set; }

		[JsonInclude]
		[JsonPropertyName("normalizer")]
		public string? Normalizer { get; set; }

		[JsonInclude]
		[JsonPropertyName("text")]
		public Elastic.Clients.Elasticsearch.IndexManagement.Analyze.TextToAnalyze? Text { get; set; }

		[JsonInclude]
		[JsonPropertyName("tokenizer")]
		public Union<string?, Elastic.Clients.Elasticsearch.Analysis.Tokenizers?>? Tokenizer { get; set; }
	}

	[JsonConverter(typeof(AnalyzeIndexRequestDescriptorConverter))]
	public partial class AnalyzeIndexRequestDescriptor : RequestDescriptorBase<AnalyzeIndexRequestDescriptor, AnalyzeIndexRequestParameters>
	{
		public AnalyzeIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName? index) : base(r => r.Optional("index", index))
		{
		}

		internal string? _analyzer;
		internal IEnumerable<string>? _attributes;
		internal IEnumerable<Union<string?, Elastic.Clients.Elasticsearch.Analysis.CharFilters?>>? _charFilter;
		internal bool? _explain;
		internal string? _field;
		internal IEnumerable<Union<string?, Elastic.Clients.Elasticsearch.Analysis.TokenFilters?>>? _filter;
		internal string? _normalizer;
		internal Elastic.Clients.Elasticsearch.IndexManagement.Analyze.TextToAnalyze? _text;
		internal Union<string?, Elastic.Clients.Elasticsearch.Analysis.Tokenizers?>? _tokenizer;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementAnalyze;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public AnalyzeIndexRequestDescriptor Analyzer(string? analyzer) => Assign(analyzer, (a, v) => a._analyzer = v);
		public AnalyzeIndexRequestDescriptor Attributes(IEnumerable<string>? attributes) => Assign(attributes, (a, v) => a._attributes = v);
		public AnalyzeIndexRequestDescriptor CharFilter(IEnumerable<Union<string?, Elastic.Clients.Elasticsearch.Analysis.CharFilters?>>? charFilter) => Assign(charFilter, (a, v) => a._charFilter = v);
		public AnalyzeIndexRequestDescriptor Explain(bool? explain = true) => Assign(explain, (a, v) => a._explain = v);
		public AnalyzeIndexRequestDescriptor Field(string? field) => Assign(field, (a, v) => a._field = v);
		public AnalyzeIndexRequestDescriptor Filter(IEnumerable<Union<string?, Elastic.Clients.Elasticsearch.Analysis.TokenFilters?>>? filter) => Assign(filter, (a, v) => a._filter = v);
		public AnalyzeIndexRequestDescriptor Normalizer(string? normalizer) => Assign(normalizer, (a, v) => a._normalizer = v);
		public AnalyzeIndexRequestDescriptor Text(Elastic.Clients.Elasticsearch.IndexManagement.Analyze.TextToAnalyze? text) => Assign(text, (a, v) => a._text = v);
		public AnalyzeIndexRequestDescriptor Tokenizer(Union<string?, Elastic.Clients.Elasticsearch.Analysis.Tokenizers?>? tokenizer) => Assign(tokenizer, (a, v) => a._tokenizer = v);
	}

	internal sealed class AnalyzeIndexRequestDescriptorConverter : JsonConverter<AnalyzeIndexRequestDescriptor>
	{
		public override AnalyzeIndexRequestDescriptor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, AnalyzeIndexRequestDescriptor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(value._analyzer))
			{
				writer.WritePropertyName("analyzer");
				writer.WriteStringValue(value._analyzer);
			}

			if (value._attributes is not null)
			{
				writer.WritePropertyName("attributes");
				JsonSerializer.Serialize(writer, value._attributes, options);
			}

			if (value._charFilter is not null)
			{
				writer.WritePropertyName("char_filter");
				JsonSerializer.Serialize(writer, value._charFilter, options);
			}

			if (value._explain.HasValue)
			{
				writer.WritePropertyName("explain");
				writer.WriteBooleanValue(value._explain.Value);
			}

			if (value._field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value._field, options);
			}

			if (value._filter is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, value._filter, options);
			}

			if (!string.IsNullOrEmpty(value._normalizer))
			{
				writer.WritePropertyName("normalizer");
				writer.WriteStringValue(value._normalizer);
			}

			if (value._text is not null)
			{
				writer.WritePropertyName("text");
				JsonSerializer.Serialize(writer, value._text, options);
			}

			if (value._tokenizer is not null)
			{
				writer.WritePropertyName("tokenizer");
				JsonSerializer.Serialize(writer, value._tokenizer, options);
			}

			writer.WriteEndObject();
		}
	}
}