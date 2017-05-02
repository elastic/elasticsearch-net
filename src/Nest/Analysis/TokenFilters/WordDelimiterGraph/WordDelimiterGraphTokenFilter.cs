using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Named word_delimiter, it Splits words into subwords and performs optional transformations on subword groups.
	/// Unlike the word_delimiter this token filter named word_delimiter_graph correctly handles multi terms expansion at query time.
	/// </summary>
	public interface IWordDelimiterGraphTokenFilter : ITokenFilter
	{
		/// <summary>
		/// If true causes parts of words to be generated: "PowerShot" ⇒ "Power" "Shot". Defaults to true.
		/// </summary>
		[JsonProperty("generate_word_parts")]
		bool? GenerateWordParts { get; set; }

		/// <summary>
		/// If true causes number subwords to be generated: "500-42" ⇒ "500" "42". Defaults to true.
		/// </summary>
		[JsonProperty("generate_number_parts")]
		bool? GenerateNumberParts { get; set; }

		/// <summary>
		/// If true causes maximum runs of word parts to be catenated: "wi-fi" ⇒ "wifi". Defaults to false.
		/// </summary>
		[JsonProperty("catenate_words")]
		bool? CatenateWords { get; set; }

		/// <summary>
		/// If true causes maximum runs of number parts to be catenated: "500-42" ⇒ "50042". Defaults to false.
		/// </summary>
		[JsonProperty("catenate_numbers")]
		bool? CatenateNumbers { get; set; }

		/// <summary>
		/// If true causes all subword parts to be catenated: "wi-fi-4000" ⇒ "wifi4000". Defaults to false.
		/// </summary>
		[JsonProperty("catenate_all")]
		bool? CatenateAll { get; set; }

		/// <summary>
		/// If true causes "PowerShot" to be two tokens; ("Power-Shot" remains two parts regards). Defaults to true.
		/// </summary>
		[JsonProperty("split_on_case_change")]
		bool? SplitOnCaseChange { get; set; }

		/// <summary>
		/// If true includes original words in subwords: "500-42" ⇒ "500-42" "500" "42". Defaults to false.
		/// </summary>
		[JsonProperty("preserve_original")]
		bool? PreserveOriginal { get; set; }

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
		///  A list of protected words from being delimiter.
		/// </summary>
		[JsonProperty("protected_words")]
		IEnumerable<string> ProtectedWords { get; set; }

		/// <summary>
		///protected_words_path which resolved to a file configured with protected words (one on each line).
		/// Automatically resolves to config/ based location if exists.
		/// </summary>
		[JsonProperty("protected_words_path ")]
		string ProtectedWordsPath { get; set; }

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

	/// <inheritdoc/>
	public class WordDelimiterGraphTokenFilter : TokenFilterBase, IWordDelimiterGraphTokenFilter
	{
		public WordDelimiterGraphTokenFilter() : base("word_delimiter_graph") { }

		/// <inheritdoc/>
		public bool? GenerateWordParts { get; set; }

		/// <inheritdoc/>
		public bool? GenerateNumberParts { get; set; }

		/// <inheritdoc/>
		public bool? CatenateWords { get; set; }

		/// <inheritdoc/>
		public bool? CatenateNumbers { get; set; }

		/// <inheritdoc/>
		public bool? CatenateAll { get; set; }

		/// <inheritdoc/>
		public bool? SplitOnCaseChange { get; set; }

		/// <inheritdoc/>
		public bool? PreserveOriginal { get; set; }

		/// <inheritdoc/>
		public bool? SplitOnNumerics { get; set; }

		/// <inheritdoc/>
		public bool? StemEnglishPossessive { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> ProtectedWords { get; set; }

		/// <inheritdoc/>
		public string ProtectedWordsPath { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> TypeTable { get; set; }

		/// <inheritdoc/>
		public string TypeTablePath { get; set; }
	}

	///<inheritdoc/>
	public class WordDelimiterGraphTokenFilterDescriptor
		: TokenFilterDescriptorBase<WordDelimiterGraphTokenFilterDescriptor, IWordDelimiterGraphTokenFilter>, IWordDelimiterGraphTokenFilter
	{
		protected override string Type => "word_delimiter_graph";

		IEnumerable<string> IWordDelimiterGraphTokenFilter.ProtectedWords { get; set; }
		string IWordDelimiterGraphTokenFilter.ProtectedWordsPath { get; set; }
		IEnumerable<string> IWordDelimiterGraphTokenFilter.TypeTable { get; set; }
		string IWordDelimiterGraphTokenFilter.TypeTablePath { get; set; }
		bool? IWordDelimiterGraphTokenFilter.GenerateWordParts { get; set; }
		bool? IWordDelimiterGraphTokenFilter.GenerateNumberParts { get; set; }
		bool? IWordDelimiterGraphTokenFilter.CatenateWords { get; set; }
		bool? IWordDelimiterGraphTokenFilter.CatenateNumbers { get; set; }
		bool? IWordDelimiterGraphTokenFilter.CatenateAll { get; set; }
		bool? IWordDelimiterGraphTokenFilter.SplitOnCaseChange { get; set; }
		bool? IWordDelimiterGraphTokenFilter.PreserveOriginal { get; set; }
		bool? IWordDelimiterGraphTokenFilter.SplitOnNumerics { get; set; }
		bool? IWordDelimiterGraphTokenFilter.StemEnglishPossessive { get; set; }

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor GenerateWordParts(bool? generateWordParts = true) => Assign(a => a.GenerateWordParts = generateWordParts);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor GenerateNumberParts(bool? generateNumberParts = true) => Assign(a => a.GenerateNumberParts = generateNumberParts);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor CatenateWords(bool? catenateWords = true) => Assign(a => a.CatenateWords = catenateWords);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor CatenateNumbers(bool? catenateNumbers = true) => Assign(a => a.CatenateNumbers = catenateNumbers);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor CatenateAll(bool? catenateAll = true) => Assign(a => a.CatenateAll = catenateAll);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor SplitOnCaseChange(bool? split = true) => Assign(a => a.SplitOnCaseChange = split);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor SplitOnNumerics(bool? split = true) => Assign(a => a.SplitOnNumerics = split);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(a => a.PreserveOriginal = preserve);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor StemEnglishPossessive(bool? stem = true) => Assign(a => a.StemEnglishPossessive = stem);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor ProtectedWords(IEnumerable<string> protectedWords) => Assign(a => a.ProtectedWords = protectedWords);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor ProtectedWords(params string[] protectedWords) => Assign(a => a.ProtectedWords = protectedWords);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor ProtectedWordsPath(string path) => Assign(a => a.ProtectedWordsPath = path);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor TypeTable(IEnumerable<string> typeTable) => Assign(a => a.TypeTable = typeTable);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor TypeTable(params string[] typeTable) => Assign(a => a.TypeTable = typeTable);

		///<inheritdoc/>
		public WordDelimiterGraphTokenFilterDescriptor TypeTablePath(string path) => Assign(a => a.TypeTablePath = path);

	}

}
