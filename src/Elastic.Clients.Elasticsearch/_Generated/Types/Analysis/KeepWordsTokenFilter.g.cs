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
	public sealed partial class KeepWordsTokenFilter
	{
		[JsonInclude]
		[JsonPropertyName("keep_words")]
		public IEnumerable<string>? KeepWords { get; set; }

		[JsonInclude]
		[JsonPropertyName("keep_words_case")]
		public bool? KeepWordsCase { get; set; }

		[JsonInclude]
		[JsonPropertyName("keep_words_path")]
		public string? KeepWordsPath { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "keep";
		[JsonInclude]
		[JsonPropertyName("version")]
		public string? Version { get; set; }
	}

	public sealed partial class KeepWordsTokenFilterDescriptor : SerializableDescriptorBase<KeepWordsTokenFilterDescriptor>, IBuildableDescriptor<KeepWordsTokenFilter>
	{
		internal KeepWordsTokenFilterDescriptor(Action<KeepWordsTokenFilterDescriptor> configure) => configure.Invoke(this);
		public KeepWordsTokenFilterDescriptor() : base()
		{
		}

		private IEnumerable<string>? KeepWordsValue { get; set; }

		private bool? KeepWordsCaseValue { get; set; }

		private string? KeepWordsPathValue { get; set; }

		private string? VersionValue { get; set; }

		public KeepWordsTokenFilterDescriptor KeepWords(IEnumerable<string>? keepWords)
		{
			KeepWordsValue = keepWords;
			return Self;
		}

		public KeepWordsTokenFilterDescriptor KeepWordsCase(bool? keepWordsCase = true)
		{
			KeepWordsCaseValue = keepWordsCase;
			return Self;
		}

		public KeepWordsTokenFilterDescriptor KeepWordsPath(string? keepWordsPath)
		{
			KeepWordsPathValue = keepWordsPath;
			return Self;
		}

		public KeepWordsTokenFilterDescriptor Version(string? version)
		{
			VersionValue = version;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (KeepWordsValue is not null)
			{
				writer.WritePropertyName("keep_words");
				JsonSerializer.Serialize(writer, KeepWordsValue, options);
			}

			if (KeepWordsCaseValue.HasValue)
			{
				writer.WritePropertyName("keep_words_case");
				writer.WriteBooleanValue(KeepWordsCaseValue.Value);
			}

			if (!string.IsNullOrEmpty(KeepWordsPathValue))
			{
				writer.WritePropertyName("keep_words_path");
				writer.WriteStringValue(KeepWordsPathValue);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("keep");
			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}

		KeepWordsTokenFilter IBuildableDescriptor<KeepWordsTokenFilter>.Build() => new()
		{ KeepWords = KeepWordsValue, KeepWordsCase = KeepWordsCaseValue, KeepWordsPath = KeepWordsPathValue, Version = VersionValue };
	}
}