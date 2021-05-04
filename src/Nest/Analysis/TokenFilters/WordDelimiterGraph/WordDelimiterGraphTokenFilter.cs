// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Named word_delimiter, it Splits words into subwords and performs optional transformations on subword groups.
	/// Unlike the word_delimiter this token filter named word_delimiter_graph correctly handles multi terms expansion at query time.
	/// </summary>
	public interface IWordDelimiterGraphTokenFilter : ITokenFilter
	{
		/// <summary>
		/// By default, the filter tries to output subtokens with adjusted offsets to reflect their actual position in the token stream. However, when used in combination with other filters that alter the length or starting position of tokens without changing their offsets (e.g. <see cref="TrimTokenFilter"/>) this can cause tokens with illegal offsets to be emitted. Setting <see cref="AdjustOffsets"/> to false will stop <see cref="WordDelimiterGraphTokenFilter"/> from adjusting these internal offsets.
		/// </summary>
		[DataMember(Name ="adjust_offsets")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? AdjustOffsets { get; set; }

		/// <summary>
		/// If true causes all subword parts to be catenated: "wi-fi-4000" ⇒ "wifi4000". Defaults to false.
		/// </summary>
		[DataMember(Name ="catenate_all")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? CatenateAll { get; set; }

		/// <summary>
		/// If true causes maximum runs of number parts to be catenated: "500-42" ⇒ "50042". Defaults to false.
		/// </summary>
		[DataMember(Name ="catenate_numbers")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? CatenateNumbers { get; set; }

		/// <summary>
		/// If true causes maximum runs of word parts to be catenated: "wi-fi" ⇒ "wifi". Defaults to false.
		/// </summary>
		[DataMember(Name ="catenate_words")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? CatenateWords { get; set; }

		/// <summary>
		/// If true causes number subwords to be generated: "500-42" ⇒ "500" "42". Defaults to true.
		/// </summary>
		[DataMember(Name ="generate_number_parts")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? GenerateNumberParts { get; set; }

		/// <summary>
		/// If true causes parts of words to be generated: "PowerShot" ⇒ "Power" "Shot". Defaults to true.
		/// </summary>
		[DataMember(Name ="generate_word_parts")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? GenerateWordParts { get; set; }

		/// <summary>
		/// If true, the filter skips tokens with a keyword attribute of true. Defaults to false.
		/// </summary>
		[DataMember(Name = "ignore_keywords")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? IgnoreKeywords { get; set; }

		/// <summary>
		/// If true includes original words in subwords: "500-42" ⇒ "500-42" "500" "42". Defaults to false.
		/// </summary>
		[DataMember(Name ="preserve_original")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? PreserveOriginal { get; set; }

		/// <summary>
		///  A list of protected words from being delimiter.
		/// </summary>
		[DataMember(Name ="protected_words")]
		IEnumerable<string> ProtectedWords { get; set; }

		/// <summary>
		/// protected_words_path which resolved to a file configured with protected words (one on each line).
		///  Automatically resolves to config/ based location if exists.
		/// </summary>
		[DataMember(Name ="protected_words_path ")]
		string ProtectedWordsPath { get; set; }

		/// <summary>
		/// If true causes "PowerShot" to be two tokens; ("Power-Shot" remains two parts regards). Defaults to true.
		/// </summary>
		[DataMember(Name ="split_on_case_change")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? SplitOnCaseChange { get; set; }

		/// <summary>
		/// If true causes "j2se" to be three tokens; "j" "2" "se". Defaults to true.
		/// </summary>
		[DataMember(Name ="split_on_numerics")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? SplitOnNumerics { get; set; }

		/// <summary>
		/// If true causes trailing "'s" to be removed for each subword: "O’Neil’s" ⇒ "O", "Neil". Defaults to true.
		/// </summary>
		[DataMember(Name ="stem_english_possessive")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? StemEnglishPossessive { get; set; }

		/// <summary>
		/// A custom type mapping table
		/// </summary>
		[DataMember(Name ="type_table")]
		IEnumerable<string> TypeTable { get; set; }

		/// <summary>
		/// A path to a custom type mapping table file
		/// </summary>
		[DataMember(Name ="type_table_path")]
		string TypeTablePath { get; set; }
	}

	/// <inheritdoc />
	public class WordDelimiterGraphTokenFilter : TokenFilterBase, IWordDelimiterGraphTokenFilter
	{
		public WordDelimiterGraphTokenFilter() : base("word_delimiter_graph") { }

		/// <inheritdoc />
		public bool? AdjustOffsets { get; set; }

		/// <inheritdoc />
		public bool? CatenateAll { get; set; }

		/// <inheritdoc />
		public bool? CatenateNumbers { get; set; }

		/// <inheritdoc />
		public bool? CatenateWords { get; set; }

		/// <inheritdoc />
		public bool? GenerateNumberParts { get; set; }

		/// <inheritdoc />
		public bool? GenerateWordParts { get; set; }

		/// <inheritdoc />
		public bool? IgnoreKeywords { get; set; }

		/// <inheritdoc />
		public bool? PreserveOriginal { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> ProtectedWords { get; set; }

		/// <inheritdoc />
		public string ProtectedWordsPath { get; set; }

		/// <inheritdoc />
		public bool? SplitOnCaseChange { get; set; }

		/// <inheritdoc />
		public bool? SplitOnNumerics { get; set; }

		/// <inheritdoc />
		public bool? StemEnglishPossessive { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> TypeTable { get; set; }

		/// <inheritdoc />
		public string TypeTablePath { get; set; }
	}

	/// <inheritdoc />
	public class WordDelimiterGraphTokenFilterDescriptor
		: TokenFilterDescriptorBase<WordDelimiterGraphTokenFilterDescriptor, IWordDelimiterGraphTokenFilter>, IWordDelimiterGraphTokenFilter
	{
		protected override string Type => "word_delimiter_graph";
		bool? IWordDelimiterGraphTokenFilter.AdjustOffsets { get; set; }
		bool? IWordDelimiterGraphTokenFilter.CatenateAll { get; set; }
		bool? IWordDelimiterGraphTokenFilter.CatenateNumbers { get; set; }
		bool? IWordDelimiterGraphTokenFilter.CatenateWords { get; set; }
		bool? IWordDelimiterGraphTokenFilter.GenerateNumberParts { get; set; }
		bool? IWordDelimiterGraphTokenFilter.GenerateWordParts { get; set; }
		bool? IWordDelimiterGraphTokenFilter.IgnoreKeywords { get; set; }
		bool? IWordDelimiterGraphTokenFilter.PreserveOriginal { get; set; }
		IEnumerable<string> IWordDelimiterGraphTokenFilter.ProtectedWords { get; set; }
		string IWordDelimiterGraphTokenFilter.ProtectedWordsPath { get; set; }
		bool? IWordDelimiterGraphTokenFilter.SplitOnCaseChange { get; set; }
		bool? IWordDelimiterGraphTokenFilter.SplitOnNumerics { get; set; }
		bool? IWordDelimiterGraphTokenFilter.StemEnglishPossessive { get; set; }
		IEnumerable<string> IWordDelimiterGraphTokenFilter.TypeTable { get; set; }
		string IWordDelimiterGraphTokenFilter.TypeTablePath { get; set; }

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor GenerateWordParts(bool? generateWordParts = true) =>
			Assign(generateWordParts, (a, v) => a.GenerateWordParts = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor GenerateNumberParts(bool? generateNumberParts = true) =>
			Assign(generateNumberParts, (a, v) => a.GenerateNumberParts = v);

		/// <summary>
		/// <para>Configure whether the filter will skip tokens with a keyword attribute of true.</para>
		/// <para>(Optional) When not configured, this defaults to false in Elasticsearch.</para>
		/// </summary>
		/// <param name="ignoreKeywords">If true, the filter skips tokens with a keyword attribute of true.</param>
		public WordDelimiterGraphTokenFilterDescriptor IgnoreKeywords(bool? ignoreKeywords = true) =>
			Assign(ignoreKeywords, (a, v) => a.IgnoreKeywords = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor CatenateWords(bool? catenateWords = true) => Assign(catenateWords, (a, v) => a.CatenateWords = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor CatenateNumbers(bool? catenateNumbers = true) =>
			Assign(catenateNumbers, (a, v) => a.CatenateNumbers = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor AdjustOffsets(bool? adjustOffsets = true) => Assign(adjustOffsets, (a, v) => a.AdjustOffsets = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor CatenateAll(bool? catenateAll = true) => Assign(catenateAll, (a, v) => a.CatenateAll = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor SplitOnCaseChange(bool? split = true) => Assign(split, (a, v) => a.SplitOnCaseChange = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor SplitOnNumerics(bool? split = true) => Assign(split, (a, v) => a.SplitOnNumerics = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(preserve, (a, v) => a.PreserveOriginal = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor StemEnglishPossessive(bool? stem = true) => Assign(stem, (a, v) => a.StemEnglishPossessive = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor ProtectedWords(IEnumerable<string> protectedWords) =>
			Assign(protectedWords, (a, v) => a.ProtectedWords = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor ProtectedWords(params string[] protectedWords) =>
			Assign(protectedWords, (a, v) => a.ProtectedWords = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor ProtectedWordsPath(string path) => Assign(path, (a, v) => a.ProtectedWordsPath = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor TypeTable(IEnumerable<string> typeTable) => Assign(typeTable, (a, v) => a.TypeTable = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor TypeTable(params string[] typeTable) => Assign(typeTable, (a, v) => a.TypeTable = v);

		/// <inheritdoc />
		public WordDelimiterGraphTokenFilterDescriptor TypeTablePath(string path) => Assign(path, (a, v) => a.TypeTablePath = v);
	}
}
