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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Analysis;
public sealed partial class DictionaryDecompounderTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("hyphenation_patterns_path")]
	public string? HyphenationPatternsPath { get; set; }

	[JsonInclude, JsonPropertyName("max_subword_size")]
	public int? MaxSubwordSize { get; set; }

	[JsonInclude, JsonPropertyName("min_subword_size")]
	public int? MinSubwordSize { get; set; }

	[JsonInclude, JsonPropertyName("min_word_size")]
	public int? MinWordSize { get; set; }

	[JsonInclude, JsonPropertyName("only_longest_match")]
	public bool? OnlyLongestMatch { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "dictionary_decompounder";
	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }

	[JsonInclude, JsonPropertyName("word_list")]
	public ICollection<string>? WordList { get; set; }

	[JsonInclude, JsonPropertyName("word_list_path")]
	public string? WordListPath { get; set; }
}

public sealed partial class DictionaryDecompounderTokenFilterDescriptor : SerializableDescriptor<DictionaryDecompounderTokenFilterDescriptor>, IBuildableDescriptor<DictionaryDecompounderTokenFilter>
{
	internal DictionaryDecompounderTokenFilterDescriptor(Action<DictionaryDecompounderTokenFilterDescriptor> configure) => configure.Invoke(this);
	public DictionaryDecompounderTokenFilterDescriptor() : base()
	{
	}

	private string? HyphenationPatternsPathValue { get; set; }

	private int? MaxSubwordSizeValue { get; set; }

	private int? MinSubwordSizeValue { get; set; }

	private int? MinWordSizeValue { get; set; }

	private bool? OnlyLongestMatchValue { get; set; }

	private string? VersionValue { get; set; }

	private ICollection<string>? WordListValue { get; set; }

	private string? WordListPathValue { get; set; }

	public DictionaryDecompounderTokenFilterDescriptor HyphenationPatternsPath(string? hyphenationPatternsPath)
	{
		HyphenationPatternsPathValue = hyphenationPatternsPath;
		return Self;
	}

	public DictionaryDecompounderTokenFilterDescriptor MaxSubwordSize(int? maxSubwordSize)
	{
		MaxSubwordSizeValue = maxSubwordSize;
		return Self;
	}

	public DictionaryDecompounderTokenFilterDescriptor MinSubwordSize(int? minSubwordSize)
	{
		MinSubwordSizeValue = minSubwordSize;
		return Self;
	}

	public DictionaryDecompounderTokenFilterDescriptor MinWordSize(int? minWordSize)
	{
		MinWordSizeValue = minWordSize;
		return Self;
	}

	public DictionaryDecompounderTokenFilterDescriptor OnlyLongestMatch(bool? onlyLongestMatch = true)
	{
		OnlyLongestMatchValue = onlyLongestMatch;
		return Self;
	}

	public DictionaryDecompounderTokenFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	public DictionaryDecompounderTokenFilterDescriptor WordList(ICollection<string>? wordList)
	{
		WordListValue = wordList;
		return Self;
	}

	public DictionaryDecompounderTokenFilterDescriptor WordListPath(string? wordListPath)
	{
		WordListPathValue = wordListPath;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(HyphenationPatternsPathValue))
		{
			writer.WritePropertyName("hyphenation_patterns_path");
			writer.WriteStringValue(HyphenationPatternsPathValue);
		}

		if (MaxSubwordSizeValue.HasValue)
		{
			writer.WritePropertyName("max_subword_size");
			writer.WriteNumberValue(MaxSubwordSizeValue.Value);
		}

		if (MinSubwordSizeValue.HasValue)
		{
			writer.WritePropertyName("min_subword_size");
			writer.WriteNumberValue(MinSubwordSizeValue.Value);
		}

		if (MinWordSizeValue.HasValue)
		{
			writer.WritePropertyName("min_word_size");
			writer.WriteNumberValue(MinWordSizeValue.Value);
		}

		if (OnlyLongestMatchValue.HasValue)
		{
			writer.WritePropertyName("only_longest_match");
			writer.WriteBooleanValue(OnlyLongestMatchValue.Value);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("dictionary_decompounder");
		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		if (WordListValue is not null)
		{
			writer.WritePropertyName("word_list");
			JsonSerializer.Serialize(writer, WordListValue, options);
		}

		if (!string.IsNullOrEmpty(WordListPathValue))
		{
			writer.WritePropertyName("word_list_path");
			writer.WriteStringValue(WordListPathValue);
		}

		writer.WriteEndObject();
	}

	DictionaryDecompounderTokenFilter IBuildableDescriptor<DictionaryDecompounderTokenFilter>.Build() => new()
	{
		HyphenationPatternsPath = HyphenationPatternsPathValue,
		MaxSubwordSize = MaxSubwordSizeValue,
		MinSubwordSize = MinSubwordSizeValue,
		MinWordSize = MinWordSizeValue,
		OnlyLongestMatch = OnlyLongestMatchValue,
		Version = VersionValue,
		WordList = WordListValue,
		WordListPath = WordListPathValue
	};
}