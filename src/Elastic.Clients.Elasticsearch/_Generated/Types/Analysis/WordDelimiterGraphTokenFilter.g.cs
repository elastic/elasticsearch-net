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
	public sealed partial class WordDelimiterGraphTokenFilter : ITokenFilterDefinition
	{
		[JsonInclude]
		[JsonPropertyName("adjust_offsets")]
		public bool? AdjustOffsets { get; set; }

		[JsonInclude]
		[JsonPropertyName("catenate_all")]
		public bool? CatenateAll { get; set; }

		[JsonInclude]
		[JsonPropertyName("catenate_numbers")]
		public bool? CatenateNumbers { get; set; }

		[JsonInclude]
		[JsonPropertyName("catenate_words")]
		public bool? CatenateWords { get; set; }

		[JsonInclude]
		[JsonPropertyName("generate_number_parts")]
		public bool? GenerateNumberParts { get; set; }

		[JsonInclude]
		[JsonPropertyName("generate_word_parts")]
		public bool? GenerateWordParts { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_keywords")]
		public bool? IgnoreKeywords { get; set; }

		[JsonInclude]
		[JsonPropertyName("preserve_original")]
		public bool? PreserveOriginal { get; set; }

		[JsonInclude]
		[JsonPropertyName("protected_words")]
		public IEnumerable<string>? ProtectedWords { get; set; }

		[JsonInclude]
		[JsonPropertyName("protected_words_path")]
		public string? ProtectedWordsPath { get; set; }

		[JsonInclude]
		[JsonPropertyName("split_on_case_change")]
		public bool? SplitOnCaseChange { get; set; }

		[JsonInclude]
		[JsonPropertyName("split_on_numerics")]
		public bool? SplitOnNumerics { get; set; }

		[JsonInclude]
		[JsonPropertyName("stem_english_possessive")]
		public bool? StemEnglishPossessive { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "word_delimiter_graph";
		[JsonInclude]
		[JsonPropertyName("type_table")]
		public IEnumerable<string>? TypeTable { get; set; }

		[JsonInclude]
		[JsonPropertyName("type_table_path")]
		public string? TypeTablePath { get; set; }

		[JsonInclude]
		[JsonPropertyName("version")]
		public string? Version { get; set; }
	}

	public sealed partial class WordDelimiterGraphTokenFilterDescriptor : SerializableDescriptorBase<WordDelimiterGraphTokenFilterDescriptor>, IBuildableDescriptor<WordDelimiterGraphTokenFilter>
	{
		internal WordDelimiterGraphTokenFilterDescriptor(Action<WordDelimiterGraphTokenFilterDescriptor> configure) => configure.Invoke(this);
		public WordDelimiterGraphTokenFilterDescriptor() : base()
		{
		}

		private bool? AdjustOffsetsValue { get; set; }

		private bool? CatenateAllValue { get; set; }

		private bool? CatenateNumbersValue { get; set; }

		private bool? CatenateWordsValue { get; set; }

		private bool? GenerateNumberPartsValue { get; set; }

		private bool? GenerateWordPartsValue { get; set; }

		private bool? IgnoreKeywordsValue { get; set; }

		private bool? PreserveOriginalValue { get; set; }

		private IEnumerable<string>? ProtectedWordsValue { get; set; }

		private string? ProtectedWordsPathValue { get; set; }

		private bool? SplitOnCaseChangeValue { get; set; }

		private bool? SplitOnNumericsValue { get; set; }

		private bool? StemEnglishPossessiveValue { get; set; }

		private IEnumerable<string>? TypeTableValue { get; set; }

		private string? TypeTablePathValue { get; set; }

		private string? VersionValue { get; set; }

		public WordDelimiterGraphTokenFilterDescriptor AdjustOffsets(bool? adjustOffsets = true)
		{
			AdjustOffsetsValue = adjustOffsets;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor CatenateAll(bool? catenateAll = true)
		{
			CatenateAllValue = catenateAll;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor CatenateNumbers(bool? catenateNumbers = true)
		{
			CatenateNumbersValue = catenateNumbers;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor CatenateWords(bool? catenateWords = true)
		{
			CatenateWordsValue = catenateWords;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor GenerateNumberParts(bool? generateNumberParts = true)
		{
			GenerateNumberPartsValue = generateNumberParts;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor GenerateWordParts(bool? generateWordParts = true)
		{
			GenerateWordPartsValue = generateWordParts;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor IgnoreKeywords(bool? ignoreKeywords = true)
		{
			IgnoreKeywordsValue = ignoreKeywords;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor PreserveOriginal(bool? preserveOriginal = true)
		{
			PreserveOriginalValue = preserveOriginal;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor ProtectedWords(IEnumerable<string>? protectedWords)
		{
			ProtectedWordsValue = protectedWords;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor ProtectedWordsPath(string? protectedWordsPath)
		{
			ProtectedWordsPathValue = protectedWordsPath;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor SplitOnCaseChange(bool? splitOnCaseChange = true)
		{
			SplitOnCaseChangeValue = splitOnCaseChange;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor SplitOnNumerics(bool? splitOnNumerics = true)
		{
			SplitOnNumericsValue = splitOnNumerics;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor StemEnglishPossessive(bool? stemEnglishPossessive = true)
		{
			StemEnglishPossessiveValue = stemEnglishPossessive;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor TypeTable(IEnumerable<string>? typeTable)
		{
			TypeTableValue = typeTable;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor TypeTablePath(string? typeTablePath)
		{
			TypeTablePathValue = typeTablePath;
			return Self;
		}

		public WordDelimiterGraphTokenFilterDescriptor Version(string? version)
		{
			VersionValue = version;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (AdjustOffsetsValue.HasValue)
			{
				writer.WritePropertyName("adjust_offsets");
				writer.WriteBooleanValue(AdjustOffsetsValue.Value);
			}

			if (CatenateAllValue.HasValue)
			{
				writer.WritePropertyName("catenate_all");
				writer.WriteBooleanValue(CatenateAllValue.Value);
			}

			if (CatenateNumbersValue.HasValue)
			{
				writer.WritePropertyName("catenate_numbers");
				writer.WriteBooleanValue(CatenateNumbersValue.Value);
			}

			if (CatenateWordsValue.HasValue)
			{
				writer.WritePropertyName("catenate_words");
				writer.WriteBooleanValue(CatenateWordsValue.Value);
			}

			if (GenerateNumberPartsValue.HasValue)
			{
				writer.WritePropertyName("generate_number_parts");
				writer.WriteBooleanValue(GenerateNumberPartsValue.Value);
			}

			if (GenerateWordPartsValue.HasValue)
			{
				writer.WritePropertyName("generate_word_parts");
				writer.WriteBooleanValue(GenerateWordPartsValue.Value);
			}

			if (IgnoreKeywordsValue.HasValue)
			{
				writer.WritePropertyName("ignore_keywords");
				writer.WriteBooleanValue(IgnoreKeywordsValue.Value);
			}

			if (PreserveOriginalValue.HasValue)
			{
				writer.WritePropertyName("preserve_original");
				writer.WriteBooleanValue(PreserveOriginalValue.Value);
			}

			if (ProtectedWordsValue is not null)
			{
				writer.WritePropertyName("protected_words");
				JsonSerializer.Serialize(writer, ProtectedWordsValue, options);
			}

			if (!string.IsNullOrEmpty(ProtectedWordsPathValue))
			{
				writer.WritePropertyName("protected_words_path");
				writer.WriteStringValue(ProtectedWordsPathValue);
			}

			if (SplitOnCaseChangeValue.HasValue)
			{
				writer.WritePropertyName("split_on_case_change");
				writer.WriteBooleanValue(SplitOnCaseChangeValue.Value);
			}

			if (SplitOnNumericsValue.HasValue)
			{
				writer.WritePropertyName("split_on_numerics");
				writer.WriteBooleanValue(SplitOnNumericsValue.Value);
			}

			if (StemEnglishPossessiveValue.HasValue)
			{
				writer.WritePropertyName("stem_english_possessive");
				writer.WriteBooleanValue(StemEnglishPossessiveValue.Value);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("word_delimiter_graph");
			if (TypeTableValue is not null)
			{
				writer.WritePropertyName("type_table");
				JsonSerializer.Serialize(writer, TypeTableValue, options);
			}

			if (!string.IsNullOrEmpty(TypeTablePathValue))
			{
				writer.WritePropertyName("type_table_path");
				writer.WriteStringValue(TypeTablePathValue);
			}

			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}

		WordDelimiterGraphTokenFilter IBuildableDescriptor<WordDelimiterGraphTokenFilter>.Build() => new()
		{ AdjustOffsets = AdjustOffsetsValue, CatenateAll = CatenateAllValue, CatenateNumbers = CatenateNumbersValue, CatenateWords = CatenateWordsValue, GenerateNumberParts = GenerateNumberPartsValue, GenerateWordParts = GenerateWordPartsValue, IgnoreKeywords = IgnoreKeywordsValue, PreserveOriginal = PreserveOriginalValue, ProtectedWords = ProtectedWordsValue, ProtectedWordsPath = ProtectedWordsPathValue, SplitOnCaseChange = SplitOnCaseChangeValue, SplitOnNumerics = SplitOnNumericsValue, StemEnglishPossessive = StemEnglishPossessiveValue, TypeTable = TypeTableValue, TypeTablePath = TypeTablePathValue, Version = VersionValue };
	}
}