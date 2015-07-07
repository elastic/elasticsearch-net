using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// Token filters that allow to decompose compound words.
    /// </summary>
    public abstract class CompoundWordTokenFilter : TokenFilterBase
    {
        protected CompoundWordTokenFilter(string type)
            : base(type)
        {
        }

        /// <summary>
        /// A list of words to use.
        /// </summary>
        [JsonProperty("word_list")]
        public IEnumerable<string> WordList { get; set; }

        /// <summary>
        /// A path (either relative to config location, or absolute) to a list of words.
        /// </summary>
        [JsonProperty("word_list_path")]
        public string WordListPath { get; set; }

        /// <summary>
        /// Minimum word size.
        /// </summary>
        [JsonProperty("min_word_size")]
        public int MinWordSize { get; set; }

        /// <summary>
        /// Minimum subword size.
        /// </summary>
        [JsonProperty("min_subword_size")]
        public int MinSubwordSize { get; set; }

        /// <summary>
        /// Maximum subword size.
        /// </summary>
        [JsonProperty("max_subword_size")]
        public int MaxSubwordSize { get; set; }

        /// <summary>
        /// Only matching the longest.
        /// </summary>
        [JsonProperty("only_longest_match")]
        public bool OnlyLongestMatch { get; set; }
    }
}
