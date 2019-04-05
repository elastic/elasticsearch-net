﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Named word_delimiter, it Splits words into subwords and performs optional transformations on subword groups.
	/// </summary>
	public interface IWordDelimiterTokenFilter : ITokenFilter
	{
		/// <summary>
		/// If true causes all subword parts to be catenated: "wi-fi-4000" ⇒ "wifi4000". Defaults to false.
		/// </summary>
		[JsonProperty("catenate_all")]
		bool? CatenateAll { get; set; }

		/// <summary>
		/// If true causes maximum runs of number parts to be catenated: "500-42" ⇒ "50042". Defaults to false.
		/// </summary>
		[JsonProperty("catenate_numbers")]
		bool? CatenateNumbers { get; set; }

		/// <summary>
		/// If true causes maximum runs of word parts to be catenated: "wi-fi" ⇒ "wifi". Defaults to false.
		/// </summary>
		[JsonProperty("catenate_words")]
		bool? CatenateWords { get; set; }

		/// <summary>
		/// If true causes number subwords to be generated: "500-42" ⇒ "500" "42". Defaults to true.
		/// </summary>
		[JsonProperty("generate_number_parts")]
		bool? GenerateNumberParts { get; set; }

		/// <summary>
		/// If true causes parts of words to be generated: "PowerShot" ⇒ "Power" "Shot". Defaults to true.
		/// </summary>
		[JsonProperty("generate_word_parts")]
		bool? GenerateWordParts { get; set; }

		/// <summary>
		/// If true includes original words in subwords: "500-42" ⇒ "500-42" "500" "42". Defaults to false.
		/// </summary>
		[JsonProperty("preserve_original")]
		bool? PreserveOriginal { get; set; }

		/// <summary>
		///  A list of protected words from being delimiter.
		/// </summary>
		[JsonProperty("protected_words")]
		IEnumerable<string> ProtectedWords { get; set; }

		/// <summary>
		/// protected_words_path which resolved to a file configured with protected words (one on each line).
		///  Automatically resolves to config/ based location if exists.
		/// </summary>
		[JsonProperty("protected_words_path ")]
		string ProtectedWordsPath { get; set; }

		/// <summary>
		/// If true causes "PowerShot" to be two tokens; ("Power-Shot" remains two parts regards). Defaults to true.
		/// </summary>
		[JsonProperty("split_on_case_change")]
		bool? SplitOnCaseChange { get; set; }

		/// <summary>
		/// If true causes "j2se" to be three tokens; "j" "2" "se". Defaults to true.
		/// </summary>
		[JsonProperty("split_on_numerics")]
		bool? SplitOnNumerics { get; set; }

		/// <summary>
		/// If true causes trailing "'s" to be removed for each subword: "O’Neil’s" ⇒ "O", "Neil". Defaults to true.
		/// </summary>
		[JsonProperty("stem_english_possessive")]
		bool? StemEnglishPossessive { get; set; }

		/// <summary>
		/// A custom type mapping table
		/// </summary>
		[JsonProperty("type_table")]
		IEnumerable<string> TypeTable { get; set; }

		/// <summary>
		/// A path to a custom type mapping table file
		/// </summary>
		[JsonProperty("type_table_path")]
		string TypeTablePath { get; set; }
	}

	/// <inheritdoc />
	public class WordDelimiterTokenFilter : TokenFilterBase, IWordDelimiterTokenFilter
	{
		public WordDelimiterTokenFilter() : base("word_delimiter") { }

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
	public class WordDelimiterTokenFilterDescriptor
		: TokenFilterDescriptorBase<WordDelimiterTokenFilterDescriptor, IWordDelimiterTokenFilter>, IWordDelimiterTokenFilter
	{
		protected override string Type => "word_delimiter";
		bool? IWordDelimiterTokenFilter.CatenateAll { get; set; }
		bool? IWordDelimiterTokenFilter.CatenateNumbers { get; set; }
		bool? IWordDelimiterTokenFilter.CatenateWords { get; set; }
		bool? IWordDelimiterTokenFilter.GenerateNumberParts { get; set; }
		bool? IWordDelimiterTokenFilter.GenerateWordParts { get; set; }
		bool? IWordDelimiterTokenFilter.PreserveOriginal { get; set; }

		IEnumerable<string> IWordDelimiterTokenFilter.ProtectedWords { get; set; }
		string IWordDelimiterTokenFilter.ProtectedWordsPath { get; set; }
		bool? IWordDelimiterTokenFilter.SplitOnCaseChange { get; set; }
		bool? IWordDelimiterTokenFilter.SplitOnNumerics { get; set; }
		bool? IWordDelimiterTokenFilter.StemEnglishPossessive { get; set; }
		IEnumerable<string> IWordDelimiterTokenFilter.TypeTable { get; set; }
		string IWordDelimiterTokenFilter.TypeTablePath { get; set; }

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor GenerateWordParts(bool? generateWordParts = true) =>
			Assign(generateWordParts, (a, v) => a.GenerateWordParts = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor GenerateNumberParts(bool? generateNumberParts = true) =>
			Assign(generateNumberParts, (a, v) => a.GenerateNumberParts = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor CatenateWords(bool? catenateWords = true) => Assign(catenateWords, (a, v) => a.CatenateWords = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor CatenateNumbers(bool? catenateNumbers = true) => Assign(catenateNumbers, (a, v) => a.CatenateNumbers = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor CatenateAll(bool? catenateAll = true) => Assign(catenateAll, (a, v) => a.CatenateAll = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor SplitOnCaseChange(bool? split = true) => Assign(split, (a, v) => a.SplitOnCaseChange = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor SplitOnNumerics(bool? split = true) => Assign(split, (a, v) => a.SplitOnNumerics = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(preserve, (a, v) => a.PreserveOriginal = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor StemEnglishPossessive(bool? stem = true) => Assign(stem, (a, v) => a.StemEnglishPossessive = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor ProtectedWords(IEnumerable<string> protectedWords) =>
			Assign(protectedWords, (a, v) => a.ProtectedWords = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor ProtectedWords(params string[] protectedWords) => Assign(protectedWords, (a, v) => a.ProtectedWords = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor ProtectedWordsPath(string path) => Assign(path, (a, v) => a.ProtectedWordsPath = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor TypeTable(IEnumerable<string> typeTable) => Assign(typeTable, (a, v) => a.TypeTable = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor TypeTable(params string[] typeTable) => Assign(typeTable, (a, v) => a.TypeTable = v);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor TypeTablePath(string path) => Assign(path, (a, v) => a.TypeTablePath = v);
	}
}
