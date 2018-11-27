using System.Collections.Generic;
using System.Runtime.Serialization;

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
		[DataMember(Name ="catenate_all")]
		bool? CatenateAll { get; set; }

		/// <summary>
		/// If true causes maximum runs of number parts to be catenated: "500-42" ⇒ "50042". Defaults to false.
		/// </summary>
		[DataMember(Name ="catenate_numbers")]
		bool? CatenateNumbers { get; set; }

		/// <summary>
		/// If true causes maximum runs of word parts to be catenated: "wi-fi" ⇒ "wifi". Defaults to false.
		/// </summary>
		[DataMember(Name ="catenate_words")]
		bool? CatenateWords { get; set; }

		/// <summary>
		/// If true causes number subwords to be generated: "500-42" ⇒ "500" "42". Defaults to true.
		/// </summary>
		[DataMember(Name ="generate_number_parts")]
		bool? GenerateNumberParts { get; set; }

		/// <summary>
		/// If true causes parts of words to be generated: "PowerShot" ⇒ "Power" "Shot". Defaults to true.
		/// </summary>
		[DataMember(Name ="generate_word_parts")]
		bool? GenerateWordParts { get; set; }

		/// <summary>
		/// If true includes original words in subwords: "500-42" ⇒ "500-42" "500" "42". Defaults to false.
		/// </summary>
		[DataMember(Name ="preserve_original")]
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
		bool? SplitOnCaseChange { get; set; }

		/// <summary>
		/// If true causes "j2se" to be three tokens; "j" "2" "se". Defaults to true.
		/// </summary>
		[DataMember(Name ="split_on_numerics")]
		bool? SplitOnNumerics { get; set; }

		/// <summary>
		/// If true causes trailing "'s" to be removed for each subword: "O’Neil’s" ⇒ "O", "Neil". Defaults to true.
		/// </summary>
		[DataMember(Name ="stem_english_possessive")]
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
			Assign(a => a.GenerateWordParts = generateWordParts);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor GenerateNumberParts(bool? generateNumberParts = true) =>
			Assign(a => a.GenerateNumberParts = generateNumberParts);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor CatenateWords(bool? catenateWords = true) => Assign(a => a.CatenateWords = catenateWords);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor CatenateNumbers(bool? catenateNumbers = true) => Assign(a => a.CatenateNumbers = catenateNumbers);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor CatenateAll(bool? catenateAll = true) => Assign(a => a.CatenateAll = catenateAll);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor SplitOnCaseChange(bool? split = true) => Assign(a => a.SplitOnCaseChange = split);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor SplitOnNumerics(bool? split = true) => Assign(a => a.SplitOnNumerics = split);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(a => a.PreserveOriginal = preserve);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor StemEnglishPossessive(bool? stem = true) => Assign(a => a.StemEnglishPossessive = stem);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor ProtectedWords(IEnumerable<string> protectedWords) =>
			Assign(a => a.ProtectedWords = protectedWords);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor ProtectedWords(params string[] protectedWords) => Assign(a => a.ProtectedWords = protectedWords);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor ProtectedWordsPath(string path) => Assign(a => a.ProtectedWordsPath = path);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor TypeTable(IEnumerable<string> typeTable) => Assign(a => a.TypeTable = typeTable);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor TypeTable(params string[] typeTable) => Assign(a => a.TypeTable = typeTable);

		/// <inheritdoc />
		public WordDelimiterTokenFilterDescriptor TypeTablePath(string path) => Assign(a => a.TypeTablePath = path);
	}
}
