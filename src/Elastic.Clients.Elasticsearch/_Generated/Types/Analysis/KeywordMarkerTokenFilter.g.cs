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
namespace Elastic.Clients.Elasticsearch.Analysis
{
	public sealed partial class KeywordMarkerTokenFilter
	{
		[JsonInclude]
		[JsonPropertyName("ignore_case")]
		public bool? IgnoreCase { get; set; }

		[JsonInclude]
		[JsonPropertyName("keywords")]
		public IEnumerable<string>? Keywords { get; set; }

		[JsonInclude]
		[JsonPropertyName("keywords_path")]
		public string? KeywordsPath { get; set; }

		[JsonInclude]
		[JsonPropertyName("keywords_pattern")]
		public string? KeywordsPattern { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "keyword_marker";
		[JsonInclude]
		[JsonPropertyName("version")]
		public string? Version { get; set; }
	}

	public sealed partial class KeywordMarkerTokenFilterDescriptor : SerializableDescriptorBase<KeywordMarkerTokenFilterDescriptor>, IBuildableDescriptor<KeywordMarkerTokenFilter>
	{
		internal KeywordMarkerTokenFilterDescriptor(Action<KeywordMarkerTokenFilterDescriptor> configure) => configure.Invoke(this);
		public KeywordMarkerTokenFilterDescriptor() : base()
		{
		}

		private bool? IgnoreCaseValue { get; set; }

		private IEnumerable<string>? KeywordsValue { get; set; }

		private string? KeywordsPathValue { get; set; }

		private string? KeywordsPatternValue { get; set; }

		private string? VersionValue { get; set; }

		public KeywordMarkerTokenFilterDescriptor IgnoreCase(bool? ignoreCase = true)
		{
			IgnoreCaseValue = ignoreCase;
			return Self;
		}

		public KeywordMarkerTokenFilterDescriptor Keywords(IEnumerable<string>? keywords)
		{
			KeywordsValue = keywords;
			return Self;
		}

		public KeywordMarkerTokenFilterDescriptor KeywordsPath(string? keywordsPath)
		{
			KeywordsPathValue = keywordsPath;
			return Self;
		}

		public KeywordMarkerTokenFilterDescriptor KeywordsPattern(string? keywordsPattern)
		{
			KeywordsPatternValue = keywordsPattern;
			return Self;
		}

		public KeywordMarkerTokenFilterDescriptor Version(string? version)
		{
			VersionValue = version;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (IgnoreCaseValue.HasValue)
			{
				writer.WritePropertyName("ignore_case");
				writer.WriteBooleanValue(IgnoreCaseValue.Value);
			}

			if (KeywordsValue is not null)
			{
				writer.WritePropertyName("keywords");
				JsonSerializer.Serialize(writer, KeywordsValue, options);
			}

			if (!string.IsNullOrEmpty(KeywordsPathValue))
			{
				writer.WritePropertyName("keywords_path");
				writer.WriteStringValue(KeywordsPathValue);
			}

			if (!string.IsNullOrEmpty(KeywordsPatternValue))
			{
				writer.WritePropertyName("keywords_pattern");
				writer.WriteStringValue(KeywordsPatternValue);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("keyword_marker");
			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}

		KeywordMarkerTokenFilter IBuildableDescriptor<KeywordMarkerTokenFilter>.Build() => new()
		{ IgnoreCase = IgnoreCaseValue, Keywords = KeywordsValue, KeywordsPath = KeywordsPathValue, KeywordsPattern = KeywordsPatternValue, Version = VersionValue };
	}
}