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
	public sealed partial class StopTokenFilter
	{
		[JsonInclude]
		[JsonPropertyName("ignore_case")]
		public bool? IgnoreCase { get; set; }

		[JsonInclude]
		[JsonPropertyName("remove_trailing")]
		public bool? RemoveTrailing { get; set; }

		[JsonInclude]
		[JsonPropertyName("stopwords")]
		[JsonConverter(typeof(StopWordsConverter))]
		public IEnumerable<string>? Stopwords { get; set; }

		[JsonInclude]
		[JsonPropertyName("stopwords_path")]
		public string? StopwordsPath { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "stop";
		[JsonInclude]
		[JsonPropertyName("version")]
		public string? Version { get; set; }
	}

	public sealed partial class StopTokenFilterDescriptor : SerializableDescriptorBase<StopTokenFilterDescriptor>, IBuildableDescriptor<StopTokenFilter>
	{
		internal StopTokenFilterDescriptor(Action<StopTokenFilterDescriptor> configure) => configure.Invoke(this);
		public StopTokenFilterDescriptor() : base()
		{
		}

		private bool? IgnoreCaseValue { get; set; }

		private bool? RemoveTrailingValue { get; set; }

		private IEnumerable<string>? StopwordsValue { get; set; }

		private string? StopwordsPathValue { get; set; }

		private string? VersionValue { get; set; }

		public StopTokenFilterDescriptor IgnoreCase(bool? ignoreCase = true)
		{
			IgnoreCaseValue = ignoreCase;
			return Self;
		}

		public StopTokenFilterDescriptor RemoveTrailing(bool? removeTrailing = true)
		{
			RemoveTrailingValue = removeTrailing;
			return Self;
		}

		public StopTokenFilterDescriptor Stopwords(IEnumerable<string>? stopwords)
		{
			StopwordsValue = stopwords;
			return Self;
		}

		public StopTokenFilterDescriptor StopwordsPath(string? stopwordsPath)
		{
			StopwordsPathValue = stopwordsPath;
			return Self;
		}

		public StopTokenFilterDescriptor Version(string? version)
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

			if (RemoveTrailingValue.HasValue)
			{
				writer.WritePropertyName("remove_trailing");
				writer.WriteBooleanValue(RemoveTrailingValue.Value);
			}

			if (StopwordsValue is not null)
			{
				writer.WritePropertyName("stopwords");
				SingleOrManySerializationHelper.Serialize<string>(StopwordsValue, writer, options);
			}

			if (!string.IsNullOrEmpty(StopwordsPathValue))
			{
				writer.WritePropertyName("stopwords_path");
				writer.WriteStringValue(StopwordsPathValue);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("stop");
			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}

		StopTokenFilter IBuildableDescriptor<StopTokenFilter>.Build() => new()
		{ IgnoreCase = IgnoreCaseValue, RemoveTrailing = RemoveTrailingValue, Stopwords = StopwordsValue, StopwordsPath = StopwordsPathValue, Version = VersionValue };
	}
}