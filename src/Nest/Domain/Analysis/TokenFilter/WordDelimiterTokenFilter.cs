using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{

	/// <summary>
	/// Named word_delimiter, it Splits words into subwords and performs optional transformations on subword groups.
	/// </summary>
    public class WordDelimiterTokenFilter : TokenFilterBase
    {
        public WordDelimiterTokenFilter()
            : base("word_delimiter")
        {
        }

        [JsonProperty("generate_word_parts")]
        public bool? GenerateWordParts { get; set; }

        [JsonProperty("generate_number_parts")]
		public bool? GenerateNumberParts { get; set; }

        [JsonProperty("catenate_words")]
		public bool? CatenateWords { get; set; }

        [JsonProperty("catenate_numbers")]
		public bool? CatenateNumbers { get; set; }

        [JsonProperty("catenate_all")]
		public bool? CatenateAll { get; set; }

        [JsonProperty("split_on_case_change")]
		public bool? SplitOnCaseChange { get; set; }

        [JsonProperty("preserve_original")]
		public bool? PreserveOriginal { get; set; }

        [JsonProperty("split_on_numerics")]
		public bool? SplitOnNumerics { get; set; }

        [JsonProperty("stem_english_possessive")]
		public bool? StemEnglishPossessive { get; set; }

        [JsonProperty("protected_words")]
        public IList<string> ProtectedWords { get; set; }

        [JsonProperty("protected_words_path ")]
        public string ProtectedWordsPath { get; set; }

        [JsonProperty("type_table")]
        public string TypeTable { get; set; }

        [JsonProperty("type_table_path")]
        public string TypeTablePath { get; set; }
    }
}